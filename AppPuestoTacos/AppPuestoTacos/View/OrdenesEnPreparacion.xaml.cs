﻿using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VistaDelModelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPuestoTacos.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OrdenesEnPreparacion : ContentPage
	{

        bool Ordenamiento = false;
        public OrdenesEnPreparacion ()
        {
            try
            {
                InitializeComponent();
                App.MVOrden.BuscarOrdenesAppSucursal("Sucursal", UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), EstatusSucursal: "Pendiente para elaborar", TipoDeSucursal: "S");
                MyListviewOrdenesPorRealizar.ItemsSource = App.MVOrden.ListaDeOrdenesPorElaborar;
            }
            catch (Exception)
            {
                DisplayAlert("Sorry", "Sin acceso a internet", "Ok");
            }
        }

        private async void ButtonFinalizarOrdenList_Clicked(object sender, EventArgs e)
        {
            var item = sender as Button;
            var ObjItem = item.BindingContext as VMOrden;
            var action = await DisplayAlert("Finalizar?", "¿A concluido el proceso de elaboracion de la orden "+ ObjItem .LNGFolio+"?", "Si", "No");
            if (action)
            {
                App.MVOrden.AgregaEstatusALaOrden(new Guid("c412d367-7d05-45d8-aeca-b8fabbf129d9"), UidOrden: ObjItem.Uidorden, UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), StrParametro: "S");
                //Crea el codigo para que el repartidor pueda recoger la orden
                // MVOrden.AsociarOrdenConSucursalDistribuidora(Codigo: Guid.NewGuid(), UidLicencia: Licencia, LngFolio: Folio);
                //App.MVTarifario.AgregarCodigoAOrdenTarifario(UidCodigo: Guid.NewGuid(), UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), uidorden: ObjItem.Uidorden);

                App.MVOrden.BuscarOrdenes("Sucursal", UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), EstatusSucursal: "Pendiente para elaborar", TipoDeSucursal: "S");
                MyListviewOrdenesPorRealizar.ItemsSource = null;
                MyListviewOrdenesPorRealizar.ItemsSource = App.MVOrden.ListaDeOrdenesPorElaborar;

            }
        }

        private async void MyListviewOrdenesPorRealizar_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new AppPuestoTacos.Popup.Loanding());
            var item = ((ItemTappedEventArgs)e);
            VMOrden ObjItem = (VMOrden)item.Item;

            await Navigation.PushAsync(new OrdenDescripcionPorElaborar(ObjItem, MyListviewOrdenesPorRealizar));       
            await PopupNavigation.Instance.PopAllAsync();
        }

        private void ButtonBusquedaFecha_Clicked(object sender, EventArgs e)
        {
            if (Ordenamiento)
            {
                App.MVOrden.ListaDeOrdenesPorElaborar = App.MVOrden.ListaDeOrdenesPorElaborar.OrderBy(x => x.FechaDeOrden).ToList();
                MyListviewOrdenesPorRealizar.ItemsSource = null;
                MyListviewOrdenesPorRealizar.ItemsSource = App.MVOrden.ListaDeOrdenesPorElaborar;
                Ordenamiento = false;
            }
            else
            {
                App.MVOrden.ListaDeOrdenesPorElaborar = App.MVOrden.ListaDeOrdenesPorElaborar.OrderByDescending(x => x.FechaDeOrden).ToList();
                MyListviewOrdenesPorRealizar.ItemsSource = null;
                MyListviewOrdenesPorRealizar.ItemsSource = App.MVOrden.ListaDeOrdenesPorElaborar;
                Ordenamiento = true;
            }
        }

        private void ButtonBusquedaOrdenes_Clicked(object sender, EventArgs e)
        {
            if (Ordenamiento)
            {
                App.MVOrden.ListaDeOrdenesPorElaborar = App.MVOrden.ListaDeOrdenesPorElaborar.OrderBy(x => x.LNGFolio).ToList();
                MyListviewOrdenesPorRealizar.ItemsSource = null;
                MyListviewOrdenesPorRealizar.ItemsSource = App.MVOrden.ListaDeOrdenesPorElaborar;
                Ordenamiento = false;
            }
            else
            {
                App.MVOrden.ListaDeOrdenesPorElaborar = App.MVOrden.ListaDeOrdenesPorElaborar.OrderByDescending(x => x.LNGFolio).ToList();
                MyListviewOrdenesPorRealizar.ItemsSource = null;
                MyListviewOrdenesPorRealizar.ItemsSource = App.MVOrden.ListaDeOrdenesPorElaborar;
                Ordenamiento = true;
            }
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {

        }

        private void ImageButtonBuscarIdOrden_Clicked(object sender, EventArgs e)
        {
            //await PopupNavigation.Instance.PushAsync(new AppPuestoTacos.Popup.BuscarRecibidas(MyListviewOrdenesPorRealizar, txtBusqueda));
        }
    }
}