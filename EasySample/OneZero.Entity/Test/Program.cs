using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OneZero.Entity
{
    public class Program
    {
        static void Main(string[] args)
        {
            var types=Assembly.GetExecutingAssembly().GetTypes().Where(v => !v.GetType().IsAbstract &&
                                                  !v.GetType().IsInterface &&
                                                  typeof(IEntityTypeConfiguration<>).GetInterfaces().Any(x => x.GetTypeInfo().IsGenericType &&
                                                                                x.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));
            foreach (var item in types)
            {
                Console.WriteLine(item.Name);
            }
            Console.ReadKey();
        }
    }
}
