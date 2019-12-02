using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceLibrary
{
    public class Client : Person
    {
        //конструктор по умолчанию
        public Client() { }
        //конструктор с 1 параметром
        public Client(string Name) : base(Name) { }
        //конструктор с 4 параметрами
        public Client(string OwnerName, string INN, string Address, string NumberOfTel)
            : base(OwnerName, INN, Address, NumberOfTel)
        {
        }
        //переопределенный метод для вывод информации по клиенту
        public override string ToString()
        {
            return $"\nНаименование: {Name}, \nИНН: {INN},\nНомер телефона: {NumberOfTel}";
        }
    }
}
