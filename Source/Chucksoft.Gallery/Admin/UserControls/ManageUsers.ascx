<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManageUsers.ascx.cs" Inherits="Chucksoft.Admin.UserControls.ManageUsers" %>
<div style="margin-top:25px;">
    <h1>Users (<a href="Default.aspx?a=Users" title="Add a new user">add new</a>)</h1>
    <asp:GridView ID="manageUsers" AutoGenerateColumns="false" 
        AlternatingRowStyle-CssClass="alt"  CssClass="galleryimages" 
        DataKeyNames="UserID" runat="server" 
        onrowediting="manageUsers_RowEditing" >
    <Columns>
        <asp:TemplateField ItemStyle-CssClass="selectedRow" HeaderStyle-CssClass="selectedRow" >
            <HeaderTemplate >
                <asp:CheckBox ID="selectAllRows" runat="server" /> <br /> <label>ALL</label>
            </HeaderTemplate>
            <ItemTemplate>
                <asp:CheckBox ID="selectedRow" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Name">
            <ItemTemplate>
                <asp:Literal ID="NameLiteral" Text='<%# Eval("FirstName") + " " + Eval("LastName") %>'  runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Email">
            <ItemTemplate>
                <asp:HyperLink ID="galleryEditLink" NavigateUrl='mailto:<%# Eval("Email")%>' ToolTip='<%# "Send" + Eval("FirstName") + " " + Eval("LastName") %> email'  Text='<%# Eval("Email")%>' runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Role">
            <ItemTemplate>
                <asp:Literal ID="roleLiteral" runat="server" Text='Administrator' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Images">
            <ItemTemplate>
                <asp:Literal ID="Images" runat="server" Text='4' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:CommandField ButtonType="Link" ShowEditButton="true"  EditText="Edit" />
    </Columns>
    </asp:GridView>

    <p><asp:LinkButton ID="deleteGalleriesButton" runat="server" 
            Text="Delete Selected Users" CssClass="buttonstyle" 
            OnClientClick="confirm_delete('Are you sure you want to delete selected users?');" 
            onclick="deleteGalleriesButton_Click" /></p>

    <chucksoft:Paragraph ID="message" runat="server" />
</div>