using GalaSoft.MvvmLight.Command;
using Repartidores_GoDeliverix.Modelo;
using Repartidores_GoDeliverix.Views;
using Repartidores_GoDeliverix.Views.Popup;
using Rg.Plugins.Popup.Services;
using System;
using System.Windows.Input;
using VistaDelModelo;
using Xamarin.Forms;
using Newtonsoft.Json;
using Repartidores_GoDeliverix.Helpers;
using System.Net.Http;

namespace Repartidores_GoDeliverix.VM
{
    public class VMAjustesItem
    {
        ResponseHelper oWebApiResponse;
        string UrlApi = "http://www.godeliverix.net/api/";
        public string Titulo { get; set; }
        public string Detalles { get; set; }
        public string StrRuta { get; set; }
        public ICommand DisplaySettingsCommand { get { return new RelayCommand(DisplaySetting); } }


        private VMAcceso MVAcceso = new VMAcceso();
        private async void DisplaySetting()
        {
            var AppInstance = MainViewModel.GetInstance();
            HttpClient _WebApiGoDeliverix = new HttpClient();
            AppInstance.MVAjustes.ModuloACambiar = Titulo;
            switch (this.Titulo)
            {
                case "Nombre":
                    await PopupNavigation.Instance.PushAsync(new Ajustes_Nombre());
                    break;
                case "Usuario":
                    break;
                case "Fecha de nacimiento":
                    await PopupNavigation.Instance.PushAsync(new Ajustes_FechaDeNacimiento());
                    break;
                case "Correo electronico":
                    await PopupNavigation.Instance.PushAsync(new Ajustes_CorreoElectronico());
                    break;
                case "Telefonos":
                    await (Application.Current.MainPage as TabbedPage).Navigation.PushAsync(new Ajustes_Telefono());
                    break;
                case "Direcciones":
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new Ajustes_Direccion());
                    break;
                case "Cerrar sesion":
                    string url = UrlApi + "Profile/GetBitacoraRegistroRepartidores?StrParametro=S&UidUsuario=" + AppInstance.Session_.UidUsuario + "&UidEstatus=AAD35D44-5E65-46B6-964F-CD2DF026ECB1";
                    await _WebApiGoDeliverix.GetAsync(url);
                    //MVAcceso.BitacoraRegistroRepartidores(char.Parse("S"), AppInstance.Session_.UidUsuario, new Guid("AAD35D44-5E65-46B6-964F-CD2DF026ECB1"));
                    AppInstance.MVLogin = new VMLogin();
                    AppInstance.Session_ = new Session();
                    Application.Current.MainPage = new NavigationPage(new Login());
                    break;
                default:
                    break;
            }
        }
        protected async void GenerateMessage(string Tittle, string Message, string TextOption)
        {
            await Application.Current.MainPage.DisplayAlert(
              Tittle,
              Message,
              TextOption);
        }
    }
}
