<%@ Page Title="Vehiculos" Language="C#" MasterPageFile="~/Vista/MasterDeliverix.Master" AutoEventWireup="true" CodeBehind="Vehiculos.aspx.cs" Inherits="WebApplication1.Vista.Vehiculos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="favicon" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12">
        <%--Panel de busquedas --%>
        <asp:Panel runat="server" CssClass="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Busqueda de vehiculos
                </div>
                <div class="pull-right">
                    <asp:LinkButton runat="server" ID="BtnBAOcultar" OnClick="BtnBAOcultar_Click" CssClass="btn btn-sm btn-default">
                        <asp:Label runat="server" ID="IconoBotonMostrar" CssClass="glyphicon glyphicon-eye-open"></asp:Label>
                        <asp:Label ID="lblBAFiltrosVisibilidad" Text=" Mostrar" runat="server" />
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" ID="BtnBALimpiar" OnClick="BtnBALimpiar_Click" CssClass="btn btn-sm btn-default"><span class="glyphicon glyphicon-trash"></span> Limpiar</asp:LinkButton>
                    <asp:LinkButton runat="server" ID="BtnBABuscar" OnClick="BtnBABuscar_Click" CssClass="btn btn-sm btn-default"><span class="glyphicon glyphicon-search"></span> Buscar</asp:LinkButton>
                </div>
                <div class="clearfix"></div>
                <div class="panel-body">
                    <asp:Panel runat="server" ID="PanelFiltro">
                        <div class="row">
                            <div class="col-md-4">
                                <label>No. de serie</label>
                                <asp:TextBox CssClass="form-control" MaxLength="50" ID="txtFNo_serie" runat="server" />
                            </div>
                            <div class="col-md-4">
                                <label>Cilindrada del motor</label>
                                <asp:TextBox CssClass="form-control" MaxLength="4" ID="txtFCilindrada" runat="server" />
                            </div>
                            <div class="col-md-4">
                                <label>Tipo de vehiculo</label>
                                <asp:DropDownList CssClass="form-control" ID="DDLFTipoDeVehiculo" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <label>Año</label>
                                <asp:TextBox CssClass="form-control" MaxLength="4" ID="txtFAnio" runat="server" />
                            </div>
                            <div class="col-md-4">
                                <label>Marca</label>
                                <asp:TextBox CssClass="form-control" MaxLength="30" ID="txtFMarca" runat="server" />
                            </div>
                            <div class="col-md-4">
                                <label>Modelo</label>
                                <asp:TextBox CssClass="form-control" MaxLength="10" ID="txtFModelo" runat="server" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <label>Placa</label>
                                <asp:TextBox CssClass="form-control" MaxLength="20" ID="txtFPlaca" runat="server" />
                            </div>
                            <div class="col-md-4">
                                <label>Color</label>
                                <asp:TextBox CssClass="form-control" MaxLength="30" ID="txtFColor" runat="server" />
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:GridView runat="server" ID="dgvVehiculos" Style="margin-top: 10px" OnRowDataBound="dgvVehiculos_RowDataBound" OnSelectedIndexChanged="dgvVehiculos_SelectedIndexChanged" AutoGenerateColumns="false" DataKeyNames="UID" CssClass="table table-bordered table-hover table-condensed table-striped input-sm">
                        <EmptyDataTemplate>
                            Sin resultados en la busqueda
                        </EmptyDataTemplate>
                        <SelectedRowStyle CssClass="table table-hover input-sm success" />
                        <SortedAscendingHeaderStyle CssClass="glyphicon glyphicon-sort-by-alphabet" />
                        <Columns>
                            <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide">
                                <FooterStyle CssClass="hide" />
                                <HeaderStyle CssClass="hide" />
                                <ItemStyle CssClass="hide" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="LngNumeroDeSerie" HeaderText="NO. SERIE" HeaderStyle-CssClass="text-center" SortExpression="LngNumeroDeSerie" />
                            <asp:BoundField DataField="IntCilindrada" HeaderText="Cilindrada" HeaderStyle-CssClass="text-center" SortExpression="IntCilindrada" />
                            <asp:BoundField DataField="StrAnio" HeaderText="Año" HeaderStyle-CssClass="text-center" SortExpression="StrAnio" />
                            <asp:BoundField DataField="StrMarca" HeaderText="Marca" HeaderStyle-CssClass="text-center" SortExpression="StrMarca" />
                            <asp:BoundField DataField="StrModelo" HeaderText="Modelo" HeaderStyle-CssClass="text-center" SortExpression="StrModelo" />
                            <asp:BoundField DataField="StrNoDePLaca" HeaderText="Placa" HeaderStyle-CssClass="text-center" SortExpression="StrNoDePLaca" />
                        </Columns>
                    </asp:GridView>

                </div>
            </div>
        </asp:Panel>
        <%-- Panel de gestion --%>
        <asp:Panel runat="server" CssClass="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Gestion de vehiculo
                </div>
                <div class=" pull-left">
                    <asp:TextBox ID="txtUidVehiculo" CssClass="hide" runat="server" />
                    <asp:LinkButton runat="server" ID="btnNuevo" OnClick="btnNuevo_Click" CssClass="btn btn-sm btn-default "><span class="glyphicon glyphicon-file"></span> Nuevo</asp:LinkButton>
                    <asp:LinkButton runat="server" ID="btnEditar" OnClick="btnEditar_Click" CssClass="btn btn-sm btn-default disabled"><span class="glyphicon glyphicon-cog"></span> Editar</asp:LinkButton>
                    <asp:LinkButton runat="server" ID="btnGuardar" OnClick="btnGuardar_Click" CssClass="btn btn-sm btn-success ">
                        <span class="glyphicon glyphicon-ok"></span>
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" ID="btnCancelar" OnClick="btnCancelar_Click" CssClass="btn btn-sm btn-danger "><asp:label CssClass=" glyphicon glyphicon-remove" runat="server" /></asp:LinkButton><asp:Label runat="server" ID="lblEstado"></asp:Label>
                </div>
                <div class="clearfix"></div>
                <div class="panel-body">
                    <ul class="nav nav-tabs">
                        <li role="presentation" id="liDatosGenerales" runat="server">
                            <asp:LinkButton runat="server" ID="btnDatosGenerales" OnClick="btnDatosGenerales_Click" ><span class="glyphicon glyphicon-globe"></span> GENERAL</asp:LinkButton></li>
                        <li role="presentation" id="liDatosDeEmpresa" runat="server">
                            <asp:LinkButton runat="server" OnClick="btnDatosSucursal_Click" ID="btnDatosSucursal"><span class="glyphicon glyphicon-tower"></span> SUCURSAL</asp:LinkButton></li>
                    </ul>
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
                    <%-- Datos generales --%>
                    <asp:Panel ID="panelDatosGenerales" runat="server">
                        <div class="row">
                            <div class="col-md-offset-4 col-md-3">
                                <asp:Image ImageUrl="~/Vista/Img/Categoria/Default.jpg" Width="150" runat="server" />
                                <asp:LinkButton CssClass="btn btn-sm btn-default" ID="btnGImagen" runat="server">
                                <span class="glyphicon glyphicon-open">                                                </span>
                                Cargar Imagen
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <label>No. de serie</label>
                                <asp:TextBox CssClass="form-control" MaxLength="50" ID="txtGNoSerie" runat="server" />
                            </div>
                            <div class="col-md-4">
                                <label>Cilindrada del motor</label>
                                <asp:TextBox CssClass="form-control" MaxLength="4" ID="txtGCilindrada" runat="server" />
                            </div>
                            <div class="col-md-4">
                                <label>Tipo de vehiculo</label>
                                <asp:DropDownList CssClass="form-control" ID="DDLGTipoDeVehiculo" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <label>Año</label>
                                <asp:TextBox CssClass="form-control" MaxLength="4" ID="txtGAnio" runat="server" />
                            </div>
                            <div class="col-md-4">
                                <label>Marca</label>
                                <asp:TextBox CssClass="form-control" MaxLength="30" ID="txtGMarca" runat="server" />
                            </div>
                            <div class="col-md-4">
                                <label>Modelo</label>
                                <asp:TextBox CssClass="form-control" MaxLength="10" ID="txtGModelo" runat="server" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <label>Placa</label>
                                <asp:TextBox CssClass="form-control" MaxLength="20" ID="txtGPlaca" runat="server" />
                            </div>
                            <div class="col-md-4">
                                <label>Color</label>
                                <asp:TextBox CssClass="form-control" MaxLength="30" ID="txtGColor" runat="server" />
                            </div>
                        </div>
                    </asp:Panel>
                    <%-- Datos de la sucursal --%>
                    <asp:Panel runat="server" ID="panelDatosEmpresa">
                        <div class="panel panel-default" style="margin-top: 10px;">
                            <div style="margin-top: 5px;">
                                <div class="pull-left">ASOCIAR A SUCURSAL*</div>
                                <div class="pull-right">
                                    <asp:LinkButton runat="server" ID="btnCambiarEmpresa" OnClick="CambiarEmpresa" CssClass="btn btn-default btn-sm"><span class="glyphicon glyphicon-refresh"></span> Cambiar</asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnBuscarEmpresa" OnClick="BuscaEmpresa" CssClass="btn btn-sm btn-default"><span class="glyphicon glyphicon-search"></span> Buscar</asp:LinkButton>
                                </div>
                            </div>

                            <div class="clearfix"></div>
                            <div class="panel-body">
                                <div class="row">
                                    <asp:TextBox runat="server" CssClass="hidden" ID="txtUidSucursal" />
                                    <div class="col-md-6">
                                        <label>Identificador</label>
                                        <asp:TextBox runat="server" ID="txtdidentificador" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Hora de Apertura</label>
                                        <asp:TextBox runat="server" ID="txtdHoraApertura" TextMode="Time" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label>Hora de Cierre</label>
                                        <asp:TextBox runat="server" ID="txtdHoraDeCierre" TextMode="Time" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <asp:GridView runat="server" ID="DGVBUSQUEDADEEMPRESA" Style="margin-top: 10px;" OnRowDataBound="DGVBUSQUEDADEEMPRESA_DataBound" OnSelectedIndexChanged="DGVBUSQUEDADEEMPRESA_SelectedIndexChanged" AutoGenerateColumns="false" DataKeyNames="ID" CssClass="table table-bordered table-hover table-condensed table-striped input-sm">
                                    <EmptyDataTemplate>No hay coincidencia de busqueda</EmptyDataTemplate>
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
                                </asp:GridView>
                            </div>

                        </div>
                    </asp:Panel>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
