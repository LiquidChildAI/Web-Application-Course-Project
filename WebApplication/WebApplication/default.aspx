<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="WebApplication._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="user_pass_form" runat="server">
    
    <div id='username' >
     <% Response.Write(DateTime.Now);   %>
     <br />
    Username: <asp:TextBox ID="tusername" runat="server" ></asp:TextBox> <br />
    Password: <asp:TextBox ID="tpassword" runat="server" TextMode="Password" ></asp:TextBox> <br />
    <asp:Button ID="userBClick" runat="server" Text="Sign in" 
            onclick="userBClick_Click" />
        <asp:CheckBox ID="adminCheckBox" runat="server" OnCheckedChanged="adminCheckBox_CheckedChanged" Text="admin?" />
        <br />
        <asp:Label ID="lLastSigned" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lSigned" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
