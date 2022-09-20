Imports System.Data
Imports System.Data.SqlClient
Imports Routes.Database

Partial Public Class TeamWelcome
    Inherits PageBase

    Private Const GOAL_1 = "<div class=""divTop5Goal1""><strong><u>EVERY</u></strong> JOB EXPECTATIONS<br /><ul><li>1) CALL AHEAD 15-30 MINS BEFORE SCHEDULED TIME</li><li>2) ARRIVE ON-TIME </li><li>3) UP-SELL </li><li>4) CLEANUP </li><li>5) ASK FOR REFERRALS </li><li>6) ASK FOR A SIGN IN THE YARD </li><li>7) 20-30 DOORHANGERS</li></div>"
    Private Const GOAL_2 = "Revenue Goal:  "

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        RedirectToPage("welcome.aspx")


        If Not Page.IsPostBack Then
            LoadTeamMembers()
            LoadEditableRoutes()

            'divAnchorHome.Visible = False
            'If Not String.IsNullOrEmpty(Session("username")) Then
            '    divAnchorHome.Visible = Not (Session("username").ToString.ToLower = "truckteam")
            'End If

            'load route numbers...
            Dim db As New Connection("Consumer_DSN")
            Dim cmd As New SqlCommand
            cmd.CommandText = "spGetDropDownListData"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@segment", "routeNumber")

            Dim rs As SqlDataReader = db.ExecuteReader(cmd)
            While rs.Read
                ddlRouteNumbers.Items.Add(rs("displayName"))
            End While
        End If



    End Sub

    Protected Sub gridTeamMembers_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridTeamMembers.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim output As CheckBox = CType(e.Row.FindControl("checkTeamMember"), CheckBox)
            output.Checked = False

        End If
    End Sub






    Private Sub LoadTeamMembers()

        Dim tmToday As New DataTable
        tmToday.Columns.Add("userID")
        tmToday.Columns.Add("firstName")
        tmToday.Columns.Add("lastName")
        tmToday.Columns.Add("routeNumber")
        tmToday.Columns.Add("signIn")
        tmToday.Columns.Add("signOut")

        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        cmd.CommandText = "spGetUsersByPermission"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("permissionLevel", "tt")

        Dim dr As DataRow
        Dim dt As DataTable = db.FillWithSP(cmd)
        For Each dtr As DataRow In dt.Rows
            If Not dtr("firstName").ToString.ToLower = "truck" AndAlso Not dtr("lastName").ToString.ToLower = "team" AndAlso dtr("active").ToString.ToLower = "y" Then
                dr = tmToday.NewRow

                dr("userID") = dtr("id")
                dr("firstName") = dtr("firstName")
                dr("lastName") = dtr("lastName")

                cmd = New SqlCommand
                cmd.CommandText = "spGetTodayTeamMemberData"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@userID", dtr("id"))
                cmd.Parameters.AddWithValue("@routeDate", Today)

                Dim sdr As SqlDataReader = db.ExecuteReader(cmd)
                If sdr.HasRows Then
                    'is logged in
                    sdr.Read()
                    dr("routeNumber") = sdr("routeNumber")
                    dr("signIn") = sdr("signIn")
                    dr("signOut") = sdr("signOut")
                End If

                tmToday.Rows.Add(dr)
            End If

        Next

        gridTeamMembers.DataSource = tmToday
        gridTeamMembers.DataBind()

    End Sub

    Private Sub LoadEditableRoutes()

        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        cmd.CommandText = "spGetRouteByDate"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@routeDate", Today)

        Dim dli As ListItem
        Dim dt As DataTable = db.FillWithSP(cmd)
        If dt.Rows.Count <= 0 Then
            teamWelcomeEditRouteButton.Visible = False

        Else
            For Each dtr As DataRow In dt.Rows
                dli = New ListItem
                dli.Value = dtr("id")
                dli.Text = dtr("routeNumber")

                ddlEditRoute.Items.Add(dli)
            Next
        End If

    End Sub

    Private Sub btnLogOut_Click(sender As Object, e As EventArgs) Handles btnLogOut.Click
        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand

        For Each gvr As GridViewRow In gridTeamMembers.Rows

            Dim gvrCheck As CheckBox = CType(gvr.FindControl("checkTeamMember"), CheckBox)
            If gvrCheck.Checked Then
                'get userid
                Dim gvrLabel As Label = CType(gvr.FindControl("lblUserID"), Label)

                'sign them in...
                cmd = New SqlCommand
                cmd.CommandText = "spUpdateSignIn"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@userID", gvrLabel.Text)
                cmd.Parameters.AddWithValue("@signIn", Today)

                db.ExecuteNonQuerySP(cmd)

            End If

        Next

        LoadTeamMembers()

    End Sub

    Private Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        RedirectToPage("Landing.aspx")
    End Sub

    Private Sub btnLogIn_Click(sender As Object, e As EventArgs) Handles btnLogIn.Click
        'check if route already exists
        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        cmd.CommandText = "spGetRouteByDateNumber"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@routeDate", Today)
        cmd.Parameters.AddWithValue("@routeNumber", ddlRouteNumbers.SelectedItem.Value)

        Dim sRouteID As String = String.Empty
        Dim rs As SqlDataReader = db.ExecuteReader(cmd)
        If rs.HasRows Then
            'route already created....
            rs.Read()
            sRouteID = rs("id")
        Else
            'route doesn't exist....
            sRouteID = Guid.NewGuid.ToString()

            cmd = New SqlCommand
            cmd.CommandText = "spInsertRoute"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@id", sRouteID)
            cmd.Parameters.AddWithValue("@routeNumber", ddlRouteNumbers.SelectedItem.Value)

            db.ExecuteNonQuerySP(cmd)

            '******************************************************************
            'insert start of day dump
            cmd = New SqlCommand
            cmd.CommandText = "spInsertDump"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@id", Guid.NewGuid.ToString())
            cmd.Parameters.AddWithValue("@routeID", sRouteID)
            cmd.Parameters.AddWithValue("@dumpLocation", "Not My Junk                                       ")
            cmd.Parameters.AddWithValue("@ticketNumber", "")
            cmd.Parameters.AddWithValue("@ticketTons", "")
            cmd.Parameters.AddWithValue("@ticketDetails", "")
            cmd.Parameters.AddWithValue("@ticketTotal", "0")

            db.ExecuteNonQuerySP(cmd)


            'insert end of day dump
            cmd = New SqlCommand
            cmd.CommandText = "spInsertDump"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@id", Guid.NewGuid.ToString())
            cmd.Parameters.AddWithValue("@routeID", sRouteID)
            cmd.Parameters.AddWithValue("@dumpLocation", "Junk Left In Truck                                ")
            cmd.Parameters.AddWithValue("@ticketNumber", "")
            cmd.Parameters.AddWithValue("@ticketTons", "")
            cmd.Parameters.AddWithValue("@ticketDetails", "")
            cmd.Parameters.AddWithValue("@ticketTotal", "0")

            db.ExecuteNonQuerySP(cmd)

            'insert large tvs dump
            cmd = New SqlCommand
            cmd.CommandText = "spInsertDump"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@id", Guid.NewGuid.ToString())
            cmd.Parameters.AddWithValue("@routeID", sRouteID)
            cmd.Parameters.AddWithValue("@dumpLocation", "Large TV                                          ")
            cmd.Parameters.AddWithValue("@ticketNumber", "")
            cmd.Parameters.AddWithValue("@ticketTons", "")
            cmd.Parameters.AddWithValue("@ticketDetails", "")
            cmd.Parameters.AddWithValue("@ticketTotal", "0")

            db.ExecuteNonQuerySP(cmd)

            'insert small tvs dump
            cmd = New SqlCommand
            cmd.CommandText = "spInsertDump"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@id", Guid.NewGuid.ToString())
            cmd.Parameters.AddWithValue("@routeID", sRouteID)
            cmd.Parameters.AddWithValue("@dumpLocation", "Small TV                                          ")
            cmd.Parameters.AddWithValue("@ticketNumber", "")
            cmd.Parameters.AddWithValue("@ticketTons", "")
            cmd.Parameters.AddWithValue("@ticketDetails", "")
            cmd.Parameters.AddWithValue("@ticketTotal", "0")

            db.ExecuteNonQuerySP(cmd)

            'insert mattresses dump
            cmd = New SqlCommand
            cmd.CommandText = "spInsertDump"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@id", Guid.NewGuid.ToString())
            cmd.Parameters.AddWithValue("@routeID", sRouteID)
            cmd.Parameters.AddWithValue("@dumpLocation", "Mattress                                          ")
            cmd.Parameters.AddWithValue("@ticketNumber", "")
            cmd.Parameters.AddWithValue("@ticketTons", "")
            cmd.Parameters.AddWithValue("@ticketDetails", "")
            cmd.Parameters.AddWithValue("@ticketTotal", "0")

            db.ExecuteNonQuerySP(cmd)

            'insert tires dump
            cmd = New SqlCommand
            cmd.CommandText = "spInsertDump"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@id", Guid.NewGuid.ToString())
            cmd.Parameters.AddWithValue("@routeID", sRouteID)
            cmd.Parameters.AddWithValue("@dumpLocation", "Tires                                             ")
            cmd.Parameters.AddWithValue("@ticketNumber", "")
            cmd.Parameters.AddWithValue("@ticketTons", "")
            cmd.Parameters.AddWithValue("@ticketDetails", "")
            cmd.Parameters.AddWithValue("@ticketTotal", "0")

            db.ExecuteNonQuerySP(cmd)


            '******************************************************************


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

        End If

        For Each gvr As GridViewRow In gridTeamMembers.Rows

            Dim gvrCheck As CheckBox = CType(gvr.FindControl("checkTeamMember"), CheckBox)
            If gvrCheck.Checked Then
                'get userid
                Dim gvrLabel As Label = CType(gvr.FindControl("lblUserID"), Label)
                Dim gvrSignIn As Label = CType(gvr.FindControl("lblSignIn"), Label)

                '**********************************
                'need to check whether or not they are already signed in..
                '**********************************
                cmd = New SqlCommand
                cmd.CommandText = "spGetIsUserSignedIn"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@routeDate", Today)
                cmd.Parameters.AddWithValue("@userID", gvrLabel.Text)

                Dim sdr As SqlDataReader = db.ExecuteReader(cmd)
                If Not sdr.HasRows Then
                    'sign them in...
                    cmd = New SqlCommand
                    cmd.CommandText = "spInsertSignIn"
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@id", Guid.NewGuid.ToString())
                    cmd.Parameters.AddWithValue("@userID", gvrLabel.Text)
                    cmd.Parameters.AddWithValue("@routeID", sRouteID)

                    db.ExecuteNonQuerySP(cmd)
                Else
                    'keep sign in time....just change route
                    cmd = New SqlCommand
                    cmd.CommandText = "spDeleteSignInByUserRoute"
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@userID", gvrLabel.Text)
                    cmd.Parameters.AddWithValue("@signIn", gvrSignIn.Text)

                    db.ExecuteNonQuerySP(cmd)

                    'sign them in with the same time...
                    cmd = New SqlCommand
                    cmd.CommandText = "spInsertSignInWithTime"
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@id", Guid.NewGuid.ToString())
                    cmd.Parameters.AddWithValue("@userID", gvrLabel.Text)
                    cmd.Parameters.AddWithValue("@routeID", sRouteID)
                    cmd.Parameters.AddWithValue("@signIn", gvrSignIn.Text)

                    db.ExecuteNonQuerySP(cmd)
                End If
            End If
        Next

        RedirectToPage("TeamWelcome.aspx")


    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        'RedirectToPage("RouteEntry.aspx?rid=" & ddlEditRoute.SelectedValue)
        'RedirectToPage("RouteLogging.aspx?rid=" & ddlEditRoute.SelectedValue)
        'RedirectToPage("RouteCapture.aspx?rid=" & ddlEditRoute.SelectedValue)
        RedirectToPage("RouteLog.aspx?rid=" & ddlEditRoute.SelectedValue)
    End Sub
End Class