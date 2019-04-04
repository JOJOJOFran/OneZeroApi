using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OneZero.Common.Extensions
{
    public static class AssemblyExtension
    {
        /// <summary>
        /// 装载泛型接口实体配置类类型
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<Type> LoadGenericInterfaceEntityConfigration(this Assembly assembly, Type type)
        {

            return assembly.GetTypes().Where(v => !v.IsAbstract &&
                                                  !v.IsInterface &&
                                                  v.GetInterfaces().Any(x => x.GetTypeInfo().IsGenericType &&
                                                                                x.GetGenericTypeDefinition() == type));
        }
    }
}
