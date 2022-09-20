Imports Routes.Database
Imports System.Data.SqlClient
Imports Routes.Utilities

Partial Public Class Landing
    Inherits PageBase

    Private Const GOAL_1 = "<div class=""divTop5Goal1""><strong><u>EVERY</u></strong> JOB EXPECTATIONS<br /><ul><li>1) CALL AHEAD 15-30 MINS BEFORE SCHEDULED TIME</li><li>2) ARRIVE ON-TIME </li><li>3) UP-SELL </li><li>4) CLEANUP </li><li>5) ASK FOR REFERRALS </li><li>6) ASK FOR A SIGN IN THE YARD </li><li>7) 20-30 DOORHANGERS</li></div>"
    Private Const GOAL_2 = "Revenue Goal:  "

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        RedirectToPage("start.aspx")


        'Session("userName") = "aknutson"
        'RedirectToPage("RouteEntry.aspx?rid=96171017-600e-40f5-90d6-aed8a5dca8d7")

        If Not CheckUser(Session("userName"), Session("password")).ToLower = MATCH Then
            RedirectToPage(HOME_PAGE)
        End If

        If Not Page.IsPostBack Then
            AddControlsToPlaceholder()
            LoadRoutes()
            LoadEmployees()

            Dim x As New Routes.Utilities
            txtTruckHoursMin.Text = x.ReturnTruckHoursMin
            txt30DayTruckHoursMin.Text = x.ReturnTruckHoursMin30Day
            txt90DayTruckHoursMin.Text = x.ReturnTruckHoursMin90Day
            txtYTDTruckHoursMin.Text = x.ReturnTruckHoursMinYTD
        End If

    End Sub

    Private Sub AddControlsToPlaceholder()
        Dim bRouteAssignment As Boolean = False
        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        Dim strLinks As String = String.Empty

        cmd.CommandText = "spGetUserNamePermissionPages"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@username", Session("userName"))

        Dim dt As DataTable = db.FillWithSP(cmd)
        For Each x As DataRow In dt.Rows
            If x(1).ToString.ToLower.Contains("routeassignment") Then
                bRouteAssignment = True
            Else
                strLinks += "<div class='homeButton'>"
        strLinks += "<a href='" & x(1).ToString() & "' class='button blue'>" & x(0).ToString() & "</a>"
                strLinks += "</div><div class='divEnterJobsSpacer'></div>"
            End If
        Next

        litLinks.Text = strLinks
        teamWelcomeRouteAssignmentsButton.Visible = bRouteAssignment
    End Sub

    Private Sub LoadRoutes()
        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        cmd.CommandText = "spGetAllRoutes"
        cmd.CommandType = CommandType.StoredProcedure

        Dim dli As ListItem
        Dim dt As DataTable = db.FillWithSP(cmd)
        If dt.Rows.Count <= 0 Then
            teamWelcomeEditRouteButton.Visible = False
            teamWelcomeRouteAssignmentsButton.Visible = False

        Else
            For Each dtr As DataRow In dt.Rows
                If DateDiff(DateInterval.Day, dtr("routeDate"), Now) < 180 Then
                    dli = New ListItem
                    dli.Value = dtr("id")
                    dli.Text = "Route " & dtr("routeNumber") & " - " & FormatDateTime(dtr("routeDate"), DateFormat.ShortDate)

                    ddlEditRoute.Items.Add(dli)
                    ddlRouteAssignments.Items.Add(dli)
                    ddlDeleteRoute.Items.Add(dli)
                End If
            Next
        End If

    End Sub

    Private Sub LoadEmployees()
        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        cmd.CommandText = "spGetUsers"
        cmd.CommandType = CommandType.StoredProcedure

        Dim dli As ListItem
        Dim dt As DataTable = db.FillWithSP(cmd)
        If dt.Rows.Count <= 0 Then
            teamWelcomeUpdateNPSButton.Visible = False

        Else
            For Each dtr As DataRow In dt.Rows
                dli = New ListItem
                dli.Value = dtr("id")
                dli.Text = dtr("lastName") & ", " & dtr("firstName")

                ddlEmpNPS.Items.Add(dli)
            Next
        End If

    End Sub

    Private Sub lnkGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkGo.Click
        'RedirectToPage("RouteEntry.aspx?rid=" & ddlEditRoute.SelectedValue)
        'RedirectToPage("RouteLogging.aspx?rid=" & ddlEditRoute.SelectedValue)
        'RedirectToPage("RouteCapture.aspx?rid=" & ddlEditRoute.SelectedValue)
        RedirectToPage("RouteLog.aspx?rid=" & ddlEditRoute.SelectedValue)
    End Sub

    Private Sub lnkGoRouteAssignment_Click(sender As Object, e As EventArgs) Handles lnkGoRouteAssignment.Click
        RedirectToPage("RouteAssignments.aspx?rid=" & ddlRouteAssignments.SelectedValue)
    End Sub

    Private Sub lnkCreateRoute_Click(sender As Object, e As EventArgs) Handles lnkCreateRoute.Click
        CreateRoute()
    End Sub

    Private Sub lnkDeleteRoute_Click(sender As Object, e As EventArgs) Handles lnkDeleteRoute.Click
        DeleteRoute()
    End Sub

    Private Sub DeleteRoute()
        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        Dim sRouteID As String = ddlDeleteRoute.SelectedValue

        cmd = New SqlCommand
        cmd.CommandText = "spDeleteRouteByID"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@id", sRouteID)

        db.ExecuteNonQuerySP(cmd)

        Response.Redirect("landing.aspx")

    End Sub

    Private Sub CreateRoute()

        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        cmd.CommandText = "spGetRouteByDateNumber"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@routeDate", txtCreateRouteDate.Text)
        cmd.Parameters.AddWithValue("@routeNumber", txtCreateRouteNumber.Text)

        Dim dtOuter As DataTable = db.FillWithSP(cmd)
        If dtOuter.Rows.Count > 0 Then
            'route already created....
            lblCreateError.Visible = True

        Else
            Dim sRouteID As String = Guid.NewGuid.ToString()
            cmd = New SqlCommand
            cmd.CommandText = "spInsertRouteByDate"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@id", sRouteID)
            cmd.Parameters.AddWithValue("@routeDate", txtCreateRouteDate.Text)
            cmd.Parameters.AddWithValue("@routeNumber", txtCreateRouteNumber.Text)

            db.ExecuteNonQuerySP(cmd)


            'create psRouteItems
            cmd = New SqlCommand
            cmd.CommandText = "spGetProfitShareItems"
            cmd.CommandType = CommandType.StoredProcedure

            Dim dt As DataTable = db.FillWithSP(cmd)
            For Each dtr As DataRow In dt.Rows
                cmd = New SqlCommand
                cmd.CommandText = "spInsertPSRouteItem"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@itemID", dtr("id"))
                cmd.Parameters.AddWithValue("@routeID", sRouteID)
                cmd.Parameters.AddWithValue("@itemValue", "N")

                db.ExecuteNonQuerySP(cmd)
            Next

            'create route marketing items
            cmd = New SqlCommand
            cmd.CommandText = "spGetMarketingTactics"
            cmd.CommandType = CommandType.StoredProcedure

            dt = db.FillWithSP(cmd)
            For Each dtr As DataRow In dt.Rows
                cmd = New SqlCommand
                cmd.CommandText = "spInsertMarketing"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@tacticID", dtr("id"))
                cmd.Parameters.AddWithValue("@routeID", sRouteID)

                db.ExecuteNonQuerySP(cmd)
            Next

            'create route top 5
            cmd = New SqlCommand
            cmd.CommandText = "spInsertTop5"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@id", Guid.NewGuid.ToString())
            cmd.Parameters.AddWithValue("@routeID", sRouteID)
            cmd.Parameters.AddWithValue("@goal1", GOAL_1)
            cmd.Parameters.AddWithValue("@goal1value", DBNull.Value)
            cmd.Parameters.AddWithValue("@goal2", GOAL_2)
            cmd.Parameters.AddWithValue("@goal2value", DBNull.Value)
            cmd.Parameters.AddWithValue("@goal3", DBNull.Value)
            cmd.Parameters.AddWithValue("@goal3value", DBNull.Value)
            cmd.Parameters.AddWithValue("@goal4", DBNull.Value)
            cmd.Parameters.AddWithValue("@goal4value", DBNull.Value)
            cmd.Parameters.AddWithValue("@goal5", DBNull.Value)
            cmd.Parameters.AddWithValue("@goal5value", DBNull.Value)
            cmd.Parameters.AddWithValue("@wave1", DBNull.Value)
            cmd.Parameters.AddWithValue("@wave1value", DBNull.Value)
            cmd.Parameters.AddWithValue("@wave2", DBNull.Value)
            cmd.Parameters.AddWithValue("@wave2value", DBNull.Value)
            cmd.Parameters.AddWithValue("@wave3", DBNull.Value)
            cmd.Parameters.AddWithValue("@wave3value", DBNull.Value)
            cmd.Parameters.AddWithValue("@trucklocation", DBNull.Value)
            cmd.Parameters.AddWithValue("@truckboxspace", DBNull.Value)
            cmd.Parameters.AddWithValue("@trucknotes", DBNull.Value)

            db.ExecuteNonQuerySP(cmd)


            Response.Redirect("landing.aspx")
        End If


    End Sub


    Private Sub lnkUpdateTruckHoursMin_Click(sender As Object, e As EventArgs) Handles lnkUpdateTruckHoursMin.Click
        UpdateTruckHourMin()
    End Sub

    Private Sub UpdateTruckHourMin()

        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        cmd.CommandText = "spUpdateTruckHourMin"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@newMin", txtTruckHoursMin.Text)
        db.ExecuteNonQuerySP(cmd)

        Response.Redirect("landing.aspx")

    End Sub

    Private Sub lnkUpdateTruckHoursMinReport_Click(sender As Object, e As EventArgs) Handles lnkUpdateTruckHoursMinReport.Click
        UpdateTruckHourMinReport()
    End Sub

    Private Sub UpdateTruckHourMinReport()

        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        cmd.CommandText = "spUpdateTruckHourMinReport"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@newMin30Day", txt30DayTruckHoursMin.Text)
        cmd.Parameters.AddWithValue("@newMin90Day", txt90DayTruckHoursMin.Text)
        cmd.Parameters.AddWithValue("@newMinYTD", txtYTDTruckHoursMin.Text)
        db.ExecuteNonQuerySP(cmd)

        Response.Redirect("landing.aspx")

    End Sub

    Private Sub lnkUpdateNPS_Click(sender As Object, e As EventArgs) Handles lnkUpdateNPS.Click
        RedirectToPage("EmpNPS.aspx?empid=" & ddlEmpNPS.SelectedValue)

    End Sub

    Private Sub lnkPayrollAdjustments_Click(sender As Object, e As EventArgs) Handles lnkPayrollAdjustments.Click
        RedirectToPage("payrolladjustments.aspx?empid=" & ddlEmpNPS.SelectedValue)

    End Sub
End Class