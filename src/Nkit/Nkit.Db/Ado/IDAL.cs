using System;
using System.Data; 
using System.Collections;

namespace Nkit.Db.Ado
{
    /// <summary>
    /// 数据库操作接口
    /// 所有操作只需提供SQL或存储过程
    /// 如果SQL或存储过程需要参数,需先执行AddParameter(string ParamName, object Value);  
    /// </summary>
    public interface IDAL
	{
		#region 设置连接
		/// <summary>
		/// 设置连接
		/// </summary>
		/// <param name="strConnectionString"></param>
		void SetConnection(string strConnectionString);
		#endregion
		#region 添加 SqlParameter
        /// <summary>
        /// 添加 SqlParameter
        /// </summary>
        /// <param name="ParamName">参数名</param>
        /// <param name="Value">参数值</param>
		void AddParameter(string ParamName, object Value);
		#endregion 添加 SqlParameter
        #region 清除 SqlParameter
        /// <summary>
        /// 清除 SqlParameter
        /// </summary>
        void ClearParameter(); 
        #endregion

		#region 判断表是否存在   返回 True/False   string SQL
		/// <summary>
		/// 判断数据库中表是否存在
		/// </summary>
		/// <param name="strTableName">表名称</param>
		/// <returns>返回值</returns>
		bool ExistTable(string strTableName);
		#endregion
		#region 检验是否存在数据 返回 True/False   string SQL
		/// <summary> 
		/// 检验是否存在数据 
		/// </summary> 
		/// <returns></returns>
		bool ExistData(string SQL) ;
		#endregion
		#region 执行SQL操作      返回 -1为失败 >0为成功   string SQL
		/// <summary> 
		/// 运行SQL语句 
		/// </summary> 
		/// <param name="SQL"></param>
		int ExeCmd(string SQL);
		#endregion 
		#region 返回DataSet对象  string SQL
		/// <summary> 
		/// 运行SQL语句,返回DataSet对象 
		/// </summary>
		/// <param name="procName">SQL语句</param> 
		DataSet ReturnDataSet(string SQL) ;
		/// <summary> 
		/// 运行SQL语句,返回DataSet对象 
		/// </summary> 
		DataSet ReturnDataSet(string SQL, int StartIndex ,int PageSize, string tablename ) ;
		#endregion
		#region 返回DataTable对象 string SQL
		/// <summary> 
		/// 运行SQL语句,返回DataTable对象 
		/// </summary>
		/// <param name="procName">SQL语句</param> 
		DataTable ReturnDataTable(string SQL) ;
		#endregion 返回DataTable对象 string SQL 
		#region 返回DataReader数据  string SQL
		/// <summary> 
		/// 返回SQL语句执行结果的第一行第一列 
		/// </summary> 
		/// <returns>字符串</returns> 
		object ReturnValue(string SQL) ;
		/// <summary> 
		/// 以集合方式返回指定名称字段列的数据
		/// </summary>
		/// <param name="SQL">sql语句</param>
		/// <returns></returns>
		ArrayList ReturnValues(string SQL);
		#endregion

		#region 事务
		/// <summary>
		/// 开始事务
		/// </summary>
		void BeginTransaction();
		/// <summary>
		/// 提交事务，如出现异常就撤消执行
		/// </summary>
		void CommitTransaction();
		/// <summary>
		/// 回滚事务
		/// </summary>
		void RollbackTransaction();
		#endregion 事务 ]

		//存储过程................................................................
		#region  运行存储过程,如果存储过程有参数需提前添加(AddParameter())，或传参数 prams
		/// <summary>
		/// 运行存储过程,返回 true 执行成功
		/// 如果存储过程有参数需提前添加(AddParameter())
		/// </summary>
		/// <param name="procName"></param>
		/// <returns></returns>
		bool RunProc(string procName) ;
		/// <summary> 
		/// 运行存储过程,返回 true 执行成功
		/// </summary> 
		/// <param name="procName">存储过程名</param> 
		/// <param name="prams">存储过程参数</param> 
		bool RunProc(string procName, IDbDataParameter[] prams) ;
		/// <summary>
		/// 运行存储过程,返回DataTable
		/// 如果存储过程有参数需提前添加(AddParameter())
		/// </summary>
		/// <param name="procName">存储过程名</param>
		/// <returns>DataTable对象</returns>
		DataTable RunProcReturnDatatable(string procName) ;
		/// <summary> 
		/// 运行存储过程,返回DataTable. 
		/// </summary> 
		/// <param name="procName">存储过程名.</param> 
		/// <param name="prams">存储过程入参数组.</param> 
		/// <returns>DataTable对象.</returns> 
		DataTable RunProcReturnDatatable(string procName,IDbDataParameter[] prams) ;
		/// <summary>
		/// 运行存储过程,返回DataTable
		/// 如果存储过程有参数需提前添加(AddParameter())
		/// </summary>
		/// <param name="procName">存储过程名</param>
		/// <returns>DataSet对象</returns>
		DataSet RunProcReturnDataSet(string procName) ;
		/// <summary> 
		/// 运行存储过程,返回DataTable. 
		/// </summary> 
		/// <param name="procName">存储过程名.</param> 
		/// <param name="prams">存储过程入参数组.</param> 
		/// <returns>DataSet对象.</returns> 
		DataSet RunProcReturnDataSet(string procName,IDbDataParameter[] prams) ;
		#endregion 运行存储过程如果存储过程有参数需提前添加(AddParameter())，或传参数 prams
	}
}
