using Xamarin.Forms.GoogleMaps;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using VistaDelModelo;

namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeleccionTuUbicacionMapa : ContentPage
    {
        Position MyPosicion= new Position(0, 0);
        VMDireccion objDireccion=null;
        public SeleccionTuUbicacionMapa()
        {
            InitializeComponent();
        }

        public SeleccionTuUbicacionMapa(VMDireccion objDireccion)
        {
            InitializeComponent();
            this.objDireccion = objDireccion;
            map.Pins.Clear();

            Pin AquiEstoy = new Pin()
            {
                Type = PinType.Place,
                Label = "Aki toy =_=",
                Position = new Position(double.Parse(objDireccion.Latitud), double.Parse(objDireccion.Longitud))
            };

            map.Pins.Add(AquiEstoy);
            var pos = new Position(double.Parse(objDireccion.Latitud), double.Parse(objDireccion.Longitud));

            MyPosicion = pos;
            map.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromMeters(5000)), true);

        }

        private void Map_MapClicked(object sender, Xamarin.Forms.GoogleMaps.MapClickedEventArgs e)
        {
            var lat = e.Point.Latitude.ToString("0.000");
            var lng = e.Point.Longitude.ToString("0.000");
            Pin AquiEstoy = new Pin()
            {
                Type = PinType.Place,
                Label = "Aki toy =_=",
                Position = new Position(double.Parse(lat), double.Parse(lng))
            };
            map.Pins.Clear();
            map.Pins.Add(AquiEstoy);

            var pos = new Position(double.Parse(lat), double.Parse(lng));
            MyPosicion = pos;
        }

        private async void Button_MiUbicacion(object sender, EventArgs e)
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location != null)
            {
                double Latitud = location.Latitude;
                double Longitud = location.Longitude;
                var pos = new Position(Latitud, Longitud);
                map.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromMeters(500)));
                MyPosicion = pos;

                Pin AquiEstoy = new Pin()
                {
                    Type = PinType.Place,
                    Label = "Aki toy =_=",
                    Position = new Position(Latitud, Longitud)
                };
                map.Pins.Clear();
                map.Pins.Add(AquiEstoy);
            }
        }

        private void Button_ContinuarConElProcesoGuardado(object sender, EventArgs e)
        {
            if (MyPosicion.Latitude!=0)
            {
                if (objDireccion==null)
                {
                    Navigation.PushAsync(new DireccionModificar(MyPosicion));
                }
                else
                {
                    Navigation.PushAsync(new DireccionModificar(MyPosicion, objDireccion));
                }
            }
            else
            {
                DisplayAlert("", "Seleccione su ubicacion", "ok");
            }
        }
    }
}