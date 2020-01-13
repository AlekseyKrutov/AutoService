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

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Malfunctions malf = new Malfunctions();
            CardOfRepair card = new CardOfRepair();
            malf = new MalfMapper().Get("10");
            card = new CardMapper().Get("258");
            card.ListOfMalf.Add(malf);
            CardMapper cm = new CardMapper();
            cm.UpdateWorks(card);

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
