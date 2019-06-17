
using Repartidores_GoDeliverix.VM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Repartidores_GoDeliverix.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            //if (!string.IsNullOrEmpty(Repartidores_GoDeliverix.Helpers.settings.Password) && !string.IsNullOrEmpty(Repartidores_GoDeliverix.Helpers.settings.UserName))
            //{
            //    var AppInstance = MainViewModel.GetInstance();
            //    string User = Repartidores_GoDeliverix.Helpers.settings.UserName, Password = Repartidores_GoDeliverix.Helpers.settings.Password;
            //    AppInstance.MVLogin = new VMLogin();
            //    AppInstance.MVLogin.AccesoGuardado(User, Password);
            //}
            //else
            //{
                InitializeComponent();
            //}
        }
    }
}