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
                    data = MVOferta.ExportarExcel(Session["UidSucursal"].ToString());
                    using (DataTable dt = data)
                    {
                        ExporttoExcel(dt, dateTime.ToString("ddMMyyyyHHmmssfff"));
                    }
                }
            }
            else
            {

            }

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
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename= Menu" + filename + ".xlsx");

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