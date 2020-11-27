<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/MasterDeliverix.Master" AutoEventWireup="true" CodeBehind="Catalogos.aspx.cs" Inherits="WebApplication1.Vista.Catalogos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Categorias
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="favicon" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-md-12">
                <%-- Panel izquierdo --%>
                <div class="col-md-6">

                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            Busqueda de categorias
                        </div>
                        <div class="pull-right">
                            <asp:LinkButton runat="server" ID="BtnBOcultar" OnClick="BtnBOcultar_Click" CssClass="btn btn-sm btn-default">
                                <span class="glyphicon glyphicon-eye-open"></span>
                                <asp:Label ID="lblBAFiltrosVisibilidad" runat="server" />
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="BtnBLimpiar" OnClick="BtnBALimpiar_Click" CssClass="btn btn-sm btn-default"><span class="glyphicon glyphicon-trash"></span> Limpiar</asp:LinkButton>
                            <asp:LinkButton runat="server" ID="BtnBBuscar" OnClick="BtnBBuscar_Click" CssClass="btn btn-sm btn-default"><span class="glyphicon glyphicon-search"></span> Buscar</asp:LinkButton>
                        </div>
                        <div class="clearfix"></div>
                        <div class="panel-body">
                            <asp:Panel ID="PnlFiltrosCategoria" runat="server">
                                <div class="col-md-12" style="margin-bottom: 10px;">
                                    <div class="col-md-4">
                                        <h6>Nombre</h6>
                                        <asp:TextBox ID="txtFiltroNombre" CssClass="form-control" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <h6>Estatus</h6>
                                        <asp:DropDownList ID="ddlFiltroEstatus" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </asp:Panel>
                            <%-- Barra de navegacion inferior --%>
                            <ul class="nav nav-tabs pull-left" style="margin-bottom: 10px">
                                <li role="presentation" id="libtnGridGiro" runat="server">
                                    <asp:LinkButton runat="server" ID="BtnGridGiro" OnClick="BtnGridGiro_Click" >
                                        <span class="glyphicon glyphicon-adjust"></span>
                                        Giro
                                    </asp:LinkButton>
                                </li>
                                <li role="presentation" id="libtnGridCategoria" runat="server">
                                    <asp:LinkButton runat="server" ID="btnGridCategoria" OnClick="btnGridCategoria_Click" >
                                        <span class="glyphicon glyphicon-object-align-horizontal"></span>
                                        Categoria
                                    </asp:LinkButton>
                                </li>
                                <li role="presentation" id="libtnGridSubcategoria" runat="server">
                                    <asp:LinkButton runat="server" ID="btnGridSubcategoria" OnClick="btnGridSubcategoria_Click" >
                                        <span class="glyphicon glyphicon-object-align-left"></span>
                                        Subcategoria
                                    </asp:LinkButton>
                                </li>
                            </ul>
                            <%-- Panel de gridview Giro --%>
                            <asp:Panel ID="PanelGridGiro" runat="server">

                                <asp:GridView ID="DGVGiro" AutoGenerateColumns="false"  OnRowDataBound="DGVGiro_RowDataBound" OnPageIndexChanging="DGVGiro_PageIndexChanging" OnSelectedIndexChanged="DGVGiro_SelectedIndexChanged" Style="margin-top: 5px;" AllowSorting="true" AllowPaging="True" DataKeyNames="UIDVM" runat="server" CssClass="table table-bordered table-hover table-condensed table-striped input-sm text-center" PageSize="20">
                                    <EmptyDataTemplate>
                                        No se encontro información 
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
                                        <asp:BoundField DataField="INTESTATUS" HeaderText="Estatus" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" SortExpression="INTESTATUS" />
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                            <%-- Panel de gridview Categorias --%>
                            <asp:Panel ID="PanelGridCategoria" runat="server">

                                <asp:GridView ID="DGVCategorias" OnPageIndexChanging="DGVCategorias_PageIndexChanging" PageSize="20" Style="margin-top: 5px;" AllowSorting="true" AllowPaging="True" DataKeyNames="UIDCATEGORIA" AutoGenerateColumns="false" OnRowDataBound="DGVCategorias_RowDataBound" OnSelectedIndexChanged="DGVCategorias_SelectedIndexChanged" CssClass="table table-bordered table-hover table-condensed table-striped input-sm text-center" runat="server">
                                    <EmptyDataTemplate>
                                        No se encontro información
                                    </EmptyDataTemplate>
                                    <SelectedRowStyle CssClass="table table-hover input-sm success" />
                                    <SortedAscendingHeaderStyle CssClass="glyphicon glyphicon-sort-by-alphabet" />
                                    <Columns>
                                        <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide">
                                            <FooterStyle CssClass="hide" />
                                            <HeaderStyle CssClass="hide" />
                                            <ItemStyle CssClass="hide" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="STRNOMBRE" HeaderText="Categoria" HeaderStyle-CssClass="text-center" SortExpression="STRNOMBRE" />
                                        <asp:TemplateField HeaderText="Estatus" SortExpression="STATUS">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEstatus" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ESTATUS" HeaderText="Estatus" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" SortExpression="ESTATUS" />
                                        <asp:BoundField DataField="STRDESCRIPCION" HeaderText="Descripcion" HeaderStyle-CssClass="text-center" SortExpression="STRDESCRIPCION" />
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                            <%-- Panel de gridview Subcategorias --%>
                            <asp:Panel ID="PanelGridSubcategoria" runat="server">


                                <asp:GridView ID="DGVSubcategorias" AutoGenerateColumns="false" OnPageIndexChanging="DGVSubcategorias_PageIndexChanging" Style="margin-top: 5px;" AllowSorting="true" AllowPaging="True" OnRowDataBound="DGVSubcategorias_RowDataBound" DataKeyNames="UID" runat="server" OnSelectedIndexChanged="DGVSubcategorias_OnSelectedIndexChanged" CssClass="table table-bordered table-hover table-condensed table-striped input-sm text-center" PageSize="20">
                                    <EmptyDataTemplate>
                                        No se encontro información 
                                    </EmptyDataTemplate>
                                    <SelectedRowStyle CssClass="table table-hover input-sm success" />
                                    <SortedAscendingHeaderStyle CssClass="glyphicon glyphicon-sort-by-alphabet" />
                                    <Columns>
                                        <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide">
                                            <FooterStyle CssClass="hide" />
                                            <HeaderStyle CssClass="hide" />
                                            <ItemStyle CssClass="hide" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="STRNOMBRE" HeaderText="Subcategoria" HeaderStyle-CssClass="text-center" SortExpression="STRNOMBRE" />
                                        <asp:BoundField DataField="STRDESCRIPCION" HeaderText="Descripcion" HeaderStyle-CssClass="text-center" SortExpression="STRDESCRIPCION" />
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                    </div>
                    <%--<asp:ComboBox ID="ComboBox1" DropDownStyle="DropDown"
                AutoCompleteMode="Suggest"
                CaseSensitive="false"
                RenderMode="Inline"
                ItemInsertLocation="Prepend"
                ListItemHoverCssClass="ComboBoxListItemHover" runat="server">
                <asp:ListItem>India</asp:ListItem>
                <asp:ListItem>Lanka</asp:ListItem>
                <asp:ListItem>Pak</asp:ListItem>
                <asp:ListItem>Aus</asp:ListItem>
                <asp:ListItem>Aps</asp:ListItem>
            </asp:ComboBox>--%>
                </div>
                <%-- Panel derecho --%>
                <div class="col-md-6">

                    <asp:Panel runat="server">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                Gestion de areas
                            </div>
                            <div class=" pull-left">
                                <asp:LinkButton runat="server" ID="btnNuevo" OnClick="btnNuevo_Click" CssClass="btn btn-sm btn-default "><span class="glyphicon glyphicon-file"></span> Nuevo</asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnEditar" OnClick="btnEditar_Click" CssClass="btn btn-sm btn-default disabled"><span class="glyphicon glyphicon-cog"></span> Editar</asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnGuardar" OnClick="btnGuardar_Click" CssClass="btn btn-sm btn-success ">
                                    <asp:Label runat="server" ID="lblGuardarDatos"></asp:Label>
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnCancelar" OnClick="btnCancelar_Click" CssClass="btn btn-sm btn-danger "><asp:label CssClass=" glyphicon glyphicon-remove" runat="server" />
                                </asp:LinkButton>
                                <asp:Label runat="server" ID="lblEstado"></asp:Label>
                            </div>
                            <div class="clearfix"></div>
                            <div class="panel-body">
                                <%--<ul class="nav nav-tabs">
                                    <li role="presentation" id="liDatosGiro" runat="server">
                                        <asp:LinkButton ID="btnDatosGiro" OnClick="btnDatosGiro_Click" runat="server">
                                            <span class="glyphicon glyphicon-adjust"></span>
                                            Giro
                                        </asp:LinkButton>
                                    </li>
                                    <li role="presentation" id="liDatosCategoria" runat="server">
                                        <asp:LinkButton runat="server" ID="btnDatosCategoria" OnClick="btnDatosCategoria_Click">
                                            <span class="glyphicon glyphicon-object-align-horizontal"></span> 
                                            CATEGORIA
                                        </asp:LinkButton>
                                    </li>
                                    <li role="presentation" id="liDatosSubCategorias" runat="server">
                                        <asp:LinkButton runat="server" ID="btnDatosSubCategorias" OnClick="btnDatosSubCategorias_Click">
                                            <span class="glyphicon glyphicon-object-align-left"></span>
                                             SUBCATEGORIAS
                                        </asp:LinkButton>
                                    </li>

                                </ul>--%>
                                <div class="col-md-12">

                                    <%-- Imagen --%>
                                    <div class="col-md-6 ">

                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <div class="col-md-12 text-center pull-right" style="margin-top: 10px;">
                                                    <asp:Image runat="server" CssClass="img img-thumbnail" ID="ImgGiro" Width="200px" />
                                                    <asp:TextBox CssClass="hide" ID="txtUidImagenGiro" runat="server" />
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class=" col-md-12 text-center" style="margin-top: 5px;">
                                                    <script type="text/javascript">
                                                        function UploadFileGiro(fileUpload) {
                                                            if (fileUpload.value != '') {
                                                                document.getElementById("<%=btnImagenGiro.ClientID %>").click();
                                                                }
                                                            }
                                                    </script>
                                                    <asp:LinkButton CssClass="btn btn-sm btn-default" ID="btn2ImagenGiro" OnClick="ActivaFileUploadGiro" runat="server">
                                                <span class="glyphicon glyphicon-open">
                                                </span>
                                                Cargar Imagen
                                                    </asp:LinkButton>
                                                    <asp:FileUpload ID="FUGiro" CssClass="hide" runat="server" />
                                                    <asp:Button OnClick="MuestraFotoGiro" CssClass="hide" ID="btnImagenGiro" runat="server" />

                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnImagenGiro" />
                                            </Triggers>
                                        </asp:UpdatePanel>

                                    </div>

                                    <%-- Informacion --%>
                                    <div class="col-md-6">
                                        <h6>Nombre</h6>
                                        <asp:TextBox CssClass="form-control" ID="txtNombreGiro" runat="server" />
                                        <h6>Estatus</h6>
                                        <asp:DropDownList ID="DDLEstatusGIro" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                        <h6>Descripcion</h6>
                                        <asp:TextBox CssClass="form-control" TextMode="MultiLine" Rows="5" ID="txtDescripcionGiro" runat="server" />
                                    </div>

                                    <asp:TextBox CssClass="hide" ID="txtUidGiro" runat="server" />
                                    <asp:TextBox ID="txtIdCategoria" CssClass="hide" runat="server" />
                                    <asp:TextBox ID="txtUidSubCategoria" CssClass="hide" runat="server" />


                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
