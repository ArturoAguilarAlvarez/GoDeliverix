using System;
using System.Collections.Generic;
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

        public bool Registry(Guid UidOrdenFormaCobro, Guid UidFormaCobro, Guid UidOrden, Guid UidEstatusCobro, decimal monto, decimal? descuentoMonedero)
        {
            SqlCommand command = new SqlCommand();
            try
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "asp_RegistrarPagoClienteMovil";

                command.Parameters.AddWithValue("@UidOrdenFormaDeCobro", UidOrdenFormaCobro);

                command.Parameters.AddWithValue("@UidFormaDeCobro", UidFormaCobro);

                command.Parameters.AddWithValue("@UidOrden", UidOrden);

                command.Parameters.AddWithValue("@UidEstatusDeCobro", UidEstatusCobro);

                command.Parameters.AddWithValue("@Monto", monto);

                if (descuentoMonedero.HasValue)
                {
                    command.Parameters.AddWithValue("@DescuentoMonedero", descuentoMonedero);
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
    }
}
