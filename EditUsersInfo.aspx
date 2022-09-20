<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Route.Master" CodeBehind="EditUsersInfo.aspx.vb" Inherits="Routes.EditUsersInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">

    <div class="routeEntryButtonClose">
        <asp:LinkButton ID="btnNew" runat="server" CssClass="button blue buttonborder" Text="Return" />
    </div>

    <div class="fullScreen" >

        <div class="centeredScreen">

            <div class="addUserTitle">
                Active Users - Click to Edit
            </div>

            <asp:Repeater runat="server" ID="rptUsers">
                <HeaderTemplate></HeaderTemplate>
                <ItemTemplate>
                    <div class="addUserBlock" >
                        <div class="addUserLabel" style="display:none">
                            <asp:TextBox runat="server" ID="txtID"></asp:TextBox>
                        </div>
                        <div class="addUserEntry addUserMinWidth">
                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="button blue buttonborder" Text="Edit"></asp:LinkButton>
                        </div>
                    </div>

                </ItemTemplate>
                <FooterTemplate></FooterTemplate>


            </asp:Repeater>

            <div class="addUserTitle" style="clear:both;">
                Inactive Users - Click to Edit
            </div>

            <asp:Repeater runat="server" ID="rptInactiveUsers">
                <HeaderTemplate></HeaderTemplate>
                <ItemTemplate>
                    <div class="addUserBlock" >
                        <div class="addUserLabel" style="display:none">
                            <asp:TextBox runat="server" ID="txtID"></asp:TextBox>
                        </div>
                        <div class="addUserEntry addUserMinWidth">
                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="button blue buttonborder gray" Text="Edit"></asp:LinkButton>
                        </div>
                    </div>

                </ItemTemplate>
                <FooterTemplate></FooterTemplate>


            </asp:Repeater>


        </div>

    </div>


</asp:Content>
