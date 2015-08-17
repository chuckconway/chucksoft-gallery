<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Chucksoft.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Login - PhotoGallery - By Chucksoft</title>
    <style type="text/css">@import url("GalleryStyles.css");</style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        
            <div id="login">
                
                <p>
                    <label>Email Address</label> <br />
                    <asp:TextBox ID="emailAddressTextBox" runat="server" />
                </p>
                
                <p>
                    <label>Password</label><br />
                    <asp:TextBox ID="passwordTextBox" TextMode="Password" runat="server" />                
                </p>
                
                <p style="font-weight:normal;font-size:11px;">Forgot your password?  <a href="ResetPassword.aspx" title="Forgot your password? Reset it">Reset your password</a></p>
                
                <asp:Button id="loginButton" CssClass="loginbutton" runat="server" Text="Login" onclick="loginButton_Click" />
                 <chucksoft:Paragraph ID="message" runat="server" />
            </div>
            
           
        
        </div>
    </form>
</body>
</html>
