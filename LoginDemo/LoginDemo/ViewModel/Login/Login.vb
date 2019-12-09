Imports System.Security.Cryptography
Imports System.Text
Imports System.Timers
Imports LoginDemo.DotNet.Utilities

Public Class Login
    Private ReadOnly 登录状态 As String() = {"帐号密码错误", "登录成功", "", "帐号已经到期或者停用"}
    Private WithEvents 检测时钟 As New Timer


    Public Function 登录(帐号 As String, 密码 As String)


        检测时钟.Enabled = True  '啟動控件
        检测时钟.Interval = 30 * 1000 '設定跳動頻為1秒。1000＝1秒
        Return {True, "00"}

    End Function

    Private Sub 检测Post()

    End Sub

    Private Sub 检测时钟_Tick() Handles 检测时钟.Elapsed
        '异步检测用户权限帐号
        Run()
    End Sub

    Async Sub Run()
        Await Task.Delay(300)
        检测Post()
    End Sub
End Class
