using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceLibrary
{
    public class SystemOwner : Company
    {
        public Employee Director { get; set; }
        public SystemOwner() { }
        public SystemOwner(Employee director, string inn, string name, Bank bank,
                       string phoneNumber, string email,
                       string bill, string kpp, string oktmo, string okato,
                       string ogrn, string address, string factAddress)
            : base(inn, name, bank, phoneNumber, email, bill, kpp,
                   oktmo, okato, ogrn, address, factAddress)
        {
            Director = director;
        }
        public override string ToString()
        {
            return $"{Name}, ИНН: {INN}, Адрес: {Address}"; 
        }
    }
}
