<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="monthly_working_hours_all_employees.aspx.cs" Inherits="WebApplication.monthly_working_hours_all_employees" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    
        <br />
        <asp:Label ID="lAllEMonthlyHoursHeader" runat="server" Text="All Employees Monthly Working Hours" style="text-decoration: underline; font-weight: 700; font-size: xx-large"></asp:Label>
        <br />
        <br />
        <br />
        <br />
        <asp:Label ID="lMonth" runat="server" Text="Month : "></asp:Label>
        <asp:TextBox ID="tbMonth" runat="server" Height="16px" Width="37px" MaxLength="2"></asp:TextBox>
            
        &nbsp;&nbsp;&nbsp;
            
        <asp:Label ID="lYear" runat="server" Text="Year : "></asp:Label>
        <asp:TextBox ID="tbYear" runat="server"  Height="16px" Width="51px" MaxLength="4"></asp:TextBox>
        <br />
        <br />
        
        
        <asp:Label ID="lNotiftication" runat="server" ></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="bCreateReport" runat="server" Text="Create" OnClick="bCreateReport_Click" />
        <br />
                <br />

    <hr width ="75%" />

          <br />
                <br />
        <asp:GridView ID="gvMonthlyHours" runat="server">
        </asp:GridView>
    
        <br />
    
    </div>
    </form>
</body>
</html>
