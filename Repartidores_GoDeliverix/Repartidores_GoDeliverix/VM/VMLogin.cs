using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Repartidores_GoDeliverix.Helpers;
using Repartidores_GoDeliverix.Modelo;
using Repartidores_GoDeliverix.Views;
using Repartidores_GoDeliverix.Views.Popup;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Input;
using VistaDelModelo;
using Xamarin.Essentials;
using Xamarin.Forms;
using Com.OneSignal;
namespace Repartidores_GoDeliverix.VM
{
    public class VMLogin : ControlsController
    {
        #region atributos
        private string _strUser;
        private string _Password;
        private bool _IsEnable;
        private bool _IsLoading;
        private bool _IsSavingValues;

        #endregion
        #region Propiedades

        public string User
        {
            get { return _strUser; }
            set { SetValue(ref _strUser, value); }
        }
        public string Password
        {
            get { return _Password; }
            set { SetValue(ref _Password, value); }
        }
        public bool IsEnable
        {
            get { return _IsEnable; }
            set { SetValue(ref _IsEnable, value); }
        }
        public bool IsLoading
        {
            get { return _IsLoading; }
            set { SetValue(ref _IsLoading, value); }
        }

        public bool IsSavingValues
        {
            get { return _IsSavingValues; }
            set { SetValue(ref _IsSavingValues, value); }
        }

        #endregion

        #region Constructor
        public VMLogin()
        {
            IsSavingValues = true;
        }
        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(this.Login);
            }
        }

        private async void Login()
        {
            var supportsUri = false;
            if (Device.RuntimePlatform == Device.Android)
            {
                supportsUri = true;
            }

            if (Device.RuntimePlatform == Device.iOS)
            {
                supportsUri = await Launcher.CanOpenAsync("comgooglemaps://");
            }
            if (supportsUri)
            {
                try
                {
                    this.IsLoading = true;
                    this.IsEnable = false;
                    if (string.IsNullOrEmpty(this.User))
                    {
                        this.IsLoading = true;
                        this.IsEnable = false;
                        GenerateMessage("Datos invalidos", "Usuario requerido", "Aceptar");
                        return;
                    }
                    else
                    if (string.IsNullOrEmpty(this.Password))
                    {
                        this.IsLoading = true;
                        this.IsEnable = false;
                        GenerateMessage("Datos invalidos", "Contraseña requerida", "Aceptar");
                        return;
                    }
                    else
                    {

                        Acceso(User, Password);

                    }
                }
                catch (Exception)
                {
                    this.IsLoading = false;
                    this.IsEnable = true;

                    GenerateMessage("Alerta!!", "No hay internet", "Aceptar");

                }
            }
            else
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    var action = await Application.Current.MainPage.DisplayAlert("Aplicacion requerida", "No se encuentra la aplicacion de google maps en este dispositivo, reinicie la aplicacion despues de la instalarla", "Instalar", "Cancelar");
                    if (action)
                    {
                        await Launcher.OpenAsync("https://apps.apple.com/mx/app/google-maps-trafico-y-comida/id585027354");
                    }
                }
            }
        }


        public async void Acceso(string Usuario, string password)
        {
            try
            {
                string Uid;
                using (HttpClient _WebApiGoDeliverix = new HttpClient())
                {
                    string url = "https://www.godeliverix.net/api/Profile/GET?Usuario=" + Usuario + "&Contrasena=" + password + "";

                    string content = await _WebApiGoDeliverix.GetStringAsync(url);
                    Uid = content = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                }

                Guid UidUsuario = new Guid(Uid);
                if (UidUsuario != null && UidUsuario != Guid.Empty)
                {
                    string perfil;
                    using (HttpClient _WebApiGoDeliverix = new HttpClient())
                    {
                        string url = "https://www.godeliverix.net/api/Profile/GetProfileType?UidUsuario=" + UidUsuario + "";
                        string content = await _WebApiGoDeliverix.GetStringAsync(url);
                        var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                        perfil = obj.ToString();
                    }

                    //Entrada solo para perfil de repartidor
                    if (perfil.ToUpper() == "DFC29662-0259-4F6F-90EA-B24E39BE4346")
                    {
                        using (var _webAppi = new HttpClient())
                        {
                            string url = "https://www.godeliverix.net/api/Profile/GetBitacoraRegistroRepartidores?StrParametro=S&UidUsuario=" + UidUsuario + "&UidEstatus=A298B40F-C495-4BD8-A357-4A3209FBC162";
                            await _webAppi.GetAsync(url);
                        }

                        var AppInstance = MainViewModel.GetInstance();
                        AppInstance.Session_ = new Session() { UidUsuario = UidUsuario };


                        VMUsuarios MVUsuario = new VMUsuarios();
                        using (var _webAppi = new HttpClient())
                        {
                            string url = "https://www.godeliverix.net/api/Usuario/GetBuscarUsuarios?UidUsuario=" + UidUsuario + "&UIDPERFIL=DFC29662-0259-4F6F-90EA-B24E39BE4346";
                            string content = await _webAppi.GetStringAsync(url);
                            string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                            MVUsuario = JsonConvert.DeserializeObject<VistaDelModelo.VMUsuarios>(obj);
                        }


                        AppInstance.Nombre = MVUsuario.StrNombre + " " + MVUsuario.StrApellidoPaterno;

                        AppInstance.MVHome = new VMHome
                        {
                            BlEstatus = true
                        };
                        AppInstance.MVAjustes = new VMAjustes();
                        AppInstance.MVTurno = new VMTurno();
                        //OneSignal.Current.SetExternalUserId(UidUsuario.ToString());

                        Application.Current.Properties["IsLogged"] = true;
                        Application.Current.MainPage = new NavigationPage(new TabbedPageMain());

                        //await Application.Current.MainPage.Navigation.PushAsync(new Prueba());

                        if (IsSavingValues)
                        {
                            Repartidores_GoDeliverix.Helpers.settings.UserName = User;
                            Repartidores_GoDeliverix.Helpers.settings.Password = Password;
                        }
                        else if (!IsSavingValues)
                        {
                            Repartidores_GoDeliverix.Helpers.settings.ClearAllData();
                        }
                    }
                    else
                    {
                        GenerateMessage("Datos invalidos", "El usuario no es repartidor", "Aceptar");
                        this.IsLoading = false;
                        this.IsEnable = true;
                    }
                }
                else
                {
                    Repartidores_GoDeliverix.Helpers.settings.ClearAllData();
                    GenerateMessage("Datos invalidos", "El usuario no existe", "Aceptar");
                    this.IsLoading = false;
                    this.IsEnable = true;
                }
            }
            catch (Exception)
            {
                GenerateMessage("Sin coneccion a internet", "No se pudo conectar con los servicios", "OK");
                throw;
            }
        }


        protected async void GenerateMessage(string Tittle, string Message, string TextOption)
        {
            await Application.Current.MainPage.DisplayAlert(
              Tittle,
              Message,
              TextOption);
        }

        #endregion
    }
}
