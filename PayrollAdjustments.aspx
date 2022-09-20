<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Route.Master" CodeBehind="PayrollAdjustments.aspx.vb" Inherits="Routes.PayrollAdjustments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">

    <div class="largeOuterDiv">

        <div class="topLeftOuterDiv topLeftOuterDivNPS">

            <asp:UpdatePanel ID="updatePayrollAdjustments" runat="server" UpdateMode="conditional">
                <ContentTemplate>

                    <asp:Panel ID="pnlSelectEmployee" runat="server" CssClass="enterJobsPanel" Visible="true" >
                        <div id="divRptPayroll">
                            <h1>Select Employee</h1>
                            <div id="divRptPayrollStartDate">
                                <asp:DropDownList ID="ddlEmployees" runat="server" />
                            </div>
                            <div id="divRptPayrollSubmit">
                                <asp:LinkButton ID="lnkAdjustmentEmployee" CssClass="button blue reportButton" runat="server" Text="SUBMIT" ></asp:LinkButton>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlEditAdjustments" runat="server" CssClass="enterJobsPanel" Visible="false" >

                        <div id="divEnterFuel" >
                            <div id="div1" class="enterFuelGrid" >
                                <div class="enterJobsTitleDiv">
                                    Enter/Edit/Delete Payroll Adjustments
                                </div>
                                <asp:Label ID="lblFuelNoFuel" runat="server" CssClass="enterJobsNoJobsLabel" Visible="false" />
                                <asp:GridView ID="gridPayrollAdjustmnets" runat="server" ShowFooter="true" CellPadding="0" CellSpacing="0" AutoGenerateColumns="false" CssClass="ecc_table ecc_table_payroll_adjustments" >
                                    <HeaderStyle CssClass="enterFuelGridHeader" />
                                    <Columns >
                                        <asp:TemplateField HeaderText="" Visible="false" >
                                            <ItemTemplate >
                                                <asp:Label runat="server" Text='<%# Bind("paID")%>' id="lblEnterFuelGridTicketID"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="enterFuelTicketIDColumn" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="enterFuelTicketIDColumn" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Adjustment Date">
                                            <ItemTemplate>
                                                <%#DataBinder.Eval(Container.DataItem, "adjDate")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="enterFuelDumpLocationColumn" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Adjustment Amount">
                                            <ItemTemplate>
                                                <%#DataBinder.Eval(Container.DataItem, "adjAmount")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" CssClass="enterFuelTicketTotalColumn" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" >
                                            <ItemTemplate >
                                                <asp:LinkButton ID="lnkEditFuel" runat="server" CssClass="button blue gridbutton" Text="EDIT" CommandName="edit_data" />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" >
                                            <ItemTemplate >
                                                <asp:LinkButton ID="lnkDeleteFuel" runat="server" CssClass="button blue gridbutton" Text="DELETE" CommandName="delete_data" />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            
                            </div>
                        
                            <!-- Entry panel for entering/editing dump tickets -->
                            <div id="div2" class="enterFuelEntry" >
                                <div class="enterFuelEntryTicketID">
                                    <asp:TextBox ID="txtEnterFuelTicketID" runat="server" />
                                </div>
                                <div class="enterFuelLocationLabel">
                                    <asp:Label runat="server" ID="Label1" Text="Adjustment Date:" />
                                </div>
                                <div class="enterFuelLocationLabel" >
                                    <asp:TextBox ID="txtEnterFuelFuelLocation" runat="server" />
                                </div>
                                <div class="enterFuelLocationLabel">
                                    <asp:Label runat="server" ID="Label3" Text="Adjustment Amount:" />
                                </div>
                                <div class="enterFuelLocationLabel">
                                    <asp:TextBox ID="txtEnterFuelTicketTotal" runat="server" />
                                </div>
                                <div class="enterFuelLocationLabel">
                                    <asp:Label ID="lblFuelError" runat="server" ForeColor="Red" Visible="false" />
                                </div>
                            
                                <div class="enterFuelSaveButton">
                                    <div class="enterJobsButtonEdit">
                                        <asp:LinkButton ID="lnkFuelSave" runat="server" CssClass="button blue" Text="SAVE" />
                                    </div>
                                </div>

                            </div>
                        </div>  


                    </asp:Panel>



                </ContentTemplate>
            </asp:UpdatePanel>

        </div>

        <div class="topRightOuterDiv">

            <div class="topRightOuterButtonDiv">
                <asp:LinkButton ID="lnkReturn" runat="server" CssClass="button blue" Text="RETURN" />
            </div>
            <div style="clear:both;"></div>

        </div>

    </div>

</asp:Content>
