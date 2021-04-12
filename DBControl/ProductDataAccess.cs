using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBControl
{
    /// <summary>
    /// Acceso de datos para la tabla Productos
    /// </summary>
    public class ProductDataAccess
    {
        /// <summary>
        /// Conexion a la base de datos
        /// </summary>
        protected readonly Conexion dbConexion;

        public ProductDataAccess()
        {
            this.dbConexion = new Conexion();
        }

        public DataTable ReadAllStore(int pageSize, int pageNumber, string sortField, string sortOrder, Guid uidEstado, Guid uidColonia, string dia, string tipoFiltro, Guid uidFiltro, string filtro = null, Guid? uidSeccion = null, Guid? uidOferta = null, Guid? uidEmpresa = null)
        {

            string filterJoin = string.Empty;
            string filterWhere = string.Empty;
            string where = string.Empty;

            if (tipoFiltro.Equals("Giro"))
            {
                filterJoin = "INNER JOIN [GiroProducto] as GP ON GP.UidProducto = P.UidProducto";
                filterWhere = "AND GP.UidGiro = @UidFilter";
            }
            else if (tipoFiltro.Equals("Categoria"))
            {
                filterJoin = "INNER JOIN [CategoriaProducto] as CP ON CP.UidProducto = P.UidProducto";
                filterWhere = "AND CP.UidCategoria = @UidFilter";
            }
            else if (tipoFiltro.Equals("Subcategoria"))
            {
                filterJoin = "INNER JOIN [SubcategoriaProducto] as SCP ON SCP.UidProducto = P.UidProducto";
                filterWhere = "AND SCP.UidSubcategoria = @UidFilter";
            }

            string order = (string.IsNullOrEmpty(sortField) || string.IsNullOrEmpty(sortOrder)) ? " UID DESC " : $"{sortField} {sortOrder.ToUpper()}";

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;

            command.Parameters.AddWithValue("@pageSize", pageSize);
            command.Parameters.AddWithValue("@pageNumber", pageNumber);
            command.Parameters.AddWithValue("@UidColonia", uidColonia);
            command.Parameters.AddWithValue("@UidEstado", uidEstado);
            command.Parameters.AddWithValue("@Dia", dia);
            command.Parameters.AddWithValue("@FilterType", tipoFiltro);
            command.Parameters.AddWithValue("@UidFilter", uidFiltro);

            if (!string.IsNullOrEmpty(filtro))
            {
                command.Parameters.AddWithValue("@Filter", filtro);
                where += " and p.VchNombre like '%'+@Filter+'%' ";
            }

            if (uidSeccion.HasValue)
            {
                command.Parameters.AddWithValue("@UidSeccion", uidSeccion);
                where += " and se.UidSeccion = @UidSeccion ";
            }

            if (uidOferta.HasValue)
            {
                command.Parameters.AddWithValue("@UidOferta", uidOferta);
                where += " and o.UidOferta = @UidOferta ";
            }

            if (uidEmpresa.HasValue)
            {
                command.Parameters.AddWithValue("@UidEmpresa", uidEmpresa);
                where += " and e.UidEmpresa = @UidEmpresa ";
            }


            string query = $@"
            -- Zona horaria del usuario acorde al estado
            DECLARE @TimeZone VARCHAR(50);
            -- Fecha y Hora local del usuario
            DECLARE @UserDateTime DATETIME;
            -- Hora actual del usuario
            DECLARE @UserTime VARCHAR(20);

            -- Obtener zona horaria del estado
            SELECT
                @TimeZone = Z.IdZonaHoraria
            FROM [ZonaHoraria] AS Z
                INNER JOIN [ZonaHorariaPais] AS P ON P.[IdZonaHoraria] = Z.[IdZonaHoraria]
                INNER JOIN [ZonaHorariaEstado] AS E ON E.[UidRelacionZonaPaisEstado] = P.[UidZonaHorariaPais]
            WHERE E.UidEstado = @UidEstado

            -- Obtener DateTime del la zona horaria
            SELECT @UserDateTime = SYSDATETIMEOFFSET() AT TIME ZONE @TimeZone 

            -- Obtener Time del DateTime
            SELECT @UserTime = CONVERT(VARCHAR, @UserDateTime, 8)

            SELECT *, [Count] = COUNT (*) OVER() FROM (
                SELECT 
                    DISTINCT 
                    p.uidproducto AS [Uid], 
                    i.NVchRuta AS [ImgUrl],
                    (select top 1 i.NVchRuta from imagenes i inner join imagenempresa ie on ie.uidImagen = i.uidimagen where ie.uidempresa = e.uidempresa and i.NVchRuta like'%FotoPerfil%') as [CompanyImgUrl],  
                    p.VchNombre AS [Name], 
                    p.VchDescripcion AS [Description],
                    e.UidEmpresa AS [UidCompany],
                    e.NombreComercial AS [CompanyName],
                    dbo.ObtenerMenorPrecioPorProducto(@FilterType,@Dia,@UidFilter,p.uidproducto,@UidColonia,@UidEstado) AS [Price]
                FROM [Productos] p 
	                INNER JOIN ImagenProducto ip on ip.UidProducto = p.UidProducto 
	                INNER JOIN Imagenes i on ip.UidImagen = i.UIdImagen 
	                INNER JOIN SeccionProducto sp on sp.UidProducto = p.UidProducto 
	                INNER JOIN Seccion se on se.UidSeccion = sp.UidSeccion 
	                INNER JOIN Oferta O on O.UidOferta = se.UidOferta 
	                INNER JOIN DiaOferta DO on DO.UidOferta = O.UidOferta 
	                INNER JOIN Dias D on D.UidDia = DO.UidDia 
	                INNER JOIN Sucursales s on s.UidSucursal = o.Uidsucursal 
	                INNER JOIN ContratoDeServicio CDS on CDS.UidSucursalSuministradora = s.UidSucursal 
	                INNER JOIN turnosuministradora ts on ts.uidsucursal = CDS.UidSucursalSuministradora and ts.dtmhorafin is null
	                INNER JOIN TurnoDistribuidora td on td.UidSucursal = CDS.UidSucursalDistribuidora and td.DtmHoraFin is null
	                INNER JOIN ZonaDeRepartoDeContrato ZDRC on ZDRC.UidContrato = CDS.UidContrato 
	                INNER JOIN Tarifario t on t.UidRegistroTarifario = ZDRC.UidTarifario 
	                INNER JOIN ZonaDeServicio zd on zd.UidColonia = @UidColonia and zd.UidRelacionZonaServicio = t.UidRelacionZonaEntrega 
	                INNER JOIN Empresa e on e.UidEmpresa = p.UidEmpresa 
                    {filterJoin}
                WHERE 
                    @UserTime between se.VchHoraInicio and se.VchHoraFin
	                and zd.UidColonia = @UidColonia 
	                and p.IntEstatus = 1 and D.VchNombre = @Dia and O.IntEstatus = 1 and se.IntEstatus = 1 
	                and sp.VchTiempoElaboracion is not null and CDS.UidEstatusContrato = 'CD20F9BF-EBA2-4128-88FB-647544457B2D'
	                and s.UidSucursal in ( select s.UidSucursal from Sucursales s inner join Direccion d on d.UidDireccion = s.UidDireccion inner join ZonaHorariaEstado ZHE on ZHE.UidEstado = d.UidEstado	inner join ZonaHorariaPais ZHP on ZHP.UidZonaHorariaPais = ZHE.UidRelacionZonaPaisEstado inner join Empresa e on e.UidEmpresa = s.UidEmpresa where  @UserTime between s.HorarioApertura and s.HorarioCierre  and ZHP.IdZonaHoraria = @TimeZone) 
                    and e.IdEstatus = 1 
                    and s.IntEstatus = 1
                    {filterWhere} {where}
            ) payload 
            ORDER BY {order}
            OFFSET @pageSize * @pageNumber ROWS
            FETCH NEXT @pageSize ROWS ONLY";
            command.CommandText = query;

            DataTable data = this.dbConexion.Busquedas(command);
            return data;
        }

        public DataTable ReadAllStoreVersion2(int pageSize, int pageNumber, string sortField, string sortOrder, Guid uidEstado, Guid uidColonia, string dia, string tipoFiltro, Guid uidFiltro, string filtro = null, Guid? uidSeccion = null, Guid? uidOferta = null, Guid? uidEmpresa = null)
        {

            string filterJoin = string.Empty;
            string filterWhere = string.Empty;
            string where = string.Empty;
            if (string.IsNullOrEmpty(tipoFiltro))
            {
                tipoFiltro = string.Empty;
            }
            if (tipoFiltro.Equals("Giro"))
            {
                filterJoin = "INNER JOIN [GiroProducto] as GP ON GP.UidProducto = P.UidProducto";
                filterWhere = "AND GP.UidGiro = @UidFilter";
            }
            else if (tipoFiltro.Equals("Categoria"))
            {
                filterJoin = "INNER JOIN [CategoriaProducto] as CP ON CP.UidProducto = P.UidProducto";
                filterWhere = "AND CP.UidCategoria = @UidFilter";
            }
            else if (tipoFiltro.Equals("Subcategoria"))
            {
                filterJoin = "INNER JOIN [SubcategoriaProducto] as SCP ON SCP.UidProducto = P.UidProducto";
                filterWhere = "AND SCP.UidSubcategoria = @UidFilter";
            }
            else if (tipoFiltro.Equals("None") || string.IsNullOrEmpty(tipoFiltro))
            {
                filterJoin = "INNER JOIN [GiroProducto] as GP ON GP.UidProducto = P.UidProducto";
            }

            string order = (string.IsNullOrEmpty(sortField) || string.IsNullOrEmpty(sortOrder)) ? " UID DESC " : $"{sortField} {sortOrder.ToUpper()}";

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;

            command.Parameters.AddWithValue("@pageSize", pageSize);
            command.Parameters.AddWithValue("@pageNumber", pageNumber);
            command.Parameters.AddWithValue("@UidColonia", uidColonia);
            command.Parameters.AddWithValue("@UidEstado", uidEstado);
            command.Parameters.AddWithValue("@Dia", dia);
            command.Parameters.AddWithValue("@FilterType", tipoFiltro);
            command.Parameters.AddWithValue("@UidFilter", uidFiltro);

            if (!string.IsNullOrEmpty(filtro))
            {
                command.Parameters.AddWithValue("@Filter", filtro);
                where += " and P.VchNombre like '%'+@Filter+'%' ";
            }

            if (uidSeccion.HasValue)
            {
                command.Parameters.AddWithValue("@UidSeccion", uidSeccion);
                where += " and SEC.UidSeccion = @UidSeccion ";
            }

            if (uidOferta.HasValue)
            {
                command.Parameters.AddWithValue("@UidOferta", uidOferta);
                where += " and O.UidOferta = @UidOferta ";
            }

            if (uidEmpresa.HasValue)
            {
                command.Parameters.AddWithValue("@UidEmpresa", uidEmpresa);
                where += " and EMP.UidEmpresa = @UidEmpresa ";
            }


            string query = $@"
            -- Zona horaria del usuario acorde al estado
            DECLARE @TimeZone VARCHAR(50);
            -- Fecha y Hora local del usuario
            DECLARE @UserDateTime DATETIME;
            -- Hora actual del usuario
            DECLARE @UserTime VARCHAR(20);

            DECLARE @ValorComision INT;

            -- Obtener zona horaria del estado
            SELECT
                @TimeZone = Z.IdZonaHoraria
            FROM [ZonaHoraria] AS Z
                INNER JOIN [ZonaHorariaPais] AS P ON P.[IdZonaHoraria] = Z.[IdZonaHoraria]
                INNER JOIN [ZonaHorariaEstado] AS E ON E.[UidRelacionZonaPaisEstado] = P.[UidZonaHorariaPais]
            WHERE E.UidEstado = @UidEstado

            -- Obtener DateTime del la zona horaria
            SELECT @UserDateTime = SYSDATETIMEOFFSET() AT TIME ZONE @TimeZone 

            -- Obtener Time del DateTime
            SELECT @UserTime = CONVERT(VARCHAR, @UserDateTime, 8)

            SELECT *, [Count] = COUNT (*) OVER() FROM (
                SELECT     
                    P.[UidProducto] AS [Uid],
                    IPROD.NVchRuta  AS [ImgUrl],
                    IEMP.NVchRuta AS [CompanyImgUrl],
                    P.VchNombre AS [Name],
                    P.VchDescripcion AS [Description],
                    EMP.[UidEmpresa] AS [UidCompany],
                    EMP.[NombreComercial] AS [CompanyName],
                    MIN(
                    ObtenerPrecioDeProductoDesdeContrato(s.UidSucursal,SP.Mcosto)
                    ) AS [Price]
                FROM [Productos] AS P    
                    INNER JOIN [SeccionProducto] AS SP ON P.[UidProducto] = SP.[UidProducto]
                    INNER JOIN [Seccion] AS SEC ON SEC.UidSeccion = SP.UidSeccion AND SEC.IntEstatus = 1
                    INNER JOIN [ImagenProducto] AS [IP] ON [IP].[UidProducto] = P.[UidProducto]
                    INNER JOIN [Imagenes] AS IPROD ON IPROD.UIdImagen = [IP].UidImagen
                    INNER JOIN [ImagenEmpresa] AS IE ON IE.UidRelacion = (SELECT TOP 1 UidRelacion FROM ImagenEmpresa WHERE UidEmpresa = P.UidEmpresa)
                    INNER JOIN [Imagenes] AS IEMP ON IEMP.UIdImagen = [IE].UidImagen
                    INNER JOIN [Oferta] AS O ON O.UidOferta= SEC.UidOferta AND O.IntEstatus = 1
                    INNER JOIN DiaOferta AS DO ON DO.UidOferta = O.UidOferta
                    INNER JOIN Dias AS D ON D.UidDia = DO.UidDia
                    INNER JOIN Sucursales AS SUC ON SUC.UidSucursal = O.Uidsucursal AND SUC.IntEstatus = 1
                    INNER JOIN Empresa AS EMP ON EMP.UidEmpresa = P.UidEmpresa AND EMP.IdEstatus =1

                    INNER JOIN Direccion AS DIR ON DIR.UidDireccion = SUC.UidDireccion
                    INNER JOIN ZonaHorariaEstado AS ZHE ON ZHE.UidEstado = DIR.UidEstado
                    INNER JOIN ZonaHorariaPais ZHP ON ZHP.UidZonaHorariaPais = ZHE.UidRelacionZonaPaisEstado

                    INNER JOIN ContratoDeServicio CDS on CDS.UidSucursalSuministradora = SUC.UidSucursal 
                    INNER JOIN ZonaDeRepartoDeContrato ZDRDC on ZDRDC.UidContrato = CDS.UidContrato 
	                INNER JOIN Tarifario T on t.UidRegistroTarifario = ZDRDC.UidTarifario 
	                INNER JOIN ZonaDeServicio ZDS on ZDS.UidColonia = @UidColonia and ZDS.UidRelacionZonaServicio = T.UidRelacionZonaEntrega 
    
	                INNER JOIN turnosuministradora TS on TS.uidsucursal = CDS.UidSucursalSuministradora and TS.dtmhorafin is null
	                INNER JOIN TurnoDistribuidora TD on TD.UidSucursal = CDS.UidSucursalDistribuidora and TD.DtmHoraFin is null 
                    INNER JOIN Comision AS COM ON COM.UidEmpresa = EMP.UidEmpresa
                    {filterJoin}
                WHERE 
                    @UserTime BETWEEN SEC.VchHoraInicio AND SEC.VchHoraFin    
                    AND D.VchNombre = @Dia    
                    AND SP.VchTiempoElaboracion IS NOT NULL 
                    AND ZDS.UidColonia = @UidColonia 
                    AND CDS.UidEstatusContrato = 'CD20F9BF-EBA2-4128-88FB-647544457B2D'
                    AND ZHP.IdZonaHoraria = @TimeZone
                    AND @UserTime between SUC.HorarioApertura and SUC.HorarioCierre
                    AND P.intEstatus = 1
                    {filterWhere} {where}
                GROUP BY P.UidProducto,P.[VchNombre],IPROD.NVchRuta,IEMP.NVchRuta,EMP.[UidEmpresa],EMP.[NombreComercial],P.VchDescripcion
            ) payload 
            ORDER BY {order}
            OFFSET @pageSize * @pageNumber ROWS
            FETCH NEXT @pageSize ROWS ONLY";
            command.CommandText = query;

            DataTable data = this.dbConexion.Busquedas(command);
            return data;
        }

        /// <summary>
        /// VERSION 3 DE LA BUSQUEDA DE PRODUCTOS, PERMITE OBTENER PARAMETROS PARA DETERMINAR SI ESTA DISPONIBLE O NO
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="sortField"></param>
        /// <param name="sortOrder"></param>
        /// <param name="uidEstado"></param>
        /// <param name="uidColonia"></param>
        /// <param name="dia"></param>
        /// <param name="tipoFiltro"></param>
        /// <param name="uidFiltro"></param>
        /// <param name="filtro"></param>
        /// <param name="uidSeccion"></param>
        /// <param name="uidOferta"></param>
        /// <param name="uidEmpresa"></param>
        /// <returns></returns>
        public DataTable ReadAllStoreVersion3(int pageSize, int pageNumber, string sortField, string sortOrder, Guid uidEstado, Guid uidColonia, string dia, string tipoFiltro, Guid uidFiltro, string filtro = null, Guid? uidSeccion = null, Guid? uidOferta = null, Guid? uidEmpresa = null, bool? available = null)
        {

            string filterJoin = string.Empty;
            string filterWhere = string.Empty;
            string where = string.Empty;
            if (string.IsNullOrEmpty(tipoFiltro))
            {
                tipoFiltro = string.Empty;
            }
            if (tipoFiltro.Equals("Giro"))
            {
                filterJoin = "INNER JOIN [GiroProducto] as GP ON GP.UidProducto = P.UidProducto";
                filterWhere = "AND GP.UidGiro = @UidFilter";
            }
            else if (tipoFiltro.Equals("Categoria"))
            {
                filterJoin = "INNER JOIN [CategoriaProducto] as CP ON CP.UidProducto = P.UidProducto";
                filterWhere = "AND CP.UidCategoria = @UidFilter";
            }
            else if (tipoFiltro.Equals("Subcategoria"))
            {
                filterJoin = "INNER JOIN [SubcategoriaProducto] as SCP ON SCP.UidProducto = P.UidProducto";
                filterWhere = "AND SCP.UidSubcategoria = @UidFilter";
            }
            else if (tipoFiltro.Equals("None") || string.IsNullOrEmpty(tipoFiltro))
            {
                filterJoin = "INNER JOIN [GiroProducto] as GP ON GP.UidProducto = P.UidProducto";
            }

            string order = (string.IsNullOrEmpty(sortField) || string.IsNullOrEmpty(sortOrder)) ? " UID DESC " : $"{sortField} {sortOrder.ToUpper()}";

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;

            command.Parameters.AddWithValue("@pageSize", pageSize);
            command.Parameters.AddWithValue("@pageNumber", pageNumber);
            command.Parameters.AddWithValue("@UidColonia", uidColonia);
            command.Parameters.AddWithValue("@UidEstado", uidEstado);
            command.Parameters.AddWithValue("@Dia", dia);
            command.Parameters.AddWithValue("@FilterType", tipoFiltro);
            command.Parameters.AddWithValue("@UidFilter", uidFiltro);

            if (!string.IsNullOrEmpty(filtro))
            {
                command.Parameters.AddWithValue("@Filter", filtro);
                where += " and P.VchNombre like '%'+@Filter+'%' ";
            }

            if (uidSeccion.HasValue)
            {
                command.Parameters.AddWithValue("@UidSeccion", uidSeccion);
                where += " and SEC.UidSeccion = @UidSeccion ";
            }

            if (uidOferta.HasValue)
            {
                command.Parameters.AddWithValue("@UidOferta", uidOferta);
                where += " and O.UidOferta = @UidOferta ";
            }

            if (uidEmpresa.HasValue)
            {
                command.Parameters.AddWithValue("@UidEmpresa", uidEmpresa);
                where += " and EMP.UidEmpresa = @UidEmpresa ";
            }

            string outerParams = $",(CASE WHEN [SecAvailable] = 0 OR [SucAvailable] = 0 OR [TdAvailable] = 0 OR [TsAvailable] = 0 THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END) AS [Available]";

            string AvailableSelectStatemens = "";
            string AvailableWhereStatemens = "";

            AvailableSelectStatemens = @"
,SUM (CASE WHEN @UserTime BETWEEN SEC.VchHoraInicio AND SEC.VchHoraFin THEN 1 ELSE 0 END ) AS [SecAvailable],
SUM (CASE WHEN @UserTime between SUC.HorarioApertura and SUC.HorarioCierre THEN 1 ELSE 0 END) AS [SucAvailable],
SUM (CASE WHEN CAST(@UserDateTime AS DATETIME) >= CAST(TS.DtmHoraInicio AS DATETIME) AND TS.DtmHoraFin IS NULL AND CAST(@UserDateTime AS TIME) BETWEEN CAST(TSUC.HorarioApertura AS TIME) AND CAST(TSUC.HorarioCierre AS TIME) THEN 1 ELSE 0 END) AS [TsAvailable],
SUM (CASE WHEN CAST(@UserDateTime AS DATETIME) >= CAST(TD.DtmHoraInicio AS DATETIME) AND TD.DtmHoraFin IS NULL AND CAST(@UserDateTime AS TIME) BETWEEN CAST(TDSUC.HorarioApertura AS TIME) AND CAST(TDSUC.HorarioCierre AS TIME)   THEN 1 ELSE 0 END) AS [TdAvailable] ";

            if (available.HasValue)
            {
                outerParams = ",(CAST(1 AS BIT)) AS [Available]";
                AvailableSelectStatemens = "";
                AvailableWhereStatemens = @" AND @UserTime BETWEEN SEC.VchHoraInicio AND SEC.VchHoraFin ";
                AvailableWhereStatemens += " AND @UserTime between SUC.HorarioApertura and SUC.HorarioCierre ";
                AvailableWhereStatemens += " AND CAST(@UserDateTime AS DATETIME) >= CAST(TS.DtmHoraInicio AS DATETIME) AND TS.DtmHoraFin IS NULL AND CAST(@UserDateTime AS TIME) BETWEEN CAST(TSUC.HorarioApertura AS TIME) AND CAST(TSUC.HorarioCierre AS TIME) ";
                AvailableWhereStatemens += " AND CAST(@UserDateTime AS DATETIME) >= CAST(TD.DtmHoraInicio AS DATETIME) AND TD.DtmHoraFin IS NULL AND CAST(@UserDateTime AS TIME) BETWEEN CAST(TDSUC.HorarioApertura AS TIME) AND CAST(TDSUC.HorarioCierre AS TIME) ";
            }


            string query = $@"
            -- Zona horaria del usuario acorde al estado
            DECLARE @TimeZone VARCHAR(50);
            -- Fecha y Hora local del usuario
            DECLARE @UserDateTime DATETIME;
            -- Hora actual del usuario
            DECLARE @UserTime VARCHAR(20);

            DECLARE @ValorComision INT;

            -- Obtener zona horaria del estado
            SELECT
                @TimeZone = Z.IdZonaHoraria
            FROM [ZonaHoraria] AS Z
                INNER JOIN [ZonaHorariaPais] AS P ON P.[IdZonaHoraria] = Z.[IdZonaHoraria]
                INNER JOIN [ZonaHorariaEstado] AS E ON E.[UidRelacionZonaPaisEstado] = P.[UidZonaHorariaPais]
            WHERE E.UidEstado = @UidEstado

            -- Obtener DateTime del la zona horaria
            SELECT @UserDateTime = SYSDATETIMEOFFSET() AT TIME ZONE @TimeZone 

            -- Obtener Time del DateTime
            SELECT @UserTime = CONVERT(VARCHAR, @UserDateTime, 8)

            SELECT 
                * {outerParams},[Count] = COUNT (*) OVER()  
            FROM (
                SELECT     
                    P.[UidProducto] AS [Uid],
                    IPROD.NVchRuta  AS [ImgUrl],
                    IEMP.NVchRuta AS [CompanyImgUrl],
                    P.VchNombre AS [Name],
                    P.VchDescripcion AS [Description],
                    EMP.[UidEmpresa] AS [UidCompany],
                    EMP.[NombreComercial] AS [CompanyName],
                    MIN(
                    CASE 
                         WHEN COM.BAboserveComision = 1 THEN SP.Mcosto
                         ELSE ((SP.Mcosto / 100)*CDS.ComisionTotalProducto) + SP.Mcosto
                    END 
                    ) AS [Price] {AvailableSelectStatemens}                    
                FROM [Productos] AS P    
                    INNER JOIN [SeccionProducto] AS SP ON P.[UidProducto] = SP.[UidProducto]
                    INNER JOIN [Seccion] AS SEC ON SEC.UidSeccion = SP.UidSeccion AND SEC.IntEstatus = 1
                    INNER JOIN [ImagenProducto] AS [IP] ON [IP].[UidProducto] = P.[UidProducto]
                    INNER JOIN [Imagenes] AS IPROD ON IPROD.UIdImagen = [IP].UidImagen
                    INNER JOIN [ImagenEmpresa] AS IE ON IE.UidRelacion = (SELECT TOP 1 UidRelacion FROM ImagenEmpresa WHERE UidEmpresa = P.UidEmpresa)
                    INNER JOIN [Imagenes] AS IEMP ON IEMP.UIdImagen = [IE].UidImagen

                    INNER JOIN [Oferta] AS O ON O.UidOferta= SEC.UidOferta AND O.IntEstatus = 1
                    INNER JOIN DiaOferta AS DO ON DO.UidOferta = O.UidOferta
                    INNER JOIN Dias AS D ON D.UidDia = DO.UidDia
                    INNER JOIN Sucursales AS SUC ON SUC.UidSucursal = O.Uidsucursal AND SUC.IntEstatus = 1

                    INNER JOIN Empresa AS EMP ON EMP.UidEmpresa = P.UidEmpresa AND EMP.IdEstatus =1

                    INNER JOIN Direccion AS DIR ON DIR.UidDireccion = SUC.UidDireccion
                    INNER JOIN ZonaHorariaEstado AS ZHE ON ZHE.UidEstado = DIR.UidEstado
                    INNER JOIN ZonaHorariaPais ZHP ON ZHP.UidZonaHorariaPais = ZHE.UidRelacionZonaPaisEstado

                    INNER JOIN ContratoDeServicio CDS on CDS.UidSucursalSuministradora = SUC.UidSucursal 
                    INNER JOIN ZonaDeRepartoDeContrato ZDRDC on ZDRDC.UidContrato = CDS.UidContrato 
	                INNER JOIN Tarifario T on t.UidRegistroTarifario = ZDRDC.UidTarifario 
	                INNER JOIN ZonaDeServicio ZDS on ZDS.UidColonia = @UidColonia and ZDS.UidRelacionZonaServicio = T.UidRelacionZonaEntrega 
    
	                INNER JOIN turnosuministradora TS on TS.uidsucursal = CDS.UidSucursalSuministradora
                    INNER JOIN Sucursales AS TSUC ON TSUC.UidSucursal = TS.UidSucursal
	                INNER JOIN TurnoDistribuidora TD on TD.UidSucursal = CDS.UidSucursalDistribuidora
                    INNER JOIN Sucursales AS TDSUC ON TDSUC.UidSucursal = TD.UidSucursal
                    INNER JOIN Comision AS COM ON COM.UidEmpresa = EMP.UidEmpresa
                    {filterJoin}
                WHERE 
                     D.VchNombre = @Dia    
                    AND SP.VchTiempoElaboracion IS NOT NULL 
                    AND ZDS.UidColonia = @UidColonia 
                    AND CDS.UidEstatusContrato = 'CD20F9BF-EBA2-4128-88FB-647544457B2D'
                    AND ZHP.IdZonaHoraria = @TimeZone    
                    AND P.intEstatus = 1
                    {filterWhere} {where} {AvailableWhereStatemens}
                GROUP BY P.UidProducto,P.[VchNombre],IPROD.NVchRuta,IEMP.NVchRuta,EMP.[UidEmpresa],EMP.[NombreComercial],P.VchDescripcion
            ) payload 
            WHERE 1=1 
            ORDER BY {order}
            OFFSET @pageSize * @pageNumber ROWS
            FETCH NEXT @pageSize ROWS ONLY";
            command.CommandText = query;

            DataTable data = this.dbConexion.Busquedas(command);
            return data;
        }

        public DataTable ReadAllStoreStoreProcedure(int pageSize, int pageNumber, string sortField, string sortOrder, Guid uidEstado, Guid uidColonia, string dia, string tipoFiltro, Guid uidFiltro, string filtro = null, Guid? uidSeccion = null, Guid? uidOferta = null, Guid? uidEmpresa = null)
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "asp_SearchProductsFromStore";

            command.Parameters.AddWithValue("@pageSize", pageSize);
            command.Parameters.AddWithValue("@pageNumber", pageNumber);
            command.Parameters.AddWithValue("@UidColonia", uidColonia);
            command.Parameters.AddWithValue("@UidEstado", uidEstado);
            command.Parameters.AddWithValue("@Dia", dia);
            command.Parameters.AddWithValue("@FilterType", tipoFiltro);
            command.Parameters.AddWithValue("@UidFilter", uidFiltro);
            command.Parameters.AddWithValue("@SortField", uidFiltro);
            command.Parameters.AddWithValue("@SortDirection", uidFiltro);

            if (!string.IsNullOrEmpty(filtro))
            {
                command.Parameters.AddWithValue("@Filter", filtro);
            }

            if (uidSeccion.HasValue)
            {
                command.Parameters.AddWithValue("@UidSeccion", uidSeccion);
            }

            if (uidOferta.HasValue)
            {
                command.Parameters.AddWithValue("@UidOferta", uidOferta);
            }

            if (uidEmpresa.HasValue)
            {
                command.Parameters.AddWithValue("@UidEmpresa", uidEmpresa);
            }

            DataTable data = this.dbConexion.Busquedas(command);
            return data;
        }

        /// <summary>
        /// Realizar busqueda de los comercios
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="sortField"></param>
        /// <param name="sortOrder"></param>
        /// <param name="uidEstado"></param>
        /// <param name="uidColonia"></param>
        /// <param name="dia"></param>
        /// <param name="tipoFiltro"></param>
        /// <param name="uidFiltro"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public DataTable ReadAllStores(int pageSize, int pageNumber, string sortField, string sortOrder, Guid uidEstado, Guid uidColonia, string dia, string tipoFiltro, Guid uidFiltro, string filtro = null, bool? available = null)
        {
            string filterJoin = string.Empty;
            string filterWhere = string.Empty;
            string where = string.Empty;

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;

            command.Parameters.AddWithValue("@pageSize", pageSize);
            command.Parameters.AddWithValue("@pageNumber", pageNumber);
            command.Parameters.AddWithValue("@UidColonia", uidColonia);
            command.Parameters.AddWithValue("@UidEstado", uidEstado);
            command.Parameters.AddWithValue("@Dia", dia);
            command.Parameters.AddWithValue("@FilterType", tipoFiltro);
            command.Parameters.AddWithValue("@UidFilter", uidFiltro);

            if (!string.IsNullOrEmpty(filtro))
            {
                command.Parameters.AddWithValue("@Filter", filtro);
                where += " AND e.NombreComercial like '%'+@Filter+'%' ";
            }

            if (string.IsNullOrEmpty(tipoFiltro) || tipoFiltro.Equals("None"))
            {
                filterJoin = " INNER JOIN GiroProducto gp on gp.UidProducto = p.UidProducto ";
            }
            else if (tipoFiltro.Equals("Giro"))
            {
                filterJoin = " INNER JOIN GiroProducto gp on gp.UidProducto = p.UidProducto ";
                filterWhere = " AND gp.UidGiro = @UidFilter ";
            }
            else if (tipoFiltro.Equals("Categoria"))
            {
                filterJoin = " INNER JOIN CategoriaProducto cp on cp.UidProducto = p.UidProducto ";
                filterWhere = " AND cp.UidCategoria = @UidFilter ";
            }
            else if (tipoFiltro.Equals("Subcategoria"))
            {
                filterJoin = " INNER JOIN SubcategoriaProducto scp on scp.UidProducto = p.UidProducto ";
                filterWhere = " AND scp.UidSubcategoria = @UidFilter ";
            }

            string selectAvailability = $@"
@UserTime between S.HorarioApertura and S.HorarioCierre and ZHP.IdZonaHoraria = @TimeZone 
AND CAST(@UserDateTime AS DATETIME) >= CAST(TS.DtmHoraInicio AS DATETIME) AND TS.DtmHoraFin IS NULL AND CAST(@UserDateTime AS TIME) BETWEEN CAST(TSUC.HorarioApertura AS TIME) AND CAST(TSUC.HorarioCierre AS TIME)
AND CAST(@UserDateTime AS DATETIME) >= CAST(TD.DtmHoraInicio AS DATETIME) AND TD.DtmHoraFin IS NULL AND CAST(@UserDateTime AS TIME) BETWEEN CAST(TDSUC.HorarioApertura AS TIME) AND CAST(TDSUC.HorarioCierre AS TIME)
AND @UserTime between se.VchHoraInicio and se.VchHoraFin ";
            string whereAvailability = $@" AND 1=1 ";

            if (available.HasValue)
            {
                selectAvailability = " 1=1 ";
                whereAvailability = $@"
AND @UserTime between S.HorarioApertura and S.HorarioCierre and ZHP.IdZonaHoraria = @TimeZone
AND CAST(@UserDateTime AS DATETIME) >= CAST(TS.DtmHoraInicio AS DATETIME) AND TS.DtmHoraFin IS NULL AND CAST(@UserDateTime AS TIME) BETWEEN CAST(TSUC.HorarioApertura AS TIME) AND CAST(TSUC.HorarioCierre AS TIME)
AND CAST(@UserDateTime AS DATETIME) >= CAST(TD.DtmHoraInicio AS DATETIME) AND TD.DtmHoraFin IS NULL AND CAST(@UserDateTime AS TIME) BETWEEN CAST(TDSUC.HorarioApertura AS TIME) AND CAST(TDSUC.HorarioCierre AS TIME)
AND @UserTime between se.VchHoraInicio and se.VchHoraFin ";
            }

            string order = (string.IsNullOrEmpty(sortField) || string.IsNullOrEmpty(sortOrder)) ? " Uid " : $"{sortField} {sortOrder.ToUpper()}";

            string query = $@"
-- Zona horaria del usuario acorde al estado
DECLARE @TimeZone VARCHAR(50);
-- Fecha y Hora local del usuario
DECLARE @UserDateTime DATETIME;
-- Hora actual del usuario
DECLARE @UserTime VARCHAR(20);
-- Obtener zona horaria del estado
SELECT
    @TimeZone = Z.IdZonaHoraria
FROM [ZonaHoraria] AS Z
    INNER JOIN [ZonaHorariaPais] AS P ON P.[IdZonaHoraria] = Z.[IdZonaHoraria]
    INNER JOIN [ZonaHorariaEstado] AS E ON E.[UidRelacionZonaPaisEstado] = P.[UidZonaHorariaPais]
WHERE E.UidEstado = @UidEstado
-- Obtener DateTime del la zona horaria
SELECT @UserDateTime = SYSDATETIMEOFFSET() AT TIME ZONE @TimeZone 
-- Obtener Time del DateTime
SELECT @UserTime = CONVERT(VARCHAR, @UserDateTime, 8)
SELECT *, [Count] = COUNT (*) OVER() FROM (
    SELECT DISTINCT 
        E.UidEmpresa AS [Uid], 
        E.NombreComercial AS [Name],
        I.NVchRuta AS [ImgUrl],
        COUNT(DISTINCT S.UidSucursal) AS [AvailableBranches],
        CASE WHEN 
        SUM(
        CASE 
            WHEN {selectAvailability} THEN 1 ELSE 0 
        END) = 0 THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS [Available]
    FROM Empresa E
        INNER JOIN Productos p on e.UidEmpresa = p.UidEmpresa
	    INNER JOIN SeccionProducto sp on sp.UidProducto = p.UidProducto 
	    INNER JOIN Seccion se on se.UidSeccion = sp.UidSeccion 
	    INNER JOIN Oferta O on O.UidOferta = se.UidOferta 
	    INNER JOIN DiaOferta DO on DO.UidOferta = O.UidOferta 
	    INNER JOIN Dias D on D.UidDia = DO.UidDia 
	    INNER JOIN Sucursales s on s.UidSucursal = o.Uidsucursal 
	    INNER JOIN ContratoDeServicio CDS on CDS.UidSucursalSuministradora = s.UidSucursal 
	    INNER JOIN turnosuministradora ts on ts.uidsucursal = CDS.UidSucursalSuministradora
        INNER JOIN Sucursales AS TSUC ON TSUC.UidSucursal = TS.UidSucursal
	    INNER JOIN TurnoDistribuidora td on td.UidSucursal = CDS.UidSucursalDistribuidora
        INNER JOIN Sucursales AS TDSUC ON TDSUC.UidSucursal = TD.UidSucursal
	    INNER JOIN ZonaDeRepartoDeContrato ZDRC on ZDRC.UidContrato = CDS.UidContrato 
	    INNER JOIN Tarifario t on t.UidRegistroTarifario = ZDRC.UidTarifario 
	    INNER JOIN ZonaDeServicio zd on zd.UidColonia = @UidColonia and zd.UidRelacionZonaServicio = t.UidRelacionZonaEntrega 
	    INNER JOIN ImagenEmpresa ie on ie.UidEmpresa = e.UidEmpresa 
	    INNER JOIN Imagenes i on ie.UidImagen = i.UIdImagen 		

        INNER JOIN Direccion AS DIR ON DIR.UidDireccion = S.UidDireccion
        INNER JOIN ZonaHorariaEstado ZHE on ZHE.UidEstado = DIR.UidEstado
        INNER JOIN ZonaHorariaPais ZHP on ZHP.UidZonaHorariaPais = ZHE.UidRelacionZonaPaisEstado 

        {filterJoin}
    WHERE   
        1=1
	    AND zd.UidColonia = @UidColonia 
        AND p.IntEstatus = 1 
        AND	D.VchNombre = @Dia 
        AND O.IntEstatus = 1 
        AND se.IntEstatus = 1 
        AND sp.VchTiempoElaboracion IS NOT NULL 
        AND CDS.UidEstatusContrato = 'CD20F9BF-EBA2-4128-88FB-647544457B2D'
	    -- AND ((@Filter is not null AND e.NombreComercial like '%'+@Filter+'%')or(@Filter is null))
        AND i.NVchRuta not like '%/Portada/%'
        AND i.NVchRuta LIKE '%FotoPerfil%'
        AND e.IdEstatus = 1 
        AND s.IntEstatus = 1 {filterWhere} {where} {whereAvailability}
    GROUP BY  E.UidEmpresa, E.NombreComercial, I.NVchRuta
) payload 
ORDER BY {order}
OFFSET @pageSize * @pageNumber ROWS
FETCH NEXT @pageSize ROWS ONLY";

            command.CommandText = query;

            DataTable data = this.dbConexion.Busquedas(command);
            return data;
        }

        public DataTable ReadAllCompanyBranch(Guid uidEmpresa, Guid uidEstado, Guid uidColonia)
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;

            command.Parameters.AddWithValue("@UidEmpresa", uidEmpresa);
            command.Parameters.AddWithValue("@UidColonia", uidColonia);
            command.Parameters.AddWithValue("@UidEstado", uidEstado);

            string query = $@"
-- Zona horaria del usuario acorde al estado
DECLARE @TimeZone VARCHAR(50);
-- Fecha y Hora local del usuario
DECLARE @UserDateTime DATETIME;
-- Hora actual del usuario
DECLARE @UserTime VARCHAR(20);

-- Obtener zona horaria del estado
SELECT
    @TimeZone = Z.IdZonaHoraria
FROM [ZonaHoraria] AS Z
    INNER JOIN [ZonaHorariaPais] AS P ON P.[IdZonaHoraria] = Z.[IdZonaHoraria]
    INNER JOIN [ZonaHorariaEstado] AS E ON E.[UidRelacionZonaPaisEstado] = P.[UidZonaHorariaPais]
WHERE E.UidEstado = @UidEstado

-- Obtener DateTime del la zona horaria
SELECT @UserDateTime = SYSDATETIMEOFFSET() AT TIME ZONE @TimeZone 

-- Obtener Time del DateTime
SELECT @UserTime = CONVERT(VARCHAR, @UserDateTime, 8)

SELECT 
    DISTINCT s.UidSucursal AS [Uid],
    s.HorarioApertura AS [OpenAt],
    s.HorarioCierre AS [CloseAt],
    s.Identificador AS [Identifier], 
    s.IntEstatus AS [Status],
    CASE WHEN @UserTime between s.HorarioApertura and s.HorarioCierre THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS [Available]
FROM Sucursales s 
	inner join ContratoDeServicio CDS on CDS.UidSucursalSuministradora = s.UidSucursal 
	inner join turnosuministradora ts on ts.uidsucursal = CDS.UidSucursalSuministradora and ts.dtmhorafin is null
    inner join TurnoDistribuidora td on td.UidSucursal = CDS.UidSucursalDistribuidora and td.DtmHoraFin is null
    inner join ZonaDeRepartoDeContrato ZDRC on ZDRC.UidContrato = CDS.UidContrato 
	inner join Oferta o on o.Uidsucursal = s.UidSucursal
	inner join DiaOferta do on do.UidOferta = o.UidOferta
	inner join Tarifario t on t.Uidregistrotarifario  = ZDRC.UidTarifario
	inner join ZonaDeServicio ZDS on ZDS.UidRelacionZonaServicio = t.UidRelacionZonaEntrega
WHERE ZDS.UidColonia = @UidColonia
	AND s.UidEmpresa = @UidEmpresa
	AND s.IntEstatus = 1
	AND CDS.UidEstatusContrato = 'CD20F9BF-EBA2-4128-88FB-647544457B2D'
    -- AND @UserTime between s.HorarioApertura and s.HorarioCierre
ORDER BY [Identifier]";

            command.CommandText = query;

            DataTable data = this.dbConexion.Busquedas(command);
            return data;
        }

        public DataTable Company(Guid uidEmpresa)
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;

            command.Parameters.AddWithValue("@UidEmpresa", uidEmpresa);
            string query = $@"
