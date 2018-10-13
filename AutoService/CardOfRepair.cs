using AutoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService
{
    public class CardOfRepair : Car
    {
        //статическая переменная хранящая количество ремонтов
        public static int Number = 0;
        //статическая переменная хранящая список ремонтов
        public static List<CardOfRepair> repairsList= new List<CardOfRepair>();
        //переменная хранящая список неисправностей
        public List<Malfunctions> ListOfMalf= new List<Malfunctions>();
        //переменная для хранения времени начала ремонта
        public string TimeOfStart { get; set; }
        //переменная для хранения времени окончания ремонта
        public string TimeOfEnd { get; set; }
        //переменная для хранения записей
        public string Notes { get; set; }
        //переменная для хранения ответственного механика за ремонт
        public Personal Mechanic { get; set; }
        //пременная для хранения общей суммы за ремонт
        public double TotalPrice { get; set; }
        //переменная для проверки исполняется ли ремонт в данный момент
        public bool RepairIsCurrent { get; set; }
        //переменная для хранения номера ремонта
        public int NumberOfAct { get; set; }
        //конструктор по умолчанию
        public CardOfRepair() { }
        //конструктор с 10 параметрами
        public CardOfRepair(string TimeOfStart, List<Malfunctions> malfunctions,string CarVIN, string RegCertific, string CarMark, string CarModel,
                    string NumberOfCar, Client Owner, Personal Mechanic, bool RepairIsCurrent = true):base(CarVIN, RegCertific, CarMark, CarModel,
                    NumberOfCar, Owner)
        {
            this.TimeOfStart = TimeOfStart;
            this.Mechanic = Mechanic;
            this.NumberOfAct = Number;
            this.RepairIsCurrent = RepairIsCurrent;

            foreach (Malfunctions malf in malfunctions)
            {
                ListOfMalf.Add(new Malfunctions(malf.Price, malf.DescriptionOfMalf, malf.Currancy));
                TotalPrice += malf.Price;
            }
        }
        //конструктор с 11 параметрами
        public CardOfRepair(string Notes, string TimeOfStart, List<Malfunctions> malfunctions, string CarVIN, string RegCertific, string CarMark, string CarModel,
                    string NumberOfCar, Client Owner, Personal Mechanic, bool RepairIsCurrent = true) : this(TimeOfStart, malfunctions, CarVIN, RegCertific, 
                    CarMark,  CarModel,  NumberOfCar,  Owner,  Mechanic, RepairIsCurrent)
        {
            this.Notes = Notes;
        }
        //функция для вывода неисправностей по ремонту
        public string MalfunctionsToString()
        {
            string MalfString = "";
            for (int i = 0; i < ListOfMalf.Count; i++)
            {
                if (i == (ListOfMalf.Count-1))
                {
                    MalfString += ListOfMalf[i].DescriptionOfMalf;
                }
                else
                    MalfString += ListOfMalf[i].DescriptionOfMalf + "\n";
            }
            MalfString += "\nИтоговая стоимость: " + TotalPrice + " Рублей";
            return MalfString;
        }
        //функция для вывода автомобиля
        public string CarInRepairToString()
        {
            return $"Марка: {CarMark}\nМодель: {CarModel}\nГос. номер: {NumberOfCar}\nВладелец: {Owner.Name}";
        }
    }
}
