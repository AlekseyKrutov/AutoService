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
                TotalCost = Cost * number;
            }
        }
        public Units Unit { get; set; }
        public double Cost { get; set; }
        public double TotalCost { get; set; }
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
            this.Cost = Price;
            this.Unit = Unit;
            this.MalfOrSpare = MalfOrSpare;
        }
        public Malfunctions(double Price, string DescriptionOfMalf, Units Unit, int Number, int MalfOrSpare)
            : this (DescriptionOfMalf, Number)
        {
            this.Cost = Price;
            this.Unit = Unit;
            this.Number = Number;
            this.MalfOrSpare = MalfOrSpare;
            
        }
        public Malfunctions(int Id, double Price, string DescriptionOfMalf, Units Unit, int Number, int MalfOrSpare)
            : this(Price, DescriptionOfMalf, Unit, Number, MalfOrSpare)
        {
            this.IdMalf = Id;
        }
        public void CalculateCostForClient(Client client)
        {
            if (client.Discount == 0 || Cost == 0 || MalfOrSpare == 1)
                return;
            else
            {
                Cost -= (Cost * (client.Discount));
                Cost = Math.Round(Cost);
                TotalCost = Cost * number;
            }
        }
        //переопределенный метод для вывода информации по неисправности
        public override string ToString()
        {
            return $"\nНаименование: {Description}";
        }
        public static double GetTotalPriceFromList(List<Malfunctions> listMalf)
        {
            return listMalf.Select(n => n.TotalCost).Sum();
        }
    }
}
