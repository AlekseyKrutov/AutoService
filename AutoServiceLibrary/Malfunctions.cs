using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

namespace AutoServiceLibrary
{
    public enum Units { Pieces, Hours, Liters, Kilograms, Nothing }
    public class Malfunctions
    {
        private int number;
        public int IdMalf { get; set; }
        public string Description { get; set; }
        public int Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
                TotalPrice = Price * number;
            }
        }
        public Units Unit { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
        public int MalfOrSpare { get; set; }
        public Malfunctions() { }
        public Malfunctions(string DescriptionOfMalf, int Number)
        {
            this.Description = DescriptionOfMalf;
            this.Number = Number;
        }
        public Malfunctions(string DescriptionOfMalf, double Price, Units Unit, int MalfOrSpare = 0)
        {
            this.Description = DescriptionOfMalf;
            this.Price = Price;
            this.Unit = Unit;
            this.MalfOrSpare = MalfOrSpare;
        }
        public Malfunctions(double Price, string DescriptionOfMalf, Units Unit, int Number, int MalfOrSpare)
            : this (DescriptionOfMalf, Number)
        {
            this.Price = Price;
            this.Unit = Unit;
            this.Number = Number;
            this.MalfOrSpare = MalfOrSpare;
            
        }
        public Malfunctions(int Id, double Price, string DescriptionOfMalf, Units Unit, int Number, int MalfOrSpare)
            : this(Price, DescriptionOfMalf, Unit, Number, MalfOrSpare)
        {
            this.IdMalf = Id;
        }
        //переопределенный метод для вывода информации по неисправности
        public override string ToString()
        {
            return $"\nНаименование: {Description}";
        }
        public static double GetTotalPriceFromList(List<Malfunctions> listMalf)
        {
            return listMalf.Select(n => n.TotalPrice).Sum();
        }
    }
}
