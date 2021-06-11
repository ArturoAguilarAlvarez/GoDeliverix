<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uDropDownListCheck.ascx.cs" Inherits="WebApplication1.UserControls.uDropDownListCheck" %>

<h6 class="text-muted pb-2">
    <asp:Label runat="server" ID="lblTitle" />
</h6>
<div class="input-group">
    <div class="input-group-addon">
        <asp:CheckBox Text="" runat="server" ID="uChk" AutoPostBack="true" OnCheckedChanged="uChk_CheckedChanged" />
    </div>
    <asp:DropDownList runat="server" ID="uDdl" AutoPostBack="true" OnSelectedIndexChanged="uDdl_SelectedIndexChanged" CssClass="form-control form-control-sm">
        <asp:ListItem Text="text1" Value=""/>
    </asp:DropDownList>
</div>
