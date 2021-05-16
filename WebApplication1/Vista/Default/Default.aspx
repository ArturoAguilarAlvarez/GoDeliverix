<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Descarga nuestra app GoDeliverix</title>
    <!-- Declaracion de scripts -->
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <%--Carga Loader--%>
    <link href="../../Content/Loader.css" rel="stylesheet" />
    <!-- Declaracion de CSS -->
    <link href="../../Content/bootstrap.css" rel="stylesheet" />
    <link href="../../Content/bootstrap-theme.css" rel="stylesheet" />
    <link rel="icon" href="../../Vista/FavIcon/Favicon.png" />
    <%--Fabook validation--%>
    <meta name="facebook-domain-verification" content="mqg1mvgcedre1vugy0hmqjji378hjg" />
</head>
<body class="container">
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" />
        <asp:UpdatePanel ID="PanelCentral" runat="server">
            <ContentTemplate>
                <div class="col-lg-offset-1  col-lg-8 col-md-offset-2 col-md-6 col-md-offset-3 col-sm-11 col-xs-11" style="margin-top: 70px; z-index: 1; position: absolute">
                    <!-- Modal -->
                    <asp:Panel ID="PanelRecuperarContrasenia" runat="server" class="panel panel-primary" Style="z-index: 1; position: absolute;">
                        <div class="panel-heading">
                            <asp:LinkButton runat="server" ID="BtnCerrarPanelRecuperarPassword" OnClick="BtnCerrarPanelRecuperarPassword_Click" CssClass="close">&times;</asp:LinkButton>
                            <h4 class="modal-title">Recuperar contraseña</h4>
                        </div>
                        <div class="panel-body">
                            <p>Introduce tu correo electronico</p>
                            <div class="input-group">
                                <i class="input-group-addon">
                                    <span class="glyphicon glyphicon-send"></span>
                                </i>
                                <asp:TextBox ID="txtCorreoElectronico" CssClass="form-control" runat="server" />
                            </div>
                        </div>
                        <div class="panel-footer pull-right">
                            <asp:Label ID="LblMensajePassword" runat="server" />
                            <asp:LinkButton CssClass="btn  btn-info" OnClick="BtnRecuperarCuenta_Click" ID="BtnRecuperarCuenta" runat="server">
                                        <span class="glyphicon glyphicon-ok"></span>
                                    Recuperar
                            </asp:LinkButton>
                        </div>
                    </asp:Panel>
                </div>

                <div class="col-lg-offset-1 col-lg-10 col-md-offset-1 col-md-6" style="z-index: 0; margin-top: -100px;">
                    <div class="panel panel-primary" style="align-content: center; margin-top: 5cm;">
                        <div class="panel-body text-center">
                            <asp:Panel ID="panelMensaje" Visible="false" runat="server">
                                <div class="col-md-8">
                                    <asp:Label CssClass="label" Style="color: red" runat="server" ID="lblMensaje" />
                                </div>
                                <div class="col-md-offset-1 col-md-3">
                                    <asp:LinkButton ToolTip="Enviar correo" ID="btnCorreoConfirmacion" OnClick="btnCorreoConfirmacion_Click" CssClass="btn btn-default" runat="server">
                                   <span class="glyphicon glyphicon-send"></span>
                                    </asp:LinkButton>
                                </div>
                            </asp:Panel>
                            <asp:Image ImageUrl="~/Vista/Img/Logo.png" Width="200" runat="server" />
                            <div class="input-group" style="margin-top: 10px;">
                                <i class="input-group-addon">
                                    <span class="glyphicon glyphicon-user"></span>
                                </i>
                                <asp:TextBox runat="server" ID="txtUser" CssClass="form-control"></asp:TextBox>
                            </div>
                            <br />
                            <div class="input-group">
                                <i class="input-group-addon">
                                    <span class="glyphicon glyphicon-lock"></span>
                                </i>
                                <asp:TextBox runat="server" ID="txtPass" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            </div>
                            <br />

                            <asp:LinkButton runat="server" CssClass=" form-control btn btn-success" OnClick="btnAcceso_Click" ID="btnAcceso">
                            <span class="glyphicon glyphicon-log-in"></span> Entrar
                            </asp:LinkButton>
                            <%--<div class="col-md-6" style="margin-top: 15px;">
                                <i>No tienes cuenta? 
                        <asp:LinkButton runat="server" CssClass=" btn btn-link" ID="BtnRegistro" OnClick="BtnRegistro_Click" >
                            <span class="glyphicon glyphicon-arrow-right"></span>
                            Registrate aqui</asp:LinkButton>
                                </i>
                                <asp:LinkButton  runat="server" ID="BtnRecuperarPassword" OnClick="BtnRecuperarPassword_Click" CssClass="btn btn-link" >
                                    <span class="glyphicon glyphicon-arrow-right"></span>
                                    Olvidaste tu contraseña?</asp:LinkButton>
                                
                            </div>--%>
                            <%--                            <asp:CheckBox ID="chkRecuerdame" Text="Recuerdame" CssClass="checkbox" runat="server" />--%>
                            <a href="https://play.google.com/store/apps/details?id=com.CompuAndSoft.GDCliente">
                                <img src="../Img/DownloadIcons/GooglePlay.png" height="100" />
                            </a>
                            <a href="http://appstore.com/compuandsoft/godeliverix">
                                <img src="../Img/DownloadIcons/AppleStore.jpg" height="88" />
                            </a>
                        </div>
                        <div class="panel-footer">
                        </div>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress AssociatedUpdatePanelID="PanelCentral" runat="server">
            <ProgressTemplate>
                <div style="z-index: 1">
                    <div class="cssload-preloader">
                        <div class="cssload-preloader-box">
                            <div>G</div>
                            <div>o</div>
                            <div>D</div>
                            <div>e</div>
                            <div>l</div>
                            <div>i</div>
                            <div>v</div>
                            <div>e</div>
                            <div>r</div>
                            <div>i</div>
                            <div>x</div>
                        </div>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </form>

    <script>
        if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
            if (/Android/i.test(navigator.userAgent)) {
                location.href = "https://play.google.com/store/apps/details?id=com.CompuAndSoft.GDCliente";
            } else if (/iPhone|iPad|iPod/i.test(navigator.userAgent)) {
                location.href = "https://apps.apple.com/app/godeliverix/id1495911877";
            }
        }
    </script>
</body>
</html>
