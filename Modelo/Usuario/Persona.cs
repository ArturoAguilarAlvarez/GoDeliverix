using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Modelo.Usuario
{
    public class Persona
    {
        #region Propiedades
        private Guid _uidPersona;
        public Guid IDPERSONA
        {
            get { return _uidPersona; }
            set { _uidPersona = value; }
        }
        private string _primerNombre;
        public string PRIMERNOMBRE
        {
            get { return _primerNombre; }
            set { _primerNombre = value; }
        }

        private string _segundoNombre;
        public string SEGUNDONOMBRE
        {
            get { return _segundoNombre; }
            set { _segundoNombre = value; }
        }
        private string _primerApellido;
        public string PRIMERAPELLIDO
        {
            get { return _primerApellido; }
            set { _primerApellido = value; }
        }

        private string _segundoApellido;
        public string SEGUNDOAPELLIDO
        {
            get { return _segundoApellido; }
            set { _segundoApellido = value; }
        }

        private int _edad;
        public int EDAD
        {
            get { return _edad; }
            set { _edad = value; }
        }
        private string _usuario;
        public string USUARIO
        {
            get { return _usuario; }
            set { _usuario = value; }
        }
        private string _email;
        public string EMAIL
        {
            get { return _email; }
            set { _email = value; }
        }
        private string _contraseña;
        public string PASSWORD
        {
            get { return _contraseña; }
            set { _contraseña = value; }
        }
        private DateTime _fechaDeNacimiento;
        public DateTime FECHADENACIMIENTO
        {
            get { return _fechaDeNacimiento; }
            set { _fechaDeNacimiento = value; }
        }
        #endregion

        #region Constructores
        public Persona(Guid Id, string PrimerNombre, string SegundoNombre, string PrimerApellido, string SegundoApellido, int Edad, string Usuario, string Email, string Password, string FechaDeNacimiento)
        {
            IDPERSONA = Id;
            PRIMERNOMBRE = PrimerNombre;
            SEGUNDONOMBRE = SegundoNombre;
            PRIMERAPELLIDO = PrimerApellido;
            SEGUNDOAPELLIDO = SegundoApellido;
            EDAD = Edad;
            USUARIO = Usuario;
            EMAIL = Email;
            PASSWORD = Password;
            FECHADENACIMIENTO = Convert.ToDateTime(FechaDeNacimiento);
        }
        #endregion
    }
}
