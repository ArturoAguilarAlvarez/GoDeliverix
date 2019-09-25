using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCliente
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
            if (Salir == "AppCliente.MasterMenuDetail")
            {
                AppCliente.App.ListaDeProductos.Clear();
                AppCliente.App.LISTADEEMPRESAS.Clear();
                //AppCliente.App.MVDireccion = null;
                Application.Current.Properties.Remove("IsLogged");
                App.Current.MainPage = new NavigationPage(new Login());

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