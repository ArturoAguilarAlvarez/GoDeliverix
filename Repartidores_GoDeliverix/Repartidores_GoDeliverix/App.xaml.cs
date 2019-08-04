using Repartidores_GoDeliverix.Views;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            MainPage = new NavigationPage(new Login());
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
            if (Application.Current.Properties.ContainsKey("IsLogged"))
            {
                MainPage = new NavigationPage(new TabbedPageMain()); 
            }
            else
            {
                MainPage = new NavigationPage(new Login());
            }
        }
    }
}
