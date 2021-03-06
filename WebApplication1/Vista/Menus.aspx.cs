﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VistaDelModelo;
using System.Linq;
using System.Timers;
using System.Drawing;

namespace WebApplication1.Vista
{
    public partial class Menus : System.Web.UI.Page
    {
        #region Propiedades
        VMSucursales MVSucursales = new VMSucursales();
        VMProducto MVProducto = new VMProducto();
        VMGiro MVGiro = new VMGiro();
        VMCategoria MVCategoria = new VMCategoria();
        VMSubCategoria MVSubcategoria = new VMSubCategoria();
        VMSeccion MVSeccion = new VMSeccion();
        VMEstatus MVEstatus = new VMEstatus();
        VMOferta MVOferta = new VMOferta();
        VMDia MVDia = new VMDia();
        VMComision MVComision = new VMComision();
        string Acciones = "";

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["MVSucursales"] = MVSucursales;
                Session["MVProducto"] = MVProducto;
                Session["MVGiro"] = MVGiro;
                Session["MVCategoria"] = MVCategoria;
                Session["MVSubcategoria"] = MVSubcategoria;
                Session["MVSeccion"] = MVSeccion;
                Session["MVEstatus"] = MVEstatus;
                Session["MVOferta"] = MVOferta;
                Session["MVDia"] = MVDia;
                Session["MVComision"] = MVComision;


                DGVSucursales.DataSource = null;
                DGVSucursales.DataBind();

                DLProductos.DataSource = null;
                DLProductos.DataBind();

                MuestraPanel("Oferta");

                MVEstatus.OBTENERLISTA();
                ddlEstatusSeccion.DataSource = MVEstatus.ListaEstatus;
                ddlEstatusSeccion.DataTextField = "NOMBRE";
                ddlEstatusSeccion.DataValueField = "ID";
                ddlEstatusSeccion.DataBind();


                ddldEstatusOferta.DataSource = MVEstatus.ListaEstatus;
                ddldEstatusOferta.DataTextField = "NOMBRE";
                ddldEstatusOferta.DataValueField = "ID";
                ddldEstatusOferta.DataBind();

                DGVSeccion.DataSource = null;
                DGVSeccion.DataBind();


                MVDia.Buscar();
                chbxlistDiasOferta.DataSource = MVDia.ListaDeDias;
                chbxlistDiasOferta.DataTextField = "StrNombre";
                chbxlistDiasOferta.DataValueField = "UID";
                chbxlistDiasOferta.DataBind();


                dgvoferta.DataSource = null;
                dgvoferta.DataBind();

                ManejoDeControlesSeccion();
                ManejoDeControlesOferta();
                HabilitaControlesProductos(false);
                EstatusControlesInformacionProducto(false);


                txtNombreOferta.Attributes.Add("placeholder", "Nombre");
                txtSeccionNombre.Attributes.Add("placeholder", "Nombre");
                txtCostoProduto.Attributes.Add("placeholder", "Costo");

                lblSeleccionSucursal.Visible = false;
                lblSeleccionOferta.Visible = false;
                lblSeleccionSeccion.Visible = false;

