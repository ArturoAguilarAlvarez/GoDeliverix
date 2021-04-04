using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
namespace Repartidores_GoDeliverix.Helpers
{
    public class settings
    {
        public static ISettings AppSettings
        {
            get { return CrossSettings.Current; }
        }

        public static string UserName
        {
            get => AppSettings.GetValueOrDefault(nameof(UserName), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(UserName), value);
        }

        public static string Password
        {
            get => AppSettings.GetValueOrDefault(nameof(Password), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Password), value);
        }

        public static void ClearAllData()
        {
            AppSettings.Clear();
        }
        //Produccion
        public static string Sitio = "https://www.godeliverix.net/";
        //Local
        //public static string Sitio = "http://192.168.1.72/";

        //#region data values 
        ////No funciona
        //private const string UserNameKey = "username";
        //private static readonly string EmptyKey = string.Empty;
        //#endregion

        //#region Metodos
        // public static string userName
        //{
        //    get { return AppSettings.GetValueOrDefault(UserNameKey, EmptyKey); }
        //    set { AppSettings.AddOrUpdateValue(UserNameKey, value); }
        //}
        //#endregion
    }
}
