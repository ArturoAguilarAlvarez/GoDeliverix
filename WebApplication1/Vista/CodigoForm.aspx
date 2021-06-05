<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/MasterDeliverix.Master" AutoEventWireup="true" CodeBehind="CodigoForm.aspx.cs" Inherits="WebApplication1.Vista.CodigoForm" %>

<asp:Content runat="server" ID="Styles" ContentPlaceHolderID="Styles">
    <style>
        .w-100 {
            width: 100%
        }

        .pt-20 {
            padding-top: 20px !important;
        }

        .steps-container {
            display: inline-block;
            margin: 0px auto;
        }

        .step-dots {
            padding-left: 0;
            margin-bottom: 0;
            list-style: none;
        }

            .step-dots > li {
                float: left;
                width: 12px;
                height: 12px;
                border-radius: 6px;
                background-color: #d9ffff;
            }

                .step-dots > li > a {
                    border-radius: 4px;
                }

                .step-dots > li + li {
                    margin-left: 6px;
                }

                .step-dots > li.active,
                .step-dots > li.active:hover,
                .step-dots > li.active:focus {
                    color: #fff;
                    background-color: #337ab7;
                }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-12">
                <%--WIZARD--%>
                <asp:Wizard ID="Wizard1" runat="server" CssClass="panel w-100" OnSideBarButtonClick="Wizard1_SideBarButtonClick">

                    <StepPreviousButtonStyle CssClass="btn btn-sm btn-default" />
                    <StartNextButtonStyle CssClass="btn btn-sm btn-primary" />
                    <StepNextButtonStyle CssClass="btn btn-sm btn-primary" />
                    <FinishPreviousButtonStyle CssClass="btn btn-sm btn-default" />
                    <FinishCompleteButtonStyle CssClass="btn btn-sm btn-success" />

                    <NavigationButtonStyle CssClass="col-xs-12" />
                    <SideBarStyle CssClass="col-xs-2 pt-20" />

                    <SideBarTemplate>
                        <asp:ListView runat="server" ID="SideBarList" Enabled="false">
                            <LayoutTemplate>
                                <ul class="step-dots">
                                    <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                                </ul>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li>
                                    <asp:LinkButton runat="server" ID="SideBarButton" />
                                </li>
                            </ItemTemplate>
                            <SelectedItemTemplate>
                                <li class="active">
                                    <asp:LinkButton runat="server" ID="SideBarButton" />
                                </li>
                            </SelectedItemTemplate>
                        </asp:ListView>
                    </SideBarTemplate>

                    <HeaderTemplate>
                        <h3>Código de promoción</h3>
                        <asp:Label Text="Error" runat="server" ID="lblError" CssClass="text-danger" />
                    </HeaderTemplate>

                    <LayoutTemplate>
                        <%--PANEL--%>
                        <div class="panel">
                            <%--BODY--%>
                            <div class="panel-body">
                                <%--ROW--%>
                                <div class="row">
                                    <%--HEADER--%>
                                    <div class="col-sm-12 text-center">
                                        <div class="steps-container">
                                            <asp:PlaceHolder runat="server" ID="sideBarPlaceholder"></asp:PlaceHolder>
                                        </div>
                                    </div>
                                    <%--FILL--%>
                                    <div class="col-sm-12"></div>
                                    <%--FILL--%>

                                    <%--FILL--%>
                                    <div class="col-md-2"></div>
                                    <%--FILL--%>

                                    <%--CONTENT--%>
                                    <div class="col-md-8">
                                        <asp:PlaceHolder runat="server" ID="headerPlaceholder"></asp:PlaceHolder>
                                        <asp:PlaceHolder runat="server" ID="wizardStepPlaceholder"></asp:PlaceHolder>
                                        <br />
                                        <asp:PlaceHolder runat="server" ID="navigationPlaceholder"></asp:PlaceHolder>
                                    </div>
                                    <%--CONTENT--%>

                                    <%--FILL--%>
                                    <div class="col-md-2"></div>
                                    <%--FILL--%>
                                </div>
                                <%--ROW--%>
                            </div>
                            <%--BODY--%>
                        </div>
                        <%--PANEL--%>
                    </LayoutTemplate>

                    <WizardSteps>
                        <asp:WizardStep ID="wsCodeType" runat="server" Title=" ">
                            <h5 class="text-muted">Selección del tipo de codigo de promocion</h5>

                            <div class="row">
                                <div class="col-md-12">
                                    <h6 class="text-muted">Tipo de codigo</h6>
                                    <asp:DropDownList runat="server" ID="ddlCodeType" CssClass="form-control form-group-sm">
                                        <asp:ListItem Text="text1" />
                                        <asp:ListItem Text="text2" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </asp:WizardStep>
                        <asp:WizardStep ID="wsLocation" runat="server" Title=" ">
                            <%--TITLE--%>
                            <h5 class="text-muted">Region (Direccion de entrega)</h5>
                            <%--TITLE--%>

                            <%--ROW--%>
                            <div class="row">
                                <div class="col-md-12">
                                    
                                </div>
                                <div class="col-md-12">
                                    <h6 class="text-muted">Pais</h6>
                                    <asp:DropDownList runat="server" CssClass="form-control form-control-sm">
                                        <asp:ListItem Text="text1" />
                                        <asp:ListItem Text="text2" />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-12">
                                    <h6 class="text-muted">Estado</h6>
                                    <asp:DropDownList runat="server" CssClass="form-control form-control-sm">
                                        <asp:ListItem Text="text1" />
                                        <asp:ListItem Text="text2" />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-12">
                                    <h6 class="text-muted">Municipio</h6>
                                    <asp:DropDownList runat="server" CssClass="form-control form-control-sm">
                                        <asp:ListItem Text="text1" />
                                        <asp:ListItem Text="text2" />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-12">
                                    <h6 class="text-muted">Ciudad</h6>
                                    <asp:DropDownList runat="server" CssClass="form-control form-control-sm">
                                        <asp:ListItem Text="text1" />
                                        <asp:ListItem Text="text2" />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-12">
                                    <h6 class="text-muted">Colonia</h6>
                                    <asp:DropDownList runat="server" CssClass="form-control form-control-sm">
                                        <asp:ListItem Text="text1" />
                                        <asp:ListItem Text="text2" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <%--ROW--%>
                        </asp:WizardStep>
                        <asp:WizardStep ID="wsDeliveryCompany" runat="server" Title=" ">
                            <%--TITLE--%>
                            <h5 class="text-muted">Datos de la distribuidora</h5>
                            <%--TITLE--%>

                            <%--ROW--%>
                            <div class="row">
                                <div class="col-md-12">
                                    <h6 class="text-muted">Distribuidora</h6>
                                    <asp:DropDownList runat="server" CssClass="form-control form-control-sm">
                                        <asp:ListItem Text="text1" />
                                        <asp:ListItem Text="text2" />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-12">
                                    <h6 class="text-muted">Sucursal</h6>
                                    <asp:DropDownList runat="server" CssClass="form-control form-control-sm">
                                        <asp:ListItem Text="text1" />
                                        <asp:ListItem Text="text2" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <%--ROW--%>
                        </asp:WizardStep>
                        <asp:WizardStep ID="wsCompany" runat="server" Title=" ">
                            <%--TITLE--%>
                            <h5 class="text-muted">Datos de la Suministradora</h5>
                            <%--TITLE--%>

                            <%--ROW--%>
                            <div class="row">
                                <div class="col-md-12">
                                    <h6 class="text-muted">Empresa</h6>
                                    <asp:DropDownList runat="server" CssClass="form-control form-control-sm">
                                        <asp:ListItem Text="text1" />
                                        <asp:ListItem Text="text2" />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-12">
                                    <h6 class="text-muted">Sucursal</h6>
                                    <asp:DropDownList runat="server" CssClass="form-control form-control-sm">
                                        <asp:ListItem Text="text1" />
                                        <asp:ListItem Text="text2" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <%--ROW--%>
                        </asp:WizardStep>
                        <asp:WizardStep ID="wsCodeDetails" runat="server" Title=" ">
                            <%--TITLE--%>
                            <h5 class="text-muted">Datos del codigo</h5>
                            <%--TITLE--%>

                            <div class="row">
                                <%--DATOS GENERALES--%>
                                <div class="col-md-12">
                                    <%--ROW--%>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <h6 class="text-muted  mb-2">Código</h6>
                                            <asp:TextBox runat="server" ID="txtCode" CssClass="form-control form-group-sm" MaxLength="16"></asp:TextBox>
                                        </div>

                                        <div class="col-md-3">
                                            <h6 class="text-muted  mb-2">Recompensa</h6>
                                            <asp:DropDownList runat="server" ID="ddlRewardType" CssClass="form-control form-group-sm" AutoPostBack="true">
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
                                            <asp:DropDownList runat="server" ID="ddlValueType" AutoPostBack="true" CssClass="form-control form-group-sm">
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
                                            <asp:DropDownList runat="server" ID="ddlCodeExpirationType" AutoPostBack="true" CssClass="form-control form-group-sm">
                                                <asp:ListItem Text="None" Value="0" />
                                                <asp:ListItem Text="Fecha" Value="1" />
                                                <asp:ListItem Text="Activaciones" Value="2" />
                                                <asp:ListItem Text="Dias despues de canjear" Value="3" />
                                            </asp:DropDownList>
                                        </div>

                                        <asp:Panel runat="server" ID="pnlExpirationDate" Visible="false" class="col-md-3">
                                            <h6 class="text-muted mb-2">Fecha de expiración</h6>
                                            <asp:TextBox runat="server" TextMode="Date" CssClass="form-control form-group-sm" />
                                        </asp:Panel>

                                        <asp:Panel runat="server" ID="pnlActivationNumber" Visible="false" class="col-md-4">
                                            <h6 class="text-muted mb-2">Numero de activaciones</h6>
                                            <asp:TextBox runat="server" CssClass="form-control form-group-sm" />
                                        </asp:Panel>

                                        <asp:Panel runat="server" ID="pnlDaysBeforeActivation" Visible="false" class="col-md-2">
                                            <h6 class="text-muted mb-2">Dias</h6>
                                            <asp:TextBox runat="server" CssClass="form-control form-group-sm" />
                                        </asp:Panel>
                                    </div>
                                    <%--ROW--%>
                                </div>
                                <%--DATOS GENERALES--%>
                            </div>
                        </asp:WizardStep>
                        <asp:WizardStep ID="wsRules" runat="server" Title=" ">
                            <%--TITLE--%>
                            <div style="flex-direction: row; box-sizing: border-box; display: flex; place-content: center space-between; align-items: center;">
                                <h5 class="text-muted">Reglas</h5>

                                <div>
                                    <asp:Button Text="Nuevo" runat="server" ID="btnCodeRuleNew" CssClass="btn btn-sm btn-default" />
                                    <asp:Button Text="Editar" runat="server" ID="btnCodeRuleEdit" Visible="false" CssClass="btn btn-sm btn-info" />
                                    <asp:Button Text="Agregar" runat="server" ID="btnCodeRuleSave" Visible="false" CssClass="btn btn-sm btn-success" />
                                    <asp:Button Text="Cancelar" runat="server" ID="btnCodeRuleCancel" Visible="false" CssClass="btn btn-sm btn-danger" />
                                </div>
                            </div>
                            <%--TITLE--%>

                            <%--ROW--%>
                            <div class="row">
                                <div class="col-md-4">
                                    <h6 class="text-muted mb-2">Donde</h6>
                                    <asp:DropDownList runat="server" ID="ddlCodeRuleValueType" AutoPostBack="true" CssClass="form-control form-group-sm">
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
                        </asp:WizardStep>
                        <asp:WizardStep ID="wsSummary" runat="server" Title=" ">
                            <h5 class="text-muted">Resumen</h5>
                            <div class="row">
                            </div>
                        </asp:WizardStep>
                    </WizardSteps>
                </asp:Wizard>
                <%--WIZARD--%>
            </div>
        </div>
    </div>
</asp:Content>
