<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Route.Master" CodeBehind="ReportsV4.aspx.vb" Inherits="Routes.ReportsV4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">

    <div class="routeEntryButtonClose">
        <asp:LinkButton ID="lnkLanding" runat="server" CssClass="button blue buttonborder" Text="RETURN"></asp:LinkButton>
    </div>

    <div class="fullScreen" >

        <div class="centeredScreen startPage">

            <div class="startBlock" >
                <div class="startLeft startLeftBorder">
                    <asp:RadioButtonList ID="radReports" runat="server">
                        <asp:ListItem Text="Jobs" Value="jobs"></asp:ListItem>
                        <asp:ListItem Text="Jobs - East" Value="jobs_east"></asp:ListItem>
                        <asp:ListItem Text="Jobs - West" Value="jobs_west"></asp:ListItem>
                        <asp:ListItem Text="Jobs - By Employee" Value="jobs_employee"></asp:ListItem>
                        <asp:ListItem Text="Residential AJS" Value="resajs"></asp:ListItem>
                        <asp:ListItem Text="Residential AJS (by Sales Lead)" Value="resajsestimator"></asp:ListItem>
                        <asp:ListItem Text="Gross Margin" Value="grossmargin"></asp:ListItem>
                        <asp:ListItem Text="Tips" Value="tips"></asp:ListItem>
                        <asp:ListItem Text="RPH" Value="rph"></asp:ListItem>
                        <asp:ListItem Text="Hours Worked" Value="hours"></asp:ListItem>
                        <asp:ListItem Text="Bonus" Value="bonus"></asp:ListItem>
                        <asp:ListItem Text="Duplicate Jobs" Value="duplicate"></asp:ListItem>
                        <asp:ListItem Text="Real Time Revenue / AJS" Value="revajs"></asp:ListItem>
                    </asp:RadioButtonList>
                    <div class="startBlock" >
                        <div class="startLeft">
                            <asp:Label ID="lblStartDate" runat="server" Text="Start Date:"></asp:Label>
                        </div>
                        <div class="startRight">
                            <asp:TextBox ID="txtStartDate" runat="server" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="startBlock" >
                        <div class="startLeft">
                            <asp:Label ID="lblEndDate" runat="server" Text="End Date:"></asp:Label>
                        </div>
                        <div class="startRight">
                            <asp:TextBox ID="txtEndDate" runat="server" ></asp:TextBox>
                        </div>
                    </div>

                    <div class="startBlock" >
                        <div class="startLeft">
                            <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button blue buttonborder" Text="SUBMIT" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="startBlock" >
                <div class="startLeft startLeftBorder">
                    <asp:RadioButtonList ID="radReportsV2" runat="server">
                        <asp:ListItem Text="2 Week Payroll Summary" Value="2week" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                    <div class="startBlock" >
                        <div class="startLeft">
                            <asp:Label ID="lblStartDateV2" runat="server" Text="Start Date:"></asp:Label>
                        </div>
                        <div class="startRight">
                            <asp:TextBox ID="txtStartDateV2" runat="server" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="startBlock" >
                        <div class="startLeft">
                            <asp:LinkButton ID="lnkSubmitV2" runat="server" CssClass="button blue buttonborder" Text="SUBMIT" />
                        </div>
                    </div>
                </div>
            </div>


        </div>

    </div>

</asp:Content>
