using System;
using System.Data;
using System.Data.SqlClient;

namespace DBControl
{
    public class DbUsuarios
    {
        #region Propiedades
        Conexion oConexcion;
        #endregion

        #region Metodos

        #region Catalogos de direccion



        public void EliminaTelefono(Guid id)
        {
            string sentencia = "delete from Telefono where UidTelefono in (select UidTelefono from TelefonoUsuario where UidUsuario ='" + id + "'); delete from TelefonoUsuario where UidUsuario ='" + id + "'";
            oConexcion.Consultas(sentencia);
        }

        #endregion


        public DataTable ObtenerTelefonos(string id)
        {
            oConexcion = new Conexion();
            string sentencia = "select UidTelefono,UidTipoDeTelefono,Numero from Telefono where UidTelefono in (select UidTelefono from TelefonoUsuario where UidUsuario = '" + id + "')";
            return oConexcion.Consultas(sentencia);
        }
        public DataTable Perfiles(string perfil)
        {
            oConexcion = new Conexion();
            string sentencia = "Select UidPerfil, Nombre from perfiles where UidPerfil = '" + perfil + "'";
            return oConexcion.Consultas(sentencia);
        }
        public DataTable ObtenerUsuario(string ID)
        {
            oConexcion = new Conexion();
            string sentencia = "SELECT U.UidUsuario as UidUsuario, U.Nombre as Nombre,U.ApellidoPaterno as ApellidoPaterno,U.ApellidoMaterno as ApellidoMaterno, U.Usuario as Usuario,U.Contrasena as Contrasena,U.FechaDeNacimiento as FechaDeNacimiento,U.UidPerfil as UidPerfil,U.ESTATUS as ESTATUS,U.UidEmpresa as UidEmpresa FROM Usuarios U  where U.UidUsuario ='" + ID + "'";
            return oConexcion.Consultas(sentencia);
        }
        public DataTable ObtenerUsuarioSimpleBusquedaNormal(string perfil, string UidEmpresa = "")
        {
            oConexcion = new Conexion();
            string sentencia = string.Empty;
            switch (perfil)
            {
                //Administrador de empresas
                case "76a96ff6-e720-4092-a217-a77a58a9bf0d":
                    sentencia = " SELECT U.UidUsuario as UidUsuario, U.Nombre as Nombre,U.ApellidoPaterno as ApellidoPaterno,U.ApellidoMaterno as ApellidoMaterno, U.Usuario as Usuario,"
                               + " P.Nombre as Perfil,Es.Nombre as Estatus, E.NombreComercial as Empresa  FROM Usuarios U"
                               + " inner join Estatus Es on Es.IdEstatus = U.ESTATUS"
                               + " inner join Perfiles P on P.UidPerfil = U.UidPerfil"
                               + " inner join Empresa E on E.UidEmpresa = U.UidEmpresa where u.UidPerfil = '76a96ff6-e720-4092-a217-a77a58a9bf0d' ";
                    break;
                //Supervisor de sucursal
                case "81232596-4c6b-4568-9005-8d4a0a382fda":
                    sentencia = " SELECT U.UidUsuario as UidUsuario, S.Identificador, U.Nombre as Nombre,U.ApellidoPaterno as ApellidoPaterno,U.ApellidoMaterno as ApellidoMaterno,P.Nombre as Perfil,Es.Nombre as Estatus FROM Usuarios U  "
                               + " inner join Estatus Es on Es.IdEstatus = U.ESTATUS"
                               + " inner join SucursalSupervisor SS on SS.UidUsuario = U.UidUsuario"
                               + " inner join Perfiles P on P.UidPerfil = U.UidPerfil"
                               + " inner join Sucursales S on S.UidSucursal = SS.UidSucursal where u.UidPerfil = '81232596-4c6b-4568-9005-8d4a0a382fda' and S.UidEmpresa ='" + UidEmpresa + "'";
                    break;
                case "DFC29662-0259-4F6F-90EA-B24E39BE4346":
                    sentencia = " SELECT U.UidUsuario as UidUsuario, S.Identificador, U.Nombre as Nombre,U.ApellidoPaterno as ApellidoPaterno,U.ApellidoMaterno as ApellidoMaterno,P.Nombre as Perfil,Es.Nombre as Estatus FROM Usuarios U  "
                               + " inner join Estatus Es on Es.IdEstatus = U.ESTATUS"
                               + " inner join UsuarioSucursal SS on SS.UidUsuario = U.UidUsuario"
                               + " inner join Perfiles P on P.UidPerfil = U.UidPerfil"
                               + " inner join Sucursales S on S.UidSucursal = SS.UidSucursal where u.UidPerfil = 'DFC29662-0259-4F6F-90EA-B24E39BE4346' and S.UidEmpresa ='" + UidEmpresa + "'";
                    break;
                default:
                    break;
            }

            return oConexcion.Consultas(sentencia);
        }
        public DataTable ObtenerUsuarioSimpleBusquedaAvanzada(string perfil)
        {
            oConexcion = new Conexion();
            string sentencia = string.Empty;
            switch (perfil)
            {
                //Administrador de empresas
                case "76a96ff6-e720-4092-a217-a77a58a9bf0d":
                    sentencia = " SELECT U.UidUsuario as UidUsuario, U.Nombre as Nombre,U.ApellidoPaterno as ApellidoPaterno,U.ApellidoMaterno as ApellidoMaterno, U.Usuario as Usuario,U.Contrasena as Contrasena,"
                  + " U.FechaDeNacimiento as FechaDeNacimiento,U.UidPerfil as UidPerfil,U.ESTATUS as ESTATUS,U.UidEmpresa as UidEmpresa,CU.UidCorreo as IdCorreo, C.Correo as Correo FROM Usuarios U"
                  + " inner join DireccionUsuario DU on DU.UidUsuario = U.UidUsuario"
                  + " inner join Direccion D on D.UidDireccion = DU.UidDireccion"
                  + " inner join Paises P on p.UidPais = D.UidPais"
                  + " inner join estados est on est.UidEstado = d.UidEstado"
                  + " inner join Municipios Mun on Mun.UidMunicipio = d.UidMunicipio"
                  + " inner join Ciudades City on City.UidCiudad = d.UidCiudad"
                  + " inner join Colonia Col on Col.UidColonia = D.UidColonia"
                  + " inner join CorreoUsuario CU on CU.UidUsuario = U.UidUsuario"
                  + " inner join CorreoElectronico C on C.IdCorreo = CU.UidCorreo where u.UidPerfil = '76a96ff6-e720-4092-a217-a77a58a9bf0d'";
                    break;
                //Supervisor de sucursal
                case "81232596-4c6b-4568-9005-8d4a0a382fda":
                    sentencia = " SELECT U.UidUsuario as UidUsuario, S.Identificador, U.Nombre as Nombre,U.ApellidoPaterno as ApellidoPaterno,U.ApellidoMaterno as ApellidoMaterno,P.Nombre as Perfil,Es.Nombre as Estatus FROM Usuarios U  "
                               + " inner join Estatus Es on Es.IdEstatus = U.ESTATUS"
                               + " inner join SucursalSupervisor SS on SS.UidUsuario = U.UidUsuario"
                               + " inner join Perfiles P on P.UidPerfil = U.UidPerfil"
                               + " inner join Sucursales S on S.UidSucursal = SS.UidSucursal where u.UidPerfil = '81232596-4c6b-4568-9005-8d4a0a382fda'";
                    break;
                default:
                    break;
            }

            return oConexcion.Consultas(sentencia);
        }
        public DataTable ValidarUsuario(string usuario)
        {
            oConexcion = new Conexion();
            string sentencia = "select usuario from usuarios where usuario = '" + usuario + "'"; ;
            return oConexcion.Consultas(sentencia);
        }
        public DataTable RecuperarSupervisor(Guid uidUsuario)
        {
            oConexcion = new Conexion();
            string query = "select u.UidUsuario, u.Usuario, s.Identificador,e.nombrecomercial from usuarios u  inner join SucursalSupervisor SS on SS.UidUsuario = u.UidUsuario inner join Sucursales s on s.UidSucursal = ss.UidSucursal inner join empresa e on e.uidempresa = s.uidempresa where u.UidUsuario ='" + uidUsuario + "'";
            return oConexcion.Consultas(query);
        }
        public DataTable ValidarCorreoDeEmpresa(string id)
        {
            oConexcion = new Conexion();
            string sentencia = "select C.Correo from CorreoElectronico C inner join CorreoEmpresa CE on CE.IdCorreo = C.IdCorreo where C.Correo = '" + id + "' ";
            return oConexcion.Consultas(sentencia);
        }
        public DataTable VAlidarCorreoElectronicoUsuario(string correo)
        {
            oConexcion = new Conexion();
            string sentencia = "select correo from correoelectronico where correo = '" + correo + "'";
            return oConexcion.Consultas(sentencia);
        }
        public DataTable ObtenerEstatus()
        {
            oConexcion = new Conexion();
            string Sentencia = "select IdEstatus,Nombre from estatus order by Nombre";
            return oConexcion.Consultas(Sentencia);
        }
        public DataTable Busquedas(SqlCommand CMD)
        {
            oConexcion = new Conexion();
            return oConexcion.Busquedas(CMD);
        }
        public DataTable ObtenerEmpresa(string id)
        {
            oConexcion = new Conexion();
            string sentencia = "select UidEmpresa,NombreComercial,Razonsocial,Rfc from empresa where UidEmpresa = '" + id + "'";
            return oConexcion.Consultas(sentencia);
        }
        public DataTable obtenerUidEmpresa(string id)
        {
            oConexcion = new Conexion();
            string sentencia = "select UidEmpresa from EmpresaUsuario where UidUsuario = '" + id + "'";
            return oConexcion.Consultas(sentencia);
        }

