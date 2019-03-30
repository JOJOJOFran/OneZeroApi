using Microsoft.EntityFrameworkCore;
using OneZero.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OneZero.EntityFrameworkCore.SqlServer.Extensions
{
    public static class ModelBuilderExtension
    {
        /// <summary>
        /// 通过程序集自动进行EF实体配置
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="assembly"></param>
        /// <param name="path"></param>
        public static void AddEntityConfigFromAssembly(this ModelBuilder builder, string path = null)
        {
            int count = 0;
            foreach (var file in Directory.GetFiles(path ?? AppDomain.CurrentDomain.BaseDirectory , "*.dll"))
            {
                var types =  Assembly.LoadFrom(file).LoadEntityConfigration(typeof(IEntityTypeConfiguration<>));
                if (types == null || types.Count() < 1)
                    continue;

                foreach (var item in types)
                {
                    count++;
                    dynamic instance = Activator.CreateInstance(item);
                    builder.ApplyConfiguration(instance);
                }
            }

            if(count<=0)
                throw new OneZeroException("ModelBuilderExtension=>AddEntityConfigFromAssembly:程序集中实体配置为空");
        }

        /// <summary>
        /// 装载实体配置类类型
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static IEnumerable<Type> LoadEntityConfigration(this Assembly assembly, Type type)
        {

            return assembly.GetTypes().Where(v => !v.GetType().IsAbstract &&
                                                  !v.GetType().IsInterface &&
                                                 // type.IsAssignableFrom(v)&&
                                                  v.GetInterfaces().Any(x => x.GetTypeInfo().IsGenericType &&
                                                                                x.GetGenericTypeDefinition() == type));
        }
    }
}
