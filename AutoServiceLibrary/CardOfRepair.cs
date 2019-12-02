using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

namespace AutoServiceLibrary
{
    public class CardOfRepair : Car
    {
        //статическая переменная хранящая список ремонтов
        public static List<CardOfRepair> repairsList = new List<CardOfRepair>();
        //переменная хранящая список неисправностей
        public List<Malfunctions> ListOfMalf = new List<Malfunctions>();

        public List<SparePart> ListOfSpareParts = new List<SparePart>();

        public List<Personal> ListOfPersonal = new List<Personal>();
        //переменная для хранения времени начала ремонта
        public DateTime TimeOfStart { get; set; }
        //переменная для хранения времени окончания ремонта
        public DateTime TimeOfFinish { get; set; }
        //переменная для хранения записей
        public string Notes { get; set; }
        //пременная для хранения общей суммы за ремонт
        public double TotalPrice { get; set; }
        //переменная для проверки исполняется ли ремонт в данный момент
        public bool RepairIsCurrent { get; set; }
        //переменная для хранения номера ремонта
        public int id_repair { get; set; }
        //конструктор по умолчанию
        public CardOfRepair() { }
        //конструктор с 10 параметрами
        public CardOfRepair(int id_repair, string TimeOfStart, string TimeOfFinish, List<Malfunctions> malfunctions, List<SparePart> spareParts,
                    List<Personal> listOfPersonal, Client Owner, string CarVIN, string CarMark, string NumberOfCar, string CarVin, string RegCertific)
                    : base(CarVIN, RegCertific, CarMark, NumberOfCar, Owner)
        {
            this.id_repair = id_repair;
            this.TimeOfStart = DateTime.Parse(TimeOfStart);
            this.TimeOfFinish = DateTime.Parse(TimeOfFinish);
            this.RepairIsCurrent = RepairIsCurrent;

            foreach (Malfunctions malf in malfunctions)
            {
                ListOfMalf.Add(malf);
            }
            foreach (SparePart sp in spareParts)
            {
                ListOfSpareParts.Add(sp);
            }
            foreach (Personal p in listOfPersonal)
            {
                ListOfPersonal.Add(p);
            }
            TotalPrice = Malfunctions.GetTotalPriceFromList(malfunctions) + SparePart.GetTotalPriceFromList(spareParts);
        }
        public CardOfRepair(FbConnection db, int id_repair, List<Malfunctions> malfunctions, List<SparePart> spareParts,
                    List<Personal> listOfPersonal)
        {
            string query = $"select * from repair_cars_owner where id_card_of_repair = {id_repair}";
            using (FbCommand command = new FbCommand(query, db))
            {
                FbDataReader dr;
                db.Open();
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    this.id_repair = id_repair;
                    TimeOfStart = dr.GetDateTime(dr.GetOrdinal("START_DATE"));
                    TimeOfFinish = dr.GetDateTime(dr.GetOrdinal("FINISH_DATE"));
                    TotalPrice = dr.GetDouble(dr.GetOrdinal("TOTAL_COST"));
                    RepairIsCurrent = dr.GetBoolean(dr.GetOrdinal("CURRENT_OR_NOT"));
                    Notes = @dr.GetString(dr.GetOrdinal("NOTES"));
                    CarVIN = dr.GetString(dr.GetOrdinal("VIN"));
                    NumberOfCar = dr.GetString(dr.GetOrdinal("STATE_NUMBER"));
                    CarMark = dr.GetString(dr.GetOrdinal("CAR_MODEL"));
                    RegCertific = dr.GetString(dr.GetOrdinal("REG_CERT"));
                    Owner = new Client
                    {
                        @Address = dr.GetString(dr.GetOrdinal("ADDRESS")),
                        INN = dr.GetString(dr.GetOrdinal("INN")),
                        @Name = dr.GetString(dr.GetOrdinal("NAME_ORG"))
                    };
                }
                db.Close();
            }
            foreach (Malfunctions malf in malfunctions)
            {
                ListOfMalf.Add(malf);
            }
            foreach (SparePart sp in spareParts)
            {
                ListOfSpareParts.Add(sp);
            }
            foreach (Personal p in listOfPersonal)
            {
                ListOfPersonal.Add(p);
            }
        }
        //функция для вывода автомобиля
        public string CarInRepairToString()
        {
            return $"Марка: {CarMark}\nМодель: {CarModel}\nГос. номер: {NumberOfCar}\nВладелец: {Owner.Name}";
        }
    }
}
