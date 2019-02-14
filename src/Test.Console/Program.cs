using System;
using System.IO;
using System.Reflection;

namespace Test.ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
            GetAssembly3();
            Console.ReadKey();          
        }


        public static void GetAssembly1()
        {
            var a = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            foreach (var assemblyName in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
            {
                Assembly assembly = Assembly.Load(assemblyName);
                foreach (var type in assembly.GetTypes())
                {
                    Console.WriteLine(type.Name);
                }
            }
        }

        public static void GetAssembly2()
        {
            foreach (var item in AppDomain.CurrentDomain.GetAssemblies())
            {
                Console.WriteLine(item.GetName());
            }
        }

        public static void GetAssembly3()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            foreach (var file in Directory.GetFiles(basePath + @"\", "*.dll"))
            {
                var asm = Assembly.LoadFrom(file);
                foreach (var type in asm.GetTypes())
                {
                    Console.WriteLine(type.Name);
                }
            }
            
        }
    }
}
