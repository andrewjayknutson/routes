Public Class ReportsV4
    Inherits Utilities

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub lnkLanding_Click(sender As Object, e As EventArgs) Handles lnkLanding.Click
        RedirectToPage("start.aspx")
    End Sub

    Private Sub lnkSubmit_Click(sender As Object, e As EventArgs) Handles lnkSubmit.Click
        Select Case radReports.SelectedIndex
            Case 0 : RedirectToPage("ReportsViewV4.aspx?rpt=jobs&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 1 : RedirectToPage("ReportsViewV4.aspx?rpt=jobs_east&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 2 : RedirectToPage("ReportsViewV4.aspx?rpt=jobs_west&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 3 : RedirectToPage("ReportsViewV4.aspx?rpt=jobs_employee&sort=jobs&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 4 : RedirectToPage("ReportsViewV4.aspx?rpt=resajs&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 5 : RedirectToPage("ReportsViewV4.aspx?rpt=resajsestimator&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 6 : RedirectToPage("ReportsViewV4.aspx?rpt=grossmargin&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 7 : RedirectToPage("ReportsViewV4.aspx?rpt=tips&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 8 : RedirectToPage("ReportsViewV4.aspx?rpt=rph&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 9 : RedirectToPage("ReportsViewV4.aspx?rpt=hours&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 10 : RedirectToPage("ReportsViewV4.aspx?rpt=bonus&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 11 : RedirectToPage("ReportsViewV4.aspx?rpt=duplicate&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
            Case 12 : RedirectToPage("ReportsViewV4.aspx?rpt=revajs&startDate=" & txtStartDate.Text & "&endDate=" & txtEndDate.Text)
        End Select

    End Sub

    Private Sub lnkSubmitV2_Click(sender As Object, e As EventArgs) Handles lnkSubmitV2.Click
        Dim startDate As Date
        startDate = txtStartDateV2.Text

        Select Case radReportsV2.SelectedIndex
            Case 0 : RedirectToPage("ReportsViewV4.aspx?rpt=pay&week1StartDate=" & txtStartDateV2.Text & "&week1EndDate=" & startDate.AddDays(6) & "&week2StartDate=" & startDate.AddDays(7) & "&week2EndDate=" & startDate.AddDays(13))
        End Select

    End Sub

End Class