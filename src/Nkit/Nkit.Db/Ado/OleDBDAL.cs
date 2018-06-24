using System;
using System.Data; 
using System.Data.OleDb; 
using System.Collections;

namespace Nkit.Db.Ado
{
	/// <summary>
	/// ֧�� Access(�޴洢��) SQL Oracle
	/// </summary>
	public class OleDBDAL:IDAL
	{
		#region Private Variables
		string connectionString;// = ConfigurationSettings.AppSettings["OleDbConnectionString"];
		OleDbConnection connection;
		OleDbTransaction transaction;
		OleDbCommand cmd;
        Hashtable listParameters;  //����ǰ���
		#endregion
        /// <summary>
        /// OLEDB��ʽ���ݿ����
        /// </summary>
		public OleDBDAL()
		{
            connectionString = ConfigHelper.ConnectionString;
			connection = new OleDbConnection(connectionString);
		}
		/// <summary>
        /// OLEDB��ʽ���ݿ����
		/// </summary>
		/// <param name="connectionString"></param>
		public OleDBDAL(string connectionString)
		{
			this.connectionString = connectionString;
			connection = new OleDbConnection(connectionString);
		}

		#region  ���أ���������,��ʼ����Ա
		/// <summary>
		/// ������
		/// </summary>
		public void Open()
		{
			if (connection.State  == ConnectionState.Closed)
			{
				connection.Open();
			}
		}
		/// <summary>
		/// �ر�����
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
		/// ��ʼ����Ա
		/// </summary>
		void InitMember()
		{
			listParameters = null;
			cmd = null;
		}
		public void Dispose() 
		{ 
			if(connection!=null) 
			{ 
				connection.Close(); 
				connection.Dispose(); 
			} 
			GC.Collect(); 
		} 
		#endregion
		#region ��������
		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="strConnectionString"></param>
		public void SetConnection(string strConnectionString)
		{
			connectionString = strConnectionString;
			connection = new OleDbConnection(connectionString);
		}
		#endregion
		#region ����������Command����
		/// <summary>
		/// ���ô������OleDbCommand������
		/// </summary>
		/// <param name="SQL"></param>
		/// <param name="cmdType"></param>
		public OleDbCommand SetCmd(string SQL, CommandType cmdType)
		{
			try
			{
				Open();
				if (null == cmd)
				{
					cmd = new OleDbCommand(SQL,connection);
				}
				cmd.CommandType = cmdType;
				cmd.CommandText = SQL;
				if(listParameters != null)
                {
                    foreach (DictionaryEntry parameter in listParameters)
                    {
                        cmd.Parameters.Add(new OleDbParameter(parameter.Key.ToString(), parameter.Value));
                    }
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
			return cmd;
		} 
		/// <summary> 
		/// ����Command���� 
		/// </summary> 
		/// <param name="SQL"></param> 
		/// <param name="Conn"></param> 
		/// <returns></returns> 
		public OleDbCommand CreateCmd(string SQL) 
		{ 
			return SetCmd(SQL, CommandType.Text);
		}

		#endregion ����������Command����
		#region ����OleDbDataReader  string SQL

		/// <summary> 
		/// ����SQL��䷵��DataReader 
		/// </summary> 
		/// <param name="SQL"></param> 
		/// <returns>OleDbDataReader����.</returns> 
		public OleDbDataReader ReturnDataReader(string SQL) 
		{
			return CreateCmd(SQL).ExecuteReader();
		}
		#endregion
        #region ��� SqlParameter
        /// <summary>
        /// ��� SqlParameter
        /// </summary>
        /// <param name="ParamName">������</param>
        /// <param name="Value">����ֵ</param>
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
			catch(Exception e)
			{
				throw new Exception("���OleDbParameter����:  "+e.Message);
			}
        }
        #endregion ��� SqlParameter
        #region ��� SqlParameter
        /// <summary>
        /// ��� SqlParameter
        /// </summary>
        public void ClearParameter() { listParameters = null; }
        #endregion 

		#region ���� OleDbDataAdapter string SQL
		OleDbDataAdapter GetDataAdapter(string SQL)
		{
			OleDbDataAdapter Da = new OleDbDataAdapter();

			cmd = SetCmd(SQL, CommandType.Text);
			Da.SelectCommand = cmd;
			OleDbCommandBuilder custCB = new OleDbCommandBuilder(Da);
			return Da; 
		}
		#endregion

		#region �жϱ��Ƿ����   ���� True/False   string SQL
		/// <summary>
		/// �ж����ݿ��б��Ƿ����
		/// </summary>
		/// <param name="strTableName">������</param>
		/// <returns>����ֵ</returns>
		public bool ExistTable(string strTableName)
		{   
			bool ret = false;
			try
			{
				string sql = "select   name   from   sysobjects where name = '" +strTableName+ "'";
				ret = ExistData(sql);
			}
			catch(Exception e)
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
		#region �����Ƿ�������� ���� True/False   string SQL
		/// <summary> 
		/// �����Ƿ�������� 
		/// </summary> 
		/// <returns></returns>
		public bool ExistData(string SQL) 
		{ 
			bool ret = false;
			try
			{
				System.Data.OleDb.OleDbDataReader dr = ReturnDataReader(SQL);
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
        #region ִ��SQL����      ���� -1Ϊʧ�� >0Ϊ�ɹ�   string SQL
        /// <summary> 
		/// ����SQL��� 
		/// </summary> 
		/// <param name="SQL"></param>
		public int ExeCmd(string SQL)
		{ 
			int ret = -1;
			try 
			{
				ret = CreateCmd(SQL).ExecuteNonQuery();
			} 
			catch(Exception ee)
			{ 
				throw new Exception(SQL+ee.ToString()); 
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
		#region ����DataSet  string SQL

		/// <summary> 
		/// ����SQL���,����DataSet���� 
		/// </summary>
		/// <param name="procName">SQL���</param> 
		public DataSet ReturnDataSet(string SQL) 
		{
			DataSet Ds = new DataSet();
			try
			{ 
				OleDbDataAdapter Da = GetDataAdapter(SQL); 
				Da.Fill(Ds); 
			} 
			catch(Exception Err) 
			{ 
				throw new Exception(SQL+Err.ToString()); 
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
		/// ����SQL���,����DataSet���� 
		/// </summary> 
		/// <param name="procName">SQL���</param> 
		/// <param name="prams">DataSet����</param> 
		/// <param name="dataReader">����</param> 
		public DataSet ReturnDataSet(string SQL, string tablename) 
		{ 
			DataSet Ds = new DataSet();
			try 
			{ 
				OleDbDataAdapter Da = GetDataAdapter(SQL); 
				Da.Fill(Ds,tablename); 
			} 
			catch(Exception Err) 
			{ 
				throw new Exception(SQL+Err.ToString()); 
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
		/// ����SQL���,����DataSet���� 
		/// </summary> 
		public DataSet ReturnDataSet(string SQL, int StartIndex ,int PageSize, string tablename ) 
		{ 
			DataSet Ds = new DataSet();
			try 
			{ 
				OleDbDataAdapter Da = GetDataAdapter(SQL);
				Da.Fill(Ds, StartIndex, PageSize, tablename);
			} 
			catch(Exception Err) 
			{ 
				throw new Exception(SQL+Err.ToString()); 
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
		#region ����DataTable���� string SQL
		/// <summary> 
		/// ����SQL���,����DataTable���� 
		/// </summary>
		/// <param name="procName">SQL���</param> 
		public DataTable ReturnDataTable(string SQL) 
		{
			DataTable dt = new DataTable();
			try
			{ 
				OleDbDataAdapter Da = GetDataAdapter(SQL); 
				Da.Fill(dt); 
			} 
			catch(Exception Err) 
			{ 
				throw new Exception(SQL+Err.ToString()); 
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
		#endregion ����DataTable���� string SQL 
		#region ����DataReader����  string SQL
		/// <summary> 
		/// ����SQL���ִ�н���ĵ�һ�е�һ�� 
		/// </summary> 
		/// <returns>�ַ���</returns> 
        public object ReturnValue(string SQL) 
		{
            object result = ""; 
			try 
			{
				result = CreateCmd(SQL).ExecuteScalar().ToString();
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
		/// ���ַ�����ʽ���ص�һ��ָ�������ֶ�
		/// </summary> 
		/// <returns>���ַ�����ʽ���ظ��ֶζ�Ӧ������</returns> 
		public string ReturnValue(string SQL,string Filed)
		{
			string ret = ""; 
			OleDbDataReader dr = null;
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
		/// ����SQL����һ��,��ColumnI��, 
		/// </summary> 
		/// <returns>�ַ���</returns> 
		public string ReturnValue(string SQL, int ColumnI) 
		{ 
			string result =String.Empty; 
			OleDbDataReader dr = null;
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
		/// �Լ��Ϸ�ʽ����ָ�������ֶ��е�����
		/// </summary>
		/// <param name="SQL">sql���</param>
		/// <returns></returns>
		public ArrayList ReturnValues(string SQL)
		{
			ArrayList ret = new ArrayList();
			OleDbDataReader dr = null;
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
		/// �Լ��Ϸ�ʽ����ָ�������ֶ��е�����
		/// </summary>
		/// <param name="SQL">sql���</param>
		/// <param name="Filed">Ҫ���ص��ֶ�</param>
		/// <returns></returns>
		public ArrayList ReturnValues(string SQL,string Filed)
		{
			ArrayList ret = new ArrayList();
			OleDbDataReader dr = null;
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

		#region ����
		/// <summary>
		/// ��ʼ����
		/// </summary>
		public void BeginTransaction()
		{
			Open();
			cmd = connection.CreateCommand(); 
			transaction = connection.BeginTransaction();

			cmd.Transaction = transaction;
		}
		/// <summary>
		/// �ύ����������쳣�ͳ���ִ��
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
		/// �ع�����
		/// </summary>
		public void RollbackTransaction()
		{
			if (null != transaction)
			{
				transaction.Rollback();
			}
		}
		#endregion 

		//�洢����................................................................
		#region ���д洢����,����洢�����в�������ǰ���(AddParameter())���򴫲��� prams
		#region ����һ���洢����ʹ�õ�sqlcommand.  CreateProcCmd
		/// <summary> 
		/// ����һ���洢����ʹ�õ�sqlcommand. 
		/// </summary> 
		/// <param name="procName">�洢������.</param> 
		/// <returns>OleDbCommand����.</returns> 
		public OleDbCommand CreateProcCmd(string procName) 
		{ 
			return SetCmd(procName,CommandType.StoredProcedure);
		} 
		/// <summary> 
		/// ����һ���洢����ʹ�õ�sqlcommand. 
		/// </summary> 
		/// <param name="procName">�洢������.</param> 
		/// <param name="prams">�洢�����������.</param> 
		/// <returns>OleDbCommand����.</returns> 
		public OleDbCommand CreateProcCmd(string procName, IDbDataParameter[] prams) 
		{ 
			cmd = SetCmd(procName,CommandType.StoredProcedure);
			if (prams != null) 
			{ 
				foreach (OleDbParameter parameter in prams) 
				{ 
					cmd.Parameters.Add(parameter); 
				} 
			} 
			return cmd;
		}
		#endregion
		#region ���ɴ洢���̲���
		public OleDbParameter MakeInParam(string ParamName, object Value) 
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
		public OleDbParameter MakeInParam(string ParamName, OleDbType dbType, object Value) 
		{
			return MakeParam(ParamName, dbType, ParameterDirection.Input, Value);
		}
		/// <summary>
		/// Make input param.
		/// </summary>
		/// <param name="ParamName">Name of param.</param>
		/// <param name="DbType">Param type.</param>
		/// <param name="Size">Param size.</param>
		/// <param name="Value">Param value.</param>
		/// <returns>New parameter.</returns>
		public OleDbParameter MakeInParam(string ParamName, OleDbType dbType, int Size, object Value) 
		{
			return MakeParam(ParamName, dbType, Size, ParameterDirection.Input, Value);
		}  
		/// <summary>
		/// Make input param.
		/// </summary>
		/// <param name="ParamName">Name of param.</param>
		/// <param name="DbType">Param type.</param>
		/// <returns>New parameter.</returns>
		public OleDbParameter MakeOutParam(string ParamName, OleDbType dbType) 
		{
			return MakeParam(ParamName, dbType, ParameterDirection.Output);
		}
		/// <summary>
		/// Make input param.
		/// </summary>
		/// <param name="ParamName">Name of param.</param>
		/// <param name="DbType">Param type.</param>
		/// <param name="Size">Param size.</param>
		/// <returns>New parameter.</returns>
		public OleDbParameter MakeOutParam(string ParamName, OleDbType dbType, int Size) 
		{
			return MakeParam(ParamName, dbType, Size, ParameterDirection.Output, null);
		}   

		/// <summary>
		/// Make stored procedure param.
		/// </summary>
		/// <param name="ParamName"></param>
		/// <param name="Direction"></param>
		/// <param name="Value"></param>
		/// <returns></returns>
		public OleDbParameter MakeParam(string ParamName,ParameterDirection Direction, object Value)
		{    
			OleDbParameter param;
			param = new OleDbParameter(ParamName, Value);
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
		public OleDbParameter MakeParam(string ParamName, OleDbType dbType, ParameterDirection Direction)
		{    
			OleDbParameter param;
			param = new OleDbParameter(ParamName, dbType);
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
		public OleDbParameter MakeParam(string ParamName, OleDbType dbType,
			ParameterDirection Direction,object Value)
		{    
			OleDbParameter param;
			param = new OleDbParameter(ParamName, dbType);
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
		public OleDbParameter MakeParam(string ParamName, OleDbType dbType, Int32 Size,
			ParameterDirection Direction, object Value) 
		{
			OleDbParameter param;
			if(Size > 0)
				param = new OleDbParameter(ParamName,dbType,Size);
			else
				param = new OleDbParameter(ParamName, dbType);
			param.Direction = Direction;
			if (!(Direction == ParameterDirection.Output && Value == null))
				param.Value = Value;
			return param;
		}
		#endregion
		#region  ���д洢����,����洢�����в�������ǰ���(AddParameter())���򴫲��� prams
		/// <summary>
		/// ���д洢����,���� true ִ�гɹ�
		/// ����洢�����в�������ǰ���(AddParameter())
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
		/// ���д洢����,���� true ִ�гɹ�
		/// </summary> 
		/// <param name="procName">�洢������</param> 
		/// <param name="prams">�洢���̲���</param> 
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
		/// ���д洢����,����DataTable
		/// ����洢�����в�������ǰ���(AddParameter())
		/// </summary>
		/// <param name="procName">�洢������</param>
		/// <returns>DataTable����</returns>
		public DataTable RunProcReturnDatatable(string procName) 
		{ 
			DataTable Ds = new DataTable(); 
			try 
			{
				OleDbDataAdapter Da = new OleDbDataAdapter(CreateProcCmd(procName));
				Da.Fill(Ds); 
			} 
			catch(Exception Ex) 
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
		/// ���д洢����,����DataTable. 
		/// </summary> 
		/// <param name="procName">�洢������.</param> 
		/// <param name="prams">�洢�����������.</param> 
		/// <returns>DataTable����.</returns> 
		public DataTable RunProcReturnDatatable(string procName,IDbDataParameter[] prams) 
		{ 
			DataTable Ds = new DataTable(); 
			try 
			{
				OleDbDataAdapter Da = new OleDbDataAdapter(CreateProcCmd(procName, prams));
				Da.Fill(Ds); 
			} 
			catch(Exception Ex) 
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
		/// ���д洢����,����DataTable
		/// ����洢�����в�������ǰ���(AddParameter())
		/// </summary>
		/// <param name="procName">�洢������</param>
		/// <returns>DataSet����</returns>
		public DataSet RunProcReturnDataSet(string procName) 
		{ 
			DataSet Ds = new DataSet(); 
			try 
			{
				OleDbDataAdapter Da = new OleDbDataAdapter(CreateProcCmd(procName));
				Da.Fill(Ds); 
			} 
			catch(Exception Ex) 
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
		/// ���д洢����,����DataTable. 
		/// </summary> 
		/// <param name="procName">�洢������.</param> 
		/// <param name="prams">�洢�����������.</param> 
		/// <returns>DataSet����.</returns> 
		public DataSet RunProcReturnDataSet(string procName,IDbDataParameter[] prams) 
		{ 
			DataSet Ds = new DataSet(); 
			try 
			{
				OleDbDataAdapter Da = new OleDbDataAdapter(CreateProcCmd(procName, prams));
				Da.Fill(Ds); 
			} 
			catch(Exception Ex) 
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
		#endregion ���д洢��������洢�����в�������ǰ���(AddParameter())���򴫲��� prams
		#endregion
	}
}
