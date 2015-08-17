<%@ Control Language="C#" AutoEventWireup="true" Inherits="Chucksoft.Web.Presentation.Views.PhotoView" %>

<div id="photosview">
     
    <asp:Repeater ID="photoRepeater" runat="server" 
        onitemdatabound="photoRepeater_ItemDataBound">
        <ItemTemplate>
            <div class="landscapeview">
                <asp:Literal ID="imageLiteral" runat="server" />
            </div>
        </ItemTemplate>
    </asp:Repeater>
    
</div>