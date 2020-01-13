using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceLibrary
{
    public class Bank
    {
        public string KorBill { get; set; }
        public string Name { get; set; }
        public string BIK { get; set; }
        public Bank() { }
        public Bank(string korBill, string name, string bik)
        {
            KorBill = korBill;
            Name = name;
            BIK = bik;
        }
    }
}
