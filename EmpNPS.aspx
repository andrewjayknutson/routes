<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Route.Master" CodeBehind="EmpNPS.aspx.vb" Inherits="Routes.EmpNPS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">

        <div class="largeOuterDiv">

        <div class="topLeftOuterDiv topLeftOuterDivNPS topLeftOuterDivEmpNPS">

            <div class="enterJobsTitleDiv">
                Employee NPS
            </div>

            <div class="enterFuelGrid">

                <asp:Label ID="enterJobsGridNoJobs" runat="server" CssClass="enterJobsNoJobsLabel" Visible="false" />
                <asp:GridView ID="gridNPS" runat="server" ShowFooter="true" CellPadding="0" CellSpacing="0" AutoGenerateColumns="false"  CssClass="ecc_table" >
                    <HeaderStyle CssClass="enterJobsGridHeader" />
                    <Columns >
                        <asp:TemplateField HeaderText="" Visible="false" >
                            <ItemTemplate >
                                <asp:Label runat="server" Text='<%# Bind("id") %>' id="lblEnterNPSGridID"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="enterJobsTicketIDColumn" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="enterJobsTicketIDColumn" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "entryDate")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="enterJobsJobIDColumn" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Total">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "total")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="enterJobsTicketNumberColumn" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Promoter">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "promoter")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" CssClass="enterJobsTicketRevenueColumn" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Pass">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "pass")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" CssClass="enterJobsTicketDiscountColumn" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Detractor">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "detractor")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" CssClass="enterJobsTicketCECColumn" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Net NPS">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "netNPS")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" CssClass="enterJobsTicketNetColumn" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="" >
                            <ItemTemplate >
                                <asp:LinkButton ID="lnkEditRev" runat="server" CssClass="button blue gridbutton" Text="EDIT" CommandName="edit_data" />
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" >
                            <ItemTemplate >
                                <asp:LinkButton ID="lnkDeleteRev" runat="server" CssClass="button blue gridbutton" Text="DELETE" CommandName="delete_data" />
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" />
                        </asp:TemplateField>


                    </Columns>
                </asp:GridView>
            </div>

            <!-- Entry panel for entering/editing dump tickets -->
            <div id="div6" class="enterJobsEntry enterFuelEntry" >
                <div class="enterJobsEntryTicketID">
                    <asp:TextBox ID="txtEnterNPSID" runat="server" />
                </div>
                <div class="enterJobsEntryJobIDLabel">
                    Date:
                </div>
                <div class="enterJobsEntryJobID">
                    <asp:TextBox ID="txtEnterNPSDate" runat="server" />
                </div>
                <div class="enterJobsEntryTicketNumberLabel">
                    Total:
                </div>
                <div class="enterJobsEntryTicketNumber">
                    <asp:TextBox ID="txtEnterNPSTotal" runat="server" />
                </div>
                <div class="enterJobsEntryRevenueLabel">
                    Promoter:
                </div>
                <div class="enterJobsEntryRevenue">
                    <asp:TextBox ID="txtEnterNPSPromoter" runat="server" />
                </div>
                <div class="enterJobsEntryDiscountLabel">
                    Pass:
                </div>
                <div class="enterJobsEntryDiscount">
                    <asp:TextBox ID="txtEnterNPSPass" runat="server" />
                </div>
                <div class="enterJobsEntryCECLabel">
                    Detractor:
                </div>
                <div class="enterJobsEntryCEC">
                    <asp:TextBox ID="txtEnterNPSDetractor" runat="server" />
                </div>



                <div class="enterJobsEntryDetailsLabel">
                    <asp:Label ID="lblEnterNPSError" runat="server" ForeColor="Red" Visible="false" />
                </div>
                            
                <div class="enterJobsButtonsSave enterNPSButtonsSave">
                    <div class="enterJobsButtonEdit">
                        <asp:LinkButton ID="lnkNPSSave" runat="server" CssClass="button blue" Text="SAVE" />
                    </div>
                    <div class="enterJobsButtonsDelete">
                        <asp:LinkButton ID="lnkNPSNew" runat="server" CssClass="button blue" Text="NEW" />
                    </div>
                </div>
                            
            </div>


        </div>

        <div class="topRightOuterDiv">

            <div class="topRightOuterButtonDiv">
                <asp:LinkButton ID="lnkReturn" runat="server" CssClass="button blue" Text="RETURN" />
            </div>
            <div style="clear:both;"></div>

        </div>

    </div>



</asp:Content>
