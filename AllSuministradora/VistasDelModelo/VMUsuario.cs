using AllSuministradora.model;
using AllSuministradora.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VistaDelModelo;

namespace AllSuministradora.VistasDelModelo
{
    public class VMUsuario : NotifyBase
    {

        #region Propiedades
        #region Acceso
        private string _test;

        public string StrUsuario
        {
            get { return _test; }
            set { _test = value; OnpropertyChanged("StrUsuario"); }
        }
        private string _StrContrasena;

        public string StrContrasena
        {
            get { return _StrContrasena; }
            set { _StrContrasena = value; OnpropertyChanged("StrContrasena"); }
        }
        #endregion


        #endregion
        private ICommand _Acceder;

        public ICommand Ingresar
        {
            get { return _Acceder; }
            set { _Acceder = value; }
        }


        public VMUsuario()
        {
            Ingresar = new CommandBase(param => Ingresa());
        }

        public void Ingresa()
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
                            VMUsuarios mvusuario = new VMUsuarios();
                            mvusuario.BusquedaDeUsuario(UidUsuario: Uidusuario, UIDPERFIL: new Guid(perfil));
                            var instance = ControlGeneral.GetInstance();
                            instance.Principal.VisibilidadVentanaLogin = false;
                            instance.Principal.VisibilidadCerrarTurno = Visibility.Visible;
                            instance.Principal.VisibilidadInicioTurno = Visibility.Hidden;

                            instance.Principal.StrNombre = mvusuario.StrNombre;
                            instance.Principal.UidUsuario = Uidusuario.ToString();

                            VMTurno MVTurno = new VMTurno();

                            MVTurno.InformacionTurnoCallCenter(Uidusuario);

                            if (MVTurno.DtmHoraFin == DateTime.MinValue && MVTurno.DtmHoraInicio != DateTime.MinValue)
                            {
                                instance.Principal.oTurno = new Turno() { UidTurno = MVTurno.UidTurno, LngFolio = MVTurno.LngFolio, StrHoraInicio = MVTurno.DtmHoraInicio.ToString() };
                            }
                            else
                            {
                                MVTurno.TurnoCallCenter(Uidusuario);
                                MVTurno.InformacionTurnoCallCenter(Uidusuario);
                                instance.Principal.oTurno = new Turno() { UidTurno = MVTurno.UidTurno, LngFolio = MVTurno.LngFolio, StrHoraInicio = MVTurno.DtmHoraInicio.ToString() };
                            }
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
                    MessageBox.Show("LLene todos los campos");
                }
            }
        }
    }
}
