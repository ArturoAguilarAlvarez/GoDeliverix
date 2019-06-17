namespace Modelo
{
    public class TipoDeEmpresa
    {
        #region Propiedades
        private int _idTipo;
        public int ID
        {
            get { return _idTipo; }
            set { _idTipo = value; }
        }

        private string _strNombre;
        public string NOMBRE
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }
        #endregion
        #region Constructores
        public TipoDeEmpresa()
        {

        }
        public TipoDeEmpresa(int id)
        {
            ID = id;
        }
        public TipoDeEmpresa(string nombre)
        {
            NOMBRE = nombre;
        }
        public TipoDeEmpresa(int id, string nombre)
        {
            ID = id;
            NOMBRE = nombre;
        }
        #endregion
    }
}
