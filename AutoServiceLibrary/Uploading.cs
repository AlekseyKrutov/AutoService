using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;
using AutoServiceLibrary;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Configuration;
using DataMapper;

namespace AutoServiceLibrary
{
    public static class NewUploadInExcel
    {
        static Excel.Application xlApp;
        static Excel.Workbook xlWorkBook;
        static Excel.Worksheet xlSheet;
        static private int rowStartForDelete;
        static private int rowStartAct1 = 9;
        static private int rowStartAct2 = 32;
        static private int rowFinishAct1 = 28;
        static private int rowFinishAct2 = 44;
        static private int rowStartOrder1 = 16;
        static private int rowStartOrder2 = 39;
        static private int rowFinishOrder1 = 35;
        static private int rowFinishOrder2 = 51;
        static string fileName;
        //работа с excel
        public static async void UploadAllDocsInExcAsync(string idRepair)
        {
            await Task.Run(() => MakeActOfWorkInExcel(idRepair));
            await Task.Run(() => MakeOrderInExcel(idRepair));
            await Task.Run(() => MakeBillInExcel(idRepair));
        }
        public static async void MakeActOfWorkInExcelAsync(string idRepair)
        {
            await Task.Run(() => MakeActOfWorkInExcel(idRepair));
        }
        static async public void MakeOrderInExcelAsync(string idRepair)
        {
            await Task.Run(() => MakeOrderInExcel(idRepair));
        }
        static async public void MakeBillInExcelAsync(string idRepair)
        {
            await Task.Run(() => MakeBillInExcel(idRepair));
        }
        static public void MakeActOfWorkInExcel(string idRepair)
        {
            rowStartForDelete = 0;
            try
            {
                FileOperator fo = new FileOperator();
                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(ConfigurationManager.AppSettings.Get("PathForOpenAct"));
                xlSheet = xlWorkBook.Sheets[1];
                CardOfRepair card = new CardMapper().Get(idRepair.ToString());
                xlSheet.Cells[3, "G"] = card.IdRepair;
                xlSheet.Cells[3, "J"] = ((DateTime)card.TimeOfFinish).ToString("dd/MM/yyyy");
                if (card.Car.Owner != null)
                    xlSheet.Cells[4, "D"] = $"{card.Car.Owner.Name}, ИНН {card.Car.Owner.INN}";
                xlSheet.Cells[5, "E"] = $"{card.Car.Mark} {card.Car.Number}";
                List<Malfunctions> MalfList = card.ListOfMalf.Select(n => n).Where(n => n.MalfOrSpare == 0).ToList<Malfunctions>();
                List<Malfunctions> SpareList = card.ListOfMalf.Select(n => n).Where(n => n.MalfOrSpare == 1).ToList<Malfunctions>();
                if (!FillRowsWithMalf(rowStartAct1, rowFinishAct1, MalfList))
                    return;
                DeleteRows(xlSheet, "B", rowStartForDelete, rowFinishAct1);
                if (!FillRowsWithSpares(rowStartAct2, rowFinishAct2, SpareList))
                    return;
                DeleteRows(xlSheet, "B", rowStartForDelete, rowFinishAct2);
                xlSheet.Cells[29, "Q"] = Malfunctions.GetTotalPriceFromList(MalfList);
                xlSheet.Cells[45, "Q"] = Malfunctions.GetTotalPriceFromList(SpareList);
                xlSheet.Cells[46, "Q"] = card.TotalPrice;
                xlWorkBook.SaveAs(fo.ExcelPath + fo.MakeActNameFile(card));
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
        static public void MakeOrderInExcel(string idRepair)
        {
            rowStartForDelete = 0;
            try
            {
                FileOperator fo = new FileOperator();
                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(ConfigurationManager.AppSettings.Get("PathForOpenOrder"));
                xlSheet = xlWorkBook.Sheets[1];
                CardOfRepair card = new CardMapper().Get(idRepair.ToString());
                xlSheet.Cells[3, "G"] = card.IdRepair;
                xlSheet.Cells[5, "O"] = card.TimeOfStart.ToString("dd/MM/yyyy");
                xlSheet.Cells[7, "O"] = ((DateTime)card.TimeOfFinish).ToString("dd/MM/yyyy");
                xlSheet.Cells[9, "A"] = $"Заказчик: {card.Car.Owner.Name}";
                xlSheet.Cells[10, "D"] = card.Car.Mark;
                xlSheet.Cells[11, "G"] = card.Car.Number;
                List<Malfunctions> MalfList = card.ListOfMalf.Select(n => n).Where(n => n.MalfOrSpare == 0).ToList<Malfunctions>();
                List<Malfunctions> SpareList = card.ListOfMalf.Select(n => n).Where(n => n.MalfOrSpare == 1).ToList<Malfunctions>();
                if (!FillRowsWithMalf(rowStartOrder1, rowFinishOrder1, MalfList))
                    return;
                DeleteRows(xlSheet, "B", rowStartForDelete, rowFinishOrder1);
                if (!FillRowsWithSpares(rowStartOrder2, rowFinishOrder2, SpareList))
                    return;
                DeleteRows(xlSheet, "B", rowStartForDelete, rowFinishOrder2);
                xlSheet.Cells[36, "Q"] = Malfunctions.GetTotalPriceFromList(MalfList);
                xlSheet.Cells[52, "Q"] = Malfunctions.GetTotalPriceFromList(SpareList);
                xlSheet.Cells[53, "Q"] = card.TotalPrice;
                fileName = String.Format(@"{0} Заказ наряд {1}.xlsx", card.IdRepair, card.Car.Owner.Name.Replace("\"", ""));
                xlWorkBook.SaveAs(fo.ExcelPath + fo.MakeOrderNameFile(card));
                xlWorkBook.Close();
                xlApp.Quit();
                while (System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp) > 0) { }
                //MessageBox.Show($"Наряд № {idRepair} выгружен в формате Excel!");
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
        static public void MakeBillInExcel(string idRepair)
        {
            try
            {
                FileOperator fo = new FileOperator();
                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(ConfigurationManager.AppSettings.Get("PathForOpenBill"));
                xlSheet = xlWorkBook.Sheets[1];
                CardOfRepair card = new CardMapper().Get(idRepair.ToString());
                xlSheet.Cells[13, "K"] = card.IdRepair;
                xlSheet.Cells[13, "Q"] = ((DateTime)card.TimeOfFinish).ToString("dd/MM/yyyy");
                xlSheet.Cells[19, "F"] = $"{card.Car.Owner.Name}, ИНН {card.Car.Owner.INN}, {card.Car.Owner.Address}";
                xlSheet.Cells[5, "E"] = $"{card.Car.Mark} {card.Car.Number}";
                xlSheet.Cells[22, "D"] = $"Ремонт автомобиля: {card.Car.Mark} {card.Car.Number} по заявке № {idRepair}" +
                    $" от {((DateTime)card.TimeOfFinish).ToString("dd/MM/yyyy")}";
                xlSheet.Cells[22, "AB"] = card.TotalPrice;
                xlSheet.Cells[28, "B"] = RuDateAndMoneyConverter.CurrencyToTxt(card.TotalPrice, true);
                fileName = String.Format(@"{0} Счет на оплату {1}.xlsx", card.IdRepair, card.Car.Owner.Name.Replace("\"", ""));
                xlWorkBook.SaveAs(fo.ExcelPath + fo.MakeBillNameFile(card));
                xlWorkBook.Close();
                xlApp.Quit();
                while (System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp) > 0) { }
                //MessageBox.Show($"Счет № {idRepair} выгружен в формате Excel!");
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
        static private bool FillRowsWithMalf(int startRow, int FinishRow, List<Malfunctions> MalfList)
        {
            if (MalfList.Count > FinishRow)
            {
                MessageBox.Show("Не удалось выгрузить документ!");
                return false;
            }
            for (int i = startRow, j = 0; j < MalfList.Count; i++, j++)
            {
                if (MalfList[j].Unit == Units.Pieces)
                    xlSheet.Cells[i, "M"] = MalfList[j].Number;
                else
                    xlSheet.Cells[i, "N"] = MalfList[j].Number;
                xlSheet.Cells[i, "B"] = $"{MalfList[j].Description}";
                xlSheet.Cells[i, "O"] = $"{MalfList[j].Price}";
                xlSheet.Cells[i, "Q"] = $"{MalfList[j].TotalPrice}";
                rowStartForDelete = i + 1;
            }
            return true;
        }
        static private bool FillRowsWithSpares(int startRow, int FinishRow, List<Malfunctions> SpareList)
        {
            rowStartForDelete = startRow;
            if (SpareList.Count > FinishRow)
            {
                MessageBox.Show("Не удалось выгрузить документ!");
                return false;
            }
            for (int i = startRow, j = 0; j < SpareList.Count; i++, j++)
            {
                xlSheet.Cells[i, "N"] = SpareList[j].Number;
                xlSheet.Cells[i, "B"] = $"{SpareList[j].Description}";
                xlSheet.Cells[i, "O"] = $"{SpareList[j].Price}";
                xlSheet.Cells[i, "Q"] = $"{SpareList[j].TotalPrice}";
                rowStartForDelete = i + 1;
            }
            return true;
        }

        static void DeleteRows(Excel.Worksheet xlSheet, string column, int rowStart, int rowFinish)
        {
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
