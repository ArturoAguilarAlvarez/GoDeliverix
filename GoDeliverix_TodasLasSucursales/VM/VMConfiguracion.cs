using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using VistaDelModelo;
namespace GoDeliverix_TodasLasSucursales.VM
{
   
    public class VMConfiguracion : NotifyBase
    {
        private string _StrUsuario;

        public string StrUsuario
        {
            get { return _StrUsuario; }
            set { _StrUsuario = value; OnPropertyChanged("StrUsuario"); }
        }

        private string _StrContrasena;

        public string StrContrasena
        {
            get { return _StrContrasena; }
            set { _StrContrasena = value; OnPropertyChanged("StrContrasena"); }
        }

        private ICommand _Ingresar;

        public ICommand IngresarCMD
        {
            get { return _Ingresar; }
            set { _Ingresar = value; }
        }

        private ICommand _cmdVerDialog;

        public ICommand CDMAbrirDilog
        {
            get { return _cmdVerDialog; }
            set { _cmdVerDialog = value; }
        }

        private ICommand _CMDCerrarDialog;

        public ICommand CMDCerrarDIalog
        {
            get { return _CMDCerrarDialog; }
            set { _CMDCerrarDialog = value; }
        }


        protected void AbrirDialog()
        {
            Visibility = true;
        }
        protected void CerrarDialog() { Visibility = false; }



        public VMConfiguracion()
        {
            StrUsuario = "Manu";
            StrContrasena = "54321";
            IngresarCMD = new CommandBase(param => this.Ingresar());
            CDMAbrirDilog = new CommandBase(param => this.AbrirDialog());
            CMDCerrarDIalog = new CommandBase(param => this.CerrarDialog());
            Visibility = false;
        }

        #region Variables de controles
        private bool _IsVisible;

        public bool Visibility
        {
            get { return _IsVisible; }
            set { _IsVisible = value; OnPropertyChanged("_IsVisible"); }
        }
        private SolidColorBrush _ColorRequiredUser;

        public SolidColorBrush ColorRequiredUser
        {
            get { return _ColorRequiredUser; }
            set { _ColorRequiredUser = value; OnPropertyChanged("_ColorRequiredUser"); }
        }
        private SolidColorBrush _ColorRequiredPassword;

        public SolidColorBrush ColorRequiredPassword
        {
            get { return _ColorRequiredPassword; }
            set { _ColorRequiredPassword = value; OnPropertyChanged("_ColorRequiredPassword"); }
        }

        #endregion
        private void Ingresar()
        {
            if (string.IsNullOrEmpty(StrUsuario))
            {
                MessageBox.Show("Usuario vacio");
            }
            else if (string.IsNullOrEmpty(StrContrasena))
            {
                MessageBox.Show("Contraseña vacia");
            }
            if (!string.IsNullOrEmpty(StrUsuario) && !string.IsNullOrEmpty(StrContrasena))
            {
                VMAcceso MVAcceso = new VMAcceso();
                if (!string.IsNullOrWhiteSpace(StrUsuario) && !string.IsNullOrWhiteSpace(StrContrasena))
                {
                    Guid Uidusuario = MVAcceso.Ingresar(StrUsuario, StrContrasena);
                    if (Uidusuario != Guid.Empty)
                    {
                        string perfil = MVAcceso.PerfilDeUsuario(Uidusuario.ToString());

                        //Dios Maya
                        if (perfil.ToUpper() == "8D2E2925-A2A7-421F-A72B-56F2E8296D77")
                        {
                            MessageBox.Show("Hola Dios Maya");
                            Visibility = false;
                        }
                        else
                        {
                            MessageBox.Show("Solo el dios maya puede usar esta applicación");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Acceso incorrecto");
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(StrUsuario))
                    {
                        ColorRequiredUser = Brushes.Red;
                    }
                    if (string.IsNullOrWhiteSpace(StrContrasena))
                    {
                        ColorRequiredPassword = Brushes.Red;
                    }
                }
            }
        }
    }
}
