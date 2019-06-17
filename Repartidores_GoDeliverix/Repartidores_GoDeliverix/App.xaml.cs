using Repartidores_GoDeliverix.Views;
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

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
