using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;

namespace OneZero.EntityFramwork.EntityConfiguration.Extension
{
    public static class ModelBuilderExtension
    {
        public static void AddEntityConfigurationFromAssembly(this ModelBuilder builder, Assembly assembly)
        {
            var types= assembly.LoadEntityConfigration(typeof(IEntityTypeConfiguration<>));
            if (types == null || types.Count() < 1)
                //throwException(); 抛出自定义异常
                return;

            foreach (var item in types)
            {
                Console.WriteLine(item.Name);
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