SELECT
    E.UidEmpresa AS[Uid],
    E.NombreComercial AS[Name],
    I.NVchRuta AS[ImgUrl]
FROM Empresa AS E
    INNER JOIN ImagenEmpresa AS IE ON IE.UidEmpresa = E.UidEmpresa
    INNER JOIN Imagenes AS I ON I.UIdImagen = IE.UidImagen AND I.NVchRuta LIKE '%FotoPerfil%'
WHERE E.UidEmpresa = @UidEmpresa";
            command.CommandText = query;

            DataTable data = this.dbConexion.Busquedas(command);
            return data;
        }

        /// <summary>
        /// Obtener las sucursales di
        /// </summary>
        /// <param name="uidProducto"></param>
        /// <param name="uidEstado"></param>
        /// <param name="uidColonia"></param>
        /// <param name="dia"></param>
        /// <returns></returns>
        public DataTable GetProductBranches(Guid uidProducto, Guid uidEstado, Guid uidColonia, string dia)
        {
            string query = $@"sp_ObtenerSucursalesProductoTienda";

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = query;

            command.Parameters.AddWithValue("@UidProducto", uidProducto);
            command.Parameters.AddWithValue("@UidEstado", uidEstado);
            command.Parameters.AddWithValue("@UidColonia", uidColonia);
            command.Parameters.AddWithValue("@Dia", dia);

            DataTable data = this.dbConexion.Busquedas(command);
            return data;
        }

        /// <summary>
        /// Obtener el detalle del producto para la tienda
        /// </summary>
        /// <param name="uidProducto"></param>
        /// <param name="uidEstado"></param>
        /// <param name="uidColonia"></param>
        /// <param name="dia"></param>
        /// <returns></returns>
        public DataTable GetProductDetail(Guid uidProducto, Guid uidEstado, Guid uidColonia, string dia)
        {
            string query = $@"sp_ObtenerDetalleProductoTienda";

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = query;

            command.Parameters.AddWithValue("@UidProducto", uidProducto);
            command.Parameters.AddWithValue("@UidEstado", uidEstado);
            command.Parameters.AddWithValue("@UidColonia", uidColonia);
            command.Parameters.AddWithValue("@Dia", dia);

            DataTable data = this.dbConexion.Busquedas(command);
            return data;
        }

        #region Categoria
        public DataTable GetGiros()
        {
            string query = $@"
SELECT 
    G.UidGiro AS [Uid], 
    G.VchNombre AS [Name], 
    G.VchDescripcion AS [Description], 
    I.NVchRuta AS [ImgUrl] 
FROM Giro G
    inner join ImagenGiro IG on IG.UidGiro = G.UidGiro 
    inner join Imagenes I on I.UIdImagen = IG.UidImagen 
WHERE G.intEstatus = 1
ORDER BY G.VchNombre ASC";

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = query;

            DataTable data = this.dbConexion.Busquedas(command);
            return data;
        }

        public DataTable GetCategories(Guid Uid)
        {
            string query = $@"
SELECT 
    [UidCategoria] AS [Uid],
    [VchNombre] AS [Name]
FROM [Categorias]
WHERE [UidGiro] = @Uid AND [intEstatus] = 1;";

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = query;

            command.Parameters.AddWithValue("@Uid", Uid);

            DataTable data = this.dbConexion.Busquedas(command);
            return data;
        }

        public DataTable GetSubcategories(Guid Uid)
        {
            string query = $@"
SELECT 
    [UidSubcategoria] AS [Uid],
    [VchNombre] AS [Name]
FROM [Subcategoria]
WHERE [UidCategoria] = @Uid AND [intEstatus] = 1;";

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = query;

            command.Parameters.AddWithValue("@Uid", Uid);

            DataTable data = this.dbConexion.Busquedas(command);
            return data;
        }
        #endregion
    }
}
