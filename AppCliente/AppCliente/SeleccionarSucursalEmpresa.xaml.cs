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
	public partial class SeleccionarSucursalEmpresa : ContentPage
	{
		public SeleccionarSucursalEmpresa ()
		{
			InitializeComponent ();
		}
        Picker Menu;
        VMEmpresas ObjItem;
        Label UiSucursal;
        Label txtNombreSucursal;
        public SeleccionarSucursalEmpresa(Picker Menu, List<VMSucursales> lista, VMEmpresas ObjItem, Label UiSucursal,Label txtNombreSucursal)
        {
            InitializeComponent();
            this.ObjItem = ObjItem;
            var item = lista.Find(t => t.ID.ToString() == UiSucursal.Text);
            MyListViewSeleccionarEmpresas.ItemsSource = lista;
            MyListViewSeleccionarEmpresas.SelectedItem = item;

            this.txtNombreSucursal = txtNombreSucursal;
            this.UiSucursal = UiSucursal;
            this.Menu = Menu;

            //Menu.ItemsSource = App.MVOferta.ListaDeOfertas;
        }

        private void MyListViewSeleccionarEmpresas_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = ((ItemTappedEventArgs)e);
            VMSucursales ObjItem = (VMSucursales)item.Item;
            App.MVOferta.Buscar(UIDSUCURSAL: ObjItem.ID);
            UiSucursal.Text = ObjItem.ID.ToString();
            Menu.ItemsSource = null;
            Menu.ItemsSource = App.MVOferta.ListaDeOfertas;
            Menu.SelectedIndex = 0;
            Navigation.PopAsync();
            txtNombreSucursal.Text = ObjItem.IDENTIFICADOR;
        }
    }
}