using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppCliente.ViewModel
{
    public class MVMProductos
    {
        private Guid _UidRegistroProductoEnCarrito;

        public Guid UidRegistroProductoEnCarrito
        {
            get { return _UidRegistroProductoEnCarrito; }
            set { _UidRegistroProductoEnCarrito = value; }
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
        private Guid _UidNota;

        public Guid UidNota
        {
            get { return _UidNota; }
            set { _UidNota = value; }
        }
        private Guid _UidTarifario;

        public Guid UidTarifario
        {
            get { return _UidTarifario; }
            set { _UidTarifario = value; }
        }
        private string _STRNOMBRE;

        public string STRNOMBRE
        {
            get { return _STRNOMBRE; }
            set { _STRNOMBRE = value; }
        }
        private string _StrNota;

        public string StrNota
        {
            get { return _StrNota; }
            set { _StrNota = value; }
        }
        private string _STRRUTA;

        public string STRRUTA
        {
            get { return _STRRUTA; }
            set { _STRRUTA = value; }
        }
        private int _Cantidad;

        public int Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        private string _Empresa;

        public string Empresa
        {
            get { return _Empresa; }
            set { _Empresa = value; }
        }
        private string _StrCosto;

        public string StrCosto
        {
            get { return _StrCosto; }
            set { _StrCosto = value; }
        }
        private bool _IsVisible;

        public bool IsVisible
        {
            get { return _IsVisible; }
            set { _IsVisible = value; }
        }
        private decimal _Total;

        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }
        private decimal _Subtotal;

        public decimal Subtotal
        {
            get { return _Subtotal; }
            set { _Subtotal = value; }
        }
        private decimal _CostoEnvio;

        public decimal CostoEnvio
        {
            get { return _CostoEnvio; }
            set { _CostoEnvio = value; }
        }
        private decimal _DPropina;

        public decimal DPropina
        {
            get { return _DPropina; }
            set { _DPropina = value; }
        }
        private Guid _UidSeccionPoducto;

        public Guid UidSeccionPoducto
        {
            get { return _UidSeccionPoducto; }
            set { _UidSeccionPoducto = value; }
        }
        private Color _color;

        public Color CColor
        {
            get { return _color; }
            set { _color = value; }
        }
            
        private List<MVMProductos> _ListaDeProductos;

        public List<MVMProductos> ListaDeProductos
        {
            get { return _ListaDeProductos; }
            set { _ListaDeProductos = value; }
        }
        private List<MVMProductos> _listainformacionsucursales;

        public List<MVMProductos> listainformacionsucursales
        {
            get { return _listainformacionsucursales; }
            set { _listainformacionsucursales = value; }
        }

    }
}
