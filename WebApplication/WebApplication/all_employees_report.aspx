<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="all_employees_report.aspx.cs" Inherits="WebApplication.all_employees_report" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    
        <div style="text-align: center">
    
        <asp:Label ID="lAllEHeader" runat="server" Text="All Employees Report"  style="font-weight: 700; text-decoration: underline; font-size: xx-large"></asp:Label>
        <br />
        <br />
        <br />
    
        <asp:GridView ID="gvAllEmployees" runat="server" style="text-align: center">
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
