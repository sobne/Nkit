<!--�������ļ��м�����-->


<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <appSettings>
    <!--���ݿ�����(mssql|oledb|oracle|mysql|sqlite)-->
    <add key="dbtype" value="sqlite"/>
    <!--���ݷ����������(���ƿռ�.����) �̳�IDAL�ӿڵ���-->
    <!--MSSqlDAL,OracleDAL,OleDBDAL,SQLiteDAL-->
    <add key="typeName" value="Nkit.Db.Ado.MSSqlDAL"/>
  </appSettings>
  <connectionStrings>
    <!-- �����ַ���(nameͬappSetting�е�dbtype��value) -->
    <add name="mssql" connectionString="server=.;database=AccountBook;uid=sa;pwd=sa"/>
    <add name="oledb" connectionString="~/App_Data/AccountBook.mdb"/>
    <add name="oracle" connectionString="Data Source=AccountBook;User Id=sa;Password=sa;"/>
    <add name="mysql" connectionString="server=.;database=test;uid=sa;pwd=sa"/>
    <add name="sqlite" connectionString="Data Source=F:\1.DotNet\AccountBook\AccountBook\App_Data\Account.db;"/>
  </connectionStrings>
</configuration>


����:
    IDAL idb = DALFactory.GetInstance();
    string sqlAccess = @"SELECT * FROM  tuser";
    DataTable dt = idb.ReturnDataTable(sqlAccess);
