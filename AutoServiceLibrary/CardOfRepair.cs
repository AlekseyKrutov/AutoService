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
        public delegate void PriceChanged();

        public static event PriceChanged PriceChangedEvent;
        //переменная хранящая список неисправностей
        public List<Malfunctions> ListOfMalf = new List<Malfunctions>();

        public List<SparePart> ListOfSpareParts = new List<SparePart>();

        public List<Employee> ListOfPersonal = new List<Employee>();
        //переменная для хранения времени начала ремонта
        public DateTime TimeOfStart { get; set; }
        //переменная для хранения времени окончания ремонта
        public DateTime? TimeOfFinish { get; set; }
        //переменная для хранения записей
        public string Notes { get; set; }
        //пременная для хранения общей суммы за ремонт
        public double TotalPrice { get; set; }
        public Car Car { get; set; }
        //переменная для проверки исполняется ли ремонт в данный момент
        public bool RepairIsCurrent { get; set; }
        //переменная для хранения номера ремонта
        public int id_repair { get; set; }
        //конструктор по умолчанию
        public CardOfRepair() { PriceChangedEvent += CalculateTotalPrice; }
        //конструктор с 10 параметрами
        public CardOfRepair(int id_repair, string TimeOfStart, string TimeOfFinish, List<Malfunctions> malfunctions, List<SparePart> spareParts,
                    List<Employee> listOfPersonal, string Owner, string CarVIN, string CarMark, string NumberOfCar, string CarVin, string RegCertific)
                    : base(CarVIN, RegCertific, CarMark, NumberOfCar, Owner)
        {
            this.id_repair = id_repair;
            this.TimeOfStart = DateTime.Parse(TimeOfStart);
            this.TimeOfFinish = DateTime.Parse(TimeOfFinish);
            this.RepairIsCurrent = RepairIsCurrent;

            PriceChangedEvent += CalculateTotalPrice;

            ListOfMalf.AddRange(malfunctions);
            ListOfSpareParts.AddRange(spareParts);
            ListOfPersonal.AddRange(listOfPersonal);
            TotalPrice = Malfunctions.GetTotalPriceFromList(malfunctions) + SparePart.GetTotalPriceFromList(spareParts);
        }
        public void FinishRepair()
        {
            RepairIsCurrent = false;
        }
        public void AddMalfInList(Malfunctions malf)
        {
            ListOfMalf.Add(malf);
            PriceChangedEvent();
        }
        public void AddSpareInList(SparePart spare)
        {
            ListOfSpareParts.Add(spare);
            PriceChangedEvent();
        }
        public void RemoveMalfFromList(Malfunctions malf)
        {
            malf = ListOfMalf.Find(m => m.IdMalf == malf.IdMalf);
            ListOfMalf.Remove(malf);
            PriceChangedEvent();
        }
        public void RemoveSpareFromList(SparePart spare)
        {
            spare = ListOfSpareParts.Find(s => s.IdSpare == spare.IdSpare);
            ListOfSpareParts.Remove(spare);
            PriceChangedEvent();
        }
        public void AddPersonalInList(Employee emp)
        {
            if (ListOfPersonal.Find(e => e.TubNumb == emp.TubNumb) == null)
                ListOfPersonal.Add(emp);
            else
                throw new FormatException();
        }
        public void RemovePersonalFromList(Employee emp)
        {
            emp = ListOfPersonal.Find(e => e.TubNumb == emp.TubNumb);
            ListOfPersonal.Remove(emp);
        }
        public void CalculateTotalPrice()
        {
            TotalPrice = Malfunctions.GetTotalPriceFromList(ListOfMalf) + SparePart.GetTotalPriceFromList(ListOfSpareParts);
        }
        public CardOfRepair(FbConnection db, int id_repair, List<Malfunctions> malfunctions, List<SparePart> spareParts,
                    List<Employee> listOfPersonal)
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
                    //TimeOfFinish = dr.GetDateTime(dr.GetOrdinal("FINISH_DATE"));
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
            ListOfMalf.AddRange(malfunctions);
            ListOfSpareParts.AddRange(spareParts);
            ListOfPersonal.AddRange(ListOfPersonal);
        }
        //вывод информации о ремонте
        public override string ToString()
        {
            string showStr = "";
            if (this == null || (this.ListOfMalf.Count == 0) &&
                this.ListOfSpareParts.Count == 0 && this.ListOfPersonal.Count == 0)
                return showStr;
            List<Malfunctions> malfs = this.ListOfMalf.Select(m => m).Where(m => m.MalfOrSpare == 0).ToList();
            List<Malfunctions> fakeSpare = this.ListOfMalf.Select(m => m).Where(m => m.MalfOrSpare == 1).ToList();
            double sumForMalf = malfs.Select(m => m.TotalPrice).Sum();
            double sumForSpare = fakeSpare.Select(m => m.TotalPrice).Sum() + this.ListOfSpareParts.Select(s => s.TotalPrice).Sum();
            string enter = Environment.NewLine;
            if (this.Car.Owner != null)
            showStr += $"Заказчик: {this.Car.Owner.Name}{enter}";
            if (malfs.Count > 0)
            {
                showStr += $"Работы: {enter}";
                foreach (Malfunctions m in malfs)
                {
                    showStr += $"{enter}{m.DescriptionOfMalf} {enter}Количество ({UnitsConvert.ConvertUnit(m.Unit)}): {m.Number}" +
                        $"{enter}Сумма: {m.Price}{enter}Итоговая сумма: {m.TotalPrice}{enter}";
                }
                showStr += $"{enter}Итого: {sumForMalf} руб.{enter}";
                showStr += "------------------------------------";
            }
            if (fakeSpare.Count > 0)
            {
                showStr += $"{enter}Запчасти: {enter}";
                foreach (Malfunctions s in fakeSpare)
                {
                    showStr += $"{enter}{s.DescriptionOfMalf} {enter}Количество ({UnitsConvert.ConvertUnit(s.Unit)}): {s.Number}" +
                        $"{enter}Сумма: {s.Price}{enter}Итоговая сумма: {s.TotalPrice}{enter}";
                }
                if (this.ListOfSpareParts.Count > 0)
                {
                    foreach (SparePart s in this.ListOfSpareParts)
                    {
                        showStr += $"{enter}{s.Description} {enter}Количество ({UnitsConvert.ConvertUnit(s.Unit)}): {s.Number}" +
                            $"{enter}Сумма: {s.Price}{enter}Итоговая сумма: {s.TotalPrice}{enter}";
                    }
                }
                showStr += $"{enter}Итого: {sumForSpare} руб.{enter}";
                showStr += "------------------------------------";
            }
            showStr += $"{enter}Итого за ремонт: {sumForMalf + sumForSpare} руб.{enter}";
            showStr += "------------------------------------";
            if (this.ListOfPersonal.Count > 0)
            {
                showStr += $"{enter}Исполнители: {enter}";
                foreach (Employee emp in this.ListOfPersonal)
                {
                    showStr += $"{emp.ToString()}{enter}";
                }
            }
            return showStr;
        }
    }
}
