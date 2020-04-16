using System;
using System.Data;
using System.Data.SqlClient;

namespace DBControl
{
    public class DbSucursal
    {
        #region Propiedades

        Conexion oConexion;

        #endregion

        #region constructores
        public DbSucursal()
        {

        }
        #endregion

        #region Metodos

        public DataTable Busquedas(SqlCommand CMD)
        {
            oConexion = new Conexion();
            return oConexion.Busquedas(CMD);
        }
        public DataTable ObtenerDireccion(string id)
        {
            oConexion = new Conexion();
            string sentencia = "select * from Direccion where UidDireccion = '" + id + "'";
            return oConexion.Consultas(sentencia);
        }
        public DataTable ObtenerSucursalBusquedaNormal(string uidempresa)
        {
            oConexion = new Conexion();
            string sentencia = "select * from sucursales where UidEmpresa ='" + uidempresa + "'";
            return oConexion.Consultas(sentencia);
        }
        public DataTable ObtenerSucursalBusquedaAmpliada(string uidempresa)
        {
            oConexion = new Conexion();
            string sentencia = "select * from sucursales where UidEmpresa ='" + uidempresa + "'";
            return oConexion.Consultas(sentencia);
        }
        public DataTable ObtenerTipoDeTelefono()
        {
            oConexion = new Conexion();
            string Sentencia = "select UidTipoDeTelefono,Nombre from TipoDeTelefono";
            return oConexion.Consultas(Sentencia);
        }
        public DataTable ObtenerSucursal(string id)
        {
            oConexion = new Conexion();
            string sentencia = "select * from sucursales where UidSucursal = '" + id + "'";
            return oConexion.Consultas(sentencia);
        }
        public DataTable ObtenerUidDeEmpresa(string id)
        {
            oConexion = new Conexion();
            string sentencia = "select UidEmpresa from EmpresaUsuario where Uidusuario = '" + id + "'";
            return oConexion.Consultas(sentencia);
        }
        public DataTable ObtenUidDeEmpresa(string UidSucursal)
        {
            oConexion = new Conexion();
            string query = "select UidEmpresa from Sucursales where UidSucursal = '" + UidSucursal + "'";
            return oConexion.Consultas(query);
        }
        public void AgregaCiudad(string IdPais, string IdEstado, string IdMunicipio, string Nombre)
        {
            oConexion = new Conexion();
            SqlCommand CMD = new SqlCommand();

            CMD.CommandType = CommandType.StoredProcedure;
            CMD.CommandText = "asp_AgregaCiudad";

            CMD.Parameters.Add("@IdPais", SqlDbType.UniqueIdentifier);
            CMD.Parameters["@IdPais"].Value = new Guid(IdPais);

            CMD.Parameters.Add("@IdEstado", SqlDbType.UniqueIdentifier);
            CMD.Parameters["@IdEstado"].Value = new Guid(IdEstado);

            CMD.Parameters.Add("@IdMunicipio", SqlDbType.UniqueIdentifier);
            CMD.Parameters["@IdMunicipio"].Value = new Guid(IdMunicipio);

            CMD.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100);
            CMD.Parameters["@Nombre"].Value = Nombre;

            oConexion.ModificarDatos(CMD);

        }
        public void AgregaColonia(string IdPais, string IdEstado, string IdMunicipio, string IdCiudad, string Nombre)
        {
            oConexion = new Conexion();
            SqlCommand CMD = new SqlCommand();

            CMD.CommandType = CommandType.StoredProcedure;
            CMD.CommandText = "asp_AgregaColonia";

            CMD.Parameters.Add("@IdPais", SqlDbType.UniqueIdentifier);
            CMD.Parameters["@IdPais"].Value = new Guid(IdPais);

            CMD.Parameters.Add("@IdEstado", SqlDbType.UniqueIdentifier);
            CMD.Parameters["@IdEstado"].Value = new Guid(IdEstado);

            CMD.Parameters.Add("@IdMunicipio", SqlDbType.UniqueIdentifier);
            CMD.Parameters["@IdMunicipio"].Value = new Guid(IdMunicipio);

            CMD.Parameters.Add("@IdCiudad", SqlDbType.UniqueIdentifier);
            CMD.Parameters["@IdCiudad"].Value = new Guid(IdCiudad);

            CMD.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100);
            CMD.Parameters["@Nombre"].Value = Nombre;

