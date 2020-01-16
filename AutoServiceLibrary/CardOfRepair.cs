using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

namespace AutoServiceLibrary
{
    public class CardOfRepair
    {
        public delegate void PriceChanged();

        public static event PriceChanged PriceChangedEvent;

        private double paidMoney;

        public List<Malfunctions> ListOfMalf = new List<Malfunctions>();
        public List<SparePart> ListOfSpareParts = new List<SparePart>();
        public List<Employee> ListOfPersonal = new List<Employee>();

        public int IdRepair { get; set; }
        public DateTime TimeOfStart { get; set; }
        public DateTime? TimeOfFinish { get; set; }
        public string Notes { get; set; }
        public double TotalPrice { get; set; }
        public double PaidMoney
        {
            get => paidMoney;
            set
            {
                if (value > TotalPrice)
                    paidMoney = TotalPrice;
                else
                    paidMoney = value;
            }
        }
        public Car Car { get; set; }
        public bool RepairIsCurrent { get; set; }
        public CardOfRepair() { PriceChangedEvent += CalculateTotalPrice; }
        public CardOfRepair(int id_repair, string TimeOfStart, string TimeOfFinish, List<Malfunctions> malfunctions, List<SparePart> spareParts,
                    List<Employee> listOfPersonal, string Owner, string CarVIN, string CarMark, string NumberOfCar, string CarVin, string RegCertific)
        {
            this.IdRepair = id_repair;
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
        public void ActivateRepair()
        {
            RepairIsCurrent = true;
        }
        public void AddMalfInList(Malfunctions malf)
        {
            malf.CalculateCostForClient(Car.Owner);
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
        
        //вывод информации о ремонте
        public override string ToString()
        {
            string showStr = "";
            if (this == null || (this.ListOfMalf.Count == 0) &&
                this.ListOfSpareParts.Count == 0 && this.ListOfPersonal.Count == 0)
                return showStr;
            List<Malfunctions> malfs = this.ListOfMalf.Select(m => m).Where(m => m.MalfOrSpare == 0).ToList();
            List<Malfunctions> fakeSpare = this.ListOfMalf.Select(m => m).Where(m => m.MalfOrSpare == 1).ToList();
            double sumForMalf = malfs.Select(m => m.TotalCost).Sum();
            double sumForSpare = fakeSpare.Select(m => m.TotalCost).Sum() + this.ListOfSpareParts.Select(s => s.TotalCost).Sum();
            string enter = Environment.NewLine;
            if (this.Car.Owner != null)
            showStr += $"Заказчик: {this.Car.Owner.Name}{enter}";
            showStr += $"Автомобиль: {this.Car.Mark + " " + this.Car.Model + " " + this.Car.Number}{enter}";
            if (this.TimeOfStart != null)
            {
                showStr += $"Дата начала: {this.TimeOfStart}{enter}";
            }
            if (this.TimeOfFinish != null)
            {
                showStr += $"Дата окончания: {this.TimeOfFinish}{enter}";
            }
            if (malfs.Count > 0)
            {
                showStr += $"{enter}Работы: {enter}";
                foreach (Malfunctions m in malfs)
                {
                    showStr += $"{enter}{m.Description} {enter}Количество ({UnitsConvert.ConvertUnit(m.Unit)}): {m.Number}" +
                        $"{enter}Сумма: {m.Cost}{enter}Итоговая сумма: {m.TotalCost}{enter}";
                }
                showStr += $"{enter}Итого: {sumForMalf} руб.{enter}";
                showStr += "------------------------------------";
            }
            if (fakeSpare.Count > 0 || this.ListOfSpareParts.Count > 0)
            {
                showStr += $"{enter}Запчасти: {enter}";
                if (fakeSpare.Count > 0)
                {
                    foreach (Malfunctions s in fakeSpare)
                    {
                        showStr += $"{enter}{s.Description} {enter}Количество ({UnitsConvert.ConvertUnit(s.Unit)}): {s.Number}" +
                            $"{enter}Сумма: {s.Cost}{enter}Итоговая сумма: {s.TotalCost}{enter}";
                    }
                }
                if (this.ListOfSpareParts.Count > 0)
                {
                    foreach (SparePart s in this.ListOfSpareParts)
                    {
                        showStr += $"{enter}{s.Description} {enter}Количество ({UnitsConvert.ConvertUnit(s.Unit)}): {s.Number}" +
                            $"{enter}Сумма: {s.Cost}{enter}Итоговая сумма: {s.TotalCost}{enter}";
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