                //vacia los uid del modulo
                txtUidSucursal.Text = string.Empty;
                txtUidOferta.Text = string.Empty;
                txtUidSeccion.Text = string.Empty;





            }
            else
            {
                MVSucursales = (VMSucursales)Session["MVSucursales"];
                MVProducto = (VMProducto)Session["MVProducto"];
                MVGiro = (VMGiro)Session["MVGiro"];
                MVCategoria = (VMCategoria)Session["MVCategoria"];
                MVSubcategoria = (VMSubCategoria)Session["MVSubcategoria"];
                MVSeccion = (VMSeccion)Session["MVSeccion"];
                MVEstatus = (VMEstatus)Session["MVEstatus"];
                MVOferta = (VMOferta)Session["MVOferta"];
                MVDia = (VMDia)Session["MVDia"];
                MVComision = (VMComision)Session["MVComision"];

            }
        }

        protected void BtnBABuscar_Click(object sender, EventArgs e)
        {
            string Identificador = txtFIdentificador.Text;
            string HA = txtFHoraApertura.Text;
            string HC = txtFHoraCierre.Text;

            if (txtFIdentificador.Text == string.Empty && txtFHoraApertura.Text == string.Empty && txtFHoraCierre.Text == string.Empty)
            {
                MVSucursales.DatosGridViewBusquedaNormal(Session["UidEmpresaSistema"].ToString());

                DGVSucursales.DataSource = MVSucursales.LISTADESUCURSALES;
                DGVSucursales.DataBind();

                lblVisibilidadfiltros.Text = " Mostrar";
                PnlFiltros.Visible = false;
                BtnBuscar.Enabled = false;
                BtnLimpiar.Enabled = false;
                BtnBuscar.CssClass = "btn btn-sm btn-default disabled";
                BtnLimpiar.CssClass = "btn btn-sm btn-default disabled";
            }
            else
            {
                MVSucursales.BuscarSucursales(Identificador, HA, HC, Uidempresa: Session["UidEmpresaSistema"].ToString());

                DGVSucursales.DataSource = MVSucursales.LISTADESUCURSALES;
                DGVSucursales.DataBind();

                lblVisibilidadfiltros.Text = " Mostrar";
                PnlFiltros.Visible = false;
                BtnBuscar.Enabled = false;
                BtnLimpiar.Enabled = false;
                BtnBuscar.CssClass = "btn btn-sm btn-default disabled";
                BtnLimpiar.CssClass = "btn btn-sm btn-default disabled";
            }
        }

        protected void BtnOcultar_Click(object sender, EventArgs e)
        {
            if (lblVisibilidadfiltros.Text == " Mostrar")
            {
                lblVisibilidadfiltros.Text = " Ocultar";
                BtnBuscar.Enabled = true;
                PnlFiltros.Visible = true;
                BtnLimpiar.Enabled = true;
                BtnBuscar.CssClass = "btn btn-sm btn-default";
                BtnLimpiar.CssClass = "btn btn-sm btn-default";

            }
            else if (lblVisibilidadfiltros.Text == " Ocultar")
            {
                lblVisibilidadfiltros.Text = " Mostrar";
                BtnBuscar.Enabled = false;
                BtnBuscar.CssClass = "btn btn-sm btn-default disabled";
                BtnLimpiar.CssClass = "btn btn-sm btn-default disabled";
                PnlFiltros.Visible = false;
                BtnLimpiar.Enabled = false;
            }
        }
        protected void MuestraPanel(string Panel)
        {
            //Obtiene los productos seleccionados.
            ObtenerProductosSeleccionados();
            //Alimenta al data list de producto seleccionado si la lista de seleccionados no esta vacia
            if (MVProducto.ListaDeProductosSeleccionados != null)
            {
                DLProductoSeleccionado.DataSource = MVProducto.ListaDeProductosSeleccionados;
                DLProductoSeleccionado.DataBind();
            }

            //Muestra el panel
            switch (Panel)
            {
                case "Oferta":
                    PanelOferta.Visible = true;
                    PanelSeccion.Visible = false;
                    PanelDetalles.Visible = false;
                    PanelProductos.Visible = false;
                    liDatosOferta.Attributes.Add("class", "active");
                    liDatosSecciones.Attributes.Add("class", "");
                    liDatosProductos.Attributes.Add("class", "");
                    liDetallesProducto.Attributes.Add("class", "");
                    break;
                case "Seccion":
                    PanelOferta.Visible = false;
                    PanelSeccion.Visible = true;
                    PanelDetalles.Visible = false;
                    PanelProductos.Visible = false;
                    liDatosOferta.Attributes.Add("class", "");
                    liDatosSecciones.Attributes.Add("class", "active");
                    liDatosProductos.Attributes.Add("class", "");
                    liDetallesProducto.Attributes.Add("class", "");
                    break;
                case "Productos":
                    PanelOferta.Visible = false;
                    PanelSeccion.Visible = false;
                    PanelDetalles.Visible = false;
                    PanelProductos.Visible = true;
                    liDatosOferta.Attributes.Add("class", "");
                    liDatosSecciones.Attributes.Add("class", "");
                    liDatosProductos.Attributes.Add("class", "active");
                    liDetallesProducto.Attributes.Add("class", "");
                    break;
                case "Detalles":
                    PanelOferta.Visible = false;
                    PanelSeccion.Visible = false;
                    PanelDetalles.Visible = true;
                    PanelProductos.Visible = false;
                    liDatosOferta.Attributes.Add("class", "");
                    liDatosSecciones.Attributes.Add("class", "");
                    liDatosProductos.Attributes.Add("class", "");
                    liDetallesProducto.Attributes.Add("class", "active");

                    DLProductoSeleccionado.SelectedIndex = -1;

                    MuestraHorasYMinutos();
                    break;

            }
        }

        protected void ObtenerProductosSeleccionados()
        {
            foreach (DataListItem item in DLProductos.Items)
            {
                CheckBox ObjectoCheckBox = (CheckBox)item.FindControl("ChkProducto");
                if (ObjectoCheckBox != null)
                {
                    if (ObjectoCheckBox.Checked == true)
                    {
                        if (!MVProducto.ListaDeProductosSeleccionados.Exists(producto => producto.UID.ToString() == DLProductos.DataKeys[item.ItemIndex].ToString()))
                        {
                            MVProducto.SeleccionDeProducto(DLProductos.DataKeys[item.ItemIndex].ToString());
                        }
                    }
                    else if (ObjectoCheckBox.Checked == false)
                    {
                        MVProducto.DesSeleccionDeProducto(DLProductos.DataKeys[item.ItemIndex].ToString());
                    }
                }
            }
        }

        protected void DLProductoSeleccionado_ItemCommand(object source, DataListCommandEventArgs e)
        {
            DataList dl = source as DataList;
            if (e == null || e.Item == null)
            {
                Trace.Write("dl_ItemCommand", "EventArgs.Item is null");
                throw new Exception("dl_ItemCommand: EventArgs.Item is null");
            }

            int selIdx = dl.SelectedIndex;



            Trace.Write("dl_ItemCommand", String.Format("{0}: {1}",
             e.CommandName.ToLower(), e.Item.ItemIndex));
            switch (e.CommandName.ToLower())
            {
                case "select":
                    selIdx = e.Item.ItemIndex;


                    break;
                case "unselect":
                    selIdx = -1;
                    break;
            }

            if (selIdx != dl.SelectedIndex)
                dl.SelectedIndex = selIdx;
            dl.DataSource = MVProducto.ListaDeProductosSeleccionados;
            dl.DataBind();

            foreach (DataListItem item in DLProductos.Items)
            {
                //Obtiene el control de checkbox del datalist
                CheckBox ObjectoCheckBox = (CheckBox)item.FindControl("ChkCategoria");
                if (ObjectoCheckBox != null)
                {
                    //Verifica que el checkbox no este seleccionado
                    if (ObjectoCheckBox.Checked == false)
                    {
                        foreach (var Producto in MVProducto.ListaDeProductosSeleccionados)
                        {
                            Guid IDPRoducto = new Guid(DLProductos.DataKeys[item.ItemIndex].ToString());
                            if (IDPRoducto == Producto.UID)
                            {
                                ObjectoCheckBox.Checked = true;
                            }
                        }
                    }
                }
            }
            Session["MVProducto"] = MVProducto;
        }

        protected void DGVSucursales_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid valor = Guid.Parse(DGVSucursales.SelectedDataKey.Value.ToString());
            MVSucursales.RecuperaCategoria(valor.ToString());
            MVSucursales.ObtenerSucursal(valor.ToString());
            txtUidSucursal.Text = MVSucursales.ID.ToString();
            lblSucursal.Text = MVSucursales.IDENTIFICADOR;

            if (txtUidSucursal.Text != string.Empty && lblSeleccionSucursal.Visible == false)
            {
                lblSeleccionSucursal.Visible = true;
            }

            if (lblSeleccionOferta.Visible == true)
            {
                lblSeleccionOferta.Visible = false;
                lblOferta.Text = string.Empty;
                txtUidOferta.Text = string.Empty;
            }

            if (lblSeleccionSeccion.Visible == true)
            {
                lblSeleccionSeccion.Visible = false;
                lblSeccion.Text = string.Empty;
                txtUidSeccion.Text = string.Empty;
            }

            //Muestra los productos asociados a la subcategoria dependiendo la subcategoria a la que esta pertenezca
            MVProducto.BuscarProductos(valor);
            DLProductos.DataSource = MVProducto.ListaDeProductos;
            DLProductos.DataBind();

            DLProductoSeleccionado.DataSource = null;
            DLProductoSeleccionado.DataBind();


            MVOferta.Buscar(UIDSUCURSAL: valor);
            CargaGrid("Oferta");

            DGVSeccion.DataSource = null;
            DGVSeccion.DataBind();



        }

        protected void DGVSucursales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(DGVSucursales, "Select$" + e.Row.RowIndex);
            }
        }

        protected void btnDatosSecciones_Click(object sender, EventArgs e)
        {
            MuestraPanel("Seccion");
        }

        protected void BtnSeleccionDeProducto_Click(object sender, EventArgs e)
        {
            MuestraPanel("Productos");
        }

        protected void BtnDetallesProducto_Click(object sender, EventArgs e)
        {
            MuestraPanel("Detalles");
        }


        #region Metodos del panel de seccion
        protected void DGVSeccion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(DGVSeccion, "Select$" + e.Row.RowIndex);

                Label ESTATUS = e.Row.FindControl("lblEstatus") as Label;
                if (e.Row.Cells[5].Text == "1")
                {
                    ESTATUS.CssClass = "glyphicon glyphicon-ok";
                    ESTATUS.ToolTip = "ACTIVO";
                }
                if (e.Row.Cells[5].Text == "2")
                {
                    ESTATUS.CssClass = "glyphicon glyphicon-remove";
                    ESTATUS.ToolTip = "INACTIVO";
                }
            }
        }

        protected void DGVSeccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valor = DGVSeccion.SelectedDataKey.Value.ToString();
            MVSeccion.Buscar(UIDSECCION: Guid.Parse(valor));
            txtUidSeccion.Text = MVSeccion.UID.ToString();
            txtSeccionNombre.Text = MVSeccion.StrNombre;
            txtHoraInicio.Text = MVSeccion.StrHoraInicio;
            txtHoraFin.Text = MVSeccion.StrHoraFin;

            ddlEstatusSeccion.SelectedIndex = ddlEstatusSeccion.Items.IndexOf(ddlEstatusSeccion.Items.FindByValue(MVSeccion.IntEstatus.ToString()));


            //Manda la el nombre de la seccion en el panel par ubicacion del usuario
            lblSeccion.Text = MVSeccion.StrNombre;

            if (txtUidSeccion.Text != string.Empty && lblSeleccionSeccion.Visible == false)
            {
                lblSeleccionSeccion.Visible = true;
                if ("-" != lblSeleccionSeccion.Text.Substring(0, 1))
                {
                    lblSeleccionSeccion.Text = "-> " + lblSeleccionSeccion.Text;
                }
            }
            LimpiaDatalistProductos();
            //Recupera los productos de la empresa
            MVSucursales.ListaDeproductos.Clear();
            MVProducto.ListaDeProductosSeleccionados.Clear();
            MVSucursales.RecuperarProductos(new Guid(txtUidSucursal.Text), valor);
            foreach (var item in MVSucursales.ListaDeproductos)
            {
                MVProducto.SeleccionDeProducto(item.UidProducto.ToString());
            }
            DLProductoSeleccionado.DataSource = MVProducto.ListaDeProductosSeleccionados;
            DLProductoSeleccionado.DataBind();
            LimpiaDatalistProductos();
            SeleccionaProductos();

            Acciones = "Edicion";
            ManejoDeControlesSeccion("Desactivado");
        }


        protected void btnModificarSeccion_Click(object sender, EventArgs e)
        {
            string strnombre = txtSeccionNombre.Text;
            string horainicio = txtHoraInicio.Text;
            string horafin = txtHoraFin.Text;
            string estatus = ddlEstatusSeccion.SelectedItem.Value;
            Guid UidOferta = new Guid(dgvoferta.SelectedDataKey.Value.ToString());

            if (string.IsNullOrEmpty(txtUidSeccion.Text))
            {
                Guid UidSeccion = Guid.NewGuid();

                MVSeccion.Guardar(UIDSECCION: UidSeccion, UIDOFERTA: UidOferta, NOMBRE: strnombre, HORAINICIO: horainicio, HORAFIN: horafin, Estatus: Int32.Parse(estatus));


                txtSeccionNombre.Text = string.Empty;
                txtHoraInicio.Text = string.Empty;
                txtHoraFin.Text = string.Empty;
                ddlEstatusSeccion.SelectedIndex = -1;
                ManejoDeControlesSeccion();
            }
            else
            {
                Guid uidseccion = Guid.Parse(txtUidSeccion.Text);
                MVSeccion.Actualiza(UIDSECCION: uidseccion, NOMBRE: strnombre, HORAINICIO: horainicio, HORAFIN: horafin, Estatus: Int32.Parse(estatus));

                Acciones = "Edicion";
                ManejoDeControlesSeccion("Desactivado");
            }
            MVSeccion.Buscar(UIDOFERTA: UidOferta);
            DGVSeccion.DataSource = MVSeccion.ListaDeSeccion;
            DGVSeccion.DataBind();



        }

        protected void btnNuevoSeccion_Click(object sender, EventArgs e)
        {
            if (txtUidOferta.Text != string.Empty)
            {
                Acciones = "NuevoRegistro";
                ManejoDeControlesSeccion();
            }
        }

        protected void btnEditarSeccion_Click(object sender, EventArgs e)
        {
            Acciones = "Edicion";
            ManejoDeControlesSeccion("Activado");
        }

        protected void btnCancelarSeccion_Click(object sender, EventArgs e)
        {
            if (txtUidSeccion.Text == string.Empty)
            {
                ManejoDeControlesSeccion();
            }
            if (txtUidSeccion.Text != string.Empty)
            {
                Acciones = "Edicion";
                ManejoDeControlesSeccion("Desactivado");
            }
        }

        protected void HabilitarControlesSeccion(bool Estatus)
        {
            txtSeccionNombre.Enabled = Estatus;
            txtHoraInicio.Enabled = Estatus;
            txtHoraFin.Enabled = Estatus;
            ddlEstatusSeccion.Enabled = Estatus;

        }

        protected void LimpiaControlesSeccion()
        {
            txtSeccionNombre.Text = string.Empty;
            txtHoraInicio.Text = string.Empty;
            txtHoraFin.Text = string.Empty;
            ddlEstatusSeccion.SelectedIndex = -1;
        }
        public void ManejoDeControlesSeccion(string ControlDeAccion = "")
        {
            if (Acciones == "NuevoRegistro")
            {
                HabilitarControlesSeccion(true);
                LimpiaControlesSeccion();
                txtUidSeccion.Text = string.Empty;
                //Visibilidad de boton guardar y cancelar
                btnModificarSeccion.Visible = true;
                btnCancelarSeccion.Visible = true;

                btnNuevoSeccion.Enabled = true;
                btnNuevoSeccion.CssClass = "btn btn-sm btn-default ";
                lblAcciones.CssClass = "glyphicon glyphicon-ok";
                btnEditarSeccion.Enabled = false;
                btnEditarSeccion.CssClass = "btn btn-sm btn-default disabled";
            }
            if (Acciones == "Edicion" && ControlDeAccion == "Activado")
            {
                HabilitarControlesSeccion(true);
                //Visibilidad de boton guardar y cancelar
                btnModificarSeccion.Visible = true;
                btnCancelarSeccion.Visible = true;

                btnNuevoSeccion.Enabled = true;
                btnNuevoSeccion.CssClass = "btn btn-sm btn-default ";
                lblAcciones.CssClass = "glyphicon glyphicon-ok";
            }
            if (Acciones == "Edicion" && ControlDeAccion == "Desactivado")
            {
                HabilitarControlesSeccion(false);
                btnEditarSeccion.Enabled = true;
                btnEditarSeccion.CssClass = "btn btn-sm btn-default";


                btnNuevoSeccion.Enabled = true;
                btnNuevoSeccion.CssClass = "btn btn-sm btn-default ";

                btnModificarSeccion.Visible = false;
                btnCancelarSeccion.Visible = false;

            }
            if (Acciones == "" && ControlDeAccion == "")
            {
                btnNuevoSeccion.Enabled = true;
                btnNuevoSeccion.CssClass = "btn btn-sm btn-default ";
                btnModificarSeccion.Visible = false;
                btnCancelarSeccion.Visible = false;

                LimpiaControlesSeccion();
                HabilitarControlesSeccion(false);

                btnEditarSeccion.Enabled = false;
                btnEditarSeccion.CssClass = "btn btn-sm btn-default disabled";

            }
        }
        #endregion

        #region metodos del panel de Detalles de producto

        /// <summary>
        /// Alimenta los dropdownlist dentro del panel de detalles de producto
        /// </summary>
        protected void MuestraHorasYMinutos()
        {
            List<int> Horas = new List<int>();
            List<int> Minutos = new List<int>();
            for (int i = 0; i < 60; i++)
            {
                if (i <= 23)
                {
                    Horas.Add(i);
                }
                if (i <= 60)
                {
                    Minutos.Add(i);
                }
            }
            DDLHoras.DataSource = Horas;
            DDLHoras.DataBind();
            DDLMinutos.DataSource = Minutos;
            DDLMinutos.DataBind();
        }
        protected void DLProductoSeleccionado_ItemCommand1(object source, DataListCommandEventArgs e)
        {
            DataList dl = source as DataList;
            if (e == null || e.Item == null)
            {
                Trace.Write("dl_ItemCommand", "EventArgs.Item is null");
                throw new Exception("dl_ItemCommand: EventArgs.Item is null");
            }

            int selIdx = dl.SelectedIndex;

            Trace.Write("dl_ItemCommand", String.Format("{0}: {1}",
             e.CommandName.ToLower(), e.Item.ItemIndex));
            switch (e.CommandName.ToLower())
            {
                case "select":
                    selIdx = e.Item.ItemIndex;
                    MVProducto.InformacionProducto(DLProductoSeleccionado.DataKeys[selIdx].ToString(), txtUidSucursal.Text);
                    txtUidProducto.Text = MVProducto.UID.ToString();


                    string Hora = "";
                    string Minuto = "";
                    if (!string.IsNullOrEmpty(MVProducto.STRTiemporElaboracion))
                    {
                        DateTime tiempo = DateTime.Parse(MVProducto.STRTiemporElaboracion);
                        Hora = tiempo.Hour.ToString();
                        Minuto = tiempo.Minute.ToString();
                    }
                    else
                    {
                        Hora = "0";
                        Minuto = "0";
                    }
                    //Carga las horas y minutos
                    MuestraHorasYMinutos();
                    //Muestra las horas guardadas
                    DDLHoras.SelectedIndex = DDLHoras.Items.IndexOf(DDLHoras.Items.FindByText(Hora));
                    //Muestra los minutos guardados
                    DDLMinutos.SelectedIndex = DDLMinutos.Items.IndexOf(DDLMinutos.Items.FindByText(Minuto));
                    txtCostoProduto.Text = MVProducto.StrCosto;
                    MVComision.ObtenerComisionPorEmpresa(new Guid(Session["UidEmpresaSistema"].ToString()));
                    decimal total = 0;
                    decimal totalPorcentaje = 0;
                    decimal valor = 0;
                    //Porcentaje
                    if (MVComision.UidTipoDeComision == new Guid("960D9483-4058-4AC9-A1C3-79F5B303E3BA"))
                    {
                        valor = decimal.Parse(MVProducto.StrCosto) / 100;
                        totalPorcentaje = MVComision.FValor * valor; ;
                        if (MVComision.BAbsorveComision)
                        {
                            total = int.Parse(MVProducto.StrCosto);
                        }
                        else
                        {
                            total = decimal.Parse(MVProducto.StrCosto) + totalPorcentaje;
                        }
                    }
                    //Comision
                    if (MVComision.UidTipoDeComision == new Guid("29875A81-7247-4CC5-821F-04A3B3C839FF"))
                    {
                        totalPorcentaje = MVComision.FValor;
                        if (MVComision.BAbsorveComision)
                        {
                            total = int.Parse(MVProducto.StrCosto);
                        }
                        else
                        {
                            total = decimal.Parse(MVProducto.StrCosto) + totalPorcentaje;
                        }
                    }
                    txtCostoComision.Text = totalPorcentaje.ToString();

                    txtCostoTotal.Text = total.ToString();
                    break;
                case "unselect":
                    selIdx = -1;
                    DDLHoras.SelectedIndex = -1;
                    //Muestra los minutos guardados
                    DDLMinutos.SelectedIndex = -1;
                    txtCostoProduto.Text = string.Empty;
                    txtCostoComision.Text = string.Empty;
                    txtCostoTotal.Text = string.Empty;
                    break;
            }
            if (selIdx != dl.SelectedIndex)
                dl.SelectedIndex = selIdx;
            dl.DataSource = MVProducto.ListaDeProductosSeleccionados;
            dl.DataBind();
        }

        #endregion

        #region Metodos del panel de oferta
        protected void dgvoferta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(dgvoferta, "Select$" + e.Row.RowIndex);

                Label ESTATUS = e.Row.FindControl("lblEstatus") as Label;
                if (e.Row.Cells[2].Text == "1")
                {
                    ESTATUS.CssClass = "glyphicon glyphicon-ok";
                    ESTATUS.ToolTip = "ACTIVO";
                }
                if (e.Row.Cells[2].Text == "2")
                {
                    ESTATUS.CssClass = "glyphicon glyphicon-remove";
                    ESTATUS.ToolTip = "INACTIVO";
                }
            }
        }
        protected void dgvoferta_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid valor = Guid.Parse(dgvoferta.SelectedDataKey.Value.ToString());
            MVOferta.Buscar(UIDOFERTA: valor);
            txtUidOferta.Text = MVOferta.UID.ToString();
            txtNombreOferta.Text = MVOferta.STRNOMBRE;
            ddldEstatusOferta.SelectedIndex = ddldEstatusOferta.Items.IndexOf(ddldEstatusOferta.Items.FindByValue(MVOferta.StrEstatus));

            //Manda el nombre de la oferta para la ubicacion del usuario  dentro del panel
            lblOferta.Text = MVOferta.STRNOMBRE;
            MVDia.ObtenerDiaOferta(valor);

            if (txtUidOferta.Text != string.Empty && lblSeleccionOferta.Visible == false)
            {
                lblSeleccionOferta.Visible = true;

                if ("-" != lblSeleccionOferta.Text.Substring(0, 1))
                {
                    lblSeleccionOferta.Text = "-> " + lblSeleccionOferta.Text;
                }
            }
            if (lblSeleccionSeccion.Visible == true)
            {
                lblSeleccionSeccion.Visible = false;
                lblSeccion.Text = string.Empty;
                txtUidSeccion.Text = string.Empty;
            }

            //Desselecciona los checkbox 
            for (int i = 0; i < chbxlistDiasOferta.Items.Count; i++)
            {
                chbxlistDiasOferta.Items[i].Selected = false;
            }
            //recorre el checkboxlist y verifica cual elemento tiene la oferta
            for (int i = 0; i < chbxlistDiasOferta.Items.Count; i++)
            {
                foreach (var item in MVDia.ListaDeDiasSeleccionados)
                {
                    if (chbxlistDiasOferta.Items[i].Value == item.UID.ToString())
                    {
                        chbxlistDiasOferta.Items[i].Selected = true;
                    }
                }
            }

            //Vacia los datalist de  los productos seleccionados.          
            DLProductoSeleccionado.DataSource = null;
            DLProductoSeleccionado.DataBind();
            //Controla la accion del panel.
            Acciones = "Edicion";
            ManejoDeControlesOferta("Desactivado");

            MVSeccion.Buscar(UIDOFERTA: valor);
            CargaGrid("Seccion");
        }
        protected void btnNuevoOferta_Click(object sender, EventArgs e)
        {
            if (txtUidSucursal.Text != string.Empty)
            {
                Acciones = "NuevoRegistro";
                ManejoDeControlesOferta();
            }
        }
        protected void btnGuardarOferta_Click(object sender, EventArgs e)
        {
            Guid UidSucursal = new Guid(txtUidSucursal.Text);
            string Nombre = txtNombreOferta.Text;
            string Estatus = ddldEstatusOferta.SelectedItem.Value;
            if (txtUidOferta.Text == string.Empty)
            {
                Guid UIDOferta = Guid.NewGuid();

                MVOferta.Guardar(UIDOferta, UidSucursal, Nombre, Estatus);
                //recorre el checkboxlist y verifica cual elemento esta seleccionado o checado
                for (int i = 0; i < chbxlistDiasOferta.Items.Count; i++)
                {
                    if (chbxlistDiasOferta.Items[i].Selected == true)
                    {
                        MVDia.RelacionDiaOferta(new Guid(chbxlistDiasOferta.Items[i].Value), UIDOferta);
                    }
                }

                ManejoDeControlesOferta();

            }
            else
            {
                Guid UIDOferta = new Guid(txtUidOferta.Text);
                if (MVOferta.Actualiza(UIDOferta, Nombre, Estatus))
                {
                    MVDia.EliminaDiaOferta(UIDOferta.ToString());
                    //recorre el checkboxlist y verifica cual elemento esta seleccionado o checado
                    for (int i = 0; i < chbxlistDiasOferta.Items.Count; i++)
                    {
                        if (chbxlistDiasOferta.Items[i].Selected == true)
                        {
                            MVDia.RelacionDiaOferta(new Guid(chbxlistDiasOferta.Items[i].Value), UIDOferta);
                        }
                    }
                }
                Acciones = "Edicion";
                ManejoDeControlesOferta("Desactivado");
            }

            Session["MVOferta"] = MVOferta;
            MVOferta.Buscar(UIDSUCURSAL: UidSucursal);
            CargaGrid("Oferta");
        }

        protected void HabilitarControlesOferta(bool estatus)
        {
            txtNombreOferta.Enabled = estatus;
            ddldEstatusOferta.Enabled = estatus;
            chbxlistDiasOferta.Enabled = estatus;
            chkSeleccionarTodosLosDias.Enabled = estatus;
        }
        protected void LimpiarControlesOferta()
        {
            txtNombreOferta.Text = string.Empty;
            ddldEstatusOferta.SelectedIndex = -1;
        }
        public void ManejoDeControlesOferta(string ControlDeAccion = "")
        {
            if (Acciones == "NuevoRegistro")
            {
                HabilitarControlesOferta(true);
                LimpiarControlesOferta();
                txtUidOferta.Text = string.Empty;
                //Visibilidad de boton guardar y cancelar
                btnGuardarOferta.Visible = true;
                btnCancelarOferta.Visible = true;

                btnEditarOferta.Enabled = false;
                btnEditarOferta.CssClass = "btn btn-sm btn-default disabled";
                lblAccionOferta.CssClass = "glyphicon glyphicon-ok";

                foreach (ListItem item in chbxlistDiasOferta.Items)
                {
                    item.Selected = false;
                }
            }
            if (Acciones == "Edicion" && ControlDeAccion == "Activado")
            {
                HabilitarControlesOferta(true);
                //Visibilidad de boton guardar y cancelar
                btnGuardarOferta.Visible = true;
                btnCancelarOferta.Visible = true;

                btnNuevoOferta.Enabled = true;
                btnNuevoOferta.CssClass = "btn btn-sm btn-default ";

                btnEditarOferta.Enabled = false;
                btnEditarOferta.CssClass = "btn btn-sm btn-default disabled";

                lblAccionOferta.CssClass = "glyphicon glyphicon-refresh";
            }
            if (Acciones == "Edicion" && ControlDeAccion == "Desactivado")
            {
                HabilitarControlesOferta(false);
                btnEditarOferta.Enabled = true;
                btnEditarOferta.CssClass = "btn btn-sm btn-default";


                btnNuevoOferta.Enabled = true;
                btnNuevoOferta.CssClass = "btn btn-sm btn-default ";

                btnGuardarOferta.Visible = false;
                btnCancelarOferta.Visible = false;

            }
            if (Acciones == "" && ControlDeAccion == "")
            {
                btnNuevoOferta.Enabled = true;
                btnNuevoOferta.CssClass = "btn btn-sm btn-default ";

                btnEditarOferta.Enabled = false;
                btnEditarOferta.CssClass = "btn btn-sm btn-default disabled";

                btnGuardarOferta.Visible = false;
                btnCancelarOferta.Visible = false;

                LimpiarControlesOferta();
                HabilitarControlesOferta(false);

                foreach (ListItem item in chbxlistDiasOferta.Items)
                {
                    item.Selected = false;
                }

            }
        }
        protected void btnEditarOferta_Click(object sender, EventArgs e)
        {
            Acciones = "Edicion";
            ManejoDeControlesOferta("Activado");
        }
        protected void btnCancelarOferta_Click(object sender, EventArgs e)
        {
            if (txtUidOferta.Text == string.Empty)
            {
                ManejoDeControlesOferta();
            }
            if (txtUidOferta.Text != string.Empty)
            {
                Acciones = "Edicion";
                ManejoDeControlesOferta("Desactivado");
            }
        }
        #endregion

        protected void CargaGrid(string GridView)
        {
            switch (GridView)
            {
                case "Oferta":
                    dgvoferta.DataSource = MVOferta.ListaDeOfertas;
                    dgvoferta.DataBind();
                    break;
                case "Seccion":
                    //Gestion de seccion
                    DGVSeccion.DataSource = MVSeccion.ListaDeSeccion;
                    DGVSeccion.DataBind();
                    break;
            }
        }
        protected void btnDatosOferta_Click(object sender, EventArgs e)
        {
            MuestraPanel("Oferta");
        }

        #region Panel Productos
        protected void seleccionaProducto()
        {
            foreach (DataListItem item in DLProductos.Items)
            {
                CheckBox ch = (CheckBox)item.FindControl("ChkProducto");
                if (ch != null)
                {
                    if (ch.Checked == true)
                    {
                        MVProducto.SeleccionDeProducto(DLProductos.DataKeys[item.ItemIndex].ToString());
                    }
                    if (ch.Checked == false)
                    {
                        MVProducto.DesSeleccionDeProducto(DLProductos.DataKeys[item.ItemIndex].ToString());
                    }
                }
            }
        }

        protected void LimpiaDatalistProductos()
        {
            foreach (DataListItem item in DLProductos.Items)
            {
                CheckBox ch = (CheckBox)item.FindControl("ChkProducto");
                ch.Checked = false;
            }

        }

        protected void SeleccionaProductos()
        {
            foreach (DataListItem item in DLProductos.Items)
            {
                CheckBox ch = (CheckBox)item.FindControl("ChkProducto");
                if (ch != null)
                {
                    if (MVProducto.ListaDeProductosSeleccionados.Exists(Producto => Producto.UID.ToString() == DLProductos.DataKeys[item.ItemIndex].ToString()))
                    {
                        ch.Checked = true;
                    }
                }
            }
        }

        protected void btnGuardarSeleccionproducto_Click(object sender, EventArgs e)
        {
            seleccionaProducto();

            Guid uidseccion = new Guid(txtUidSeccion.Text);

            if (MVProducto.ListaDeProductosSeleccionados != null)
            {
                MVProducto.BuscarProductos(new Guid(txtUidSucursal.Text));

                foreach (var productos in MVProducto.ListaDeProductos)
                {
                    //Si existe el producto seleccionado en los productos de la sucursal y el registro no exista en la base de datos
                    if (MVProducto.ListaDeProductosSeleccionados.Exists(seleccionado => productos.UID == seleccionado.UID))
                    {
                        foreach (var item in MVProducto.ListaDeProductosSeleccionados)
                        {
                            if (MVSeccion.EncuentraRegistro(item.UID, uidseccion) == 0)
                            {
                                MVSeccion.RelacionConProducto(uidseccion, item.UID);
                            }
                        }
                    }
                    else
                    //Si el producto seleccinado no coincide
                    if (!MVProducto.ListaDeProductosSeleccionados.Exists(seleccionado => productos.UID == seleccionado.UID))
                    {
                        MVSeccion.eliminaProductos(productos.UID, uidseccion);
                    }
                }

            }
            HabilitaControlesProductos(false);
        }

        protected void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            string nombre = txtBusquedaNombre.Text;
            Guid sucursal = new Guid(txtUidSucursal.Text);
            MVProducto.BuscarProductos(sucursal, nombre);
            DLProductos.DataSource = MVProducto.ListaDeProductos;
            DLProductos.DataBind();
            SeleccionaProductos();
        }

        protected void HabilitaControlesProductos(bool estatus)
        {
            DLProductos.Enabled = estatus;
            txtBusquedaNombre.Enabled = estatus;
            btnGuardarSeleccionproducto.Visible = estatus;
            btnCancelarSeleccionProducto.Visible = estatus;
        }

        protected void btnSeleccionarProducto_Click(object sender, EventArgs e)
        {
            if (txtUidSeccion.Text != string.Empty)
            {
                HabilitaControlesProductos(true);
            }
        }

        protected void btnCancelarSeleccionProducto_Click(object sender, EventArgs e)
        {
            HabilitaControlesProductos(false);
        }
        #endregion

        protected void EstatusControlesInformacionProducto(bool estatus)
        {
            DDLHoras.Enabled = estatus;
            DDLMinutos.Enabled = estatus;
            txtCostoProduto.Enabled = estatus;
            btnModificarProducto.Visible = estatus;
            btnCancelarProducto.Visible = estatus;

        }

        protected void btnEditarProducto_Click(object sender, EventArgs e)
        {
            if (DLProductoSeleccionado.SelectedIndex != -1)
            {
                EstatusControlesInformacionProducto(true);
            }
            //MEnsaje de validacion
        }

        protected void btnCancelarProducto_Click(object sender, EventArgs e)
        {
            EstatusControlesInformacionProducto(false);
        }

        protected void btnModificarProducto_Click(object sender, EventArgs e)
        {
            string Costo = txtCostoProduto.Text;
            string tiempo = DDLHoras.SelectedItem.Text + ":" + DDLMinutos.SelectedItem.Text;
            MVProducto.ActualizarProducto(txtUidProducto.Text, tiempo, Costo, txtUidSeccion.Text);
            EstatusControlesInformacionProducto(false);
        }

        protected void chkSeleccionarTodosLosDias_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionarTodosLosDias.Checked)
            {
                foreach (ListItem item in chbxlistDiasOferta.Items)
                {
                    item.Selected = true;
                }
            }
            else if (!chkSeleccionarTodosLosDias.Checked)
            {
                foreach (ListItem item in chbxlistDiasOferta.Items)
                {
                    item.Selected = false;
                }
            }
        }

        protected void txtCostoProduto_TextChanged(object sender, EventArgs e)
        {
            float resultado = 0;

            if (float.TryParse(txtCostoProduto.Text, out resultado))
            {
                MVComision.ObtenerComisionPorEmpresa(new Guid(Session["UidEmpresaSistema"].ToString()));
                float total = 0f;
                var totalPorcentaje = 0f;
                //Porcentaje
                if (MVComision.UidTipoDeComision == new Guid("960D9483-4058-4AC9-A1C3-79F5B303E3BA"))
                {
                    totalPorcentaje = (100 / MVComision.FValor) * resultado;
                    if (MVComision.BAbsorveComision)
                    {
                        total = resultado;
                    }
                    else
                    {
                        total = resultado + totalPorcentaje;
                    }
                }
                //Comision
                if (MVComision.UidTipoDeComision == new Guid("29875A81-7247-4CC5-821F-04A3B3C839FF"))
                {
                    totalPorcentaje = MVComision.FValor;
                    if (MVComision.BAbsorveComision)
                    {

                        total = resultado;
                    }
                    else
                    {
                        total = resultado + totalPorcentaje;
                    }
                }
                txtCostoComision.Text = totalPorcentaje.ToString();
                txtCostoTotal.Text = total.ToString();
            }
            else
            {
                txtCostoComision.BorderColor = Color.Red;
            }
        }
    }
}