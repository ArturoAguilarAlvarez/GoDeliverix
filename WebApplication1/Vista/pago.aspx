<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/MasterDeliverix.Master" AutoEventWireup="true" CodeBehind="pago.aspx.cs" Inherits="WebApplication1.Vista.pago" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    <style>
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="favicon" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblRespuesta" runat="server" />
    <div class="embed-responsive embed-responsive-16by9">
        <iframe id="iframePrueba" style="position: absolute; top: 0px; left: 0px; width: 100%;background: url(https://www.goparkix.net/Images/loader2.gif); height: 100%; background-position: center; background-repeat: no-repeat; background-size: cover;" seamless="seamless" runat="server" />
    </div>
</asp:Content>
