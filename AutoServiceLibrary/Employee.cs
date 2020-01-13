using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

namespace AutoServiceLibrary
{
    public enum Sex { Male, Female, Nothing }
    public enum Functions { Locksmith = 1, Mechanic, Director, Cleaner, Admin, Seller, Driver, Nothing }
    public class Employee
    {
        public int TubNumb { get; set; }
        public string INN { get; set; }
        public Functions Function { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string Passport { get; set; }
        public string Address { get; set; }
        public DateTime BornDate { get; set; }
        public Sex Gender { get; set; }
        public string PhoneNumb { get; set; }
        public Account Account { get; set; }
        //конструктор по умолчанию
        public Employee() { }
        public Employee(int TubNUmb)
        {
            this.TubNumb = TubNUmb;
        }
        public Employee(string INN, string FirstName, string SecondName,
            string LastName, string Passport, string Address, DateTime BornDate, Sex Gender,
            string PhoneNumb, Functions Function)
        {
            this.INN = INN;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.LastName = LastName;
            this.Passport = Passport;
            this.Address = Address;
            this.BornDate = BornDate;
            this.Gender = Gender;
            this.PhoneNumb = PhoneNumb;
            this.Function = Function;
        }
        public Employee(int TubNumb, string INN, string FirstName, string SecondName,
            string LastName, string Passport, string Address, DateTime BornDate, Sex Gender,
            string PhoneNumb, Functions Function) : this (INN, FirstName, SecondName,
            LastName, Passport, Address, BornDate, Gender, PhoneNumb, Function)
        {
            this.TubNumb = TubNumb;
        }
        public static List<Employee> GetListPersonalFromDb(FbConnection db, int id_repair)
        {
            return new List<Employee>();
        }
        public override string ToString()
        {
            return this.TubNumb + "  " + this.FirstName + ' ' + this.SecondName + ' ' + this.LastName;
        }
    }
}
