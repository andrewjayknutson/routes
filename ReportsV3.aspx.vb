Public Class ReportsV3
    Inherits Utilities

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        RedirectToPage("reportsv4.aspx")

    End Sub

    Private Sub lnkLanding_Click(sender As Object, e As EventArgs) Handles lnkLanding.Click
        RedirectToPage("landing.aspx")
    End Sub

    Private Sub lnkSubmit_Click(sender As Object, e As EventArgs) Handles lnkSubmit.Click
        Select Case radReports.SelectedIndex
            Case 0 : RedirectToPage("ReportsViewV2.aspx?rpt=jobs&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 1 : RedirectToPage("ReportsViewV2.aspx?rpt=resajs&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 2 : RedirectToPage("ReportsViewV2.aspx?rpt=resajsestimator&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 3 : RedirectToPage("ReportsViewV2.aspx?rpt=truckhours&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 4 : RedirectToPage("ReportsViewV2.aspx?rpt=grossmargin&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 5 : RedirectToPage("ReportsViewV2.aspx?rpt=dumplocationoverall&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 6 : RedirectToPage("ReportsViewV2.aspx?rpt=dumplocation&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 7 : RedirectToPage("ReportsViewV2.aspx?rpt=dumpbyguy&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 8 : RedirectToPage("ReportsViewV2.aspx?rpt=tips&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
                'Case 0 : LoadCurrentDataV2(txtStartDate.Text, txtEndDate.Text)
                'Case 1 : RedirectToPage("ReportsViewV2.aspx?rpt=jobs&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
                'Case 2 : RedirectToPage("ReportsViewV2.aspx?rpt=rph&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text & "&sort=rphMTD")
                'Case 3 : RedirectToPage("ReportsViewV2.aspx?rpt=tips&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
                'Case 4 : RedirectToPage("ReportsViewV2.aspx?rpt=ajs&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text & "&sort=ajsTotal")
                'Case 5 : RedirectToPage("ReportsViewV2.aspx?rpt=topdog&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text & "&sort=overallRank")
                'Case 6 : RedirectToPage("ReportsViewV2.aspx?rpt=paydates&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
                'Case 7 : RedirectToPage("ReportsViewV2.aspx?rpt=hourtype&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
                'Case 8 : RedirectToPage("ReportsViewV2.aspx?rpt=hourtypesummary&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
                'Case 9 : RedirectToPage("ReportsViewV2.aspx?rpt=reasonsnotconvert&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
                'Case 10 : RedirectToPage("ReportsViewV2.aspx?rpt=oscbyguy&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
                'Case 11 : RedirectToPage("ReportsViewV2.aspx?rpt=dumplocationoverall&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
                'Case 12 : RedirectToPage("ReportsViewV2.aspx?rpt=dumplocation&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
                'Case 13 : RedirectToPage("ReportsViewV2.aspx?rpt=dumpbyguy&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
                'Case 14 : RedirectToPage("ReportsViewV2.aspx?rpt=gaslocation&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
                'Case 15 : RedirectToPage("ReportsViewV2.aspx?rpt=discountsguy&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
                'Case 16 : RedirectToPage("ReportsViewV2.aspx?rpt=mattressesguy&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
                'Case 17 : RedirectToPage("ReportsViewV2.aspx?rpt=grossmargin&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
                'Case 18 : RedirectToPage("ReportsViewV2.aspx?rpt=teamajs&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
                ''Case 19 : RedirectToPage("ReportsViewV2.aspx?rpt=performancebyguy&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
                'Case 19 : RedirectToPage("ReportsViewV2.aspx?rpt=resajs&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
                'Case 20 : RedirectToPage("ReportsViewV2.aspx?rpt=truckhours&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
        End Select

    End Sub

    Private Sub lnkSubmitV2_Click(sender As Object, e As EventArgs) Handles lnkSubmitV2.Click
        Dim startDate As Date
        startDate = txtStartDateV2.Text

        Select Case radReportsV2.SelectedIndex
            Case 0 : RedirectToPage("ReportsViewV2.aspx?rpt=pay&week1StartDate=" & txtStartDateV2.Text & "&week1EndDate=" & startDate.AddDays(6) & "&week2StartDate=" & startDate.AddDays(7) & "&week2EndDate=" & startDate.AddDays(13))
            Case 1 : RedirectToPage("ReportsViewV2.aspx?rpt=dailyroute&startDate=" & txtStartDateV2.Text & "&endDate=" & txtEndDate.Text)
        End Select

    End Sub

End Class