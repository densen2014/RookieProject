Imports System.IO
Imports Newtonsoft.Json

Public Class 读写文件(Of T)
    Private 路径 As String

    Sub New(Str As String)
        路径 = Str
    End Sub
    ''' <summary>
    ''' 读取配置
    ''' </summary>
    ''' <returns></returns>
    Public Function 读取配置() As T
        If Not File.Exists(路径) Then
            创建文件()
            Return Nothing
        Else
            Dim json = File.ReadAllText(路径)
            Dim 数据 = JsonConvert.DeserializeObject(Of T)(json)
            'If Not 数据.记录 Then 数据 = New 用户DataClass
            Return 数据
        End If
    End Function

    ''' <summary>
    ''' 保存配置
    ''' </summary>
    Public Sub 保存配置(Data As T)
        Dim json = JsonConvert.SerializeObject(Data, Formatting.Indented)
        File.WriteAllText(路径, json)
    End Sub

    ''' <summary>
    ''' 创建文件
    ''' </summary>
    Private Sub 创建文件()
        '数据 = New T With {.帐号 = "", .密码 = "", .记录 = False}
        File.WriteAllText(路径, Nothing)
    End Sub
End Class
