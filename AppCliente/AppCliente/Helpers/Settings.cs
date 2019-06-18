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

        public static void ClearAllData()
        {
            AppSettings.Clear();
        }
    }
}
