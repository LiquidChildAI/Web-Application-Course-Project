<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin_shift_reports.aspx.cs" Inherits="WebApplication.working_now_report" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
        #workers_working_now_report {
            height: 724px;
        }
    </style>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <div id="workers_working_now_report" >
        <h1 class="auto-style1">Admin Shift Report</h1>
        <br />
        
        <asp:Label ID="Label1" runat="server" Text="Day : "></asp:Label>
&nbsp;
        <asp:TextBox ID="tbDay" runat="server" Height="17px" MaxLength="2" Width="46px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
        
        <asp:Label ID="lMonth" runat="server" Text="Month : "></asp:Label>
        <asp:TextBox ID="tbMonth" runat="server" OnTextChanged="tbMonth_TextChanged" Width="47px" MaxLength="2"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lYear" runat="server" Text="Year : "></asp:Label>
        <asp:TextBox ID="tbYear" runat="server" OnTextChanged="tbYear_TextChanged" Width="78px" MaxLength="4"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="lNotification" runat="server" ></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="bGetReports" runat="server" Text="Get Reports" OnClick="bGetReports_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        
        <br />        
        <br />
         <br />
        <hr width="75%" />
        <br />        
        <br />
       <br />        
        <asp:Label ID="lReportsLogHeder" runat="server" style="font-size: x-large" Visible="False"></asp:Label>
        <br />
        <br />


    
        


    
        <asp:Button ID="bDeleteReport" runat="server" Height="29px" OnClick="bDeleteReport_Click" Text="Delete Report" Width="107px" Visible="False" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;


    
        


    
        <asp:TextBox ID="lReports" runat="server" Enabled="False" Height="216px" TextMode="MultiLine" Visible="False" Width="558px"></asp:TextBox>


    
        


    
    </div>
    <p style="text-align: center">
       
        </p>
    </form>
    </body>
</html>
