<%@ Page Language="C#" MasterPageFile="~/Admin/MangeGallery.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="Chucksoft.Admin.Settings" Title="Settings - PhotoGallery - By Chucksoft" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content">  
    
        <h1>Gallery Settings</h1>
        
        <table class="settings" >
            <tr>
                <th scope="row">PhotoGallery Title</th>
                <td><asp:TextBox ID="galleryTitle" runat="server" /></td>
            </tr>
            <tr>
                <th scope="row">Presentation (<a href="Help.aspx#Views" title="Learn more about the different presentations PhotoGallery has to offer">?</a>)</th>
                <td>
                    <asp:RadioButtonList ID="presentationModeRadioButtonList" CssClass="presentationselection" runat="server" >
                        <asp:ListItem Text="Gallery" Selected="True" ></asp:ListItem>
                        <asp:ListItem Text="Album" ></asp:ListItem>
                        <asp:ListItem Text="Random" ></asp:ListItem>
                        <asp:ListItem Text="Single" ></asp:ListItem> 
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <th scope="row">Storage (<a href="Help.aspx#Views" title="Learn more about the different presentations PhotoGallery has to offer">?</a>)</th>
                <td>
                    <asp:DropDownList ID="storageSelection" runat="server" >
                        <asp:ListItem Text="Internal Storage" Value="SqlCe" />
                        <asp:ListItem Text="Sql Server" Value="SqlServer" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th scope="row">Theme (<a href="Help.aspx#Views" title="Learn more about the different presentations PhotoGallery has to offer">?</a>)</th>
                <td>
                    <asp:DropDownList ID="themeDropdown" runat="server" />
                </td>
            </tr>
            <tr>
                <th scope="row">Thumbnail Size</th>
                <td>
                    <p><label>Max. Height</label> : <asp:TextBox ID="thumbnailHeight" runat="server" Width="30" /></p>
                    <p><label style="padding-right:5px;">Max. Width</label>  : <asp:TextBox ID="thumbnailWidth" runat="server" Width="30" /></p>
                </td>
            </tr>
            <tr >
                <th class="settingstablebottom" scope="row">Image Size</th>
                <td class="settingstablebottom">
                    <p><label>Max. Height</label> : <asp:TextBox ID="fullsizeHeight" runat="server" Width="30" /></p>
                    <p><label style="padding-right:5px;">Max. Width</label> : <asp:TextBox ID="fullsizeWidth" runat="server" Width="30" /></p>                    
                </td>
            </tr>
        </table>
        
        <p><asp:LinkButton ID="saveLinkButton" runat="server" Text="Save Settings" CssClass="buttonstyle" onclick="saveButton_Click" /></p>

        <chucksoft:Paragraph ID="message" runat="server" />

</div>
</asp:Content>
