using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

namespace AutoServiceLibrary
{
    public class Personal : Person
    {
        //статическая переменная хранящая список персонала
        public static List<Personal> PersonalList = new List<Personal>();
        //переменная хранящая профессию персонала
        public string Function { get; set; }
        public int TubNumb { get; set; }
        //конструктор по умолчанию
        public Personal() { }
        //конструктор с одним параметром
        public Personal(string Name) : base(Name) { }
        public Personal(int TubNUmb) : base ()
        {
            this.TubNumb = TubNUmb;
        }
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

        public static List<Personal> GetListPersonalFromDb(FbConnection db, int id_repair)
        {
            List<Personal> listPersonal = new List<Personal>();
            string query = $"select * from workers_and_repair where id_repair = {id_repair}";
            using (FbCommand command = new FbCommand(query, db))
            {
                FbDataReader dr;
                db.Open();
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    listPersonal.Add(new Personal(
                        dr.GetString(dr.GetOrdinal("NAME"))
                        ));
                }
                db.Close();
            }
            return listPersonal;
        }
    }
}
