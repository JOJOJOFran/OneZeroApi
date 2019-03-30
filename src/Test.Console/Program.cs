using System;
using System.Collections.Concurrent;
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
            // GetAssembly3();

            //ConcurrentQueue<int> currentQueue = new ConcurrentQueue<int>();
            //currentQueue.Enqueue(1);
            //currentQueue.Enqueue(2);
            //currentQueue.Enqueue(3);
            //currentQueue.Enqueue(4);
            //int peek = 0;
            //currentQueue.TryPeek(out peek);
            //Console.WriteLine(peek);
            //foreach (var item in currentQueue)
            //{
            //    Console.WriteLine(item);
            //}
            //foreach (var item in currentQueue)
            //{
            //    int i = 0;
            //    currentQueue.TryDequeue(out i);
            //    Console.WriteLine(i);
            //}

            //string str = "1^20975961.jpg|2^9ef08fee-2aeb-4e08-96d2-6e6fd2449eba.jpg";

            int i = 4;
            int j = 5;
            Console.WriteLine(Convert.ToString(i,2));
            Console.WriteLine(Convert.ToString(j, 2));
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
