<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivacionDeCuenta.aspx.cs" Inherits="WebApplication1.Vista.ActivacionDeCuenta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Activacion de cuenta</title>
    <script src="../scripts/bootstrap.js"></script>
    <script src="../scripts/jquery-3.1.0.js"></script>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap-theme.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" />

        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="col-md-12 text-center" style="align-content: center; margin-top: 7cm;">
                    <label>Cuenta activada!! </label>
                    <br />
                    <a href="Default/Default.aspx">Inicia sesion para continuar</a>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>
</body>
</html>
