using Android.App;
using Android.Content.PM;
using Android.OS;
using Com.OneSignal;
using Com.OneSignal.Abstractions;
using ImageCircle.Forms.Plugin.Droid;
using System.Collections.Generic;

namespace AppCliente.Droid
{
    [Activity(Label = "GoDeliverix", Icon = "@drawable/logo48", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Xfx.XfxControls.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            global::Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            global::Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.FormsMaps.Init(this, savedInstanceState);
            Xamarin.FormsGoogleMaps.Init(this, savedInstanceState);
            OneSignal.Current.StartInit("170c0582-a7c3-4b75-b1a8-3fe4a952351f").Settings(settings: new Dictionary<string, bool>() { { IOSSettings.kOSSettingsKeyAutoPrompt, true }, { IOSSettings.kOSSettingsKeyInAppLaunchURL, true } })
                  .EndInit();
            ImageCircleRenderer.Init();
            LoadApplication(new App());
        }
    }
}