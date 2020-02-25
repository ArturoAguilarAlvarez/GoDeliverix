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
    public partial class SeleccionarSucursalPrecioProducto : ContentPage
    {


        public static List<VMProducto> ListaPreciosProcto = new List<VMProducto>();
        Button btn;
        Button lbEmpresa;
        Label lbCosto;
        Label uidEmpresaSeleccionada;
        Label sucursal;
        double cantidad;

        public SeleccionarSucursalPrecioProducto(List<VMProducto> lista, Button btn, Button lbEmpresa, Label lbCosto, double cantidad, Label uidEmpresaSeleccionada, Label sucursal)
        {
            InitializeComponent();
            this.sucursal = sucursal;
            int index = App.MVProducto.ListaDePreciosSucursales.FindIndex(t => t.StrIdentificador == lbEmpresa.Text);
            MyListViewBusquedaEmpresaDelProducto.ItemsSource = App.MVProducto.ListaDePreciosSucursales;
            MyListViewBusquedaEmpresaDelProducto.SelectedItem = App.MVProducto.ListaDePreciosSucursales[index];
            ListaPreciosProcto = lista;
            this.btn = btn;
            this.lbEmpresa = lbEmpresa;
            this.lbCosto = lbCosto;
            this.cantidad = cantidad;
            this.uidEmpresaSeleccionada = uidEmpresaSeleccionada;
        }

        private void MyListViewBusquedaEmpresaDelProducto_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                VMProducto a = (VMProducto)MyListViewBusquedaEmpresaDelProducto.SelectedItem;
                lbEmpresa.Text = a.StrIdentificador;
                string i = a.UidSeccion.ToString();
                lbCosto.Text = "$" + a.StrCosto;
                string sad = a.UID.ToString();
                double costo = double.Parse(a.StrCosto);
                uidEmpresaSeleccionada.Text = a.UID.ToString();
                sucursal.Text = a.UidSucursal.ToString();
                costo = (cantidad) * (costo);
                btn.Text = "Agregar carrito $" + costo;
                Navigation.PopAsync();
            }
            catch (Exception)
            {
                DisplayAlert("Alerta", "Seleciona una sucursal", "ok");
            }
        }

        private void BtnCabiarEmpresa_clicked(object sender, EventArgs e)
        {
            try
            {
                VMProducto a = (VMProducto)MyListViewBusquedaEmpresaDelProducto.SelectedItem;

                lbEmpresa.Text = a.StrIdentificador;

                lbCosto.Text = a.StrCosto;
                string sad = a.UID.ToString();
                double costo = double.Parse(a.StrCosto);
                uidEmpresaSeleccionada.Text = a.UID.ToString();
                sucursal.Text = a.UidSucursal.ToString();
                costo = (cantidad) * (costo);

                btn.Text = "Agregar carrito $" + costo; ;


                Navigation.PopAsync();
            }
            catch (Exception)
            {
                DisplayAlert("Alerta", "Seleciona una sucursal", "ok");
            }

        }
    }
}