using AppCliente.WebApi;
using Com.OneSignal;
using Newtonsoft.Json;
using Plugin.Geolocator.Abstractions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VistaDelModelo;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterMenu : MasterDetailPage
    {
        public MasterMenu()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterMenuMenuItem;
            if (item == null)
                return;
            string Salir = item.TargetType.ToString();
            string NombreMenu = item.Title.ToString();
            if (NombreMenu == "Inciar session")
            {
                var page = (Page)Activator.CreateInstance(item.TargetType);
                page.Title = item.Title;

                Detail = new NavigationPage(new Login());
                IsPresented = false;

                MasterPage.ListView.SelectedItem = null;
            }
            else
            if (NombreMenu == "Salir")
            {
                App.ListaDeProductos.Clear();
                App.LISTADEEMPRESAS.Clear();
                if (App.MVDireccion.ListaDIRECCIONES.Count > 0)
                {
                    App.MVDireccion.ListaDIRECCIONES.Clear();
                }
                App.MVDireccionDemo = null;

                //OneSignal.Current.RemoveExternalUserId();
                Application.Current.Properties.Remove("IsLogged");
                App.Global1 = string.Empty;
                Application.Current.MainPage = new MasterMenu();

                IsPresented = false;
                MasterPage.ListView.SelectedItem = null;
                //Detail = new NavigationPage(new Login());
            }
            if (NombreMenu == "Actualizar mi ubicación")
            {
                App.MVDireccionDemo = null;
                //var page = (Page)Activator.CreateInstance(item.TargetType);
                //page.Title = item.Title;
                //App.Navegacion = page.GetType().Name;
                //Detail = new NavigationPage(page);
                IsPresented = false;
                MasterPage.ListView.SelectedItem = null;
                NavigationPage NPScannerCompanyPage = ((NavigationPage)((MasterDetailPage)App.Current.MainPage).Detail);
                Device.InvokeOnMainThreadAsync(async () =>
                {
                    await NPScannerCompanyPage.PushAsync(new SeleccionaColonia());
                });
            }
            else
            {
                var page = (Page)Activator.CreateInstance(item.TargetType);
                page.Title = item.Title;
                App.Navegacion = page.GetType().Name;
                Detail = new NavigationPage(page);
                IsPresented = false;

                MasterPage.ListView.SelectedItem = null;
            }
        }
    }
}