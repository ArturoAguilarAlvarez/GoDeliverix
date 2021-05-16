<%@ Page Title="" Language="C#" MasterPageFile="MasterDeliverix.Master" AutoEventWireup="true" CodeBehind="Empresas.aspx.cs" Inherits="WebApplication1.Vista.EmpSuministradora" %>

<%@ MasterType VirtualPath="~/Vista/MasterDeliverix.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    EMPRESA
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="favicon" runat="server">
    <link rel="icon" href="../FavIcon/empresaicon.png" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-md-12 text-center" runat="server" id="PanelCargando" visible="false" style="z-index: 1; margin-bottom: 10px;">
        <asp:Panel runat="server" Height="50" Width="50">
            <asp:Image ImageUrl="~/Vista/Img/Cargando.gif" Height="50" Width="50" runat="server" AlternateText="Cargando" />
        </asp:Panel>
    </div>

    <div class="col-md-12">
        <!--Panel de busqueda ampliada -->
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
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
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" ID="BtnBALimpiar" OnClick="BorrarFiltrosBusquedaAvanzada" CssClass="btn btn-sm btn-default"><span class="glyphicon glyphicon-trash"></span> Limpiar</asp:LinkButton>
                                <asp:LinkButton runat="server" ID="BtnBABuscar" OnClick="BuscaEmpresasBusquedaAmpliada" CssClass="btn btn-sm btn-default"><span class="glyphicon glyphicon-search"></span> Buscar</asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <!-- Panel de filtros de la busqueda ampliada -->
                            <asp:Panel ID="PanelFiltrosBusquedaAmpliada" runat="server">
                                <!-- Datos generales -->
                                <div class="row container-fluid">
                                    <div class="col-md-3">
                                        <h6>Razon social</h6>
                                        <asp:TextBox ID="txtBARazonSocial" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <h6>Nombre comercial</h6>
                                        <asp:TextBox ID="txtBANombreComercial" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <h6>RFC</h6>
                                        <asp:TextBox ID="txtBARF" runat="server" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <h6>Estatus</h6>
                                        <asp:DropDownList ID="DDLBAEstatus" AppendDataBoundItems="true" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="SELECCIONAR" Value="0" />
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <h6>Tipo</h6>
                                        <asp:DropDownList ID="DDLBATipo" runat="server" CssClass="form-control"></asp:DropDownList>
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
                                        <asp:TextBox ID="TextBox2" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <asp:Label ID="lblprueba" runat="server" />
                            </asp:Panel>

                            <asp:GridView runat="server" ID="DGVBUSQUEDAAMPLIADA" OnSorting="GridViewBusquedaAmplicada_Sorting" OnPreRender="GridViewPreRender" AllowSorting="true" Style="margin-top: 5px;" PageSize="10" CssClass="table table-bordered table-hover table-condensed table-striped input-sm" AutoGenerateColumns="false" DataKeyNames="UIDEMPRESA" OnRowDataBound="GVWEmpresaAmpliada_RowDataBound" OnSelectedIndexChanged="GVBusquedaAvanzadaEmpresa_SelectedIndexChanged" AllowPaging="True" OnPageIndexChanging="GridViewBusquedaAmpliada_PageIndexChanging">
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
                                    <asp:BoundField DataField="NOMBRECOMERCIAL" HeaderText="Nombre comercial" SortExpression="NOMBRECOMERCIAL" />
                                    <asp:BoundField DataField="RFC" HeaderText="Rfc" SortExpression="Rfc" />
                                    <asp:BoundField DataField="RAZONSOCIAL" HeaderText="Razon Social" SortExpression="RAZONSOCIAL" />
                                    <asp:TemplateField HeaderText="Estatus" SortExpression="STATUS">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEstatus" CssClass="tooltip" data-toggle="tooltip" data-placement="top" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="StrEstatus" HeaderText="Estatus" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" />
                                    <asp:TemplateField HeaderText="Tipo" SortExpression="Tipo">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTipo" CssClass="tooltip" data-toggle="tooltip" data-placement="top" runat="server" />

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="StrTipoDeEmpresa" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" HeaderText="Tipo" />

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
                                                    <asp:ListItem Text="10" Value="3" />
                                                    <asp:ListItem Text="20" Value="5" />
                                                    <asp:ListItem Text="30" Value="6" />
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
            </ContentTemplate>
        </asp:UpdatePanel>

        <!-- Panel izquierdo -->
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div runat="server" id="PanelIzquierdo" class="col-md-6">

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

                    <!-- Panel de filtros -->

                    <asp:Panel runat="server" ID="PanelDeBusqueda">
                        <div class="panel panel-primary">
                            <!--Header de panel-->
                            <div class="panel-heading text-center">
                                <h5>Busqueda de EMPRESAS</h5>
                            </div>
                            <!-- Botones de acciones del panel-->
                            <div class="row container-fluid" style="margin: 10px;">
                                <%--<div class="pull-left">
                                    <asp:LinkButton runat="server" ID="btnBusquedaAmpliada" OnClick="BusquedaAvanzada" CssClass="btn btn-sm btn-default"><span class="glyphicon glyphicon-zoom-in"></span> Busqueda ampliada</asp:LinkButton>
                                </div>--%>
                                <div class="pull-left">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="btnExportarTodasLasSucursales" runat="server" OnClick="btnExportarTodasLasSucursales_Click" CssClass="btn btn-sm btn-success " ToolTip="Descargar">
                                        <span class="glyphicon glyphicon-save">
                                        </span>
                                        Descargar todas las sucursales
                                            </asp:LinkButton>
                                            <script type="text/javascript">
                                                function Upload2(fileUpload) {
                                                    if (fileUpload.value != '') {
                                                        document.getElementById("<%=btnCargarTodasLAsSucursales.ClientID %>").click();
                                                    }
                                                }
                                            </script>
                                            <asp:LinkButton CssClass="btn btn-sm btn-warning " ID="BtnImportarTodasSucursales" runat="server">
                                                <span class="glyphicon glyphicon-open">
                                                </span>
                                                Importar todas las sucursales
                                            </asp:LinkButton>
                                            <asp:FileUpload ID="FUAllSucursales" CssClass="hide" runat="server" />
                                            <asp:Button Text="Subir" OnClick="MuestraExcelTodasLAsSucursales" CssClass="hide" ID="btnCargarTodasLAsSucursales" runat="server" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnCargarTodasLAsSucursales" />
                                            <asp:PostBackTrigger ControlID="btnExportarTodasLasSucursales" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="pull-right">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton runat="server" ID="btnMostrarFiltros" OnClick="MostrarYOcultarFiltrosBusquedaNormal" CssClass="btn btn-sm btn-default">
                                                <span class="glyphicon glyphicon-eye-open"></span>
                                                <asp:Label ID="lblVisibilidadfiltros" runat="server" />
                                            </asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="btnBorrarFiltros" OnClick="VaciarFiltros" CssClass="btn btn-sm btn-default disabled"><span class="glyphicon glyphicon-trash"></span> Limpiar</asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="btnBuscar" OnClick="BuscarEmpresasBusquedaNormal" CssClass="btn btn-sm btn-default disabled"><span class="glyphicon glyphicon-search"></span> Buscar</asp:LinkButton>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnMostrarFiltros" />
                                            <asp:PostBackTrigger ControlID="btnBorrarFiltros" />
                                            <asp:PostBackTrigger ControlID="btnBuscar" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                            <!-- Contenido del panel-->
                            <div class="panel-body">
                                <asp:Panel runat="server" ID="PnlFiltros">
                                    <div class="clearfix"></div>
                                    <div class="row ">
                                        <div class="col-md-6">
                                            <h6>Razon social</h6>
                                            <asp:TextBox runat="server" ID="txtFRazonSocial" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <h6>Nombre comercial</h6>
                                            <asp:TextBox runat="server" ID="txtFNombreComercial" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <h6>RFC</h6>
                                            <asp:TextBox runat="server" ID="txtFRfc" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>

                                        </div>
                                        <div class="col-md-4">
                                            <h6>Estatus</h6>
                                            <asp:DropDownList runat="server" EnableViewState="true" ID="DDLFEstatus" AppendDataBoundItems="true" CssClass="form-control">
                                                <asp:ListItem Text="SELECCIONAR" Value="0" />
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <h6>Tipo</h6>
                                            <asp:DropDownList runat="server" EnableViewState="true" ID="DDLFTipoDeEmpresa" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <h6>Fecha de inicio</h6>
                                            <asp:TextBox ID="txtDFechaDeNacimiento" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <h6>Fecha de fin</h6>
                                            <asp:TextBox ID="TextBox1" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                </asp:Panel>

                                <!-- GridView de empresas simple-->
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div style="overflow-x: auto;" class="table-responsibe">
                                            <asp:GridView runat="server" ID="DGVEMPRESAS" OnPreRender="GridViewPreRender" PageSize="10" AllowSorting="true" Style="margin-top: 5px;" OnSorting="DGVEMPRESASNORMAL_Sorting" CssClass="table table-bordered table-hover table-condensed table-striped input-sm" AutoGenerateColumns="false" DataKeyNames="UIDEMPRESA" OnRowDataBound="GVWEmpresaNormal_RowDataBound" OnSelectedIndexChanged="GVWEmpresaBusquedaNormal_SelectedIndexChanged" AllowPaging="True" OnPageIndexChanging="GridViewBusquedaNormal_PageIndexChanging" PagerSettings-Mode="NextPreviousFirstLast">
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
                                                    <asp:BoundField DataField="NOMBRECOMERCIAL" HeaderText="Nombre comercial" SortExpression="NOMBRECOMERCIAL" />
                                                    <asp:BoundField DataField="RFC" HeaderText="Rfc" SortExpression="Rfc" />
                                                    <asp:TemplateField HeaderText="Estatus" SortExpression="STATUS">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEstatus" CssClass="tooltip " data-toggle="tooltip" data-placement="top" data-html="true" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="StrEstatus" HeaderText="Estatus" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" />
                                                    <asp:TemplateField HeaderText="Tipo" SortExpression="Tipo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTipo" Enabled="false" CssClass="tooltip" data-placement="top" data-html="true" data-toggle="tooltip" runat="server">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="StrTipoDeEmpresa" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" />
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
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="DGVEMPRESAS" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <!-- Panel Derecho -->
        <div runat="server" id="PanelDerecho" class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading text-center">
                    <h5>Datos de EMPRESA</h5>
                </div>
                <div class="clearfix"></div>
                <div class="clearfix">
                    <%--importacion y exportacion de Menu--%>
                    <div class="col-md-12" style="margin-top: 10px;">
                        <div class="pull-right">

                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="BtnExportarMenu" runat="server" OnClick="BtnExportarMenu_Click" CssClass="btn btn-sm btn-success " ToolTip="Descargar">
                                        <span class="glyphicon glyphicon-save">
                                        </span>
                                        Descargar sucursales
                                    </asp:LinkButton>
                                    <script type="text/javascript">
                                        function Upload(fileUpload) {
                                            if (fileUpload.value != '') {
                                                document.getElementById("<%=btnCargarExcel.ClientID %>").click();
                                            }
                                        }
                                    </script>
                                    <asp:LinkButton CssClass="btn btn-sm btn-warning " ID="BtnImportarMenu" runat="server">
                                                <span class="glyphicon glyphicon-open">
                                                </span>
                                                Importar sucursales
                                    </asp:LinkButton>
                                    <asp:FileUpload ID="FUImportExcel" CssClass="hide" runat="server" />
                                    <asp:Button Text="Subir" OnClick="MuestraExcel" CssClass="hide" ID="btnCargarExcel" runat="server" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="BtnExportarMenu" />
                                    <asp:PostBackTrigger ControlID="btnCargarExcel" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>

                                <asp:LinkButton runat="server" ID="btnExportarProductos" OnClick="btnExportarPoroductos_Click" CssClass="btn btn-sm btn-success">
                            <span class="glyphicon glyphicon-save"/> Descargar productos</asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnImportarProductos" OnClick="btnImportarProductos_Click" CssClass="btn btn-sm btn-warning">
                            <span class="glyphicon glyphicon-open"/> Importar productos</asp:LinkButton>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnExportarProductos" />
                                <asp:PostBackTrigger ControlID="btnImportarProductos" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <%--importacion y exportacion de Menu--%>

                        <%-- Update Panel Controlador de las acciones de usuario --%>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class=" pull-left" style="margin: 10px;">
                                    <asp:LinkButton runat="server" ID="btnNuevo" OnClick="ActivarCajasDeTexto" CssClass="btn btn-sm btn-default "><span class="glyphicon glyphicon-file"></span> Nuevo</asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnEditar" OnClick="ActivarEdicion" CssClass="btn btn-sm btn-default disabled"><span class="glyphicon glyphicon-cog"></span> Editar</asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnGuardar" OnClick="GuardarDatos" CssClass="btn btn-sm btn-success ">
                                        <asp:Label runat="server" ID="lblGuardarDatos"></asp:Label>
                                    </asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnCancelar" OnClick="CancelarAgregacion" CssClass="btn btn-sm btn-danger "><asp:label CssClass=" glyphicon glyphicon-remove" runat="server" /></asp:LinkButton><asp:Label runat="server" ID="lblEstado"></asp:Label>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnNuevo" />
                                <asp:PostBackTrigger ControlID="btnEditar" />
                                <asp:PostBackTrigger ControlID="btnGuardar" />
                                <asp:PostBackTrigger ControlID="btnCancelar" />
                            </Triggers>
                        </asp:UpdatePanel>
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
                    <div class="panel-body">
                        <asp:Panel runat="server" ID="pnlProductos">
                            <div class="pull-left">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton runat="server" ID="btnCargarProductos" OnClick="btnCargarProductos_Click" CssClass="btn btn-sm btn-success">
                            <span class="glyphicon glyphicon-ok"></span>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="BtnCancelarImportacion" OnClick="BtnCancelarImportacion_Click" CssClass="btn btn-sm btn-danger">
                            <span class="glyphicon glyphicon-remove"></span>
                                        </asp:LinkButton>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnCargarProductos" />
                                        <asp:PostBackTrigger ControlID="BtnCancelarImportacion" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-12">
                                <h6>Selecciona imagenes</h6>
                                <asp:FileUpload runat="server" ID="FUImportarImagenes" AllowMultiple="true" />
                            </div>
                            <div class="col-md-12">
                                <h6>Subir excel</h6>
                                <asp:FileUpload runat="server" ID="FUImportarProductos" />
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlAgregarOEditar">
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
                                        <li role="presentation" id="LiDatosComision" runat="server">
                                            <asp:LinkButton runat="server" ID="btnDatosComision" OnClick="PanelCoMision"><span class="glyphicon glyphicon-phone"></span> COMISIONES</asp:LinkButton></li>
                                    </ul>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnDatosGenerales" />
                                    <asp:PostBackTrigger ControlID="btnDatosDireccion" />
                                    <asp:PostBackTrigger ControlID="btnDatosDeConectado" />
                                    <asp:PostBackTrigger ControlID="btnDatosComision" />
                                </Triggers>
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
                                                    <asp:LinkButton CssClass="btn btn-sm btn-default" ID="BtnCargarImagen" runat="server">
                                                <span class="glyphicon glyphicon-open">
                                                </span>
                                                Cargar Imagen
                                                    </asp:LinkButton>
                                                    <asp:FileUpload ID="FUImagen" CssClass="hide" runat="server" />
                                                    <asp:Button Text="Subir" OnClick="MuestraFoto" CssClass="hide" ID="btnSubirImagen" runat="server" />
                                                </div>

                                            </div>
                                            <div class="col-md-9">

                                                <div class="col-md-6">
                                                    <h6>Razon social*</h6>
                                                    <asp:TextBox ID="txtDRazonSocial" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-6">
                                                    <h6>RFC*</h6>
                                                    <asp:TextBox ID="txtDRfc" runat="server" AutoPostBack="true" CssClass="form-control" OnTextChanged="TxtDRfc_TextChanged" Style="text-transform: uppercase"></asp:TextBox>
                                                </div>

                                                <div class="col-md-6">
                                                    <h6>Nombre comercial*</h6>
                                                    <asp:TextBox ID="txtDNombreComercial" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-6">
                                                    <h6>Estatus</h6>
                                                    <asp:DropDownList ID="DDLDEstatus" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-12">
                                                    <h6>Correo electronico</h6>
                                                    <asp:TextBox ID="txtDCorreoElectronico" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                                                    <asp:Label CssClass="hidden" ID="txtUidCorreoElectronico" runat="server" />
                                                </div>
                                                <div class="col-md-6">
                                                    <h6>Tipo*</h6>
                                                    <asp:DropDownList ID="DDLDTipoDeEmpresa" OnSelectedIndexChanged="DDLDTipoDeEmpresa_SelectedIndexChanged" runat="server" CssClass="form-control"></asp:DropDownList>
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
                                    <asp:Panel ID="PnlComisiones" runat="server">
                                        <div class="row container-fluid">
                                            <div class="col-md-6">
                                                <h6>Incluir comisión de pago con tarjeta </h6>
                                                <asp:CheckBox ID="chkbxComisionTarjeta" runat="server" />
                                            </div>
                                            <div class="col-md-6" id="panelComisionGoDeliverix" runat="server">
                                                <h6>Incluir comisión de GoDeliverix </h6>
                                                <asp:CheckBox ID="chkbxComision" runat="server" />
                                            </div>
                                            <div class="col-md-6">
                                                <h6>Comisión por productos</h6>
                                                <asp:TextBox ID="txtComisionProductos" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-md-6">
                                                <h6>Comisión por envio</h6>
                                                <asp:TextBox ID="txtComisionEnvio" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSubirImagen" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
            </div>

        </div>
</asp:Content>
