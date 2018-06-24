using System;
using System.Data; 
using System.Collections;

namespace Nkit.Db.Ado
{
    /// <summary>
    /// ���ݿ�����ӿ�
    /// ���в���ֻ���ṩSQL��洢����
    /// ���SQL��洢������Ҫ����,����ִ��AddParameter(string ParamName, object Value);  
    /// </summary>
    public interface IDAL
	{
		#region ��������
		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="strConnectionString"></param>
		void SetConnection(string strConnectionString);
		#endregion
		#region ��� SqlParameter
        /// <summary>
        /// ��� SqlParameter
        /// </summary>
        /// <param name="ParamName">������</param>
        /// <param name="Value">����ֵ</param>
		void AddParameter(string ParamName, object Value);
		#endregion ��� SqlParameter
        #region ��� SqlParameter
        /// <summary>
        /// ��� SqlParameter
        /// </summary>
        void ClearParameter(); 
        #endregion

		#region �жϱ��Ƿ����   ���� True/False   string SQL
		/// <summary>
		/// �ж����ݿ��б��Ƿ����
		/// </summary>
		/// <param name="strTableName">������</param>
		/// <returns>����ֵ</returns>
		bool ExistTable(string strTableName);
		#endregion
		#region �����Ƿ�������� ���� True/False   string SQL
		/// <summary> 
		/// �����Ƿ�������� 
		/// </summary> 
		/// <returns></returns>
		bool ExistData(string SQL) ;
		#endregion
		#region ִ��SQL����      ���� -1Ϊʧ�� >0Ϊ�ɹ�   string SQL
		/// <summary> 
		/// ����SQL��� 
		/// </summary> 
		/// <param name="SQL"></param>
		int ExeCmd(string SQL);
		#endregion 
		#region ����DataSet����  string SQL
		/// <summary> 
		/// ����SQL���,����DataSet���� 
		/// </summary>
		/// <param name="procName">SQL���</param> 
		DataSet ReturnDataSet(string SQL) ;
		/// <summary> 
		/// ����SQL���,����DataSet���� 
		/// </summary> 
		DataSet ReturnDataSet(string SQL, int StartIndex ,int PageSize, string tablename ) ;
		#endregion
		#region ����DataTable���� string SQL
		/// <summary> 
		/// ����SQL���,����DataTable���� 
		/// </summary>
		/// <param name="procName">SQL���</param> 
		DataTable ReturnDataTable(string SQL) ;
		#endregion ����DataTable���� string SQL 
		#region ����DataReader����  string SQL
		/// <summary> 
		/// ����SQL���ִ�н���ĵ�һ�е�һ�� 
		/// </summary> 
		/// <returns>�ַ���</returns> 
		object ReturnValue(string SQL) ;
		/// <summary> 
		/// �Լ��Ϸ�ʽ����ָ�������ֶ��е�����
		/// </summary>
		/// <param name="SQL">sql���</param>
		/// <returns></returns>
		ArrayList ReturnValues(string SQL);
		#endregion

		#region ����
		/// <summary>
		/// ��ʼ����
		/// </summary>
		void BeginTransaction();
		/// <summary>
		/// �ύ����������쳣�ͳ���ִ��
		/// </summary>
		void CommitTransaction();
		/// <summary>
		/// �ع�����
		/// </summary>
		void RollbackTransaction();
		#endregion ���� ]

		//�洢����................................................................
		#region  ���д洢����,����洢�����в�������ǰ���(AddParameter())���򴫲��� prams
		/// <summary>
		/// ���д洢����,���� true ִ�гɹ�
		/// ����洢�����в�������ǰ���(AddParameter())
		/// </summary>
		/// <param name="procName"></param>
		/// <returns></returns>
		bool RunProc(string procName) ;
		/// <summary> 
		/// ���д洢����,���� true ִ�гɹ�
		/// </summary> 
		/// <param name="procName">�洢������</param> 
		/// <param name="prams">�洢���̲���</param> 
		bool RunProc(string procName, IDbDataParameter[] prams) ;
		/// <summary>
		/// ���д洢����,����DataTable
		/// ����洢�����в�������ǰ���(AddParameter())
		/// </summary>
		/// <param name="procName">�洢������</param>
		/// <returns>DataTable����</returns>
		DataTable RunProcReturnDatatable(string procName) ;
		/// <summary> 
		/// ���д洢����,����DataTable. 
		/// </summary> 
		/// <param name="procName">�洢������.</param> 
		/// <param name="prams">�洢�����������.</param> 
		/// <returns>DataTable����.</returns> 
		DataTable RunProcReturnDatatable(string procName,IDbDataParameter[] prams) ;
		/// <summary>
		/// ���д洢����,����DataTable
		/// ����洢�����в�������ǰ���(AddParameter())
		/// </summary>
		/// <param name="procName">�洢������</param>
		/// <returns>DataSet����</returns>
		DataSet RunProcReturnDataSet(string procName) ;
		/// <summary> 
		/// ���д洢����,����DataTable. 
		/// </summary> 
		/// <param name="procName">�洢������.</param> 
		/// <param name="prams">�洢�����������.</param> 
		/// <returns>DataSet����.</returns> 
		DataSet RunProcReturnDataSet(string procName,IDbDataParameter[] prams) ;
		#endregion ���д洢��������洢�����в�������ǰ���(AddParameter())���򴫲��� prams
	}
}
