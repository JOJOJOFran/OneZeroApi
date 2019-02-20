using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Common.Dapper
{
    /// <summary>
    /// Dapper方法扩展接口
    /// </summary>
    public interface IDapperProvider
    {
        IEnumerable<dynamic> FromSql(string connectionString, string sql,object param=null);
    }
}
