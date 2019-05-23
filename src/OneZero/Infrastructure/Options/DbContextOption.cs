using OneZero.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Options
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    public class DbContextOption
    {
        public string ConnectString { get; set; }

        public DatabaseType DBType;

        public Type DbContextType;

        public  ConcurrentBag<Type> EntityInstanceList { get; set; }
    }
}
