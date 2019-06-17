<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnviarCorreoDeConfirmacion.aspx.cs" Inherits="WebApplication1.Vista.EnviarCorreoDeConfirmacion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Correo de confirmacion</title>
    <!-- Declaracion de scripts -->
    <script src="scripts/jquery-3.1.0.js"></script>
    <script src="scripts/bootstrap.js"></script>
    <!-- Declaracion de CSS -->
    <link href="../../Content/bootstrap.css" rel="stylesheet" />
    <link href="../../Content/bootstrap-theme.css" rel="stylesheet" />
    <link rel="icon" href="../../Vista/FavIcon/login.png" />

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" />
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="col-md-3"></div>
                <div class="col-md-6">
                    <div class="panel panel-primary" style="align-content: center; margin-top: 5cm;">
                        <div class="panel-body text-center">
                            <div class="row pull-left" style="margin-left: 15px; margin-bottom: 15px;">
                                <a href="Default/Default.aspx"><span class="glyphicon glyphicon-share"></span>Iniciar Sesion</a>
                            </div>
                            <asp:Panel ID="panelMensaje" Visible="false" runat="server">
                                <asp:Label CssClass="label" Style="color: red" runat="server" ID="lblMensaje" />
                            </asp:Panel>
                            <div class="col-md-12">
                                <h1>Escribe tu correo electronico</h1>
                                <div class="input-group" style="margin-bottom: 20px;">
                                    <i class="input-group-addon">
                                        <span class="glyphicon glyphicon-envelope"></span>
                                    </i>
                                    <asp:TextBox runat="server" ID="txtCorreoElectronico" CssClass="form-control"></asp:TextBox>
                                </div>
                                <asp:LinkButton runat="server" CssClass=" form-control btn btn-success" OnClick="btnEnviarCorreo_Click" ID="btnEnviarCorreo">
                            <span class="glyphicon glyphicon-new-window"></span> Enviar correo
                                </asp:LinkButton>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="col-md-3"></div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
