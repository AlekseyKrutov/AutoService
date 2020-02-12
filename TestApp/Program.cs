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
using AutoServiceLibrary;
using FirebirdSql.Data.FirebirdClient;
using WorkWithExcelLibrary;
using DataMapper;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee emp = new EmployeeMapper().Get("1037");
            AccountMapper am = new AccountMapper();
            Account account = new Account(emp, am.GetAccounts());
            am.Insert(account);
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
