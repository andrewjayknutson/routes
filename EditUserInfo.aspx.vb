Imports System.Data
Imports System.Data.SqlClient
Imports Routes.Database

Public Class EditUserInfo
    Inherits PageBase

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not CheckUser(Session("userName"), Session("password")).ToLower = MATCH Then
            RedirectToPage(HOME_PAGE)
        End If

        If Not Page.IsPostBack Then
            InitializeForm()

            LoadUser()

        End If

    End Sub

    Private Sub btnSaveUser_Click(sender As Object, e As EventArgs) Handles btnSaveUser.Click
        saveUser()

        RedirectToPage("EditUsersInfo.aspx")
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        RedirectToPage("EditUsersInfo.aspx")
    End Sub

#End Region

#Region "Subs / Functions"

    Private Sub LoadUser()
        Try
            Dim db As New Connection("Consumer_DSN")
            Dim cmd As New SqlCommand

            cmd.CommandText = "spGetUserByIDWithTitle"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@id", Request.QueryString("id"))

            Dim rs As SqlDataReader = db.ExecuteReader(cmd)
            If rs.HasRows And rs.Read Then
                lblID.Text = rs("id")
                txtEmployeeID.Text = IIf(rs("address") Is System.DBNull.Value, "", rs("address"))
                txtFirst.Text = IIf(rs("firstName") Is System.DBNull.Value, "", rs("firstName"))
                txtLast.Text = IIf(rs("lastName") Is System.DBNull.Value, "", rs("lastName"))
                ddlActiveSelection.Text = IIf(rs("active") Is System.DBNull.Value, "N", rs("active"))
                ddlBonusSelection.Text = IIf(rs("pseligible") Is System.DBNull.Value, "N", rs("pseligible"))
                ddlPermissionSelection.Text = IIf(rs("permissionLevel") Is System.DBNull.Value, "", rs("permissionLevel").ToString.Trim)
                ddlTitleSelection.Text = IIf(rs("userTitleID") Is System.DBNull.Value, "", rs("userTitleID").ToString.Trim)
            End If


            cmd = New SqlCommand
            cmd.CommandText = "spGetUserWageType"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@id", Request.QueryString("id"))

            Dim dt As DataTable = db.FillWithSP(cmd)
            For Each dtr As DataRow In dt.Rows
                Select Case dtr("wageTitle").ToString().Trim().ToLower()
                    Case "truck"
                        ddlTruckWageSelection.Text = dtr("wage").ToString().Trim()
                    Case "clerical"
                        ddlClericalSelection.Text = dtr("wage").ToString().Trim()
                    Case "support"
                        ddlSupportSelection.Text = dtr("wage").ToString().Trim()
                    Case "meeting"
                        ddlMeetingSelection.Text = dtr("wage").ToString().Trim()
                    Case "training"
                        ddlTrainingSelection.Text = dtr("wage").ToString().Trim()
                    Case "point"
                        ddlPointSelection.Text = dtr("wage").ToString().Trim()
                    Case "fleet"
                        ddlFleetSelection.Text = dtr("wage").ToString().Trim()
                    Case "warehouse"
                        ddlWarehouseSelection.Text = dtr("wage").ToString().Trim()
                End Select
            Next



        Catch ex As Exception

        End Try
    End Sub

    Private Sub saveUser()
        Try
            Dim db As New Connection("Consumer_DSN")
            Dim cmd As New SqlCommand
            cmd.CommandText = "spUpdateUserByID"
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("@id", SqlDbType.NVarChar, 50)
            cmd.Parameters("@id").Value = lblID.Text

            cmd.Parameters.Add("@username", SqlDbType.NVarChar, 255)
            cmd.Parameters("@username").Value = ""

            cmd.Parameters.Add("@firstName", SqlDbType.NVarChar, 255)
            cmd.Parameters("@firstName").Value = txtFirst.Text

            cmd.Parameters.Add("@lastName", SqlDbType.NVarChar, 255)
            cmd.Parameters("@lastName").Value = txtLast.Text

            cmd.Parameters.Add("@address", SqlDbType.NVarChar, 255)
            cmd.Parameters("@address").Value = txtEmployeeID.Text

            cmd.Parameters.Add("@city", SqlDbType.NVarChar, 255)
            cmd.Parameters("@city").Value = ""

            cmd.Parameters.Add("@state", SqlDbType.NVarChar, 255)
            cmd.Parameters("@state").Value = ""

            cmd.Parameters.Add("@zip", SqlDbType.NVarChar, 10)
            cmd.Parameters("@zip").Value = ""

            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 255)
            cmd.Parameters("@email").Value = ""

            cmd.Parameters.Add("@homePhone", SqlDbType.NVarChar, 255)
            cmd.Parameters("@homePhone").Value = ""

            cmd.Parameters.Add("@mobilePhone", SqlDbType.NVarChar, 255)
            cmd.Parameters("@mobilePhone").Value = ""

            cmd.Parameters.Add("@basePayRate", SqlDbType.NVarChar, 50)
            cmd.Parameters("@basePayRate").Value = ""

            cmd.Parameters.Add("@password", SqlDbType.NVarChar, 255)
            cmd.Parameters("@password").Value = ""

            cmd.Parameters.Add("@active", SqlDbType.NVarChar, 255)
            cmd.Parameters("@active").Value = ddlActiveSelection.Text

            cmd.Parameters.Add("@permissionLevel", SqlDbType.NVarChar, 50)
            cmd.Parameters("@permissionLevel").Value = ddlPermissionSelection.Text

            cmd.Parameters.Add("@pseligible", SqlDbType.NVarChar, 1)
            cmd.Parameters("@pseligible").Value = ddlBonusSelection.Text

            cmd.Parameters.Add("@currentNPS", SqlDbType.Int, 1)
            cmd.Parameters("@currentNPS").Value = 0

            cmd.Parameters.Add("@ytdNPS", SqlDbType.Int, 1)
            cmd.Parameters("@ytdNPS").Value = 0

            cmd.Parameters.Add("@payrollSummary", SqlDbType.NVarChar, 1)
            cmd.Parameters("@payrollSummary").Value = "Y"

            Dim ret As Int32 = db.ExecuteNonQuerySP(cmd)

            UpdateWages()

            UpdatePermissions()

            UpdateTitle()

        Catch ex As Exception

        End Try

    End Sub


    Private Sub UpdateWages()

        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        cmd.CommandText = "spDeleteUserWageTypes"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@id", lblID.Text)
        db.ExecuteNonQuerySP(cmd)

        'insert wage types...
        cmd = New SqlCommand
        cmd.CommandText = "spInsertUserWageType"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@id", Guid.NewGuid().ToString())
        cmd.Parameters.AddWithValue("@userInfoID", lblID.Text)
        cmd.Parameters.AddWithValue("@wageTitle", "Truck")
        cmd.Parameters.AddWithValue("@wage", ddlTruckWageSelection.Text)
        db.ExecuteNonQuerySP(cmd)

        cmd = New SqlCommand
        cmd.CommandText = "spInsertUserWageType"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@id", Guid.NewGuid().ToString())
        cmd.Parameters.AddWithValue("@userInfoID", lblID.Text)
        cmd.Parameters.AddWithValue("@wageTitle", "Clerical")
        cmd.Parameters.AddWithValue("@wage", ddlClericalSelection.Text)
        db.ExecuteNonQuerySP(cmd)

        cmd = New SqlCommand
        cmd.CommandText = "spInsertUserWageType"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@id", Guid.NewGuid().ToString())
        cmd.Parameters.AddWithValue("@userInfoID", lblID.Text)
        cmd.Parameters.AddWithValue("@wageTitle", "Support")
        cmd.Parameters.AddWithValue("@wage", ddlSupportSelection.Text)
        db.ExecuteNonQuerySP(cmd)

        cmd = New SqlCommand
        cmd.CommandText = "spInsertUserWageType"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@id", Guid.NewGuid().ToString())
        cmd.Parameters.AddWithValue("@userInfoID", lblID.Text)
        cmd.Parameters.AddWithValue("@wageTitle", "Meeting")
        cmd.Parameters.AddWithValue("@wage", ddlMeetingSelection.Text)
        db.ExecuteNonQuerySP(cmd)

        cmd = New SqlCommand
        cmd.CommandText = "spInsertUserWageType"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@id", Guid.NewGuid().ToString())
        cmd.Parameters.AddWithValue("@userInfoID", lblID.Text)
        cmd.Parameters.AddWithValue("@wageTitle", "Training")
        cmd.Parameters.AddWithValue("@wage", ddlTrainingSelection.Text)
        db.ExecuteNonQuerySP(cmd)

        cmd = New SqlCommand
        cmd.CommandText = "spInsertUserWageType"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@id", Guid.NewGuid().ToString())
        cmd.Parameters.AddWithValue("@userInfoID", lblID.Text)
        cmd.Parameters.AddWithValue("@wageTitle", "Point")
        cmd.Parameters.AddWithValue("@wage", ddlPointSelection.Text)
        db.ExecuteNonQuerySP(cmd)

        cmd = New SqlCommand
        cmd.CommandText = "spInsertUserWageType"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@id", Guid.NewGuid().ToString())
        cmd.Parameters.AddWithValue("@userInfoID", lblID.Text)
        cmd.Parameters.AddWithValue("@wageTitle", "Fleet")
        cmd.Parameters.AddWithValue("@wage", ddlFleetSelection.Text)
        db.ExecuteNonQuerySP(cmd)

        cmd = New SqlCommand
        cmd.CommandText = "spInsertUserWageType"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@id", Guid.NewGuid().ToString())
        cmd.Parameters.AddWithValue("@userInfoID", lblID.Text)
        cmd.Parameters.AddWithValue("@wageTitle", "Warehouse")
        cmd.Parameters.AddWithValue("@wage", ddlWarehouseSelection.Text)
        db.ExecuteNonQuerySP(cmd)

    End Sub


    Private Sub UpdatePermissions()
        Try
            Dim db As New Connection("Consumer_DSN")
            Dim cmd As New SqlCommand

            cmd = New SqlCommand
            cmd.CommandText = "spDeletePermission"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@userid", lblID.Text)
            db.ExecuteNonQuerySP(cmd)

            cmd = New SqlCommand
            cmd.CommandText = "spInsertUserPermissions"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@userid", lblID.Text)
            cmd.Parameters.AddWithValue("@permissionsid", ddlPermissionSelection.SelectedValue)
            db.ExecuteNonQuerySP(cmd)



            'cmd.CommandText = "spGetPermissionsByDisplayName"
            'cmd.CommandType = CommandType.StoredProcedure
            'cmd.Parameters.AddWithValue("@displayName", ddlPermissionSelection.Text)
            'Dim rs As SqlDataReader = db.ExecuteReader(cmd)
            'If rs.HasRows And rs.Read Then

            '    'delete persmissions...
            '    cmd = New SqlCommand
            '    cmd.CommandText = "spDeletePermission"
            '    cmd.CommandType = CommandType.StoredProcedure
            '    cmd.Parameters.AddWithValue("@userid", lblID.Text)
            '    db.ExecuteNonQuerySP(cmd)

            '    'insert permissions...
            '    cmd = New SqlCommand
            '    cmd.CommandText = "spInsertUserPermissions"
            '    cmd.CommandType = CommandType.StoredProcedure
            '    cmd.Parameters.AddWithValue("@userid", lblID.Text)
            '    cmd.Parameters.AddWithValue("@permissionsid", rs("permissionsID").ToString())
            '    db.ExecuteNonQuerySP(cmd)

            'End If

        Catch ex As Exception

        End Try

    End Sub


    Private Sub UpdateTitle()
        Try
            Dim db As New Connection("Consumer_DSN")

            'delete persmissions...
            Dim cmd As SqlCommand = New SqlCommand
            cmd.CommandText = "spDeleteUserTitle"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@userid", lblID.Text)
            db.ExecuteNonQuerySP(cmd)

            'insert permissions...
            cmd = New SqlCommand
            cmd.CommandText = "spInsertUserTitle"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@userid", lblID.Text)
            cmd.Parameters.AddWithValue("@userTitleID", ddlTitleSelection.SelectedValue)
            db.ExecuteNonQuerySP(cmd)

        Catch ex As Exception

        End Try

    End Sub


    Private Sub InitializeForm()

        ddlActiveSelection.Items.Add("Y")
        ddlActiveSelection.Items.Add("N")

        ddlBonusSelection.Items.Add("Y")
        ddlBonusSelection.Items.Add("N")

        For x As Decimal = 11 To 26 Step 0.25
            ddlTruckWageSelection.Items.Add(ReturnDecimalValue(x.ToString()))
            ddlClericalSelection.Items.Add(ReturnDecimalValue(x.ToString()))
            ddlTrainingSelection.Items.Add(ReturnDecimalValue(x.ToString()))
            ddlMeetingSelection.Items.Add(ReturnDecimalValue(x.ToString()))
            ddlSupportSelection.Items.Add(ReturnDecimalValue(x.ToString()))
            ddlPointSelection.Items.Add(ReturnDecimalValue(x.ToString()))
            ddlFleetSelection.Items.Add(ReturnDecimalValue(x.ToString()))
            ddlWarehouseSelection.Items.Add(ReturnDecimalValue(x.ToString()))
        Next

        Dim db As New Connection("Consumer_DSN")
        Dim cmd As SqlCommand = New SqlCommand
        cmd.CommandText = "spGetUserTitle"
        cmd.CommandType = CommandType.StoredProcedure

        Dim li As ListItem
        Dim xy As DataTable = db.FillWithSP(cmd)
        For Each drow As DataRow In xy.Rows
            li = New ListItem
            li.Text = drow("displayName").ToString()
            li.Value = drow("userTitleID").ToString()
            ddlTitleSelection.Items.Add(li)
        Next

        cmd = New SqlCommand
        cmd.CommandText = "spGetPermissions"
        cmd.CommandType = CommandType.StoredProcedure

        xy = New DataTable()
        xy = db.FillWithSP(cmd)
        For Each drow As DataRow In xy.Rows
            li = New ListItem
            li.Text = drow("displayName").ToString()
            li.Value = drow("permissionsID").ToString()
            ddlPermissionSelection.Items.Add(li)
        Next

        'ddlPermissionSelection.Items.Add("Franchise Partner")
        'ddlPermissionSelection.Items.Add("General Manager")
        'ddlPermissionSelection.Items.Add("Operations Manager")
        'ddlPermissionSelection.Items.Add("Truck Team")
        'ddlPermissionSelection.Items.Add("Admin")


    End Sub

#End Region

End Class