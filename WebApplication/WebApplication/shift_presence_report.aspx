<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shift_presence_report.aspx.cs" Inherits="WebApplication.shift_presence_report" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="lFromH" style="text-align: center">
    
        <asp:Label ID="lShiftPresenceHeader" runat="server" Text="Employees on Shift Report" style="font-weight: 700; text-decoration: underline; font-size: xx-large"></asp:Label>
        <br />
        <br />
        <br />
        <br />
        From Hour:<asp:TextBox ID="tbFromH" runat="server" Height="16px" Width="159px" TextMode="DateTimeLocal" OnTextChanged="tbFromH_TextChanged"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;
        <asp:Label ID="lToH" runat="server" Text="To Hour: "></asp:Label>
        <asp:TextBox ID="tbToHour" runat="server" Height="16px" Width="170px" TextMode="DateTimeLocal" OnTextChanged="tbToHour_TextChanged"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="lNotification" runat="server"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="bCreate" runat="server" Height="27px" Text="Create" OnClick="bCreate_Click" />
        <br />
        <br />
        <br />
        <br />
        <hr width ="75%" />
        <br />
        <br />
        <asp:Label ID="lOnshift" runat="server" style="font-size: x-large" Visible="False"></asp:Label>
        <br />
        <br />
        <br />
    
        <asp:GridView ID="gvOnshift" runat="server" OnSelectedIndexChanged="gvOnshift_SelectedIndexChanged">
        </asp:GridView>
    
        <br />
    
    </div>
    </form>
</body>
</html>
