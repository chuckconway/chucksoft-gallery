<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddUsers.ascx.cs" Inherits="Chucksoft.Admin.UserControls.AddUser" %>

        <h1><asp:Literal ID="h1HeaderLiteral" runat="server" Text="Add New User" /></h1>
        
        <table class="settings" >
            <tr>
                <th scope="row">Email</th>
                <td><asp:TextBox ID="emailAddressTextBox" runat="server" /> (required, 50 characters max.)</td>
            </tr>
            <tr>
                <th scope="row">Password (twice)</th>
                <td><p style="margin-top:0px;"><asp:TextBox ID="passwordOneTextBox" TextMode="Password" runat="server" /></p>
                    <p style=margin-bottom:0px;"><asp:TextBox ID="passwordTwoTextBox" TextMode="Password" runat="server" /></p>
                </td>
            </tr>
            <tr>
                <th scope="row">First Name</th>
                <td>
                    <asp:TextBox ID="firstNameTextBox" runat="server" />
                </td>
            </tr>
            <tr>
                <th scope="row">Last Name</th>
                <td>
                    <asp:TextBox ID="lastNameTextBox" runat="server" />
                </td>
            </tr>
            <tr>
                <th scope="row">Website</th>
                <td>
                    <asp:TextBox ID="websiteTextBox" Width="300" runat="server" />
                </td>
            </tr>
            <tr>
                <th class="settingstablebottom" scope="row">Role</th>
                <td class="settingstablebottom" >
                    <asp:DropDownList ID="roleDropDownList" runat="server" >
                        <asp:ListItem Text="Site Owner" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        
        <p><asp:LinkButton ID="saveLinkButton" runat="server" Text="Add User" CssClass="buttonstyle" onclick="saveButton_Click" /></p>
        <chucksoft:Paragraph ID="message" runat="server" />

<%--
<p>
    <label>Email (50 characters max., required)</label><br />
    <asp:TextBox ID="emailTextBox" runat="server" />
</p>

<p>
    <label>Password (required)</label><br />
    <asp:TextBox ID="passwordTextBox" TextMode="Password" runat="server" />
</p>

<p>
    <label>Display Name</label><br />
    <asp:TextBox ID="displayName" runat="server" />
</p>

<p>
    <label>Website</label><br />
    <asp:TextBox ID="website" runat="server" />
</p>

<asp:Button ID="saveButton" runat="server" Text="Add User" onclick="saveButton_Click" />
<chucksoft:Paragraph ID="message" runat="server" />--%>