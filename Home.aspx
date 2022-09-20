<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Route.Master" CodeBehind="Home.aspx.vb" Inherits="Routes.Home" title="1-800-GOT-JUNK? Route Management" %>
    
    
<asp:Content ID="Content3" ContentPlaceHolderID="main" runat="server">

    <div class="divHomeOuter" >
        <div class="divHomeInner">
            <span class="spanHomeTitle" >1-800-GOT-JUNK? Twin Cities Route Managment</span>
            <asp:Panel ID="pnlHome" runat="server" DefaultButton="lnkGo">
                <div class="homeDiv">
                    UserName
                </div>
                <div class="homeDiv">
                    <asp:TextBox ID="txtUser" runat="server" Width="250px" />
                </div>
                <div class="homeDiv">
                    Password
                </div>
                <div class="homeDiv">
                    <asp:TextBox ID="txtPwd" runat="server" Width="250px" TextMode="Password" />
                </div>
                <div class="homeDiv homeButton">
                    <asp:LinkButton ID="lnkGo" runat="server" CssClass="button blue" Text="SUBMIT" />
                </div>
                <div class="homeDiv">
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
                </div>
            </asp:Panel>
        </div>
    </div>

</asp:Content>
