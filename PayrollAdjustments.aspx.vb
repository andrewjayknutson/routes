Imports System.Data
Imports System.Data.SqlClient
Imports Routes.Database

Public Class PayrollAdjustments
    Inherits PageBase

    Private empID As String = String.Empty

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not CheckUser(Session("userName"), Session("password")).ToLower = MATCH Then
            RedirectToPage(HOME_PAGE)
        End If

        If Not Request.QueryString("empID") Is Nothing Then
            empID = Request.QueryString("empID")
        End If

        If Not Page.IsPostBack Then
            If String.IsNullOrEmpty(empID) Then
                pnlSelectEmployee.Visible = True
                pnlEditAdjustments.Visible = False
                LoadEmployees()
            Else
                pnlSelectEmployee.Visible = False
                pnlEditAdjustments.Visible = True
                LoadAdjustments()
            End If
        End If
    End Sub

    Private Sub LoadEmployees()
        Dim db As New Connection("Consumer_DSN")

        Dim cmd As New SqlCommand
        cmd.CommandText = "spGetUsers"
        cmd.CommandType = CommandType.StoredProcedure

        Dim dtEmps As New DataTable
        dtEmps = db.FillWithSP(cmd)
        For Each dtewhr As DataRow In dtEmps.Rows
            Dim spLI As New ListItem
            spLI.Text = dtewhr("lastName") & ", " & dtewhr("firstname")
            spLI.Value = dtewhr("id")

            ddlEmployees.Items.Add(spLI)
        Next

    End Sub

    Private Sub lnkAdjustmentEmployee_Click(sender As Object, e As EventArgs) Handles lnkAdjustmentEmployee.Click
        RedirectToPage("PayrollAdjustments.aspx?empID=" & ddlEmployees.SelectedValue)
    End Sub

    Private Sub LoadAdjustments()
        Dim reAdjustments As New DataTable
        reAdjustments.Columns.Add("paID")
        reAdjustments.Columns.Add("adjDate")
        reAdjustments.Columns.Add("adjAmount")

        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        cmd.CommandText = "spGetPayrollAdjustment"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@userID", empID)

        Dim dr As DataRow
        Dim dt As DataTable = db.FillWithSP(cmd)
        For Each dtr As DataRow In dt.Rows
            dr = reAdjustments.NewRow

            dr("paID") = dtr("paID")
            dr("adjDate") = dtr("adjDate")
            dr("adjAmount") = dtr("adjAmount")

            reAdjustments.Rows.Add(dr)
        Next

        lblFuelNoFuel.Visible = False
        If reAdjustments.Rows.Count <= 0 Then
            lblFuelNoFuel.Text = "No adjustments found for this employee..."
            lblFuelNoFuel.Visible = True
        End If

        gridPayrollAdjustmnets.DataSource = reAdjustments
        gridPayrollAdjustmnets.DataBind()

    End Sub

    Private Sub gridPayrollAdjustmnets_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridPayrollAdjustmnets.RowCommand
        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand

        Dim gvrow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
        Dim gvrLabel As Label = CType(gvrow.FindControl("lblEnterFuelGridTicketID"), Label)

        If e.CommandName.ToLower = "edit_data" Then
            cmd = New SqlCommand
            cmd.CommandText = "spGetPayrollAdjustmentByID"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@id", gvrLabel.Text)

            Dim rs As SqlDataReader = db.ExecuteReader(cmd)
            While rs.Read
                txtEnterFuelTicketID.Text = gvrLabel.Text
                txtEnterFuelFuelLocation.Text = rs("adjDate")
                txtEnterFuelTicketTotal.Text = rs("adjAmount")
            End While


        ElseIf e.CommandName.ToLower = "delete_data" Then
            cmd = New SqlCommand
            cmd.CommandText = "spDeletePayrollAdjustment"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@id", gvrLabel.Text)

            db.ExecuteNonQuerySP(cmd)
            txtEnterFuelTicketID.Text = String.Empty
            txtEnterFuelFuelLocation.Text = String.Empty
            txtEnterFuelTicketTotal.Text = String.Empty

        End If

        lblFuelError.Visible = False
        LoadAdjustments()



    End Sub

    Private Sub lnkFuelSave_Click(sender As Object, e As EventArgs) Handles lnkFuelSave.Click
        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand

        Dim iTicketTotal As String = 0

        If Not IsNumeric(txtEnterFuelTicketTotal.Text) Then
            lblFuelError.Text = "Invalid adjustment total entered, please enter numeric value only"
            lblFuelError.Visible = True
            Exit Sub
        Else
            iTicketTotal = txtEnterFuelTicketTotal.Text
        End If

        cmd.CommandType = CommandType.StoredProcedure
        If String.IsNullOrEmpty(txtEnterFuelTicketID.Text) Then
            cmd.CommandText = "spInsertPayrollAdjustment"
            cmd.Parameters.AddWithValue("@userID", empID)
        Else
            cmd.CommandText = "spUpdatePayrollAdjustment"
            cmd.Parameters.AddWithValue("@id", txtEnterFuelTicketID.Text)
        End If
        cmd.Parameters.AddWithValue("@adjDate", txtEnterFuelFuelLocation.Text)
        cmd.Parameters.AddWithValue("@adjAmount", txtEnterFuelTicketTotal.Text)

        db.ExecuteNonQuerySP(cmd)

        txtEnterFuelTicketID.Text = String.Empty
        txtEnterFuelFuelLocation.Text = String.Empty
        txtEnterFuelTicketTotal.Text = String.Empty
        lblFuelError.Visible = False

        LoadAdjustments()
    End Sub

    Private Sub lnkReturn_Click(sender As Object, e As EventArgs) Handles lnkReturn.Click
        RedirectToPage("landing.aspx")
    End Sub
End Class