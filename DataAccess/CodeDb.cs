using Dapper;
using DataAccess.Common;
using DataAccess.Entities;
using DataAccess.Enum;
using DataAccess.Models;
using DataAccess.Util;
using Modelo.v2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CodeDb : BaseDapper
    {
        public CodeDb()
        {

        }

        public async Task<Codes> FindCodeAsync(string code)
        {
            string query = "SELECT Uid, CreatedDate, Code, Type FROM Codes WHERE Code = @Code";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Code", code);

            return await this.QuerySingleOrDefaultAsync<Codes>(query, parameters);
        }
        public Task<Guid?> Add(CodeType type, string code = "")
        {
            return this.InsertAsync(new Codes() { Code = code, Type = type });
        }

        #region User Network Code
        public async Task<UserCodeNetwork> GetByCodeUid(Guid codeUid)
        {
            string query = "SELECT Uid, UserUid, CodeUid, OwnerCodeUid, CreatedDate, Activations FROM UserCodeNetwork WHERE CodeUid = @CodeUid;";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CodeUid", codeUid);

            return await this.QuerySingleOrDefaultAsync<UserCodeNetwork>(query, parameters);
        }

        public async Task<UserCodeNetwork> GetByUserUid(Guid userUid)
        {
            string query = "SELECT Uid, UserUid, CodeUid, OwnerCodeUid, CreatedDate, Activations FROM UserCodeNetwork WHERE UserUid = @UserUid;";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@UserUid", userUid);

            return await this.QuerySingleOrDefaultAsync<UserCodeNetwork>(query, parameters);
        }

        public async Task<UserCodeNetworkConfig> GetConfigAsync()
        {
            return await this.QuerySingleOrDefaultAsync<UserCodeNetworkConfig>("SELECT TOP 1 Uid, ExpirationDate, UserShareLimit, GlobalShareLimit, OwnerRewardType, OwnerRewardValueType, OwnerRewardValue, GuestRewardType, GuestRewardValueType, GuestRewardValue FROM UserCodeNetworkConfig", null);
        }

        public async Task<UserCodeNetworkGlobalStats> GetGlobalStats()
        {
            string query = @"
SELECT SUM(Activations) as activations
FROM UserCodeNetwork";
            return await this.QuerySingleOrDefaultAsync<UserCodeNetworkGlobalStats>(query, null);
        }

        public int ReferenceUserNetworkCode(Guid uid, Guid ownerUid)
        {
            string query = "UPDATE UserCodeNetwork SET OwnerCodeUid = @OwnerUid WHERE Uid = @Uid;";
            query += " UPDATE UserCodeNetwork SET Activations = (Activations + 1) WHERE Uid = @OwnerUid;";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Uid", uid);
            parameters.Add("@OwnerUid", ownerUid);

            return this.Execute(query, parameters);
        }

        public Task<UserCodeNetworkPurchaseDetail> GetPurchaseDetailAsync(Guid uid)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PurchaseUid", uid); ;

            return this.QuerySingleOrDefaultAsync<UserCodeNetworkPurchaseDetail>("sp_UserNetworkCode_GetPurchaseDetail", parameters, System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// Registrar la recompensa del usuario
        /// </summary>
        /// <param name="uidUserNetworkCode">Uid del codigo del usuario que recibe recompensa</param>
        /// <param name="uidShareUserNetworkCode">Uid del codigo del usuario que esta brindando la recompensa a partir de su compra</param>
        /// <param name="uidUser">Uid del usuario que recibe la recompensa</param>
        /// <param name="rewardType">Tipo de recompensa</param>
        /// <param name="amount">Monto de la recompensa</param>
        /// <returns></returns>
        public async Task<bool> RegistryUserCodeNetworkReward(Guid uidUserNetworkCode, Guid? uidShareUserNetworkCode, Guid uidUser, CodeRewardType rewardType, decimal amount)
        {
            DateTime Now = DateTimeUtils.GetTimeZoneDate();

            this.BeginTransaction();
            try
            {
                string query = string.Empty;
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserCodeUid", uidUserNetworkCode);
                parameters.Add("@UserUid", uidUser);
                parameters.Add("@RewardAmount", amount);


                if (rewardType == CodeRewardType.WalletAmount)
                {
                    // Actualizar monedero
                    query = "UPDATE Monedero SET MMonto = (MMonto + @RewardAmount) WHERE UidUsuario = @UserUid;";
                    this.Execute(query, parameters);

                    // Registrar movimiento monedero
                    query = @"
                INSERT INTO Movimientos (UidMovimiento,DtmFechaRegistro,LngFolio,UidTipoDeMovimiento,UidConcepto,UidMonedero,MMonto)
                SELECT 
                    NEWID(),
                    GETDATE(),
                    (SELECT ISNULL(MAX(LngFolio) + 1,1) FROM Movimientos WHERE UidMonedero = M.UidMonedero),
                    'E85F0486-1FBE-494C-86A2-BFDDC733CA5D',
                    '8565A18B-FD65-4B18-9B9F-2154147D3179',
                    M.UidMonedero,
                    @RewardAmount
                FROM Monedero AS M WHERE M.UidUsuario = @UserUid;";
                    this.Execute(query, parameters);

                    query = "update UserCodeNetwork set Rewards = (Rewards + 1) where Uid = @UserCodeUid;";
                    this.Execute(query, parameters);

                    await this.InsertAsync(new UserCodeNetworkLog()
                    {
                        UserCodeNetworkUid = uidUserNetworkCode,
                        Type = UserCodeNetworkLogType.Rewarded,
                        SharedCodeNetworkUid = uidShareUserNetworkCode,
                        Amount = amount,
                        CreatedDate = Now
                    });

                    if (uidShareUserNetworkCode.HasValue)
                    {
                        await this.InsertAsync(new UserCodeNetworkLog()
                        {
                            UserCodeNetworkUid = uidShareUserNetworkCode.Value,
                            Type = UserCodeNetworkLogType.Give,
                            SharedCodeNetworkUid = uidUserNetworkCode,
                            Amount = amount,
                            CreatedDate = Now
                        });
                    }
                }

                this.CommitTransaction();
                return true;
            }
            catch (Exception)
            {
                this.RollbackTransaction();
                return false;
            }
        }

        public ApplyPurchaseReward GetPurchaseInfoToApplyReward(Guid uidOrden)
        {
            string query = @"
select os.UidOrden      AS UidPurchase,
       ou.UidUsuario    AS UidUser
from OrdenSucursal os
         inner join OrdenUsuario ou on ou.UidOrden = os.UidOrden
where os.UidRelacionOrdenSucursal = @ordenUid";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ordenUid", uidOrden);

            return this.QuerySingleOrDefault<ApplyPurchaseReward>(query, parameters);
        }
        #endregion

        #region User
        public UserDetails GetUser(Guid uid)
        {
            string query = @"
select u.UidUsuario                                      as [Uid],
       u.Usuario                                         as [Username],
       u.Nombre                                          as [Name],
       concat(u.ApellidoPaterno, ' ', u.ApellidoMaterno) as [LastName],
       e.Correo                                          as [Email],
       p.Numero                                          as [Phone],
       u.Lang
from Usuarios u
         left join CorreoElectronico as e
                   on e.IdCorreo = (select top 1 t.UidCorreo from CorreoUsuario t where t.UidUsuario = u.UidUsuario)
         left join Telefono p
                   on p.UidTelefono =
                      (select top 1 t.UidTelefono from TelefonoUsuario t where t.UidUsuario = u.UidUsuario)
where u.UidUsuario = @Uid";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Uid", uid);

            return this.QuerySingleOrDefault<UserDetails>(query, parameters);
        }
        #endregion

        #region View Resources
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"> 1=>Suministradora, 2=>Distribuidora </param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public IEnumerable<ListboxView> ReadAllCompanies(int type, Guid? uid = null)
        {
            string query = $@"
select UidEmpresa as Uid, NombreComercial as Name
from Empresa
where IdTipoDeEmpresa = @Type
  and IdEstatus = 1
  {(uid.HasValue ? "and UidEmpresa = @Uid" : "")}
order by NombreComercial";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Type", type);
            parameters.Add("@Uid", uid);

            return this.Query<ListboxView>(query, parameters);
        }

        public IEnumerable<ListboxView> ReadAllCompanyBranches(Guid uid)
        {
            string query = $@"select UidSucursal as Uid, Identificador as Name from Sucursales where UidEmpresa = @Uid and IntEstatus = 1 order by Identificador";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Uid", uid);

            return this.Query<ListboxView>(query, parameters);
        }

        public IEnumerable<ListboxView> ReadAllCompanyBranchProducts(Guid uid)
        {
            string query = $@"
select p.UidProducto as Uid,
       p.VchNombre   as Name
from Productos p
         inner join GiroProducto gp on gp.UidProducto = p.UidProducto
         inner join GiroSucursal gs on gs.UidGiro = gp.UidGiro
         inner join Giro g on g.UidGiro = gp.UidGiro
where p.intEstatus = 1
  and g.intEstatus = 1
  and gs.UidSucursal = @Uid
group by p.UidProducto, p.VchNombre";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Uid", uid);

            return this.Query<ListboxView>(query, parameters);
        }

        public IEnumerable<ListboxView> ReadAllCompanyProducts(Guid uid)
        {
            string query = $@"
select p.UidProducto as Uid,
       p.VchNombre   as Name
from Productos p
         inner join GiroProducto gp on gp.UidProducto = p.UidProducto
         inner join Giro g on g.UidGiro = gp.UidGiro
where p.intEstatus = 1
  and g.intEstatus = 1
  and p.UidEmpresa = @Uid
group by p.UidProducto, p.VchNombre
";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Uid", uid);

            return this.Query<ListboxView>(query, parameters);
        }
        #endregion

        #region Promotion Code
        public bool AddPromotionCode(
            PromotionCodeLevel codeLevel,
            string code,
            CodeRewardType rewardType,
            CodeRewardValueType valueType,
            decimal value,
            PromotionCodeActivationType activationType,
            CodeExpirationType expirationType,
            DateTime startAt,
            DateTime? expiredAt = null,
            int? expirationValue = null,
            PromotionCodeBusiness company = null,
            PromotionCodeBusiness deliveryCompany = null,
            PromotionCodeGeography geography = null,
            IEnumerable<PromotionCodeRuleView> rules = null)
        {
            this.BeginTransaction();

            try
            {

                Codes cCode = new Codes()
                {
                    Code = code,
                    Type = CodeType.PromotionCode
                };
                var cUid = this.Insert(cCode);

                if (!cUid.HasValue) throw new Exception("Cant create code");

                PromotionCodeExpiration codeExpiration = new PromotionCodeExpiration()
                {
                    Type = expirationType,
                    StartAt = startAt,
                    ExpirationDate = expirationType == CodeExpirationType.Date ? (DateTime?)expiredAt : null,
                    ActivationsLimit = expirationType == CodeExpirationType.Activations ? (int?)expirationValue : null,
                    DaysAfterActivation = expirationType == CodeExpirationType.DaysBeforeActivations ? (int?)expirationValue : null,
                };
                var eUid = this.Insert(codeExpiration);

                if (!cUid.HasValue) throw new Exception("Cant create expiration code");

                PromotionCode pCode = new PromotionCode()
                {
                    Activations = 0,
                    ActivationType = activationType,
                    CodeUid = cUid.Value,
                    ExpirationUid = eUid,
                    RewardType = rewardType,
                    Value = value,
                    ValueType = valueType,
                    Level = codeLevel
                };
                var pUid = this.Insert(pCode);

                if (!pUid.HasValue) throw new Exception("Cant create promotion code");

                if (geography != null)
                {
                    PromotionCodeRegion codeRegion = new PromotionCodeRegion()
                    {
                        PromotionCodeUid = pUid.Value,
                        CountryUid = geography.CountryUid.Value,
                        StateUid = geography.StateUid,
                        MunicipalityUid = geography.MunicipalityUid,
                        CityUid = geography.CityUid,
                        NeighborhoodUid = geography.NeighborhoodUid
                    };

                    this.Insert(codeRegion);
                }

                if (company != null)
                {
                    PromotionCodeCompany codeCompany = new PromotionCodeCompany()
                    {
                        PromotionCodeUid = pUid.Value,
                        CompanyUid = company.UidCompany.Value,
                        BranchUid = company.UidCompanyBranch
                    };

                    this.Insert(codeCompany);
                }

                if (deliveryCompany != null)
                {
                    PromotionCodeDeliveryCompany codeDeliveryCompany = new PromotionCodeDeliveryCompany()
                    {
                        PromotionCodeUid = pUid.Value,
                        CompanyUid = deliveryCompany.UidCompany.Value,
                        BranchUid = deliveryCompany.UidCompanyBranch
                    };
                    this.Insert(codeDeliveryCompany);
                }

                if (rules != null)
                {
                    foreach (PromotionCodeRuleView rule in rules)
                    {
                        PromotionCodeRule eRule = new PromotionCodeRule()
                        {
                            Value = rule.Value,
                            PromotionCodeUid = pUid.Value,
                            Operator = (ComparisonOperator)rule.Operator,
                            Position = rule.Position,
                            Union = LogicalOperator.And,
                            ValueType = (PromotionCodeRuleValueType)rule.ValueType
                        };

                        this.Insert(eRule);
                    }
                }

                this.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                this.RollbackTransaction();

                throw;
            }
        }

        public IEnumerable<PromotionCodeGridView> ReadAllPromotionCodes()
        {
            string query = @"
select pc.Uid,
       pc.CreatedAt,
       ex.StartAt,
       c.Code,
       pc.Activations,
       case pc.Level
           when 0 then 'Region'
           when 1 then 'Distribuidora'
           when 2 then 'Suministradora'
           end as Level,
       case ex.Type
           when 0 then 'Ninguna'
           when 1 then cast(ex.ExpirationDate as varchar)
           when 2 then concat('', ex.ActivationsLimit, ' activaciones')
           when 3 then concat('', ex.DaysAfterActivation, ' dias despues de activacion')
           end as Expiration
from Codes c
         inner join PromotionCodes pc on pc.CodeUid = c.Uid
         inner join PromotionCodeExpirations ex on ex.Uid = pc.ExpirationUid";

            return this.Query<PromotionCodeGridView>(query, null);
        }

        public PromotionCodeEditView GetPromotionCode(Guid uid)
        {
            string query = @"
select pc.Uid,
       pc.Level,
       pc.RewardType,
       pc.ValueType,
       pc.Value,
       pc.ActivationType,
       pc.Activations,
       pc.CreatedAt,
       pc.Status,

       c.Uid                                                                   as CodeUid,
       c.Code,

       ex.Uid                                                                  as ExpirationUid,
       ex.StartAt,
       ex.Type                                                                 as ExpirationType,
       ex.ExpirationDate,
       ex.ActivationsLimit,
       ex.DaysAfterActivation,

       cr.Uid                                                                  as CodeRegionUid,
       cr.CountryUid,
       isnull(cp.Nombre, '')                                                   as Country,
       cr.StateUid,
       isnull(ce.Nombre, '')                                                   as State,
       cr.MunicipalityUid,
       isnull(cm.Nombre, '')                                                   as Municipality,
       cr.CityUid,
       isnull(cc.Nombre, '')                                                   as City,
       cr.NeighborhoodUid,
       isnull(ccl.Nombre, '')                                                  as Neighborhood,

       cs.Uid                                                                  as CodeCompanyUid,
       cs.CompanyUid                                                           as CompanyUid,
       (select NombreComercial from Empresa where UidEmpresa = cs.CompanyUid)  as Company,
       cs.BranchUid                                                            as CompanyBranchUid,
       (select Identificador from Sucursales where UidSucursal = cs.BranchUid) as CompanyBranch,

       cd.Uid                                                                  as CodeDeliveryCompanyUid,
       cd.CompanyUid                                                           as DeliveryCompanyUid,
       (select NombreComercial from Empresa where UidEmpresa = cd.CompanyUid)  as DeliveryCompany,
       cd.BranchUid                                                            as DeliveryCompanyBranchUid,
       (select Identificador from Sucursales where UidSucursal = cd.BranchUid) as DeliveryCompanyBranch
from PromotionCodes pc
         inner join Codes c on c.Uid = pc.CodeUid
         inner join PromotionCodeExpirations ex on ex.Uid = pc.ExpirationUid
         left join PromotionCodeRegions cr on cr.PromotionCodeUid = pc.Uid
         left join Paises cp on cp.UidPais = cr.CountryUid
         left join estados ce on ce.UidEstado = cr.StateUid
         left join Municipios cm on cm.UidMunicipio = cr.MunicipalityUid
         left join Ciudades cc on cc.UidCiudad = cr.CityUid
         left join Colonia ccl on ccl.UidColonia = cr.NeighborhoodUid
         left join PromotionCodeCompanies cs on cs.PromotionCodeUid = pc.Uid
         left join PromotionCodeDeliveryCompanies cd on cd.PromotionCodeUid = pc.Uid
where pc.Uid = @Uid";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Uid", uid);

            return this.QuerySingleOrDefault<PromotionCodeEditView>(query, parameters);
        }
        #endregion
    }
}
