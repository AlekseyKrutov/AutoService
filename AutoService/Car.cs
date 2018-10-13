using AutoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService
{
    public class Car
    {
        //статическая переменная хранящая список автомобилей
        public static List<Car> CarList = new List<Car>();
        //переменная хранящая VIN номер автомобиля
        public string CarVIN { get; set; }
        //переменная хранящая номер свидетельства о регистрации
        public string RegCertific { get; set;}
        //переменная хранящая марку автомобиля
        public string CarMark { get; set; }
        //переменная хранящая модель автомобиля
        public string CarModel { get; set; }
        //переменная хранящая Гос. номер автомобиля
        public string NumberOfCar { get; set; }
        //переменная хранящая информацию о владельце автомобиля
        public Client Owner { get; set; }
        //конструктор по умолчанию
        public Car() { }
        //конструктор с 6 параметрами
        public Car(string CarVIN, string RegCertific, string CarMark, string CarModel,
                    string NumberOfCar, Client Owner)
        {
            this.CarVIN = CarVIN;
            this.RegCertific = RegCertific;
            this.CarMark = CarMark;
            this.CarModel = CarModel;
            this.NumberOfCar = NumberOfCar;
            this.Owner = Owner;
        }
        //конструктор с 5 параметрами
        public Car(string CarVIN, string RegCertific, string CarMark, string CarModel,
                    string NumberOfCar)
        {
            this.CarVIN = CarVIN;
            this.RegCertific = RegCertific;
            this.CarMark = CarMark;
            this.CarModel = CarModel;
            this.NumberOfCar = NumberOfCar;
            this.Owner = new Client();
        }
        //метод для вывода автомобиля
        public override string ToString()
        {
            return $"\nМарка: {CarMark}, \nМодель: {CarModel},\nГос.номер: {NumberOfCar}";
        }
    }
}
