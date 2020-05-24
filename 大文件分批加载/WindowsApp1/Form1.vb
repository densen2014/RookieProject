Imports System.Text

Public Class Form1
    Dim sw As Stopwatch = New Stopwatch
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If IO.File.Exists("test.txt") = False Then
            Init()
        Else
            Button1.PerformClick()
        End If
    End Sub
    Sub Init()
        sw.Restart()
        Dim trex = "1,3326509.331,456239.325,13.21"
        Dim sqlCopyStringBuilder As StringBuilder = New StringBuilder
        For index = 1 To 1000000
            sqlCopyStringBuilder.AppendLine(index & "----" & Now.ToString & " - " & trex)
        Next
        Console.WriteLine($"费{sw.ElapsedMilliseconds}")
        IO.File.WriteAllText("test.txt", sqlCopyStringBuilder.ToString)
        Console.WriteLine($"费{sw.ElapsedMilliseconds}")
        sw.Stop()
        Button1.PerformClick()
    End Sub
    Dim lq
    Dim last
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        sw.Restart()
        Dim txt As StringBuilder = New StringBuilder
        lq = IO.File.ReadAllLines("test.txt")
        Console.WriteLine($"ReadAllLines费{sw.ElapsedMilliseconds}")
        For index = 0 To 1000
            txt.AppendLine(lq(index))
        Next
        last = 1000
        Console.WriteLine($"Join费{sw.ElapsedMilliseconds}")
        sw.Stop()
        TextBox1.Text = txt.ToString
        VScrollBar1.Maximum = lq.Length
        VScrollBar1.Value = 1000

        'Dim lq2 = IO.File.ReadAllText("test.txt")
        'Console.WriteLine($"ReadAllText费{sw.ElapsedMilliseconds}")
        'TextBox1.Text = lq3
        'Console.WriteLine($"TextBox1费{sw.ElapsedMilliseconds}")
    End Sub

    Private Sub VScrollBar1_Scroll(sender As Object, e As ScrollEventArgs) Handles VScrollBar1.Scroll
        懒加载()
    End Sub
    Sub 懒加载()
        sw.Restart()
        Me.Text = $"{VScrollBar1.Value}/{VScrollBar1.Maximum}"

        Dim 块 As StringBuilder = New StringBuilder
        For index = If(VScrollBar1.Value > 1000, VScrollBar1.Value - 1000, 0) To VScrollBar1.Value
            块.AppendLine(lq(index))
        Next
        last = VScrollBar1.Value
        Console.WriteLine($"从{last}到{VScrollBar1.Value}. 费{sw.ElapsedMilliseconds}")

        If CheckBox1.Checked = False Then
            TextBox1.Text = 块.ToString
            Console.WriteLine($"从{last}到{VScrollBar1.Value}. 显示费{sw.ElapsedMilliseconds}")
        End If
        sw.Stop()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        VScrollBar1.Value += 1000
        懒加载()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Init()
    End Sub
End Class
