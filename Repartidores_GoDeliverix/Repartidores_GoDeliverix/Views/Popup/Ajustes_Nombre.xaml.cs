
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Repartidores_GoDeliverix.Views.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Ajustes_Nombre : ContentPage
    {
        public Ajustes_Nombre()
        {
            InitializeComponent();
        }
        public async void CloseWindowsPopup(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }
    }
}