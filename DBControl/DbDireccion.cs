using System;
using System.Data;
using System.Data.SqlClient;

namespace DBControl
{
    public class DbDireccion
    {
        Conexion oConexcion;
        #region Metodos
        public void AgregaCiudad(string IdPais, string IdEstado, string IdMunicipio, string Nombre)
        {
            oConexcion = new Conexion();
            SqlCommand CMD = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "asp_AgregaCiudad"
            };

            CMD.Parameters.Add("@IdPais", SqlDbType.UniqueIdentifier);
            CMD.Parameters["@IdPais"].Value = new Guid(IdPais);

            CMD.Parameters.Add("@IdEstado", SqlDbType.UniqueIdentifier);
            CMD.Parameters["@IdEstado"].Value = new Guid(IdEstado);

            CMD.Parameters.Add("@IdMunicipio", SqlDbType.UniqueIdentifier);
            CMD.Parameters["@IdMunicipio"].Value = new Guid(IdMunicipio);

            CMD.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100);
            CMD.Parameters["@Nombre"].Value = Nombre;

            oConexcion.ModificarDatos(CMD);

        }
        public void AgregaColonia(string IdPais, string IdEstado, string IdMunicipio, string IdCiudad, string Nombre)
        {
            Conexion conexion = new Conexion();
            oConexcion = conexion;
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

            oConexcion.ModificarDatos(CMD);
        }
        

        public DataTable obtenerNombreColonia(string UidColonia)
        {
            oConexcion = new Conexion();
            string sentencia = "Select Nombre from colonia where UidColonia ='" + UidColonia + "'";
            return oConexcion.Consultas(sentencia);
        }
        public DataTable obtenerNombreCiudad(string UidCiudad)
        {
            oConexcion = new Conexion();
            string sentencia = "select Nombre from ciudades where UidCiudad ='" + UidCiudad + "'";
            return oConexcion.Consultas(sentencia);
        }

        public DataTable ObtenerDireccionesUsuario(string id)
        {
            oConexcion = new Conexion();
            string sentencia = "SELECT D.UidDireccion,D.UidPais,D.UidEstado,D.UidMunicipio,D.UidCiudad,D.UidColonia,D.Calle0,D.Calle1,D.Calle2,D.Manzana,D.Lote,D.CodigoPostal,D.Referencia, D.Identificador  FROM Direccion D inner join DireccionUsuario DE on DE.UidDireccion = D.UidDireccion where DE.UidUsuario ='" + id + "'";
            return oConexcion.Consultas(sentencia);
        }
        public DataTable ObtenerDireccionesEmpresa(string id)
        {
            oConexcion = new Conexion();
            string sentencia = "SELECT D.UidDireccion,D.UidPais,D.UidEstado,D.UidMunicipio,D.UidCiudad,D.UidColonia,D.Calle0,D.Calle1,D.Calle2,D.Manzana,D.Lote,D.CodigoPostal,D.Referencia, D.Identificador  FROM Direccion D inner join DireccionEmpresa DE on DE.UidDireccion = D.UidDireccion where DE.UidEmpresa ='" + id + "'";
            return oConexcion.Consultas(sentencia);
        }
        public DataTable ObtenerPais()
        {
            oConexcion = new Conexion();
            string sentencia = "(select '00000000-0000-0000-0000-000000000000' as UidPais,'-- Seleccionar --' as Nombre)union all (select UidPais,Nombre from Paises where  Nombre != 'INDEFINIDO' and Nombre != 'INDETERMINADO' ) ";
            return oConexcion.Consultas(sentencia);
        }
        public DataTable ObtenerEstados(Guid Pais,string busqueda = "", string NombreABuscar = "")
        {
            oConexcion = new Conexion();
            string Sentencia = string.Empty;
            if (busqueda =="")
            {
                Sentencia = "(select '00000000-0000-0000-0000-000000000000' as IdEstado, '-- Seleccionar --' as Nombre,-1 as Orden) union all(select UidEstado as IdEstado, Nombre, Orden from estados where UidPais = '" + Pais + "' and Nombre != 'INDEFINIDO' and Nombre != 'INDETERMINADO') order by Nombre ";
            }
            else
            {
                if (string.IsNullOrEmpty(NombreABuscar))
                {
                    Sentencia = "select UidEstado as IdEstado, Nombre, Orden from estados where UidPais = '" + Pais + "' and Nombre != 'INDEFINIDO' and Nombre != 'INDETERMINADO'  order by Nombre ";

                }
                else
                {
                    Sentencia = "select UidEstado as IdEstado, Nombre, Orden from estados where UidPais = '" + Pais + "' and Nombre != 'INDEFINIDO' and Nombre != 'INDETERMINADO' and Nombre like '%" + NombreABuscar + "%' order by Nombre ";

                }
            }
             
            return oConexcion.Consultas(Sentencia);
        }

        public DataTable ObtenerDireccion(Guid uidDireccion)
        {
            oConexcion = new Conexion();
            string query = "select * from Direccion where UidDireccion  = '"+uidDireccion.ToString()+"'";
            return oConexcion.Consultas(query);
        }

        public DataTable ObtenerMunicipio(Guid Estado)
        {
            oConexcion = new Conexion();
            string Sentencia = "(select  '00000000-0000-0000-0000-000000000000' as IdMunicipio, '-- Selecciona --' as Nombre,-1 as IntOrden) union all (Select UidMunicipio as IdMunicipio, Nombre,IntOrden from municipios where UidEstado ='" + Estado + "' and Nombre != 'INDEFINIDO' and Nombre != 'INDETERMINADO'  ) order by Nombre";
            return oConexcion.Consultas(Sentencia);
        }
        public DataTable ObtenerCiudades(Guid Municipio, string Ubicacion = "")
        {
            oConexcion = new Conexion();
            string Sentencia = "(select '00000000-0000-0000-0000-000000000000' as IdCiudad, '-- Seleccionar --' as Nombre)union all(select UidCiudad as IdCiudad, nombre from Ciudades where UidMunicipio = '" + Municipio + "' and Nombre != 'INDEFINIDO' and Nombre != 'INDETERMINADO' ) order by Nombre ";
            return oConexcion.Consultas(Sentencia);
        }
        public DataTable ObtenerColonias(Guid Ciudad, string ubicacion = "", string Nombre = "")
        {
            oConexcion = new Conexion();
            DataTable tabla = new DataTable();
            if (ubicacion == string.Empty)
            {
                string Sentencia = "(select '00000000-0000-0000-0000-000000000000' as IdColonia, '-- Seleccionar --' as Nombre)union all(select UidColonia as IdColonia, Nombre as Nombre from Colonia where UidCiudad = '" + Ciudad + "'and Nombre != 'INDEFINIDO' and Nombre != 'INDETERMINADO') order by Nombre";
                tabla = oConexcion.Consultas(Sentencia);
            }
            if (ubicacion != string.Empty)
            {
                string Sentencia = "select UidColonia as IdColonia, Nombre as Nombre from Colonia where UidCiudad = '" + Ciudad + "'";
                tabla = oConexcion.Consultas(Sentencia);
            }
            if (!string.IsNullOrWhiteSpace(Nombre))
            {
                string Sentencia = "select UidColonia as IdColonia, Nombre as Nombre from Colonia where UidCiudad = '" + Ciudad + "' and Nombre like '%" + Nombre + "%'";
                tabla = oConexcion.Consultas(Sentencia);
            }
            return tabla;
        }
        public DataTable ObtenerCP(Guid Colonia)
        {
            oConexcion = new Conexion();
            string Sentencia = "Select CodigoPostal from Colonia where UidColonia ='" + Colonia + "' ";
            return oConexcion.Consultas(Sentencia);
        }

        public void EliminaDireccionUsuario(Guid id)
        {
            oConexcion = new Conexion();
            string Sentencia = "delete from Direccion  where UidDireccion in (select UidDireccion from DireccionUsuario where UidUsuario ='" + id + "'); delete from DireccionUsuario where UidUsuario ='" + id + "'";
            oConexcion.Consultas(Sentencia);
        }
        public void EliminaDireccionesEmpresa(Guid id)
        {
            oConexcion = new Conexion();
            string Sentencia = "delete from Direccion  where UidDireccion in (select UidDireccion from DireccionEmpresa where UidEmpresa ='" + id + "'); delete from DireccionEmpresa where UidEmpresa ='" + id + "'";
            oConexcion.Consultas(Sentencia);
        }
        /// <summary>
        /// Query que elimina una direccion del usuario
        /// </summary>
        /// <param name="UidDireccion"></param>
        public void EliminaDireccionUsuario(string UidDireccion)
        {
            oConexcion = new Conexion();
            string Sentencia = "delete from Direccion  where UidDireccion  ='" + UidDireccion + "'; delete from DireccionUsuario where UidUsuario ='" + UidDireccion + "'";
            oConexcion.Consultas(Sentencia);
        }
        public DataTable ObtenerDireccionSucursal(string UidSucursal)
        {
            oConexcion = new Conexion();
            string Query = "select * from Direccion where UidDireccion in(select UidDireccion from Sucursales where UidSucursal = '" + UidSucursal + "')";
            return oConexcion.Consultas(Query);
        }

        public void EliminaDireccionSucursal(Guid uidsucursal)
        {
            oConexcion = new Conexion();
            string Query = "delete from direccion where uidDireccion in (select uiddireccion from sucursales where uidsucursal = '" + uidsucursal.ToString() + "')";
            oConexcion.Consultas(Query);
        }

        public DataTable ObtenerCiudad(Guid uidciudad)
        {
            oConexcion = new Conexion();
            string query = "select uidciudad,nombre from Ciudades where uidciudad ='" + uidciudad.ToString() + "'";
            return oConexcion.Consultas(query);
        }

        public void EliminaColoniaDeZonaDeServicio(Guid uidciudad, Guid uidSucursal)
        {
            oConexcion = new Conexion();
            string query = "delete from ZonaDeServicio where uidsucursal = '" + uidSucursal.ToString() + "' and uidcolonia in (select uidcolonia from colonia where uidciudad = '" + uidciudad.ToString() + "')";
            oConexcion.Consultas(query);
        }

        public DataTable ObtenerNombrePais(string uidPais)
        {
            oConexcion = new Conexion();
            string sentencia = "Select Nombre from Paises where UidPais ='" + uidPais + "'";
            return oConexcion.Consultas(sentencia);
        }
        public DataTable ObtenerNombreEstado(string UidEstado)
        {
            oConexcion = new Conexion();
            string sentencia = "Select Nombre from Estados where UidEstado ='" + UidEstado + "'";
            return oConexcion.Consultas(sentencia);
        }
        public DataTable ObtenerNombreMunicipio(string UidMunicipio)
        {
            oConexcion = new Conexion();
            string sentencia = "Select Nombre from Municipios where UidMunicipio ='" + UidMunicipio + "'";
            return oConexcion.Consultas(sentencia);
        }

        #endregion
    }
}
