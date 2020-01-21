using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceLibrary
{
    public class Client : Company
    {
        public string Director { get; set; }
        public double Discount { get; set; } = 0;
        //конструктор по умолчанию
        public Client() { }
        //конструктор с 4 параметрами
        public Client(string director, string inn, string name, Bank bank,
                      string phoneNumber, string email,
                      string bill, string kpp, string oktmo, string okato,
                      string ogrn, string address, string factAddress)
           : base(inn, name, bank, phoneNumber, email, bill, kpp,
                  oktmo, okato, ogrn, address, factAddress)
        {
            Director = director;
        }
        public string GetShortName()
        {
            string shortName = "";
            string[] nameArr = Director.Split(' ');
            shortName += nameArr.First() + " ";
            shortName += nameArr[1].ToList<char>().First<char>() + ".";
            shortName += nameArr.Last().ToList<char>().First<char>() + ".";
            return shortName;
        }
    }
}
