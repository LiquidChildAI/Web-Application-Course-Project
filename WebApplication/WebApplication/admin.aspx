<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="WebApplication.admin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #management_details {
            text-align: right;
        }

        .auto-style1 {
            text-align: center;
        }

        #adminForm {
        }

        .auto-style2 {
            text-align: right;
        }
    </style>
</head>
<body style="text-align: right">
    <form id="adminForm" runat="server">
        <div>
            <asp:Label ID="lHello" runat="server"></asp:Label>
            <h1><strong style="text-decoration: underline; text-align: right;">הכנסת עובד חדש</strong></h1>
            <div class="auto-style2">
                <br />
                <br />
                <asp:TextBox ID="tEID" runat="server" Style="margin-top: 0px" Width="119px" Height="25px" OnTextChanged="tEID_TextChanged" MaxLength="9" TextMode="Number"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;             
                <asp:Label ID="lEID" runat="server" Text="מס' ת.ז  "></asp:Label>
                &nbsp;&nbsp;&nbsp;                                   
                <br />
                <br />
                <asp:TextBox ID="tEFirstName" runat="server" Style="margin-top: 0px" Width="119px" Height="25px" OnTextChanged="tEFirstName_TextChanged"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;             
                <asp:Label ID="lEFirstName" runat="server" Text="שם פרטי  "></asp:Label>
                &nbsp;&nbsp;&nbsp;                                   
                <br />
                <br />
                <asp:TextBox ID="tELastname" runat="server" Style="margin-top: 0px" Width="119px" Height="25px" OnTextChanged="tELastname_TextChanged"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;             
                <asp:Label ID="lELastname" runat="server" Text="שם משפחה  "></asp:Label>
                &nbsp;&nbsp;&nbsp;                                   
                <br />
                <br />
                <asp:TextBox ID="tEUsername" runat="server" Style="margin-top: 0px" Width="119px" Height="25px" OnTextChanged="tEUsername_TextChanged"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;             
                <asp:Label ID="lEUsername" runat="server" Text="שם משתמש  "></asp:Label>
                &nbsp;&nbsp;&nbsp;                                   
                <br />
                <br />
                <asp:CheckBox ID="cbEAdmin" runat="server" Text="Admin Account" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                <asp:TextBox ID="tEPassword" runat="server" Style="margin-top: 0px" Width="119px" Height="25px" OnTextChanged="tEPassword_TextChanged"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;             
                <asp:Label ID="lEPassword" runat="server" Text="סיסמא  "></asp:Label>
                &nbsp;&nbsp;&nbsp;                                   
            

                <br />
                <br />
                <asp:Label ID="lAdminAddEmployee" runat="server" ></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;


                <asp:Button ID="bESubmit" runat="server" OnClick="bESubmit_Click" Text="בצע רישום" />
            </div>
        </div>

        <br />
        <br />



        <div id="management_details" align="center" class="auto-style1">

            <h1><strong style="text-decoration: underline">פרטים ניהוליים</strong></h1>
            <br />
            <br />

            <asp:Button ID="bChangeMinMonthlyHours" runat="server" Text="שנה" OnClick="bChangeMinMonthlyHours_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="tMinMonthlyHours" runat="server" Style="margin-top: 0px" Width="43px" Height="19px" OnTextChanged="tMinMonthlyHours_TextChanged"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;             
            <asp:Label ID="lMinMonthlyHours" runat="server" Text=":שעות עבודה מינימליות לחודש"></asp:Label>
            &nbsp;&nbsp;&nbsp; 
                                  
            <br />
            <br />

            <asp:Button ID="bChangeMaxMonthlyHours" runat="server" Text="שנה" OnClick="bChangeMaxMonthlyHours_Click" style="height: 29px" />
            &nbsp;&nbsp;&nbsp; 
            <asp:TextBox ID="tMaxMonthlyHours" runat="server" OnTextChanged="tMaxMonthlyHours_TextChanged" Height="19px" Style="margin-bottom: 0px" Width="42px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;                       
            <asp:Label ID="lMaxMonthlyHours" runat="server" Text=":שעות עבודה מקסימליות לחודש"></asp:Label>
            &nbsp;&nbsp;&nbsp;  
                                  
            <br />
            <br />

            <asp:Button ID="bChangeHourSalary" runat="server" Text="שנה" OnClick="bChangeHourSalary_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="tHourSalary" runat="server" OnTextChanged="tHourSalary_TextChanged" Height="17px" Style="margin-bottom: 2px" Width="36px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;  
            <asp:Label ID="lHourSalary" runat="server" Text=":שכר לשעה"></asp:Label>
            &nbsp;&nbsp;&nbsp;
           <br />
            <br />
            <asp:Button ID="bChangeExtraHourSalary" runat="server" Text="שנה" OnClick="bChangeExtraHourSalary_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="tExtraHourSalary" runat="server" Height="16px" Width="37px" OnTextChanged="tExtraHourSalary_TextChanged"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;           
            <asp:Label ID="lExtraHoursSalary" runat="server" Text="Label">:שכר לשעות נוספות</asp:Label>
            &nbsp;&nbsp;&nbsp;
            
        </div>
        <br />
        <br />
        <div id="shift_report">
            <h1>
                <asp:Button ID="bGeneralReports" runat="server" OnClick="bGeneralReports_Click" Text="דוחות כללים" />
            </h1>
            <h1><strong style="text-decoration: underline">דיווחי משמרות</strong></h1>

            <p>
                <asp:Label ID="lReportNotification" runat="server" ></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="bAddReport" runat="server" OnClick="bAddReport_Click" Text="הוסף דיווח" />
                &nbsp;&nbsp;
                 <asp:Button ID="bClean" runat="server" Text="נקה טקסט" OnClick="bClean_Click" />
                &nbsp;&nbsp;
                 <asp:TextBox ID="tReport" runat="server" Height="127px" MaxLength="500" OnTextChanged="tReport_TextChanged" TextMode="MultiLine" Width="586px"></asp:TextBox>

            </p>
        </div>




    </form>
</body>
</html>
