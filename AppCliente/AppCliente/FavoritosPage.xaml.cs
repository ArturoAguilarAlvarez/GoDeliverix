using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;

using Xamarin.Essentials;
using System.Linq;

namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FavoritosPage : ContentPage
	{
        public FavoritosPage()
        {
            InitializeComponent();
        }
    }
}