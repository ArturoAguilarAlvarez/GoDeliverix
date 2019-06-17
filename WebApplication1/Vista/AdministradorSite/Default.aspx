<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/MasterDeliverix.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.Vista.AdministradorSite.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Inicio
</asp:Content>
<asp:Content ID="Favicon" ContentPlaceHolderID="favicon" runat="server">
    <link rel="icon" href="../FavIcon/inicio.png"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="panel panel-default container" style="opacity:0.9;">
       <div class="col-md-12">
           <div class="col-md-6 text-center" style="margin-top:.5cm; border-style:double; border-radius:5px;">
               <a href="#" style="align-items:center;" >
                   <h3>Suministradores</h3>
                   <img src="../Img/provedora.jpg" width="200" style="margin-top:.3cm; margin-left:4cm;" class="img-responsive img-rounded" alt="Empresa Provedora" />
                   <asp:LinkButton runat="server" CssStyle="margin-top:.03cm;" CssClass="btn btn-info">Ver mas...<span class="glyphicon glyphicon-arrow-right"></span></asp:LinkButton> 
               </a>
           </div>
           <div class="col-md-6 text-center" style="margin-top:.5cm; border-style:double;">
              <a href="EmpSuministradora.aspx" style="align-items:center;" >
                   <h3>Distribuidores</h3>
                   <img src="../Img/distribuidora.png" width="256" style="margin-top:.3cm; margin-left:4cm;" class="img-responsive img-rounded" alt="Empresa Provedora" />
                   <asp:LinkButton runat="server" style="margin-left:10cm; margin-bottom:1cm:" CssClass="btn btn-info">Ver mas...<span class="glyphicon glyphicon-arrow-right"></span></asp:LinkButton> 
               </a>
           </div>
       </div>
       <hr />
       <div class="col-md-12" style="margin-top:1.5cm;">
           <div class="col-md-6 text-center"  style="margin-top:.5cm; border-style:double;">
               <a href="#" style="align-items:center;" >
                   <h3>Usuarios</h3>
                   <img src="../Img/provedora.jpg" width="200" style="margin-top:.3cm; margin-left:4cm;" class="img-responsive img-rounded" alt="Empresa Provedora" />
                   <asp:LinkButton runat="server" CssStyle="margin-top:.03cm;" CssClass="btn btn-info">Ver mas...<span class="glyphicon glyphicon-arrow-right"></span></asp:LinkButton> 
               </a>
           </div>
           <div class="col-md-6 text-center" style="margin-top:.5cm; border-style:double; border-radius:5px;">
               <a href="#" style="align-items:center;" >
                   <h3>Suministradora</h3>
                   <img src="../Img/provedora.jpg" width="200" style="margin-top:.3cm; margin-left:4cm;" class="img-responsive img-rounded" alt="Empresa Provedora" />
                   <asp:LinkButton runat="server" CssStyle="margin-top:.03cm;" CssClass="btn btn-info">Ver mas...<span class="glyphicon glyphicon-arrow-right"></span></asp:LinkButton> 
               </a>
           </div>
       </div>
   </div>
</asp:Content>
