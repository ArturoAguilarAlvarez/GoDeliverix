<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/MasterDeliverix.Master" AutoEventWireup="true" CodeBehind="Codigos.aspx.cs" Inherits="WebApplication1.Vista.Codigos" %>

<asp:Content runat="server" ContentPlaceHolderID="Styles">
    <style>
        .nav > li > a {
            padding: 6px 8px;
        }

        .panel-body {
            padding: 12px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <asp:Panel runat="server" ID="pnlCodeList" class="col-md-8">
                <div class="panel">
                    <div class="panel-body">
                        <h4 class="text-muted">Codigos de promoción</h4>

                        <asp:GridView
                            runat="server"
                            ID="gvPromotionCodes"
                            CssClass="table table-striped table-bordered table-condensed"
                            AutoGenerateColumns="False"
                            OnSelectedIndexChanged="gvPromotionCodes_SelectedIndexChanged"
                            OnRowCommand="gvPromotionCodes_RowCommand"
                            AutoGenerateSelectButton="false"
                            AutoGenerateEditButton="false"
                            DataKeyNames="Uid">
                            <Columns>
                                <asp:BoundField HeaderText="Creacion" DataField="CreatedAt" DataFormatString="{0:d}" />
                                <asp:BoundField HeaderText="Inicio" DataField="StartAt" DataFormatString="{0:d}" />
                                <asp:BoundField HeaderText="Codigo" DataField="Code" />
                                <asp:BoundField HeaderText="Nivel" DataField="Level" />
                                <asp:BoundField HeaderText="Expiracion" DataField="Expiration" />
                                <asp:BoundField HeaderText="Usos" DataField="Activations" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" CommandName="Select" CommandArgument='<%#Eval("Uid")%>' Text="Editar" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <SelectedRowStyle BackColor="#dff0d8" ForeColor="#0f5132" />
                        </asp:GridView>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel runat="server" ID="pnlCodeSummary" CssClass="col-md-4">
                <div class="panel">
                    <div class="panel-body">

                        <%--TITLE--%>
                        <div style="flex-direction: row; box-sizing: border-box; display: flex; place-content: center space-between; align-items: center;">
                            <h4 class="text-muted">Código de promoción</h4>

                            <div>
                                <asp:Button Text="Eliminar" runat="server" ID="btnDelete" CssClass="btn btn-sm btn-danger" />
                                <asp:Button Text="Editar" runat="server" ID="btnEdit" CssClass="btn btn-sm btn-info" />
                                <asp:Button Text="Agregar" runat="server" ID="btnSave" Visible="false" CssClass="btn btn-sm btn-success" />
                                <asp:Button Text="Cancelar" runat="server" ID="btnCancel" Visible="false" CssClass="btn btn-sm btn-danger" />
                            </div>
                        </div>
                        <%--TITLE--%>

                        <ul class="nav nav-tabs">
                            <li runat="server" id="tab0" class="active">
                                <asp:LinkButton runat="server" ID="btnLinkData" Text="Datos" OnCommand="CodeTab_Clicked" CommandArgument="0"></asp:LinkButton>
                            </li>
                            <li runat="server" id="tab1">
                                <asp:LinkButton runat="server" ID="btnLinkRegion" Text="Region" OnCommand="CodeTab_Clicked" CommandArgument="1"></asp:LinkButton>
                            </li>
                            <li runat="server" id="tab2">
                                <asp:LinkButton runat="server" ID="btnLinkDeliveryCompany" Text="Distribuidora" OnCommand="CodeTab_Clicked" CommandArgument="2"></asp:LinkButton>
                            </li>
                            <li runat="server" id="tab3">
                                <asp:LinkButton runat="server" ID="btnLinkCompany" Text="Suministradora" OnCommand="CodeTab_Clicked" CommandArgument="3"></asp:LinkButton>
                            </li>
                            <li runat="server" id="tab4">
                                <asp:LinkButton runat="server" ID="btnLinkRules" Text="Reglas" OnCommand="CodeTab_Clicked" CommandArgument="4"></asp:LinkButton>
                            </li>
                        </ul>

                        <asp:MultiView ActiveViewIndex="0" runat="server" ID="mvTabs">
                            <%--GENERAL--%>
                            <asp:View runat="server">
                                <%--ROW--%>
                                <div class="row">
                                    <div class="col-md-8">
                                        <h6 class="text-muted  mb-2">Código</h6>
                                        <asp:TextBox runat="server" ID="txtCode" CssClass="form-control form-group-sm" MaxLength="16"></asp:TextBox>
                                    </div>

                                    <div class="col-md-4">
                                        <h6 class="text-muted mb-2">Estatus:</h6>
                                        <asp:DropDownList runat="server" ID="ddlCodeStatus" CssClass="form-control form-group-sm">
                                            <asp:ListItem Text="Activo" Value="1" />
                                            <asp:ListItem Text="Inactivo" Value="0" />
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-12">
                                        <h6 class="text-muted  mb-2">Recompensa</h6>
                                        <asp:DropDownList runat="server" ID="ddlRewardType" CssClass="form-control form-group-sm"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-6">
                                        <h6 class="text-muted  mb-2">Tipo de valor</h6>
                                        <asp:DropDownList runat="server" ID="ddlValueType" CssClass="form-control form-group-sm">
                                            <asp:ListItem Text="Monto" Value="0" />
                                            <asp:ListItem Text="Porcentage" Value="1" />
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-6">
                                        <h6 class="text-muted mb-2">Valor</h6>
                                        <div class="input-group">
                                            <asp:TextBox runat="server" ID="txtValue" CssClass="form-control" MaxLength="16"></asp:TextBox>
                                            <asp:Label Text="$" runat="server" ID="lblValueType" CssClass="input-group-addon" />
                                        </div>
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
                                    <div class="col-md-6">
                                        <h6 class="text-muted mb-2">Fecha de inicio</h6>
                                        <asp:TextBox runat="server" ID="txtStartAt" TextMode="Date" CssClass="form-control form-group-sm" />
                                    </div>

                                    <div class="col-md-6">
                                        <h6 class="text-muted mb-2">Tipo de expiración</h6>
                                        <asp:DropDownList runat="server" ID="ddlCodeExpirationType" CssClass="form-control form-group-sm">
                                            <asp:ListItem Text="None" Value="0" />
                                            <asp:ListItem Text="Fecha" Value="1" />
                                            <asp:ListItem Text="Activaciones" Value="2" />
                                            <asp:ListItem Text="Dias despues de canjear" Value="3" />
                                        </asp:DropDownList>
                                    </div>

                                    <asp:Panel runat="server" ID="pnlExpirationDate" Visible="false" class="col-md-4">
                                        <h6 class="text-muted mb-2">Fecha de expiración</h6>
                                        <asp:TextBox runat="server" TextMode="Date" ID="txtExpirationDate" CssClass="form-control form-group-sm" />
                                    </asp:Panel>

                                    <asp:Panel runat="server" ID="pnlActivationNumber" Visible="false" class="col-md-4">
                                        <h6 class="text-muted mb-2">Numero de activaciones</h6>
                                        <asp:TextBox runat="server" ID="txtActivationsNumber" CssClass="form-control form-group-sm" />
                                    </asp:Panel>

                                    <asp:Panel runat="server" ID="pnlDaysBeforeActivation" Visible="false" class="col-md-4">
                                        <h6 class="text-muted mb-2">Dias</h6>
                                        <asp:TextBox runat="server" ID="txtDaysBeforeActivation" CssClass="form-control form-group-sm" />
                                    </asp:Panel>
                                </div>
                                <%--ROW--%>
                            </asp:View>
                            <%--GENERAL--%>

                            <%--REGION--%>
                            <asp:View runat="server">
                                <div class="row">
                                    <asp:Panel runat="server" ID="pnlSmCountry" class="col-md-12">
                                        <h6 class="text-muted pb-0 mb-0">Pais</h6>
                                        <asp:Label runat="server" ID="lblSmCountry" Visible="True" />
                                    </asp:Panel>

                                    <asp:Panel runat="server" ID="pnlSmState" class="col-md-12">
                                        <h6 class="text-muted pb-0 mb-0">Estado</h6>
                                        <asp:Label runat="server" ID="lblSmState" Visible="True" />
                                    </asp:Panel>

                                    <asp:Panel runat="server" ID="pnlSmMunicipality" class="col-md-12">
                                        <h6 class="text-muted pb-0 mb-0">Municipio</h6>
                                        <asp:Label runat="server" ID="lblSmMunicipality" Visible="True" />
                                    </asp:Panel>

                                    <asp:Panel runat="server" ID="pnlSmCity" class="col-md-12">
                                        <h6 class="text-muted pb-0 mb-0">Ciudad</h6>
                                        <asp:Label runat="server" ID="lblSmCity" Visible="True" />
                                    </asp:Panel>

                                    <asp:Panel runat="server" ID="pnlSmNeighborhood" class="col-md-12">
                                        <h6 class="text-muted pb-0 mb-0">Colonia</h6>
                                        <asp:Label runat="server" ID="lblSmNeighborhood" Visible="True" />
                                    </asp:Panel>
                                </div>
                            </asp:View>
                            <%--REGION--%>

                            <%--DELIVERY COMPANY--%>
                            <asp:View runat="server">
                                <div class="row">
                                    <asp:Panel runat="server" ID="pnlDeliveryCompany" class="col-md-12">
                                        <h6 class="text-muted pb-0 mb-0">Empresa</h6>
                                        <asp:Label runat="server" ID="lblDeliveryCompany" Visible="True" />
                                    </asp:Panel>
                                    
                                    <asp:Panel runat="server" ID="pnlDeliveryCompanyBranch" class="col-md-12">
                                        <h6 class="text-muted pb-0 mb-0">Sucursal</h6>
                                        <asp:Label runat="server" ID="lblDeliveryCompanyBranch" Visible="True" />
                                    </asp:Panel>
                                </div>
                            </asp:View>
                            <%--DELIVERY COMPANY--%>

                            <%--COMPANY--%>
                            <asp:View runat="server">
                                <div class="row">
                                    <asp:Panel runat="server" ID="pnlCompany" class="col-md-12">
                                        <h6 class="text-muted pb-0 mb-0">Empresa</h6>
                                        <asp:Label runat="server" ID="lblCompany" Visible="True" />
                                    </asp:Panel>
                                    
                                    <asp:Panel runat="server" ID="pnlCompanyBranch" class="col-md-12">
                                        <h6 class="text-muted pb-0 mb-0">Sucursal</h6>
                                        <asp:Label runat="server" ID="lblCompanyBranch" Visible="True" />
                                    </asp:Panel>
                                </div>
                            </asp:View>
                            <%--COMPANY--%>

                            <%--RULES--%>
                            <asp:View runat="server">
                                <div class="row">
                                </div>
                            </asp:View>
                            <%--RULES--%>
                        </asp:MultiView>
                    </div>
                </div>
                <div class="panel">
                    <div class="panel-body">
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
