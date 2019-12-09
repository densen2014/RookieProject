Imports System.Globalization
Imports System.Windows.Data


Public Class CheckConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
        If value Is Nothing OrElse parameter Is Nothing Then
            Return False
        End If
        Dim checkvalue As String = value.ToString()
        Dim targetvalue As String = parameter.ToString()
        Dim r As Boolean = checkvalue.Equals(targetvalue)
        Return r
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
        If value Is Nothing OrElse parameter Is Nothing Then
            Return Nothing
        End If

        If CBool(value) Then
            Return parameter.ToString()
        End If
        Return Nothing
    End Function
End Class
