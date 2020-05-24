<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.VScrollBar1 = New System.Windows.Forms.VScrollBar()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(64, 31)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(163, 81)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "REad"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(76, 180)
        Me.TextBox1.MaxLength = 0
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(1044, 750)
        Me.TextBox1.TabIndex = 1
        '
        'VScrollBar1
        '
        Me.VScrollBar1.Location = New System.Drawing.Point(1140, 180)
        Me.VScrollBar1.Name = "VScrollBar1"
        Me.VScrollBar1.Size = New System.Drawing.Size(83, 749)
        Me.VScrollBar1.TabIndex = 2
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(284, 60)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(99, 25)
        Me.CheckBox1.TabIndex = 3
        Me.CheckBox1.Text = "不显示"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(556, 31)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(163, 81)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "+1000"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(50, 973)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(163, 81)
        Me.Button3.TabIndex = 5
        Me.Button3.Text = "init"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1448, 1066)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.VScrollBar1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents VScrollBar1 As VScrollBar
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
End Class
