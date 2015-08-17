<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddPhotos.ascx.cs" Inherits="Chucksoft.Admin.UserControls.AddPhoto" %>
<%@ Register Assembly="System.Web.Silverlight" Namespace="System.Web.UI.SilverlightControls" TagPrefix="asp" %>

        <div style="margin-top:20px;height:450px;" >
            <asp:Silverlight ID="Xaml1" runat="server" Source="~/ClientBin/Chucksoft.Silverlight.Controls.xap" MinimumVersion="2.0.30523" Width="100%" Height="100%" />
        </div>


