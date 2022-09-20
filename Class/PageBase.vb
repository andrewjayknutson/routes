Imports System.Data
Imports System.Data.SqlClient
Imports Routes.Database
Imports System.Configuration.ConfigurationSettings

Public Class PageBase
    Inherits System.Web.UI.Page

    Public Const MATCH As String = "match"
    Public Const NO_CEC As String = "0% CEC - Not Collected"
    Public Const CEC_RESIDENTIAL As String = "Residential"
    Public Const CEC_COMMERCIAL As String = "Commercial"
    Public Const HOME_PAGE As String = "home2.aspx"
    Public Const MATTRESS_STRIPPERS = "michael ruch,calvin corey,jamel staten,steve xiong,marcel white,marvin stewart,chris lemm,tim rance,gary coleman,bernardo flores,jean mayas"
    Public Const POINT_ADDITIONAL_WAGES = "jacob janisch,douglas kalmbacher"

    Public Function CheckUser(ByVal userName As String, ByVal password As String) As String
        If String.IsNullOrEmpty(userName) Or String.IsNullOrEmpty(password) Then
            Return "Invalid Username...please try again"
        End If

        'Dim db As New Connection("Consumer_DSN")
        'Dim cmd As New SqlCommand
        'cmd.CommandText = "spGetUserByUsername"
        'cmd.CommandType = CommandType.StoredProcedure
        'cmd.Parameters.AddWithValue("@username", userName)
        'Dim rs As SqlDataReader = db.ExecuteReader(cmd)
        'If rs.HasRows And rs.Read Then
        '    If password = rs("password").ToString.Trim Then
        '        Return MATCH
        '    Else
        '        'password doesn't match
        '        Return "Invalid Password...please try again"
        '    End If
        'Else
        '    'no user name
        '    Return "Invalid Username...please try again"
        'End If

        Dim db As New Connection("Consumer_DSN")
        Dim cmd As New SqlCommand
        cmd.CommandText = "spGetUserByUsername"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@username", userName)

        Dim dt As DataTable = db.FillWithSP(cmd)
        If dt.Rows.Count <= 0 Then
            Return "Invalid Username...please try again"

        Else
            If dt.Rows(0)(10).ToString().Trim().ToLower() = password.Trim().ToLower() Then
                Return MATCH
            Else
                Return "Invalid Password...please try again"
            End If
        End If

    End Function

    Public Function ReturnSubtractionValue(ByVal val1 As String, ByVal val2 As String) As Double

        Try
            If String.IsNullOrEmpty(val1) Then
                val1 = 0
            End If
            If String.IsNullOrEmpty(val2) Then
                val2 = 0
            End If

            Return val1 - val2
        Catch ex As Exception
            Return 0

        End Try

    End Function

    Public Function ReturnAdditionValue(ByVal val1 As String, ByVal val2 As String) As Double

        Try
            If String.IsNullOrEmpty(val1) Then
                val1 = 0
            End If
            If String.IsNullOrEmpty(val2) Then
                val2 = 0
            End If
            Return val1 + val2
        Catch ex As Exception
            Return 0

        End Try

    End Function

    Public Function ReturnCurrencyValue(ByVal valueToChange As String)
        Try
            If Not String.IsNullOrEmpty(valueToChange) Then
                Return FormatCurrency(valueToChange, 2)
            End If
        Catch ex As Exception
            Return valueToChange

        End Try
    End Function

    Public Function ReturnDecimalValue(ByVal valueToChange As String)
        Try
            If Not String.IsNullOrEmpty(valueToChange) Then
                Return FormatNumber(valueToChange, 2)
            End If
        Catch ex As Exception
            Return valueToChange

        End Try
    End Function

    Public Function ReturnGrossMargin(Revenue As Double, Wages As Double, Dumps As Double)
        Dim GM As Double = 0
        Try
            GM = (Revenue - (Wages + Dumps)) / Revenue
        Catch ex As Exception
            Return GM
        End Try
        Return GM
    End Function

    Public Sub RedirectToPage(PageURL As String)
        'If Request.Url.Host.ToLower = "localhost" Then
        Response.Redirect("/" & AppSettings("urlRoute") & PageURL)
        'Else
        '    Response.Redirect("/routes/" & PageURL)
        'End If
    End Sub

    Public Function ReturnRoundedValue(BaseValue As String) As String
        Dim x As String
        Dim ResultValue As Double

        Try
            Double.TryParse(BaseValue, ResultValue)
            x = Math.Round(ResultValue, 2).ToString("F")
        Catch ex As Exception
            x = "0.00"
        End Try

        Return x

    End Function


End Class
