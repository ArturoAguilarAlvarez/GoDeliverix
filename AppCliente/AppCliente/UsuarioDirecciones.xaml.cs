﻿using AppCliente.WebApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VistaDelModelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCliente
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UsuarioDirecciones : ContentPage
	{

        HttpClient _client = new HttpClient();


        public UsuarioDirecciones ()
		{
			InitializeComponent ();
            Iniciar();
        }


        private void MyListViewDirecciones_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = ((ItemTappedEventArgs)e);
            VMDireccion ObjItem = (VMDireccion)item.Item;
            if (ObjItem.Clicked)
            {
                ObjItem.Clicked = false;
                txtIDDireccionn.Text = "0";
            }
            else
            {
                for (int i = 0; i < AppCliente.App.MVDireccion.ListaDIRECCIONES.Count; i++)
                {
                    AppCliente.App.MVDireccion.ListaDIRECCIONES[i].Clicked = false;
                }
                ObjItem.Clicked = true;
                txtIDDireccionn.Text = ObjItem.ID.ToString();
            }
            MyListViewDirecciones.ItemsSource = null;
            MyListViewDirecciones.ItemsSource = AppCliente.App.MVDireccion.ListaDIRECCIONES;
        }

        private void BtnNuevo_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SeleccionTuUbicacionMapa());
        }

        private void Button_Clicked_Eliminar(object sender, EventArgs e)
        {
            EliminarDireccion();
        }

        //private void Button_Clicked_Editar(object sender, EventArgs e)
        //{
        //    var item = sender as Button;
        //    var ObjItem = item.BindingContext as VMDireccion;

        //    Navigation.PushAsync(new SeleccionTuUbicacionMapa(ObjItem));
        //}

        public async void EliminarDireccion()
        {
            if (txtIDDireccionn.Text != "0" && !string.IsNullOrEmpty(txtIDDireccionn.Text))
            {
                var action = await DisplayAlert("Eliminar?", "Estas seguro de eliminar esta direccion?", "Si", "No");
                if (action)
                {
                    Guid Gui = new Guid(txtIDDireccionn.Text);
                    int index = AppCliente.App.MVDireccion.ListaDIRECCIONES.FindIndex(x => x.ID == Gui);
                    //int index = MVTelefono.ListaDeTelefonos.FindIndex(x => x.ID == Gui);
                    AppCliente.App.MVDireccion.QuitaDireeccionDeLista(txtIDDireccionn.Text);

                    // AppCliente.App.MVDireccion.EliminaDireccionUsuario(txtIDDireccionn.Text);


                    string _Url = $"http://godeliverix.net/api/Direccion/DeleteDireccionUsuario?UidDireccion={txtIDDireccionn.Text}";
                    var content = await _client.DeleteAsync(_Url);

                    AppCliente.App.MVDireccion.ObtenerDireccionesUsuario(AppCliente.App.Global1);
                    MyListViewDirecciones.ItemsSource = null;
                    MyListViewDirecciones.ItemsSource = AppCliente.App.MVDireccion.ListaDIRECCIONES;
                    txtIDDireccionn.Text = "0";
                }
            }

        }

        public async void Iniciar()
        {
            var tex = ("http://godeliverix.net/api/Direccion/GetObtenerDireccionUsuario?UidUsuario=" + App.Global1);
            string strDirecciones = await _client.GetStringAsync(tex);
            var obj = JsonConvert.DeserializeObject<ResponseHelper>(strDirecciones).Data.ToString();
            App.MVDireccion = JsonConvert.DeserializeObject<VMDireccion>(obj);
            //JArray blogPostArray = JArray.Parse(obj.ToString());

            ////IList<VMDireccion> blogPosts 
            //App.MVDireccion.ListaDIRECCIONES = blogPostArray.Select(p => new VMDireccion
            //{
            //    REFERENCIA = (string)p["REFERENCIA"],
            //    ID = (Guid)p["ID"],
            //    PAIS = (string)p["PAIS"],
            //    ESTADO = (string)p["ESTADO"],
            //    MUNICIPIO = (string)p["MUNICIPIO"],
            //    CIUDAD = (string)p["CIUDAD"],
            //    COLONIA = (string)p["COLONIA"],
            //    CALLE0 = (string)p["CALLE0"],
            //    CALLE1 = (string)p["CALLE1"],
            //    CALLE2 = (string)p["CALLE2"],
            //    MANZANA = (string)p["MANZANA"],
            //    LOTE = (string)p["LOTE"],
            //    CodigoPostal = (string)p["CodigoPostal"],
            //    IDENTIFICADOR = (string)p["REFERENCIA"],
            //    NOMBRECIUDAD = (string)p["NOMBRECIUDAD"],
            //    NOMBRECOLONIA = (string)p["NOMBRECOLONIA"],
            //    Clicked = false

            //}).ToList();

            for (int i = 0; i < AppCliente.App.MVDireccion.ListaDIRECCIONES.Count; i++)
            {
                AppCliente.App.MVDireccion.ListaDIRECCIONES[i].Clicked = false;
            }

            MyListViewDirecciones.ItemsSource = AppCliente.App.MVDireccion.ListaDIRECCIONES;
        }
    }
}