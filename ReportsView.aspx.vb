Imports System.Data
Imports System.Data.SqlClient
Imports Routes.Database


Partial Public Class ReportsView
    Inherits Utilities

#Region "Properties"
    Private RowCount As Integer

    Public TruckHoursMin As Double
    Private dOverallMktgRound As Double
    Private dOverallTruckRound As Double

    Private _employee As String
    Public Property Employee() As String
        Get
            Return _employee
        End Get
        Set(ByVal value As String)
            _employee = value
        End Set
    End Property

    Private _startdate As String
    Public Property StartDate() As String
        Get
            Return _startdate
        End Get
        Set(ByVal value As String)
            _startdate = value
        End Set
    End Property

    Private _enddate As String
    Public Property EndDate() As String
        Get
            Return _enddate
        End Get
        Set(ByVal value As String)
            _enddate = value
        End Set
    End Property

    Private _week1startdate As String
    Public Property Week2StartDate() As String
        Get
            Return _week1startdate
        End Get
        Set(ByVal value As String)
            _week1startdate = value
        End Set
    End Property

    Private _week2enddate As String
    Public Property Week2EndDate() As String
        Get
            Return _week2enddate
        End Get
        Set(ByVal value As String)
            _week2enddate = value
        End Set
    End Property


#End Region

#Region "Page Events"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        RedirectToPage("reportsv4.aspx")


        StartDate = Request.QueryString("startDate")
        EndDate = Request.QueryString("endDate")

        pnlCECReport.Visible = False
        pnlJobsReport.Visible = False
        pnlPayroll.Visible = False
        pnlPayrollDates.Visible = False
        pnlRPH.Visible = False
        pnlSign.Visible = False
        pnlTopDog.Visible = False
        pnlTopDogV2.Visible = False
        pnlEmployeeMetrics.Visible = False
        pnlAJS.Visible = False
        pnlTips.Visible = False
        pnlHourType.Visible = False
        pnlReasonNotConvert.Visible = False
        pnlOSCbyGuy.Visible = False
        pnlOSCGuyResults.Visible = False
        pnlDailyRoute.Visible = False
        pnlDumpLocation.Visible = False
        pnlDumpsByGuy.Visible = False
        pnlDumpsByGuyResults.Visible = False
        pnlDiscountsGuy.Visible = False

        If Not Page.IsPostBack Then

            Select Case Request.QueryString("rpt").ToLower
                Case "cec"
                    pnlCECReport.Visible = True
                    LoadCECReport()
                Case "jobs"
                    pnlJobsReport.Visible = True
                    LoadJobsReport()
                Case "pay"
                    StartDate = Request.QueryString("week1StartDate")
                    EndDate = Request.QueryString("week1EndDate")
                    Week2StartDate = Request.QueryString("week2StartDate")
                    Week2EndDate = Request.QueryString("week2EndDate")
                    pnlPayroll.Visible = True
                    LoadTopDogData()
                    LoadPayrollReport()
                Case "paydates"
                    StartDate = Request.QueryString("startDate")
                    EndDate = Request.QueryString("endDate")
                    pnlPayrollDates.Visible = True
                    LoadTopDogData()
                    LoadPayrollDatesReport()
                Case "rph"
                    pnlRPH.Visible = True
                    LoadRPHReport()
                Case "topdog"
                    pnlTopDogV3.Visible = True
                    LoadTopDogDataV3()
                    LoadTopDogReportV3()
                    'pnlTopDog.Visible = True
                    'LoadTopDogData()
                    'LoadTopDogReport()
                'Case "topdogv2"
                '    pnlTopDogV2.Visible = True
                '    LoadTopDogDataV2()
                '    LoadTopDogReportV2()
                'Case "topdogv3"
                '    pnlTopDogV3.Visible = True
                '    LoadTopDogDataV3()
                '    LoadTopDogReportV3()
                Case "metrics"
                    pnlEmployeeMetrics.Visible = True
                    LoadEmployeeMetricsData()
                    LoadEmployeeMetricsReport()
                Case "signs"
                    pnlSign.Visible = True
                    LoadSignsReport()
                Case "tips"
                    pnlTips.Visible = True
                    LoadTipsReport()
                Case "ajs"
                    pnlAJS.Visible = True
                    LoadAJSReport()
                Case "hourtype"
                    pnlHourType.Visible = True
                    LoadHourTypeReport()
                Case "reasonsnotconvert"
                    pnlReasonNotConvert.Visible = True
                    LoadReasonNotConvertReport()
                Case "oscbyguy"
                    pnlOSCbyGuy.Visible = True
                    LoadOnSiteConversionReport()
                Case "oscguyresults"
                    pnlOSCGuyResults.Visible = True
                    LoadOSCEmployeeDetailsReport()
                Case "dailyroute"
                    pnlDailyRoute.Visible = True
                    LoadDailyRouteReport()
                Case "dumplocation"
                    pnlDumpLocation.Visible = True
                    LoadDumpLocationReport()
                Case "gaslocation"
                    pnlGasLocation.Visible = True
                    LoadGasLocationReport()
                Case "dumpbyguy"
                    pnlDumpsByGuy.Visible = True
                    LoadDumpByGuyReport()
                Case "dumpguyresults"
                    pnlDumpsByGuyResults.Visible = True
                    LoadDumpByGuyResultsReport()
                Case "discountsguy"
                    pnlDiscountsGuy.Visible = True
                    LoadDiscountsByGuyReport()
                Case Else
                    litCECReportsView.Text = "No report specified"
            End Select
        End If

    End Sub

    Private Sub grdJobs_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdJobs.RowDataBound
        Static RowSpan As Integer
        Static PrevItem As String
        Static FirstItemInIndex As Integer
        If (e.Row.RowIndex = 0) Then
            PrevItem = e.Row.Cells(0).Text
            FirstItemInIndex = 0
            RowSpan = 0
        End If
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If (PrevItem = e.Row.Cells(0).Text) Then
                If (e.Row.RowIndex <> FirstItemInIndex) Then
                    e.Row.Cells.RemoveAt(0)
                End If
                RowSpan += 1
            Else
                Dim FirstCell = grdJobs.Rows(FirstItemInIndex).Cells(0)
                FirstCell.RowSpan = RowSpan
                FirstItemInIndex = e.Row.RowIndex
                PrevItem = e.Row.Cells(0).Text
                RowSpan = 1
            End If
        End If
        If (e.Row.RowIndex = RowCount - 1) AndAlso e.Row.RowIndex > 0 Then
            Dim FirstCell = grdJobs.Rows(FirstItemInIndex).Cells(0)
            FirstCell.RowSpan = RowSpan
        End If
    End Sub


#End Region

