using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VistaDelModelo;
using Rg.Plugins.Popup.Services;
using System.Net.Http;
using AppCliente.WebApi;
using Newtonsoft.Json;

namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductoDescripcionPage : ContentPage
    {
        VMProducto objProducto;
        double cantidad = 1;
        string costo;
        string CostoPorSucursal = "";
        Guid idSeccion;
        bool tipo = false;
        string UIDEmpresa;
        bool Cargando = false;

        public static List<VMProducto> ListaPreciosProcto = new List<VMProducto>();

        public ProductoDescripcionPage()
        {
            InitializeComponent();
            tipo = false;
        }

        //inicio con busqueda de productos
        public ProductoDescripcionPage(VMProducto objProducto, List<VMProducto> lista)
        {
            InitializeComponent();
            CargaVentanaBusquedaProductos(objProducto, lista);
        }
        protected async void CargaVentanaBusquedaProductos(VMProducto objProducto, List<VMProducto> lista)
        {
            Cargando = true;
            pnlDescripcionProducto.IsVisible = false;
            Title = objProducto.STRNOMBRE;
            UIDEmpresa = objProducto.UIDEMPRESA.ToString();
            this.objProducto = objProducto;
            ImagenProducto.Source = objProducto.STRRUTA;

            txtNombreProducto.Text = objProducto.STRNOMBRE;
            txtDescripcionProducto.Text = objProducto.STRDESCRIPCION;
            txtIDSeccion.Text = lista[0].UID.ToString();
            idSeccion = lista[0].UID;
            txtSucursalSeleccionada.Text = lista[0].StrIdentificador;
            ListaPreciosProcto = lista;
            App.MVProducto.ListaDePreciosSucursales = lista;
            using (HttpClient _WebApi = new HttpClient())
            {
                string _URL = "" + Helpers.Settings.sitio + "/api/Seccion/GetBuscarSeccion?UIDSECCION=" + idSeccion.ToString() + "&UidEstado=" + App.UidEstadoABuscar + "&UidColonia=" + App.UidColoniaABuscar + "";
                var content = await _WebApi.GetStringAsync(_URL);
                var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                var oSeccion = JsonConvert.DeserializeObject<VMSeccion>(obj);
                txtHoraDisponibilidad.Text = "Disponible hasta " + oSeccion.StrHoraFin + "";

                _URL = "" + Helpers.Settings.sitio + "/api/Usuario/GetObtenerHora?UidEstado=" + App.UidEstadoABuscar + "";
                content = await _WebApi.GetStringAsync(_URL);
                obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                DateTime HoraActual = DateTime.Parse(obj);

                DateTime HoraSeccion = DateTime.Parse(oSeccion.StrHoraFin);
                TimeSpan TiempoRestante = new TimeSpan(0, 10, 0);
                TimeSpan Diferencia = new TimeSpan();
                Diferencia = (HoraSeccion - HoraActual);
                if (Diferencia <= TiempoRestante)
                {
                    txtHoraDisponibilidad.TextColor = Color.Red;
                    await DisplayAlert("¡Aviso!", "El producto esta por terminar\n Pide rapido!", "Aceptar");
                }
            }
            txtEmpresaCosto.Text = "$" + lista[0].StrCosto;
            btnAgregarCarrito.Text = "Agregar $" + lista[0].StrCosto;
            idSeucursalSeleccionada.Text = lista[0].UidSucursal.ToString();
            HttpClient _api = new HttpClient();
            string _link = "" + Helpers.Settings.sitio + "/api/Imagen/GetImagenDePerfilEmpresa?UidEmpresa=" + lista[0].UIDEMPRESA + "";
            var conten = await _api.GetStringAsync(_link);
            var ob = JsonConvert.DeserializeObject<ResponseHelper>(conten).Data.ToString();
            var oimagen = JsonConvert.DeserializeObject<VMImagen>(ob);
            string ruta = Helpers.Settings.sitio + "/vista/" + oimagen.STRRUTA;
            ImagenEmpresa.Source = ruta;
            tipo = true;
            Cargando = false;
            pnlDescripcionProducto.IsVisible = true;
        }

        //inicio con busqueda de empresa
        public ProductoDescripcionPage(VMProducto objProducto, Guid UiEmpresa, VMSeccion objSeccion)
        {
            InitializeComponent();
            CargaVentanaBusquedaDeEmpresa(objProducto, UiEmpresa, objSeccion);
        }
        protected async void CargaVentanaBusquedaDeEmpresa(VMProducto objProducto, Guid UiEmpresa, VMSeccion objSeccion)
        {
            UIDEmpresa = objProducto.UIDEMPRESA.ToString();
            this.objProducto = objProducto;
            ImagenProducto.Source = objProducto.STRRUTA;
            txtNombreProducto.Text = objProducto.STRNOMBRE;
            txtDescripcionProducto.Text = objProducto.STRDESCRIPCION;

            txtEmpresaCosto.Text = "Precio: $" + objProducto.StrCosto;
            CostoPorSucursal = objProducto.StrCosto;
            txtIDSeccion.Text = objSeccion.UID.ToString();
            idSeucursalSeleccionada.Text = UiEmpresa.ToString();
            //txtSucursall.IsVisible = false;
            txtSucursalSeleccionada.IsVisible = false;
            btnAgregarCarrito.Text = "Agregar  $" + objProducto.StrCosto;
            idSeccion = objSeccion.UID;
            using (HttpClient _WebApi = new HttpClient())
            {
                string _URL = "" + Helpers.Settings.sitio + "/api/Seccion/GetBuscarSeccion?UIDSECCION=" + objSeccion.UID.ToString() + "&UidEstado=" + App.UidEstadoABuscar + "&UidColonia=" + App.UidColoniaABuscar + "";
                var content = await _WebApi.GetStringAsync(_URL);
                var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                var oSeccion = JsonConvert.DeserializeObject<VMSeccion>(obj);
                txtHoraDisponibilidad.Text = "Disponible hasta " + oSeccion.StrHoraFin + "";

            }
            tipo = false;
        }
        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            double value = e.NewValue;
            _displayLabel.Text = string.Format("{0}", value);
            cantidad = value;
            string costoo;
            if (ListaPreciosProcto.Count > 0)
            {
                costoo = ListaPreciosProcto.Find(t => t.UID.ToString() == txtIDSeccion.Text).StrCosto;
            }
            else
            {
                costoo = CostoPorSucursal;
            }

            costo = (Double.Parse(costoo) * value).ToString();


            btnAgregarCarrito.Text = "Agregar  $" + costo;
        }

        private async void ButtonCarrito_Clicked(object sender, EventArgs e)
        {
            //await PopupNavigation.Instance.PushAsync(new Popup.PopupLoanding());
            if (cantidad > 0)
            {

                App.MVTarifario.BuscarTarifario("Cliente", ZonaEntrega: App.UidColoniaABuscar, uidSucursal: idSeucursalSeleccionada.Text);

                objProducto.IsSelected = true;
                AgregarAlcarrito(objProducto.UID, new Guid(idSeucursalSeleccionada.Text), idSeccion, cantidad.ToString(), txtComentario.Text);
                await DisplayAlert("¡Que bien!", "Productos agregados al carrito", "ok");
                await Navigation.PopToRootAsync();


            }
            else
            {
                // await PopupNavigation.Instance.PopAllAsync();
                await DisplayAlert("Sorry", "No se puede agregar al carrito por que no a seleccionado una cantidad valida", "ok");
            }
            //await PopupNavigation.Instance.PopAllAsync();
        }

        private void BtnCambioDeSucursal_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SeleccionarSucursalPrecioProducto(App.MVProducto.ListaDePreciosSucursales, btnAgregarCarrito, txtSucursalSeleccionada, txtEmpresaCosto, cantidad, txtIDSeccion, idSeucursalSeleccionada));
        }

        protected void AgregarAlcarrito(Guid UidProducto, Guid UidSucursal, Guid UidSeccion, string StrCantidad, string StrNotas = "")
        {
            #region Guardar el link de imagen
            string URLEmpresa;
            if (UIDEmpresa == "00000000-0000-0000-0000-000000000000")
            {
                URLEmpresa = "" + Helpers.Settings.sitio + "/vista/" + App.MVImagen.STRRUTA;
            }
            else
            {
                App.MVImagen.ObtenerImagenPerfilDeEmpresa(UIDEmpresa);
                URLEmpresa = "" + Helpers.Settings.sitio + "/vista/" + App.MVImagen.STRRUTA;
            }
            #endregion

            var Producto = App.MVProducto.ListaDelCarrito.FindAll(Objeto => Objeto.UID == UidProducto && Objeto.UidNota == Guid.Empty && Objeto.UidSucursal == new Guid(idSeucursalSeleccionada.Text));
            if (Producto.Count <= 1 && !string.IsNullOrEmpty(StrNotas) || (Producto.Count == 0 && string.IsNullOrEmpty(StrNotas)))
            {
                if (App.MVTarifario.ListaDeTarifarios.Count > 0)
                {
                    Guid UidTarifario = new Guid();
                    decimal DmPrecio = 0.0m;
                    for (int i = 0; i < 1; i++)
                    {
                        UidTarifario = App.MVTarifario.ListaDeTarifarios[i].UidTarifario;
                        DmPrecio = App.MVTarifario.ListaDeTarifarios[i].DPrecio;
                    }
                    //UidSeccion
                    App.MVProducto.AgregaAlCarrito(UidProducto, new Guid(idSeucursalSeleccionada.Text), new Guid(txtIDSeccion.Text), StrCantidad, DmPrecio, UidTarifario, strNota: StrNotas, URLEmpresa: URLEmpresa);
                } // los datos de la informacion del tarifario se muestran vacios en caso de existir varios registros para esta orden.
                else
                {
                    if (tipo)
                    {
                        App.MVProducto.AgregaAlCarrito(UidProducto, UidSeccion, UidSucursal, StrCantidad, 0.0m, Guid.Empty, strNota: StrNotas, URLEmpresa: URLEmpresa);
                    }
                    else
                    {
                        App.MVProducto.AgregaAlCarrito(UidProducto, UidSucursal, UidSeccion, StrCantidad, 0.0m, Guid.Empty, strNota: StrNotas, URLEmpresa: URLEmpresa);
                    }
                }

                decimal Subtotal = 0.0m;
                decimal Envio = 0.0m;
                int cantidad = 0;
                foreach (var producto in App.MVProducto.ListaDelCarrito)
                {
                    cantidad = cantidad + producto.Cantidad;
                    Subtotal = Subtotal + decimal.Parse(producto.StrCosto);
                }
                foreach (var obj in App.MVProducto.ListaDelInformacionSucursales)
                {
                    Envio = Envio + obj.CostoEnvio;
                }
            }
            else
            {
                var productoRepetido = App.MVProducto.ListaDelCarrito.Find(Objeto => Objeto.UID == UidProducto && Objeto.UidNota == Guid.Empty && Objeto.UidSucursal == UidSucursal);
                App.MVProducto.AgregaAlCarrito(UidProducto, new Guid(idSeucursalSeleccionada.Text), new Guid(txtIDSeccion.Text), StrCantidad, 0.0m, Guid.Empty, strNota: StrNotas, URLEmpresa: URLEmpresa, RegistroProductoEnCarrito: productoRepetido.UidRegistroProductoEnCarrito);
            }

        }

        private void ButtonMenos_Clicked(object sender, EventArgs e)
        {
            Cargando = true;
            pnlDescripcionProducto.IsVisible = false;
            if (cantidad == 1)
            {

            }
            else
            {
                cantidad = cantidad - 1;
                string costoo;
                if (ListaPreciosProcto.Count > 0)
                {
                    costoo = ListaPreciosProcto.Find(t => t.UidSucursal.ToString() == idSeucursalSeleccionada.Text).StrCosto;
                }
                else
                {
                    costoo = CostoPorSucursal;
                }
                costo = (Double.Parse(costoo) * cantidad).ToString();
                _displayLabel.Text = string.Format("{0}", cantidad);
                btnAgregarCarrito.Text = "Agregar  $" + costo;
            }
            Cargando = false;
            pnlDescripcionProducto.IsVisible = true;
        }

        private void ButtonMas_Clicked(object sender, EventArgs e)
        {
            Cargando = true;
            pnlDescripcionProducto.IsVisible = false;
            cantidad = cantidad + 1;
            string costoo;
            if (ListaPreciosProcto.Count > 0)
            {
                costoo = ListaPreciosProcto.Find(t => t.UidSucursal.ToString() == idSeucursalSeleccionada.Text).StrCosto;
            }
            else
            {
                costoo = CostoPorSucursal;
            }

            costo = (Double.Parse(costoo) * cantidad).ToString();

            _displayLabel.Text = string.Format("{0}", cantidad);
            btnAgregarCarrito.Text = "Agregar  $" + costo;
            if (cantidad > 1)
            {

                btnMenos.IsEnabled = true;
            }
            Cargando = false;
            pnlDescripcionProducto.IsVisible = true;
        }

        private void _displayLabel_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string NewValue = e.NewTextValue.ToString();
                string oldValue = e.OldTextValue.ToString();
                if (NewValue.Length > 0)
                {
                    btnAgregarCarrito.IsEnabled = true;
                    for (int i = 0; i < NewValue.Length; i++)
                    {
                        if (
                            NewValue.Substring(i, 1).Equals("1") ||
                            NewValue.Substring(i, 1).Equals("2") ||
                            NewValue.Substring(i, 1).Equals("3") ||
                            NewValue.Substring(i, 1).Equals("4") ||
                            NewValue.Substring(i, 1).Equals("5") ||
                            NewValue.Substring(i, 1).Equals("6") ||
                            NewValue.Substring(i, 1).Equals("7") ||
                            NewValue.Substring(i, 1).Equals("8") ||
                            NewValue.Substring(i, 1).Equals("9") ||
                            NewValue.Substring(i, 1).Equals("7") ||
                            NewValue.Substring(i, 1).Equals("0")
                            )
                        {
                            cantidad = int.Parse(NewValue);
                            string costoo;
                            if (ListaPreciosProcto.Count > 0)
                            {
                                costoo = ListaPreciosProcto.Find(t => t.UidSucursal.ToString() == idSeucursalSeleccionada.Text).StrCosto;
                            }
                            else
                            {
                                costoo = CostoPorSucursal;
                            }

                            costo = (Double.Parse(costoo) * cantidad).ToString();

                            _displayLabel.Text = string.Format("{0}", cantidad);
                            btnAgregarCarrito.Text = "Agregar  $" + costo;
                        }
                        else
                        {
                            _displayLabel.Text = oldValue;
                            break;
                        }
                    }
                }
                else
                {
                    btnAgregarCarrito.IsEnabled = false;
                }

            }
            catch (Exception)
            {
            }
        }

    }
}