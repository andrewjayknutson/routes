<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Route.Master" CodeBehind="Reports.aspx.vb" Inherits="Routes.Reports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">

    <div class="rptContainer">
        <div class="rptContain1">
            <div >
                <div class="rptHeadText">
                    CEC Summary
                </div>
                <div class="rptStartDate" >
                    <span>Start Date:</span><asp:TextBox ID="txtCECStartDate" runat="server" />
                </div>
                <div class="rptEndDate" >
                    <span>End Date:</span><asp:TextBox ID="txtCECEndDate" runat="server" />
                </div>
                <div class="rptLinkButton" >
                    <asp:LinkButton ID="lnkCEC" runat="server" CssClass="button blue reportButton" Text="SUBMIT" />
                </div>
            </div>
        </div>
        <div class="rptContain2">
            <div >
                <div class="rptHeadText">
                    Jobs Summary
                </div>
                <div class="rptStartDate">
                    Start Date:  <asp:TextBox ID="txtJobsStartDate" runat="server" />
                </div>
                <div class="rptEndDate" >
                    End Date:  <asp:TextBox ID="txtJobsEndDate" runat="server" />
                </div>
                <div class="rptLinkButton">
                    <asp:LinkButton ID="lnkJobs" runat="server" CssClass="button blue reportButton" Text="SUBMIT" />
                </div>
            </div>
        </div>
        <div class="rptContain3">
            <div >
                <div class="rptHeadText">
                    2 Week Payroll Summary
                </div>
                <div class="rptStartDate">
                    Start Date:  <asp:TextBox ID="txtPayrollStartDate" runat="server" />     
                </div>
                <div class="rptLinkButton">
                    <asp:LinkButton ID="lnkPayroll" runat="server" CssClass="button blue reportButton" Text="SUBMIT" />
                </div>
            </div>
        </div>
    </div>


    <div class="rptContainer">
        <div class="rptContain1">
            <div >
                <div class="rptHeadText">
                    Payroll Summary
                </div>
                <div class="rptStartDate">
                    Start Date:  <asp:TextBox ID="txtPayrollDatesStart" runat="server" />     
                </div>
                <div class="rptEndDate">
                    End Date:  <asp:TextBox ID="txtPayrollDatesEnd" runat="server" />     
                </div>
                <div class="rptLinkButton">
                    <asp:LinkButton ID="lnkPayrollDates" runat="server" CssClass="button blue reportButton" Text="SUBMIT" />
                </div>
            </div>
        </div>
        <div class="rptContain2">
            <div >
                <div class="rptHeadText">
                    Revenue Per Hour
                </div>
                <div class="rptStartDate">
                    Start Date:  <asp:TextBox ID="txtRPHStartDate" runat="server" />
                </div>
                <div class="rptEndDate">
                    End Date:  <asp:TextBox ID="txtRPHEndDate" runat="server" />
                </div>
                <div class="rptLinkButton">
                    <asp:LinkButton ID="lnkRPH" runat="server" CssClass="button blue reportButton" Text="SUBMIT" />
                </div>
            </div>
        </div>
        <div class="rptContain3">
            <div >
                <div class="rptHeadText">
                    Sign Percentage
                </div>
                <div class="rptStartDate">
                    Start Date:  <asp:TextBox ID="txtSignsStarDate" runat="server" />
                </div>
                <div class="rptEndDate">
                    End Date:  <asp:TextBox ID="txtSignsEndDate" runat="server" />
                </div>
                <div class="rptLinkButton">
                    <asp:LinkButton ID="lnkSigns" runat="server" CssClass="button blue reportButton" Text="SUBMIT" />
                </div>
            </div>
        </div>
    </div>

    <div class="rptContainer">
        <div class="rptContain1">
            <div >
                <div class="rptHeadText">
                    Top Dog
                </div>
                <div class="rptStartDate">
                    Start Date:  <asp:TextBox ID="txtTopDogStartDate" runat="server" />
                </div>
                <div class="rptEndDate">
                    End Date:  <asp:TextBox ID="txtTopDogEndDate" runat="server" />
                </div>
                <div class="rptLinkButton">
                    <asp:LinkButton ID="lnkTopDog" runat="server" CssClass="button blue reportButton" Text="SUBMIT" />
                </div>
            </div>
        </div>
        <div class="rptContain2">
            <div >
                <div class="rptHeadText">
                    Tips
                </div>
                <div class="rptStartDate">
                    Start Date:  <asp:TextBox ID="txtTipsStartDate" runat="server" />
                </div>
                <div class="rptEndDate">
                    End Date:  <asp:TextBox ID="txtTipsEndDate" runat="server" />
                </div>
                <div class="rptLinkButton">
                    <asp:LinkButton ID="lnkTips" runat="server" CssClass="button blue reportButton" Text="SUBMIT" />
                </div>
            </div>
        </div>
        <div class="rptContain3">
            <div >
                <div class="rptHeadText">
                    Metrics Scorecard
                </div>
                <div class="rptStartDate">
                    As of Date:  <asp:TextBox ID="txtMetricsAsOfDate" runat="server" />
                </div>
                <div class="rptLinkButton">
                    <asp:LinkButton ID="lnkMetricsScorecard" runat="server" CssClass="button blue reportButton" Text="SUBMIT" />
                </div>
            </div>
        </div>
    </div>


        <div class="rptContain2">
            <div >
                <div class="rptHeadText">
                    Average Job Size
                </div>
                <div class="rptStartDate">
                    Start Date:  <asp:TextBox ID="txtAJSStartDate" runat="server" />
                </div>
                <div class="rptEndDate">
                    End Date:  <asp:TextBox ID="txtAJSEndDate" runat="server" />
                </div>
                <div class="rptLinkButton">
                    <asp:LinkButton ID="lnkAJS" runat="server" CssClass="button blue reportButton" Text="SUBMIT" />
                </div>
            </div>
        </div>

        <div class="rptContain3">
            <div >
                <div class="rptHeadText">
                    Top Dog V2
                </div>
                <div class="rptStartDate">
                    Start Date:  <asp:TextBox ID="txtTopDogV2StartDate" runat="server" />
                </div>
                <div class="rptEndDate">
                    End Date:  <asp:TextBox ID="txtTopDogV2EndDate" runat="server" />
                </div>
                <div class="rptLinkButton">
                    <asp:LinkButton ID="lnkTopDogV2" runat="server" CssClass="button blue reportButton" Text="SUBMIT" />
                </div>
            </div>
        </div>

        <div class="rptContain3">
            <div >
                <asp:LinkButton ID="lnkReturn" runat="server" CssClass="button blue reportButton" Text="RETURN" />
            </div>
        </div>

</asp:Content>
