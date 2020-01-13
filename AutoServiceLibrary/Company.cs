using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceLibrary
{
    abstract public class Company
    {
        public int IdCompany { get; set; }
        public string INN { get; set; }
        public string Name { get; set; }
        public Bank Bank { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Bill { get; set; }
        public string KPP { get; set; }
        public string OKTMO { get; set; }
        public string OKATO { get; set; }
        public string OGRN { get; set; }
        public string Address { get; set; }
        public string FactAddress { get; set; }
        public Company() { }
        public Company(string inn, string name, Bank bank,
                       string phoneNumber, string email,
                       string bill, string kpp, string oktmo, string okato,
                       string ogrn, string address, string factAddress)
        {
            INN = inn;
            Name = name;
            Bank = bank;
            PhoneNumber = phoneNumber;
            Email = email;
            Bill = bill;
            KPP = kpp;
            OKTMO = oktmo;
            OKATO = okato;
            OGRN = ogrn;
            Address = address;
            FactAddress = factAddress;
        }
    }
}
