using AllSuministradora.model;
using AllSuministradora.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VistaDelModelo;
using LibPrintTicket;
using TestLib;

namespace AllSuministradora.VistasDelModelo
{
    public class VMPantallaPrincipal : NotifyBase
    {



        #region Propiedades
        #region Atributos
        private string _UidUsuario;

        public string UidUsuario
        {
            get { return _UidUsuario; }
            set { _UidUsuario = value; OnpropertyChanged("UidUsuario"); }
        }
        private string _Strnombre;

        public string StrNombre
        {
            get { return _Strnombre; }
            set { _Strnombre = value; OnpropertyChanged("StrNombre"); }
        }

        #endregion

        #region Propiedades de controles
        private bool _Visibilidad;

        public bool VisibilidadVentanaLogin
        {
            get { return _Visibilidad; }
            set { _Visibilidad = value; OnpropertyChanged("VisibilidadVentanaLogin"); }
        }
        private bool _VisibilidadVentanaLicencia;

        public bool VisibilidadVentanaLicencia
        {
            get { return _VisibilidadVentanaLicencia; }
            set { _VisibilidadVentanaLicencia = value; OnpropertyChanged("VisibilidadVentanaLicencia"); }
        }

        private bool _VisibilidadVentanaCancelar;

        public bool VisibilidadVentanaCancelar
        {
            get { return _VisibilidadVentanaCancelar; }
            set { _VisibilidadVentanaCancelar = value; OnpropertyChanged("VisibilidadVentanaCancelar"); }
        }

        private Visibility _VisibilidadInicioTurno;

        public Visibility VisibilidadInicioTurno
        {
            get { return _VisibilidadInicioTurno; }
            set { _VisibilidadInicioTurno = value; OnpropertyChanged("VisibilidadInicioTurno"); }
        }
        private Visibility _VisibilidadCerrarTurno;

        public Visibility VisibilidadCerrarTurno
        {
            get { return _VisibilidadCerrarTurno; }
            set { _VisibilidadCerrarTurno = value; OnpropertyChanged("VisibilidadCerrarTurno"); }
        }
        private Orden _oOrdenCancelada;

        public Orden oOrdenCancelada
        {
            get { return _oOrdenCancelada; }
            set { _oOrdenCancelada = value; OnpropertyChanged("oOrdenCancelada"); }
        }
        private Turno _oTurno;

        public Turno oTurno
        {
            get { return _oTurno; }
            set { _oTurno = value; OnpropertyChanged("oTurno"); }
        }

        #endregion
        #endregion


        #region Comandos
        private ICommand _Comando;

        public ICommand AbrirLogin
        {
            get { return _Comando; }
            set { _Comando = value; }
        }

        private ICommand _cmdCerrarTurno;

        public ICommand CmdCerrarTurno
        {
            get { return _cmdCerrarTurno; }
            set { _cmdCerrarTurno = value; }
        }



        #endregion

        #region Contructor
        public VMPantallaPrincipal()
        {
            VisibilidadVentanaLogin = false;
            VisibilidadVentanaLicencia = false;
            VisibilidadVentanaCancelar = false;
            VisibilidadCerrarTurno = Visibility.Hidden;
            VisibilidadInicioTurno = Visibility.Visible;
            AbrirLogin = new CommandBase(param => AbrirVentanaLogin());
            CmdCerrarTurno = new CommandBase(param => CerrarTurno());
        }
        #endregion

