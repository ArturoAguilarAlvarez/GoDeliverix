﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace DeliverixSucursales
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("es-ES");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("es-ES");
        }
    }
}
