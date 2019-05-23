using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OneZero.EntityFrameworkCore.Extensions
{
    public static class DbExceptionExtension
    {
        public const int SqlServerViolationOfUniqueIndex = 2601;
        public const int SqlServerViolationOfUniqueConstraint = 2627;
        //唯一索引匹配,获取按规则定义名称的唯一索引
        private static readonly Regex UniqueConstraintRegex =new Regex("'UniqueIndex_([a-zA-Z0-9]*)_([a-zA-Z0-9]*)'", RegexOptions.Compiled);
        public static string  SaveChangesExceptionHandler(this Exception e, DbContext dbContext)
        {
            var dbUpdateEx = e as DbUpdateException;
            var sqlEx = dbUpdateEx?.InnerException as SqlException;
            var errorMsg = e?.InnerException?.Message;
            if (sqlEx != null)
            {
                if (sqlEx.Number.Equals(SqlServerViolationOfUniqueIndex) || sqlEx.Number.Equals(SqlServerViolationOfUniqueIndex))
                {
                    //currently the entitiesNotSaved is empty for unique constraints - see https://github.com/aspnet/EntityFrameworkCore/issues/7829
                    errorMsg = UniqueErrorFormatter(sqlEx, dbUpdateEx.Entries)?? errorMsg;
                }
            }

            return errorMsg;
        }


        public static string UniqueErrorFormatter(SqlException ex, IReadOnlyList<EntityEntry> entitiesNotSaved)
        {
            var message = ex.Errors[0].Message;
            var matches = UniqueConstraintRegex.Matches(message);

            string  returnError =null;

            if (matches.Count > 0)
            {
                //currently the entitiesNotSaved is empty for unique constraints - see https://github.com/aspnet/EntityFrameworkCore/issues/7829
                var entityDisplayName = entitiesNotSaved.Count == 1
                ? entitiesNotSaved.Single().Entity.GetType().Name
                : matches[0].Groups[1].Value;

                returnError = $"{entityDisplayName}不能有重复的值"; 

                var openingBadValue = message.IndexOf("(");
                if (openingBadValue > 0)
                {
                    var dupPart = message.Substring(openingBadValue + 1,
                        message.Length - openingBadValue - 3);
                    returnError += $" 重复的值为 '{dupPart}'.";
                }
            }
             

            return returnError;
        }

    }
}
