<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SeminarskaREST.Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard</title>
    <link href="./CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="./JS/jquery-3.2.1.min.js"></script>
</head>
<body>
    <div class="container">
        <form runat="server">
            <asp:GridView ID="T_movies" runat="server" CssClass="table table-striped"></asp:GridView>
        </form>
    </div>

    <script src="JS/Dashboard.js"></script>
</body>
</html>
