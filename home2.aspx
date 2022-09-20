<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Route.Master" CodeBehind="home2.aspx.vb" Inherits="Routes.home2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">

    <div class="mainBodyArea">
        <img style="user-select: none; cursor: zoom-in;" src="http://www.1800gotjunk.com/sites/default/files/videos/happy-couple-video-still.jpg" >
    </div>


    <div class="homeLoginBlock" >
        <asp:Panel ID="pnlHome" runat="server" DefaultButton="lnkGo">
            <div class="homeDiv">
                Username
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
            <div class="homeDiv homeButton homeButton2">
                <asp:LinkButton ID="lnkGo" runat="server" CssClass="" Text="SUBMIT" />
            </div>
            <div class="homeDiv">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
            </div>
        </asp:Panel>
    </div>

    <div class="navbar-footer">

    </div>

</asp:Content>
