<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GalleryLandingView.ascx.cs" Inherits="Chucksoft.Templates.Default.GalleryLandingView" %>

<div id="galleryviewlanding">

            <h1><asp:Literal ID="galleryTitle" runat="server" /></h1>
            
            <div>
                
                <asp:Panel ID="firstImagePanel" runat="server">
                    <asp:Literal ID="firstImageLiteral" runat="server" />                                
                </asp:Panel>
                
                <asp:Panel ID="secondImagePanel" runat="server">
                    <asp:Literal ID="secondImageLiteral" runat="server" />                
                </asp:Panel>
                
                <asp:Panel ID="thirdImagePanel" runat="server">
                    <asp:Literal ID="thirdImageLiteral" runat="server" />                
                </asp:Panel>
                
                <div style="clear:both;"></div>
            
            </div>
          
            <chucksoft:Paragraph ID="galleryDescription" runat="server" />    
</div>
