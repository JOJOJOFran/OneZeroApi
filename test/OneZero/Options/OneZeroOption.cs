
using OneZero.Enums;
using OneZero.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Options
{
    /// <summary>
    /// 框架配置类
    /// </summary>
    public class OneZeroOption
    {
        public ConcurrentDictionary<Type, DbContextOption> DbContextCenter { get; set; } = new ConcurrentDictionary<Type, DbContextOption>();
        public JwtOption JwtOption { get; set; }
        public QiniuOption QiniuOption { get; set; }
        public string DefaultTenanId { get; set; }
        public string IsAuthentic { get; set; }

        public void RegisterToDbContextCenter(Type type, DbContextOption dbContextOption)
        {
            if (!DbContextCenter.ContainsKey(type))
            {
                if (DbContextCenter.TryAdd(type, dbContextOption))
                {
                    throw new OneZeroException($"将数据库上下文{type.Name}放入配置中心发生异常", ResponseCode.UnExpectedException);
                }
            }
        }

    }





}