#Region "Report Loading Subs"
    Private Sub LoadCECReport()
        Dim tonsCom As Double
        Dim tonsRes As Double
        Dim tonsNoCEC As Double
        Dim amtCom As Double
        Dim amtRes As Double
        Dim amtNoCEC As Double
        Dim jobsCom As Double
        Dim jobsRes As Double
        Dim jobsNoCEC As Double

        Dim tonsComTotal As Double = 0
        Dim tonsResTotal As Double = 0
        Dim tonsNoCECTotal As Double = 0
        Dim tonsTotal As Double = 0
        Dim amtComTotal As Double = 0
        Dim amtResTotal As Double = 0
        Dim amtNoCECTotal As Double = 0
        Dim amtTotal As Double = 0
        Dim jobsComTotal As Double = 0
        Dim jobsResTotal As Double = 0
        Dim jobsNoCECTotal As Double = 0
        Dim jobsTotal As Double = 0

        Dim dt As New DataTable
        dt.Columns.Add("county")
        dt.Columns.Add("tonsCommercial")
        dt.Columns.Add("tonsResidential")
        dt.Columns.Add("tonsNoCEC")
        dt.Columns.Add("tonsTotal")
        dt.Columns.Add("amountCommercial")
        dt.Columns.Add("amountResidential")
        dt.Columns.Add("amountNoCEC")
        dt.Columns.Add("amountTotal")
        dt.Columns.Add("jobsCommercial")
        dt.Columns.Add("jobsResidential")
        dt.Columns.Add("jobsNoCEC")
        dt.Columns.Add("jobsTotal")

        Dim dr As DataRow
        dr = dt.NewRow
        dr("county") = "County"
        dr("tonsCommercial") = CEC_COMMERCIAL
        dr("tonsResidential") = CEC_RESIDENTIAL
        dr("tonsNoCEC") = NO_CEC
        dr("tonsTotal") = "Total"
        dr("amountCommercial") = CEC_COMMERCIAL
        dr("amountResidential") = CEC_RESIDENTIAL
        dr("amountNoCEC") = NO_CEC
        dr("amountTotal") = "Total"
        dr("jobsCommercial") = CEC_COMMERCIAL
        dr("jobsResidential") = CEC_RESIDENTIAL
        dr("jobsNoCEC") = NO_CEC
        dr("jobsTotal") = "Total"
        dt.Rows.Add(dr)



        Dim db As New Connection("Consumer_DSN")

        Dim cmd As New SqlCommand
        cmd.CommandText = "spGetDropDownListData"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@segment", "cecCounty")

        Dim dtCEC As New DataTable
        dtCEC = db.FillWithSP(cmd)
        For Each dtr As DataRow In dtCEC.Rows

            tonsCom = 0
            tonsRes = 0
            tonsNoCEC = 0
            amtCom = 0
            amtRes = 0
            amtNoCEC = 0
            jobsCom = 0
            jobsRes = 0
            jobsNoCEC = 0


            Dim cmdCEC As New SqlCommand
            cmdCEC.CommandText = "spReport_GetCec"
            cmdCEC.CommandType = CommandType.StoredProcedure
            cmdCEC.Parameters.AddWithValue("@startDate", Request.QueryString("startDate"))
            cmdCEC.Parameters.AddWithValue("@endDate", Request.QueryString("endDate"))
            cmdCEC.Parameters.AddWithValue("@county", dtr("displayName").ToString.Trim)

            Dim dtCECResults As New DataTable
            dtCECResults = db.FillWithSP(cmdCEC)
            For Each drcecresults As DataRow In dtCECResults.Rows
                Select Case drcecresults("clientType").ToString.ToLower
                    Case CEC_COMMERCIAL.ToLower
                        tonsCom = tonsCom + (ReturnFractionValue(drcecresults("loadSize")) * (drcecresults("mswPercentage") / 100))
                        amtCom = amtCom + drcecresults("feeCharged")
                        jobsCom = jobsCom + 1
                    Case NO_CEC.ToLower
                        tonsNoCEC = tonsNoCEC + ReturnFractionValue(drcecresults("loadSize"))
                        amtNoCEC = amtNoCEC + drcecresults("feeCharged")
                        jobsNoCEC = jobsNoCEC + 1
                    Case Else
                        tonsRes = tonsRes + (ReturnFractionValue(drcecresults("loadSize")) * (drcecresults("mswPercentage") / 100))
                        amtRes = amtRes + drcecresults("feeCharged")
                        jobsRes = jobsRes + 1
                End Select

            Next

            dr = dt.NewRow
            dr("county") = dtr("displayName")
            dr("tonsCommercial") = tonsCom.ToString("0,0.00")
            dr("tonsResidential") = tonsRes.ToString("0,0.00")
            dr("tonsNoCEC") = tonsNoCEC.ToString("0,0.00")
            dr("tonsTotal") = (tonsCom + tonsRes).ToString("0,0.00")
            dr("amountCommercial") = amtCom.ToString("C2")
            dr("amountResidential") = amtRes.ToString("C2")
            dr("amountNoCEC") = amtNoCEC.ToString("C2")
            dr("amountTotal") = (amtCom + amtRes).ToString("C2")
            dr("jobsCommercial") = jobsCom
            dr("jobsResidential") = jobsRes
            dr("jobsNoCEC") = jobsNoCEC
            dr("jobsTotal") = jobsCom + jobsRes
            dt.Rows.Add(dr)

            tonsComTotal = tonsComTotal + tonsCom
            tonsResTotal = tonsResTotal + tonsRes
            tonsNoCECTotal = tonsNoCECTotal + tonsNoCEC
            tonsTotal = tonsComTotal + tonsResTotal

            amtComTotal = amtComTotal + amtCom
            amtResTotal = amtResTotal + amtRes
            amtNoCECTotal = amtNoCECTotal + amtNoCEC
            amtTotal = amtComTotal + amtResTotal

            jobsComTotal = jobsComTotal + jobsCom
            jobsResTotal = jobsResTotal + jobsRes
            jobsNoCECTotal = jobsNoCECTotal + jobsNoCEC
            jobsTotal = jobsComTotal + jobsResTotal

        Next

        grdCECResults.DataSource = dt
        grdCECResults.DataBind()

        grdCECResults.HeaderRow.Cells(0).Text = ""
        grdCECResults.HeaderRow.Cells(1).ColumnSpan = 4
        grdCECResults.HeaderRow.Cells(1).Text = "Tons"

        grdCECResults.HeaderRow.Cells(2).ColumnSpan = 4
        grdCECResults.HeaderRow.Cells(2).Text = "Amount"

        grdCECResults.HeaderRow.Cells(3).ColumnSpan = 4
        grdCECResults.HeaderRow.Cells(3).Text = "Jobs"

        grdCECResults.HeaderRow.Cells.RemoveAt(4)
        grdCECResults.HeaderRow.Cells.RemoveAt(4)
        grdCECResults.HeaderRow.Cells.RemoveAt(4)
        grdCECResults.HeaderRow.Cells.RemoveAt(4)
        grdCECResults.HeaderRow.Cells.RemoveAt(4)
        grdCECResults.HeaderRow.Cells.RemoveAt(4)
        grdCECResults.HeaderRow.Cells.RemoveAt(4)
        grdCECResults.HeaderRow.Cells.RemoveAt(4)
        grdCECResults.HeaderRow.Cells.RemoveAt(4)

        grdCECResults.FooterRow.Cells(1).Text = tonsComTotal.ToString("0,0.00")
        grdCECResults.FooterRow.Cells(2).Text = tonsResTotal.ToString("0,0.00")
        grdCECResults.FooterRow.Cells(4).Text = tonsNoCECTotal.ToString("0,0.00")
        grdCECResults.FooterRow.Cells(3).Text = tonsTotal.ToString("0,0.00")

        grdCECResults.FooterRow.Cells(5).Text = amtComTotal.ToString("C2")
        grdCECResults.FooterRow.Cells(6).Text = amtResTotal.ToString("C2")
        grdCECResults.FooterRow.Cells(8).Text = amtNoCECTotal.ToString("C2")
        grdCECResults.FooterRow.Cells(7).Text = amtTotal.ToString("C2")

        grdCECResults.FooterRow.Cells(9).Text = jobsComTotal
        grdCECResults.FooterRow.Cells(10).Text = jobsResTotal
        grdCECResults.FooterRow.Cells(12).Text = jobsNoCECTotal
        grdCECResults.FooterRow.Cells(11).Text = jobsTotal

    End Sub

    Private Sub LoadJobsReport()

        Dim totJobs As Double = 0
        Dim totRev As Double = 0
        Dim totDisc As Double = 0
        Dim totCEC As Double = 0
        Dim totNet As Double = 0

        Dim dt As New DataTable
        dt.Columns.Add("routeInfo")
        dt.Columns.Add("jobID")
        dt.Columns.Add("ticketNumber")
        dt.Columns.Add("ticketRevenue")
        dt.Columns.Add("ticketDiscount")
        dt.Columns.Add("ticketCEC")
        dt.Columns.Add("ticketNet")
        dt.Columns.Add("sign")
        dt.Columns.Add("doorHangers")
        dt.Columns.Add("payMethod")
        dt.Columns.Add("ticketDetails")

        Dim dr As DataRow
        dr = dt.NewRow
        dr("routeInfo") = "Route"
        dr("jobID") = "Job ID"
        dr("ticketNumber") = "Ticket Number"
        dr("ticketRevenue") = "Revenue"
        dr("ticketDiscount") = "Discount"
        dr("ticketCEC") = "CEC"
        dr("ticketNet") = "Net"
        dr("sign") = "Sign"
        dr("doorHangers") = "Door Hangers"
        dr("payMethod") = "Payment"
        dr("ticketDetails") = "Details"
        dt.Rows.Add(dr)

        Dim db As New Connection("Consumer_DSN")

        Dim cmd As New SqlCommand
        cmd.CommandText = "spReport_GetJobs"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@startDate", Request.QueryString("startDate"))
        cmd.Parameters.AddWithValue("@endDate", Request.QueryString("endDate"))

        Dim dtJobs As New DataTable
        dtJobs = db.FillWithSP(cmd)
        For Each dtr As DataRow In dtJobs.Rows

            dr = dt.NewRow
            dr("jobID") = "<a href='/routes/RouteEntry.aspx?rid=" & dtr("routeID") & "'>" & dtr("jobID") & "</a>"
            dr("routeInfo") = dtr("routeNumber") & " - " & dtr("routeDate")
            dr("ticketNumber") = dtr("ticketNumber")

            dr("ticketRevenue") = CDbl(dtr("ticketRevenue")).ToString("0,0.00")
            totRev = totRev + dtr("ticketRevenue")

            If Not String.IsNullOrEmpty(dtr("ticketDiscount").ToString.Trim) Then
                dr("ticketDiscount") = CDbl(dtr("ticketDiscount")).ToString("0,0.00")
                totDisc = totDisc + dtr("ticketDiscount")
            Else
                dr("ticketDiscount") = "0.00"
            End If

            If Not String.IsNullOrEmpty(dtr("ticketCEC").ToString.Trim) Then
                dr("ticketCEC") = CDbl(dtr("ticketCEC")).ToString("0,0.00")
                totCEC = totCEC + dtr("ticketCEC")
            Else
                dr("ticketCEC") = "0.00"
            End If

            If Not String.IsNullOrEmpty(dtr("ticketNet").ToString.Trim) Then
                dr("ticketNet") = CDbl(dtr("ticketNet")).ToString("0,0.00")
                totNet = totNet + dtr("ticketNet")
            Else
                dr("ticketNet") = "0.00"
            End If

            dr("sign") = dtr("sign")
            dr("doorHangers") = dtr("doorHangers")
            dr("payMethod") = dtr("payMethod")
            dr("ticketDetails") = dtr("ticketDetails")
            dt.Rows.Add(dr)

        Next

        RowCount = dt.Rows.Count
        grdJobs.DataSource = dt
        grdJobs.DataBind()

        grdJobs.FooterRow.Cells(3).Text = "Revenue: " & totRev.ToString("0,0.00")
        grdJobs.FooterRow.Cells(4).Text = "Discount: " & totDisc.ToString("0,0.00")
        grdJobs.FooterRow.Cells(5).Text = "CEC: " & totCEC.ToString(",0.00")
        grdJobs.FooterRow.Cells(6).Text = "Net: " & totNet.ToString("0,0.00")

        grdJobs.FooterRow.Cells(8).Text = "Jobs: " & RowCount - 1
        grdJobs.FooterRow.Cells(9).Text = "AJS: " & (totNet / (RowCount - 1)).ToString("0,0.00")

    End Sub

    Private Sub LoadPayrollReport()
        Dim db As New Connection("Consumer_DSN")

        Dim cmd As New SqlCommand

        Dim dtFirstWeek As New DataTable
        dtFirstWeek.Columns.Add("employee")
        dtFirstWeek.Columns.Add("totalHours")
        dtFirstWeek.Columns.Add("clericalHours")
        dtFirstWeek.Columns.Add("mktgHours")
        dtFirstWeek.Columns.Add("truckHours")
        dtFirstWeek.Columns.Add("otTruckHours")
        dtFirstWeek.Columns.Add("basePayRate")
        dtFirstWeek.Columns.Add("basePay")
        dtFirstWeek.Columns.Add("otPayRate")
        dtFirstWeek.Columns.Add("otPay")
        dtFirstWeek.Columns.Add("bonus")
        dtFirstWeek.Columns.Add("profitShare")
        dtFirstWeek.Columns.Add("totalBonus")
        dtFirstWeek.Columns.Add("totalPay")
        dtFirstWeek.Columns.Add("adjustments")
        dtFirstWeek.Columns.Add("netPay")
        dtFirstWeek.Columns.Add("hourlyAvg")

        Dim dr As DataRow

        cmd = New SqlCommand
        cmd.CommandText = "spPayrollSummaryGetEmployees"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@startDate", StartDate)
        cmd.Parameters.AddWithValue("@endDate", EndDate)

        Dim dBasePayRate As Decimal = 0
        Dim dTrkHours As Decimal = 0
        Dim dOTHours As Decimal = 0
        Dim dNonTrkHours As Decimal = 0
        Dim dBasePay As Decimal = 0
        Dim dOTPay As Decimal = 0
        Dim dPSPay As Decimal = 0
        Dim dClericalHours As Decimal = 0
        Dim dMktgHours As Decimal = 0
        Dim dBonus As Decimal = 0
        Dim dAdjustment As Decimal = 0

        Dim dTotalHours As Decimal = 0
        Dim dTotalClerical As Decimal = 0
        Dim dTotalMktg As Decimal = 0
        Dim dTotalTruck As Decimal = 0
        Dim dTotalOT As Decimal = 0
        Dim dTotalBasePay As Decimal = 0
        Dim dTotalOTPay As Decimal = 0
        Dim dTotalBonus As Decimal = 0
        Dim dTotalPS As Decimal = 0
        Dim dTotalAdj As Decimal = 0
        Dim dTotalNet As Decimal = 0

        Dim dOverallTotalHours As Decimal = 0
        Dim dOverallTotalClerical As Decimal = 0
        Dim dOverallTotalMktg As Decimal = 0
        Dim dOverallTotalTruck As Decimal = 0
        Dim dOverallTotalOT As Decimal = 0
        Dim dOverallTotalBasePay As Decimal = 0
        Dim dOverallTotalOTPay As Decimal = 0
        Dim dOverallTotalBonus As Decimal = 0
        Dim dOverallTotalPS As Decimal = 0
        Dim dOverallTotalAdj As Decimal = 0
        Dim dOverallTotalNet As Decimal = 0


        Dim dtEmpsWithHours As New DataTable
        dtEmpsWithHours = db.FillWithSP(cmd)
        For Each dtewhr As DataRow In dtEmpsWithHours.Rows
            If dtewhr.Item("payrollSummary").ToString.Trim.ToLower = "y" Then
                dBasePay = 0
                dOTPay = 0
                dPSPay = 0
                dClericalHours = 0
                dMktgHours = 0
                dBonus = 0
                dAdjustment = 0

                dr = dtFirstWeek.NewRow

                cmd = New SqlCommand
                cmd.CommandText = "spGetPayrollAdjustment"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@userID", dtewhr.Item("userID"))
                Dim xy As DataTable = db.FillWithSP(cmd)

                For Each xrow As DataRow In xy.Rows
                    If xrow("adjDate") >= StartDate And xrow("adjDate") <= EndDate Then
                        If xrow("adjAmount") >= 0 Then
                            dBonus += xrow("adjAmount")
                        Else
                            dAdjustment += xrow("adjAmount")
                        End If
                    End If
                Next


                'we now have employees with wages for this period
                dr("employee") = dtewhr.Item("firstName") & " " & dtewhr.Item("lastName")

                cmd = New SqlCommand
                cmd.CommandText = "spPayrollSummaryGetWageSummary"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@userID", dtewhr.Item("userID"))
                cmd.Parameters.AddWithValue("@startDate", StartDate)
                cmd.Parameters.AddWithValue("@endDate", EndDate)

                xy = db.FillWithSP(cmd)

                If xy.Rows.Count > 0 Then
                    dTrkHours = xy.Rows(0)("truckHours")
                    dNonTrkHours = xy.Rows(0)("nonTruckHours")
                    dClericalHours = xy.Rows(0)("routeCount") * 0.5
                    dPSPay = xy.Rows(0)("profitShare")
                    dMktgHours = ReturnMarketingHours(dtewhr.Item("userID"), StartDate, EndDate)

                    dBasePayRate = (xy.Rows(0)("nonTruckWages") + xy.Rows(0)("truckWages")) / xy.Rows(0)("totalHours")
                    If (dTrkHours + dNonTrkHours) > 40 Then
                        dr("basePay") = ((dTrkHours + dNonTrkHours) * dBasePayRate).ToString("F")
                        dBasePay = (dTrkHours + dNonTrkHours) * dBasePayRate

                        dr("otTruckHours") = (dTrkHours + dNonTrkHours) - 40
                        dOTHours = (dTrkHours + dNonTrkHours) - 40
                        dr("otPayRate") = (dBasePayRate * 0.5).ToString("F")
                        dr("otPay") = (((dTrkHours + dNonTrkHours) - 40) * (dBasePayRate * 0.5)).ToString("F")
                        dOTPay = ((dTrkHours + dNonTrkHours) - 40) * (dBasePayRate * 0.5)

                    Else
                        dr("basePay") = xy.Rows(0)("nonTruckWages") + xy.Rows(0)("truckWages")
                        dBasePay = xy.Rows(0)("nonTruckWages") + xy.Rows(0)("truckWages")

                        dr("otTruckHours") = "0.00"
                        dOTHours = 0
                        dr("otPayRate") = "0.00"
                        dr("otPay") = "0.00"
                        dOTPay = 0
                    End If


                    dr("totalHours") = (dTrkHours + dNonTrkHours).ToString("F")
                    dr("clericalHours") = dClericalHours.ToString("F")
                    dr("mktgHours") = dMktgHours.ToString("F")
                    dr("truckHours") = ((dTrkHours + dNonTrkHours) - dClericalHours - dMktgHours - dOTHours).ToString("F")


                    dr("basePayRate") = dBasePayRate.ToString("F")
                    dr("profitShare") = dPSPay.ToString("F")
                    dr("bonus") = dBonus.ToString("F")
                    dr("totalBonus") = (dBonus + dPSPay).ToString("F")
                    dr("adjustments") = dAdjustment.ToString("F")
                    dr("totalPay") = (dBasePay + dOTPay + dPSPay + dBonus).ToString("F")
                    dr("netPay") = (dBasePay + dOTPay + dPSPay + dBonus + dAdjustment).ToString("F")
                    dr("hourlyAvg") = ((dBasePay + dOTPay + dPSPay + dBonus + dAdjustment) / (dTrkHours + dNonTrkHours)).ToString("F")

                End If

                'add together the totals for each column
                dTotalHours += dTrkHours + dNonTrkHours
                dTotalClerical += dClericalHours
                dTotalMktg += dMktgHours
                dTotalTruck += (dTrkHours + dNonTrkHours) - dClericalHours - dMktgHours - dOTHours
                dTotalOT += dr("otTruckHours")
                dTotalBasePay += dBasePay
                dTotalOTPay += dOTPay
                dTotalBonus += dBonus
                dTotalPS += dPSPay
                dTotalAdj += dAdjustment
                dTotalNet += dBasePay + dOTPay + dPSPay + dBonus + dAdjustment

                dtFirstWeek.Rows.Add(dr)
            End If
        Next


        grdPayroll.DataSource = dtFirstWeek
        grdPayroll.DataBind()

        grdPayroll.HeaderRow.Cells(0).Text = "Employee"
        grdPayroll.HeaderRow.Cells(1).Text = "Total Hours"
        grdPayroll.HeaderRow.Cells(2).Text = "Clerical Hours"
        grdPayroll.HeaderRow.Cells(3).Text = "Mktg. Hours"
        grdPayroll.HeaderRow.Cells(4).Text = "Truck Hours"
        grdPayroll.HeaderRow.Cells(5).Text = "OT Truck Hours"
        grdPayroll.HeaderRow.Cells(6).Text = "Base Pay Rate"
        grdPayroll.HeaderRow.Cells(7).Text = "Base Pay"
        grdPayroll.HeaderRow.Cells(8).Text = "OT Pay Rate"
        grdPayroll.HeaderRow.Cells(9).Text = "OT Pay"
        grdPayroll.HeaderRow.Cells(10).Text = "Bonus"
        grdPayroll.HeaderRow.Cells(11).Text = "Profit Share"
        grdPayroll.HeaderRow.Cells(12).Text = "Total Bonus"
        grdPayroll.HeaderRow.Cells(13).Text = "Total Pay"
        grdPayroll.HeaderRow.Cells(14).Text = "Adjustments"
        grdPayroll.HeaderRow.Cells(15).Text = "Net Pay"
        grdPayroll.HeaderRow.Cells(16).Text = "Hourly Avg"

        grdPayroll.FooterRow.Cells(1).Text = dTotalHours.ToString("F")
        grdPayroll.FooterRow.Cells(2).Text = dTotalClerical.ToString("F")
        grdPayroll.FooterRow.Cells(3).Text = dTotalMktg.ToString("F")
        grdPayroll.FooterRow.Cells(4).Text = dTotalTruck.ToString("F")
        grdPayroll.FooterRow.Cells(5).Text = dTotalOT.ToString("F")
        grdPayroll.FooterRow.Cells(7).Text = dTotalBasePay.ToString("F")
        grdPayroll.FooterRow.Cells(9).Text = dTotalOTPay.ToString("F")
        grdPayroll.FooterRow.Cells(10).Text = dTotalBonus.ToString("F")
        grdPayroll.FooterRow.Cells(11).Text = dTotalPS.ToString("F")
        grdPayroll.FooterRow.Cells(12).Text = (dTotalBonus + dTotalPS).ToString("F")
        grdPayroll.FooterRow.Cells(13).Text = (dTotalBasePay + dTotalOTPay + dTotalBonus + dTotalPS).ToString("F")
        grdPayroll.FooterRow.Cells(14).Text = (dTotalAdj).ToString("F")
        grdPayroll.FooterRow.Cells(15).Text = (dTotalNet).ToString("F")

        dOverallTotalHours = dTotalHours
        dOverallTotalClerical = dTotalClerical
        dOverallTotalMktg = dTotalMktg
        dOverallTotalTruck = dTotalTruck
        dOverallTotalOT = dTotalOT
        dOverallTotalBasePay = dTotalBasePay
        dOverallTotalOTPay = dTotalOTPay
        dOverallTotalBonus = dTotalBonus
        dOverallTotalPS = dTotalPS
        dOverallTotalNet = dTotalNet















        Dim dtSecondWeek = New DataTable
        dtSecondWeek.Columns.Add("employee")
        dtSecondWeek.Columns.Add("totalHours")
        dtSecondWeek.Columns.Add("clericalHours")
        dtSecondWeek.Columns.Add("mktgHours")
        dtSecondWeek.Columns.Add("truckHours")
        dtSecondWeek.Columns.Add("otTruckHours")
        dtSecondWeek.Columns.Add("basePayRate")
        dtSecondWeek.Columns.Add("basePay")
        dtSecondWeek.Columns.Add("otPayRate")
        dtSecondWeek.Columns.Add("otPay")
        dtSecondWeek.Columns.Add("bonus")
        dtSecondWeek.Columns.Add("profitShare")
        dtSecondWeek.Columns.Add("totalBonus")
        dtSecondWeek.Columns.Add("totalPay")
        dtSecondWeek.Columns.Add("adjustments")
        dtSecondWeek.Columns.Add("netPay")
        dtSecondWeek.Columns.Add("hourlyAvg")

        cmd = New SqlCommand
        cmd.CommandText = "spPayrollSummaryGetEmployees"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@startDate", Week2StartDate)
        cmd.Parameters.AddWithValue("@endDate", Week2EndDate)

        dBasePayRate = 0
        dTrkHours = 0
        dNonTrkHours = 0
        dBasePay = 0
        dOTPay = 0
        dPSPay = 0
        dClericalHours = 0
        dMktgHours = 0
        dBonus = 0
        dAdjustment = 0

        dTotalHours = 0
        dTotalClerical = 0
        dTotalMktg = 0
        dTotalTruck = 0
        dTotalOT = 0
        dTotalBasePay = 0
        dTotalOTPay = 0
        dTotalBonus = 0
        dTotalPS = 0
        dTotalAdj = 0
        dTotalNet = 0


        dtEmpsWithHours = New DataTable
        dtEmpsWithHours = db.FillWithSP(cmd)
        For Each dtewhr As DataRow In dtEmpsWithHours.Rows
            If dtewhr.Item("payrollSummary").ToString.Trim.ToLower = "y" Then
                dBasePay = 0
                dOTPay = 0
                dPSPay = 0
                dClericalHours = 0
                dMktgHours = 0
                dBonus = 0
                dAdjustment = 0

                dr = dtSecondWeek.NewRow

                cmd = New SqlCommand
                cmd.CommandText = "spGetPayrollAdjustment"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@userID", dtewhr.Item("userID"))
                Dim xy As DataTable = db.FillWithSP(cmd)

                For Each xrow As DataRow In xy.Rows
                    If xrow("adjDate") >= StartDate And xrow("adjDate") <= EndDate Then
                        If xrow("adjAmount") >= 0 Then
                            dBonus += xrow("adjAmount")
                        Else
                            dAdjustment += xrow("adjAmount")
                        End If
                    End If
                Next


                'we now have employees with wages for this period
                dr("employee") = dtewhr.Item("firstName") & " " & dtewhr.Item("lastName")

                cmd = New SqlCommand
                cmd.CommandText = "spPayrollSummaryGetWageSummary"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@userID", dtewhr.Item("userID"))
                cmd.Parameters.AddWithValue("@startDate", Week2StartDate)
                cmd.Parameters.AddWithValue("@endDate", Week2EndDate)

                xy = db.FillWithSP(cmd)

                If xy.Rows.Count > 0 Then
                    dTrkHours = xy.Rows(0)("truckHours")
                    dNonTrkHours = xy.Rows(0)("nonTruckHours")
                    dClericalHours = xy.Rows(0)("routeCount") * 0.5
                    dPSPay = xy.Rows(0)("profitShare")
                    dMktgHours = ReturnMarketingHours(dtewhr.Item("userID"), Week2StartDate, Week2EndDate)

                    dBasePayRate = (xy.Rows(0)("nonTruckWages") + xy.Rows(0)("truckWages")) / xy.Rows(0)("totalHours")
                    If (dTrkHours + dNonTrkHours) > 40 Then
                        dr("basePay") = ((dTrkHours + dNonTrkHours) * dBasePayRate).ToString("F")
                        dBasePay = (dTrkHours + dNonTrkHours) * dBasePayRate

                        dr("otTruckHours") = (dTrkHours + dNonTrkHours) - 40
                        dOTHours = (dTrkHours + dNonTrkHours) - 40
                        dr("otPayRate") = (dBasePayRate * 0.5).ToString("F")
                        dr("otPay") = (((dTrkHours + dNonTrkHours) - 40) * (dBasePayRate * 0.5)).ToString("F")
                        dOTPay = ((dTrkHours + dNonTrkHours) - 40) * (dBasePayRate * 0.5)

                    Else
                        dr("basePay") = xy.Rows(0)("nonTruckWages") + xy.Rows(0)("truckWages")
                        dBasePay = xy.Rows(0)("nonTruckWages") + xy.Rows(0)("truckWages")

                        dr("otTruckHours") = "0.00"
                        dOTHours = 0
                        dr("otPayRate") = "0.00"
                        dr("otPay") = "0.00"
                        dOTPay = 0
                    End If

                    dr("totalHours") = (dTrkHours + dNonTrkHours).ToString("F")
                    dr("clericalHours") = dClericalHours.ToString("F")
                    dr("mktgHours") = dMktgHours.ToString("F")
                    dr("truckHours") = ((dTrkHours + dNonTrkHours) - dClericalHours - dMktgHours - dOTHours).ToString("F")
                    dr("basePayRate") = dBasePayRate.ToString("F")
                    dr("profitShare") = dPSPay.ToString("F")
                    dr("bonus") = dBonus.ToString("F")
                    dr("totalBonus") = (dBonus + dPSPay).ToString("F")
                    dr("adjustments") = dAdjustment.ToString("F")
                    dr("totalPay") = (dBasePay + dOTPay + dPSPay + dBonus).ToString("F")
                    dr("netPay") = (dBasePay + dOTPay + dPSPay + dBonus + dAdjustment).ToString("F")
                    dr("hourlyAvg") = ((dBasePay + dOTPay + dPSPay + dBonus + dAdjustment) / (dTrkHours + dNonTrkHours)).ToString("F")

                End If

                'add together the totals for each column
                dTotalHours += dTrkHours + dNonTrkHours
                dTotalClerical += dClericalHours
                dTotalMktg += dMktgHours
                dTotalTruck += (dTrkHours + dNonTrkHours) - dClericalHours - dMktgHours - dOTHours
                dTotalOT += dr("otTruckHours")
                dTotalBasePay += dBasePay
                dTotalOTPay += dOTPay
                dTotalBonus += dBonus
                dTotalPS += dPSPay
                dTotalAdj += dAdjustment
                dTotalNet += dBasePay + dOTPay + dPSPay + dBonus + dAdjustment

                dtSecondWeek.Rows.Add(dr)
            End If
        Next


        grdPayrollSecondWeek.DataSource = dtSecondWeek
        grdPayrollSecondWeek.DataBind()

        grdPayrollSecondWeek.HeaderRow.Cells(0).Text = "Employee"
        grdPayrollSecondWeek.HeaderRow.Cells(1).Text = "Total Hours"
        grdPayrollSecondWeek.HeaderRow.Cells(2).Text = "Clerical Hours"
        grdPayrollSecondWeek.HeaderRow.Cells(3).Text = "Mktg. Hours"
        grdPayrollSecondWeek.HeaderRow.Cells(4).Text = "Truck Hours"
        grdPayrollSecondWeek.HeaderRow.Cells(5).Text = "OT Truck Hours"
        grdPayrollSecondWeek.HeaderRow.Cells(6).Text = "Base Pay Rate"
        grdPayrollSecondWeek.HeaderRow.Cells(7).Text = "Base Pay"
        grdPayrollSecondWeek.HeaderRow.Cells(8).Text = "OT Pay Rate"
        grdPayrollSecondWeek.HeaderRow.Cells(9).Text = "OT Pay"
        grdPayrollSecondWeek.HeaderRow.Cells(10).Text = "Bonus"
        grdPayrollSecondWeek.HeaderRow.Cells(11).Text = "Profit Share"
        grdPayrollSecondWeek.HeaderRow.Cells(12).Text = "Total Bonus"
        grdPayrollSecondWeek.HeaderRow.Cells(13).Text = "Total Pay"
        grdPayrollSecondWeek.HeaderRow.Cells(14).Text = "Adjustments"
        grdPayrollSecondWeek.HeaderRow.Cells(15).Text = "Net Pay"
        grdPayrollSecondWeek.HeaderRow.Cells(16).Text = "Hourly Avg"

        grdPayrollSecondWeek.FooterRow.Cells(1).Text = dTotalHours.ToString("F")
        grdPayrollSecondWeek.FooterRow.Cells(2).Text = dTotalClerical.ToString("F")
        grdPayrollSecondWeek.FooterRow.Cells(3).Text = dTotalMktg.ToString("F")
        grdPayrollSecondWeek.FooterRow.Cells(4).Text = dTotalTruck.ToString("F")
        grdPayrollSecondWeek.FooterRow.Cells(5).Text = dTotalOT.ToString("F")
        grdPayrollSecondWeek.FooterRow.Cells(7).Text = dTotalBasePay.ToString("F")
        grdPayrollSecondWeek.FooterRow.Cells(9).Text = dTotalOTPay.ToString("F")
        grdPayrollSecondWeek.FooterRow.Cells(10).Text = dTotalBonus.ToString("F")
        grdPayrollSecondWeek.FooterRow.Cells(11).Text = dTotalPS.ToString("F")
        grdPayrollSecondWeek.FooterRow.Cells(12).Text = (dTotalBonus + dTotalPS).ToString("F")
        grdPayrollSecondWeek.FooterRow.Cells(13).Text = (dTotalBasePay + dTotalOTPay + dTotalBonus + dTotalPS).ToString("F")
        grdPayrollSecondWeek.FooterRow.Cells(14).Text = (dTotalAdj).ToString("F")
        grdPayrollSecondWeek.FooterRow.Cells(15).Text = (dTotalNet).ToString("F")

        dOverallTotalHours += dTotalHours
        dOverallTotalClerical += dTotalClerical
        dOverallTotalMktg += dTotalMktg
        dOverallTotalTruck += dTotalTruck
        dOverallTotalOT += dTotalOT
        dOverallTotalBasePay += dTotalBasePay
        dOverallTotalOTPay += dTotalOTPay
        dOverallTotalBonus += dTotalBonus
        dOverallTotalPS += dTotalPS
        dOverallTotalNet += dTotalNet








        grdPayrollTotal.DataSource = ReturnMergedTable(dtFirstWeek, dtSecondWeek)
        grdPayrollTotal.DataBind()

        grdPayrollTotal.HeaderRow.Cells(0).Text = "Employee"
        grdPayrollTotal.HeaderRow.Cells(1).Text = "Total Hours"
        grdPayrollTotal.HeaderRow.Cells(2).Text = "Clerical Hours"
        grdPayrollTotal.HeaderRow.Cells(3).Text = "Mktg. Hours"
        grdPayrollTotal.HeaderRow.Cells(4).Text = "Mktg. Round"
        grdPayrollTotal.HeaderRow.Cells(5).Text = "Truck Hours"
        grdPayrollTotal.HeaderRow.Cells(6).Text = "Truck Round"
        grdPayrollTotal.HeaderRow.Cells(7).Text = "OT Truck Hours"
        grdPayrollTotal.HeaderRow.Cells(8).Text = "Base Pay Rate"
        grdPayrollTotal.HeaderRow.Cells(9).Text = "Base Pay"
        grdPayrollTotal.HeaderRow.Cells(10).Text = "OT Pay Rate"
        grdPayrollTotal.HeaderRow.Cells(11).Text = "OT Pay"
        grdPayrollTotal.HeaderRow.Cells(12).Text = "Bonus"
        grdPayrollTotal.HeaderRow.Cells(13).Text = "Profit Share"
        grdPayrollTotal.HeaderRow.Cells(14).Text = "Total Bonus"
        grdPayrollTotal.HeaderRow.Cells(15).Text = "Total Pay"
        grdPayrollTotal.HeaderRow.Cells(16).Text = "Adjustments"
        grdPayrollTotal.HeaderRow.Cells(17).Text = "Net Pay"
        grdPayrollTotal.HeaderRow.Cells(18).Text = "Hourly Avg"

        grdPayrollTotal.FooterRow.Cells(1).Text = dOverallTotalHours.ToString("F")
        grdPayrollTotal.FooterRow.Cells(2).Text = dOverallTotalClerical.ToString("F")
        grdPayrollTotal.FooterRow.Cells(3).Text = dOverallTotalMktg.ToString("F")
        grdPayrollTotal.FooterRow.Cells(4).Text = dOverallMktgRound.ToString("F")
        grdPayrollTotal.FooterRow.Cells(5).Text = dOverallTotalTruck.ToString("F")
        grdPayrollTotal.FooterRow.Cells(6).Text = (dOverallTotalHours - dOverallTotalClerical - dOverallMktgRound - dOverallTotalOT).ToString("F")
        grdPayrollTotal.FooterRow.Cells(7).Text = dOverallTotalOT.ToString("F")
        grdPayrollTotal.FooterRow.Cells(9).Text = dOverallTotalBasePay.ToString("F")
        grdPayrollTotal.FooterRow.Cells(11).Text = dOverallTotalOTPay.ToString("F")
        grdPayrollTotal.FooterRow.Cells(12).Text = dOverallTotalBonus.ToString("F")
        grdPayrollTotal.FooterRow.Cells(13).Text = dOverallTotalPS.ToString("F")
        grdPayrollTotal.FooterRow.Cells(14).Text = (dOverallTotalBonus + dOverallTotalPS).ToString("F")
        grdPayrollTotal.FooterRow.Cells(15).Text = (dOverallTotalBasePay + dOverallTotalOTPay + dOverallTotalBonus + dOverallTotalPS).ToString("F")
        grdPayrollTotal.FooterRow.Cells(16).Text = (dOverallTotalAdj).ToString("F")
        grdPayrollTotal.FooterRow.Cells(17).Text = (dOverallTotalNet).ToString("F")


    End Sub

    Private Sub LoadPayrollDatesReport()
        Dim db As New Connection("Consumer_DSN")

        Dim cmd As New SqlCommand

        Dim dtFirstWeek As New DataTable
        dtFirstWeek.Columns.Add("employee")
        dtFirstWeek.Columns.Add("totalHours")
        dtFirstWeek.Columns.Add("clericalHours")
        dtFirstWeek.Columns.Add("mktgHours")
        dtFirstWeek.Columns.Add("truckHours")
        dtFirstWeek.Columns.Add("otTruckHours")
        dtFirstWeek.Columns.Add("basePayRate")
        dtFirstWeek.Columns.Add("basePay")
        dtFirstWeek.Columns.Add("otPayRate")
        dtFirstWeek.Columns.Add("otPay")
        dtFirstWeek.Columns.Add("bonus")
        dtFirstWeek.Columns.Add("profitShare")
        dtFirstWeek.Columns.Add("totalBonus")
        dtFirstWeek.Columns.Add("totalPay")
        dtFirstWeek.Columns.Add("adjustments")
        dtFirstWeek.Columns.Add("netPay")
        dtFirstWeek.Columns.Add("hourlyAvg")

        Dim dr As DataRow

        cmd = New SqlCommand
        cmd.CommandText = "spPayrollSummaryGetEmployees"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@startDate", StartDate)
        cmd.Parameters.AddWithValue("@endDate", EndDate)

        Dim dBasePayRate As Decimal = 0
        Dim dTrkHours As Decimal = 0
        Dim dOTHours As Decimal = 0
        Dim dNonTrkHours As Decimal = 0
        Dim dBasePay As Decimal = 0
        Dim dOTPay As Decimal = 0
        Dim dPSPay As Decimal = 0
        Dim dClericalHours As Decimal = 0
        Dim dMktgHours As Decimal = 0
        Dim dBonus As Decimal = 0
        Dim dAdjustment As Decimal = 0

        Dim dTotalHours As Decimal = 0
        Dim dTotalClerical As Decimal = 0
        Dim dTotalMktg As Decimal = 0
        Dim dTotalTruck As Decimal = 0
        Dim dTotalOT As Decimal = 0
        Dim dTotalBasePay As Decimal = 0
        Dim dTotalOTPay As Decimal = 0
        Dim dTotalBonus As Decimal = 0
        Dim dTotalPS As Decimal = 0
        Dim dTotalAdj As Decimal = 0
        Dim dTotalNet As Decimal = 0

        Dim dOverallTotalHours As Decimal = 0
        Dim dOverallTotalClerical As Decimal = 0
        Dim dOverallTotalMktg As Decimal = 0
        Dim dOverallTotalTruck As Decimal = 0
        Dim dOverallTotalOT As Decimal = 0
        Dim dOverallTotalBasePay As Decimal = 0
        Dim dOverallTotalOTPay As Decimal = 0
        Dim dOverallTotalBonus As Decimal = 0
        Dim dOverallTotalPS As Decimal = 0
        Dim dOverallTotalAdj As Decimal = 0
        Dim dOverallTotalNet As Decimal = 0

        Dim dtEmpsWithHours As New DataTable
        dtEmpsWithHours = db.FillWithSP(cmd)
        For Each dtewhr As DataRow In dtEmpsWithHours.Rows
            If dtewhr.Item("payrollSummary").ToString.Trim.ToLower = "y" Then
                dBasePay = 0
                dOTPay = 0
                dPSPay = 0
                dClericalHours = 0
                dMktgHours = 0
                dBonus = 0
                dAdjustment = 0

                dr = dtFirstWeek.NewRow

                cmd = New SqlCommand
                cmd.CommandText = "spGetPayrollAdjustment"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@userID", dtewhr.Item("userID"))
                Dim xy As DataTable = db.FillWithSP(cmd)

                For Each xrow As DataRow In xy.Rows
                    If xrow("adjDate") >= StartDate And xrow("adjDate") <= EndDate Then
                        If xrow("adjAmount") >= 0 Then
                            dBonus += xrow("adjAmount")
                        Else
                            dAdjustment += xrow("adjAmount")
                        End If
                    End If
                Next


                'we now have employees with wages for this period
                dr("employee") = dtewhr.Item("firstName") & " " & dtewhr.Item("lastName")

                cmd = New SqlCommand
                cmd.CommandText = "spPayrollSummaryGetWageSummary"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@userID", dtewhr.Item("userID"))
                cmd.Parameters.AddWithValue("@startDate", StartDate)
                cmd.Parameters.AddWithValue("@endDate", EndDate)

                xy = db.FillWithSP(cmd)

                If xy.Rows.Count > 0 Then
                    dTrkHours = xy.Rows(0)("truckHours")
                    dNonTrkHours = xy.Rows(0)("nonTruckHours")
                    dClericalHours = xy.Rows(0)("routeCount") * 0.5
                    dPSPay = xy.Rows(0)("profitShare")
                    dMktgHours = ReturnMarketingHours(dtewhr.Item("userID"), StartDate, EndDate)

                    dBasePayRate = (xy.Rows(0)("nonTruckWages") + xy.Rows(0)("truckWages")) / xy.Rows(0)("totalHours")
                    If (dTrkHours + dNonTrkHours) > 40 Then
                        dr("basePay") = ((dTrkHours + dNonTrkHours) * dBasePayRate).ToString("F")
                        dBasePay = (dTrkHours + dNonTrkHours) * dBasePayRate

                        dr("otTruckHours") = (dTrkHours + dNonTrkHours) - 40
                        dOTHours = (dTrkHours + dNonTrkHours) - 40
                        dr("otPayRate") = (dBasePayRate * 0.5).ToString("F")
                        dr("otPay") = (((dTrkHours + dNonTrkHours) - 40) * (dBasePayRate * 0.5)).ToString("F")
                        dOTPay = ((dTrkHours + dNonTrkHours) - 40) * (dBasePayRate * 0.5)

                    Else
                        dr("basePay") = xy.Rows(0)("nonTruckWages") + xy.Rows(0)("truckWages")
                        dBasePay = xy.Rows(0)("nonTruckWages") + xy.Rows(0)("truckWages")

                        dr("otTruckHours") = "0.00"
                        dOTHours = 0
                        dr("otPayRate") = "0.00"
                        dr("otPay") = "0.00"
                        dOTPay = 0
                    End If

                    dr("totalHours") = (dTrkHours + dNonTrkHours).ToString("F")
                    dr("clericalHours") = dClericalHours.ToString("F")
                    dr("mktgHours") = dMktgHours.ToString("F")
                    dr("truckHours") = ((dTrkHours + dNonTrkHours) - dClericalHours - dMktgHours - dOTHours).ToString("F")
                    dr("basePayRate") = dBasePayRate.ToString("F")
                    dr("profitShare") = dPSPay.ToString("F")
                    dr("bonus") = dBonus.ToString("F")
                    dr("totalBonus") = (dBonus + dPSPay).ToString("F")
                    dr("adjustments") = dAdjustment.ToString("F")
                    dr("totalPay") = (dBasePay + dOTPay + dPSPay + dBonus).ToString("F")
                    dr("netPay") = (dBasePay + dOTPay + dPSPay + dBonus + dAdjustment).ToString("F")
                    dr("hourlyAvg") = ((dBasePay + dOTPay + dPSPay + dBonus + dAdjustment) / (dTrkHours + dNonTrkHours)).ToString("F")

                End If

                'add together the totals for each column
                dTotalHours += dTrkHours + dNonTrkHours
                dTotalClerical += dClericalHours
                dTotalMktg += dMktgHours
                dTotalTruck += (dTrkHours + dNonTrkHours) - dClericalHours - dMktgHours - dOTHours
                dTotalOT += dr("otTruckHours")
                dTotalBasePay += dBasePay
                dTotalOTPay += dOTPay
                dTotalBonus += dBonus
                dTotalPS += dPSPay
                dTotalAdj += dAdjustment
                dTotalNet += dBasePay + dOTPay + dPSPay + dBonus + dAdjustment

                dtFirstWeek.Rows.Add(dr)
            End If
        Next


        grdPayrollDates.DataSource = dtFirstWeek
        grdPayrollDates.DataBind()

        grdPayrollDates.HeaderRow.Cells(0).Text = "Employee"
        grdPayrollDates.HeaderRow.Cells(1).Text = "Total Hours"
        grdPayrollDates.HeaderRow.Cells(2).Text = "Clerical Hours"
        grdPayrollDates.HeaderRow.Cells(3).Text = "Mktg. Hours"
        grdPayrollDates.HeaderRow.Cells(4).Text = "Truck Hours"
        grdPayrollDates.HeaderRow.Cells(5).Text = "OT Truck Hours"
        grdPayrollDates.HeaderRow.Cells(6).Text = "Base Pay Rate"
        grdPayrollDates.HeaderRow.Cells(7).Text = "Base Pay"
        grdPayrollDates.HeaderRow.Cells(8).Text = "OT Pay Rate"
        grdPayrollDates.HeaderRow.Cells(9).Text = "OT Pay"
        grdPayrollDates.HeaderRow.Cells(10).Text = "Bonus"
        grdPayrollDates.HeaderRow.Cells(11).Text = "Profit Share"
        grdPayrollDates.HeaderRow.Cells(12).Text = "Total Bonus"
        grdPayrollDates.HeaderRow.Cells(13).Text = "Total Pay"
        grdPayrollDates.HeaderRow.Cells(14).Text = "Adjustments"
        grdPayrollDates.HeaderRow.Cells(15).Text = "Net Pay"
        grdPayrollDates.HeaderRow.Cells(16).Text = "Hourly Avg"

        grdPayrollDates.FooterRow.Cells(1).Text = dTotalHours.ToString("F")
        grdPayrollDates.FooterRow.Cells(2).Text = dTotalClerical.ToString("F")
        grdPayrollDates.FooterRow.Cells(3).Text = dTotalMktg.ToString("F")
        grdPayrollDates.FooterRow.Cells(4).Text = dTotalTruck.ToString("F")
        grdPayrollDates.FooterRow.Cells(5).Text = dTotalOT.ToString("F")
        grdPayrollDates.FooterRow.Cells(7).Text = dTotalBasePay.ToString("F")
        grdPayrollDates.FooterRow.Cells(9).Text = dTotalOTPay.ToString("F")
        grdPayrollDates.FooterRow.Cells(10).Text = dTotalBonus.ToString("F")
        grdPayrollDates.FooterRow.Cells(11).Text = dTotalPS.ToString("F")
        grdPayrollDates.FooterRow.Cells(12).Text = (dTotalBonus + dTotalPS).ToString("F")
        grdPayrollDates.FooterRow.Cells(13).Text = (dTotalBasePay + dTotalOTPay + dTotalBonus + dTotalPS).ToString("F")
        grdPayrollDates.FooterRow.Cells(14).Text = (dTotalAdj).ToString("F")
        grdPayrollDates.FooterRow.Cells(15).Text = (dTotalNet).ToString("F")


    End Sub

    Private Sub LoadRPHReport()

        TruckHoursMin = ReturnTruckHoursMin()

        Dim db As New Connection("Consumer_DSN")

        Dim dHoursMTD As Decimal = 0
        Dim dJobRevenue As Decimal = 0
        Dim dJustJobRevenue As Decimal = 0

        Dim dTotalMetRevMTD As Decimal = 0
        Dim dTotalMetRevMktgMTD As Decimal = 0
        Dim dTotalMetHoursMTD As Decimal = 0
        Dim dTotalMetRevYTD As Decimal = 0
        Dim dTotalMetRevMktgYTD As Decimal = 0
        Dim dTotalMetHoursYTD As Decimal = 0

        Dim dTotalNotMetRevMTD As Decimal = 0
        Dim dTotalNotMetRevMktgMTD As Decimal = 0
        Dim dTotalNotMetHoursMTD As Decimal = 0
        Dim dTotalNotMetRevYTD As Decimal = 0
        Dim dTotalNotMetRevMktgYTD As Decimal = 0
        Dim dTotalNotMetHoursYTD As Decimal = 0

        'clear userInfo truck hours as we'll be setting it for all current Users upcoming....
        Dim sSQL As String
        db.ExecuteScaler("update userInfo set currentTruckHours = 0, ytdTruckHours = 0")

        Dim dt As New DataTable
        dt.Columns.Add("employee")
        dt.Columns.Add("revMTD", Type.GetType("System.Double"))
        dt.Columns.Add("revMktgMTD", Type.GetType("System.Double"))
        dt.Columns.Add("hoursMTD", Type.GetType("System.Double"))
        dt.Columns.Add("rphMTD", Type.GetType("System.Double"))
        dt.Columns.Add("rphMktgMTD", Type.GetType("System.Double"))

        dt.Columns.Add("revYTD", Type.GetType("System.Double"))
        dt.Columns.Add("revMktgYTD", Type.GetType("System.Double"))
        dt.Columns.Add("hoursYTD", Type.GetType("System.Double"))
        dt.Columns.Add("rphYTD", Type.GetType("System.Double"))
        dt.Columns.Add("rphMktgYTD", Type.GetType("System.Double"))

        Dim dtNotMet As New DataTable
        dtNotMet.Columns.Add("employee")
        dtNotMet.Columns.Add("revMTD", Type.GetType("System.Double"))
        dtNotMet.Columns.Add("revMktgMTD", Type.GetType("System.Double"))
        dtNotMet.Columns.Add("hoursMTD", Type.GetType("System.Double"))
        dtNotMet.Columns.Add("rphMTD", Type.GetType("System.Double"))
        dtNotMet.Columns.Add("rphMktgMTD", Type.GetType("System.Double"))

        dtNotMet.Columns.Add("revYTD", Type.GetType("System.Double"))
        dtNotMet.Columns.Add("revMktgYTD", Type.GetType("System.Double"))
        dtNotMet.Columns.Add("hoursYTD", Type.GetType("System.Double"))
        dtNotMet.Columns.Add("rphYTD", Type.GetType("System.Double"))
        dtNotMet.Columns.Add("rphMktgYTD", Type.GetType("System.Double"))

        Dim dr As DataRow

        Dim cmd As New SqlCommand
        cmd.CommandText = "spPayrollSummaryGetEmployees"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@startDate", Request.QueryString("startDate"))
        cmd.Parameters.AddWithValue("@endDate", Request.QueryString("endDate"))

        Dim dtEmpsWithHours As New DataTable
        dtEmpsWithHours = db.FillWithSP(cmd)
        For Each dtewhr As DataRow In dtEmpsWithHours.Rows
            cmd = New SqlCommand
            cmd.CommandText = "spReport_GetRevenueByEmployee"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@startDate", Request.QueryString("startDate"))
            cmd.Parameters.AddWithValue("@endDate", Request.QueryString("endDate"))
            cmd.Parameters.AddWithValue("@userID", dtewhr("userID"))

            Dim sdrRevenue As SqlDataReader = db.ExecuteReader(cmd)
            If sdrRevenue.HasRows Then

                dHoursMTD = 0
                dJobRevenue = 0
                dJustJobRevenue = 0

                While sdrRevenue.Read
                    dJobRevenue = sdrRevenue("jobRevenue")
                    dJustJobRevenue = sdrRevenue("justJobRevenue")
                End While


                cmd = New SqlCommand
                cmd.CommandText = "spPayrollSummaryGetWageSummary"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@startDate", Request.QueryString("startDate"))
                cmd.Parameters.AddWithValue("@endDate", Request.QueryString("endDate"))
                cmd.Parameters.AddWithValue("@userID", dtewhr("userID"))

                Dim sdrWorkHours As SqlDataReader = db.ExecuteReader(cmd)
                If sdrWorkHours.HasRows Then
                    While sdrWorkHours.Read
                        dHoursMTD = sdrWorkHours("truckHours")
                    End While
                End If


                If dHoursMTD >= TruckHoursMin Then
                    dr = dt.NewRow
                Else
                    dr = dtNotMet.NewRow
                End If


                dr("employee") = dtewhr("firstName") & " " & dtewhr("lastName")
                dr("hoursMTD") = dHoursMTD
                dr("revMktgMTD") = dJobRevenue
                dr("revMTD") = dJustJobRevenue

                If dHoursMTD = 0 Then
                    dr("rphMTD") = 0
                    dr("rphMktgMTD") = 0
                Else
                    dr("rphMTD") = (dr("revMTD") / dr("hoursMTD")) * 0.5
                    dr("rphMktgMTD") = (dr("revMktgMTD") / dr("hoursMTD")) * 0.5
                End If


                If dHoursMTD >= TruckHoursMin Then
                    dTotalMetRevMTD += dr("revMTD")
                    dTotalMetRevMktgMTD += dr("revMktgMTD")
                    dTotalMetHoursMTD += dHoursMTD
                Else
                    dTotalNotMetRevMTD += dr("revMTD")
                    dTotalNotMetRevMktgMTD += dr("revMktgMTD")
                    dTotalNotMetHoursMTD += dHoursMTD
                End If

                If Year(Request.QueryString("startDate")) = Year(Request.QueryString("endDate")) Then
                    '***** YTD
                    cmd = New SqlCommand
                    cmd.CommandText = "spReport_GetRevenueByEmployee"
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@startDate", "1/1/" & Year(Request.QueryString("endDate")))
                    cmd.Parameters.AddWithValue("@endDate", Request.QueryString("endDate"))
                    cmd.Parameters.AddWithValue("@userID", dtewhr("userID"))

                    Dim sdrYTDRevenue As SqlDataReader = db.ExecuteReader(cmd)
                    If sdrYTDRevenue.HasRows Then
                        While sdrYTDRevenue.Read
                            dr("revMktgYTD") = sdrYTDRevenue("jobRevenue")
                            dr("revYTD") = sdrYTDRevenue("justJobRevenue")
                        End While
                        If dHoursMTD >= TruckHoursMin Then
                            dTotalMetRevYTD += dr("revYTD")
                            dTotalMetRevMktgYTD += dr("revMktgYTD")
                        Else
                            dTotalNotMetRevYTD += dr("revYTD")
                            dTotalNotMetRevMktgYTD += dr("revMktgYTD")
                        End If

                        cmd = New SqlCommand
                        cmd.CommandText = "spPayrollSummaryGetWageSummary"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@startDate", "1/1/" & Year(Request.QueryString("endDate")))
                        cmd.Parameters.AddWithValue("@endDate", Request.QueryString("endDate"))
                        cmd.Parameters.AddWithValue("@userID", dtewhr("userID"))

                        Dim sdrYTDWorkHours As SqlDataReader = db.ExecuteReader(cmd)
                        If sdrYTDWorkHours.HasRows Then
                            While sdrYTDWorkHours.Read
                                dr("hoursYTD") = sdrYTDWorkHours("truckHours")
                            End While
                        End If
                        If dr("hoursYTD") = 0 Then
                            dr("rphYTD") = 0
                            dr("rphMktgYTD") = 0
                        Else
                            dr("rphYTD") = (dr("revYTD") / dr("hoursYTD")) * 0.5
                            dr("rphMktgYTD") = (dr("revMktgYTD") / dr("hoursYTD")) * 0.5
                        End If

                        If dHoursMTD >= TruckHoursMin Then
                            dTotalMetHoursYTD += dr("hoursYTD")
                        Else
                            dTotalNotMetHoursYTD += dr("hoursYTD")
                        End If

                    End If

                Else
                    dr("revYTD") = 0
                    dr("revMktgYTD") = 0
                    dr("hoursYTD") = 0
                    dr("rphYTD") = 0
                    dr("rphMktgYTD") = 0
                End If

                '** update employee RPH
                db.ExecuteScaler("update userInfo set currentRPH = " & dr("rphMTD") & ", ytdRPH = " & dr("rphYTD") & " where userinfo.id = '" & dtewhr("userID") & "'")

                '** update truck hours - MTD
                db.ExecuteScaler("update userInfo set currentTruckHours = " & dr("hoursMTD") & " where userinfo.id = '" & dtewhr("userID") & "'")

                '** update truck hours - YTD
                db.ExecuteScaler("update userInfo set ytdTruckHours = " & dr("hoursYTD") & " where userinfo.id = '" & dtewhr("userID") & "'")

                If dHoursMTD >= TruckHoursMin Then
                    dt.Rows.Add(dr)
                Else
                    dtNotMet.Rows.Add(dr)
                End If
            Else

            End If

        Next

        Dim sort As String = Request.QueryString("sort")
        If String.IsNullOrEmpty(sort) Then
            sort = "rphMTD"
        End If
        Dim dv As DataView = New DataView(dt)
        If sort.Contains("employee") Then
            dv.Sort = sort & " ASC"
        Else
            dv.Sort = sort & " DESC"
        End If


        grdRPH.DataSource = dv
        grdRPH.DataBind()

        grdRPH.HeaderRow.Cells(0).Text = "<a href='/ReportsView.aspx?rpt=rph&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=employee'>Employee</a>"
        grdRPH.HeaderRow.Cells(1).Text = "<a href='/ReportsView.aspx?rpt=rph&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=revMTD'>Revenue</a>"
        grdRPH.HeaderRow.Cells(2).Text = "<a href='/ReportsView.aspx?rpt=rph&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=revMktgMTD'>Revenue+Mktg</a>"
        grdRPH.HeaderRow.Cells(3).Text = "<a href='/ReportsView.aspx?rpt=rph&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=hoursMTD'>Truck Hours</a>"
        grdRPH.HeaderRow.Cells(4).Text = "<a href='/ReportsView.aspx?rpt=rph&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=rphMTD'>RPH</a>"
        grdRPH.HeaderRow.Cells(5).Text = "<a href='/ReportsView.aspx?rpt=rph&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=rphMTD'>R+M/PH</a>"

        grdRPH.HeaderRow.Cells(6).Text = "<a href='/ReportsView.aspx?rpt=rph&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=revYTD'>Revenue YTD</a>"
        grdRPH.HeaderRow.Cells(7).Text = "<a href='/ReportsView.aspx?rpt=rph&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=revYTD'>Revenue+Mktg YTD</a>"
        grdRPH.HeaderRow.Cells(8).Text = "<a href='/ReportsView.aspx?rpt=rph&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=hoursYTD'>Truck Hours YTD</a>"
        grdRPH.HeaderRow.Cells(9).Text = "<a href='/ReportsView.aspx?rpt=rph&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=rphYTD'>RPH YTD</a>"
        grdRPH.HeaderRow.Cells(10).Text = "<a href='/ReportsView.aspx?rpt=rph&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=rphYTD'>R+M/PH YTD</a>"

        grdRPH.FooterRow.Cells(1).Text = dTotalMetRevMTD.ToString("F")
        grdRPH.FooterRow.Cells(2).Text = dTotalMetRevMktgMTD.ToString("F")
        grdRPH.FooterRow.Cells(3).Text = dTotalMetHoursMTD.ToString("F")
        grdRPH.FooterRow.Cells(4).Text = (dTotalMetRevMTD / dTotalMetHoursMTD).ToString("F")
        grdRPH.FooterRow.Cells(5).Text = (dTotalMetRevMktgMTD / dTotalMetHoursMTD).ToString("F")

        If Year(Request.QueryString("startDate")) = Year(Request.QueryString("endDate")) Then
            grdRPH.FooterRow.Cells(6).Text = dTotalMetRevYTD.ToString("F")
            grdRPH.FooterRow.Cells(7).Text = dTotalMetRevMktgYTD.ToString("F")
            grdRPH.FooterRow.Cells(8).Text = dTotalMetHoursYTD.ToString("F")
            grdRPH.FooterRow.Cells(9).Text = (dTotalMetRevYTD / dTotalMetHoursYTD).ToString("F")
            grdRPH.FooterRow.Cells(10).Text = (dTotalMetRevMktgYTD / dTotalMetHoursYTD).ToString("F")
        Else
            grdRPH.FooterRow.Cells(6).Text = 0
            grdRPH.FooterRow.Cells(7).Text = 0
            grdRPH.FooterRow.Cells(8).Text = 0
            grdRPH.FooterRow.Cells(9).Text = 0
            grdRPH.FooterRow.Cells(10).Text = 0
        End If




        Dim dvNotMet As DataView = New DataView(dtNotMet)
        If sort.Contains("employee") Then
            dvNotMet.Sort = sort & " ASC"
        Else
            dvNotMet.Sort = sort & " DESC"
        End If

        grdRPHNotMet.DataSource = dvNotMet
        grdRPHNotMet.DataBind()

        grdRPHNotMet.HeaderRow.Cells(0).Text = "<a href='/ReportsView.aspx?rpt=rph&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=employee'>Employee</a>"
        grdRPHNotMet.HeaderRow.Cells(1).Text = "<a href='/ReportsView.aspx?rpt=rph&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=revMTD'>Revenue</a>"
        grdRPHNotMet.HeaderRow.Cells(2).Text = "<a href='/ReportsView.aspx?rpt=rph&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=revMTD'>Revenue+Mktg</a>"
        grdRPHNotMet.HeaderRow.Cells(3).Text = "<a href='/ReportsView.aspx?rpt=rph&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=hoursMTD'>Truck Hours</a>"
        grdRPHNotMet.HeaderRow.Cells(4).Text = "<a href='/ReportsView.aspx?rpt=rph&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=rphMTD'>RPH</a>"
        grdRPHNotMet.HeaderRow.Cells(5).Text = "<a href='/ReportsView.aspx?rpt=rph&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=rphMTD'>R+M/PH</a>"

        grdRPHNotMet.HeaderRow.Cells(6).Text = "<a href='/ReportsView.aspx?rpt=rph&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=revYTD'>Revenue YTD</a>"
        grdRPHNotMet.HeaderRow.Cells(7).Text = "<a href='/ReportsView.aspx?rpt=rph&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=revYTD'>Revenue+Mktg YTD</a>"
        grdRPHNotMet.HeaderRow.Cells(8).Text = "<a href='/ReportsView.aspx?rpt=rph&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=hoursYTD'>Truck Hours YTD</a>"
        grdRPHNotMet.HeaderRow.Cells(9).Text = "<a href='/ReportsView.aspx?rpt=rph&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=rphYTD'>RPH YTD</a>"
        grdRPHNotMet.HeaderRow.Cells(10).Text = "<a href='/ReportsView.aspx?rpt=rph&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=rphYTD'>R+M/PH YTD</a>"

        grdRPHNotMet.FooterRow.Cells(1).Text = dTotalNotMetRevMTD.ToString("F")
        grdRPHNotMet.FooterRow.Cells(2).Text = dTotalNotMetRevMktgMTD.ToString("F")
        grdRPHNotMet.FooterRow.Cells(3).Text = dTotalNotMetHoursMTD.ToString("F")
        grdRPHNotMet.FooterRow.Cells(4).Text = (dTotalNotMetRevMTD / dTotalNotMetHoursMTD).ToString("F")
        grdRPHNotMet.FooterRow.Cells(5).Text = (dTotalNotMetRevMktgMTD / dTotalNotMetHoursMTD).ToString("F")

        If Year(Request.QueryString("startDate")) = Year(Request.QueryString("endDate")) Then
            grdRPHNotMet.FooterRow.Cells(6).Text = dTotalNotMetRevYTD.ToString("F")
            grdRPHNotMet.FooterRow.Cells(7).Text = dTotalNotMetRevMktgYTD.ToString("F")
            grdRPHNotMet.FooterRow.Cells(8).Text = dTotalNotMetHoursYTD.ToString("F")
            grdRPHNotMet.FooterRow.Cells(9).Text = (dTotalNotMetRevYTD / dTotalNotMetHoursYTD).ToString("F")
            grdRPHNotMet.FooterRow.Cells(10).Text = (dTotalNotMetRevMktgYTD / dTotalNotMetHoursYTD).ToString("F")
        Else
            grdRPHNotMet.FooterRow.Cells(6).Text = 0
            grdRPHNotMet.FooterRow.Cells(7).Text = 0
            grdRPHNotMet.FooterRow.Cells(8).Text = 0
            grdRPHNotMet.FooterRow.Cells(9).Text = 0
            grdRPHNotMet.FooterRow.Cells(10).Text = 0
        End If



    End Sub

    Private Sub LoadTopDogReport()

        Dim dt As New DataTable
        dt.Columns.Add("employee")
        dt.Columns.Add("npsMTD", Type.GetType("System.Double"))
        dt.Columns.Add("npsRank", Type.GetType("System.Double"))
        dt.Columns.Add("rphMTD", Type.GetType("System.Double"))
        dt.Columns.Add("rphRank", Type.GetType("System.Double"))
        dt.Columns.Add("signMTD", Type.GetType("System.Double"))
        dt.Columns.Add("signRank", Type.GetType("System.Double"))
        dt.Columns.Add("overallMTD", Type.GetType("System.Double"))
        dt.Columns.Add("overallRank", Type.GetType("System.Double"))

        Dim dr As DataRow
        Dim db As New Connection("Consumer_DSN")

        Dim cmd As New SqlCommand
        cmd.CommandText = "spReport_GetTopDog"
        cmd.CommandType = CommandType.StoredProcedure

        Dim dtEmpsWithHours As New DataTable
        dtEmpsWithHours = db.FillWithSP(cmd)
        For Each dtewhr As DataRow In dtEmpsWithHours.Rows
            dr = dt.NewRow
            dr("employee") = dtewhr("userName")
            dr("npsMTD") = dtewhr("nps")
            dr("npsRank") = dtewhr("npsRank")
            dr("rphMTD") = dtewhr("rph")
            dr("rphRank") = dtewhr("rphRank")
            dr("signMTD") = dtewhr("sign")
            dr("signRank") = dtewhr("signRank")
            dr("overallMTD") = dtewhr("overall")
            dr("overallRank") = dtewhr("overallRank")
            dt.Rows.Add(dr)
        Next

        Dim sort As String = Request.QueryString("sort")
        If String.IsNullOrEmpty(sort) Then
            sort = "npsMTD"
        End If
        Dim dv As DataView = New DataView(dt)
        If sort.ToLower.Contains("rank") Then
            dv.Sort = sort & " ASC"
        Else
            dv.Sort = sort & " DESC"
        End If

        With grdTopDog
            .DataSource = dv
            .DataBind()

            .HeaderRow.Cells(0).Text = "<a href='/ReportsView.aspx?rpt=topdog&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=employee'>Employee</a>"
            .HeaderRow.Cells(1).Text = "<a href='/ReportsView.aspx?rpt=topdog&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=npsMTD'>NPS MTD</a>"
            .HeaderRow.Cells(2).Text = "<a href='/ReportsView.aspx?rpt=topdog&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=npsRank'>NPS Rank</a>"
            .HeaderRow.Cells(3).Text = "<a href='/ReportsView.aspx?rpt=topdog&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=rphMTD'>RPH MTD</a>"
            .HeaderRow.Cells(4).Text = "<a href='/ReportsView.aspx?rpt=topdog&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=rphRank'>RPH Rank</a>"
            .HeaderRow.Cells(5).Text = "<a href='/ReportsView.aspx?rpt=topdog&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=signMTD'>Sign MTD</a>"
            .HeaderRow.Cells(6).Text = "<a href='/ReportsView.aspx?rpt=topdog&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=signRank'>Sign Rank</a>"
            .HeaderRow.Cells(7).Text = "<a href='/ReportsView.aspx?rpt=topdog&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=overallMTD'>Overall</a>"
            .HeaderRow.Cells(8).Text = "<a href='/ReportsView.aspx?rpt=topdog&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=overallRank'>Overall Rank</a>"
        End With

    End Sub

    Private Sub LoadTopDogReportV2()

        Dim dt As New DataTable
        dt.Columns.Add("employee")
        dt.Columns.Add("rphMTD", Type.GetType("System.Double"))
        dt.Columns.Add("rphRank", Type.GetType("System.Double"))
        dt.Columns.Add("ajsMTD", Type.GetType("System.Double"))
        dt.Columns.Add("ajsRank", Type.GetType("System.Double"))
        dt.Columns.Add("npsMTD", Type.GetType("System.Double"))
        dt.Columns.Add("npsRank", Type.GetType("System.Double"))
        dt.Columns.Add("oscMTD", Type.GetType("System.Double"))
        dt.Columns.Add("oscRank", Type.GetType("System.Double"))
        dt.Columns.Add("overallMTD", Type.GetType("System.Double"))
        dt.Columns.Add("overallRank", Type.GetType("System.Double"))

        Dim dr As DataRow
        Dim db As New Connection("Consumer_DSN")

        Dim cmd As New SqlCommand
        cmd.CommandText = "spReport_GetTopDogV2"
        cmd.CommandType = CommandType.StoredProcedure

        Dim dtEmpsWithHours As New DataTable
        dtEmpsWithHours = db.FillWithSP(cmd)
        For Each dtewhr As DataRow In dtEmpsWithHours.Rows
            dr = dt.NewRow
            dr("employee") = dtewhr("userName")
            dr("rphMTD") = dtewhr("rph")
            dr("rphRank") = dtewhr("rphRank")
            dr("ajsMTD") = dtewhr("ajs")
            dr("ajsRank") = dtewhr("ajsRank")
            dr("npsMTD") = dtewhr("nps")
            dr("npsRank") = dtewhr("npsRank")
            dr("oscMTD") = dtewhr("osc")
            dr("oscRank") = dtewhr("oscRank")
            dr("overallMTD") = dtewhr("overall")
            dr("overallRank") = dtewhr("overallRank")
            dt.Rows.Add(dr)
        Next

        Dim sort As String = Request.QueryString("sort")
        If String.IsNullOrEmpty(sort) Then
            sort = "npsMTD"
        End If
        Dim dv As DataView = New DataView(dt)
        If sort.ToLower.Contains("rank") Then
            dv.Sort = sort & " ASC"
        Else
            dv.Sort = sort & " DESC"
        End If

        With grdViewTopDogV2
            .DataSource = dv
            .DataBind()

            .HeaderRow.Cells(0).Text = "<a href='/ReportsView.aspx?rpt=topdogv2&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=employee'>Employee</a>"
            .HeaderRow.Cells(1).Text = "<a href='/ReportsView.aspx?rpt=topdogv2&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=rphMTD'>RPH MTD</a>"
            .HeaderRow.Cells(2).Text = "<a href='/ReportsView.aspx?rpt=topdogv2&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=rphRank'>RPH Rank</a>"
            .HeaderRow.Cells(3).Text = "<a href='/ReportsView.aspx?rpt=topdogv2&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=ajsMTD'>AJS MTD</a>"
            .HeaderRow.Cells(4).Text = "<a href='/ReportsView.aspx?rpt=topdogv2&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=ajsRank'>AJS Rank</a>"
            .HeaderRow.Cells(5).Text = "<a href='/ReportsView.aspx?rpt=topdogv2&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=npsMTD'>NPS MTD</a>"
            .HeaderRow.Cells(6).Text = "<a href='/ReportsView.aspx?rpt=topdogv2&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=npsRank'>NPS Rank</a>"
            .HeaderRow.Cells(7).Text = "<a href='/ReportsView.aspx?rpt=topdogv2&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=oscMTD'>OSC MTD</a>"
            .HeaderRow.Cells(8).Text = "<a href='/ReportsView.aspx?rpt=topdogv2&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=oscRank'>OSC Rank</a>"
            .HeaderRow.Cells(9).Text = "<a href='/ReportsView.aspx?rpt=topdogv2&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=overallMTD'>Overall</a>"
            .HeaderRow.Cells(10).Text = "<a href='/ReportsView.aspx?rpt=topdogv2&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=overallRank'>Overall Rank</a>"
        End With

    End Sub

    Private Sub LoadTopDogReportV3()

        Dim dt As New DataTable
        dt.Columns.Add("employee")
        dt.Columns.Add("rphMTD", Type.GetType("System.Double"))
        dt.Columns.Add("rphRank", Type.GetType("System.Double"))
        dt.Columns.Add("ajsMTD", Type.GetType("System.Double"))
        dt.Columns.Add("ajsRank", Type.GetType("System.Double"))
        dt.Columns.Add("npsMTD", Type.GetType("System.Double"))
        dt.Columns.Add("npsRank", Type.GetType("System.Double"))
        dt.Columns.Add("overallMTD", Type.GetType("System.Double"))
        dt.Columns.Add("overallRank", Type.GetType("System.Double"))

        Dim dr As DataRow
        Dim db As New Connection("Consumer_DSN")

        Dim cmd As New SqlCommand
        cmd.CommandText = "spReport_GetTopDogV2"
        cmd.CommandType = CommandType.StoredProcedure

        Dim dtEmpsWithHours As New DataTable
        dtEmpsWithHours = db.FillWithSP(cmd)
        For Each dtewhr As DataRow In dtEmpsWithHours.Rows
            dr = dt.NewRow
            dr("employee") = dtewhr("userName")
            dr("rphMTD") = dtewhr("rph")
            dr("rphRank") = dtewhr("rphRank")
            dr("ajsMTD") = dtewhr("ajs")
            dr("ajsRank") = dtewhr("ajsRank")
            dr("npsMTD") = dtewhr("nps")
            dr("npsRank") = dtewhr("npsRank")
            dr("overallMTD") = dtewhr("overall")
            dr("overallRank") = dtewhr("overallRank")
            dt.Rows.Add(dr)
        Next

        Dim sort As String = Request.QueryString("sort")
        If String.IsNullOrEmpty(sort) Then
            sort = "npsMTD"
        End If
        Dim dv As DataView = New DataView(dt)
        If sort.ToLower.Contains("rank") Then
            dv.Sort = sort & " ASC"
        Else
            dv.Sort = sort & " DESC"
        End If

        With grdTopDogV3
            .DataSource = dv
            .DataBind()

            .HeaderRow.Cells(0).Text = "<a href='/ReportsView.aspx?rpt=topdog&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=employee'>Employee</a>"
            .HeaderRow.Cells(1).Text = "<a href='/ReportsView.aspx?rpt=topdog&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=rphMTD'>RPH MTD</a>"
            .HeaderRow.Cells(2).Text = "<a href='/ReportsView.aspx?rpt=topdog&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=rphRank'>RPH Rank</a>"
            .HeaderRow.Cells(3).Text = "<a href='/ReportsView.aspx?rpt=topdog&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=ajsMTD'>AJS MTD</a>"
            .HeaderRow.Cells(4).Text = "<a href='/ReportsView.aspx?rpt=topdog&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=ajsRank'>AJS Rank</a>"
            .HeaderRow.Cells(5).Text = "<a href='/ReportsView.aspx?rpt=topdog&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=npsMTD'>NPS MTD</a>"
            .HeaderRow.Cells(6).Text = "<a href='/ReportsView.aspx?rpt=topdog&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=npsRank'>NPS Rank</a>"
            .HeaderRow.Cells(7).Text = "<a href='/ReportsView.aspx?rpt=topdog&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=overallMTD'>Overall</a>"
            .HeaderRow.Cells(8).Text = "<a href='/ReportsView.aspx?rpt=topdog&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=overallRank'>Overall Rank</a>"
        End With

    End Sub


    Private Sub LoadEmployeeMetricsReport()

        Dim dt As New DataTable
        dt.Columns.Add("employee")
        dt.Columns.Add("nps30", Type.GetType("System.Double"))
        dt.Columns.Add("npsRank30", Type.GetType("System.Double"))
        dt.Columns.Add("rph30", Type.GetType("System.Double"))
        dt.Columns.Add("rphRank30", Type.GetType("System.Double"))
        dt.Columns.Add("sign30", Type.GetType("System.Double"))
        dt.Columns.Add("signRank30", Type.GetType("System.Double"))
        dt.Columns.Add("overall30", Type.GetType("System.Double"))
        dt.Columns.Add("overallRank30", Type.GetType("System.Double"))
        dt.Columns.Add("nps90", Type.GetType("System.Double"))
        dt.Columns.Add("npsRank90", Type.GetType("System.Double"))
        dt.Columns.Add("rph90", Type.GetType("System.Double"))
        dt.Columns.Add("rphRank90", Type.GetType("System.Double"))
        dt.Columns.Add("sign90", Type.GetType("System.Double"))
        dt.Columns.Add("signRank90", Type.GetType("System.Double"))
        dt.Columns.Add("overall90", Type.GetType("System.Double"))
        dt.Columns.Add("overallRank90", Type.GetType("System.Double"))
        dt.Columns.Add("npsYTD", Type.GetType("System.Double"))
        dt.Columns.Add("npsRankYTD", Type.GetType("System.Double"))
        dt.Columns.Add("rphYTD", Type.GetType("System.Double"))
        dt.Columns.Add("rphRankYTD", Type.GetType("System.Double"))
        dt.Columns.Add("signYTD", Type.GetType("System.Double"))
        dt.Columns.Add("signRankYTD", Type.GetType("System.Double"))
        dt.Columns.Add("overallYTD", Type.GetType("System.Double"))
        dt.Columns.Add("overallRankYTD", Type.GetType("System.Double"))

        Dim dr As DataRow
        Dim db As New Connection("Consumer_DSN")

        Dim cmd As New SqlCommand
        cmd.CommandText = "spReport_GetEmployeeMetrics"
        cmd.CommandType = CommandType.StoredProcedure

        Dim dtEmpsWithHours As New DataTable
        dtEmpsWithHours = db.FillWithSP(cmd)
        For Each dtewhr As DataRow In dtEmpsWithHours.Rows
            dr = dt.NewRow
            dr("employee") = dtewhr("userName")
            dr("nps30") = dtewhr("nps30")
            dr("npsRank30") = dtewhr("npsRank30")
            dr("rph30") = dtewhr("rph30")
            dr("rphRank30") = dtewhr("rphRank30")
            dr("sign30") = dtewhr("sign30")
            dr("signRank30") = dtewhr("signRank30")
            dr("overall30") = dtewhr("overall30")
            dr("overallRank30") = dtewhr("overallRank30")
            dr("nps90") = dtewhr("nps90")
            dr("npsRank90") = dtewhr("npsRank90")
            dr("rph90") = dtewhr("rph90")
            dr("rphRank90") = dtewhr("rphRank90")
            dr("sign90") = dtewhr("sign90")
            dr("signRank90") = dtewhr("signRank90")
            dr("overall90") = dtewhr("overall90")
            dr("overallRank90") = dtewhr("overallRank90")
            dr("npsYTD") = dtewhr("npsYTD")
            dr("npsRankYTD") = dtewhr("npsRankYTD")
            dr("rphYTD") = dtewhr("rphYTD")
            dr("rphRankYTD") = dtewhr("rphRankYTD")
            dr("signYTD") = dtewhr("signYTD")
            dr("signRankYTD") = dtewhr("signRankYTD")
            dr("overallYTD") = dtewhr("overallYTD")
            dr("overallRankYTD") = dtewhr("overallRankYTD")
            dt.Rows.Add(dr)
        Next

        Dim sort As String = Request.QueryString("sort")
        If String.IsNullOrEmpty(sort) Then
            sort = "nps30"
        End If
        Dim dv As DataView = New DataView(dt)
        If sort.ToLower.Contains("rank") Then
            dv.Sort = sort & " ASC"
        Else
            dv.Sort = sort & " DESC"
        End If

        With grdEmployeeMetrics
            .DataSource = dv
            .DataBind()

            .HeaderRow.Cells(0).Text = "<a href='/ReportsView.aspx?rpt=metrics&startDate=" & Request.QueryString("startDate") & "&sort=employee'>Employee</a>"
            .HeaderRow.Cells(1).Text = "<a href='/ReportsView.aspx?rpt=metrics&startDate=" & Request.QueryString("startDate") & "&sort=nps30'>NPS 30</a>"
            .HeaderRow.Cells(2).Text = "<a href='/ReportsView.aspx?rpt=metrics&startDate=" & Request.QueryString("startDate") & "&sort=npsRank30'>NPS Rank 30</a>"
            .HeaderRow.Cells(3).Text = "<a href='/ReportsView.aspx?rpt=metrics&startDate=" & Request.QueryString("startDate") & "&sort=rph30'>RPH 30</a>"
            .HeaderRow.Cells(4).Text = "<a href='/ReportsView.aspx?rpt=metrics&startDate=" & Request.QueryString("startDate") & "&sort=rphRank30'>RPH Rank 30</a>"
            .HeaderRow.Cells(5).Text = "<a href='/ReportsView.aspx?rpt=metrics&startDate=" & Request.QueryString("startDate") & "&sort=sign30'>Sign 30</a>"
            .HeaderRow.Cells(6).Text = "<a href='/ReportsView.aspx?rpt=metrics&startDate=" & Request.QueryString("startDate") & "&sort=signRank30'>Sign Rank 30</a>"
            .HeaderRow.Cells(7).Text = "<a href='/ReportsView.aspx?rpt=metrics&startDate=" & Request.QueryString("startDate") & "&sort=overall30'>Overall 30</a>"
            .HeaderRow.Cells(8).Text = "<a href='/ReportsView.aspx?rpt=metrics&startDate=" & Request.QueryString("startDate") & "&sort=overallRank30'>Overall Rank 30</a>"
            .HeaderRow.Cells(9).Text = "<a href='/ReportsView.aspx?rpt=metrics&startDate=" & Request.QueryString("startDate") & "&sort=nps90'>NPS 90</a>"
            .HeaderRow.Cells(10).Text = "<a href='/ReportsView.aspx?rpt=metrics&startDate=" & Request.QueryString("startDate") & "&sort=npsRank90'>NPS Rank 90</a>"
            .HeaderRow.Cells(11).Text = "<a href='/ReportsView.aspx?rpt=metrics&startDate=" & Request.QueryString("startDate") & "&sort=rph90'>RPH 90</a>"
            .HeaderRow.Cells(12).Text = "<a href='/ReportsView.aspx?rpt=metrics&startDate=" & Request.QueryString("startDate") & "&sort=rphRank90'>RPH Rank 90</a>"
            .HeaderRow.Cells(13).Text = "<a href='/ReportsView.aspx?rpt=metrics&startDate=" & Request.QueryString("startDate") & "&sort=sign90'>Sign 90</a>"
            .HeaderRow.Cells(14).Text = "<a href='/ReportsView.aspx?rpt=metrics&startDate=" & Request.QueryString("startDate") & "&sort=signRank90'>Sign Rank 90</a>"
            .HeaderRow.Cells(15).Text = "<a href='/ReportsView.aspx?rpt=metrics&startDate=" & Request.QueryString("startDate") & "&sort=overall90'>Overall 90</a>"
            .HeaderRow.Cells(16).Text = "<a href='/ReportsView.aspx?rpt=metrics&startDate=" & Request.QueryString("startDate") & "&sort=overallRank90'>Overall Rank 90</a>"
            .HeaderRow.Cells(17).Text = "<a href='/ReportsView.aspx?rpt=metrics&startDate=" & Request.QueryString("startDate") & "&sort=npsYTD'>NPS YTD</a>"
            .HeaderRow.Cells(18).Text = "<a href='/ReportsView.aspx?rpt=metrics&startDate=" & Request.QueryString("startDate") & "&sort=npsRankYTD'>NPS Rank YTD</a>"
            .HeaderRow.Cells(19).Text = "<a href='/ReportsView.aspx?rpt=metrics&startDate=" & Request.QueryString("startDate") & "&sort=rphYTD'>RPH YTD</a>"
            .HeaderRow.Cells(20).Text = "<a href='/ReportsView.aspx?rpt=metrics&startDate=" & Request.QueryString("startDate") & "&sort=rphRankYTD'>RPH Rank YTD</a>"
            .HeaderRow.Cells(21).Text = "<a href='/ReportsView.aspx?rpt=metrics&startDate=" & Request.QueryString("startDate") & "&sort=signYTD'>Sign YTD</a>"
            .HeaderRow.Cells(22).Text = "<a href='/ReportsView.aspx?rpt=metrics&startDate=" & Request.QueryString("startDate") & "&sort=signRankYTD'>Sign Rank YTD</a>"
            .HeaderRow.Cells(23).Text = "<a href='/ReportsView.aspx?rpt=metrics&startDate=" & Request.QueryString("startDate") & "&sort=overallYTD'>Overall YTD</a>"
            .HeaderRow.Cells(24).Text = "<a href='/ReportsView.aspx?rpt=metrics&startDate=" & Request.QueryString("startDate") & "&sort=overallRankYTD'>Overall Rank YTD</a>"
        End With

    End Sub

    Private Sub LoadSignsReport()

        TruckHoursMin = ReturnTruckHoursMin()

        Dim dTotalMetMinJobsMTD As Double = 0
        Dim dTotalMetMinSignsMTD As Double = 0
        Dim dTotalMetMinJobsYTD As Double = 0
        Dim dTotalMetMinSignsYTD As Double = 0

        Dim dTotalNotMetMinJobsMTD As Double = 0
        Dim dTotalNotMetMinSignsMTD As Double = 0
        Dim dTotalNotMetMinJobsYTD As Double = 0
        Dim dTotalNotMetMinSignsYTD As Double = 0


        Dim dt As New DataTable
        dt.Columns.Add("employee")
        dt.Columns.Add("truckHours")
        dt.Columns.Add("jobsMTD")
        dt.Columns.Add("jobsYTD")
        dt.Columns.Add("signsMTD")
        dt.Columns.Add("signsYTD")
        dt.Columns.Add("signpercentageMTD")
        dt.Columns.Add("signpercentageYTD")

        Dim dtNoMin As New DataTable
        dtNoMin.Columns.Add("employee")
        dtNoMin.Columns.Add("truckHours")
        dtNoMin.Columns.Add("jobsMTD")
        dtNoMin.Columns.Add("jobsYTD")
        dtNoMin.Columns.Add("signsMTD")
        dtNoMin.Columns.Add("signsYTD")
        dtNoMin.Columns.Add("signpercentageMTD")
        dtNoMin.Columns.Add("signpercentageYTD")

        Dim dr As DataRow
        Dim db As New Connection("Consumer_DSN")

        Dim cmd As New SqlCommand
        Dim cmdYTD As New SqlCommand
        cmd.CommandText = "spGetUsers"
        cmd.CommandType = CommandType.StoredProcedure

        Dim dtEmps As New DataTable
        dtEmps = db.FillWithSP(cmd)
        For Each dtewhr As DataRow In dtEmps.Rows

            '***** MTD
            cmd = New SqlCommand
            cmd.CommandText = "spReport_GetSignPercentage"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@startDate", Request.QueryString("startDate"))
            cmd.Parameters.AddWithValue("@endDate", Request.QueryString("endDate"))
            cmd.Parameters.AddWithValue("@userID", dtewhr.Item("id"))

            '***** YTD
            cmdYTD = New SqlCommand
            cmdYTD.CommandText = "spReport_GetSignPercentage"
            cmdYTD.CommandType = CommandType.StoredProcedure
            cmdYTD.Parameters.AddWithValue("@startDate", "1/1/" & Year(Request.QueryString("endDate")))
            cmdYTD.Parameters.AddWithValue("@endDate", Request.QueryString("endDate"))
            cmdYTD.Parameters.AddWithValue("@userID", dtewhr.Item("id"))

            Dim dSignsTotal As Integer = 0
            Dim sSignsYes As Integer = 0
            Dim dSignsYTDTotal As Integer = 0
            Dim sSignsYTDYes As Integer = 0

            Dim dtSigns As New DataTable
            dtSigns = db.FillWithSP(cmd)
            For Each dts As DataRow In dtSigns.Rows
                dSignsTotal += 1
                If dts("sign").ToString.ToLower = "y" Then
                    sSignsYes += 1
                End If
            Next

            dtSigns = New DataTable
            dtSigns = db.FillWithSP(cmdYTD)
            For Each dts As DataRow In dtSigns.Rows
                dSignsYTDTotal += 1
                If dts("sign").ToString.ToLower = "y" Then
                    sSignsYTDYes += 1
                End If
            Next



            cmd = New SqlCommand
            cmd.CommandText = "spReport_GetTruckHours"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@startDate", Request.QueryString("startDate"))
            cmd.Parameters.AddWithValue("@endDate", Request.QueryString("endDate"))
            cmd.Parameters.AddWithValue("@userID", dtewhr.Item("id"))

            Dim dtTruckHours As New DataTable
            dtTruckHours = db.FillWithSP(cmd)
            For Each dtHour As DataRow In dtTruckHours.Rows
                If Not dtHour.IsNull("totalHours") AndAlso dtHour("totalHours") >= TruckHoursMin Then
                    dr = dt.NewRow
                    dr("employee") = dtewhr.Item("firstName") & " " & dtewhr.Item("lastName")
                    dr("truckHours") = dtHour("totalHours")
                    dr("jobsMTD") = dSignsTotal
                    dr("jobsYTD") = dSignsYTDTotal
                    dr("signsMTD") = sSignsYes
                    dr("signsYTD") = sSignsYTDYes

                    dTotalMetMinJobsMTD += dSignsTotal
                    dTotalMetMinSignsMTD += sSignsYes

                    dTotalMetMinJobsYTD += dSignsYTDTotal
                    dTotalMetMinSignsYTD += sSignsYTDYes

                    dr("signpercentageMTD") = (sSignsYes / dSignsTotal).ToString("P2")
                    dr("signpercentageYTD") = (sSignsYTDYes / dSignsYTDTotal).ToString("P2")

                    dt.Rows.Add(dr)

                    '** update employee Sign
                    cmd = New SqlCommand
                    cmd.CommandText = "spUpdateUserSign"
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@id", dtewhr("id"))
                    cmd.Parameters.AddWithValue("@sign", (sSignsYes / dSignsTotal) * 100)
                    If dSignsYTDTotal <= 0 Then
                        cmd.Parameters.AddWithValue("@ytdSign", 0)
                    Else
                        cmd.Parameters.AddWithValue("@ytdSign", (sSignsYTDYes / dSignsYTDTotal) * 100)
                    End If
                    db.ExecuteNonQuerySP(cmd)

                ElseIf Not dtHour.IsNull("totalHours") Then
                    dr = dtNoMin.NewRow
                    dr("employee") = dtewhr.Item("firstName") & " " & dtewhr.Item("lastName")
                    dr("truckHours") = dtHour("totalHours")
                    dr("jobsMTD") = dSignsTotal
                    dr("jobsYTD") = dSignsYTDTotal
                    dr("signsMTD") = sSignsYes
                    dr("signsYTD") = sSignsYTDYes
                    dr("signpercentageMTD") = (sSignsYes / dSignsTotal).ToString("P2")
                    dr("signpercentageYTD") = (sSignsYTDYes / dSignsYTDTotal).ToString("P2")


                    dTotalNotMetMinJobsMTD += dSignsTotal
                    dTotalNotMetMinSignsMTD += sSignsYes

                    dTotalNotMetMinJobsYTD += dSignsYTDTotal
                    dTotalNotMetMinSignsYTD += sSignsYTDYes

                    dtNoMin.Rows.Add(dr)

                    '** update employee Sign
                    cmd = New SqlCommand
                    cmd.CommandText = "spUpdateUserSign"
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@id", dtewhr("id"))
                    cmd.Parameters.AddWithValue("@sign", 0)
                    If dSignsYTDTotal <= 0 Then
                        cmd.Parameters.AddWithValue("@ytdSign", 0)
                    Else
                        cmd.Parameters.AddWithValue("@ytdSign", (sSignsYTDYes / dSignsYTDTotal) * 100)
                    End If
                    db.ExecuteNonQuerySP(cmd)
                End If




            Next

        Next

        If dt.Rows.Count = 0 Then
            dr = dt.NewRow
            dr("employee") = "No elegible employees ..."
            dr("truckHours") = ""
            dr("jobsMTD") = ""
            dr("jobsYTD") = ""
            dr("signsMTD") = ""
            dr("signsYTD") = ""

            dr("signpercentageMTD") = ""
            dr("signpercentageYTD") = ""

            dt.Rows.Add(dr)

        End If

        Dim sort As String = Request.QueryString("sort")
        If String.IsNullOrEmpty(sort) Then
            sort = "signpercentageMTD"
        End If
        Dim dv As DataView = New DataView(dt)
        dv.Sort = sort & " DESC"

        grdSigns.DataSource = dv
        grdSigns.DataBind()

        grdSigns.HeaderRow.Cells(0).Text = "<a href='/ReportsView.aspx?rpt=signs&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=employee'>Employee</a>"
        grdSigns.HeaderRow.Cells(1).Text = "Truck Hours"
        grdSigns.HeaderRow.Cells(2).Text = "<a href='/ReportsView.aspx?rpt=signs&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=jobsMTD'>Jobs</a>"
        grdSigns.HeaderRow.Cells(3).Text = "<a href='/ReportsView.aspx?rpt=signs&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=signsMTD'>Signs</a>"
        grdSigns.HeaderRow.Cells(4).Text = "<a href='/ReportsView.aspx?rpt=signs&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=signpercentageMTD'>Signs %</a>"
        grdSigns.HeaderRow.Cells(5).Text = "<a href='/ReportsView.aspx?rpt=signs&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=jobsYTD'>Jobs YTD</a>"
        grdSigns.HeaderRow.Cells(6).Text = "<a href='/ReportsView.aspx?rpt=signs&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=signsYTD'>Sign YTD</a>"
        grdSigns.HeaderRow.Cells(7).Text = "<a href='/ReportsView.aspx?rpt=signs&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=signpercentageYTD'>Sign % YTD</a>"

        grdSigns.FooterRow.Cells(2).Text = dTotalMetMinJobsMTD.ToString("F")
        grdSigns.FooterRow.Cells(3).Text = dTotalMetMinSignsMTD.ToString("F")
        grdSigns.FooterRow.Cells(4).Text = (dTotalMetMinSignsMTD / dTotalMetMinJobsMTD).ToString("P2")

        grdSigns.FooterRow.Cells(5).Text = dTotalMetMinJobsYTD.ToString("F")
        grdSigns.FooterRow.Cells(6).Text = dTotalMetMinSignsYTD.ToString("F")
        grdSigns.FooterRow.Cells(7).Text = (dTotalMetMinSignsYTD / dTotalMetMinJobsYTD).ToString("P2")



        If dtNoMin.Rows.Count = 0 Then
            dr = dtNoMin.NewRow
            dr("employee") = "No elegible employees ..."
            dr("truckHours") = ""
            dr("jobsMTD") = ""
            dr("jobsYTD") = ""
            dr("signsMTD") = ""
            dr("signsYTD") = ""

            dr("signpercentageMTD") = ""
            dr("signpercentageYTD") = ""

            dtNoMin.Rows.Add(dr)

        End If

        dv = New DataView(dtNoMin)
        dv.Sort = sort & " DESC"

        grdSignsNoMinimum.DataSource = dv
        grdSignsNoMinimum.DataBind()

        grdSignsNoMinimum.HeaderRow.Cells(0).Text = "<a href='/ReportsView.aspx?rpt=signs&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=employee'>Employee</a>"
        grdSignsNoMinimum.HeaderRow.Cells(1).Text = "Truck Hours"
        grdSignsNoMinimum.HeaderRow.Cells(2).Text = "<a href='/ReportsView.aspx?rpt=signs&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=jobsMTD'>Jobs</a>"
        grdSignsNoMinimum.HeaderRow.Cells(3).Text = "<a href='/ReportsView.aspx?rpt=signs&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=signsMTD'>Signs</a>"
        grdSignsNoMinimum.HeaderRow.Cells(4).Text = "<a href='/ReportsView.aspx?rpt=signs&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=signpercentageMTD'>Signs %</a>"
        grdSignsNoMinimum.HeaderRow.Cells(5).Text = "<a href='/ReportsView.aspx?rpt=signs&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=jobsYTD'>Jobs YTD</a>"
        grdSignsNoMinimum.HeaderRow.Cells(6).Text = "<a href='/ReportsView.aspx?rpt=signs&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=signsYTD'>Sign YTD</a>"
        grdSignsNoMinimum.HeaderRow.Cells(7).Text = "<a href='/ReportsView.aspx?rpt=signs&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=signpercentageYTD'>Sign % YTD</a>"

        grdSignsNoMinimum.FooterRow.Cells(2).Text = dTotalNotMetMinJobsMTD.ToString("F")
        grdSignsNoMinimum.FooterRow.Cells(3).Text = dTotalNotMetMinSignsMTD.ToString("F")
        grdSignsNoMinimum.FooterRow.Cells(4).Text = (dTotalNotMetMinSignsMTD / dTotalNotMetMinJobsMTD).ToString("P2")

        grdSignsNoMinimum.FooterRow.Cells(5).Text = dTotalNotMetMinJobsYTD.ToString("F")
        grdSignsNoMinimum.FooterRow.Cells(6).Text = dTotalNotMetMinSignsYTD.ToString("F")
        grdSignsNoMinimum.FooterRow.Cells(7).Text = (dTotalNotMetMinSignsYTD / dTotalNotMetMinJobsYTD).ToString("P2")

    End Sub

    Private Sub LoadTipsReport()

        Dim tipsTotal As Double = 0

        Dim dt As New DataTable
        dt.Columns.Add("employee")
        dt.Columns.Add("tipsTotal")

        Dim dr As DataRow
        Dim db As New Connection("Consumer_DSN")

        Dim cmd As New SqlCommand
        Dim cmdYTD As New SqlCommand
        cmd.CommandText = "spGetUsers"
        cmd.CommandType = CommandType.StoredProcedure

        Dim dtEmps As New DataTable
        dtEmps = db.FillWithSP(cmd)
        For Each dtewhr As DataRow In dtEmps.Rows

            cmd = New SqlCommand
            cmd.CommandText = "spReport_GetTips"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@startDate", Request.QueryString("startDate"))
            cmd.Parameters.AddWithValue("@endDate", Request.QueryString("endDate"))
            cmd.Parameters.AddWithValue("@userID", dtewhr.Item("id"))

            Dim dtTips As New DataTable
            dtTips = db.FillWithSP(cmd)
            For Each dts As DataRow In dtTips.Rows
                If Not dts.Item("totalTips") Is Nothing AndAlso Not IsDBNull(dts.Item("totalTips")) Then
                    dr = dt.NewRow
                    dr("employee") = dtewhr.Item("firstName") & " " & dtewhr.Item("lastName")
                    dr("tipsTotal") = dts.Item("totalTips")

                    tipsTotal += dts.Item("totalTips")

                    dt.Rows.Add(dr)
                End If
            Next

        Next

        grdTips.DataSource = dt
        grdTips.DataBind()

        grdTips.FooterRow.Cells(1).Text = tipsTotal.ToString("0,0.00")

    End Sub

    Private Sub LoadAJSReport()

        Dim dt As New DataTable
        dt.Columns.Add("id")
        dt.Columns.Add("employee")
        dt.Columns.Add("ajsTotal")

        Dim dr As DataRow
        Dim db As New Connection("Consumer_DSN")

        Dim cmd As New SqlCommand
        Dim cmdYTD As New SqlCommand
        cmd.CommandText = "spGetUsers"
        cmd.CommandType = CommandType.StoredProcedure

        Dim dtEmps As New DataTable
        dtEmps = db.FillWithSP(cmd)
        For Each dtewhr As DataRow In dtEmps.Rows

            cmd = New SqlCommand
            cmd.CommandText = "spReport_GetAJS"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@startDate", Request.QueryString("startDate"))
            cmd.Parameters.AddWithValue("@endDate", Request.QueryString("endDate"))
            cmd.Parameters.AddWithValue("@userID", dtewhr.Item("id"))

            Dim dtAJS As New DataTable
            dtAJS = db.FillWithSP(cmd)
            For Each dts As DataRow In dtAJS.Rows
                If Not dts.Item("ajs") Is Nothing AndAlso Not IsDBNull(dts.Item("ajs")) Then
                    dr = dt.NewRow
                    dr("id") = dtewhr.Item("id")
                    dr("employee") = dtewhr.Item("lastName") & ", " & dtewhr.Item("firstName")
                    dr("ajsTotal") = Math.Round(dts.Item("ajs"), 2)

                    dt.Rows.Add(dr)


                End If
            Next

        Next

        For Each drNew As DataRow In dt.Rows
            db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[userInfo] SET currentAJS = " & drNew("ajsTotal") & " where id = '" & drNew("id") & "'")
        Next

        Dim sort As String = Request.QueryString("sort")
        If String.IsNullOrEmpty(sort) Then
            sort = "ajsTotal"
        End If
        Dim dv As DataView = New DataView(dt)
        If sort = "employee" Then
            dv.Sort = sort & " ASC"
        Else
            dv.Sort = sort & " DESC"
        End If

        grdAJS.DataSource = dv
        grdAJS.DataBind()

        grdAJS.HeaderRow.Cells(0).Text = "<a href='/ReportsView.aspx?rpt=ajs&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=employee'>Employee</a>"
        grdAJS.HeaderRow.Cells(1).Text = "<a href='/ReportsView.aspx?rpt=ajs&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=ajsTotal'>AJS</a>"

    End Sub

    Private Sub LoadHourTypeReport()

        Dim workTypeTotal As Decimal = 0
        Dim workTypeOverall As Decimal = 0

        Dim dt As New DataTable
        dt.Columns.Add("routeDate")
        dt.Columns.Add("employee")
        dt.Columns.Add("totalHours")
        dt.Columns.Add("workType")

        Dim dr As DataRow
        Dim db As New Connection("Consumer_DSN")

        Dim cmd As New SqlCommand
        Dim cmdYTD As New SqlCommand
        cmd.CommandText = "spGetDropDownListData"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@segment", "workType")

        Dim dtEmps As New DataTable
        dtEmps = db.FillWithSP(cmd)
        For Each dtewhr As DataRow In dtEmps.Rows

            cmd = New SqlCommand
            cmd.CommandText = "spReport_GetHourType"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@startDate", Request.QueryString("startDate"))
            cmd.Parameters.AddWithValue("@endDate", Request.QueryString("endDate"))
            cmd.Parameters.AddWithValue("@hourType", dtewhr.Item("displayName"))

            Dim dtAJS As New DataTable
            dtAJS = db.FillWithSP(cmd)
            For Each dts As DataRow In dtAJS.Rows

                If dts.Item("totalHours") > 0 Then
                    dr = dt.NewRow
                    dr("routeDate") = dts.Item("routeDate")
                    dr("employee") = dts.Item("fullName")
                    dr("totalHours") = dts.Item("totalHours")
                    dr("workType") = dts.Item("workType")

                    dt.Rows.Add(dr)

                    workTypeTotal = workTypeTotal + dts.Item("totalHours")
                    workTypeOverall = workTypeOverall + dts.Item("totalHours")
                End If

            Next

            If dtAJS.Rows.Count > 0 Then
                dr = dt.NewRow
                dr("routeDate") = ""
                dr("employee") = ""
                dr("totalHours") = ""
                dr("workType") = ""

                dt.Rows.Add(dr)

                dr = dt.NewRow
                dr("routeDate") = ""
                dr("employee") = ""
                dr("totalHours") = workTypeTotal
                dr("workType") = "Total " & dtewhr.Item("displayName")

                dt.Rows.Add(dr)

                dr = dt.NewRow
                dr("routeDate") = ""
                dr("employee") = ""
                dr("totalHours") = ""
                dr("workType") = ""

                dt.Rows.Add(dr)

            End If

            workTypeTotal = 0

        Next

        Dim dv As DataView = New DataView(dt)

        dr = dt.NewRow
        dr("routeDate") = ""
        dr("employee") = ""
        dr("totalHours") = ""
        dr("workType") = ""

        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("routeDate") = ""
        dr("employee") = ""
        dr("totalHours") = workTypeOverall
        dr("workType") = "Total Overall Hours"

        dt.Rows.Add(dr)

        grdHourType.DataSource = dv
        grdHourType.DataBind()

    End Sub

    Private Sub LoadReasonNotConvertReport()

        Dim reasonNotConvertTotal As Decimal = 0

        Dim dt As New DataTable
        dt.Columns.Add("routeNumber")
        dt.Columns.Add("routeDate")
        dt.Columns.Add("jobID")
        dt.Columns.Add("id")
        dt.Columns.Add("reasonNotConvert")
        dt.Columns.Add("ticketDetails")

        Dim dr As DataRow
        Dim db As New Connection("Consumer_DSN")

        Dim cmd As New SqlCommand
        Dim cmdYTD As New SqlCommand
        cmd.CommandText = "spGetDropDownListData"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@segment", "reasonNotConvert")

        Dim dtEmps As New DataTable
        dtEmps = db.FillWithSP(cmd)
        For Each dtewhr As DataRow In dtEmps.Rows

            cmd = New SqlCommand
            cmd.CommandText = "spReport_GetReasonNotConvert"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@startDate", Request.QueryString("startDate"))
            cmd.Parameters.AddWithValue("@endDate", Request.QueryString("endDate"))
            cmd.Parameters.AddWithValue("@reasonNotConvert", dtewhr.Item("displayName"))

            Dim dtAJS As New DataTable
            dtAJS = db.FillWithSP(cmd)
            For Each dts As DataRow In dtAJS.Rows

                dr = dt.NewRow
                dr("routeNumber") = dts.Item("routeNumber")
                dr("routeDate") = Convert.ToDateTime(dts.Item("routeDate")).ToString("d")
                dr("jobID") = dts.Item("jobID")
                dr("id") = dts.Item("id")
                dr("reasonNotConvert") = dts.Item("reasonNotConvert")
                dr("ticketDetails") = dts.Item("ticketDetails")

                dt.Rows.Add(dr)

                reasonNotConvertTotal = reasonNotConvertTotal + 1

            Next

            If reasonNotConvertTotal > 0 Then
                dr = dt.NewRow
                dr("routeNumber") = ""
                dr("routeDate") = ""
                dr("jobID") = ""
                dr("id") = ""
                dr("reasonNotConvert") = reasonNotConvertTotal & " - " & dtewhr.Item("displayName")
                dr("ticketDetails") = ""

                dt.Rows.Add(dr)

                dr = dt.NewRow
                dr("routeNumber") = ""
                dr("routeDate") = ""
                dr("jobID") = ""
                dr("id") = ""
                dr("reasonNotConvert") = ""
                dr("ticketDetails") = ""

                dt.Rows.Add(dr)

                reasonNotConvertTotal = 0
            End If


        Next

        If dt.Rows.Count > 0 Then
            Dim dv As DataView = New DataView(dt)

            grdReasonsNotConvert.DataSource = dv
            grdReasonsNotConvert.DataBind()

            grdReasonsNotConvert.HeaderRow.Cells(0).Text = "Route Number"
            grdReasonsNotConvert.HeaderRow.Cells(1).Text = "Route Date"
            grdReasonsNotConvert.HeaderRow.Cells(2).Text = "Job ID"
            grdReasonsNotConvert.HeaderRow.Cells(3).Text = "Reason Not Convert"
            grdReasonsNotConvert.HeaderRow.Cells(4).Text = "Ticket Details"
        End If


    End Sub


    Private Sub LoadOnSiteConversionReport()

        Dim notConverted As Decimal = 0
        Dim converted As Decimal = 0
        Dim conversionPercentage As Decimal = 0

        Dim dt As New DataTable
        dt.Columns.Add("employee")
        dt.Columns.Add("employeeName")
        dt.Columns.Add("empID")
        dt.Columns.Add("startDate")
        dt.Columns.Add("endDate")
        dt.Columns.Add("totalJobs")
        dt.Columns.Add("converted")
        dt.Columns.Add("notConverted")
        dt.Columns.Add("conversionPercentage")

        Dim dr As DataRow
        Dim db As New Connection("Consumer_DSN")

        Dim cmd As New SqlCommand
        Dim cmdYTD As New SqlCommand
        cmd.CommandText = "spReport_GetEmpsWithSignIn"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@startDate", Request.QueryString("startDate"))
        cmd.Parameters.AddWithValue("@endDate", Request.QueryString("endDate"))

        Dim dtEmps As New DataTable
        dtEmps = db.FillWithSP(cmd)
        For Each dtewhr As DataRow In dtEmps.Rows

            cmd = New SqlCommand
            cmd.CommandText = "spReport_GetEmpOSCNotConverted"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@startDate", Request.QueryString("startDate"))
            cmd.Parameters.AddWithValue("@endDate", Request.QueryString("endDate"))
            cmd.Parameters.AddWithValue("@empID", dtewhr.Item("id"))

            Dim dtAJS As New DataTable
            dtAJS = db.FillWithSP(cmd)
            For Each dts As DataRow In dtAJS.Rows
                notConverted = Convert.ToDecimal(dts.Item("NotConverted"))
            Next


            cmd = New SqlCommand
            cmd.CommandText = "spReport_GetEmpOSCConverted"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@startDate", Request.QueryString("startDate"))
            cmd.Parameters.AddWithValue("@endDate", Request.QueryString("endDate"))
            cmd.Parameters.AddWithValue("@empID", dtewhr.Item("id"))

            dtAJS = New DataTable
            dtAJS = db.FillWithSP(cmd)
            For Each dts As DataRow In dtAJS.Rows
                converted = Convert.ToDecimal(dts.Item("Converted"))
            Next


            '**** Total Jobs
            dr = dt.NewRow
            dr("employee") = dtewhr.Item("firstName") & " " & dtewhr.Item("lastName")
            dr("employeeName") = dtewhr.Item("firstName") & " " & dtewhr.Item("lastName")
            dr("empID") = dtewhr.Item("id")
            dr("startDate") = Request.QueryString("startDate")
            dr("endDate") = Request.QueryString("endDate")
            dr("totalJobs") = converted + notConverted
            dr("converted") = converted
            dr("notConverted") = notConverted
            If converted > 0 Then
                conversionPercentage = (converted / (converted + notConverted)) * 100
                dr("conversionPercentage") = conversionPercentage.ToString("F2") & "%"
            Else
                dr("conversionPercentage") = "0.00%"
            End If

            dt.Rows.Add(dr)
        Next

        If dt.Rows.Count > 0 Then
            Dim dv As DataView = New DataView(dt)

            grdOSCbyGuy.DataSource = dv
            grdOSCbyGuy.DataBind()

            grdOSCbyGuy.HeaderRow.Cells(0).Text = "Employee"
            grdOSCbyGuy.HeaderRow.Cells(1).Text = "Total Jobs"
            grdOSCbyGuy.HeaderRow.Cells(2).Text = "Converted"
            grdOSCbyGuy.HeaderRow.Cells(3).Text = "Not Converted"
            grdOSCbyGuy.HeaderRow.Cells(4).Text = "OSC Percentage"

        End If


    End Sub


    Private Sub LoadOSCEmployeeDetailsReport()

        Employee = Request.QueryString("employee")

        Dim dt As New DataTable
        dt.Columns.Add("notConvertReason")
        dt.Columns.Add("total")

        Dim dr As DataRow
        Dim db As New Connection("Consumer_DSN")

        Dim cmd As New SqlCommand
        Dim cmdYTD As New SqlCommand
        cmd.CommandText = "spReport_GetEmpOSCReasons"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@startDate", Request.QueryString("startDate"))
        cmd.Parameters.AddWithValue("@endDate", Request.QueryString("endDate"))
        cmd.Parameters.AddWithValue("@empID", Request.QueryString("empID"))

        Dim dtEmps As New DataTable
        dtEmps = db.FillWithSP(cmd)
        For Each dtewhr As DataRow In dtEmps.Rows

            dr = dt.NewRow
            dr("notConvertReason") = dtewhr.Item("displayName")
            dr("total") = dtewhr.Item("displayNameCount")

            dt.Rows.Add(dr)
        Next

        If dt.Rows.Count > 0 Then
            Dim dv As DataView = New DataView(dt)

            grdOSCGuyResults.DataSource = dv
            grdOSCGuyResults.DataBind()

            grdOSCGuyResults.HeaderRow.Cells(0).Text = "Reason"
            grdOSCGuyResults.HeaderRow.Cells(1).Text = "Total"

        End If


    End Sub





    Private Sub LoadDailyRouteReport()

        Dim truckTeam As String = ""
        Dim dailyMktgRev As Decimal = 0
        Dim dailyRPH As Decimal = 0
        Dim dailyOSC As Decimal = 0
        Dim dailySignPercentage As Decimal = 0
        Dim dailyDoorHangerPercentage As Decimal = 0
        Dim dailySwipePercentage As Decimal = 0

        Dim totalRev As Decimal = 0
        Dim totalTruckHours As Decimal = 0
        Dim totalJobsWithRev As Decimal = 0
        Dim totalJobsWithoutRev As Decimal = 0
        Dim totalSign As Decimal = 0
        Dim totalSwipe As Decimal = 0
        Dim totalDH As Decimal = 0



        Dim dt As New DataTable
        dt.Columns.Add("routeID")
        dt.Columns.Add("routeDate")
        dt.Columns.Add("routeNumber")
        dt.Columns.Add("truckTeam")
        dt.Columns.Add("jobsCount")
        dt.Columns.Add("jobsRevenueCount")
        dt.Columns.Add("revenue")
        dt.Columns.Add("ajs")
        dt.Columns.Add("rph")
        dt.Columns.Add("osc")
        dt.Columns.Add("signpercentage")
        dt.Columns.Add("doorhangerpercentage")
        dt.Columns.Add("swipepercentage")
        dt.Columns.Add("tips")
        dt.Columns.Add("profitshare")

        Dim dr As DataRow
        Dim db As New Connection("Consumer_DSN")

        '** delete first
        db.ExecuteScaler("DELETE FROM [RouteManagement].[dbo].[dailyRevReport]")

        Dim cmd As New SqlCommand
        cmd.CommandText = "spGetRouteByDate"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@routeDate", Request.QueryString("startDate"))

        Dim dtDailyRev As New DataTable
        dtDailyRev = db.FillWithSP(cmd)
        For Each dteroutes As DataRow In dtDailyRev.Rows

            cmd = New SqlCommand
            cmd.CommandText = "spReport_InsertDailyRev"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@startDate", Request.QueryString("startDate"))
            cmd.Parameters.AddWithValue("@routeID", dteroutes("id"))

            db.ExecuteNonQuerySP(cmd)


            cmd = New SqlCommand
            cmd.CommandText = "spReport_InsertDailyTips"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@startDate", Request.QueryString("startDate"))
            cmd.Parameters.AddWithValue("@routeID", dteroutes("id"))

            db.ExecuteNonQuerySP(cmd)



            truckTeam = ""
            cmd = New SqlCommand
            cmd.CommandText = "spGetRouteUsers"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@routeID", dteroutes("id"))

            Dim dtjobs As New DataTable
            dtjobs = db.FillWithSP(cmd)
            For Each dtewhr As DataRow In dtjobs.Rows
                truckTeam = truckTeam & dtewhr("firstName") & " " & dtewhr("lastName") & ", "
            Next


            dailyMktgRev = 0

            cmd = New SqlCommand
            cmd.CommandText = "spGetMarketingByRouteID"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@routeID", dteroutes("id"))

            dtjobs = New DataTable
            dtjobs = db.FillWithSP(cmd)
            For Each dtewhr As DataRow In dtjobs.Rows
                If Not dtewhr("mktgdollars") Is DBNull.Value Then
                    dailyMktgRev += dtewhr("mktgdollars")
                End If
            Next


            totalTruckHours = 0

            cmd = New SqlCommand
            cmd.CommandText = "spGetWageSummaryRoute"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@routeID", dteroutes("id"))

            dtjobs = New DataTable
            dtjobs = db.FillWithSP(cmd)
            For Each dtewhr As DataRow In dtjobs.Rows
                If Not dtewhr("truckHours") Is DBNull.Value Then
                    totalTruckHours += dtewhr("truckHours")
                End If
            Next



            totalJobsWithRev = 0
            totalJobsWithoutRev = 0
            totalSign = 0
            totalSwipe = 0
            totalDH = 0
            totalRev = 0

            cmd = New SqlCommand
            cmd.CommandText = "spGetJobsByRouteID"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@routeID", dteroutes("id"))

            dtjobs = New DataTable
            dtjobs = db.FillWithSP(cmd)
            For Each dtewhr As DataRow In dtjobs.Rows

                '** OSC
                If Convert.ToDecimal(dtewhr("ticketRevenue")) > 0 Then
                    totalJobsWithRev += 1
                    totalRev += dtewhr("ticketRevenue") - dtewhr("ticketDiscount")
                Else
                    totalJobsWithoutRev += 1
                End If

                '** Sign Percentage
                If dtewhr("sign").ToString.Trim.ToLower = "y" Then
                    totalSign += 1
                End If

                '** Door Hangers
                If dtewhr("doorHangers").ToString.Trim.ToLower = "y" Then
                    totalDH += 1
                End If

                '** Swipe Percentage
                If dtewhr("payMethod").ToString.Trim.ToLower = "credit swipe" Then
                    totalSwipe += 1
                End If

            Next

            If totalJobsWithRev > 0 Then
                cmd = New SqlCommand
                cmd.CommandText = "spReport_InsertDailyRestOfIt"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@routeID", dteroutes("id"))
                cmd.Parameters.AddWithValue("@truckTeam", truckTeam)
                cmd.Parameters.AddWithValue("@jobsCount", totalJobsWithRev + totalJobsWithoutRev)
                cmd.Parameters.AddWithValue("@jobsRevenueCount", totalJobsWithRev)
                cmd.Parameters.AddWithValue("@rph", (totalRev + dailyMktgRev) / totalTruckHours)
                cmd.Parameters.AddWithValue("@osc", (totalJobsWithRev / (totalJobsWithRev + totalJobsWithoutRev)) * 100)
                cmd.Parameters.AddWithValue("@signPercentage", (totalSign / totalJobsWithRev) * 100)
                cmd.Parameters.AddWithValue("@doorHangersPercentage", (totalDH / totalJobsWithRev) * 100)
                cmd.Parameters.AddWithValue("@swipePercentage", (totalSwipe / totalJobsWithRev) * 100)

                db.ExecuteNonQuerySP(cmd)
            End If


        Next







        cmd = New SqlCommand
        cmd.CommandText = "spReport_GetDailyRev"
        cmd.CommandType = CommandType.StoredProcedure

        Dim dtRoutes As New DataTable
        dtRoutes = db.FillWithSP(cmd)
        For Each dteroutes As DataRow In dtRoutes.Rows
            dr = dt.NewRow

            dr("routeID") = dteroutes("routeID")
            dr("routeDate") = Convert.ToDateTime(dteroutes("routeDate")).ToString("d")
            dr("routeNumber") = dteroutes("routeNumber")
            dr("truckTeam") = dteroutes("truckTeam")
            dr("jobsCount") = dteroutes("jobsCount")
            dr("jobsRevenueCount") = dteroutes("jobsRevenueCount")
            dr("revenue") = Math.Round(dteroutes("revenue"), 2)
            dr("ajs") = Math.Round(dteroutes("ajs"), 2)
            dr("rph") = Math.Round(dteroutes("rph"), 2)
            dr("osc") = Math.Round(dteroutes("osc"), 1) & "%"
            dr("signpercentage") = Math.Round(dteroutes("signPercentage"), 1) & "%"
            dr("doorhangerpercentage") = Math.Round(dteroutes("doorHangerPercentage"), 1) & "%"
            dr("swipepercentage") = Math.Round(dteroutes("swipePercentage"), 1) & "%"
            dr("tips") = Math.Round(dteroutes("tips"), 2)
            dr("profitshare") = Math.Round(dteroutes("profitShare"), 2)

            dt.Rows.Add(dr)

        Next








        Dim dv As DataView = New DataView(dt)

        Dim sort As String = Request.QueryString("sort")
        If String.IsNullOrEmpty(sort) Then
            sort = "routeNumber"
        End If

        If sort = "routeNumber" Then
            dv.Sort = sort & " ASC"
        Else
            dv.Sort = sort & " DESC"
        End If

        grdDailyRoute.DataSource = dv
        grdDailyRoute.DataBind()

        grdDailyRoute.HeaderRow.Cells(0).Text = "Route Date"
        grdDailyRoute.HeaderRow.Cells(1).Text = "<a href='/ReportsView.aspx?rpt=dailyroute&startDate=" & Request.QueryString("startDate") & "&sort=routeNumber'>Route Number</a>"
        grdDailyRoute.HeaderRow.Cells(2).Text = "Truck Team"
        grdDailyRoute.HeaderRow.Cells(3).Text = "Total Jobs"
        grdDailyRoute.HeaderRow.Cells(4).Text = "Jobs W/ Revenue"
        grdDailyRoute.HeaderRow.Cells(5).Text = "<a href='/ReportsView.aspx?rpt=dailyroute&startDate=" & Request.QueryString("startDate") & "&sort=revenue'>Net Revenue</a>"
        grdDailyRoute.HeaderRow.Cells(6).Text = "<a href='/ReportsView.aspx?rpt=dailyroute&startDate=" & Request.QueryString("startDate") & "&sort=ajs'>AJS</a>"
        grdDailyRoute.HeaderRow.Cells(7).Text = "<a href='/ReportsView.aspx?rpt=dailyroute&startDate=" & Request.QueryString("startDate") & "&sort=rph'>RPH</a>"
        grdDailyRoute.HeaderRow.Cells(8).Text = "<a href='/ReportsView.aspx?rpt=dailyroute&startDate=" & Request.QueryString("startDate") & "&sort=osc'>OSC</a>"
        grdDailyRoute.HeaderRow.Cells(9).Text = "<a href='/ReportsView.aspx?rpt=dailyroute&startDate=" & Request.QueryString("startDate") & "&sort=signpercentage'>Sign %</a>"
        grdDailyRoute.HeaderRow.Cells(10).Text = "<a href='/ReportsView.aspx?rpt=dailyroute&startDate=" & Request.QueryString("startDate") & "&sort=doorhangerpercentage'>DH %</a>"
        grdDailyRoute.HeaderRow.Cells(11).Text = "<a href='/ReportsView.aspx?rpt=dailyroute&startDate=" & Request.QueryString("startDate") & "&sort=swipepercentage'>Swipe %</a>"
        grdDailyRoute.HeaderRow.Cells(12).Text = "<a href='/ReportsView.aspx?rpt=dailyroute&startDate=" & Request.QueryString("startDate") & "&sort=tips'>Tips</a>"
        grdDailyRoute.HeaderRow.Cells(13).Text = "<a href='/ReportsView.aspx?rpt=dailyroute&startDate=" & Request.QueryString("startDate") & "&sort=profitshare'>Profit Share</a>"

    End Sub

    Private Sub LoadDumpLocationReport()

        Dim dTotal As Double = 0

        Dim dt As New DataTable
        dt.Columns.Add("routeID")
        dt.Columns.Add("routeNumber")
        dt.Columns.Add("routeDate")
        dt.Columns.Add("ticketNumber")
        dt.Columns.Add("ticketTotal")

        Dim dr As DataRow
        Dim db As New Connection("Consumer_DSN")

        Dim cmd As New SqlCommand
        cmd.CommandText = "spGetDropDownListDataByDisplayName"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@segment", "dumpLocation")

        Dim dtCEC As New DataTable
        dtCEC = db.FillWithSP(cmd)
        For Each dtr As DataRow In dtCEC.Rows

            Dim cmdDumpLocation As New SqlCommand
            cmdDumpLocation.CommandText = "spReport_GetDumpsByLocation"
            cmdDumpLocation.CommandType = CommandType.StoredProcedure
            cmdDumpLocation.Parameters.AddWithValue("@startDate", Request.QueryString("startDate"))
            cmdDumpLocation.Parameters.AddWithValue("@endDate", Request.QueryString("endDate"))
            cmdDumpLocation.Parameters.AddWithValue("@dumpLocation", dtr("displayName").ToString.Trim)

            Dim dtCECResults As New DataTable
            dtCECResults = db.FillWithSP(cmdDumpLocation)

            If dtCECResults.Rows.Count > 0 Then
                dTotal = 0

                dr = dt.NewRow
                dr("routeNumber") = dtr("displayName")
                dr("routeID") = ""
                dr("routeDate") = ""
                dr("ticketNumber") = ""
                dr("ticketTotal") = ""
                dt.Rows.Add(dr)

                For Each drcecresults As DataRow In dtCECResults.Rows

                    dr = dt.NewRow
                    dr("routeNumber") = "Route " & drcecresults("routeNumber")
                    dr("routeID") = drcecresults("id")
                    dr("routeDate") = drcecresults("routeDate")
                    dr("ticketNumber") = drcecresults("ticketNumber")
                    dr("ticketTotal") = drcecresults("ticketTotal")
                    dt.Rows.Add(dr)

                    dTotal = dTotal + drcecresults("ticketTotal")

                Next

                dr = dt.NewRow
                dr("routeNumber") = ""
                dr("routeID") = ""
                dr("routeDate") = ""
                dr("ticketNumber") = ""
                dr("ticketTotal") = dtr("displayName") & " - " & dTotal
                dt.Rows.Add(dr)

                dr = dt.NewRow
                dr("routeNumber") = ""
                dr("routeID") = ""
                dr("routeDate") = ""
                dr("ticketNumber") = ""
                dr("ticketTotal") = ""
                dt.Rows.Add(dr)

            End If

        Next

        grdDumpLocation.DataSource = dt
        grdDumpLocation.DataBind()

    End Sub

    Private Sub LoadDumpByGuyReport()


        Dim dRevenue As Decimal = 0
        Dim dDumps As Decimal = 0
        Dim dDumpsPercentage As Decimal = 0

        Dim dt As New DataTable
        dt.Columns.Add("empID")
        dt.Columns.Add("startDate")
        dt.Columns.Add("endDate")

        dt.Columns.Add("employee")
        dt.Columns.Add("revenue", System.Type.GetType("System.Decimal"))
        dt.Columns.Add("dumps", System.Type.GetType("System.Decimal"))
        dt.Columns.Add("dumpsPercentage", System.Type.GetType("System.Decimal"))

        Dim dr As DataRow
        Dim db As New Connection("Consumer_DSN")

        Dim cmd As New SqlCommand
        cmd.CommandText = "spReport_GetDumpsPercentage"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@startDate", Request.QueryString("startDate"))
        cmd.Parameters.AddWithValue("@endDate", Request.QueryString("endDate"))

        Dim dtCEC As New DataTable
        dtCEC = db.FillWithSP(cmd)
        For Each dtr As DataRow In dtCEC.Rows
            dRevenue = 0
            dDumps = 0

            dr = dt.NewRow
            dr("employee") = dtr("fullName")

            cmd = New SqlCommand
            cmd.CommandText = "spReport_GetRevenueByEmployee"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@userID", dtr("id"))
            cmd.Parameters.AddWithValue("@startDate", Request.QueryString("startDate"))
            cmd.Parameters.AddWithValue("@endDate", Request.QueryString("endDate"))

            Dim dtjobs As New DataTable
            dtjobs = db.FillWithSP(cmd)
            For Each dtewhr As DataRow In dtjobs.Rows
                dRevenue = dtewhr("justJobRevenue")
                dr("revenue") = dRevenue
            Next

            dDumps = dtr("ticketTotalSum")
            dr("dumps") = dDumps

            dDumpsPercentage = 0
            If dRevenue > 0 Then
                dDumpsPercentage = dDumps / dRevenue
            End If
            dr("dumpsPercentage") = dDumpsPercentage

            dr("empID") = dtr("id")
            dr("startDate") = Request.QueryString("startDate")
            dr("endDate") = Request.QueryString("endDate")
            dt.Rows.Add(dr)

        Next

        Dim sort As String = "dumpsPercentage"
        Dim dv As DataView = New DataView(dt)
        dv.Sort = sort & " ASC"

        grdDumpsByGuy.DataSource = dv
        grdDumpsByGuy.DataBind()

        grdDumpsByGuy.HeaderRow.Cells(0).Text = "Employee"
        grdDumpsByGuy.HeaderRow.Cells(1).Text = "Total Revenue"
        grdDumpsByGuy.HeaderRow.Cells(2).Text = "Total Dumps"
        grdDumpsByGuy.HeaderRow.Cells(3).Text = "Dumps Percentage"


    End Sub


    Private Sub LoadDumpByGuyResultsReport()

        Dim dTotalTickets As Decimal = 0
        Dim dTicketsCount As Decimal = 0

        Employee = Request.QueryString("employee")

        Dim dt As New DataTable
        dt.Columns.Add("dumpLocation")
        dt.Columns.Add("total", System.Type.GetType("System.Decimal"))
        dt.Columns.Add("ticketsCount", System.Type.GetType("System.Decimal"))
        dt.Columns.Add("averageTicket", System.Type.GetType("System.Decimal"))

        Dim dr As DataRow
        Dim db As New Connection("Consumer_DSN")

        Dim cmd As New SqlCommand
        Dim cmdYTD As New SqlCommand
        cmd.CommandText = "spReport_GetDumpsByGuy"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@startDate", Request.QueryString("startDate"))
        cmd.Parameters.AddWithValue("@endDate", Request.QueryString("endDate"))
        cmd.Parameters.AddWithValue("@userID", Request.QueryString("empID"))

        Dim dtEmps As New DataTable
        dtEmps = db.FillWithSP(cmd)
        For Each dtewhr As DataRow In dtEmps.Rows

            dr = dt.NewRow
            dr("dumpLocation") = dtewhr.Item("dumpLocation")

            dTotalTickets = dtewhr.Item("ticketTotalSum")
            dTicketsCount = dtewhr.Item("ticketCount")

            dr("total") = dTotalTickets
            dr("ticketsCount") = dTicketsCount
            dr("averageTicket") = dTotalTickets / dTicketsCount

            dt.Rows.Add(dr)
        Next

        If dt.Rows.Count > 0 Then
            Dim dv As DataView = New DataView(dt)

            grdDumpsByGuyResults.DataSource = dv
            grdDumpsByGuyResults.DataBind()

            grdDumpsByGuyResults.HeaderRow.Cells(0).Text = "Dump Location"
            grdDumpsByGuyResults.HeaderRow.Cells(1).Text = "Total"
            grdDumpsByGuyResults.HeaderRow.Cells(2).Text = "Ticket Count"
            grdDumpsByGuyResults.HeaderRow.Cells(3).Text = "Average Ticket"

        End If

    End Sub


    Private Sub LoadDiscountsByGuyReport()


        Dim dTotalJobs As Decimal = 0
        Dim dTotalTotalJobs As Decimal = 0

        Dim dDiscountCount As Decimal = 0
        Dim dTotalDiscountCount As Decimal = 0

        Dim dDiscountSum As Decimal = 0
        Dim dTotalDiscountSum As Decimal = 0

        Dim dDiscountAvg As Decimal = 0

        Dim dTippedWithDiscount As Decimal = 0
        Dim dTotalTippedWithDiscount As Decimal = 0

        Dim dTipsValue As Decimal = 0
        Dim dTotalTipsValue As Decimal = 0

        Dim dTipsAverage As Decimal = 0
        Dim dPercentTippedWithDiscount As Decimal = 0
        Dim dAJS As Decimal = 0

        Dim dt As New DataTable
        dt.Columns.Add("empID")
        dt.Columns.Add("startDate")
        dt.Columns.Add("endDate")

        dt.Columns.Add("employee")
        dt.Columns.Add("totaljobsnum", System.Type.GetType("System.Decimal"))
        dt.Columns.Add("totaljobswithdiscount", System.Type.GetType("System.Decimal"))
        dt.Columns.Add("totaldiscountvalue", System.Type.GetType("System.Decimal"))
        dt.Columns.Add("percentdiscount", System.Type.GetType("System.Decimal"))
        dt.Columns.Add("averagediscount", System.Type.GetType("System.Decimal"))
        dt.Columns.Add("tippedjobswithdiscount", System.Type.GetType("System.Decimal"))
        dt.Columns.Add("totaltipsvalue", System.Type.GetType("System.Decimal"))
        dt.Columns.Add("averagetips", System.Type.GetType("System.Decimal"))
        dt.Columns.Add("percenttippedwithdiscount", System.Type.GetType("System.Decimal"))
        dt.Columns.Add("ajs", System.Type.GetType("System.Decimal"))

        Dim dr As DataRow
        Dim db As New Connection("Consumer_DSN")
        Dim ds As New DataSet

        Dim cmd As New SqlCommand
        cmd.CommandText = "spReport_GetDiscountsGuyTotal"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@startDate", Request.QueryString("startDate"))
        cmd.Parameters.AddWithValue("@endDate", Request.QueryString("endDate"))

        Dim dtCEC As New DataTable
        dtCEC = db.FillWithSP(cmd)
        For Each dtr As DataRow In dtCEC.Rows
            dr = dt.NewRow
            dr("employee") = dtr("fullName")

            dTotalJobs = dtr("totalJobs")
            dr("totaljobsnum") = dTotalJobs
            dTotalTotalJobs = dTotalTotalJobs + dTotalJobs

            cmd = New SqlCommand
            cmd.CommandText = "spReport_GetDiscountsGuyDiscount"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@userID", dtr("id"))
            cmd.Parameters.AddWithValue("@startDate", Request.QueryString("startDate"))
            cmd.Parameters.AddWithValue("@endDate", Request.QueryString("endDate"))

            Dim dtjobs As New DataTable
            dtjobs = db.FillWithSP(cmd)
            For Each dtewhr As DataRow In dtjobs.Rows
                dDiscountCount = dtewhr("tdCount")
                dTotalDiscountCount = dTotalDiscountCount + dDiscountCount

                If dDiscountCount > 0 Then
                    dr("totaljobswithdiscount") = dDiscountCount
                    dr("percentdiscount") = ((dDiscountCount / dTotalJobs) * 100)

                    dDiscountSum = dtewhr("tdSum")
                    dr("totaldiscountvalue") = dDiscountSum
                    dTotalDiscountSum = dTotalDiscountSum + dDiscountSum

                    dr("percentdiscount") = (dDiscountCount / dTotalJobs)

                    dDiscountAvg = dtewhr("tdAvg")
                    dr("averagediscount") = dDiscountAvg
                Else
                    dr("totaljobswithdiscount") = 0
                    dr("percentdiscount") = 0

                    dDiscountSum = 0
                    dr("totaldiscountvalue") = dDiscountSum

                    dr("percentdiscount") = 0

                    dDiscountAvg = 0
                    dr("averagediscount") = dDiscountAvg
                End If
            Next


            cmd = New SqlCommand
            cmd.CommandText = "spReport_GetDiscountsGuyTips"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@userID", dtr("id"))
            cmd.Parameters.AddWithValue("@startDate", Request.QueryString("startDate"))
            cmd.Parameters.AddWithValue("@endDate", Request.QueryString("endDate"))

            dtjobs = New DataTable
            dtjobs = db.FillWithSP(cmd)
            For Each dtewhr As DataRow In dtjobs.Rows
                dTippedWithDiscount = dtewhr("ttCount")
                dTotalTippedWithDiscount = dTotalTippedWithDiscount + dTippedWithDiscount

                If dTippedWithDiscount > 0 Then
                    dr("tippedjobswithdiscount") = dTippedWithDiscount
                    dr("percenttippedwithdiscount") = ((dTippedWithDiscount / dDiscountCount) * 100)

                    dTipsValue = dtewhr("ttSum")
                    dr("totaltipsvalue") = dTipsValue
                    dTotalTipsValue = dTotalTipsValue + dTipsValue

                    dTipsAverage = dtewhr("ttAvg")
                    dr("averagetips") = dTipsAverage

                    dr("percenttippedwithdiscount") = (dTippedWithDiscount / dDiscountCount)
                Else
                    dr("tippedjobswithdiscount") = 0
                    dr("percenttippedwithdiscount") = 0

                    dTipsValue = 0
                    dr("totaltipsvalue") = dTipsValue

                    dTipsAverage = 0
                    dr("averagetips") = dTipsAverage

                    dr("percenttippedwithdiscount") = 0
                End If

            Next


            cmd = New SqlCommand
            cmd.CommandText = "spReport_GetAJS"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@userID", dtr("id"))
            cmd.Parameters.AddWithValue("@startDate", Request.QueryString("startDate"))
            cmd.Parameters.AddWithValue("@endDate", Request.QueryString("endDate"))

            dtjobs = New DataTable
            dtjobs = db.FillWithSP(cmd)
            For Each dtewhr As DataRow In dtjobs.Rows
                dAJS = dtewhr("ajs")
                dr("ajs") = dAJS
            Next

            dr("empID") = dtr("id")
            dr("startDate") = Request.QueryString("startDate")
            dr("endDate") = Request.QueryString("endDate")
            dt.Rows.Add(dr)

        Next

        Dim sort As String = Request.QueryString("sort")
        If String.IsNullOrEmpty(sort) Then
            sort = "percentdiscount"
        End If
        Dim dv As DataView = New DataView(dt)
        dv.Sort = sort & " DESC"

        grdDiscountsGuy.DataSource = dv
        grdDiscountsGuy.DataBind()

        grdDiscountsGuy.HeaderRow.Cells(0).Text = "<a href='/ReportsView.aspx?rpt=discountsguy&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=employee'>Employee</a>"
        grdDiscountsGuy.HeaderRow.Cells(1).Text = "<a href='/ReportsView.aspx?rpt=discountsguy&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=totaljobsnum'>Total Jobs"
        grdDiscountsGuy.HeaderRow.Cells(2).Text = "<a href='/ReportsView.aspx?rpt=discountsguy&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=totaljobswithdiscount'>Total Discounted"
        grdDiscountsGuy.HeaderRow.Cells(3).Text = "<a href='/ReportsView.aspx?rpt=discountsguy&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=percentdiscount'>Percent Discounted"
        grdDiscountsGuy.HeaderRow.Cells(4).Text = "<a href='/ReportsView.aspx?rpt=discountsguy&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=totaldiscountvalue'>Discount Amount"
        grdDiscountsGuy.HeaderRow.Cells(5).Text = "<a href='/ReportsView.aspx?rpt=discountsguy&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=averagediscount'>Average Discount"
        grdDiscountsGuy.HeaderRow.Cells(6).Text = "<a href='/ReportsView.aspx?rpt=discountsguy&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=tippedjobswithdiscount'>Tipped W/ Discount"
        grdDiscountsGuy.HeaderRow.Cells(7).Text = "<a href='/ReportsView.aspx?rpt=discountsguy&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=percenttippedwithdiscount'>Percent Discount"
        grdDiscountsGuy.HeaderRow.Cells(8).Text = "<a href='/ReportsView.aspx?rpt=discountsguy&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=totaltipsvalue'>Tipped Total"
        grdDiscountsGuy.HeaderRow.Cells(9).Text = "<a href='/ReportsView.aspx?rpt=discountsguy&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=averagetips'>Avg Tip"
        grdDiscountsGuy.HeaderRow.Cells(10).Text = "<a href='/ReportsView.aspx?rpt=discountsguy&startDate=" & Request.QueryString("startDate") & "&endDate=" & Request.QueryString("endDate") & "&sort=ajs'>AJS"

        grdDiscountsGuy.FooterRow.Cells(1).Text = dTotalTotalJobs
        grdDiscountsGuy.FooterRow.Cells(2).Text = dTotalDiscountCount
        grdDiscountsGuy.FooterRow.Cells(3).Text = (dTotalDiscountCount / dTotalTotalJobs).ToString("p")
        grdDiscountsGuy.FooterRow.Cells(4).Text = dTotalDiscountSum.ToString("c")
        grdDiscountsGuy.FooterRow.Cells(5).Text = (dTotalDiscountSum / dTotalDiscountCount).ToString("c")
        grdDiscountsGuy.FooterRow.Cells(6).Text = dTotalTippedWithDiscount
        grdDiscountsGuy.FooterRow.Cells(7).Text = (dTotalTippedWithDiscount / dTotalDiscountCount).ToString("p")
        grdDiscountsGuy.FooterRow.Cells(8).Text = dTotalTipsValue.ToString("c")
        grdDiscountsGuy.FooterRow.Cells(9).Text = (dTotalTipsValue / dTotalTippedWithDiscount).ToString("c")

    End Sub



    Private Sub LoadGasLocationReport()

        Dim dTotal As Double = 0

        Dim dt As New DataTable
        dt.Columns.Add("routeID")
        dt.Columns.Add("routeNumber")
        dt.Columns.Add("routeDate")
        dt.Columns.Add("ticketTotal")

        Dim dr As DataRow
        Dim db As New Connection("Consumer_DSN")

        Dim cmd As New SqlCommand
        cmd.CommandText = "spGetDropDownListDataByDisplayName"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@segment", "gasLocation")

        Dim dtCEC As New DataTable
        dtCEC = db.FillWithSP(cmd)
        For Each dtr As DataRow In dtCEC.Rows

            Dim cmdDumpLocation As New SqlCommand
            cmdDumpLocation.CommandText = "spReport_GetGasByLocation"
            cmdDumpLocation.CommandType = CommandType.StoredProcedure
            cmdDumpLocation.Parameters.AddWithValue("@startDate", Request.QueryString("startDate"))
            cmdDumpLocation.Parameters.AddWithValue("@endDate", Request.QueryString("endDate"))
            cmdDumpLocation.Parameters.AddWithValue("@gasLocation", dtr("displayName").ToString.Trim)

            Dim dtCECResults As New DataTable
            dtCECResults = db.FillWithSP(cmdDumpLocation)

            If dtCECResults.Rows.Count > 0 Then
                dTotal = 0

                dr = dt.NewRow
                dr("routeNumber") = dtr("displayName")
                dr("routeID") = ""
                dr("routeDate") = ""
                dr("ticketTotal") = ""
                dt.Rows.Add(dr)

                For Each drcecresults As DataRow In dtCECResults.Rows

                    dr = dt.NewRow
                    dr("routeNumber") = "Route " & drcecresults("routeNumber")
                    dr("routeID") = drcecresults("id")
                    dr("routeDate") = drcecresults("routeDate")
                    dr("ticketTotal") = drcecresults("ticketTotal")
                    dt.Rows.Add(dr)

                    dTotal = dTotal + drcecresults("ticketTotal")

                Next

                dr = dt.NewRow
                dr("routeNumber") = ""
                dr("routeID") = ""
                dr("routeDate") = ""
                dr("ticketTotal") = dtr("displayName") & " - " & dTotal
                dt.Rows.Add(dr)

                dr = dt.NewRow
                dr("routeNumber") = ""
                dr("routeID") = ""
                dr("routeDate") = ""
                dr("ticketTotal") = ""
                dt.Rows.Add(dr)

            End If

        Next

        grdGasLocation.DataSource = dt
        grdGasLocation.DataBind()

    End Sub


