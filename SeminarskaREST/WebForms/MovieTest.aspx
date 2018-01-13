<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MovieTest.aspx.cs" Inherits="SeminarskaREST.MovieTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:dbmoviesConnectionString %>" SelectCommand="SELECT * FROM [Movies]"></asp:SqlDataSource>
        <asp:Label ID="Label1" runat="server" Text="Hello World"></asp:Label>
    </form>
</body>
</html>
