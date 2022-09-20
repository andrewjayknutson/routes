Imports System.Data
Imports System.Data.SqlClient
Imports Routes.Database

Public Class Utilities
    Inherits PageBase


    Public Sub GlobalWageSummary(sRouteID As String)
        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand

        Dim dblTotalDumps As Double = 0
        Dim dblTotalGas As Double = 0
        Dim dblTotalMisc As Double = 0
        Dim dblTotalWages As Double = 0
        Dim dblTotalDoorHangers As Double = 0
        Dim dblTotalSigns As Double = 0
        Dim dblTotalJobs As Double = 0
        Dim dblJobRevenue As Double = 0
        Dim dblMktgRevenue As Double = 0
        Dim dblMktgTime As Double = 0
        Dim dblTotalHours As Double = 0
        Dim dblTotalProfitShare As Double = 0
        Dim dblTotalTips As Double = 0
        Dim dblTotalPSItems As Double = 0
        Dim dblRouteUsers As Double = 0

        'Number of route users
        cmd.CommandText = "spGetRouteUsers"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@routeID", sRouteID)

        Dim dt As DataTable = db.FillWithSP(cmd)
        dblRouteUsers = dt.Rows.Count


        'PS & Tips
        cmd = New SqlCommand
        cmd.CommandText = "spGetWageSummaryByRouteID"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@routeID", sRouteID)

        Dim rs As SqlDataReader = db.ExecuteReader(cmd)
        While rs.Read
            dblTotalProfitShare += rs("profitShare")
            dblTotalTips += rs("tips")
        End While


        'Job Revenue
        cmd = New SqlCommand
        cmd.CommandText = "spGetJobsByRouteID"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@routeID", sRouteID)

        rs = db.ExecuteReader(cmd)
        While rs.Read
            dblJobRevenue += rs("ticketNet")
            dblTotalTips += rs("ticketTips")
            dblTotalJobs += 1
            If rs("sign").ToString.ToLower = "y" Then
                dblTotalSigns += 1
            End If
            If rs("doorHangers").ToString.ToLower = "20" Or rs("doorHangers").ToString.ToLower = "y" Or rs("doorHangers").ToString.ToLower = "n" Then
                dblTotalDoorHangers += 1
            End If
        End While

        'Marketing Revenue
        cmd = New SqlCommand
        cmd.CommandText = "spGetMarketingByRouteID"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@routeID", sRouteID)

        rs = db.ExecuteReader(cmd)
        While rs.Read
            If Not rs("mktgdollars") Is DBNull.Value Then
                dblMktgRevenue += rs("mktgdollars")
            End If
            If Not rs("mktgtime") Is DBNull.Value Then
                If rs("empMultiplier").ToString.ToLower = "y" Then
                    dblMktgTime += rs("mktgtime") * dblRouteUsers
                Else
                    dblMktgTime += rs("mktgtime")
                End If
            End If
        End While

        'Wage Expense
        cmd = New SqlCommand
        cmd.CommandText = "spGetRouteWages"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@routeID", sRouteID)


        Dim dt1 As DateTime
        Dim dt2 As DateTime
        Dim diffInDays As Double

        rs = db.ExecuteReader(cmd)
        While rs.Read
            If rs("workType").ToString.Trim.ToLower = "truck hours" Then
                dt1 = Convert.ToDateTime(rs("endTime"))
                dt2 = Convert.ToDateTime(rs("startTime"))
                diffInDays = (dt1.Subtract(dt2).TotalMinutes / 60)

                dblTotalHours += (dt1.Subtract(dt2).TotalMinutes / 60)

                dblTotalWages += (diffInDays * rs("wage"))
            End If
        End While

        'Misc Expense
        cmd = New SqlCommand
        cmd.CommandText = "spGetExpenseByRouteID"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@routeID", sRouteID)

        rs = db.ExecuteReader(cmd)
        While rs.Read
            dblTotalMisc += rs("ticketTotal")
        End While

        'Gas Expense
        cmd = New SqlCommand
        cmd.CommandText = "spGetFuelByRouteID"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@routeID", sRouteID)

        rs = db.ExecuteReader(cmd)
        While rs.Read
            dblTotalGas += rs("ticketTotal")
        End While

        'Dump Expense
        cmd = New SqlCommand
        cmd.CommandText = "spGetDumpsByRouteID"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@routeID", sRouteID)

        rs = db.ExecuteReader(cmd)
        While rs.Read
            If Not String.IsNullOrEmpty(rs("ticketTotal")) Then
                dblTotalDumps += rs("ticketTotal")
            End If
        End While




        Dim x As ListItem
        Dim chkEmployees As New CheckBoxList
        cmd = New SqlCommand
        cmd.CommandText = "spGetRouteUsers"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@routeID", sRouteID)

        rs = db.ExecuteReader(cmd)
        While rs.Read
            x = New ListItem
            x.Value = rs("id")
            x.Text = rs("firstName") & " " & rs("lastName")
            x.Selected = (rs("confirmed").ToString.ToLower = "y")
            chkEmployees.Items.Add(x)
        End While

        GlobalLoadWageSummary(dblTotalProfitShare, dblTotalTips, chkEmployees, sRouteID)

        'save route information
        cmd = New SqlCommand
        cmd.CommandText = "spUpdateRouteSummary"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@routeID", sRouteID)
        cmd.Parameters.AddWithValue("@jobRevenue", dblJobRevenue)
        cmd.Parameters.AddWithValue("@mktgRevenue", dblMktgRevenue)
        cmd.Parameters.AddWithValue("@wageTotal", dblTotalWages)
        cmd.Parameters.AddWithValue("@dumpTotal", dblTotalDumps)
        cmd.Parameters.AddWithValue("@gasTotal", dblTotalGas)
        cmd.Parameters.AddWithValue("@miscTotal", dblTotalMisc)
        cmd.Parameters.AddWithValue("@psTotal", dblTotalProfitShare)

        db.ExecuteNonQuerySP(cmd)

    End Sub

    Public Sub GlobalLoadWageSummary(ByVal ps As Double, ByVal tips As Double, chkEmployees As CheckBoxList, sRouteID As String)
        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        Dim dr As DataRow

        Dim dt As New DataTable
        dt.Columns.Add("firstName")
        dt.Columns.Add("nonTruckWage")
        dt.Columns.Add("truckWage")
        dt.Columns.Add("profitShare")
        dt.Columns.Add("tips")
        dt.Columns.Add("totalEarnings")
        dt.Columns.Add("nonTruckHrs")
        dt.Columns.Add("truckHrs")
        dt.Columns.Add("totalHours")
        dt.Columns.Add("avgHr")

        Dim bPSEligible As Boolean = False
        Dim dblAvg As Double = 0
        Dim dblTruckHours As Double = 0
        Dim dblNonTruckHours As Double = 0
        Dim dblTruckWages As Double = 0
        Dim dblNonTruckWages As Double = 0


        Dim dt1 As DateTime
        Dim dt2 As DateTime
        Dim diffInDays As Double

        For Each x As ListItem In chkEmployees.Items
            dblTruckHours = 0
            dblTruckWages = 0
            dblNonTruckHours = 0
            dblNonTruckWages = 0

            'get truck hours
            cmd = New SqlCommand
            cmd.CommandText = "spGetRouteUserWagesGrouped"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@routeID", sRouteID)
            cmd.Parameters.AddWithValue("@userID", x.Value)
            cmd.Parameters.AddWithValue("@workType", "Truck Hours")

            Dim xy As DataTable = db.FillWithSP(cmd)
            For Each drow As DataRow In xy.Rows
                dt1 = Convert.ToDateTime(drow("endTime"))
                dt2 = Convert.ToDateTime(drow("startTime"))
                diffInDays = (dt1.Subtract(dt2).TotalMinutes / 60)
                dblTruckHours += (dt1.Subtract(dt2).TotalMinutes / 60)
                dblTruckWages += (diffInDays * drow("wage"))

                bPSEligible = (drow("psEligible").ToString.Trim.ToLower = "y")
            Next

            'get non truck hours
            cmd = New SqlCommand
            cmd.CommandText = "spGetRouteUserWagesGrouped"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@routeID", sRouteID)
            cmd.Parameters.AddWithValue("@userID", x.Value)
            cmd.Parameters.AddWithValue("@workType", "Non-Truck Hours")

            xy = db.FillWithSP(cmd)
            For Each drow As DataRow In xy.Rows
                dt1 = Convert.ToDateTime(drow("endTime"))
                dt2 = Convert.ToDateTime(drow("startTime"))
                diffInDays = (dt1.Subtract(dt2).TotalMinutes / 60)
                dblNonTruckHours += (dt1.Subtract(dt2).TotalMinutes / 60)
                dblNonTruckWages += (diffInDays * drow("wage"))

                bPSEligible = (drow("psEligible").ToString.Trim.ToLower = "y")
            Next

            'get non truck hours
            cmd = New SqlCommand
            cmd.CommandText = "spGetRouteUserWagesGrouped"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@routeID", sRouteID)
            cmd.Parameters.AddWithValue("@userID", x.Value)
            cmd.Parameters.AddWithValue("@workType", "Office")

            xy = db.FillWithSP(cmd)
            For Each drow As DataRow In xy.Rows
                dt1 = Convert.ToDateTime(drow("endTime"))
                dt2 = Convert.ToDateTime(drow("startTime"))
                diffInDays = (dt1.Subtract(dt2).TotalMinutes / 60)
                dblNonTruckHours += (dt1.Subtract(dt2).TotalMinutes / 60)
                dblNonTruckWages += (diffInDays * drow("wage"))

                bPSEligible = (drow("psEligible").ToString.Trim.ToLower = "y")
            Next

            'get non truck hours
            cmd = New SqlCommand
            cmd.CommandText = "spGetRouteUserWagesGrouped"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@routeID", sRouteID)
            cmd.Parameters.AddWithValue("@userID", x.Value)
            cmd.Parameters.AddWithValue("@workType", "Training")

            xy = db.FillWithSP(cmd)
            For Each drow As DataRow In xy.Rows
                dt1 = Convert.ToDateTime(drow("endTime"))
                dt2 = Convert.ToDateTime(drow("startTime"))
                diffInDays = (dt1.Subtract(dt2).TotalMinutes / 60)
                dblNonTruckHours += (dt1.Subtract(dt2).TotalMinutes / 60)
                dblNonTruckWages += (diffInDays * drow("wage"))

                bPSEligible = (drow("psEligible").ToString.Trim.ToLower = "y")
            Next


            dr = dt.NewRow
            dr("firstName") = x.Text
            dr("nonTruckWage") = ReturnCurrencyValue(dblNonTruckWages)
            dr("truckWage") = ReturnCurrencyValue(dblTruckWages)
            dr("tips") = ReturnCurrencyValue(tips / chkEmployees.Items.Count)
            If bPSEligible Then
                dr("profitShare") = ReturnCurrencyValue(ps / chkEmployees.Items.Count)
                dr("avgHr") = ReturnCurrencyValue((dblNonTruckWages + dblTruckWages + (ps / chkEmployees.Items.Count) + (tips / chkEmployees.Items.Count)) / (dblTruckHours + dblNonTruckHours))
                dblAvg = (dblNonTruckWages + dblTruckWages + (ps / chkEmployees.Items.Count) + (tips / chkEmployees.Items.Count)) / (dblTruckHours + dblNonTruckHours)
            Else
                dr("profitShare") = ReturnCurrencyValue(0)
                dr("avgHr") = ReturnCurrencyValue((dblNonTruckWages + dblTruckWages + (tips / chkEmployees.Items.Count)) / (dblTruckHours + dblNonTruckHours))
                dblAvg = (dblNonTruckWages + dblTruckWages + (tips / chkEmployees.Items.Count)) / (dblTruckHours + dblNonTruckHours)
            End If
            dr("totalEarnings") = ReturnCurrencyValue(dblNonTruckWages + dblTruckWages + (ps / chkEmployees.Items.Count) + (tips / chkEmployees.Items.Count))
            dr("nonTruckHrs") = FormatNumber(dblNonTruckHours, 2)
            dr("truckHrs") = FormatNumber(dblTruckHours, 2)
            dr("totalHours") = FormatNumber(dblTruckHours + dblNonTruckHours, 2)
            dt.Rows.Add(dr)

            If Not dblAvg >= 0 Or Double.IsInfinity(dblAvg) Then
                dblAvg = 0
            End If

            'delete wage summary information for this employee
            db.ExecuteCommand("DELETE FROM wageSummary WHERE routeID = '" & sRouteID & "' and userID = '" & x.Value & "'")

            'insert wage summary
            cmd = New SqlCommand
            cmd.CommandText = "spInsertWageSummary"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@id", Guid.NewGuid.ToString())
            cmd.Parameters.AddWithValue("@routeID", sRouteID)
            cmd.Parameters.AddWithValue("@userID", x.Value)
            cmd.Parameters.AddWithValue("@nonTruckWages", dblNonTruckWages)
            cmd.Parameters.AddWithValue("@truckWages", dblTruckWages)
            If bPSEligible Then
                cmd.Parameters.AddWithValue("@profitShare", ps / chkEmployees.Items.Count)
            Else
                cmd.Parameters.AddWithValue("@profitShare", 0)
            End If
            cmd.Parameters.AddWithValue("@tips", tips / chkEmployees.Items.Count)
            cmd.Parameters.AddWithValue("@totalWages", dblNonTruckWages + dblTruckWages + (ps / chkEmployees.Items.Count))
            cmd.Parameters.AddWithValue("@nonTruckHours", dblNonTruckHours)
            cmd.Parameters.AddWithValue("@truckHours", dblTruckHours)
            cmd.Parameters.AddWithValue("@totalHours", dblTruckHours + dblNonTruckHours)
            cmd.Parameters.AddWithValue("@avgHour", dblAvg)

            db.ExecuteNonQuerySP(cmd)


        Next

    End Sub

    Public Function IsEmployeePSEligible(ByVal empID As String) As Boolean
        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        cmd.CommandText = "spGetUserByID"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@id", empID)

        Dim rs As SqlDataReader = db.ExecuteReader(cmd)
        While rs.Read
            Return (rs("pseligible").ToString.ToLower = "y")
        End While

    End Function

    Public Function RoundDown(dFirst As Decimal) As Decimal

        Dim dwhole As Decimal
        dwhole = Math.Floor(dFirst)

        Dim dfraction As Decimal
        dfraction = dFirst - dwhole

        Select Case dfraction
            Case 0 To 0.24
                Return dwhole
            Case 0.25 To 0.49
                Return dwhole + 0.25
            Case 0.5 To 0.74
                Return dwhole + 0.5
            Case 0.75 To 1
                Return dwhole + 0.75
        End Select


    End Function

    Public Function RoundUp(dFirst As Decimal) As Decimal

        Dim dwhole As Decimal
        dwhole = Math.Floor(dFirst)

        Dim dfraction As Decimal
        dfraction = dFirst - dwhole

        Select Case dfraction
            Case Is < 0.5
                Return dwhole
            Case Else
                Return dwhole + 1
        End Select


    End Function

    Public Function ReturnTruckHoursMin() As Double
        '*** GRAB TRUCK HOURS MIN ***
        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT [displayName] FROM [RouteManagement].[dbo].[dropDownListData] where segment = 'truckHourMin'"
        Dim sdrRevenue As SqlDataReader = db.ExecuteReader(cmd)

        If sdrRevenue.HasRows Then
            While sdrRevenue.Read
                Return sdrRevenue("displayName")
            End While
        Else
            Return 55
        End If
    End Function

    Public Function ReturnTruckHoursMin30Day() As Double
        '*** GRAB TRUCK HOURS MIN ***
        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT [displayName] FROM [RouteManagement].[dbo].[dropDownListData] where segment = 'truckHoursMin30Day'"
        Dim sdrRevenue As SqlDataReader = db.ExecuteReader(cmd)

        If sdrRevenue.HasRows Then
            While sdrRevenue.Read
                Return sdrRevenue("displayName")
            End While
        Else
            Return 80
        End If
    End Function

    Public Function ReturnTruckHoursMin90Day() As Double
        '*** GRAB TRUCK HOURS MIN ***
        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT [displayName] FROM [RouteManagement].[dbo].[dropDownListData] where segment = 'truckHoursMin90Day'"
        Dim sdrRevenue As SqlDataReader = db.ExecuteReader(cmd)

        If sdrRevenue.HasRows Then
            While sdrRevenue.Read
                Return sdrRevenue("displayName")
            End While
        Else
            Return 150
        End If
    End Function

    Public Function ReturnTruckHoursMinYTD() As Double
        '*** GRAB TRUCK HOURS MIN ***
        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT [displayName] FROM [RouteManagement].[dbo].[dropDownListData] where segment = 'truckHoursMinYTD'"
        Dim sdrRevenue As SqlDataReader = db.ExecuteReader(cmd)

        If sdrRevenue.HasRows Then
            While sdrRevenue.Read
                Return sdrRevenue("displayName")
            End While
        Else
            Return 275
        End If
    End Function

    Public Sub LoadCurrentData(startDate As String, endDate As String)
        Dim dHoursMTD As Decimal = 0
        Dim dHoursYTD As Decimal = 0
        Dim dRPHMTD As Decimal = 0
        Dim dRPHYTD As Decimal = 0
        Dim dJobRevenueMTD As Decimal = 0
        Dim dJustJobRevenueMTD As Decimal = 0
        Dim dJobRevenueYTD As Decimal = 0
        Dim dJustJobRevenueYTD As Decimal = 0

        Dim db As New Connection("Consumer_DSN")

        'run this code to update userInfo.currentTruckHours, .currentRPH, .currentSign, .currentAJS and .currentNPS
        db.ExecuteScaler("update userInfo set currentTruckHours = 0, ytdTruckHours = 0, " &
                         "currentRPH = 0, ytdRPH = 0, " &
                         "currentSign = 0, ytdSign = 0, " &
                         "currentNPS = 0, ytdNPS = 0, " &
                         "currentAJS = 0, ytdAJS = 0")



        'update currentTruckHours, ytdTruckHours, currentRPH and ytdRPH
        Dim cmd As New SqlCommand
        cmd.CommandText = "spPayrollSummaryGetEmployees"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@startDate", startDate)
        cmd.Parameters.AddWithValue("@endDate", endDate)

        Dim dtEmpsWithHours As New DataTable
        dtEmpsWithHours = db.FillWithSP(cmd)
        For Each dtewhr As DataRow In dtEmpsWithHours.Rows
            cmd = New SqlCommand
            cmd.CommandText = "spReport_GetRevenueByEmployee"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@startDate", startDate)
            cmd.Parameters.AddWithValue("@endDate", endDate)
            cmd.Parameters.AddWithValue("@userID", dtewhr("userID"))

            Dim sdrRevenue As SqlDataReader = db.ExecuteReader(cmd)
            If sdrRevenue.HasRows Then

                dHoursMTD = 0
                dHoursYTD = 0
                dRPHMTD = 0
                dRPHYTD = 0
                dJobRevenueMTD = 0
                dJustJobRevenueMTD = 0
                dJobRevenueYTD = 0
                dJustJobRevenueYTD = 0

                While sdrRevenue.Read
                    dJobRevenueMTD = sdrRevenue("jobRevenue")
                    dJustJobRevenueMTD = sdrRevenue("justJobRevenue")
                End While


                cmd = New SqlCommand
                cmd.CommandText = "spPayrollSummaryGetWageSummary"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@startDate", startDate)
                cmd.Parameters.AddWithValue("@endDate", endDate)
                cmd.Parameters.AddWithValue("@userID", dtewhr("userID"))

                Dim sdrWorkHours As SqlDataReader = db.ExecuteReader(cmd)
                If sdrWorkHours.HasRows Then
                    While sdrWorkHours.Read
                        dHoursMTD = sdrWorkHours("truckHours")
                    End While
                End If

                If dHoursMTD > 0 Then
                    dRPHMTD = (dJobRevenueMTD / dHoursMTD) * 0.5
                End If

                If Year(startDate) = Year(endDate) Then
                    '***** YTD
                    cmd = New SqlCommand
                    cmd.CommandText = "spReport_GetRevenueByEmployee"
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@startDate", "1/1/" & Year(startDate))
                    cmd.Parameters.AddWithValue("@endDate", endDate)
                    cmd.Parameters.AddWithValue("@userID", dtewhr("userID"))

                    Dim sdrYTDRevenue As SqlDataReader = db.ExecuteReader(cmd)
                    If sdrYTDRevenue.HasRows Then
                        While sdrYTDRevenue.Read
                            dJobRevenueYTD = sdrYTDRevenue("jobRevenue")
                            dJustJobRevenueYTD = sdrYTDRevenue("justJobRevenue")
                        End While


                        cmd = New SqlCommand
                        cmd.CommandText = "spPayrollSummaryGetWageSummary"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@startDate", "1/1/" & Year(startDate))
                        cmd.Parameters.AddWithValue("@endDate", endDate)
                        cmd.Parameters.AddWithValue("@userID", dtewhr("userID"))

                        Dim sdrYTDWorkHours As SqlDataReader = db.ExecuteReader(cmd)
                        If sdrYTDWorkHours.HasRows Then
                            While sdrYTDWorkHours.Read
                                dHoursYTD = sdrYTDWorkHours("truckHours")
                            End While
                        End If

                        If dHoursYTD > 0 Then
                            dRPHYTD = (dJobRevenueYTD / dHoursYTD) * 0.5
                        End If

                    End If

                End If

                db.ExecuteScaler("update userInfo set currentRPH = " & dRPHMTD & ", " &
                                    "ytdRPH = " & dRPHYTD & ", " &
                                    "currentTruckHours = " & dHoursMTD & ", " &
                                    "ytdTruckHours = " & dHoursYTD & " " &
                                    "where userinfo.id = '" & dtewhr("userID") & "'")

            End If
        Next






        'update currentAJS, ytdAJS, currentSign, ytdSign, currentNPS, ytdNPS
        Dim currentAJS As Decimal
        Dim ytdAJS As Decimal
        Dim currentSign As Decimal
        Dim ytdSign As Decimal
        Dim currentNPS As Decimal
        Dim ytdNPS As Decimal
        Dim currentOSC As Decimal
        Dim ytdOSC As Decimal
        Dim cmdYTD As New SqlCommand

        cmd = New SqlCommand With {
            .CommandText = "spGetUsers",
            .CommandType = CommandType.StoredProcedure
        }

        Dim dtEmps As New DataTable
        dtEmps = db.FillWithSP(cmd)
        For Each dtewhr As DataRow In dtEmps.Rows


            '******************************************************************************* AJS *******************


            'currentAJS
            cmd = New SqlCommand
            cmd.CommandText = "spReport_GetAJS"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@startDate", startDate)
            cmd.Parameters.AddWithValue("@endDate", endDate)
            cmd.Parameters.AddWithValue("@userID", dtewhr.Item("id"))

            Dim dtAJS As New DataTable
            dtAJS = db.FillWithSP(cmd)
            For Each dts As DataRow In dtAJS.Rows
                If Not dts.Item("ajs") Is Nothing AndAlso Not IsDBNull(dts.Item("ajs")) Then
                    currentAJS = Math.Round(dts.Item("ajs"), 2)
                End If
            Next

            'ytdAJS
            cmdYTD = New SqlCommand
            cmdYTD.CommandText = "spReport_GetAJS"
            cmdYTD.CommandType = CommandType.StoredProcedure
            cmdYTD.Parameters.AddWithValue("@startDate", "1/1/" & Year(startDate))
            cmdYTD.Parameters.AddWithValue("@endDate", startDate)
            cmdYTD.Parameters.AddWithValue("@userID", dtewhr.Item("id"))

            dtAJS = New DataTable
            dtAJS = db.FillWithSP(cmd)
            For Each dts As DataRow In dtAJS.Rows
                If Not dts.Item("ajs") Is Nothing AndAlso Not IsDBNull(dts.Item("ajs")) Then
                    ytdAJS = Math.Round(dts.Item("ajs"), 2)
                End If
            Next


            '******************************************************************************* SIGN *******************


            Dim dSignsTotal As Integer = 0
            Dim sSignsYes As Integer = 0
            Dim dSignsYTDTotal As Integer = 0
            Dim sSignsYTDYes As Integer = 0



            'currentSign
            cmd = New SqlCommand
            cmd.CommandText = "spReport_GetSignPercentage"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@startDate", startDate)
            cmd.Parameters.AddWithValue("@endDate", endDate)
            cmd.Parameters.AddWithValue("@userID", dtewhr.Item("id"))

            Dim dtSigns As New DataTable
            dtSigns = db.FillWithSP(cmd)
            For Each dts As DataRow In dtSigns.Rows
                dSignsTotal += 1
                If dts("sign").ToString.ToLower = "y" Then
                    sSignsYes += 1
                End If
            Next

            If dSignsTotal <= 0 Then
                currentSign = 0
            Else
                currentSign = (sSignsYes / dSignsTotal) * 100
            End If




            'ytdSign
            cmdYTD = New SqlCommand
            cmdYTD.CommandText = "spReport_GetSignPercentage"
            cmdYTD.CommandType = CommandType.StoredProcedure
            cmdYTD.Parameters.AddWithValue("@startDate", "1/1/" & Year(startDate))
            cmdYTD.Parameters.AddWithValue("@endDate", endDate)
            cmdYTD.Parameters.AddWithValue("@userID", dtewhr.Item("id"))

            dtSigns = New DataTable
            dtSigns = db.FillWithSP(cmdYTD)
            For Each dts As DataRow In dtSigns.Rows
                dSignsYTDTotal += 1
                If dts("sign").ToString.ToLower = "y" Then
                    sSignsYTDYes += 1
                End If
            Next


            If dSignsYTDTotal <= 0 Then
                ytdSign = 0
            Else
                ytdSign = (sSignsYTDYes / dSignsYTDTotal) * 100
            End If


            '******************************************************************************* NPS *******************


            'currentNPS
            cmdYTD = New SqlCommand
            cmdYTD.CommandText = "spGetNPSByDateEmpID"
            cmdYTD.CommandType = CommandType.StoredProcedure
            cmdYTD.Parameters.AddWithValue("@startDate", startDate)
            cmdYTD.Parameters.AddWithValue("@endDate", endDate)
            cmdYTD.Parameters.AddWithValue("@userInfoId", dtewhr.Item("id"))

            currentNPS = 0
            dtSigns = New DataTable
            dtSigns = db.FillWithSP(cmdYTD)
            For Each dts As DataRow In dtSigns.Rows
                If Not dts("total") Is DBNull.Value Then
                    currentNPS = Math.Round(((dts("promoter").ToString() / dts("total").ToString()) - (dts("detractor").ToString() / dts("total").ToString())) * 100, 0)
                End If
            Next


            'ytdNPS
            cmdYTD = New SqlCommand
            cmdYTD.CommandText = "spGetNPSByDateEmpID"
            cmdYTD.CommandType = CommandType.StoredProcedure
            cmdYTD.Parameters.AddWithValue("@startDate", "1/1/" & Year(startDate))
            cmdYTD.Parameters.AddWithValue("@endDate", endDate)
            cmdYTD.Parameters.AddWithValue("@userInfoId", dtewhr.Item("id"))

            ytdNPS = 0
            dtSigns = New DataTable
            dtSigns = db.FillWithSP(cmdYTD)
            If dtSigns.Rows.Count > 0 Then
                For Each dts As DataRow In dtSigns.Rows
                    If Not dts("total") Is DBNull.Value Then
                        ytdNPS = Math.Round(((dts("promoter").ToString() / dts("total").ToString()) - (dts("detractor").ToString() / dts("total").ToString())) * 100, 0)
                    End If
                Next
            End If




            '******************************************************************************* OSC*******************


            'currentOSC
            Dim totalJobs As Integer = 0
            Dim totalWithRev As Integer = 0
            Dim totalWithoutRev As Integer = 0

            cmdYTD = New SqlCommand
            cmdYTD.CommandText = "spGetJobsByEmp"
            cmdYTD.CommandType = CommandType.StoredProcedure
            cmdYTD.Parameters.AddWithValue("@startDate", startDate)
            cmdYTD.Parameters.AddWithValue("@endDate", endDate)
            cmdYTD.Parameters.AddWithValue("@empID", dtewhr.Item("id"))

            dtSigns = New DataTable
            dtSigns = db.FillWithSP(cmdYTD)
            For Each dts As DataRow In dtSigns.Rows
                totalJobs = totalJobs + 1

                If Not dts("ticketRevenue") Is DBNull.Value Then
                    If dts("ticketRevenue") > 0 Then
                        totalWithRev = totalWithRev + 1
                    Else
                        totalWithoutRev = totalWithoutRev + 1
                    End If
                End If
            Next

            currentOSC = 0
            If totalJobs > 0 AndAlso totalWithRev > 0 Then
                currentOSC = Math.Round((totalWithRev / totalJobs) * 100, 2)
            End If




            'ytdOSC
            totalJobs = 0
            totalWithRev = 0
            totalWithoutRev = 0

            cmdYTD = New SqlCommand
            cmdYTD.CommandText = "spGetJobsByEmp"
            cmdYTD.CommandType = CommandType.StoredProcedure
            cmdYTD.Parameters.AddWithValue("@startDate", "1/1/" & Year(startDate))
            cmdYTD.Parameters.AddWithValue("@endDate", endDate)
            cmdYTD.Parameters.AddWithValue("@empID", dtewhr.Item("id"))

            dtSigns = New DataTable
            dtSigns = db.FillWithSP(cmdYTD)
            For Each dts As DataRow In dtSigns.Rows
                totalJobs = totalJobs + 1

                If Not dts("ticketRevenue") Is DBNull.Value Then
                    If dts("ticketRevenue") > 0 Then
                        totalWithRev = totalWithRev + 1
                    Else
                        totalWithoutRev = totalWithoutRev + 1
                    End If
                End If
            Next

            ytdOSC = 0
            If totalJobs > 0 AndAlso totalWithRev > 0 Then
                ytdOSC = Math.Round((totalWithRev / totalJobs) * 100, 2)
            End If






            db.ExecuteScaler("update userInfo set currentAJS = " & currentAJS & ", " &
                                    "ytdAJS = " & ytdAJS & ", " &
                                    "currentSign = " & currentSign & ", " &
                                    "ytdSign = " & ytdSign & ", " &
                                    "currentOSC = " & currentOSC & ", " &
                                    "ytdOSC = " & ytdOSC & ", " &
                                    "currentNPS = " & currentNPS & ", " &
                                    "ytdNPS = " & ytdNPS & " " &
                                    "where id = '" & dtewhr.Item("id") & "'")




        Next


    End Sub

    Public Sub LoadCurrentDataV2(startDate As String, endDate As String)

        Dim dHoursMTD As Decimal = 0
        Dim dHoursYTD As Decimal = 0
        Dim dRPHMTD As Decimal = 0
        Dim dRPHYTD As Decimal = 0
        Dim dJobRevenueMTD As Decimal = 0
        Dim dJustJobRevenueMTD As Decimal = 0
        Dim dJobRevenueYTD As Decimal = 0
        Dim dJustJobRevenueYTD As Decimal = 0

        Dim db As New Connection("Consumer_DSN")

        'run this code to update userInfo.currentTruckHours, .currentRPH, .currentSign, .currentAJS and .currentNPS
        db.ExecuteScaler("update userInfo set currentTruckHours = 0, ytdTruckHours = 0, " &
                         "currentRPH = 0, ytdRPH = 0, " &
                         "currentSign = 0, ytdSign = 0, " &
                         "currentNPS = 0, ytdNPS = 0, " &
                         "currentAJS = 0, ytdAJS = 0")



        'update currentTruckHours, ytdTruckHours, currentRPH and ytdRPH
        Dim cmd As New SqlCommand
        cmd.CommandText = "spPayrollSummaryGetEmployeesV2"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@startDate", startDate)
        cmd.Parameters.AddWithValue("@endDate", endDate)

        Dim dtEmpsWithHours As New DataTable
        dtEmpsWithHours = db.FillWithSP(cmd)
        For Each dtewhr As DataRow In dtEmpsWithHours.Rows
            cmd = New SqlCommand
            cmd.CommandText = "spReport_GetRevenueByEmployee"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@startDate", startDate)
            cmd.Parameters.AddWithValue("@endDate", endDate)
            cmd.Parameters.AddWithValue("@userID", dtewhr("userID"))

            Dim sdrRevenue As SqlDataReader = db.ExecuteReader(cmd)
            If sdrRevenue.HasRows Then

                dHoursMTD = 0
                dHoursYTD = 0
                dRPHMTD = 0
                dRPHYTD = 0
                dJobRevenueMTD = 0
                dJustJobRevenueMTD = 0
                dJobRevenueYTD = 0
                dJustJobRevenueYTD = 0

                While sdrRevenue.Read
                    dJobRevenueMTD = sdrRevenue("jobRevenue")
                    dJustJobRevenueMTD = sdrRevenue("justJobRevenue")
                End While


                cmd = New SqlCommand
                cmd.CommandText = "spPayrollSummaryGetWageSummaryV2"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@startDate", startDate)
                cmd.Parameters.AddWithValue("@endDate", endDate)
                cmd.Parameters.AddWithValue("@userID", dtewhr("userID"))

                Dim sdrWorkHours As SqlDataReader = db.ExecuteReader(cmd)
                If sdrWorkHours.HasRows Then
                    While sdrWorkHours.Read
                        dHoursMTD = sdrWorkHours("truckHours")
                    End While
                End If

                If dHoursMTD > 0 Then
                    dRPHMTD = (dJobRevenueMTD / dHoursMTD) * 0.5
                End If

                If Year(startDate) = Year(endDate) Then
                    '***** YTD
                    cmd = New SqlCommand
                    cmd.CommandText = "spReport_GetRevenueByEmployee"
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@startDate", "1/1/" & Year(startDate))
                    cmd.Parameters.AddWithValue("@endDate", endDate)
                    cmd.Parameters.AddWithValue("@userID", dtewhr("userID"))

                    Dim sdrYTDRevenue As SqlDataReader = db.ExecuteReader(cmd)
                    If sdrYTDRevenue.HasRows Then
                        While sdrYTDRevenue.Read
                            dJobRevenueYTD = sdrYTDRevenue("jobRevenue")
                            dJustJobRevenueYTD = sdrYTDRevenue("justJobRevenue")
                        End While


                        cmd = New SqlCommand
                        cmd.CommandText = "spPayrollSummaryGetWageSummaryV2"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@startDate", "1/1/" & Year(startDate))
                        cmd.Parameters.AddWithValue("@endDate", endDate)
                        cmd.Parameters.AddWithValue("@userID", dtewhr("userID"))

                        Dim sdrYTDWorkHours As SqlDataReader = db.ExecuteReader(cmd)
                        If sdrYTDWorkHours.HasRows Then
                            While sdrYTDWorkHours.Read
                                dHoursYTD = sdrYTDWorkHours("truckHours")
                            End While
                        End If

                        If dHoursYTD > 0 Then
                            dRPHYTD = (dJobRevenueYTD / dHoursYTD) * 0.5
                        End If

                    End If

                End If

                db.ExecuteScaler("update userInfo set currentRPH = " & dRPHMTD & ", " &
                                    "ytdRPH = " & dRPHYTD & ", " &
                                    "currentTruckHours = " & dHoursMTD & ", " &
                                    "ytdTruckHours = " & dHoursYTD & " " &
                                    "where userinfo.id = '" & dtewhr("userID") & "'")

            End If
        Next






        'update currentAJS, ytdAJS, currentSign, ytdSign, currentNPS, ytdNPS
        Dim currentAJS As Decimal
        Dim ytdAJS As Decimal
        Dim currentSign As Decimal
        Dim ytdSign As Decimal
        Dim currentNPS As Decimal
        Dim ytdNPS As Decimal
        Dim currentOSC As Decimal
        Dim ytdOSC As Decimal
        Dim cmdYTD As New SqlCommand

        cmd = New SqlCommand With {
            .CommandText = "spGetUsers",
            .CommandType = CommandType.StoredProcedure
        }

        Dim dtEmps As New DataTable
        dtEmps = db.FillWithSP(cmd)
        For Each dtewhr As DataRow In dtEmps.Rows


            '******************************************************************************* AJS *******************


            'currentAJS
            cmd = New SqlCommand
            cmd.CommandText = "spReport_GetAJS"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@startDate", startDate)
            cmd.Parameters.AddWithValue("@endDate", endDate)
            cmd.Parameters.AddWithValue("@userID", dtewhr.Item("id"))

            Dim dtAJS As New DataTable
            dtAJS = db.FillWithSP(cmd)
            For Each dts As DataRow In dtAJS.Rows
                If Not dts.Item("ajs") Is Nothing AndAlso Not IsDBNull(dts.Item("ajs")) Then
                    currentAJS = Math.Round(dts.Item("ajs"), 2)
                End If
            Next

            'ytdAJS
            cmdYTD = New SqlCommand
            cmdYTD.CommandText = "spReport_GetAJS"
            cmdYTD.CommandType = CommandType.StoredProcedure
            cmdYTD.Parameters.AddWithValue("@startDate", "1/1/" & Year(startDate))
            cmdYTD.Parameters.AddWithValue("@endDate", startDate)
            cmdYTD.Parameters.AddWithValue("@userID", dtewhr.Item("id"))

            dtAJS = New DataTable
            dtAJS = db.FillWithSP(cmd)
            For Each dts As DataRow In dtAJS.Rows
                If Not dts.Item("ajs") Is Nothing AndAlso Not IsDBNull(dts.Item("ajs")) Then
                    ytdAJS = Math.Round(dts.Item("ajs"), 2)
                End If
            Next


            '******************************************************************************* SIGN *******************


            Dim dSignsTotal As Integer = 0
            Dim sSignsYes As Integer = 0
            Dim dSignsYTDTotal As Integer = 0
            Dim sSignsYTDYes As Integer = 0



            'currentSign
            cmd = New SqlCommand
            cmd.CommandText = "spReport_GetSignPercentage"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@startDate", startDate)
            cmd.Parameters.AddWithValue("@endDate", endDate)
            cmd.Parameters.AddWithValue("@userID", dtewhr.Item("id"))

            Dim dtSigns As New DataTable
            dtSigns = db.FillWithSP(cmd)
            For Each dts As DataRow In dtSigns.Rows
                dSignsTotal += 1
                If dts("sign").ToString.ToLower = "y" Then
                    sSignsYes += 1
                End If
            Next

            If dSignsTotal <= 0 Then
                currentSign = 0
            Else
                currentSign = (sSignsYes / dSignsTotal) * 100
            End If




            'ytdSign
            cmdYTD = New SqlCommand
            cmdYTD.CommandText = "spReport_GetSignPercentage"
            cmdYTD.CommandType = CommandType.StoredProcedure
            cmdYTD.Parameters.AddWithValue("@startDate", "1/1/" & Year(startDate))
            cmdYTD.Parameters.AddWithValue("@endDate", endDate)
            cmdYTD.Parameters.AddWithValue("@userID", dtewhr.Item("id"))

            dtSigns = New DataTable
            dtSigns = db.FillWithSP(cmdYTD)
            For Each dts As DataRow In dtSigns.Rows
                dSignsYTDTotal += 1
                If dts("sign").ToString.ToLower = "y" Then
                    sSignsYTDYes += 1
                End If
            Next


            If dSignsYTDTotal <= 0 Then
                ytdSign = 0
            Else
                ytdSign = (sSignsYTDYes / dSignsYTDTotal) * 100
            End If


            '******************************************************************************* NPS *******************


            'currentNPS
            cmdYTD = New SqlCommand
            cmdYTD.CommandText = "spGetNPSByDateEmpID"
            cmdYTD.CommandType = CommandType.StoredProcedure
            cmdYTD.Parameters.AddWithValue("@startDate", startDate)
            cmdYTD.Parameters.AddWithValue("@endDate", endDate)
            cmdYTD.Parameters.AddWithValue("@userInfoId", dtewhr.Item("id"))

            currentNPS = 0
            dtSigns = New DataTable
            dtSigns = db.FillWithSP(cmdYTD)
            For Each dts As DataRow In dtSigns.Rows
                If Not dts("total") Is DBNull.Value Then
                    currentNPS = Math.Round(((dts("promoter").ToString() / dts("total").ToString()) - (dts("detractor").ToString() / dts("total").ToString())) * 100, 0)
                End If
            Next


            'ytdNPS
            cmdYTD = New SqlCommand
            cmdYTD.CommandText = "spGetNPSByDateEmpID"
            cmdYTD.CommandType = CommandType.StoredProcedure
            cmdYTD.Parameters.AddWithValue("@startDate", "1/1/" & Year(startDate))
            cmdYTD.Parameters.AddWithValue("@endDate", endDate)
            cmdYTD.Parameters.AddWithValue("@userInfoId", dtewhr.Item("id"))

            ytdNPS = 0
            dtSigns = New DataTable
            dtSigns = db.FillWithSP(cmdYTD)
            If dtSigns.Rows.Count > 0 Then
                For Each dts As DataRow In dtSigns.Rows
                    If Not dts("total") Is DBNull.Value Then
                        ytdNPS = Math.Round(((dts("promoter").ToString() / dts("total").ToString()) - (dts("detractor").ToString() / dts("total").ToString())) * 100, 0)
                    End If
                Next
            End If




            '******************************************************************************* OSC*******************


            'currentOSC
            Dim totalJobs As Integer = 0
            Dim totalWithRev As Integer = 0
            Dim totalWithoutRev As Integer = 0

            cmdYTD = New SqlCommand
            cmdYTD.CommandText = "spGetJobsByEmp"
            cmdYTD.CommandType = CommandType.StoredProcedure
            cmdYTD.Parameters.AddWithValue("@startDate", startDate)
            cmdYTD.Parameters.AddWithValue("@endDate", endDate)
            cmdYTD.Parameters.AddWithValue("@empID", dtewhr.Item("id"))

            dtSigns = New DataTable
            dtSigns = db.FillWithSP(cmdYTD)
            For Each dts As DataRow In dtSigns.Rows
                totalJobs = totalJobs + 1

                If Not dts("ticketRevenue") Is DBNull.Value Then
                    If dts("ticketRevenue") > 0 Then
                        totalWithRev = totalWithRev + 1
                    Else
                        totalWithoutRev = totalWithoutRev + 1
                    End If
                End If
            Next

            currentOSC = 0
            If totalJobs > 0 AndAlso totalWithRev > 0 Then
                currentOSC = Math.Round((totalWithRev / totalJobs) * 100, 2)
            End If




            'ytdOSC
            totalJobs = 0
            totalWithRev = 0
            totalWithoutRev = 0

            cmdYTD = New SqlCommand
            cmdYTD.CommandText = "spGetJobsByEmp"
            cmdYTD.CommandType = CommandType.StoredProcedure
            cmdYTD.Parameters.AddWithValue("@startDate", "1/1/" & Year(startDate))
            cmdYTD.Parameters.AddWithValue("@endDate", endDate)
            cmdYTD.Parameters.AddWithValue("@empID", dtewhr.Item("id"))

            dtSigns = New DataTable
            dtSigns = db.FillWithSP(cmdYTD)
            For Each dts As DataRow In dtSigns.Rows
                totalJobs = totalJobs + 1

                If Not dts("ticketRevenue") Is DBNull.Value Then
                    If dts("ticketRevenue") > 0 Then
                        totalWithRev = totalWithRev + 1
                    Else
                        totalWithoutRev = totalWithoutRev + 1
                    End If
                End If
            Next

            ytdOSC = 0
            If totalJobs > 0 AndAlso totalWithRev > 0 Then
                ytdOSC = Math.Round((totalWithRev / totalJobs) * 100, 2)
            End If






            db.ExecuteScaler("update userInfo set currentAJS = " & currentAJS & ", " &
                                    "ytdAJS = " & ytdAJS & ", " &
                                    "currentSign = " & currentSign & ", " &
                                    "ytdSign = " & ytdSign & ", " &
                                    "currentOSC = " & currentOSC & ", " &
                                    "ytdOSC = " & ytdOSC & ", " &
                                    "currentNPS = " & currentNPS & ", " &
                                    "ytdNPS = " & ytdNPS & " " &
                                    "where id = '" & dtewhr.Item("id") & "'")




        Next


    End Sub

End Class