#End Region

#Region "Button Clicks"
    Private Sub lnkCECSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCECSubmit.Click
        RedirectToPage("ReportsView.aspx?rpt=cec&startDate=" & txtCECStartDate.Text & "&endDate=" & txtCECEndDate.Text)
    End Sub

    Private Sub lnkJobsSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkJobsSubmit.Click
        RedirectToPage("ReportsView.aspx?rpt=jobs&startDate=" & txtJobsStartDate.Text & "&endDate=" & txtJobsEndDate.Text)
    End Sub

    Private Sub lnkPayrollSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPayrollSubmit.Click
        Dim startDate As Date
        startDate = txtPayrollStartDate.Text
        RedirectToPage("ReportsView.aspx?rpt=pay&week1StartDate=" & txtPayrollStartDate.Text & "&week1EndDate=" & startDate.AddDays(6) & "&week2StartDate=" & startDate.AddDays(7) & "&week2EndDate=" & startDate.AddDays(13))
    End Sub

    Private Sub lnkRPH_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRPH.Click
        RedirectToPage("ReportsView.aspx?rpt=rph&startDate=" & txtRPHStartDate.Text & "&endDate=" & txtRPHEndDate.Text)
    End Sub

    Private Sub lnkSigns_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSigns.Click
        RedirectToPage("ReportsView.aspx?rpt=signs&startDate=" & txtSignsStarDate.Text & "&endDate=" & txtSignsEndDate.Text)
    End Sub

    Private Sub lnkTips_Click(sender As Object, e As EventArgs) Handles lnkTips.Click
        RedirectToPage("ReportsView.aspx?rpt=tips&startDate=" & txtTipsEndDate.Text & "&endDate=" & txtTipsEndDate.Text)
    End Sub

    Private Sub lnkReturn_Click(sender As Object, e As EventArgs) Handles lnkReturn.Click
        RedirectToPage("reportsv2.aspx")
    End Sub

    Private Sub lnkTopDogSubmit_Click(sender As Object, e As EventArgs) Handles lnkTopDogSubmit.Click
        'RedirectToPage("ReportsView.aspx?rpt=topdog&startDate=" & txtTopDogStartDate.Text & "&endDate=" & txtTopDogEndDate.Text & "&sort=overallRank")
        RedirectToPage("ReportsView.aspx?rpt=topdog&startDate=" & txtTopDogStartDate.Text & "&endDate=" & txtTopDogEndDate.Text & "&sort=overallRank")
    End Sub

    Private Sub lnkTopDogV2Submit_Click(sender As Object, e As EventArgs) Handles lnkTopDogV2Submit.Click
        RedirectToPage("ReportsView.aspx?rpt=topdog&startDate=" & txtTopDogV2StartDate.Text & "&endDate=" & txtTopDogV2EndDate.Text & "&sort=overallRank")
    End Sub

    Private Sub lnkTopDogV3Submit_Click(sender As Object, e As EventArgs) Handles lnkTopDogV3Submit.Click
        RedirectToPage("ReportsView.aspx?rpt=topdog&startDate=" & txtTopDogV3StartDate.Text & "&endDate=" & txtTopDogV3EndDate.Text & "&sort=overallRank")
    End Sub

    Private Sub lnkEmployeeMetricsSubmit_Click(sender As Object, e As EventArgs) Handles lnkEmployeeMetricsSubmit.Click
        RedirectToPage("ReportsView.aspx?rpt=metrics&startDate=" & txtEmployeeMetricsStartDate.Text & "&sort=overallRank30")
    End Sub

    Private Sub lnkDailyRoute_Click(sender As Object, e As EventArgs) Handles lnkDailyRoute.Click
        RedirectToPage("ReportsView.aspx?rpt=dailyroute&startDate=" & txtDailyRouteStartDate.Text)
    End Sub

