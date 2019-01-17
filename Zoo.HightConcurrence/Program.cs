using System;
using System.Threading;

namespace HighConcurrence.App
{
    class Program
    {
        static void Main(string[] args)
        {

           Console.WriteLine("输入回车键终止进程");
            
            var processer = new BussinessProcesser();
            do
            {
                while (!Console.KeyAvailable)
                {
                    var thread = new Thread(processer.Process);
                    thread.Start();

                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Enter);
        }
    }
}
