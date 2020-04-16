using GalaSoft.MvvmLight.Command;
using Repartidores_GoDeliverix.Modelo;
using Repartidores_GoDeliverix.Views;
using Repartidores_GoDeliverix.Views.Popup;
using System;
using System.Windows.Input;
using VistaDelModelo;
using Xamarin.Forms;
using Newtonsoft.Json;
using Repartidores_GoDeliverix.Helpers;
using System.Net.Http;
using Com.OneSignal;
namespace Repartidores_GoDeliverix.VM
{
    public class VMAjustesItem
    {
        public string Titulo { get; set; }
        public string Detalles { get; set; }
        public string StrRuta { get; set; }
        public ICommand DisplaySettingsCommand { get { return new RelayCommand(DisplaySetting); } }

        private async void DisplaySetting()
        {
            var AppInstance = MainViewModel.GetInstance();
            using (HttpClient _WebApiGoDeliverix = new HttpClient())
            {
                AppInstance.MVAjustes.ModuloACambiar = Titulo;
                switch (this.Titulo)
                {
                    case "Nombre":
                        await Application.Current.MainPage.Navigation.PushAsync(new Ajustes_Nombre());
                        break;
                    case "Usuario":
                        break;
                    case "Fecha de nacimiento":
                        await Application.Current.MainPage.Navigation.PushAsync(new Ajustes_FechaDeNacimiento());
                        break;
                    case "Correo electronico":
                        await Application.Current.MainPage.Navigation.PushAsync(new Ajustes_CorreoElectronico());
                        break;
                    case "Telefonos":
                        await (Application.Current.MainPage as TabbedPage).Navigation.PushAsync(new Ajustes_Telefono());
                        break;
                    case "Direcciones":
                        await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new Ajustes_Direccion());
                        break;
                    case "Cerrar sesion":
                        _WebApiGoDeliverix.BaseAddress = new Uri("" + Helpers.settings.Sitio + "api/");

                        string url = "Orden/GetBuscarOrdenAsiganadaRepartidor?UidTurnoRepartidor=" + AppInstance.Session_.UidTurnoRepartidor + "";
                        string content = await _WebApiGoDeliverix.GetStringAsync(url);
                        var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                        var MVOrden = JsonConvert.DeserializeObject<VistaDelModelo.VMOrden>(obj);


                        if (MVOrden.Uidorden == Guid.Empty)
                        {
                            url = "Profile/GetBitacoraRegistroRepartidores?StrParametro=S&UidUsuario=" + AppInstance.Session_.UidUsuario + "&UidEstatus=AAD35D44-5E65-46B6-964F-CD2DF026ECB1";
                            await _WebApiGoDeliverix.GetAsync(url);
                            AppInstance.MVLogin = new VMLogin();
                            AppInstance.Session_ = new Session();
                            // OneSignal.Current.RemoveExternalUserId();
                            Application.Current.MainPage = new NavigationPage(new Login());
                        }
                        else
                        {
                            string UidEstatus = MVOrden.StrEstatusOrdenRepartidor;
                            //Cancelado
                            if (UidEstatus.ToUpper() == "12748F8A-E746-427D-8836-B54432A38C07")
                            {
                                url = "Profile/GetBitacoraRegistroRepartidores?StrParametro=S&UidUsuario=" + AppInstance.Session_.UidUsuario + "&UidEstatus=AAD35D44-5E65-46B6-964F-CD2DF026ECB1";
                                await _WebApiGoDeliverix.GetAsync(url);
                                AppInstance.MVLogin = new VMLogin();
                                AppInstance.Session_ = new Session();
                                // OneSignal.Current.RemoveExternalUserId();
                                Repartidores_GoDeliverix.Helpers.settings.UserName = string.Empty;
                                Repartidores_GoDeliverix.Helpers.settings.Password = string.Empty;
                                Application.Current.MainPage = new NavigationPage(new Login());
                            }
                            else//Orden pendiente
                            if (UidEstatus.ToUpper() == "6294DACE-C9D1-4F9F-A942-FF12B6E7E957")
                            {
                                GenerateMessage("Aviso", "No puedes cerrar session al tener una orden asignada", "OK");
                            }
                            else
                            //Orden Confirmada
                            if (UidEstatus.ToUpper() == "A42B2588-D650-4DD9-829D-5978C927E2ED")
                            {
                                GenerateMessage("Aviso", "No puedes cerrar session al haber confirmado la orden", "OK");
                            }
                            else
                            //Entrega
                            if (UidEstatus.ToUpper() == "B6791F2C-FA16-40C6-B5F5-123232773612")
                            {
                                GenerateMessage("Aviso", "No puedes cerrar session sin haber entregado la orden recolectada", "OK");
                            }
                            else
                            {
                                url = "Profile/GetBitacoraRegistroRepartidores?StrParametro=S&UidUsuario=" + AppInstance.Session_.UidUsuario + "&UidEstatus=AAD35D44-5E65-46B6-964F-CD2DF026ECB1";
                                await _WebApiGoDeliverix.GetAsync(url);
                                AppInstance.MVLogin = new VMLogin();
                                AppInstance.Session_ = new Session();

                                Repartidores_GoDeliverix.Helpers.settings.UserName = string.Empty;
                                Repartidores_GoDeliverix.Helpers.settings.Password = string.Empty;

                                Application.Current.MainPage = new NavigationPage(new Login());
                            }
                        }
                        break;
                    default:
                        break;
                }
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