        public void EliminaRelacionSucursal(Guid iDUsuario)
        {
            oConexcion = new Conexion();
            string query = "Delete from UsuarioSucursal where UidUsuario = '" + iDUsuario.ToString() + "'";
            oConexcion.Consultas(query);
        }

        public DataTable ObtenRepartidores(string licencia)
        {
            oConexcion = new Conexion();
            string query = "select * from Usuarios where UidUsuario in (select UidUsuario from UsuarioSucursal where UidSucursal in ( select UidSucursal from SucursalLicencia where UidLicencia = '" + licencia + "'))";
            return oConexcion.Consultas(query);
        }

        public DataTable ObtenerRepartidoresConVehiculosYTurnoAbierto(string licencia)
        {
            oConexcion = new Conexion();
            string query = "select u.UidUsuario,u.Nombre,u.usuario,dbo.asp_ObtenerUtimoEstatus(u.UidUsuario) as estatus, tr.DtmHoraFin, tr.UidTurnoRepartidor from Usuarios u " +
                " inner join TurnoRepartidor tr on tr.UidUsuario = u.UidUsuario and  tr.DtmHoraFin is null  where u.UidUsuario in (select UidUsuario from VehiculoUsuario where UidVehiculo in (select UidVehiculo from vehiculoSucursal where uidsucursal in ( select UidSucursal from SucursalLicencia where UidLicencia = '" + licencia + "'))) order by tr.DtmHoraInicio desc";
            return oConexcion.Consultas(query);
        }

        public DataTable ValidaExistenciaDeAdministrador(string licencia, Guid uidusuario)
        {
            oConexcion = new Conexion();
            string query = "  select Nombre from Usuarios where UidUsuario in (select UidUsuario From EmpresaUsuario EU inner join Sucursales s on EU.UidEmpresa = s.UidEmpresa inner join SucursalLicencia sl on sl.UidSucursal = s.UidSucursal where sl.UidLicencia ='" + licencia + "') and UidUsuario ='" + uidusuario.ToString() + "'";
            return oConexcion.Consultas(query);
        }

        public DataTable ValidarexistenciaDeUsuario(string strNombreDeUsuario)
        {
            oConexcion = new Conexion();
            string query = "  select * from Usuarios where usuario == " + strNombreDeUsuario + "";
            return oConexcion.Consultas(query);
        }
        #endregion
    }
}
