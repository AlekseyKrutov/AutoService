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
        public string DescriptionOfMalf { get; set; }
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
            this.DescriptionOfMalf = DescriptionOfMalf;
            this.Number = Number;
        }
        public Malfunctions(string DescriptionOfMalf, double Price, Units Unit, int MalfOrSpare = 0)
        {
            this.DescriptionOfMalf = DescriptionOfMalf;
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
            return $"\nНаименование: {DescriptionOfMalf}";
        }
        public static List<Malfunctions> GetListMalfFromDb(FbConnection db, int id_repair)
        {
            List<Malfunctions> listMalf = new List<Malfunctions>();
            string query = $"select * from malf_and_repair where id_repair = {id_repair}";
            using (FbCommand command = new FbCommand(query, db))
            {
                FbDataReader dr;
                db.Open();
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    listMalf.Add(new Malfunctions( 
                        dr.GetDouble(dr.GetOrdinal("COST")),
                        dr.GetString(dr.GetOrdinal("DESCRIPTION")),
                        (Units) dr.GetInt32(dr.GetOrdinal("UNIT")),
                        dr.GetInt32(dr.GetOrdinal("NUMBER")),
                        int.Parse(dr.GetString(dr.GetOrdinal("MALF_OR_SPARE")))
                        ));
                }
                db.Close();
            }
            return listMalf;
        }
        public static double GetTotalPriceFromList(List<Malfunctions> listMalf)
        {
            return listMalf.Select(n => n.TotalPrice).Sum();
        }
    }
}
