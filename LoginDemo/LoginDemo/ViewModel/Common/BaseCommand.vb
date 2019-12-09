''' <summary>
''' 命令基类
''' </summary>
Public Class BaseCommand
    Implements ICommand

    'Public Custom Event CanExecuteChanged As EventHandler
    '    AddHandler(ByVal value As EventHandler)
    '        If _canExecute IsNot Nothing Then
    '            CommandManager.RequerySuggested += value
    '        End If
    '    End AddHandler
    '    RemoveHandler(ByVal value As EventHandler)
    '        If _canExecute IsNot Nothing Then
    '            CommandManager.RequerySuggested -= value
    '        End If
    '    End RemoveHandler
    'End Event

    Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
        If _canExecute Is Nothing Then
            Return True
        End If
        '------------
        RaiseEvent ICommand_CanExecuteChanged(Me, New EventArgs)
        '------------
        Return _canExecute(parameter)
    End Function

    Public Sub Execute(parameter As Object) Implements ICommand.Execute
        If _execute IsNot Nothing AndAlso CanExecute(parameter) Then
            _execute(parameter)
        End If
    End Sub

    Private _canExecute As Func(Of Object, Boolean)
    Private _execute As Action(Of Object)
    Private Event ICommand_CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

    Public Sub New(execute As Action(Of Object), canExecute As Func(Of Object, Boolean))
        _execute = execute
        _canExecute = canExecute
    End Sub

    Public Sub New(execute As Action(Of Object))
        Me.New(execute, Nothing)
    End Sub


End Class

