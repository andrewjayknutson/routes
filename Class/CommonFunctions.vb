Imports System.Data
Imports System.Data.SqlClient
Imports Routes.Database

Public Class CommonFunctions
    Inherits PageBase

    Public Function ReturnTotalRevenue(startDate As String, endDate As String) As Double
        Dim db As New Connection("Consumer_DSN")
        Dim ds As New DataSet

        Dim cmd As New SqlCommand
        Try
            Dim returnResult As Double = 0
            cmd.CommandText = "spAutomated_GetRevByDates"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@startDate", startDate)
            cmd.Parameters.AddWithValue("@endDate", endDate)

            Dim dtCEC As New DataTable
            dtCEC = db.FillWithSP(cmd)
            For Each dtr As DataRow In dtCEC.Rows
                returnResult = dtr("totalRev")
            Next

            Return returnResult

        Catch ex As Exception
            Return 0

        Finally
            cmd.Dispose()

        End Try


    End Function

End Class
