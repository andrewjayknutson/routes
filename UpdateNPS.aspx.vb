Imports System.Data
Imports System.Data.SqlClient
Imports Routes.Database

Public Class UpdateNPS
    Inherits Utilities

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            LoadUsers()

        End If

    End Sub

    Private Sub LoadUsers()

        Dim reUsers As New DataTable
        reUsers.Columns.Add("id")
        reUsers.Columns.Add("fullName")
        reUsers.Columns.Add("currentNPS")
        reUsers.Columns.Add("YTDNPS")

        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        cmd.CommandText = "spGetUsers"
        cmd.CommandType = CommandType.StoredProcedure

        Dim dr As DataRow
        Dim dt As DataTable = db.FillWithSP(cmd)
        For Each dtr As DataRow In dt.Rows
            If dtr("active").ToString.ToLower = "y" Then
                dr = reUsers.NewRow

                dr("id") = dtr("id")
                dr("fullName") = dtr("lastName") & ", " & dtr("firstName")
                dr("currentNPS") = dtr("currentNPS")
                dr("YTDNPS") = dtr("ytdNPS")

                reUsers.Rows.Add(dr)
            End If
        Next

        'lblFuelNoFuel.Visible = False
        'If reFuel.Rows.Count <= 0 Then
        '    lblFuelNoFuel.Text = "No fuel found for this route/day..."
        '    lblFuelNoFuel.Visible = True
        'End If

        gridJobs.DataSource = reUsers
        gridJobs.DataBind()

    End Sub

    Private Sub gridJobs_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridJobs.RowCommand
        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand

        Dim gvrow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
        Dim gvrLabel As Label = CType(gvrow.FindControl("lblEnterJobsGridTicketID"), Label)
        Dim gvrTextNPS As TextBox = CType(gvrow.FindControl("txtCurrentNPS"), TextBox)
        Dim gvrTextYTDNPS As TextBox = CType(gvrow.FindControl("txtYTDNPS"), TextBox)

        If e.CommandName.ToLower = "save_data" Then

            cmd = New SqlCommand
            cmd.CommandText = "spUpdateUserNPS"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@id", gvrLabel.Text)
            cmd.Parameters.AddWithValue("@currentNPS", gvrTextNPS.Text)
            cmd.Parameters.AddWithValue("@ytdNPS", gvrTextYTDNPS.Text)

            db.ExecuteNonQuerySP(cmd)

        End If

        enterJobsGridNoJobs.Text = "Updated successfully...."
        enterJobsGridNoJobs.Visible = True


    End Sub

    Public Sub gridJobs_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gridJobs.RowCreated

    End Sub

    Private Sub gridJobs_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gridJobs.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            ' Retrieve the LinkButton control from the first column.
            Dim gvrTextNPS As TextBox = CType(e.Row.FindControl("txtCurrentNPS"), TextBox)
            Dim gvrTextYTDNPS As TextBox = CType(e.Row.FindControl("txtYTDNPS"), TextBox)

            Dim x As DataRowView
            x = e.Row.DataItem

            gvrTextNPS.Text = 0
            If Not x("currentNPS") Is Nothing Then
                If Not IsDBNull(x("currentNPS")) Then
                    gvrTextNPS.Text = x("currentNPS")
                End If
            End If

            gvrTextYTDNPS.Text = 0
            If Not x("ytdNPS") Is Nothing Then
                If Not IsDBNull(x("ytdNPS")) Then
                    gvrTextYTDNPS.Text = x("ytdNPS")
                End If
            End If

        End If
    End Sub

    Private Sub lnkReturn_Click(sender As Object, e As EventArgs) Handles lnkReturn.Click
        Response.Redirect("Landing.aspx")
    End Sub
End Class