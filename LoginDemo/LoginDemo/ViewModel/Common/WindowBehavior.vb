Imports System.Windows
Imports System.Windows.Interactivity


''' <summary>
''' 窗口行为
''' </summary>
Public Class WindowBehavior
        Inherits Behavior(Of Window)
        ''' <summary>
        ''' 关闭窗口
        ''' </summary>
        Public Property Close() As Boolean
            Get
                Return CBool(GetValue(CloseProperty))
            End Get
            Set
                SetValue(CloseProperty, Value)
            End Set
        End Property
        Public Shared ReadOnly CloseProperty As DependencyProperty = DependencyProperty.Register("Close", GetType(Boolean), GetType(WindowBehavior), New PropertyMetadata(False, AddressOf OnCloseChanged))
        Private Shared Sub OnCloseChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
            Dim window = DirectCast(d, WindowBehavior).AssociatedObject
            Dim newValue = CBool(e.NewValue)
            If newValue Then
                window.Close()
            End If
        End Sub

    End Class

