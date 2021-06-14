Imports System.Linq


Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ButtonInitDatas.PerformClick()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim CT As Integer = AddgoodsGridView.Rows.Count

        Dim DT As New DataTable
        DT.Columns.Add(New DataColumn("X1"))
        DT.Columns.Add(New DataColumn("X2"))
        DT.Columns.Add(New DataColumn("X3"))
        DT.Columns.Add(New DataColumn("X4"))
        DT.Columns.Add(New DataColumn("X5"))

        Dim SelecTionCount As Integer = 0
        'Dim T As Integer = AddgoodsGridView.CurrentRow.Index 
        If CT > 0 Then  '===============有数据  
            DT.Rows.Clear()

            For T As Integer = 0 To CT - 1
                If AddgoodsGridView.Rows(T).Cells(0).Value = True Then
                    DT.Rows.Add()
                    DT.Rows(SelecTionCount)(1) = AddgoodsGridView.Rows(SelecTionCount）.Cells(1).Value.ToString().Trim()
                    DT.Rows(SelecTionCount)(2) = AddgoodsGridView.Rows(SelecTionCount）.Cells(2).Value.ToString().Trim()
                    DT.Rows(SelecTionCount)(3) = AddgoodsGridView.Rows(SelecTionCount）.Cells(3).Value.ToString().Trim()
                    DT.Rows(SelecTionCount)(4) = AddgoodsGridView.Rows(SelecTionCount）.Cells(4).Value.ToString().Trim()
                    SelecTionCount = SelecTionCount + 1

                End If
            Next
            For P As Integer = 0 To SelecTionCount - 1

            Next

            SUM(DT)
            FormMain.SUM(DT)
        End If


        'Me.Close()
        Try
        Catch ex As Exception

        End Try
    End Sub

    Public Sub SUM(ByVal dt As DataTable)

        If dt IsNot Nothing Then
            For Each item In dt.Rows
                DSkinDataGridView1.Rows.Add(item(1).ToString, item(1).ToString, item(1).ToString)
            Next
        End If


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles ButtonInitDatas.Click
        For P = 1 To 10
            AddgoodsGridView.Rows.Add(False, "名称" & P, P * 0.8, P, "水水水水")
        Next

    End Sub
End Class
