using System;
using System.Data;


namespace DBControl
{
    public class DBProducto
    {
        Conexion oConexion;

        public DataTable ProductoConimagen(string UidEmpresa)
        {
            oConexion = new Conexion();
            string Query = "select P.UidProducto, p.VchNombre, p.VchDescripcion,  i.NVchRuta from Productos p inner join ImagenProducto IP on IP.UidProducto = p.UidProducto inner join Imagenes I on I.UIdImagen = IP.UidImagen where p.UidEmpresa = '" + UidEmpresa + "' and p.intEstatus = 1";
            return oConexion.Consultas(Query);
        }

        public DataTable ProductoConImagen(string uidProducto)
        {
            oConexion = new Conexion();
            string Query = "select P.UidProducto, p.VchNombre, p.VchDescripcion,  i.NVchRuta from Productos p inner join ImagenProducto IP on IP.UidProducto = p.UidProducto inner join Imagenes I on I.UIdImagen = IP.UidImagen where  p.intEstatus = 1 and P.UidProducto = '" + uidProducto + "'";
            return oConexion.Consultas(Query);
        }

        public DataTable ObtenerGiro(string UidProducto)
        {
            oConexion = new Conexion();
            string query = "select UidGiro from GiroProducto where UidProducto = '" + UidProducto + "'";
            return oConexion.Consultas(query);
        }

        public DataTable ObtenerCategoria(string UidProducto)
        {
            oConexion = new Conexion();
            string Query = "select UidCategoria from CategoriaProducto where UidProducto = '" + UidProducto + "'";
            return oConexion.Consultas(Query);
        }

        public DataTable ObtenerSubcategoria(string UidProducto)
        {
            oConexion = new Conexion();
            string query = "select UidSubcategoria from subcategoriaproducto where uidproducto = '" + UidProducto + "'";
            return oConexion.Consultas(query);
        }
        public void EliminaGiro(string UidProducto)
        {
            oConexion = new Conexion();
            string Query = "delete from GiroProducto where UidProducto = '" + UidProducto + "'";
            oConexion.Consultas(Query);
        }

        public void EliminaCategoria(string UidProducto)
        {
            oConexion = new Conexion();
            string Query = "delete from CategoriaProducto where UidProducto = '" + UidProducto + "'";
            oConexion.Consultas(Query);
        }

        public void EliminaSubcategoria(string UidProducto)
        {
            oConexion = new Conexion();
            string Query = "delete from SubcategoriaProducto where UidProducto = '" + UidProducto + "'";
            oConexion.Consultas(Query);
        }

        public DataTable ProductosDeSucursal(Guid valor)
        {
            oConexion = new Conexion();
            string Query = "select p.UidProducto,p.VchNombre, i.NVchRuta from Productos p " +
                            "inner join ImagenProducto ip on ip.UidProducto = p.UidProducto " +
                            "inner join Imagenes i on i.UIdImagen = ip.UidImagen " +
                            " where p.UidProducto in (select Uidproducto from SubcategoriaProducto where UidSubcategoria in " +
                            " (select UidSubcategoria from SubcategoriaSucursal where UidSucursal = '" + valor.ToString() + "'))";
            return oConexion.Consultas(Query);
        }

