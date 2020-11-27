<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/MasterDeliverix.Master" AutoEventWireup="true" CodeBehind="Tarifario.aspx.cs" Inherits="WebApplication1.Vista.Tarifario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Tarifario
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="favicon" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
    <div class="col-md-4">
        <div class="panel panel-primary">
            <%--Panel de busqueda--%>
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
                    <asp:GridView runat="server" ID="DgvSucursales" PageSize="10" OnRowDataBound="DgvSucursales_RowDataBound" Style="margin-top: 5px;" CssClass="table table-bordered table-hover table-condensed table-striped input-sm" AutoGenerateColumns="false" DataKeyNames="ID" OnSelectedIndexChanged="DgvSucursales_SelectedIndexChanged" AllowPaging="True" OnPageIndexChanging="DgvSucursales_PageIndexChanging" PagerSettings-Mode="NextPreviousFirstLast">
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
    </div>
    <div class="col-md-8">
        <%--panel de tarifario--%>
        <div class="panel panel-primary">

            <div class="panel-heading text-center">
                <label>Tarifario</label>
            </div>
            <div class="clearfix"></div>
            <div class=" pull-left">
                <asp:TextBox ID="txtUidSucursal" CssClass="hide" runat="server" />
                <asp:Label Visible="false" ID="lblUidSucursal" runat="server" />
                <asp:LinkButton runat="server" ID="btnEditar" OnClick="btnEditar_Click" CssClass="btn btn-sm btn-default disabled"><span class="glyphicon glyphicon-cog"></span> Editar</asp:LinkButton>
                <asp:LinkButton runat="server" ID="btnGuardar" OnClick="btnGuardar_Click" CssClass="btn btn-sm btn-success ">
                    <asp:Label runat="server" ID="lblGuardarDatos" CssClass="glyphicon glyphicon-ok"></asp:Label>
                </asp:LinkButton>
                <asp:LinkButton runat="server" ID="btnCancelar" OnClick="btnCancelar_Click" CssClass="btn btn-sm btn-danger ">
                     <asp:label CssClass=" glyphicon glyphicon-remove" runat="server" />
                </asp:LinkButton>
            </div>
            <div class="clearfix"></div>
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
            <div class="panel-body" style="height: 500px;">
                <asp:Panel ID="PanelZonaDeServicio" runat="server">
                    <%-- Panel de zona de servicio --%>
                    <asp:Panel ID="PanelZonasServicio" runat="server">
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
                            <%-- Contenido del panel --%>
                            <div class="col-md-12">
                                <%-- Gridview de las ciudades seleccionadas --%>
                                <div class="table table-responsive" style="margin: 0 5px 0 0;">
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
                                <%--Filtros de busqueda--%>
                                <div class="col-md-12" style="margin-top: 20px;">

                                    <%-- Filtros de busqueda y control de la lista de colonias --%>
                                    <div class="form-inline">
                                        <%-- Filtros de busqueda --%>
                                        <div class="form-group">
                                            <label>Mostrar</label>
                                            <asp:DropDownList ID="DDLTipoDeColonias" AutoPostBack="true" OnSelectedIndexChanged="DDLTipoDeColonias_SelectedIndexChanged" CssClass="form-control" runat="server">
                                                <asp:ListItem Text="Todos" />
                                                <asp:ListItem Text="Seleccionados" />
                                                <asp:ListItem Text="Deseleccionados" />
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtBusquedaColonia" CssClass="form-control" runat="server" />
                                                <asp:LinkButton ID="btnBusquedaColonia" OnClick="btnBusquedaColonia_Click" CssClass=" input-group-addon" ToolTip="Buscar" runat="server">
                                                        <span class="glyphicon glyphicon-search">
                                                        </span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:CheckBox Text="TODOS" AutoPostBack="true" Style="margin-left: 50px;" TextAlign="Right" CssClass="checkbox" ToolTip="seleccionar todos" ID="chkSeleccionarTodos" OnCheckedChanged="chkSeleccionarTodos_CheckedChanged" runat="server" />
                                    <asp:CheckBoxList ID="chklColonias" AutoPostBack="false" Height="150" Style="margin-left: 50px;" RepeatColumns="1" RepeatDirection="Vertical" RepeatLayout="Table" CssClass="checkbox" runat="server">
                                    </asp:CheckBoxList>
                                </div>

                            </div>

                        </div>
                    </asp:Panel>
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

                    <div style="overflow: auto; white-space: nowrap; height: 400px;">
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
            </div>
        </div>
    </div>
</asp:Content>
