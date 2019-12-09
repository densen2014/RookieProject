Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Interactivity

''' <summary>
''' 增加Password扩展属性
''' </summary>
Public NotInheritable Class PasswordBoxHelper
        Private Sub New()
        End Sub
        Public Shared Function GetPassword(obj As DependencyObject) As String
            Return DirectCast(obj.GetValue(PasswordProperty), String)
        End Function

        Public Shared Sub SetPassword(obj As DependencyObject, value As String)
            obj.SetValue(PasswordProperty, value)
        End Sub

        Public Shared ReadOnly PasswordProperty As DependencyProperty = DependencyProperty.RegisterAttached("Password", GetType(String), GetType(PasswordBoxHelper), New PropertyMetadata("", AddressOf OnPasswordPropertyChanged))

        Private Shared Sub OnPasswordPropertyChanged(sender As DependencyObject, e As DependencyPropertyChangedEventArgs)
            Dim box As PasswordBox = TryCast(sender, PasswordBox)
            Dim password As String = DirectCast(e.NewValue, String)
            If box IsNot Nothing AndAlso box.Password <> password Then
                box.Password = password
            End If
        End Sub
    End Class

    ''' <summary>
    ''' 接收PasswordBox的密码修改事件
    ''' </summary>
    Public Class PasswordBoxBehavior
        Inherits Behavior(Of PasswordBox)
        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()

            AddHandler AssociatedObject.PasswordChanged, AddressOf OnPasswordChanged
        End Sub

        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()

            RemoveHandler AssociatedObject.PasswordChanged, AddressOf OnPasswordChanged
        End Sub

        Private Shared Sub OnPasswordChanged(sender As Object, e As RoutedEventArgs)
            Dim box As PasswordBox = TryCast(sender, PasswordBox)
            Dim password As String = PasswordBoxHelper.GetPassword(box)
            If box IsNot Nothing AndAlso box.Password <> password Then
                PasswordBoxHelper.SetPassword(box, box.Password)
            End If
        End Sub
    End Class
