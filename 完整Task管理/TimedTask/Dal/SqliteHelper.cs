using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Text;
using TimedTask.Common;

namespace TimedTask.Dal
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SqliteHelper
    {
        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        public static string ConnectionString { get; set; }
        static SqliteHelper()
        {
            //数据库文件路径
            var filePath = System.Environment.CurrentDirectory + @"\Task.db";
            var connStr = string.Format("Data Source={0};failifmissing=false", filePath);//Pooling=true;
            ConnectionString = connStr;
        }

        #region AddParameters
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameters">Hashtable</param>
        public static SQLiteParameter[] AddParameters(Hashtable parameters)
        {
            SQLiteParameter[] param = new SQLiteParameter[parameters.Count];
            IDictionaryEnumerator ide = parameters.GetEnumerator();
            int i = 0;
            while (ide.MoveNext())
            {
                param[i] = AddParameters(ide.Key.ToString(), ide.Value);
                i++;
            }
            return param;
        }
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">参数值</param>
        public static SQLiteParameter AddParameters(string parameterName, object value)
        {
            return AddParameters(parameterName, value, DbType.String, -1, ParameterDirection.Input);
        }
        /// <summary>
        /// 创建一个传入参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="type">参数类型</param>
        /// <param name="size">参数大小</param>
        /// <returns></returns>
        public static SQLiteParameter AddParameters(string name, object value, DbType type, int size, ParameterDirection direction)
        {
            SQLiteParameter para = new SQLiteParameter();
            para.ParameterName = name;
            para.Value = value;
            para.DbType = type;
            para.Direction = direction;
            return para;
        }
        /// <summary>
        /// 创建一个传出参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="type">参数类型</param>
        /// <param name="size">参数大小</param>
        /// <returns></returns>
        public static SQLiteParameter AddOutParameter(string name, DbType type = DbType.String, int size = 50)
        {
            var para = new SQLiteParameter(name, type, size);
            para.Direction = ParameterDirection.Output;
            return para;
        }

        /// <summary>
        /// 预处理Command对象,数据库链接,事务,需要执行的对象,参数等的初始化
        /// </summary>
        /// <param name="cmd">Command对象</param>
        /// <param name="conn">Connection对象</param>
        /// <param name="trans">Transcation对象</param>
        /// <param name="useTrans">是否使用事务</param>
        /// <param name="cmdType">SQL字符串执行类型</param>
        /// <param name="cmdText">SQL Text</param>
        /// <param name="cmdParms">SQLiteParameters to use in the command</param>
        private static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, ref SQLiteTransaction trans, bool useTrans, CommandType cmdType, string cmdText, params SQLiteParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (useTrans)
            {
                trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.Transaction = trans;
            }

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SQLiteParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        #endregion

        #region ExecSql

        /// <summary>
        /// 执行数据库操作(新增、更新或删除)
        /// </summary>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="cmdParms">SQL参数对象</param>
        /// <returns>所受影响的行数</returns>
        public static int ExecSql(string connectionString, string commandText, Hashtable ht)
        {
            SQLiteParameter[] cmdParms = AddParameters(ht);
            if (String.IsNullOrEmpty(connectionString))
                connectionString = ConnectionString;

            return ExecSql(ConnectionString, commandText, CommandType.Text, cmdParms);
        }

        /// <summary>
        /// 执行数据库操作(新增、更新或删除)
        /// </summary>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="cmdParms">SQL参数对象</param>
        /// <returns>所受影响的行数</returns>
        public static int ExecSql(string commandText, Hashtable ht)
        {
            return ExecSql(null, commandText, ht);
        }
        /// <summary>
        /// 执行数据库操作(新增、更新或删除)
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmd">SqlCommand对象</param>
        /// <returns>所受影响的行数</returns>
        public static int ExecSql(string connectionString, SQLiteCommand cmd)
        {
            int result = 0;
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                SQLiteTransaction trans = null;
                PrepareCommand(cmd, con, ref trans, true, cmd.CommandType, cmd.CommandText);
                try
                {
                    result = cmd.ExecuteNonQuery();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// 执行数据库操作(新增、更新或删除)
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="commandType">执行类型</param>
        /// <returns>所受影响的行数</returns>
        public static int ExecSql(string connectionString, string commandText, CommandType commandType)
        {
            int result = 0;
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");
            SQLiteCommand cmd = new SQLiteCommand();
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                SQLiteTransaction trans = null;
                PrepareCommand(cmd, con, ref trans, true, commandType, commandText);
                try
                {
                    result = cmd.ExecuteNonQuery();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// 执行数据库操作(新增、更新或删除)
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="commandType">执行类型</param>
        /// <param name="cmdParms">SQL参数对象</param>
        /// <returns>所受影响的行数</returns>
        public static int ExecSql(string connectionString, string commandText, CommandType commandType, params SQLiteParameter[] cmdParms)
        {
            int result = 0;
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");

            SQLiteCommand cmd = new SQLiteCommand();

            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                SQLiteTransaction trans = null;
                PrepareCommand(cmd, con, ref trans, true, commandType, commandText, cmdParms);
                try
                {
                    result = cmd.ExecuteNonQuery();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
            return result;
        }
        #endregion

        #region ExecScalar
        /// <summary>
        /// 执行数据库操作(新增、更新或删除)同时返回执行后查询所得的第1行第1列数据
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmd">SqlCommand对象</param>
        /// <returns>查询所得的第1行第1列数据</returns>
        public static object ExecScalar(string connectionString, SQLiteCommand cmd)
        {
            object result = 0;
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                SQLiteTransaction trans = null;
                PrepareCommand(cmd, con, ref trans, true, cmd.CommandType, cmd.CommandText);
                try
                {
                    result = cmd.ExecuteScalar();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// 执行数据库操作(新增、更新或删除)同时返回执行后查询所得的第1行第1列数据
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="commandType">执行类型</param>
        /// <returns>查询所得的第1行第1列数据</returns>
        public static object ExecScalar(string connectionString, string commandText, CommandType commandType)
        {
            object result = 0;
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");
            SQLiteCommand cmd = new SQLiteCommand();
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                SQLiteTransaction trans = null;
                PrepareCommand(cmd, con, ref trans, true, commandType, commandText);
                try
                {
                    result = cmd.ExecuteScalar();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// 执行数据库操作(新增、更新或删除)同时返回执行后查询所得的第1行第1列数据
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="commandType">执行类型</param>
        /// <param name="cmdParms">SQL参数对象</param>
        /// <returns>查询所得的第1行第1列数据</returns>
        public static object ExecScalar(string connectionString, string commandText, CommandType commandType, params SQLiteParameter[] cmdParms)
        {
            object result = 0;
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");

            SQLiteCommand cmd = new SQLiteCommand();
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                SQLiteTransaction trans = null;
                PrepareCommand(cmd, con, ref trans, true, commandType, commandText);
                try
                {
                    result = cmd.ExecuteScalar();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// 执行数据库操作(新增、更新或删除)同时返回执行后查询所得的第1行第1列数据
        /// </summary>
        /// <param name="cmdParms">SQL参数对象</param>
        /// <returns>查询所得的第1行第1列数据</returns>
        public static object ExecScalar(string commandText, Hashtable ht)
        {
            SQLiteParameter[] cmdParms = AddParameters(ht);
            return ExecScalar(ConnectionString, commandText, CommandType.Text, cmdParms);
        }

        /// <summary>
        /// 执行数据库操作(新增、更新或删除)同时返回执行后查询所得的第1行第1列数据
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="ht">SQL参数对象</param>
        /// <returns>查询所得的第1行第1列数据</returns>
        public static object ExecScalar(string connectionString, string commandText, Hashtable ht)
        {
            SQLiteParameter[] cmdParms = AddParameters(ht);
            return ExecScalar(connectionString, commandText, CommandType.Text, cmdParms);
        }
        #endregion

        #region ExecReader
        /// <summary>
        /// 执行数据库查询，返回SqlDataReader对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmd">SqlCommand对象</param>
        /// <returns>SqlDataReader对象</returns>
        public static DbDataReader ExecReader(string connectionString, SQLiteCommand cmd)
        {
            DbDataReader reader = null;
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");

            SQLiteConnection con = new SQLiteConnection(connectionString);
            SQLiteTransaction trans = null;
            PrepareCommand(cmd, con, ref trans, false, cmd.CommandType, cmd.CommandText);
            try
            {
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return reader;
        }

        /// <summary>
        /// 执行数据库查询，返回SqlDataReader对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="commandType">执行类型</param>
        /// <returns>SqlDataReader对象</returns>
        public static DbDataReader ExecReader(string connectionString, string commandText, CommandType commandType)
        {
            DbDataReader reader = null;
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");

            SQLiteConnection con = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand();
            SQLiteTransaction trans = null;
            PrepareCommand(cmd, con, ref trans, false, commandType, commandText);
            try
            {
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return reader;
        }

        /// <summary>
        /// 执行数据库查询，返回SqlDataReader对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="commandType">执行类型</param>
        /// <param name="cmdParms">SQL参数对象</param>
        /// <returns>SqlDataReader对象</returns>
        public static DbDataReader ExecReader(string connectionString, string commandText, CommandType commandType, params SQLiteParameter[] cmdParms)
        {
            DbDataReader reader = null;
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");

            SQLiteConnection con = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand();
            SQLiteTransaction trans = null;
            PrepareCommand(cmd, con, ref trans, false, commandType, commandText, cmdParms);
            try
            {
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return reader;
        }
        #endregion

        #region Query
        /// <summary>
        /// 执行数据库查询，返回DataSet对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmd">SqlCommand对象</param>
        /// <returns>DataSet对象</returns>
        public static DataTable Query(string connectionString, SQLiteCommand cmd)
        {
            DataTable dt = new DataTable();
            SQLiteConnection con = new SQLiteConnection(connectionString);
            SQLiteTransaction trans = null;
            PrepareCommand(cmd, con, ref trans, false, cmd.CommandType, cmd.CommandText);
            try
            {
                SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd.Connection != null)
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// 执行数据库查询，返回DataSet对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="commandType">执行类型</param>
        /// <returns>DataSet对象</returns>
        public static DataTable Query(string connectionString, string commandText, CommandType commandType)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");
            DataTable dt = new DataTable();
            SQLiteConnection con = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand();
            SQLiteTransaction trans = null;
            PrepareCommand(cmd, con, ref trans, false, commandType, commandText);
            try
            {
                SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            return dt;

        }

        /// <summary>
        /// 执行数据库查询，返回DataSet对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="commandType">执行类型</param>
        /// <param name="cmdParms">SQL参数对象</param>
        /// <returns>DataSet对象</returns>
        public static DataTable Query(string connectionString, string commandText, CommandType commandType, params SQLiteParameter[] cmdParms)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");
            DataTable dt = new DataTable();
            SQLiteConnection con = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand();
            SQLiteTransaction trans = null;
            PrepareCommand(cmd, con, ref trans, false, commandType, commandText, cmdParms);
            try
            {
                SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// 通用分页查询方法
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <param name="tableName">表名</param>
        /// <param name="showColumns">查询字段名</param>
        /// <param name="where">where条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="pageSize">每页数据数量</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="recordOut">数据总量</param>
        /// <returns>DataTable数据表</returns>
        public static DataTable Query(string connectionString, string tableName, string showColumns, string where, string orderBy, int pageSize, int pageIndex, out int recordOut)
        {
            DataTable dt = new DataTable();
            recordOut = Convert.ToInt32(ExecScalar(connectionString, "select count(*) from " + tableName, CommandType.Text));
            string pagingTemplate = "select {0} from {1} where {2} order by {3} limit {4} offset {5} ";
            int offsetCount = (pageIndex - 1) * pageSize;
            string commandText = String.Format(pagingTemplate, showColumns, tableName, where, orderBy, pageSize.ToString(), offsetCount.ToString());
            using (DbDataReader reader = ExecReader(connectionString, commandText, CommandType.Text))
            {
                if (reader != null)
                {
                    dt.Load(reader);
                }
            }
            return dt;
        }
        #endregion

        #region Exists

        /// <summary>
        /// 记录是否存在
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <returns></returns>
        public static bool Exists(string commandText)
        {
            return Exists(null, commandText);
        }
        /// <summary>
        /// 记录是否存在
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandText">sql语句</param>
        /// <returns></returns>
        public static bool Exists(string connectionString, string commandText)
        {
            int result;
            if (String.IsNullOrEmpty(connectionString))
                connectionString = ConnectionString;

            object obj = ExecScalar(connectionString, commandText, CommandType.Text);
            if (object.Equals(obj, null) || object.Equals(obj, DBNull.Value))
            {
                result = 0;
            }
            else
            {
                result = int.Parse(obj.ToString());
            }
            if (result == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 记录是否存在
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="isProc">是否是存储过程</param>
        /// <returns></returns>
        public static bool Exists(string commandText, Hashtable parameters)
        {
            return Exists(null, commandText, parameters);
        }
        /// <summary>
        /// 记录是否存在
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="isProc">是否是存储过程</param>
        /// <returns></returns>
        public static bool Exists(string connectionString, string commandText, Hashtable parameters)
        {
            int result;
            if (String.IsNullOrEmpty(connectionString))
                connectionString = ConnectionString;

            object obj = ExecScalar(connectionString, commandText, parameters);
            if (object.Equals(obj, null) || object.Equals(obj, DBNull.Value))
            {
                result = 0;
            }
            else
            {
                result = int.Parse(obj.ToString());
            }
            if (result == 0)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region DB

        //你可以在你的程序中约定一个时间间隔执行一次重新组织数据库的操作，节约空间
        /// <summary>
        /// 收缩数据库
        /// </summary>
        public void Reset()
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    cmd.Parameters.Clear();
                    cmd.Connection = conn;
                    cmd.CommandText = "vacuum";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 30;
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion
    }
}
