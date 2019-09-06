using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repartidores_GoDeliverix.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Windows.Input;
using VistaDelModelo;
using Xamarin.Forms;

namespace Repartidores_GoDeliverix.VM
{
    public class VMAjustes : ControlsController
    {

        string url = "";
        HttpClient _WebApiGoDeliverix = new HttpClient();

        /// <summary>
        /// Atributos para el popup de los ajustes generales
        /// </summary>
        private string _strNombre;

        public string StrNombre
        {
            get { return _strNombre; }
            set { SetValue(ref _strNombre, value); }
        }
        private string _StrApellidoPaterno;

        public string StrApellidoPaterno
        {
            get { return _StrApellidoPaterno; }
            set { SetValue(ref _StrApellidoPaterno, value); }
        }
        private string _StrApellidoMaterno;

        public string StrApellidoMaterno
        {
            get { return _StrApellidoMaterno; }
            set { SetValue(ref _StrApellidoMaterno, value); }
        }
        private string _strFechaDeNacimiento;

        public string StrFechaDeNacimiento
        {
            get { return _strFechaDeNacimiento; }
            set { SetValue(ref _strFechaDeNacimiento, value); }
        }

        private string _strCorreoElectronico;

        //Datos de correo electronico
        public string StrCorreoElectronico
        {
            get { return _strCorreoElectronico; }
            set { SetValue(ref _strCorreoElectronico, value); }
        }



        private ObservableCollection<VMAjustesItem> _lsAjustes;

        public ObservableCollection<VMAjustesItem> LsAjustes
        {
            get { return _lsAjustes; }
            set { SetValue(ref _lsAjustes, value); }
        }

        private ObservableCollection<VMAjustesTelefono> _lsTelefono;

        public ObservableCollection<VMAjustesTelefono> LsTelefono
        {
            get { return _lsTelefono; }
            set { SetValue(ref _lsTelefono, value); }
        }
        private ObservableCollection<VMAjustesDireccion> _ListaDireccion;

        public ObservableCollection<VMAjustesDireccion> ListaDireccion
        {
            get { return _ListaDireccion; }
            set { SetValue(ref _ListaDireccion, value); }
        }

        private bool _IsLoading;
        private Guid UidUsuario { get; set; }
        public string ModuloACambiar { get; set; }
        public bool IsLoading
        {
            get { return _IsLoading; }
            set { SetValue(ref _IsLoading, value); }
        }

        public VMAjustes()
        {
            Obtendatos();
        }
        public ICommand IsReloading { get { return new RelayCommand(Recargar); } }
        public ICommand IsSavingName { get { return new RelayCommand(GuardarUsuario); } }
        public ICommand IsSavingMail { get { return new RelayCommand(GuardarUsuario); } }
        public ICommand IsSavingBirthday { get { return new RelayCommand(GuardarUsuario); } }
        public ICommand IsSavingPhone { get { return new RelayCommand(GuardarUsuario); } }
        public ICommand AddNumber { get { return new RelayCommand(AgregarTelefono); } }
        public ICommand AddAddres { get { return new RelayCommand(AgregarDireccion); } }

        private void AgregarDireccion()
        {
            var AppInstance = MainViewModel.GetInstance();
            AppInstance.vmAjustesDireccion = new VMAjustesDireccion();
            AppInstance.vmAjustesDireccion.AgregaDireccion();
        }

        private void AgregarTelefono()
        {
            var AppInstance = MainViewModel.GetInstance();
            AppInstance.MVAjustesTelefono = new VMAjustesTelefono();
            AppInstance.MVAjustesTelefono.AgregarTelefono();
        }

        private void GuardarUsuario()
        {
            switch (ModuloACambiar)
            {
                case "Nombre":
                    ActualizaUsuario(Nombre: StrNombre, ApellidoMaterno: StrApellidoMaterno, ApellidoPaterno: StrApellidoPaterno);
                    break;
                case "Fecha de nacimiento":
                    ActualizaUsuario(fnacimiento: StrFechaDeNacimiento);
                    break;
                case "Correo electronico":
                    ActualizaCorreo();
                    break;
                case "Telefonos":
                    ActualizaTelefonos();
                    break;
            }
            Recargar();
        }

