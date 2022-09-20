Imports System.Data
Imports System.Data.SqlClient
Imports Routes.Database

Partial Public Class AddUser
    Inherits PageBase

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not CheckUser(Session("userName"), Session("password")).ToLower = MATCH Then
            RedirectToPage(HOME_PAGE)
        End If

        If Not Page.IsPostBack Then
            InitializeForm()
        End If

    End Sub

    Private Sub btnSaveUser_Click(sender As Object, e As EventArgs) Handles btnSaveUser.Click
        lblStatus.Text = saveUser()

        txtUsername.Text = String.Empty
        txtFirstName.Text = String.Empty
        txtLastName.Text = String.Empty
        txtAddress.Text = String.Empty
        txtCity.Text = String.Empty
        ddlState.SelectedIndex = 0
        txtZip.Text = String.Empty
        txtEmail.Text = String.Empty
        txtPhone1.Text = String.Empty
        txtPhone2.Text = String.Empty
        txtPassword.Text = String.Empty
        txtBasePayRate.Text = String.Empty
        ddlActive.SelectedIndex = 0
        ddlPermissions.SelectedIndex = 0
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        RedirectToPage("Landing.aspx")
    End Sub

#End Region

#Region "Subs / Functions"

    Private Function saveUser() As String

        Try
            Dim db As New Connection("Consumer_DSN")
            Dim cmd As New SqlCommand

            cmd.CommandText = "spGetUserByUsername"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@username", txtUsername.Text)
            Dim rs As SqlDataReader = db.ExecuteReader(cmd)
            If rs.HasRows And rs.Read Then
                Return "Username " & txtUsername.Text & " already exists.  New user not saved"
            Else
                cmd.Parameters.Clear()

                cmd.CommandText = "spInsertUser"
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier)
                cmd.Parameters("@id").Value = Guid.NewGuid

                cmd.Parameters.Add("@username", SqlDbType.NVarChar, 255)
                cmd.Parameters("@username").Value = txtUsername.Text

                cmd.Parameters.Add("@firstName", SqlDbType.NVarChar, 255)
                cmd.Parameters("@firstName").Value = txtFirstName.Text

                cmd.Parameters.Add("@lastName", SqlDbType.NVarChar, 255)
                cmd.Parameters("@lastName").Value = txtLastName.Text

                cmd.Parameters.Add("@address", SqlDbType.NVarChar, 255)
                cmd.Parameters("@address").Value = txtAddress.Text

                cmd.Parameters.Add("@city", SqlDbType.NVarChar, 255)
                cmd.Parameters("@city").Value = txtCity.Text

                cmd.Parameters.Add("@state", SqlDbType.NVarChar, 2)
                cmd.Parameters("@state").Value = ddlState.Text

                cmd.Parameters.Add("@zip", SqlDbType.NVarChar, 10)
                cmd.Parameters("@zip").Value = txtZip.Text

                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 255)
                cmd.Parameters("@email").Value = txtEmail.Text

                cmd.Parameters.Add("@homePhone", SqlDbType.NVarChar, 255)
                cmd.Parameters("@homePhone").Value = txtPhone1.Text

                cmd.Parameters.Add("@mobilePhone", SqlDbType.NVarChar, 255)
                cmd.Parameters("@mobilePhone").Value = txtPhone2.Text

                cmd.Parameters.Add("@basePayRate", SqlDbType.NVarChar, 50)
                cmd.Parameters("@basePayRate").Value = txtBasePayRate.Text

                cmd.Parameters.Add("@password", SqlDbType.NVarChar, 255)
                cmd.Parameters("@password").Value = txtPassword.Text

                cmd.Parameters.Add("@active", SqlDbType.NVarChar, 1)
                cmd.Parameters("@active").Value = ddlActive.Text

                cmd.Parameters.Add("@permissionLevel", SqlDbType.NVarChar, 50)
                cmd.Parameters("@permissionLevel").Value = ddlPermissions.Text

                cmd.Parameters.Add("@pseligible", SqlDbType.NVarChar, 1)
                cmd.Parameters("@pseligible").Value = ddlPSEligible.Text

                Dim ret As Int32 = db.ExecuteNonQuerySP(cmd)

                Dim sReturn As String = UpdatePermissions()
                If String.IsNullOrEmpty(sReturn) Then
                    Return txtUsername.Text & " added and permissions set successfully"
                Else
                    Return txtUsername.Text & " added but error: " & sReturn
                End If

            End If

        Catch ex As Exception
            Return ex.Message

        End Try

    End Function


    Private Sub InitializeForm()

        ddlState.Items.Add("MN")
        ddlState.Items.Add("WI")
        ddlState.Items.Add("IA")
        ddlState.Items.Add("ND")
        ddlState.Items.Add("SD")

        ddlActive.Items.Add("Y")
        ddlActive.Items.Add("N")

        ddlPSEligible.Items.Add("Y")
        ddlPSEligible.Items.Add("N")

        ddlPermissions.Items.Add("Franchise Partner")
        ddlPermissions.Items.Add("General Manager")
        ddlPermissions.Items.Add("Operations Manager")
        ddlPermissions.Items.Add("Truck Team")
        ddlPermissions.Items.Add("Admin")

    End Sub

    Private Function UpdatePermissions() As String
        Try
            Dim userID As String
            Dim permissionsID As String

            Dim db As New Connection("Consumer_DSN")
            Dim cmd As New SqlCommand
            cmd.CommandText = "spGetPermissionsByDisplayName"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@displayName", ddlPermissions.Text)
            Dim rs As SqlDataReader = db.ExecuteReader(cmd)
            If rs.HasRows And rs.Read Then
                permissionsID = rs("permissionsID")

                cmd = New SqlCommand
                cmd.CommandText = "spGetUserByUsername"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@username", txtUsername.Text)
                rs = db.ExecuteReader(cmd)
                If rs.HasRows And rs.Read Then
                    userID = rs("id")


                    'delete persmissions...
                    cmd = New SqlCommand
                    cmd.CommandText = "spDeletePermission"
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@userid", userID)
                    db.ExecuteNonQuerySP(cmd)

                    'insert permissions...
                    cmd = New SqlCommand
                    cmd.CommandText = "spInsertUserPermissions"
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@userid", userID)
                    cmd.Parameters.AddWithValue("@permissionsid", permissionsID)
                    db.ExecuteNonQuerySP(cmd)

                    Return String.Empty

                Else
                    Return txtUsername.Text & " not found...."
                End If

            Else
                Return ddlPermissions.Text & " permissions not found...."

            End If

        Catch ex As Exception
            Return ex.Message

        End Try

    End Function

#End Region

End Class