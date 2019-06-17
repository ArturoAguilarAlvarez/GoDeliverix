<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Cliente/MasterCliente.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="WebApplication1.Vista.Cliente.Carrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div style="margin-top: 10px;">
        <div class="col-md-6 col-xs-12" style="border-right: 2px solid #999999;">
            <%-- Panel del carrito de compras --%>
            <asp:Panel ID="PanelCarrito" runat="server">
                <div class="list-group">
                    <asp:DataList ID="DLCarrito" OnItemCommand="DLCarrito_ItemCommand" OnItemDataBound="DLCarrito_ItemDataBound" DataKeyField="UidSucursal" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                        <ItemStyle CssClass="col-xs-12 col-sm-6 col-md-4 col-lg-12 QuitaEspacios" />
                        <ItemTemplate>
                            <div class="list-group-item QuitaEspacios" style="background-color: #f1f1f1">
                                <%--<div class="pull-right QuitaEspacios">
                                    
                                </div>--%>
                                <div class="list-group-item-heading" style="margin: 10px;">
                                    <table style="width: 100%;" class="QuitaEspacios">
                                        <tr>
                                            <td>
                                                <h4>
                                                    <label><%#Eval("Empresa") %></label>
                                                </h4>
                                            </td>
                                            <td>
                                                <h4>
                                                    <label>
                                                        Productos
                                                    </label>
                                                    <label><%#Eval("Cantidad") %></label>
                                                </h4>

                                            </td>
                                            <td>
                                                <h4>Total
                                            <label>$<%#Eval("Total") %></label>
                                                </h4>
                                            </td>
                                            <td class="pull-right">
                                                <asp:LinkButton CssClass="btn btn-sm btn-info" CommandName="PanelDetalles" ID="btnInformacion" Style="margin: 5px" runat="server">
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <asp:Panel ID="PanelDetalles" runat="server">

                                    <div class="text-center" style="width: 100%; border-top: solid purple; background-color: #f1f1f1; border-bottom: solid purple">
                                        <div style="width: 100%; padding: 5px; align-content: center">
                                            <asp:DataList runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" DataKeyField="UidRegistroProductoEnCarrito" ID="DLDetallesCompra" OnItemCommand="DLDetallesCompra_ItemCommand" OnItemDataBound="DLDetallesCompra_ItemDataBound">
                                                <ItemStyle CssClass="QuitaEspacios" />
                                                <ItemTemplate>
                                                    <div style="background-color: #f1f1f1;">
                                                        <div class="media" style="padding: 10px;">
                                                            <div class="media-left">
                                                                <img src=" <%#Eval("STRRUTA") %>" height="150" width="150" />
                                                            </div>
                                                            <div class="media-body QuitaEspacios" style="background-color: white;">
                                                                <div class="media-heading QuitaEspacios">
                                                                    <div class="row QuitaEspacios">
                                                                        <div class="col-md-6 QuitaEspacios">
                                                                            <h2 class="QuitaEspacios"><%#Eval("STRNOMBRE") %></h2>

                                                                            <%--Boton Quitar--%>
                                                                            <asp:LinkButton CssClass="btn btn-sm btn-danger" Style="border-radius: 50px;" ID="btnQuita" CommandName="Quita" runat="server">
                                                                <span class="glyphicon glyphicon-minus"></span>
                                                                            </asp:LinkButton>

                                                                            <label>
                                                                                <%#Eval("Cantidad") %>
                                                                            </label>
                                                                            <%--Boton Agregar--%>
                                                                            <asp:LinkButton CssClass="btn btn-sm btn-success" Style="border-radius: 50px;" ID="btnAgrega" CommandName="Agrega" runat="server">
                                                                <span class="glyphicon glyphicon-plus"></span>
                                                                            </asp:LinkButton>

                                                                            <%--Boton Eliminar--%>
                                                                            <asp:LinkButton CssClass="btn btn-sm btn-default" ID="btnEliminar" CommandName="EliminaProducto" runat="server">
                                                                        <span class="glyphicon glyphicon-trash"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                        <div class="col-md-6 QuitaEspacios">




                                                                            <h3 class="QuitaEspacios">Importe $<%#Eval("Subtotal") %></h3>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div style="padding: 5px">

                                                                    <%--Textbox de notas--%>
                                                                    <asp:TextBox TextMode="MultiLine" OnTextChanged="txtNotas_TextChanged" ID="txtNotas" CssClass="form-control" Rows="3" runat="server" />

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </div>
                                    </div>

                                    <div class="text-right" style="padding-top: 10px; padding-right: 10px;">
                                        <div class="form-inline " style="align-items: flex-end;">
                                            <div class="form-group">
                                                <label>Subtotal</label>
                                                <i>$<%#Eval("Subtotal") %></i>
                                            </div>
                                            <div class="form-group">
                                                <asp:LinkButton CssClass="btn btn-sm btn-info" ID="btnSeleccionarDistribuidora" Style="margin-bottom: 5px; margin-left: 10px;" CommandName="SeleccionDistribuidora" runat="server">
                                                            <span class="glyphicon glyphicon-send"></span>
                                            Envio <i>$<%#Eval("CostoEnvio") %></i>
                                                </asp:LinkButton>
                                            </div>
                                            <div class="form-group">
                                                <label>Total</label>
                                            </div>
                                            <div class="form-group">
                                                <label>$<%#Eval("Total") %></label>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                            </div>

                        </ItemTemplate>
                    </asp:DataList>
                </div>



            </asp:Panel>

        </div>
        <div class="col-md-6 col-xs-12" style="align-content: center">
            <div class="panel">
                <div class="panel-heading text-center" style="background-color: purple; color: white">
                    <h3>Detalles del carrito</h3>
                </div>
                <div class="panel-body text-center" style="background-color: #f1f1f1;padding:30px">
                    <%--Elementos de los datos generales de la orden--%>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td class="text-right">
                                        <h4>
                                            <label>Total de articulos: </label></h4>
                                    </td>
                                    <td class="text-left" style="padding-left:10px;">
                                        <h4>
                                            <asp:Label ID="lblCantidadProductos" runat="server" />
                                        </h4>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-right">
                                        <h4>
                                            <label>Subtotal: </label></h4>
                                    </td>
                                    <td class="text-left" style="padding-left:10px;">
                                        <h4> $<asp:Label ID="lblTotalDeProductos" runat="server" />
                                        </h4>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-right">
                                        <h4>
                                            <label>Total de envio:</label>
                                        </h4>
                                    </td>
                                    <td class="text-left" style="padding-left:10px;">
                                        <h4> $<asp:Label ID="lblCostoDeEnvio" runat="server" />
                                        </h4>
                                    </td>
                                </tr>
                            </table>
                            <div class="form-horizontal">
                                <div class="form-group text-center">
                                    <asp:LinkButton CssClass="btn btn-sm btn-success" Font-Size="20" ID="btnPagar" OnClick="btnPagar_Click" runat="server">
                                        <span class="glyphicon glyphicon-credit-card"></span> Total a pagar $<asp:Label ID="lblTotal" runat="server" />
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