        public void ActualizaUsuario(string Nombre = "", string ApellidoPaterno = "", string ApellidoMaterno = "", string password = "", string fnacimiento = "")
        {
            VMUsuarios MVUsuario = new VMUsuarios();
            string PerfilDeUsuario = "DFC29662-0259-4F6F-90EA-B24E39BE4346";
            // _WebApiGoDeliverix.BaseAddress = new Uri("http://www.godeliverix.net/api/");

            if (!string.IsNullOrEmpty(Nombre) && !string.IsNullOrEmpty(ApellidoPaterno) && !string.IsNullOrEmpty(ApellidoMaterno))
            {
                url = "https://www.godeliverix.net/api/Usuario/GetActualizarUsuario?UidUsuario=" + UidUsuario + "&Nombre=" + Nombre + "&ApellidoPaterno=" + ApellidoPaterno + "&ApellidoMaterno=" + ApellidoMaterno + "&perfil=" + PerfilDeUsuario + "";
                // MVUsuario.ActualizarUsuario(UidUsuario: UidUsuario, Nombre: Nombre, ApellidoPaterno: ApellidoPaterno, ApellidoMaterno: ApellidoMaterno, perfil: PerfilDeUsuario);
            }
            if (!string.IsNullOrEmpty(fnacimiento))
            {
                url = "https://www.godeliverix.net/api/Usuario/GetActualizarUsuario?UidUsuario=" + UidUsuario + "&fnacimiento=" + StrFechaDeNacimiento + "&perfil=" + PerfilDeUsuario + "";
                // MVUsuario.ActualizarUsuario(UidUsuario: UidUsuario, fnacimiento: StrFechaDeNacimiento, perfil: PerfilDeUsuario);
            }
            _WebApiGoDeliverix.GetAsync(url);
        }
        public void ActualizaCorreo()
        {
            VMCorreoElectronico MVCorreoElectronico = new VMCorreoElectronico();
            //_WebApiGoDeliverix.BaseAddress = new Uri("http://www.godeliverix.net/api/");
            url = "https://www.godeliverix.net/api/CorreoElectronico/GetActualizarCorreo?UidPropietario=" + UidUsuario + "&strParametroDeInsercion=Usuario&strCorreoElectronico=" + StrCorreoElectronico + "&UidCorreoElectronico=" + Guid.NewGuid() + "";
            _WebApiGoDeliverix.GetAsync(url);

            //MVCorreoElectronico.EliminaCorreoUsuario(UidUsuario.ToString());
            //MVCorreoElectronico.AgregarCorreo(UidUsuario, "Usuario", StrCorreoElectronico, Guid.NewGuid());

        }
        private void ActualizaTelefonos()
        {
            VMTelefono MVTelefono = new VMTelefono();
            //_WebApiGoDeliverix.BaseAddress = new Uri("http://www.godeliverix.net/api/");

            url = "https://www.godeliverix.net/api/Telefono/GetActualizaTelefonoApi?UidPropietario=" + UidUsuario + "&strParametroDeInsercion=Usuario&strCorreoElectronico=" + StrCorreoElectronico + "&UidCorreoElectronico=" + Guid.NewGuid() + "";
            _WebApiGoDeliverix.GetAsync(url);
            //MVTelefono.EliminaTelefonosUsuario(UidUsuario);
            //MVTelefono.GuardaTelefono(UidUsuario, "Usuario");
        }
        public void Recargar()
        {
            Obtendatos();
        }

        protected async void Obtendatos()
        {
            IsLoading = true;
            var AppInstance = MainViewModel.GetInstance();
            UidUsuario = AppInstance.Session_.UidUsuario;
            //Declaracion de las vistas del modelo 
            VMUsuarios MVUsuario = new VMUsuarios();
            //Obtiene los datos
            url = "https://www.godeliverix.net/api/Usuario/GetBuscarUsuarios?UidUsuario=" + UidUsuario + "&UIDPERFIL=DFC29662-0259-4F6F-90EA-B24E39BE4346";
            string content = await _WebApiGoDeliverix.GetStringAsync(url);
            var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
            MVUsuario = JsonConvert.DeserializeObject<VMUsuarios>(obj);
            //Datos del usuario
            AppInstance.Nombre = MVUsuario.StrNombre;
            StrNombre = MVUsuario.StrNombre;
            StrApellidoMaterno = MVUsuario.StrApellidoPaterno;
            StrApellidoPaterno = MVUsuario.StrApellidoMaterno;
            StrFechaDeNacimiento = DateTime.Parse(MVUsuario.DtmFechaDeNacimiento).ToShortDateString();
            //Obtiene el correo electronico
            CargaCorreoElectronico(UidUsuario);
            //Obtiene los telefonos 
            CargaTelefonos(UidUsuario);
            //Obtiene las direcciones
            CargaDirecciones(UidUsuario);

            LsAjustes = new ObservableCollection<VMAjustesItem>();
            LsAjustes.Add(new VMAjustesItem() { StrRuta = "Nombre.png", Titulo = "Nombre", Detalles = StrNombre + " " + StrApellidoMaterno + " " + StrApellidoPaterno });
            LsAjustes.Add(new VMAjustesItem() { StrRuta = "Usuario.png", Titulo = "Usuario", Detalles = MVUsuario.StrUsuario });
            LsAjustes.Add(new VMAjustesItem() { StrRuta = "FechaDeNacimiento.png", Titulo = "Fecha de nacimiento", Detalles = StrFechaDeNacimiento });
            LsAjustes.Add(new VMAjustesItem() { StrRuta = "Mail.png", Titulo = "Correo electronico", Detalles = StrCorreoElectronico });
            //LsAjustes.Add(new VMAjustesItem() { StrRuta = "Telefono.png", Titulo = "Telefonos", Detalles = LsTelefono.Count.ToString() });
            //LsAjustes.Add(new VMAjustesItem() { StrRuta = "Direccion.png", Titulo = "Direcciones", Detalles = ListaDireccion.Count.ToString() });
            LsAjustes.Add(new VMAjustesItem() { StrRuta = "Salida.png", Titulo = "Cerrar sesion", Detalles = "" });
            IsLoading = false;
        }

