using Subgurim.Controles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using VistaDelModelo;

namespace WebApplication1.Vista
{
    public partial class Tarifario : System.Web.UI.Page
    {
        VMDireccion MVDireccion = new VMDireccion();
        VMTarifario MVTarifario = new VMTarifario();
        VMSucursales MVSucursales = new VMSucursales();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UidEmpresaSistema"] != null)
            {
                if (!IsPostBack)
                {
                    Session["MVDireccion"] = MVDireccion;
                    Session["MVTarifario"] = MVTarifario;
                    Session["MVSucursales"] = MVSucursales;
                    #region Filtros
                    MVSucursales.DatosGridViewBusquedaNormal(Session["UidEmpresaSistema"].ToString());
                    CargaGrid("Sucursal");
                    #endregion
                    #region Zona de servicio

                    //Obtiene el pais para el dropdownlist
                    DDLZonaPais.DataSource = MVDireccion.Paises();
                    DDLZonaPais.DataTextField = "Nombre";
                    DDLZonaPais.DataValueField = "UidPais";
                    DDLZonaPais.DataBind();
                    //Comienza en vacio el GridView de las ciudades
                    DGVZonaCiudades.DataSource = null;
                    DGVZonaCiudades.DataBind();
                    //Limpia la lista de checkbox
                    DeseleccionaCheckboxListColoniasEntrega();
                    DeseleccionaCheckboxListColoniasRecolecta();
                    #endregion

                    #region Zona de recolecta
                    DDLZRPais.DataSource = MVDireccion.Paises();
                    DDLZRPais.DataTextField = "Nombre";
                    DDLZRPais.DataValueField = "UidPais";
                    DDLZRPais.DataBind();

                    SeleccionaPanel("Recolecta");
                    //Oculta el mensaje
                    PanelMensaje.Visible = false;
                    activaEdicion(false, "Informacion");
                    activaEdicion(false, "Gestion");
                    lblUidSucursal.Text = string.Empty;
                    #endregion
                }
                else
                {
                    MVDireccion = (VMDireccion)Session["MVDireccion"];
                    MVTarifario = (VMTarifario)Session["MVTarifario"];
                    MVSucursales = (VMSucursales)Session["MVSucursales"];
                }

            }
            else
            {
                Response.Redirect("Default/default.aspx");
            }


        }

        protected void CargaGrid(string Grid)
        {
            switch (Grid)
            {
                case "Sucursal":
                    DgvSucursales.DataSource = MVSucursales.LISTADESUCURSALES;
                    DgvSucursales.DataBind();
                    break;
                default:
                    break;
            }
        }

        #region GridView busqueda
        protected void DgvSucursales_SelectedIndexChanged(object sender, EventArgs e)
        {


            //AccionesDeLaPagina = string.Empty;
            //textboxActivados();

            string valor = DgvSucursales.SelectedDataKey.Value.ToString();

            //btnEditar.CssClass = "btn btn-sm btn-default";
            //btnEditar.Enabled = true;


            CargaTarifario(valor);
            activaEdicion(true, "Informacion");

        }
        /// <summary>
        /// Controla el estatus de los controles de la vista
        /// </summary>
        /// <param name="Estatus"></param>
        protected void activaEdicion(bool Estatus, string tipo)
        {
            switch (tipo)
            {
                case "Informacion":
                    if (Estatus)
                    {
                        btnEditar.Enabled = true;
                        btnEditar.CssClass = "btn btn-sm btn-default";
                    }
                    else if (!Estatus)
                    {
                        btnEditar.Enabled = false;
                        btnEditar.CssClass = "btn btn-sm btn-default disabled";
                    }
                    break;
                case "Gestion":
                    if (Estatus)
                    {
                        btnGuardar.Visible = true;
                        btnCancelar.Visible = true;
                        btnAgregarCiudad.Enabled = true;
                        btnZRAgregaCiudad.Enabled = true;
                        btnAgregarCiudad.CssClass = "btn btn-sm btn-success";
                        btnZRAgregaCiudad.CssClass = "btn btn-sm btn-success";
                    }
                    else if (!Estatus)
                    {
                        btnGuardar.Visible = false;
                        btnCancelar.Visible = false;
                        btnZRAgregaCiudad.Enabled = false;
                        btnAgregarCiudad.CssClass = "btn btn-sm btn-success disabled";
                        btnZRAgregaCiudad.CssClass = "btn btn-sm btn-success disabled";
                    }
                    break;
                default:
                    break;
            }

        }
        protected void DgvSucursales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow PagerRow = DgvSucursales.TopPagerRow;
                Session["MVSucursales"] = MVSucursales;
                //Label Registros = PagerRow.Cells[0].FindControl("lblTotalDeRegistros") as Label;
                //ImageButton DobleDerecho = PagerRow.Cells[0].FindControl("btnUltimo") as ImageButton;
                //ImageButton DobleIzquierdo = PagerRow.Cells[0].FindControl("btnPrimero") as ImageButton;
                //ImageButton Izquierda = PagerRow.Cells[0].FindControl("btnAnterior") as ImageButton;
                //ImageButton Derecha = PagerRow.Cells[0].FindControl("btnSiguiente") as ImageButton;
                //DropDownList PaginasBusquedaNormal = PagerRow.Cells[0].FindControl("DDLDNUMERODEPAGINAS") as DropDownList;

                //int PaginaActual = DgvSucursales.PageIndex + 1;
                //int Total = DgvSucursales.PageCount;

                //if (Registros != null)
                //{
                //    //Limpia la lista de elementos asociada al dropdownlist de la paginacion de la busqueda normal.
                //    PaginasBusquedaNormal.Items.Clear();
                //    for (int i = 0; i < Total; i++)
                //    {
                //        int Pagina = i + 1;
                //        ListItem item = new ListItem(Pagina.ToString());
                //        if (i == DgvSucursales.PageIndex)
                //        {
                //            item.Selected = true;
                //        }
                //        PaginasBusquedaNormal.Items.Add(item);
                //    }

                //    if (PaginaActual == 1 && PaginaActual != Total)
                //    {
                //        DobleIzquierdo.Enabled = false;
                //        Izquierda.Enabled = false;
                //        DobleIzquierdo.Visible = true;
                //        Izquierda.Visible = true;
                //        DobleIzquierdo.CssClass = "disabled";
                //        Izquierda.CssClass = "disabled";

                //        DobleDerecho.Enabled = true;
                //        Derecha.Enabled = true;
                //        DobleDerecho.CssClass = " ";
                //        Derecha.CssClass = " ";
                //    }
                //    else if (PaginaActual != 1 && PaginaActual == Total)
                //    {
                //        DobleIzquierdo.Enabled = true;
                //        Izquierda.Enabled = true;
                //        DobleIzquierdo.Visible = true;
                //        Izquierda.Visible = true;
                //        DobleIzquierdo.CssClass = " ";
                //        Izquierda.CssClass = " ";

                //        DobleDerecho.Enabled = false;
                //        Derecha.Enabled = false;
                //        DobleDerecho.CssClass = "disabled";
                //        Derecha.CssClass = "disabled";
                //        DobleDerecho.Visible = true;
                //        Derecha.Visible = true;
                //    }
                //    else if (DgvSucursales.PageSize >= MVSucursales.LISTADESUCURSALES.Count)
                //    {
                //        DobleIzquierdo.Enabled = false;
                //        Izquierda.Enabled = false;
                //        DobleIzquierdo.Visible = false;
                //        Izquierda.Visible = false;
                //        DobleIzquierdo.CssClass = "disabled";
                //        Izquierda.CssClass = "disabled";

