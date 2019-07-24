using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppPrueba.Services;
using AppPrueba.Views;
using VistaDelModelo;
using System.Collections.Generic;

namespace AppPrueba
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

           // DependencyService.Register<MockDataStore>();
            MainPage = new NavigationPage(new LoginPage());
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
            if (Application.Current.Properties.ContainsKey("IsLogged"))
            {
                MainPage = new MasterMenu();
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
        }
    }
}