            oConexion.ModificarDatos(CMD);
        }
        public void EliminaDireccion(Guid id)
        {
            oConexion = new Conexion();
            string Sentencia = "delete from Direccion  where UidDireccion in (select UidDireccion from sucursales where UidSucursal ='" + id + "')";
            oConexion.Consultas(Sentencia);
        }
        public DataTable ObtenerUidSucursal(string uidusuario)
        {
            oConexion = new Conexion();
            string sentencia = "Select UidSucursal from SucursalSupervisor where UidUsuario = '" + uidusuario + "'";
            return oConexion.Consultas(sentencia);
        }
        public DataTable ObtenerGiro(string UidSucursal)
        {
            oConexion = new Conexion();
            string Query = "Select * from GiroSucursal where UidSucursal = '" + UidSucursal + "'";
            return oConexion.Consultas(Query);
        }
        public void EliminaGiro(string idSucursal)
        {
            oConexion = new Conexion();
            oConexion = new Conexion();
            string Query = "delete from GiroSucursal where UidSucursal = '" + idSucursal + "'";
            oConexion.Consultas(Query);
        }
        public void EliminaCategoria(string idSucursal)
        {
            oConexion = new Conexion();
            string Query = "delete from CategoriaSucursal where UidSucursal = '" + idSucursal + "'";
            oConexion.Consultas(Query);
        }
        public void EliminaSubcategoria(string idSucursal)
        {
            oConexion = new Conexion();
            string Query = "delete from SubcategoriaSucursal where UidSucursal = '" + idSucursal + "'";
            oConexion.Consultas(Query);
        }
        public DataTable ObtenerCategoria(string uidSucursal)
        {
            oConexion = new Conexion();
            string Query = "Select * from CategoriaSucursal where UidSucursal = '" + uidSucursal + "'";
            return oConexion.Consultas(Query);
        }
        public DataTable ObtenerSubcategoria(string uidSucursal)
        {
            oConexion = new Conexion();
            string Query = "Select * from SubcategoriaSucursal where UidSucursal = '" + uidSucursal + "'";
            return oConexion.Consultas(Query);
        }
        public DataTable obtenerProductos(string UIDSUCURSAL, string UIDSECCION)
        {
            oConexion = new Conexion();
            string Query = "select p.UidProducto from Productos p inner join SeccionProducto sp on sp.UidProducto = p.UidProducto inner join Seccion s on s.UidSeccion = sp.UidSeccion inner join Oferta o on o.UidOferta = s.UidOferta where o.Uidsucursal = '" + UIDSUCURSAL + "' and s.UidSeccion = '" + UIDSECCION + "' and p.intEstatus = 1";
            return oConexion.Consultas(Query);
        }
        public void EliminaZonaDeServicio(Guid uidsucursal)
        {
            oConexion = new Conexion();
            string query = "delete from ZonaDeServicio where uidsucursal = '" + uidsucursal.ToString() + "'";
            oConexion.Consultas(query);

            query = "delete from ZonaDeRecoleccion where uidsucursal = '" + uidsucursal.ToString() + "'";
            oConexion.Consultas(query);
        }
        public DataTable RecuperaZonaEntrega(Guid uidsucursal)
        {
            oConexion = new Conexion();
            string query = "select Z.UidRelacionZonaServicio,c.Nombre,Z.uidcolonia,c.uidciudad from ZonaDeServicio Z inner join colonia c on c.uidcolonia = Z.uidcolonia  where uidsucursal = '" + uidsucursal.ToString() + "'";
            return oConexion.Consultas(query);
        }
        public DataTable RecuperaZonaRecoleccion(Guid uidsucursal)
        {
            oConexion = new Conexion();
            string query = "select Z.UidZonaDeRecolecta, Z.uidcolonia,c.Nombre,c.uidciudad from ZonaDeRecoleccion Z inner join colonia c on c.uidcolonia = Z.uidcolonia  where uidsucursal = '" + uidsucursal.ToString() + "'";
            return oConexion.Consultas(query);
        }
        public DataTable obtenerSucursalesGiro(string uidGiro, string UidColonia, string tiempoactual, string Nombre = "")
        {
            oConexion = new Conexion();
            string Query = "";

            if (string.IsNullOrWhiteSpace(Nombre))
            {
                Query = "select S.UidSucursal,S.HorarioApertura,S.HorarioCierre,S.Identificador, E.NombreComercial, I.NVchRuta, d.Manzana,d.Lote,d.CodigoPostal,ci.Nombre as ciudad,co.Nombre as colonia,d.Calle0 from Sucursales S inner join GiroSucursal GS on GS.UidSucursal = S.UidSucursal inner join Giro G on GS.UidGiro = G.UidGiro inner join Empresa E on E.UidEmpresa = S.UidEmpresa inner join Direccion d on d.UidDireccion = s.UidDireccion inner join Ciudades ci on ci.UidCiudad = d.UidCiudad inner join Colonia co on co.UidColonia = d.UidColonia inner join ImagenEmpresa IE on IE.UidEmpresa = E.UidEmpresa inner join Imagenes I on I.UIdImagen = IE.UidImagen inner join ZonaDeServicio ZD on ZD.UidSucursal = s.UidSucursal  where G.UidGiro = '" + uidGiro + "' and zd.UidColonia = '" + UidColonia + "' and '" + tiempoactual + "' between s.HorarioApertura and s.HorarioCierre";

            }
            else
            {
                Query = "select S.UidSucursal,S.HorarioApertura,S.HorarioCierre,S.Identificador, E.NombreComercial, I.NVchRuta, d.Manzana,d.Lote,d.CodigoPostal,ci.Nombre as ciudad,co.Nombre as colonia,d.Calle0 from Sucursales S inner join GiroSucursal GS on GS.UidSucursal = S.UidSucursal inner join Giro G on GS.UidGiro = G.UidGiro inner join Empresa E on E.UidEmpresa = S.UidEmpresa inner join ImagenEmpresa IE on IE.UidEmpresa = E.UidEmpresa inner join Direccion d on d.UidDireccion = s.UidDireccion inner join Ciudades ci on ci.UidCiudad = d.UidCiudad inner join Colonia co on co.UidColonia = d.UidColonia inner join Imagenes I on I.UIdImagen = IE.UidImagen inner join ZonaDeServicio ZD on ZD.UidSucursal = s.UidSucursal  where G.UidGiro = '" + uidGiro + "' and zd.UidColonia = '" + UidColonia + "' and '" + tiempoactual + "' between s.HorarioApertura and s.HorarioCierre and s.Identificador like '%" + Nombre + "%'";

            }
            return oConexion.Consultas(Query);
        }
        public DataTable obtenerSucursalesCategoria(string uidCategoria, string UidColonia, string tiempoactual, string Nombre = "")
        {
            oConexion = new Conexion();
            string query = "";
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                query = "select S.UidSucursal,S.HorarioApertura,S.HorarioCierre,S.Identificador, E.NombreComercial, I.NVchRuta, d.Manzana,d.Lote,d.CodigoPostal,ci.Nombre as ciudad,co.Nombre as colonia,d.Calle0 from Sucursales S inner join CategoriaSucursal CS on CS.UidSucursal = S.UidSucursal inner join Categorias C on C.UidCategoria = CS.UidCategoria inner join Empresa E on E.UidEmpresa = S.UidEmpresa inner join ImagenEmpresa IE on IE.UidEmpresa = E.UidEmpresa inner join Direccion d on d.UidDireccion = s.UidDireccion inner join Ciudades ci on ci.UidCiudad = d.UidCiudad inner join Colonia co on co.UidColonia = d.UidColonia inner join Imagenes I on I.UIdImagen = IE.UidImagen inner join ZonaDeServicio ZD on ZD.UidSucursal = s.UidSucursal  where C.UidCategoria = '" + uidCategoria + "' and zd.UidColonia = '" + UidColonia + "' and '" + tiempoactual + "' between s.HorarioApertura and s.HorarioCierre";

            }
            else
            {
                query = "select S.UidSucursal,S.HorarioApertura,S.HorarioCierre,S.Identificador, E.NombreComercial, I.NVchRuta, d.Manzana,d.Lote,d.CodigoPostal,ci.Nombre as ciudad,co.Nombre as colonia,d.Calle0 from Sucursales S inner join CategoriaSucursal CS on CS.UidSucursal = S.UidSucursal inner join Categorias C on C.UidCategoria = CS.UidCategoria inner join Empresa E on E.UidEmpresa = S.UidEmpresa inner join ImagenEmpresa IE on IE.UidEmpresa = E.UidEmpresa inner join Direccion d on d.UidDireccion = s.UidDireccion inner join Ciudades ci on ci.UidCiudad = d.UidCiudad inner join Colonia co on co.UidColonia = d.UidColonia inner join Imagenes I on I.UIdImagen = IE.UidImagen inner join ZonaDeServicio ZD on ZD.UidSucursal = s.UidSucursal  where C.UidCategoria = '" + uidCategoria + "' and zd.UidColonia = '" + UidColonia + "' and '" + tiempoactual + "' between s.HorarioApertura and s.HorarioCierre and s.Identificador like '%" + Nombre + "%'";

            }
            return oConexion.Consultas(query);
        }
        public DataTable obtenerSucursalesSubcategoria(string uidsubcategoria, string UidColonia, string tiempoactual, string Nombre = "")
        {
            oConexion = new Conexion();
            string query = "";
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                query = "select S.UidSucursal,S.HorarioApertura,S.HorarioCierre,S.Identificador, E.NombreComercial, I.NVchRuta, d.Manzana,d.Lote,d.CodigoPostal,ci.Nombre as ciudad,co.Nombre as colonia,d.Calle0 from Sucursales S inner join SubcategoriaSucursal SP on SP.UidSucursal = S.UidSucursal inner join Subcategoria Su on Su.UidSubcategoria = SP.UidSubcategoria inner join Empresa E on E.UidEmpresa = S.UidEmpresa inner join ImagenEmpresa IE on IE.UidEmpresa = E.UidEmpresa inner join Direccion d on d.UidDireccion = s.UidDireccion inner join Ciudades ci on ci.UidCiudad = d.UidCiudad inner join Colonia co on co.UidColonia = d.UidColonia inner join Imagenes I on I.UIdImagen = IE.UidImagen  join ZonaDeServicio ZD on ZD.UidSucursal = s.UidSucursal  where Su.UidSubcategoria = '" + uidsubcategoria + "' and zd.UidColonia = '" + UidColonia + "' and '" + tiempoactual + "' between s.HorarioApertura and s.HorarioCierre ";

            }
            else
            {
                query = "select S.UidSucursal,S.HorarioApertura,S.HorarioCierre,S.Identificador, E.NombreComercial, I.NVchRuta, d.Manzana,d.Lote,d.CodigoPostal,ci.Nombre as ciudad,co.Nombre as colonia,d.Calle0 from Sucursales S inner join SubcategoriaSucursal SP on SP.UidSucursal = S.UidSucursal inner join Subcategoria Su on Su.UidSubcategoria = SP.UidSubcategoria inner join Empresa E on E.UidEmpresa = S.UidEmpresa inner join ImagenEmpresa IE on IE.UidEmpresa = E.UidEmpresa inner join Direccion d on d.UidDireccion = s.UidDireccion inner join Ciudades ci on ci.UidCiudad = d.UidCiudad inner join Colonia co on co.UidColonia = d.UidColonia inner join Imagenes I on I.UIdImagen = IE.UidImagen  join ZonaDeServicio ZD on ZD.UidSucursal = s.UidSucursal  where Su.UidSubcategoria = '" + uidsubcategoria + "' and zd.UidColonia = '" + UidColonia + "' and '" + tiempoactual + "' between s.HorarioApertura and s.HorarioCierre and s.Identificador like '%" + Nombre + "%'";

            }
            return oConexion.Consultas(query);
        }

        public DataTable ObtenerUidSucursalVehiculo(Guid valor)
        {
            oConexion = new Conexion();
            string query = "select UidSucursal from VehiculoSucursal where UidVehiculo = '" + valor + "'";
            return oConexion.Consultas(query);
        }

        public DataTable VerificaSucursalActiva(string uidSucursal)
        {
            oConexion = new Conexion();
            string Query = "select * from sucursales where IntEstatus = 1 and UidSucursal = '" + uidSucursal + "' ";
            return oConexion.Consultas(Query);
        }

        public DataTable ObtenerSucursalAPartirDeLicencia(object licencia)
        {
            oConexion = new Conexion();
            string query = "select UidSucursal from sucursallicencia where uidlicencia = '" + licencia + "'";
            return oConexion.Consultas(query);
        }

        public DataTable verificaExistenciaUsuario(string uidUsuario, string UidSucursal)
        {
            oConexion = new Conexion();
            string query = "select * from sucursalsupervisor where UIdUsuario = '" + uidUsuario + "' and UidSucursal ='" + UidSucursal + "'";
            return oConexion.Consultas(query);
        }

        public DataTable ObtenerDistribuidorasEnContrato(string uidLicencia)
        {
            oConexion = new Conexion();
            string query = "select s.UidSucursal,s.Identificador from Sucursales s where UidSucursal in (select cs.UidSucursalDistribuidora from ContratoDeServicio cs where UidSucursalSuministradora in (select UidSucursal from SucursalLicencia where UidLicencia = '" + uidLicencia + "')) ";
            return oConexion.Consultas(query);
        }

        public DataTable ObtenerUidSucursalRepartidor(string uidusuario)
        {
            oConexion = new Conexion();
            string query = "select UidSucursal from UsuarioSucursal where uidUsuario = '" + uidusuario + "'";
            return oConexion.Consultas(query);
        }

        public DataTable ObtenerElTipoDeEmpresa(string uidLicencia)
        {
            oConexion = new Conexion();
            string query = "select * from Empresa e where e.UidEmpresa in (select UidEmpresa from Sucursales where UidSucursal in (select UidSucursal from SucursalLicencia where UidLicencia = '" + uidLicencia + "')) and e.IdTipoDeEmpresa = 1";
            return oConexion.Consultas(query);
        }

        public void EliminarRegistroRepartidoresVehiculo(Guid idRegistro)
        {
            oConexion = new Conexion();
            string query = "delete from VehiculoUsuario where UidRelacionVehiculoUsuario = '" + idRegistro.ToString() + "'";
            oConexion.Consultas(query);
        }

        public DataTable ObtenerRepartidoresYVehiculos(string uidLicencia)
        {
            oConexion = new Conexion();
            string query = "select vu.UidRelacionVehiculoUsuario,vu.mfondo, u.uidusuario,v.uidvehiculo,u.Nombre,u.Usuario,v.VchModelo from VehiculoUsuario vu inner join Usuarios u on u.UidUsuario = vu.UidUsuario inner join Vehiculo v on v.UidVehiculo = vu.UidVehiculo  where vu.UidVehiculo in  (select UidVehiculo from Vehiculo where UidEmpresa in (Select UidEmpresa From Sucursales where Uidsucursal in (Select UidSucursal from SucursalLicencia where UidLicencia = '" + uidLicencia + "')))";
            return oConexion.Consultas(query);
        }

        public DataTable ObtenerOrdenesAsignadasARepartir(string uidlicencia)
        {
            oConexion = new Conexion();
            string Query = " select distinct orp.UidRelacionOrdenRepartidor,ot.UidRelacionOrdenTarifario,dbo.asp_ObtenerUltimoEstatusOrdenRepartidor(orp.UidRelacionOrdenRepartidor) as EstatusRepartidor, orp.UidTurnoRepartidor, orp.UidOrden, u.Usuario,u.Nombre, o.IntFolio,s.Identificador,os.MTotalSucursal, convert(varchar,orp.dtmFechaAsignacion, 7) as DtmFecha from OrdenRepartidor orp  inner join OrdenTarifario ot on ot.UidRelacionOrdenTarifario = orp.UidOrden inner join OrdenSucursal os on os.UidRelacionOrdenSucursal = ot.UidOrden inner join Ordenes o on o.UidOrden = os.UidOrden inner join TurnoRepartidor tr on tr.UidTurnoRepartidor = orp.UidTurnoRepartidor inner join Sucursales s on s.UidSucursal = os.UidSucursal  inner join Usuarios u on u.UidUsuario = tr.UidUsuario inner join Tarifario t on t.UidRegistroTarifario = ot.UidTarifario inner  join ZonaDeRecoleccion ZDR on ZDR.UidZonaDeRecolecta = t.UidRelacionZonaRecolecta inner join ContratoDeServicio CDS on  CDS.UidSucursalSuministradora = s.UidSucursal and CDS.UidSucursalDistribuidora = ZDR.UidSucursal where CDS.UidSucursalDistribuidora in  (select UidSucursal from SucursalLicencia where UidLicencia = '" + uidlicencia + "')";
            return oConexion.Consultas(Query);
        }

        public DataTable ObtenerSucursalSupervisor(Guid uid)
        {
            oConexion = new Conexion();
            string query = "select * from sucursales where uidsucursal in (select uidSucursal from sucursalsupervisor where uidusuario = '" + uid.ToString() + "')";
            return oConexion.Consultas(query);
        }

        public DataTable ObtenerSucursalRepartidor(Guid uid)
        {
            oConexion = new Conexion();
            string query = "select * from sucursales where uidsucursal in (select uidSucursal from UsuarioSucursal where uidusuario = '" + uid.ToString() + "')";
            return oConexion.Consultas(query);
        }

        public DataTable obtenerContratosSucursal(int tipodeEmpresa, Guid UidSucursal)
        {
            oConexion = new Conexion();
            string query = string.Empty;
            switch (tipodeEmpresa)
            {
                case 2:
                    query = "select * from ContratoDeServicio where UidSucursalDistribuidora = '" + UidSucursal + "'";
                    break;
                case 1:
                    query = "select * from ContratoDeServicio where UidSucursalSuministradora = '" + UidSucursal + "'";
                    break;
                default:
                    break;
            }
            return oConexion.Consultas(query);
        }
        #endregion
    }
}
