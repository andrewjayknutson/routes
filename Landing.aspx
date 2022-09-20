<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Route.Master" CodeBehind="Landing.aspx.vb" Inherits="Routes.Landing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">

        <script type="text/javascript">

            function clearDefault(ctl) {
                if (ctl.value == ctl.defaultValue) { ctl.value = ""; }

                ctl.style.color = "#000";
            }

    </script>

    <div class="largeOuterDiv">

        <div class="topLeftOuterDiv">
            <div class="teamWelcomeEditRouteButton">
                <asp:Literal ID="litLinks" runat="server" />
            </div>
            <div id="teamWelcomeUpdateNPSButton" runat="server" class="teamWelcomeEditRouteButton" >
                <asp:Label ID="lblUpdateNPS" runat="server">Select Employee</asp:Label>
                <asp:DropDownList id="ddlEmpNPS" runat="server" CssClass="dropdownRoute" />
                <div class="homeButton homeGoButton">
                    <asp:LinkButton ID="lnkUpdateNPS" runat="server" CssClass="button blue" Text="Update NPS" />
                </div>
                <div class="homeButton homeGoButton">
                    <asp:LinkButton ID="lnkPayrollAdjustments" runat="server" CssClass="button blue" Text="Payroll Adjustments" />
                </div>
            </div>

        </div>

        <div class="topMiddleOuterDiv">
            <div id="teamWelcomeEditTruckHoursMin" runat="server" class="teamWelcomeEditRouteButton" >
                <asp:Label ID="lblTruckHoursMin" runat="server">Top Dog</asp:Label><asp:TextBox ID="txtTruckHoursMin" runat="server" Width="100px" CssClass="dropdownRoute" Text="enter truck hours min" ForeColor="gray" onfocus="clearDefault(this)"  ></asp:TextBox><br />
                <div class="homeButton homeGoButton">
                    <asp:LinkButton ID="lnkUpdateTruckHoursMin" runat="server" CssClass="button blue" Text="Update Truck Hours Min (Top Dog Rpt)" />
                </div>
            </div>
            <div id="teamWelcomeEditTruckHoursMinReport" runat="server" class="teamWelcomeEditRouteButton" >
                <asp:Label ID="lbl30Day" runat="server">30 Day</asp:Label><asp:TextBox ID="txt30DayTruckHoursMin" runat="server" Width="100px" CssClass="dropdownRoute" Text="enter truck hours min" ForeColor="gray" onfocus="clearDefault(this)"  ></asp:TextBox><br />
                <asp:Label ID="lbl90Day" runat="server">90 Day</asp:Label><asp:TextBox ID="txt90DayTruckHoursMin" runat="server" Width="100px" CssClass="dropdownRoute" Text="enter truck hours min" ForeColor="gray" onfocus="clearDefault(this)"  ></asp:TextBox><br />
                <asp:Label ID="lblYTD" runat="server">YTD</asp:Label><asp:TextBox ID="txtYTDTruckHoursMin" runat="server" Width="100px" CssClass="dropdownRoute" Text="enter truck hours min" ForeColor="gray" onfocus="clearDefault(this)"  ></asp:TextBox><br />
                <div class="homeButton homeGoButton">
                    <asp:LinkButton ID="lnkUpdateTruckHoursMinReport" runat="server" CssClass="button blue" Text="Update Truck Hours Min" />
                </div>
            </div>

        </div>

        <div class="topRightOuterDiv backgroundWhite topRightOuterDivLanding">
            <div id="teamWelcomeEditRouteButton" runat="server" class="teamWelcomeEditRouteButton" >
                <asp:DropDownList id="ddlEditRoute" runat="server" CssClass="dropdownRoute" />
                <div class="homeButton homeGoButton">
                    <asp:LinkButton ID="lnkGo" runat="server" CssClass="button blue" Text="View Route" />
                </div>
            </div>

            <div id="teamWelcomeRouteAssignmentsButton" runat="server" class="teamWelcomeEditRouteButton" >
                <asp:DropDownList id="ddlRouteAssignments" runat="server" CssClass="dropdownRoute" />
                <div class="homeButton homeGoButton">
                    <asp:LinkButton ID="lnkGoRouteAssignment" runat="server" CssClass="button blue" Text="Route Assignment" />
                </div>
            </div>


            <div id="teamWelcomeCreateRouteButton" runat="server" class="teamWelcomeEditRouteButton" >
                <asp:TextBox ID="txtCreateRouteNumber" runat="server" Width="150px" CssClass="dropdownRoute" Text="enter route number" ForeColor="gray" onfocus="clearDefault(this)"  ></asp:TextBox><br />
                <asp:TextBox ID="txtCreateRouteDate" runat="server" Width="150px" CssClass="dropdownRoute" Text="enter route date" ForeColor="gray" onfocus="clearDefault(this)"  ></asp:TextBox>
                <div class="homeButton homeGoButton">
                    <asp:LinkButton ID="lnkCreateRoute" runat="server" CssClass="button blue" Text="Create Route" />
                </div>
                <asp:Label ID="lblCreateError" runat="server" ForeColor="Red" Text="Route already created for that date" Visible="false"></asp:Label>
            </div>

            <div id="teamWelcomeDeleteRouteButton" runat="server" class="teamWelcomeEditRouteButton" >
                <span style="font-weight:bold; color:red;">NOTE:  There is no confirmation of delete after hitting this button....please confirm you are deleting the correct route date.</span>
                <asp:DropDownList id="ddlDeleteRoute" runat="server" CssClass="dropdownRoute" />
                <div class="homeButton homeGoButton">
                    <asp:LinkButton ID="lnkDeleteRoute" runat="server" CssClass="button blue" Text="Delete Route" />
                </div>
            </div>

        </div>
    </div>



</asp:Content>
 