Densen 的 Winforms 常用扩展库

### DataGridView

|  列控件   | 说明  | 参数  |
|  ----  | ----  |
| DataGridViewCalendarColumn  | 日期编辑列 | |
| DataGridViewIntegerColumn | 整数编辑列 | |
| DataGridViewNumericColumn | 数字编辑列,默认两位小数 | ThousandsSeparator,DecimalPlaces,Minimum,Maximum |
| DataGridViewPasswordColumn | 密码编辑列 | UsePasswordCharWhenEditing |
| DataGridViewRolloverCellColumn | 鼠标焦点变色列 | |

示例:

https://github.com/densen2014/RookieProject/tree/master/DgvValidatingData


|  扩展   | 说明  | 参数  | 示例 |
|  ----  | ----  |
|  Bind<T>(this DataGridView grid, IList<T> data, bool autoGenerateColumns = true) | 渲染生成列 | | 示例:dataGridView1.Bind(list) |

### RDLC

LocalReport类创建了一个扩展方法，调用Print它可以更轻松地打印报表而不显示报表或任何对话框：

它有两个重载：

- Print()：它使用报告的默认页面设置。

- Print(PageSettings)：它使用传递给方法的页面设置对象。

例如，您可以轻松地以这种方式使用它：

this.reportViewer1.LocalReport.Print();


#### 参考资料来源

1. How to: Customize Cells and Columns in the Windows Forms DataGridView Control by Extending Their Behavior and Appearance
   
   https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/customize-cells-and-columns-in-the-datagrid-by-extending-behavior?view=netframeworkdesktop-4.8

2. Reza Aghaei  

   https://www.reza-aghaei.com/