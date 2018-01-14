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
            <asp:GridView ID="T_movies" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" 
                HorizontalAlign="Center" ShowFooter="true" DataKeyNames="MovieID" ShowHeaderWhenEmpty="true"
                OnRowCommand="AddNewMovie"
                OnRowEditing="EditMovie" 
                OnRowCancelingEdit="CancelEditMovie" 
                OnRowDeleting="DeleteMovie" 
                OnRowUpdating="UpdateMovie"
                EnableViewState="true"
                >

                <Columns>
                    <asp:TemplateField HeaderText="Title">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Title") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox EnableViewState="true" ID="TB_movie_title" Text='<%# Eval("Title") %>' runat="server"></asp:TextBox>
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
                            <asp:TextBox EnableViewState="true" ID="TB_movie_description" Text='<%# Eval("Description") %>' runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TB_movie_description_footer" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Date") != null ? Convert.ToDateTime(Eval("Date")).ToString("dd. MM, yyyy") : "No Date" %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox EnableViewState="true" ID="TB_movie_date" Text='<%# Eval("Date") != null ? Convert.ToDateTime(Eval("Date")).ToString("dd. MM, yyyy") : "No Date" %>' TextMode="Date" runat="server"></asp:TextBox>
                            <asp:HiddenField ID="TB_movie_date_hidden" runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TB_movie_date_footer" TextMode="Date" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Rating">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Rating") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox EnableViewState="true" ID="TB_movie_rating" Text='<%# Eval("Rating") %>' TextMode="Number" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TB_movie_rating_footer" TextMode="Number" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="./Img/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px" />
                            <asp:ImageButton ImageUrl="./Img/trash.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ImageUrl="./Img/save.png" runat="server" CommandName="Update" ToolTip="Update" Width="20px" Height="20px" />
                            <asp:ImageButton ImageUrl="./Img/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton ImageUrl="./Img/add.png" runat="server" CommandName="AddNew" ToolTip="AddNew" Width="20px" Height="20px" />
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            
            <br />
            <asp:Label ID="L_movies_success" Text="" runat="server" ForeColor="Green"></asp:Label>

            <br />
            <asp:Label ID="L_movies_error" Text="" runat="server" ForeColor="Red"></asp:Label>

            <asp:Button ID="B_logout" runat="server" Text="Logout" CssClass="btn btn-primary" OnClick="B_logout_Click" />

        </form>
    </div>

    <script src="./JS/Dashboard.js"></script>
</body>
</html>
