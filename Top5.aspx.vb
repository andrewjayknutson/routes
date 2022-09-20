Imports System.Data
Imports System.Data.SqlClient
Imports Routes.Database

Partial Public Class Top5
    Inherits PageBase

    Private ROUTE_ID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Request.QueryString("rid") Is Nothing Then
            ROUTE_ID = Request.QueryString("rid")
        Else
            RedirectToPage("teamwelcome.aspx")
        End If

        If Not Page.IsPostBack Then
            LoadRouteInformation()
            LoadDropDowns()
            LoadRouteTop5()

        End If
    End Sub

    Private Sub LoadRouteInformation()
        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        cmd.CommandText = "spGetRouteInfo"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@routeID", ROUTE_ID)

        Dim rs As SqlDataReader = db.ExecuteReader(cmd)
        While rs.Read
            lblRouteNumber.Text = rs("routeNumber")
            lblTodaysDate.Text = Convert.ToDateTime(rs("routeDate")).ToLongDateString()
        End While

    End Sub

    Private Sub LoadDropDowns()

        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        cmd.CommandText = "spGetDropDownListData"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@segment", "loadSize")

        Dim li As ListItem
        Dim dt As DataTable = db.FillWithSP(cmd)
        For Each dtr As DataRow In dt.Rows
            li = New ListItem
            li.Text = dtr("displayName")
            ddlBoxSpace.Items.Add(li)
        Next

    End Sub

    Private Sub LoadRouteTop5()

        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        cmd.CommandText = "spGetTop5ByRouteID"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@routeID", ROUTE_ID)

        Dim dt As DataTable = db.FillWithSP(cmd)
        For Each dtr As DataRow In dt.Rows
            lblGoal1.Text = dtr("goal1").ToString()
            txtGoal1Value.Text = dtr("goal1value").ToString()
            txtGoal2.Text = dtr("goal2").ToString()
            txtGoal2Value.Text = dtr("goal2value").ToString()
            txtGoal3.Text = dtr("goal3").ToString()
            txtGoal3Value.Text = dtr("goal3value").ToString()
            txtGoal4.Text = dtr("goal4").ToString()
            txtGoal4Value.Text = dtr("goal4value").ToString()
            txtGoal5.Text = dtr("goal5").ToString()
            txtGoal5Value.Text = dtr("goal5value").ToString()
            txtWave1.Text = dtr("wave1").ToString()
            txtWave1Value.Text = dtr("wave1value").ToString()
            txtWave2.Text = dtr("wave2").ToString()
            txtWave2Value.Text = dtr("wave2value").ToString()
            txtWave3.Text = dtr("wave3").ToString()
            txtWave3Value.Text = dtr("wave3value").ToString()
            txtTruckLocation.Text = dtr("trucklocation").ToString()
            ddlBoxSpace.Text = dtr("truckboxspace").ToString()
            txtTruckNotes.Text = dtr("trucknotes").ToString()
        Next

    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        cmd.CommandText = "spUpdateTop5"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@routeID", ROUTE_ID)
        cmd.Parameters.AddWithValue("@goal1value", txtGoal1Value.Text)
        cmd.Parameters.AddWithValue("@goal2", txtGoal2.Text)
        cmd.Parameters.AddWithValue("@goal2value", txtGoal2Value.Text)
        cmd.Parameters.AddWithValue("@goal3", txtGoal3.Text)
        cmd.Parameters.AddWithValue("@goal3value", txtGoal3Value.Text)
        cmd.Parameters.AddWithValue("@goal4", txtGoal4.Text)
        cmd.Parameters.AddWithValue("@goal4value", txtGoal4Value.Text)
        cmd.Parameters.AddWithValue("@goal5", txtGoal5.Text)
        cmd.Parameters.AddWithValue("@goal5value", txtGoal5Value.Text)
        cmd.Parameters.AddWithValue("@wave1", txtWave1.Text)
        cmd.Parameters.AddWithValue("@wave1value", txtWave1Value.Text)
        cmd.Parameters.AddWithValue("@wave2", txtWave2.Text)
        cmd.Parameters.AddWithValue("@wave2value", txtWave2Value.Text)
        cmd.Parameters.AddWithValue("@wave3", txtWave3.Text)
        cmd.Parameters.AddWithValue("@wave3value", txtWave3Value.Text)
        cmd.Parameters.AddWithValue("@trucklocation", txtTruckLocation.Text)
        cmd.Parameters.AddWithValue("@truckboxspace", ddlBoxSpace.Text)
        cmd.Parameters.AddWithValue("@trucknotes", txtTruckNotes.Text)

        db.ExecuteNonQuerySP(cmd)

    End Sub
End Class