using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using TimedTask.Common;

namespace TimedTask.Dal
{
    /// <summary>
    /// 数据库访问基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DalBase<T> //where T : new()
    {
        #region 变量

        private string _tableName;  //表名
        private string _connString; //
        private StringBuilder _columns;//列名
        private StringBuilder _values;
        private Hashtable _params;//参数
        private DataBaseType _dbType = DataBaseType.SQLite;//数据库类型
        private string _primaryKey;//主键 

        /// <summary>
        /// 
        /// </summary>
        public DataBaseType DbType
        {
            get { return _dbType; }
            set { _dbType = value; }
        }
        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connString">链接</param>
        /// <param name="tableName">表名</param>
        /// <param name="incrementPrimaryKey">自增主键</param>
        public DalBase(string connString, string tableName, string incrementPrimaryKey)
        {
            this._connString = connString;
            this._tableName = tableName;
            this._primaryKey = incrementPrimaryKey;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connString">链接</param>
        /// <param name="incrementPrimaryKey">主键</param>
        public DalBase(string tableName, string incrementPrimaryKey)
        {
            this._connString = SqliteHelper.ConnectionString;
            this._tableName = tableName;
            this._primaryKey = incrementPrimaryKey;
        }
        #endregion

        #region 增

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">model实体</param>
        /// <returns></returns>
        public virtual int Add(T model)
        {
            StringBuilder sb = new StringBuilder();
            this._columns = new StringBuilder();
            this._values = new StringBuilder();
            this._params = new Hashtable();
            sb.AppendFormat("INSERT INTO {0}(", this._tableName);

            AddForParamters(model);
            sb.AppendFormat("{0}) VALUES ({1});", Regex.Replace(this._columns.ToString(), "^,", ""), Regex.Replace(this._values.ToString(), "^,", ""));
            return SqliteHelper.ExecSql(sb.ToString(), this._params);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">model实体</param>
        /// <param name="i">返回INT型主键值</param>
        public virtual void Add(T model, out int i)
        {
            Add(null, null, model, out i);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="model">model实体</param>
        /// <param name="i">返回INT型主键值</param>
        public virtual void Add(T model, string tableName, out int i)
        {
            Add(null, tableName, model, out i);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="connString">链接</param>
        /// <param name="tableName">表名</param>
        /// <param name="model">model实体</param>
        /// <param name="i">返回INT型主键值</param>
        public virtual void Add(string connString, string tableName, T model, out int i)
        {
            if (String.IsNullOrEmpty(connString))
                connString = this._connString;
            if (String.IsNullOrEmpty(tableName))
                tableName = this._tableName;

            StringBuilder sb = new StringBuilder();
            this._columns = new StringBuilder();
            this._values = new StringBuilder();
            this._params = new Hashtable();
            sb.AppendFormat("INSERT INTO {0}(", tableName);
            AddForParamters(model);
            sb.AppendFormat("{0}) VALUES ({1});SELECT @@IDENTITY;", Regex.Replace(this._columns.ToString(), "^,", ""), Regex.Replace(this._values.ToString(), "^,", ""));
            object obj = SqliteHelper.ExecScalar(connString, sb.ToString(), this._params);
            if (obj == null)
            {
                i = 0;
            }
            else
            {
                i = Convert.ToInt32(obj);
            }
        }
        #endregion

        #region 统计

        /// <summary>
        /// 记录总数
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public virtual int Count(string strWhere)
        {
            string sql = String.Format("SELECT COUNT(*) FROM {0}", this._tableName);
            if (!String.IsNullOrEmpty(strWhere))
                sql += " WHERE " + strWhere;

            object obj = SqliteHelper.ExecScalar(this._connString, sql, CommandType.Text);
            if (obj == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj);
        }

        /// <summary>
        /// 记录总数
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public virtual int Count(string tableName, string strWhere)
        {
            return Count(null, tableName, strWhere);
        }

        /// <summary>
        /// 记录总数
        /// </summary>
        /// <param name="connString">链接</param>
        /// <param name="tableName">表名</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public virtual int Count(string connString, string tableName, string strWhere)
        {
            if (String.IsNullOrEmpty(connString))
                connString = this._connString;
            if (String.IsNullOrEmpty(tableName))
                tableName = this._tableName;

            string sql = String.Format("SELECT COUNT(*) FROM {0}", tableName);
            if (!String.IsNullOrEmpty(strWhere))
                sql += " WHERE " + strWhere;

            object obj = SqliteHelper.ExecScalar(connString, sql, CommandType.Text);
            if (obj == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj);
        }
        #endregion

        #region 存在

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public virtual bool Exists(string strWhere)
        {
            return Exists(null, null, strWhere);
        }
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public virtual bool Exists(string tableName, string strWhere)
        {
            return Exists(null, tableName, strWhere);
        }
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="connString">链接</param>
        /// <param name="tableName">表名</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public virtual bool Exists(string connString, string tableName, string strWhere)
        {
            if (String.IsNullOrEmpty(connString))
                connString = this._connString;
            if (String.IsNullOrEmpty(tableName))
                tableName = this._tableName;

            string sql = String.Format("SELECT COUNT(1) FROM {0}", tableName);
            if (!String.IsNullOrEmpty(strWhere))
                sql += " WHERE " + strWhere;

            return SqliteHelper.Exists(connString, sql);
        }
        #endregion

        #region 删除

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public virtual int Delete(string strWhere)
        {
            string sql = String.Format("DELETE FROM {0}", this._tableName);
            if (!String.IsNullOrEmpty(strWhere))
                sql += " WHERE " + strWhere;

            return SqliteHelper.ExecSql(this._connString, sql, CommandType.Text);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public virtual int Delete(string tableName, string strWhere)
        {
            return Delete(null, tableName, strWhere);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="connString">链接</param>
        /// <param name="tableName">表名</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public virtual int Delete(string connString, string tableName, string strWhere)
        {
            if (String.IsNullOrEmpty(connString))
                connString = this._connString;
            if (String.IsNullOrEmpty(tableName))
                tableName = this._tableName;

            string sql = String.Format("DELETE FROM {0}", tableName);

            if (!String.IsNullOrEmpty(strWhere))
                sql += " WHERE " + strWhere;

            return SqliteHelper.ExecSql(connString, sql, CommandType.Text);
        }
        #endregion

        #region 查询 实体集合

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="top"></param>
        /// <param name="strWhere">条件</param>
        /// <param name="orderBy">排序</param>
        /// <returns></returns>
        public virtual List<T> GetList(int top, string strWhere, string orderBy)
        {
            return this.GetList(null, top, strWhere, orderBy);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="top"></param>
        /// <param name="strWhere">条件</param>
        /// <param name="orderBy">排序</param>
        /// <returns></returns>
        public virtual List<T> GetList(string tableName, int top, string strWhere, string orderBy)
        {
            int recordOut = 0;
            return this.GetList(this._connString, tableName, top, 1, "*", strWhere, orderBy, out recordOut);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="connString">链接</param>
        /// <param name="tableName">表名</param>
        /// <param name="top"></param>
        /// <param name="showFields">显示字段</param>
        /// <param name="strWhere">条件</param>
        /// <param name="orderBy">排序</param>
        /// <returns></returns>
        public virtual List<T> GetList(string connString, string tableName, int pageSize, int pageIndex, string showFields, string strWhere, string orderBy, out int recordOut)
        {
            if (String.IsNullOrEmpty(connString))
                connString = this._connString;

            if (String.IsNullOrEmpty(tableName))
                tableName = this._tableName;

            DataTable dt = SqliteHelper.Query(connString, tableName, showFields, strWhere, orderBy, pageSize, pageIndex, out recordOut);
            return Helper.ToList<T>(dt);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="connString">链接</param>
        /// <param name="tableName">表名</param>
        /// <param name="strWhere">条件</param>
        /// <param name="orderBy">排序</param>
        /// <returns></returns>
        public virtual List<T> GetList(string strWhere, string showFields, string orderBy)
        {
            return GetList(null, null, showFields, strWhere, orderBy);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="connString">链接</param>
        /// <param name="tableName">表名</param>
        /// <param name="showFields">显示列</param>
        /// <param name="strWhere">条件</param>
        /// <param name="orderBy">排序</param>
        /// <returns></returns>
        public virtual List<T> GetList(string connString, string tableName, string showFields, string strWhere, string orderBy)
        {
            if (String.IsNullOrEmpty(connString))
                connString = this._connString;
            if (String.IsNullOrEmpty(tableName))
                tableName = this._tableName;
            if (String.IsNullOrEmpty(showFields))
                showFields = "*";
            if (String.IsNullOrEmpty(orderBy))
                orderBy = " Id DESC";

            string sql = String.Format("SELECT {0} FROM {1} WHERE {2} ORDER BY {3}", showFields, tableName, strWhere, orderBy);
            DataTable dt = SqliteHelper.Query(connString, sql, CommandType.Text);
            return Helper.ToList<T>(dt);
        }
        #endregion

        #region 获得实体

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public virtual T GetEntity(string strWhere)
        {
            return this.GetEntity(null, strWhere);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="showFields">显示字段</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public virtual T GetEntity(string showFields, string strWhere)
        {
            return GetEntity(null, null, showFields, strWhere);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="showFields">显示字段</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public virtual T GetEntity(string tableName, string showFields, string strWhere)
        {
            return GetEntity(null, tableName, showFields, strWhere);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="connString">链接</param>
        /// <param name="tableName">表名</param>
        /// <param name="showFields">显示字段</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public virtual T GetEntity(string connString, string tableName, string showFields, string strWhere)
        {
            if (String.IsNullOrEmpty(connString))
                connString = this._connString;
            if (String.IsNullOrEmpty(tableName))
                tableName = this._tableName;
            if (String.IsNullOrEmpty(showFields))
                showFields = "*";

            string sql = String.Format("SELECT TOP 1 {1} FROM {0}", tableName, showFields);
            if (this._dbType == DataBaseType.SQLite)
                sql = String.Format("SELECT {1} FROM {0}", tableName, showFields);

            if (!String.IsNullOrEmpty(strWhere))
                sql += " WHERE " + strWhere;

            if (this._dbType == DataBaseType.SQLite)
                sql += " Limit 1";

            DataTable dt = SqliteHelper.Query(connString, sql, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                return Helper.ToEntity<T>(dt.Rows[0]);
            }
            return default(T);
        }
        #endregion

        #region 更新

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="connString">链接</param>
        /// <param name="tableName">表名</param>
        /// <param name="model">实体</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public virtual int Update(string connString, string tableName, T model, string strWhere)
        {
            if (String.IsNullOrEmpty(connString))
                connString = this._connString;
            if (String.IsNullOrEmpty(tableName))
                tableName = this._tableName;

            StringBuilder sb = new StringBuilder();
            this._columns = new StringBuilder();
            this._params = new Hashtable();
            sb.AppendFormat("UPDATE {0} SET ", tableName);
            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                PropertyInfo pi = typeof(T).GetProperty(info.Name);
                object value = pi.GetValue(model, null);
                if (value == null) continue;
                //消除主键影响
                if (!String.IsNullOrEmpty(this._primaryKey))
                {
                    if (pi.Name.Equals(this._primaryKey, StringComparison.CurrentCultureIgnoreCase))
                        continue;
                }
                value = ConvertValue(pi, value);
                this._columns.AppendFormat(",{0}=@{0}", info.Name);
                this._params.Add(info.Name, value);
            }
            sb.Append(Regex.Replace(this._columns.ToString(), "^,", ""));
            if (!String.IsNullOrEmpty(strWhere))
            {
                sb.AppendFormat(" WHERE {0}", strWhere);
            }
            return SqliteHelper.ExecSql(connString, sb.ToString(), this._params);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="model">实体</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public virtual int Update(string tableName, T model, string strWhere)
        {
            return Update(null, tableName, model, strWhere);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public virtual int Update(T model, string strWhere)
        {
            return Update(null, null, model, strWhere);
        }
        #endregion

        #region 私有方法

        /// <summary>
        /// 特殊值转换
        /// </summary>
        /// <param name="pi"></param>
        /// <param name="value">值</param>
        /// <returns></returns>
        private object ConvertValue(PropertyInfo pi, object value)
        {
            object obj = value;
            //SQLite
            if (_dbType == DataBaseType.SQLite)
            {
                if (pi.PropertyType == typeof(DateTime?) || pi.PropertyType == typeof(DateTime))
                    obj = Convert.ToDateTime(value).ToString("s");
            }
            return obj;
        }
        /// <summary>
        /// 添加记录时参数设置
        /// </summary>
        /// <param name="model"></param>
        private void AddForParamters(T model)
        {
            if (model == null) return;

            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                PropertyInfo pi = typeof(T).GetProperty(info.Name);
                object value = pi.GetValue(model, null);
                if (value == null) continue;
                //消除主键影响
                if (!String.IsNullOrEmpty(this._primaryKey))
                {
                    if (pi.Name.Equals(this._primaryKey, StringComparison.CurrentCultureIgnoreCase))
                        continue;
                }

                this._columns.AppendFormat(",{0}", info.Name);
                this._values.AppendFormat(",@{0}", info.Name);
                value = ConvertValue(pi, value);
                this._params.Add(info.Name, value);
            }
        }

        #endregion
    }
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DataBaseType
    {
        SQLite,
        Access
    }
}
