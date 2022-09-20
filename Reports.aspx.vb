Imports Routes.Database
Imports System.Data.SqlClient

Partial Public Class Reports
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not CheckUser(Session("userName"), Session("password")).ToLower = MATCH Then
        '    RedirectToPage("home.aspx")
        'End If
    End Sub

    Private Sub lnkRPH_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRPH.Click
        RedirectToPage("ReportsView.aspx?rpt=rph&startDate=" & txtRPHStartDate.Text & "&endDate=" & txtRPHEndDate.Text & "&sort=rphMTD")
    End Sub

    Private Sub lnkSigns_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSigns.Click
        RedirectToPage("ReportsView.aspx?rpt=signs&startDate=" & txtSignsStarDate.Text & "&endDate=" & txtSignsEndDate.Text & "&sort=signpercentageMTD")
    End Sub

    Private Sub lnkCEC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCEC.Click
        RedirectToPage("ReportsView.aspx?rpt=cec&startDate=" & txtCECStartDate.Text & "&endDate=" & txtCECEndDate.Text)
    End Sub

    Private Sub lnkJobs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkJobs.Click
        RedirectToPage("ReportsView.aspx?rpt=jobs&startDate=" & txtJobsStartDate.Text & "&endDate=" & txtJobsEndDate.Text)
    End Sub

    Private Sub lnkPayroll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPayroll.Click
        Dim startDate As Date
        startDate = txtPayrollStartDate.Text
        RedirectToPage("ReportsView.aspx?rpt=pay&week1StartDate=" & txtPayrollStartDate.Text & "&week1EndDate=" & startDate.AddDays(6) & "&week2StartDate=" & startDate.AddDays(7) & "&week2EndDate=" & startDate.AddDays(13))
    End Sub

    Private Sub lnkTopDog_Click(sender As Object, e As System.EventArgs) Handles lnkTopDog.Click
        RedirectToPage("ReportsView.aspx?rpt=topdog&startDate=" & txtTopDogStartDate.Text & "&endDate=" & txtTopDogEndDate.Text & "&sort=overallRank")
    End Sub

    Private Sub lnkReturn_Click(sender As Object, e As EventArgs) Handles lnkReturn.Click
        RedirectToPage("landing.aspx")
    End Sub

    Private Sub lnkTips_Click(sender As Object, e As EventArgs) Handles lnkTips.Click
        RedirectToPage("ReportsView.aspx?rpt=tips&startDate=" & txtTipsStartDate.Text & "&endDate=" & txtTipsEndDate.Text)
    End Sub

    Private Sub lnkPayrollDates_Click(sender As Object, e As EventArgs) Handles lnkPayrollDates.Click
        RedirectToPage("ReportsView.aspx?rpt=paydates&startDate=" & txtPayrollDatesStart.Text & "&endDate=" & txtPayrollDatesEnd.Text)
    End Sub

    Private Sub lnkMetricsScorecard_Click(sender As Object, e As EventArgs) Handles lnkMetricsScorecard.Click
        RedirectToPage("ReportsView.aspx?rpt=metrics&startDate=" & txtMetricsAsOfDate.Text & "&sort=overallRank30")
    End Sub

    Private Sub lnkAJS_Click(sender As Object, e As EventArgs) Handles lnkAJS.Click
        RedirectToPage("ReportsView.aspx?rpt=ajs&startDate=" & txtAJSStartDate.Text & "&endDate=" & txtAJSEndDate.Text & "&sort=ajsTotal")
    End Sub

    Private Sub lnkTopDogV2_Click(sender As Object, e As EventArgs) Handles lnkTopDogV2.Click
        RedirectToPage("ReportsView.aspx?rpt=topdogv2&startDate=" & txtTopDogV2StartDate.Text & "&endDate=" & txtTopDogV2EndDate.Text & "&sort=overallRank")
    End Sub
End Class