using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OneZero.EntityFrameworkCore.SqlServer.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void AddEntityConfigFromAssembly(this ModelBuilder builder, Assembly assembly)
        {
            var types = assembly.LoadEntityConfigration(typeof(IEntityTypeConfiguration<>));
            if (types == null || types.Count() < 1)
                //throw new OneZeroException("ModelBuilderExtension=>AddEntityConfigFromAssembly:程序集中实体配置为空");
                return;

            foreach (var item in types)
            {
                dynamic instance = Activator.CreateInstance(item);
                builder.ApplyConfiguration(instance);
            }
        }


        private static IEnumerable<Type> LoadEntityConfigration(this Assembly assembly, Type type)
        {

            return assembly.GetTypes().Where(v => !v.GetType().IsAbstract &&
                                                  !v.GetType().IsInterface &&
                                                  //type.IsAssignableFrom(v)&&
                                                  v.GetInterfaces().Any(x => x.GetTypeInfo().IsGenericType &&
                                                                                x.GetGenericTypeDefinition() == type));
        }
    }
}
