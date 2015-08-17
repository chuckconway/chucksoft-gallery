<%@ Page Language="C#" MasterPageFile="MangeGallery.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="Chucksoft.Admin.Manage" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" >
        
    <style type="text/css" media="screen">@import url("../litebox.css");</style>
    <script type="text/javascript"  src="../Scripts/prototype.lite.js"></script>
    <script type="text/javascript" src="../Scripts/moo.fx.js"></script>
    <script type="text/javascript" src="../Scripts/litebox-1.0.js"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content">
        <asp:PlaceHolder ID="manageGalleries" runat="server" />
    </div>
</asp:Content>
