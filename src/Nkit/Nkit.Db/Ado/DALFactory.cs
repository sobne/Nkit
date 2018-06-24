using System;
using System.Reflection;

namespace Nkit.Db.Ado
{
    public class DALFactory
	{
		public static IDAL GetInstance()
		{            
			IDAL db = null;

            //数据操作对象的类型        
            //string typeName = ConfigHelper.GetAppSetting("typeName");
            //读取配置的程序集名称
            //string assemblyName = "Utility4Net"; //ConfigurationSettings.AppSettings["assemblyName"];
            ////加载程序集(加载数据访问组件Common)
            //Assembly ab = Assembly.Load(assemblyName);
            ////通过程序集对象,创建数据访问实例对象
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
