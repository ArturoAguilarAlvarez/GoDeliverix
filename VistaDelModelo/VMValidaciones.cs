using DBControl;
using System.Data;

namespace VistaDelModelo
{
    public class VMValidaciones
    {
        Conexion oConexion;

        /// <summary>
        /// Valida si existe el usuario en el sistema
        /// </summary>
        /// <param name="StrUsuario">Usuario</param>
        /// <returns>Retorna verdadero si existe el registro</returns>
        public bool ValidaExistenciaDeUsuario(string StrUsuario)
        {
            oConexion = new Conexion();
            bool Resultado = false;
            string query = "Select * from usuarios where usuario = '" + StrUsuario + "'";
            DataTable Tabla = oConexion.Consultas(query);
            if (Tabla.Rows.Count != 0)
            {
                Resultado = true;
            }
            return Resultado;
        }


    }
}
