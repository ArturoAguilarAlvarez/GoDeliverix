using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VistaDelModelo;

namespace WebApplication1.Vista.Cliente
{
    public partial class Historico : System.Web.UI.Page
    {
        VMOrden MVOrden = new VMOrden();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["MVOrden"] = MVOrden;
                MVOrden.ObtenerOrdenesCliente(Session["IdUsuario"].ToString(),"Usuario");
                CargaGrid("Ordenes");
                PanelDetalles.Visible = false;
            }
            else
            {
                MVOrden = (VMOrden)Session["MVOrden"];
            }
        }
        protected void CargaGrid(string GridView)
        {
            switch (GridView)
            {
                case "Ordenes":
                    DgvBitacoraOrdenes.DataSource = MVOrden.ListaDeOrdenes;
                    DgvBitacoraOrdenes.DataBind();
                    break;
                default:
                    break;
            }
        }
        protected void DgvBitacoraOrdenes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(DgvBitacoraOrdenes, "Select$" + e.Row.RowIndex);
            }
        }


        protected void DgvBitacoraOrdenes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Detalles")
            {
                int index = DgvBitacoraOrdenes.SelectedIndex;
                LinkButton BotonDetalles = DgvBitacoraOrdenes.Rows[index].FindControl("btnMuestraDetalles") as LinkButton;
                Label etiquetaDeTalles = DgvBitacoraOrdenes.Rows[index].FindControl("lblDetalles") as Label;

                if (e.CommandArgument.ToString() == "Oculta")
                {
                    BotonDetalles.CommandArgument = "Muestra";
                    BotonDetalles.CssClass = "btn btn-sm btn-info";
                    etiquetaDeTalles.CssClass = "glyphicon glyphicon-plus";
                }
                else
                {
                    BotonDetalles.CssClass = "btn btn-sm btn-danger";
                    etiquetaDeTalles.CssClass = "glyphicon glyphicon-minus";
                    BotonDetalles.CommandArgument = "Oculta";
                }
            }
        }

        protected void DgvBitacoraOrdenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int intdex = DgvBitacoraOrdenes.SelectedRow.RowIndex;
            DgvBitacoraOrdenes.SelectedIndex = intdex;
           
            lblOrden.Text = DgvBitacoraOrdenes.Rows[intdex].Cells[1].Text;
            lblFecha.Text = DgvBitacoraOrdenes.Rows[intdex].Cells[2].Text;
            lblTotal.Text = DgvBitacoraOrdenes.Rows[intdex].Cells[3].Text;
            
            PanelDetalles.Visible = true;
            string id = DgvBitacoraOrdenes.DataKeys[intdex].Value.ToString();
   
            DgvDetalles.DataSource = MVOrden.ObtenerSucursaleDeOrden(new Guid(id));
            DgvDetalles.DataBind();
        }

        protected void DgvDetalles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Detalles")
            {

                int index = int.Parse(e.CommandArgument.ToString());
                LinkButton BtnDetalles = DgvDetalles.Rows[index].FindControl("btnDetallesOrden") as LinkButton;

                Label lblEstatus = BtnDetalles.FindControl("lblEstatus") as Label;
                GridView dgvEstatus = DgvDetalles.Rows[index].FindControl("DGVEstatusOrden") as GridView;
                Panel PanelEstatus = DgvDetalles.Rows[index].FindControl("PanelDetallesEstatus") as Panel;

                PanelEstatus.Visible = true;
                dgvEstatus.DataSource = MVOrden.ObtenerEstatusOrden(DgvDetalles.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString());
                dgvEstatus.DataBind();
            }
            if (e.CommandName == "Productos")
            {
                int index = int.Parse(e.CommandArgument.ToString());
                LinkButton BtnDetalleProducto = DgvDetalles.Rows[index].FindControl("BtnDetallesProductos") as LinkButton;
                GridView dgvProdutos = DgvDetalles.Rows[index].FindControl("DGVProductosEnOrden") as GridView;
                Panel PanelProductos = DgvDetalles.Rows[index].FindControl("PanelProductos") as Panel;

                PanelProductos.Visible = true;
                MVOrden.ObtenerProductosDeOrden(DgvDetalles.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString());
                dgvProdutos.DataSource = MVOrden.ListaDeProductos;
                dgvProdutos.DataBind();
            }

            if (e.CommandName == "CierraPanelProductos")
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridView dgvProdutos = DgvDetalles.Rows[index].FindControl("DGVProductosEnOrden") as GridView;
                Panel PanelProductos = DgvDetalles.Rows[index].FindControl("PanelProductos") as Panel;

                PanelProductos.Visible = false;
                dgvProdutos.DataSource = null;
                dgvProdutos.DataBind();
            }
            if (e.CommandName == "CierraPanelEstatus")
            {
                int index = int.Parse(e.CommandArgument.ToString());
                Panel PanelEstatus = DgvDetalles.Rows[index].FindControl("PanelDetallesEstatus") as Panel;
                PanelEstatus.Visible = false;
            }
        }

        protected void btnMuestraDetalles_Click(object sender, EventArgs e)
        {
            PanelDetalles.Visible = false;
        }

        protected void DgvDetalles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int index = e.Row.RowIndex;
                LinkButton BtnDetalles = e.Row.FindControl("btnDetallesOrden") as LinkButton;
                Label lblEstatus = e.Row.FindControl("lblEstatus") as Label;
                Panel PanelProductos = e.Row.FindControl("PanelProductos") as Panel;
                Panel PanelEstatus = e.Row.FindControl("PanelDetallesEstatus") as Panel;

                PanelEstatus.Visible = false;
                PanelProductos.Visible = false;
                lblEstatus.CssClass = "glyphicon glyphicon-eye-open";
                BtnDetalles.CssClass = "btn btn-sm btn-success";
                BtnDetalles.ToolTip = "Más";

                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(DgvDetalles, "Select$" + index);
            }
        }

        protected void btnBuscarOrden_Click(object sender, EventArgs e)
        {
            string FechaInicial = txtFechaInicial.Text;
            string FechaFinal = txtFechaFinal.Text;
            string Numero = txtNumeroDeOrden.Text;
            lblMensaje.Visible = false;
            DgvBitacoraOrdenes.SelectedIndex = -1;
            if (!string.IsNullOrWhiteSpace(FechaInicial) && !string.IsNullOrWhiteSpace(FechaFinal))
            {
                if (DateTime.Parse(FechaInicial) > DateTime.Parse(FechaFinal))
                {
                    lblMensaje.Visible = true;
                    lblMensaje.Text = "La fecha inicial no debe ser mayor a la final";
                }
            }
            if (!lblMensaje.Visible)
            {
                lblMensaje.Visible = false;
                MVOrden.BuscarOrdenes("Usuario",new Guid(Session["IdUsuario"].ToString()), FechaInicial, FechaFinal, Numero);
                DgvBitacoraOrdenes.DataSource = MVOrden.ListaDeOrdenes;
                DgvBitacoraOrdenes.DataBind();
            }


        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            txtFechaFinal.Text = string.Empty;
            txtFechaInicial.Text = string.Empty;
            txtNumeroDeOrden.Text = string.Empty;
        }
    }
}