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
                TotalCost = Cost * number;
            }
        }
        public double Cost { get; set; }
        public double TotalCost { get; set; }
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
            this.Cost = Cost;
            this.TotalCost = Cost * Number;
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
            return listSpare.Select(n => n.TotalCost).Sum();
        }
    }
}
