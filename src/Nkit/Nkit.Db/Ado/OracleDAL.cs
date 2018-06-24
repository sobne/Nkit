using System;
using System.Collections.Generic;
using System.Text;
//using System.Data.OracleClient;
using System.Collections;
using System.Data;

namespace Nkit.Db.Ado
{
        /*
    public class OracleDataAccess : IDBOperator
    {
        #region Private Variables
        string connectionString;// = ConfigurationSettings.AppSettings["SqlConnectionString"];
        OracleConnection connection;
        OracleTransaction transaction;
        OracleCommand cmd;
        Hashtable listParameters;  //需提前添加
        #endregion

        #region  开关，销毁连接,初始化成员
        /// <summary>
        /// 打开连接
        /// </summary>
        public void Open()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            if (connection.State.ToString() == "Open")
            {
                connection.Close();
                InitMember();
            }
        }
        /// <summary>
        /// 初始化成员
        /// </summary>
        void InitMember()
        {
            listParameters = null;
            cmd = null;
        }
        public void Dispose()
        {
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
            }
            GC.Collect();
        }
        #endregion

        #region IDBOperator 成员
        public OracleDataAccess()
        {
            connectionString = ConfigurationSettings.AppSettings["ConnectionString"];
            connection = new OracleConnection(connectionString);
        }

        public void SetConnection(string strConnectionString)
        {
            connection = new OracleConnection(strConnectionString);
        }

        #region 创建，设置Command对象
        /// <summary>
        /// 设置带事务的OracleCommand并返回
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="cmdType"></param>
        public OracleCommand SetCmd(string SQL, CommandType cmdType)
        {
            try
            {
                Open();
                if (null == cmd)
                {
                    cmd = new OracleCommand(SQL, connection);
                }
                cmd.CommandType = cmdType;
                cmd.CommandText = SQL;
                if (listParameters != null)
                {
                    foreach (DictionaryEntry parameter in listParameters)
                    {
                        cmd.Parameters.Add(new OracleParameter(parameter.Key.ToString(), parameter.Value));
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return cmd;
        }
        /// <summary> 
        /// 生成Command对象 
        /// </summary> 
        /// <param name="SQL"></param> 
        /// <param name="Conn"></param> 
        /// <returns></returns> 
        public OracleCommand CreateCmd(string SQL)
        {
            return SetCmd(SQL, CommandType.Text);
        }

        #endregion 创建，设置Command对象
        #region 返回OracleDataReader  string SQL
        /// <summary> 
        /// 运行SQL语句返回DataReader 
        /// </summary> 
        /// <param name="SQL"></param> 
        /// <returns>OracleDataReader对象.</returns> 
        public OracleDataReader ReturnDataReader(string SQL)
        {
            return CreateCmd(SQL).ExecuteReader();
        }
        #endregion
        #region 添加 OracleParameter
        /// <summary>
        /// 添加 OracleParameter
        /// </summary>
        /// <param name="ParamName">参数名</param>
        /// <param name="Value">参数值</param>
        public void AddParameter(string ParamName, object Value)
        {
            try
            {
                if (listParameters == null)
                {
                    listParameters = new Hashtable();
                }
                listParameters.Add(ParamName, Value);
            }
            catch (Exception e)
            {
                throw new Exception("添加OracleParameter出错:  " + e.Message);
            }
        }
        #endregion 添加 OracleParameter
        #region 清除 OracleParameter
        /// <summary>
        /// 清除 OracleParameter
        /// </summary>
        public void ClearParameter() { listParameters = null; }
        #endregion 

        #region 返回 OracleDataAdapter string SQL
        OracleDataAdapter GetDataAdapter(string SQL)
        {
            OracleDataAdapter Da = new OracleDataAdapter();

            cmd = SetCmd(SQL, CommandType.Text);
            Da.SelectCommand = cmd;
            OracleCommandBuilder custCB = new OracleCommandBuilder(Da);
            return Da;
        }
        #endregion

        #region 判断表是否存在   返回 True/False   string SQL
        /// <summary>
        /// 判断数据库中表是否存在
        /// </summary>
        /// <param name="strTableName">表名称</param>
        /// <returns>返回值</returns>
        public bool ExistTable(string strTableName)
        {
            bool ret = false;
            try
            {
                string sql = "select   name   from   sysobjects where name = '" + strTableName + "'";
                ret = ExistData(sql);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (null == transaction)
                {
                    Close();
                }
            }
            return ret;
        }
        #endregion
        #region 检验是否存在数据 返回 True/False   string SQL
        /// <summary> 
        /// 检验是否存在数据 
        /// </summary> 
        /// <returns></returns>
        public bool ExistData(string SQL)
        {
            bool ret = false;
            try
            {
                OracleDataReader dr = ReturnDataReader(SQL);
                if (dr.HasRows)
                {
                    ret = true;
                }
                dr.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception(SQL + Ex.ToString());
            }
            finally
            {
                if (null == transaction)
                {
                    Close();
                }
            }
            return ret;
        }
        #endregion
        #region 执行SQL操作      返回 -1为失败 >0为成功   string SQL
        /// <summary> 
        /// 运行SQL语句 
        /// </summary> 
        /// <param name="SQL"></param>
        public int ExeCmd(string SQL)
        {
            int ret = -1;
            try
            {
                ret = CreateCmd(SQL).ExecuteNonQuery();
            }
            catch (Exception ee)
            {
                throw new Exception(SQL + ee.ToString());
            }
            finally
            {
                if (null == transaction)
                {
                    Close();
                }
            }
            return ret;
        }
        #endregion
        #region 返回DataSet  string SQL

        /// <summary> 
        /// 运行SQL语句,返回DataSet对象 
        /// </summary>
        /// <param name="procName">SQL语句</param> 
        public DataSet ReturnDataSet(string SQL)
        {
            DataSet Ds = new DataSet();
            try
            {
                OracleDataAdapter Da = GetDataAdapter(SQL);
                Da.Fill(Ds);
            }
            catch (Exception Err)
            {
                throw new Exception(SQL + Err.ToString());
            }
            finally
            {
                if (null == transaction)
                {
                    Close();
                }
            }
            return Ds;
        }
        /// <summary> 
        /// 运行SQL语句,返回DataSet对象 
        /// </summary> 
        /// <param name="procName">SQL语句</param> 
        /// <param name="prams">DataSet对象</param> 
        /// <param name="dataReader">表名</param> 
        public DataSet ReturnDataSet(string SQL, string tablename)
        {
            DataSet Ds = new DataSet();
            try
            {
                OracleDataAdapter Da = GetDataAdapter(SQL);
                Da.Fill(Ds, tablename);
            }
            catch (Exception Err)
            {
                throw new Exception(SQL + Err.ToString());
            }
            finally
            {
                if (null == transaction)
                {

                    Close();
                }
            }
            return Ds;
        }
        /// <summary> 
        /// 运行SQL语句,返回DataSet对象 
        /// </summary> 
        public DataSet ReturnDataSet(string SQL, int StartIndex, int PageSize, string tablename)
        {
            DataSet Ds = new DataSet();
            try
            {
                OracleDataAdapter Da = GetDataAdapter(SQL);
                Da.Fill(Ds, StartIndex, PageSize, tablename);
            }
            catch (Exception Err)
            {
                throw new Exception(SQL + Err.ToString());
            }
            finally
            {
                if (null == transaction)
                {
                    Close();
                }
            }
            return Ds;
        }
        #endregion
        #region 返回DataTable对象 string SQL
        /// <summary> 
        /// 运行SQL语句,返回DataTable对象 
        /// </summary>
        /// <param name="procName">SQL语句</param> 
        public DataTable ReturnDataTable(string SQL)
        {
            DataTable dt = new DataTable();
            try
            {
                OracleDataAdapter Da = GetDataAdapter(SQL);
                Da.Fill(dt);
            }
            catch (Exception Err)
            {
                throw new Exception(SQL + Err.ToString());
            }
            finally
            {
                if (null == transaction)
                {
                    Close();
                }
            }
            return dt;
        }
        #endregion 返回DataTable对象 string SQL
        #region 返回DataReader数据  string SQL
        /// <summary> 
        /// 返回SQL语句执行结果的第一行第一列 
        /// </summary> 
        /// <returns>字符串</returns> 
        public object ReturnValue(string SQL)
        {
            object result = null;
            try
            {
                result = CreateCmd(SQL).ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(SQL + ex.Message);
            }
            finally
            {
                if (null == transaction)
                {
                    Close();
                }
            }
            return result;
        }
        /// <summary> 
        /// 用字符串方式返回第一行指定名称字段
        /// </summary> 
        /// <returns>以字符串形式返回该字段对应的数据</returns> 
        public string ReturnValue(string SQL, string Filed)
        {
            string ret = "";
            OracleDataReader dr = null;
            try
            {
                dr = ReturnDataReader(SQL);
                if (dr.Read())
                {
                    ret = dr[Filed].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(SQL + ex.Message);
            }
            finally
            {
                dr.Close();
                if (null == transaction)
                {
                    Close();
                }
            }
            return ret;
        }
        /// <summary> 
        /// 返回SQL语句第一列,第ColumnI列, 
        /// </summary> 
        /// <returns>字符串</returns> 
        public string ReturnValue(string SQL, int ColumnI)
        {
            string result = String.Empty;
            OracleDataReader dr = null;
            try
            {
                dr = ReturnDataReader(SQL);
                if (dr.Read())
                    result = dr[ColumnI].ToString();
                else
                    result = "";
            }
            catch (Exception ex)
            {
                throw new Exception(SQL + ex.Message);
            }
            finally
            {
                dr.Close();
                if (null == transaction)
                {
                    Close();
                }
            }
            return result;
        }

        /// <summary> 
        /// 以集合方式返回指定名称字段列的数据
        /// </summary>
        /// <param name="SQL">sql语句</param>
        /// <returns></returns>
        public ArrayList ReturnValues(string SQL)
        {
            ArrayList ret = new ArrayList();
            OracleDataReader dr = null;
            try
            {
                dr = ReturnDataReader(SQL);
                while (dr.Read())
                {
                    ret.Add(dr[0]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(SQL + ex.Message);
            }
            finally
            {
                dr.Close();
                if (null == transaction)
                {
                    Close();
                }
            }
            return ret;
        }
        /// <summary> 
        /// 以集合方式返回指定名称字段列的数据
        /// </summary>
        /// <param name="SQL">sql语句</param>
        /// <param name="Filed">要返回的字段</param>
        /// <returns></returns>
        public ArrayList ReturnValues(string SQL, string Filed)
        {
            ArrayList ret = new ArrayList();
            OracleDataReader dr = null;
            try
            {
                dr = ReturnDataReader(SQL);
                while (dr.Read())
                {
                    ret.Add(dr[Filed]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(SQL + ex.Message);
            }
            finally
            {
                dr.Close();
                if (null == transaction)
                {
                    Close();
                }
            }
            return ret;
        }
        #endregion

        #region 事务
        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTransaction()
        {
            Open();
            cmd = connection.CreateCommand();
            transaction = connection.BeginTransaction();

            cmd.Transaction = transaction;
        }
        /// <summary>
        /// 提交事务，如出现异常就撤消执行
        /// </summary>
        public void CommitTransaction()
        {
            try
            {
                if (null != transaction)
                {
                    transaction.Commit();
                }
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                transaction = null;
                Close();
            }
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTransaction()
        {
            if (null != transaction)
            {
                transaction.Rollback();
            }
        }
        #endregion
        //存储过程................................................................
        #region 运行存储过程,如果存储过程有参数需提前添加(AddParameter())，或传参数 prams
        #region 生成一个存储过程使用的OracleCommand.  CreateProcCmd
        /// <summary> 
        /// 生成一个存储过程使用的OracleCommand. 
        /// </summary> 
        /// <param name="procName">存储过程名.</param> 
        /// <returns>OracleCommand对象.</returns> 
        public OracleCommand CreateProcCmd(string procName)
        {
            return SetCmd(procName, CommandType.StoredProcedure);
        }
        /// <summary> 
        /// 生成一个存储过程使用的OracleCommand. 
        /// </summary> 
        /// <param name="procName">存储过程名.</param> 
        /// <param name="prams">存储过程入参数组.</param> 
        /// <returns>OracleCommand对象.</returns> 
        public OracleCommand CreateProcCmd(string procName, IDbDataParameter[] prams)
        {
            cmd = SetCmd(procName, CommandType.StoredProcedure);
            if (prams != null)
            {
                foreach (OracleParameter parameter in prams)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
            return cmd;
        }
        #endregion
        #region 生成存储过程参数
        public OracleParameter MakeInParam(string ParamName, object Value)
        {
            return MakeParam(ParamName, ParameterDirection.Input, Value);
        }
        /// <summary>
        /// Make input param.
        /// </summary>
        /// <param name="ParamName">Name of param.</param>
        /// <param name="DbType">Param type.</param>
        /// <param name="Value">Param value.</param>
        /// <returns>New parameter.</returns>
        public OracleParameter MakeInParam(string ParamName, OracleType DbType, object Value)
        {
            return MakeParam(ParamName, DbType, ParameterDirection.Input, Value);
        }
        /// <summary>
        /// Make input param.
        /// </summary>
        /// <param name="ParamName">Name of param.</param>
        /// <param name="DbType">Param type.</param>
        /// <param name="Size">Param size.</param>
        /// <param name="Value">Param value.</param>
        /// <returns>New parameter.</returns>
        public OracleParameter MakeInParam(string ParamName, OracleType DbType, int Size, object Value)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
        }
        /// <summary>
        /// Make input param.
        /// </summary>
        /// <param name="ParamName">Name of param.</param>
        /// <param name="DbType">Param type.</param>
        /// <returns>New parameter.</returns>
        public OracleParameter MakeOutParam(string ParamName, OracleType DbType)
        {
            return MakeParam(ParamName, DbType, ParameterDirection.Output);
        }
        /// <summary>
        /// Make input param.
        /// </summary>
        /// <param name="ParamName">Name of param.</param>
        /// <param name="DbType">Param type.</param>
        /// <param name="Size">Param size.</param>
        /// <returns>New parameter.</returns>
        public OracleParameter MakeOutParam(string ParamName, OracleType DbType, int Size)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Output, null);
        }

        /// <summary>
        /// Make stored procedure param.
        /// </summary>
        /// <param name="ParamName"></param>
        /// <param name="Direction"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public OracleParameter MakeParam(string ParamName, ParameterDirection Direction, object Value)
        {
            OracleParameter param;
            param = new OracleParameter(ParamName, Value);
            param.Direction = Direction;
            return param;
        }
        /// <summary>
        /// Make stored procedure param.
        /// </summary>
        /// <param name="ParamName">Name of param.</param>
        /// <param name="DbType">Param type.</param>
        /// <param name="Direction">Parm direction.</param>
        /// <returns>New parameter.</returns>
        public OracleParameter MakeParam(string ParamName, OracleType DbType, ParameterDirection Direction)
        {
            OracleParameter param;
            param = new OracleParameter(ParamName, DbType);
            param.Direction = Direction;
            return param;
        }
        /// <summary>
        /// Make stored procedure param.
        /// </summary>
        /// <param name="ParamName">Name of param.</param>
        /// <param name="DbType">Param type.</param>
        /// <param name="Direction">Parm direction.</param>
        /// <param name="Value">Param value.</param>
        /// <returns>New parameter.</returns>
        public OracleParameter MakeParam(string ParamName, OracleType DbType,
            ParameterDirection Direction, object Value)
        {
            OracleParameter param;
            param = new OracleParameter(ParamName, DbType);
            param.Direction = Direction;
            if (!(Direction == ParameterDirection.Output && Value == null))
                param.Value = Value;
            return param;
        }
        /// <summary>
        /// Make stored procedure param.
        /// </summary>
        /// <param name="ParamName">Name of param.</param>
        /// <param name="DbType">Param type.</param>
        /// <param name="Size">Param size.</param>
        /// <param name="Direction">Parm direction.</param>
        /// <param name="Value">Param value.</param>
        /// <returns>New parameter.</returns>
        public OracleParameter MakeParam(string ParamName, OracleType DbType, Int32 Size,
            ParameterDirection Direction, object Value)
        {
            OracleParameter param;
            if (Size > 0)
                param = new OracleParameter(ParamName, DbType, Size);
            else
                param = new OracleParameter(ParamName, DbType);
            param.Direction = Direction;
            if (!(Direction == ParameterDirection.Output && Value == null))
                param.Value = Value;
            return param;
        }
        #endregion
        #region  运行存储过程,如果存储过程有参数需提前添加(AddParameter())，或传参数 prams
        /// <summary>
        /// 运行存储过程,返回 true 执行成功
        /// 如果存储过程有参数需提前添加(AddParameter())
        /// </summary>
        /// <param name="procName"></param>
        /// <returns></returns>
        public bool RunProc(string procName)
        {
            bool ret = false;
            try
            {
                Open();
                int test = CreateProcCmd(procName).ExecuteNonQuery();
                if (test > 0)
                {
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(procName + ex.Message);
            }
            finally
            {
                if (null == transaction)
                {
                    Close();
                }
            }
            return ret;
        }
        /// <summary> 
        /// 运行存储过程,返回 true 执行成功
        /// </summary> 
        /// <param name="procName">存储过程名</param> 
        /// <param name="prams">存储过程参数</param> 
        public bool RunProc(string procName, IDbDataParameter[] prams)
        {
            bool ret = false;
            Open();
            try
            {
                int test = CreateProcCmd(procName, prams).ExecuteNonQuery();
                if (test > 0)
                {
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(procName + ex.Message);
            }
            finally
            {
                if (null == transaction)
                {

                    Close();
                }
            }
            return ret;
        }
        /// <summary>
        /// 运行存储过程,返回DataTable
        /// 如果存储过程有参数需提前添加(AddParameter())
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <returns>DataTable对象</returns>
        public DataTable RunProcReturnDatatable(string procName)
        {
            DataTable Ds = new DataTable();
            try
            {
                OracleDataAdapter Da = new OracleDataAdapter(CreateProcCmd(procName));
                Da.Fill(Ds);
            }
            catch (Exception Ex)
            {
                throw new Exception(procName + Ex.Message);
            }
            finally
            {
                if (null == transaction)
                {
                    Close();
                }
            }
            return Ds;
        }
        /// <summary> 
        /// 运行存储过程,返回DataTable. 
        /// </summary> 
        /// <param name="procName">存储过程名.</param> 
        /// <param name="prams">存储过程入参数组.</param> 
        /// <returns>DataTable对象.</returns> 
        public DataTable RunProcReturnDatatable(string procName, IDbDataParameter[] prams)
        {
            DataTable Ds = new DataTable();
            try
            {
                OracleDataAdapter Da = new OracleDataAdapter(CreateProcCmd(procName, prams));
                Da.Fill(Ds);
            }
            catch (Exception Ex)
            {
                throw new Exception(procName + Ex.Message);
            }
            finally
            {
                if (null == transaction)
                {
                    Close();
                }
            }
            return Ds;
        }

        /// <summary>
        /// 运行存储过程,返回DataTable
        /// 如果存储过程有参数需提前添加(AddParameter())
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <returns>DataSet对象</returns>
        public DataSet RunProcReturnDataSet(string procName)
        {
            DataSet Ds = new DataSet();
            try
            {
                OracleDataAdapter Da = new OracleDataAdapter(CreateProcCmd(procName));
                Da.Fill(Ds);
            }
            catch (Exception Ex)
            {
                throw new Exception(procName + Ex.Message);
            }
            finally
            {
                if (null == transaction)
                {
                    Close();
                }
            }
            return Ds;
        }
        /// <summary> 
        /// 运行存储过程,返回DataTable. 
        /// </summary> 
        /// <param name="procName">存储过程名.</param> 
        /// <param name="prams">存储过程入参数组.</param> 
        /// <returns>DataSet对象.</returns> 
        public DataSet RunProcReturnDataSet(string procName, IDbDataParameter[] prams)
        {
            DataSet Ds = new DataSet();
            try
            {
                OracleDataAdapter Da = new OracleDataAdapter(CreateProcCmd(procName, prams));
                Da.Fill(Ds);
            }
            catch (Exception Ex)
            {
                throw new Exception(procName + Ex.Message);
            }
            finally
            {
                if (null == transaction)
                {
                    Close();
                }
            }
            return Ds;
        }
        #endregion 运行存储过程如果存储过程有参数需提前添加(AddParameter())，或传参数 prams
        #endregion
        #endregion

    }

        */


    //public interface IXXX
    //{
    //    string ExeCmd();

    //    void ExeProcduce();
    //}

    //public class SQLOBJ : IXXX
    //{
    //    public string ExeCmd()
    //    {
    //        //throw new NotImplementedException();
    //        return "SQL 操作";

    //    }


    //    public void ExeProcduce()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //public class OCLOBJ : IXXX
    //{
    //    public string ExeCmd()
    //    {
    //        //throw new NotImplementedException();
    //        return "Oracle 操作";
    //    }


    //    public void ExeProcduce()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
