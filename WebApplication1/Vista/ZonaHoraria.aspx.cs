using System;
using System.Web.UI.WebControls;
using VistaDelModelo;
namespace WebApplication1.Vista
{
    public partial class ZonaHoraria : System.Web.UI.Page
    {
        VMDireccion MVDireccion;
        VMZonaHoraria MVZOnaHoaria;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["MVDireccion"] != null)
                {
                    MVDireccion = (VMDireccion)Session["MVDireccion"];
                }
                else
                {
                    MVDireccion = new VMDireccion();
                    Session["MVDireccion"] = MVDireccion;
                }
                if (Session["MVZOnaHoaria"] != null)
                {
                    MVZOnaHoaria = (VMZonaHoraria)Session["MVZOnaHoaria"];
                }
                else
                {
                    MVZOnaHoaria = new VMZonaHoraria();
                    Session["MVZOnaHoaria"] = MVZOnaHoaria;
                }
                ddlPais.DataSource = MVDireccion.Paises();
                ddlPais.DataTextField = "Nombre";
                ddlPais.DataValueField = "UidPais";
                ddlPais.DataBind();

                ddlEstadoPais.DataSource = MVDireccion.Paises();
                ddlEstadoPais.DataTextField = "Nombre";
                ddlEstadoPais.DataValueField = "UidPais";
                ddlEstadoPais.DataBind();

                CargaZonasHorarias();
                EstatusControlesPaneles(false,"Pais");
                EstatusControlesPaneles(false,"Estados");

