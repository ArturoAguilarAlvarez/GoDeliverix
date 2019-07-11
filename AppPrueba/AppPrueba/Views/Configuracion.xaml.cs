using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPrueba.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Configuracion : ContentPage
    {
        public Configuracion()
        {
            InitializeComponent();


            txtLicencia.Text = AppPrueba.Helpers.Settings.Licencia;
            txtNombreSucursal.Text = AppPrueba.Helpers.Settings.NombreSucursal;
        }




        private void PickerSucursales_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    VMSucursales ovjSeccion = PickerSucursales.SelectedItem as VMSucursales;
            //    App.MVLicencia.ObtenerLicenciaSucursal(ovjSeccion.ID.ToString());
            //    PickerLicencia.ItemsSource = null;
            //    int a = App.MVLicencia.ListaDeLicencias.Count;
            //    for (int i = 0; i < a; i++)
            //    {
            //        if (App.MVLicencia.ListaDeLicencias[i].UidEstatus == 0)
            //        {
            //            App.MVLicencia.ListaDeLicencias.Remove(App.MVLicencia.ListaDeLicencias[i]);
            //            a = a - 1;
            //            i = i - 1;
            //        }
            //        if (App.MVLicencia.ListaDeLicencias[i].BLUso == true)
            //        {
            //            App.MVLicencia.ListaDeLicencias.Remove(App.MVLicencia.ListaDeLicencias[i]);
            //            a = a - 1;
            //            i = i - 1;
            //        }
            //    }
            //    //foreach (var item in App.MVLicencia.ListaDeLicencias)
            //    //{
            //    //if (item.UidEstatus == 0)
            //    //{
            //    //    App.MVLicencia.ListaDeLicencias.Remove(item);
            //    //}
            //    //}
            //    App.MVLicencia.ListaDeLicencias.RemoveAt(App.MVLicencia.ListaDeLicencias.Count - 1);
            //    PickerLicencia.ItemsSource = App.MVLicencia.ListaDeLicencias;
            //}
            //catch (Exception)
            //{
            //}
        }

        private void ButtonGuardarLicencia_Clicked(object sender, EventArgs e)
        {
            //try
            //{

            //    if (!string.IsNullOrEmpty(AppPuestoTacos.Helpers.Settings.Licencia))
            //    {
            //        App.MVLicencia.ActualizarLicenciaSucursal(UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), bdisponibilidad: false);
            //        AppPuestoTacos.Helpers.Settings.ClearAllData();
            //    }
            //    VMSucursales objSucursal = PickerSucursales.SelectedItem as VMSucursales;
            //    VMLicencia objLicencia = PickerLicencia.SelectedItem as VMLicencia;
            //    App.MVLicencia.CambiaDisponibilidadDeLicencia(objLicencia.UidLicencia.ToString());
            //    AppPuestoTacos.Helpers.Settings.NombreSucursal = objSucursal.IDENTIFICADOR;
            //    AppPuestoTacos.Helpers.Settings.Licencia = objLicencia.UidLicencia.ToString();
            //    AppPuestoTacos.Helpers.Settings.UidSucursal = objSucursal.ID.ToString();

            //    AppPuestoTacos.Helpers.Settings.Perfil = perfil;
            //    App.Current.MainPage = new MasterMenu();
            //}
            //catch (Exception)
            //{


            //}

        }

        private async void ButtonRevocarLicencia_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayAlert("", "¿Esta seguro de eliminar esta licencia de este dispositivo?", "Si", "No");
            if (action)
            {
                App.MVLicencia.ActualizarLicenciaSucursal(UidLicencia: new Guid(AppPrueba.Helpers.Settings.Licencia), bdisponibilidad: false);
                AppPrueba.Helpers.Settings.ClearAllData();
                AppPrueba.App.Current.MainPage = new NavigationPage(new LoginPage());
            }

        }

        private void ButtonCambioLicencia_Clicked(object sender, EventArgs e)
        {
          //  Navigation.PushAsync(new ConfiguracionCambioLicencia());
        }
    }
}