                //        DobleDerecho.Enabled = false;
                //        Derecha.Enabled = false;
                //        DobleDerecho.Visible = false;
                //        Derecha.Visible = false;
                //        DobleDerecho.CssClass = "disabled";
                //        Derecha.CssClass = "disabled";

                //    }
                //    else
                //    {
                //        DobleIzquierdo.Enabled = true;
                //        Izquierda.Enabled = true;
                //        DobleIzquierdo.Visible = true;
                //        Izquierda.Visible = true;
                //        DobleIzquierdo.CssClass = " ";
                //        Izquierda.CssClass = " ";

                //        DobleDerecho.Enabled = true;
                //        Derecha.Enabled = true;
                //        DobleDerecho.Visible = true;
                //        Derecha.Visible = true;
                //        DobleDerecho.CssClass = " ";
                //        Derecha.CssClass = " ";
                //    }

                //    int RegistroFinal = (DgvSucursales.PageIndex + 1) * (DgvSucursales.Rows.Count + 1);
                //    int RegistroInicial = (((DgvSucursales.PageIndex + 1) * DgvSucursales.PageSize) - DgvSucursales.PageSize) + 1;
                //    string CantidadDeRegistros = MVSucursales.LISTADESUCURSALES.Count.ToString();


                //    Registros.Text = RegistroInicial + " - " + RegistroFinal + " de " + CantidadDeRegistros;
                //}
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(DgvSucursales, "Select$" + e.Row.RowIndex);

            }
        }
        protected void CargaTarifario(string Sucursal)
        {
            MVTarifario.ListaDeTarifarios.Clear();
            lblUidSucursal.Text = Sucursal;
            //Si es suministradora lo obtiene por el contrato
            //if (MVEmpresa.ObtenerTipoDeEmpresa(Session["UidEmpresaSistema"].ToString()))
            //{
            //    MVTarifario.ListaDeTarifariosSeleccionados.Clear();
            //    foreach (var item in MVContrato.ListaDeSucursalesEnContrato)
            //    {
            //        MVTarifario.BuscarTarifario(TipoDeBusqueda: "Contrato", contrato: item.Uid.ToString());
            //    }
            //}
            //else //Si es por distribuidora solo por el uid trae los tarifarios
            //{
            MuestraZonaDeServicio(new Guid(Sucursal));
            MVTarifario.BuscarTarifario("Gestion", uidSucursal: Sucursal);
            CrearGridViewTarifario(MVTarifario.ListaDeTarifarios);

            //}
        }
        protected void MuestraZonaDeServicio(Guid uidsucursal = new Guid())
        {
            //Limpia la lista de checkbox
            //DeseleccionaCheckboxListColonias();
            //Recupera la zona de servicio
            MVSucursales.RecuperaZonaEntrega(uidsucursal);
            MVSucursales.RecuperaZonaRecoleccion(uidsucursal);
            //limpia las ciudades seleccionadas
            MVDireccion.ListaCiudadesSeleccionadasEntrega.Clear();
            MVDireccion.ListaColoniasSeleccionadasEntrega.Clear();
            MVDireccion.ListaCiudadesSeleccionadasRecolecta.Clear();
            MVDireccion.ListaColoniasSeleccionadasRecolecta.Clear();
            for (int i = 0; i < MVSucursales.ListaDeColoniasEntrega.Count; i++)
            {
                MVDireccion.SeleccionarCiudadEntrega(MVSucursales.ListaDeColoniasEntrega[i].ID);
                MVDireccion.SeleccionarColoniaEntrega(MVSucursales.ListaDeColoniasEntrega[i].UidRelacionRegistro, MVSucursales.ListaDeColoniasEntrega[i].UidColonia, MVSucursales.ListaDeColoniasEntrega[i].ID, MVSucursales.ListaDeColoniasEntrega[i].StrNombreColonia);
            }
            for (int i = 0; i < MVSucursales.ListaDeColoniasRecolecta.Count; i++)
            {
                MVDireccion.SeleccionarCiudadRecolecta(MVSucursales.ListaDeColoniasRecolecta[i].ID);
                MVDireccion.SeleccionarColoniaRecolecta(MVSucursales.ListaDeColoniasRecolecta[i].UidRelacionRegistro, MVSucursales.ListaDeColoniasRecolecta[i].UidColonia, MVSucursales.ListaDeColoniasRecolecta[i].ID, MVSucursales.ListaDeColoniasRecolecta[i].StrNombreColonia);
            }
            //Recarga el gridview de las ciudades asociadas
            DGVZonaCiudades.DataSource = MVDireccion.ListaCiudadesSeleccionadasEntrega;
            DGVZonaCiudades.DataBind();
            DGVZRCiudades.DataSource = MVDireccion.ListaCiudadesSeleccionadasRecolecta;
            DGVZRCiudades.DataBind();

        }

        #endregion
        #region Filtros
        protected void MostrarYOcultarFiltrosBusquedaNormal(object sender, EventArgs e)
        {
            VisivilidadDeFiltros("Normal");
        }

        protected void VisivilidadDeFiltros(string Filtro)
        {
            switch (Filtro)
            {
                case "Normal":

                    if (lblVisibilidadfiltros.Text == " Mostrar")
                    {
                        lblVisibilidadfiltros.Text = " Ocultar";
                        btnBuscar.Enabled = true;
                        PnlFiltros.Visible = true;
                        btnBorrarFiltros.Enabled = true;
                        btnBuscar.CssClass = "btn btn-sm btn-default";
                        btnBorrarFiltros.CssClass = "btn btn-sm btn-default";

                    }
                    else if (lblVisibilidadfiltros.Text == " Ocultar")
                    {
                        lblVisibilidadfiltros.Text = " Mostrar";
                        btnBuscar.Enabled = false;
                        btnBuscar.CssClass = "btn btn-sm btn-default disabled";
                        btnBorrarFiltros.CssClass = "btn btn-sm btn-default disabled";
                        PnlFiltros.Visible = false;
                        btnBorrarFiltros.Enabled = false;
                    }

                    break;
                default:
                    break;
            }

        }

        protected void VaciarFiltros(object sender, EventArgs e)
        {
            LimpiaFiltros();
        }

        protected void LimpiaFiltros()
        {
            txtFIdentificador.Text = string.Empty;
            txtFHoraApertura.Text = string.Empty;
            txtFHoraCierre.Text = string.Empty;

        }
        #endregion
        #region Tarifario
        protected System.Data.DataTable CreaTarifario(List<VMTarifario> ListaDePrecios)
        {
            var tabla = new System.Data.DataTable();
            //Creacion de columnas
            foreach (var item in ListaDePrecios.OrderBy(x => x.StrNombreColoniaZE).ToList())
            {
                if (tabla.Columns.Count == 0)
                {
                    tabla.Columns.Add("Recolecta\\Entrega", typeof(string));
                    tabla.Columns.Add(item.StrNombreColoniaZE, typeof(string));
                }
                else
                {
                    if (!tabla.Columns.Contains(item.StrNombreColoniaZE))
                    {
                        tabla.Columns.Add(item.StrNombreColoniaZE, typeof(string));
                    }
                }
            }
            //Creacion del contenido
            var registros = new List<VMTarifario>();
            for (int i = 0; i < MVDireccion.ListaColoniasSeleccionadasRecolecta.Count; i++)
            {
                MVDireccion.ListaColoniasSeleccionadasRecolecta.OrderBy(x => x.NOMBRECOLONIA).ToList();
                if (!registros.Exists(x => x.UidRelacionZR != MVDireccion.ListaColoniasSeleccionadasRecolecta[i].UidRegistro))
                {
                    registros = ListaDePrecios.FindAll(T => T.UidRelacionZR == MVDireccion.ListaColoniasSeleccionadasRecolecta[i].UidRegistro);
                    var Row = tabla.NewRow();
                    tabla.Rows.Add(Row);
                    registros.OrderBy(x => x.StrNombreColoniaZE).ToList();

                    for (int p = 0; p < registros.Count; p++)
                    {
                        var campo = "";
                        if ((p + 1) < tabla.Columns.Count)
                        {
                            campo = tabla.Columns[p + 1].ColumnName;
                        }
                        var row = tabla.Rows.Count - 1;

                        if (p == 0)
                        {
                            tabla.Rows[row][p] = registros[p].StrNombreColoniaZR + "," + registros[p].UidRelacionZR;
                            VMTarifario registro = registros.Find(x => x.StrNombreColoniaZE == campo);
                            if (registro != null)
                            {
                                tabla.Rows[row][p + 1] = registro.DPrecio.ToString() + "," + registro.UidTarifario + "," + registro.StrNombreColoniaZR;
                            }
                            else
                            {
                                tabla.Columns.RemoveAt(p);
                            }
                        }
                        else if ((p + 1) < registros.Count)
                        {
                            VMTarifario registro = registros.Find(x => x.StrNombreColoniaZE == campo);
                            if (registro != null)
                            {
                                tabla.Rows[row][p + 1] = registro.DPrecio.ToString() + "," + registro.UidTarifario + "," + registro.StrNombreColoniaZR;
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(campo))
                                {
                                    tabla.Columns.RemoveAt(p);
                                }
                            }
                        }
                    }
                    registros.Clear();
                }
            }
            return tabla;
        }

        protected void CrearGridViewTarifario(List<VMTarifario> Precios)
        {


            for (int i = 0; DGVTarifario.Columns.Count > i;)
            {
                DGVTarifario.Columns.RemoveAt(i);
            }
            System.Data.DataTable t = CreaTarifario(Precios);
            int r = 1;

            DGVTarifario.DataSource = t;
            DGVTarifario.DataBind();
            foreach (TableRow item in DGVTarifario.Rows)
            {
                int j = 0;
                double precio;
                Guid newGuid;
                foreach (TableCell celda in item.Cells)
                {

                    TemplateField campo = new TemplateField();
                    string ColumnName = celda.Text.ToString();


                    TableCell tc = new TableCell();

                    campo.HeaderStyle.HorizontalAlign = HorizontalAlign.Justify;
                    campo.HeaderText = ColumnName;


                    if (!DGVTarifario.Columns.Equals(ColumnName))
                    {
                        DGVTarifario.Columns.Add(campo);
                    }

                    if (j != 0)
                    {
                        string[] ColumnValues = celda.Text.Split(new char[] { ',' });
                        string ColumnValue = string.Empty;
                        string ColumnaName = string.Empty;
                        string FilaZonaRecolecta = string.Empty;
                        foreach (string columnas in ColumnValues)
                        {
                            if (Guid.TryParse(columnas, out newGuid))
                            {
                                ColumnValue = columnas;
                            }
                            else if (double.TryParse(columnas, out precio))
                            {
                                ColumnaName = columnas;
                            }
                            else
                            {
                                FilaZonaRecolecta = columnas;
                            }
                        }

                        Label lblUid = new Label();
                        lblUid.Text = ColumnValue;
                        lblUid.ID = "UidRegistro" + (r + j).ToString();
                        lblUid.Visible = false;
                        celda.Controls.Add(lblUid);

                        string Id = "txt," + r + "," + j;
                        TextBox txtBox = new TextBox();
                        txtBox.ID = Id;
                        txtBox.Text = ColumnaName;
                        txtBox.TextChanged += new EventHandler(CambiarTexto);
                        txtBox.AutoPostBack = true;
                        txtBox.Width = 100;
                        txtBox.CssClass = "form-control text-center";
                        celda.Controls.Add(txtBox);

                        CompareValidator cv = new CompareValidator(); // Create validator and configure
                        cv.SetFocusOnError = true;
                        cv.Operator = ValidationCompareOperator.GreaterThan;
                        cv.ValueToCompare = "-1";
                        cv.Type = ValidationDataType.Double;
                        cv.Display = ValidatorDisplay.Dynamic;
                        cv.ErrorMessage = "<br/>Campo no valido";
                        cv.ForeColor = Color.Red;
                        cv.ControlToValidate = txtBox.ID;
                        celda.Controls.Add(cv);
                        //Create and add AsyncPostBackTrigger
                        AsyncPostBackTrigger APBT_trig = new AsyncPostBackTrigger();
                        APBT_trig.EventName = "TextChanged";
                        APBT_trig.ControlID = txtBox.UniqueID;
                        //UPSucursales.Triggers.Add(APBT_trig);

                        j++;
                    }
                    else
                    {
                        string[] ColumnValues = celda.Text.Split(new char[] { ',' });
                        string ColumnValue = string.Empty;
                        foreach (string columnas in ColumnValues)
                        {
                            if (Guid.TryParse(columnas, out newGuid))
                            {
                                ColumnValue = columnas;
                            }
                            else
                            {
                                ColumnName = columnas;
                            }
                        }


                        Label etiqueta = new Label();
                        etiqueta.ID = "LblUidZonaRecolecta";
                        etiqueta.Text = ColumnValue;
                        etiqueta.Visible = false;
                        celda.Controls.Add(etiqueta);

                        Label Nombre = new Label();
                        Nombre.Text = ColumnName;

                        Nombre.Visible = true;
                        celda.Controls.Add(Nombre);


                        j++;
                    }
                }
                r++;
            }
        }

        protected void CambiarTexto(object Sender, EventArgs e)
        {
            TextBox txt = Sender as TextBox;
            var t = txt.Text;
            var y = txt.ID;
            string[] valores = y.Split(new char[] { ',' });
            if (lblCelda.Text != valores[2])
            {
                lblFila.Text = valores[1];
                lblCelda.Text = valores[2];
                lblPrecio.Text = t;
            }
        }
        protected void GuardaTarifario()
        {
            string UidZonaRecolecta = "";
            string precio = "";
            string ZonaEntrega = "";
            // verificar que la lista no se cree de nuevo al momento del postback porque le cambia los id a la lista y no deja actualizar
            foreach (GridViewRow item in DGVTarifario.Rows)
            {
                int number = 0;
                foreach (TableCell celda in item.Cells)
                {
                    if (number == 0)
                    {
                        var UidZR = celda.FindControl("LblUidZonaRecolecta") as Label;
                        if (UidZR != null)
                        {
                            UidZonaRecolecta = UidZR.Text;
                        }

                    }
                    else
                    {
                        if (celda.Controls.Count > 0)
                        {
                            var txtPrecio = celda.Controls[1] as TextBox;
                            var lblUidRegistro = celda.Controls[0] as Label;
                            ZonaEntrega = lblUidRegistro.Text;
                            precio = txtPrecio.Text;
                        }

                    }
                    if (!string.IsNullOrEmpty(UidZonaRecolecta) && !string.IsNullOrEmpty(precio) && !string.IsNullOrEmpty(ZonaEntrega))
                    {
                        MVTarifario.ActualizaLista(UidZonaRecolecta, precio, ZonaEntrega);
                    }
                    number++;
                }
            }
        }

        protected void BtnCopiarTarifarioArriba_Click(object sender, EventArgs e)
        {
            if (lblFila.Text == (1).ToString())
            {
                PanelMensaje.Visible = true;
                LblMensaje.Text = "No se puede copiar la primera fila hacia arriba";
            }
            else
            {
                var fila = int.Parse(lblFila.Text);
                for (int i = 1; i < DGVTarifario.Rows[fila].Cells.Count; i++)
                {
                    TextBox TxtCopiado = DGVTarifario.Rows[fila].Cells[i].Controls[1] as TextBox;
                    TextBox txtAsignarValor = DGVTarifario.Rows[fila + 1].Cells[i].Controls[1] as TextBox;
                }
            }
        }

        protected void BtnCopiarTarifarioAbajo_Click(object sender, EventArgs e)
        {
            if (lblFila.Text == DGVTarifario.Rows.Count.ToString())
            {
                PanelMensaje.Visible = true;
                LblMensaje.Text = "No se puede copiar la ultima fila hacia abajo";
            }
            else
            {
                var fila = int.Parse(lblFila.Text);
                for (int i = 1; i < DGVTarifario.Rows[fila].Cells.Count; i++)
                {
                    TextBox TxtCopiado = DGVTarifario.Rows[fila].Cells[i].Controls[1] as TextBox;
                    TextBox txtAsignarValor = DGVTarifario.Rows[fila - 1].Cells[i].Controls[1] as TextBox;
                }
            }
        }

        protected void BtnCopiarDerecha_Click(object sender, EventArgs e)
        {
            var fila = int.Parse(lblFila.Text);
            var celda = int.Parse(lblCelda.Text);
            if (lblCelda.Text == (DGVTarifario.Columns.Count).ToString())
            {
                PanelMensaje.Visible = true;
                LblMensaje.Text = "No se puede copiar la primera columna hacia la derecha";
            }
            else
            {
                for (int i = 0; i < DGVTarifario.Rows.Count; i++)
                {
                    TextBox TxtCopiado = DGVTarifario.Rows[i].Cells[celda].Controls[1] as TextBox;
                    TextBox txtAsignarValor = DGVTarifario.Rows[i].Cells[celda + 1].Controls[1] as TextBox;
                    txtAsignarValor.Text = TxtCopiado.Text;
                }
            }
        }

        protected void BtnCopiarIzquierda_Click(object sender, EventArgs e)
        {
            var fila = int.Parse(lblFila.Text);
            var celda = int.Parse(lblCelda.Text);
            if (lblCelda.Text == (1).ToString())
            {
                PanelMensaje.Visible = true;
                LblMensaje.Text = "No se puede copiar la primera columna hacia la izquierda";
            }
            else
            {
                for (int i = 0; i < DGVTarifario.Rows.Count; i++)
                {
                    TextBox TxtCopiado = DGVTarifario.Rows[i].Cells[celda].Controls[1] as TextBox;
                    TextBox txtAsignarValor = DGVTarifario.Rows[i].Cells[celda - 1].Controls[1] as TextBox;
                    txtAsignarValor.Text = TxtCopiado.Text;
                }
            }
        }

        protected void BtnCopiarTodaLaTabla_Click(object sender, EventArgs e)
        {
            var precio = lblPrecio.Text;
            for (int i = 0; i < DGVTarifario.Rows.Count; i++)
            {
                for (int j = 1; j < DGVTarifario.Rows[i].Cells.Count; j++)
                {
                    TextBox TxtCopiado = DGVTarifario.Rows[i].Cells[j].Controls[1] as TextBox;
                    TxtCopiado.Text = precio;
                }
            }
        }


        protected void BtnTarifario_Click(object sender, EventArgs e)
        {

            MuestraPanel("Zona de servicio");
            SeleccionaPanel("Tarifario");

            ColoniasSeleccionadas();
            for (int i = 0; i < MVDireccion.ListaColoniasSeleccionadasRecolecta.Count; i++)
            {
                for (int j = 0; j < MVDireccion.ListaColoniasSeleccionadasEntrega.Count; j++)
                {
                    MVTarifario.AgregaALista(MVDireccion.ListaColoniasSeleccionadasEntrega[j].UidRegistro, UidZonaRecoleccion: MVDireccion.ListaColoniasSeleccionadasRecolecta[i].UidRegistro, NombreZE: MVDireccion.ListaColoniasSeleccionadasEntrega[j].NOMBRECOLONIA, NombreZR: MVDireccion.ListaColoniasSeleccionadasRecolecta[i].NOMBRECOLONIA);
                }
            }

            for (int i = 0; i < MVTarifario.ListaDeTarifarios.Count; i++)
            {
                if (!MVDireccion.ListaColoniasSeleccionadasEntrega.Exists(o => o.UidRegistro == MVTarifario.ListaDeTarifarios[i].UidRelacionZE))
                {
                    var obj = MVTarifario.ListaDeTarifarios[i];
                    MVTarifario.ListaDeTarifarios.Remove(obj);
                }
            }
            CrearGridViewTarifario(MVTarifario.ListaDeTarifarios);
        }

        protected void btnDatosZonaDeServicio_Click(object sender, EventArgs e)
        {

            if (DGVTarifario.Visible)
            {
                GuardaTarifario();
            }

            ColoniasSeleccionadas();

            SeleccionaPanel("Entrega");

        }
        protected void SeleccionaPanel(string Panel)
        {
            switch (Panel)
            {
                case "Entrega":
                    liZonaDeRecolecta.Attributes.Add("class", "");
                    liDatosZonaDeEntrega.Attributes.Add("class", "active");
                    liDatosTarifario.Attributes.Add("class", "");

                    PanelZonasServicio.Visible = true;
                    PanelZonaDeRecolecta.Visible = false;
                    PanelTarifario.Visible = false;
                    break;
                case "Recolecta":
                    liDatosZonaDeEntrega.Attributes.Add("class", "");
                    liDatosTarifario.Attributes.Add("class", "");
                    liZonaDeRecolecta.Attributes.Add("class", "active");

                    PanelZonasServicio.Visible = false;
                    PanelTarifario.Visible = false;
                    PanelZonaDeRecolecta.Visible = true;
                    break;
                case "Tarifario":
                    liDatosZonaDeEntrega.Attributes.Add("class", "");
                    liDatosTarifario.Attributes.Add("class", "active");
                    liZonaDeRecolecta.Attributes.Add("class", "");

                    PanelZonasServicio.Visible = false;
                    PanelZonaDeRecolecta.Visible = false;
                    PanelTarifario.Visible = true;
                    break;
                default:
                    liZonaDeRecolecta.Attributes.Add("class", "");
                    liDatosZonaDeEntrega.Attributes.Add("class", "active");
                    liDatosTarifario.Attributes.Add("class", "");

                    PanelZonasServicio.Visible = true;
                    PanelZonaDeRecolecta.Visible = false;
                    PanelTarifario.Visible = false;
                    break;
            }
        }
        protected void btnZonaDeRecolecta_Click(object sender, EventArgs e)
        {
            if (DGVTarifario.Visible)
            {
                GuardaTarifario();
            }
            ColoniasSeleccionadas();

            SeleccionaPanel("Recolecta");
        }
        protected void BtnZonaDeServicio_Click(object sender, EventArgs e)
        {
            //Este es el metodo del boton del menu 
            MuestraPanel("Zona de servicio");

            ColoniasSeleccionadas();

            SeleccionaPanel("Entrega");

        }
        protected void ObtenerEstado(object sender, EventArgs e)
        {
            DropDownList objeto = sender as DropDownList;
            switch (objeto.ID)
            {
                case "DDLZonaPais":
                    if (DDLZonaPais.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        MuestraEstados(DDLZonaPais.SelectedItem.Value.ToString(), "Zona de servicio");
                        MuestraMunicipio("00000000-0000-0000-0000-000000000000", "Zona de servicio");
                        MuestraCiudad("00000000-0000-0000-0000-000000000000", "Zona de servicio");
                        MuestraColonia("00000000-0000-0000-0000-000000000000", "Zona de servicio");
                    }
                    break;
                case "DDLZRPais":
                    if (DDLZRPais.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        MuestraEstados(DDLZRPais.SelectedItem.Value.ToString(), "Recolecta");
                        MuestraMunicipio("00000000-0000-0000-0000-000000000000", "Recolecta");
                        MuestraCiudad("00000000-0000-0000-0000-000000000000", "Recolecta");
                        MuestraColonia("00000000-0000-0000-0000-000000000000", "Recolecta");
                    }
                    break;
            }
        }
        protected void ObtenerMunicipio(object sender, EventArgs e)
        {
            DropDownList objeto = sender as DropDownList;
            switch (objeto.ID)
            {
                case "DDLZonaEstado":
                    if (DDLZonaEstado.SelectedItem != null)
                    {
                        if (DDLZonaEstado.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
                        {
                            MuestraMunicipio(DDLZonaEstado.SelectedItem.Value.ToString(), "Zona de servicio");
                            MuestraCiudad("00000000-0000-0000-0000-000000000000", "Zona de servicio");
                            MuestraColonia("00000000-0000-0000-0000-000000000000", "Zona de servicio");
                        }
                    }
                    break;
                case "DDLZREstado":
                    if (DDLZREstado.SelectedItem != null)
                    {
                        if (DDLZREstado.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
                        {
                            MuestraMunicipio(DDLZREstado.SelectedItem.Value.ToString(), "Recolecta");
                            MuestraCiudad("00000000-0000-0000-0000-000000000000", "Recolecta");
                            MuestraColonia("00000000-0000-0000-0000-000000000000", "Recolecta");
                        }
                    }
                    break;
            }
        }
        protected void ObtenerCiudad(object sender, EventArgs e)
        {
            DropDownList objeto = sender as DropDownList;
            switch (objeto.ID)
            {
                case "DDLZonaMunicipio":
                    if (DDLZonaMunicipio.SelectedItem != null)
                    {
                        if (DDLZonaMunicipio.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
                        {
                            MuestraCiudad(DDLZonaMunicipio.SelectedItem.Value.ToString(), "Zona de servicio");
                            MuestraColonia("00000000-0000-0000-0000-000000000000", "Zona de servicio");
                        }
                    }
                    break;
                case "DDLZRMunicipio":
                    if (DDLZRMunicipio.SelectedItem != null)
                    {
                        if (DDLZRMunicipio.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
                        {
                            MuestraCiudad(DDLZRMunicipio.SelectedItem.Value.ToString(), "Recolecta");
                            MuestraColonia("00000000-0000-0000-0000-000000000000", "Recolecta");
                        }
                    }
                    break;
            }
        }
        protected void ObtenerColonia(object sender, EventArgs e)
        {
            DropDownList objeto = sender as DropDownList;
            switch (objeto.ID)
            {


                case "DDLZonaCiudad":
                    if (DDLZonaCiudad.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        MuestraColonia(DDLZonaCiudad.SelectedItem.Value.ToString(), "Filtro");
                    }
                    break;
                default:
                    break;
            }
        }
        protected void MuestraEstados(string id, string tipo)
        {
            Guid Pais = new Guid(id);

            switch (tipo)
            {
                case "Zona de servicio":
                    DDLZonaEstado.DataSource = MVDireccion.Estados(Pais);
                    DDLZonaEstado.DataValueField = "IdEstado";
                    DDLZonaEstado.DataTextField = "Nombre";
                    DDLZonaEstado.DataBind();
                    break;
                case "Recolecta":
                    DDLZREstado.DataSource = MVDireccion.Estados(Pais);
                    DDLZREstado.DataValueField = "IdEstado";
                    DDLZREstado.DataTextField = "Nombre";
                    DDLZREstado.DataBind();
                    break;
                default:
                    break;
            }
        }
        protected void MuestraMunicipio(string id, string tipo)
        {
            Guid estado = new Guid(id);
            switch (tipo)
            {
                case "Zona de servicio":
                    DDLZonaMunicipio.DataSource = MVDireccion.Municipios(estado);
                    DDLZonaMunicipio.DataTextField = "NOMBRE";
                    DDLZonaMunicipio.DataValueField = "IDMUNICIPIO";
                    DDLZonaMunicipio.DataBind();
                    break;
                case "Recolecta":
                    DDLZRMunicipio.DataSource = MVDireccion.Municipios(estado);
                    DDLZRMunicipio.DataTextField = "NOMBRE";
                    DDLZRMunicipio.DataValueField = "IDMUNICIPIO";
                    DDLZRMunicipio.DataBind();
                    break;
                default:
                    break;
            }
        }
        protected void MuestraCiudad(string id, string tipo)
        {
            Guid Municipio = new Guid(id);
            switch (tipo)
            {

                case "Zona de servicio":
                    DDLZonaCiudad.DataSource = MVDireccion.Ciudades(Municipio);
                    DDLZonaCiudad.DataTextField = "Nombre";
                    DDLZonaCiudad.DataValueField = "IdCiudad";
                    DDLZonaCiudad.DataBind();
                    break;
                case "Recolecta":
                    DDLZRCiudad.DataSource = MVDireccion.Ciudades(Municipio);
                    DDLZRCiudad.DataTextField = "Nombre";
                    DDLZRCiudad.DataValueField = "IdCiudad";
                    DDLZRCiudad.DataBind();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Visualiza los controles donde se cargara la colonia
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tipo">Gestion, Filtro, Zona de servicio</param>
        protected void MuestraColonia(string id, string tipo)
        {
            Guid Ciudad = new Guid(id);
            switch (tipo)
            {

                case "Zona de servicio":
                    break;
                default:
                    break;
            }

        }
        #endregion
        #region Zona de servicio

        protected void LimpiaListaDeColoniasEntrega()
        {
            chklColonias.Items.Clear();
            chklColonias.DataBind();
        }
        protected void LimpiaListaDeColoniasRecolecta()
        {
            chklZR.Items.Clear();
            chklZR.DataBind();
        }


        protected void EstatusControlesZonaDeServicio(bool estatus, string panel)
        {
            if (panel == "Entrega")
            {
                //Zona de servicio
                btnAgregarCiudad.Enabled = estatus;
                DGVZonaCiudades.Enabled = estatus;
                btnBusquedaColonia.Enabled = estatus;
                if (estatus)
                {
                    btnBusquedaColonia.CssClass = "input-group-addon";
                    btnAgregarCiudad.CssClass = "btn btn-sm btn-success";
                }
                else
                {
                    btnBusquedaColonia.CssClass = "input-group-addon disabled";
                    btnAgregarCiudad.CssClass = "btn btn-sm btn-success disabled";
                }

                chklColonias.Enabled = estatus;
                chkSeleccionarTodos.Enabled = estatus;
            }
            if (panel == "Recolecta")
            {
                //Zona de servicio
                btnZRAgregaCiudad.Enabled = estatus;
                DGVZRCiudades.Enabled = estatus;
                btnZrBusquedaColonia.Enabled = estatus;
                if (estatus)
                {
                    btnZrBusquedaColonia.CssClass = "input-group-addon";
                    btnZRAgregaCiudad.CssClass = "btn btn-sm btn-success";
                }
                else
                {
                    btnZrBusquedaColonia.CssClass = "input-group-addon disabled";
                    btnZRAgregaCiudad.CssClass = "btn btn-sm btn-success disabled";
                }

                chklZRSeleccionaTodos.Enabled = estatus;
                chklZR.Enabled = estatus;
            }
        }
        /// <summary>
        /// Este metodo selecciona las colonias ya sea de entrega o recolecta, valida que se haya seleccionado la ciudad y la guarda junto con las colonias en una lista temporal.
        /// </summary>
        protected void ColoniasSeleccionadas()
        {
            if (DGVZonaCiudades.SelectedValue != null)
            {
                if (chklColonias.Items.Count != 0)
                {
                    foreach (ListItem item in chklColonias.Items)
                    {
                        if (item.Selected)
                        {

                            MVDireccion.SeleccionarColoniaEntrega(Guid.NewGuid(), new Guid(item.Value), new Guid(DGVZonaCiudades.SelectedValue.ToString()), StrNombreColonia: item.Text);
                        }
                        else
                        {
                            if (DGVZonaCiudades.SelectedValue != null)
                            {
                                MVDireccion.DeseleccionarColoniaEntrega(UidColonia: new Guid(item.Value), UidCiudad: new Guid(DGVZonaCiudades.SelectedValue.ToString()));
                            }
                        }
                    }
                }
            }
            if (DGVZRCiudades.SelectedValue != null)
            {
                if (chklZR.Items.Count != 0)
                {
                    foreach (ListItem item in chklZR.Items)
                    {
                        if (item.Selected)
                        {
                            MVDireccion.SeleccionarColoniaRecolecta(Guid.NewGuid(), new Guid(item.Value), new Guid(DGVZRCiudades.SelectedValue.ToString()), StrNombreColonia: item.Text);
                        }
                        else
                        {
                            if (DGVZRCiudades.SelectedValue != null)
                            {
                                MVDireccion.DeseleccionarColoniaRecolecta(UidColonia: new Guid(item.Value), UidCiudad: new Guid(DGVZRCiudades.SelectedValue.ToString()));
                            }
                        }
                    }
                }
            }
        }

        protected void SeleccionaColonias()
        {
            int contador = 0;
            if (chklColonias.Items.Count != 0)
            {
                foreach (ListItem item in chklColonias.Items)
                {
                    if (MVDireccion.ListaColoniasSeleccionadasEntrega.Exists(Col => Col.ID.ToString() == item.Value))
                    {
                        contador = contador + 1;
                        item.Selected = true;
                    }
                    else
                    {
                        item.Selected = false;
                    }
                }
            }
            if (contador == chklColonias.Items.Count)
            {
                chkSeleccionarTodos.Checked = true;
            }
            else
            {
                chkSeleccionarTodos.Checked = false;
            }
        }

        protected void chkSeleccionarTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionarTodos.Checked)
            {
                SeleccionaTodasLasColoniasEntrega();
            }
            else
            {
                DeseleccionaCheckboxListColoniasEntrega();
            }
        }
        protected void chklZTSeleccionaTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (chklZRSeleccionaTodos.Checked)
            {
                SeleccionaTodasLasColoniasRecolecta();
            }
            else
            {
                DeseleccionaCheckboxListColoniasRecolecta();
            }
        }
        protected void DeseleccionaCheckboxListColoniasEntrega()
        {
            //Recorre la lista y desselecciona los checkbox
            foreach (ListItem item in chklColonias.Items)
            {
                item.Selected = false;
            }
        }
        protected void DeseleccionaCheckboxListColoniasRecolecta()
        {
            //Recorre la lista y desselecciona los checkbox
            foreach (ListItem item in chklZR.Items)
            {
                item.Selected = false;
            }
        }
        protected void btnAgregarCiudad_Click(object sender, EventArgs e)
        {
            if (DDLZonaCiudad.SelectedItem != null)
            {
                Guid ciudad = new Guid(DDLZonaCiudad.SelectedItem.Value);
                MVDireccion.SeleccionarCiudadEntrega(ciudad);

                //Actualiza el datagrid
                DGVZonaCiudades.DataSource = MVDireccion.ListaCiudadesSeleccionadasEntrega;
                DGVZonaCiudades.DataBind();

                if (DGVZonaCiudades.SelectedIndex != -1)
                {
                    LinkButton boton = DGVZonaCiudades.SelectedRow.FindControl("btnEliminaZona") as LinkButton;
                    boton.Enabled = false;
                    boton.CssClass = "btn btn-sm btn-default disabled";
                }

                DGVZonaCiudades.SelectedIndex = -1;

                //Verifica si existen colonias seleccionadas
                ColoniasSeleccionadas();

                DGVZonaCiudades.SelectedIndex = -1;
                LimpiaListaDeColoniasEntrega();
            }
            if (DDLZRCiudad.SelectedItem != null)
            {
                Guid ciudad = new Guid(DDLZRCiudad.SelectedItem.Value);
                MVDireccion.SeleccionarCiudadRecolecta(ciudad);

                //Actualiza el datagrid
                DGVZRCiudades.DataSource = MVDireccion.ListaCiudadesSeleccionadasRecolecta;
                DGVZRCiudades.DataBind();

                if (DGVZRCiudades.SelectedIndex != -1)
                {
                    LinkButton boton = DGVZRCiudades.SelectedRow.FindControl("btnEliminaZona") as LinkButton;
                    boton.Enabled = false;
                    boton.CssClass = "btn btn-sm btn-default disabled";
                }

                DGVZRCiudades.SelectedIndex = -1;

                //Verifica si existen colonias seleccionadas
                ColoniasSeleccionadas();

                DGVZonaCiudades.SelectedIndex = -1;
                LimpiaListaDeColoniasEntrega();
            }

        }

        protected void DGVZonaCiudades_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Guarda las colonias seleccionadas
            ColoniasSeleccionadas();

            Guid ciudad = new Guid(DGVZonaCiudades.SelectedDataKey.Value.ToString());
            //Recupera la zona de servicio
            // MuestraZonaDeServicio();

            //Recarga la lista de colonias 
            chklColonias.DataSource = MVDireccion.Colonias(ciudad, "CheckboxList");
            chklColonias.DataTextField = "Nombre";
            chklColonias.DataValueField = "IdColonia";
            chklColonias.DataBind();

            SeleccionaCheckboxListColoniasEntrega();

            EstatusControlesZonaDeServicio(true, "Entrega");

            LinkButton boton = DGVZonaCiudades.SelectedRow.FindControl("btnEliminaZona") as LinkButton;
            boton.Enabled = true;
            boton.CssClass = "btn btn-sm btn-default";

            //Actualiza el datagrid
            DGVZonaCiudades.DataSource = MVDireccion.ListaCiudadesSeleccionadasEntrega;
            DGVZonaCiudades.DataBind();



            //Si hay colonias seleccionadas las selecciona en el control
            SeleccionaColonias();
        }

        protected void DGVZRCiudades_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Guarda las colonias seleccionadas
            ColoniasSeleccionadas();

            Guid ciudad = new Guid(DGVZRCiudades.SelectedDataKey.Value.ToString());
            //Recupera la zona de servicio
            // MuestraZonaDeServicio();

            //Recarga la lista de colonias 
            chklZR.DataSource = MVDireccion.Colonias(ciudad, "CheckboxList");
            chklZR.DataTextField = "Nombre";
            chklZR.DataValueField = "IdColonia";
            chklZR.DataBind();

            SeleccionaCheckboxListColoniasRecolecta();

            EstatusControlesZonaDeServicio(true, "Recolecta");

            LinkButton boton = DGVZRCiudades.SelectedRow.FindControl("btnEliminaZona") as LinkButton;
            boton.Enabled = true;
            boton.CssClass = "btn btn-sm btn-default";

            //Actualiza el datagrid
            DGVZRCiudades.DataSource = MVDireccion.ListaCiudadesSeleccionadasRecolecta;
            DGVZRCiudades.DataBind();



            //Si hay colonias seleccionadas las selecciona en el control
            SeleccionaColonias();
        }

        public void SeleccionaCheckboxListColoniasEntrega()
        {
            foreach (ListItem chk in chklColonias.Items)
            {
                if (MVDireccion.ListaColoniasSeleccionadasEntrega.Exists(objeto => objeto.ID.ToString() == chk.Value))
                {
                    chk.Selected = true;
                }
                else
                {
                    chk.Selected = false;
                }
            }
        }

        public void SeleccionaCheckboxListColoniasRecolecta()
        {
            foreach (ListItem chk in chklZR.Items)
            {
                if (MVDireccion.ListaColoniasSeleccionadasRecolecta.Exists(objeto => objeto.ID.ToString() == chk.Value))
                {
                    chk.Selected = true;
                }
                else
                {
                    chk.Selected = false;
                }
            }
        }

        protected void DGVZonaCiudades_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(DGVZonaCiudades, "Select$" + e.Row.RowIndex);
                LinkButton boton = e.Row.FindControl("btnEliminaZona") as LinkButton;

                if (e.Row.RowIndex != DGVZonaCiudades.SelectedIndex)
                {
                    boton.Enabled = false;
                    boton.CssClass = "btn btn-sm btn-default disabled";
                }

            }
        }

        protected void DGVZonaCiudades_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Delete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                //Obtiene el datakey del gridzonadeservicio
                string uid = DGVZonaCiudades.DataKeys[index].Value.ToString();
                Guid uidCiudad = new Guid(uid);
                // MVDireccion.EliminaColoniasDeZonaDeServicio(uidCiudad, txtUidSucursal.Text);
                // MVDireccion.DeseleccionarCiudad(uidCiudad);


                //Limpia el checkboxlist
                chklColonias.DataSource = MVDireccion.Colonias(uidCiudad, "CheckboxList");
                chklColonias.DataTextField = "Nombre";
                chklColonias.DataValueField = "IdColonia";
                chklColonias.DataBind();

                foreach (ListItem item in chklColonias.Items)
                {
                    //Elimina las colonias asociadas a la ciudad
                    MVDireccion.DeseleccionarColoniaEntrega(UidCiudad: uidCiudad, UidColonia: new Guid(item.Value.ToString()));
                }

                //Limpia el checkboxlist
                LimpiaListaDeColoniasEntrega();

                //Elimina la ciudad de la lista
                MVDireccion.DeseleccionarCiudadEntrega(uidCiudad);

            }

        }
        protected void DGVZRCiudades_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                //Obtiene el datakey del gridzonadeservicio
                string uid = DGVZRCiudades.DataKeys[index].Value.ToString();
                Guid uidCiudad = new Guid(uid);
                // MVDireccion.EliminaColoniasDeZonaDeServicio(uidCiudad, txtUidSucursal.Text);
                // MVDireccion.DeseleccionarCiudad(uidCiudad);


                //Limpia el checkboxlist
                chklZR.DataSource = MVDireccion.Colonias(uidCiudad, "CheckboxList");
                chklZR.DataTextField = "Nombre";
                chklZR.DataValueField = "IdColonia";
                chklZR.DataBind();

                foreach (ListItem item in chklZR.Items)
                {
                    //Elimina las colonias asociadas a la ciudad
                    MVDireccion.DeseleccionarColoniaEntrega(UidCiudad: uidCiudad, UidColonia: new Guid(item.Value.ToString()));
                }

                //Limpia el checkboxlist
                LimpiaListaDeColoniasRecolecta();

                //Elimina la ciudad de la lista
                MVDireccion.DeseleccionarCiudadEntrega(uidCiudad);

            }

        }
        protected void DGVZonaCiudades_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            DGVZonaCiudades.SelectedIndex = -1;
            //Actualiza el datagrid
            DGVZonaCiudades.DataSource = MVDireccion.ListaCiudadesSeleccionadasEntrega;
            DGVZonaCiudades.DataBind();
        }
        protected void DGVZRCiudades_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DGVZRCiudades.SelectedIndex = -1;
            //Actualiza el datagrid
            DGVZRCiudades.DataSource = MVDireccion.ListaCiudadesSeleccionadasRecolecta;
            DGVZRCiudades.DataBind();
        }
        protected void btnBusquedaColonia_Click(object sender, EventArgs e)
        {
            if (DGVZonaCiudades.SelectedIndex != -1)
            {
                //Guarda las colonias seleccionadas en la lista
                ColoniasSeleccionadas();


                if (string.IsNullOrWhiteSpace(txtBusquedaColonia.Text))
                {
                    //Retorna todas las colonias
                    chklColonias.DataSource = MVDireccion.Colonias(new Guid(DGVZonaCiudades.DataKeys[Convert.ToInt32(DGVZonaCiudades.SelectedIndex)].Value.ToString()), "CheckboxList");
                }
                else
                {
                    //Retorna colonias filtradas por nombre
                    chklColonias.DataSource = MVDireccion.Colonias(new Guid(DGVZonaCiudades.DataKeys[Convert.ToInt32(DGVZonaCiudades.SelectedIndex)].Value.ToString()), Nombre: txtBusquedaColonia.Text);
                }

                chklColonias.DataTextField = "Nombre";
                chklColonias.DataValueField = "IdColonia";
                chklColonias.DataBind();

                //Si hay colonias seleccionadas en la lista y selecciona en el control
                SeleccionaColonias();
            }
        }
        protected void btnZrBusquedaColonia_Click(object sender, EventArgs e)
        {
            if (DGVZRCiudades.SelectedIndex != -1)
            {
                //Guarda las colonias seleccionadas en la lista
                ColoniasSeleccionadas();

                if (string.IsNullOrWhiteSpace(txtZRBusquedaColonia.Text))
                {
                    //Retorna todas las colonias
                    chklZR.DataSource = MVDireccion.Colonias(new Guid(DGVZRCiudades.DataKeys[Convert.ToInt32(DGVZRCiudades.SelectedIndex)].Value.ToString()), "CheckboxList");
                }
                else
                {
                    //Retorna colonias filtradas por nombre
                    chklZR.DataSource = MVDireccion.Colonias(new Guid(DGVZRCiudades.DataKeys[Convert.ToInt32(DGVZRCiudades.SelectedIndex)].Value.ToString()), Nombre: txtZRBusquedaColonia.Text);
                }

                chklZR.DataTextField = "Nombre";
                chklZR.DataValueField = "IdColonia";
                chklZR.DataBind();

                //Si hay colonias seleccionadas en la lista y selecciona en el control
                SeleccionaColonias();
            }
        }
        protected void DDLTipoDeColonias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DGVZonaCiudades.SelectedIndex != -1)
            {
                string tipo = DDLTipoDeColonias.SelectedItem.Text;
                Guid ciudad = new Guid(DGVZonaCiudades.SelectedDataKey.Value.ToString());
                ColoniasSeleccionadas();
                if (chklColonias.Items.Count > 0)
                {
                    //Recarga la lista de colonias 
                    chklColonias.DataSource = MVDireccion.Colonias(ciudad, "CheckboxList");
                    chklColonias.DataTextField = "Nombre";
                    chklColonias.DataValueField = "IdColonia";
                    chklColonias.DataBind();
                    switch (tipo)
                    {
                        case "Todos":
                            SeleccionaCheckboxListColoniasEntrega();
                            break;
                        case "Seleccionados":
                            //Se crea la lista, 
                            List<ListItem> Seleccionados = new List<ListItem>();

                            //Recorre los elemtentos del control y si existe un valor, marca su checkbox
                            for (int i = 0; i < chklColonias.Items.Count; i++)
                            {
                                if (MVDireccion.ListaColoniasSeleccionadasEntrega.Exists(dir => dir.ID.ToString() == chklColonias.Items[i].Value))
                                { chklColonias.Items[i].Selected = true; }
                                else { Seleccionados.Add(chklColonias.Items[i]); }
                            }
                            //Si el elemto  existe en la lista, este lo elimina
                            for (int i = 0; i < Seleccionados.Count; i++)
                            {
                                chklColonias.Items.Remove(Seleccionados[i]);
                            }

                            break;
                        case "Deseleccionados":

                            List<ListItem> Deseleccionados = new List<ListItem>();

                            for (int i = 0; i < chklColonias.Items.Count; i++)
                            {
                                if (!MVDireccion.ListaColoniasSeleccionadasEntrega.Exists(dir => dir.ID.ToString() == chklColonias.Items[i].Value))
                                { chklColonias.Items[i].Selected = false; }
                                else { Deseleccionados.Add(chklColonias.Items[i]); }
                            }

                            for (int i = 0; i < Deseleccionados.Count; i++)
                            {
                                chklColonias.Items.Remove(Deseleccionados[i]);
                            }

                            break;
                        default:
                            break;
                    }
                }

            }
        }

        protected void SeleccionaTodasLasColoniasEntrega()
        {
            //Recorre la lista y desselecciona los checkbox
            foreach (ListItem item in chklColonias.Items)
            {
                item.Selected = true;
            }
        }
        protected void SeleccionaTodasLasColoniasRecolecta()
        {
            //Recorre la lista y desselecciona los checkbox
            foreach (ListItem item in chklZR.Items)
            {
                item.Selected = true;
            }
        }

        protected void DGVZRCiudades_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(DGVZRCiudades, "Select$" + e.Row.RowIndex);
                LinkButton boton = e.Row.FindControl("btnEliminaZona") as LinkButton;

                if (e.Row.RowIndex != DGVZRCiudades.SelectedIndex)
                {
                    boton.Enabled = false;
                    boton.CssClass = "btn btn-sm btn-default disabled";
                }
            }
        }

        protected void ddlZRTIpoSeleccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DGVZRCiudades.SelectedIndex != -1)
            {
                string tipo = ddlZRTIpoSeleccion.SelectedItem.Text;
                Guid ciudad = new Guid(DGVZRCiudades.SelectedDataKey.Value.ToString());
                ColoniasSeleccionadas();
                if (chklZR.Items.Count > 0)
                {
                    //Recarga la lista de colonias 
                    chklZR.DataSource = MVDireccion.Colonias(ciudad, "CheckboxList");
                    chklZR.DataTextField = "Nombre";
                    chklZR.DataValueField = "IdColonia";
                    chklZR.DataBind();
                    switch (tipo)
                    {
                        case "Todos":
                            SeleccionaCheckboxListColoniasRecolecta();
                            break;
                        case "Seleccionados":
                            List<ListItem> Seleccionados = new List<ListItem>();

                            for (int i = 0; i < chklZR.Items.Count; i++)
                            {
                                if (MVDireccion.ListaColoniasSeleccionadasRecolecta.Exists(dir => dir.ID.ToString() == chklZR.Items[i].Value))
                                { chklZR.Items[i].Selected = true; }
                                else { Seleccionados.Add(chklZR.Items[i]); }
                            }

                            for (int i = 0; i < Seleccionados.Count; i++)
                            {
                                chklZR.Items.Remove(Seleccionados[i]);
                            }

                            break;
                        case "Deseleccionados":

                            List<ListItem> Deseleccionados = new List<ListItem>();

                            for (int i = 0; i < chklZR.Items.Count; i++)
                            {
                                if (!MVDireccion.ListaColoniasSeleccionadasRecolecta.Exists(dir => dir.ID.ToString() == chklZR.Items[i].Value))
                                { chklZR.Items[i].Selected = false; }
                                else { Deseleccionados.Add(chklZR.Items[i]); }
                            }

                            for (int i = 0; i < Deseleccionados.Count; i++)
                            {
                                chklZR.Items.Remove(Deseleccionados[i]);
                            }

                            break;
                        default:
                            break;
                    }
                }
            }
        }


        #endregion
        protected void BuscarEmpresasBusquedaNormal(object sender, EventArgs e)
        {
            string Identificador = txtFIdentificador.Text;
            string HA = txtFHoraApertura.Text;
            string HC = txtFHoraCierre.Text;

            if (txtFIdentificador.Text == string.Empty && txtFHoraApertura.Text == string.Empty && txtFHoraCierre.Text == string.Empty)
            {
                MVSucursales.DatosGridViewBusquedaNormal(Session["UidEmpresaSistema"].ToString());
                CargaGrid("Sucursal");
                lblVisibilidadfiltros.Text = " Mostrar";
                PnlFiltros.Visible = false;
                btnBuscar.Enabled = false;
                btnBorrarFiltros.Enabled = false;
                btnBuscar.CssClass = "btn btn-sm btn-default disabled";
                btnBorrarFiltros.CssClass = "btn btn-sm btn-default disabled";
            }
            else
            {
                MVSucursales.BuscarSucursales(Identificador, HA, HC, Uidempresa: Session["UidEmpresaSistema"].ToString());
                CargaGrid("Sucursal");
                lblVisibilidadfiltros.Text = " Mostrar";
                PnlFiltros.Visible = false;
                btnBuscar.Enabled = false;
                btnBorrarFiltros.Enabled = false;
                btnBuscar.CssClass = "btn btn-sm btn-default disabled";
                btnBorrarFiltros.CssClass = "btn btn-sm btn-default disabled";
            }
        }
        protected void PaginaSeleccionadaBusquedaNormal(object sender, EventArgs e)
        {
            int valor = Convert.ToInt32(((DropDownList)sender).SelectedValue);
            DgvSucursales.PageIndex = valor - 1;
            CargaGrid("Sucursal");
        }

        private void MuestraPanel(string Panel) { }

        #region Panel mensaje de sistema
        protected void BtnCerrarPanelMensaje_Click(object sender, EventArgs e)
        {
            PanelMensaje.Visible = false;
        }
        #endregion




        protected void DgvSucursales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DgvSucursales.PageIndex = e.NewPageIndex;
            CargaGrid("Sucursal");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            #region Zona de servicio
            var UidSucursal = new Guid(lblUidSucursal.Text);
            MVSucursales.EliminaZona(UidSucursal);
            MVTarifario.EliminaTarifarioDeBaseDeDatos(UidSucursal.ToString());
            //Zona de recolecta
            if (MVDireccion.ListaColoniasSeleccionadasEntrega.Count > 0 && MVDireccion.ListaColoniasSeleccionadasEntrega != null)
            {
                foreach (var item in MVDireccion.ListaColoniasSeleccionadasEntrega)
                {
                    MVSucursales.GuardaZona(item.UidRegistro, UidSucursal, item.ID, "Entrega");
                }
                //Limpia colonias
                LimpiaListaDeColoniasEntrega();
            }
            //Zona de entrega
            if (MVDireccion.ListaColoniasSeleccionadasRecolecta.Count > 0 && MVDireccion.ListaColoniasSeleccionadasRecolecta != null)
            {
                foreach (var item in MVDireccion.ListaColoniasSeleccionadasRecolecta)
                {
                    MVSucursales.GuardaZona(item.UidRegistro, UidSucursal, item.ID, "Recolecta");
                }
                //Limpia las colonias 
                LimpiaListaDeColoniasRecolecta();
            }

            #endregion

            #region Tarifario
            if (DGVTarifario.Visible)
            {
                GuardaTarifario();
            }
            if (MVTarifario.ListaDeTarifarios.Count > 0)
            {
                MVTarifario.GuardaTarifario();
            }
            #endregion
            activaEdicion(false, "Gestion");

        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            activaEdicion(true, "Gestion");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            activaEdicion(false, "Gestion");
        }
    }
}