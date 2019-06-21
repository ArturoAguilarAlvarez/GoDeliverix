﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VistaDelModelo;
using System.Diagnostics;
using Modelo;

namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerfilTelefonoPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }
        public ObservableCollection<VMTelefono> TelefonoLista { get; set; }
        public PerfilTelefonoPage()
        {
            
            InitializeComponent();

            //         Items = new ObservableCollection<string>
            //         {
            //             "Item 1",
            //             "Item 2",
            //             "Item 3",
            //             "Item 4",
            //             "Item 5"
            //         };

            //MyListView.ItemsSource = Items;
            //AppCliente.App.MVTelefono.TipoDeTelefonos();
            //AppCliente.App.MVTelefono.BuscarTelefonos(UidPropietario: new Guid(AppCliente.App.Global1), ParadetroDeBusqueda: "Usuario");

            App.MVTelefono.TipoDeTelefonos();
            App.MVTelefono.BuscarTelefonos(UidPropietario: new Guid(App.Global1), ParadetroDeBusqueda: "Usuario");
            CargarNombreTelefono();




            for (int i = 0; i < AppCliente.App.MVTelefono.ListaDeTelefonos.Count; i++)
            {
                AppCliente.App.MVTelefono.ListaDeTelefonos[i].Estado = false;
            }

            MyPicker.ItemsSource = AppCliente.App.MVTelefono.TIPOTELEFONO;
            MyListView.ItemsSource = AppCliente.App.MVTelefono.ListaDeTelefonos;
        }

        public void CargarNombreTelefono()
        {
            int a = AppCliente.App.MVTelefono.ListaDeTelefonos.Count();
            a = a - 1;
            MyListView.ItemsSource = AppCliente.App.MVTelefono.ListaDeTelefonos;
            for (int i = 0; i <= a; i++)
            {
                AppCliente.App.MVTelefono.ListaDeTelefonos[i].StrNombreTipoDeTelefono = AppCliente.App.MVTelefono.TIPOTELEFONO.Where(t => t.ID == AppCliente.App.MVTelefono.ListaDeTelefonos[i].UidTipo).FirstOrDefault().NOMBRE;

                //int index = MVTelefono.TIPOTELEFONO.FindIndex(x => x.ID == MVTelefono.ListaDeTelefonos[i].ID);
                //MVTelefono.ListaDeTelefonos[i].StrNombreTipoDeTelefono = MVTelefono.TIPOTELEFONO[index].NOMBRE;
            }
        }

        public void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var item = ((ItemTappedEventArgs)e);
            VMTelefono ObjItem = (VMTelefono)item.Item;
            if (ObjItem.Estado)
            {
                ObjItem.Estado = false;
                txtIDTelefono.Text = "0";
            }
            else
            {
                for (int i = 0; i < AppCliente.App.MVTelefono.ListaDeTelefonos.Count; i++)
                {
                    AppCliente.App.MVTelefono.ListaDeTelefonos[i].Estado = false;
                }            
                ObjItem.Estado = true;
                txtIDTelefono.Text = ObjItem.ID.ToString();
            }
            MyListView.ItemsSource = null;
            MyListView.ItemsSource = AppCliente.App.MVTelefono.ListaDeTelefonos;
            //var item = ((VMTelefono)sender);
            //VMTelefono ObjItem = (VMTelefono)item.CommandParameter;

            ////if (e.Item == null)
            ////    return;
            ////string a=((VMTelefono)((MenuItem)sender).CommandParameter).TIPOTELEFONO.ToString();
            //await DisplayAlert("Item Tapped","An item was tapped.", "OK");

            ////Deselect Item
            ////((ListView)sender).SelectedItem = null;
        }

        private void OnMore(object sender, EventArgs e)
        {
            btnCancelar.IsVisible = true;
            btnGuardarEditar.IsVisible = true;
            cajaDatosTelefono.IsVisible = true;
            var item = ((MenuItem)sender);
            VMTelefono ObjItem = (VMTelefono)item.CommandParameter;
            txtNumeroTelefonico.Text = ObjItem.NUMERO;

            txtIDTelefono.Text = ObjItem.ID.ToString();
            int query1=AppCliente.App.MVTelefono.TIPOTELEFONO.FindIndex(t => t.NOMBRE == ObjItem.StrNombreTipoDeTelefono);
            MyPicker.SelectedIndex= query1;
        }

        private void OnDelete(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
            VMTelefono ObjItem = (VMTelefono)item.CommandParameter;
            EliminarTelefono(ObjItem);
        }

        public async void EliminarTelefono(VMTelefono ObjItem)
        {
            var action = await DisplayAlert("Exit?", "Are you sure to close", "Yes", "No");
            if (action)
            {

                Guid Gui = ObjItem.ID;
                int index = AppCliente.App.MVTelefono.ListaDeTelefonos.FindIndex(x => x.ID == Gui);
                AppCliente.App.MVTelefono.EliminaTelefonoUsuario(ObjItem.ID.ToString());
                //MVTelefono.ListaDeTelefonos[index].NUMERO = "1234";
                AppCliente.App.MVTelefono.BuscarTelefonos(UidPropietario: new Guid(AppCliente.App.Global1), ParadetroDeBusqueda: "Usuario");
                MyListView.ItemsSource = null;
                CargarNombreTelefono();
                MyListView.ItemsSource = AppCliente.App.MVTelefono.ListaDeTelefonos;
            }
        }

        void OnRefresh(object sender, EventArgs e)
        {
         
        }

        private void BtnGuardarEditar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNumeroTelefonico.Text))
            {
                try
                {

                    TipoDeTelefono a = (TipoDeTelefono)MyPicker.SelectedItem;
                    string Numero = txtNumeroTelefonico.Text;
                    string TipoTelefono = a.ID.ToString();
                    string NombreTipoTelefono = a.NOMBRE.ToString();

                    if (!string.IsNullOrEmpty(txtIDTelefono.Text))
                    {

                        //MVTelefono.ActualizaRegistroEnListaDeTelefonos(TipoTelefono)
                        AppCliente.App.MVTelefono.ActualizaRegistroEnListaDeTelefonos(txtIDTelefono.Text, TipoTelefono, txtNumeroTelefonico.Text);
                    }
                    else
                    {
                        AppCliente.App.MVTelefono.AgregaTelefonoALista(TipoTelefono, txtNumeroTelefonico.Text, NombreTipoTelefono);
                    }

                    //Guarda los telefonos
                    if (AppCliente.App.MVTelefono.ListaDeTelefonos != null)
                    {
                        if (AppCliente.App.MVTelefono.ListaDeTelefonos.Count != 0)
                        {

                            AppCliente.App.MVTelefono.EliminaTelefonosUsuario(new Guid(AppCliente.App.Global1));
                            AppCliente.App.MVTelefono.GuardaTelefono(new Guid(AppCliente.App.Global1), "Usuario");
                        }
                    }
                    AppCliente.App.MVTelefono.BuscarTelefonos(UidPropietario: new Guid(AppCliente.App.Global1), ParadetroDeBusqueda: "Usuario");
                    CargarNombreTelefono();
                    MyListView.ItemsSource = null;
                    MyListView.ItemsSource = AppCliente.App.MVTelefono.ListaDeTelefonos;

                    MyPicker.ItemsSource = AppCliente.App.MVTelefono.TIPOTELEFONO;
                    txtNumeroTelefonico.Text = "";
                    txtIDTelefono.Text = "";
                    MyListView.IsVisible = true;
                    btnNuevoNumero.IsVisible = true;

                    cajaDatosTelefono.IsVisible = false;
                    btnGuardarEditar.IsVisible = false;
                    btnCancelar.IsVisible = false;
                }
                catch (Exception)
                {
                     DisplayAlert("!Ooooops", "NO ha seleccionado el tipo de telefono", "ok");
                }

            }
        }

        private void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            cajaDatosTelefono.IsVisible = false;
            btnGuardarEditar.IsVisible = false;
            btnCancelar.IsVisible = false;
            MyListView.IsVisible = true;
            btnNuevoNumero.IsVisible = true;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            btnCancelar.IsVisible = true;
            btnGuardarEditar.IsVisible = true;
            cajaDatosTelefono.IsVisible = true;
            txtNumeroTelefonico.Text = "";
            txtIDTelefono.Text = "";
            MyPicker.SelectedItem = 0;
            btnNuevoNumero.IsVisible = false;
            MyListView.IsVisible = false;
        }

        private void Button_Clicked_Eliminar(object sender, EventArgs e)
        {
            EliminarTelefono2();
        }

        public async void EliminarTelefono2()
        {
            if (txtIDTelefono.Text!="0" && !string.IsNullOrEmpty(txtIDTelefono.Text))
            {
                var action = await DisplayAlert("Eliminar?", "Estas seguro de eliminar este Telefono", "Si", "No");
                if (action)
                {

                    //Guid Gui = new Guid(txtIDTelefono.Text);
                    //int index = AppCliente.App.MVTelefono.ListaDeTelefonos.FindIndex(x => x.ID == Gui);
                    AppCliente.App.MVTelefono.EliminaTelefonoUsuario(txtIDTelefono.Text);
                    //MVTelefono.ListaDeTelefonos[index].NUMERO = "1234";
                    AppCliente.App.MVTelefono.BuscarTelefonos(UidPropietario: new Guid(AppCliente.App.Global1), ParadetroDeBusqueda: "Usuario");
                    MyListView.ItemsSource = null;
                    CargarNombreTelefono();
                    MyListView.ItemsSource = AppCliente.App.MVTelefono.ListaDeTelefonos;
                }
            }

        }

        private void Button_Clicked_Editar(object sender, EventArgs e)
        {
            MyListView.IsVisible = false;
            btnCancelar.IsVisible = true;
            btnGuardarEditar.IsVisible = true;
            cajaDatosTelefono.IsVisible = true;
            var item = AppCliente.App.MVTelefono.ListaDeTelefonos.Find(t => t.ID == new Guid(txtIDTelefono.Text));
            txtNumeroTelefonico.Text = AppCliente.App.MVTelefono.ListaDeTelefonos.Find(t => t.ID == new Guid(txtIDTelefono.Text)).NUMERO;
            txtIDTelefono.Text =txtIDTelefono.Text;
            int query1 = AppCliente.App.MVTelefono.TIPOTELEFONO.FindIndex(t => t.NOMBRE == item.StrNombreTipoDeTelefono);
            MyPicker.SelectedIndex = query1;
            btnNuevoNumero.IsVisible = false;
          
        }
    }
}