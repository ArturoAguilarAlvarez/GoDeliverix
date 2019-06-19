using GalaSoft.MvvmLight.Command;
using Repartidores_GoDeliverix.Views;
using Repartidores_GoDeliverix.Views.Popup;
using Repartidores_GoDeliverix.Modelo;
using Rg.Plugins.Popup.Services;
using System;
using System.Windows.Input;
using VistaDelModelo;
using Xamarin.Forms;

namespace Repartidores_GoDeliverix.VM
{
    public class VMLogin : ControlsController
    {
        VMAcceso mVAcceso = new VMAcceso();

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
            this.User = "Rep1dis1";
            this.Password = "12345";
            IsSavingValues = true;
        }
        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(this.login);
            }
        }

        private async void login()
        {
            try
            {
                this.IsLoading = true;
                this.IsEnable = false;
                await PopupNavigation.Instance.PushAsync(new PopoLoading());
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
                    if (Acceso(User, Password, "Login"))
                    {
                        var AppInstance = MainViewModel.GetInstance();
                        AppInstance.MVHome = new VMHome();
                        AppInstance.MVHome.BlEstatus = true;
                        AppInstance.MVAjustes = new VMAjustes();
                        Application.Current.MainPage = new NavigationPage(new TabbedPageMain());                       
                    }
                    await PopupNavigation.Instance.PopAllAsync();
                }
            }
            catch (Exception)
            {
                this.IsLoading = false;
                this.IsEnable = true;

                GenerateMessage("Alerta!!", "No hay internet", "Aceptar");
                await PopupNavigation.Instance.PopAllAsync();
            }
        }

        public void AccesoGuardado(string Usuario, string password)
        {
            if (Acceso(Usuario, password, "Datoas Guardados"))
            {
                Application.Current.MainPage = new NavigationPage(new TabbedPageMain());
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(new Login());
            }
        }
        public bool Acceso(string Usuario, string password, string Modulo)
        {
            bool resultado = false;

            Guid UidUsuario = mVAcceso.Ingresar(Usuario, password);
            if (UidUsuario != null && UidUsuario != Guid.Empty)
            {
                string perfil = mVAcceso.PerfilDeUsuario(UidUsuario.ToString());
                //Entrada solo para perfil de repartidor
                if (perfil.ToUpper() == "DFC29662-0259-4F6F-90EA-B24E39BE4346")
                {
                    mVAcceso.BitacoraRegistroRepartidores(char.Parse("S"), UidUsuario, new Guid("A298B40F-C495-4BD8-A357-4A3209FBC162"));
                    var AppInstance = MainViewModel.GetInstance();
                    AppInstance.Session_ = new Session() { UidUsuario = UidUsuario };
                    VMUsuarios MVUsuario = new VMUsuarios();
                    MVUsuario.BusquedaDeUsuario(UidUsuario: UidUsuario, UIDPERFIL: new Guid("DFC29662-0259-4F6F-90EA-B24E39BE4346"));
                    AppInstance.Nombre = MVUsuario.StrNombre + " " + MVUsuario.StrApellidoPaterno;


                    if (IsSavingValues && Modulo == "Login")
                    {
                        Repartidores_GoDeliverix.Helpers.settings.UserName = User;
                        Repartidores_GoDeliverix.Helpers.settings.Password = Password;
                    }
                    else if (!IsSavingValues && Modulo == "Login")
                    {
                        Repartidores_GoDeliverix.Helpers.settings.ClearAllData();
                    }
                    resultado = true;
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
            return resultado;
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
