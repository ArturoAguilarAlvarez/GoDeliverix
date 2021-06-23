<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/MasterDeliverix.Master" AutoEventWireup="true" CodeBehind="CodigoForm.aspx.cs" Inherits="WebApplication1.Vista.CodigoForm" %>

<%@ Register TagName="DropDownListCheck" TagPrefix="uWebControl" Src="~/UserControls/uDropDownListCheck.ascx" %>
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
                width: 24px;
                height: 12px;
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

                <asp:Panel runat="server" ID="pnlError" CssClass="panel panel-danger">
                    <div class="panel-body">
                        <asp:Label Text="Error" runat="server" ID="lblError"></asp:Label>
                    </div>
                </asp:Panel>

                <%--WIZARD--%>
                <asp:Wizard ID="Wizard1" runat="server" CssClass="panel w-100" OnNextButtonClick="Wizard1_NextButtonClick" OnSideBarButtonClick="Wizard1_SideBarButtonClick" OnFinishButtonClick="Wizard1_OnFinishButtonClick">

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
                                <li class="<%# GetLinkStepClass(Container.DataItem) %>">
                                    <asp:LinkButton runat="server" ID="SideBarButton" />
                                </l>
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
                                    <asp:DropDownList runat="server" ID="ddlCodeLevel" CssClass="form-control form-group-sm">
                                        <asp:ListItem Text="Region" Value="0" />
                                        <asp:ListItem Text="Distribuidora" Value="1" />
                                        <asp:ListItem Text="Suministradora" Value="2" />
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
                                    <h6 class="text-muted">Pais</h6>
                                    <asp:DropDownList runat="server" ID="ddlCountry" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" CssClass="form-control form-control-sm"></asp:DropDownList>
                                </div>
                                <div class="col-md-12">
                                    <uWebControl:DropDownListCheck runat="server" ID="ddlState" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" Label="Estado" />
                                </div>
                                <div class="col-md-12">
                                    <uWebControl:DropDownListCheck runat="server" ID="ddlMunicipality" Visible="False" OnSelectedIndexChanged="ddlMunicipality_SelectedIndexChanged" Label="Municipio" />
                                </div>
                                <div class="col-md-12">
                                    <uWebControl:DropDownListCheck runat="server" ID="ddlCity" Visible="False" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" Label="Ciudad" />
                                </div>
                                <div class="col-md-12">
                                    <uWebControl:DropDownListCheck runat="server" ID="ddlNeighborhood" Visible="False" OnSelectedIndexChanged="ddlNeighborhood_SelectedIndexChanged" Label="Colonia" />
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
                                    <asp:DropDownList runat="server" ID="ddlDeliveryCompany" AutoPostBack="true" OnSelectedIndexChanged="ddlDeliveryCompany_SelectedIndexChanged" CssClass="form-control form-control-sm">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-12">
                                    <uWebControl:DropDownListCheck runat="server" ID="ddlDeliveryCompanyBranch" OnSelectedIndexChanged="ddlDeliveryCompanyBranch_SelectedIndexChanged" Label="Sucursal" />
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
                                    <h6 class="text-muted">Suministradora</h6>
                                    <asp:DropDownList runat="server" ID="ddlCompany" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" CssClass="form-control form-control-sm">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-12">
                                    <uWebControl:DropDownListCheck runat="server" ID="ddlCompanyBranch" OnSelectedIndexChanged="ddlCompanyBranch_SelectedIndexChanged" Label="Sucursal" />
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
                                            <asp:DropDownList runat="server" ID="ddlRewardType" CssClass="form-control form-group-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlRewardType_OnSelectedIndexChanged">
                                                <asp:ListItem Text="Ninguno" Value="0" />
                                                <asp:ListItem Text="Monto a monedero" Value="1" />
                                                <asp:ListItem Text="Envío gratis" Value="2" />
                                                <asp:ListItem Text="Descuento (Orden)" Value="3" />
                                                <asp:ListItem Text="Descuento en envío (Orden)" Value="4" />
                                                <asp:ListItem Text="Reembolso (Orden)" Value="5" />
                                                <asp:ListItem Text="Reembolso del envío (Orden)" Value="6" />
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-md-2">
                                            <h6 class="text-muted  mb-2">Tipo de valor</h6>
                                            <asp:DropDownList runat="server" ID="ddlValueType" AutoPostBack="true" OnSelectedIndexChanged="ddlValueType_OnSelectedIndexChanged" CssClass="form-control form-group-sm">
                                                <asp:ListItem Text="Monto" Value="0" />
                                                <asp:ListItem Text="Porcentage" Value="1" />
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-md-2">
                                            <h6 class="text-muted mb-2">Valor</h6>
                                            <div class="input-group">
                                                <asp:TextBox runat="server" ID="txtValue" CssClass="form-control" MaxLength="16"></asp:TextBox>
                                                <asp:Label Text="$" runat="server" ID="lblValueType" CssClass="input-group-addon" />
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <h6 class="text-muted mb-2">Estatus:</h6>
                                            <asp:DropDownList runat="server" ID="ddlCodeStatus" CssClass="form-control form-group-sm">
                                                <asp:ListItem Text="Activo" Value="1" />
                                                <asp:ListItem Text="Inactivo" Value="0" />
                                            </asp:DropDownList>
                                        </div>

                                        <asp:Panel class="col-md-3" runat="server" Visible="False">
                                            <h6 class="text-muted mb-2">Aplica:</h6>
                                            <asp:DropDownList runat="server" ID="ddlActivationType" CssClass="form-control form-group-sm" Enabled="False">
                                                <asp:ListItem Text="Durante la compra" Value="0" />
                                                <asp:ListItem Text="Al finalizar la compra" Value="1" />
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </div>
                                    <%--ROW--%>

                                    <%--ROW--%>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <h6 class="text-muted mb-2">Inicio</h6>
                                            <%--<asp:Calendar runat="server" CssClass="form-control form-control-sm"></asp:Calendar>--%>
                                            <asp:TextBox runat="server" ID="txtStartAt" TextMode="Date" CssClass="form-control form-group-sm" />
                                        </div>

                                        <div class="col-md-3">
                                            <h6 class="text-muted mb-2">Tipo de expiración</h6>
                                            <asp:DropDownList runat="server" ID="ddlCodeExpirationType" AutoPostBack="true" OnSelectedIndexChanged="ddlCodeExpirationType_OnSelectedIndexChanged" CssClass="form-control form-group-sm">
                                                <asp:ListItem Text="None" Value="0" />
                                                <asp:ListItem Text="Fecha" Value="1" />
                                                <asp:ListItem Text="Activaciones" Value="2" />
                                                <asp:ListItem Text="Dias despues de canjear" Value="3" />
                                            </asp:DropDownList>
                                        </div>

                                        <asp:Panel runat="server" ID="pnlExpirationDate" Visible="false" class="col-md-3">
                                            <h6 class="text-muted mb-2">Fecha de expiración</h6>
                                            <asp:TextBox runat="server" TextMode="Date" ID="txtExpirationDate" CssClass="form-control form-group-sm" />
                                        </asp:Panel>

                                        <asp:Panel runat="server" ID="pnlActivationNumber" Visible="false" class="col-md-4">
                                            <h6 class="text-muted mb-2">Numero de activaciones</h6>
                                            <asp:TextBox runat="server" ID="txtActivationsNumber" CssClass="form-control form-group-sm" />
                                        </asp:Panel>

                                        <asp:Panel runat="server" ID="pnlDaysBeforeActivation" Visible="false" class="col-md-2">
                                            <h6 class="text-muted mb-2">Dias</h6>
                                            <asp:TextBox runat="server" ID="txtDaysBeforeActivation" CssClass="form-control form-group-sm" />
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
                                    <asp:Button Text="Nuevo" runat="server" ID="btnCodeRuleNew" OnClick="btnCodeRuleNew_Click" CssClass="btn btn-sm btn-default" />
                                    <asp:Button Text="Editar" runat="server" ID="btnCodeRuleEdit" OnClick="btnCodeRuleEdit_Click" Visible="false" CssClass="btn btn-sm btn-info" />
                                    <asp:Button Text="Agregar" runat="server" ID="btnCodeRuleSave" OnClick="btnCodeRuleSave_Click" Visible="false" CssClass="btn btn-sm btn-success" />
                                    <asp:Button Text="Cancelar" runat="server" ID="btnCodeRuleCancel" OnClick="btnCodeRuleCancel_Click" Visible="false" CssClass="btn btn-sm btn-danger" />
                                </div>
                            </div>
                            <%--TITLE--%>

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


                                <asp:Panel runat="server" CssClass="col-md-4" ID="pnlRuleValueType">
                                    <h6 class="text-muted mb-2">A</h6>
                                    <asp:TextBox runat="server" ID="txtCodeRuleValue" CssClass="form-control form-group-sm" />
                                    <asp:DropDownList runat="server" ID="ddlCodeRuleValue" Visible="false" CssClass="form-control form-group-sm">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </div>
                            <%--ROW--%>

                            <asp:Panel runat="server" CssClass="row" ID="pnlRuleGiroValueType" Visible="False">
                                <asp:Panel runat="server" ID="pnlRuleGiro" CssClass="col-md-3">
                                    <h6 class="text-muted mb-2">Giro</h6>
                                    <asp:DropDownList runat="server" ID="ddlRuleGiro" AutoPostBack="True" OnSelectedIndexChanged="ddlRuleGiro_OnSelectedIndexChanged" CssClass="form-control form-group-sm"></asp:DropDownList>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnlRuleCategoria" CssClass="col-md-3">
                                    <h6 class="text-muted mb-2">Categoria</h6>
                                    <asp:DropDownList runat="server" ID="ddlRuleCategoria" AutoPostBack="True" OnSelectedIndexChanged="ddlRuleCategoria_OnSelectedIndexChanged" CssClass="form-control form-group-sm"></asp:DropDownList>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnlRuleSubcategoria" CssClass="col-md-3">
                                    <h6 class="text-muted mb-2">Subcategoria</h6>
                                    <asp:DropDownList runat="server" ID="ddlRuleSubcategoria" AutoPostBack="True" OnSelectedIndexChanged="ddlRuleSubcategoria_OnSelectedIndexChanged" CssClass="form-control form-group-sm"></asp:DropDownList>
                                </asp:Panel>
                            </asp:Panel>

                            <br />

                            <%--ROW--%>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView runat="server" ID="gvCodeRules" AutoGenerateColumns="false" CssClass="table table-condensed table-responsive table-bordered">
                                        <EmptyDataTemplate>
                                            <div class="panel panel-info">No hay reglas registradas </div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField HeaderText="Donde" DataField="ValueTypeText" />
                                            <asp:BoundField HeaderText="Sea" DataField="OperatorText" />
                                            <asp:BoundField HeaderText="A" DataField="ValueText" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <%--ROW--%>
                        </asp:WizardStep>

                        <asp:WizardStep ID="wsSummary" runat="server" Title=" ">

                            <h5 class="text-bold mb-0">Tipo de codigo</h5>
                            <asp:Label Text="-" ID="lblSmCodeType" runat="server" />

                            <asp:Panel runat="server" ID="pnlSmRegion">
                                <h5 class="text-bold mb-0">Region</h5>
                                <div class="row">
                                    <asp:Panel runat="server" ID="pnlSmCountry" class="col-md-4">
                                        <h6 class="text-muted pb-0 mb-0">Pais</h6>
                                        <asp:Label runat="server" ID="lblSmCountry" Visible="True" />
                                    </asp:Panel>

                                    <asp:Panel runat="server" ID="pnlSmState" class="col-md-4">
                                        <h6 class="text-muted pb-0 mb-0">Estado</h6>
                                        <asp:Label runat="server" ID="lblSmState" Visible="True" />
                                    </asp:Panel>

                                    <asp:Panel runat="server" ID="pnlSmMunicipality" class="col-md-4">
                                        <h6 class="text-muted pb-0 mb-0">Municipio</h6>
                                        <asp:Label runat="server" ID="lblSmMunicipality" Visible="True" />
                                    </asp:Panel>

                                    <asp:Panel runat="server" ID="pnlSmCity" class="col-md-4">
                                        <h6 class="text-muted pb-0 mb-0">Ciudad</h6>
                                        <asp:Label runat="server" ID="lblSmCity" Visible="True" />
                                    </asp:Panel>

                                    <asp:Panel runat="server" ID="pnlSmNeighborhood" class="col-md-4">
                                        <h6 class="text-muted pb-0 mb-0">Colonia</h6>
                                        <asp:Label runat="server" ID="lblSmNeighborhood" Visible="True" />
                                    </asp:Panel>
                                </div>
                            </asp:Panel>

                            <asp:Panel runat="server" ID="pnlSmDeliveryCompany">
                                <h5 class="text-bold mb-0">Distribuidora</h5>
                                <div class="row">
                                    <div class="col-md-6">
                                        <h6 class="text-muted pb-0 mb-0">Empresa</h6>
                                        <asp:Label runat="server" ID="lblSmDeliveryCompany" Visible="True" />
                                    </div>
                                    <asp:Panel runat="server" ID="pnlSmDeliveryCompanyBranch" class="col-md-6">
                                        <h6 class="text-muted pb-0 mb-0">Sucursal</h6>
                                        <asp:Label runat="server" ID="lblSmDeliveryCompanyBranch" />
                                    </asp:Panel>
                                </div>
                            </asp:Panel>

                            <asp:Panel runat="server" ID="pnlSmCompany">
                                <h5 class="text-bold mb-0">Suministradora</h5>
                                <div class="row">
                                    <div class="col-md-6">
                                        <h6 class="text-muted pb-0 mb-0">Empresa</h6>
                                        <asp:Label runat="server" ID="lblSmCompany" Visible="True" />
                                    </div>
                                    <asp:Panel runat="server" ID="pnlSmCompanyBranch" class="col-md-6">
                                        <h6 class="text-muted pb-0 mb-0">Sucursal</h6>
                                        <asp:Label runat="server" ID="lblSmCompanyBranch" Visible="True" />
                                    </asp:Panel>
                                </div>
                            </asp:Panel>

                            <h5 class="text-bold mb-0">Datos generales</h5>
                            <%--ROW--%>
                            <div class="row">
                                <div class="col-md-3">
                                    <h6 class="text-muted  mb-2 mb-0">Código</h6>
                                    <asp:Label runat="server" ID="lblSmCode" />
                                </div>

                                <div class="col-md-3">
                                    <h6 class="text-muted  mb-2 mb-0">Recompensa</h6>
                                    <asp:Label runat="server" ID="lblSmRewardType" />
                                </div>

                                <div class="col-md-3">
                                    <h6 class="text-muted  mb-2 mb-0">Tipo de valor</h6>
                                    <asp:Label runat="server" ID="lblSmValueType" />
                                </div>

                                <div class="col-md-3">
                                    <h6 class="text-muted mb-2 mb-0">Valor</h6>
                                    <asp:Label runat="server" ID="lblSmValue" />
                                </div>

                                <div class="col-md-3">
                                    <h6 class="text-muted mb-2 mb-0">Aplica:</h6>
                                    <asp:Label runat="server" ID="lblSmActivationType" />
                                </div>

                                <div class="col-md-3">
                                    <h6 class="text-muted mb-2 mb-0">Estatus:</h6>
                                    <asp:Label runat="server" ID="lblSmStatus" />
                                </div>
                            </div>
                            <%--ROW--%>

                            <%--ROW--%>
                            <div class="row">
                                <div class="col-md-3">
                                    <h6 class="text-muted mb-2 mb-0">Inicio</h6>
                                    <asp:Label runat="server" ID="lblSmStartAt" />
                                </div>

                                <div class="col-md-3">
                                    <h6 class="text-muted mb-2 mb-0">Tipo de expiración</h6>
                                    <asp:Label runat="server" ID="lblSmExpirationType" />
                                </div>

                                <asp:Panel runat="server" ID="pnlSmExpirationDate" Visible="false" class="col-md-3">
                                    <h6 class="text-muted mb-2 mb-0">Fecha de expiración</h6>
                                    <asp:Label runat="server" ID="lblSmExpirationDate" />
                                </asp:Panel>

                                <asp:Panel runat="server" ID="pnlSmActivationNumber" Visible="false" class="col-md-4">
                                    <h6 class="text-muted mb-2 mb-0">Numero de activaciones</h6>
                                    <asp:Label runat="server" ID="lblSmActivationNumber" />
                                </asp:Panel>

                                <asp:Panel runat="server" ID="pnlSmDaysBeforeActivation" Visible="false" class="col-md-2">
                                    <h6 class="text-muted mb-2 mb-0">Dias</h6>
                                    <asp:Label runat="server" ID="lblSmDaysBeforeActivation" />
                                </asp:Panel>
                            </div>
                            <%--ROW--%>

                            <asp:Panel runat="server" ID="pnlSmCodeRules">
                                <h5 class="text-bold mb-0">Reglas</h5>
                                <%--ROW--%>

                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvSmCodeRules" AutoGenerateColumns="false" CssClass="table table-condensed table-responsive table-bordered">
                                            <EmptyDataTemplate>
                                                <div class="panel panel-info">No hay reglas registradas </div>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField HeaderText="Donde" DataField="ValueTypeText" />
                                                <asp:BoundField HeaderText="Sea" DataField="OperatorText" />
                                                <asp:BoundField HeaderText="A" DataField="ValueText" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <%--ROW--%>
                            </asp:Panel>

                        </asp:WizardStep>
                    </WizardSteps>
                </asp:Wizard>
                <%--WIZARD--%>
            </div>
        </div>
    </div>
</asp:Content>
