using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AppCliente.ViewModel
{
    public class MvmSucursales
    {
        private string _StrIdentificador;

        public string StrIdentificador
        {
            get { return _StrIdentificador; }
            set { _StrIdentificador = value; }
        }
        private string _StrDireccion;

        public string StrDireccion
        {
            get { return _StrDireccion; }
            set { _StrDireccion = value; }
        }
        private string _StrCosto;

        public string StrCosto
        {
            get { return _StrCosto; }
            set { _StrCosto = value; }
        }
        private Color _CcolorSeleccion;

        public Color CSeleccion
        {
            get { return _CcolorSeleccion; }
            set { _CcolorSeleccion = value; }
        }
        private Guid _UidSeccion;

        public Guid UidSeccion
        {
            get { return _UidSeccion; }
            set { _UidSeccion = value; }
        }
        private Guid _UID;

        public Guid UID
        {
            get { return _UID; }
            set { _UID = value; }
        }
        private Guid _UidSucursal;

        public Guid UidSucursal
        {
            get { return _UidSucursal; }
            set { _UidSucursal = value; }
        }

    }
}