        private async void CargaDirecciones(Guid uidUsuario)
        {
            url = "https://www.godeliverix.net/api/Direccion/GetObtenerDireccionUsuario?UidUsuario=" + uidUsuario + "";
            var content = await _WebApiGoDeliverix.GetStringAsync(url);
            var Informacion = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
            VMDireccion MVDireccion = JsonConvert.DeserializeObject<VMDireccion>(Informacion);
            //Asignacion de variables locales
            ListaDireccion = new ObservableCollection<VMAjustesDireccion>();
            foreach (VMDireccion item in MVDireccion.ListaDIRECCIONES)
            {
                ListaDireccion.Add(new VMAjustesDireccion()
                {
                    UidDireccion = item.ID,
                    UidPais = new Guid(item.PAIS),
                    UidEstado = new Guid(item.ESTADO),
                    UidMunicipio = new Guid(item.MUNICIPIO),
                    UidCiudad = new Guid(item.CIUDAD),
                    UidColonia = new Guid(item.COLONIA),
                    CallePrincipal = item.CALLE0,
                    CalleAux1 = item.CALLE1,
                    CalleAux2 = item.CALLE2,
                    Manzana = item.MANZANA,
                    Lote = item.LOTE,
                    CodigoPostal = item.CodigoPostal,
                    Referencia = item.REFERENCIA,
                    Identificador = item.IDENTIFICADOR,
                    NombreColonia = item.NOMBRECOLONIA
                });
            }
        }

        private async void CargaTelefonos(Guid uidUsuario)
        {
            url = "https://www.godeliverix.net/api/Telefono/GetBuscarTelefonos?UidPropietario=" + uidUsuario + "&ParadetroDeBusqueda=Usuario";
            string content = await _WebApiGoDeliverix.GetStringAsync(url);
            var inf = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
            VMTelefono MVTelefono = JsonConvert.DeserializeObject<VMTelefono>(inf);


            LsTelefono = new ObservableCollection<VMAjustesTelefono>();
            foreach (VMTelefono item in MVTelefono.ListaDeTelefonos)
            {
                LsTelefono.Add(new VMAjustesTelefono()
                {
                    UidTelefono = item.ID,
                    intNumeroTelefono = item.NUMERO,
                    StrTipoDeTelefono = item.StrNombreTipoDeTelefono,
                    UidTipoDeTelefono = item.UidTipo
                });
            }
        }

        private async void CargaCorreoElectronico(Guid uidUsuario)
        {
            _WebApiGoDeliverix = new HttpClient();
            string uril = "https://www.godeliverix.net/api/CorreoElectronico/GetBuscarCorreo?UidPropietario=" + uidUsuario + "&strParametroDebusqueda=Usuario";
            var content = await _WebApiGoDeliverix.GetStringAsync(uril);
            var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
            VMCorreoElectronico MVCorreoElectronico = JsonConvert.DeserializeObject<VMCorreoElectronico>(obj);

            StrCorreoElectronico = MVCorreoElectronico.CORREO;
        }

        protected async void GenerateMessage(string Tittle, string Message, string TextOption)
        {
            await Application.Current.MainPage.DisplayAlert(
              Tittle,
              Message,
              TextOption);
        }

    }
}
