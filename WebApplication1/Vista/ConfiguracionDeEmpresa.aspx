<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/MasterDeliverix.Master" AutoEventWireup="true" CodeBehind="ConfiguracionDeEmpresa.aspx.cs" Inherits="WebApplication1.Vista.ConfiguracionDeEmpresa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="favicon" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Panel de direccion-->
    <asp:Panel runat="server" ID="PanelDatosDireccion">
        <div class="panel panel-primary">
            <div class="panel-heading text-center">
                <label>DIRECCION</label>
            </div>
            <div class="pull-right">
                <asp:LinkButton runat="server" ID="btnGuardarDireccion" OnClick="AgregaDireccion" CssClass="btn btn-sm btn-success">
                    <asp:Label runat="server" ID="lblDatosDireccion"></asp:Label>
                </asp:LinkButton>
                <asp:LinkButton runat="server" ID="btnCancelarDireccion" OnClick="OcultarPanelDireccion" CssClass="btn btn-sm btn-danger"><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                <asp:LinkButton runat="server" ID="btnCerrarVetanaDireccion" OnClick="CierraVentanaDireccion" CssClass="btn btn-sm btn-default"><span class="glyphicon glyphicon-remove"></span> Cerrar</asp:LinkButton>
            </div>
            <div class="panel-body">

                <div class="row">
                    <asp:TextBox runat="server" CssClass="hide" ID="txtIdDireccion" />
                    <div class="col-md-4">
                        <h6>Identificador</h6>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtIdentificadorDeDireccion"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <h6>Pais</h6>
                        <asp:DropDownList ID="DDLDPais" runat="server" OnSelectedIndexChanged="ObtenerEstado" AutoPostBack="true" OnTextChanged="ObtenerEstado" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <h6>Estado</h6>
                        <asp:DropDownList ID="DDLDEstado" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ObtenerMunicipio"></asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <h6>Municipio</h6>
                        <asp:DropDownList ID="DDLDMunicipio" runat="server" OnSelectedIndexChanged="ObtenerCiudad" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <h6>Ciudad</h6>
                        <asp:DropDownList ID="DDLDCiudad" AutoPostBack="true" OnSelectedIndexChanged="ObtenerColonia" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <h6>Colonia </h6>
                        <asp:DropDownList ID="DDLDColonia" AutoPostBack="true" OnSelectedIndexChanged="ObtenerCP" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <h6>Calle</h6>
                        <asp:TextBox ID="txtCalle0" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-4">
                        <h6>Entre calle</h6>
                        <asp:TextBox ID="txtCalle1" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <h6>y calle</h6>
                        <asp:TextBox ID="txtCalle2" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <h6>Manzana/No. Ext</h6>
                        <asp:TextBox ID="txtDManzana" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <h6>Lote/No. Int</h6>
                        <asp:TextBox ID="txtDLote" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <h6>Codigo postal</h6>
                        <asp:TextBox ID="txtDCodigoPostal" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-12">
                        <h6>Referencia</h6>
                        <asp:TextBox ID="txtDReferencia" TextMode="MultiLine" MaxLength="500" Columns="20" Rows="5" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>

    </asp:Panel>

    <asp:Panel ID="PanelInformacionDeEmpresa" runat="server">

        <div class="col-md-12">
            <div class="panel panel-primary">
                <div class="panel-heading text-center">
                    <h5>Datos de EMPRESA</h5>
                </div>
                <div class="clearfix"></div>
                <%-- Update Panel Controlador de las acciones de usuario --%>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class=" pull-left">
                            <asp:LinkButton runat="server" ID="btnEditar" OnClick="ActivarEdicion" CssClass="btn btn-sm btn-default"><span class="glyphicon glyphicon-cog"></span> Editar</asp:LinkButton>
                            <asp:LinkButton runat="server" ID="btnGuardar" OnClick="GuardarDatos" CssClass="btn btn-sm btn-success ">
                            <span class="glyphicon glyphicon-ok"></span>
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="btnCancelar" OnClick="CancelarAgregacion" CssClass="btn btn-sm btn-danger "><asp:label CssClass=" glyphicon glyphicon-remove" runat="server" /></asp:LinkButton><asp:Label runat="server" ID="lblEstado"></asp:Label>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="clearfix"></div>
                <%--Panel de mensaje de sistema--%>
                <asp:Panel CssClass=" alert alert-danger " Style="padding: 10px;" ID="PanelMensaje" runat="server">
                    <span class="glyphicon glyphicon-info-sign"></span>
                    <asp:Label ID="LblMensaje" Text="Mensaje del sistema" Font-Size="Large" runat="server" />

                    <div class="pull-right">
                        <asp:LinkButton ID="BtnCerrarPanelMensaje" CssClass="btn btn-sm btn-danger" OnClick="BtnCerrarPanelMensaje_Click" ForeColor="White" runat="server">
                            <span class="glyphicon glyphicon-remove"></span>
                        </asp:LinkButton>
                    </div>
                </asp:Panel>


                <div class="panel-body">
                    <%-- Menu para la navegacion entre paneles --%>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <ul class="nav nav-tabs">
                                <li role="presentation" id="liDatosGenerales" runat="server">
                                    <asp:LinkButton runat="server" ID="btnDatosGenerales" OnClick="PanelGeneral"><span class="glyphicon glyphicon-globe"></span> GENERAL</asp:LinkButton></li>
                                <li role="presentation" id="liDatosDireccion" runat="server">
                                    <asp:LinkButton runat="server" ID="btnDatosDireccion" OnClick="PanelDireccion"><span class="glyphicon glyphicon-road"></span> DIRECCION</asp:LinkButton></li>
                                <li role="presentation" id="liDatosContacto" runat="server">
                                    <asp:LinkButton runat="server" ID="btnDatosDeConectado" OnClick="PanelContacto"><span class="glyphicon glyphicon-phone"></span> CONTACTO</asp:LinkButton></li>
                            </ul>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>

                            <asp:Label ID="lblUidEmpresa" CssClass="hide" runat="server" />
                            <asp:Panel runat="server" ID="pnlDatosGenerales">
                                <div class="row" style="margin-top: 5px;">


                                    <div class="col-md-3">
                                        <div class="col-md-12 text-center pull-right" style="margin-top: 10px;">
                                            <asp:Image runat="server" CssClass="img img-thumbnail" ID="ImageEmpresa" Width="200px" />
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class=" col-md-12 text-center" style="margin-top: 5px;">
                                            <script type="text/javascript">
                                                function UploadFile(fileUpload) {
                                                    if (fileUpload.value != '') {
                                                        document.getElementById("<%=btnSubirImagen.ClientID %>").click();
                                                    }
                                                }
                                            </script>
                                            <asp:TextBox ID="txtRutaImagen" CssClass="hide" runat="server" />
                                            <asp:LinkButton CssClass="btn btn-sm btn-default" ID="BtnCargarImagen" OnClick="SeleccionarImagen" runat="server">
                                                <span class="glyphicon glyphicon-open">
                                                </span>
                                                Cargar foto de perfil
                                            </asp:LinkButton>
                                            <asp:FileUpload ID="FUImagen" CssClass="hide" runat="server" />
                                            <asp:Button Text="Subir" OnClick="MuestraFoto" CssClass="hide" ID="btnSubirImagen" runat="server" />
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="col-md-12 text-center pull-right" style="margin-top: 10px;">
                                            <asp:Image runat="server" CssClass="img img-thumbnail" ID="imgPortada" Width="200px" />
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class=" col-md-12 text-center" style="margin-top: 5px;">
                                            <script type="text/javascript">
                                                function CargaPortada(fileUpload) {
                                                    if (fileUpload.value != '') {
                                                        document.getElementById("<%=btnSubirImagenPortada.ClientID %>").click();
                                                    }
                                                }
                                                function SeleccionaFotoPortada() {
                                                    document.getElementById("<%= FuImagenPortada.ClientID %>").click()
                                                }
                                            </script>
                                            <asp:TextBox ID="txtRutaImagenPortada" CssClass="hide" runat="server" />
                                            <asp:LinkButton CssClass="btn btn-sm btn-default" ID="btnCargarImagenDePortada" OnClientClick="SeleccionaFotoPortada()" runat="server">
                                                <span class="glyphicon glyphicon-open">
                                                </span>
                                                Cargar foto de portada
                                            </asp:LinkButton>
                                            <asp:FileUpload ID="FuImagenPortada" onChange="CargaPortada(this)" CssClass="hide" runat="server" />
                                            <asp:Button Text="Subir" OnClick="MuestraFoto" CssClass="hide" ID="btnSubirImagenPortada" runat="server" />
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="col-md-5">
                                            <h6>Razon social*</h6>
                                            <asp:TextBox ID="txtDRazonSocial" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-5">
                                            <h6>RFC*</h6>
                                            <asp:TextBox ID="txtDRfc" runat="server" Enabled="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="TxtDRfc_TextChanged" Style="text-transform: uppercase"></asp:TextBox>
                                        </div>

                                        <div class="col-md-5">
                                            <h6>Nombre comercial*</h6>
                                            <asp:TextBox ID="txtDNombreComercial" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-5">
                                            <h6>Correo electronico</h6>
                                            <asp:TextBox ID="txtDCorreoElectronico" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                                            <asp:Label CssClass="hidden" ID="txtUidCorreoElectronico" runat="server" />
                                        </div>
                                    </div>


                                </div>
                            </asp:Panel>

                            <asp:Panel runat="server" ID="pnlDireccion">
                                <div class="pull-left" style="margin-top: 10px;">
                                    <asp:LinkButton runat="server" ID="btnNuevaDireccion" OnClick="NuevaDireccion" CssClass="btn btn-sm btn-default disabled"><span class="glyphicon glyphicon-file"></span>Nuevo</asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnEdiarDireccion" OnClick="ActivaEdicionDeDireccion" CssClass="btn btn-sm btn-default disabled"><span class="glyphicon glyphicon-cog"></span>Editar</asp:LinkButton>
                                </div>
                                <div class="clearfix"></div>
                                <div style="overflow-x: auto;" class="table-responsibe">
                                    <asp:GridView runat="server" Style="margin-top: 10px;" DataKeyNames="ID" OnRowCommand="GVDireccion_RowCommand" AutoGenerateColumns="false" ID="GVDireccion" OnRowDataBound="GVDireccion_RowDataBound" OnSelectedIndexChanged="GVDireccion_SelectedIndexChanged"
                                        CssClass="table table-bordered table-hover table-condensed table-striped input-sm">
                                        <EmptyDataTemplate>
                                            <div class="info">
                                                No existen direcciones guardadas
                                            </div>
                                        </EmptyDataTemplate>
                                        <SelectedRowStyle CssClass="table table-hover input-sm success" />
                                        <SortedAscendingHeaderStyle CssClass="glyphicon glyphicon-sort-by-alphabet" />
                                        <Columns>
                                            <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide">
                                                <FooterStyle CssClass="hide" />
                                                <HeaderStyle CssClass="hide" />
                                                <ItemStyle CssClass="hide" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="IDENTIFICADOR" HeaderText="Identificador" />
                                            <asp:BoundField DataField="NOMBRECIUDAD" HeaderText="Ciudad" />
                                            <asp:BoundField DataField="NOMBRECOLONIA" HeaderText="Colonia" />
                                            <asp:BoundField DataField="REFERENCIA" HeaderText="Referencia" />
                                            <asp:BoundField DataField="CodigoPostal" HeaderText="Codigo Postal" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton CssClass="btn btn-sm btn-default" ID="EliminaDireccion" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" CommandName="Eliminar" runat="server">
                                                        <asp:Label ID="lblEliminarTelefono" runat="server" />
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnlContacto">

                                <div class="pull-left" style="margin-top: 10px;">
                                    <asp:LinkButton runat="server" ID="btnNuevoTelefono" OnClick="NuevoTelefono" CssClass="btn btn-sm btn-default disabled"><span class="glyphicon glyphicon-file"></span>Nuevo</asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnEditarTelefono" OnClick="EditaTelefono" CssClass="btn btn-sm btn-default disabled"><span class="glyphicon glyphicon-cog" ></span>Editar</asp:LinkButton>
                                    <asp:LinkButton CssClass="btn btn-sm btn-success" ID="btnGuardarTelefono" runat="server" OnClick="AgregaTelefono">
                                        <asp:Label CssClass="glyphicon glyphicon-ok" runat="server" ID="IconActualizaTelefono"></asp:Label>
                                    </asp:LinkButton>
                                    <asp:LinkButton CssClass="btn btn-sm btn-danger" ID="btnCancelarTelefono" runat="server" OnClick="CancelarTelefono"><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                                </div>
                                <div class="clearfix"></div>
                                <div class="row container-fluid text-center">
                                    <asp:TextBox CssClass="hidden" runat="server" ID="txtIdTelefono"></asp:TextBox>
                                    <div class="col-md-6">
                                        <h6>Tipo de telefono</h6>
                                        <asp:DropDownList CssClass="form-control" ID="DDLDTipoDETelefono" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-6">
                                        <h6>Telefono</h6>
                                        <asp:TextBox ID="txtDTelefono" runat="server" MaxLength="30" CssClass="form-control" TextMode="Phone"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row container-fluid text-center">
                                    <asp:GridView runat="server" Style="margin-top: 10px;" DataKeyNames="ID" ID="DGVTELEFONOS" OnRowCommand="DGVTELEFONOS_RowCommand" OnSelectedIndexChanged="DGVTELEFONOS_SelectedIndexChanged" OnRowDataBound="DGVTELEFONOS_RowDataBound" AutoGenerateColumns="false" CssClass="table table-bordered table-hover table-condensed table-striped input-sm">
                                        <EmptyDataTemplate>
                                            <div class="info">
                                                No existen telefonos guardados 
                                            </div>
                                        </EmptyDataTemplate>
                                        <SelectedRowStyle CssClass="table table-hover input-sm success" />
                                        <SortedAscendingHeaderStyle CssClass="glyphicon glyphicon-sort-by-alphabet" />
                                        <Columns>
                                            <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" />
                                            <asp:BoundField DataField="StrNombreTipoDeTelefono" HeaderText="Tipo" />
                                            <asp:BoundField DataField="NUMERO" HeaderText="Numero" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton CssClass="btn btn-sm btn-default" ID="EliminaTelefono" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" CommandName="Eliminar" runat="server">
                                                        <asp:Label ID="lblEliminarTelefono" runat="server" />
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSubirImagen" />
                            <asp:PostBackTrigger ControlID="btnSubirImagenPortada" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
