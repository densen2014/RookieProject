Imports System.ComponentModel.DataAnnotations

Public Class NotEmptyCheck
    Inherits ValidationAttribute
    Public Overrides Function IsValid(value As Object) As Boolean
        Dim name = TryCast(value, String)
        If String.IsNullOrEmpty(name) Then
            Return False
        End If
        Return True
    End Function

    Public Overrides Function FormatErrorMessage(name As String) As String
        Return "不能为空"
    End Function
End Class

Public Class UserNameExists
    Inherits ValidationAttribute
    Public Overrides Function IsValid(value As Object) As Boolean
        Dim name = TryCast(value, String)
        If Not IsNothing(name) AndAlso name.Contains("qige") Then
            Return True
        End If
        Return False
    End Function

    Public Overrides Function FormatErrorMessage(name As String) As String
        Return "用户名必须包含qige"
    End Function
End Class

