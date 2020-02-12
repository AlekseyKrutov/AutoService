using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AutoServiceLibrary
{
    public class FileOperator
    {
        public string mailFolderName = @"mail_buffer";
        public string ExcelPath { get; set; }
        public string AppPath { get; set; }
        public FileOperator()
        {
            AppPath = Environment.CurrentDirectory;
        }

        public string CreateFolder(string path, string folderName)
        {
            string fullPath = path + "\\" + folderName + "\\";
            return  Directory.CreateDirectory(fullPath).FullName;
        }
        public string CreateFolder()
        {
            return CreateFolder(AppPath, mailFolderName);
        }
        public void DeleteFolder(string path, string folderName)
        {
            Directory.Delete(path + "\\" + folderName, true);
        }
        public void DeleteFolder()
        {
            DeleteFolder(AppPath, mailFolderName);
        }
        public string[] GetFileNamesFromFolder(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            return directoryInfo.GetFiles().Select(f => f.FullName).ToArray();
        }
        public bool CheckFileExistance(string path) => File.Exists(path);

        public string MakeActPath(CardOfRepair card, string path) => path + MakeActNameFile(card);
        public string MakeActpath(WayBill wayBill, string path) => path + MakeActNameFile(wayBill);
        public string MakeOrderPath(CardOfRepair card, string path) => path + MakeOrderNameFile(card);
        public string MakeBillPath(CardOfRepair card, string path) => path + MakeBillNameFile(card);
        public string MakeBillPath(WayBill wayBill, string path) => path + MakeBillNameFile(wayBill);

        public string MakeActNameFile(CardOfRepair card) =>
            String.Format(@"{0} Акт - ремонт {1}.xlsx", card.IdRepair, card.Car.Owner.Name.Replace("\"", ""));
        public string MakeActNameFile(WayBill wayBill) =>
            String.Format(@"{0} Акт - грузоперевозка {1}.xlsx", wayBill.IdWayBill, wayBill.Client.Name.Replace("\"", ""));
        public string MakeOrderNameFile(CardOfRepair card) =>
            String.Format(@"{0} Заказ наряд {1}.xlsx", card.IdRepair, card.Car.Owner.Name.Replace("\"", ""));
        public string MakeBillNameFile(CardOfRepair card) =>
            String.Format(@"{0} Счет на оплату - ремонт {1}.xlsx", card.IdRepair, card.Car.Owner.Name.Replace("\"", ""));
        public string MakeBillNameFile(WayBill wayBill) =>
           String.Format(@"{0} Счет на оплату - грузоперевозка {1}.xlsx", wayBill.IdWayBill, wayBill.Client.Name.Replace("\"", ""));
    }

}
