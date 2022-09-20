Imports System.Data
Imports System.Data.SqlClient
Imports Routes.Database

Partial Public Class EditUsers
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'every time a page is loaded, check to make sure valid user and is logged in
        If Not CheckUser(Session("userName"), Session("password")).ToLower = MATCH Then
            RedirectToPage(HOME_PAGE)
        End If

        FillGrid()

    End Sub

    Public Sub FillGrid()
        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        cmd.CommandText = "spGetUsers"
        cmd.CommandType = CommandType.StoredProcedure
        Dim da As SqlDataReader = db.ExecuteReader(cmd)
        Dim contacts As New DataTable
        contacts.Load(da)

        If (contacts.Rows.Count > 0) Then
            grdContact.DataSource = contacts
            grdContact.DataBind()
        End If
    End Sub

    Public Sub grdContact_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdContact.RowCancelingEdit

    End Sub

    Public Sub grdContact_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdContact.RowCommand

    End Sub

    Public Sub grdContact_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdContact.RowDataBound

    End Sub

    Public Sub grdContact_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdContact.RowDeleting
        Dim lblUsername As Label = grdContact.Rows(e.RowIndex).Cells(0).FindControl("lblusername")

        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        cmd.CommandText = "spDeleteUserByUsername"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@username", lblUsername.Text)
        db.ExecuteNonQuerySP(cmd)

        RedirectToPage("EditUsers.aspx")

    End Sub

    Public Sub grdContact_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdContact.RowEditing
        Dim lblUsername As Label = grdContact.Rows(e.NewEditIndex).Cells(0).FindControl("lblusername")
        RedirectToPage("EditUser.aspx?user=" & lblUsername.Text)
    End Sub

    Public Sub grdContact_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdContact.RowUpdating

    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        RedirectToPage("landing.aspx")
    End Sub
End Class