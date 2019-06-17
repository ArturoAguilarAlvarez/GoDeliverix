<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/MasterDeliverix.Master" AutoEventWireup="true" CodeBehind="ZonaHoraria.aspx.cs" Inherits="WebApplication1.Vista.ZonaHoraria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="favicon" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12">
        <div class="panel panel-primary">
            <div class="panel-heading text-center">
                Seleccion de zona horaria por pais
            </div>
            <div class="pull-left">
                <ul class="nav nav-tabs">
                    <li role="presentation" runat="server" id="liPanelPaises">
                        <asp:LinkButton ID="btnPanelPaises" OnClick="btnPanelPaises_Click" runat="server">
                            Paises
                        </asp:LinkButton>
                    </li>
                    <li role="presentation" runat="server" id="liPanelEstados">
                        <asp:LinkButton ID="btnPanelEstados" OnClick="btnPanelEstados_Click" runat="server">
                            Estados
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
            <div class="clearfix"></div>
            <asp:Panel ID="PanelPais" runat="server">
                <div class="pull-left" style="padding: 10px;">
                    <asp:LinkButton CssClass="btn btn-sm btn-default" ID="btnEditarPais" OnClick="btnEditar_Click" runat="server">
                    <span class="glyphicon glyphicon-cog"></span>
                    Editar
                    </asp:LinkButton>
                    <asp:LinkButton CssClass="btn btn-sm btn-success" ID="btnAceptarPais" OnClick="btnAceptarPais_Click" runat="server">
                    <span class="glyphicon glyphicon-ok"></span>
                    
                    </asp:LinkButton>
                    <asp:LinkButton CssClass="btn btn-sm btn-danger" ID="btnCancelarPais" OnClick="btnCancelarPais_Click" runat="server">
                    <span class="glyphicon glyphicon-remove"></span>
                    </asp:LinkButton>
                </div>
                <div class="clearfix"></div>
                <div class="panel-body">
                    <div class="col-md-6">
                        <div class="container">
                            <label>Pais</label>
                            <asp:DropDownList ID="ddlPais" Width="200" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPais_SelectedIndexChanged" runat="server">
                            </asp:DropDownList>
                            <label>Ordenar</label>
                            <asp:DropDownList CssClass="form-control" Width="200" ID="ddlOrdenZonasPais" AutoPostBack="true" OnSelectedIndexChanged="ddlOrdenZonasPais_SelectedIndexChanged" runat="server">
                                <asp:ListItem Value="0" Text="Todos" />
                                <asp:ListItem Value="1" Text="Seleccionados" />
                                <asp:ListItem Value="2" Text="Deseleccionados" />
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">

                        <label>Zonas horarias existentes</label>
                        <div class="input-group">
                            <asp:TextBox CssClass="form-control" ID="txtBuscarZonaPais" runat="server" />
                            <asp:LinkButton CssClass="input-group-addon" ID="btnBuscarPais" OnClick="btnBuscarPais_Click" runat="server">
                                <span class="glyphicon glyphicon-search "></span>
                            </asp:LinkButton>
                        </div>
                        <div style="overflow-y: auto; max-height: 200px; padding: 5px; margin-top: 10px;">
                            <asp:CheckBoxList ID="chkbxlZonaHoraria" runat="server">
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="PanelEstados" runat="server">
                <div class="pull-left" style="padding: 10px;">
                    <asp:LinkButton CssClass="btn btn-sm btn-default" ID="btnEditarEstados" OnClick="btnEditarEstados_Click" runat="server">
                        <span class="glyphicon glyphicon-cog"></span>
                        Editar
                    </asp:LinkButton>
                    <asp:LinkButton CssClass="btn btn-sm btn-success" ID="btnAceptarEstados" OnClick="btnAceptarEstados_Click" runat="server">
                        <span class="glyphicon glyphicon-ok"></span>
                    </asp:LinkButton>
                    <asp:LinkButton CssClass="btn btn-sm btn-danger" ID="btnCancelarEstados" OnClick="btnCancelarEstados_Click" runat="server">
                        <span class="glyphicon glyphicon-remove"></span>
                    </asp:LinkButton>
                </div>
                <div class="clearfix"></div>
                <div class="panel-body">
                    <div class="col-md-3">
                        <label>Pais</label>
                        <asp:DropDownList ID="ddlEstadoPais" AutoPostBack="true" OnSelectedIndexChanged="ddlEstadoPais_SelectedIndexChanged" CssClass="form-control" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-5">
                        <label>Zonas horarias  disponibles</label>

                        <asp:RadioButtonList ID="RBLZonasHorarias" AutoPostBack="true" OnSelectedIndexChanged="RBLZonasHorarias_SelectedIndexChanged" runat="server" />

                    </div>
                    <div class="col-md-4">
                        <label>Estados</label>
                        <div class="input-group">
                            <asp:TextBox CssClass="form-control" ID="txtBuscarEstado" runat="server" />
                            <asp:LinkButton CssClass="input-group-addon" ID="btnBuscarEstado" OnClick="btnBuscarEstado_Click" runat="server">
                                <span class="glyphicon glyphicon-search "></span>
                            </asp:LinkButton>
                        </div>
                        <div style="overflow-y: auto; max-height: 200px; padding: 5px; margin-top: 10px;">
                            <asp:CheckBoxList ID="chkbxlEstados" AutoPostBack="true" runat="server">
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>

    </div>
</asp:Content>
