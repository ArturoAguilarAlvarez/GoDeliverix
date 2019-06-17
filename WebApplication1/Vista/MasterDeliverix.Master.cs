using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using VistaDelModelo;
namespace WebApplication1.Vista
{
    public partial class MasterDeliverix : System.Web.UI.MasterPage
    {
        VMAcceso MVAcceso = new VMAcceso();
        VMEmpresas MVEmpresaSistema;


        private string _UidEmpresa;

        public string UidempresaEnSistema
        {
            get { return _UidEmpresa; }
            set { _UidEmpresa = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string uidusuario = string.Empty;
            //Valida si existe un usuario en el sistema
            if (Session["IdUsuario"] != null)
            {
                uidusuario = Session["IdUsuario"].ToString();
                MVEmpresaSistema = new VMEmpresas();
                //Obtiene el perfil del usuario
                lblNombreUsuario.Text = MVAcceso.NombreDeUsuario(new Guid(uidusuario));
                //Valida que este asociado con una empresa
                if (Session["UidEmpresaSistema"] != null)
                {
                    string nombrecomercial = string.Empty;

                    Guid uidempresa = new Guid(Session["UidEmpresaSistema"].ToString());
                    if (!IsPostBack)
                    {
                        if (uidempresa == null && uidempresa == Guid.Empty)
                        {
                            //Obtiene el nombe de la empresa asociada
                            MVEmpresaSistema.ObtenerNombreComercial(uidusuario);
                            if (MVEmpresaSistema.NOMBRECOMERCIAL != null && string.IsNullOrEmpty(MVEmpresaSistema.NOMBRECOMERCIAL))
                            {
                                nombrecomercial = MVEmpresaSistema.NOMBRECOMERCIAL;
                            }
                            else
                            {
                                nombrecomercial = "Go-Deliverix";
                            }
                            if (MVEmpresaSistema.UIDEMPRESA != null)
                            {
                                Session["UidEmpresaSistema"] = MVEmpresaSistema.UIDEMPRESA;
                            }
                            
                            ArmaElMenu(uidusuario, uidempresa.ToString());
                        }
                        else
                        {
                            MVEmpresaSistema.BuscarEmpresas(UidEmpresa: uidempresa);
                            nombrecomercial = MVEmpresaSistema.NOMBRECOMERCIAL;
                            lblNombreDeEmpresa.Text = nombrecomercial;
                            ArmaElMenu(uidusuario, uidempresa.ToString());
                        }
                    }
                    else
                    {
                        MVEmpresaSistema.BuscarEmpresas(UidEmpresa: uidempresa);
                        nombrecomercial = MVEmpresaSistema.NOMBRECOMERCIAL;
                        lblNombreDeEmpresa.Text = nombrecomercial;
                        ArmaElMenu(uidusuario, uidempresa.ToString());
                    }
                }
            }
            else
            {
                Response.Redirect("Default/Default.aspx");
            }
        }

        public void ArmaElMenu(string UidUsuario, string uidEmpresa)
        {
            string Perfil = MVAcceso.PerfilDeUsuario(UidUsuario);

            //Menu de empresas
            btnEmpresasMenus.Visible = false;
            btnEmpresas.Visible = false;
            btnAdministradores.Visible = false;

            //Modulos distribuidora
            btnRepartidores.Visible = false;
            btnVehiculos.Visible = false;
            //Modulo suministradora
            btnModuloProductos.Visible = false;
            btnModuloMenu.Visible = false;
            btnSucursalesMenus.Visible = false;

            //Perfil de super adimistrador
            if (Perfil == "8d2e2925-a2a7-421f-a72b-56f2e8296d77")
            {
                //Menu de empresas
                btnEmpresasMenus.Visible = true;
                btnEmpresas.Visible = true;
                btnAdministradores.Visible = true;
                
                if (uidEmpresa != Guid.Empty.ToString() && !string.IsNullOrEmpty(uidEmpresa))
                {
                    
                    if (MVEmpresaSistema.ObtenerTipoDeEmpresa(uidEmpresa))
                    {
                        //Modulo suministradora
                        btnModuloProductos.Visible = true;
                        btnModuloMenu.Visible = true;
                        btnSucursalesMenus.Visible = true;
                    }
                    else if (!MVEmpresaSistema.ObtenerTipoDeEmpresa(uidEmpresa))
                    {
                        //Modulo distribuidora
                        btnRepartidores.Visible = true;
                        btnSucursalesMenus.Visible = true;
                        btnVehiculos.Visible = true;
                    }
                }


            }
            //Perfil de administrador
            if (Perfil == "76a96ff6-e720-4092-a217-a77a58a9bf0d")
            {
                btnSucursalesMenus.Visible = true;
                if (MVEmpresaSistema.ObtenerTipoDeEmpresa(uidEmpresa))
                {
                    //Modulo suministradora
                    btnModuloProductos.Visible = true;
                    btnModuloMenu.Visible = true;
                }
                else
                {
                    //Modulo distribuidora
                    btnRepartidores.Visible = true;
                    btnVehiculos.Visible = true;
                }

            }
            //Perfil de supervisor
            if (Perfil == "81232596-4c6b-4568-9005-8d4a0a382fda")
            {

            }
            if (Perfil == "5cbcc741-6bfe-44ed-9280-81eb87178ca5")
            {

            }
            if (Perfil == "dfc29662-0259-4f6f-90ea-b24e39be4346")
            {

            }

        }

        protected void RedireccionamientoSucursales(object sender, EventArgs e)
        {
            Response.Redirect("Sucursales.aspx", false);
        }

        protected void RedireccionamientoInicio(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
        protected void RedireccionamientoSupervisores(object sender, EventArgs e)
        {
            Response.Redirect("Supervisores.aspx");
        }
        protected void MenuEmpresas(object sender, EventArgs e)
        {
            Response.Redirect("Empresas.aspx", false);
        }

        protected void Usuarios(object sender, EventArgs e)
        {
            Response.Redirect("AdministradoresDeEmpresas.aspx", false);
        }
        protected void Catalogos(object sender, EventArgs e)
        {
            Response.Redirect("Catalogos.aspx");
        }
        protected void Salir(object sender, EventArgs e)
        {
            Session.Remove("IdUsuario");
            Session.Remove("Usuario");
            Response.Redirect("Default/Default.aspx");
        }


        #region Metodos para el cambio de empresa en cualquier punto
        protected void DGVEMPRESAS_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(DGVEMPRESAS, "Select$" + e.Row.RowIndex);
            }
        }

