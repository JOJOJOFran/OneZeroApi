using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Text;
using OneZero.Exceptions;
using System.Threading.Tasks;

namespace OneZero.Common.Dapper
{
    /// <summary>
    /// Dapper方法扩展
    /// </summary>
    public class DapperProvider : IDapperProvider
    {
        public async Task<int> ExcuteSqlAsync(string connectionString, string sql, object param = null)
        {
            if (String.IsNullOrWhiteSpace(connectionString) || String.IsNullOrWhiteSpace(sql))
                throw new OneZeroException($"DapperProvider.FromSql:连接字符串:{connectionString}或sql语句:{sql}不合法", Enums.ResponseCode.UnExpectedException);
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var result = await conn.ExecuteAsync(sql, param);
                    return result;
                }
            }
            catch (Exception e)
            {
                throw new OneZeroException($"DapperProvider.FromSql:在数据库:{connectionString}执行:{sql}查询失败", e, Enums.ResponseCode.UnExpectedException);
            }
        }

        public IEnumerable<dynamic> FromSql(string connectionString, string sql, object param=null)
        {
            if (String.IsNullOrWhiteSpace(connectionString) || String.IsNullOrWhiteSpace(sql))
                throw new OneZeroException($"DapperProvider.FromSql:连接字符串:{connectionString}或sql语句:{sql}不合法",Enums.ResponseCode.UnExpectedException);
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var result = conn.Query(sql, param);
                    return result;
                }
            }
            catch (Exception e)
            {
                throw new OneZeroException($"DapperProvider.FromSql:在数据库:{connectionString}执行:{sql}查询失败", e,Enums.ResponseCode.UnExpectedException);
            }
        }


        public async Task<IEnumerable<dynamic>> FromSqlAsync(string connectionString, string sql, object param = null)
        {
            if (String.IsNullOrWhiteSpace(connectionString) || String.IsNullOrWhiteSpace(sql))
                throw new OneZeroException($"DapperProvider.FromSql:连接字符串:{connectionString}或sql语句:{sql}不合法", Enums.ResponseCode.UnExpectedException);
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    return await conn.QueryAsync(sql, param);
                }
            }
            catch (Exception e)
            {
                throw new OneZeroException($"DapperProvider.FromSql:在数据库:{connectionString}执行:{sql}查询失败", e, Enums.ResponseCode.UnExpectedException);
            }
        }


    }
}
