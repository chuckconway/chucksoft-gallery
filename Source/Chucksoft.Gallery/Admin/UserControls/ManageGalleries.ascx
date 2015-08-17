<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManageGalleries.ascx.cs" Inherits="Chucksoft.Admin.UserControls.ManageGalleries" %>

<div style="margin-top:25px;">
    <h1>Galleries (<a href="Default.aspx?a=Galleries" title="Add a new gallery">add new</a>)</h1>
    <asp:GridView ID="manageGalleries" AutoGenerateColumns="false" 
        AlternatingRowStyle-CssClass="alt"  CssClass="galleryimages" 
        DataKeyNames="GalleryID" runat="server" 
        onrowdatabound="manageGalleries_RowDataBound" 
        onrowcancelingedit="manageGalleries_RowCancelingEdit" 
        onrowediting="manageGalleries_RowEditing" 
        onrowupdating="manageGalleries_RowUpdating" >
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
                <asp:HyperLink ID="galleryEditLink" runat="server" />
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="editGalleryName" runat="server" />
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Description">
            <ItemTemplate>
                <asp:Literal ID="galleryDescriptionLink" runat="server" Text='<%# Eval("Description") %>' />
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="editDescriptionName" runat="server" />
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Photos">
            <ItemTemplate>
                <asp:Literal ID="galleryPhotoCountLink" runat="server" Text='<%# Eval("PhotoCount") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:CommandField ButtonType="Link" EditText="Edit" ShowEditButton="true" />
    </Columns>
    </asp:GridView>

    <p><asp:LinkButton ID="deleteGalleriesButton" runat="server" CssClass="buttonstyle" OnClientClick="confirm_delete('Are you sure you want to DELETE the selected galleries? All images within these galleries will be also be deleted! You will not be able to recover these photos. To move photos to another galleries, click the photos link above.');" Text="Delete Selected Galleries" /></p>

    <chucksoft:Paragraph ID="message" runat="server" />
</div>