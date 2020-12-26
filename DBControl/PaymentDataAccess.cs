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
    /// Acceso de datos a la gestion de pagos
    /// <see cref="[OrdenFormaDeCobro]"/>
    /// </summary>
    public class PaymentDataAccess
    {
        /// <summary>
        /// Conexion a la base de datos
        /// </summary>
        protected readonly Conexion dbConexion;

        public PaymentDataAccess()
        {
            this.dbConexion = new Conexion();
        }

        /// <summary>
        /// Registrar pago para la orden de la sucursal
        /// </summary>
        /// <param name="uidOrden"></param>
        /// <param name="uidUsuario"></param>
        /// <param name="uidDireccion"></param>
        /// <param name="uidSucursal"></param>
        /// <param name="uidRelacionOrdenSucursal"></param>
        /// <param name="uidTarifario"></param>
        /// <param name="codigoEntrega"></param>
        /// <param name="total"></param>
        /// <param name="totalSucursal"></param>
        /// <param name="descuentoMonedero"></param>
        /// <param name="comisionPagoTarjeta"></param>
        /// <returns></returns>
        public bool RegistryBranchePayment(Guid uidOrden, Guid uidUsuario, Guid uidDireccion, Guid uidSucursal, Guid uidRelacionOrdenSucursal, Guid uidTarifario, long codigoEntrega, decimal total, decimal totalSucursal, decimal? descuentoMonedero, decimal? comisionPagoTarjeta)
        {
            SqlCommand command = new SqlCommand();
            try
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "asp_Agrega_Orden";

                command.Parameters.Add("@UidOrden", SqlDbType.UniqueIdentifier);
                command.Parameters["@UidOrden"].Value = uidOrden;

                command.Parameters.Add("@MTotal", SqlDbType.Money);
                command.Parameters["@MTotal"].Value = total;

                command.Parameters.Add("@BiCodigoDeEntrega", SqlDbType.BigInt);
                command.Parameters["@BiCodigoDeEntrega"].Value = codigoEntrega;

                command.Parameters.Add("@MTotalSucursal", SqlDbType.Money);
                command.Parameters["@MTotalSucursal"].Value = totalSucursal;

                command.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
                command.Parameters["@UidUsuario"].Value = uidUsuario;

                command.Parameters.Add("@UidDireccion", SqlDbType.UniqueIdentifier);
                command.Parameters["@UidDireccion"].Value = uidDireccion;

                command.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                command.Parameters["@UidSucursal"].Value = uidSucursal;

                command.Parameters.Add("@RelacionDeOrden", SqlDbType.UniqueIdentifier);
                command.Parameters["@RelacionDeOrden"].Value = uidRelacionOrdenSucursal;

                command.Parameters.Add("@TarifarioDistribuidora", SqlDbType.UniqueIdentifier);
                command.Parameters["@TarifarioDistribuidora"].Value = uidTarifario;

                if (descuentoMonedero.HasValue)
                {
                    command.Parameters.Add("@DescuentoMonedero", SqlDbType.Money);
                    command.Parameters["@DescuentoMonedero"].Value = descuentoMonedero;
                }

                if (comisionPagoTarjeta.HasValue)
                {
                    command.Parameters.Add("@ComisionPagoTarjeta", SqlDbType.Money);
                    command.Parameters["@ComisionPagoTarjeta"].Value = comisionPagoTarjeta;
                }

                return this.dbConexion.ModificarDatos(command);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RegistryWalletTransaction(Guid UidUsuario, Guid UidTipoDeMovimiento, Guid UidConcepto, Guid UidDireccion, decimal descuento)
        {
            SqlCommand command = new SqlCommand();
            try
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "asp_MovimientosMonedero";

                command.Parameters.AddWithValue("@UidUsuario", UidUsuario);

                command.Parameters.AddWithValue("@UidTipoDeMovimiento", UidTipoDeMovimiento);

                command.Parameters.AddWithValue("@UidConcepto", UidConcepto);

                command.Parameters.AddWithValue("@UidDireccionDeOperacion", UidDireccion);

                command.Parameters.AddWithValue("@MMonto", descuento);

                return this.dbConexion.ModificarDatos(command);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Cancelar la orden y regresar aplicar reembolso al monedero
        /// </summary>
        /// <param name="UidOrden"></param>
        /// <param name="UidUsuario"></param>
        /// <param name="UidDireccion"></param>
        /// <returns></returns>
        public bool CancelOrderAndApplyDiscount(Guid UidOrdenSucursal, Guid UidUsuario, Guid UidDireccion)
        {

            SqlCommand command = new SqlCommand();
            try
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "BitacoraEstatusOrdenReembolso";

                command.Parameters.Add("@SPUidOrdenSucursal", SqlDbType.UniqueIdentifier);
                command.Parameters["@SPUidOrdenSucursal"].Value = UidOrdenSucursal;

                command.Parameters.Add("@SPUidEstatusEnOrden", SqlDbType.UniqueIdentifier);
                command.Parameters["@SPUidEstatusEnOrden"].Value = Guid.Parse("A2D33D7C-2E2E-4DC6-97E3-73F382F30D93");

                command.Parameters.Add("@SPParametro", SqlDbType.VarChar,1);
                command.Parameters["@SPParametro"].Value = char.Parse("S");

                command.Parameters.Add("@SPUidUsuario", SqlDbType.UniqueIdentifier);
                command.Parameters["@SPUidUsuario"].Value = UidUsuario;

                command.Parameters.Add("@SPUidDireccion", SqlDbType.UniqueIdentifier);
                command.Parameters["@SPUidDireccion"].Value = UidDireccion;

                return this.dbConexion.ModificarDatos(command);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
