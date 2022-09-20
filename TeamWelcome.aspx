<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Route.Master" CodeBehind="TeamWelcome.aspx.vb" Inherits="Routes.TeamWelcome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">

    <div class="largeOuterDiv">

        <div class="topLeftOuterDiv topLeftOuterDivNPS topLeftOuterDivUsers">
            <div class="enterJobsTitleDiv"> 

                <div id="teamWelcomeSignIn" >
                    <asp:GridView ID="gridTeamMembers" runat="server" CellPadding="0" CellSpacing="0" AutoGenerateColumns="false"  OnRowCreated="gridTeamMembers_RowCreated" CssClass="teamWelcomeGrid" >
                        <HeaderStyle CssClass="teamWelcomeGridHeader" />
                        <Columns >
                            <asp:TemplateField HeaderText="" >
                                <ItemTemplate >
                                    <asp:CheckBox ID="checkTeamMember" runat="server" />
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" >
                                <ItemTemplate >
                                    <asp:Label runat="server" Text='<%# Bind("userID") %>' id="lblUserID"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="teamWelcomeUserIDColumn" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="teamWelcomeUserIDColumn" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Team Member">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "firstName")%>&nbsp;&nbsp;<%#DataBinder.Eval(Container.DataItem, "lastName")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="teamWelcomeNameColumn" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Route">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "routeNumber")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="teamWelcomeRouteColumn" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sign In">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("signIn") %>' id="lblSignIn"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="teamWelcomeSignInColumn" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sign Out">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "signOut")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="teamWelcomeSignOutColumn" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

                <div id="teamWelcomeSignInButton" >
                    Select Route:
                    <asp:DropDownList ID="ddlRouteNumbers" runat="server" CssClass="dropdownRoute" />
                    <asp:LinkButton ID="btnLogIn" runat="server" CssClass="blue button" Text="Sign In" />
                </div>

                <div id="teamWelcomeEditRouteButton" runat="server" class="teamWelcomeEditRouteButton" >
                    Edit Route:
                    <asp:DropDownList id="ddlEditRoute" runat="server" CssClass="dropdownRoute" />
                    <asp:LinkButton ID="btnEdit" runat="server" CssClass="button blue" Text="Edit Route"  />
                </div>

                <div id="teamWelcomeSignOutButton" >
                    <asp:LinkButton ID="btnLogOut" runat="server" CssClass="button blue" Text="Sign Out" />
                </div>

            </div>
        </div>

        <div class="topRightOuterDiv">

            <div class="topRightOuterButtonDiv">
                <asp:LinkButton ID="btnReturn" runat="server" CssClass="button blue" Text="Return" />            
            </div>
            <div style="clear:both;"></div>

        </div>

    </div>


</asp:Content>
