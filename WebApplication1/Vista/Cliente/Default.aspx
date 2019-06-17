<%@ Page Title="Inicio" Language="C#" EnableEventValidation="true" ClientIDMode="AutoID" MasterPageFile="~/Vista/Cliente/MasterCliente.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.Vista.Cliente.Default" %>

<%@ Register Assembly="GMaps" Namespace="Subgurim.Controles" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">

    <style>
        .EfectoElementos {
            transition: all .2s ease-in-out;
        }

            .EfectoElementos:hover {
                transform: scale(1.1)
            }

        .text-overflow {
            display: block;
            overflow: hidden;
            white-space: nowrap;
            text-overflow: ellipsis;
        }

        .LetrasPortada {
            color: white;
            text-shadow: 0 0 3px rgba(0, 0, 0, .8);
            font-weight: bold;
            font-family: Helvetica, Arial, sans-serif;
        }
    </style>

    <div style="margin-top: 10px;">
        <asp:Panel ID="PanelBusqueda" runat="server">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                <div class="navbar" style="margin-bottom: 0px">
                    <ul class="nav nav-tabs navbar-left">
                        <li id="liPanelEmpresa" runat="server" class="active">
                            <asp:LinkButton ID="btnPanelEmpresa" OnClick="btnPanelEmpresa_Click" runat="server">
                                <span class="glyphicon glyphicon-tower"></span>
                                Empresas
                            </asp:LinkButton>
                        </li>
                        <li id="liPanelProductos" runat="server">
                            <asp:LinkButton ID="btnPanelProductos" OnClick="btnPanelProductos_Click" runat="server">
                                <span class="glyphicon glyphicon-tree-deciduous"></span>
                                Productos
                            </asp:LinkButton>
                        </li>
                    </ul>
                    <ul class="nav  navbar-right nav-tabs">
                        <li role="presentation">
                            <asp:LinkButton ID="btnFiltrosBusqueda" OnClick="btnFiltrosBusqueda_Click" runat="server">
                                <span class="glyphicon glyphicon-th"></span>
                                Filros
                            </asp:LinkButton>
                        </li>
                    </ul>
                </div>
                <div class="clearfix"></div>

                <asp:Panel ID="PanelCategorias" runat="server">
                    <div class="form panel-body QuitaEspacios">
                        <div class="row">
                            <div class="container">
                                <div class="form-inline">
                                    <div class="form-group">
                                        <div class="input-group" style="width: 150px">
                                            <label class=" input-group-addon" style="background-color: purple; color: white">Giro</label>
                                            <asp:DropDownList ID="DDlGiro" OnSelectedIndexChanged="DDlGiro_SelectedIndexChanged" AutoPostBack="true" Width="200" CssClass="form-control input-sm" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="input-group" style="width: 150px">
                                            <label class=" input-group-addon" style="background-color: purple; color: white">Categoria</label>
                                            <asp:DropDownList ID="DDlCategoria" OnSelectedIndexChanged="DDlCategoria_SelectedIndexChanged" AutoPostBack="true" Width="200" CssClass="form-control input-sm" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="input-group" style="width: 150px">
                                            <label class=" input-group-addon" style="background-color: purple; color: white">Subcategoria</label>
                                            <asp:DropDownList ID="DDlSubcategoria" CssClass="form-control input-sm" Width="200" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="input-group" style="width: 150px">
                                            <asp:TextBox CssClass="form-control input-sm" ID="txtNombreDeBusqueda" runat="server" />
                                            <asp:LinkButton CssClass="btn btn-default input-group-addon input-sm" ForeColor="White" BackColor="Purple" ID="btnBuscar" OnClick="btnBuscar_Click" runat="server">
                                    <span class=" glyphicon glyphicon-search" id="basic-addon1"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </asp:Panel>

                <table style="width: 100%; background-color: #f1f1f1;">
                    <tr>
                        <td>
                            <asp:BulletedList CssClass="breadcrumb" Style="background-color: #f1f1f1; margin-bottom: 0px" ID="blBuqueda" runat="server">
                            </asp:BulletedList>
                        </td>
                        <td class="pull-right">
                            <asp:Label ID="lblCantidadDeResultados" runat="server" /></td>
                    </tr>
                </table>
                <asp:Panel ID="PanelEmpresas" runat="server">
                    <div class="row">
                        <asp:DataList ID="DLEmpresa" OnItemDataBound="DLEmpresa_ItemDataBound" OnItemCommand="DLEmpresa_ItemCommand" DataKeyField="UIDEMPRESA" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                            <ItemStyle CssClass="col-xs-12 col-sm-6 col-md-4 col-lg-4 " />
                            <%--<FooterTemplate>
                        <div class="success">
                            <asp:Label Visible='<%#bool.Parse((DLEmpresa.Items.Count==0).ToString())%>'
                                runat="server" ID="lblNoRecord" Text="No hay empresa relacionada con la busqueda!"></asp:Label>
                        </div>
                    </FooterTemplate>--%>
                            <ItemTemplate>
                                <div style="padding: 20px;" class="EfectoElementos">
                                    <asp:LinkButton ID="btnSucursales" Style="text-decoration: none; color: black;" CommandName="VentanaEmpresas" runat="server">
                                        <div style="background-color: #f1f1f1; border-radius: 6px 6px 6px 6px; -moz-border-radius: 6px 6px 6px 6px; -webkit-border-radius: 6px 6px 6px 6px; border: 0px solid #000000; -webkit-box-shadow: 5px 5px 3px 2px #9C9C9C; -moz-box-shadow: 5px 5px 3px 2px #9C9C9C; box-shadow: 5px 5px 3px 2px #9C9C9C;">
                                            <div class="media">
                                                <asp:Label ID="lblUidRegistro" runat="server" />
                                                <div class="media-body text-center " style="padding: 15px">
                                                    <div class="media-heading">
                                                        <strong style="width: 155px">
                                                            <label class="text-overflow"><%# Eval("NOMBRECOMERCIAL") %></label>
                                                        </strong>
                                                    </div>
                                                    <asp:Label ID="lblCantidadDeSucursales" Style="margin-top: 20px;" runat="server" />
                                                    <div style="padding-top: 35px; padding-right: 5px">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td style="padding-left: 10px" class="pull-left">
                                                                    <asp:Label ID="lblCantidadDeProductos" CssClass="glyphicon glyphicon-shopping-cart" Style="margin-top: 20px;" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>

                                                </div>
                                                <div class="media-right QuitaEspacios">
                                                    <img class="media-object" src="<%# Eval("StrRuta") %>" height="150" width="150" alt="<%#Eval("NOMBRECOMERCIAL") %>" />
                                                </div>
                                            </div>
                                        </div>
                                    </asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </asp:Panel>

                <asp:Panel ID="PanelProductos" runat="server">
                    <div class="row">
                        <asp:DataList ID="DLProductos" OnItemCommand="DLProductos_ItemCommand" RepeatLayout="Flow" RepeatDirection="Horizontal" OnItemDataBound="DLProductos_ItemDataBound" runat="server">
                            <ItemStyle CssClass="col-xs-12 col-sm-6 col-md-4 col-lg-4 QuitaEspacios" />
                            <ItemTemplate>
                                <div style="padding: 20px;" class="EfectoElementos">
                                    <asp:HiddenField ID="HFUidProducto" runat="server" />
                                    <asp:LinkButton CommandName="VentanaModalCarrito" Style="text-decoration: none; color: black;" ID="btnAgregarProducto" runat="server">
                                        <div style="background-color: #f1f1f1; border-radius: 6px 6px 6px 6px; -moz-border-radius: 6px 6px 6px 6px; -webkit-border-radius: 6px 6px 6px 6px; border: 0px solid #000000; -webkit-box-shadow: 5px 5px 3px 2px #9C9C9C; -moz-box-shadow: 5px 5px 3px 2px #9C9C9C; box-shadow: 5px 5px 3px 2px #9C9C9C;">
                                            <div class="media">
                                                <div class="media-body">
                                                    <div class="media-heading text-center">
                                                        <strong style="width: 155px">
                                                            <label class="text-overflow "><%# Eval("STRNOMBRE") %></label>
                                                        </strong>
                                                    </div>
                                                    <p class="text-center">
                                                        <asp:Label ID="lblDescripcionDeProducto" runat="server" />
                                                    </p>
                                                    <div style="padding-top: 55px; padding-right: 5px">

                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td style="padding-left: 10px">
                                                                    <asp:Label ID="LblProductoEnCarrito" CssClass="glyphicon glyphicon-shopping-cart" runat="server" />
                                                                </td>
                                                                <td class="text-right">
                                                                    <h4>Desde
                                                                <asp:Label ID="lblPrecio" runat="server" /></h4>
                                                                    </h4>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                                <div class="media-right QuitaEspacios">
                                                    <img src="<%# Eval("STRRUTA") %>" class="media-object" style="width: 150px; height: auto; border-radius: 6px 6px 6px 6px; -moz-border-radius: 6px 6px 6px 6px; -webkit-border-radius: 6px 6px 6px 6px; border: 0px solid #000000;" alt="<%#Eval("STRNOMBRE") %>" />
                                                </div>
                                            </div>
                                        </div>
                                    </asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </asp:Panel>

            </div>
        </asp:Panel>
        <asp:Panel ID="PanelDeDetallesEmpresa" runat="server">
            <div class="col-md-12">

                <div class="row QuitaEspacios" style="margin-top: -4px">
                    <div class="container">
                        <div class="media" runat="server" id="PortadaEmpresa">
                            <div class="media-left QuitaEspacios" style="margin-top: 10px; width: 170px; height: 170px">
                                <asp:Image ID="imgEmpresa" Style="border-radius: 150px; border: 5px solid white; width: 170px; height: 170px" runat="server" />
                            </div>
                            <div class="media-body " style="vertical-align: middle;">
                                <div class="media-heading" style="margin-top: 10px; margin-left: 10px;">

                                    <asp:Label ID="lblNombreEmpresa" Font-Size="20" CssClass="LetrasPortada" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hfSucursalSeleccionada" runat="server" />

                                </div>
                                <div class="media-bottom" style="margin-top: 70px;">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-inline">
                                                <div class="form-group">
                                                    <asp:Label ID="lblHorarioDeServicio" CssClass="LetrasPortada" Font-Size="14" runat="server" />
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="lblDireccionSucursalSeleccionada" CssClass="LetrasPortada" Font-Size="14" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6 text-right">
                                            <asp:LinkButton ID="btnMostrarSucursales" Style="margin-right: 30px;" CssClass="btn btn-sm btn-default" OnClick="btnMostrarSucursales_Click" OnClientClick="MuestraSucursales()" runat="server">
                                                <span class="glyphicon glyphicon-refresh"></span>
                                                <asp:Label ID="lblNombreSucursal" runat="server" />
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-inline" style="background-color: #f1f1f1">
                            <div class="form-group" style="margin-left: 10px">
                                <asp:LinkButton ID="btnRegresar" OnClick="btnRegresar_Click" ToolTip="Regresar" CssClass="btn btn-sm btn-default" runat="server">
                    <span class="glyphicon glyphicon-repeat"></span>
                                </asp:LinkButton>
                            </div>
                            <div class="form-group">
                                <asp:DropDownList ID="ddlOfertas" CssClass="form-control" OnSelectedIndexChanged="ddlOfertas_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group" style="margin-left: 10px">
                                <asp:Menu ID="MnSecciones" StaticMenuStyle-CssClass="nav nav-pills" BorderStyle="Ridge" BorderColor="Black" DynamicSelectedStyle-CssClass="active" OnMenuItemClick="MnSecciones_MenuItemClick" Orientation="Horizontal" runat="server">
                                    <StaticSelectedStyle ForeColor="Purple" BackColor="White" />
                                    <StaticHoverStyle ForeColor="black" />

                                    <StaticMenuItemStyle ForeColor="black" />
                                </asp:Menu>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Label ID="Horario" runat="server" />
                <div class="clearfix"></div>
                <div class="row">
                    <div class="container">
                        <asp:DataList ID="DLProductosSucursal" OnItemCommand="DLProductosSucursal_ItemCommand" RepeatLayout="Flow" RepeatDirection="Horizontal" OnItemDataBound="DLProductosSucursal_ItemDataBound" runat="server">
                            <ItemStyle CssClass="col-xs-12 col-sm-6 col-md-4 col-lg-4 QuitaEspacios" />
                            <ItemTemplate>
                                <div style="padding: 10px;" class="EfectoElementos">
                                    <asp:HiddenField ID="HFUidProducto" runat="server" />
                                    <asp:LinkButton CommandName="VentanaModalCarrito" OnClientClick="MuestraProducto()" Style="text-decoration: none; color: black;" ID="btnAgregarProducto" runat="server">

                                        <div style="background-color: #f1f1f1; border-radius: 6px 6px 6px 6px; -moz-border-radius: 6px 6px 6px 6px; -webkit-border-radius: 6px 6px 6px 6px; border: 0px solid #000000; -webkit-box-shadow: 5px 5px 3px 2px #9C9C9C; -moz-box-shadow: 5px 5px 3px 2px #9C9C9C; box-shadow: 5px 5px 3px 2px #9C9C9C;">
                                            <%--<a data-toggle="modal" data-target="#myModal" style="text-decoration: none">--%>

                                            <div class="media" style="padding-left: 5px">

                                                <div class="media-body ">
                                                    <div class="media-heading text-center">
                                                        <h3>
                                                            <label><%# Eval("STRNOMBRE") %></label>
                                                        </h3>
                                                    </div>
                                                    <p>
                                                        <asp:Label ID="lblDescripcionDeProducto" runat="server" />
                                                    </p>
                                                    <div style="padding-top: 35px;">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td class="pull-left">
                                                                    <asp:Label ID="LblProductoEnCarrito" CssClass="glyphicon glyphicon-shopping-cart" runat="server" />
                                                                </td>
                                                                <td class="pull-right">
                                                                    <h4 class="QuitaEspacios">
                                                                        <asp:Label ID="lblPrecio" runat="server" />
                                                                    </h4>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                                <div class="media-right">
                                                    <img src="<%# Eval("STRRUTA") %>" style="width: 150px; height: auto; border-radius: 6px 6px 6px 6px; -moz-border-radius: 6px 6px 6px 6px; -webkit-border-radius: 6px 6px 6px 6px; border: 0px solid #000000;" alt="<%#Eval("STRNOMBRE") %>" />
                                                </div>
                                            </div>
                                            <%-- </a>--%>
                                        </div>
                                    </asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
