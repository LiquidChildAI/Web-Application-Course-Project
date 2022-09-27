<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin_reports.aspx.cs" Inherits="WebApplication.admin_reports" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="text-align: center">
    <form id="admin_report_form" runat="server">
        <div style="text-align: left">
            <asp:Label ID="lHello" runat="server"></asp:Label>
        </div>
        <h1 style="text-align: center">דוחות כללים</h1>
        <p>
            &nbsp;</p>
        <asp:HyperLink ID="daily_hours_per_month" runat="server" NavigateUrl="~/daily_hours_per_month_report.aspx" Target="_blank">דו"ח שעות יומי לפי חודש לעובד יחיד  --  Daily Hours per Month for Single user</asp:HyperLink>
        <br />
        <br />
        <asp:HyperLink ID="hlAllEmployees" runat="server" NavigateUrl="~/all_employees_report.aspx" Target="_blank">הצגת כל העובדים</asp:HyperLink>
    <p>
        <asp:HyperLink ID="hlMonthlyHoursForAllEmplloyees" runat="server" NavigateUrl="~/monthly_working_hours_all_employees.aspx">שעות חודשיות לכל העובדים</asp:HyperLink>
        </p>
    <p>
        <asp:HyperLink ID="hlShiftReport" runat="server" NavigateUrl="~/shift_presence_report.aspx" Target="_blank">נוכחות משמרת</asp:HyperLink>
        </p>
    <p>
        <asp:HyperLink ID="hlAdminSReports" runat="server" NavigateUrl="~/admin_shift_reports.aspx" Target="_blank">Admin Shift Reports</asp:HyperLink>
        </p>
    </form>
    <p>
        &nbsp;</p>
</body>
</html>
