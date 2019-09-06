using GalaSoft.MvvmLight.Command;
using Repartidores_GoDeliverix.VM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Repartidores_GoDeliverix.Views;
namespace Repartidores_GoDeliverix
{
    public class MenuItemViewModel
    {
        public string Icon { get; set; }
        public string Titulo { get; set; }
        public string PageName { get; set; }
        public string Usuario { get; set; }
        public string Sucursal { get; set; }


        public ICommand NavigateCommand { get { return new RelayCommand(Navigate); } }

        private void Navigate()
        {
            //switch (this.PageName)
            //{
            //    //case "Home":
            //    //    MainViewModel.GetInstance().MVHome = new VMHome();
            //    //    (Application.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new Home());
            //    //    (Application.Current.MainPage as MasterDetailPage).IsPresented = false;
            //    //    break;
            //    //case "Ajustes":
            //    //    MainViewModel.GetInstance().MVAjustes = new VMAjustes();
            //    //    (Application.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new Ajustes());
            //    //    (Application.Current.MainPage as MasterDetailPage).IsPresented = false;
            //    //    break;
            //    //case "Login":
            //    //    var AppInstance = MainViewModel.GetInstance();
            //    //    AppInstance.MVLogin = new VMLogin();
            //    //    Application.Current.MainPage = new NavigationPage(new Login());
            //    //    break;
            //    //default:
            //    //    break;
            //}
        }
    }
}
