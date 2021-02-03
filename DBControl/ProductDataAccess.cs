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
        public DataTable ReadAllStores(int pageSize, int pageNumber, string sortField, string sortOrder, Guid uidEstado, Guid uidColonia, string dia, string tipoFiltro, Guid uidFiltro, string filtro = null)
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

            if (tipoFiltro.Equals("Giro"))
            {
                filterJoin = " INNER JOIN GiroProducto gp on gp.UidProducto = p.UidProducto ";
                filterWhere = " gp.UidGiro = @UidFilter ";
            }
            else if (tipoFiltro.Equals("Categoria"))
            {
                filterJoin = " INNER JOIN CategoriaProducto cp on cp.UidProducto = p.UidProducto ";
                filterWhere = " cp.UidCategoria = @UidFilter ";
            }
            else if (tipoFiltro.Equals("Subcategoria"))
            {
                filterJoin = " INNER JOIN SubcategoriaProducto scp on scp.UidProducto = p.UidProducto ";
                filterWhere = " scp.UidSubcategoria = @UidFilter ";
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
        I.NVchRuta AS [ImgUrl]
    FROM Empresa E
        INNER JOIN Productos p on e.UidEmpresa = p.UidEmpresa
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
	    INNER JOIN ImagenEmpresa ie on ie.UidEmpresa = e.UidEmpresa 
	    INNER JOIN Imagenes i on ie.UidImagen = i.UIdImagen 	
        {filterJoin}
    WHERE   
        {filterWhere} {where}
        AND @UserTime between se.VchHoraInicio and se.VchHoraFin 
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
        AND s.UidSucursal IN( 
            select  
                s.UidSucursal 
            from Sucursales s 
                INNER JOIN Direccion d on d.UidDireccion = s.UidDireccion 
                INNER JOIN ZonaHorariaEstado ZHE on ZHE.UidEstado = d.UidEstado
                INNER JOIN ZonaHorariaPais ZHP on ZHP.UidZonaHorariaPais = ZHE.UidRelacionZonaPaisEstado 
                INNER JOIN Empresa e on e.UidEmpresa = s.UidEmpresa 
            WHERE  @UserTime between s.HorarioApertura and s.HorarioCierre  and ZHP.IdZonaHoraria = @TimeZone) 
        AND e.IdEstatus = 1 
        AND s.IntEstatus = 1
) payload 
ORDER BY {order}
OFFSET @pageSize * @pageNumber ROWS
FETCH NEXT @pageSize ROWS ONLY";

            command.CommandText = query;

            DataTable data = this.dbConexion.Busquedas(command);
            return data;
        }
    }
}
