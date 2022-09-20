Imports System.Data
Imports System.Data.SqlClient
Imports Routes.Database

Public Class EmpNPS
    Inherits Utilities

    Private EMP_ID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Request.QueryString("empid") Is Nothing Then
            EMP_ID = Request.QueryString("empid")
        Else
            RedirectToPage("TeamWelcome.aspx")
        End If

        If Not Page.IsPostBack Then

            LoadNPSGrid()

        End If

    End Sub

    Private Sub gridNPS_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridNPS.RowCommand
        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand

        Dim gvrow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
        Dim gvrLabel As Label = CType(gvrow.FindControl("lblEnterNPSGridID"), Label)

        If e.CommandName.ToLower = "edit_data" Then

            cmd = New SqlCommand
            cmd.CommandText = "spGetNPSByID"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@id", gvrLabel.Text)

            Dim rs As SqlDataReader = db.ExecuteReader(cmd)
            While rs.Read
                txtEnterNPSID.Text = gvrLabel.Text
                txtEnterNPSDate.Text = rs("entryDate")
                txtEnterNPSTotal.Text = rs("total")
                txtEnterNPSPromoter.Text = rs("promoter")
                txtEnterNPSPass.Text = rs("pass")
                txtEnterNPSDetractor.Text = rs("detractor")
            End While

        ElseIf e.CommandName.ToLower = "delete_data" Then

            cmd = New SqlCommand
            cmd.CommandText = "spDeleteNPS"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@id", gvrLabel.Text)

            db.ExecuteNonQuerySP(cmd)

            txtEnterNPSID.Text = String.Empty
            txtEnterNPSDate.Text = String.Empty
            txtEnterNPSTotal.Text = String.Empty
            txtEnterNPSPromoter.Text = String.Empty
            txtEnterNPSPass.Text = String.Empty
            txtEnterNPSDetractor.Text = String.Empty

        End If

        LoadNPSGrid()


    End Sub

    Private Sub lnkNPSNew_Click(sender As Object, e As EventArgs) Handles lnkNPSNew.Click
        txtEnterNPSID.Text = String.Empty
        txtEnterNPSDate.Text = String.Empty
        txtEnterNPSTotal.Text = String.Empty
        txtEnterNPSPromoter.Text = String.Empty
        txtEnterNPSPass.Text = String.Empty
        txtEnterNPSDetractor.Text = String.Empty

        LoadNPSGrid()

    End Sub

    Private Sub lnkNPSSave_Click(sender As Object, e As EventArgs) Handles lnkNPSSave.Click
        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand

        If Not IsNumeric(txtEnterNPSTotal.Text) Then
            lblEnterNPSError.Text = "Invalid Total entered, please enter numeric value only"
            lblEnterNPSError.Visible = True
            Exit Sub
        End If

        If Not IsNumeric(txtEnterNPSPromoter.Text) Then
            lblEnterNPSError.Text = "Invalid Promoter entered, please enter numeric value only"
            lblEnterNPSError.Visible = True
            Exit Sub
        End If

        If Not IsNumeric(txtEnterNPSPass.Text) Then
            lblEnterNPSError.Text = "Invalid Pass entered, please enter numeric value only"
            lblEnterNPSError.Visible = True
            Exit Sub
        End If

        If Not IsNumeric(txtEnterNPSDetractor.Text) Then
            lblEnterNPSError.Text = "Invalid Detractor entered, please enter numeric value only"
            lblEnterNPSError.Visible = True
            Exit Sub
        End If

        cmd.CommandType = CommandType.StoredProcedure
        If String.IsNullOrEmpty(txtEnterNPSID.Text) Then
            cmd.CommandText = "spInsertNPS"
            cmd.Parameters.AddWithValue("@id", Guid.NewGuid.ToString())
        Else
            cmd.CommandText = "spUpdateNPS"
            cmd.Parameters.AddWithValue("@id", txtEnterNPSID.Text)
        End If

        cmd.Parameters.AddWithValue("@userInfoId", EMP_ID)
        cmd.Parameters.AddWithValue("@entryDate", txtEnterNPSDate.Text)
        cmd.Parameters.AddWithValue("@total", txtEnterNPSTotal.Text)
        cmd.Parameters.AddWithValue("@promoter", txtEnterNPSPromoter.Text)
        cmd.Parameters.AddWithValue("@pass", txtEnterNPSPass.Text)
        cmd.Parameters.AddWithValue("@detractor", txtEnterNPSDetractor.Text)
        cmd.Parameters.AddWithValue("@netNPS", ReturnNetNPS(txtEnterNPSTotal.Text, txtEnterNPSPromoter.Text, txtEnterNPSPass.Text, txtEnterNPSDetractor.Text))

        db.ExecuteNonQuerySP(cmd)

        txtEnterNPSID.Text = String.Empty
        txtEnterNPSDate.Text = String.Empty
        txtEnterNPSTotal.Text = String.Empty
        txtEnterNPSPromoter.Text = String.Empty
        txtEnterNPSPass.Text = String.Empty
        txtEnterNPSDetractor.Text = String.Empty
        lblEnterNPSError.Visible = False

        LoadNPSGrid()

    End Sub

    Private Sub lnkReturn_Click(sender As Object, e As EventArgs) Handles lnkReturn.Click
        RedirectToPage("landing.aspx")
    End Sub

    Private Sub LoadNPSGrid()

        Dim reNPS As New DataTable
        reNPS.Columns.Add("id")
        reNPS.Columns.Add("entryDate")
        reNPS.Columns.Add("total")
        reNPS.Columns.Add("promoter")
        reNPS.Columns.Add("pass")
        reNPS.Columns.Add("detractor")
        reNPS.Columns.Add("netNPS")

        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        cmd.CommandText = "spGetNPSByEmp"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@userInfoId", EMP_ID)

        Dim dr As DataRow
        Dim dt As DataTable = db.FillWithSP(cmd)
        For Each dtr As DataRow In dt.Rows
            dr = reNPS.NewRow

            dr("id") = dtr("id")
            dr("entryDate") = Convert.ToDateTime(dtr("entryDate")).ToString("d")
            dr("total") = dtr("total")
            dr("promoter") = dtr("promoter")
            dr("pass") = dtr("pass")
            dr("detractor") = dtr("detractor")
            dr("netNPS") = dtr("netNPS")

            reNPS.Rows.Add(dr)
        Next

        enterJobsGridNoJobs.Visible = False
        If reNPS.Rows.Count <= 0 Then
            enterJobsGridNoJobs.Text = "No NPS found for this employee..."
            enterJobsGridNoJobs.Visible = True
        End If
        gridNPS.DataSource = reNPS
        gridNPS.DataBind()

    End Sub

    Private Function ReturnNetNPS(total As String, promoter As String, pass As String, detractor As String) As String

        Dim netNPS As String = 0

        netNPS = Math.Round(((promoter / total) - (detractor / total)) * 100, 0)

        Return netNPS


    End Function

End Class