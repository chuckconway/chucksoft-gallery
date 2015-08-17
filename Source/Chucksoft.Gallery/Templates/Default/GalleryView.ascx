<%@ Control Language="C#" AutoEventWireup="true" Inherits="Chucksoft.Web.Presentation.Views.GalleryView" %>

<div id="galleryview">
    <asp:Repeater ID="galleryViewRepeater" runat="server" onitemdatabound="galleryViewRepeater_ItemDataBound" >
        <ItemTemplate>           
             <div class="galleryviewimage">
                  <div style="height:160px;">
                    <asp:Literal ID="imageLiteral" runat="server" />
                     <p class="gallerytitle"><asp:Literal ID="titleLiteral" runat="server" /></p> 
                  </div> 
                  <p class="galleryphotocount">(<asp:Literal ID="photoCount" runat="server" />)</p>                 
            </div>                 
        </ItemTemplate>
    </asp:Repeater>
    <div style="clear:both;"></div>    
</div>