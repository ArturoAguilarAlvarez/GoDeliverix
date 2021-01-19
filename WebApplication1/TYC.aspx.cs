using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net.Http;
namespace WebApplication1
{
    public partial class TYC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string line;
            string url = "";
            url = Request["url"];
            StreamReader s = URLStream(url);
            String myline = s.ReadToEnd(); //First Line
            pnlPrincipal.InnerHtml = myline;
        }
        static StreamReader URLStream(String fileurl)
        {
            return new StreamReader(new HttpClient().GetStreamAsync(fileurl).Result);
        }
    }
}