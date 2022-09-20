<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Route.Master" CodeBehind="ReportsV2.aspx.vb" Inherits="Routes.ReportsV2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">

    <div class="largeOuterDiv">

        <div class="topLeftOuterDiv">

            <asp:RadioButtonList ID="radReports" runat="server">
                <asp:ListItem Text="Load Current Data*" Value="loaddata"></asp:ListItem>
                <asp:ListItem Text="CEC" Value="cec" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Jobs" Value="jobs"></asp:ListItem>
                <asp:ListItem Text="*RPH" Value="rph"></asp:ListItem>
                <asp:ListItem Text="Tips" Value="tips"></asp:ListItem>
                <asp:ListItem Text="*AJS" Value="ajs"></asp:ListItem>
                <asp:ListItem Text="*Top Dog" Value="topdog"></asp:ListItem>
                <asp:ListItem Text="*Payroll Summary" Value="payroll"></asp:ListItem>
                <asp:ListItem Text="Sign Percentage" Value="sign"></asp:ListItem>
                <asp:ListItem Text="Hours Type" Value="hourstype"></asp:ListItem>
                <asp:ListItem Text="Reasons Not Convert" Value="reasonsnotconvert"></asp:ListItem>
                <asp:ListItem Text="OSC by Guy" Value="oscbyguy"></asp:ListItem>
                <asp:ListItem Text="Dumps by Location" Value="dumplocation"></asp:ListItem>
                <asp:ListItem Text="Dumps by Guy" Value="dumpbyguy"></asp:ListItem>
                <asp:ListItem Text="Gas by Location" Value="gaslocation"></asp:ListItem>
                <asp:ListItem Text="Discounts by Guy" Value="discountsguy"></asp:ListItem>
            </asp:RadioButtonList>

            <br /><br />

            <asp:Label ID="lblStartDate" runat="server" Text="Start Date:"></asp:Label>
            <br />
            <asp:TextBox ID="txtStartDate" runat="server" ></asp:TextBox>

            <br /><br />

            <asp:Label ID="lblEndDate" runat="server" Text="End Date:"></asp:Label>
            <br />
            <asp:TextBox ID="txtEndDate" runat="server" ></asp:TextBox>

            <br /><br />

            <div class="topLeftOuterDivButton">
                <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button blue" Text="SUBMIT" />
            </div>

        </div>

        <div class="topMiddleOuterDiv">
            <asp:RadioButtonList ID="radReportsV2" runat="server">
                <asp:ListItem Text="2 Week Payroll Summary" Value="2week" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Daily Route" Value="dailyroute"></asp:ListItem>
            </asp:RadioButtonList>

            <br /><br />

            <asp:Label ID="lblStartDateV2" runat="server" Text="Start Date:"></asp:Label>
            <br />
            <asp:TextBox ID="txtStartDateV2" runat="server" ></asp:TextBox>

            <br /><br />

            <div class="topMiddleOuterDivButton">
                <asp:LinkButton ID="lnkSubmitV2" runat="server" CssClass="button blue" Text="SUBMIT" />
            </div>


        </div>

        <div class="topRightOuterDiv">

            <div class="topRightOuterButtonDiv">
                <asp:LinkButton ID="lnkLanding" runat="server" CssClass="button blue" Text="RETURN"></asp:LinkButton>
            </div>
            <div style="clear:both;"></div>

        </div>
    </div>


</asp:Content>
