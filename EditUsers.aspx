<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Route.Master" CodeBehind="EditUsers.aspx.vb" Inherits="Routes.EditUsers" %>


<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">

    <div class="largeOuterDiv">

        <div class="topLeftOuterDiv topLeftOuterDivNPS topLeftOuterDivUsers">
            <div class="enterJobsTitleDiv">
                Edit Users
            </div>

            <asp:GridView ID="grdContact" CssClass="EditUsersGrid ecc_table" runat="server" AutoGenerateColumns="false" DataKeyNames="username" OnRowEditing="grdContact_RowEditing" ShowFooter="false" AlternatingRowStyle-BackColor="silver" > 
                <Columns> 
                    <asp:TemplateField HeaderText="Id"  HeaderStyle-HorizontalAlign="Left" Visible="false"> 
                        <EditItemTemplate> 
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                        </EditItemTemplate> 
                        <ItemTemplate> 
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("id") %>'></asp:Label> 
                        </ItemTemplate> 
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Username"  HeaderStyle-HorizontalAlign="Left"> 
                        <EditItemTemplate> 
                            <asp:Label ID="lblusername" runat="server" Text='<%# Bind("username") %>'></asp:Label>
                        </EditItemTemplate> 
                        <ItemTemplate> 
                            <asp:Label ID="lblusername" runat="server" Text='<%# Bind("username") %>'></asp:Label> 
                        </ItemTemplate> 
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="First Name" HeaderStyle-HorizontalAlign="Left"> 
                        <EditItemTemplate> 
                            <asp:TextBox ID="txtfirstName" runat="server" Text='<%# Bind("firstName") %>'></asp:TextBox> 
                        </EditItemTemplate> 
                        <FooterTemplate> 
                            <asp:TextBox ID="txtNewFirstName" runat="server" ></asp:TextBox> 
                        </FooterTemplate> 
                        <ItemTemplate> 
                            <asp:Label ID="lblfirstName" runat="server" Text='<%# Bind("firstName") %>'></asp:Label> 
                        </ItemTemplate> 
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Last Name" HeaderStyle-HorizontalAlign="Left"> 
                        <EditItemTemplate> 
                            <asp:TextBox ID="txtlastName" runat="server" Text='<%# Bind("lastName") %>'></asp:TextBox> 
                        </EditItemTemplate> 
                        <FooterTemplate> 
                            <asp:TextBox ID="txtNewLastName" runat="server" ></asp:TextBox> 
                        </FooterTemplate> 
                        <ItemTemplate> 
                            <asp:Label ID="lbllastName" runat="server" Text='<%# Bind("lastName") %>'></asp:Label> 
                        </ItemTemplate> 
                    </asp:TemplateField> 
                    
                    <asp:TemplateField HeaderText=" " ShowHeader="False" HeaderStyle-HorizontalAlign="Left"> 
                        <ItemTemplate> 
                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit" CssClass="button blue gridbutton editUsersButton" Text="Edit"></asp:LinkButton> 
                        </ItemTemplate> 
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText=" " ShowHeader="False" HeaderStyle-HorizontalAlign="Left"> 
                        <ItemTemplate> 
                            <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete" CssClass="button blue gridbutton editUsersButton" Text="Delete"></asp:LinkButton> 
                        </ItemTemplate> 
                    </asp:TemplateField> 
                </Columns> 
            </asp:GridView> 

        </div>

        <div class="topRightOuterDiv">

            <div class="topRightOuterButtonDiv">
                <asp:LinkButton ID="btnNew" runat="server" CssClass="button blue" Text="Return" />
            </div>
            <div style="clear:both;"></div>

        </div>

    </div>

</asp:Content>
