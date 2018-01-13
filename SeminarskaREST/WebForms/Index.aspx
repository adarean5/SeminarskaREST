<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SeminarskaREST.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Index</title>
    <link href="../CSS/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
        <div class="col-md-4 mx-auto">
            <form id="form1" runat="server">
            
                <div class="form-group">
                    <asp:Label runat="server" Text="Username"></asp:Label>
                    <asp:TextBox ID="TB_username" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            
                <div class="form-group">
                    <asp:Label runat="server" Text="Password"></asp:Label>
                    <asp:TextBox ID="TB_password" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </div>

                <asp:Button ID="B_login" runat="server" Text="Login" CssClass="btn btn-primary float-left" OnClick="B_login_Click" />

                <asp:Button ID="B_register" runat="server" Text="Don't have an account?" CssClass="btn btn-default float-right" OnClick="B_register_Click" />
            </form>
        </div>
    </div>
</body>
</html>
