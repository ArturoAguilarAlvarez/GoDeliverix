<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="WebApplication1.Vista.Registro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Registro</title>
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>

    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <!-- Importacion de archivos css -->
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/bootstrap-theme.css" rel="stylesheet" />
    <link href="../Content/MisEstilos.css" rel="stylesheet" />
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <link href="https://code.jquery.com/ui/1.12.1/themes/dark-hive/jquery-ui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" />
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="col-md-3"></div>
                <div class="col-md-6">
                    <div class="panel panel-primary" style="align-content: center; margin-top: 1cm;">
                        <div class="panel-body text-center">
                            <div class="row pull-left" style="margin-left: 15px">
                                <a href="Default/Default.aspx"><span class="glyphicon glyphicon-share"></span>Iniciar Sesion</a>
                            </div>
                            <div class="clearfix"></div>
                            <h1>Formulario de registro </h1>
                            <div class="row pull-left">
                                Campos requeridos *
                            </div>
                            <div class="clearfix"></div>
                            <div class="row">
                                <div class="col-md-4">
                                    <label>Nombre(s)*</label>
                                    <div class="input-group">
                                        <span class="input-group-addon">
                                            <label class="glyphicon glyphicon-menu-right"></label>
                                        </span>
                                        <asp:TextBox ID="txtNombreRegistro" CssClass="form-control" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <label>Apellido Paterno*</label>
                                    <div class="input-group">
                                        <span class="input-group-addon">
                                            <label class="glyphicon glyphicon-menu-right"></label>
                                        </span>
                                        <asp:TextBox ID="txtApellidoPRegistro" CssClass="form-control" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <label>Apellido Materno*</label>
                                    <div class="input-group">
                                        <span class="input-group-addon">
                                            <label class="glyphicon glyphicon-menu-right"></label>
                                        </span>
                                        <asp:TextBox ID="txtApellidoMRegistro" CssClass="form-control" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <label>Usuario*</label>
                                    <div class="input-group">
                                        <span class="input-group-addon">
                                            <label class="glyphicon glyphicon-user"></label>
                                        </span>
                                        <asp:TextBox ID="txtUsuarioRegistro" AutoPostBack="true" OnTextChanged="txtUsuarioRegistro_TextChanged" CssClass="form-control" runat="server" />
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <label>Contraseña*</label>
                                    <div class="input-group">
                                        <span class="input-group-addon">
                                            <label class="glyphicon glyphicon-lock"></label>
                                        </span>
                                        <asp:TextBox ID="txtpasswordRegistro" CssClass="form-control" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <label>Repetir contraseña*</label>
                                    <div class="input-group">
                                        <span class="input-group-addon">
                                            <label class="glyphicon glyphicon-lock"></label>
                                        </span>
                                        <asp:TextBox ID="txtpasswordConfirmacionRegistro" CssClass="form-control" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-4 ">
                                    <label>Fecha de Naciento</label>
                                    <div class="input-group">
                                        <span class="input-group-addon">
                                            <label class="glyphicon glyphicon-calendar"></label>
                                        </span>
                                        <asp:TextBox TextMode="Date" ID="txtFechaDeNacimiento" CssClass="form-control" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-8">
                                    <label>Telefono</label>
                                    <div class="input-group">
                                        <span class="input-group-addon">
                                            <label class="glyphicon glyphicon-phone"></label>
                                        </span>
                                        <asp:TextBox ID="txtTelefonoRegistro" CssClass="form-control" TextMode="Number" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-9 ">
                                    <label>Correo electronico*</label>
                                    <div class="input-group">
                                        <span class="input-group-addon">
                                            <label class="glyphicon glyphicon-envelope"></label>
                                        </span>
                                        <asp:TextBox ID="txtEmailRegistro" CssClass="form-control" TextMode="Email" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-9">
                                    <label>Confirmar correo electronico*</label>
                                    <div class="input-group">
                                        <span class="input-group-addon">
                                            <label class="glyphicon glyphicon-envelope"></label>
                                        </span>
                                        <asp:TextBox ID="TxtConfirmacionEmail" OnTextChanged="TxtConfirmacionEmail_TextChanged" CssClass="form-control" TextMode="Email" runat="server" />
                                    </div>
                                </div>
                                <%-- <div class="col-md-4">
                                    <label>Fecha de nacimiento*</label>
                                    <div class="input-group">
                                        <span class="input-group-addon">
                                            <label class="glyphicon glyphicon-calendar"></label>
                                        </span>
                                        <asp:TextBox ID="DTFechaNacimientoRegistro" CssClass="form-control" TextMode="Date" runat="server" />
                                    </div>
                                </div>--%>


                                <div class="col-md-8" style="margin-top: 15px;">

                                    <asp:CheckBox ID="chkTerminosYcondiciones" Text="Acepto terminos y condiciones" runat="server" />

                                    Ver <a href="#">terminos y condiciones*</a>

                                </div>
                                <div class="col-md-4 text-right" style="margin-top: 10px">
                                    <asp:LinkButton CssClass="btn btn-sm btn-default" OnClick="BtnRegistroUsuario_Click" ID="btnRegistroUsuario" runat="server">
                            <span class="glyphicon glyphicon-check">
                            </span>
                            Registrarme
                                    </asp:LinkButton><br />
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-3"></div>

                <div id="myModal" class="modal " role="dialog">
                    <div class="modal-dialog modal-lg">

                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header text-center">
                                    Verificar identidad
                                    </div> 
                            <div class="modal-body">
                                <div class="panel-body">
                                    <div class="col-md-12">
                                        <label>Se ha enviado un correo de verificacion al correo </label>
                                        <asp:Label ID="lblCorreoDeVerificacion" runat="server" />
                                    </div>
                                    <div class="pull-right">
                                        <a href="Default/Default.aspx"><span class="glyphicon glyphicon-share"></span>Iniciar Sesion</a>
                                    </div>
                                </div>

                                <!-- <div class="modal-footer">
                                    
                                    </div> -->
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>
</body>
</html>
