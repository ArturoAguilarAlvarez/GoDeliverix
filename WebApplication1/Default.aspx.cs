using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var plataforma = Request.UserAgent;
            switch (plataforma)
            {
                case "Android":
                    Response.Redirect("https://play.google.com/store/apps/details?id=com.CompuAndSoft.GDCliente");
                    break;
                case "iPad":
                    Response.Redirect("http://appstore.com/compuandsoft/godeliverix");
                    break;
                case "iPhone":
                    Response.Redirect("http://appstore.com/compuandsoft/godeliverix");
                    break;
                default:
                    Response.Redirect("Vista/Default/");
                    break;
            }
        }
    }
}