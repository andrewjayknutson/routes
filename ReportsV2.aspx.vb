Public Class ReportsV2
    Inherits Utilities

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        RedirectToPage("reportsv4.aspx")

    End Sub

    Private Sub lnkLanding_Click(sender As Object, e As EventArgs) Handles lnkLanding.Click
        RedirectToPage("landing.aspx")
    End Sub

    Private Sub lnkSubmit_Click(sender As Object, e As EventArgs) Handles lnkSubmit.Click
        Select Case radReports.SelectedIndex
            Case 0 : LoadCurrentData(txtStartDate.Text, txtEndDate.Text)
            Case 1 : RedirectToPage("ReportsView.aspx?rpt=cec&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 2 : RedirectToPage("ReportsView.aspx?rpt=jobs&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 3 : RedirectToPage("ReportsView.aspx?rpt=rph&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text & "&sort=rphMTD")
            Case 4 : RedirectToPage("ReportsView.aspx?rpt=tips&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 5 : RedirectToPage("ReportsView.aspx?rpt=ajs&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text & "&sort=ajsTotal")
            Case 6 : RedirectToPage("ReportsView.aspx?rpt=topdog&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text & "&sort=overallRank")
            Case 7 : RedirectToPage("ReportsView.aspx?rpt=paydates&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 8 : RedirectToPage("ReportsView.aspx?rpt=signs&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text & "&sort=signpercentageMTD")
            Case 9 : RedirectToPage("ReportsView.aspx?rpt=hourtype&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 10 : RedirectToPage("ReportsView.aspx?rpt=reasonsnotconvert&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 11 : RedirectToPage("ReportsView.aspx?rpt=oscbyguy&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 12 : RedirectToPage("ReportsView.aspx?rpt=dumplocation&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 13 : RedirectToPage("ReportsView.aspx?rpt=dumpbyguy&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 14 : RedirectToPage("ReportsView.aspx?rpt=gaslocation&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 15 : RedirectToPage("ReportsView.aspx?rpt=discountsguy&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
        End Select

    End Sub

    Private Sub lnkSubmitV2_Click(sender As Object, e As EventArgs) Handles lnkSubmitV2.Click
        Dim startDate As Date
        startDate = txtStartDateV2.Text

        Select Case radReportsV2.SelectedIndex
            Case 0 : RedirectToPage("ReportsView.aspx?rpt=pay&week1StartDate=" & txtStartDateV2.Text & "&week1EndDate=" & startDate.AddDays(6) & "&week2StartDate=" & startDate.AddDays(7) & "&week2EndDate=" & startDate.AddDays(13))
            Case 1 : RedirectToPage("ReportsView.aspx?rpt=dailyroute&startDate=" & txtStartDateV2.Text & "&endDate=" & txtEndDate.Text)
        End Select

    End Sub
End Class