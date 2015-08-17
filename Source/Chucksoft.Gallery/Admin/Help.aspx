<%@ Page Language="C#" MasterPageFile="~/Admin/MangeGallery.Master" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="Chucksoft.Admin.Help" Title="Untitled Page" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h2>Views</h2>

<a name="Ablum_View"></a>
<h3>Ablum View</h3>
<p>Album view displays a gallery of galleries.  Each album contain multiple galleries, each gallery contains multiple photos.</p>

<a name="Gallery_View"></a>
<h3>Gallery View</h3>
<p>Gallery view displays a collection of galleries. Each gallery has one or more containing photos.</p>

<a name="Random_View"></a>
<h3>Random View</h3>
<p>Random view is the same as Single View, with the exception that it displays a photo randomly from all galleries. </p>

<a name="Single_View"></a>
<h3>Single View</h3>
<p>Single view displays only one and the newest photo.</p>

</asp:Content>
