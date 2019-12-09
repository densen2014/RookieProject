Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Interactivity


''' <summary>
''' 事件命令
''' </summary>
Public Class EventCommand
        Inherits TriggerAction(Of DependencyObject)
        Protected Overrides Sub Invoke(parameter As Object)
            If CommandParameter IsNot Nothing Then
                parameter = CommandParameter
            End If
            If Command IsNot Nothing Then
                Command.Execute(parameter)
            End If
        End Sub

        ''' <summary>
        ''' 事件
        ''' </summary>
        Public Property Command() As ICommand
            Get
                Return DirectCast(GetValue(CommandProperty), ICommand)
            End Get
            Set
                SetValue(CommandProperty, Value)
            End Set
        End Property
        Public Shared ReadOnly CommandProperty As DependencyProperty = DependencyProperty.Register("Command", GetType(ICommand), GetType(EventCommand), New PropertyMetadata(Nothing))

        ''' <summary>
        ''' 事件参数，如果为空，将自动传入事件的真实参数
        ''' </summary>
        Public Property CommandParameter() As Object
            Get
                Return DirectCast(GetValue(CommandParameterProperty), Object)
            End Get
            Set
                SetValue(CommandParameterProperty, Value)
            End Set
        End Property
        Public Shared ReadOnly CommandParameterProperty As DependencyProperty = DependencyProperty.Register("CommandParameter", GetType(Object), GetType(EventCommand), New PropertyMetadata(Nothing))
    End Class
