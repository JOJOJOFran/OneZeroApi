using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.EntityFramwork.Configuration
{
    public class OneZeroOption
    {
        public readonly DbType _dbType ;
        public readonly CacheType _cacheType ;
        public readonly bool IsOpenCache;

        /// <summary>
        /// 默认使用
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="cacheType"></param>
        public OneZeroOption(int dbType = 0x0000, int cacheType= 0x0000)
        {

            if (!Enum.IsDefined(typeof(DbType), dbType))
                //throw XXXException
                return;
            
            if (!Enum.IsDefined(typeof(CacheType), cacheType))
                //throw XXXException
                return;

            _dbType = (DbType)dbType;
            _cacheType = (CacheType)cacheType;
        }

        
    }

    public enum DbType
    {
        MySql = 0x0000,
        MSSql = 0x0001,
        PgSql = 0x0010

    }

    public enum CacheType
    {
        Redis = 0x0000
    }
}
