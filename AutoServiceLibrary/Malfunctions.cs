using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

namespace AutoServiceLibrary
{
    public class Malfunctions
    {
        //статическая переменная хранящая список неисправностей 
        public static List<Malfunctions> MalfList = new List<Malfunctions>();
        //переменная хранящая стоимость работы
        public double Price { get; set; }
        public double TotalPrice { get; set; }
        //переменная хранящая описание неисправности
        public string DescriptionOfMalf { get; set; }
        //переменная хранящая валюту
        public string Unit { get; set; }
        //переменная хранящая количество
        public int Number { get; set; }
 
        public int MalfOrSpare { get; set; }
        //конструктор по умолчанию
        public Malfunctions() { }
        //конструктор с 3 параметрами
        public Malfunctions(string DescriptionOfMalf, int Number)
        {
            this.DescriptionOfMalf = DescriptionOfMalf;
            this.Number = Number;
        }
        public Malfunctions(double Price, string DescriptionOfMalf, string Unit, int Number, int MalfOrSpare)
            : this (DescriptionOfMalf, Number)
        {
            this.Price = Price;
            this.TotalPrice = Price * Number;
            this.Unit = Unit;
            this.MalfOrSpare = MalfOrSpare;
            
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
                        dr.GetString(dr.GetOrdinal("UNIT")),
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
