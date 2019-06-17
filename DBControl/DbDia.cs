using System.Data;

namespace DBControl
{
    public class DbDia
    {
        #region Propiedades

        Conexion Datos;
        #endregion

        #region metodos

        public void EliminaRelacionDiaOferta(string UidOferta)
        {
            Datos = new Conexion();
            string Query = "delete from DiaOferta where uidoferta = '" + UidOferta + "'";

            Datos.Consultas(Query);
        }

        public DataTable ObtenerDiaOferta(string UidOferta)
        {
            Datos = new Conexion();
            string Query = "select UidDia from DiaOferta where uidOferta = '" + UidOferta + "'";
            return Datos.Consultas(Query);
        }
        #endregion
    }
}