        public DataTable Informacion(string uidProducto, string UidSucursal)
        {
            oConexion = new Conexion();
            string Query = "select sp.mCosto, sp.uidproducto,sp.VchTiempoElaboracion from SeccionProducto sp" +
                " inner join Seccion s on s.UidSeccion = sp.UidSeccion" +
                " inner join Oferta o on o.UidOferta = s.UidOferta" +
                " where sp.uidProducto = '" + uidProducto + "' and o.Uidsucursal = '" + UidSucursal + "'";
            return oConexion.Consultas(Query);
        }
        /// <summary>
        /// Obtiene los productos con filtro de busqueda por giro
        /// </summary>
        /// <param name="giro"></param>
        /// <param name="UidColonia"></param>
        /// <param name="tiempoActual"></param>
        /// <param name="dia"></param>
        /// <param name="Nombre"></param>
        /// <returns></returns>
        public DataTable ObtenProductoConGiro(string giro, string UidColonia, string tiempoActual, string dia, string Nombre = "")
        {
            oConexion = new Conexion();
            string Query = string.Empty;

            if (string.IsNullOrEmpty(Nombre))
            {
                Query = "select distinct p.uidproducto, i.NVchRuta, s.Identificador,se.UidSeccion, s.UidSucursal , p.VchNombre, p.VchDescripcion, sp.VchTiempoElaboracion,sp.Mcosto, e.NombreComercial" +
                    " from Productos p inner join ImagenProducto ip on ip.UidProducto = p.UidProducto inner join Imagenes i on ip.UidImagen = i.UIdImagen inner join GiroProducto gp on " +
                    "gp.UidProducto = p.UidProducto " +
                    "inner join Giro g on g.UidGiro = gp.UidGiro " +
                    "inner join SeccionProducto sp on sp.UidProducto = p.UidProducto " +
                    "inner join Seccion se on se.UidSeccion = sp.UidSeccion " +
                    "inner join Oferta O on O.UidOferta = se.UidOferta " +
                    "inner join DiaOferta DO on DO.UidOferta = O.UidOferta " +
                    "inner join Dias D on D.UidDia = DO.UidDia " +
                    "inner join Sucursales s on s.UidSucursal = o.Uidsucursal " +
                    "inner join ContratoDeServicio CDS on CDS.UidSucursalSuministradora = s.UidSucursal " +
                    "inner join ZonaDeRepartoDeContrato ZDRC on ZDRC.UidContrato = CDS.UidContrato " +
                    "inner join Tarifario t on t.UidRegistroTarifario = ZDRC.UidTarifario " +
                    "inner join ZonaDeServicio zd on zd.UidColonia = '" + UidColonia + "' and zd.UidRelacionZonaServicio = t.UidRelacionZonaEntrega " +
                    "inner join Empresa e on e.UidEmpresa = p.UidEmpresa where g.UidGiro = '" + giro + "' and '" + tiempoActual + "' " +
                    "between se.VchHoraInicio and se.VchHoraFin and  '" + tiempoActual + "'   between s.HorarioApertura and s.HorarioCierre  and zd.UidColonia = '" + UidColonia + "' and p.IntEstatus = 1 and D.VchNombre = '" + dia + "' " +
                    "and O.IntEstatus = 1 and se.IntEstatus = 1 and sp.VchTiempoElaboracion is not null and CDS.UidEstatusContrato = 'CD20F9BF-EBA2-4128-88FB-647544457B2D'";
            }
            else
            {
                Query = "select distinct p.uidproducto,i.NVchRuta, s.Identificador,se.UidSeccion, s.UidSucursal , p.VchNombre, p.VchDescripcion, sp.VchTiempoElaboracion,sp.Mcosto, e.NombreComercial " +
                    "from Productos p inner join ImagenProducto ip on ip.UidProducto = p.UidProducto " +
                    "inner join Imagenes i on ip.UidImagen = i.UIdImagen " +
                    "inner join GiroProducto gp on gp.UidProducto = p.UidProducto " +
                    "inner join Giro g on g.UidGiro = gp.UidGiro " +
                    "inner join SeccionProducto sp on sp.UidProducto = p.UidProducto " +
                    "inner join Seccion se on se.UidSeccion = sp.UidSeccion " +
                    "inner join Oferta O on O.UidOferta = se.UidOferta " +
                    "inner join DiaOferta DO on DO.UidOferta = O.UidOferta " +
                    "inner join Dias D on D.UidDia = DO.UidDia " +
                    "inner join Sucursales s on s.UidSucursal = o.Uidsucursal " +
                    "inner join ContratoDeServicio CDS on CDS.UidSucursalSuministradora = s.UidSucursal " +
                    "inner join ZonaDeRepartoDeContrato ZDRC on ZDRC.UidContrato = CDS.UidContrato " +
                    "inner join Tarifario t on t.UidRegistroTarifario = ZDRC.UidTarifario " +
                    "inner join ZonaDeServicio zd on zd.UidColonia = '" + UidColonia + "' and zd.UidRelacionZonaServicio = t.UidRelacionZonaEntrega " +
                    "inner join Empresa e on e.UidEmpresa = p.UidEmpresa where g.UidGiro = '" + giro + "' and '" + tiempoActual + "' " +
                    "between se.VchHoraInicio and se.VchHoraFin and '" + tiempoActual + "'  between s.HorarioApertura and s.HorarioCierre  and zd.UidColonia = '" + UidColonia + "' and p.IntEstatus = 1 and D.VchNombre = '" + dia + "' " +
                    "and O.IntEstatus = 1 and se.IntEstatus = 1 and p.VchNombre like '%" + Nombre + "%' and sp.VchTiempoElaboracion is not null and CDS.UidEstatusContrato = 'CD20F9BF-EBA2-4128-88FB-647544457B2D'";
            }

            return oConexion.Consultas(Query);
        }
        /// <summary>
        /// Obtiene los productos con filtro de busqueda por categoria
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="UidColonia"></param>
        /// <param name="tiempoActual"></param>
        /// <param name="dia"></param>
        /// <param name="Nombre"></param>
        /// <returns></returns>
        public DataTable ObtenerProductosConCategoria(string categoria, string UidColonia, string tiempoActual, string dia, string Nombre = "")
        {
            oConexion = new Conexion();
            string Query = string.Empty;

            if (string.IsNullOrEmpty(Nombre))
            {
                Query = "select distinct p.uidproducto,i.NVchRuta, s.Identificador,se.UidSeccion, s.UidSucursal , p.VchNombre, p.VchDescripcion, sp.VchTiempoElaboracion,sp.Mcosto, " +
                   "e.NombreComercial from Productos p " +
                   "inner join ImagenProducto ip on ip.UidProducto = p.UidProducto " +
                   "inner join Imagenes i on ip.UidImagen = i.UIdImagen " +
                   "inner join CategoriaProducto cp on cp.UidProducto = p.UidProducto " +
                   "inner join Categorias c on c.UidCategoria = cp.UidCategoria inner " +
                   "join SeccionProducto sp on sp.UidProducto = p.UidProducto inner " +
                   "join Seccion se on se.UidSeccion = sp.UidSeccion " +
                   "inner join Oferta O on O.UidOferta = se.UidOferta " +
                   "inner join DiaOferta DO on DO.UidOferta = O.UidOferta " +
                   "inner join Dias D on D.UidDia = DO.UidDia " +
                   "inner join Sucursales s on s.UidSucursal = o.Uidsucursal " +
                   "inner join ContratoDeServicio CDS on CDS.UidSucursalSuministradora = s.UidSucursal " +
                   "inner join ZonaDeRepartoDeContrato ZDRC on ZDRC.UidContrato = CDS.UidContrato " +
                   "inner join Tarifario t on t.UidRegistroTarifario = ZDRC.UidTarifario " +
                   "inner join ZonaDeServicio zd on zd.UidColonia = '" + UidColonia + "' and zd.UidRelacionZonaServicio = t.UidRelacionZonaEntrega " +
                   "inner join Empresa e on e.UidEmpresa = p.UidEmpresa where  c.UidCategoria = '" + categoria + "' and '" + tiempoActual + "' " +
                   "between se.VchHoraInicio and se.VchHoraFin and  '" + tiempoActual + "'   between s.HorarioApertura and s.HorarioCierre  and zd.UidColonia = '" + UidColonia + "' and p.IntEstatus = 1  " +
                   "and D.VchNombre = '" + dia + "' and O.IntEstatus = 1 and se.IntEstatus = 1 and sp.VchTiempoElaboracion is not null and CDS.UidEstatusContrato = 'CD20F9BF-EBA2-4128-88FB-647544457B2D'";
            }
            else
            {
                Query = "select distinct p.uidproducto,i.NVchRuta, s.Identificador,se.UidSeccion, s.UidSucursal , p.VchNombre, p.VchDescripcion, sp.VchTiempoElaboracion,sp.Mcosto, " +
                    "e.NombreComercial from Productos p " +
                    "inner join ImagenProducto ip on ip.UidProducto = p.UidProducto " +
                    "inner join Imagenes i on ip.UidImagen = i.UIdImagen " +
                    "inner join CategoriaProducto cp on cp.UidProducto = p.UidProducto " +
                    "inner join Categorias c on c.UidCategoria = cp.UidCategoria " +
                    "inner join SeccionProducto sp on sp.UidProducto = p.UidProducto " +
                    "inner join Seccion se on se.UidSeccion = sp.UidSeccion " +
                    "inner join Oferta O on O.UidOferta = se.UidOferta " +
                    "inner join DiaOferta DO on DO.UidOferta = O.UidOferta " +
                    "inner join Dias D on D.UidDia = DO.UidDia " +
                    "inner join Sucursales s on s.UidSucursal = o.Uidsucursal " +
                    "inner join ContratoDeServicio CDS on CDS.UidSucursalSuministradora = s.UidSucursal " +
                    "inner join ZonaDeRepartoDeContrato ZDRC on ZDRC.UidContrato = CDS.UidContrato " +
                    "inner join Tarifario t on t.UidRegistroTarifario = ZDRC.UidTarifario " +
                    "inner join ZonaDeServicio zd on zd.UidColonia = '" + UidColonia + "' and zd.UidRelacionZonaServicio = t.UidRelacionZonaEntrega " +
                    "inner join Empresa e on e.UidEmpresa = p.UidEmpresa where  c.UidCategoria = '" + categoria + "' and '" + tiempoActual + "' " +
                    "between se.VchHoraInicio and se.VchHoraFin and  '" + tiempoActual + "'   between s.HorarioApertura and s.HorarioCierre  and zd.UidColonia = '" + UidColonia + "' and p.IntEstatus = 1  and D.VchNombre = '" + dia + "' " +
                    "and O.IntEstatus = 1 and se.IntEstatus = 1 and p.VchNombre like '%" + Nombre + "%' and sp.VchTiempoElaboracion is not null and CDS.UidEstatusContrato = 'CD20F9BF-EBA2-4128-88FB-647544457B2D'";

            }
            return oConexion.Consultas(Query);
        }
        /// <summary>
        /// Obtiene los productos con filtro de  busqueda por subcategoria
        /// </summary>
        /// <param name="Subcategoria"></param>
        /// <param name="UidColonia"></param>
        /// <param name="tiempoActual"></param>
        /// <param name="dia"></param>
        /// <param name="Nombre"></param>
        /// <returns></returns>
        public DataTable ObtenerProductosConSubcategoria(string Subcategoria, string UidColonia, string tiempoActual, string dia, string Nombre = "")
        {
            oConexion = new Conexion();
            string Query = string.Empty;

            if (string.IsNullOrEmpty(Nombre))
            {
                Query = "select distinct p.uidproducto,i.NVchRuta, s.Identificador,se.UidSeccion, s.UidSucursal , p.VchNombre, p.VchDescripcion, sp.VchTiempoElaboracion,sp.Mcosto, " +
                   "e.NombreComercial from Productos p " +
                   "inner join ImagenProducto ip on ip.UidProducto = p.UidProducto " +
                   "inner join Imagenes i on ip.UidImagen = i.UIdImagen " +
                   "inner join SubcategoriaProducto scp on scp.UidProducto = p.UidProducto " +
                   "inner join Subcategoria su on su.UidSubcategoria = scp.UidSubcategoria " +
                   "inner join SeccionProducto sp on sp.UidProducto = p.UidProducto " +
                   "inner join Seccion se on se.UidSeccion = sp.UidSeccion " +
                   "inner join Oferta O on O.UidOferta = se.UidOferta " +
                   "inner join DiaOferta DO on DO.UidOferta = O.UidOferta " +
                   "inner join Dias D on D.UidDia = DO.UidDia " +
                   "inner join Sucursales s on s.UidSucursal = o.Uidsucursal " +
                   "inner join ContratoDeServicio CDS on CDS.UidSucursalSuministradora = s.UidSucursal " +
                   "inner join ZonaDeRepartoDeContrato ZDRC on ZDRC.UidContrato = CDS.UidContrato " +
                   "inner join Tarifario t on t.UidRegistroTarifario = ZDRC.UidTarifario " +
                   "inner join ZonaDeServicio zd on zd.UidColonia =  '" + UidColonia + "' and zd.UidRelacionZonaServicio = t.UidRelacionZonaEntrega " +
                   "inner join Empresa e on e.UidEmpresa = p.UidEmpresa where  su.UidSubcategoria = '" + Subcategoria + "' and '" + tiempoActual + "' " +
                   "between se.VchHoraInicio and se.VchHoraFin and  '" + tiempoActual + "'  between s.HorarioApertura and s.HorarioCierre  and zd.UidColonia = '" + UidColonia + "' and p.IntEstatus = 1  and D.VchNombre = '" + dia + "' " +
                   "and O.IntEstatus = 1 and se.IntEstatus = 1 and sp.VchTiempoElaboracion is not null and CDS.UidEstatusContrato = 'CD20F9BF-EBA2-4128-88FB-647544457B2D'";
            }
            else
            {
                Query = "select distinct p.uidproducto,i.NVchRuta, s.Identificador,se.UidSeccion, s.UidSucursal , p.VchNombre, p.VchDescripcion, sp.VchTiempoElaboracion,sp.Mcosto, " +
                    "e.NombreComercial from Productos p inner join ImagenProducto ip on ip.UidProducto = p.UidProducto " +
                    "inner join Imagenes i on ip.UidImagen = i.UIdImagen " +
                    "inner join SubcategoriaProducto scp on scp.UidProducto = p.UidProducto " +
                    "inner join Subcategoria su on su.UidSubcategoria = scp.UidSubcategoria " +
                    "inner join SeccionProducto sp on sp.UidProducto = p.UidProducto " +
                    "inner join Seccion se on se.UidSeccion = sp.UidSeccion " +
                    "inner join Oferta O on O.UidOferta = se.UidOferta " +
                    "inner join DiaOferta DO on DO.UidOferta = O.UidOferta " +
                    "inner join Dias D on D.UidDia = DO.UidDia " +
                    "inner join Sucursales s on s.UidSucursal = o.Uidsucursal " +
                    "inner join ContratoDeServicio CDS on CDS.UidSucursalSuministradora = s.UidSucursal " +
                    "inner join ZonaDeRepartoDeContrato ZDRC on ZDRC.UidContrato = CDS.UidContrato " +
                    "inner join Tarifario t on t.UidRegistroTarifario = ZDRC.UidTarifario " +
                    "inner join ZonaDeServicio zd on zd.UidColonia =  '" + UidColonia + "' and zd.UidRelacionZonaServicio = t.UidRelacionZonaEntrega " +
                    "inner join Empresa e on e.UidEmpresa = p.UidEmpresa where  su.UidSubcategoria = '" + Subcategoria + "' and '" + tiempoActual + "' " +
                    "between se.VchHoraInicio and se.VchHoraFin and  '" + tiempoActual + "'  between s.HorarioApertura and s.HorarioCierre  and zd.UidColonia = '" + UidColonia + "' and p.IntEstatus = 1  and D.VchNombre = '" + dia + "'" +
                    " and O.IntEstatus = 1 and se.IntEstatus = 1 and p.VchNombre like '%" + Nombre + "%' and sp.VchTiempoElaboracion is not null and CDS.UidEstatusContrato = 'CD20F9BF-EBA2-4128-88FB-647544457B2D'";

            }
            return oConexion.Consultas(Query);
        }

