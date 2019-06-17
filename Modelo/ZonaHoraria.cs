using DBControl;
using System;
using System.Data;
using System.Data.SqlClient;
namespace Modelo
{
    public class ZonaHoraria
    {
        Conexion oConexion;
        public string Id { get; set; }
        public string NombreEstandar { get; set; }
        public string NombreCompleto { get; set; }
        public string Uid { get; set; }

        public void GuardarZonaHorariaConPais(string uidPais)
        {
            oConexion = new Conexion();
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_AgregarZonaHorariaConPais";

                Comando.Parameters.Add("@UidPais", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidPais"].Value = new Guid(uidPais);

                Comando.Parameters.Add("@IdZOnaHoraria", SqlDbType.VarChar, 1000);
                Comando.Parameters["@IdZOnaHoraria"].Value = Id;

                oConexion.ModificarDatos(Comando);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void GuardarZonaHorariaConEstados(Guid uidEstado)
        {
            oConexion = new Conexion();
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_AgregarRelacionZonaHorariaEstados";

                Comando.Parameters.Add("@UidEstado", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidEstado"].Value = uidEstado;

                Comando.Parameters.Add("@UidZonaHorariaPais", SqlDbType.VarChar, 1000);
                Comando.Parameters["@UidZonaHorariaPais"].Value = Uid;

                oConexion.ModificarDatos(Comando);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
