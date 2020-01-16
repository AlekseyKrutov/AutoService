using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceLibrary
{
    public class WayBill
    {
        private double paidMoney;

        public int IdWayBill { get; set; }
        public DateTime LoadDate { get; set; }
        public DateTime? UnloadDate { get; set; }
        public double Cost { get; set; }
        public double PaidMoney { 
            get => paidMoney;
            set 
            {
                if (value > Cost)
                    paidMoney = Cost;
                else
                    paidMoney = value;
            }
        }
        public int Kilometers { get; set; }
        public float Fuel { get; set; }
        public Trip Trip { get; set; }
        public Client Client { get; set; }
        public Car Car { get; set; }
        public Employee Driver { get; set; }
        public bool IsCurrent { get; set; }
        public bool ActiveFlag { get; set; }
        public string Notes { get; set; }
        public string BaseDocument { get; set; }

        public WayBill() { IsCurrent = true; ActiveFlag = true; }
        public WayBill(DateTime loadDate, DateTime? unloadDate, double cost, int kilometers,
                       float fuel, Trip trip, Client client, Car car, Employee driver, string notes,
                       string baseDocument, double paidMoney, bool isCurrent = true, bool active_flag = true)
        {
            LoadDate = loadDate;
            UnloadDate = unloadDate;
            Cost = cost;
            Kilometers = kilometers;
            Fuel = fuel;
            Trip = trip;
            Client = client;
            Car = car;
            Driver = driver;
            IsCurrent = isCurrent;
            ActiveFlag = active_flag;
            Notes = notes;
            BaseDocument = baseDocument;
            PaidMoney = paidMoney;
        }
        public WayBill(int idWayBill, DateTime loadDate, DateTime? unloadDate, double cost, int kilometers,
                       float fuel, Trip trip, Client client, Car car, Employee driver, string notes,
                       bool isCurrent, bool active_flag, string baseDocument, double paidMoney)
                       : this(loadDate, unloadDate, cost, kilometers, fuel, trip, client, car, driver, notes, 
                              baseDocument, paidMoney, isCurrent, active_flag)
        {
            IdWayBill = idWayBill;
        }
        public void Activate()
        {
            IsCurrent = true;
        }
        public void Finish()
        {
            IsCurrent = false;
        }
        public void Delete()
        {
            Finish();
            ActiveFlag = false;
        }
        public override string ToString()
        {
            string showStr = "";
            string enter = Environment.NewLine;
            showStr += $"Грузоперевозка №{IdWayBill}{enter}{enter}";
            showStr += $"Дата загрузки: {LoadDate}{enter}";
            showStr += $"Дата выгрузки: {UnloadDate}{enter}{enter}";
            showStr += $"Заказчик: {Client.Name}{enter}";
            showStr += $"Маршрут: {Trip.Name}{enter}{enter}";
            showStr += $"Автомобиль: {Car.Mark + " " + Car.Model + " " + Car.Number}{enter}";
            showStr += $"Водитель: {Driver.ToString()}{enter}{enter}";
            showStr += $"Заявка: {BaseDocument}{enter}";
            showStr += $"Пройдено: {Kilometers} км.{enter}";
            showStr += $"Затрачено топлива: {Fuel} л.{enter}";
            showStr += $"Заметки: {Notes}";
            return showStr;
        }
    }
}
