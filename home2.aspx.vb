Public Class home2
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub lnkGo_Click(sender As Object, e As EventArgs) Handles lnkGo.Click
        Dim validUser As String
        validUser = CheckUser(txtUser.Text, txtPwd.Text)

        If validUser.ToLower = MATCH Then
            Session("userName") = txtUser.Text
            Session("password") = txtPwd.Text
            If txtUser.Text.ToLower = "truckteam" Then
                RedirectToPage("welcome.aspx")
            Else
                'RedirectToPage("Landing.aspx")
                RedirectToPage("start.aspx")
            End If
        Else
            lblMessage.Text = validUser
        End If

    End Sub
End Class