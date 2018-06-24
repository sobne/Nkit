using System;
using System.Reflection;

namespace Nkit.Db.Ado
{
    public class DALFactory
	{
		public static IDAL GetInstance()
		{            
			IDAL db = null;

            //���ݲ������������        
            //string typeName = ConfigHelper.GetAppSetting("typeName");
            //��ȡ���õĳ�������
            //string assemblyName = "Utility4Net"; //ConfigurationSettings.AppSettings["assemblyName"];
            ////���س���(�������ݷ������Common)
            //Assembly ab = Assembly.Load(assemblyName);
            ////ͨ�����򼯶���,�������ݷ���ʵ������
            //db = (IDataAccess)ab.CreateInstance(typeName);

            //db = new Nkit.Db.Ado.SQLDBOperator();

            switch (ConfigHelper.dbtype)
            {
                case "mssql":
                    db = new MSSqlDAL();
                    break;
                //case "oracle":
                //    db = new OracleDAL();
                //    break;
                case "oledb":
                    db = new OleDBDAL();
                    break;
                case "sqlite":
                    db = new SQLiteDAL();
                    break;
            }
			return db;
		}
	}
}
