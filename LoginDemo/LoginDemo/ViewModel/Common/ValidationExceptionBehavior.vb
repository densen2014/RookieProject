Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Interactivity



''' <summary>
''' 验证异常行为
''' </summary>
Public Class ValidationExceptionBehavior
        Inherits Behavior(Of FrameworkElement)
        ''' <summary>
        ''' 记录异常的数量
        ''' </summary>
        ''' <remarks>在一个页面里面，所有控件的验证错误信息都会传到这个类上，每个控制需不需要显示验证错误，需要分别记录</remarks>
        Private ExceptionCount As Dictionary(Of UIElement, Integer)
        ''' <summary>
        ''' 缓存页面的提示装饰器
        ''' </summary>
        Private AdornerDict As Dictionary(Of UIElement, NotifyAdorner)

        Protected Overrides Sub OnAttached()
            ExceptionCount = New Dictionary(Of UIElement, Integer)()
            AdornerDict = New Dictionary(Of UIElement, NotifyAdorner)()

            Me.AssociatedObject.[AddHandler](Validation.ErrorEvent, New EventHandler(Of ValidationErrorEventArgs)(AddressOf OnValidationError))
        End Sub

        ''' <summary>
        ''' 当验证错误信息改变时，首先调用此函数
        ''' </summary>
        Private Sub OnValidationError(sender As Object, e As ValidationErrorEventArgs)
            Try
                Dim handler = GetValidationExceptionHandler()
                '插入<c:ValidationExceptionBehavior></c:ValidationExceptionBehavior>此语句的窗口的DataContext，也就是ViewModel
                Dim element = TryCast(e.OriginalSource, UIElement)
                '错误信息发生改变的控件
                If handler Is Nothing OrElse element Is Nothing Then
                    Return
                End If

                If e.Action = ValidationErrorEventAction.Added Then
                    If ExceptionCount.ContainsKey(element) Then
                        ExceptionCount(element) += 1
                    Else
                        ExceptionCount.Add(element, 1)
                    End If
                ElseIf e.Action = ValidationErrorEventAction.Removed Then
                    If ExceptionCount.ContainsKey(element) Then
                        ExceptionCount(element) -= 1
                    Else
                        ExceptionCount.Add(element, -1)
                    End If
                End If

                If ExceptionCount(element) <= 0 Then
                    HideAdorner(element)
                Else
                    ShowAdorner(element, e.[Error].ErrorContent.ToString())
                End If

                Dim TotalExceptionCount As Integer = 0
                For Each kvp As KeyValuePair(Of UIElement, Integer) In ExceptionCount
                    TotalExceptionCount += kvp.Value
                Next

                'ViewModel里面的IsValid
                handler.IsValid = (TotalExceptionCount <= 0)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        ''' <summary>
        ''' 获得行为所在窗口的DataContext
        ''' </summary>
        Private Function GetValidationExceptionHandler() As NotificationObject
            If TypeOf Me.AssociatedObject.DataContext Is NotificationObject Then
                Dim handler = TryCast(Me.AssociatedObject.DataContext, NotificationObject)

                Return handler
            End If

            Return Nothing
        End Function

        ''' <summary>
        ''' 显示错误信息提示
        ''' </summary>
        Private Sub ShowAdorner(element As UIElement, errorMessage As String)
            If AdornerDict.ContainsKey(element) Then
                AdornerDict(element).ChangeToolTip(errorMessage)
            Else
                Dim adornerLayer__1 = AdornerLayer.GetAdornerLayer(element)
                Dim adorner As New NotifyAdorner(element, errorMessage)
                adornerLayer__1.Add(adorner)
                AdornerDict.Add(element, adorner)
            End If
        End Sub

        ''' <summary>
        ''' 隐藏错误信息提示
        ''' </summary>
        Private Sub HideAdorner(element As UIElement)
            If AdornerDict.ContainsKey(element) Then
                Dim adornerLayer__1 = AdornerLayer.GetAdornerLayer(element)
                adornerLayer__1.Remove(AdornerDict(element))
                AdornerDict.Remove(element)
            End If
        End Sub
    End Class
