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

namespace AllSuministradora.VistasDelModelo
{
    public class VMLicencias : NotifyBase
    {
        #region Pripoedades
        Licencia oLicencia;

        private string _StrLicencia;

        public string StrLicencia
        {
            get { return _StrLicencia; }
            set { _StrLicencia = value; OnpropertyChanged("StrLicencia"); }
        }

        #endregion

        #region Commandos
        private ICommand _NuevaLicencia;

        public ICommand NuevaLicencia
        {
            get { return _NuevaLicencia; }
            set { _NuevaLicencia = value; }
        }

        private ICommand _AgregarLicencia;

        public ICommand AgregaLicencia
        {
            get { return _AgregarLicencia; }
            set { _AgregarLicencia = value; }
        }
        
        #endregion

        #region Contructor
        public VMLicencias()
        {
            NuevaLicencia = new CommandBase(param => AgregarLicencia());
            AgregaLicencia = new CommandBase(param => GuardarLicencia());
            
        }
        #endregion

        #region Metodos
        protected void AgregarLicencia()
        {
            var instance = ControlGeneral.GetInstance();
            instance.Principal.VisibilidadVentanaLicencia = true;
        }

        
        protected void GuardarLicencia()
        {
            oLicencia = new Licencia();
            VMSucursales MVSucursal = new VMSucursales();
            VMLicencia HostingMvLicencia = new VMLicencia();
            VMLicencias licenciaLocal = new VMLicencias();
            var instance = ControlGeneral.GetInstance();
            string licencia = instance.VMLicencia.StrLicencia.Trim();
            if (string.IsNullOrEmpty(licencia) || licencia.Length < 36)
            {
                MessageBox.Show("Debe ingresar una licencia valida");
            }
            else
            {
                if (MVSucursal.ObtenerElTipoDeSucursal(licencia))
                {
                    if (HostingMvLicencia.VerificaDisponibilidad(licencia))
                    {
                        int resultado = oLicencia.GuardarLicencia(licencia);
                        instance.VMLicencia.StrLicencia = string.Empty;
                        instance.VMSucursalesLocal.ObtenSucursales();
                        switch (resultado)
                        {
                            case 0:
                                HostingMvLicencia.CambiaDisponibilidadDeLicencia(licencia);

                                instance.Principal.VisibilidadVentanaLicencia = false;

                                //instance.MVSucursalesLocal.ObtenSucursales();
                                MessageBox.Show("Sucursal agregada");
                                break;
                            case 1:
                                MessageBox.Show("Esta licencia ya ha sido vinculada");
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Licencia no disponible!!");
                    }
                }
                else
                {
                    MessageBox.Show("La licencia no pertenece a una sucursal suministradora!!");
                }
            }
        }
        #endregion
    }
}
