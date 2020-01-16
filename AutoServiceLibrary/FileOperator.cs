using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace AutoServiceLibrary
{
    class FileOperator
    {
        public string ExcelPath { get; set; }

        public FileOperator()
        {
            ExcelPath = ConfigurationManager.AppSettings.Get("DestExcelFolder");
        }

        public bool CheckFileExistance(string path) => File.Exists(path);

        public string MakeActNameFile(CardOfRepair card) =>
            String.Format(@"{0} Акт {1}.xlsx", card.IdRepair, card.Car.Owner.Name.Replace("\"", ""));
        public string MakeActNameFile(WayBill wayBill) =>
            String.Format("");
        public string MakeOrderNameFile(CardOfRepair card) =>
            String.Format(@"{0} Заказ наряд {1}.xlsx", card.IdRepair, card.Car.Owner.Name.Replace("\"", ""));
        public string MakeBillNameFile(CardOfRepair card) =>
            String.Format(@"{0} Счет на оплату {1}.xlsx", card.IdRepair, card.Car.Owner.Name.Replace("\"", ""));
        public string MakeBillNameFile(WayBill way) =>
            String.Format("");
    }
}
