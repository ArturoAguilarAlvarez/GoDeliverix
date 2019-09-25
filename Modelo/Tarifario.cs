using System;
using System.Data;
using System.Data.SqlClient;
using DBControl;

namespace Modelo
{
    public class Tarifario
    {
        #region Propiedades
        Conexion CN = new Conexion();
        public Guid UidTarifario { get; set; }
        /// <summary>
        /// Relacion de zona de recoleccion
        /// </summary>
        public Guid UidRelacionZR { get; set; }
        public string StrNombreColoniaZR { get; set; }

        /// <summary>
        /// Relacion de zona de entrega
        /// </summary>
        public Guid UidRelacionZE { get; set; }
        public string StrNombreColoniaZE { get; set; }
        /// <summary>
        /// Precio de la tafira
        /// </summary>
        public decimal DPrecio { get; set; }
        public string StrNombreSucursalDistribuidora { get; set; }
        public Guid UidContrato { get; set; }
        #endregion

        #region metodos
        public bool Guardar()
        {
            bool resultado = false;
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_AgregarTarifario";

                Comando.Parameters.Add("@UidRegistro", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidRegistro"].Value = UidTarifario;

                Comando.Parameters.Add("@UidRelacionZR", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidRelacionZR"].Value = UidRelacionZR;

                Comando.Parameters.Add("@UidRelacionZE", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidRelacionZE"].Value = UidRelacionZE;

                Comando.Parameters.Add("@MCosto", SqlDbType.Decimal);
                Comando.Parameters["@MCosto"].Value = DPrecio;



                resultado = CN.ModificarDatos(Comando);
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }

        public bool GuardarTarifarioConContrato()
        {
            bool resultado = false;
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_AgregarTarifarioConContrato";

                Comando.Parameters.Add("@UidContrato", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidContrato"].Value = UidContrato;

                Comando.Parameters.Add("@UidTarifario", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidTarifario"].Value = UidTarifario;


                resultado = CN.ModificarDatos(Comando);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public bool RelacionConOrden(Guid uidorden, Guid uidTarifario,decimal DPropina)
        {
            bool resultado = false;
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_AgregarTarifarioOrden";

                Comando.Parameters.Add("@Uidorden", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@Uidorden"].Value = uidorden;

                Comando.Parameters.Add("@UidTarifario", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidTarifario"].Value = uidTarifario;

                Comando.Parameters.Add("@DPropina", SqlDbType.Money);
                Comando.Parameters["@DPropina"].Value = DPropina;

                resultado = CN.ModificarDatos(Comando);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public bool AgregarCodigoOrden(Guid UidCodigo, Guid UidLicencia, Guid uidorden)
        {
            bool resultado = false;
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_AgregarCodigoAOrden";

                Comando.Parameters.Add("@Uidorden", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@Uidorden"].Value = uidorden;

                Comando.Parameters.Add("@uidCodigo", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@uidCodigo"].Value = UidCodigo;

                resultado = CN.ModificarDatos(Comando);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        #endregion
    }
}
