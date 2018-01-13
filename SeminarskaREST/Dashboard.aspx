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
            <asp:GridView ID="T_movies" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">

                <HeaderStyle CssClass="thead-inverse" />

                <Columns>
                    <asp:TemplateField HeaderText="Title">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Title") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TB_movie_title" Text='<%# Eval("Title") %>' runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TB_movie_title_footer" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Description") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TB_movie_description" Text='<%# Eval("Description") %>' runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TB_movie_description_footer" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Date") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TB_movie_date" Text='<%# Eval("Date") %>' runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TB_movie_date_footer" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Rating">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Rating") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TB_movie_rating" Text='<%# Eval("Rating") %>' runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TB_movie_rating_footer" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="./Img/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px" />
                            <asp:ImageButton ImageUrl="./Img/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </form>
    </div>

    <script src="./JS/Dashboard.js"></script>
</body>
</html>
