Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations

Public MustInherit Class NotificationObject
    Implements INotifyPropertyChanged, IDataErrorInfo
#Region "属性修改通知"

    Private Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    ''' <summary>
    ''' 发起通知
    ''' </summary>
    ''' <param name="propertyName">属性名</param>
    Public Sub RaisePropertyChanged(ByVal propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub
#End Region
#Region "数据验证"

    Public ReadOnly Property [Error]() As String Implements IDataErrorInfo.Error
        Get
            Return ""
        End Get
    End Property

    Default Public ReadOnly Property Item(columnName As String) As String Implements IDataErrorInfo.Item
        Get
            Dim vc = New ValidationContext(Me, Nothing, Nothing)
            vc.MemberName = columnName
            Dim res = New List(Of ValidationResult)()
            Dim result = Validator.TryValidateProperty(Me.[GetType]().GetProperty(columnName).GetValue(Me, Nothing), vc, res)
            If res.Count > 0 Then
                Return String.Join(Environment.NewLine, res.[Select](Function(r) r.ErrorMessage).ToArray())
            End If
            Return String.Empty
        End Get
    End Property

    ''' <summary>
    ''' 页面中是否所有控制数据验证正确
    ''' </summary>
    Public Overridable Property IsValid() As Boolean
        Get
            Return m_IsValid
        End Get
        Set
            m_IsValid = Value
        End Set
    End Property
    Private m_IsValid As Boolean


#End Region
End Class