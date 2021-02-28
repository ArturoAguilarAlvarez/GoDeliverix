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
        VMOferta MVOferta = new VMOferta();
        VMSeccion MVSeccion = new VMSeccion();
        VMSucursales MVSucursal = new VMSucursales();
        VMEmpresas MVEmpresa = new VMEmpresas();


        protected void Page_Load(object sender, EventArgs e)
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
                                    DR["Sucursal"] = MVSucursal.IDENTIFICADOR;
                                    DR["SECCION"] = item.StrNombre;
                                    DR["HORAAPARTURA"] = item.StrHoraInicio;
                                    DR["HORACIERRE"] = item.StrHoraFin;
                                    data.Rows.Add(DR);
                                }
                            }
                            NombreDearchivo = "TiempoSecciones";
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

            DataColumn Lsucursal = new DataColumn();
            Lsucursal.DataType = System.Type.GetType("System.String");
            Lsucursal.ColumnName = "Sucursal";
            Lsucursal.DefaultValue = "";
            namesTable.Columns.Add(Lsucursal);
            DataColumn lIDENTIFICADOR = new DataColumn();
            lIDENTIFICADOR.DataType = System.Type.GetType("System.String");
            lIDENTIFICADOR.ColumnName = "SECCION";
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