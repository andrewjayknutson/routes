<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Route.Master" CodeBehind="ReportsView.aspx.vb" Inherits="Routes.ReportsView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  Namespace="System.Web.UI" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">

    <div class="rptOuter" >

        <div class="routeEntryButtonClose">
            <asp:LinkButton ID="lnkReturn" runat="server" CssClass="button blue" Text="RETURN" />
        </div>
        
        <asp:UpdatePanel ID="updateReports" runat="server" UpdateMode="conditional">
            <ContentTemplate>
                <asp:Literal ID="litCECReportsView" runat="server" ></asp:Literal>            
                
                <asp:Panel ID="pnlCECReport" runat="server" Visible="false" >
                
                    <div class="divCECTitle">
                        <h1><span>CEC Summary</span></h1>
                    </div>
                    <div class="divCECDateRange">
                        <span><% =StartDate %> - <% =EndDate%></span>
                    </div>
                    
                    <div class="divCECGrid">
                        <asp:GridView ID="grdCECResults" runat="server" CellPadding="5" CellSpacing="1" 
                            EnableSortingAndPagingCallbacks="true" AutoGenerateColumns="false" 
                            ShowHeader="true" ShowFooter="true" CssClass="ecc_table ecc_table_nofloat" >
                            
                            <Columns >
                                <asp:BoundField ReadOnly="true" DataField="county" />
                                <asp:BoundField ReadOnly="true" DataField="tonsCommercial" />
                                <asp:BoundField ReadOnly="true" DataField="tonsResidential" />
                                <asp:BoundField ReadOnly="true" DataField="tonsTotal" />
                                <asp:BoundField ReadOnly="true" DataField="tonsNoCEC" />
                                <asp:BoundField ReadOnly="true" DataField="amountCommercial" />
                                <asp:BoundField ReadOnly="true" DataField="amountResidential" />
                                <asp:BoundField ReadOnly="true" DataField="amountTotal" />
                                <asp:BoundField ReadOnly="true" DataField="amountNoCEC" />
                                <asp:BoundField ReadOnly="true" DataField="jobsCommercial" />
                                <asp:BoundField ReadOnly="true" DataField="jobsResidential" />
                                <asp:BoundField ReadOnly="true" DataField="jobsTotal" />
                                <asp:BoundField ReadOnly="true" DataField="jobsNoCEC" />
                            </Columns>
                        </asp:GridView>
                    </div>


                    <div id="divCEC">
                        <h1>CEC Summary</h1>
                        <div id="divCECStartDate">
                            Start Date:  <asp:TextBox ID="txtCECStartDate" runat="server" />
                        </div>
                        <div id="divCECEndDate">
                            End Date:  <asp:TextBox ID="txtCECEndDate" runat="server" />
                        </div>
                        <div id="divCECSubmit">
                            <asp:LinkButton ID="lnkCECSubmit" CssClass="button blue reportButton" runat="server" Text="SUBMIT" ></asp:LinkButton>
                        </div>
                    </div>

                    <div id="divCECReportsViewReturn">
                        <a href="/routes/Reports.aspx" class="button blue reportButton" >RETURN</a>
                    </div>
                
                </asp:Panel>

                <asp:Panel ID="pnlJobsReport" runat="server" Visible="false" >
                    <div class="divCECTitle">
                        <h1><span>Jobs Summary</span></h1>
                    </div>
                    <div class="divCECDateRange">
                        <span><% =StartDate %> - <% =EndDate%></span>
                    </div>

                    <div class="divJobsGrid">
                        <asp:GridView ID="grdJobs" runat="server" CellPadding="5" CellSpacing="1" 
                            EnableSortingAndPagingCallbacks="true" AutoGenerateColumns="false" ShowHeader="false" ShowFooter=true CssClass="ecc_table ecc_table_nofloat">
                            
                            <Columns >
                                <asp:BoundField ReadOnly="true" DataField="routeInfo" />
                                <asp:BoundField ReadOnly="true" DataField="jobID" HtmlEncode="false" />
                                <asp:BoundField ReadOnly="true" DataField="ticketNumber" />
                                <asp:BoundField ReadOnly="true" DataField="ticketRevenue" />
                                <asp:BoundField ReadOnly="true" DataField="ticketDiscount" />
                                <asp:BoundField ReadOnly="true" DataField="ticketCEC" />
                                <asp:BoundField ReadOnly="true" DataField="ticketNet" />
                                <asp:BoundField ReadOnly="true" DataField="sign" />
                                <asp:BoundField ReadOnly="true" DataField="doorHangers" />
                                <asp:BoundField ReadOnly="true" DataField="payMethod" />
                                <asp:BoundField ReadOnly="true" DataField="ticketDetails" />
                            </Columns>
                        </asp:GridView>
                    </div>

                    
                    <div id="divRptJobs">
                        <h1>Jobs Summary</h1>
                        <div id="divRptJobsStartDate">
                            Start Date:  <asp:TextBox ID="txtJobsStartDate" runat="server" />
                        </div>
                        <div id="divRptJobsEndDate">
                            End Date:  <asp:TextBox ID="txtJobsEndDate" runat="server" />
                        </div>
                        <div id="divRptJobsSubmit">
                            <asp:LinkButton ID="lnkJobsSubmit" CssClass="button blue reportButton" runat="server" Text="SUBMIT"></asp:LinkButton>
                        </div>
                    </div>
                    
                </asp:Panel>



                <asp:Panel ID="pnlPayroll" runat="server" Visible="false" >
                    <div class="divCECTitle">
                        <h1><span>PAYROLL SUMMARY</span></h1>
                    </div>
                    <div class="divCECDateRange">
                        <span><% =StartDate %> - <% =EndDate%></span>
                    </div>

                    <div class="divJobsGrid">
                        <asp:GridView ID="grdPayroll" runat="server" CellPadding="5" CellSpacing="1" 
                            EnableSortingAndPagingCallbacks="true" AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true" AlternatingRowStyle-BackColor="silver" CssClass="ecc_table ecc_table_nofloat">
                            
                            <Columns >
                                <asp:BoundField ReadOnly="true" DataField="employee" />
                                <asp:BoundField ReadOnly="true" DataField="totalHours" />
                                <asp:BoundField ReadOnly="true" DataField="clericalHours" />
                                <asp:BoundField ReadOnly="true" DataField="mktgHours" />
                                <asp:BoundField ReadOnly="true" DataField="truckHours" />
                                <asp:BoundField ReadOnly="true" DataField="otTruckHours" />
                                <asp:BoundField ReadOnly="true" DataField="basePayRate" />
                                <asp:BoundField ReadOnly="true" DataField="basePay" />
                                <asp:BoundField ReadOnly="true" DataField="otPayRate" />
                                <asp:BoundField ReadOnly="true" DataField="otPay" />
                                <asp:BoundField ReadOnly="true" DataField="bonus" />
                                <asp:BoundField ReadOnly="true" DataField="profitShare" />
                                <asp:BoundField ReadOnly="true" DataField="totalBonus" />
                                <asp:BoundField ReadOnly="true" DataField="totalPay" />
                                <asp:BoundField ReadOnly="true" DataField="adjustments" />
                                <asp:BoundField ReadOnly="true" DataField="netPay" />
                                <asp:BoundField ReadOnly="true" DataField="hourlyAvg" />
                            </Columns>
                        </asp:GridView>
                    </div>


                    <div class="divCECTitle">
                        <br />
                        <br />
                        <h1><span>PAYROLL SUMMARY</span></h1>
                    </div>
                    <div class="divCECDateRange">
                        <span><% =Week2StartDate%> - <% =Week2EndDate%></span>
                    </div>
                    <div class="divJobsGrid">
                        <asp:GridView ID="grdPayrollSecondWeek" runat="server" CellPadding="5" CellSpacing="1" 
                            EnableSortingAndPagingCallbacks="true" AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true" AlternatingRowStyle-BackColor="silver" CssClass="ecc_table ecc_table_nofloat">
                            
                            <Columns >
                                <asp:BoundField ReadOnly="true" DataField="employee" />
                                <asp:BoundField ReadOnly="true" DataField="totalHours" />
                                <asp:BoundField ReadOnly="true" DataField="clericalHours" />
                                <asp:BoundField ReadOnly="true" DataField="mktgHours" />
                                <asp:BoundField ReadOnly="true" DataField="truckHours" />
                                <asp:BoundField ReadOnly="true" DataField="otTruckHours" />
                                <asp:BoundField ReadOnly="true" DataField="basePayRate" />
                                <asp:BoundField ReadOnly="true" DataField="basePay" />
                                <asp:BoundField ReadOnly="true" DataField="otPayRate" />
                                <asp:BoundField ReadOnly="true" DataField="otPay" />
                                <asp:BoundField ReadOnly="true" DataField="bonus" />
                                <asp:BoundField ReadOnly="true" DataField="profitShare" />
                                <asp:BoundField ReadOnly="true" DataField="totalBonus" />
                                <asp:BoundField ReadOnly="true" DataField="totalPay" />
                                <asp:BoundField ReadOnly="true" DataField="adjustments" />
                                <asp:BoundField ReadOnly="true" DataField="netPay" />
                                <asp:BoundField ReadOnly="true" DataField="hourlyAvg" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    
                    <div class="divCECTitle">
                        <br />
                        <br />
                        <h1><span>PAYROLL SUMMARY</span></h1>
                    </div>
                    <div class="divCECDateRange">
                        <span><% =StartDate%> - <% =Week2EndDate%></span>
                    </div>
                    <div class="divJobsGrid">
                        <asp:GridView ID="grdPayrollTotal" runat="server" CellPadding="5" CellSpacing="1" 
                            EnableSortingAndPagingCallbacks="true" AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true" AlternatingRowStyle-BackColor="silver" CssClass="ecc_table ecc_table_nofloat">
                            
                            <Columns >
                                <asp:BoundField ReadOnly="true" DataField="employee" />
                                <asp:BoundField ReadOnly="true" DataField="totalHours" />
                                <asp:BoundField ReadOnly="true" DataField="clericalHours" />
                                <asp:BoundField ReadOnly="true" DataField="mktgHours" />
                                <asp:BoundField ReadOnly="true" DataField="mktgRound" />
                                <asp:BoundField ReadOnly="true" DataField="truckHours" />
                                <asp:BoundField ReadOnly="true" DataField="truckRound" />
                                <asp:BoundField ReadOnly="true" DataField="otTruckHours" />
                                <asp:BoundField ReadOnly="true" DataField="basePayRate" />
                                <asp:BoundField ReadOnly="true" DataField="basePay" />
                                <asp:BoundField ReadOnly="true" DataField="otPayRate" />
                                <asp:BoundField ReadOnly="true" DataField="otPay" />
                                <asp:BoundField ReadOnly="true" DataField="bonus" />
                                <asp:BoundField ReadOnly="true" DataField="profitShare" />
                                <asp:BoundField ReadOnly="true" DataField="totalBonus" />
                                <asp:BoundField ReadOnly="true" DataField="totalPay" />
                                <asp:BoundField ReadOnly="true" DataField="adjustments" />
                                <asp:BoundField ReadOnly="true" DataField="netPay" />
                                <asp:BoundField ReadOnly="true" DataField="hourlyAvg" />
                            </Columns>
                        </asp:GridView>
                    </div>

                    <div id="divRptPayroll">
                        <h1>Payroll Summary</h1>
                        <div id="divRptPayrollStartDate">
                            Start Date:  <asp:TextBox ID="txtPayrollStartDate" runat="server" />
                        </div>
                        <div id="divRptPayrollSubmit">
                            <asp:LinkButton ID="lnkPayrollSubmit" CssClass="button blue reportButton" runat="server" Text="SUBMIT" ></asp:LinkButton>
                        </div>
                    </div>
                    
                </asp:Panel>






                <asp:Panel ID="pnlPayrollDates" runat="server" Visible="false" >
                    <div class="divCECTitle">
                        <h1><span>PAYROLL SUMMARY</span></h1>
                    </div>
                    <div class="divCECDateRange">
                        <span><% =StartDate %> - <% =EndDate%></span>
                    </div>

                    <div class="divJobsGrid">
                        <asp:GridView ID="grdPayrollDates" runat="server" CellPadding="5" CellSpacing="1" 
                            EnableSortingAndPagingCallbacks="true" AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true" AlternatingRowStyle-BackColor="silver" CssClass="ecc_table ecc_table_nofloat">
                            
                            <Columns >
                                <asp:BoundField ReadOnly="true" DataField="employee" />
                                <asp:BoundField ReadOnly="true" DataField="totalHours" />
                                <asp:BoundField ReadOnly="true" DataField="clericalHours" />
                                <asp:BoundField ReadOnly="true" DataField="mktgHours" />
                                <asp:BoundField ReadOnly="true" DataField="truckHours" />
                                <asp:BoundField ReadOnly="true" DataField="otTruckHours" />
                                <asp:BoundField ReadOnly="true" DataField="basePayRate" />
                                <asp:BoundField ReadOnly="true" DataField="basePay" />
                                <asp:BoundField ReadOnly="true" DataField="otPayRate" />
                                <asp:BoundField ReadOnly="true" DataField="otPay" />
                                <asp:BoundField ReadOnly="true" DataField="bonus" />
                                <asp:BoundField ReadOnly="true" DataField="profitShare" />
                                <asp:BoundField ReadOnly="true" DataField="totalBonus" />
                                <asp:BoundField ReadOnly="true" DataField="totalPay" />
                                <asp:BoundField ReadOnly="true" DataField="adjustments" />
                                <asp:BoundField ReadOnly="true" DataField="netPay" />
                                <asp:BoundField ReadOnly="true" DataField="hourlyAvg" />
                            </Columns>
                        </asp:GridView>
                    </div>

                    
                </asp:Panel>



                <asp:Panel ID="pnlRPH" runat="server" Visible="false" >
                    <div class="divCECTitle">
                        <h1><span>REVENUE PER HOUR</span></h1>
                        <span><% =StartDate %> - <% =EndDate%></span>
                    </div>
                    <div class="divCECDateRange">
                        <span></span>
                    </div>
                    <div class="divJobsGrid">
                        <div class="divCECDateRange">
                            <span>Qualified (<% =TruckHoursMin%> Truck Hour Minimum MTD)</span>
                        </div>

                        <div>
                            <asp:GridView ID="grdRPH" runat="server" CellPadding="5" CellSpacing="1" 
                                EnableSortingAndPagingCallbacks="true" AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true" AlternatingRowStyle-BackColor="silver" CssClass="ecc_table ecc_table_nofloat" >
                            
                                <Columns >
                                    <asp:BoundField ReadOnly="true" DataField="employee" />
                                    <asp:BoundField ReadOnly="true" DataField="revMTD" DataFormatString="{0:N2}" />
                                    <asp:BoundField ReadOnly="true" DataField="revMktgMTD" DataFormatString="{0:N2}" />
                                    <asp:BoundField ReadOnly="true" DataField="hoursMTD" DataFormatString="{0:N2}" />
                                    <asp:BoundField ReadOnly="true" DataField="rphMTD" DataFormatString="{0:N2}" />
                                    <asp:BoundField ReadOnly="true" DataField="rphMktgMTD" DataFormatString="{0:N2}" />
                                    <asp:BoundField ReadOnly="true" DataField="revYTD" DataFormatString="{0:N2}" />
                                    <asp:BoundField ReadOnly="true" DataField="revMktgYTD" DataFormatString="{0:N2}" />
                                    <asp:BoundField ReadOnly="true" DataField="hoursYTD" DataFormatString="{0:N2}" />
                                    <asp:BoundField ReadOnly="true" DataField="rphYTD" DataFormatString="{0:N2}" />
                                    <asp:BoundField ReadOnly="true" DataField="rphMktgYTD" DataFormatString="{0:N2}" />
                                </Columns>
                            </asp:GridView>
                        </div>

                        <div class="divCECDateRange">
                            <span>NOT Qualified (<% =TruckHoursMin%> Truck Hour Minimum MTD)</span>
                        </div>

                        <div >
                            <asp:GridView ID="grdRPHNotMet" runat="server" CellPadding="5" CellSpacing="1" 
                                EnableSortingAndPagingCallbacks="true" AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true" AlternatingRowStyle-BackColor="silver" CssClass="ecc_table ecc_table_nofloat" >
                            
                                <Columns >
                                    <asp:BoundField ReadOnly="true" DataField="employee" />
                                    <asp:BoundField ReadOnly="true" DataField="revMTD" DataFormatString="{0:N2}" />
                                    <asp:BoundField ReadOnly="true" DataField="revMktgMTD" DataFormatString="{0:N2}" />
                                    <asp:BoundField ReadOnly="true" DataField="hoursMTD" DataFormatString="{0:N2}" />
                                    <asp:BoundField ReadOnly="true" DataField="rphMTD" DataFormatString="{0:N2}" />
                                    <asp:BoundField ReadOnly="true" DataField="rphMktgMTD" DataFormatString="{0:N2}" />
                                    <asp:BoundField ReadOnly="true" DataField="revYTD" DataFormatString="{0:N2}" />
                                    <asp:BoundField ReadOnly="true" DataField="revMktgYTD" DataFormatString="{0:N2}" />
                                    <asp:BoundField ReadOnly="true" DataField="hoursYTD" DataFormatString="{0:N2}" />
                                    <asp:BoundField ReadOnly="true" DataField="rphYTD" DataFormatString="{0:N2}" />
                                    <asp:BoundField ReadOnly="true" DataField="rphMktgYTD" DataFormatString="{0:N2}" />
                                </Columns>
                            </asp:GridView>
                        </div>


                    </div>
                    
                    <div id="divRphRPH">
                        <h1>Revenue Per Hour</h1>
                        <div id="divRphStartDate">
                            Start Date:  <asp:TextBox ID="txtRPHStartDate" runat="server" />
                        </div>
                        <div id="divRphEndDate">
                            End Date:  <asp:TextBox ID="txtRPHEndDate" runat="server" />
                        </div>
                        <div id="divRphSubmit">
                            <asp:LinkButton ID="lnkRPH" CssClass="button blue reportButton" runat="server" Text="SUBMIT" ></asp:LinkButton>
                        </div>
                    </div>
                    
                </asp:Panel>


                <asp:Panel ID="pnlSign" runat="server" Visible="false" >
                    <div class="divCECTitle">
                        <h1><span>Lawn Signs</span></h1>
                        <span><% =StartDate %> - <% =EndDate%></span>
                    </div>
                    <div class="divCECDateRange">
                        <span></span>
                    </div>

                    <div class="divJobsGrid">
                        <div class="divCECDateRange">
                            <span>Qualified (<% =TruckHoursMin%> Truck Hour Minimum MTD)</span>
                        </div>

                        <div >
                            <asp:GridView ID="grdSigns" runat="server" CellPadding="5" CellSpacing="1" 
                                EnableSortingAndPagingCallbacks="true" AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true" AlternatingRowStyle-BackColor="silver" CssClass="ecc_table ecc_table_nofloat" >
                            
                                <Columns >
                                    <asp:BoundField ReadOnly="true" DataField="employee" />
                                    <asp:BoundField ReadOnly="true" DataField="truckHours" />
                                    <asp:BoundField ReadOnly="true" DataField="jobsMTD" />
                                    <asp:BoundField ReadOnly="true" DataField="signsMTD" />
                                    <asp:BoundField ReadOnly="true" DataField="signpercentageMTD" />
                                    <asp:BoundField ReadOnly="true" DataField="jobsYTD" />
                                    <asp:BoundField ReadOnly="true" DataField="signsYTD" />
                                    <asp:BoundField ReadOnly="true" DataField="signpercentageYTD" />
                                </Columns>
                            </asp:GridView>
                        </div>

                        <div class="divCECDateRange">
                            <span>NOT Qualified (<% =TruckHoursMin%> Truck Hour Minimum MTD)</span>
                        </div>

                        <div >
                            <asp:GridView ID="grdSignsNoMinimum" runat="server" CellPadding="5" CellSpacing="1" 
                                EnableSortingAndPagingCallbacks="true" AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true" AlternatingRowStyle-BackColor="silver" CssClass="ecc_table ecc_table_nofloat"  >
                            
                                <Columns >
                                    <asp:BoundField ReadOnly="true" DataField="employee" />
                                    <asp:BoundField ReadOnly="true" DataField="truckHours" />
                                    <asp:BoundField ReadOnly="true" DataField="jobsMTD" />
                                    <asp:BoundField ReadOnly="true" DataField="signsMTD" />
                                    <asp:BoundField ReadOnly="true" DataField="signpercentageMTD" />
                                    <asp:BoundField ReadOnly="true" DataField="jobsYTD" />
                                    <asp:BoundField ReadOnly="true" DataField="signsYTD" />
                                    <asp:BoundField ReadOnly="true" DataField="signpercentageYTD" />
                                </Columns>
                            </asp:GridView>
                        </div>

                    </div>
                    
                    <div id="divRptSigns">
                        <h1>Lawn Signs</h1>
                        <div id="divSignsStartDate">
                            Start Date:  <asp:TextBox ID="txtSignsStarDate" runat="server" />
                        </div>
                        <div id="divSignsEndDate">
                            End Date:  <asp:TextBox ID="txtSignsEndDate" runat="server" />
                        </div>
                        <div id="divSignsSubmit">
                            <asp:LinkButton ID="lnkSigns" CssClass="button blue reportButton" runat="server" Text="SUBMIT" ></asp:LinkButton>
                        </div>
                    </div>
                    
                </asp:Panel>

                <asp:Panel ID="pnlTips" runat="server" Visible="false" >
                    <div class="divCECTitle">
                        <h1><span>Tips</span></h1>
                        <span><% =StartDate %> - <% =EndDate%></span>
                    </div>
                    <div class="divCECDateRange">
                        <span></span>
                    </div>

                    <div class="divJobsGrid">
                        <div class="divCECDateRange">
                            <span></span>
                        </div>

                        <div >
                            <asp:GridView ID="grdTips" runat="server" CellPadding="5" CellSpacing="1" 
                                EnableSortingAndPagingCallbacks="true" AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true" AlternatingRowStyle-BackColor="silver" CssClass="ecc_table ecc_table_nofloat" >
                            
                                <Columns >
                                    <asp:BoundField ReadOnly="true" DataField="employee" />
                                    <asp:BoundField ReadOnly="true" DataField="tipsTotal" />
                                </Columns>
                            </asp:GridView>
                        </div>

                    </div>
                    
                    <div id="divRptTips">
                        <h1>Tips</h1>
                        <div id="divTipsStartDate">
                            Start Date:  <asp:TextBox ID="txtTipsStartDate" runat="server" />
                        </div>
                        <div id="divTipsEndDate">
                            End Date:  <asp:TextBox ID="txtTipsEndDate" runat="server" />
                        </div>
                        <div id="divTipsSubmit">
                            <asp:LinkButton ID="lnkTips" CssClass="button blue reportButton" runat="server" Text="SUBMIT" ></asp:LinkButton>
                        </div>
                    </div>
                    
                </asp:Panel>


                <asp:Panel ID="pnlTopDog" runat="server" Visible="false" >
                    <div class="divCECTitle">
                        <h1><span>Top Dog</span></h1>
                        <span><% =StartDate %> - <% =EndDate%></span>
                    </div>
                    <div class="divJobsGrid">
                        <asp:GridView ID="grdTopDog" runat="server" CellPadding="5" CellSpacing="1" 
                            EnableSortingAndPagingCallbacks="true" AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true" AlternatingRowStyle-BackColor="silver" CssClass="ecc_table ecc_table_nofloat" >
                            
                            <Columns >
                                <asp:BoundField ReadOnly="true" DataField="employee" />
                                <asp:BoundField ReadOnly="true" DataField="npsMTD" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="npsRank" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="rphMTD" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="rphRank" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="signMTD" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="signRank" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="overallMTD" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="overallRank" DataFormatString="{0:N2}" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    
                    <div id="divRptTopDog">
                        <h1>Top Dog</h1>
                        <div id="divTopDogStartDate">
                            Start Date:  <asp:TextBox ID="txtTopDogStartDate" runat="server" />
                        </div>
                        <div id="divTopDogEndDate">
                            End Date:  <asp:TextBox ID="txtTopDogEndDate" runat="server" />
                        </div>
                        <div id="divTopDogSubmit">
                            <asp:LinkButton ID="lnkTopDogSubmit" CssClass="button blue reportButton" runat="server" Text="SUBMIT" ></asp:LinkButton>
                        </div>
                    </div>
                    
                </asp:Panel>


                <asp:Panel ID="pnlTopDogV2" runat="server" Visible="false" >
                    <div class="divCECTitle">
                        <h1><span>Top Dog</span></h1>
                        <span><% =StartDate %> - <% =EndDate%></span>
                    </div>
                    <div class="divJobsGrid">
                        <asp:GridView ID="grdViewTopDogV2" runat="server" CellPadding="5" CellSpacing="1" 
                            EnableSortingAndPagingCallbacks="true" AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true" AlternatingRowStyle-BackColor="silver" CssClass="ecc_table ecc_table_nofloat" >
                            
                            <Columns >
                                <asp:BoundField ReadOnly="true" DataField="employee" />
                                <asp:BoundField ReadOnly="true" DataField="rphMTD" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="rphRank" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="ajsMTD" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="ajsRank" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="npsMTD" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="npsRank" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="oscMTD" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="oscRank" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="overallMTD" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="overallRank" DataFormatString="{0:N2}" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    
                    <div id="divRptTopDogV2">
                        <h1>Top Dog</h1>
                        <div id="divTopDogStartDateV2">
                            Start Date:  <asp:TextBox ID="txtTopDogV2StartDate" runat="server" />
                        </div>
                        <div id="divTopDogEndDateV2">
                            End Date:  <asp:TextBox ID="txtTopDogV2EndDate" runat="server" />
                        </div>
                        <div id="divTopDogSubmitV2">
                            <asp:LinkButton ID="lnkTopDogV2Submit" CssClass="button blue reportButton" runat="server" Text="SUBMIT" ></asp:LinkButton>
                        </div>
                    </div>
                    
                </asp:Panel>



                <asp:Panel ID="pnlTopDogV3" runat="server" Visible="false" >
                    <div class="divCECTitle">
                        <h1><span>Top Dog</span></h1>
                        <span><% =StartDate %> - <% =EndDate%></span>
                    </div>
                    <div class="divJobsGrid">
                        <asp:GridView ID="grdTopDogV3" runat="server" CellPadding="5" CellSpacing="1" 
                            EnableSortingAndPagingCallbacks="true" AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true" AlternatingRowStyle-BackColor="silver" CssClass="ecc_table ecc_table_nofloat" >
                            
                            <Columns >
                                <asp:BoundField ReadOnly="true" DataField="employee" />
                                <asp:BoundField ReadOnly="true" DataField="rphMTD" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="rphRank" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="ajsMTD" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="ajsRank" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="npsMTD" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="npsRank" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="overallMTD" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="overallRank" DataFormatString="{0:N2}" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    
                    <div id="divRptTopDogV3">
                        <h1>Top Dog</h1>
                        <div id="divTopDogStartDateV3">
                            Start Date:  <asp:TextBox ID="txtTopDogV3StartDate" runat="server" />
                        </div>
                        <div id="divTopDogEndDateV3">
                            End Date:  <asp:TextBox ID="txtTopDogV3EndDate" runat="server" />
                        </div>
                        <div id="divTopDogSubmitV3">
                            <asp:LinkButton ID="lnkTopDogV3Submit" CssClass="button blue reportButton" runat="server" Text="SUBMIT" ></asp:LinkButton>
                        </div>
                    </div>
                    
                </asp:Panel>




                <asp:Panel ID="pnlEmployeeMetrics" runat="server" Visible="false" >
                    <div class="divCECTitle">
                        <h1><span>Employee Metrics</span></h1>
                        <span><% =StartDate %></span>
                    </div>
                    <div class="divJobsGrid">
                        <asp:GridView ID="grdEmployeeMetrics" runat="server" CellPadding="5" CellSpacing="1" 
                            EnableSortingAndPagingCallbacks="true" AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true" AlternatingRowStyle-BackColor="silver" CssClass="ecc_table ecc_table_nofloat" >
                            
                            <Columns >
                                <asp:BoundField ReadOnly="true" DataField="employee" />
                                <asp:BoundField ReadOnly="true" DataField="nps30" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="npsRank30" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="rph30" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="rphRank30" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="sign30" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="signRank30" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="overall30" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="overallRank30" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="nps90" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="npsRank90" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="rph90" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="rphRank90" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="sign90" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="signRank90" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="overall90" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="overallRank90" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="npsYTD" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="npsRankYTD" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="rphYTD" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="rphRankYTD" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="signYTD" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="signRankYTD" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="overallYTD" DataFormatString="{0:N2}" />
                                <asp:BoundField ReadOnly="true" DataField="overallRankYTD" DataFormatString="{0:N2}" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    
                    <div id="divRptEmployeeMetrics">
                        <h1>Employee Metrics</h1>
                        <div id="divEmployeeMetricsStartDate">
                            Start Date:  <asp:TextBox ID="txtEmployeeMetricsStartDate" runat="server" />
                        </div>
                        <div id="divEmployeeMetricsSubmit">
                            <asp:LinkButton ID="lnkEmployeeMetricsSubmit" CssClass="button blue reportButton" runat="server" Text="SUBMIT" ></asp:LinkButton>
                        </div>
                    </div>
                    
                </asp:Panel>


                <asp:Panel ID="pnlAJS" runat="server" Visible="false" >
                    <div class="divCECTitle">
                        <h1><span>Average Job Size</span></h1>
                        <span><% =StartDate %> - <% =EndDate%></span>
                    </div>
                    <div class="divCECDateRange">
                        <span></span>
                    </div>

                    <div class="divJobsGrid">
                        <div class="divCECDateRange">
                            <span></span>
                        </div>

                        <div >
                            <asp:GridView ID="grdAJS" runat="server" CellPadding="5" CellSpacing="1" 
                                EnableSortingAndPagingCallbacks="true" AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true" AlternatingRowStyle-BackColor="silver" CssClass="ecc_table ecc_table_nofloat" >
                            
                                <Columns >
                                    <asp:BoundField ReadOnly="true" DataField="employee" />
                                    <asp:BoundField ReadOnly="true" DataField="ajsTotal" />
                                </Columns>
                            </asp:GridView>
                        </div>

                    </div>
                    
                    <div id="divRptAJS">
                        <h1>AJS</h1>
                        <div id="divAJSStartDate">
                            Start Date:  <asp:TextBox ID="txtAJSStartDate" runat="server" />
                        </div>
                        <div id="divAJSEndDate">
                            End Date:  <asp:TextBox ID="txtAJSEndDate" runat="server" />
                        </div>
                        <div id="divAJSSubmit">
                            <asp:LinkButton ID="lnkAJS" CssClass="button blue reportButton" runat="server" Text="SUBMIT" ></asp:LinkButton>
                        </div>
                    </div>
                    
                </asp:Panel>


                <asp:Panel ID="pnlHourType" runat="server" Visible="false" >
                    <div class="divCECTitle">
                        <h1><span>Hour Type</span></h1>
                        <span><% =StartDate %> - <% =EndDate%></span>
                    </div>
                    <div class="divCECDateRange">
                        <span></span>
                    </div>

                    <div class="divJobsGrid">
                        <div >
                            <asp:GridView ID="grdHourType" runat="server" CellPadding="5" CellSpacing="1" 
                                EnableSortingAndPagingCallbacks="true" AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true" AlternatingRowStyle-BackColor="silver" CssClass="ecc_table ecc_table_nofloat" >
                            
                                <Columns >
                                    <asp:BoundField ReadOnly="true" DataField="routeDate" />
                                    <asp:BoundField ReadOnly="true" DataField="employee" />
                                    <asp:BoundField ReadOnly="true" DataField="totalHours" />
                                    <asp:BoundField ReadOnly="true" DataField="workType" />
                                </Columns>
                            </asp:GridView>
                        </div>

                    </div>
                    
                    <div id="divRptHourType">
                        <h1>Hour Type</h1>
                        <div id="divHourTypeStartDate">
                            Start Date:  <asp:TextBox ID="txtHourTypeStartDate" runat="server" />
                        </div>
                        <div id="divHourTypeEndDate">
                            End Date:  <asp:TextBox ID="txtHourTypeEndDate" runat="server" />
                        </div>
                        <div id="divHourTypeSubmit">
                            <asp:LinkButton ID="lnkHourTypeSubmit" CssClass="button blue reportButton" runat="server" Text="SUBMIT" ></asp:LinkButton>
                        </div>
                    </div>
                    
                </asp:Panel>




                <asp:Panel ID="pnlReasonNotConvert" runat="server" Visible="false" >
                    <div class="divCECTitle">
                        <h1><span>Reasons Not Convert</span></h1>
                        <span><% =StartDate %> - <% =EndDate%></span>
                    </div>
                    <div class="divCECDateRange">
                        <span></span>
                    </div>

                    <div class="divJobsGrid">
                        <div >
                            <asp:GridView ID="grdReasonsNotConvert" runat="server" CellPadding="5" CellSpacing="1" 
                                EnableSortingAndPagingCallbacks="true" AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true" AlternatingRowStyle-BackColor="silver" CssClass="ecc_table ecc_table_nofloat" >
                            
                                <Columns >
                                    <asp:BoundField ReadOnly="true" DataField="routeNumber" />
                                    <asp:BoundField ReadOnly="true" DataField="routeDate" />
                                    <asp:HyperLinkField DataTextField="jobID" DataNavigateUrlFields="id" DataNavigateUrlFormatString="\RouteEntry.aspx?rid={0}" />
                                    <asp:BoundField ReadOnly="true" DataField="reasonNotConvert" />
                                    <asp:BoundField ReadOnly="true" DataField="ticketDetails" />
                                </Columns>
                            </asp:GridView>
                        </div>

                    </div>
                    
                    <div id="divRptReasonsNotConvert">
                        <h1>Reason Not Convert</h1>
                        <div id="divReasonsNotConvertStartDate">
                            Start Date:  <asp:TextBox ID="txtReasonNotConvertStateDate" runat="server" />
                        </div>
                        <div id="divReasonsNotConvertEndDate">
                            End Date:  <asp:TextBox ID="txtReasonNotConvertEndDate" runat="server" />
                        </div>
                        <div id="divReasonsNotConvertSubmit">
                            <asp:LinkButton ID="lnkReasonNotConvert" CssClass="button blue reportButton" runat="server" Text="SUBMIT" ></asp:LinkButton>
                        </div>
                    </div>
                    
                </asp:Panel>








                <asp:Panel ID="pnlDailyRoute" runat="server" Visible="false" >
                    <div class="divCECTitle">
                        <h1><span>Daily Route</span></h1>
                        <span><% =StartDate %> - <% =EndDate%></span>
                    </div>
                    <div class="divCECDateRange">
                        <span></span>
                    </div>

                    <div class="divJobsGrid">
                        <div >
                            <asp:GridView ID="grdDailyRoute" runat="server" CellPadding="5" CellSpacing="1" 
                                EnableSortingAndPagingCallbacks="true" AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true" AlternatingRowStyle-BackColor="silver" CssClass="ecc_table ecc_table_nofloat" >
                            
                                <Columns >
                                    <asp:BoundField ReadOnly="true" DataField="routeDate" />
                                    <asp:HyperLinkField DataTextField="routeNumber" DataNavigateUrlFields="routeID" DataNavigateUrlFormatString="\RouteEntry.aspx?rid={0}" />
                                    <asp:BoundField ReadOnly="true" DataField="truckTeam" />
                                    <asp:BoundField ReadOnly="true" DataField="jobsCount" />
                                    <asp:BoundField ReadOnly="true" DataField="jobsRevenueCount" />
                                    <asp:BoundField ReadOnly="true" DataField="revenue" />
                                    <asp:BoundField ReadOnly="true" DataField="ajs" />
                                    <asp:BoundField ReadOnly="true" DataField="rph" />
                                    <asp:BoundField ReadOnly="true" DataField="osc" />
                                    <asp:BoundField ReadOnly="true" DataField="signpercentage" />
                                    <asp:BoundField ReadOnly="true" DataField="doorhangerpercentage" />
                                    <asp:BoundField ReadOnly="true" DataField="swipepercentage" />
                                    <asp:BoundField ReadOnly="true" DataField="tips" />
                                    <asp:BoundField ReadOnly="true" DataField="profitshare" />
                                </Columns>
                            </asp:GridView>
                        </div>

                    </div>

                    <div>
                        <div>
                            <br />
                        </div>
                        <div style="font-size:12px">
                            AJS:   total net revenue / jobs with revenue
                        </div>
                        <div style="font-size:12px">
                            RPH:   total net revenue + marketing revenue / total truck hours
                        </div>
                        <div style="font-size:12px">
                            OSC:   total jobs with revenue / total jobs
                        </div>
                        <div style="font-size:12px">
                            Sign %:   jobs with sign / jobs with revenue
                        </div>
                        <div style="font-size:12px">
                            DH %:   jobs with DH / jobs with revenue
                        </div>
                    </div>

                    
                    <div id="divRptDailyRoute">
                        <div id="divDailyRouteStartDate">
                            Start Date:  <asp:TextBox ID="txtDailyRouteStartDate" runat="server" />
                        </div>
                        <div id="divDailyRouteSubmit">
                            <asp:LinkButton ID="lnkDailyRoute" CssClass="button blue reportButton" runat="server" Text="SUBMIT" ></asp:LinkButton>
                        </div>
                    </div>
                    
                </asp:Panel>






                <asp:Panel ID="pnlDumpLocation" runat="server" Visible="false" >
                    <div class="divCECTitle">
                        <h1><span>Dumps by Location</span></h1>
                        <span><% =StartDate %> - <% =EndDate%></span>
                    </div>
                    <div class="divCECDateRange">
                        <span></span>
                    </div>

                    <div class="divJobsGrid">
                        <div >
                            <asp:GridView ID="grdDumpLocation" runat="server" CellPadding="5" CellSpacing="1" 
                                EnableSortingAndPagingCallbacks="true" AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true" AlternatingRowStyle-BackColor="silver" CssClass="ecc_table ecc_table_nofloat" >
                            
                                <Columns >
                                    <asp:HyperLinkField DataTextField="routeNumber" DataNavigateUrlFields="routeID" DataNavigateUrlFormatString="\RouteEntry.aspx?rid={0}" />
                                    <asp:BoundField ReadOnly="true" DataField="routeDate" />
                                    <asp:BoundField ReadOnly="true" DataField="ticketNumber" />
                                    <asp:BoundField ReadOnly="true" DataField="ticketTotal" />
                                </Columns>
                            </asp:GridView>
                        </div>

                    </div>

                    
                </asp:Panel>






                <asp:Panel ID="pnlGasLocation" runat="server" Visible="false" >
                    <div class="divCECTitle">
                        <h1><span>Gas by Location</span></h1>
                        <span><% =StartDate %> - <% =EndDate%></span>
                    </div>
                    <div class="divCECDateRange">
                        <span></span>
                    </div>

                    <div class="divJobsGrid">
                        <div >
                            <asp:GridView ID="grdGasLocation" runat="server" CellPadding="5" CellSpacing="1" 
                                EnableSortingAndPagingCallbacks="true" AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true" AlternatingRowStyle-BackColor="silver" CssClass="ecc_table ecc_table_nofloat" >
                            
                                <Columns >
                                    <asp:HyperLinkField DataTextField="routeNumber" DataNavigateUrlFields="routeID" DataNavigateUrlFormatString="\RouteEntry.aspx?rid={0}" />
                                    <asp:BoundField ReadOnly="true" DataField="routeDate" />
                                    <asp:BoundField ReadOnly="true" DataField="ticketTotal" />
                                </Columns>
                            </asp:GridView>
                        </div>

                    </div>

                    
                </asp:Panel>



                <asp:Panel ID="pnlOSCbyGuy" runat="server" Visible="false" >
                    <div class="divCECTitle">
                        <h1><span>On-Site Conversion</span></h1>
                        <span><% =StartDate %> - <% =EndDate%></span>
                    </div>
                    <div class="divCECDateRange">
                        <span></span>
                    </div>

                    <div class="divJobsGrid">
                        <div >
                            <asp:GridView ID="grdOSCbyGuy" runat="server" CellPadding="5" CellSpacing="1" 
                                EnableSortingAndPagingCallbacks="true" AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true" AlternatingRowStyle-BackColor="silver" CssClass="ecc_table ecc_table_nofloat" >
                            
                                <Columns >
                                    <asp:HyperLinkField DataTextField="employee" DataNavigateUrlFields="empID,startDate,endDate,employeeName" DataNavigateUrlFormatString="\ReportsView.aspx?rpt=oscguyresults&startDate={1}&endDate={2}&empID={0}&employee={3}" />
                                    <asp:BoundField ReadOnly="true" DataField="totalJobs" />
                                    <asp:BoundField ReadOnly="true" DataField="converted" />
                                    <asp:BoundField ReadOnly="true" DataField="notConverted" />
                                    <asp:BoundField ReadOnly="true" DataField="conversionPercentage" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    
                </asp:Panel>


                <asp:Panel ID="pnlOSCGuyResults" runat="server" Visible="false" >
                    <div class="divCECTitle">
                        <h1><span><% =Employee %> OSC Details</span></h1>
                        <span><% =StartDate %> - <% =EndDate%></span>
                    </div>
                    <div class="divCECDateRange">
                        <span></span>
                    </div>

                    <div class="divJobsGrid">
                        <div >
                            <asp:GridView ID="grdOSCGuyResults" runat="server" CellPadding="5" CellSpacing="1" 
                                EnableSortingAndPagingCallbacks="true" AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true" AlternatingRowStyle-BackColor="silver" CssClass="ecc_table ecc_table_nofloat" >
                            
                                <Columns >
                                    <asp:BoundField ReadOnly="true" DataField="notConvertReason" />
                                    <asp:BoundField ReadOnly="true" DataField="total" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    
                </asp:Panel>



                <asp:Panel ID="pnlDumpsByGuy" runat="server" Visible="false" >
                    <div class="divCECTitle">
                        <h1><span>Dumps By Guy</span></h1>
                        <span><% =StartDate %> - <% =EndDate%></span>
                    </div>
                    <div class="divCECDateRange">
                        <span></span>
                    </div>

                    <div class="divJobsGrid">
                        <div >
                            <asp:GridView ID="grdDumpsByGuy" runat="server" CellPadding="5" CellSpacing="1" 
                                EnableSortingAndPagingCallbacks="true" AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true" AlternatingRowStyle-BackColor="silver" CssClass="ecc_table ecc_table_nofloat" >
                            
                                <Columns >
                                    <asp:HyperLinkField DataTextField="employee" DataNavigateUrlFields="empID,startDate,endDate,employee" DataNavigateUrlFormatString="\ReportsView.aspx?rpt=dumpguyresults&startDate={1}&endDate={2}&empID={0}&employee={3}" />
                                    <asp:BoundField ReadOnly="true" DataField="revenue" DataFormatString="{0:c}" />
                                    <asp:BoundField ReadOnly="true" DataField="dumps" DataFormatString="{0:c}"/>
                                    <asp:BoundField ReadOnly="true" DataField="dumpsPercentage" DataFormatString="{0:p}"/>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    
                </asp:Panel>


                <asp:Panel ID="pnlDumpsByGuyResults" runat="server" Visible="false" >
                    <div class="divCECTitle">
                        <h1><span><% =Employee %> Dumps Details</span></h1>
                        <span><% =StartDate %> - <% =EndDate%></span>
                    </div>
                    <div class="divCECDateRange">
                        <span></span>
                    </div>

                    <div class="divJobsGrid">
                        <div >
                            <asp:GridView ID="grdDumpsByGuyResults" runat="server" CellPadding="5" CellSpacing="1" 
                                EnableSortingAndPagingCallbacks="true" AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true" AlternatingRowStyle-BackColor="silver" CssClass="ecc_table ecc_table_nofloat" >
                            
                                <Columns >
                                    <asp:BoundField ReadOnly="true" DataField="dumpLocation" />
                                    <asp:BoundField ReadOnly="true" DataField="total" DataFormatString="{0:c}" />
                                    <asp:BoundField ReadOnly="true" DataField="ticketsCount" DataFormatString="{0:0}" />
                                    <asp:BoundField ReadOnly="true" DataField="averageTicket" DataFormatString="{0:c}" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    
                </asp:Panel>


                <asp:Panel ID="pnlDiscountsGuy" runat="server" Visible="false" >
                    <div class="divCECTitle">
                        <h1><span>Discounts by Guy</span></h1>
                        <span><% =StartDate %> - <% =EndDate%></span>
                    </div>
                    <div class="divCECDateRange">
                        <span></span>
                    </div>

                    <div class="divJobsGrid">
                        <div >
                            <asp:GridView ID="grdDiscountsGuy" runat="server" CellPadding="5" CellSpacing="1" 
                                EnableSortingAndPagingCallbacks="true" AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true" AlternatingRowStyle-BackColor="silver" CssClass="ecc_table ecc_table_nofloat" >
                            
                                <Columns >
                                    <asp:BoundField ReadOnly="true" DataField="employee" />
                                    <asp:BoundField ReadOnly="true" DataField="totaljobsnum" DataFormatString="{0:0}" />
                                    <asp:BoundField ReadOnly="true" DataField="totaljobswithdiscount" DataFormatString="{0:0}" />
                                    <asp:BoundField ReadOnly="true" DataField="percentdiscount" DataFormatString="{0:p}" />
                                    <asp:BoundField ReadOnly="true" DataField="totaldiscountvalue" DataFormatString="{0:c}" />
                                    <asp:BoundField ReadOnly="true" DataField="averagediscount" DataFormatString="{0:c}" />
                                    <asp:BoundField ReadOnly="true" DataField="tippedjobswithdiscount" DataFormatString="{0:0}" />
                                    <asp:BoundField ReadOnly="true" DataField="percenttippedwithdiscount" DataFormatString="{0:p}" />
                                    <asp:BoundField ReadOnly="true" DataField="totaltipsvalue" DataFormatString="{0:c}" />
                                    <asp:BoundField ReadOnly="true" DataField="averagetips" DataFormatString="{0:c}" />
                                    <asp:BoundField ReadOnly="true" DataField="ajs" DataFormatString="{0:c}" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    
                </asp:Panel>

            </ContentTemplate>
        </asp:UpdatePanel>
        
    </div>

</asp:Content>
