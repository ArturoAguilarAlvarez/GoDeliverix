<%@ Page Title="" Language="C#" MasterPageFile="MasterDeliverix.Master" AutoEventWireup="true" CodeBehind="AdministradoresDeEmpresas.aspx.cs" Inherits="WebApplication1.Vista.AdministradoresDeEmpresas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    ADMINISTRADORES
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="favicon" runat="server">
    <link rel="icon" href="FavIcon/Usuarios.png" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-md-12">

        <!--Panel de busqueda ampliada -->
        <asp:Panel runat="server" ID="PanelBusquedaAmpliada">
            <div class="panel panel-primary">
                <div class="panel-heading text-center">Busqueda ampliada de ADMINISTRADORES</div>
                <div class="row container-fluid">
                    <div class="pull-left">
                        <asp:LinkButton runat="server" ID="btnBusquedaNormal" OnClick="BusquedaNormal" CssClass="btn btn-sm btn-default"><span class="glyphicon glyphicon-zoom-out"></span>Busqueda normal</asp:LinkButton>
                    </div>
                    <div class="pull-right">
                        <asp:LinkButton runat="server" ID="BtnBAOcultar" OnClick="MostrarYOcultarFiltrosBusquedaAvanzada" CssClass="btn btn-sm btn-default">
                            <span class="glyphicon glyphicon-eye-open"></span>
                            <asp:Label ID="lblBAFiltrosVisibilidad" runat="server" />
                        </asp:LinkButton>
                        <asp:LinkButton runat="server" ID="BtnBALimpiar" OnClick="BorrarFiltrosBusquedaAvanzada" CssClass="btn btn-sm btn-default"><span class="glyphicon glyphicon-trash"></span> Limpiar</asp:LinkButton>
                        <asp:LinkButton runat="server" ID="BtnBABuscar" OnClick="BuscarEmpresasBusquedaAmpliada" CssClass="btn btn-sm btn-default"><span class="glyphicon glyphicon-search"></span> Buscar</asp:LinkButton>
                    </div>
                </div>
                <div class="panel-body">


                    <!-- Panel de filtros de la busqueda ampliada -->
                    <asp:Panel ID="PanelFiltrosBusquedaAmpliada" runat="server">
                        <!-- Datos generales -->
                        <div class="row container-fluid">
                            <div class="col-md-3">
                                <h6>Nombre</h6>
                                <asp:TextBox ID="txtBANombre" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <h6>Apellido</h6>
                                <asp:TextBox ID="txtBaApellido" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>


                            <div class="col-md-3">
                                <h6>Estatus</h6>
                                <asp:DropDownList ID="DDLBAEstatus" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                    <asp:ListItem Text="SELECCIONAR" Value="0" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <h6>Usuario</h6>
                                <asp:TextBox runat="server"  CssClass="form-control" ID="txtBAUSuario" />
                            </div>
                            <div class="col-md-4">
                                <h6>Correo electronico</h6>
                                <asp:TextBox ID="txtBACorreoElectronico" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <h6>Pais</h6>
                                <asp:DropDownList ID="DDLDBAPAIS" runat="server" OnSelectedIndexChanged="ObtenerEstado" AutoPostBack="true" OnTextChanged="ObtenerEstado" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <h6>Estado</h6>
                                <asp:DropDownList ID="DDLDBAESTADO" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ObtenerMunicipio"></asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <h6>Municipio</h6>
                                <asp:DropDownList ID="DDLDBAMUNICIPIO" runat="server" OnSelectedIndexChanged="ObtenerCiudad" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-md-2">
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
                            <div class="col-md-3">
                                <h6>Fecha</h6>
                                <asp:TextBox ID="TextBox1" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:GridView runat="server" ID="DGVBUSQUEDAAMPLIADA" OnPreRender="DGVEMPRESAS_PreRender" AllowSorting="true" OnSorting="BusquedaAmpliada_Sorting" Style="margin-top: 5px;" PageSize="10" CssClass="table table-bordered table-hover table-condensed table-striped input-sm" AutoGenerateColumns="false" DataKeyNames="Uid" OnRowDataBound="GVWEmpresaAmpliada_RowDataBound" OnSelectedIndexChanged="GVBusquedaAvanzadaEmpresa_SelectedIndexChanged" AllowPaging="True" OnPageIndexChanging="GridViewBusquedaAmpliada_PageIndexChanging">
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
                            <asp:BoundField DataField="StrNombre" HeaderText="Nombre" SortExpression="NOMBRE" />
                            <asp:BoundField DataField="StrUsuario" HeaderText="Usuario" SortExpression="USUARIO" />
                            <asp:BoundField DataField="StrApellidoPaterno" HeaderText="Apellidos" SortExpression="APELLIDOPATERNO" />
                            <asp:BoundField DataField="StrNombreDeSucursal" HeaderText="Empresa" SortExpression="NOMBREDELAEMPRESA" />
                            <asp:TemplateField HeaderText="Estatus" SortExpression="NOMBREESTATUS">
                                <ItemTemplate>
                                    <asp:Label ID="lblEstatus" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="StrEstatus" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" HeaderText="ESTATUS" SortExpression="NOMBREESTATUS" />
                            <asp:TemplateField HeaderText="PERFIL" SortExpression="NOMBREDEPERFIL">
                                <ItemTemplate>
                                    <asp:Label ID="lblPerfil" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="StrPerfil" HeaderText="Perfil" SortExpression="NOMBREDEPERFIL" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" />
                        </Columns>
                        <PagerSettings Mode="NextPreviousFirstLast" Position="Top" />
                        <PagerStyle HorizontalAlign="Center" />
                        <PagerTemplate>

                            <table class="text-center">
                                <tr>
                                    <td>
                                        <asp:ImageButton ImageUrl="~/Vista/Img/FlechasDoblesIzquierda.png" ID="btnPrimero" runat="server" CommandName="Page" CommandArgument="First" />
                                        <asp:ImageButton ImageUrl="~/Vista/Img/FechaIzquierda.png" ID="btnAnterior" runat="server" CommandName="Page" CommandArgument="Prev" />
                                        Filas:
                                              
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
                                        <asp:ImageButton ImageUrl="~/Vista/Img/FechaDerecha.png" ID="btnSiguiente" runat="server" CommandName="Page" CommandArgument="Next" />
                                        <asp:ImageButton ImageUrl="~/Vista/Img/FlechasDoblesDerecha.png" ID="btnUltimo" runat="server" CommandName="Page" CommandArgument="Last" />

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTotalDeRegistros" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </PagerTemplate>
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>

        <!-- Panel izquierdo -->
        <div runat="server" id="PanelIzquierdo" class="col-md-6">

            <!-- Panel de direccion-->
            <asp:Panel runat="server" ID="PanelDatosDireccion">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <label>DIRECCION</label>
                    </div>

                    <div class="panel-body">
                        <div class="row text-right">
                            <asp:LinkButton runat="server" ID="btnGuardarDireccion" OnClick="AgregaDireccion" CssClass="btn btn-sm btn-success">
                                <asp:Label runat="server" ID="lblDatosDireccion"></asp:Label>
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="btnCancelarDireccion" OnClick="OcultarPanelDireccion" CssClass="btn btn-sm btn-danger"><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="btnCerrarVetanaDireccion" OnClick="CierraVentanaDireccion" CssClass="btn btn-sm btn-default"><span class="glyphicon glyphicon-remove"></span> Cerrar</asp:LinkButton>
                        </div>
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

            <!-- Panel de filtros -->

            <asp:Panel runat="server" ID="PanelDeBusqueda">
                <div class="panel panel-primary">
                    <!--Header de panel-->
                    <div class="panel-heading text-center">
                        <h5>Busqueda de ADMINISTRADORES</h5>
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
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="btnBorrarFiltros" OnClick="VaciarFiltros" CssClass="btn btn-sm btn-default disabled"><span class="glyphicon glyphicon-trash"></span> Limpiar</asp:LinkButton>
                            <asp:LinkButton runat="server" ID="btnBuscar" OnClick="BuscarEmpresasBusquedaNormal" CssClass="btn btn-sm btn-default disabled"><span class="glyphicon glyphicon-search"></span> Buscar</asp:LinkButton>
                        </div>
                    </div>
                    <!-- Contenido del panel-->
                    <div class="panel-body">
                        <asp:Panel runat="server" ID="PnlFiltros">
                            <div class="clearfix"></div>
                            <div class="row ">
                                <div class="col-md-6">
                                    <h6>Nombre</h6>
                                    <asp:TextBox runat="server" ID="txtFNombreDeUsuario" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <h6>Apellido</h6>
                                    <asp:TextBox runat="server" ID="txtFApellido" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <h6>Usuario</h6>
                                    <asp:TextBox runat="server" ID="txtFUsuario" CssClass="form-control" />
                                </div>
                                <div class="col-md-6">
                                    <h6>Estatus</h6>
                                    <asp:DropDownList runat="server" EnableViewState="true" ID="DDLFEstatus" AppendDataBoundItems="true" CssClass="form-control">
                                        <asp:ListItem Text="SELECCIONAR" Value="0" Selected="True" />
                                    </asp:DropDownList>
                                </div>
                                <%--<div class="col-md-6">
                                            <h6>Perfil</h6>
                                            <asp:DropDownList runat="server" EnableViewState="true" ID="DDLFPERFILDEUSUARIO" CssClass="form-control">
                                                <asp:ListItem Text="SELECCIONAR PERFIL" Value="00000000-0000-0000-0000-000000000000" Selected="True"/>
                                            </asp:DropDownList>
                                        </div>--%>
                            </div>

                        </asp:Panel>
                        <div class="table-responsive">
                            <!-- GridView de empresas simple-->
                            <asp:GridView runat="server" ID="DGVEMPRESAS" OnPreRender="DGVEMPRESAS_PreRender" PageSize="10" AllowSorting="true" Style="margin-top: 10px;" OnSorting="DGVEMPRESASNORMAL_Sorting" CssClass="table table-bordered table-hover table-condensed table-striped input-sm" AutoGenerateColumns="false" DataKeyNames="Uid" OnRowDataBound="GVWEmpresaNormal_RowDataBound" OnSelectedIndexChanged="GVWEmpresaBusquedaNormal_SelectedIndexChanged" AllowPaging="True" OnPageIndexChanging="GridViewBusquedaNormal_PageIndexChanging" PagerSettings-Mode="NextPreviousFirstLast">
                                <EmptyDataTemplate>
                                    <div class="info">NO HAY DATOS EXISTENTES</div>
                                </EmptyDataTemplate>


                                <SelectedRowStyle CssClass="table table-hover input-sm success" />
                                <SortedAscendingHeaderStyle CssClass="glyphicon glyphicon-sort-by-alphabet" />
                                <Columns>
                                    <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide">
                                        <FooterStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                        <ItemStyle CssClass="hide" />
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="StrUsuario" HeaderText="Usuario" SortExpression="USUARIO" />
                                    <asp:BoundField DataField="StrNombre" HeaderText="Nombre" SortExpression="NOMBRE" />
                                    <asp:BoundField DataField="StrApellidoPaterno" HeaderText="Apellidos" SortExpression="APELLIDOPATERNO" />
                                    <asp:BoundField DataField="StrNombreDeSucursal" HeaderText="Empresa" SortExpression="NOMBREDELAEMPRESA" />
                                    <asp:TemplateField HeaderText="Estatus" SortExpression="StrEstatus">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEstatus" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="StrEstatus" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" HeaderText="ESTATUS" SortExpression="StrEstatus" />
                                    <asp:TemplateField HeaderText="PERFIL" SortExpression="NOMBREDEPERFIL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPerfil" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="StrPerfil" HeaderText="Perfil" SortExpression="StrPerfil" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" />
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
                                            <asp:DropDownList ID="ddlPaginasBusquedaNormal" AutoPostBack="true" OnTextChanged="PaginaSeleccionadaBusquedaNormal" OnSelectedIndexChanged="PaginaSeleccionadaBusquedaNormal" runat="server">
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

        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div runat="server" id="PanelDerecho" class="col-md-6">
                    <div class="panel panel-primary">
                        <div class="panel-heading text-center">
                            <h5>Gestion de administrador</h5>
                        </div>
                        <div class="clearfix"></div>

                        <div class=" pull-left">
                            <asp:LinkButton runat="server" ID="btnNuevo" OnClick="ActivarCajasDeTexto" CssClass="btn btn-sm btn-default "><span class="glyphicon glyphicon-file"></span> Nuevo</asp:LinkButton>
                            <asp:LinkButton runat="server" ID="btnEditar" OnClick="ActivarEdicion" CssClass="btn btn-sm btn-default disabled"><span class="glyphicon glyphicon-cog"></span> Editar</asp:LinkButton>
                            <asp:LinkButton runat="server" ID="btnGuardar" OnClick="GuardarDatos" CssClass="btn btn-sm btn-success ">
                                <asp:Label runat="server" ID="lblGuardarDatos"></asp:Label>
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="btnCancelar" OnClick="CancelarAgregacion" CssClass="btn btn-sm btn-danger "><asp:label CssClass=" glyphicon glyphicon-remove" runat="server" /></asp:LinkButton><asp:Label runat="server" ID="lblEstado"></asp:Label>
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
                            <ul class="nav nav-tabs">
                                <li role="presentation" id="liDatosGenerales" runat="server">
                                    <asp:LinkButton runat="server" ID="btnDatosGenerales" OnClick="PanelGeneral"><span class="glyphicon glyphicon-globe"></span> GENERAL</asp:LinkButton></li>
                                <li role="presentation" id="liDatosDireccion" runat="server">
                                    <asp:LinkButton runat="server" ID="btnDatosDireccion" OnClick="PanelDireccion"><span class="glyphicon glyphicon-road"></span> DIRECCION</asp:LinkButton></li>
                                <li role="presentation" id="liDatosContacto" runat="server">
                                    <asp:LinkButton runat="server" ID="btnDatosDeConectado" OnClick="PanelContacto"><span class="glyphicon glyphicon-phone"></span> CONTACTO</asp:LinkButton></li>
                                <li role="presentation" id="liDatosDeEmpresa" runat="server">
                                    <asp:LinkButton runat="server" ID="btnDatosDeEmpresa" OnClick="PanelDeDatosDeEmpresa"><span class="glyphicon glyphicon-folder-close"></span> EMPRESA</asp:LinkButton></li>
                            </ul>
                            <asp:Panel runat="server" ID="pnlDatosGenerales">
                                <asp:Label ID="txtUidUsuario" CssClass="hide" runat="server" />
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
                                        <asp:TextBox ID="txtDUsuario" OnTextChanged="txtDUsuario_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <h6>Contraseña*</h6>
                                        <asp:TextBox ID="txtdContrasena" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <h6>Estatus</h6>
                                        <asp:DropDownList ID="DDLDEstatus" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                        <h6>Fecha de nacimiento</h6>
                                        <asp:TextBox ID="txtDFechaDeNacimiento" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox runat="server" ID="txtdUidCorreo" CssClass="hidden" />
                                        <h6>Correo electronico *</h6>
                                        <asp:TextBox ID="txtDCorreoElectronico" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                                        <asp:Label CssClass="hidden" ID="txtUidCorreoElectronico" runat="server" />
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnlDireccion">
                                <div class="pull-left" style="margin-top: 10px;">
                                    <asp:LinkButton runat="server" ID="btnNuevaDireccion" OnClick="NuevaDireccion" CssClass="btn btn-sm btn-default disabled"><span class="glyphicon glyphicon-file"></span>Nuevo</asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnEdiarDireccion" OnClick="ActivaEdicionDeDireccion" CssClass="btn btn-sm btn-default disabled"><span class="glyphicon glyphicon-cog"></span>Editar</asp:LinkButton>
                                </div>
                                <div class="clearfix"></div>
                                <div class="table-responsive">
                                    <div class="table-responsive">
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
                                                            <asp:Label ID="lblEliminarTelefono" CssClass="glyphicon glyphicon-trash" runat="server" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
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
                                        <asp:TextBox ID="txtDTelefono" runat="server" CssClass="form-control" TextMode="Phone"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="table-responsive">
                                    <div class=" container-fluid text-center">
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
                                                            <asp:Label ID="lblEliminarTelefono" CssClass="glyphicon glyphicon-trash" runat="server" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="panelDatosEmpresa">

                                <div class="panel panel-default" style="margin-top: 10px;">
                                    <div style="margin-top: 5px;">
                                        <div class="pull-left">ASOCIAR A EMPRESA*</div>
                                        <div class="pull-right">
                                            <asp:LinkButton runat="server" ID="btnCambiarEmpresa" OnClick="CambiarEmpresa" CssClass="btn btn-default btn-sm"><span class="glyphicon glyphicon-refresh"></span> Cambiar</asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="btnBuscarEmpresa" OnClick="BuscaEmpresa" CssClass="btn btn-sm btn-default"><span class="glyphicon glyphicon-search"></span> Buscar</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <asp:TextBox runat="server" CssClass="hidden" ID="txtIdEmpresa" />
                                            <div class="col-md-6">
                                                <label>Razon Social</label>
                                                <asp:TextBox runat="server" ID="txtDRazonSocial" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                                <label>Nombre Comercial</label>
                                                <asp:TextBox runat="server" ID="txtdNombreComercial" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <label>RFC</label>
                                                <asp:TextBox runat="server" ID="txtdRFC" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <asp:GridView runat="server" ID="DGVBUSQUEDADEEMPRESA" Style="margin-top: 10px;" OnRowDataBound="DGVBUSQUEDADEEMPRESA_DataBound" OnSelectedIndexChanged="DGVBUSQUEDADEEMPRESA_SelectedIndexChanged" AutoGenerateColumns="false" DataKeyNames="UIDEMPRESA" CssClass="table table-bordered table-hover table-condensed table-striped input-sm">
                                            <EmptyDataTemplate>No hay coincidencia de busqueda</EmptyDataTemplate>
                                            <Columns>
                                                <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide">
                                                    <FooterStyle CssClass="hide" />
                                                    <HeaderStyle CssClass="hide" />
                                                    <ItemStyle CssClass="hide" />
                                                </asp:ButtonField>
                                                <asp:BoundField DataField="RAZONSOCIAL" HeaderText="Razon Social" />
                                                <asp:BoundField DataField="NOMBRECOMERCIAL" HeaderText="Nombre Comercial" />
                                                <asp:BoundField DataField="RFC" HeaderText="RFC" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                </div>
                            </asp:Panel>

                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
