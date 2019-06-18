using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCliente
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistorialEmpresaProductos : ContentPage
	{
		public HistorialEmpresaProductos ()
		{
			InitializeComponent ();
            MyListViewHistorial.ItemsSource = App.MVOrden.ListaDeProductos;
        }
	}
}