<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="daily_hours_per_month_report.aspx.cs" Inherits="WebApplication.daily_hours_per_month_report" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            font-size: xx-large;
            text-decoration: underline;
            text-align: right;
        }
    </style>
</head>
<body>
    <form id="daily_hours_report_from" runat="server">
        <div style="text-align: center" >
            <span class="auto-style1">דו&quot;ח שעות חודשי לפי עובד<br />
            <br />
            </span>
            <div>
                <asp:Label ID="lUsername" runat="server" Text="Username: "></asp:Label>
             &nbsp;&nbsp;
            <asp:TextBox ID="tbUsername" runat="server" Width="124px"></asp:TextBox>
            &nbsp;&nbsp;
            
            <br />
            <br />
            
                
               
               
             
                 
               
           
            
            
                
                <asp:Label ID="lMonth" runat="server" Text="Month: (MM)"></asp:Label>
             &nbsp;&nbsp;                
                <asp:TextBox ID="tbMonth" runat="server" Height="17px" Width="38px"></asp:TextBox>
            &nbsp;&nbsp;,&nbsp;&nbsp;
            
               
                
            
             <asp:Label ID="lYear" runat="server" Text="Year: (YYYY)"></asp:Label>
             &nbsp;&nbsp;
                <asp:TextBox ID="tbYear" runat="server" Height="17px" Width="38px"></asp:TextBox>
            &nbsp;&nbsp;
            
            
            <br />
            <br />

            <asp:Label ID="lNotification" runat="server" ></asp:Label>
             &nbsp;&nbsp;
            <asp:Button ID="bCreateReport" runat="server" Text=" בצע " OnClick="bCreateReport_Click"  />
            &nbsp;&nbsp;
            <br />
            <br />
            <br />
               
               
            
            </div>
             <hr width ="75%" />
            <div style="text-align: center">
                <asp:Label ID="lReportHeader" runat="server" style="font-size: x-large"  ></asp:Label>
             <br />
                <br />
                <br />
            <asp:GridView ID="gvDailyHoursPerMonth" runat="server" ></asp:GridView>
            </div>
        
            <br />

        
    </div>
    </form>
</body>
</html>
