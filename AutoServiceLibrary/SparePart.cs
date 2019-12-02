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
        public string Articul { get; set; }
        public int Number { get; set; }

        public double Cost { get; set; }
        public double TotalCost { get; set; }
        public string Description { get; set; }
        public SparePart() { }
        public SparePart(string Articul, int Number)
        {
            this.Articul = Articul;
            this.Number = Number;
        }
        public SparePart(string Articul, int Number, double Cost, string Description)
        {
            this.Articul = Articul;
            this.Number = Number;
            this.Cost = Cost;
            this.TotalCost = Cost * Number;
            this.Description = Description;
        }
        public static List<SparePart> GetListSpareFromDb(FbConnection db, int id_repair)
        {
            List<SparePart> listSpare = new List<SparePart>();
            string query = $"select * from spare_and_repair where id_repair = {id_repair}";
            using (FbCommand command = new FbCommand(query, db))
            {
                FbDataReader dr;
                db.Open();
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    listSpare.Add(new SparePart(
                        @dr.GetString(dr.GetOrdinal("ARTICUL")),
                        int.Parse(dr.GetString(dr.GetOrdinal("NUMBER"))),
                        dr.GetDouble(dr.GetOrdinal("COST")),
                        @dr.GetString(dr.GetOrdinal("DESCRIPTION"))
                        ));
                }
                db.Close();
            }
            return listSpare;
        }
        public static double GetTotalPriceFromList(List<SparePart> listSpare)
        {
            return listSpare.Select(n => n.TotalCost).Sum();
        }
    }
}
