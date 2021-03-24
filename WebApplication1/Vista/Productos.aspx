<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/MasterDeliverix.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="WebApplication1.Vista.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Productos
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="favicon" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12">
        <%-- Panel  izquierdo --%>
        <div class="col-md-5">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Busqueda de productos
                </div>
                <div class="row container-fluid">
                    <div class="pull-right">
                        <asp:LinkButton runat="server" ID="BtnBAOcultar" CssClass="btn btn-sm btn-default">
                            <span class="glyphicon glyphicon-eye-open"></span>
                            <asp:Label ID="lblBAFiltrosVisibilidad" runat="server" />
                        </asp:LinkButton>
                        <asp:LinkButton runat="server" ID="BtnBALimpiar" OnClick="BtnBALimpiar_Click" CssClass="btn btn-sm btn-default"><span class="glyphicon glyphicon-trash"></span> Limpiar</asp:LinkButton>
                        <asp:LinkButton runat="server" ID="BtnBABuscar" OnClick="BtnBABuscar_Click" CssClass="btn btn-sm btn-default"><span class="glyphicon glyphicon-search"></span> Buscar</asp:LinkButton>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-md-12" style="margin-bottom: 5px;">
                        <div class="col-md-4">
                            <asp:CheckBox Text="Multiseleccion" AutoPostBack="true" ID="ChkFGiro" OnCheckedChanged="ChkFGiro_CheckedChanged" CssClass="checkbox" runat="server" />
                            <label>Giro</label>
                            <asp:DropDownList ID="DDLFGiro" AutoPostBack="true" OnSelectedIndexChanged="DDLFGiro_SelectedIndexChanged" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                            <asp:ListBox ID="LBFFGiro" CssClass="form-control" SelectionMode="Multiple" runat="server"></asp:ListBox>
                        </div>
                        <div class="col-md-4">
                            <asp:CheckBox Text="Multiseleccion" AutoPostBack="true" ID="ChkFCategoria" OnCheckedChanged="ChkFCategoria_CheckedChanged" CssClass="checkbox" runat="server" />
                            <label>Categoria</label>
                            <asp:DropDownList ID="DDLFCategoria" AutoPostBack="true" OnSelectedIndexChanged="DDLFCategoria_SelectedIndexChanged" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                            <asp:ListBox ID="LBFCategoria" CssClass="form-control" SelectionMode="Multiple" runat="server"></asp:ListBox>
                        </div>
                        <div class="col-md-4">
                            <asp:CheckBox Text="Multiseleccion" AutoPostBack="true" ID="ChkFSubcategoria" OnCheckedChanged="ChkFSubcategoria_CheckedChanged" CssClass="checkbox" runat="server" />
                            <label>Subcategoria</label>
                            <asp:DropDownList ID="DDLFSubcategoria" AutoPostBack="true" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                            <asp:ListBox ID="LBFSubcategoria" CssClass="form-control" SelectionMode="Multiple" runat="server"></asp:ListBox>
                        </div>
                        <div class="col-md-4">
                            <h6>Nombre</h6>
                            <asp:TextBox ID="txtFNombre" CssClass="form-control" runat="server" />
                        </div>
                    </div>
                    <asp:GridView ID="dgvProductos" AutoGenerateColumns="false" AllowSorting="true" OnSorting="dgvProductos_Sorting" AllowPaging="True" OnPageIndexChanging="dgvProductos_PageIndexChanging" PageSize="10" DataKeyNames="UID" OnSelectedIndexChanged="dgvProductos_SelectedIndexChanged" OnRowDataBound="dgvProductos_RowDataBound" CssClass="table table-bordered table-hover table-condensed table-striped input-sm" runat="server">
                        <EmptyDataTemplate>
                            No hay informacion relacionada
                        </EmptyDataTemplate>
                        <SelectedRowStyle CssClass="table table-hover input-sm success" />
                        <SortedAscendingHeaderStyle CssClass="glyphicon glyphicon-sort-by-alphabet" />
                        <Columns>
                            <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide">
                                <FooterStyle CssClass="hide" />
                                <HeaderStyle CssClass="hide" />
                                <ItemStyle CssClass="hide" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="STRNOMBRE" HeaderText="Giro" HeaderStyle-CssClass="text-center" SortExpression="STRNOMBRE" />
                            <asp:TemplateField HeaderText="Estatus" SortExpression="STATUS">
                                <ItemTemplate>
                                    <asp:Label ID="lblEstatus" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ESTATUS" HeaderText="Estatus" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" SortExpression="ESTATUS" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <%--  Panel derecho --%>
        <div class="col-md-7">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Gestion de producto
                </div>
                <div class=" pull-left">
                    <asp:LinkButton runat="server" ID="btnNuevo" OnClick="btnNuevo_Click" CssClass="btn btn-sm btn-default "><span class="glyphicon glyphicon-file"></span> Nuevo</asp:LinkButton>
                    <asp:LinkButton runat="server" ID="btnEditar" OnClick="btnEditar_Click" CssClass="btn btn-sm btn-default disabled"><span class="glyphicon glyphicon-cog"></span> Editar</asp:LinkButton>
                    <asp:LinkButton runat="server" ID="btnGuardar" OnClick="btnGuardar_Click" CssClass="btn btn-sm btn-success ">
                        <asp:Label runat="server" ID="lblGuardarDatos"></asp:Label>
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" ID="btnCancelar" OnClick="btnCancelar_Click" CssClass="btn btn-sm btn-danger "><asp:label CssClass=" glyphicon glyphicon-remove" runat="server" /></asp:LinkButton><asp:Label runat="server" ID="lblEstado"></asp:Label>
                </div>
                <div class="clearfix"></div>
                <%--Panel de mensaje de sistema--%>
                <asp:Panel CssClass="well well-lg" ID="PanelMensaje" runat="server">
                    <asp:Label ID="LblMensaje" Text="Mensaje del sistema" Font-Size="Large" runat="server" />
                    <div class="pull-right">
                        <asp:LinkButton ID="BtnCerrarPanelMensaje" CssClass="btn btn-sm btn-danger" OnClick="BtnCerrarPanelMensaje_Click" ForeColor="White" runat="server">
                            <span class="glyphicon glyphicon-remove"></span>
                        </asp:LinkButton>
                    </div>
                </asp:Panel>
                <div class="panel-body">

                    <asp:TextBox ID="txtUidProducto" CssClass="hide" runat="server" />

                    <%-- Barra de navegacion --%>
                    <ul class="nav nav-tabs">
                        <li role="presentation" id="liDatosGenerales" runat="server">
                            <asp:LinkButton runat="server" ID="btnDatosGenerales" OnClick="btnDatosGenerales_Click" ToolTip="General">
                                        <span class="glyphicon glyphicon-globe"></span> GENERAL</asp:LinkButton></li>
                        <li role="presentation" id="liDatosGiro" runat="server">
                            <asp:LinkButton runat="server" ID="btnDatosGiro" OnClick="btnDatosGiro_Click" ToolTip="Giro">
                                        <span class="glyphicon glyphicon-tasks"></span> GIRO</asp:LinkButton></li>
                        <li role="presentation" id="liDatosCategoria" runat="server">
                            <asp:LinkButton runat="server" ID="btnGridCategoria" OnClick="btnGridCategoria_Click" ToolTip="Categoria">
                                        <span class="glyphicon glyphicon-object-align-horizontal"></span>
                                        Categoria
                            </asp:LinkButton></li>
                        <li role="presentation" id="liDatosSubcategoria" runat="server">
                            <asp:LinkButton runat="server" ID="btnGridSubcategoria" OnClick="btnGridSubcategoria_Click" ToolTip="SUBCATEGORIA">
                                        <span class="glyphicon glyphicon-object-align-left"></span>
                                        Subcategoria
                            </asp:LinkButton>
                        </li>
                    </ul>
                    <asp:Panel ID="panelGeneral" runat="server">
                        <div class="col-md-6">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="col-md-12 text-center pull-right" style="margin-top: 10px;">
                                        <asp:Image runat="server" CssClass="img img-thumbnail" ID="ImgProducto" Width="200px" />
                                        <asp:TextBox CssClass="hide" ID="txtUidImagen" runat="server" />
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class=" col-md-12 text-center" style="margin-top: 5px;">
                                        <script type="text/javascript">
                                            function UploadFile(fileUpload) {
                                                if (fileUpload.value != '') {
                                                    document.getElementById("<%=btnImagenGiro.ClientID %>").click();
                                                }
                                            }
                                        </script>
                                        <asp:LinkButton CssClass="btn btn-sm btn-default" ID="btnImagen" OnClick="SeleccionarImagen" runat="server">
                                                <span class="glyphicon glyphicon-open">
                                                </span>
                                                Cargar Imagen
                                        </asp:LinkButton>
                                        <asp:FileUpload ID="FUImagen" CssClass="hide" runat="server" />
                                        <asp:Button CssClass="hide" OnClick="MuestraFoto" ID="btnImagenGiro" runat="server" />
                                        <asp:TextBox runat="server" ID="txtNombreDeImagen" CssClass="hide" />
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnImagenGiro" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-md-6">
                            <h6>Nombre</h6>
                            <asp:TextBox CssClass="form-control" MaxLength="150" ID="txtNombre" runat="server" />
                            <h6>Estatus</h6>
                            <asp:DropDownList ID="ddlestatus" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                            <h6>Descripcion</h6>
                            <asp:TextBox CssClass="form-control" ID="txtDescripcion" Rows="5" TextMode="MultiLine" runat="server" />
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="panelGiro" runat="server">
                        <div style="margin-top: 5px;">
                            <div class="col-md-12" style="overflow-x: hidden; overflow: scroll; height: 400px; margin-left: 15px">

                                <asp:DataList RepeatColumns="3" CssClass="row container-fluid" ID="DLGiro" OnItemCommand="DLGiro_ItemCommand" DataKeyField="UIDVM" RepeatDirection="Horizontal" CellSpacing="3" RepeatLayout="Table" runat="server">
                                    <SeparatorStyle BackColor="White" HorizontalAlign="Right" Height="180px" CssClass="col-md-1" />
                                    <SeparatorTemplate />
                                    <ItemStyle CssClass="col-md-3 panel panel-body" />
                                    <ItemTemplate>
                                        <div class="col-md-6">
                                            <img src="<%#  Eval("RUTAIMAGEN") %>" width="80" height="80" class="img-thumbnail" />
                                        </div>
                                        <div class="col-md-6">
                                            <div class="col-md-12 text-justify">
                                                <p>
                                                    <span>
                                                        <asp:Label ID="lblDescripcion" runat="server">
                                                                      <h6><%# Eval("STRNOMBRE") %></h6>  
                                                        </asp:Label>
                                                    </span>
                                                </p>
                                            </div>
                                            <div class="col-md-12 text-center">
                                                <div class="pull-right">
                                                    <asp:CheckBox ID="ChkGiro" CssClass="checkbox" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>

                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="panelCategoria" runat="server">
                        <div class="col-md-3">
                            <label>Giros seleccionados</label>

                            <div style="overflow-x: hidden; overflow: scroll; height: 100%; margin-top: 5px;">

                                <asp:DataList CssClass="row" ID="DLGiroSeleccionado" RepeatColumns="1" DataKeyField="UIDVM" OnItemCommand="DLGiroSeleccionado_ItemCommand" RepeatDirection="Horizontal" CellSpacing="3" RepeatLayout="Table" runat="server">
                                    <ItemStyle CssClass="col-md-3 panel panel-body" BorderStyle="Solid" BorderColor="DeepSkyBlue" />
                                    <SelectedItemStyle CssClass="col-md-3 panel panel-body" />
                                    <SelectedItemTemplate>

                                        <asp:LinkButton CommandName="unselect" CssClass=" btn-default" runat="server">
                                            Seleccionado
                                                    <div class="col-md-6">
                                                        <img src="<%#  Eval("RUTAIMAGEN") %>" width="80" height="80" class="img-thumbnail" />
                                                    </div>
                                            <div class="col-md-6 ">
                                                <p>
                                                    <asp:Label ID="lblDescripcion" runat="server">
                                                                      <h6><%# Eval("STRNOMBRE") %></h6>  
                                                    </asp:Label>
                                                </p>
                                            </div>
                                        </asp:LinkButton>
                                    </SelectedItemTemplate>


                                    <ItemTemplate>
                                        <asp:LinkButton CommandName="select" CssClass=" btn-default" runat="server">
                                            <div class="col-md-6">
                                                <img src="<%#  Eval("RUTAIMAGEN") %>" width="80" height="80" class="img-thumbnail" />
                                            </div>
                                            <div class="col-md-6">
                                                <p>
                                                    <asp:Label ID="lblDescripcion" runat="server">
                                                                      <h6><%# Eval("STRNOMBRE") %></h6>  
                                                    </asp:Label>
                                                </p>
                                            </div>
                                        </asp:LinkButton>

                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </div>

                        <div class="col-md-9" style="margin-top: 5px;">
                            <label>Seleccionar Categorias</label>
                            <asp:DataList CssClass="col-md-12" ID="DLCategoria" RepeatColumns="4" DataKeyField="UIDCATEGORIA" RepeatDirection="Horizontal" CellSpacing="2" RepeatLayout="Table" runat="server">
                                <ItemStyle CssClass="col-md-3 panel panel-body" BorderStyle="Solid" BorderColor="DeepSkyBlue" />
                                <ItemTemplate>
                                    <div class="col-md-8">
                                        <img src="<%#  Eval("RUTAIMAGEN") %>" width="80" height="80" class="img-thumbnail" />
                                    </div>
                                    <div class="col-md-4">
                                        <p>
                                            <asp:Label ID="lblDescripcion" runat="server">
                                                                      <h6><%# Eval("STRNOMBRE") %></h6>  
                                            </asp:Label>
                                        </p>
                                        <div class="pull-right">
                                            <asp:CheckBox ID="ChkCategoria" runat="server" />
                                        </div>
                                    </div>

                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="panelSubcategoria" runat="server">
                        <div class="col-md-3">

                            <label>Categorias seleccionadas</label>
                            <div style="overflow-x: hidden; overflow: scroll; height: 100%; margin-top: 5px;">

                                <asp:DataList CssClass="col-md-12" ID="DlCategoriaSeleccionada" RepeatColumns="1" DataKeyField="UIDCATEGORIA" OnItemCommand="DlCategoriaSeleccionada_ItemCommand" RepeatDirection="Horizontal" CellSpacing="3" RepeatLayout="Table" runat="server">
                                    <ItemStyle CssClass="col-md-3 panel panel-body" BorderStyle="Solid" BorderColor="DeepSkyBlue" />
                                    <SelectedItemStyle CssClass="col-md-3 panel panel-body success" />
                                    <SelectedItemTemplate>

                                        <asp:LinkButton CommandName="unselect" CssClass=" btn-default" runat="server">

                                            <div class="col-md-6">
                                                <img src="<%#  Eval("RUTAIMAGEN") %>" width="80" height="80" class="img-thumbnail" />
                                            </div>
                                            <div class="col-md-6" style="color: black">
                                                <p>
                                                    <asp:Label ID="lblDescripcion" runat="server">
                                                                      <h6><%# Eval("STRNOMBRE") %></h6>  
                                                    </asp:Label>
                                                </p>
                                            </div>


                                        </asp:LinkButton>
                                    </SelectedItemTemplate>


                                    <ItemTemplate>
                                        <asp:LinkButton CommandName="select" CssClass=" btn-default" runat="server">

                                            <div class="col-md-6">
                                                <img src="<%#  Eval("RUTAIMAGEN") %>" width="80" height="80" class="img-thumbnail" />
                                            </div>
                                            <div class="col-md-6">
                                                <p>
                                                    <asp:Label ID="lblDescripcion" Style="color: black" runat="server">
                                                                      <h6><%# Eval("STRNOMBRE") %></h6>  
                                                    </asp:Label>
                                                </p>
                                            </div>


                                        </asp:LinkButton>

                                    </ItemTemplate>
                                </asp:DataList>

                            </div>
                        </div>



                        <div class="col-md-9">
                            <label>Seleccionar subcategorias</label>
                            <asp:DataList CssClass="col-md-12" ID="dlSubcategoria" RepeatColumns="4" DataKeyField="UID" RepeatDirection="Horizontal" CellSpacing="3" RepeatLayout="Table" runat="server">
                                <ItemStyle CssClass="col-md-3 panel panel-body" BorderStyle="Solid" BorderColor="DeepSkyBlue" />
                                <ItemTemplate>
                                    <div class="col-md-8">
                                        <img src="<%#  Eval("RUTAIMAGEN") %>" width="80" height="80" class="img-thumbnail" />
                                    </div>
                                    <div class="col-md-4">
                                        <p>
                                            <span>
                                                <asp:Label ID="lblDescripcion" runat="server">
                                                                      <h6><%# Eval("STRNOMBRE") %></h6>  
                                                </asp:Label>
                                            </span>
                                        </p>
                                        <div class="pull-right">
                                            <asp:CheckBox ID="ChkSubcategoria" runat="server" />
                                        </div>
                                    </div>

                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
