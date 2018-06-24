<!--在配置文件中加如下-->


<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <appSettings>
    <!--数据库类型(mssql|oledb|oracle|mysql|sqlite)-->
    <add key="dbtype" value="sqlite"/>
    <!--数据访问类的类名(名称空间.类名) 继承IDAL接口的类-->
    <!--MSSqlDAL,OracleDAL,OleDBDAL,SQLiteDAL-->
    <add key="typeName" value="Nkit.Db.Ado.MSSqlDAL"/>
  </appSettings>
  <connectionStrings>
    <!-- 连接字符串(name同appSetting中的dbtype的value) -->
    <add name="mssql" connectionString="server=.;database=AccountBook;uid=sa;pwd=sa"/>
    <add name="oledb" connectionString="~/App_Data/AccountBook.mdb"/>
    <add name="oracle" connectionString="Data Source=AccountBook;User Id=sa;Password=sa;"/>
    <add name="mysql" connectionString="server=.;database=test;uid=sa;pwd=sa"/>
    <add name="sqlite" connectionString="Data Source=F:\1.DotNet\AccountBook\AccountBook\App_Data\Account.db;"/>
  </connectionStrings>
</configuration>


调用:
    IDAL idb = DALFactory.GetInstance();
    string sqlAccess = @"SELECT * FROM  tuser";
    DataTable dt = idb.ReturnDataTable(sqlAccess);
