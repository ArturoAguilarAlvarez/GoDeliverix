<%@ Page Title="Configuracion" Language="C#" MasterPageFile="~/Vista/Cliente/MasterCliente.Master" AutoEventWireup="true" CodeBehind="Configuracion.aspx.cs" Inherits="WebApplication1.Vista.Cliente.Configuracion" %>

<%@ Register Assembly="GMaps" Namespace="Subgurim.Controles" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div class="col-md-12 col-sm-12 col-xs-12" style="margin-top: 15px;">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-primary">
                <div class="panel-heading text-center">
                    CONFIGURACION DE CUENTA
                </div>
                <%--Panel de mensaje de sistema--%>
                <asp:Panel CssClass="well well-lg" ID="PanelMensaje" runat="server">
                    <asp:Label ID="LblMensaje" Text="Mensaje del sistema" Font-Size="Large" runat="server" />
                    <div class="pull-right">
                        <asp:LinkButton ID="BtnCerrarPanelMensaje" CssClass="btn btn-sm btn-danger" OnClick="BtnCerrarPanelMensaje_Click" ForeColor="White" runat="server">
                            <span class="glyphicon glyphicon-remove"></span>
                        </asp:LinkButton>
                    </div>
                </asp:Panel>
                <div class="clearfix"></div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" style="margin-bottom: 5px;">
                        <li role="presentation" id="liDatosGenerales" runat="server">
                            <asp:LinkButton runat="server" ID="btnDatosGenerales" OnClick="btnDatosGenerales_Click">
                                        <span class="glyphicon glyphicon-globe">

                                                                                                                 </span> GENERAL</asp:LinkButton></li>
                        <li role="presentation" id="liDatosDireccion" runat="server">
                            <asp:LinkButton runat="server" ID="btnDatosDireccion" OnClick="btnDatosDireccion_Click">
                                        <span class="glyphicon glyphicon-road">

                                                                                                                   </span> DIRECCIONES</asp:LinkButton></li>
                        <li role="presentation" id="liDatosContacto" runat="server">
                            <asp:LinkButton runat="server" ID="btnDatosDeConectado" OnClick="btnDatosDeConectado_Click">
                                        <span class="glyphicon glyphicon-phone">  </span> CONTACTO</asp:LinkButton>

                        </li>
                    </ul>

                    <%--                    Panel de datos generales--%>
                    <asp:Panel runat="server" ID="pnlDatosGenerales">

                        <div class="pull-left">
                            <asp:LinkButton CssClass="btn btn-default btn-sm" ID="btnEditarDatosGenerales" OnClick="btnEditarDatosGenerales_Click" runat="server">
                                <span class="glyphicon glyphicon-edit"></span>
                                Editar
                            </asp:LinkButton>
                            <asp:LinkButton CssClass="btn btn-sm btn-success" ID="BtnGuardarDatosGenerales" OnClick="BtnGuardarDatosGenerales_Click" runat="server">
                                <span class="glyphicon glyphicon-ok"></span>
                            </asp:LinkButton>
                            <asp:LinkButton CssClass="btn btn-sm btn-danger" ID="BtnCancelarDatosGenerales" OnClick="BtnCancelarDatosGenerales_Click" runat="server">
                                <span class="glyphicon glyphicon-remove"></span>
                            </asp:LinkButton>
                        </div>
                        <div class="clearfix"></div>

                        <div class="row">
                            <div class="col-md-4">
                                <h6>Nombre*</h6>
                                <asp:TextBox ID="txtDNombre" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <h6>Apellido Paterno*</h6>
                                <asp:TextBox ID="txtDApellidoPaterno" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <h6>Apellido Materno*</h6>
                                <asp:TextBox ID="txtDApellidoMaterno" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <h6>Usuario*</h6>
                                <asp:TextBox ID="txtDUsuario" runat="server" AutoPostBack="true" OnTextChanged="txtDUsuario_TextChanged" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <h6>Contraseña*</h6>
                                <asp:TextBox ID="txtdContrasena" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <h6>Fecha de nacimiento</h6>
                                <asp:TextBox ID="txtDFechaDeNacimiento" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtdUidCorreo" CssClass="hidden" />
                                <h6>Correo electronico</h6>
                                <asp:TextBox ID="txtDCorreoElectronico" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                            </div>
                        </div>
                    </asp:Panel>

                    <%--                    Panelde datos de direccion--%>
                    <asp:Panel ID="PnlDirecciones" runat="server">

                        <div class="pull-left">
                            <asp:LinkButton runat="server" ID="BtnNuevo" OnClick="BtnNuevo_Click" CssClass="btn btn-sm btn-default ">
                        <span class="glyphicon glyphicon-file"></span>Nuevo</asp:LinkButton>

                        </div>
                        <div class="clearfix"></div>
                        <div class="table-responsive">
                            <%-- GridView de direcciones --%>
                            <asp:GridView runat="server" Style="margin-top: 10px;" ID="DgvDirecciones" OnRowEditing="DgvDirecciones_RowEditing" DataKeyNames="ID" AutoGenerateColumns="false" OnRowDeleting="DgvDirecciones_RowDeleting" OnRowCommand="DgvDirecciones_RowCommand" OnSelectedIndexChanged="DgvDirecciones_SelectedIndexChanged" OnRowDataBound="DgvDirecciones_RowDataBound" CssClass="table table-bordered table-hover table-condensed table-striped input-sm">
                                <EmptyDataRowStyle CssClass="info" />
                                <EmptyDataTemplate>
                                    <asp:Label Text="No hay direcciones" runat="server" />
                                </EmptyDataTemplate>
                                <SelectedRowStyle CssClass="table table-hover input-sm success" />
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

                                            <asp:LinkButton CssClass="btn btn-sm btn-default" ID="EditarDireccion" ToolTip="Editar" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" CommandName="Edit" runat="server">
                                            <span class="glyphicon glyphicon-edit"></span>
                                            </asp:LinkButton>

                                            <asp:LinkButton CssClass="btn btn-sm btn-default" ID="EliminaDireccion" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" CommandName="Delete" runat="server">
                                                <asp:Label ID="lblEliminarDireccion" runat="server" />
                                            </asp:LinkButton>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>


                    </asp:Panel>

                    <%--                    Panel de datos de contacto--%>
                    <asp:Panel runat="server" ID="pnlContacto">

                        <div class="pull-left" style="margin-top: 10px;">
                            <asp:LinkButton runat="server" ID="btnNuevoTelefono" OnClick="NuevoTelefono" CssClass="btn btn-sm btn-default "><span class="glyphicon glyphicon-file"></span>Nuevo</asp:LinkButton>
                            <asp:LinkButton runat="server" ID="btnEditarTelefono" OnClick="EditaTelefono" CssClass="btn btn-sm btn-default "><span class="glyphicon glyphicon-cog" ></span>Editar</asp:LinkButton>
                            <asp:LinkButton CssClass="btn btn-sm btn-success" ID="btnGuardarTelefono" OnClick="AgregaTelefono" runat="server">
                                <asp:Label CssClass="glyphicon glyphicon-ok" runat="server" ID="IconActualizaTelefono"></asp:Label>
                            </asp:LinkButton>
                            <asp:LinkButton CssClass="btn btn-sm btn-danger" OnClick="CancelarTelefono" ID="btnCancelarTelefono" runat="server"><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>

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


                                <asp:TextBox ID="txtDTelefono" runat="server" CssClass="form-control" TextMode="Phone"></asp:TextBox>

                            </div>
                        </div>

                        <div class="row container-fluid text-center">
                            <asp:GridView runat="server" Style="margin-top: 10px;" OnSelectedIndexChanged="DGVTELEFONOS_SelectedIndexChanged" OnRowCommand="DGVTELEFONOS_RowCommand" OnRowDataBound="DGVTELEFONOS_RowDataBound" DataKeyNames="ID" ID="DGVTELEFONOS" AutoGenerateColumns="false" CssClass="table table-bordered table-hover table-condensed table-striped input-sm">
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

                </div>
            </div>
            <div class="clearfix"></div>
            <%-- Panel izquierdo --%>
            <asp:Panel ID="PnlDetallesDireccion" Style="margin-bottom: 1300px;" runat="server">
                <div class="col-md-offset-2 col-md-9 col-sm-12 col-xs-12" style="z-index: 1; top: 100px; position: absolute;">
                    <div class="panel panel-info" style="-webkit-box-shadow: 10px 10px 61px 5px rgba(0,0,0,0.70); -moz-box-shadow: 10px 10px 61px 5px rgba(0,0,0,0.75); box-shadow: 10px 10px 61px 5px rgba(0,0,0,0.75);">
                        <div class="panel-heading text-center">
                            Datos de direccion
                            <div class="clearfix"></div>
                            <div class="pull-right" style="margin-right: 10px; margin-top: 5px;">
                                <%--Boton guardar--%>
                                <asp:LinkButton runat="server" ID="BtnAgregarDireccion" OnClick="BtnAgregarDireccion_Click" CssClass="btn btn-sm btn-success">
                                    <asp:Label runat="server" CssClass="glyphicon glyphicon-ok" ID="Label2"></asp:Label>
                                </asp:LinkButton>
                                <%--Boton cancelar--%>
                                <asp:LinkButton CssClass="btn btn-sm btn-danger" ID="btnCerrarPanelDetalleDireccion" OnClick="btnCancelarDireccion_Click" runat="server">
                            <span class="glyphicon glyphicon-remove"></span>
                                </asp:LinkButton>
                            </div>
                            <ul class="nav nav-pills">
                                <li role="presentation" id="liInformacionDireccion" runat="server">
                                    <asp:LinkButton runat="server" ID="BtnInformacionDireccion" OnClick="BtnInformacionDireccion_Click" ToolTip="General">
                                        <span class="glyphicon glyphicon-globe"></span> 
                                        Informacion</asp:LinkButton></li>
                                <li role="presentation" id="liDatosUbicacion" runat="server">
                                    <asp:LinkButton ID="btnDatosUbicacion" runat="server" OnClick="btnDatosUbicacion_Click" ToolTip="Ubicacion">
                                        <span class="glyphicon glyphicon-map-marker"></span>
                                        Ubicacion
                                    </asp:LinkButton></li>
                            </ul>
                        </div>

                        <div class="clearfix"></div>
                        <div class="panel-body">
                            <asp:Panel ID="PanelDatosDireccion" runat="server">
                                <asp:Panel runat="server" ID="pnlDireccion">
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
                                    <div class="clearfix"></div>
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
                                </asp:Panel>
                            </asp:Panel>
                            <%-- Panel de ubicacion --%>
                            <asp:Panel ID="PanelUbicacion" runat="server">



                                <%-- Funcion de javaScript para mandar la latitud y longitud a los campos de texto corresponientes --%>
                                <asp:TextBox ID="latitud" CssClass="hidden" runat="server" />
                                <asp:TextBox ID="lonjitud" CssClass="hidden" runat="server" />

                                <div class="col-md-4">
                                    <asp:TextBox CssClass="hide" ID="txtUidUbicacion" runat="server" />
                                    <label>Buscar direccion</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtBusquedaUbicacion" CssClass="form-control" runat="server" />
                                        <asp:LinkButton ID="btnBuscarUbicacion" OnClick="btnBuscarUbicacion_Click" CssClass=" input-group-addon" runat="server">
                                        <span class="glyphicon glyphicon-search"></span>
                                        </asp:LinkButton>

                                    </div>
                                </div>
                                <div class="col-md-2" style="margin-top: 25px;">
                                    <asp:LinkButton CssClass="btn btn-default" ID="btnMiUbicacion" OnClick="btnMiUbicacion_Click" ToolTip="Mi ubicación" runat="server">
                                        <span class="glyphicon glyphicon-screenshot"></span>
                                    </asp:LinkButton>
                                    
                                </div>
                                <div class="col-md-12" style="margin-top: 15px;">
                                    <div class="container-fluid">
                                        <cc1:GMap ID="MapaPrueba" ajaxUpdateProgressMessage="Cargando..." enableServerEvents="true" OnMarkerClick="MapaPrueba_MarkerClick" Width="100%" Height="600" runat="server" />
                                    </div>
                                </div>

                                <%-- AIzaSyAnrh2zheeNGeywmv1YVwddIeKgLMCWRN0 API Key Google Maps --%>
                            </asp:Panel>
                        </div>



                    </div>

                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
