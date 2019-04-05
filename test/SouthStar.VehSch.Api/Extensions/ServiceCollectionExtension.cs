using Microsoft.Extensions.DependencyInjection;
using OneZero.Common.Extensions;
using OneZero.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection BatchScopedServiceRegister(this IServiceCollection services, Type serviceType,string path=null)
        {
            foreach (var file in Directory.GetFiles(path ?? AppDomain.CurrentDomain.BaseDirectory, "*.dll"))
            {
                var types = Assembly.LoadFrom(file).LoadAssemblyWithSubClassOfAbstractWithOutGeneric(serviceType);
                if (types == null || types.Count() < 1)
                    continue;
                foreach (var type in types)
                {
                    services.AddScoped(type);
                }
            }
            return services;
        }

        /// <summary>
        /// 获取从抽象类继承的非抽象方法
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<Type> LoadAssemblyWithSubClassOfAbstractWithOutGeneric(this Assembly assembly, Type type)
        {
            return assembly.GetTypes().Where(v => !v.IsAbstract && !v.IsInterface && v.IsSubclassOf(type));

        }
    }
}
