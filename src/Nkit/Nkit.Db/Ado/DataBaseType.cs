using System;

namespace Nkit.Db.Ado
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    [Serializable]
    public enum DatabaseType 
    { 
        MsSql,
        Oracle,
        OleDB, 
        Sqlite
    }
}
