<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Route.Master" CodeBehind="AddUser.aspx.vb" Inherits="Routes.AddUser" title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">

    <div class="largeOuterDiv">

        <div class="topLeftOuterDiv topLeftOuterDivNPS">
            <div class="enterJobsTitleDiv">
                Create New User
            </div>
            <div >
                <table class="addUserTable">
                    <tr>
                        <td >Username:</td>
                        <td><asp:TextBox runat="server" ID="txtUsername"  /></td>
                    </tr>
                    <tr>
                        <td >First Name:</td>
                        <td><asp:TextBox runat="server" ID="txtFirstName"  /></td>
                    </tr>
                    <tr>
                        <td >Last Name:</td>
                        <td><asp:TextBox runat="server" ID="txtLastName"  /></td>
                    </tr>
                    <tr>
                        <td >Address:</td>
                        <td><asp:TextBox runat="server" ID="txtAddress"  /></td>
                    </tr>
                    <tr>
                        <td >City/State/Zip:</td>
                        <td><asp:TextBox runat="server" ID="txtCity"  />&nbsp;&nbsp;
                            <asp:DropDownList runat="server" ID="ddlState" />&nbsp;&nbsp;
                            <asp:TextBox runat="server" ID="txtZip" /></td>
                    </tr>
                    <tr>
                        <td >Password:</td>
                        <td><asp:TextBox runat="server" ID="txtPassword"  /></td>
                    </tr>
                    <tr>
                        <td >Email:</td>
                        <td><asp:TextBox runat="server" ID="txtEmail"  /></td>
                    </tr>
                    <tr>
                        <td >Home Phone:</td>
                        <td><asp:TextBox runat="server" ID="txtPhone1"  /></td>
                    </tr>
                    <tr>
                        <td >Mobile Phone:</td>
                        <td><asp:TextBox runat="server" ID="txtPhone2"  /></td>
                    </tr>
                    <tr>
                        <td >Base Pay Rate:</td>
                        <td><asp:TextBox runat="server" ID="txtBasePayRate"  /></td>
                    </tr>
                    <tr>
                        <td >Active:</td>
                        <td><asp:DropDownList runat="server" ID="ddlActive" /></td>
                    </tr>
                    <tr>
                        <td >Permission:</td>
                        <td><asp:DropDownList ID="ddlPermissions" runat="server" /></td>
                    </tr>
                    <tr>
                        <td >PS Eligible:</td>
                        <td><asp:DropDownList runat="server" ID="ddlPSEligible" /></td>
                    </tr>
                    <tr>
                        <td ></td>
                        <td><asp:Label runat="server" ID="lblStatus" CssClass="StatusLabel" /></td>
                    </tr>
                </table>

            </div>

            <div class="editUserButtons">
                <div class="">
                    <asp:LinkButton ID="btnSaveUser" runat="server" CssClass="button blue" Text="Save" />
                </div>
                <div class="">
                    
                </div>
            </div>

        </div>

        <div class="topRightOuterDiv">

            <div class="topRightOuterButtonDiv">
                <asp:LinkButton ID="btnClose" runat="server" CssClass="button blue" Text="Return" />
            </div>
            <div style="clear:both;"></div>

        </div>

    </div>


</asp:Content>
