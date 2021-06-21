<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/MasterDeliverix.Master" AutoEventWireup="true" CodeBehind="Codigos.aspx.cs" Inherits="WebApplication1.Vista.Codigos" %>

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

                        <ul class="nav nav-tabs">
                            <li runat="server" id="tab0">
                                <asp:LinkButton runat="server" Text="Datos" OnCommand="CodeTab_Clicked" CommandArgument="0"></asp:LinkButton>
                            </li>
                            <li runat="server" id="tab1">
                                <asp:LinkButton runat="server" Text="Region" OnCommand="CodeTab_Clicked" CommandArgument="1"></asp:LinkButton>
                            </li>
                            <li runat="server" id="tab2">
                                <asp:LinkButton runat="server" Text="Distribuidora" OnCommand="CodeTab_Clicked" CommandArgument="2"></asp:LinkButton>
                            </li>
                            <li runat="server" id="tab3">
                                <asp:LinkButton runat="server" Text="Suministradora" OnCommand="CodeTab_Clicked" CommandArgument="3"></asp:LinkButton>
                            </li>
                            <li runat="server" id="tab4">
                                <asp:LinkButton runat="server" Text="Reglas" OnCommand="CodeTab_Clicked" CommandArgument="4"></asp:LinkButton>
                            </li>
                        </ul>

                        <asp:MultiView ActiveViewIndex="0" runat="server" ID="mvTabs">
                            <asp:View runat="server">One</asp:View>
                            <asp:View runat="server">Two</asp:View>
                            <asp:View runat="server">Three</asp:View>
                            <asp:View runat="server">Four</asp:View>
                            <asp:View runat="server">Five</asp:View>
                        </asp:MultiView>
                    </div>
                </div>
                <div class="panel">
                    <div class="panel-body">
                        <%--DATOS GENERALES--%>
                        <div class="col-md-12">
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
                                    <%--<asp:Calendar runat="server" CssClass="form-control form-control-sm"></asp:Calendar>--%>
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
                        </div>
                        <%--DATOS GENERALES--%>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
