using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OneZero.Common.Dapper
{
    /// <summary>
    /// Dapper方法扩展接口
    /// </summary>
    public interface IDapperProvider
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        IEnumerable<dynamic> FromSql(string connectionString, string sql,object param=null);

        /// <summary>
        /// 异步查询
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> FromSqlAsync(string connectionString, string sql, object param = null);

        /// <summary>
        /// 异步执行sql
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<int> ExcuteSqlAsync(string connectionString, string sql, object param = null);
    }
}
