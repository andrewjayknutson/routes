<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Route.Master" CodeBehind="UpdateNPS.aspx.vb" Inherits="Routes.UpdateNPS" %>


<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">

    <div class="largeOuterDiv">

        <div class="topLeftOuterDiv topLeftOuterDivNPS">

            <div id="div5" class="enterJobsGridDiv" >
                <div class="enterJobsTitleDiv">
                    Enter/Edit User NPS Information
                </div>
                <asp:Label ID="enterJobsGridNoJobs" runat="server" CssClass="enterJobsNoJobsLabel" Visible="false" />
                <asp:GridView ID="gridJobs" runat="server" ShowFooter="true" CellPadding="0" CellSpacing="0" AutoGenerateColumns="false"  OnRowCreated="gridJobs_RowCreated" CssClass="ecc_table tableNPS" >
                    <HeaderStyle CssClass="enterJobsGridHeader" />
                    <Columns >
                        <asp:TemplateField HeaderText="" Visible="false" >
                            <ItemTemplate >
                                <asp:Label runat="server" Text='<%# Bind("id")%>' id="lblEnterJobsGridTicketID"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="enterJobsTicketIDColumn" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="enterJobsTicketIDColumn" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "fullName")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="enterJobsJobIDColumn" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Current NPS">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCurrentNPS" runat="server" CssClass="updateNPSTextBox" ></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="enterJobsTicketNumberColumn" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="YTD NPS">
                            <ItemTemplate>
                                <asp:TextBox ID="txtYTDNPS" runat="server" CssClass="updateNPSTextBox" ></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" CssClass="enterJobsTicketRevenueColumn" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="" >
                            <ItemTemplate >
                                <asp:LinkButton ID="lnkEditRev" runat="server" CssClass="button blue gridbutton" Text="SAVE" CommandName="save_data" />
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                            
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
