<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/MasterDeliverix.Master" AutoEventWireup="true" CodeBehind="CodigoForm.aspx.cs" Inherits="WebApplication1.Vista.CodigoForm" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-7">
                <%--ROW--%>
                <div class="row">
                    <%--DATOS GENERALES--%>
                    <div class="col-md-12">
                        <div class="panel">
                            <div class="panel-body">
                                <%--TITLE--%>
                                <h4 class="h4">Datos generales</h4>
                                <%--TITLE--%>

                                <%--ROW--%>
                                <div class="row">
                                    <div class="col-md-3">
                                        <h6 class="text-muted  mb-2">Código</h6>
                                        <asp:TextBox runat="server" ID="txtCode" CssClass="form-control form-group-sm" MaxLength="16"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3">
                                        <h6 class="text-muted  mb-2">Recompensa</h6>
                                        <asp:DropDownList runat="server" ID="ddlRewardType" CssClass="form-control form-group-sm" OnSelectedIndexChanged="ddlRewardType_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Text="Ninguno" Value="0" />
                                            <asp:ListItem Text="Monto a monedero" Value="1" />
                                            <asp:ListItem Text="Envío gratis" Value="2" />
                                            <asp:ListItem Text="Descuento (Orden)" Value="3" />
                                            <asp:ListItem Text="Descuento en envío (Orden)" Value="4" />
                                            <asp:ListItem Text="Reembolso (Orden)" Value="5" />
                                            <asp:ListItem Text="Reembolso del envío (Orden)" Value="6" />
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-3">
                                        <h6 class="text-muted  mb-2">Tipo de valor</h6>
                                        <asp:DropDownList runat="server" ID="ddlValueType" AutoPostBack="true" OnSelectedIndexChanged="ddlValueType_SelectedIndexChanged" CssClass="form-control form-group-sm">
                                            <asp:ListItem Text="Monto" Value="0" />
                                            <asp:ListItem Text="Porcentage" Value="1" />
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-3">
                                        <h6 class="text-muted mb-2">Valor</h6>
                                        <div class="input-group">
                                            <asp:TextBox runat="server" ID="txtValue" CssClass="form-control" MaxLength="16"></asp:TextBox>
                                            <asp:Label Text="$" runat="server" ID="lblValueType" CssClass="input-group-addon" />
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <h6 class="text-muted mb-2">Aplica:</h6>
                                        <asp:DropDownList runat="server" ID="DropDownList1" CssClass="form-control form-group-sm">
                                            <asp:ListItem Text="Durante la compra" Value="0" />
                                            <asp:ListItem Text="Al finalizar la compra" Value="1" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--ROW--%>

                                <%--ROW--%>
                                <div class="row">
                                    <div class="col-md-3">
                                        <h6 class="text-muted mb-2">Inicio</h6>
                                        <asp:TextBox runat="server" ID="txtStartAt" TextMode="Date" CssClass="form-control form-group-sm" />
                                    </div>

                                    <div class="col-md-3">
                                        <h6 class="text-muted mb-2">Tipo de expiración</h6>
                                        <asp:DropDownList runat="server" ID="ddlCodeExpirationType" AutoPostBack="true" OnSelectedIndexChanged="ddlCodeExpirationType_SelectedIndexChanged" CssClass="form-control form-group-sm">
                                            <asp:ListItem Text="None" Value="0" />
                                            <asp:ListItem Text="Fecha" Value="1" />
                                            <asp:ListItem Text="Activaciones" Value="2" />
                                            <asp:ListItem Text="Dias despues de canjear" Value="3" />
                                        </asp:DropDownList>
                                    </div>

                                    <asp:Panel runat="server" ID="pnlExpirationDate" class="col-md-3">
                                        <h6 class="text-muted mb-2">Fecha de expiración</h6>
                                        <asp:TextBox runat="server" TextMode="Date" CssClass="form-control form-group-sm" />
                                    </asp:Panel>

                                    <asp:Panel runat="server" ID="pnlActivationNumber" class="col-md-4">
                                        <h6 class="text-muted mb-2">Numero de activaciones</h6>
                                        <asp:TextBox runat="server" CssClass="form-control form-group-sm" />
                                    </asp:Panel>

                                    <asp:Panel runat="server" ID="pnlDaysBeforeActivation" class="col-md-2">
                                        <h6 class="text-muted mb-2">Dias</h6>
                                        <asp:TextBox runat="server" CssClass="form-control form-group-sm" />
                                    </asp:Panel>
                                </div>
                                <%--ROW--%>
                            </div>
                        </div>
                    </div>
                    <%--DATOS GENERALES--%>
                </div>
                <%--ROW--%>

                <%--ROW--%>
                <div class="row">

                    <%--DISTRUIBUIDORA--%>
                    <div class="col-md-6">
                        <asp:Panel runat="server" CssClass="panel">
                            <div class="panel-body">
                                <h4 class="h4">Distribuidora</h4>

                                <%--ROW--%>
                                <div class="row">
                                    <%--EMPRESA--%>
                                    <div class="col-md-12">
                                        <h6 class="text-muted mb-2">Empresa</h6>
                                        <asp:DropDownList runat="server" CssClass="form-control form-group-sm">
                                            <asp:ListItem Text="Seleccione Pais" />
                                        </asp:DropDownList>
                                    </div>
                                    <%--EMPRESA--%>


                                    <%--SUCURSAL--%>
                                    <div class="col-md-12">
                                        <h6 class="text-muted mb-2">Sucursal</h6>
                                        <asp:DropDownList runat="server" CssClass="form-control form-group-sm">
                                            <asp:ListItem Text="Seleccione Pais" />
                                        </asp:DropDownList>
                                    </div>
                                    <%--SUCURSAL--%>
                                </div>
                                <%--ROW--%>
                            </div>
                        </asp:Panel>
                    </div>
                    <%--DISTRUIBUIDORA--%>

                    <%--SUMINISTRADORA--%>
                    <div class="col-md-6">
                        <asp:Panel runat="server" CssClass="panel">
                            <div class="panel-body">
                                <h4 class="h4">Suministradora</h4>

                                <%--ROW--%>
                                <div class="row">
                                    <%--EMPRESA--%>
                                    <div class="col-md-12">
                                        <h6 class="text-muted mb-2">Empresa</h6>
                                        <asp:DropDownList runat="server" CssClass="form-control form-group-sm">
                                            <asp:ListItem Text="Seleccione Pais" />
                                        </asp:DropDownList>
                                    </div>
                                    <%--EMPRESA--%>


                                    <%--SUCURSAL--%>
                                    <div class="col-md-12">
                                        <h6 class="text-muted  mb-2">Sucursal</h6>
                                        <asp:DropDownList runat="server" CssClass="form-control form-group-sm">
                                            <asp:ListItem Text="Seleccione Pais" />
                                        </asp:DropDownList>
                                    </div>
                                    <%--SUCURSAL--%>
                                </div>
                                <%--ROW--%>
                            </div>
                        </asp:Panel>
                    </div>
                    <%--SUMINISTRADORA--%>
                </div>
                <%--ROW--%>


                <%--ROW--%>
                <div class="row">
                    <%--REGION--%>
                    <div class="col-md-12">
                        <asp:Panel runat="server" CssClass="panel">
                            <div class="panel-body">
                                <h4 class="h4">Regiones (Ubicación del cliente)</h4>

                                <%--ROW--%>
                                <div class="row">
                                    <%--PAIS--%>
                                    <div class="col-md-3">
                                        <h6 class="text-muted text-bold">Pais</h6>
                                        <asp:DropDownList runat="server" CssClass="form-control form-group-sm">
                                            <asp:ListItem Text="Seleccione Pais" />
                                        </asp:DropDownList>
                                    </div>
                                    <%--PAIS--%>

                                    <%--ESTADO--%>
                                    <div class="col-md-3">
                                        <h6 class="text-muted text-bold">Estado</h6>
                                        <asp:DropDownList runat="server" CssClass="form-control form-group-sm">
                                            <asp:ListItem Text="Seleccione Estado" />
                                        </asp:DropDownList>
                                    </div>
                                    <%--ESTADO--%>

                                    <%--MUNICIPIO--%>
                                    <div class="col-md-3">
                                        <h6 class="text-muted">Municipio</h6>
                                        <asp:DropDownList runat="server" CssClass="form-control form-group-sm">
                                            <asp:ListItem Text="Seleccione Municipio" />
                                        </asp:DropDownList>
                                    </div>
                                    <%--MUNICIPIO--%>

                                    <%--CIUDAD--%>
                                    <div class="col-md-3">
                                        <h6 class="text-muted text-bold">Ciudad</h6>
                                        <asp:DropDownList runat="server" CssClass="form-control form-group-sm">
                                            <asp:ListItem Text="Seleccione Ciudad" />
                                        </asp:DropDownList>
                                    </div>
                                    <%--CIUDAD--%>
                                </div>
                                <%--ROW--%>
                            </div>
                        </asp:Panel>
                    </div>
                    <%--REGION--%>
                </div>
                <%--ROW--%>
            </div>

            <div class="col-md-5">

                <%--ROW--%>
                <div class="row">
                    <%--REGLAS--%>
                    <div class="col-md-12">
                        <asp:Panel runat="server" class="panel">
                            <div class="panel-body">
                                <div style="flex-direction: row; box-sizing: border-box; display: flex; place-content: center space-between; align-items: center;">
                                    <h4 class="h4">Reglas</h4>

                                    <div>
                                        <asp:Button Text="Nuevo" runat="server" ID="btnCodeRuleNew" OnClick="btnCodeRuleNew_Click" CssClass="btn btn-sm btn-default" />
                                        <asp:Button Text="Editar" runat="server" ID="btnCodeRuleEdit" OnClick="btnCodeRuleEdit_Click" Visible="false" CssClass="btn btn-sm btn-info" />
                                        <asp:Button Text="Agregar" runat="server" ID="btnCodeRuleSave" OnClick="btnCodeRuleSave_Click" Visible="false" CssClass="btn btn-sm btn-success" />
                                        <asp:Button Text="Cancelar" runat="server" ID="btnCodeRuleCancel" OnClick="btnCodeRuleCancel_Click" Visible="false" CssClass="btn btn-sm btn-danger" />
                                    </div>
                                </div>


                                <asp:Label Text="" runat="server" ID="lblCodeRuleError" CssClass="text-danger" />


                                <%--ROW--%>
                                <div class="row">
                                    <div class="col-md-4">
                                        <h6 class="text-muted mb-2">Donde</h6>
                                        <asp:DropDownList runat="server" ID="ddlCodeRuleValueType" AutoPostBack="true" OnSelectedIndexChanged="ddlCodeRuleValueType_SelectedIndexChanged" CssClass="form-control form-group-sm">
                                            <asp:ListItem Text="Subtotal (Orden)" Value="0" />
                                            <asp:ListItem Text="Envio (Orden)" Value="1" />
                                            <asp:ListItem Text="Subtotal (Compra)" Value="2" />
                                            <asp:ListItem Text="Envio (Compra)" Value="3" />
                                            <asp:ListItem Text="Producto" Value="4" />
                                            <asp:ListItem Text="Cantidad de Producto" Value="5" />
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-4">
                                        <h6 class="text-muted mb-2">Es</h6>
                                        <asp:DropDownList runat="server" ID="ddlCodeRuleOperator" CssClass="form-control form-group-sm">
                                            <asp:ListItem Text="Igual" Value="0" />
                                            <asp:ListItem Text="Menor" Value="1" />
                                            <asp:ListItem Text="Mayor" Value="2" />
                                            <asp:ListItem Text="Menor o igual" Value="3" />
                                            <asp:ListItem Text="Mayor o igual" Value="4" />
                                            <asp:ListItem Text="Diferente" Value="5" />
                                        </asp:DropDownList>
                                    </div>


                                    <div class="col-md-4">
                                        <h6 class="text-muted mb-2">A</h6>
                                        <asp:TextBox runat="server" ID="txtCodeRuleValue" CssClass="form-control form-group-sm" />
                                        <asp:DropDownList runat="server" ID="ddlCodeRuleValue" Visible="false" CssClass="form-control form-group-sm">
                                            <asp:ListItem Text="Tacos" Value="0" />
                                            <asp:ListItem Text="Tortas" Value="0" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--ROW--%>

                                <br />

                                <%--ROW--%>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvCodeRules" AutoGenerateColumns="false" CssClass="table table-responsive table-bordered">
                                            <EmptyDataTemplate>
                                                <div class="panel panel-info">No hay reglas registradas </div>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField HeaderText="Donde" DataField="ValueTypeText" />
                                                <asp:BoundField HeaderText="Sea" DataField="OperatorText" />
                                                <asp:BoundField HeaderText="A" DataField="ValueView" />
                                                <asp:TemplateField HeaderText="Union" ItemStyle-Width="124">
                                                    <ItemTemplate>
                                                        <asp:DropDownList runat="server" CssClass="form-control form-group-sm">
                                                            <asp:ListItem Text="Ninguno" />
                                                            <asp:ListItem Text="Y" />
                                                            <asp:ListItem Text="O" />
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <%--ROW--%>
                            </div>
                        </asp:Panel>
                    </div>
                    <%--REGLAS--%>
                </div>
                <%--ROW--%>
            </div>
        </div>
    </div>
</asp:Content>
