using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Plugin.LocalNotifications;

using VistaDelModelo;
using System.Collections.Generic;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AppPuestoTacos
{
    public partial class App : Application
    {
        public static string Licencia = "";
        public static string Sucursal = "";
        public static string Perfil = "";
        public static Guid UIdUsuario;


        public static string NombreSucursal = "";
        public static string NombreEmpresa = "";
        public static string NOmbreUsuario = "";

        public List<VMOrden> ListaDeOrdenesPorConfirmar = new List<VMOrden>();


        public static VMSucursales MVSucursal = new VMSucursales();
        public static VMAcceso MVAcceso = new VMAcceso();
        public static VMLicencia MVLicencia = new VMLicencia();
        public static VMOrden MVOrden = new VMOrden();
        public static VMTarifario MVTarifario = new VMTarifario();
        public static VMUsuarios MVUsuarios = new VMUsuarios();
        public static VMMensaje MVMensaje = new VMMensaje();
        public static VMEmpresas MVEmpresas = new VMEmpresas();
        int IdNotificacion;

        public App()
        {
            InitializeComponent();
            //if (!string.IsNullOrEmpty(AppPuestoTacos.Helpers.Settings.Licencia))
            //{
            //    if (!string.IsNullOrEmpty(AppPuestoTacos.Helpers.Settings.Perfil))
            //    {
            //        Guid Uidusuario = App.MVAcceso.Ingresar(AppPuestoTacos.Helpers.Settings.Usuario, AppPuestoTacos.Helpers.Settings.Contrasena);
            //        App.UIdUsuario = Uidusuario;
            //        if (Uidusuario != Guid.Empty)
            //        {
            //            App.MVEmpresas.ObtenerNombreComercial(App.UIdUsuario.ToString());
            //            App.NombreEmpresa = App.MVEmpresas.NOMBRECOMERCIAL;
            //            App.NOmbreUsuario = AppPuestoTacos.Helpers.Settings.Usuario;
            //            MainPage = new View.MasterMenu();
            //            Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            //            {
            //                Device.BeginInvokeOnMainThread(() =>
            //                {
            //                    MetodoConsulta();
            //                });
            //                return true;
            //            });
            //        }
            //        else
            //        {
            //            MainPage = new NavigationPage(new MainPage());
            //        }
            //    }
            //    else
            //    {
            //        MainPage = new NavigationPage(new MainPage());
            //    }
            //}
            //else

            //{
                MainPage = new NavigationPage(new MainPage());
            //}

        }

        public void MetodoConsulta()
        {
            try
            {
                //App.MVOrden.BuscarOrdenes("Sucursal", UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), EstatusSucursal: "Pendientes a confirmar", TipoDeSucursal: "S");
                //for (int i = 0; i < App.MVOrden.ListaDeOrdenesPorConfirmar.Count; i++)
                //{
                //    var ItemNotificaciones = ListaDeOrdenesPorConfirmar.Find(t => t.LNGFolio == App.MVOrden.ListaDeOrdenesPorConfirmar[i].LNGFolio);
                //    if (ItemNotificaciones == null)
                //    {
                //        ListaDeOrdenesPorConfirmar.Add(App.MVOrden.ListaDeOrdenesPorConfirmar[i]);
                //        CrossLocalNotifications.Current.Show("Deliverix", "Nueva orden numero: " + App.MVOrden.ListaDeOrdenesPorConfirmar[i].LNGFolio,int.Parse(App.MVOrden.ListaDeOrdenesPorConfirmar[i].LNGFolio.ToString()), DateTime.Now.AddSeconds(7));
                //        IdNotificacion = IdNotificacion + 1;
                //    }
                //    else
                //    {

                //    }
                //}
            }
            catch (Exception)
            {

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

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
