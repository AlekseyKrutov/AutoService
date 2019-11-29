using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using AutoServiceLibrary;

namespace TestApp
{
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
