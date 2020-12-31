using AllSuministradora.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using VistaDelModelo;
using AllSuministradora.vistas.Reportes;

namespace AllSuministradora.model
{
    public class SucursalItem : NotifyBase
    {


        #region Propiedades
        private Guid _UidSucursal;

        public Guid UidSucursal
        {
            get { return _UidSucursal; }
            set { _UidSucursal = value; OnpropertyChanged("UidSucursal"); }
        }

        private string _NombreEmpresa;

        public string NombreEmpresa
        {
            get { return _NombreEmpresa; }
            set { _NombreEmpresa = value; OnpropertyChanged("NombreEmpresa"); }
        }
        private string _NombreSucursal;

        public string NombreSucursal
        {
            get { return _NombreSucursal; }
            set { _NombreSucursal = value; OnpropertyChanged("NombreSucursal"); }
        }
        private string _HorarioSucursal;

        public string HorarioSucursal
        {
            get { return _HorarioSucursal; }
            set { _HorarioSucursal = value; OnpropertyChanged("HorarioSucursal"); }
        }
        private Guid _UidLicencia;

        public Guid Licencia
        {
            get { return _UidLicencia; }
            set { _UidLicencia = value; OnpropertyChanged("Licencia"); }
        }

        private Turno _oTurno;

        public Turno oTurno
        {
            get { return _oTurno; }
            set { _oTurno = value; OnpropertyChanged("oTurno"); }
        }



        #endregion
        #region Controles
        private string _strEstatusTurno;

        public string StrEstatusTurno
        {
            get { return _strEstatusTurno; }
            set { _strEstatusTurno = value; OnpropertyChanged("StrEstatusTurno"); }
        }

        private Brush _cColorTurno;

        public Brush CcolorTurno
        {
            get { return _cColorTurno; }
            set { _cColorTurno = value; OnpropertyChanged("CcolorTurno"); }
        }

        #endregion
        #region Constructor
        public SucursalItem()
        {
            EliminarLicencia = new CommandBase(param => Eliminar());
            CmdAbrirTurno = new CommandBase(param => ControlTurno());

            //oTurno = new Turno();
            //if (!oTurno.EstatusTurno(UidSucursal))
            //{
            //    StrEstatusTurno = "Comenzar";
            //}
            //else
            //{
            //    StrEstatusTurno = "Terminar";
            //}
        }
        #endregion

        #region Comandos

        private ICommand _EliminarLicencia;

        public ICommand EliminarLicencia
        {
            get { return _EliminarLicencia; }
            set { _EliminarLicencia = value; }
        }
        private ICommand _cmdAbrirTurno;

        public ICommand CmdAbrirTurno
        {
            get { return _cmdAbrirTurno; }
            set { _cmdAbrirTurno = value; }
        }
        #endregion
        #region Metodos
        protected void Eliminar()
        {
            var oLicencia = new Licencia();
            var instance = ControlGeneral.GetInstance();
            SucursalItem sucursal = instance.VMSucursalesLocal.ListaDeSucursales.Where(x => x.UidSucursal == UidSucursal).FirstOrDefault();

            if (sucursal.oTurno == null)
            {
                VMLicencia HostingMvLicencia = new VMLicencia();
                HostingMvLicencia.CambiaDisponibilidadDeLicencia(Licencia.ToString());
                if (oLicencia.eliminarLicencia(Licencia.ToString()))
                {
                    instance.VMSucursalesLocal.ObtenSucursales();
                    MessageBox.Show("Licencia eliminada");
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al eliminar la licencia, intente mas tarde");
                }
            }
            else
            {
                MessageBox.Show("Para eliminar la licencia cierra el turno primero de esta sucursal " + NombreSucursal + " e intenta otra vez");
            }
        }

        public void ControlTurno()
        {
            var instance = ControlGeneral.GetInstance();

            if (oTurno == null)
            {
                VMTurno MVTurno = new VMTurno();
                if (!MVTurno.TurnoYaabiertoEnTurnoCallCenter(UidSucursal, instance.Principal.UidUsuario))
                {
                    oTurno = new Turno() { UidUsuario = new Guid(instance.Principal.UidUsuario), UidLicencia = Licencia };
                    oTurno.ControlDeTurno();
                    MVTurno.ConsultarUltimoTurnoSuministradora(Licencia.ToString());

                    MVTurno.RelacionTurnoSuministradoraCallcenter(MVTurno.UidTurno, instance.Principal.oTurno.UidTurno);

                    StrEstatusTurno = "Terminar";
                    CcolorTurno = Brushes.Red;
                }
                else
                {
                    MessageBox.Show("No puedes abrir 2 veces el turno de una sucursal dentro de tu turno");
                }
            }
            else if (oTurno != null)
            {
                VMOrden MVOrden = new VMOrden();
                MVOrden.InformacionDeOrdenesDeTurnoSuministradoraTurnoCallCenter(UidSucursal.ToString(), instance.Principal.oTurno.UidTurno);
                bool PosibilidadDeCerrar = true;
                if (MVOrden.ListaDeOrdenes.Count > 0)
                {
                    foreach (var o in MVOrden.ListaDeOrdenes)
                    {
                        //Estatus Enviado, Cancelado, Entregado
                        if (o.UidEstatus.ToString().ToUpper() == "A2D33D7C-2E2E-4DC6-97E3-73F382F30D93" || o.UidEstatus.ToString().ToUpper() == "B6BFC834-7CC4-4E67-817D-5ECB0EB2FFA7" || o.UidEstatus.ToString().ToUpper() == "2FDEE8E7-0D54-4616-B4C1-037F5A37409D")
                        {
                            PosibilidadDeCerrar = true;
                        }
                        else
                        {
                            PosibilidadDeCerrar = false;
                        }
                    }
                }

                if (PosibilidadDeCerrar)
                {
                    VMTurno MVTUrno = new VMTurno();
                    MVTUrno.InformacionDeCierreDeTurnoSucursalSuministradora("Suministradora", UidLicencia: Licencia.ToString());
                    oTurno = new Turno() { UidUsuario = new Guid(instance.Principal.UidUsuario), UidLicencia = Licencia };
                    oTurno.ControlDeTurno();
                    oTurno = null;
                    StrEstatusTurno = "Comenzar";
                    CcolorTurno = Brushes.Green;
                    vistas.Reportes.ReporteCierreTurnoSucursal obj = new vistas.Reportes.ReporteCierreTurnoSucursal(Licencia.ToString());
                    obj.Show();
                }
                else
                {
                    MessageBox.Show("No puedes cerrar turno si no has completado las ordenes");
                }
            }
            instance.VMSucursalesLocal.ObtenSucursales();
        }
        #endregion
    }
}
