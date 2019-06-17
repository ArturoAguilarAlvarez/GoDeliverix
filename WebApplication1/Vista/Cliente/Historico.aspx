<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Cliente/MasterCliente.Master" AutoEventWireup="true" CodeBehind="Historico.aspx.cs" Inherits="WebApplication1.Vista.Cliente.Historico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <%-- Filtros de busqueda --%>
    <div class="col-md-12">
        <div class="col-md-offset-1 col-md-3">
            <label>Fecha inicial</label>
            <asp:TextBox ID="txtFechaInicial" TextMode="date" CssClass="form-control" runat="server" />
        </div>
        <div class="col-md-3">
            <label>Fecha final</label>
            <asp:TextBox ID="txtFechaFinal" TextMode="date" CssClass="form-control" runat="server" />
        </div>
        <div class="col-md-3">
            <label>Numero de orden</label>
            <asp:TextBox ID="txtNumeroDeOrden" CssClass="form-control" runat="server" />
        </div>
        <div class="col-md-2" style="margin-top: 25px">
            <asp:LinkButton ID="btnBuscarOrden" OnClick="btnBuscarOrden_Click" CssClass="btn btn-sm btn-default" ToolTip="buscar" runat="server">
                <span class="glyphicon glyphicon-search"></span>
            </asp:LinkButton>
            <asp:LinkButton ID="btnLimpiarFiltros" OnClick="btnLimpiarFiltros_Click" CssClass="btn btn-sm btn-default" ToolTip="Limpiar filtros" runat="server">
                <span class="glyphicon glyphicon-trash"></span>
            </asp:LinkButton>
        </div>
        <%-- Mensaje al usuario --%>
        <div class="col-md-offset-1 col-md-6">
            <asp:Label ID="lblMensaje" Visible="false" ForeColor="Red" runat="server" />
        </div>
    </div>

    <%-- Panel de detalledeordenes --%>
    <asp:Panel CssClass="well well-lg col-md-offset-2 col-md-8" ID="PanelDetalles" ToolTip="Cerrar" Style="z-index: 1; position: absolute; -webkit-box-shadow: 10px 10px 61px -3px rgba(0,0,0,0.75); -moz-box-shadow: 10px 10px 61px -3px rgba(0,0,0,0.75); box-shadow: 10px 10px 61px -3px rgba(0,0,0,0.75);" runat="server">
     <div class="row" style ="margin-bottom:5px;">
         <div class="pull-left" style="margin-left:20px;">
             <p>
                 <label>Numero de orden:</label>
                 <asp:Label ID="lblOrden" runat="server" />
             
                 <label>Fecha de creacion:</label>
                 <asp:Label ID="lblFecha" runat="server" />
             
                 <label>Total:</label>
                 <asp:Label ID="lblTotal" runat="server" />
             </p>
             
         </div>
         <div class="pull-right">
            <asp:LinkButton CssClass="btn btn-sm btn-danger" ID="btnMuestraDetalles" OnClick="btnMuestraDetalles_Click" CommandName="Detalles" runat="server">
                <asp:Label ID="lblDetalles" CssClass="glyphicon glyphicon-remove-circle" runat="server" />
            </asp:LinkButton>
        </div>
     </div>
        
        <asp:GridView ID="DgvDetalles" Style="margin-top: 15px"   AutoGenerateColumns="false" OnRowDataBound="DgvDetalles_RowDataBound" OnRowCommand="DgvDetalles_RowCommand" DataKeyNames="UidRelacionOrdenSucursal" CssClass="table table-bordered table-hover table-condensed table-striped input-sm" runat="server">
            <Columns>
                <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide">
                    <FooterStyle CssClass="hide" />
                    <HeaderStyle CssClass="hide" />
                    <ItemStyle CssClass="hide" />
                </asp:ButtonField>
                <asp:BoundField DataField="LNGFolio"  HeaderText="Orden" ItemStyle-VerticalAlign="Middle"  />
                <asp:BoundField DataField="Identificador"  HeaderText="Sucursal"  ItemStyle-VerticalAlign="Middle" />
                <asp:BoundField DataField="MTotalSucursal"  HeaderText="SubTotal" ItemStyle-VerticalAlign="Middle"  />
                
                <asp:BoundField DataField="CostoEnvio"  HeaderText="Envio" ItemStyle-VerticalAlign="Middle"  />
                <asp:BoundField DataField="MTotal"  HeaderText="Total" ItemStyle-VerticalAlign="Middle"  />
                <asp:BoundField DataField="LngCodigoDeEntrega"  HeaderText="Codigo de entrega" ItemStyle-VerticalAlign="Middle"  />
                <asp:TemplateField  HeaderText="Estatus" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:LinkButton CssClass="btn btn-sm btn-success" ToolTip="Ver detalles" ID="btnDetallesOrden" CommandName="Detalles" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" runat="server">
                            <asp:Label ID="lblEstatus" runat="server" />
                        </asp:LinkButton>
                        <div style="margin-top: 15px;">
                            <asp:Panel ID="PanelDetallesEstatus" CssClass="panel" Style="-webkit-box-shadow: 10px 10px 61px 5px rgba(0,0,0,0.75); -moz-box-shadow: 10px 10px 61px 5px rgba(0,0,0,0.75); box-shadow: 10px 10px 61px 5px rgba(0,0,0,0.75); z-index: 1; position: absolute;" runat="server">
                                <div class="pull-right">
                                    <asp:LinkButton ID="btnCerrarPanelEstatus"  CommandName="CierraPanelEstatus"  CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>"  ToolTip="Cerrar" CssClass="btn btn-sm btn-danger" runat="server">
                                      <span class="glyphicon glyphicon-remove"></span>
                                      Cerrar
                                    </asp:LinkButton>
                                </div>
                                <div style="margin-top: 5px;">
                                    <asp:GridView ID="DGVEstatusOrden" AutoGenerateColumns="false" CssClass="table table-bordered table-hover table-condensed table-striped input-sm" runat="server">
                                        <EmptyDataTemplate>
                                            <asp:Label Text="No hay estatus" CssClass="info" runat="server" />
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide">
                                                <FooterStyle CssClass="hide" />
                                                <HeaderStyle CssClass="hide" />
                                                <ItemStyle CssClass="hide" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="DtmFecha" ItemStyle-CssClass="text-center" HeaderText="Hora" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="VchNombre" ItemStyle-CssClass="text-center" HeaderText="Descripcion" HeaderStyle-CssClass="text-center" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton CssClass="bt btn-sm btn-info" runat="server" >
                                                        <span class="glyphicon glyphicon-info-sign"></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="text-center" HeaderText="Productos" HeaderStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:LinkButton ID="BtnDetallesProductos" ToolTip="Detalles en orden" CommandName="Productos" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" CssClass="btn btn-sm btn-default" runat="server">
                            <img src="../Img/Iconos/MenuCliente.png" height="20" />
                        </asp:LinkButton>
                        <div style="margin-top: 15px;">
                            <asp:Panel ID="PanelProductos" CssClass="col-md-4 panel" Style="-webkit-box-shadow: 10px 10px 61px 5px rgba(0,0,0,0.44); -moz-box-shadow: 10px 10px 61px 5px rgba(0,0,0,0.44); box-shadow: 10px 10px 61px 5px rgba(0,0,0,0.44); z-index: 1; position: absolute;"
                                runat="server">
                                <div class="pull-right">
                                    <asp:LinkButton ID="btnCerrarPanelProductos" ToolTip="Cerrar" CommandName="CierraPanelProductos" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" CssClass="btn btn-sm btn-danger" runat="server">
                                      <span class="glyphicon glyphicon-remove"></span>
                                      Cerrar
                                    </asp:LinkButton>
                                </div>
                                <div style="margin-top: 15px;">

                                    <asp:GridView ID="DGVProductosEnOrden" AutoGenerateColumns="false" CssClass="table table-bordered table-hover table-condensed table-striped input-sm" runat="server">
                                        <EmptyDataTemplate>
                                            <asp:Label Text="No hay productos" CssClass="info" runat="server" />
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide">
                                                <FooterStyle CssClass="hide" />
                                                <HeaderStyle CssClass="hide" />
                                                <ItemStyle CssClass="hide" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="StrNombreProducto" ItemStyle-CssClass="text-center" HeaderText="Producto" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="intCantidad" ItemStyle-CssClass="text-center" HeaderText="Cantidad" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="MTotal" ItemStyle-CssClass="text-center" HeaderText="Total" HeaderStyle-CssClass="text-center" />
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </asp:Panel>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>


    <div class="col-md-offset-1 col-md-10" style="margin-top: 30px;">
        <asp:GridView AutoGenerateColumns="false" DataKeyNames="Uidorden" ID="DgvBitacoraOrdenes" OnRowCommand="DgvBitacoraOrdenes_RowCommand" OnRowDataBound="DgvBitacoraOrdenes_RowDataBound" OnSelectedIndexChanged="DgvBitacoraOrdenes_SelectedIndexChanged" runat="server" CssClass="table table-bordered table-hover table-condensed table-striped input-sm">
            <EmptyDataRowStyle CssClass="info" />
            <EmptyDataTemplate>
                <asp:Label Text="No hay ordenes" runat="server" />
            </EmptyDataTemplate>
            <SelectedRowStyle CssClass="info" />
            <Columns>
                <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide">
                    <FooterStyle CssClass="hide" />
                    <HeaderStyle CssClass="hide" />
                    <ItemStyle CssClass="hide" />
                </asp:ButtonField>
                <asp:BoundField DataField="LNGFolio" ItemStyle-CssClass="text-center" HeaderText="Orden" HeaderStyle-CssClass="text-center" />
                <asp:BoundField DataField="FechaDeOrden" ItemStyle-CssClass="text-center" HeaderText="Fecha" HeaderStyle-CssClass="text-center" />
                <asp:BoundField DataField="MTotal" HeaderText="Total" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />

            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
