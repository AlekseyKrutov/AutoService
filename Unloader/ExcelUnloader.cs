using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Configuration;
using AutoServiceLibrary;
using DataMapper;
using System.Windows.Forms;

namespace Unloader
{
    public class ExcelUnloader
    {
        private Excel.Application xlApp;
        private Excel.Workbook xlWorkBook;
        private Excel.Worksheet xlSheet;
        private int rowStartForDelete;
        private int rowStartAct1 = 9;
        private int rowStartAct2 = 37;
        private int rowFinishAct1 = 33;
        private int rowFinishAct2 = 53;
        private int rowStartOrder1 = 16;
        private int rowStartOrder2 = 44;
        private int rowFinishOrder1 = 40;
        private int rowFinishOrder2 = 60;

        public string mainPath { get; set; }

        //работа с excel
        public ExcelUnloader()
        {
            mainPath = ConfigurationManager.AppSettings.Get("DestExcelFolder");
        }
        public ExcelUnloader(string path)
        {
            mainPath = path;
        }
        public async void UploadAllDocsInExcAsync(string idRepair)
        {
            await Task.Run(() => MakeActOfWorkInExcel(idRepair));
            await Task.Run(() => MakeOrderInExcel(idRepair));
            await Task.Run(() => MakeBillInExcel(idRepair));
        }
        public async void MakeActOfWorkInExcelAsync(string idRepair)
        {
            await Task.Run(() => MakeActOfWorkInExcel(idRepair));
        }
        public async void MakeOrderInExcelAsync(string idRepair)
        {
            await Task.Run(() => MakeOrderInExcel(idRepair));
        }
        public async void MakeBillInExcelAsync(string idRepair)
        {
            await Task.Run(() => MakeBillInExcel(idRepair));
        }
        public void MakeActOfWorkInExcel(string idRepair)
        {
            try
            {
                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(ConfigurationManager.AppSettings.Get("PathForOpenAct"));
                xlSheet = xlWorkBook.Sheets[1];
                SystemOwner systemOwner = new OwnerMapper().Get();
                CardOfRepair card = new CardMapper().Get(idRepair.ToString());
                FileOperator fileOperator = new FileOperator();
                xlSheet.Cells[1, "A"] = systemOwner.ToString();
                xlSheet.Cells[59, "F"] = systemOwner.Director.GetShortName();
                xlSheet.Cells[3, "G"] = card.IdRepair;
                xlSheet.Cells[3, "J"] = ((DateTime)card.TimeOfFinish).ToString("dd/MM/yyyy");
                if (card.Car.Owner != null)
                    xlSheet.Cells[4, "D"] = $"{card.Car.Owner.Name}, ИНН {card.Car.Owner.INN}";
                xlSheet.Cells[5, "E"] = $"{card.Car.Mark} {card.Car.Number}";
                List<Malfunctions> MalfList = card.ListOfMalf.Select(n => n).Where(n => n.MalfOrSpare == 0).ToList<Malfunctions>();
                List<Malfunctions> SpareList = card.ListOfMalf.Select(n => n).Where(n => n.MalfOrSpare == 1).ToList<Malfunctions>();
                FillRowsWithMalf(xlSheet, rowStartAct1, rowFinishAct1, MalfList);
                FillRowsWithSpares(xlSheet, rowStartAct2, rowFinishAct2, SpareList);
                xlSheet.Cells[34, "Q"] = Malfunctions.GetTotalPriceFromList(MalfList);
                xlSheet.Cells[54, "Q"] = Malfunctions.GetTotalPriceFromList(SpareList);
                xlSheet.Cells[55, "Q"] = card.TotalPrice;
                xlWorkBook.SaveAs(fileOperator.MakeActPath(card, mainPath));
                xlWorkBook.Close();
                xlApp.Quit();
                while (System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp) > 0) { }
                //MessageBox.Show($"Акт № {idRepair} выгружен в формате Excel!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось сделать акт выполненных работ № {idRepair}\n" +
                    $"{ex.Message}");
                xlWorkBook.Close(false);
                xlApp.Quit();
                while (System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp) > 0) { }
            }
        }
        public void MakeOrderInExcel(string idRepair)
        {
            try
            {
                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(ConfigurationManager.AppSettings.Get("PathForOpenOrder"));
                xlSheet = xlWorkBook.Sheets[1];
                CardOfRepair card = new CardMapper().Get(idRepair.ToString());
                SystemOwner systemOwner = new OwnerMapper().Get();
                FileOperator fileOperator = new FileOperator();
                xlSheet.Cells[3, "G"] = card.IdRepair;
                xlSheet.Cells[5, "O"] = card.TimeOfStart.ToString("dd/MM/yyyy");
                xlSheet.Cells[7, "O"] = ((DateTime)card.TimeOfFinish).ToString("dd/MM/yyyy");
                xlSheet.Cells[9, "A"] = $"Заказчик: {card.Car.Owner.Name}";
                xlSheet.Cells[1, "J"] = $"Исполнитель: {systemOwner.Name} {systemOwner.Address} {systemOwner.PhoneNumber}";
                xlSheet.Cells[64, "I"] = systemOwner.Director.GetShortName();
                xlSheet.Cells[10, "D"] = card.Car.Mark;
                xlSheet.Cells[11, "G"] = card.Car.Number;
                List<Malfunctions> MalfList = card.ListOfMalf.Select(n => n).Where(n => n.MalfOrSpare == 0).ToList<Malfunctions>();
                List<Malfunctions> SpareList = card.ListOfMalf.Select(n => n).Where(n => n.MalfOrSpare == 1).ToList<Malfunctions>();
                FillRowsWithMalf(xlSheet, rowStartOrder1, rowFinishOrder1, MalfList);
                FillRowsWithSpares(xlSheet, rowStartOrder2, rowFinishOrder2, SpareList);
                xlSheet.Cells[41, "Q"] = Malfunctions.GetTotalPriceFromList(MalfList);
                xlSheet.Cells[61, "Q"] = Malfunctions.GetTotalPriceFromList(SpareList);
                xlSheet.Cells[62, "Q"] = card.TotalPrice;
                xlWorkBook.SaveAs(fileOperator.MakeOrderPath(card, mainPath));
                xlWorkBook.Close();
                xlApp.Quit();
                while (System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp) > 0) { }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось сделать акт выполненных работ № {idRepair}\n" +
                    $"{ex.Message}");
                xlWorkBook.Close(false);
                xlApp.Quit();
                while (System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp) > 0) { }
            }
        }
        public void MakeBillInExcel(string idRepair)
        {
            try
            {
                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(ConfigurationManager.AppSettings.Get("PathForOpenBill"));
                xlSheet = xlWorkBook.Sheets[1];
                SystemOwner systemOwner = new OwnerMapper().Get();
                CardOfRepair card = new CardMapper().Get(idRepair.ToString());
                FileOperator fileOperator = new FileOperator();
                xlSheet.Cells[5, "B"] = systemOwner.Bank.Name;
                xlSheet.Cells[5, "X"] = systemOwner.Bank.BIK;
                xlSheet.Cells[6, "X"] = systemOwner.Bank.KorBill;
                xlSheet.Cells[9, "B"] = systemOwner.Name;
                xlSheet.Cells[8, "D"] = systemOwner.INN;
                xlSheet.Cells[8, "X"] = systemOwner.Bill;
                xlSheet.Cells[17, "F"] = systemOwner.ToString();
                xlSheet.Cells[30, "H"] = systemOwner.Director.GetFullName();
                xlSheet.Cells[13, "K"] = card.IdRepair;
                xlSheet.Cells[13, "Q"] = ((DateTime)card.TimeOfFinish).ToString("dd/MM/yyyy");
                xlSheet.Cells[19, "F"] = $"{card.Car.Owner.Name}, ИНН {card.Car.Owner.INN}, {card.Car.Owner.Address}";
                xlSheet.Cells[22, "D"] = $"Ремонт автомобиля: {card.Car.Mark} {card.Car.Number} по заявке № {idRepair}" +
                    $" от {((DateTime)card.TimeOfFinish).ToString("dd/MM/yyyy")}";
                xlSheet.Cells[22, "AB"] = card.TotalPrice;
                xlSheet.Cells[28, "B"] = RuDateAndMoneyConverter.CurrencyToTxt(card.TotalPrice, true);
                xlWorkBook.SaveAs(fileOperator.MakeBillPath(card, mainPath));
                xlWorkBook.Close();
                xlApp.Quit();
                while (System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp) > 0) { }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось сделать счет на оплату № {idRepair}\n" +
                    $"{ex.Message}");
                xlWorkBook.Close(false);
                xlApp.Quit();
                while (System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp) > 0) { }
            }
        }

        public void MakeWayBillInExcel(string idWaybill)
        {
            try
            {
                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(ConfigurationManager.AppSettings.Get("PathForOpenBill"));
                xlSheet = xlWorkBook.Sheets[1];
                SystemOwner systemOwner = new OwnerMapper().Get();
                WayBill wayBill = new WayBillMapper().Get(idWaybill);
                FileOperator fileOperator = new FileOperator();
                xlSheet.Cells[22, "Y"] = "рейс";
                xlSheet.Cells[5, "B"] = systemOwner.Bank.Name;
                xlSheet.Cells[5, "X"] = systemOwner.Bank.BIK;
                xlSheet.Cells[6, "X"] = systemOwner.Bank.KorBill;
                xlSheet.Cells[9, "B"] = systemOwner.Name;
                xlSheet.Cells[8, "D"] = systemOwner.INN;
                xlSheet.Cells[8, "X"] = systemOwner.Bill;
                xlSheet.Cells[17, "F"] = systemOwner.ToString();
                xlSheet.Cells[30, "H"] = systemOwner.Director.GetFullName();
                xlSheet.Cells[13, "K"] = wayBill.IdWayBill;
                xlSheet.Cells[13, "Q"] = ((DateTime)wayBill.UnloadDate).ToString("dd/MM/yyyy");
                xlSheet.Cells[19, "F"] = $"{wayBill.Client.Name}, ИНН {wayBill.Client.INN}, {wayBill.Client.Address}";
                xlSheet.Cells[22, "D"] = $"Транспортные услуги по маршруту: {wayBill.Trip.Name} " +
                    $" Водитель: {wayBill.Driver.GetShortName()} Машина: {wayBill.Car.ToString()} По договор-заявке {wayBill.BaseDocument}";
                xlSheet.Cells[22, "AB"] = wayBill.Cost;
                xlSheet.Cells[28, "B"] = RuDateAndMoneyConverter.CurrencyToTxt(wayBill.Cost, true);
                xlWorkBook.SaveAs(fileOperator.MakeBillPath(wayBill, mainPath));
                xlWorkBook.Close();
                xlApp.Quit();
                while (System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp) > 0) { }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось сделать счет на оплату № {idWaybill}\n" +
                    $"{ex.Message}");
                xlWorkBook.Close(false);
                xlApp.Quit();
                while (System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp) > 0) { }
            }
        }
        private void FillRowsWithMalf(Excel.Worksheet xlSheet, int startRow, int finishRow, List<Malfunctions> MalfList)
        {
            if (MalfList.Count > finishRow)
            {
                MessageBox.Show("Не удалось выгрузить документ, слишком большое количество позиций!");
                throw new ArgumentException();
            }
            rowStartForDelete = startRow;
            for (int i = startRow, j = 0; j < MalfList.Count; i++, j++)
            {
                if (MalfList[j].Unit == Units.Pieces)
                    xlSheet.Cells[i, "M"] = MalfList[j].Number;
                else
                    xlSheet.Cells[i, "N"] = MalfList[j].Number;
                xlSheet.Cells[i, "B"] = $"{MalfList[j].Description}";
                xlSheet.Cells[i, "O"] = $"{MalfList[j].Cost}";
                xlSheet.Cells[i, "Q"] = $"{MalfList[j].TotalCost}";
                rowStartForDelete = i + 1;
            }
            DeleteRows(xlSheet, "B", rowStartForDelete, finishRow, MalfList.Count);
        }
        private void FillRowsWithSpares(Excel.Worksheet xlSheet, int startRow, int finishRow, List<Malfunctions> SpareList)
        {
            rowStartForDelete = startRow;
            if (SpareList.Count > finishRow)
            {
                MessageBox.Show("Не удалось выгрузить документ, слишком большое количество позиций!");
                throw new ArgumentException();
            }
            for (int i = startRow, j = 0; j < SpareList.Count; i++, j++)
            {
                xlSheet.Cells[i, "N"] = SpareList[j].Number;
                xlSheet.Cells[i, "B"] = $"{SpareList[j].Description}";
                xlSheet.Cells[i, "O"] = $"{SpareList[j].Cost}";
                xlSheet.Cells[i, "Q"] = $"{SpareList[j].TotalCost}";
                rowStartForDelete = i + 1;
            }
            DeleteRows(xlSheet, "B", rowStartForDelete, finishRow, SpareList.Count);
        }
        private void DeleteRows(Excel.Worksheet xlSheet, string column, int rowStart, int rowFinish, int listCount)
        {
            if (rowStart > rowFinish)
                return;
            if (listCount == 0)
            {
                rowStart -= 2;
                rowFinish += 1;
            }
            Excel.Range rangeRows;

            rangeRows = xlSheet.Range[column + rowStart, column + rowFinish];
            rangeRows.RowHeight = 0;
        }
        static bool CheckRepairNull(int id_repair)
        {
            if (id_repair <= 0)
            {
                MessageBox.Show("Для выгрузки необходимо выбрать ремонт!");
                return false;
            }
            else
                return true;
        }
    }
}
