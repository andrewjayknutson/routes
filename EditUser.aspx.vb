Imports System.Data
Imports System.Data.SqlClient
Imports Routes.Database

Partial Public Class EditUser
    Inherits PageBase

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not CheckUser(Session("userName"), Session("password")).ToLower = MATCH Then
            RedirectToPage(HOME_PAGE)
        End If

        If Not Page.IsPostBack Then
            InitializeForm()
            If Not String.IsNullOrEmpty(GetUser()) Then
                RedirectToPage("EditUsers.aspx")
            End If
        End If

    End Sub

    Private Sub btnSaveUser_Click(sender As Object, e As EventArgs) Handles btnSaveUser.Click
        lblStatus.Text = saveUser()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        RedirectToPage("EditUsers.aspx")
    End Sub

#End Region

#Region "Subs / Functions"

    Private Function GetUser() As String
        Try
            Dim db As New Connection("Consumer_DSN")
            Dim cmd As New SqlCommand
            cmd.CommandText = "spGetUserByUsername"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@username", Request.QueryString("user"))
            Dim rs As SqlDataReader = db.ExecuteReader(cmd)
            If rs.HasRows And rs.Read Then
                lblID.Text = rs("id")
                txtUsername.Text = IIf(rs("username") Is System.DBNull.Value, "", rs("username"))
                txtFirstName.Text = IIf(rs("firstName") Is System.DBNull.Value, "", rs("firstName"))
                txtLastName.Text = IIf(rs("lastName") Is System.DBNull.Value, "", rs("lastName"))
                txtAddress.Text = IIf(rs("address") Is System.DBNull.Value, "", rs("address"))
                txtCity.Text = IIf(rs("city") Is System.DBNull.Value, "", rs("city"))
                ddlState.Text = IIf(rs("state") Is System.DBNull.Value, "N", rs("state"))
                txtZip.Text = IIf(rs("zip") Is System.DBNull.Value, "", rs("zip"))
                txtPassword.Text = IIf(rs("password") Is System.DBNull.Value, "", rs("password"))
                txtEmail.Text = IIf(rs("email") Is System.DBNull.Value, "", rs("email"))
                txtPhone1.Text = IIf(rs("homePhone") Is System.DBNull.Value, "", rs("homePhone"))
                txtPhone2.Text = IIf(rs("mobilePhone") Is System.DBNull.Value, "", rs("mobilePhone"))
                ddlActive.Text = IIf(rs("active") Is System.DBNull.Value, "N", rs("active"))
                txtBasePayRate.Text = IIf(rs("basePayRate") Is System.DBNull.Value, "", rs("basePayRate"))
                ddlPSEligible.Text = IIf(rs("pseligible") Is System.DBNull.Value, "N", rs("pseligible"))
                ddlPermissions.Text = IIf(rs("permissionLevel") Is System.DBNull.Value, "", rs("permissionLevel").ToString.Trim)
                txtNPS.Text = IIf(rs("currentNPS") Is System.DBNull.Value, "", rs("currentNPS"))
                txtYtdNPS.Text = IIf(rs("ytdNPS") Is System.DBNull.Value, "", rs("ytdNPS"))
                ddlPayrollSummary.Text = IIf(rs("payrollSummary") Is System.DBNull.Value, "N", rs("payrollSummary"))
                Return String.Empty
            Else
                Return "User not found...."
            End If
        Catch ex As Exception
            Return ex.Message

        End Try
    End Function

    Private Function saveUser() As String
        Try
            Dim db As New Connection("Consumer_DSN")
            Dim cmd As New SqlCommand
            cmd.CommandText = "spUpdateUserByID"
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("@id", SqlDbType.NVarChar, 50)
            cmd.Parameters("@id").Value = lblID.Text

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

            cmd.Parameters.Add("@state", SqlDbType.NVarChar, 255)
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

            cmd.Parameters.Add("@active", SqlDbType.NVarChar, 255)
            cmd.Parameters("@active").Value = ddlActive.Text

            cmd.Parameters.Add("@permissionLevel", SqlDbType.NVarChar, 50)
            cmd.Parameters("@permissionLevel").Value = ddlPermissions.Text

            cmd.Parameters.Add("@pseligible", SqlDbType.NVarChar, 1)
            cmd.Parameters("@pseligible").Value = ddlPSEligible.Text

            cmd.Parameters.Add("@currentNPS", SqlDbType.Int, 1)
            If String.IsNullOrEmpty(txtNPS.Text) Then
                cmd.Parameters("@currentNPS").Value = 0
            Else
                cmd.Parameters("@currentNPS").Value = txtNPS.Text
            End If

            cmd.Parameters.Add("@ytdNPS", SqlDbType.Int, 1)
            If String.IsNullOrEmpty(txtYtdNPS.Text) Then
                cmd.Parameters("@ytdNPS").Value = 0
            Else
                cmd.Parameters("@ytdNPS").Value = txtYtdNPS.Text
            End If

            cmd.Parameters.Add("@payrollSummary", SqlDbType.NVarChar, 1)
            cmd.Parameters("@payrollSummary").Value = ddlPayrollSummary.Text

            Dim ret As Int32 = db.ExecuteNonQuerySP(cmd)

            Dim sReturn As String = UpdatePermissions()
            If String.IsNullOrEmpty(sReturn) Then
                Return txtUsername.Text & " updated and permissions set successfully"
            Else
                Return txtUsername.Text & " updated but error: " & sReturn
            End If

        Catch ex As Exception
            Return ex.Message

        End Try

    End Function

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

        ddlPayrollSummary.Items.Add("Y")
        ddlPayrollSummary.Items.Add("N")

        ddlPermissions.Items.Add("Franchise Partner")
        ddlPermissions.Items.Add("General Manager")
        ddlPermissions.Items.Add("Operations Manager")
        ddlPermissions.Items.Add("Truck Team")
        ddlPermissions.Items.Add("Admin")

    End Sub

#End Region

End Class