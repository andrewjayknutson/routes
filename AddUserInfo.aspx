<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Route.Master" CodeBehind="AddUserInfo.aspx.vb" Inherits="Routes.AddUserInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">


    <div class="routeEntryButtonClose">
        <asp:LinkButton ID="btnClose" runat="server" CssClass="button blue buttonborder" Text="Return" />
    </div>

    <div class="fullScreen" >

        <div class="centeredScreen">

            <div class="addUserTitle">
                Create New User
            </div>

            <div class="addUserBlock" >
                <div class="addUserLabel">
                    <span>Employee ID:</span>
                </div>
                <div class="addUserEntry">
                    <asp:TextBox runat="server" ID="txtEmployeeID" ></asp:TextBox>
                </div>
            </div>

            <div class="addUserBlock" >
                <div class="addUserLabel">
                    <span>First Name:</span>
                </div>
                <div class="addUserEntry">
                    <asp:TextBox runat="server" ID="txtFirst" ></asp:TextBox>
                </div>
            </div>
            <div class="addUserBlock" >
                <div class="addUserLabel">
                    <span>Last Name:</span>
                </div>
                <div class="addUserEntry">
                    <asp:TextBox runat="server" ID="txtLast" ></asp:TextBox>
                </div>
            </div>
            <div class="addUserBlock" >
                <div class="addUserLabel">
                    <span>Title:</span>
                </div>
                <div class="addUserEntry">
                    <asp:DropDownList runat="server" ID="ddlTitleSelection"></asp:DropDownList>
                </div>
            </div>
            <div class="addUserBlock" >
                <div class="addUserLabel">
                    <span>Permission:</span>
                </div>
                <div class="addUserEntry">
                    <asp:DropDownList runat="server" ID="ddlPermissionSelection"></asp:DropDownList>
                </div>
            </div>
            <div class="addUserBlock" >
                <div class="addUserLabel">
                    <span>Active:</span>
                </div>
                <div class="addUserEntry">
                    <asp:DropDownList runat="server" ID="ddlActiveSelection"></asp:DropDownList>
                </div>
            </div>
            <div class="addUserBlock" >
                <div class="addUserLabel">
                    <span>Bonus Eligible:</span>
                </div>
                <div class="addUserEntry">
                    <asp:DropDownList runat="server" ID="ddlBonusSelection"></asp:DropDownList>
                </div>
            </div>
            <div class="addUserBlock" >
                <div class="addUserLabel">
                    <span>Truck Wage (Base):</span>
                </div>
                <div class="addUserEntry">
                    <asp:DropDownList runat="server" ID="ddlTruckWageSelection"></asp:DropDownList>
                </div>
            </div>
            <div class="addUserBlock" >
                <div class="addUserLabel">
                    <span>Clerical Wage:</span>
                </div>
                <div class="addUserEntry">
                    <asp:DropDownList runat="server" ID="ddlClericalSelection"></asp:DropDownList>
                </div>
            </div>
            <div class="addUserBlock" >
                <div class="addUserLabel">
                    <span>Training Wage:</span>
                </div>
                <div class="addUserEntry">
                    <asp:DropDownList runat="server" ID="ddlTrainingSelection"></asp:DropDownList>
                </div>
            </div>
            <div class="addUserBlock" >
                <div class="addUserLabel">
                    <span>Meeting Wage:</span>
                </div>
                <div class="addUserEntry">
                    <asp:DropDownList runat="server" ID="ddlMeetingSelection"></asp:DropDownList>
                </div>
            </div>
            <div class="addUserBlock" >
                <div class="addUserLabel">
                    <span>Support Wage:</span>
                </div>
                <div class="addUserEntry">
                    <asp:DropDownList runat="server" ID="ddlSupportSelection"></asp:DropDownList>
                </div>
            </div>
            <div class="addUserBlock" >
                <div class="addUserLabel">
                    <span>Point Wage:</span>
                </div>
                <div class="addUserEntry">
                    <asp:DropDownList runat="server" ID="ddlPointSelection"></asp:DropDownList>
                </div>
            </div>
            <div class="addUserBlock" >
                <div class="addUserLabel">
                    <span>Fleet Wage:</span>
                </div>
                <div class="addUserEntry">
                    <asp:DropDownList runat="server" ID="ddlFleetSelection"></asp:DropDownList>
                </div>
            </div>
            <div class="addUserBlock" >
                <div class="addUserLabel">
                    <span>Warehouse Wage:</span>
                </div>
                <div class="addUserEntry">
                    <asp:DropDownList runat="server" ID="ddlWarehouseSelection"></asp:DropDownList>
                </div>
            </div>


            <div class="addUserBlock">
                <div class="addUserLabel">
                    <asp:LinkButton ID="btnSaveUser" runat="server" CssClass="button blue buttonborder" Text="Save" />
                </div>
            </div>

        </div>

    </div>


</asp:Content>
