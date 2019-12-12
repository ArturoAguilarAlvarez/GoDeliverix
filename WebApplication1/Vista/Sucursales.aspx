<%@ Page Language="C#" MasterPageFile="MasterDeliverix.Master" AutoEventWireup="true" CodeBehind="Sucursales.aspx.cs" Inherits="WebApplication1.Vista.Sucursales" %>

<%@ Register Assembly="GMaps" Namespace="Subgurim.Controles" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Vista/MasterDeliverix.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    SUCURSALES
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="favicon" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UPSucursales" UpdateMode="Always" runat="server" EnableViewState="True">
        <ContentTemplate>
            <div class="col-md-12">
                <!--Panel de busqueda ampliada -->
                <asp:Panel runat="server" ID="PanelBusquedaAmpliada">
                    <div class="panel panel-primary">
                        <div class="panel-heading text-center">Busqueda ampliada</div>
                        <div class="row container-fluid">
                            <div class="pull-left">
                                <asp:LinkButton runat="server" ID="btnBusquedaNormal" OnClick="BusquedaNormal" CssClass="btn btn-sm btn-default"><span class="glyphicon glyphicon-zoom-out"></span>Busqueda normal</asp:LinkButton>
                            </div>
                            <div class="pull-right">
                                <asp:LinkButton runat="server" ID="BtnBAOcultar" OnClick="MostrarYOcultarFiltrosBusquedaAvanzada" CssClass="btn btn-sm btn-default">
                                    <span class="glyphicon glyphicon-eye-open"></span>
                                    <asp:Label ID="lblBAFiltrosVisibilidad" runat="server" />
                                </asp:LinkButton><asp:LinkButton runat="server" ID="BtnBALimpiar" OnClick="BorrarFiltrosBusquedaAvanzada" CssClass="btn btn-sm btn-default"><span class="glyphicon glyphicon-trash"></span> Limpiar</asp:LinkButton><asp:LinkButton runat="server" ID="BtnBABuscar" OnClick="BuscaEmpresasBusquedaAmpliada" CssClass="btn btn-sm btn-default"><span class="glyphicon glyphicon-search"></span> Buscar</asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <!-- Panel de filtros de la busqueda ampliada -->
                            <asp:Panel ID="PanelFiltrosBusquedaAmpliada" runat="server">
                                <!-- Datos generales -->
                                <div class="row container-fluid">
                                    <div class="col-md-3">
                                        <h6>Identificador</h6>
                                        <asp:TextBox runat="server" ID="txtBAIdentificador" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <h6>Hora de apertura</h6>
                                        <asp:TextBox runat="server" ID="txtBaHorarioApertura" TextMode="Time" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <h6>Hora de cierre</h6>
                                        <asp:TextBox runat="server" ID="txtBAHorarioCierre" TextMode="Time" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <h6>Pais</h6>
                                        <asp:DropDownList ID="DDLDBAPAIS" runat="server" OnSelectedIndexChanged="ObtenerEstado" AutoPostBack="true" OnTextChanged="ObtenerEstado" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <h6>Estado</h6>
                                        <asp:DropDownList ID="DDLDBAESTADO" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ObtenerMunicipio"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <h6>Municipio</h6>
                                        <asp:DropDownList ID="DDLDBAMUNICIPIO" runat="server" OnSelectedIndexChanged="ObtenerCiudad" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <h6>Ciudad</h6>
                                        <asp:DropDownList ID="DDLDBACIUDAD" AutoPostBack="true" OnSelectedIndexChanged="ObtenerColonia" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <h6>Colonia</h6>
                                        <asp:DropDownList ID="DDLDBACOLONIA" AutoPostBack="true" OnSelectedIndexChanged="ObtenerCP" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <h6>Calle</h6>
                                        <asp:TextBox ID="txtBACalle" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <h6>Codigo postal</h6>
                                        <asp:TextBox ID="txtBACOdigoPostal" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <asp:Label ID="lblprueba" runat="server" />
                            </asp:Panel>

                            <asp:GridView runat="server" ID="DGVBUSQUEDAAMPLIADA" OnPreRender="DGVBUSQUEDAAMPLIADA_PreRender" Style="margin-top: 5px;" PageSize="2" CssClass="table table-bordered table-hover table-condensed table-striped input-sm" AutoGenerateColumns="false" DataKeyNames="ID" OnRowDataBound="GVWEmpresaAmpliada_RowDataBound" OnSelectedIndexChanged="GVBusquedaAvanzadaEmpresa_SelectedIndexChanged" AllowPaging="True" OnPageIndexChanging="GridViewBusquedaAmpliada_PageIndexChanging">
                                <EmptyDataTemplate>
                                    <div class="info">No hay coincidencia de busqueda</div>
                                </EmptyDataTemplate>
                                <SelectedRowStyle CssClass="table table-hover input-sm success" />
                                <SortedAscendingHeaderStyle CssClass="glyphicon glyphicon-sort-by-alphabet" />
                                <Columns>
                                    <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide">
                                        <FooterStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                        <ItemStyle CssClass="hide" />
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="IDENTIFICADOR" HeaderText="Identificador" SortExpression="IDENTIFICADOR" />
                                    <asp:BoundField DataField="HORAAPARTURA" HeaderText="Apertura" SortExpression="HORAAPARTURA" />
                                    <asp:BoundField DataField="HORACIERRE" HeaderText="Cierre" SortExpression="HORACIERRE" />
                                </Columns>
                                <PagerSettings Mode="NextPreviousFirstLast" Position="Top" />
                                <PagerStyle HorizontalAlign="Center" />
                                <PagerTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ImageUrl="~/Vista/Img/FlechasDoblesIzquierda.png" ID="btnPrimero" runat="server" CommandName="Page" CommandArgument="First" />
                                                <asp:ImageButton ImageUrl="~/Vista/Img/FechaIzquierda.png" ID="btnAnterior" runat="server" CommandName="Page" CommandArgument="Prev" />
                                                <asp:Label ID="lblTotalDeRegistros" runat="server" />
                                                <asp:ImageButton ImageUrl="~/Vista/Img/FechaDerecha.png" ID="btnSiguiente" runat="server" CommandName="Page" CommandArgument="Next" />
                                                <asp:ImageButton ImageUrl="~/Vista/Img/FlechasDoblesDerecha.png" ID="btnUltimo" runat="server" CommandName="Page" CommandArgument="Last" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Filas:
                                                <asp:DropDownList runat="server" ID="DDLTAMANOGRIDAMPLIADA" AutoPostBack="true" OnTextChanged="TamanioGrid" OnSelectedIndexChanged="TamanioGrid">
                                                    <asp:ListItem Text="10" Value="10" />
                                                    <asp:ListItem Text="20" Value="20" />
                                                    <asp:ListItem Text="30" Value="30" />
                                                    <asp:ListItem Text="100" Value="100" />
                                                    <asp:ListItem Text="200" Value="200" />
                                                </asp:DropDownList>

                                                Pagina:
                                                <asp:DropDownList runat="server" ID="DDLDBANUMERODEPAGINAS" AutoPostBack="true" OnTextChanged="PaginaSeleccionadaBusquedaAmpliada" OnSelectedIndexChanged="PaginaSeleccionadaBusquedaAmpliada">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </PagerTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </asp:Panel>

                <!-- Panel izquierdo -->
                <div runat="server" id="PanelIzquierdo" class="col-md-3">
                    <!-- Panel de filtros -->
                    <asp:Panel runat="server" ID="PanelDeBusqueda">
                        <div class="panel panel-primary">
                            <!--Header de panel-->
                            <div class="panel-heading text-center">
                                <h5>Busqueda de SUCURSALES</h5>
                            </div>
                            <!-- Botones de acciones del panel-->
                            <div class="row container-fluid">
                                <%--<div class="pull-left">
                                    <asp:LinkButton runat="server" ID="btnBusquedaAmpliada" OnClick="BusquedaAvanzada" CssClass="btn btn-sm btn-default"><span class="glyphicon glyphicon-zoom-in"></span> Busqueda ampliada</asp:LinkButton>
                                </div>--%>
                                <div class="pull-right">
                                    <asp:LinkButton runat="server" ID="btnMostrarFiltros" OnClick="MostrarYOcultarFiltrosBusquedaNormal" CssClass="btn btn-sm btn-default">
                                        <span class="glyphicon glyphicon-eye-open"></span>
                                        <asp:Label ID="lblVisibilidadfiltros" runat="server" />
                                    </asp:LinkButton><asp:LinkButton runat="server" ID="btnBorrarFiltros" OnClick="VaciarFiltros" CssClass="btn btn-sm btn-default disabled"><span class="glyphicon glyphicon-trash"></span> Limpiar</asp:LinkButton><asp:LinkButton runat="server" ID="btnBuscar" OnClick="BuscarEmpresasBusquedaNormal" CssClass="btn btn-sm btn-default disabled"><span class="glyphicon glyphicon-search"></span> Buscar</asp:LinkButton>
                                </div>
                            </div>
                            <!-- Contenido del panel-->
                            <div class="panel-body">
                                <asp:Panel runat="server" ID="PnlFiltros">
                                    <div class="clearfix"></div>
                                    <div class="row ">
                                        <div class="col-md-12">
                                            <h6>Identificador</h6>
                                            <asp:TextBox runat="server" ID="txtFIdentificador" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <h6>Hora de apertura</h6>
                                            <asp:TextBox runat="server" ID="txtFHoraApertura" TextMode="Time" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <h6>Hora de cierre</h6>
                                            <asp:TextBox runat="server" ID="txtFHoraCierre" TextMode="Time" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="table-responsive">
                                    <!-- GridView de empresas simple-->
                                    <asp:GridView runat="server" ID="DGVEMPRESAS" OnPreRender="DGVEMPRESAS_PreRender" PageSize="5" AllowSorting="true" Style="margin-top: 5px;" OnSorting="DGVEMPRESASNORMAL_Sorting" CssClass="table table-bordered table-hover table-condensed table-striped input-sm" AutoGenerateColumns="false" DataKeyNames="ID" OnRowDataBound="GVWEmpresaNormal_RowDataBound" OnSelectedIndexChanged="GVWEmpresaBusquedaNormal_SelectedIndexChanged" AllowPaging="True" OnPageIndexChanging="GridViewBusquedaNormal_PageIndexChanging" PagerSettings-Mode="NextPreviousFirstLast">
                                        <EmptyDataTemplate>
                                            <div class="info">Para ver información debe hacer una consulta </div>
                                        </EmptyDataTemplate>
                                        <SelectedRowStyle CssClass="table table-hover input-sm success" />
                                        <SortedAscendingHeaderStyle CssClass="glyphicon glyphicon-sort-by-alphabet" />
                                        <Columns>
                                            <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide">
                                                <FooterStyle CssClass="hide" />
                                                <HeaderStyle CssClass="hide" />
                                                <ItemStyle CssClass="hide" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="IDENTIFICADOR" HeaderText="Identificador" SortExpression="IDENTIFICADOR" />
                                            <asp:BoundField DataField="HORAAPARTURA" HeaderText="Apertura" SortExpression="HORAAPARTURA" />
                                            <asp:BoundField DataField="HORACIERRE" HeaderText="Cierre" SortExpression="HORACIERRE" />
                                        </Columns>
                                        <PagerSettings Mode="NextPreviousFirstLast" Position="Top" />
                                        <PagerStyle HorizontalAlign="Center" />
                                        <PagerTemplate>
                                            <table class="text-center">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ImageUrl="~/Vista/Img/FlechasDoblesIzquierda.png" ID="btnPrimero" runat="server" CommandName="Page" CommandArgument="First" />
                                                        <asp:ImageButton ImageUrl="~/Vista/Img/FechaIzquierda.png" ID="btnAnterior" runat="server" CommandName="Page" CommandArgument="Prev" />

                                                        <asp:Label ID="lblTotalDeRegistros" runat="server" />
                                                        <asp:ImageButton ImageUrl="~/Vista/Img/FechaDerecha.png" ID="btnSiguiente" runat="server" CommandName="Page" CommandArgument="Next" />
                                                        <asp:ImageButton ImageUrl="~/Vista/Img/FlechasDoblesDerecha.png" ID="btnUltimo" runat="server" CommandName="Page" CommandArgument="Last" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Pagina
                                                        <asp:DropDownList runat="server" ID="DDLDNUMERODEPAGINAS" AutoPostBack="true" OnTextChanged="PaginaSeleccionadaBusquedaNormal" OnSelectedIndexChanged="PaginaSeleccionadaBusquedaNormal">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </PagerTemplate>
                                    </asp:GridView>
                                </div>

                            </div>
                        </div>
                    </asp:Panel>

                </div>
                <!-- Panel Derecho -->
                <div runat="server" id="PanelDerecho" class="col-md-9">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h5>Gention de sucursal:
                                <asp:Label ID="txtNombreDeSucursal" runat="server" /></h5>
                        </div>
                        <div class="clearfix"></div>
                        <div class=" pull-left">
                            <asp:TextBox ID="txtUidSucursal" CssClass="hide" runat="server" />
                            <asp:LinkButton runat="server" ID="btnNuevo" OnClick="ActivarCajasDeTexto" CssClass="btn btn-sm btn-default "><span class="glyphicon glyphicon-file"></span> Nuevo</asp:LinkButton>
                            <asp:LinkButton runat="server" ID="btnEditar" OnClick="ActivarEdicion" CssClass="btn btn-sm btn-default disabled"><span class="glyphicon glyphicon-cog"></span> Editar</asp:LinkButton><asp:LinkButton runat="server" ID="btnGuardar" OnClick="GuardarDatos" CssClass="btn btn-sm btn-success ">
                                <asp:Label runat="server" ID="lblGuardarDatos"></asp:Label>
                            </asp:LinkButton><asp:LinkButton runat="server" ID="btnCancelar" OnClick="CancelarAgregacion" CssClass="btn btn-sm btn-danger "><asp:label CssClass=" glyphicon glyphicon-remove" runat="server" /></asp:LinkButton><asp:LinkButton ID="btnRecargarSucursal" OnClick="btnRecargarSucursal_Click" CssClass="btn btn-sm btn-default" runat="server">
                                <asp:Label CssClass="glyphicon glyphicon-refresh" runat="server" />
                            </asp:LinkButton>
                        </div>
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
                        <div class="clearfix"></div>
                        <div class="panel-body">
                            <%-- Barra de navegacion --%>
                            <ul class="nav nav-tabs NavLarge" style="margin-bottom: 5px;">
                                <li role="presentation" id="liDatosGenerales" runat="server">
                                    <asp:LinkButton runat="server" ID="btnDatosGenerales" OnClick="PanelGeneral">
                                        <span class="glyphicon glyphicon-globe"></span> GENERAL</asp:LinkButton></li>
                                <li role="presentation" id="liDatosDireccion" runat="server">
                                    <asp:LinkButton runat="server" ID="btnDatosDireccion" OnClick="PanelDireccion">
                                        <span class="glyphicon glyphicon-road">                                </span> DIRECCION</asp:LinkButton></li>
                                <li role="presentation" id="liDatosContacto" runat="server">
                                    <asp:LinkButton runat="server" ID="btnDatosDeConectado" OnClick="PanelContacto">
                                        <span class="glyphicon glyphicon-phone">  </span> CONTACTO</asp:LinkButton></li>
                                <li role="presentation" id="liDatosGiro" runat="server">
                                    <asp:LinkButton runat="server" ID="LinkButton1" OnClick="PanelGiro" ToolTip="Giro">
                                        <span class="glyphicon glyphicon-tasks"></span> GIRO</asp:LinkButton></li>
                                <li role="presentation" id="liDatosCategoria" runat="server">
                                    <asp:LinkButton runat="server" ID="btnGridCategoria" OnClick="btnGridCategoria_Click">
                                        <span class="glyphicon glyphicon-object-align-horizontal"></span>
                                        CATEGORIA
                                    </asp:LinkButton></li>
                                <li role="presentation" id="liDatosSubcategoria" runat="server">
                                    <asp:LinkButton runat="server" ID="btnGridSubcategoria" OnClick="btnGridSubcategoria_Click">
                                        <span class="glyphicon glyphicon-object-align-left"></span>
                                        SUBCATEGORIA
                                    </asp:LinkButton></li>
                                <li role="presentation" id="liDatosZonaDeServicio" runat="server">
                                    <asp:LinkButton ID="BtnZonaDeServicio" OnClick="BtnZonaDeServicio_Click" runat="server">
                                        <span class="glyphicon glyphicon-dashboard"></span>
                                        ZONA DE SERVICIO
                                    </asp:LinkButton></li>
                                <li role="presentation" id="liDatosContrato" runat="server">
                                    <asp:LinkButton ID="btnContrato" OnClick="btnContrato_Click" runat="server">
                                        <span class="glyphicon glyphicon-link"></span>
                                        CONTRATO
                                    </asp:LinkButton></li>
                                <li role="presentation" id="liDatosAtencionACliente" runat="server">
                                    <asp:LinkButton ID="btnServicioCliente" OnClick="btnServicioCliente_Click" runat="server">
                                        <span class="glyphicon glyphicon-certificate"></span>
                                        SERVICIO A CLIENTES
                                    </asp:LinkButton></li>
                            </ul>
                            <%-- Panel de datos generales --%><asp:Panel runat="server" ID="pnlDatosGenerales">
                                <%--Informacion general--%>
                                <div class="row" style="margin-top: 5px;">
                                    <asp:TextBox runat="server" ID="txtDUisSucursal" CssClass="hidden" />
                                    <div class="col-md-3">
                                        <h6>Identificador</h6>
                                        <asp:TextBox runat="server" ID="txtIdetificador" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <h6>Hora de apertura</h6>
                                        <asp:TextBox runat="server" ID="txtDHoraApertura" CssClass="form-control" TextMode="Time"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <h6>Hora de cierre</h6>
                                        <asp:TextBox runat="server" ID="txtDHoraCierre" CssClass="form-control" TextMode="Time"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <h6>Estatus</h6>
                                        <asp:DropDownList ID="ddlEstatusSucursal" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <%--Informacion de licencia--%>
                                <div class=" col-md-6" style="margin-top: 15px;">
                                    <asp:CheckBox runat="server" ID="chkVisibilidadInformacion" TextAlign="Left" Text=" Información visible " />
                                </div>
                                <div class=" col-md-3" style="margin-top: 15px;">
                                    <label>Generar codigo</label>
                                    <asp:LinkButton ID="btnGenerarCodigoDeBusqueda" OnClick="btnGenerarCodigoDeBusqueda_Click" CssClass="btn btn-sm btn-default" runat="server">
                                <img src="Img/Iconos/Generador.png" height="20" />
                                Generar
                                    </asp:LinkButton>
                                </div>
                                <div class="col-md-3" style="margin-top: 15px;">
                                    <asp:TextBox ID="txtClaveDeBusqueda" CssClass="form-control" runat="server" />
                                </div>

                                <div class="row" style="margin-top: 5px;">
                                    <div class="col-md-12 table-responsive">
                                        <asp:GridView AutoGenerateColumns="false" ID="DgvLicencia" OnSelectedIndexChanged="DgvLicencia_SelectedIndexChanged" OnRowUpdating="DgvLicencia_RowUpdating" OnRowCancelingEdit="DgvLicencia_RowCancelingEdit" OnRowDeleting="DgvLicencia_RowDeleting" OnRowDataBound="DgvLicencia_RowDataBound" OnRowEditing="DgvLicencia_RowEditing" DataKeyNames="UidLicencia" runat="server" CssClass="table table-bordered table-hover table-condensed table-striped input-sm">
                                            <EmptyDataTemplate>
                                                <div class="info">
                                                    No existen licencias
                                                </div>
                                            </EmptyDataTemplate>
                                            <SelectedRowStyle CssClass="table table-hover input-sm success" />
                                            <Columns>
                                                <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" />

                                                <asp:TemplateField HeaderText="Identificador" HeaderStyle-CssClass="text-center">
                                                    <EditItemTemplate>
                                                        <asp:TextBox CssClass="form-control" ID="txtIdentificador" runat="server" Text='<%# Eval("VchIdentificador") %>' />
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox CssClass="form-control" ID="lblIdentificador" runat="server" Text='<%# Eval("VchIdentificador") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Licencia" HeaderStyle-CssClass="text-center">
                                                    <ControlStyle CssClass="text-center" />
                                                    <EditItemTemplate>
                                                        <asp:Label Text='<%# Eval("UidLicencia") %>' runat="server" />
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%# Eval("UidLicencia") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- Columna de icono de estatus --%>
                                                <asp:TemplateField HeaderText="Estatus" HeaderStyle-CssClass="text-center">
                                                    <ItemStyle CssClass="text-center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEstatus" Style="font-size: 30px" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- Columna de icono de disponibilidad --%>
                                                <asp:TemplateField HeaderText="En uso" HeaderStyle-CssClass="text-center">
                                                    <ItemStyle CssClass="text-center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDisponibilidad" Style="font-size: 30px" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="BLUso" ItemStyle-CssClass="hide" HeaderStyle-CssClass="hide" />
                                                <%-- Boton de nuevo --%>
                                                <%--<asp:TemplateField>
                                                    <ItemStyle CssClass="text-center" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnNuevoLicencia" OnClick="btnNuevoLicencia_Click" CssClass="btn btn-sm btn-success" ToolTip="Agregar" runat="server">
                                                            <span class="glyphicon glyphicon-plus"></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:BoundField DataField="UidEstatus" ItemStyle-CssClass="hide" HeaderStyle-CssClass="hide" HeaderText="Estatus" />

                                                <asp:BoundField DataField="BLUso" ItemStyle-CssClass="hide" HeaderStyle-CssClass="hide" HeaderText="Disponibilidad" />
                                                <%-- Boton de modificar --%>
                                                <asp:TemplateField>
                                                    <ItemStyle CssClass="text-center" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnModificarLicencia" CssClass="btn btn-sm " BackColor="Black" CommandName="Edit" CommandArgument="Edicion" ToolTip="Modificar" runat="server">
                                                            <span style="color:white" class="glyphicon glyphicon-pencil"></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- Boton estatus --%><asp:TemplateField>
                                                    <ItemStyle CssClass="text-center" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEstatusLicencia" CommandName="Estatus" OnClick="btnEstatusLicencia_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ToolTip="Cambiar estatus" runat="server">
                                                            <asp:Label ID="lblIconoEstatus" runat="server" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- Boton eliminar --%>
                                                <%-- <asp:TemplateField>
                                                    <ItemStyle CssClass="text-center" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEliminarLicencia" CssClass="btn btn-sm btn-danger" ToolTip="Eliminar" CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server">
                                                            <span class="glyphicon glyphicon-trash"></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <%-- Boton para renovar --%><asp:TemplateField>
                                                    <ItemStyle CssClass="text-center" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton CssClass="btn btn-sm btn-warning" OnClick="btnRenovarLicencia_Click" ToolTip="Renovar licencia" ID="btnRenovarLicencia" CommandName="Restaura" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server">
                                                            <span class="glyphicon glyphicon-retweet"></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- Boton para aceptar --%><asp:TemplateField>
                                                    <ItemStyle CssClass="text-center" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnAceptarLicencia" Visible="false" CssClass="btn btn-sm btn-success" ToolTip="Aceptar" CommandName="Update" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server">
                                                            <span class="glyphicon glyphicon-ok"></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- Boton para cancelar --%><asp:TemplateField>
                                                    <ItemStyle CssClass="text-center" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnCancelarLicencia" Visible="false" CssClass="btn btn-sm btn-danger" ToolTip="Cancelar" CommandName="Cancel" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server">
                                                            <span class="glyphicon glyphicon-remove"></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </asp:Panel>
                            <%-- Panel de direccion --%><asp:Panel runat="server" ID="pnlDireccion">
                                <%--Navegacion de direccion--%>
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
                                <%--Panel de datos de direccion--%><asp:Panel ID="PanelDatosDireccion" runat="server">
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
                                <%-- Panel de ubicacion --%><asp:Panel ID="PanelUbicacion" runat="server">

                                    <%-- Funcion de javaScript para mandar la latitud y longitud a los campos de texto corresponientes --%>
                                    <div class="col-md-3">
                                        <label>Radio de alcance(KM)</label>
                                        <div class=" input-group">
                                            <asp:TextBox CssClass="form-control" ID="txtRadio" runat="server" ToolTip="Kilometros" TextMode="Number" />
                                            <asp:LinkButton CssClass="input-group-addon" ID="btnRadioMarcador" OnClick="btnRadioMarcador_Click" ToolTip="Definir alcance" runat="server">
                                            <span  class="glyphicon glyphicon-plus">
                                            </span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
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
                                            <cc1:GMap ID="MapaPrueba" enableServerEvents="true" OnMarkerClick="MapaPrueba_MarkerClick" Width="100%" runat="server" />
                                        </div>
                                    </div>

                                    <%-- AIzaSyAnrh2zheeNGeywmv1YVwddIeKgLMCWRN0 API Key Google Maps --%>
                                </asp:Panel>

                            </asp:Panel>
                            <%-- Panel de contacto --%>
                            <asp:Panel runat="server" ID="pnlContacto">

                                <div class="pull-left" style="margin-top: 10px;">
                                    <asp:LinkButton runat="server" ID="btnNuevoTelefono" OnClick="NuevoTelefono" CssClass="btn btn-sm btn-default disabled"><span class="glyphicon glyphicon-file"></span>Nuevo</asp:LinkButton><asp:LinkButton runat="server" ID="btnEditarTelefono" OnClick="EditaTelefono" CssClass="btn btn-sm btn-default disabled"><span class="glyphicon glyphicon-cog" ></span>Editar</asp:LinkButton><asp:LinkButton CssClass="btn btn-sm btn-success" ID="btnGuardarTelefono" runat="server" OnClick="AgregaTelefono">
                                        <asp:Label CssClass="glyphicon glyphicon-ok" runat="server" ID="IconActualizaTelefono"></asp:Label>
                                    </asp:LinkButton><asp:LinkButton CssClass="btn btn-sm btn-danger" ID="btnCancelarTelefono" runat="server" OnClick="CancelarTelefono"><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                                </div>
                                <div class="clearfix"></div>


                                <div class="row container-fluid text-center">


                                    <asp:TextBox CssClass="hidden" runat="server" ID="txtIdTelefono"></asp:TextBox><div class="col-md-6">
                                        <h6>Tipo de telefono</h6>
                                        <asp:DropDownList CssClass="form-control" ID="DDLDTipoDETelefono" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-6">
                                        <h6>Telefono</h6>
                                        <asp:TextBox ID="txtDTelefono" MaxLength="30" runat="server" CssClass="form-control" TextMode="Phone"></asp:TextBox>
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
                                            <asp:BoundField DataField="UidTipo" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" />
                                            <asp:TemplateField HeaderText="Tipo de telefono">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtTipoDeTelefono" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                            <%-- Panel de Giro --%><asp:Panel ID="pnlGiro" runat="server">
                                <div style="margin-top: 5px;">
                                    <div class="col-md-12" style="overflow-x: hidden; overflow: scroll; height: 400px; margin-left: 15px">

                                        <asp:DataList RepeatColumns="4" ID="DLGiro" DataKeyField="UIDVM" OnItemCommand="DLGiro_ItemCommand" RepeatDirection="Horizontal" RepeatLayout="Table" runat="server">
                                            <SeparatorStyle BackColor="White" Width="150" />
                                            <SeparatorTemplate />

                                            <ItemTemplate>
                                                <div style="border: solid 2px blue;">
                                                    <img src="<%#  Eval("RUTAIMAGEN") %>" width="150" height="150" />
                                                </div>
                                                <div style="background-color: Highlight; z-index: -1">
                                                    <asp:CheckBox ID="ChkGiro" Style="position: relative; bottom: 158px; left: 141px;" runat="server" />
                                                    <asp:Label ID="lblDescripcion" Style="position: relative; left: 5px; bottom: 10px;" runat="server">
                                                                      <h6><%# Eval("STRNOMBRE") %></h6>  
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                </div>
                            </asp:Panel>
                            <%-- Panel de categoria --%><asp:Panel ID="pnlCategoria" runat="server">

                                <div class="col-md-3">
                                    <label>Giros seleccionados</label>
                                    <div style="overflow-x: hidden; overflow: scroll; height: 400px; margin-top: 5px;">

                                        <asp:DataList ID="DLGiroSeleccionado" RepeatColumns="1" DataKeyField="UIDVM" OnItemCommand="DLGiroSeleccionado_ItemCommand" RepeatDirection="Horizontal" CellSpacing="3" RepeatLayout="Table" runat="server">

                                            <SelectedItemStyle BorderStyle="Solid" BackColor="Red" />
                                            <SelectedItemTemplate>

                                                <asp:LinkButton CommandName="unselect" CssClass=" btn-default" runat="server">
                                                    <div>
                                                        <img src="<%#  Eval("RUTAIMAGEN") %>" width="136" height="100" />
                                                    </div>

                                                    <asp:Label ID="lblDescripcion" runat="server">
                                                                      <h6 style="color:white"><%# Eval("STRNOMBRE") %></h6>  
                                                    </asp:Label>

                                                </asp:LinkButton>
                                            </SelectedItemTemplate>
                                            <SeparatorStyle Height="50" Width="20" />
                                            <SeparatorTemplate />
                                            <ItemStyle BorderStyle="Solid" BackColor="Blue" />
                                            <ItemTemplate>
                                                <asp:LinkButton CommandName="select" CssClass="btn-default" runat="server">
                                                    <div>
                                                        <img src="<%#  Eval("RUTAIMAGEN") %>" style="top: 50%; right: 50%;" width="136" height="100" />
                                                    </div>

                                                    <asp:Label ID="lblDescripcion" BackColor="Highlight" runat="server">
                                                                      <h6 style="color:white"><%# Eval("STRNOMBRE") %></h6>  
                                                    </asp:Label>

                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                </div>
                                <div class="col-md-9" style="margin-top: 5px;">
                                    <label>Seleccionar Categorias</label>
                                    <asp:DataList ID="DLCategoria" RepeatColumns="3" DataKeyField="UIDCATEGORIA" RepeatDirection="Horizontal" CellSpacing="2" RepeatLayout="Table" runat="server">
                                        <SeparatorStyle Width="50" />
                                        <SeparatorTemplate></SeparatorTemplate>
                                        <ItemTemplate>
                                            <div style="border: solid 2px blue;">
                                                <img src="<%#  Eval("RUTAIMAGEN") %>" width="150" height="150" />
                                            </div>
                                            <div style="background-color: Highlight; z-index: -1">

                                                <asp:CheckBox ID="ChkCategoria" Style="position: relative; bottom: 158px; left: 141px;" runat="server" />
                                                <asp:Label ID="lblDescripcion" Style="position: relative; left: 5px; bottom: 10px;" runat="server">
                                                                      <h6><%# Eval("STRNOMBRE") %></h6>  
                                                </asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                            </asp:Panel>
                            <%-- Panel de subcategoria --%><asp:Panel ID="pnlSubcategoria" runat="server">
                                <div class="col-md-3">
                                    <label>Categorias seleccionadas</label>
                                    <div style="overflow-x: hidden; overflow: scroll; height: 400px; margin-top: 5px;">

                                        <asp:DataList ID="DlCategoriaSeleccionada" RepeatColumns="1" DataKeyField="UIDCATEGORIA" OnItemCommand="DlCategoriaSeleccionada_ItemCommand" RepeatDirection="Horizontal" CellSpacing="3" RepeatLayout="Table" runat="server">
                                            <SelectedItemStyle BorderStyle="Solid" BackColor="Red" />
                                            <SelectedItemTemplate>

                                                <asp:LinkButton CommandName="unselect" CssClass=" btn-default" runat="server">
                                                    <div>
                                                        <img src="<%#  Eval("RUTAIMAGEN") %>" width="136" height="100" />
                                                    </div>

                                                    <asp:Label ID="lblDescripcion" runat="server">
                                                                      <h6 style="color:white"><%# Eval("STRNOMBRE") %></h6>  
                                                    </asp:Label>

                                                </asp:LinkButton>
                                            </SelectedItemTemplate>
                                            <SeparatorStyle Height="50" Width="20" />
                                            <SeparatorTemplate />
                                            <ItemStyle BorderStyle="Solid" BackColor="Blue" />
                                            <ItemTemplate>
                                                <asp:LinkButton CommandName="select" CssClass="btn-default" runat="server">
                                                    <div>
                                                        <img src="<%#  Eval("RUTAIMAGEN") %>" style="top: 50%; right: 50%;" width="136" height="100" />
                                                    </div>

                                                    <asp:Label ID="lblDescripcion" BackColor="Highlight" runat="server">
                                                                      <h6 style="color:white"><%# Eval("STRNOMBRE") %></h6>  
                                                    </asp:Label>

                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                </div>
                                <div class="col-md-9">
                                    <label>Seleccionar subcategorias</label>
                                    <asp:DataList ID="dlSubcategoria" RepeatColumns="4" DataKeyField="UID" RepeatDirection="Horizontal" CellSpacing="3" RepeatLayout="Table" runat="server">
                                        <SeparatorStyle Width="50" />
                                        <SeparatorTemplate></SeparatorTemplate>
                                        <ItemTemplate>
                                            <div style="border: solid 2px blue;">
                                                <img src="<%#  Eval("RUTAIMAGEN") %>" width="150" height="150" />
                                            </div>
                                            <div style="background-color: Highlight; z-index: -1">

                                                <asp:CheckBox ID="ChkSubcategoria" Style="position: relative; bottom: 158px; left: 141px;" runat="server" />
                                                <asp:Label ID="lblDescripcion" Style="position: relative; left: 5px; bottom: 10px;" runat="server">
                                                                      <h6><%# Eval("STRNOMBRE") %></h6>  
                                                </asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="PanelZonaDeServicio" runat="server">

                                <ul class="nav nav-pills">
                                    <li role="presentation" id="liZonaDeRecolecta" runat="server">
                                        <asp:LinkButton ID="btnZonaDeRecolecta" OnClick="btnZonaDeRecolecta_Click" runat="server">
                                        <span class="glyphicon glyphicon-cloud-download"></span>
                                        RECOLECTA
                                        </asp:LinkButton></li>
                                    <li role="presentation" id="liDatosZonaDeEntrega" runat="server">
                                        <asp:LinkButton ID="btnDatosZonaDeServicio" OnClick="btnDatosZonaDeServicio_Click" runat="server">
                                        <span class="glyphicon glyphicon-cloud-upload"></span>
                                        ENTREGA
                                        </asp:LinkButton></li>
                                    <li role="presentation" id="liDatosTarifario" runat="server">
                                        <asp:LinkButton ID="BtnTarifario" OnClick="BtnTarifario_Click" runat="server">
                                            <span class="glyphicon glyphicon-calendar"></span>
                                            TARIFARIO
                                        </asp:LinkButton></li>
                                </ul>
                                <%-- Panel de zona de servicio --%><asp:Panel ID="PanelZonasServicio" runat="server">
                                    <div style="margin-top: 15px">
                                        <div class="pull-left">
                                        </div>
                                        <div class="clearfix"></div>
                                        <%-- Filtros para la seleccion de ciudad --%>

                                        <div class=" col-md-12 text-center" style="border: solid; border-color: gainsboro;">

                                            <div class="col-md-12">
                                                <label>Busqueda de ciudad</label>
                                            </div>
                                            <div class="col-md-10 container" style="margin-bottom: 10px;">
                                                <div class="col-md-3">
                                                    <h6>Pais</h6>
                                                    <asp:DropDownList ID="DDLZonaPais" runat="server" OnSelectedIndexChanged="ObtenerEstado" AutoPostBack="true" OnTextChanged="ObtenerEstado" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-3">
                                                    <h6>Estado</h6>
                                                    <asp:DropDownList ID="DDLZonaEstado" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ObtenerMunicipio"></asp:DropDownList>

                                                </div>
                                                <div class="col-md-3">
                                                    <h6>Municipio</h6>
                                                    <asp:DropDownList ID="DDLZonaMunicipio" runat="server" OnSelectedIndexChanged="ObtenerCiudad" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>

                                                </div>
                                                <div class="col-md-3">
                                                    <h6>Ciudad</h6>
                                                    <asp:DropDownList ID="DDLZonaCiudad" AutoPostBack="true" OnSelectedIndexChanged="ObtenerColonia" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>

                                            </div>
                                            <div class="col-md-2" style="margin-bottom: 10px;">
                                                <asp:LinkButton ID="btnAgregarCiudad" Style="margin-top: 37px;" ToolTip="Agregar" OnClick="btnAgregarCiudad_Click" CssClass="btn btn-sm btn-success" runat="server">
                                                <span class="glyphicon glyphicon-plus"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <%-- Contenido del panel --%><div class="col-md-12">
                                            <div class="col-md-5" style="margin-top: 20px;">

                                                <%-- Filtros de busqueda y control de la lista de colonias --%>
                                                <div class="row">
                                                    <%-- Filtros de busqueda --%>
                                                    <div class="col-md-6">
                                                        <label>Mostrar</label>
                                                        <asp:DropDownList ID="DDLTipoDeColonias" AutoPostBack="true" OnSelectedIndexChanged="DDLTipoDeColonias_SelectedIndexChanged" CssClass="form-control" runat="server">
                                                            <asp:ListItem Text="Todos" />
                                                            <asp:ListItem Text="Seleccionados" />
                                                            <asp:ListItem Text="Deseleccionados" />
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtBusquedaColonia" CssClass="form-control" runat="server" />
                                                            <asp:LinkButton ID="btnBusquedaColonia" OnClick="btnBusquedaColonia_Click" CssClass=" input-group-addon" ToolTip="Buscar" runat="server">
                                                        <span class="glyphicon glyphicon-search">
                                                        </span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div style="margin-top: 20px;">
                                                    <div class="col-md-offset-4">
                                                    </div>
                                                </div>
                                                <%-- Gridview de las ciudades seleccionadas --%>
                                                <div class="table table-responsive">
                                                    <asp:GridView runat="server" ID="DGVZonaCiudades" OnRowCommand="DGVZonaCiudades_RowCommand" OnRowDeleting="DGVZonaCiudades_RowDeleting" OnRowDataBound="DGVZonaCiudades_RowDataBound" OnSelectedIndexChanged="DGVZonaCiudades_SelectedIndexChanged" PageSize="10" AllowSorting="false" Style="margin-top: 5px;" CssClass="table table-bordered table-hover table-condensed table-striped input-sm" AutoGenerateColumns="false" DataKeyNames="ID" AllowPaging="True">
                                                        <EmptyDataTemplate>
                                                            <div class="info">No hay ciudades seleccionadas </div>
                                                        </EmptyDataTemplate>
                                                        <SelectedRowStyle CssClass="table table-hover input-sm success" />
                                                        <%--                                                <SortedAscendingHeaderStyle CssClass="glyphicon glyphicon-sort-by-alphabet" />--%>
                                                        <Columns>
                                                            <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide">
                                                                <FooterStyle CssClass="hide" />
                                                                <HeaderStyle CssClass="hide" />
                                                                <ItemStyle CssClass="hide" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="NOMBRECIUDAD" HeaderText="Ciudad" SortExpression="NOMBRECIUDAD" />

                                                            <asp:TemplateField>
                                                                <ItemStyle CssClass="text-center" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnEliminaZona" CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn btn-sm btn-default" ToolTip="Quitar ciudad" runat="server">
                                                <span class="glyphicon glyphicon-trash"></span>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="col-md-7" style="margin-top: 15px;">
                                                <div style="overflow-x: hidden; overflow: scroll; height: 400px;">

                                                    <asp:CheckBox Text="TODOS" AutoPostBack="true" Style="margin-left: 50px;" TextAlign="Right" CssClass="checkbox" ToolTip="seleccionar todos" ID="chkSeleccionarTodos" OnCheckedChanged="chkSeleccionarTodos_CheckedChanged" runat="server" />
                                                    <asp:CheckBoxList ID="chklColonias" AutoPostBack="false" Style="margin-left: 50px;" RepeatColumns="1" RepeatDirection="Vertical" RepeatLayout="Table" CssClass="checkbox" runat="server">
                                                    </asp:CheckBoxList>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <%-- Panel de zona de recolecta --%>
                                <asp:Panel ID="PanelZonaDeRecolecta" runat="server">
                                    <div style="margin-top: 15px">
                                        <div class="pull-left">
                                        </div>
                                        <div class="clearfix"></div>
                                        <%-- Filtros para la seleccion de ciudad --%>
                                        <div class=" col-md-12 text-center" style="border: solid; border-color: gainsboro;">
                                            <div class="col-md-12">
                                                <label>Busqueda de ciudad</label>
                                            </div>
                                            <div class="col-md-10 container" style="margin-bottom: 10px;">
                                                <div class="col-md-3">
                                                    <h6>Pais</h6>
                                                    <asp:DropDownList ID="DDLZRPais" runat="server" OnSelectedIndexChanged="ObtenerEstado" AutoPostBack="true" OnTextChanged="ObtenerEstado" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-3">
                                                    <h6>Estado</h6>
                                                    <asp:DropDownList ID="DDLZREstado" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ObtenerMunicipio"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-3">
                                                    <h6>Municipio</h6>
                                                    <asp:DropDownList ID="DDLZRMunicipio" runat="server" OnSelectedIndexChanged="ObtenerCiudad" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-3">
                                                    <h6>Ciudad</h6>
                                                    <asp:DropDownList ID="DDLZRCiudad" AutoPostBack="true" OnSelectedIndexChanged="ObtenerColonia" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>

                                            </div>
                                            <div class="col-md-2" style="margin-bottom: 10px;">
                                                <asp:LinkButton ID="btnZRAgregaCiudad" Style="margin-top: 37px;" ToolTip="Agregar" OnClick="btnAgregarCiudad_Click" CssClass="btn btn-sm btn-success" runat="server">
                                                <span class="glyphicon glyphicon-plus"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <%-- Contenido del panel --%><div class="col-md-12">
                                            <div class="col-md-5" style="margin-top: 20px;">

                                                <%-- Filtros de busqueda y control de la lista de colonias --%>
                                                <div class="row">
                                                    <%-- Filtros de busqueda --%>
                                                    <div class="col-md-6">
                                                        <label>Mostrar</label>
                                                        <asp:DropDownList ID="ddlZRTIpoSeleccion" AutoPostBack="true" OnSelectedIndexChanged="ddlZRTIpoSeleccion_SelectedIndexChanged" CssClass="form-control" runat="server">
                                                            <asp:ListItem Text="Todos" />
                                                            <asp:ListItem Text="Seleccionados" />
                                                            <asp:ListItem Text="Deseleccionados" />
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtZRBusquedaColonia" CssClass="form-control" runat="server" />
                                                            <asp:LinkButton ID="btnZrBusquedaColonia" OnClick="btnZrBusquedaColonia_Click" CssClass=" input-group-addon" ToolTip="Buscar" runat="server">
                                                        <span class="glyphicon glyphicon-search">
                                                        </span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div style="margin-top: 20px;">
                                                    <div class="col-md-offset-4">
                                                    </div>
                                                </div>
                                                <%-- Gridview de las ciudades seleccionadas --%>
                                                <div class="table table-responsive">
                                                    <asp:GridView runat="server" ID="DGVZRCiudades" OnRowCommand="DGVZRCiudades_RowCommand" OnRowDeleting="DGVZRCiudades_RowDeleting" OnRowDataBound="DGVZRCiudades_RowDataBound" OnSelectedIndexChanged="DGVZRCiudades_SelectedIndexChanged" PageSize="10" AllowSorting="false" Style="margin-top: 5px;" CssClass="table table-bordered table-hover table-condensed table-striped input-sm" AutoGenerateColumns="false" DataKeyNames="ID" AllowPaging="True">
                                                        <EmptyDataTemplate>
                                                            <div class="info">No hay ciudades seleccionadas </div>
                                                        </EmptyDataTemplate>
                                                        <SelectedRowStyle CssClass="table table-hover input-sm success" />
                                                        <%--                                                <SortedAscendingHeaderStyle CssClass="glyphicon glyphicon-sort-by-alphabet" />--%>
                                                        <Columns>
                                                            <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide">
                                                                <FooterStyle CssClass="hide" />
                                                                <HeaderStyle CssClass="hide" />
                                                                <ItemStyle CssClass="hide" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="NOMBRECIUDAD" HeaderText="Ciudad" SortExpression="NOMBRECIUDAD" />

                                                            <asp:TemplateField>
                                                                <ItemStyle CssClass="text-center" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnEliminaZona" CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn btn-sm btn-default" ToolTip="Quitar ciudad" runat="server">
                                                <span class="glyphicon glyphicon-trash"></span>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="col-md-7" style="margin-top: 15px;">
                                                <div style="overflow-x: hidden; overflow: scroll; height: 400px;">
                                                    <asp:CheckBox Text="TODOS" AutoPostBack="true" Style="margin-left: 50px;" TextAlign="Right" CssClass="checkbox" ToolTip="seleccionar todos" ID="chklZRSeleccionaTodos" OnCheckedChanged="chklZTSeleccionaTodos_CheckedChanged" runat="server" />
                                                    <asp:CheckBoxList ID="chklZR" AutoPostBack="false" Style="margin-left: 50px;" RepeatColumns="1" RepeatDirection="Vertical" RepeatLayout="Table" CssClass="checkbox" runat="server">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <%--Panel de tarifario--%>
                                <asp:Panel ID="PanelTarifario" Height="350" runat="server">

                                    <asp:Label Text="Copiar" runat="server" />
                                    <asp:LinkButton CssClass="btn btn-sm btn-default" ID="BtnCopiarTarifarioArriba" OnClick="BtnCopiarTarifarioArriba_Click" runat="server">
                                        <span class="glyphicon glyphicon-arrow-up"></span>
                                        Arriba
                                    </asp:LinkButton><asp:LinkButton CssClass="btn btn-sm btn-default" ID="BtnCopiarTarifarioAbajo" OnClick="BtnCopiarTarifarioAbajo_Click" runat="server">
                                        <span class="glyphicon glyphicon-arrow-down"></span>
                                        Abajo
                                    </asp:LinkButton><asp:LinkButton CssClass="btn btn-sm btn-default" ID="BtnCopiarDerecha" OnClick="BtnCopiarDerecha_Click" runat="server">
                                        <span class="glyphicon glyphicon-arrow-right"></span>
                                        Derecha
                                    </asp:LinkButton><asp:LinkButton CssClass="btn btn-sm btn-default" ID="BtnCopiarIzquierda" OnClick="BtnCopiarIzquierda_Click" runat="server">
                                        <span class="glyphicon glyphicon-arrow-left"></span>
                                        Izquierda
                                    </asp:LinkButton><asp:LinkButton CssClass="btn btn-sm btn-default" ID="BtnCopiarTodaLaTabla" OnClick="BtnCopiarTodaLaTabla_Click" runat="server">
                                        <span class=".glyphicon-glyphicon-refresh"></span>
                                        Toda la tabla
                                    </asp:LinkButton><div class="pull-right">
                                        <asp:Label Text="Informacion de la celda" runat="server" />
                                        <label>Fila:</label>
                                        <asp:Label ID="lblFila" runat="server" />
                                        <label>Celda:</label>
                                        <asp:Label ID="lblCelda" runat="server" />
                                        <label>Precio:</label>
                                        <asp:Label ID="lblPrecio" runat="server" />
                                    </div>

                                    <div style="overflow: auto; white-space: nowrap; height: 300px;">
                                        <asp:GridView runat="server"
                                            EnableViewState="true"
                                            Height="300"
                                            ViewStateMode="Enabled"
                                            AllowSorting="true" ID="DGVTarifario"
                                            Style="margin-top: 5px;"
                                            CssClass="table table-bordered table-hover table-condensed table-striped input-sm  "
                                            AutoGenerateColumns="true">
                                            <EmptyDataTemplate>
                                                <div class="info">No hay ciudades en el tarifario </div>
                                            </EmptyDataTemplate>
                                            <SelectedRowStyle CssClass="table table-hover input-sm success" />
                                            <SortedAscendingHeaderStyle CssClass="glyphicon glyphicon-sort-by-alphabet" />
                                            <Columns>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <script>
                                        function MandaInformacionACampos(Identificador) {
                                            var txt = document.getElementById(Identificador);
                                        }
                                    </script>
                                    <%--<style type="text/css">
                                        .Pintatabla tr td:first-child {
                                            position: fixed;
                                            z-index: 1;
                                            overflow-x: hidden;
                                            overflow-y: hidden;
                                            width: 200px;
                                            height: 100px;
                                        }

                                        .Pintatabla th:first-child {
                                            position: fixed;
                                            z-index: 1;
                                            overflow-x: hidden;
                                            overflow-y: hidden;
                                            width: 200px;
                                            height: 100px;
                                        }
                                    </style>--%>
                                    <%--                                    <div id="DivRoot" style="align-content: flex-end">
                                        <div style="overflow: hidden;" id="DivHeaderRow">
                                        </div>

                                        <div style="overflow: scroll; height: 300px" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                        </div>

                                        <div id="DivFooterRow" style="overflow: hidden">
                                        </div>
                                    </div>
                                    <script type="text/javascript">

                                        $(document).ready(function () {
                                            $('#DGVTarifario').DataTable({
                                                "scrollY": "200px",
                                                "scrollCollapse": true,
                                                "paging": true
                                            });
                                        });
                                        //function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
                                        //    var tbl = document.getElementById(gridId);


                                        //    if (tbl) {
                                        //        var DivHR = document.getElementById('DivHeaderRow');
                                        //        var DivMC = document.getElementById('DivMainContent');
                                        //        var DivFR = document.getElementById('DivFooterRow');

                                        //        //*** Set divheaderRow Properties ****
                                        //        DivHR.style.height = headerHeight + 'px';
                                        //        DivHR.style.width = (parseInt(width) - 16) + 'px';
                                        //        DivHR.style.position = 'relative';
                                        //        DivHR.style.top = '0px';
                                        //        DivHR.style.zIndex = '10';
                                        //        DivHR.style.verticalAlign = 'top';

                                        //        //*** Set divMainContent Properties ****
                                        //        DivMC.style.width = width + 'px';
                                        //        DivMC.style.height = height + 'px';
                                        //        DivMC.style.position = 'relative';
                                        //        DivMC.style.top = -headerHeight + 'px';
                                        //        DivMC.style.zIndex = '1';

                                        //        //*** Set divFooterRow Properties ****
                                        //        DivFR.style.width = (parseInt(width) - 16) + 'px';
                                        //        DivFR.style.position = 'relative';
                                        //        DivFR.style.top = -headerHeight + 'px';
                                        //        DivFR.style.verticalAlign = 'top';
                                        //        DivFR.style.paddingtop = '2px';

                                        //        if (isFooter) {
                                        //            var tblfr = tbl.cloneNode(true);
                                        //            tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                                        //            var tblBody = document.createElement('tbody');
                                        //            tblfr.style.width = '100%';
                                        //            tblfr.cellSpacing = "0";
                                        //            tblfr.border = "0px";
                                        //            tblfr.rules = "none";
                                        //            //*****In the case of Footer Row *******
                                        //            tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                                        //            tblfr.appendChild(tblBody);
                                        //            DivFR.appendChild(tblfr);
                                        //        }
                                        //        //****Copy Header in divHeaderRow****
                                        //        DivHR.appendChild(tbl.cloneNode(true));
                                        //    }
                                        //}

                                        //function OnScrollDiv(Scrollablediv) {
                                        //    document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
                                        //    document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
                                        //}

                                    </script>--%>
                                </asp:Panel>
                            </asp:Panel>

                            <%-- Panel de contrato --%>
                            <asp:Panel ID="PanelDeContrato" runat="server">
                                <%--Panel de informacion de suscursal--%>
                                <asp:Panel CssClass="well well-lg col-md-8 col-sm-8 col-xs-8" ID="PanelDeInformacion" Style="margin-bottom: 30px; -webkit-box-shadow: 10px 10px 61px 5px rgba(0,0,0,0.70); -moz-box-shadow: 10px 10px 61px 5px rgba(0,0,0,0.75); box-shadow: 10px 10px 61px 5px rgba(0,0,0,0.75); z-index: 1; position: absolute;" runat="server">
                                    <asp:Label CssClass="hidden" ID="lblIndexContrato" runat="server" />
                                    <asp:Panel CssClass=" alert alert-danger " Style="padding: 10px;" ID="PanelMensajeContrato" runat="server">

                                        <span class="glyphicon glyphicon-info-sign"></span>
                                        <asp:Label ID="lblMensajeContrato" Text="Mensaje del sistema" Font-Size="Large" runat="server" />

                                        <asp:LinkButton ID="btnCancelarCambiosSustituirColonia" OnClick="btnCancelarCambiosSustituirColonia_Click" CssClass="btn btn-sm btn-danger" ForeColor="White" runat="server">
                            <span class="glyphicon glyphicon-remove"></span>
                                                
                                        </asp:LinkButton>
                                    </asp:Panel>
                                    <div class="pull-right">
                                        <asp:LinkButton ID="btnEditarContrato" OnClick="btnEditarContrato_Click" CssClass="btn btn-sm btn-default" runat="server">
                                            <span class="glyphicon glyphicon-cog"></span>
                                            Editar contrato
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnAceptarEdicionContrato" OnClick="btnAceptarEdicionContrato_Click" CssClass="btn btn-sm btn-success" runat="server">
                                            <span class="glyphicon glyphicon-ok"></span>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="btnCerrarPanelInformacion" OnClick="btnCerrarPanelInformacion_Click" CssClass="btn btn-sm btn-danger">
                                <span class="glyphicon glyphicon-remove"></span>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="form-inline">
                                        <div class="form-group">
                                            <label>Sucursal</label>
                                            <asp:Label ID="lblInformacionNombreSucursal" runat="server" />
                                        </div>
                                        <div class="form-group">
                                            <label>Direccion</label>
                                            <asp:Label ID="lblInformacionDireccionEmpresaContrato" runat="server" />
                                        </div>
                                    </div>
                                    <label>Correo Electronico</label>
                                    <asp:HyperLink ID="HlnkCorreoElectronico" NavigateUrl="navigateurl" runat="server" />

                                    <div class="clearfix"></div>
                                    <ul class="nav nav-tabs" style="margin-bottom: 5px;">
                                        <li role="presentation" runat="server" id="liInformacionTelefono">
                                            <asp:LinkButton ID="btnInformacionTelefono" OnClick="btnInformacionTelefono_Click" runat="server">
                                    <span class="glyphicon glyphicon-phone"></span>
                                    Contacto
                                            </asp:LinkButton></li>
                                        <li role="presentation" id="liInformacionTarifario" runat="server">
                                            <asp:LinkButton ID="btnInformacionTarifario" OnClick="btnInformacionTarifario_Click" runat="server">
                                    <span ></span>
                                    Zonas de entrega
                                            </asp:LinkButton></li>
                                    </ul>
                                    <asp:Panel ID="panelInformacionContacto" OnInit="panelInformacionContacto_Init" runat="server">
                                        <asp:GridView runat="server" ID="DGVInformacionTelefonica" OnRowDataBound="DGVInformacionTelefonica_RowDataBound" Style="margin-top: 10px;" AutoGenerateColumns="false" DataKeyNames="ID" CssClass="table table-bordered table-hover table-condensed table-striped input-sm">
                                            <EmptyDataTemplate>
                                                <div class="info">
                                                    No existen telefonos guardados
                                                </div>
                                            </EmptyDataTemplate>
                                            <SelectedRowStyle CssClass="table table-hover input-sm success" />
                                            <SortedAscendingHeaderStyle CssClass="glyphicon glyphicon-sort-by-alphabet" />
                                            <Columns>
                                                <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" />
                                                <asp:BoundField DataField="Tipo" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" />
                                                <asp:TemplateField HeaderText="Tipo de telefono">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtTipoDeTelefono" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Telefono">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HlnkTelefono" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NUMERO" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" HeaderText="Numero" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>

                                    <asp:Panel ID="panelInformacionTarifario" runat="server">

                                        <asp:Panel ID="PanelTarifarioSuministradora" runat="server">

                                            <div class="form-inline">
                                                <div class="form-group">
                                                    <label>Colonia</label>
                                                    <asp:TextBox ID="txtPITxtColonia" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="form-group">
                                                    <label>Registros</label>
                                                    <asp:DropDownList CssClass="form-control" AutoPostBack="true" ID="DDLPIColonias" OnSelectedIndexChanged="DDLPIColonias_SelectedIndexChanged" runat="server">
                                                        <asp:ListItem Value="Todos" Text="Todos" />
                                                        <asp:ListItem Value="Seleccionado" Text="Seleccionados" />
                                                        <asp:ListItem Value="Deseleccionados" Text="Deseleccionados" />
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <asp:LinkButton CssClass="btn btn-default btn-sm" ID="BTNPIBuscarColonia" OnClick="BTNPIBuscarColonia_Click" runat="server">
                                                <span class="glyphicon glyphicon-search"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <asp:GridView runat="server" ID="DgvInformacionTarifario" Style="margin-top: 10px;" OnRowDataBound="DgvInformacionTarifario_RowDataBound" AutoGenerateColumns="false" DataKeyNames="UidTarifario" CssClass="table table-bordered table-hover table-condensed table-striped input-sm">
                                                <EmptyDataTemplate>
                                                    <div class="info">
                                                        No hay zonas de entrega disponibles para mostrar
                                                    </div>
                                                </EmptyDataTemplate>
                                                <SelectedRowStyle CssClass="table table-hover input-sm success" />
                                                <SortedAscendingHeaderStyle CssClass="glyphicon glyphicon-sort-by-alphabet" />
                                                <Columns>
                                                    <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" />
                                                    <asp:TemplateField ItemStyle-Width="10">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkbTarifario" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="DPrecio" ItemStyle-Width="10" HeaderText="Precio" />
                                                    <asp:BoundField DataField="StrNombreColoniaZE" HeaderText="Colonia a entregar" />

                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>

                                        <asp:Panel ID="PanelTarifarioDistribuidora" runat="server">

                                            <asp:GridView runat="server" ID="DGVInformacionTarifarioDistribuidora" Style="margin-top: 10px;" AutoGenerateColumns="false" DataKeyNames="UidTarifario" CssClass="table table-bordered table-hover table-condensed table-striped input-sm">
                                                <EmptyDataTemplate>
                                                    <div class="info">
                                                        No hay zonas de entrega disponibles para mostrar
                                                    </div>
                                                </EmptyDataTemplate>
                                                <SelectedRowStyle CssClass="table table-hover input-sm success" />
                                                <SortedAscendingHeaderStyle CssClass="glyphicon glyphicon-sort-by-alphabet" />
                                                <Columns>
                                                    <asp:BoundField DataField="StrNombreColoniaZE" HeaderText="Colonia a entregar" />
                                                    <asp:BoundField DataField="DPrecio" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" HeaderText="Precio" />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>

                                    </asp:Panel>

                                </asp:Panel>

                                <div style="margin-top: 15px">
                                    <div style="margin-top: 5px;">
                                        <div class="pull-left">Seleccione la casilla para vuncular a una sucursal</div>
                                        <div class="pull-right">
                                            <asp:LinkButton CssClass="btn btn-sm btn-default" ID="BtnLimpiarFiltrosContrato" OnClick="BtnLimpiarFiltrosContrato_Click" runat="server">
                                                <span class="glyphicon glyphicon-trash"></span>
                                                Limpiar
                                            </asp:LinkButton><asp:LinkButton runat="server" ID="btnBuscarEmpresa" OnClick="btnBuscarEmpresa_Click" CssClass="btn btn-sm btn-default"><span class="glyphicon glyphicon-search"></span> Buscar</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <asp:TextBox runat="server" CssClass="hidden" ID="TextBox1" />
                                        <div class="col-md-2">
                                            <label>Identificador</label>
                                            <asp:TextBox runat="server" ID="txtBIdentificador" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label>Hora de Apertura</label>
                                            <asp:TextBox runat="server" ID="txtBHoraApertura" TextMode="Time" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label>Hora de Cierre</label>
                                            <asp:TextBox runat="server" ID="txtBHoraDeCierre" TextMode="Time" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label>Estatus</label>
                                            <asp:DropDownList ID="ddlCEstatus" AutoPostBack="true" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <label>Codigo</label>
                                            <asp:TextBox runat="server" ID="txtBCodigo" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <%--DataGridView Contrato--%><div class="table table-responsive">
                                        <asp:GridView runat="server" ID="dgvBusquedaDeEmpresa" OnRowCommand="dgvBusquedaDeEmpresa_RowCommand" Style="margin-top: 10px;" OnRowDataBound="dgvBusquedaDeEmpresa_RowDataBound" AutoGenerateColumns="false" DataKeyNames="ID" CssClass="table table-bordered table-hover table-condensed table-striped input-sm">
                                            <EmptyDataTemplate>No hay coincidencia de busqueda</EmptyDataTemplate>
                                            <Columns>
                                                <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide">
                                                    <FooterStyle CssClass="hide" />
                                                    <HeaderStyle CssClass="hide" />
                                                    <ItemStyle CssClass="hide" />
                                                </asp:ButtonField>
                                                <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" HeaderText="Estatus">
                                                    <ItemTemplate>
                                                        <asp:Image ID="imgEstatus" Width="20" Height="20" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" HeaderText="Acciones">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEstatusContrato" CommandName="Contrato" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server">
                                                            <asp:Image ID="lblIconoEstatusContrato" Width="20" Height="20" runat="server" />
                                                        </asp:LinkButton><asp:LinkButton CommandName="Aceptar" ID="btnAceptar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn btn-sm btn-success" runat="server">
                                                                <span class="glyphicon glyphicon-ok"></span>
                                                        </asp:LinkButton><asp:LinkButton CommandName="Cancelar" ID="btnCancelar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn btn-sm btn-danger" runat="server">
                                                                <span class="glyphicon glyphicon-remove"></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" HeaderText="INFORMACION">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnInfoContacto" CommandName="Informacion" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn btn-sm btn-info" runat="server">
                                                        <span class="glyphicon glyphicon-info-sign"></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="IDENTIFICADOR" HeaderText="Sucursal" />
                                                <asp:BoundField DataField="HORAAPARTURA" HeaderText="Hora de apaertural" />
                                                <asp:BoundField DataField="HORACIERRE" HeaderText="Hora de cierre" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>



                            </asp:Panel>
                            <%--Panel de servicio a clientes--%>
                            <asp:Panel ID="PanelAtencionAClientes" runat="server">
                                <div class="col-md-12">

                                    <h4>Mensaje</h4>
                                    <asp:TextBox TextMode="MultiLine" Rows="5" CssClass="form-control" ID="txtMensaje" runat="server" />
                                    <div class="pull-right" style="margin-top: 10px">
                                        <asp:LinkButton CssClass="btn btn-sm btn-success" ID="btnAgregarMensaje" OnClick="btnAgregarMensaje_Click" ToolTip="Agregar" runat="server">
                                                <span class="glyphicon glyphicon-plus"></span>
                                            <i>Agregar</i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <asp:GridView runat="server" OnRowCommand="DgvMensajes_RowCommand" DataKeyNames="Uid" AutoGenerateColumns="false" Style="margin-top: 10px" ID="DgvMensajes" CssClass="table table-bordered table-hover table-condensed table-striped input-sm">
                                        <EmptyDataTemplate>
                                            Sin mensajes para clientes
                                        </EmptyDataTemplate>
                                        <EmptyDataRowStyle CssClass="text-center" />
                                        <SelectedRowStyle CssClass="table table-hover input-sm success" />
                                        <SortedAscendingHeaderStyle CssClass="glyphicon glyphicon-sort-by-alphabet" />
                                        <Columns>
                                            <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" />
                                            <asp:BoundField DataField="StrMensaje" HeaderText="Mensaje" />


                                            <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" HeaderText="Acciones">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnInfoContacto" CommandName="Eliminar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ToolTip="Eliminar" CssClass="btn btn-sm btn-default" runat="server">
                                                        <span class="glyphicon glyphicon-trash"></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="chklColonias" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
