using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using DbProxy;
using System.Configuration;
using DataMapper;
using AutoServiceLibrary;
using FirebirdSql.Data.FirebirdClient;
using WorkWithExcelLibrary;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkWithExcel.MakeWayBillInExcel("2");
            Console.ReadLine();
        }
        //TimerCallback tc = new TimerCallback(PrintTime);
        //Timer timer = new Timer(tc, "sasi", 0, 1000);
        static void PrintTime(object state)
        {
            Console.Clear();
            Console.Write($"{DateTime.Now.ToLongTimeString()} {state}");
        }
    }
}