                PanelPais.Visible = true;
                PanelEstados.Visible = false;
                liPanelPaises.Attributes.Add("class","active");
                liPanelEstados.Attributes.Add("class","");
            }
            else
            {
                MVDireccion = (VMDireccion)Session["MVDireccion"];
                MVZOnaHoaria = (VMZonaHoraria)Session["MVZOnaHoaria"];
            }
        }
        /// <summary>
        /// Controla el estatus de los controles dependiendo el panel visible
        /// </summary>
        /// <param name="estatus"></param>
        /// <param name="panel">Pais|Estados</param>
        protected void EstatusControlesPaneles(bool estatus, string panel)
        {
            if (panel == "Pais")
            {
                ddlPais.Enabled = estatus;
                chkbxlZonaHoraria.Enabled = estatus;
                btnAceptarPais.Visible = estatus;
                btnCancelarPais.Visible = estatus;
                ddlOrdenZonasPais.Enabled = estatus;
                if (estatus)
                {
                    ddlPais.CssClass = "form-control";
                }
                else
                {
                    ddlPais.CssClass = "form-control disabled";
                }
            }
            if (panel == "Estados")
            {
                ddlEstadoPais.Enabled = estatus;
                chkbxlEstados.Enabled = estatus;
                btnAceptarEstados.Visible = estatus;
                btnCancelarEstados.Visible = estatus;
                if (estatus)
                {
                    ddlEstadoPais.CssClass = "form-control";
                }
                else
                {
                    ddlEstadoPais.CssClass = "form-control disabled";
                }
            }

        }
        protected void VerificaZonaHorariaSeleccionadaPaises()
        {
            foreach (ListItem item in chkbxlZonaHoraria.Items)
            {
                if (item.Selected)
                {
                    MVZOnaHoaria.SeleccionarZonaHoraria(item.Value);
                }
                else
                {
                    MVZOnaHoaria.desSeleccionarZonaHoraria(item.Value);
                }
            }
        }
        protected void CargaZonasHorarias(string NombreABUscar = "", string Orden = "", string Panel = "")
        {
            VerificaZonasHorariasSeleccionadasPais();
            VerificaZonaHorariaSeleccionadaPaises();
            MVZOnaHoaria.RecuperarZonasHorarias();
            chkbxlZonaHoraria.Items.Clear();
            if (string.IsNullOrEmpty(Orden) && Orden != "0")
            {
                if (string.IsNullOrEmpty(NombreABUscar))
                {
                    chkbxlZonaHoraria.DataSource = MVZOnaHoaria.ListaDeZonas;
                    chkbxlZonaHoraria.DataTextField = "NombreCompleto";
                    chkbxlZonaHoraria.DataValueField = "Id";
                    chkbxlZonaHoraria.DataBind();

                }
                if (!string.IsNullOrEmpty(NombreABUscar))
                {

                    chkbxlZonaHoraria.DataSource = MVZOnaHoaria.ListaDeZonas.FindAll(z => z.NombreEstandar.Contains(NombreABUscar));
                    chkbxlZonaHoraria.DataTextField = "NombreCompleto";
                    chkbxlZonaHoraria.DataValueField = "Id";
                    chkbxlZonaHoraria.DataBind();
                }
            }
            else
            {
                switch (Orden)
                {
                    //Todos
                    case "0":
                        chkbxlZonaHoraria.DataSource = MVZOnaHoaria.ListaDeZonas;
                        chkbxlZonaHoraria.DataTextField = "NombreCompleto";
                        chkbxlZonaHoraria.DataValueField = "Id";
                        chkbxlZonaHoraria.DataBind();
                        break;
                    //Seleccionados
                    case "1":
                        chkbxlZonaHoraria.DataSource = MVZOnaHoaria.ListaDeZonas.FindAll(z => z.IsSelected == true);
                        chkbxlZonaHoraria.DataTextField = "NombreCompleto";
                        chkbxlZonaHoraria.DataValueField = "Id";
                        chkbxlZonaHoraria.DataBind();
                        break;
                    //Desseleccionados
                    case "2":
                        chkbxlZonaHoraria.DataSource = MVZOnaHoaria.ListaDeZonas.FindAll(z => z.IsSelected == false);
                        chkbxlZonaHoraria.DataTextField = "NombreCompleto";
                        chkbxlZonaHoraria.DataValueField = "Id";
                        chkbxlZonaHoraria.DataBind();
                        break;
                    default:
                        break;
                }
            }
            VerificaZonasHorariasSeleccionadasPais();
        }
        protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            MVZOnaHoaria.BuscarZonaHorariaDePais(UidPais: ddlPais.SelectedItem.Value);
            CargaZonasHorarias();
        }


        protected void btnBuscarPais_Click(object sender, EventArgs e)
        {
            CargaZonasHorarias(NombreABUscar: txtBuscarZonaPais.Text);
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            EstatusControlesPaneles(true,"Pais");
        }


        protected void ddlOrdenZonasPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList objeto = sender as DropDownList;
            CargaZonasHorarias(Orden: objeto.SelectedItem.Value);
        }

        protected void VerificaZonasHorariasSeleccionadasPais()
        {
            foreach (ListItem item in chkbxlZonaHoraria.Items)
            {
                if (MVZOnaHoaria.ListaSeleccionadas.Exists(z => z.Id == item.Value))
                {
                    item.Selected = true;
                }
                else
                {
                    item.Selected = false;
                }
            }
        }
        protected void VerificaEstadosDeZonaHoraria()
        {
            foreach (ListItem item in chkbxlEstados.Items)
            {
                if (MVDireccion.ListaDeEstadosSeleccionados.Exists(z => z.UidEstado == new Guid(item.Value)))
                {
                    item.Selected = true;
                }
                else
                {
                    item.Selected = false;
                }
            }
        }

        protected void btnCancelarPais_Click(object sender, EventArgs e)
        {
            EstatusControlesPaneles(false,"Pais");
            ddlPais.SelectedIndex = -1;
            ddlOrdenZonasPais.SelectedIndex = -1;
            MVZOnaHoaria.ListaSeleccionadas.Clear();
        }

        protected void btnAceptarPais_Click(object sender, EventArgs e)
        {
            VerificaZonaHorariaSeleccionadaPaises();
            MVZOnaHoaria.GuardarZonasPais(ddlPais.SelectedItem.Value);
            EstatusControlesPaneles(false,"Pais");
        }
        

        protected void btnPanelPaises_Click(object sender, EventArgs e)
        {
            PanelPais.Visible = true;
            PanelEstados.Visible = false;
            liPanelPaises.Attributes.Add("class", "active");
            liPanelEstados.Attributes.Add("class", "");
        }

        protected void btnPanelEstados_Click(object sender, EventArgs e)
        {
            PanelPais.Visible = false;
            PanelEstados.Visible = true;
            liPanelPaises.Attributes.Add("class", "");
            liPanelEstados.Attributes.Add("class", "active");
        }

        protected void ddlEstadoPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            string UidPais = ddlEstadoPais.SelectedItem.Value;
            MVZOnaHoaria.BuscarZonaHorariaDePais(UidPais: UidPais.ToString());
            MVZOnaHoaria.RecuperarZonasHorarias();

            RBLZonasHorarias.DataSource = MVZOnaHoaria.ObtenerZonasDelPais(UidPais);
            RBLZonasHorarias.DataTextField = "NombreCompleto";
            RBLZonasHorarias.DataValueField = "UidZonaHorariaPais";
            RBLZonasHorarias.DataBind();


            VerificaEstadosSeleccionados();
        }

        protected void btnEditarEstados_Click(object sender, EventArgs e)
        {
            EstatusControlesPaneles(true, "Estados");
        }

        protected void btnAceptarEstados_Click(object sender, EventArgs e)
        {
            VerificaEstadosSeleccionados();
            MVZOnaHoaria.GuardarZonasEstados(RBLZonasHorarias.SelectedItem.Value,MVDireccion.ListaDeEstadosSeleccionados);

            EstatusControlesPaneles(false, "Pais");
            EstatusControlesPaneles(false, "Estados");
        }

        private void VerificaEstadosSeleccionados()
        {
            foreach (ListItem item in chkbxlEstados.Items)
            {
                if (item.Selected)
                {
                    MVDireccion.SeleccionarEstado(item.Value);
                }
                else
                {
                    MVDireccion.desSeleccionarEstado(item.Value);
                }
            }
        }

        protected void btnCancelarEstados_Click(object sender, EventArgs e)
        {
            EstatusControlesPaneles(false, "Estados");
        }

        protected void RBLZonasHorarias_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid UidPais = new Guid(ddlEstadoPais.SelectedItem.Value);
            MVDireccion.BuscarZonaHorariaDePais(UidZonaHoraria: RBLZonasHorarias.SelectedItem.Value);

            

            chkbxlEstados.DataSource = MVDireccion.Estados(UidPais, "info");
            chkbxlEstados.CssClass = "Checkbox";
            chkbxlEstados.DataTextField = "Nombre";
            chkbxlEstados.DataValueField = "IdEstado";
            chkbxlEstados.DataBind();
            VerificaEstadosDeZonaHoraria();
        }

        protected void btnBuscarEstado_Click(object sender, EventArgs e)
        {
            VerificaEstadosSeleccionados();
            Guid UidPais = new Guid(ddlEstadoPais.SelectedItem.Value);
            chkbxlEstados.DataSource = MVDireccion.Estados(UidPais, busqueda:"info", Nombre: txtBuscarEstado.Text);
            chkbxlEstados.CssClass = "Checkbox";
            chkbxlEstados.DataTextField = "Nombre";
            chkbxlEstados.DataValueField = "IdEstado";
            chkbxlEstados.DataBind();
            VerificaEstadosDeZonaHoraria();
        }

        protected void chkbxlEstados_DataBound(object sender, EventArgs e)
        {

        }
    }
}