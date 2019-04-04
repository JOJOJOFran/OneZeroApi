using Microsoft.EntityFrameworkCore;
using OneZero.Common.Extensions;
using OneZero.Exceptions;
using OneZero.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OneZero.EntityFrameworkCore.Extensions
{
    public static class ModelBuilderExtension
    {
        /// <summary>
        /// 通过程序集自动进行EF实体配置
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="assembly"></param>
        /// <param name="path"></param>
        public static void AddEntityConfigFromAssembly(this ModelBuilder builder, string path = null) //OneZeroOption oneZeroOption,
        {
            int count = 0;
            //var dbOption = oneZeroOption.DbContextCenter[typeof(DefaultDbContext)];
            foreach (var file in Directory.GetFiles(path ?? AppDomain.CurrentDomain.BaseDirectory, "*.dll"))
            {
                var types = Assembly.LoadFrom(file).LoadGenericInterfaceEntityConfigration(typeof(IEntityTypeConfiguration<>));
                if (types == null || types.Count() < 1)
                    continue;

                foreach (var item in types)
                {
                    if (item.IsAbstract)
                    {
                        Console.WriteLine(item);
                    }
                  //  dbOption.EntityInstanceList.Add(item);
                    count++;
                    dynamic instance = Activator.CreateInstance(item);
                    builder.ApplyConfiguration(instance);
                }
            }

            if (count <= 0)
                throw new OneZeroException("ModelBuilderExtension=>AddEntityConfigFromAssembly:程序集中实体配置为空");
        }


    }
}
