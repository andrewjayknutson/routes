Imports System.Data
Imports System.Data.SqlClient
Imports Routes.Database

Partial Public Class DailyProfitShareContract
    Inherits PageBase

    'THINGS TO DO
    '   - pull ROUTE ID from query string....find the const below
    Private Const ROUTE_ID = "6dd9f269-2805-44a3-8454-b37b0aa123b7"
    Protected WithEvents gridPSItems As GridView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LoadDropDowns()
            BuildRepeaters()
        End If
    End Sub

    Private Sub LoadDropDowns()

        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        cmd.CommandText = "spGetDropDownListData"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@segment", "tubSet")

        Dim li As ListItem
        Dim dt As DataTable = db.FillWithSP(cmd)
        For Each dtr As DataRow In dt.Rows
            li = New ListItem
            li.Text = dtr("displayName")
            ddlPSContractTub.Items.Add(li)
        Next

    End Sub

    Private Sub BuildRepeaters()
        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        cmd.CommandText = "spGetProfitShareGroups"
        cmd.CommandType = CommandType.StoredProcedure

        Dim sr As SqlDataReader = db.ExecuteReader(cmd)

        repeaterGroups.DataSource = sr
        repeaterGroups.DataBind()

    End Sub

    Private Sub repeaterGroups_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repeaterGroups.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim drv As System.Data.Common.DbDataRecord = CType(e.Item.DataItem, System.Data.Common.DbDataRecord)
            Dim gv As GridView = CType(e.Item.FindControl("gridPSItems"), GridView)

            Dim psRouteItems As New DataTable
            psRouteItems.Columns.Add("itemID")
            psRouteItems.Columns.Add("itemName")
            psRouteItems.Columns.Add("itemValue")

            Dim db As New Connection("Consumer_DSN")
            Dim cmd As New SqlCommand
            cmd.CommandText = "spGetRoutePSItems"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@routeID", ROUTE_ID)
            cmd.Parameters.AddWithValue("@groupID", drv("id"))

            Dim dt As DataTable = db.FillWithSP(cmd)
            gv.DataSource = dt
            gv.DataBind()

        End If

    End Sub

    Protected Sub gridPSItems_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridPSItems.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim drv As DataRowView = e.Row.DataItem
            Dim gv As CheckBox = CType(e.Row.FindControl("checkItemValue"), CheckBox)
            gv.Checked = (drv("itemValue") = "Y")
        End If

    End Sub

    Private Sub btnPSSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPSSave.Click
        'loop through every grid view checkbox....and save the results
        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand

        For Each ri As RepeaterItem In repeaterGroups.Items
            Dim gv As GridView = CType(ri.FindControl("gridPSItems"), GridView)

            For Each gvr As GridViewRow In gv.Rows

                Dim gvrCheck As CheckBox = CType(gvr.FindControl("checkItemValue"), CheckBox)
                'get userid
                Dim gvrLabel As Label = CType(gvr.FindControl("lblItemID"), Label)

                'sign them in...
                cmd = New SqlCommand
                cmd.CommandText = "spUpdatePSRouteItem"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@itemID", gvrLabel.Text)
                cmd.Parameters.AddWithValue("@routeID", ROUTE_ID)
                If gvrCheck.Checked Then
                    cmd.Parameters.AddWithValue("@itemValue", "Y")
                Else
                    cmd.Parameters.AddWithValue("@itemValue", "N")
                End If

                db.ExecuteNonQuerySP(cmd)
            Next
        Next





    End Sub

End Class