<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Route.Master" CodeBehind="Top5.aspx.vb" Inherits="Routes.Top5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">

    <div class="top5OuterDiv" >
        <div class="top5Header">
            Truck Team Daily Top 5 - Route <asp:Label ID="lblRouteNumber" runat="server" /> Entry
            <div class="rdcTitleDate" >
                <asp:Label ID="lblTodaysDate" runat="server" />
            </div>
        </div>
        
        <div>
            <div><span>1</span></div>
            <div><asp:Label ID="lblGoal1" runat="server" /></div>
            <div><asp:TextBox ID="txtGoal1Value" runat="server" /><span> / 5</span></div>
        </div>
        
        <div>
            <div><span>2</span></div>
            <div><asp:TextBox ID="txtGoal2" runat="server" /></div>
            <div><asp:TextBox ID="txtGoal2Value" runat="server" /><span> / 4</span></div>
        </div>
        <div>
            <div><span>3</span></div>
            <div><asp:TextBox ID="txtGoal3" runat="server" /></div>
            <div><asp:TextBox ID="txtGoal3Value" runat="server" /><span> / 3</span></div>
        </div>
        <div>
            <div><span>4</span></div>
            <div><asp:TextBox ID="txtGoal4" runat="server" /></div>
            <div><asp:TextBox ID="txtGoal4Value" runat="server" /><span> / 2</span></div>
        </div>
        <div>
            <div><span>5</span></div>
            <div><asp:TextBox ID="txtGoal5" runat="server" /></div>
            <div><asp:TextBox ID="txtGoal5Value" runat="server" /><span> / 1</span></div>
        </div>
        
        <div>
            Wave Planner
        </div>
        <div>
            <div><span>Morning: </span></div>
            <div><asp:TextBox ID="txtWave1" runat="server" /><asp:TextBox ID="txtWave1Value" runat="server" /><span> / 2</span></div>
        </div>
        <div>
            <div><span>Afternoon: </span></div>
            <div><asp:TextBox ID="txtWave2" runat="server" /><asp:TextBox ID="txtWave2Value" runat="server" /><span> / 2</span></div>
        </div>
        <div>
            <div><span>Evening: </span></div>
            <div><asp:TextBox ID="txtWave3" runat="server" /><asp:TextBox ID="txtWave3Value" runat="server" /><span> / 2</span></div>
        </div>
        
        <div>
            Truck Parking Planner
        </div>
        <div>
            <div><span>Truck Drop Location: </span></div>
            <div><asp:TextBox ID="txtTruckLocation" runat="server" /></div>
        </div>
        <div>
            <div><span>Available Box Space: </span></div>
            <div><asp:DropDownList ID="ddlBoxSpace" runat="server" /></div>
        </div>
        <div>
            <div><span>Truck Notes: </span></div>
            <div><asp:TextBox ID="txtTruckNotes" runat="server" /></div>
        </div>
        
        <asp:Button ID="btnSave" runat="server" Text="Save" />
        
        
    </div>
</asp:Content>