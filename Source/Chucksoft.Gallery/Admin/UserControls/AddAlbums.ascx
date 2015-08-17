<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddAlbums.ascx.cs" Inherits="Chucksoft.Admin.UserControls.AddAlbum" %>
<div class="addstyles">
    <h1>Add Album</h1>
    
    <p><label>Name</label> <br />
    <asp:TextBox ID="albumName" runat="server" /> </p>
    
    <p><label>Description</label> <br />
    <asp:TextBox ID="description" TextMode="MultiLine" Height="200" Width="300" runat="server" /> </p>
    
    <asp:LinkButton ID="addAlbumLinkButtom" runat="server" Text="Save"  CssClass="buttonstyle" onclick="addAlbum_Click" />
    <chucksoft:Paragraph ID="message" runat="server" />

</div>