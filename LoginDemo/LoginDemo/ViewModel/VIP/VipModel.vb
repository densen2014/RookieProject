Public Class VipModel
    Private _勾选 As Boolean
    Public Property 勾选() As Boolean
        Get
            Return _勾选
        End Get
        Set(ByVal value As Boolean)
            _勾选 = value
        End Set
    End Property
    Private _序号 As Integer
    Public Property 序号() As Integer
        Get
            Return _序号
        End Get
        Set(ByVal value As Integer)
            _序号 = value
        End Set
    End Property
    Private _帐号 As String
    Public Property 帐号() As String
        Get
            Return _帐号
        End Get
        Set(ByVal value As String)
            _帐号 = value
        End Set
    End Property
    Private _密码 As String
    Public Property 密码() As String
        Get
            Return _密码
        End Get
        Set(ByVal value As String)
            _密码 = value
        End Set
    End Property
    Private _积分 As Integer
    Public Property 积分() As Integer
        Get
            Return _积分
        End Get
        Set(ByVal value As Integer)
            _积分 = value
        End Set
    End Property
End Class
