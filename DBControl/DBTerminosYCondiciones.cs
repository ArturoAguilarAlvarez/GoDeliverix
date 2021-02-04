using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBControl
{
    public class DBTerminosYCondiciones
    {
        #region Propiedades
        private Conexion oConexion { get; set; }
        #endregion

        public DBTerminosYCondiciones()
        {

        }

        #region Consultas
        public DataTable VerificaUsuario(string UidUsuario, string Idioma)
        {
            oConexion = new Conexion();
            string query = @"select  top 1  tyc.Uid, ityc.VchUrl from TYCusuario tycu inner join TerminosYCondiciones tyc on tyc.Uid = tycu.UidTYC
                            inner join IdiomaTYC ityc on ityc.UidTYC = tyc.Uid where tycu.UidUsuario = '" + UidUsuario + "' and (ityc.VchNombre = '" + Idioma + "' or ityc.BDefault =1) and tycu.BAceptada = 0 order by tyc.DtmFechaDeCreacion desc";
            return oConexion.Consultas(query);
        }
        public DataTable BuscaTerminosYCondiciones(string Idioma, string UidLada = "", string UidUsuario = "")
        {
            oConexion = new Conexion();
            string query = "";
            if (!string.IsNullOrEmpty(UidLada))
            {
                query = "select  top 1  tyc.Uid, ityc.VchUrl from TerminosYCondiciones tyc inner join IdiomaTYC ityc on ityc.UidTYC = tyc.Uid inner join Paises p on p.UidTerminosYCondiciones = tyc.Uid inner join LadasInternacional li on li.UidPais = p.UidPais where li.UidLada = '" + UidLada + "' and (ityc.VchNombre = '" + Idioma + "' or ityc.BDefault =1)  order by tyc.DtmFechaDeCreacion desc";
            }
            if (!string.IsNullOrEmpty(UidUsuario))
            {
                query = "select  top 1  tyc.Uid, ityc.VchUrl from TYCusuario tycu inner join TerminosYCondiciones tyc on tyc.Uid = tycu.UidTYC inner join IdiomaTYC ityc on ityc.UidTYC = tyc.Uid where tycu.UidUsuario = '" + UidUsuario + "' and (ityc.VchNombre = '" + Idioma + "' or ityc.BDefault =1) and tycu.BAceptada = 1 order by tyc.DtmFechaDeCreacion desc";
            }
            return oConexion.Consultas(query);
        }
        #endregion
    }
}
