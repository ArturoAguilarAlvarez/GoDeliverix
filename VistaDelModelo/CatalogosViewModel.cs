using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaDelModelo
{
    public class CatalogosViewModel
    {
        #region Properties
        private Guid _Uid;

        public Guid Uid
        {
            get { return _Uid; }
            set { _Uid = value; }
        }

        private Guid _UidGiro;

        public Guid UidGiro
        {
            get { return _UidGiro; }
            set { _UidGiro = value; }
        }
        private string _strNombreGiro;

        public string NombreGiro
        {
            get { return _strNombreGiro; }
            set { _strNombreGiro = value; }
        }
        private Guid _UidCategoria;

        public Guid UidCategoria
        {
            get { return _UidCategoria; }
            set { _UidCategoria = value; }
        }
        private string _strNombreCategoria;

        public string NombreCategoria
        {
            get { return _strNombreCategoria; }
            set { _strNombreCategoria = value; }
        }
        private Guid _UidSubcategoria;

        public Guid UidSubcategoria
        {
            get { return _UidSubcategoria; }
            set { _UidSubcategoria = value; }
        }
        private string _strNombreSubcategoria;

        public string NombreSubcategoria
        {
            get { return _strNombreSubcategoria; }
            set { _strNombreSubcategoria = value; }
        }

        #endregion
    }
}
