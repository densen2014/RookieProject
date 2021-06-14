' ********************************** 
' Densen Informatica 中讯科技 
' 作者：Alex Chow
' e-mail:zhouchuanglin@gmail.com 
' **********************************
Imports System.Linq

Public Class FormMain
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form1.Show()
    End Sub


    Public Sub SUM(ByVal dt As DataTable)

        If dt IsNot Nothing Then
            For Each item In dt.Rows
                DSkinDataGridView1.Rows.Add(item(1).ToString, item(1).ToString, item(1).ToString)
            Next
        End If


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim selectItems = DSkinDataGridView1.SelectedRows
        Form3.Show()
        Form3.init(selectItems)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        For P = 1 To 4
            DSkinDataGridView1.Rows.Add(False, "名称" & P, P * 0.8, P, "水水水水")
        Next

    End Sub

    Public Sub UpdateDGV(collectionObject As DataGridViewSelectedRowCollection)
        For Each row As DataGridViewRow In collectionObject
            If row.Cells(row.Cells.Count).Value IsNot Nothing Then
                Dim rowData As Object() = New Object(row.Cells.Count) {}
                For i As Integer = 0 To rowData.Length - 1
                    rowData(i) = row.Cells(i).Value
                Next
            End If
        Next
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'MsgBox(DSkinDataGridView1.Rows(0).Cells(0).Value)
        Dim s = ""
        For Each row As DataGridViewRow In DSkinDataGridView1.SelectedRows
            s &= row.Cells(0).Value & vbCrLf
        Next
        MsgBox(s)
    End Sub

    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DSkinDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Button3.PerformClick()
    End Sub
End Class
