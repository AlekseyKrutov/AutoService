using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

namespace AutoServiceLibrary
{
    public class SparePart
    {
        private float number;
        public int IdSpare { get; set; }
        public string Articul { get; set; }
        public float Number
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
        public double Price { get; set; }
        public double TotalPrice { get; set; }
        public string Description { get; set; }
        public Units Unit { get; set; }
        public SparePart() { }
        public SparePart(string Articul, float Number)
        {
            this.Articul = Articul;
            this.Number = Number;
        }
        public SparePart(string Articul, float Number, double Cost, string Description, Units Unit)
        {
            this.Articul = Articul;
            this.Number = Number;
            this.Price = Cost;
            this.TotalPrice = Cost * Number;
            this.Description = Description;
            this.Unit = Unit;
        }
        public SparePart(int IdSpare, string Articul, float Number, double Cost, string Description, Units Unit)
            : this (Articul, Number, Cost, Description, Unit)
        {
            this.IdSpare = IdSpare;
        }
        public static double GetTotalPriceFromList(List<SparePart> listSpare)
        {
            return listSpare.Select(n => n.TotalPrice).Sum();
        }
    }
}
