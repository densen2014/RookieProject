Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class Form1
    Private PathName As String = "C:\Users\Alex\Desktop\data.accdb"

    Dim Conn As New OleDb.OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;Data source=" + PathName)
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Conn.Open()
        Const Sqlstr = "INSERT INTO [CodeData] values('A','Bxxxx','xxC','xxxD')"
        Dim Comm As OleDb.OleDbCommand = New OleDbCommand(Sqlstr, Conn)
        Comm.CommandText = Sqlstr
        Comm.ExecuteNonQuery()
        Dim Sql = "select 代码用途,备注内容 from CodeData"

        Dim SqlEX = "Select Count(*) From MSysObjects Where Name = 'Sheet1'"
        Comm.CommandText = Sqlstr
        Dim res = Comm.ExecuteScalar

        Dim TableSchema = Conn.GetSchema("TABLES")

        Dim myTable = TableSchema.Select("TABLE_NAME='Sheet1'")

        If myTable.length = 0 Then

            Dim sqlC = "CREATE TABLE [Sheet1] ([流水号] float ,[销售员] ntext ,[客户号] float ,[销售量] float ,[销售额II] float ,[成本] float ,[利润II] float ,[销售额] float ,[提成] float ,[已付款] float ,[欠款] float )"
            Comm.CommandText = sqlC
            Comm.ExecuteNonQuery()
        End If

        myTable = TableSchema.Select("TABLE_NAME='MYTABLENAME'")

        If myTable.Length = 0 Then
            Dim cmd As New OleDb.OleDbCommand
            cmd.Connection = Conn
            cmd.CommandText = "CREATE TABLE MYTABLENAME;"
            Dim nAffected = cmd.ExecuteNonQuery
        End If


        'Dim sqlD = "Drop table [Sheet1];"
        'Comm.CommandText = sqlD
        'Comm.ExecuteNonQuery()

        Dim querystr1 As String = $"SELECT 代码用途 , 
                        iif(备注内容 = 'D' , count(备注内容) , 0 )  as A下载数量, 
                        iif(备注内容 = 'xxxD'  , count(备注内容) , 0 )  as B下载数量 
                    FROM CodeData   Group by 代码用途,备注内容"

        DataGridView1.DataSource = GetData(querystr1)

        Conn.Close()

    End Sub

    Public Function GetData(ByVal queryString As String) As DataTable


        Dim ds As New DataTable()

        Try

            Using adapter As New OleDbDataAdapter(queryString, Conn)
                adapter.Fill(ds)
            End Using


        Catch ex As Exception

            ' The connection failed. Display an error message.

        End Try

        Return ds



    End Function



End Class
