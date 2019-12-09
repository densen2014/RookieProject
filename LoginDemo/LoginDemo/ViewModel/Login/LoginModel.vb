''' <summary>
''' 登录窗口Model
''' </summary>
Public Class LoginModel
    ''' <summary>
    ''' 用户名
    ''' </summary>
    Public Property UserName() As String
        Get
            Return m_UserName
        End Get
        Set
            m_UserName = Value
        End Set
    End Property
    Private m_UserName As String

    ''' <summary>
    ''' 密码
    ''' </summary>
    Public Property Password() As String
        Get
            Return m_Password
        End Get
        Set
            m_Password = Value
        End Set
    End Property
    Private m_Password As String

    '''' <summary>
    '''' 性别
    '''' </summary>
    'Public Property Gender() As Integer
    '    Get
    '        Return m_Gender
    '    End Get
    '    Set
    '        m_Gender = Value
    '    End Set
    'End Property
    'Private m_Gender As Integer

    ''' <summary>
    ''' 记录
    ''' </summary>

    Public Property Record() As Boolean
        Get
            Return _Record
        End Get
        Set(ByVal value As Boolean)
            _Record = value
        End Set
    End Property
    Private _Record As Boolean

    ''' <summary>
    ''' 数据填写正确
    ''' </summary>
    Public Property IsValid() As Boolean
        Get
            Return m_IsValid
        End Get
        Set
            m_IsValid = Value
        End Set
    End Property
    Private m_IsValid As Boolean

End Class