        public DataTable ObtenProductoSeccion(Guid uidSeccion)
        {
            oConexion = new Conexion();
            string query = "select p.UidProducto, p.VchNombre, sp.VchTiempoElaboracion, dbo.ObtenerPrecioConComision(sp.Mcosto,p.UidEmpresa )  as Mcosto,i.NVchRuta, p.VchDescripcion from Productos p inner join SeccionProducto sp on sp.UidProducto = p.UidProducto inner join Seccion s on s.UidSeccion = sp.UidSeccion inner join ImagenProducto ip on ip.UidProducto = p.UidProducto inner join Imagenes i on i.UIdImagen = ip.UidImagen where s.UidSeccion = '" + uidSeccion.ToString() + "' and p.IntEstatus = 1";
            return oConexion.Consultas(query);
        }

        public DataTable ProductoCarrito(Guid uidProducto, string UidSucursal, string UidSeccion)
        {
            oConexion = new Conexion();
            string query = "select sp.UidSeccion,p.UidProducto,sp.UidSeccionProducto, p.VchNombre,sp.VchTiempoElaboracion,dbo.ObtenerPrecioConComision(sp.Mcosto,p.UidEmpresa )  as Mcosto, s.Identificador,s.UidSucursal, i.NVchRuta, sp.UidSeccionProducto from  productos p inner join SeccionProducto sp on sp.UidProducto = p.UidProducto inner join Seccion se on se.UidSeccion = sp.UidSeccion inner join Oferta o on o.UidOferta = se.UidOferta inner join Sucursales s on s.UidSucursal = o.Uidsucursal inner join ImagenProducto ip on ip.UidProducto = p.UidProducto   inner join imagenes i on i.UIdImagen = ip.UidImagen where p.uidProducto = '" + uidProducto.ToString() + "' and s.UidSucursal = '" + UidSucursal + "' and se.uidSeccion = '" + UidSeccion + "'";
            return oConexion.Consultas(query);
        }




    }
}