        #region Metodos
        protected void CerrarTurno()
        {
            var instance = ControlGeneral.GetInstance();
            bool respuesta = false;
            Turno turno = new Turno();
            foreach (var item in instance.VMSucursalesLocal.ListaDeSucursales)
            {
                if (turno.EstatusTurno(item.Licencia, UidSucursal: item.UidSucursal))
                {
                    respuesta = true;
                }
            }
            if (!respuesta)
            {
                Ticket2 t = new Ticket2();

                VMUsuarios MVusuario = new VMUsuarios();
                MVusuario.BusquedaDeUsuario(new Guid(instance.Principal.UidUsuario));
                VMTurno MVTurno = new VMTurno();
                MVTurno.TurnoCallCenter(new Guid(instance.Principal.UidUsuario));
                MVTurno.InformacionTurnoCallCenter(new Guid(instance.Principal.UidUsuario));

                t.AddHeaderLine("========Informacion del turno======");
                t.AddHeaderLine("Usuario: " + instance.Principal.StrNombre + "");
                t.AddHeaderLine(" Folio: " + instance.Principal.oTurno.LngFolio + "");
                t.AddHeaderLine("Inicio: " + instance.Principal.oTurno.StrHoraInicio + "");
                t.AddHeaderLine("   Fin: " + MVTurno.DtmHoraFin.ToString() + "");
                t.AddHeaderLine("===================================");
                t.AddHeaderLine("====Información de sucursales======");
                t.AddHeaderLine("===================================");
                int cantidadDeOrdenes = 0;
                decimal Total = 0;
                int OrdenesTerminadas = 0;
                int OrdenesCanceladas = 0;
                //Informacion de la empresa
                foreach (var item in instance.VMSucursalesLocal.ListaDeSucursales)
                {
                    //Informacion de ordenes
                    VMOrden MVOrden = new VMOrden();
                    MVOrden.InformacionDeOrdenesDeTurnoSuministradoraTurnoCallCenter(item.UidSucursal.ToString(), MVTurno.UidTurno);

                    t.AddHeaderLine("          " + item.NombreEmpresa + "");
                    t.AddHeaderLine("Sucursal  " + item.NombreSucursal + "");
                    //Datos de turno sucursal
                    MVTurno.ConsultarTurnoSuministradoraDesdeCallCenter(item.Licencia.ToString(), MVTurno.UidTurno);
                    //MVTurno.ConsultarUltimoTurnoSuministradora(item.Licencia.ToString());
                    t.AddHeaderLine("Folio:" + MVTurno.LngFolio + "");
                    t.AddHeaderLine("Inicio:" + MVTurno.DtmHoraInicio + "");
                    t.AddHeaderLine("   Fin:" + MVTurno.DtmHoraFin + "");

                    cantidadDeOrdenes = cantidadDeOrdenes + MVOrden.ListaDeOrdenes.Count;
                    int ordenesucursalescanceladas = 0;
                    decimal TotalSucursal = 0;
                    decimal OrdenesTerminadasSucursal = 0;

                    foreach (var o in MVOrden.ListaDeOrdenes)
                    {
                        switch (o.UidEstatus.ToString().ToUpper())
                        {
                            //Ordenes concluidas
                            case "E2BAD7D9-9CD0-4698-959D-0A211800545F":
                                OrdenesTerminadas += 1;
                                OrdenesTerminadasSucursal += 1;
                                TotalSucursal += o.MTotal;
                                Total = Total + o.MTotal;
                                break;
                            //Orden cancelada
                            case "EAE7A7E6-3F19-405E-87A9-3162D36CE21B":
                                OrdenesCanceladas += 1;
                                ordenesucursalescanceladas += 1;
                                break;
                        }
                    }
                    t.AddHeaderLine("     Total de ordenes: " + MVOrden.ListaDeOrdenes.Count + "");
                    t.AddHeaderLine("          Completadas: " + OrdenesTerminadasSucursal.ToString() + "");
                    t.AddHeaderLine("           Canceladas: " + ordenesucursalescanceladas.ToString() + "");
                    t.AddHeaderLine("                 Caja: " + TotalSucursal.ToString("N2") + "");
                    t.AddHeaderLine("===================================");
                }
                t.AddHeaderLine("=====Información de General========");
                t.AddHeaderLine("===================================");
                t.AddHeaderLine("         Total de ordenes:  " + cantidadDeOrdenes.ToString() + "");
                t.AddHeaderLine("      Ordenes completadas:  " + OrdenesTerminadas.ToString() + "");
                t.AddHeaderLine("       Ordenes canceladas:  " + OrdenesCanceladas.ToString() + "");
                t.AddHeaderLine("           Dinero en caja:  " + Total.ToString("N2") + "");
                //Informacion del turno

                //Obtiene la bitacora de las liquidaciones del turno
                t.FontSize = 6;
                t.AddHeaderLine("                            ");
                t.AddHeaderLine("      www.godeliverix.com.mx");
                t.PrintTicket("PDFCreator");




                instance.Principal.VisibilidadCerrarTurno = Visibility.Hidden;
                instance.Principal.VisibilidadInicioTurno = Visibility.Visible;
                instance.Principal.UidUsuario = null;
                instance.Principal.StrNombre = string.Empty;
                instance.Principal.oTurno = null;
            }
        }
        protected void AbrirVentanaLogin()
        {
            var instance = ControlGeneral.GetInstance();
            instance.Usuario.StrContrasena = string.Empty;
            instance.Usuario.StrUsuario = string.Empty;
            VisibilidadVentanaLogin = true;
        }
        #endregion

    }
}
