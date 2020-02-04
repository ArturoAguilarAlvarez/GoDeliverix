using Com.OneSignal;
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
            string NombreMenu = item.Title.ToString();
            if (NombreMenu == "Inciar session")
            {
                var page = (Page)Activator.CreateInstance(item.TargetType);
                page.Title = item.Title;

                Detail = new NavigationPage(new Login());
                IsPresented = false;

                MasterPage.ListView.SelectedItem = null;
            }
            else
            if (NombreMenu == "Salir")
            {
                App.ListaDeProductos.Clear();
                App.LISTADEEMPRESAS.Clear();
                if (App.MVDireccion.ListaDIRECCIONES.Count > 0)
                {
                    App.MVDireccion.ListaDIRECCIONES.Clear();
                }
                //OneSignal.Current.RemoveExternalUserId();
                Application.Current.Properties.Remove("IsLogged");
                App.Global1 = string.Empty;
                Application.Current.MainPage = new MasterMenu();

                IsPresented = false;
                MasterPage.ListView.SelectedItem = null;
                //Detail = new NavigationPage(new Login());
            }
            else
            {
                var page = (Page)Activator.CreateInstance(item.TargetType);
                page.Title = item.Title;
                App.Navegacion = page.GetType().Name;
                Detail = new NavigationPage(page);
                IsPresented = false;

                MasterPage.ListView.SelectedItem = null;
            }
        }
    }
}