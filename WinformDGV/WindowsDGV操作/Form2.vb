' ********************************** 
' Densen Informatica 中讯科技 
' 作者：Alex Chow
' e-mail:zhouchuanglin@gmail.com 
' **********************************

Public Class Form2
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form1.Show()
    End Sub


    Public Sub SUM(ByVal dt As DataTable)

        If dt IsNot Nothing Then
            For Each item In dt.Rows
                DSkinDataGridView1.Rows.Add(item(1).ToString, item(1).ToString, item(1).ToString)
            Next
        End If


    End Sub

End Class
