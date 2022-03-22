Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Class MainWindow
    Dim appSetting As New AppSettingVB

    Public Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        DataContext = appSetting

    End Sub

    Private Sub TextBox_TargetUpdated(sender As Object, e As DataTransferEventArgs)

    End Sub
End Class

Public Class AppSettingVB
    Implements System.ComponentModel.INotifyPropertyChanged
    Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler _
      Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

#Region "INotifyPropertyChanged"
    Protected Function SetProperty(Of T)(ByRef backingStore As T, value As T, <CallerMemberName> Optional propertyName As String = "", Optional onChanged As Action = Nothing, Optional force As Boolean = False) As Boolean
        If Not force AndAlso EqualityComparer(Of T).[Default].Equals(backingStore, value) Then
            Return False
        End If

        backingStore = value
        onChanged?.Invoke()
        OnPropertyChanged(propertyName)
        Return True
    End Function

    Protected Sub OnPropertyChanged(name As String)
        RaiseEvent PropertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(name))
    End Sub

    'Public Event PropertyChanged As PropertyChangedEventHandler
    'Protected Sub OnPropertyChanged(<CallerMemberName> Optional propertyName As String = "")
    '    Dim changed = PropertyChanged
    '    If changed Is Nothing Then
    '        Return
    '    End If

    '    changed.Invoke(Me, New PropertyChangedEventArgs(propertyName))
    'End Sub
#End Region

    Private _Theme As Integer
    Public Property Theme As Integer
        Get
            Return _Theme
        End Get
        Set(value As Integer)
            SetProperty(_Theme, value)
        End Set
    End Property

    Private _BackColor As String = "Black"
    Public Property BackColor As String
        Get
            Return _BackColor
        End Get
        Set(value As String)
            SetProperty(_BackColor, value)
        End Set
    End Property

    Private _ForeColor As Boolean
    Public Property ForeColor As Boolean
        Get
            Return _ForeColor
        End Get
        Set(value As Boolean)
            SetProperty(_ForeColor, value)
        End Set
    End Property

End Class
