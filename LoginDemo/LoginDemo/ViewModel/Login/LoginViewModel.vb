

Public Class LoginViewModel
    Inherits NotificationObject

    Private obj As New LoginModel()
    Private ReadOnly 路径 As String = "LoginData.txt"
    Private ReadOnly 文件读写 As New 读写文件(Of LoginModel)(路径)
    Public Sub New()
        读取()
        'obj.UserName = "test"
        'Record = True
    End Sub

    Private Sub 读取()
        Dim Data = 文件读写.读取配置()
        If Not IsNothing(Data) Then obj = Data
    End Sub

    Private Sub 保存()
        If Record Then 文件读写.保存配置(obj)
    End Sub

    Private Sub 登录()
        Dim Login As New Login
        Dim 接收 = Login.登录(UserName, Password)
        If Not IsNothing(接收) AndAlso 接收(0) Then
            WindowManager.Show("MainWindow", New VipViewModel())
            ToClose = 接收(0)
        ElseIf Not IsNothing(接收) Then
            MsgBox(接收(1))
        End If

    End Sub

    ''' <summary>
    ''' 用户名
    ''' </summary>
    <NotEmptyCheck>
    <UserNameExists>
    Public Property UserName() As String
        Get
            Return obj.UserName
        End Get
        Set
            obj.UserName = Value
            Me.RaisePropertyChanged("UserName")
        End Set
    End Property

    ''' <summary>
    ''' 密码
    ''' </summary>
    <NotEmptyCheck>
    Public Property Password() As String
        Get
            Return obj.Password
        End Get
        Set
            obj.Password = Value
            Me.RaisePropertyChanged("Password")
        End Set
    End Property

    '''' <summary>
    '''' 性别
    '''' </summary>
    'Public Property Gender() As Integer
    '    Get
    '        Return obj.Gender
    '    End Get
    '    Set
    '        obj.Gender = Value
    '        Me.RaisePropertyChanged("Gender")
    '    End Set
    'End Property

    ''' <summary>
    ''' 记录
    ''' </summary>
    Public Property Record() As Boolean
        Get
            Return obj.Record
        End Get
        Set
            obj.Record = Value
            Me.RaisePropertyChanged("Record")
        End Set
    End Property

    Private m_toClose As Boolean = False
    ''' <summary>
    ''' 是否要关闭窗口
    ''' </summary>
    Public Property ToClose() As Boolean
        Get
            Return m_toClose
        End Get
        Set
            m_toClose = Value
            If m_toClose Then
                Me.RaisePropertyChanged("ToClose")
            End If
        End Set
    End Property

    ''' <summary>
    ''' 数据填写正确
    ''' </summary>
    Public Overrides Property IsValid() As Boolean
        Get
            Return obj.IsValid
        End Get
        Set
            If Value = obj.IsValid Then
                Return
            End If
            obj.IsValid = Value
            Me.RaisePropertyChanged("IsValid")
        End Set
    End Property

    Private m_loginClick As BaseCommand
    ''' <summary>
    ''' 登录事件
    ''' </summary>
    Public ReadOnly Property LoginClick() As BaseCommand
        Get
            If m_loginClick Is Nothing Then
                m_loginClick = New BaseCommand(
                    New Action(Of Object)(Sub(o)
                                              '执行登录逻辑
                                              保存()
                                              登录()
                                          End Sub))
            End If
            Return m_loginClick
        End Get
    End Property
End Class
