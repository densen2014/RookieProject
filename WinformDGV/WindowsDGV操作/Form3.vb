Imports System.Linq


Public Class Form3
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub
    Public Sub init(collectionObject As DataGridViewSelectedRowCollection)
        For Each row As DataGridViewRow In collectionObject
            Dim rowData As Object() = New Object(row.Cells.Count) {}
            For i As Integer = 0 To rowData.Length - 1
                rowData(i) = row.Cells(i).Value
            Next
            rowData(row.Cells.Count) = Guid.NewGuid.ToString
            AddgoodsGridView.Rows.Add(rowData)
        Next


    End Sub

    Public Sub SUM(ByVal dt As DataTable)

        If dt IsNot Nothing Then
            For Each item In dt.Rows
                'DSkinDataGridView1.Rows.Add(item(1).ToString, item(1).ToString, item(1).ToString)
            Next
        End If


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        For P = 1 To 10
            AddgoodsGridView.Rows.Add(False, "名称" & P, P * 0.8, P, "水水水水")
        Next

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim selectItems = AddgoodsGridView.SelectedRows
        FormMain.UpdateDGV(selectItems)

    End Sub
End Class
