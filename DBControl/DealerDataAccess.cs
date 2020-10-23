﻿using System;
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
                    ISNULL(E.NVchRuta, '') AS [UrlLogoEmpresa]
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
                WHERE O.[UidOrden] = {uidOrden.ToString()}";
            DataTable data = this.dbConexion.Consultas(query);
            return data;
        }
        #endregion
    }
}
