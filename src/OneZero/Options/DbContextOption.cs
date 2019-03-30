using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    public class DbContextOption
    {
        public string ConnectString { get; set; }

        public DbType DBType;

        public Type DbContextType;

        public Dictionary<Type, Type> EntityList { get; set; }
    }
}
