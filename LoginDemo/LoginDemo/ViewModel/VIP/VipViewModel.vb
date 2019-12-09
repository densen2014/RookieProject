Imports System.Reflection

Public Class VipViewModel
    Inherits NotificationObject
    Property GroupList As List(Of TabItem)
    Private Grid As New DataGrid
    Private GridData As List(Of VipModel)
    Sub New()

        GridDataAdd()
        GridLoed()

        GroupList = New List(Of TabItem) From
        {
            New TabItem With
            {
            .Header = "乐园",
            .Content = Grid
            },
            New TabItem With
            {.Header = "香烟",
            .Content = Grid
            }
        }
    End Sub
    ''' <summary>
    '''  初始化Grid
    ''' </summary>
    Private Sub GridLoed()
        With Grid
            .AutoGenerateColumns = False
            .ItemsSource = GridData
            .CanUserAddRows = False
            .IsReadOnly = True
        End With

        Dim GridDataColumn = New List(Of Object) From
        {
            New DataGridCheckBoxColumn With {.Header = "勾选", .Binding = New Binding("勾选") With {.Mode = BindingMode.OneWay}, .Width = 40, .IsReadOnly = True},
            New DataGridTextColumn With {.Header = "序号", .Binding = New Binding("序号") With {.Mode = BindingMode.OneWay}, .Width = 40, .IsReadOnly = True},
            New DataGridTextColumn With {.Header = "帐号", .Binding = New Binding("帐号") With {.Mode = BindingMode.OneWay}, .Width = 100, .IsReadOnly = True},
            New DataGridTextColumn With {.Header = "密码", .Binding = New Binding("密码") With {.Mode = BindingMode.OneWay}, .Width = 100, .IsReadOnly = True},
            New DataGridTextColumn With {.Header = "积分", .Binding = New Binding("积分") With {.Mode = BindingMode.OneWay}, .Width = 100, .IsReadOnly = True}
        }

        For Each T In GridDataColumn
            Grid.Columns.Add(T)
        Next

    End Sub
    ''' <summary>
    ''' 填充Grid数据
    ''' </summary>
    Private Sub GridDataAdd()
        GridData = New List(Of VipModel) From
        {
            New VipModel With {.勾选 = True, .序号 = 1, .帐号 = "qige01", .密码 = "qwe123", .积分 = 1000},
            New VipModel With {.勾选 = False, .序号 = 2, .帐号 = "qige02", .密码 = "qwe123", .积分 = 800},
            New VipModel With {.勾选 = True, .序号 = 3, .帐号 = "qige03", .密码 = "qwe123", .积分 = 700},
            New VipModel With {.勾选 = True, .序号 = 4, .帐号 = "qige04", .密码 = "qwe123", .积分 = 900}
        }
    End Sub


    ''' <summary>
    ''' SelectionChanged事件
    ''' </summary>
    Private _ChangedClick As BaseCommand
    Public ReadOnly Property ChangedClick() As BaseCommand
        Get
            If _ChangedClick Is Nothing Then
                _ChangedClick = New BaseCommand(
                    New Action(Of Object)(Sub(o)
                                              '执行登录逻辑

                                          End Sub))
            End If
            Return _ChangedClick
        End Get
    End Property
End Class
