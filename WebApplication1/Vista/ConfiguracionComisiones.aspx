<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/MasterDeliverix.Master" AutoEventWireup="true" CodeBehind="ConfiguracionComisiones.aspx.cs" Inherits="WebApplication1.Vista.ConfiguracionComisiones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Comisiones
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="favicon" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-offset-2 col-md-8 col-md-offset-2">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="panel panel-primary ">
                    <div class="panel-heading text-center">
                        <asp:Label ID="lblTituloPanel" runat="server" />
                    </div>
                    <div class="panel-body">
                        <asp:Panel ID="PnlMensaje" BackColor="Red" Height="30" runat="server">
                            <asp:Label ID="LblMensaje" CssClass="text-left" ForeColor="White" runat="server" />
                            <div class="pull-right">
                                <asp:LinkButton runat="server" ID="btnCerrarMensaje" OnClick="btnCerrarMensaje_OnClick" CssClass="btn btn-sm btn-danger ">
                                <asp:label CssClass=" glyphicon glyphicon-remove" runat="server" /></asp:LinkButton>
                            </div>
                        </asp:Panel>
                        <%-- Barra de navegacion --%>
                        <ul class="nav nav-tabs NavLarge" style="margin-bottom: 5px;">
                            <li role="presentation" id="liComisionDelSistema" runat="server">
                                <asp:LinkButton runat="server" ID="btnComisiones" OnClick="PanelComisiones">
                        <span class="glyphicon glyphicon-globe"></span> 
                        Comisiones del sistema
                                </asp:LinkButton>
                            </li>
                            <li role="presentation" id="liConfiguracionPasarela" runat="server">
                                <asp:LinkButton runat="server" ID="btnPasarelas" OnClick="PanelPasarela">
                        <span class="glyphicon glyphicon-globe"></span> 
                        Pasarelas de cobro
                                </asp:LinkButton>
                            </li>
                        </ul>
                        <asp:Panel ID="PnlDatosComision" runat="server" Style="margin: 20px">
                            <asp:LinkButton runat="server" ID="BtnEditar" OnClick="BtnEditar_OnClick" CssClass="btn btn-sm btn-default ">
                        Editar
                        <i class="glyphicon glyphicon-cog"></i>
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="btnGuardar" OnClick="btnGuardar_OnClick" CssClass="btn btn-sm btn-success ">
                        <i class="glyphicon glyphicon-ok"></i>
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="btnCancelar" OnClick="btnCancelar_OnClick" CssClass="btn btn-sm btn-danger ">
                                <asp:label CssClass=" glyphicon glyphicon-remove" runat="server" /></asp:LinkButton>

                            <div class="col-md-12" style="margin: 10px">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Porcentaje de comisión por pedido</label>
                                        <asp:TextBox CssClass="form-control" ID="txtComisionPorOrdenGoDeliverix" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Porcentaje de comisión por envio</label>
                                        <asp:TextBox CssClass="form-control" ID="txtComisionEnvioGoDeliverix" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="PnlDatosProvedoresPasarela" runat="server" Style="margin: 20px">
                            <asp:LinkButton runat="server" ID="btnEditarComisionTarjeta" OnClick="btnEditarComisionTarjeta_OnClick" CssClass="btn btn-sm btn-default ">
                        Editar
                        <i class="glyphicon glyphicon-cog"></i>
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="btnGuardarComisionTarjeta" OnClick="btnGuardarComisionTarjeta_OnClick" CssClass="btn btn-sm btn-success ">
                        <i class="glyphicon glyphicon-ok"></i>
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="btnCancelarComisionTarjeta" OnClick="btnCancelarComisionTarjeta_OnClick" CssClass="btn btn-sm btn-danger ">
                                <asp:label CssClass=" glyphicon glyphicon-remove" runat="server" /></asp:LinkButton>
                            <div class="form-group">
                                <label>Comision con tarjeta</label>
                                <asp:TextBox CssClass="form-control" ID="txtComisionTarjeta" runat="server" />
                                <asp:Label ID="lblUidComisionTarjeta" Visible="false" runat="server" />
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
