using Dapper;
using DataAccess.Common;
using DataAccess.Entities;
using DataAccess.Enum;
using DataAccess.Models;
using DataAccess.Util;
using System;
using System.Collections.Generic;
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
        #endregion
    }
}
