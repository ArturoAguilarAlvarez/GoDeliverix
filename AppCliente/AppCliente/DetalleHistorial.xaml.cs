using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VistaDelModelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleHistorial : ContentPage
    {
        public DetalleHistorial()
        {
            InitializeComponent();
        }
        public DetalleHistorial(VMOrden ObjItem)
        {
            InitializeComponent();
            App.MVOrden.ObtenerSucursaleDeOrden(ObjItem.Uidorden);
        }
    }
}