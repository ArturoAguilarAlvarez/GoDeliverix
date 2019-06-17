using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace DBControl
{
    public class DbEmpresas
    {
        #region Propiedades

        Conexion oConexion;
        #endregion


        #region Metodos


        public void EliminaDireccion(Guid id)
        {
            oConexion = new Conexion();
            string Sentencia = "delete from Direccion  where UidDireccion in (select UidDireccion from DireccionEmpresa where UidEmpresa ='" + id + "'); delete from DireccionEmpresa where UidEmpresa ='" + id + "'";
            oConexion.Consultas(Sentencia);
        }
        public void EliminaTelefono(Guid id)
        {
            oConexion = new Conexion();
            string sentencia = "delete from Telefono where UidTelefono in (select IdTelefono from TelefonoEmpresa where IdEmpresa ='" + id + "'); delete from TelefonoEmpresa where IdEmpresa ='" + id + "'";
            oConexion.Consultas(sentencia);
        }
        public DataTable ObtenerDirecciones(string id)
        {
            oConexion = new Conexion();
            string sentencia = "SELECT D.UidDireccion,D.UidPais,D.UidEstado,D.UidMunicipio,D.UidCiudad,D.UidColonia,D.Calle0,D.Calle1,D.Calle2,D.Manzana,D.Lote,D.CodigoPostal,D.Referencia  FROM Direccion D inner join DireccionEmpresa DE on DE.UidDireccion = D.UidDireccion where DE.UidEmpresa ='" + id + "'";
            return oConexion.Consultas(sentencia);
        }
        public DataTable ObtenerTelefonos(string id)
        {
            oConexion = new Conexion();
            string sentencia = "select UidTelefono,UidTipoDeTelefono,Numero from Telefono where UidTelefono in (select IdTelefono from TelefonoEmpresa where IdEmpresa = '" + id + "')";
            return oConexion.Consultas(sentencia);
        }
        public DataTable ObtenerTipoDeEmpresa()
        {
            oConexion = new Conexion();
            string Sentencia = "select IdTipoDeEmpresa,Nombre from TipoDeEmpresa";
            return oConexion.Consultas(Sentencia);
        }
        public DataTable ObtenerEmpresasFiltros()
        {
            oConexion = new Conexion();
            string Sentencia = "(select 0 as IdTipoDeEmpresa, '--Seleccionar--' as Nombre) union all (select IdTipoDeEmpresa,Nombre from TipoDeEmpresa)";
            return oConexion.Consultas(Sentencia);
        }
        public DataTable ObtenerTipoDeTelefono()
        {
            oConexion = new Conexion();
            string Sentencia = "select UidTipoDeTelefono,Nombre from TipoDeTelefono";
            return oConexion.Consultas(Sentencia);
        }
        public DataTable ObtenerEstatus()
        {
            oConexion = new Conexion();
            string Sentencia = "select IdEstatus,Nombre from estatus order by Nombre";
            return oConexion.Consultas(Sentencia);
        }



        public DataTable ObtenerNombreComercial(Guid IdUsuario = new Guid(), string IdEmpresa = "")
        {
            string Query = "";
            oConexion = new Conexion();
            if (IdUsuario != Guid.Empty)
            {
                Query = "Select UidEmpresa,NombreComercial from Empresa where UidEmpresa in (select UidEmpresa from EmpresaUsuario where UidUsuario ='" + IdUsuario + "')";
            }
            if (IdEmpresa != "" && IdEmpresa != string.Empty)
            {
                Query = "Select UidEmpresa, NombreComercial from Empresa where UidEmpresa = '" + IdEmpresa + "'";
            }
            return oConexion.Consultas(Query);
        }

        public DataTable ObtenerEmpresa(string ID)
        {
            oConexion = new Conexion();
            string Sentencia = "select e.UidEmpresa,e.IdEstatus,e.IdTipoDeEmpresa,e.NombreComercial,e.RazonSocial,e.RFC," +
                                " C.IdCorreo, c.Correo"
                                 + " from empresa e"
                                 + " inner join CorreoEmpresa CE on CE.IdEmpresa = e.UidEMpresa"
                                 + " inner join CorreoElectronico C on c.IdCorreo = CE.IdCorreo where e.UidEMpresa = '" + ID + "'";

            return oConexion.Consultas(Sentencia);
        }

        public DataTable ObtenerTotalDeRegistros()
        {
            oConexion = new Conexion();
            string sentencia = "select count(*) as total from empresa";
            return oConexion.Consultas(sentencia);
        }

        public DataTable Busquedas(SqlCommand CMD)
        {
            oConexion = new Conexion();
            return oConexion.Busquedas(CMD);
        }
        public DataTable ValidarRFC(string RFC)
        {
            oConexion = new Conexion();
            string sentencia = "select Rfc from empresa where rfc ='" + RFC + "'"; ;
            return oConexion.Consultas(sentencia);
        }

        public DataTable VAlidarCorreoElectronico(string correo)
        {
            oConexion = new Conexion();
            string sentencia = "select correo from correoelectronico where correo = '" + correo + "'";
            return oConexion.Consultas(sentencia);
        }

        public DataTable VerificaEmpresaActiva(string uidEmpresa)
        {
            oConexion = new Conexion();
            string query = "select * from empresa where uidEmpresa = '" + uidEmpresa + "'  and idEstatus =1";
            return oConexion.Consultas(query);
        }

        public DataTable ObtenElTipoDeEmpresa(string uidEmpresa)
        {
            oConexion = new Conexion();
            string query = "select * from Empresa where UidEmpresa = '" + uidEmpresa + "' and IdTipoDeEmpresa = 1";
            return oConexion.Consultas(query);
        }

        public DataTable ObtenerUidEmpresaUsuario(string UidUsuario)
        {
            oConexion = new Conexion();
            string query = "select UidEmpresa from Usuarios where uidusuario = '" + UidUsuario + "'";
            return oConexion.Consultas(query);
        }


        #endregion

    }
}
