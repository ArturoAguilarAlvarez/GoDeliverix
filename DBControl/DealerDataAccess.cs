using System;
using System.Collections.Generic;
using System.Data;
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
        public DataTable ReadAllPhonesByUserId(Guid uidUser) {
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
