<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManagePhotos.ascx.cs"  Inherits="Chucksoft.Admin.UserControls.ManagePhotos" %>

<%--    <asp:UpdatePanel ID="photosUpdatePanel" runat="server"  RenderMode="Block" UpdateMode="Always">
    <ContentTemplate> --%>   
    <div style="margin-top:30px;margin-bottom:15px;">
        <h1>Photos (<a href="Default.aspx?a=Photos" title="Add a new photos">add new</a>)</h1>
        <p style="margin-bottom:3px;"><strong>Gallery</strong></p>
        <asp:DropDownList ID="galleriesDropDownList" runat="server" AutoPostBack="true" onselectedindexchanged="galleriesDropDownList_SelectedIndexChanged" />
    </div>
    
    <asp:GridView ID="managePhotos"  runat="server" 
    OnRowDeleting="managePhotos_RowDeleting" 
    OnRowCancelingEdit="managePhotos_RowCancelingEdit"  
    OnRowUpdating="managePhotos_RowUpdating"  AllowPaging="true" 
    DataKeyNames="PhotoID"  OnRowEditing="managePhotos_RowEditing" 
    AutoGenerateColumns="false" AlternatingRowStyle-CssClass="alt"  
    CssClass="galleryimages"  onrowdatabound="managePhotos_RowDataBound" 
    onpageindexchanging="managePhotos_PageIndexChanging" >
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="selectedRow" HeaderStyle-CssClass="selectedRow" >
                <HeaderTemplate >
                    <asp:CheckBox ID="selectAllRows" runat="server" /> <br /> <label>ALL</label>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="selectedRow" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-CssClass="imagerowstyle">
                <ItemTemplate>
                   <asp:Literal ID="imageLiteral" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Title">
                <EditItemTemplate>
                    <asp:TextBox ID="titleTextBox" runat="server" Text='<%# Eval("Title") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Literal ID="titleLiteral" runat="server" Text='<%# Eval("Title") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-CssClass="photodescription" HeaderText="Description">
                <EditItemTemplate>
                    <asp:TextBox ID="descriptionTextBox" CssClass="photodescriptiontextbox" TextMode="MultiLine" runat="server" Text='<%# Eval("Description") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Literal ID="descriptionLiteral" runat="server" Text='<%# Eval("Description") %>' />
                </ItemTemplate>
            </asp:TemplateField>
<%--            <asp:BoundField HeaderText="Date" HtmlEncode="false" DataFormatString="{0:D}" DataField="DateTaken" />--%>
            <asp:TemplateField HeaderText="Date" ItemStyle-CssClass="dateRow" HeaderStyle-CssClass="dateRow"  >
                <EditItemTemplate>
                    <p><asp:TextBox ID="photoDate" runat="server" CssClass="datetextbox" Text='<%# Eval("DateTaken") %>' /> <ajax:CalendarExtender PopupButtonID="calendarImage" TargetControlID="photoDate" ID="dateExtender" PopupPosition="BottomRight" runat="server" CssClass="calendar" /><asp:Image ID="calendarImage" runat="server" CssClass="calendarimagebutton" ImageUrl="~/Admin/Images/Calendar.png" /></p>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Literal ID="dateLiteral" Text='<%# Eval("DateTaken") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ButtonType="Link" ItemStyle-CssClass="imagerowstyle" EditText="Edit" ShowEditButton="true" />                     
        </Columns>  
    </asp:GridView>
    
        <strong>Selected Photos:</strong>
        
        <asp:RadioButton GroupName="SelectedPhotos" AutoPostBack="true"  ID="movePhotos" Checked="true" 
            Text="Move" runat="server" oncheckedchanged="movePhotos_CheckedChanged" />
        <asp:RadioButton GroupName="SelectedPhotos" AutoPostBack="true" ID="deletePhotos" oncheckedchanged="movePhotos_CheckedChanged" Text="Delete" runat="server" />
        
        <asp:Panel ID="movePhotosPanel" runat="server" >
            
         <p>
             <label>Select Gallery</label><br />
             <asp:DropDownList ID="moveGalleriesDropDown" runat="server" />
        </p>
        
        <p>
             <asp:LinkButton ID="MovePhotosLinkButton" CssClass="buttonstyle" 
                 OnClientClick="confirm_delete('Are you sure you want to MOVE the selected photos to a new gallery?');" 
                 runat="server" Text="Move Selected Photos" 
                 onclick="MovePhotosLinkButton_Click" />
        </p>
        
        </asp:Panel>
        
        <asp:Panel ID="deletePhotosPanel" Visible="false" runat="server" >        
           <p> <asp:LinkButton ID="deleteSelectedPhotos" CssClass="buttonstyle" runat="server" 
                   OnClientClick="confirm_delete('Are you sure you want to DELETE the selected photos? You will not be able to recover these photos.');" 
                   Text="Delete Selected Photos" onclick="deleteSelectedPhotos_Click" 
                   style="font-weight: 700" />  </p>  
        </asp:Panel>       

        <chucksoft:Paragraph ID="message" runat="server" />

<%--    
    </ContentTemplate>
    </asp:UpdatePanel>--%>