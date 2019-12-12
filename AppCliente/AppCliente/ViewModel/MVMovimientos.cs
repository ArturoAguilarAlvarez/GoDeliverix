using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
namespace AppCliente.ViewModel
{
    public class MVMovimientos
    {
        private string _strConcepto;

        public string StrConcepto
        {
            get { return _strConcepto; }
            set { _strConcepto = value; }
        }

        private string _strMovimiento;

        public string StrMovimiento
        {
            get { return _strMovimiento; }
            set { _strMovimiento = value; }
        }

        private long _lngFolio;

        public long LngFolio
        {
            get { return _lngFolio; }
            set { _lngFolio = value; }
        }

        private decimal _MMonto;

        public decimal MMonto
        {
            get { return _MMonto; }
            set { _MMonto = value; }
        }
        private DateTime _dtmFechaDeRegistro;

        public DateTime DtmFechaDeRegistro
        {
            get { return _dtmFechaDeRegistro; }
            set { _dtmFechaDeRegistro = value; }
        }

        private Color _CCOlor;

        public Color CColor
        {
            get { return _CCOlor; }
            set { _CCOlor = value; }
        }

        private List<MVMovimientos> _listaDeMovimientos;

        public List<MVMovimientos> ListaDeMovimientos
        {
            get { return _listaDeMovimientos; }
            set { _listaDeMovimientos = value; }
        }
    }
}
