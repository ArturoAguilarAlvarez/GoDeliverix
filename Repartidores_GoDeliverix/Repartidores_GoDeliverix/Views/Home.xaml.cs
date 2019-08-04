using Repartidores_GoDeliverix.Views.Popup;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Repartidores_GoDeliverix.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        static int ContentWidth = (int)(App.ScreenWidth / App.ScreenDensity);
        static int ContentHeight = (int)(App.ScreenHeight / App.ScreenDensity);
        static int ThresholdMax = Math.Max(ContentHeight, ContentWidth);
        static int ThresholdMin = Math.Min(ContentHeight, ContentWidth);
        public Home()
        {
            InitializeComponent();
        }

        private async void BtnNuevaOrden_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new PopoLoading());
            //await Navigation.PopAsync();
            //await Navigation.PushAsync(new Home_NuevaOrden());

        }

        private async void BtnQr_Clicked(object sender, EventArgs e)
        {


        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width != ContentWidth || height != ContentHeight)
            {
                ContentWidth = (int)width;
                ContentHeight = (int)height;
                GContenedorPrincipal.HorizontalOptions = LayoutOptions.FillAndExpand;
                GContenedorPrincipal.VerticalOptions = LayoutOptions.FillAndExpand;


                if (GContenedorPrincipal.RowDefinitions.Count > 0)
                {
                    GContenedorPrincipal.RowDefinitions.Clear();
                }
                if (GContenedorPrincipal.ColumnDefinitions.Count > 0)
                {
                    GContenedorPrincipal.ColumnDefinitions.Clear();
                }

                if (GContenedorPrincipal.Children.IndexOf(GVTitulo) != -1)
                {
                    GContenedorPrincipal.Children.Remove(GVTitulo);
                }
                if (GContenedorPrincipal.Children.IndexOf(ControlesOperacion) != -1)
                {
                    GContenedorPrincipal.Children.Remove(ControlesOperacion);
                }
                if (GContenedorPrincipal.Children.IndexOf(PanelBotones) != -1)
                {
                    GContenedorPrincipal.Children.Remove(PanelBotones);
                }
                if (ContentWidth > ContentHeight)
                {

                    GContenedorPrincipal.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
                    GContenedorPrincipal.RowDefinitions.Add(new RowDefinition { Height = new GridLength(8, GridUnitType.Star) });
                    Grid.SetRow(GVTitulo, 0);
                    Grid.SetColumnSpan(GVTitulo, 2);
                    Grid.SetRow(ControlesOperacion, 1);
                    Grid.SetColumn(ControlesOperacion, 0);
                    Grid.SetRow(PanelBotones, 1);
                    Grid.SetColumn(PanelBotones, 1);
                }
                else
                {
                    GContenedorPrincipal.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    GContenedorPrincipal.RowDefinitions.Add(new RowDefinition { Height = new GridLength(3, GridUnitType.Star) });
                    GContenedorPrincipal.RowDefinitions.Add(new RowDefinition { Height = new GridLength(6, GridUnitType.Star) });

                    Grid.SetRow(GVTitulo, 0); Grid.SetColumnSpan(GVTitulo, 1);
                    Grid.SetRow(ControlesOperacion, 1); Grid.SetColumnSpan(ControlesOperacion, 1);
                    Grid.SetRow(PanelBotones, 2);
                    Grid.SetColumnSpan(PanelBotones, 1);
                    Grid.SetColumn(PanelBotones, 0);
                }

                GContenedorPrincipal.Children.Add(GVTitulo);
                GContenedorPrincipal.Children.Add(ControlesOperacion);
                GContenedorPrincipal.Children.Add(PanelBotones);
            }
        }

        private async void BtnEntregar_Clicked(object sender, EventArgs e)
        {
            
        }

        private async void BtnMapaEspera_ClickedAsync(object sender, EventArgs e)
        {
            

            if (Device.RuntimePlatform == Device.iOS)
            {
               
                    //https://developer.apple.com/library/ios/featuredarticles/iPhoneURLScheme_Reference/MapLinks/MapLinks.html
                    Device.OpenUri(new Uri("http://maps.apple.com/"));
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                // opens the Maps app directly
                Device.OpenUri(new Uri("geo:0,0"));
            }
        }

        private void BtnMapaSucursalCliente_ClickedAsync(object sender, EventArgs e)
        {
            
            if (Device.RuntimePlatform == Device.iOS)
            {
                //https://developer.apple.com/library/ios/featuredarticles/iPhoneURLScheme_Reference/MapLinks/MapLinks.html
                Device.OpenUri(new Uri("comgooglemaps://?center=" + lblUbicacionSucursal.Text + "&zoom=14&views=traffic"));
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                Device.OpenUri(new Uri("geo:0,0?q=" + lblUbicacionSucursal.Text+"&mode=d&avoid=h"));
                // opens the Maps app directly
                // Device.OpenUri(new Uri("geo:" + location.Latitude + "," + location.Longitude + ""));
            }
        }

        private void BtnMapaSucursal_ClickedAsync(object sender, EventArgs e)
        {
            
            if (Device.RuntimePlatform == Device.iOS)
            {
                //https://developer.apple.com/library/ios/featuredarticles/iPhoneURLScheme_Reference/MapLinks/MapLinks.html
                Device.OpenUri(new Uri("comgooglemaps://?center=" + lblUbicacionSucursal.Text + "&zoom=14&views=traffic"));
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                // opens the Maps app directly
                Device.OpenUri(new Uri("geo:0,0?q=" + lblUbicacionSucursal.Text + ""));
            }
        }

        private void BtnMapaCliente_ClickedAsync(object sender, EventArgs e)
        {
            

            if (Device.RuntimePlatform == Device.iOS)
            {
                //https://developer.apple.com/library/ios/featuredarticles/iPhoneURLScheme_Reference/MapLinks/MapLinks.html
                Device.OpenUri(new Uri("comgooglemaps://?center=" + lblUbicacionCliente.Text + "&zoom=14&views=traffic"));
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                // opens the Maps app directly
                Device.OpenUri(new Uri("geo:0,0?q=" + lblUbicacionCliente.Text + ""));
            }
        }
    }
}