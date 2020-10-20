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
    }
}
