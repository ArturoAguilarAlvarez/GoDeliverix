using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPuestoTacos.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterMenu : MasterDetailPage
    {
        public MasterMenu()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterMenuMenuItem;
            if (item == null)
                return;

            string Salir = item.TargetType.ToString();
            if (Salir== "AppPuestoTacos.View.MasterMenuDetail")
            {

                AppPuestoTacos.Helpers.Settings.CerrarSesion();
                App.Current.MainPage = new NavigationPage(new MainPage());
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