<%@ Control Language="C#" AutoEventWireup="true" Inherits="Chucksoft.Web.Presentation.Views.AlbumView" %>

<asp:Repeater ID="albumViewRepeater" runat="server" >
    <ItemTemplate>
        <div >
        
            <div>
                <chucksoft:Image ID="albumImage" runat="server" />
            </div>
            
            <ul>
                <li><asp:Literal ID="title" runat="server" /></li>
                <li><asp:Literal ID="photoCount" runat="server" /></li>
            </ul>
                        
        </div>    
    </ItemTemplate>
</asp:Repeater>