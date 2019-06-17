using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DBControl
{
    public class DbZonaHoraria
    {
        Conexion oConexion;

        public DataTable ObtenerTodasLasZonasHorarias()
        {
            oConexion = new Conexion();
            string query = "select idZonaHoraria,NombreEstandar,NombreCompleto from zonahoraria";
            return oConexion.Consultas(query);



           
        }

        public DataTable ObtenerZonasPorPais(string uidPais)
        {
            oConexion = new Conexion();
            string query = "select zhp.UidZonaHorariaPais,zh.NombreCompleto,zh.IdZonaHoraria,zh.NombreEstandar from ZonaHoraria zh inner join ZonaHorariaPais zhp on zhp.IdZonaHoraria = zh.IdZonaHoraria where zhp.UidPais = '"+ uidPais + "' ";
            return oConexion.Consultas(query);
        }

        public void EliminarZonasHorariasPorPais(string uidPais)
        {
            oConexion = new Conexion();
            string query = "delete from ZonaHorariaPais where UidPais = '" + uidPais + "' ";
            oConexion.Consultas(query);
        }
    }
}
