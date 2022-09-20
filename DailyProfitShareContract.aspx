<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Route.Master" CodeBehind="DailyProfitShareContract.aspx.vb" Inherits="Routes.DailyProfitShareContract" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">

    <div id="divPSContractOuter" class="divPSContractOuter" >
        <!-- route date                              tub/clipboard set -->
        <div id="divPSContractHeader" class="divPSContractHeader" >
            <div id="div1" class="divPSContractDate" >
                Friday, August 21, 2011
            </div>
            <div id="div2" class="divPSContractTub" >
                Which tub/clipboard set do you have today:  <asp:DropDownList ID="ddlPSContractTub" runat="server" />
            </div>
        </div>

        <!-- repeater of groups -->
        <asp:Repeater id="repeaterGroups" runat="server" >
            <ItemTemplate>
                <div class="divPSContractGroupsOuter">
                    <div class="divPSContractGroupsHeader">
                        <span class="spanContractGroupsHeader"><%#DataBinder.Eval(Container.DataItem, "displayName")%></span>
                    </div>
                    <asp:GridView ID="gridPSItems" runat="server" CellPadding="0" CellSpacing="0" AutoGenerateColumns="false"  CssClass="dailyPSGridItems" OnRowDataBound="gridPSItems_RowDataBound" >
                        <HeaderStyle CssClass="dailyPSGridHeader" />
                        <Columns >
                            <asp:TemplateField HeaderText="" >
                                <ItemTemplate >
                                    <asp:Label runat="server" Text='<%# Bind("itemID") %>' id="lblItemID"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="dailyPSItemIDColumn" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="dailyPSItemIDColumn" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "itemName")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="dailyPSItemNameColumn" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Complete">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="checkItemValue" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="dailyPSItemValueColumn" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <div id="divPSContractButtons" class="divPSContractButtons" >
            <asp:Button ID="btnPSSave" runat="server" Text="Save" />
            <asp:Button ID="btnPSReturn" runat="server" Text="Return" />
        </div>

    </div>

</asp:Content>
