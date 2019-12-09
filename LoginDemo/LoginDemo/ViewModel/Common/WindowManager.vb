Imports System.Collections
Imports System.Windows


''' <summary>
''' 窗口管理器
''' </summary>
Public NotInheritable Class WindowManager
    Private Sub New()
    End Sub
    Private Shared _RegisterWindow As New Hashtable()

    Public Shared Sub Register(Of T)(key As String)
        If Not _RegisterWindow.Contains(key) Then
            _RegisterWindow.Add(key, GetType(T))
        End If
    End Sub

    Public Shared Sub Register(key As String, t As Type)
        If Not _RegisterWindow.Contains(key) Then
            _RegisterWindow.Add(key, t)
        End If
    End Sub

    Public Shared Sub Remove(key As String)
        If _RegisterWindow.ContainsKey(key) Then
            _RegisterWindow.Remove(key)
        End If
    End Sub

    Public Shared Sub Show(key As String, VM As Object)
        If _RegisterWindow.ContainsKey(key) Then
            Dim win = DirectCast(Activator.CreateInstance(DirectCast(_RegisterWindow(key), Type)), Window)
            win.DataContext = VM
            win.Show()
        End If
    End Sub
End Class

