using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace TestApp
{
    public class Printer
    {
        //private object lockToken = new object();
        //public void PrintNumbers()
        //{
        //    lock (lockToken)
        //    {
        //        Console.WriteLine($"{Thread.CurrentThread.Name}->" +
        //        $" is executing Print");
        //        Console.WriteLine("Your numbers: ");
        //        for (int i = 0; i < 10; i++)
        //        {
        //            Random rnd = new Random();
        //            Console.Write($"{i} ");
        //            Thread.Sleep(1000 * rnd.Next(5));
        //        }
        //        Console.WriteLine();
        //    }
        //}
    }
    class Program
    {
        static void Main(string[] args)
        {
            TimerCallback tc = new TimerCallback(PrintTime);

            Timer timer = new Timer(tc, "sasi", 0, 1000);
            Console.ReadLine();
        }
        static void PrintTime(object state)
        {
            Console.Clear();
            Console.Write($"{DateTime.Now.ToLongTimeString()} {state}");
        }
    }
}
