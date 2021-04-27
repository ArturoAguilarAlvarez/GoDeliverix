using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using OfficeOpenXml;
using VistaDelModelo;
using System.IO;

namespace WebApplication1.Vista.Office
{
    public partial class ExportarMenu : System.Web.UI.Page
    {
        VMGiro MVGiro = new VMGiro();
        VMCategoria MVCategoria = new VMCategoria();
        VMSubCategoria MVSubcategoria = new VMSubCategoria();
        VMOferta MVOferta = new VMOferta();
        VMSeccion MVSeccion = new VMSeccion();
        VMSucursales MVSucursal = new VMSucursales();
        VMEmpresas MVEmpresa = new VMEmpresas();
        VMTarifario MVTarifario = new VMTarifario();
        VMProducto MVProductos = new VMProducto();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null && Session["UidEmpresaSistema"] != null)
            {
                if (!IsPostBack)
                {
                    DateTime dateTime = DateTime.Now;

                    DataTable lsError = null;

                    if (lsError != null)
                    {
                        // Expor("Error " + dateTime.ToString("ddMMyyyyHHmmssfff"), gvAlumnos);
                    }
                    else
                    {
                        DataTable data = new DataTable();
                        DataRow DR;
                        string NombreDearchivo = "";
                        switch (Session["ParametroVentanaExcel"].ToString())
                        {
                            case "Horario de sucursales":
                                MVSucursal.BuscarSucursales(Uidempresa: Session["UidEmpresaSistema"].ToString());
                                MVEmpresa.BuscarEmpresas(UidEmpresa: new Guid(Session["UidEmpresaSistema"].ToString()));
                                data = ExcelSucursales();
                                foreach (var item in MVSucursal.LISTADESUCURSALES)
                                {
                                    DR = data.NewRow();
                                    // Then add the new row to the collection.
                                    DR["ID"] = item.ID;
                                    DR["EMPRESA"] = MVEmpresa.NOMBRECOMERCIAL;
                                    DR["IDENTIFICADOR"] = item.IDENTIFICADOR;
                                    DR["HORAAPARTURA"] = item.HORAAPARTURA;
                                    DR["HORACIERRE"] = item.HORACIERRE;
                                    data.Rows.Add(DR);
                                }
                                NombreDearchivo = "HorarioSucursales";
                                break;
                            case "Todas las sucursales":
                                MVEmpresa.BuscarEmpresas();
                                data = ExcelSucursales();
                                foreach (var item in MVEmpresa.LISTADEEMPRESAS)
                                {
                                    MVSucursal.BuscarSucursales(Uidempresa: item.UIDEMPRESA.ToString());
                                    foreach (var suc in MVSucursal.LISTADESUCURSALES)
                                    {
                                        DR = data.NewRow();
                                        // Then add the new row to the collection.
                                        DR["ID"] = suc.ID;
                                        DR["EMPRESA"] = item.NOMBRECOMERCIAL;
                                        DR["IDENTIFICADOR"] = suc.IDENTIFICADOR;
                                        DR["HORAAPARTURA"] = suc.HORAAPARTURA;
                                        DR["HORACIERRE"] = suc.HORACIERRE;
                                        data.Rows.Add(DR);
                                    }
                                }
                                NombreDearchivo = "HorarioDeTodasLasSucursales";
                                break;
                            case "Precio de productos":
                                data = MVOferta.ExportarExcel(Session["UidSucursal"].ToString());
                                NombreDearchivo = "PrecioYTiempoProductosEnMenu";
                                break;
                            case "Horario secciones":
                                var secciones = new List<VMSeccion>();
                                string uid = Session["UidSucursal"].ToString();
                                MVOferta.Buscar(UIDSUCURSAL: new Guid(uid));
                                MVSucursal.BuscarSucursales(UidSucursal: uid);
                                data = ExcelSecciones();
                                foreach (var oferta in MVOferta.ListaDeOfertas)
                                {
                                    MVSeccion.Buscar(UIDOFERTA: oferta.UID);
                                    foreach (var item in MVSeccion.ListaDeSeccion)
                                    {
                                        DR = data.NewRow();
                                        // Then add the new row to the collection.
                                        DR["UID"] = item.UID;
                                        DR["SECCION"] = item.StrNombre;
                                        DR["Sucursal"] = MVSucursal.IDENTIFICADOR;
                                        DR["Oferta"] = oferta.STRNOMBRE;
                                        DR["HORAAPARTURA"] = item.StrHoraInicio;
                                        DR["HORACIERRE"] = item.StrHoraFin;
                                        data.Rows.Add(DR);
                                    }
                                }
                                NombreDearchivo = "TiempoSecciones";
                                break;
                            case "Todos los tarifarios":
                                data = MVTarifario.ExportarTarifario();
                                NombreDearchivo = "TodasLasTarifas";
                                break;
                            case "Exportar productos":
                                MVProductos.Buscar(UidEmpresa: new Guid(Session["UidEmpresaSistema"].ToString()));
                                data = ExcelProductosActualizar();
                                foreach (var item in MVProductos.ListaDeProductos)
                                {
                                    DR = data.NewRow();
                                    // Then add the new row to the collection.
                                    var imagen = new VMImagen();
                                    imagen.ObtenerImagenProducto(item.UID.ToString());
                                    DR["UID"] = item.UID;
                                    DR["Imagen"] = imagen.STRRUTA;
                                    DR["Nombre"] = item.STRNOMBRE;
                                    DR["Descripcion"] = item.STRDESCRIPCION;
                                    data.Rows.Add(DR);
                                }
                                break;
                            case "Exportar productos desde empresas":
                                MVProductos.Buscar(UidEmpresa: new Guid(Session["UidEmpresaSistema"].ToString()));
                                data = ExcelProductosEmpresa();
                                foreach (var item in MVProductos.ListaDeProductos)
                                {
                                    MVProductos.RecuperaGiro(item.UID.ToString());
                                    MVProductos.RecuperaCategoria(item.UID.ToString());
                                    MVProductos.RecuperaSubcategoria(item.UID.ToString());
                                    MVGiro.BuscarGiro(UidGiro: MVProductos.ListaDeGiro[0].UIDGIRO.ToString());
                                    MVCategoria.BuscarCategorias(UidCategoria: MVProductos.ListaDeCategorias[0].UIDCATEGORIA.ToString());
                                    MVSubcategoria.BuscarSubCategoria(UidSubCategoria: MVProductos.ListaDeSubcategorias[0].UIDSUBCATEGORIA.ToString());
                                    DR = data.NewRow();
                                    // Then add the new row to the collection.
                                    var imagen = new VMImagen();
                                    imagen.ObtenerImagenProducto(item.UID.ToString());
                                    DR["UidProducto"] = item.UID;
                                    DR["Imagen"] = imagen.STRRUTA;
                                    DR["Nombre"] = item.STRNOMBRE;
                                    DR["Descripcion"] = item.STRDESCRIPCION;
                                    DR["UidGiro"] = MVGiro.UIDVM;
                                    DR["Giro"] = MVGiro.STRNOMBRE;
                                    DR["UidCategoria"] = MVCategoria.UIDCATEGORIA;
                                    DR["Categoria"] = MVCategoria.STRNOMBRE;
                                    DR["UidSubcategoria"] = MVSubcategoria.UID;
                                    DR["Subcategoria"] = MVSubcategoria.STRNOMBRE;
                                    data.Rows.Add(DR);
                                }
                                NombreDearchivo = "ProductosDeEmpresa";
                                break;
                            case "Exportar plantilla productos":
                                data = ExcelProductosAgregar();
                                NombreDearchivo = "PlantillaDeNuevosProductos";
                                break;
                            case "Catalogos":
                                data = ExcelCatalogosDeBusqueda();
                                NombreDearchivo = "CatalogosDeBusqueda";
                                MVGiro.BuscarGiro();
                                foreach (var oGiro in MVGiro.LISTADEGIRO)
                                {
                                    MVCategoria.BuscarCategorias(UidGiro: oGiro.UIDVM.ToString());
                                    foreach (var oCategoria in MVCategoria.LISTADECATEGORIAS)
                                    {
                                        MVSubcategoria.BuscarSubCategoria(UidCategoria: oCategoria.UIDCATEGORIA.ToString());
                                        foreach (var oSubcategoria in MVSubcategoria.LISTADESUBCATEGORIAS)
                                        {
                                            DR = data.NewRow();
                                            DR["UidGiro"] = oGiro.UIDVM;
                                            DR["Giro"] = oGiro.STRNOMBRE;
                                            DR["UidCategoria"] = oCategoria.UIDCATEGORIA;
                                            DR["Categoria"] = oCategoria.STRNOMBRE;
                                            DR["UidSubcategoria"] = oSubcategoria.UID;
                                            DR["Subcategoria"] = oSubcategoria.STRNOMBRE;
                                            data.Rows.Add(DR);
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        using (DataTable dt = data)
                        {
                            ExporttoExcel(dt, dateTime.ToString("ddMMyyyyHHmmssfff") + NombreDearchivo);
                        }
                    }
                }
                else
                {

                }
            }
            else
            {
                Response.Redirect("../Default/Default.aspx");
            }
        }


        private DataTable ExcelSucursales()
        {
            // Create a new DataTable titled 'Names.'
            DataTable namesTable = new DataTable("Names");

            // Add three column objects to the table.
            DataColumn lID = new DataColumn();
            lID.DataType = System.Type.GetType("System.String");
            lID.ColumnName = "ID";
            lID.DefaultValue = "";
            namesTable.Columns.Add(lID);

            DataColumn LEmpresa = new DataColumn();
            LEmpresa.DataType = System.Type.GetType("System.String");
            LEmpresa.ColumnName = "EMPRESA";
            namesTable.Columns.Add(LEmpresa);

            DataColumn lIDENTIFICADOR = new DataColumn();
            lIDENTIFICADOR.DataType = System.Type.GetType("System.String");
            lIDENTIFICADOR.ColumnName = "IDENTIFICADOR";
            lIDENTIFICADOR.DefaultValue = "";
            namesTable.Columns.Add(lIDENTIFICADOR);

            DataColumn lHORAAPARTURA = new DataColumn();
            lHORAAPARTURA.DataType = System.Type.GetType("System.String");
            lHORAAPARTURA.ColumnName = "HORAAPARTURA";
            namesTable.Columns.Add(lHORAAPARTURA);

            DataColumn lHORACIERRE = new DataColumn();
            lHORACIERRE.DataType = System.Type.GetType("System.String");
            lHORACIERRE.ColumnName = "HORACIERRE";
            namesTable.Columns.Add(lHORACIERRE);


            // Create an array for DataColumn objects.
            DataColumn[] keys = new DataColumn[1];
            keys[0] = lID;
            namesTable.PrimaryKey = keys;

            // Return the new DataTable.
            return namesTable;
        }

        private DataTable ExcelCatalogosDeBusqueda()
        {
            // Create a new DataTable titled 'Names.'
            DataTable namesTable = new DataTable("Names");

            // Add three column objects to the table.
            DataColumn lID = new DataColumn();
            lID.DataType = System.Type.GetType("System.String");
            lID.ColumnName = "UidGiro";
            lID.DefaultValue = "";
            namesTable.Columns.Add(lID);

            DataColumn Lsucursal = new DataColumn();
            Lsucursal.DataType = System.Type.GetType("System.String");
            Lsucursal.ColumnName = "Giro";
            Lsucursal.DefaultValue = "";
            namesTable.Columns.Add(Lsucursal);
            DataColumn LOferta = new DataColumn();
            LOferta.DataType = System.Type.GetType("System.String");
            LOferta.ColumnName = "UidCategoria";
            LOferta.DefaultValue = "";
            namesTable.Columns.Add(LOferta);
            DataColumn lIDENTIFICADOR = new DataColumn();
            lIDENTIFICADOR.DataType = System.Type.GetType("System.String");
            lIDENTIFICADOR.ColumnName = "Categoria";
            lIDENTIFICADOR.DefaultValue = "";
            namesTable.Columns.Add(lIDENTIFICADOR);

            DataColumn lHORAAPARTURA = new DataColumn();
            lHORAAPARTURA.DataType = System.Type.GetType("System.String");
            lHORAAPARTURA.ColumnName = "UidSubcategoria";
            namesTable.Columns.Add(lHORAAPARTURA);

            DataColumn lHORACIERRE = new DataColumn();
            lHORACIERRE.DataType = System.Type.GetType("System.String");
            lHORACIERRE.ColumnName = "Subcategoria";
            namesTable.Columns.Add(lHORACIERRE);

            // Create an array for DataColumn objects.
            //DataColumn[] keys = new DataColumn[1];
            //keys[0] = lID;
            //namesTable.PrimaryKey = keys;

            // Return the new DataTable.
            return namesTable;
        }
        private DataTable ExcelSecciones()
        {
            // Create a new DataTable titled 'Names.'
            DataTable namesTable = new DataTable("Names");

            // Add three column objects to the table.
            DataColumn lID = new DataColumn();
            lID.DataType = System.Type.GetType("System.String");
            lID.ColumnName = "UID";
            lID.DefaultValue = "";
            namesTable.Columns.Add(lID);

            DataColumn lIDENTIFICADOR = new DataColumn();
            lIDENTIFICADOR.DataType = System.Type.GetType("System.String");
            lIDENTIFICADOR.ColumnName = "SECCION";
            lIDENTIFICADOR.DefaultValue = "";
            namesTable.Columns.Add(lIDENTIFICADOR);

            DataColumn Lsucursal = new DataColumn();
            Lsucursal.DataType = System.Type.GetType("System.String");
            Lsucursal.ColumnName = "Sucursal";
            Lsucursal.DefaultValue = "";
            namesTable.Columns.Add(Lsucursal);
            DataColumn LOferta = new DataColumn();
            LOferta.DataType = System.Type.GetType("System.String");
            LOferta.ColumnName = "Oferta";
            LOferta.DefaultValue = "";
            namesTable.Columns.Add(LOferta);


            DataColumn lHORAAPARTURA = new DataColumn();
            lHORAAPARTURA.DataType = System.Type.GetType("System.String");
            lHORAAPARTURA.ColumnName = "HORAAPARTURA";
            namesTable.Columns.Add(lHORAAPARTURA);

            DataColumn lHORACIERRE = new DataColumn();
            lHORACIERRE.DataType = System.Type.GetType("System.String");
            lHORACIERRE.ColumnName = "HORACIERRE";
            namesTable.Columns.Add(lHORACIERRE);

            // Create an array for DataColumn objects.
            DataColumn[] keys = new DataColumn[1];
            keys[0] = lID;
            namesTable.PrimaryKey = keys;

            // Return the new DataTable.
            return namesTable;
        }
        private DataTable ExcelProductosEmpresa()
        {
            // Create a new DataTable titled 'Names.'
            DataTable namesTable = new DataTable("Productos");

            // Add three column objects to the table.
            DataColumn lID = new DataColumn();
            lID.DataType = System.Type.GetType("System.String");
            lID.ColumnName = "UidProducto";
            lID.DefaultValue = "";
            namesTable.Columns.Add(lID);

            DataColumn Lsucursal = new DataColumn();
            Lsucursal.DataType = System.Type.GetType("System.String");
            Lsucursal.ColumnName = "Imagen";
            Lsucursal.DefaultValue = "";
            namesTable.Columns.Add(Lsucursal);

            DataColumn lHORAAPARTURA = new DataColumn();
            lHORAAPARTURA.DataType = System.Type.GetType("System.String");
            lHORAAPARTURA.ColumnName = "Nombre";
            namesTable.Columns.Add(lHORAAPARTURA);

            DataColumn lHORACIERRE = new DataColumn();
            lHORACIERRE.DataType = System.Type.GetType("System.String");
            lHORACIERRE.ColumnName = "Descripcion";
            namesTable.Columns.Add(lHORACIERRE);

            DataColumn lUidGiro = new DataColumn();
            lUidGiro.DataType = System.Type.GetType("System.String");
            lUidGiro.ColumnName = "UidGiro";
            namesTable.Columns.Add(lUidGiro);

            DataColumn lNombreGiro = new DataColumn();
            lNombreGiro.DataType = System.Type.GetType("System.String");
            lNombreGiro.ColumnName = "Giro";
            namesTable.Columns.Add(lNombreGiro);

            DataColumn lUidCategoria = new DataColumn();
            lUidCategoria.DataType = System.Type.GetType("System.String");
            lUidCategoria.ColumnName = "UidCategoria";
            namesTable.Columns.Add(lUidCategoria);

            DataColumn lNombreCategoria = new DataColumn();
            lNombreCategoria.DataType = System.Type.GetType("System.String");
            lNombreCategoria.ColumnName = "Categoria";
            namesTable.Columns.Add(lNombreCategoria);

            DataColumn lUidSubcategoria = new DataColumn();
            lUidSubcategoria.DataType = System.Type.GetType("System.String");
            lUidSubcategoria.ColumnName = "UidSubcategoria";
            namesTable.Columns.Add(lUidSubcategoria);

            DataColumn lNombreSubcategoria = new DataColumn();
            lNombreSubcategoria.DataType = System.Type.GetType("System.String");
            lNombreSubcategoria.ColumnName = "Subcategoria";
            namesTable.Columns.Add(lNombreSubcategoria);

            // Create an array for DataColumn objects.
            DataColumn[] keys = new DataColumn[1];
            keys[0] = lID;
            namesTable.PrimaryKey = keys;

            // Return the new DataTable.
            return namesTable;
        }
        private DataTable ExcelProductosActualizar()
        {
            // Create a new DataTable titled 'Names.'
            DataTable namesTable = new DataTable("Productos");

            // Add three column objects to the table.
            DataColumn lID = new DataColumn();
            lID.DataType = System.Type.GetType("System.String");
            lID.ColumnName = "UID";
            lID.DefaultValue = "";
            namesTable.Columns.Add(lID);

            DataColumn Lsucursal = new DataColumn();
            Lsucursal.DataType = System.Type.GetType("System.String");
            Lsucursal.ColumnName = "Imagen";
            Lsucursal.DefaultValue = "";
            namesTable.Columns.Add(Lsucursal);

            DataColumn lHORAAPARTURA = new DataColumn();
            lHORAAPARTURA.DataType = System.Type.GetType("System.String");
            lHORAAPARTURA.ColumnName = "Nombre";
            namesTable.Columns.Add(lHORAAPARTURA);

            DataColumn lHORACIERRE = new DataColumn();
            lHORACIERRE.DataType = System.Type.GetType("System.String");
            lHORACIERRE.ColumnName = "Descripcion";
            namesTable.Columns.Add(lHORACIERRE);

            // Create an array for DataColumn objects.
            DataColumn[] keys = new DataColumn[1];
            keys[0] = lID;
            namesTable.PrimaryKey = keys;

            // Return the new DataTable.
            return namesTable;
        }

        private DataTable ExcelProductosAgregar()
        {
            // Create a new DataTable titled 'Names.'
            DataTable namesTable = new DataTable("Productos");

            // Add three column objects to the table.

            DataColumn LImagen = new DataColumn();
            LImagen.DataType = System.Type.GetType("System.String");
            LImagen.ColumnName = "Imagen";
            LImagen.DefaultValue = "";
            namesTable.Columns.Add(LImagen);

            DataColumn lHORAAPARTURA = new DataColumn();
            lHORAAPARTURA.DataType = System.Type.GetType("System.String");
            lHORAAPARTURA.ColumnName = "Nombre";
            namesTable.Columns.Add(lHORAAPARTURA);

            DataColumn lHORACIERRE = new DataColumn();
            lHORACIERRE.DataType = System.Type.GetType("System.String");
            lHORACIERRE.ColumnName = "Descripcion";
            namesTable.Columns.Add(lHORACIERRE);

            // Create an array for DataColumn objects.
            DataColumn[] keys = new DataColumn[1];
            keys[0] = LImagen;
            namesTable.PrimaryKey = keys;

            // Return the new DataTable.
            return namesTable;
        }

        public void ExporttoExcel(DataTable table, string filename)
        {

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xlsx");

            ExcelPackage pack = new ExcelPackage();
            ExcelWorksheet ws = pack.Workbook.Worksheets.Add(filename);
            ws.Cells["A1"].LoadFromDataTable(table, true);
            var ms = new MemoryStream();
            pack.SaveAs(ms);
            ms.WriteTo(HttpContext.Current.Response.OutputStream);

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();

        }
    }
}