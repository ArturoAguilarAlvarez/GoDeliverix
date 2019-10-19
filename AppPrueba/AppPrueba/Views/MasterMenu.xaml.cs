using AppPrueba.WebApi;
using Com.OneSignal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPrueba.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterMenu : MasterDetailPage
    {
        public MasterMenu()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterMenuMenuItemMenuItem;
            if (item == null)
                return;
            string Salir = item.TargetType.ToString();
            if (Salir == "AppPrueba.Views.MasterMenuDetailDetail")
            {
                AppPrueba.Helpers.Settings.CerrarSesion();
                OneSignal.Current.RemoveExternalUserId();
                //Inicio deturno movil
                using (HttpClient _client = new HttpClient())
                {
                    string url = RestService.Servidor + "api/Turno/GetTurnoSuministradora?UidUsuario=" + AppPrueba.Helpers.Settings.Uidusuario + "&UidTurno=" + AppPrueba.Helpers.Settings.UidTurno;
                    await _client.GetStringAsync(url);
                }
                App.Current.MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                var page = (Page)Activator.CreateInstance(item.TargetType);
                page.Title = item.Title;
                Detail = new NavigationPage(page);
                IsPresented = false;
                MasterPage.ListView.SelectedItem = null;
            }
        }
    }
}