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
    /// Acceso a datos para gestion de contenido del repartidor (version 2)
    /// </summary>
    public class DealerDataAccess
    {
        /// <summary>
        /// Conexion a la base de datos
        /// </summary>
        protected readonly Conexion dbConexion;

        public DealerDataAccess()
        {
            this.dbConexion = new Conexion();
        }

        public bool Update(Guid uid, string nombre = "", string apellidoPaterno = "", string apellidoMaterno = "", DateTime? fechaNacimiento = null)
        {
            string update = "";

            if (!string.IsNullOrEmpty(nombre))
            {
                update += string.IsNullOrEmpty(update) ? $"[Nombre] = '{nombre}'" : $",[Nombre] = '{nombre}'";
            }

            if (!string.IsNullOrEmpty(apellidoPaterno))
            {
                update += string.IsNullOrEmpty(update) ? $"[ApellidoPaterno] = '{apellidoPaterno}'" : $",[ApellidoPaterno] = '{apellidoPaterno}'";
            }

            if (!string.IsNullOrEmpty(apellidoMaterno))
            {
                update += string.IsNullOrEmpty(update) ? $"[ApellidoMaterno] = '{apellidoMaterno}'" : $",[ApellidoMaterno] = '{apellidoMaterno}'";
            }

            if (fechaNacimiento.HasValue)
            {
                update += string.IsNullOrEmpty(update) ? $"[FechaDeNacimiento] = '{fechaNacimiento.Value.ToString("MM/dd/yyyy")}'" : $",[FechaDeNacimiento] = '{fechaNacimiento.Value.ToString("MM/dd/yyyy")}'";
            }

            string query = $"UPDATE [Usuarios] SET {update} WHERE [UidUsuario] = '{uid.ToString()}'";

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = query;
            sqlCommand.CommandType = CommandType.Text;

            return this.dbConexion.ModificarDatos(sqlCommand);
        }

        #region Direcciones
        public DataTable ReadAllAddressesByUserId(Guid uidUser)
        {
            string query = $@"
                SELECT 
                    D.[UidDireccion],
                    D.[UidPais],
                    D.[UidEstado],
                    D.[UidMunicipio],
                    D.[UidCiudad],
                    D.[UidColonia],
                    D.[Calle0],
                    D.[Calle1],
                    D.[Calle2],
                    D.[Manzana],
                    D.[Lote],
                    D.[CodigoPostal],
                    D.[Referencia],
                    D.[Identificador],
                    D.[BEstatus],
                    D.[BPredeterminada]
                FROM [Direccion] AS D
                    INNER JOIN [DireccionUsuario] AS DU ON DU.[UidDireccion] = D.[UidDireccion]
                WHERE DU.[UidUsuario] = '{uidUser.ToString()}'";

            DataTable data = this.dbConexion.Consultas(query);
            return data;
        }
        #endregion

        #region Telefonos
        public DataTable ReadAllPhonesByUserId(Guid uidUser)
        {
            string query = $@"
                SELECT 
                    T.[UidTelefono],
                    T.[UidTipoDeTelefono],
                    T.[Numero],
                    TT.[Nombre] AS [Tipo]
                FROM [Telefono] AS T 
                    INNER JOIN [TipoDeTelefono] AS TT ON TT.[UidTipoDeTelefono] = T.[UidTipoDeTelefono]
                    INNER JOIN [TelefonoUsuario] AS TU ON TU.[UidTelefono] = T.[UidTelefono]
                WHERE TU.[UidUsuario] = '{uidUser.ToString()}'";

            DataTable data = this.dbConexion.Consultas(query);
            return data;
        }
        #endregion

        #region Turnos
        /// <summary>
        /// Obtener el ultimo turno de trabajo del usuario
        /// </summary>
        /// <param name="uidUser"></param>
        /// <returns></returns>
        public DataTable GetLastWorkShiftByUserId(Guid uidUser)
        {
            string query = $@"
                SELECT 
                    TOP 1 
                    T.[UidTurnoRepartidor] AS [Uid],
                    T.[UidUsuario],
                    T.[DtmHoraInicio] AS [FechaInicio],
                    T.[DtmHoraFin] AS [FechaFin],
                    T.[LngFolio] AS [Folio],
                    V.[mfondo] AS [Fondo],
                    (SELECT TOP 1 B.[UidEstatusTurno] FROM [BitacoraEstatusTurno] AS B WHERE B.[UidTurnoRepartidor] = T.[UidTurnoRepartidor] ORDER BY B.[DtmFecha]) AS [UidEstatusActual]
                FROM TurnoRepartidor AS T 
                    INNER JOIN VehiculoUsuario AS V on V.[UidUsuario] = T.[UidUsuario] 
                WHERE V.[UidUsuario] = '{uidUser.ToString()}'
                ORDER BY T.[DtmHoraInicio] DESC";

            DataTable data = this.dbConexion.Consultas(query);
            return data;
        }

        public DataTable GetWorkOrderWorkShiftBalance(Guid uidTurnoRepartidor)
        {
            string query = $@"
                SELECT 
                    count(*) AS TotalOrdenes,
                    sum(os.MTotalSucursal) AS TotalSucursal,
                    sum(ot.MtotalEnvio) AS TotalEnvio, 
                    dbo.asp_ObtenerLaCantidadDeEfectivoEnMano(tr.UidTurnoRepartidor) AS Efectivo, 
                    dbo.ObtenerLiquidacionesTurnoRepartidor(tr.uidturnorepartidor) AS Liquidacion, 
                    dbo.ObtenerLasGananciasRepartidor(tr.Uidturnorepartidor) AS Ganancias, 
                    dbo.ObtenerMontoDePagosDeOrdenesEnTurnoRepartidor(tr.uidturnorepartidor) AS PagosSucursales, 
                    dbo.ObtenerCantidadOrdenesPagadasTurnoRepartidor(tr.uidturnorepartidor) AS CantidadDePagos, 
                    dbo.ObtenerRecargasTurnoRepartidor(tr.uidturnorepartidor) AS Recarga, 
                    SUM(ot.MPropina) as Propina  
                FROM OrdenSucursal os 
                    inner join OrdenTarifario ot ON ot.UidOrden = os.UidRelacionOrdenSucursal 
                    inner join OrdenRepartidor orep ON orep.UidOrden = ot.UidRelacionOrdenTarifario 
                    inner join TurnoRepartidor tr ON tr.UidTurnoRepartidor = orep.UidTurnoRepartidor and dbo.asp_ObtenerUltimoEstatusOrdenRepartidor(orep.UidRelacionOrdenRepartidor) != '12748F8A-E746-427D-8836-B54432A38C07' and dbo.asp_ObtenerUltimoEstatusOrdenRepartidor(orep.UidRelacionOrdenRepartidor) = '7DA3A42F-2271-47B4-B9B8-EDD311F56864' 
                WHERE tr.UidTurnoRepartidor = '{uidTurnoRepartidor.ToString()}' 
                GROUP BY tr.UidTurnoRepartidor";

            DataTable data = this.dbConexion.Consultas(query);
            return data;
        }
        #endregion

        #region Ordenes
        /// <summary>
        /// Obtener la ultima orden asignada al repartidor
        /// </summary>
        /// <param name="uidUser"></param>
        /// <returns></returns>
        public DataTable ReadLastAssignedOrder(Guid uidTurnoRepartidor)
        {
            string query = $@"
                SELECT
                    TOP 1
                    OD.[UidRelacionOrdenRepartidor] AS [UidOrdenRepartidor],
                    OT.[UidOrden] AS [UidOrdenSucursal],
                    OT.[UidRelacionOrdenTarifario] AS [UidOrdenTarifario],
                    S.[UidSucursal],
                    S.[Identificador] AS [IdentificadorSucursal],
                    OS.[IntFolio] AS [FolioOrdenSucursal],
                    O.[UidDireccion] AS [UidDireccionCliente],
                    O.[UidOrden],
                    dbo.asp_ObtenerUltimoEstatusOrdenTarifario(OT.[UidRelacionOrdenTarifario]) as UidEstatusOrdenTarifario,
                    dbo.EstatusActualDeOrden(OS.[UidRelacionOrdenSucursal]) as UidEstatusOrdenGeneral, 
                    dbo.asp_ObtenerUltimoEstatusOrdenRepartidor(OD.[UidRelacionOrdenRepartidor]) as UidEstatusOrdenRepartidor,
                    M.[NombreComercial] AS [NombreEmpresa],
                    ISNULL(E.NVchRuta, '') AS [UrlLogoEmpresa],
                    ISNULL(US.[VchLatitud], '0') AS [LatSucursal],
                    ISNULL(US.[VchLongitud], '0') AS [LongSucursal],
                    ISNULL(UC.[VchLatitud], '0') AS [LatCliente],
                    ISNULL(UC.[VchLongitud], '0') AS [LongCliente],
                    CONCAT(
                        ISNULL(DS.Calle0, ''), ' ',
                        ISNULL(DS.Calle1, ''), ' ',
                        ISNULL(DS.Calle2, ''), ', ',
                        ISNULL(DS.CodigoPostal, ''), ', ',
                        (SELECT TOP 1 Nombre FROM [Colonia] WHERE UidColonia = DS.UidColonia)
                    ) AS DireccionSucursal,
                    dbo.ObtenerEstatusDeCobro(OT.[UidOrden]) AS EstatusPago,
                    (
                    SELECT 
                        COUNT(*) 
                    FROM ContratoDeServicio 
                    WHERE UidSucursalSuministradora = (select uidsucursal from OrdenSucursal where UidRelacionOrdenSucursal = OT.UidOrden)
                        AND UidSucursalDistribuidora IN (
                            SELECT 
                                szr.UidSucursal 
                            FROM OrdenSucursal sos 
                                INNER JOIN OrdenTarifario sot ON ot.UidOrden = sos.UidRelacionOrdenSucursal 
                                INNER JOIN Tarifario st ON t.UidRegistroTarifario = sot.UidTarifario 
                                INNER JOIN ZonaDeRecoleccion szr on st.UidRelacionZonaRecolecta = szr.UidZonaDeRecolecta 
                            where sos.UidRelacionOrdenSucursal = OT.UidOrden) AND BiPagaAlRecogerOrdenes = 1
                    ) AS IntPagoEnRecolecta
                FROM [OrdenTarifario] AS OT
                    INNER JOIN [Tarifario] AS T ON T.[UidRegistroTarifario] = OT.[UidTarifario]
                    INNER JOIN [ZonaDeRecoleccion] AS ZR ON ZR.[UidZonaDeRecolecta] = T.[UidRelacionZonaRecolecta]
                    INNER JOIN [OrdenSucursal] AS OS ON OS.[UidRelacionOrdenSucursal] = OT.[UidOrden]
                    INNER JOIN [Sucursales] AS S ON S.[UidSucursal] = OS.[UidSucursal]
                    INNER JOIN [Empresa] M ON M.[UidEmpresa] = S.[UidEmpresa]
                    LEFT JOIN [ImagenEmpresa] AS IE ON IE.[UidEmpresa] = M.[UidEmpresa]
                    LEFT JOIN [Imagenes] AS E ON E.[UidImagen] = IE.[UidImagen]
                    INNER JOIN [OrdenRepartidor] AS OD ON OD.[UidOrden] = OT.[UidRelacionOrdenTarifario]
                    INNER JOIN [Ordenes] AS O ON O.[UidOrden] = OS.[UidOrden]
                    LEFT JOIN [Ubicacion] AS US ON US.[UidUbicacion] = (SELECT TOP 1 [UidUbicacion] FROM [UbicacionSucursal] WHERE [UidSucursal] = S.[UidSucursal])
                    LEFT JOIN [Ubicacion] AS UC ON UC.[UidUbicacion] = (SELECT TOP 1 [UidUbicacion] FROM [DireccionUbicacion] WHERE [UidDireccion] = O.[UidDireccion])
                    LEFT JOIN [Direccion] AS DS ON DS.[UidDireccion] = S.[UidDireccion]
                WHERE OD.[UidTurnoRepartidor] = '{uidTurnoRepartidor.ToString()}'
                ORDER BY OD.[dtmFechaAsignacion] DESC";
            DataTable data = this.dbConexion.Consultas(query);
            return data;
        }

        /// <summary>
        /// Obtener las ordenes actuales que han sido confirmadas por el repartidor (aceptadas)
        /// </summary>
        /// <param name="uidUser"></param>
        /// <returns></returns>
        public DataTable ReadConfirmedAssignedOrders(Guid uidUser)
        {
            string query = "";
            DataTable data = this.dbConexion.Consultas(query);
            return data;
        }

        public DataTable ReadAllProductOrder(Guid uidOrden)
        {

            string query = $@"
                SELECT 
                    P.[VchNombre] AS [Nombre],
                    OP.[intCantidad] AS [Cantidad]
                FROM [Productos] AS P
                    INNER JOIN [SeccionProducto] AS SP ON SP.[UidProducto] = P.[UidProducto]
                    INNER JOIN [OrdenProducto] AS OP ON OP.[UidSeccionProducto] = SP.[UidSeccionProducto]
                    INNER JOIN [OrdenSucursal] AS OS ON OS.[UidRelacionOrdenSucursal] = OP.[UidOrden]
                    INNER JOIN [Ordenes] AS O ON O.[UidOrden] = OS.[UidOrden]
                WHERE O.[UidOrden] = '{uidOrden.ToString()}'";
            DataTable data = this.dbConexion.Consultas(query);
            return data;
        }
        #endregion

        #region Bitacora Estatus del repartidor
        /// <summary>
        /// Obtener el ultimo registro de la bitacora de estatus del repartidor
        /// </summary>
        /// <param name="uidUsuario"></param>
        /// <returns></returns>
        public DataTable ObtenerUltimoEstatusBitacora(Guid uidUsuario)
        {
            string query = $@"
                SELECT
                    TOP 1
                    B.[UidRelacionEstatusRepartidor] AS [Uid],
                    B.[UidEstatusRepartidor] AS [UidEstatus],
                    B.[DtmFecha] AS [Fecha],
                    B.[UidUsuario],
                    E.VchNombre AS Estatus
                FROM [BitacoraEstatusRepartidor] AS B
                    INNER JOIN [EstatusRepartidor] AS E ON E.UidEstatusRepartidor = B.UidEstatusRepartidor
                WHERE B.UidUsuario = '{uidUsuario.ToString()}' ORDER BY B.[DtmFecha] DESC";

            DataTable data = this.dbConexion.Consultas(query);
            return data;
        }
        #endregion
    }
}
