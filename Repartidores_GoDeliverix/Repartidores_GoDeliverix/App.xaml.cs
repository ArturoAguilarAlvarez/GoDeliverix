using Repartidores_GoDeliverix.Views;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Repartidores_GoDeliverix.VM;
using Com.OneSignal;
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Repartidores_GoDeliverix
{
    public partial class App : Application
    {

        public static NavigationPage Navigator { get; internal set; }
        static public int ScreenWidth;
        static public int ScreenHeight;
        static public float ScreenDensity = 1;
        public App()
        {
            SetCultureToUSEnglish();
            InitializeComponent();
            string Usuario = Repartidores_GoDeliverix.Helpers.settings.UserName;
            string Contrasena = Repartidores_GoDeliverix.Helpers.settings.Password;
            if (!string.IsNullOrEmpty(Usuario) && !string.IsNullOrEmpty(Contrasena))
            {
                MainPage = new NavigationPage(new Login());
                VMLogin obj = new VMLogin();
                obj.Acceso(Usuario, Contrasena);
            }
            else
            {
                VMLogin obj = new VMLogin();
                MainPage = new NavigationPage(new Login());
            }
            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }
        private void SetCultureToUSEnglish()
        {
            CultureInfo englishUSCulture = new CultureInfo("es-MX");
            CultureInfo.DefaultThreadCurrentCulture = englishUSCulture;
        }
        protected override void OnResume()
        {
            bool logueado = false;
            bool.TryParse(Application.Current.Properties["IsLogged"].ToString(), out logueado);
            if (logueado)
            {
                MainPage = new NavigationPage(new TabbedPageMain()); 
            }
            else
            {
                MainPage = new NavigationPage(new Login());
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
