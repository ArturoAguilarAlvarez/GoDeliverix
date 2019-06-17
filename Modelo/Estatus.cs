namespace Modelo
{
    public class Estatus
    {
        #region Propiedades
        private int _idEstatus;
        public int ID
        {
            get { return _idEstatus; }
            set { _idEstatus = value; }
        }
        private string _strNombre;
        public string NOMBRE
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }
        #endregion
        #region Constructores
        public Estatus()
        {

        }
        public Estatus(int id)
        {
            ID = id;
        }
        public Estatus(string nombre)
        {
            NOMBRE = nombre;
        }
        public Estatus(int id, string nombre)
        {
            ID = id;
            NOMBRE = nombre;
        }
        #endregion
    }
}
