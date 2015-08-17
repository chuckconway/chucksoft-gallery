<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddGalleries.ascx.cs" Inherits="Chucksoft.Admin.UserControls.AddGallery" %>

<div class="addstyles" >
    
    <h1>Add Gallery</h1>
     
    <p>
        <label>Name</label> <br />
        <asp:TextBox ID="galleryName" runat="server" />
    </p>
    
    <p><label>Description</label> <br />
    <asp:TextBox ID="description" TextMode="MultiLine" Height="200" Width="300" runat="server" /> </p>
    
    <asp:LinkButton ID="addGalleryLinkButton" runat="server" Text="Save" CssClass="buttonstyle" onclick="addGallery_Click" />

    <chucksoft:Paragraph ID="message" runat="server"  />
</div>
