using GalaSoft.MvvmLight.Command;
using Repartidores_GoDeliverix.Views.Popup;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using VistaDelModelo;
using Xamarin.Forms;

namespace Repartidores_GoDeliverix.VM
{
    public class VMAjustesTelefono : ControlsController
    {
        #region Propiedades
        private Guid _UidTelefono;
        public Guid UidTelefono
        {
            get { return _UidTelefono; }
            set { SetValue(ref _UidTelefono, value); }
        }
        private string _intNumeroTelefono;
        public string intNumeroTelefono
        {
            get { return _intNumeroTelefono; }
            set { SetValue(ref _intNumeroTelefono, value); }
        }
        private Guid _UidTipoDeTelefono;
        public Guid UidTipoDeTelefono
        {
            get { return _UidTipoDeTelefono; }
            set { SetValue(ref _UidTipoDeTelefono, value); }
        }
        private string _StrTipoDeTelefono;
        public string StrTipoDeTelefono
        {
            get { return _StrTipoDeTelefono; }
            set { SetValue(ref _StrTipoDeTelefono, value); }
        }
        private int _intSelectTelefono;
        public int intSelectTelefono
        {
            get { return _intSelectTelefono; }
            set { SetValue(ref _intSelectTelefono, value); }
        }
        //Listas
        private List<VMAjustesTelefono> _LsTipoDeTelefono;
        public List<VMAjustesTelefono> LsTipoDeTelefono
        {
            get { return _LsTipoDeTelefono; }
            set { SetValue(ref _LsTipoDeTelefono, value); }
        }
        #endregion

        #region Comandos
        public ICommand DisplaySettingsCommand { get { return new RelayCommand(EditaTelefono); } }
        public ICommand SaveNumber { get { return new RelayCommand(GuardarTelefono); } }
        public ICommand DeleteNumber { get { return new RelayCommand(EliminaTelefono); } }
        #endregion

        #region Metodos
        private void EliminaTelefono()
        {
            var AppInstance = MainViewModel.GetInstance();
            Guid UidUsuario = AppInstance.Session_.UidUsuario;
            VMTelefono MVTelefono = new VMTelefono();
            MVTelefono.BuscarTelefonos(UidPropietario: UidUsuario, ParadetroDeBusqueda: "Usuario");
            MVTelefono.QuitaTelefonoDeLista(UidTelefono.ToString());
            MVTelefono.EliminaTelefonosUsuario(UidUsuario);
            MVTelefono.GuardaTelefono(UidUsuario, "Usuario");
            AppInstance.MVAjustes.Recargar();
        }
        public async void AgregarTelefono()
        {
            var AppInstance = MainViewModel.GetInstance();
            AppInstance.MVAjustesTelefono = new VMAjustesTelefono();
            AppInstance.MVAjustesTelefono.LsTipoDeTelefono = new List<VMAjustesTelefono>();
            VMTelefono MVTelefono = new VMTelefono();

            MVTelefono.TipoDeTelefonos();
            intSelectTelefono = 0;
            foreach (VMTelefono item in MVTelefono.ListaDeTipoDeTelefono)
            {
                AppInstance.MVAjustesTelefono.LsTipoDeTelefono.Add(new VMAjustesTelefono() { UidTipoDeTelefono = item.UidTipo, StrTipoDeTelefono = item.StrNombreTipoDeTelefono });
            }

            AppInstance.MVAjustesTelefono.intNumeroTelefono = string.Empty;
            AppInstance.MVAjustesTelefono.StrTipoDeTelefono = string.Empty;
            AppInstance.MVAjustesTelefono.UidTipoDeTelefono = Guid.Empty;
            AppInstance.MVAjustesTelefono.UidTelefono = Guid.Empty;
            await Application.Current.MainPage.Navigation.PushAsync(new Ajustes_DetalleTelefono());
        }
        private void GuardarTelefono()
        {
            var AppInstance = MainViewModel.GetInstance();
            Guid UidUsuario = new Guid();
            VMTelefono MVTelefono = new VMTelefono();
            if (UidTelefono == Guid.Empty)
            {
                Guid UidTipoDeTelefono = AppInstance.MVAjustesTelefono.LsTipoDeTelefono[intSelectTelefono].UidTipoDeTelefono;
                string NombreTipoTelfono = AppInstance.MVAjustesTelefono.LsTipoDeTelefono[intSelectTelefono].StrTipoDeTelefono;
                UidUsuario = AppInstance.Session_.UidUsuario;
                MVTelefono.BuscarTelefonos(UidPropietario: UidUsuario, ParadetroDeBusqueda: "Usuario");
                MVTelefono.AgregaTelefonoALista(UidTipoDeTelefono.ToString(), intNumeroTelefono, NombreTipoTelfono);
            }
            else
            {
                Guid UidTipoDeTelefono = AppInstance.MVAjustesTelefono.LsTipoDeTelefono[intSelectTelefono].UidTipoDeTelefono;
                string NombreTipoTelfono = AppInstance.MVAjustesTelefono.LsTipoDeTelefono[intSelectTelefono].StrTipoDeTelefono;

                UidUsuario = AppInstance.Session_.UidUsuario;
                MVTelefono.BuscarTelefonos(UidPropietario: UidUsuario, ParadetroDeBusqueda: "Usuario");
                MVTelefono.ActualizaRegistroEnListaDeTelefonos(UidTelefono.ToString(), UidTipoDeTelefono.ToString(), intNumeroTelefono);

            }
            MVTelefono.EliminaTelefonosUsuario(UidUsuario);
            MVTelefono.GuardaTelefono(UidUsuario, "Usuario");
            AppInstance.MVAjustes.Recargar();
        }
        private async void EditaTelefono()
        {
            var AppInstance = MainViewModel.GetInstance();
            AppInstance.MVAjustesTelefono = new VMAjustesTelefono();
            AppInstance.MVAjustesTelefono.LsTipoDeTelefono = new List<VMAjustesTelefono>();
            VMTelefono MVTelefono = new VMTelefono();

            MVTelefono.TipoDeTelefonos();

            foreach (VMTelefono item in MVTelefono.ListaDeTipoDeTelefono)
            {
                AppInstance.MVAjustesTelefono.LsTipoDeTelefono.Add(new VMAjustesTelefono() { UidTipoDeTelefono = item.UidTipo, StrTipoDeTelefono = item.StrNombreTipoDeTelefono });
            }
            AppInstance.MVAjustesTelefono.intSelectTelefono = AppInstance.MVAjustesTelefono.LsTipoDeTelefono.FindIndex(t => t.UidTipoDeTelefono == UidTipoDeTelefono);
            AppInstance.MVAjustesTelefono.intNumeroTelefono = intNumeroTelefono;
            AppInstance.MVAjustesTelefono.StrTipoDeTelefono = StrTipoDeTelefono;
            AppInstance.MVAjustesTelefono.UidTipoDeTelefono = UidTipoDeTelefono;
            AppInstance.MVAjustesTelefono.UidTelefono = UidTelefono;
            await Application.Current.MainPage.Navigation.PushAsync(new Ajustes_DetalleTelefono());
        }
        protected async void GenerateMessage(string Tittle, string Message, string TextOption)
        {
            await Application.Current.MainPage.DisplayAlert(
              Tittle,
              Message,
              TextOption);
        }
        #endregion

        //Constructor
        public VMAjustesTelefono()
        {

        }
    }
}