        protected void DGVEMPRESAS_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DGVEMPRESAS.PageIndex = e.NewPageIndex;
            DGVEMPRESAS.DataSource = MVEmpresaSistema.LISTADEEMPRESAS;
            DGVEMPRESAS.DataBind();
        }

        //Metodo para el cambio de empresa desde cualquier parte del sitio
        //protected void BtnBuscarEmpresaCambio_Click(object sender, EventArgs e)
        //{
        //    string Nombre = txtCNombre.Text;
        //    string Rfc = txtCRfc.Text;
        //    string RazonSocial = txtCRazon.Text;

        //    if ((Nombre == "" & Nombre == string.Empty) && (Rfc == "" & Rfc == string.Empty) && (RazonSocial == "" & RazonSocial == string.Empty))
        //    {
        //        MVEmpresa.BuscarEmpresas();
        //    }
        //    else
        //    {
        //        MVEmpresa.BuscarEmpresas(RazonSocial: RazonSocial, NombreComercial: Nombre, RFC: Rfc);
        //    }
        //    DGVEMPRESAS.DataSource = MVEmpresa.LISTADEEMPRESAS;
        //    DGVEMPRESAS.DataBind();
        //}

        protected void BtnLimpiarFiltrosCambio_Click(object sender, EventArgs e)
        {
            LimpiaFiltros();
        }

        protected void BtnCCancelar_Click(object sender, EventArgs e)
        {
            LimpiaFiltros();
            //Deselecciona fila en gridview
            DGVEMPRESAS.SelectedIndex = -1;
            //OBtiene el click del boton cerrar
            btnCCancelar.Attributes.Add("onclick", "document.getElementById('" + btnCCerrarVentana.ClientID + "').click()");
        }

        protected void BtnCambiarEmpresa_Click(object sender, EventArgs e)
        {
            LimpiaFiltros();
            //Obtiene el UidEmpresa del GridView
            string UidEmpresa = DGVEMPRESAS.SelectedDataKey.Value.ToString();
            //LLama al metodo obtenerNombreComercial y se le envia el Uid de la empresa
            MVEmpresaSistema.ObtenerNombreComercial("", IdEmpresa: UidEmpresa);
            //Muestra en label el nombre de la empresa
            lblNombreDeEmpresa.Text = MVEmpresaSistema.NOMBRECOMERCIAL;
            //Cambia el valor de la session para que pueda acceder desde cualquier modulo del sitio
            Session["UidEmpresa"] = MVEmpresaSistema.UIDEMPRESA.ToString();
            //Deselecciona fila en gridview
            DGVEMPRESAS.SelectedIndex = -1;
            //Obtiene el click del boton cerrar
            BtnCambiarEmpresa.Attributes.Add("onclick", "document.getElementById('" + btnCCerrarVentana.ClientID + "').click()");
            BtnCambiarEmpresa.Attributes.Add("onclick", "document.getElementById('" + btnCCerrarVentana.ClientID + "').click()");
            //Refresca la pagina
            Response.Redirect(Request.RawUrl);
        }


        protected void LimpiaFiltros()
        {
            //Limpia los campos de texto
            txtCNombre.Text = string.Empty;
            txtCRazon.Text = string.Empty;
            txtCRfc.Text = string.Empty;
        }
        #endregion



        protected void BtnModuloProductos_Click(object sender, EventArgs e)
        {
            Response.Redirect("Productos.aspx");
        }

        protected void BtnModuloMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("Menus.aspx");
        }

        protected void BtnRepartidores_Click(object sender, EventArgs e)
        {
            Response.Redirect("Repartidores.aspx");
        }

        protected void BtnVehiculos_Click(object sender, EventArgs e)
        {
            Response.Redirect("Vehiculos.aspx");
        }

        protected void btnClientes_Click(object sender, EventArgs e)
        {
            Response.Redirect("Clientes.aspx");
        }

        protected void BtnConfiguracion_Click(object sender, EventArgs e)
        {
            Response.Redirect("Configuracion.aspx");
        }

        protected void btnConfiguracionEmpresa_Click(object sender, EventArgs e)
        {
            Response.Redirect("ConfiguracionDeEmpresa.aspx");
        }

        protected void btnZonaHoraria_Click(object sender, EventArgs e)
        {
            Response.Redirect("ZonaHoraria.aspx");
        }
    }
}