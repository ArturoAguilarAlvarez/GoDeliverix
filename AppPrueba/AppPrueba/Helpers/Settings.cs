using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using VistaDelModelo;

namespace AppPrueba.Helpers
{
    public static class Settings
    {

        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public static List<VMOrden> ListaDeOrdenesPorConfirmar = new List<VMOrden>();


        public static string Licencia
        {
            get => AppSettings.GetValueOrDefault(nameof(Licencia), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Licencia), value);
        }
        public static string Uidusuario
        {
            get => AppSettings.GetValueOrDefault(nameof(Uidusuario), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Uidusuario), value);
        }

        public static string NombreSucursal
        {
            get => AppSettings.GetValueOrDefault(nameof(NombreSucursal), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(NombreSucursal), value);
        }
        public static string UidSucursal
        {
            get => AppSettings.GetValueOrDefault(nameof(UidSucursal), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(UidSucursal), value);
        }

        public static string Perfil
        {
            get => AppSettings.GetValueOrDefault(nameof(Perfil), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Perfil), value);
        }


        public static string Usuario
        {
            get => AppSettings.GetValueOrDefault(nameof(Usuario), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Usuario), value);
        }
        public static string Contrasena
        {
            get => AppSettings.GetValueOrDefault(nameof(Contrasena), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Contrasena), value);
        }
        public static string UidTurno
        {
            get => AppSettings.GetValueOrDefault(nameof(UidTurno), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(UidTurno), value);
        }
        public static void ClearAllData()
        {
            AppSettings.Clear();
        }

        public static void CerrarSesion()
        {
            Perfil = "";
            Usuario = "";
            Contrasena = "";
        }
    }
}
