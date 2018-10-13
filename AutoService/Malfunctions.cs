using AutoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService
{
    public class Malfunctions
    {
        //статическая переменная хранящая список неисправностей 
        public static List<Malfunctions> MalfList = new List<Malfunctions>();
        //переменная хранящая стоимость работы
        public double Price { get; set; }
        //переменная хранящая описание неисправности
        public string DescriptionOfMalf { get; set; }
        //переменная хранящая валюту
        public string Currancy { get; set; }
        //конструктор по умолчанию
        public Malfunctions() { }
        //конструктор с 3 параметрами
        public Malfunctions(double Price, string DescriptionOfMalf, string Currancy = "Рублей")
        {
            this.Price = Price;
            this.DescriptionOfMalf = DescriptionOfMalf;
            this.Currancy = Currancy;
        }
        //переопределенный метод для вывода информации по неисправности
        public override string ToString()
        {
            return $"\nНаименование: {DescriptionOfMalf}";
        }
    }
}
