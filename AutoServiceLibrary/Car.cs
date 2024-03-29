﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

namespace AutoServiceLibrary
{
    public class Car
    {
        //переменная хранящая VIN номер автомобиля
        public string CarVIN { get; set; }
        //переменная хранящая номер свидетельства о регистрации
        public string RegCertific { get; set;}
        //переменная хранящая марку автомобиля
        public string Mark { get; set; }
        //переменная хранящая модель автомобиля
        public string Model { get; set; }
        //переменная хранящая Гос. номер автомобиля
        public string Number { get; set; }
        //переменная хранящая информацию о владельце автомобиля
        public Client Owner { get; set; }
        //конструктор по умолчанию
        public Car() { }
        //конструктор с 6 параметрами
        public Car(string CarVIN, string RegCertific, string CarMark,
                   string CarModel, string NumberOfCar, Client Owner)
        {
            this.CarVIN = CarVIN;
            this.RegCertific = RegCertific;
            this.Mark = CarMark;
            this.Model = CarModel;
            this.Number = NumberOfCar;
            this.Owner = Owner;
        }
        //конструктор с 5 параметрами
        public Car(string CarVIN, string RegCertific, string CarMark,
                   string CarModel, string NumberOfCar)
        {
            this.CarVIN = CarVIN;
            this.RegCertific = RegCertific;
            this.Mark = CarMark;
            this.Model = CarModel;
            this.Number = NumberOfCar;
            this.Owner = new Client();
        }
        public Car(string CarVin)
        {
            this.CarVIN = CarVin;
        }
        public Car(FbConnection db, string NumberOfCar)
        {
            string query = $"select * from cars_view where state_number = '{NumberOfCar}'";
            using (FbCommand command = new FbCommand(query, db))
            {
                FbDataReader dr;
                try
                {
                    db.Open();
                }
                catch (InvalidOperationException ex) { }
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    this.Number = NumberOfCar;
                    this.Owner = new Client
                    {
                        Name = dr.GetString(dr.GetOrdinal("ORG"))
                    };
                }
                db.Close();
            }
        }
        //метод для вывода автомобиля
        public override string ToString()
        {
            if (Model == "")
                return $"{Mark} {Number}";
            else
                return $"{Mark} {Model} {Number}";
        }
    }
}
