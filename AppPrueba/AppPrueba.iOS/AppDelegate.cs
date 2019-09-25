﻿using System;
using System.Collections.Generic;
using System.Linq;
using Com.OneSignal;
using Foundation;
using UIKit;

namespace AppPrueba.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            OneSignal.Current.StartInit("170c0582-a7c3-4b75-b1a8-3fe4a952351f").EndInit();
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            return base.FinishedLaunching(app, options);
        }
    }
}
