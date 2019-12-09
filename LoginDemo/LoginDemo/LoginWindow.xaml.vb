Public Class LoginWindow
    Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        Me.DataContext = New LoginViewModel()

        WindowManager.Register(Of MainWindow)("MainWindow")
    End Sub

    'Private Sub WindowBehavior_AccessKeyPressed(sender As Object, e As AccessKeyPressedEventArgs)

    'End Sub
End Class
