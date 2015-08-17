<%@ Page Language="C#" MasterPageFile="~/ChucksoftGallery.Master" EnableViewState="false" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Chucksoft.Default" Title="Untitled Page" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="breadcrumbnavigation"><chucksoft:BreadCrumbs ID="galleryBreadCrumbs" runat="server" CssClass="breadcrumbnavigation" Separator=">" /></div>
    <asp:PlaceHolder ID="photoView" runat="server" />
    
</asp:Content>
