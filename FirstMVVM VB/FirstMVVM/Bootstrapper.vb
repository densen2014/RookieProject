Imports Caliburn.Micro

Public Class Bootstrapper
    Inherits BootstrapperBase
    Public Sub New()
        Initialize()
    End Sub
    Protected Overrides Sub OnStartup(sender As Object, e As StartupEventArgs)
        DisplayRootViewFor(Of FirstViewModel)()
    End Sub
End Class