#End Region

#Region "Subs / Functions"
    Private Function ReturnFractionValue(ByVal strFraction As String) As Double
        Dim dblWhole As Double = 0
        Dim dblNum As Double = 0
        Dim dblDen As Double = 8

        Try
            If strFraction.ToLower = "full" Then
                strFraction = "1/1"
            End If

            If InStr(strFraction, " ") Then
                dblWhole = Mid(strFraction, 1, strFraction.IndexOf(" "))
            End If

            If Not InStr(strFraction, "/") > 0 Then
                strFraction = strFraction & "/1"
            End If

            dblNum = Mid(strFraction, strFraction.IndexOf("/"), 1)
            dblDen = Mid(strFraction, strFraction.IndexOf("/") + 2, 1)


            If dblWhole <> 0 Then
                Select Case dblDen
                    Case 8
                        dblNum = dblNum + (dblWhole * 8)
                    Case 6
                        dblNum = dblNum + (dblWhole * 6)
                    Case 4
                        dblNum = dblNum + (dblWhole * 4)
                    Case 3
                        dblNum = dblNum + (dblWhole * 3)
                    Case 2
                        dblNum = dblNum + (dblWhole * 2)
                End Select
            End If

            Return dblNum / dblDen

        Catch ex As Exception
            Return 0

        End Try


    End Function

    Private Sub LoadTopDogData()
        Dim npsPoints As Integer = 0
        Dim rphPoints As Integer = 0
        Dim signPoints As Integer = 0
        Dim overallPoints As Double = 0
        Dim truckHoursMin As Integer = 0
        Dim totalQualified As Integer = 0
        Dim db As New Connection("Consumer_DSN")


        '*** GRAB TRUCK HOURS MIN ***
        Dim cmd As New SqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT [displayName] FROM [RouteManagement].[dbo].[dropDownListData] where segment = 'truckHourMin'"
        Dim sdrRevenue As SqlDataReader = db.ExecuteReader(cmd)

        If sdrRevenue.HasRows Then
            While sdrRevenue.Read
                truckHoursMin = sdrRevenue("displayName")
            End While
        End If

        '*** GRAB TOTAL QUALIFIED ***
        cmd = New SqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT count([id]) as idCount FROM [RouteManagement].[dbo].[userInfo] where currentTruckHours >= " & truckHoursMin
        sdrRevenue = db.ExecuteReader(cmd)

        If sdrRevenue.HasRows Then
            While sdrRevenue.Read
                totalQualified = sdrRevenue("idCount")
            End While
        End If


        'clear the top dog table....
        db.ExecuteScaler("DELETE FROM [RouteManagement].[dbo].[topdog]")

        'update userInfo.currentTruckHours

        'insert NPS data....
        db.ExecuteScaler("INSERT INTO [RouteManagement].[dbo].[topdog] ([userID], [userName], [nps], [npsRank]) " & _
                            "SELECT id, firstName + ' ' + lastName, currentNPS, RANK() OVER (ORDER BY currentNPS DESC) AS RankByNPS " & _
                            "FROM [RouteManagement].[dbo].[userInfo] where [RouteManagement].[dbo].[userInfo].pseligible = 'Y' AND [RouteManagement].[dbo].[userInfo].currentTruckHours >= " & truckHoursMin)

        'update RPH data....
        db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[topdog] SET [RouteManagement].[dbo].[topdog].rph = [RouteManagement].[dbo].[userInfo].currentrph " & _
                            "FROM [RouteManagement].[dbo].[topdog] INNER Join [RouteManagement].[dbo].[userInfo] ON [RouteManagement].[dbo].[topdog].userid = [RouteManagement].[dbo].[userInfo].id " & _
                            "where [RouteManagement].[dbo].[userInfo].currentTruckHours >= " & truckHoursMin)

        db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[topdog] SET rphRank = z.RankByRPH FROM " & _
                            "(SELECT [ID], [currentTruckHours], [rph], RANK() OVER (ORDER BY rph DESC) AS RankByRPH FROM [topdog] i inner join [userInfo] td on i.userID = td.id)z " & _
                            "where z.id = [RouteManagement].[dbo].[topdog].userid")

        'update sign data....
        db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[topdog] SET [RouteManagement].[dbo].[topdog].sign = [RouteManagement].[dbo].[userInfo].currentsign " & _
                            "FROM [RouteManagement].[dbo].[topdog] INNER Join [RouteManagement].[dbo].[userInfo] ON [RouteManagement].[dbo].[topdog].userid = [RouteManagement].[dbo].[userInfo].id  " & _
                            "where [RouteManagement].[dbo].[userInfo].currentTruckHours >= " & truckHoursMin)

        db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[topdog] SET signRank = z.RankBySign FROM " & _
                            "(SELECT [ID], [currentTruckHours], [sign], RANK() OVER (ORDER BY [sign] DESC) AS RankBySign FROM [topdog] i inner join [userInfo] td on i.userID = td.id)z " & _
                            "where z.id = [RouteManagement].[dbo].[topdog].userid")

        'update truck hours....
        db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[topdog] SET truckHours = i.currentTruckHours FROM " & _
                            "(SELECT [ID], [currentTruckHours], RANK() OVER (ORDER BY currentRPH DESC) AS RankByRPH FROM [RouteManagement].[dbo].[userInfo])i " & _
                            "where i.id = [RouteManagement].[dbo].[topdog].userid And i.currentTruckHours >= " & truckHoursMin)

        '*** CALCULATE OVERALL POINTS ***
        cmd = New SqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT userID, nps, npsRank, rph, rphRank, sign, signRank, overall, overallRank, truckHours FROM [RouteManagement].[dbo].[topdog]"
        Dim dt As New DataTable
        dt = db.FillCommand(cmd)
        For Each ndr As DataRow In dt.Rows
            npsPoints = 0
            rphPoints = 0
            signPoints = 0

            '** calc NPS
            npsPoints = totalQualified - (ndr("npsRank") - 1)

            '** calc RPH
            rphPoints = totalQualified - (ndr("rphRank") - 1)

            '** calc sign
            signPoints = totalQualified - (ndr("signRank") - 1)

            overallPoints = npsPoints * 0.4 + rphPoints * 0.4 + signPoints * 0.2

            'update overall points
            db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[topdog] SET overall = " & overallPoints & " where [RouteManagement].[dbo].[topdog].userID = '" & ndr("userID").ToString().Trim & "'")
        Next


        'update overall points rank
        db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[topdog] SET overallRank = z.RankByOverall FROM " & _
                    "(SELECT [ID], [currentTruckHours], [overall], RANK() OVER (ORDER BY [overall] DESC) AS RankByOverall FROM [topdog] i inner join [userInfo] td on i.userID = td.id)z " & _
                    "where z.id = [RouteManagement].[dbo].[topdog].userid")



    End Sub

    Private Sub LoadTopDogDataV2()
        Dim npsPoints As Integer = 0
        Dim rphPoints As Integer = 0
        Dim ajsPoints As Integer = 0
        Dim oscPoints As Integer = 0
        Dim overallPoints As Double = 0
        Dim truckHoursMin As Integer = 0
        Dim totalQualified As Integer = 0
        Dim db As New Connection("Consumer_DSN")


        '*** GRAB TRUCK HOURS MIN ***
        Dim cmd As New SqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT [displayName] FROM [RouteManagement].[dbo].[dropDownListData] where segment = 'truckHourMin'"
        Dim sdrRevenue As SqlDataReader = db.ExecuteReader(cmd)

        If sdrRevenue.HasRows Then
            While sdrRevenue.Read
                truckHoursMin = sdrRevenue("displayName")
            End While
        End If

        'to run this report, need to run RPH and AJS reports first ...
        'LoadCurrentData(Request.QueryString("startDate"), Request.QueryString("endDate"))

        '*** GRAB TOTAL QUALIFIED ***
        cmd = New SqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT count([id]) as idCount FROM [RouteManagement].[dbo].[userInfo] where currentTruckHours >= " & truckHoursMin
        sdrRevenue = db.ExecuteReader(cmd)

        If sdrRevenue.HasRows Then
            While sdrRevenue.Read
                totalQualified = sdrRevenue("idCount")
                Exit While
            End While
        End If


        'clear the top dog table....
        db.ExecuteScaler("DELETE FROM [RouteManagement].[dbo].[topdog]")

        'insert NPS and OSC data....
        db.ExecuteScaler("INSERT INTO [RouteManagement].[dbo].[topdog] ([userID], [userName], [nps], [npsRank], [rph], [rphRank], [sign], [signRank], [overall], [overallRank], [truckHours], [osc], [oscRank], [ajs], [ajsRank]) " &
                            "SELECT id, firstName + ' ' + lastName, currentNPS, RANK() OVER (ORDER BY currentNPS DESC) AS RankByNPS, 0, 0, 0, 0, 0, 0, 0, currentOSC, RANK() OVER (ORDER BY currentOSC DESC) AS RankByOSC, 0, 0 " &
                                "FROM [RouteManagement].[dbo].[userInfo] " &
                                "WHERE [RouteManagement].[dbo].[userInfo].pseligible = 'Y' AND [RouteManagement].[dbo].[userInfo].currentTruckHours >= " & truckHoursMin)

        'update RPH data....
        db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[topdog] SET [RouteManagement].[dbo].[topdog].rph = [RouteManagement].[dbo].[userInfo].currentrph " &
                            "FROM [RouteManagement].[dbo].[topdog] INNER Join [RouteManagement].[dbo].[userInfo] ON [RouteManagement].[dbo].[topdog].userid = [RouteManagement].[dbo].[userInfo].id " &
                            "where [RouteManagement].[dbo].[userInfo].currentTruckHours >= " & truckHoursMin)

        db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[topdog] SET rphRank = z.RankByRPH FROM " &
                            "(SELECT [ID], [currentTruckHours], [rph], RANK() OVER (ORDER BY rph DESC) AS RankByRPH FROM [topdog] i inner join [userInfo] td on i.userID = td.id)z " &
                            "where z.id = [RouteManagement].[dbo].[topdog].userid")

        'update AJS data....
        db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[topdog] SET [RouteManagement].[dbo].[topdog].ajs = [RouteManagement].[dbo].[userInfo].currentAJS " &
                            "FROM [RouteManagement].[dbo].[topdog] INNER Join [RouteManagement].[dbo].[userInfo] ON [RouteManagement].[dbo].[topdog].userid = [RouteManagement].[dbo].[userInfo].id  " &
                            "where [RouteManagement].[dbo].[userInfo].currentTruckHours >= " & truckHoursMin)

        db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[topdog] SET ajsRank = z.RankByAJS FROM " &
                            "(SELECT [ID], [currentTruckHours], [ajs], RANK() OVER (ORDER BY [ajs] DESC) AS RankByAJS FROM [topdog] i inner join [userInfo] td on i.userID = td.id)z " &
                            "where z.id = [RouteManagement].[dbo].[topdog].userid")

        'update truck hours....
        db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[topdog] SET truckHours = i.currentTruckHours FROM " &
                            "(SELECT [ID], [currentTruckHours], RANK() OVER (ORDER BY currentRPH DESC) AS RankByRPH FROM [RouteManagement].[dbo].[userInfo])i " &
                            "where i.id = [RouteManagement].[dbo].[topdog].userid And i.currentTruckHours >= " & truckHoursMin)

        'calculate overall points....
        cmd = New SqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT userID, nps, npsRank, rph, rphRank, sign, signRank, overall, overallRank, truckHours, osc, oscRank, ajs, ajsRank FROM [RouteManagement].[dbo].[topdog]"
        Dim dt As New DataTable
        dt = db.FillCommand(cmd)
        For Each ndr As DataRow In dt.Rows
            npsPoints = 0
            ajsPoints = 0
            oscPoints = 0
            rphPoints = 0

            '** calc NPS
            npsPoints = totalQualified - (ndr("npsRank") - 1)

            '** calc RPH
            rphPoints = totalQualified - (ndr("rphRank") - 1)

            '** calc AJS
            ajsPoints = totalQualified - (ndr("ajsRank") - 1)

            '** calc OSC
            oscPoints = totalQualified - (ndr("oscRank") - 1)

            overallPoints = npsPoints * 0.25 + rphPoints * 0.25 + oscPoints * 0.25 * ajsPoints * 0.25

            'update overall points
            db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[topdog] SET overall = " & overallPoints & " where [RouteManagement].[dbo].[topdog].userID = '" & ndr("userID").ToString().Trim & "'")
        Next

        'update overall points rank
        db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[topdog] SET overallRank = z.RankByOverall FROM " &
                    "(SELECT [ID], [currentTruckHours], [overall], RANK() OVER (ORDER BY [overall] DESC) AS RankByOverall FROM [topdog] i inner join [userInfo] td on i.userID = td.id)z " &
                    "where z.id = [RouteManagement].[dbo].[topdog].userid")

    End Sub

    Private Sub LoadTopDogDataV3()
        Dim npsPoints As Integer = 0
        Dim rphPoints As Integer = 0
        Dim ajsPoints As Integer = 0
        Dim oscPoints As Integer = 0
        Dim overallPoints As Double = 0
        Dim truckHoursMin As Integer = 0
        Dim totalQualified As Integer = 0
        Dim db As New Connection("Consumer_DSN")


        '*** GRAB TRUCK HOURS MIN ***
        Dim cmd As New SqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT [displayName] FROM [RouteManagement].[dbo].[dropDownListData] where segment = 'truckHourMin'"
        Dim sdrRevenue As SqlDataReader = db.ExecuteReader(cmd)

        If sdrRevenue.HasRows Then
            While sdrRevenue.Read
                truckHoursMin = sdrRevenue("displayName")
            End While
        End If

        'to run this report, need to run RPH and AJS reports first ...
        'LoadCurrentData(Request.QueryString("startDate"), Request.QueryString("endDate"))

        '*** GRAB TOTAL QUALIFIED ***
        cmd = New SqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT count([id]) as idCount FROM [RouteManagement].[dbo].[userInfo] where currentTruckHours >= " & truckHoursMin
        sdrRevenue = db.ExecuteReader(cmd)

        If sdrRevenue.HasRows Then
            While sdrRevenue.Read
                totalQualified = sdrRevenue("idCount")
                Exit While
            End While
        End If


        'clear the top dog table....
        db.ExecuteScaler("DELETE FROM [RouteManagement].[dbo].[topdog]")

        'insert NPS and OSC data....
        db.ExecuteScaler("INSERT INTO [RouteManagement].[dbo].[topdog] ([userID], [userName], [nps], [npsRank], [rph], [rphRank], [sign], [signRank], [overall], [overallRank], [truckHours], [osc], [oscRank], [ajs], [ajsRank]) " &
                            "SELECT id, firstName + ' ' + lastName, currentNPS, RANK() OVER (ORDER BY currentNPS DESC) AS RankByNPS, 0, 0, 0, 0, 0, 0, 0, currentOSC, RANK() OVER (ORDER BY currentOSC DESC) AS RankByOSC, 0, 0 " &
                                "FROM [RouteManagement].[dbo].[userInfo] " &
                                "WHERE [RouteManagement].[dbo].[userInfo].pseligible = 'Y' AND [RouteManagement].[dbo].[userInfo].currentTruckHours >= " & truckHoursMin)

        'update RPH data....
        db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[topdog] SET [RouteManagement].[dbo].[topdog].rph = [RouteManagement].[dbo].[userInfo].currentrph " &
                            "FROM [RouteManagement].[dbo].[topdog] INNER Join [RouteManagement].[dbo].[userInfo] ON [RouteManagement].[dbo].[topdog].userid = [RouteManagement].[dbo].[userInfo].id " &
                            "where [RouteManagement].[dbo].[userInfo].currentTruckHours >= " & truckHoursMin)

        db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[topdog] SET rphRank = z.RankByRPH FROM " &
                            "(SELECT [ID], [currentTruckHours], [rph], RANK() OVER (ORDER BY rph DESC) AS RankByRPH FROM [topdog] i inner join [userInfo] td on i.userID = td.id)z " &
                            "where z.id = [RouteManagement].[dbo].[topdog].userid")

        'update AJS data....
        db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[topdog] SET [RouteManagement].[dbo].[topdog].ajs = [RouteManagement].[dbo].[userInfo].currentAJS " &
                            "FROM [RouteManagement].[dbo].[topdog] INNER Join [RouteManagement].[dbo].[userInfo] ON [RouteManagement].[dbo].[topdog].userid = [RouteManagement].[dbo].[userInfo].id  " &
                            "where [RouteManagement].[dbo].[userInfo].currentTruckHours >= " & truckHoursMin)

        db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[topdog] SET ajsRank = z.RankByAJS FROM " &
                            "(SELECT [ID], [currentTruckHours], [ajs], RANK() OVER (ORDER BY [ajs] DESC) AS RankByAJS FROM [topdog] i inner join [userInfo] td on i.userID = td.id)z " &
                            "where z.id = [RouteManagement].[dbo].[topdog].userid")

        'update truck hours....
        db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[topdog] SET truckHours = i.currentTruckHours FROM " &
                            "(SELECT [ID], [currentTruckHours], RANK() OVER (ORDER BY currentRPH DESC) AS RankByRPH FROM [RouteManagement].[dbo].[userInfo])i " &
                            "where i.id = [RouteManagement].[dbo].[topdog].userid And i.currentTruckHours >= " & truckHoursMin)

        'calculate overall points....
        cmd = New SqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT userID, nps, npsRank, rph, rphRank, sign, signRank, overall, overallRank, truckHours, osc, oscRank, ajs, ajsRank FROM [RouteManagement].[dbo].[topdog]"
        Dim dt As New DataTable
        dt = db.FillCommand(cmd)
        For Each ndr As DataRow In dt.Rows
            npsPoints = 0
            ajsPoints = 0
            rphPoints = 0

            '** calc NPS
            npsPoints = totalQualified - (ndr("npsRank") - 1)

            '** calc RPH
            rphPoints = totalQualified - (ndr("rphRank") - 1)

            '** calc AJS
            ajsPoints = totalQualified - (ndr("ajsRank") - 1)

            overallPoints = npsPoints * 0.33 + rphPoints * 0.33 + ajsPoints * 0.33

            'update overall points
            db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[topdog] SET overall = " & overallPoints & " where [RouteManagement].[dbo].[topdog].userID = '" & ndr("userID").ToString().Trim & "'")
        Next

        'update overall points rank
        db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[topdog] SET overallRank = z.RankByOverall FROM " &
                    "(SELECT [ID], [currentTruckHours], [overall], RANK() OVER (ORDER BY [overall] DESC) AS RankByOverall FROM [topdog] i inner join [userInfo] td on i.userID = td.id)z " &
                    "where z.id = [RouteManagement].[dbo].[topdog].userid")

    End Sub


    Private Sub LoadEmployeeMetricsData()
        Dim npsPoints As Integer = 0
        Dim rphPoints As Integer = 0
        Dim signPoints As Integer = 0
        Dim overallPoints As Double = 0
        Dim truckHoursMin As Integer = 0
        Dim totalQualified As Integer = 0
        Dim db As New Connection("Consumer_DSN")


        '*** GRAB TRUCK HOURS MIN ***
        Dim cmd As New SqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT [displayName] FROM [RouteManagement].[dbo].[dropDownListData] where segment = 'truckHoursMin30Day'"
        Dim sdrRevenue As SqlDataReader = db.ExecuteReader(cmd)

        If sdrRevenue.HasRows Then
            While sdrRevenue.Read
                truckHoursMin = sdrRevenue("displayName")
            End While
        End If

        '*** GRAB TOTAL QUALIFIED ***
        cmd = New SqlCommand
        cmd.CommandType = CommandType.Text
        'cmd.CommandText = "SELECT count([nps]) as npsCount FROM [RouteManagement].[dbo].[topdog] where truckHours >= " & truckHoursMin
        cmd.CommandText = "SELECT count([id]) as idCount FROM [RouteManagement].[dbo].[userInfo] where currentTruckHours >= " & truckHoursMin
        sdrRevenue = db.ExecuteReader(cmd)

        If sdrRevenue.HasRows Then
            While sdrRevenue.Read
                totalQualified = sdrRevenue("idCount")
            End While
        End If


        'clear the top dog table....
        db.ExecuteScaler("DELETE FROM [RouteManagement].[dbo].[employeeMetrics]")

        'update userInfo.currentTruckHours

        'insert NPS data....
        db.ExecuteScaler("INSERT INTO [RouteManagement].[dbo].[employeeMetrics] ([userID], [userName], [nps30], [npsRank30]) " &
                            "SELECT id, firstName + ' ' + lastName, currentNPS, RANK() OVER (ORDER BY currentNPS DESC) AS RankByNPS " &
                            "FROM [RouteManagement].[dbo].[userInfo] where [RouteManagement].[dbo].[userInfo].pseligible = 'Y' AND [RouteManagement].[dbo].[userInfo].currentTruckHours >= " & truckHoursMin)

        'update RPH data....
        db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[employeeMetrics] SET [RouteManagement].[dbo].[employeeMetrics].rph30 = [RouteManagement].[dbo].[userInfo].currentrph " &
                            "FROM [RouteManagement].[dbo].[employeeMetrics] INNER Join [RouteManagement].[dbo].[userInfo] ON [RouteManagement].[dbo].[employeeMetrics].userid = [RouteManagement].[dbo].[userInfo].id " &
                            "where [RouteManagement].[dbo].[userInfo].currentTruckHours >= " & truckHoursMin)

        db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[employeeMetrics] SET rphRank30 = z.RankByRPH FROM " &
                            "(SELECT [ID], [currentTruckHours], [rph30], RANK() OVER (ORDER BY rph30 DESC) AS RankByRPH FROM [employeeMetrics] i inner join [userInfo] td on i.userID = td.id)z " &
                            "where z.id = [RouteManagement].[dbo].[employeeMetrics].userid")

        'update sign data....
        db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[employeeMetrics] SET [RouteManagement].[dbo].[employeeMetrics].sign30 = [RouteManagement].[dbo].[userInfo].currentsign " &
                            "FROM [RouteManagement].[dbo].[employeeMetrics] INNER Join [RouteManagement].[dbo].[userInfo] ON [RouteManagement].[dbo].[employeeMetrics].userid = [RouteManagement].[dbo].[userInfo].id  " &
                            "where [RouteManagement].[dbo].[userInfo].currentTruckHours >= " & truckHoursMin)

        db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[employeeMetrics] SET signRank30 = z.RankBySign FROM " &
                            "(SELECT [ID], [currentTruckHours], [sign30], RANK() OVER (ORDER BY [sign30] DESC) AS RankBySign FROM [employeeMetrics] i inner join [userInfo] td on i.userID = td.id)z " &
                            "where z.id = [RouteManagement].[dbo].[employeeMetrics].userid")

        'update truck hours....
        db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[employeeMetrics] SET truckHours30 = i.currentTruckHours FROM " &
                            "(SELECT [ID], [currentTruckHours], RANK() OVER (ORDER BY currentRPH DESC) AS RankByRPH FROM [RouteManagement].[dbo].[userInfo])i " &
                            "where i.id = [RouteManagement].[dbo].[employeeMetrics].userid And i.currentTruckHours >= " & truckHoursMin)

        '*** CALCULATE OVERALL POINTS ***
        cmd = New SqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT userID, nps30, npsRank30, rph30, rphRank30, sign30, signRank30, overall30, overallRank30, truckHours30 FROM [RouteManagement].[dbo].[employeeMetrics]"
        Dim dt As New DataTable
        dt = db.FillCommand(cmd)
        For Each ndr As DataRow In dt.Rows
            npsPoints = 0
            rphPoints = 0
            signPoints = 0

            '** calc NPS
            npsPoints = totalQualified - (ndr("npsRank30") - 1)

            '** calc RPH
            rphPoints = totalQualified - (ndr("rphRank30") - 1)

            '** calc sign
            signPoints = totalQualified - (ndr("signRank30") - 1)

            overallPoints = npsPoints * 0.4 + rphPoints * 0.4 + signPoints * 0.2

            'update overall points
            db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[employeeMetrics] SET overall30 = " & overallPoints & " where [RouteManagement].[dbo].[employeeMetrics].userID = '" & ndr("userID").ToString().Trim & "'")
        Next


        'update overall points rank
        db.ExecuteScaler("UPDATE [RouteManagement].[dbo].[employeeMetrics] SET overallRank30 = z.RankByOverall FROM " &
                    "(SELECT [ID], [currentTruckHours], [overall30], RANK() OVER (ORDER BY [overall30] DESC) AS RankByOverall FROM [employeeMetrics] i inner join [userInfo] td on i.userID = td.id)z " &
                    "where z.id = [RouteManagement].[dbo].[employeeMetrics].userid")



    End Sub

    Private Function ReturnMarketingHours(sEmpID As String, startDateToUse As String, endDateToUse As String) As Decimal
        Dim dRouteUsers As Decimal = 0
        Dim dRouteTime As Decimal = 0
        Dim dTotalTime As Decimal = 0
        Dim sql As String = "Select tr.[id], [routeDate] FROM [route] tr inner join userSignIn tusi on tr.id = tusi.routeID " & _
            "where tr.routeDate >= '" & startDateToUse & "' and tr.routeDate <= dateadd(day, 1, '" & endDateToUse & "') and tusi.userID = '" & sEmpID & "'"

        Dim db As New Connection("Consumer_DSN")
        Dim dtRoutes As New DataTable
        dtRoutes = db.FillCommand(sql)
        For Each dtRoute As DataRow In dtRoutes.Rows
            dRouteTime = 0

            '** need to get # of users for this route...
            Dim cmd = New SqlCommand
            cmd.CommandText = "spGetRouteUsers"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@routeID", dtRoute("id"))

            Dim dtMktgTime = New DataTable
            dtMktgTime = db.FillWithSP(cmd)
            dRouteUsers = dtMktgTime.Rows.Count

            '** marketing for this route....
            cmd = New SqlCommand
            cmd.CommandText = "spGetMarketingByRouteID"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@routeID", dtRoute("id"))

            dtMktgTime = New DataTable
            dtMktgTime = db.FillWithSP(cmd)
            For Each dtTime As DataRow In dtMktgTime.Rows
                If Not dtTime("mktgtime") Is DBNull.Value Then
                    If dtTime("empMultiplier").ToString.ToLower = "y" Then
                        dRouteTime += dtTime("mktgtime") * dRouteUsers
                    Else
                        dRouteTime += dtTime("mktgtime")
                    End If
                End If
            Next

            '** total truck hours for this route....
            cmd = New SqlCommand
            cmd.CommandText = "spGetRouteWages"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@routeID", dtRoute("id"))

            Dim dt1 As DateTime
            Dim dt2 As DateTime
            Dim diffInDays As Double
            Dim dblTotalHours As Double = 0
            Dim dTruckHours As Decimal = 0

            Dim rs As SqlDataReader = db.ExecuteReader(cmd)
            While rs.Read
                If rs("workType").ToString.Trim.ToLower = "truck hours" Then
                    dt1 = Convert.ToDateTime(rs("endTime"))
                    dt2 = Convert.ToDateTime(rs("startTime"))
                    diffInDays = (dt1.Subtract(dt2).TotalMinutes / 60)

                    dblTotalHours += (dt1.Subtract(dt2).TotalMinutes / 60)
                End If

                If rs("workType").ToString.Trim.ToLower = "truck hours" And rs("userID").ToString.Trim.ToLower = sEmpID.Trim.ToLower Then
                    dTruckHours += (dt1.Subtract(dt2).TotalMinutes / 60)
                End If

            End While

            '** percentage spent on marketing....
            If dRouteTime > 0 And dblTotalHours > 0 Then
                dTotalTime += RoundUp(((dRouteTime / 60 / dblTotalHours) * 100)) / 100 * dTruckHours
                'dTotalTime += (dRouteTime / 60 / dblTotalHours) * dTruckHours
            End If

            ''** route time / # of route users # 60 minutes = mktg time in hours for this route
            'dRouteTime = dRouteTime / dRouteUsers / 60

            ''** add to total time
            'dTotalTime += dRouteTime
        Next
        Return dTotalTime

    End Function

    Private Function ReturnMergedTable(dt1 As DataTable, dt2 As DataTable) As DataTable
        Dim dr As DataRow
        Dim dtNew As New DataTable
        dtNew.Columns.Add("employee")
        dtNew.Columns.Add("totalHours")
        dtNew.Columns.Add("clericalHours")
        dtNew.Columns.Add("mktgHours")
        dtNew.Columns.Add("mktgRound")
        dtNew.Columns.Add("truckHours")
        dtNew.Columns.Add("truckRound")
        dtNew.Columns.Add("otTruckHours")
        dtNew.Columns.Add("basePayRate")
        dtNew.Columns.Add("basePay")
        dtNew.Columns.Add("otPayRate")
        dtNew.Columns.Add("otPay")
        dtNew.Columns.Add("bonus")
        dtNew.Columns.Add("profitShare")
        dtNew.Columns.Add("totalBonus")
        dtNew.Columns.Add("totalPay")
        dtNew.Columns.Add("adjustments")
        dtNew.Columns.Add("netPay")
        dtNew.Columns.Add("hourlyAvg")

        dOverallMktgRound = 0

        For Each dtRow As DataRow In dt1.Rows
            dr = dtNew.NewRow

            Dim foundRows As DataRow()
            foundRows = dt2.Select("employee = '" & dtRow("employee") & "'")
            If foundRows.Count > 0 Then
                dr("employee") = dtRow("employee")
                dr("totalHours") = CType(dtRow("totalHours"), Decimal) + CType(foundRows(0)("totalhours"), Decimal)
                dr("clericalHours") = CType(dtRow("clericalHours"), Decimal) + CType(foundRows(0)("clericalHours"), Decimal)
                dr("mktgHours") = CType(dtRow("mktgHours"), Decimal) + CType(foundRows(0)("mktgHours"), Decimal)
                dr("mktgRound") = RoundDown(CType(dtRow("mktgHours"), Decimal) + CType(foundRows(0)("mktgHours"), Decimal))
                dOverallMktgRound += dr("mktgRound")
                dr("truckHours") = CType(dtRow("truckHours"), Decimal) + CType(foundRows(0)("truckHours"), Decimal)
                dr("otTruckHours") = CType(dtRow("otTruckHours"), Decimal) + CType(foundRows(0)("otTruckHours"), Decimal)
                dr("truckRound") = dr("totalHours") - IIf(IsDBNull(dr("clericalHours")), 0, dr("clericalHours")) - IIf(IsDBNull(dr("mktgRound")), 0, dr("mktgRound")) - IIf(IsDBNull(dr("otTruckHours")), 0, dr("otTruckHours"))
                'dr("truckRound") = RoundDown(CType(dtRow("truckHours"), Decimal) + CType(foundRows(0)("truckHours"), Decimal))
                dr("basePayRate") = ""
                dr("basePay") = CType(dtRow("basePay"), Decimal) + CType(foundRows(0)("basePay"), Decimal)
                dr("otPayRate") = ""
                dr("otPay") = CType(dtRow("otPay"), Decimal) + CType(foundRows(0)("otPay"), Decimal)
                dr("bonus") = CType(dtRow("bonus"), Decimal) + CType(foundRows(0)("bonus"), Decimal)
                dr("profitShare") = CType(dtRow("profitShare"), Decimal) + CType(foundRows(0)("profitShare"), Decimal)
                dr("totalBonus") = CType(dtRow("totalBonus"), Decimal) + CType(foundRows(0)("totalBonus"), Decimal)
                dr("totalPay") = CType(dtRow("totalPay"), Decimal) + CType(foundRows(0)("totalPay"), Decimal)
                dr("adjustments") = CType(dtRow("adjustments"), Decimal) + CType(foundRows(0)("adjustments"), Decimal)
                dr("netPay") = CType(dtRow("netPay"), Decimal) + CType(foundRows(0)("netPay"), Decimal)
                dr("hourlyAvg") = ""
            Else
                dr("employee") = dtRow("employee")
                dr("totalHours") = CType(dtRow("totalHours"), Decimal)
                dr("clericalHours") = CType(dtRow("clericalHours"), Decimal)
                dr("mktgHours") = CType(dtRow("mktgHours"), Decimal)
                dr("mktgRound") = RoundDown(CType(dtRow("mktgHours"), Decimal))
                dOverallMktgRound += dr("mktgRound")
                dr("truckHours") = CType(dtRow("truckHours"), Decimal)
                dr("otTruckHours") = CType(dtRow("otTruckHours"), Decimal)
                dr("truckRound") = dr("totalHours") - IIf(IsDBNull(dr("clericalHours")), 0, dr("clericalHours")) - IIf(IsDBNull(dr("mktgRound")), 0, dr("mktgRound")) - IIf(IsDBNull(dr("otTruckHours")), 0, dr("otTruckHours"))
                'dr("truckRound") = RoundDown(CType(dtRow("truckHours"), Decimal))
                dr("basePayRate") = ""
                dr("basePay") = CType(dtRow("basePay"), Decimal)
                dr("otPayRate") = ""
                dr("otPay") = CType(dtRow("otPay"), Decimal)
                dr("bonus") = CType(dtRow("bonus"), Decimal)
                dr("profitShare") = CType(dtRow("profitShare"), Decimal)
                dr("totalBonus") = CType(dtRow("totalBonus"), Decimal)
                dr("totalPay") = CType(dtRow("totalPay"), Decimal)
                dr("adjustments") = CType(dtRow("adjustments"), Decimal)
                dr("netPay") = CType(dtRow("netPay"), Decimal)
                dr("hourlyAvg") = ""
            End If
            dtNew.Rows.Add(dr)
        Next

        For Each dtRow As DataRow In dt2.Rows

            Dim foundRows As DataRow()
            foundRows = dt1.Select("employee = '" & dtRow("employee") & "'")
            If foundRows.Count > 0 Then
                'already been entered.....
            Else
                dr = dtNew.NewRow
                'we're looking for the rows that are in dt1 but not found in dt2
                dr("employee") = dtRow("employee")
                dr("totalHours") = CType(dtRow("totalHours"), Decimal)
                dr("clericalHours") = CType(dtRow("clericalHours"), Decimal)
                dr("mktgHours") = CType(dtRow("mktgHours"), Decimal)
                dr("mktgRound") = RoundDown(CType(dtRow("mktgHours"), Decimal))
                dOverallMktgRound += dr("mktgRound")
                dr("truckHours") = CType(dtRow("truckHours"), Decimal)
                dr("otTruckHours") = CType(dtRow("otTruckHours"), Decimal)
                dr("truckRound") = dr("totalHours") - IIf(IsDBNull(dr("clericalHours")), 0, dr("clericalHours")) - IIf(IsDBNull(dr("mktgRound")), 0, dr("mktgRound")) - IIf(IsDBNull(dr("otTruckHours")), 0, dr("otTruckHours"))
                'dr("truckRound") = RoundDown(CType(dtRow("truckHours"), Decimal))
                dr("basePayRate") = ""
                dr("basePay") = CType(dtRow("basePay"), Decimal)
                dr("otPayRate") = ""
                dr("otPay") = CType(dtRow("otPay"), Decimal)
                dr("bonus") = CType(dtRow("bonus"), Decimal)
                dr("profitShare") = CType(dtRow("profitShare"), Decimal)
                dr("totalBonus") = CType(dtRow("totalBonus"), Decimal)
                dr("totalPay") = CType(dtRow("totalPay"), Decimal)
                dr("adjustments") = CType(dtRow("adjustments"), Decimal)
                dr("netPay") = CType(dtRow("netPay"), Decimal)
                dr("hourlyAvg") = ""
                dtNew.Rows.Add(dr)
            End If
        Next






        Return dtNew

    End Function


#End Region

End Class