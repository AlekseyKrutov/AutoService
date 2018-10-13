using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService
{
    public class Personal : Person
    {
        //статическая переменная хранящая список персонала
        public static List<Personal> PersonalList = new List<Personal>();
        //переменная хранящая профессию персонала
        public string Function { get; set; }
        //конструктор по умолчанию
        public Personal() { }
        //конструктор с одним параметром
        public Personal(string Name) : base(Name) { }
        //конструктор с 5 параметрами
        public Personal(string Name, string INN, string Address, string Function, string NumberOfTel)
            : base(Name, INN, Address, NumberOfTel)
        {
            this.Function = Function;
        }
        //переопределенный метод для вывод информации по работнику
        public override string ToString()
        {
            return $"\nФИО: {Name}, \nИНН: {INN},\nНомер телефона: {NumberOfTel}";
        }
    }
}
