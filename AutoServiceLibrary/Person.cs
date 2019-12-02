using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceLibrary
{
    public abstract class Person
    {
        //переменная для хранения ФИО
        public string Name { get; set; }
        //переменная для хранения ИНН
        public string INN { get; set; }
        //переменная для хранения адреса
        public string Address { get; set; }
        //переменная для хранения номера телефона
        public string NumberOfTel { get; set; }
        //конструктор по умолчанию
        public Person() { }
        //конструктор с одним параметром
        public Person(string Name)
        {
            this.Name = Name;
            this.INN = "";
            this.Address = "";
            this.NumberOfTel = "";
        }
        //констуктор с 4 параметрами
        public Person(string Name, string Address, string NumberOfTel, string INN = "")
        {
            this.Name = Name;
            this.INN = INN;
            this.Address = Address;
            this.NumberOfTel = NumberOfTel;
        }
    }
}
