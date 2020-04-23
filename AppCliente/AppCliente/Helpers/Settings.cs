using Plugin.Settings;
using Plugin.Settings.Abstractions;


namespace AppCliente.Helpers
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

        public static string UidDireccion
        {
            get => AppSettings.GetValueOrDefault(nameof(UidDireccion), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(UidDireccion), value);
        }
        public static string StrPAIS
        {
            get => AppSettings.GetValueOrDefault(nameof(StrPAIS), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(StrPAIS), value);
        }
        public static string StrESTADO
        {
            get => AppSettings.GetValueOrDefault(nameof(StrESTADO), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(StrESTADO), value);
        }
        public static string StrMUNICIPIO
        {
            get => AppSettings.GetValueOrDefault(nameof(StrMUNICIPIO), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(StrMUNICIPIO), value);
        }
        public static string StrCIUDAD
        {
            get => AppSettings.GetValueOrDefault(nameof(StrCIUDAD), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(StrCIUDAD), value);
        }
        public static string StrCOLONIA
        {
            get => AppSettings.GetValueOrDefault(nameof(StrCOLONIA), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(StrCOLONIA), value);
        }
        public static string StrCALLE0
        {
            get => AppSettings.GetValueOrDefault(nameof(StrCALLE0), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(StrCALLE0), value);
        }
        public static string StrCALLE1
        {
            get => AppSettings.GetValueOrDefault(nameof(StrCALLE1), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(StrCALLE1), value);
        }
        public static string StrCALLE2
        {
            get => AppSettings.GetValueOrDefault(nameof(StrCALLE2), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(StrCALLE2), value);
        }
        public static string StrMANZANA
        {
            get => AppSettings.GetValueOrDefault(nameof(StrMANZANA), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(StrMANZANA), value);
        }
        public static string StrLOTE
        {
            get => AppSettings.GetValueOrDefault(nameof(StrLOTE), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(StrLOTE), value);
        }
        public static string StrCodigoPostal
        {
            get => AppSettings.GetValueOrDefault(nameof(StrCodigoPostal), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(StrCodigoPostal), value);
        }
        public static string StrREFERENCIA
        {
            get => AppSettings.GetValueOrDefault(nameof(StrREFERENCIA), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(StrREFERENCIA), value);
        }
        public static string StrIDENTIFICADOR
        {
            get => AppSettings.GetValueOrDefault(nameof(StrIDENTIFICADOR), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(StrIDENTIFICADOR), value);
        }
        public static string StrNombreCiudad
        {
            get => AppSettings.GetValueOrDefault(nameof(StrNombreCiudad), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(StrNombreCiudad), value);
        }
        public static string StrNombreColonia
        {
            get => AppSettings.GetValueOrDefault(nameof(StrNombreColonia), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(StrNombreColonia), value);
        }
        public static string StrLongitud
        {
            get => AppSettings.GetValueOrDefault(nameof(StrLongitud), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(StrLongitud), value);
        }
        public static string StrLatitud
        {
            get => AppSettings.GetValueOrDefault(nameof(StrLatitud), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(StrLatitud), value);
        }

        public const string sitio = "https://www.godeliverix.net";
        //public const string sitio = "http://192.168.1.87";
        public static void ClearAllData()
        {
            AppSettings.Clear();
        }
    }
}
