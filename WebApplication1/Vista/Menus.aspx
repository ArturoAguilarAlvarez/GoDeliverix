<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/MasterDeliverix.Master" AutoEventWireup="true" CodeBehind="Menus.aspx.cs" Inherits="WebApplication1.Vista.Menus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Menu
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="favicon" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-md-12">
        <%-- Panel izquierdo --%>
        <div class="col-md-4">
            <div class="panel panel-primary">
                <div class="panel-heading text-center">
                    <label>
                        Sucursales
                    </label>
                </div>
                <div class="row container-fluid">
                    <div class="pull-right">
                        <asp:LinkButton runat="server" ID="BtnOcultar" OnClick="BtnOcultar_Click" CssClass="btn btn-sm btn-default">
                            <span class="glyphicon glyphicon-eye-open"></span>
                            <asp:Label ID="lblVisibilidadfiltros" runat="server" />
                        </asp:LinkButton><asp:LinkButton runat="server" ID="BtnLimpiar" CssClass="btn btn-sm btn-default"><span class="glyphicon glyphicon-trash"></span> Limpiar</asp:LinkButton><asp:LinkButton runat="server" ID="BtnBuscar" OnClick="BtnBABuscar_Click" CssClass="btn btn-sm btn-default"><span class="glyphicon glyphicon-search"></span> Buscar</asp:LinkButton>
                    </div>
                </div>
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
                    <!-- GridView de sucursal-->
                    <asp:GridView runat="server" ID="DGVSucursales" PageSize="10" AllowSorting="true" Style="margin-top: 5px;" OnRowDataBound="DGVSucursales_RowDataBound" OnSelectedIndexChanged="DGVSucursales_SelectedIndexChanged" CssClass="table table-bordered table-hover table-condensed table-striped input-sm" AutoGenerateColumns="false" DataKeyNames="ID" AllowPaging="True">
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
                            <asp:BoundField DataField="IDENTIFICADOR" HeaderText="Sucursal" SortExpression="IDENTIFICADOR" />
                            <asp:BoundField DataField="HORAAPARTURA" HeaderText="Apertura" SortExpression="HORAAPARTURA" />
                            <asp:BoundField DataField="HORACIERRE" HeaderText="Cierre" SortExpression="HORACIERRE" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>


        <div class="col-md-8">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="text-left">
                        <asp:Label ID="lblSeleccionSucursal" runat="server">Sucursal:</asp:Label><asp:Label ID="lblSucursal" runat="server" />
                        <asp:TextBox ID="txtUidSucursal" CssClass="hide" runat="server" />

                        <asp:Label ID="lblSeleccionOferta" runat="server">Oferta:</asp:Label><asp:Label ID="lblOferta" runat="server" />
                        <asp:TextBox ID="txtUidOferta" CssClass="hide" runat="server" />

                        <asp:Label ID="lblSeleccionSeccion" runat="server">Sección:</asp:Label><asp:Label ID="lblSeccion" runat="server" />
                        <asp:TextBox ID="txtUidSeccion" CssClass="hide" runat="server" />
                    </div>
                </div>
                <div class="clearfix">
                    <%--importacion y exportacion de Menu--%>
                    <div class="col-md-12" style="margin-top: 10px;">
                        <div class="pull-right">

                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="BtnExportarMenu" runat="server" OnClick="BtnExportarMenu_Click" CssClass="btn btn-sm btn-success " ToolTip="Descargar">
                                        <span class="glyphicon glyphicon-save">
                                        </span>
                                        Descargar menu
                                    </asp:LinkButton>
                                    <script type="text/javascript">
                                        function UploadFile(fileUpload) {
                                            if (fileUpload.value != '') {
                                                document.getElementById("<%=btnSubirImagen.ClientID %>").click();
                                            }
                                        }
                                    </script>
                                    <asp:LinkButton CssClass="btn btn-sm btn-warning " ID="BtnImportarMenu" runat="server">
                                                <span class="glyphicon glyphicon-open">
                                                </span>
                                                Importar menu
                                    </asp:LinkButton>
                                    <asp:FileUpload ID="FUImportExcel" CssClass="hide" runat="server" />
                                    <asp:Button Text="Subir" OnClick="MuestraFoto" CssClass="hide" ID="btnSubirImagen" runat="server" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="BtnExportarMenu" />
                                    <asp:PostBackTrigger ControlID="btnSubirImagen" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <%--importacion y exportacion de Menu--%>
                </div>
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
                    <ul class="nav nav-tabs">
                        <li role="presentation" id="liDatosOferta" runat="server">
                            <asp:LinkButton ID="btnDatosOferta" OnClick="btnDatosOferta_Click" runat="server">
                                <span class="glyphicon glyphicon-shopping-cart"></span>
                                Ofertas
                            </asp:LinkButton></li>
                        <li role="presentation" id="liDatosSecciones" runat="server">
                            <asp:LinkButton runat="server" ID="btnDatosSecciones" OnClick="btnDatosSecciones_Click" ToolTip="Seccion">
                                        <span class="glyphicon glyphicon-tasks"></span> Secciones</asp:LinkButton></li>
                        <li role="presentation" id="liDatosProductos" runat="server">
                            <asp:LinkButton runat="server" ID="BtnSeleccionDeProducto" OnClick="BtnSeleccionDeProducto_Click" ToolTip="Productos">
                                        <span class="glyphicon glyphicon-gift"></span> Productos</asp:LinkButton></li>
                        <li role="presentation" id="liDetallesProducto" runat="server">
                            <asp:LinkButton ID="BtnDetallesProducto" runat="server" OnClick="BtnDetallesProducto_Click" ToolTip="Detalles">
                                <span class="glyphicon glyphicon-info-sign">
                                </span>
                                Información
                            </asp:LinkButton></li>
                    </ul>
                    <%-- Pnel de Oferta --%><asp:Panel ID="PanelOferta" runat="server">
                        <%--importacion y exportacion de Menu--%>
                        <div class="col-md-12" style="margin-top: 10px;">
                            <div class="pull-right">

                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="btnExportarSecciones" runat="server" OnClick="btnExportarSecciones_Click" CssClass="btn btn-sm btn-success " ToolTip="Descargar">
                                        <span class="glyphicon glyphicon-save">
                                        </span>
                                        Descargar secciones
                                        </asp:LinkButton>
                                        <script type="text/javascript">
                                            function UploadFile1(fileUpload) {
                                                if (fileUpload.value != '') {
                                                    document.getElementById("<%=btnSubirseccion.ClientID %>").click();
                                                }
                                            }
                                        </script>
                                        <asp:LinkButton CssClass="btn btn-sm btn-warning " ID="BtnImportarSecciones" runat="server">
                                                <span class="glyphicon glyphicon-open">
                                                </span>
                                                Importar secciones
                                        </asp:LinkButton>
                                        <asp:FileUpload ID="FUImportarSecciones" CssClass="hide" runat="server" />
                                        <asp:Button Text="Subir" OnClick="MuestraSeccion" CssClass="hide" ID="btnSubirseccion" runat="server" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnExportarSecciones" />
                                        <asp:PostBackTrigger ControlID="btnSubirseccion" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <%--importacion y exportacion de Menu--%>
                        <%-- Barra de controles --%>
                        <div class="clearfix"></div>
                        <div class=" pull-left" style="margin-top: 5px; margin-left: 30px;">
                            <asp:LinkButton runat="server" ID="btnNuevoOferta" OnClick="btnNuevoOferta_Click" CssClass="btn btn-sm btn-default "><span class="glyphicon glyphicon-file"></span> Nuevo</asp:LinkButton><asp:LinkButton runat="server" ID="btnEditarOferta" OnClick="btnEditarOferta_Click" CssClass="btn btn-sm btn-default disabled"><span class="glyphicon glyphicon-cog"></span> Editar</asp:LinkButton><asp:LinkButton runat="server" ID="btnGuardarOferta" OnClick="btnGuardarOferta_Click" CssClass="btn btn-sm btn-success ">
                                <asp:Label runat="server" ID="lblAccionOferta"></asp:Label>
                            </asp:LinkButton><asp:LinkButton runat="server" ID="btnCancelarOferta" OnClick="btnCancelarOferta_Click" CssClass="btn btn-sm btn-danger "><asp:label CssClass=" glyphicon glyphicon-remove" runat="server" /></asp:LinkButton><asp:Label runat="server" ID="Label7"></asp:Label>
                            <div class="clearfix">
                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <%-- GridView de ofertas --%>
                                <asp:GridView runat="server" DataKeyNames="UID" OnPageIndexChanging="dgvoferta_PageIndexChanging" OnRowDataBound="dgvoferta_RowDataBound" OnSelectedIndexChanged="dgvoferta_SelectedIndexChanged" ID="dgvoferta" CssClass="table table-bordered table-hover table-condensed table-striped input-sm" PageSize="10" AllowSorting="true" Style="margin-top: 5px;" AllowPaging="True" AutoGenerateColumns="false">
                                    <EmptyDataTemplate>
                                        <div class="info">Para ver información debe hacer una consulta </div>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide">
                                            <FooterStyle CssClass="hide" />
                                            <HeaderStyle CssClass="hide" />
                                            <ItemStyle CssClass="hide" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="STRNOMBRE" HeaderText="Nombre" HeaderStyle-CssClass="text-center" SortExpression="StrNombre" />
                                        <asp:BoundField DataField="StrEstatus" HeaderStyle-CssClass="hide" ItemStyle-CssClass="hidden" />
                                        <asp:TemplateField HeaderText="Estatus" HeaderStyle-CssClass="text-center" SortExpression="STATUS">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEstatus" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <%-- Gestion de datos de oferta --%>
                            <div class="col-md-4">
                                <h6>Nombre</h6>
                                <asp:TextBox CssClass="form-control" ID="txtNombreOferta" runat="server" />
                                <h6>Estatus</h6>
                                <asp:DropDownList CssClass="form-control" ID="ddldEstatusOferta" runat="server">
                                </asp:DropDownList>
                            </div>
                            <%-- Seleccion de datos de oferta --%>
                            <table class=" col-md-2 text-left">
                                <th>Dias de oferta</th>
                                <tr>
                                    <td>
                                        <asp:CheckBox Text="TODOS" ID="chkSeleccionarTodosLosDias" AutoPostBack="true" OnCheckedChanged="chkSeleccionarTodosLosDias_CheckedChanged" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBoxList ID="chbxlistDiasOferta" RepeatDirection="vertical" RepeatLayout="Flow" runat="server">
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                            </table>
                        </div>

                    </asp:Panel>
                    <%-- Panel de Catalogo --%>
                    <asp:Panel ID="PanelSeccion" runat="server">

                        <div class="clearfix"></div>
                        <div class=" pull-left" style="margin-top: 5px; margin-left: 15px;">
                            <asp:LinkButton runat="server" ID="btnNuevoSeccion" OnClick="btnNuevoSeccion_Click" CssClass="btn btn-sm btn-default "><span class="glyphicon glyphicon-file"></span> Nuevo</asp:LinkButton><asp:LinkButton runat="server" ID="btnEditarSeccion" OnClick="btnEditarSeccion_Click" CssClass="btn btn-sm btn-default disabled"><span class="glyphicon glyphicon-cog"></span> Editar</asp:LinkButton><asp:LinkButton runat="server" ID="btnModificarSeccion" OnClick="btnModificarSeccion_Click" CssClass="btn btn-sm btn-success ">
                                <asp:Label runat="server" ID="lblAcciones"></asp:Label>
                            </asp:LinkButton><asp:LinkButton runat="server" ID="btnCancelarSeccion" OnClick="btnCancelarSeccion_Click" CssClass="btn btn-sm btn-danger "><asp:label CssClass=" glyphicon glyphicon-remove" runat="server" /></asp:LinkButton><asp:Label runat="server" ID="Label3"></asp:Label>
                        </div>
                        <div class="clearfix"></div>

                        <div class="col-md-6">
                            <!-- GridView de secciones-->
                            <asp:GridView runat="server" ID="DGVSeccion" PageSize="10" OnPageIndexChanging="DGVSeccion_PageIndexChanging" AllowSorting="true" Style="margin-top: 5px;" OnRowDataBound="DGVSeccion_RowDataBound" OnSelectedIndexChanged="DGVSeccion_SelectedIndexChanged" CssClass="table table-bordered table-hover table-condensed table-striped input-sm" AutoGenerateColumns="false" DataKeyNames="UID" AllowPaging="True">
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
                                    <asp:BoundField DataField="StrNombre" HeaderText="Nombre" HeaderStyle-CssClass="text-center" SortExpression="StrNombre" />
                                    <asp:BoundField DataField="StrHoraInicio" HeaderText="Apertura" HeaderStyle-CssClass="text-center" SortExpression="StrHoraInicio" />
                                    <asp:BoundField DataField="StrHoraFin" HeaderText="Cierre" HeaderStyle-CssClass="text-center" SortExpression="StrHoraFin" />
                                    <asp:TemplateField HeaderText="Estatus" HeaderStyle-CssClass="text-center" SortExpression="STATUS">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEstatus" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="text-center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="IntEstatus" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hide" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-12">
                                <h6>Nombre</h6>
                                <asp:TextBox CssClass="form-control" ID="txtSeccionNombre" runat="server" />
                            </div>
                            <div class="col-md-6">
                                <h6>Inicio</h6>
                                <asp:TextBox CssClass="form-control" ID="txtHoraInicio" runat="server" TextMode="Time" />
                            </div>
                            <div class="col-md-6">
                                <h6>Fin</h6>
                                <asp:TextBox CssClass="form-control" ID="txtHoraFin" runat="server" TextMode="Time" />
                            </div>
                            <div class="col-md-12">
                                <h6>Estatus</h6>
                                <asp:DropDownList CssClass="form-control" ID="ddlEstatusSeccion" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>

                    </asp:Panel>
                    <%-- Panel de Productos --%>
                    <asp:Panel ID="PanelProductos" runat="server">


                        <div class="clearfix"></div>
                        <div class=" pull-left" style="margin-top: 5px;">
                            <asp:LinkButton runat="server" ID="btnSeleccionarProducto" OnClick="btnSeleccionarProducto_Click" CssClass="btn btn-sm btn-default"><span class="glyphicon glyphicon-ok-circle"></span> Seleccionar</asp:LinkButton><asp:LinkButton runat="server" ID="btnGuardarSeleccionproducto" OnClick="btnGuardarSeleccionproducto_Click" CssClass="btn btn-sm btn-success ">
                                <asp:Label runat="server" ID="Label4" CssClass="glyphicon glyphicon-ok"></asp:Label>
                            </asp:LinkButton><asp:LinkButton runat="server" ID="btnCancelarSeleccionProducto" OnClick="btnCancelarSeleccionProducto_Click" CssClass="btn btn-sm btn-danger "><asp:label CssClass=" glyphicon glyphicon-remove" runat="server" /></asp:LinkButton><asp:Label runat="server" ID="Label5"></asp:Label>
                        </div>
                        <div class="clearfix"></div>


                        <div class="col-md-4" style="margin-top: 15px;">
                            <div class="input-group">
                                <asp:TextBox CssClass="form-control" ID="txtBusquedaNombre" aria-describedby="basic-addon1" runat="server" />
                                <span class="input-group-addon" id="basic-addon1">
                                    <asp:LinkButton OnClick="btnBuscarProducto_Click" ID="btnBuscarProducto" runat="server">
                                    <label class="glyphicon glyphicon-search"></label>
                                    </asp:LinkButton></span>
                            </div>
                        </div>
                        <div class="col-md-12">

                            <asp:DataList ID="DLProductos" RepeatColumns="2" DataKeyField="UID" RepeatDirection="Horizontal" RepeatLayout="Table" runat="server">
                                <HeaderStyle />
                                <HeaderTemplate>
                                    <label>Lista de productos </label>
                                </HeaderTemplate>
                                <SeparatorStyle BackColor="White" Width="150" />
                                <SeparatorTemplate />
                                <ItemStyle BorderStyle="Solid" BackColor="white" BorderColor="#0000ff" />
                                <ItemTemplate>
                                    <img src="<%#  Eval("STRRUTA") %>" width="80" height="80" />

                                    <p>
                                        <asp:Label ID="lblDescripcion" runat="server">
                                                                      <h6><%# Eval("STRNOMBRE") %></h6>  
                                        </asp:Label>
                                    </p>
                                    <asp:CheckBox ID="ChkProducto" runat="server" />

                                </ItemTemplate>
                            </asp:DataList>

                        </div>

                    </asp:Panel>
                    <%-- Panel informacion del producto --%>
                    <asp:Panel ID="PanelDetalles" runat="server">
                        <div class="clearfix"></div>
                        <div class=" pull-left" style="margin-top: 5px;">
                            <asp:LinkButton runat="server" ID="btnEditarProducto" OnClick="btnEditarProducto_Click" CssClass="btn btn-sm btn-default ">
                                <span class="glyphicon glyphicon-cog"></span> Editar</asp:LinkButton>
                            <asp:LinkButton runat="server" ID="btnModificarProducto" OnClick="btnModificarProducto_Click" CssClass="btn btn-sm btn-success ">
                                <asp:Label runat="server" ID="Label1" CssClass="glyphicon glyphicon-ok"></asp:Label>
                            </asp:LinkButton><asp:LinkButton runat="server" ID="btnCancelarProducto" OnClick="btnCancelarProducto_Click" CssClass="btn btn-sm btn-danger "><asp:label CssClass=" glyphicon glyphicon-remove" runat="server" /></asp:LinkButton><asp:Label runat="server" ID="Label2"></asp:Label>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-md-4">
                            <label>Información del producto</label>
                            <asp:DataList ID="DLProductoSeleccionado" DataKeyField="UID" OnItemCommand="DLProductoSeleccionado_ItemCommand1" runat="server">
                                <ItemStyle CssClass="col-md-6 panel panel-body" BorderStyle="Solid" BorderColor="#0000ff" />
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" CommandName="select">
                                        <div class="col-md-6">
                                            <img src="<%#  Eval("STRRUTA") %>" width="80" height="80" class="img-thumbnail" />
                                        </div>
                                        <div class="col-md-6 text-left">
                                            <p>
                                                <asp:Label ID="lblDescripcion" runat="server">
                                                                      <h6><%# Eval("STRNOMBRE") %></h6>  
                                                </asp:Label>
                                            </p>
                                        </div>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <SelectedItemStyle CssClass="col-md-3 panel panel-body" BackColor="#99ff99" />
                                <SelectedItemTemplate>
                                    <asp:LinkButton runat="server" CommandName="unselect">
                                        <div class="col-md-6">
                                            <img src="<%#  Eval("STRRUTA") %>" width="80" height="80" class="img-thumbnail" />
                                        </div>
                                        <div class="col-md-6 text-left">
                                            <p>
                                                <asp:Label ID="lblDescripcion" runat="server">
                                                                      <h6><%# Eval("STRNOMBRE") %></h6>  
                                                </asp:Label>
                                            </p>
                                        </div>
                                    </asp:LinkButton>
                                </SelectedItemTemplate>
                            </asp:DataList>
                        </div>
                        <div style="display: inline-block" class="col-md-8">
                            <asp:TextBox ID="txtUidProducto" CssClass="hide" runat="server" />
                            <h6>Tiempo de elaboración/ Costo al publico</h6>
                            <div class="col-md-4">
                                <h6>Horas:</h6>
                                <asp:DropDownList ID="DDLHoras" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <h6>Minutos:</h6>
                                <asp:DropDownList ID="DDLMinutos" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <h6>Precio del producto</h6>
                                <asp:TextBox ID="txtCostoProduto" OnTextChanged="txtCostoProduto_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-md-4">
                                <h6>Precio de la comisión</h6>
                                <asp:TextBox ID="txtCostoComision" Enabled="false" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-md-4">
                                <h6>Precio total</h6>
                                <asp:TextBox ID="txtCostoTotal" Enabled="false" CssClass="form-control" runat="server" />
                            </div>
                        </div>
                    </asp:Panel>

                </div>
            </div>

        </div>
    </div>
</asp:Content>
