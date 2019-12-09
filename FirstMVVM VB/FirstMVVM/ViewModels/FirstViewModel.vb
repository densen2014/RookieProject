Imports Caliburn.Micro

Public Class FirstViewModel
    Inherits Screen
    Private _FirtName As String
    Public Property FirtName() As String
        Get
            Return _FirtName
        End Get
        Set(ByVal value As String)
            _FirtName = value
            NotifyOfPropertyChange(Function() FirtName)
            NotifyOfPropertyChange(Function() FullName)
        End Set
    End Property

    Private _SecondName As String
    Public Property SecondName() As String
        Get
            Return _SecondName
        End Get
        Set(ByVal value As String)
            _SecondName = value
            NotifyOfPropertyChange(Function() SecondName)
            NotifyOfPropertyChange(Function() FullName)
        End Set
    End Property

    Private _FullName As String
    Public Property FullName() As String
        Get
            Return FirtName + SecondName
        End Get
        Set()

        End Set
    End Property
End Class
