Imports System.Data
Imports System.Data.SqlClient
Imports Routes.Database


Public Class EditUsersInfo
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not CheckUser(Session("userName"), Session("password")).ToLower = MATCH Then
            RedirectToPage(HOME_PAGE)
        End If

        FillGrid()

    End Sub

    Public Sub FillGrid()
        Dim dr As DataRow
        Dim dtUsers As New DataTable
        dtUsers.Columns.Add("userID")
        dtUsers.Columns.Add("userName")

        Dim dtInactiveUsers As New DataTable
        dtInactiveUsers.Columns.Add("userID")
        dtInactiveUsers.Columns.Add("userName")

        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        cmd.CommandText = "spGetUsers"
        cmd.CommandType = CommandType.StoredProcedure

        Dim dt As DataTable = db.FillWithSP(cmd)
        For Each dtr As DataRow In dt.Rows

            If dtr("active").ToString().Trim().ToLower() = "y" Then
                dr = dtUsers.NewRow()

                dr("userID") = dtr("id")
                dr("userName") = dtr("firstName") & " " & dtr("lastName")

                dtUsers.Rows.Add(dr)
            Else
                dr = dtInactiveUsers.NewRow()

                dr("userID") = dtr("id")
                dr("userName") = dtr("firstName") & " " & dtr("lastName")

                dtInactiveUsers.Rows.Add(dr)
            End If

        Next

        rptUsers.DataSource = dtUsers
        rptUsers.DataBind()

        rptInactiveUsers.DataSource = dtInactiveUsers
        rptInactiveUsers.DataBind()


        'Dim db As New Connection("Consumer_DSN")
        'Dim cmd As New SqlCommand
        'cmd.CommandText = "spGetUsers"
        'cmd.CommandType = CommandType.StoredProcedure
        'Dim da As SqlDataReader = db.ExecuteReader(cmd)
        'Dim contacts As New DataTable
        'contacts.Load(da)

        'If (contacts.Rows.Count > 0) Then
        '    grdContact.DataSource = contacts
        '    grdContact.DataBind()
        'End If
    End Sub
    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        RedirectToPage("start.aspx")
    End Sub

    Private Sub rptUsers_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptUsers.ItemDataBound
        Dim dr As DataRowView

        Dim linkButton As LinkButton = CType(e.Item.FindControl("lnkEdit"), LinkButton)
        If (Not (linkButton) Is Nothing) Then
            dr = CType(e.Item.DataItem, DataRowView)
            linkButton.Text = dr.Item("userName").ToString()
        End If

        Dim textBox As TextBox = CType(e.Item.FindControl("txtID"), TextBox)
        If (Not (textBox) Is Nothing) Then
            dr = CType(e.Item.DataItem, DataRowView)
            textBox.Text = dr.Item("userID").ToString()
        End If

    End Sub

    Private Sub rptUsers_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptUsers.ItemCommand

        Dim txtBox As TextBox

        txtBox = CType(e.Item.FindControl("txtID"), TextBox)
        If (Not (txtBox) Is Nothing) Then
            RedirectToPage("EditUserInfo.aspx?id=" & txtBox.Text)
        End If

    End Sub

    Private Sub rptInactiveUsers_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptInactiveUsers.ItemDataBound
        Dim dr As DataRowView

        Dim linkButton As LinkButton = CType(e.Item.FindControl("lnkEdit"), LinkButton)
        If (Not (linkButton) Is Nothing) Then
            dr = CType(e.Item.DataItem, DataRowView)
            linkButton.Text = dr.Item("userName").ToString()
        End If

        Dim textBox As TextBox = CType(e.Item.FindControl("txtID"), TextBox)
        If (Not (textBox) Is Nothing) Then
            dr = CType(e.Item.DataItem, DataRowView)
            textBox.Text = dr.Item("userID").ToString()
        End If
    End Sub

    Private Sub rptInactiveUsers_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptInactiveUsers.ItemCommand
        Dim txtBox As TextBox

        txtBox = CType(e.Item.FindControl("txtID"), TextBox)
        If (Not (txtBox) Is Nothing) Then
            RedirectToPage("EditUserInfo.aspx?id=" & txtBox.Text)
        End If
    End Sub
End Class