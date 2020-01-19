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

namespace WorkWithExcelLibrary
{
    public static class WorkWithExcel
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
        static string mainPath = ConfigurationManager.AppSettings.Get("DestExcelFolder");
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
                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(ConfigurationManager.AppSettings.Get("PathForOpenAct"));
                xlSheet = xlWorkBook.Sheets[1];
                CardOfRepair card = new CardMapper().Get(idRepair.ToString());
                xlSheet.Cells[3, "G"] = card.IdRepair;
                xlSheet.Cells[3, "J"] = ((DateTime) card.TimeOfFinish).ToString("dd/MM/yyyy");
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
                fileName = String.Format(@"{0} Акт {1}.xlsx", card.IdRepair, card.Car.Owner.Name.Replace("\"", ""));
                xlWorkBook.SaveAs(mainPath + fileName);
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
                xlWorkBook.SaveAs(mainPath + fileName);
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
                xlWorkBook.SaveAs(mainPath + fileName);
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
                xlSheet.Cells[i, "O"] = $"{MalfList[j].Cost}";
                xlSheet.Cells[i, "Q"] = $"{MalfList[j].TotalCost}";
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
                xlSheet.Cells[i, "O"] = $"{SpareList[j].Cost}";
                xlSheet.Cells[i, "Q"] = $"{SpareList[j].TotalCost}";
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
        public enum TextCase { Nominative/*Кто? Что?*/, Genitive/*Кого? Чего?*/, Dative/*Кому? Чему?*/, Accusative/*Кого? Что?*/, Instrumental/*Кем? Чем?*/, Prepositional/*О ком? О чём?*/ };

        public static class RuDateAndMoneyConverter
        {
            static Dictionary<TextCase, string[]> monthNames = new Dictionary<TextCase, string[]>
        {
        { TextCase.Nominative, new []{"", "январь", "февраль", "март", "апрель", "май", "июнь", "июль", "август", "сентябрь", "октябрь", "ноябрь", "декабрь"} },
        { TextCase.Genitive, new []{"", "января", "февраля", "марта", "апреля", "мая", "июня", "июля", "августа", "сентября", "октября", "ноября", "декабря"} }
        };


            static string zero = "ноль";
            static string firstMale = "один";
            static string firstFemale = "одна";
            static string firstFemaleAccusative = "одну";
            static string firstMaleGenetive = "одно";
            static string secondMale = "два";
            static string secondFemale = "две";
            static string secondMaleGenetive = "двух";
            static string secondFemaleGenetive = "двух";

            static string[] from3till19 =
            {
        "", "три", "четыре", "пять", "шесть",
        "семь", "восемь", "девять", "десять", "одиннадцать",
        "двенадцать", "тринадцать", "четырнадцать", "пятнадцать",
        "шестнадцать", "семнадцать", "восемнадцать", "девятнадцать"
    };
            static string[] from3till19Genetive =
            {
        "", "трех", "четырех", "пяти", "шести",
        "семи", "восеми", "девяти", "десяти", "одиннадцати",
        "двенадцати", "тринадцати", "четырнадцати", "пятнадцати",
        "шестнадцати", "семнадцати", "восемнадцати", "девятнадцати"
    };
            static string[] tens =
            {
        "", "двадцать", "тридцать", "сорок", "пятьдесят",
        "шестьдесят", "семьдесят", "восемьдесят", "девяносто"
    };
            static string[] tensGenetive =
            {
        "", "двадцати", "тридцати", "сорока", "пятидесяти",
        "шестидесяти", "семидесяти", "восьмидесяти", "девяноста"
    };
            static string[] hundreds =
            {
        "", "сто", "двести", "триста", "четыреста",
        "пятьсот", "шестьсот", "семьсот", "восемьсот", "девятьсот"
    };
            static string[] hundredsGenetive =
            {
        "", "ста", "двухсот", "трехсот", "четырехсот",
        "пятисот", "шестисот", "семисот", "восемисот", "девятисот"
    };
            static string[] thousands =
            {
        "", "тысяча", "тысячи", "тысяч"
    };
            static string[] thousandsAccusative =
            {
        "", "тысячу", "тысячи", "тысяч"
    };
            static string[] millions =
            {
        "", "миллион", "миллиона", "миллионов"
    };
            static string[] billions =
            {
        "", "миллиард", "миллиарда", "миллиардов"
    };
            static string[] trillions =
            {
        "", "трилион", "трилиона", "триллионов"
    };
            static string[] rubles =
            {
        "", "рубль", "рубля", "рублей"
    };
            static string[] copecks =
            {
        "", "копейка", "копейки", "копеек"
    };

            /// <summary>
            /// «07» января 2004 [+ _year(:года)]
            /// </summary>
            /// <param name="_date"></param>
            /// <param name="_year"></param>
            /// <returns></returns>
            public static string DateToTextLong(DateTime _date, string _year)
            {
                return String.Format("«{0}» {1} {2}",
                                        _date.Day.ToString("D2"),
                                        MonthName(_date.Month, TextCase.Genitive),
                                        _date.Year.ToString()) + ((_year.Length != 0) ? " " : "") + _year;
            }

            /// <summary>
            /// «07» января 2004
            /// </summary>
            /// <param name="_date"></param>
            /// <returns></returns>
            public static string DateToTextLong(DateTime _date)
            {
                return String.Format("«{0}» {1} {2}",
                                        _date.Day.ToString("D2"),
                                        MonthName(_date.Month, TextCase.Genitive),
                                        _date.Year.ToString());
            }
            /// <summary>
            /// II квартал 2004
            /// </summary>
            /// <param name="_date"></param>
            /// <returns></returns>
            public static string DateToTextQuarter(DateTime _date)
            {
                return NumeralsRoman(DateQuarter(_date)) + " квартал " + _date.Year.ToString();
            }
            /// <summary>
            /// 07.01.2004
            /// </summary>
            /// <param name="_date"></param>
            /// <returns></returns>
            public static string DateToTextSimple(DateTime _date)
            {
                return String.Format("{0:dd.MM.yyyy}", _date);
            }
            public static int DateQuarter(DateTime _date)
            {
                return (_date.Month - 1) / 3 + 1;
            }

            static bool IsPluralGenitive(int _digits)
            {
                if (_digits >= 5 || _digits == 0)
                    return true;

                return false;
            }
            static bool IsSingularGenitive(int _digits)
            {
                if (_digits >= 2 && _digits <= 4)
                    return true;

                return false;
            }
            static int lastDigit(long _amount)
            {
                long amount = _amount;

                if (amount >= 100)
                    amount = amount % 100;

                if (amount >= 20)
                    amount = amount % 10;

                return (int)amount;
            }
            /// <summary>
            /// Десять тысяч рублей 67 копеек
            /// </summary>
            /// <param name="_amount"></param>
            /// <param name="_firstCapital"></param>
            /// <returns></returns>
            public static string CurrencyToTxt(double _amount, bool _firstCapital)
            {
                //Десять тысяч рублей 67 копеек
                long rublesAmount = (long)Math.Floor(_amount);
                long copecksAmount = ((long)Math.Round(_amount * 100)) % 100;
                int lastRublesDigit = lastDigit(rublesAmount);
                int lastCopecksDigit = lastDigit(copecksAmount);

                string s = NumeralsToTxt(rublesAmount, TextCase.Nominative, true, _firstCapital) + " ";

                if (IsPluralGenitive(lastRublesDigit))
                {
                    s += rubles[3] + " ";
                }
                else if (IsSingularGenitive(lastRublesDigit))
                {
                    s += rubles[2] + " ";
                }
                else
                {
                    s += rubles[1] + " ";
                }

                s += String.Format("{0:00} ", copecksAmount);

                if (IsPluralGenitive(lastCopecksDigit))
                {
                    s += copecks[3] + " ";
                }
                else if (IsSingularGenitive(lastCopecksDigit))
                {
                    s += copecks[2] + " ";
                }
                else
                {
                    s += copecks[1] + " ";
                }

                return s.Trim();
            }
            /// <summary>
            /// 10 000 (Десять тысяч) рублей 67 копеек
            /// </summary>
            /// <param name="_amount"></param>
            /// <param name="_firstCapital"></param>
            /// <returns></returns>
            public static string CurrencyToTxtFull(double _amount, bool _firstCapital)
            {
                //10 000 (Десять тысяч) рублей 67 копеек
                long rublesAmount = (long)Math.Floor(_amount);
                long copecksAmount = ((long)Math.Round(_amount * 100)) % 100;
                int lastRublesDigit = lastDigit(rublesAmount);
                int lastCopecksDigit = lastDigit(copecksAmount);

                string s = String.Format("{0:N0} ({1}) ", rublesAmount, NumeralsToTxt(rublesAmount, TextCase.Nominative, true, _firstCapital));

                if (IsPluralGenitive(lastRublesDigit))
                {
                    s += rubles[3] + " ";
                }
                else if (IsSingularGenitive(lastRublesDigit))
                {
                    s += rubles[2] + " ";
                }
                else
                {
                    s += rubles[1] + " ";
                }

                s += String.Format("{0:00} ", copecksAmount);

                if (IsPluralGenitive(lastCopecksDigit))
                {
                    s += copecks[3] + " ";
                }
                else if (IsSingularGenitive(lastCopecksDigit))
                {
                    s += copecks[2] + " ";
                }
                else
                {
                    s += copecks[1] + " ";
                }

                return s.Trim();
            }
            /// <summary>
            /// 10 000 рублей 67 копеек  
            /// </summary>
            /// <param name="_amount"></param>
            /// <param name="_firstCapital"></param>
            /// <returns></returns>
            public static string CurrencyToTxtShort(double _amount, bool _firstCapital)
            {
                //10 000 рублей 67 копеек        
                long rublesAmount = (long)Math.Floor(_amount);
                long copecksAmount = ((long)Math.Round(_amount * 100)) % 100;
                int lastRublesDigit = lastDigit(rublesAmount);
                int lastCopecksDigit = lastDigit(copecksAmount);

                string s = String.Format("{0:N0} ", rublesAmount);

                if (IsPluralGenitive(lastRublesDigit))
                {
                    s += rubles[3] + " ";
                }
                else if (IsSingularGenitive(lastRublesDigit))
                {
                    s += rubles[2] + " ";
                }
                else
                {
                    s += rubles[1] + " ";
                }

                s += String.Format("{0:00} ", copecksAmount);

                if (IsPluralGenitive(lastCopecksDigit))
                {
                    s += copecks[3] + " ";
                }
                else if (IsSingularGenitive(lastCopecksDigit))
                {
                    s += copecks[2] + " ";
                }
                else
                {
                    s += copecks[1] + " ";
                }

                return s.Trim();
            }
            static string MakeText(int _digits, string[] _hundreds, string[] _tens, string[] _from3till19, string _second, string _first, string[] _power)
            {
                string s = "";
                int digits = _digits;

                if (digits >= 100)
                {
                    s += _hundreds[digits / 100] + " ";
                    digits = digits % 100;
                }
                if (digits >= 20)
                {
                    s += _tens[digits / 10 - 1] + " ";
                    digits = digits % 10;
                }

                if (digits >= 3)
                {
                    s += _from3till19[digits - 2] + " ";
                }
                else if (digits == 2)
                {
                    s += _second + " ";
                }
                else if (digits == 1)
                {
                    s += _first + " ";
                }

                if (_digits != 0 && _power.Length > 0)
                {
                    digits = lastDigit(_digits);

                    if (IsPluralGenitive(digits))
                    {
                        s += _power[3] + " ";
                    }
                    else if (IsSingularGenitive(digits))
                    {
                        s += _power[2] + " ";
                    }
                    else
                    {
                        s += _power[1] + " ";
                    }
                }

                return s;
            }

            /// <summary>
            /// реализовано для падежей: именительный (nominative), родительный (Genitive),  винительный (accusative)
            /// </summary>
            /// <param name="_sourceNumber"></param>
            /// <param name="_case"></param>
            /// <param name="_isMale"></param>
            /// <param name="_firstCapital"></param>
            /// <returns></returns>
            public static string NumeralsToTxt(long _sourceNumber, TextCase _case, bool _isMale, bool _firstCapital)
            {
                string s = "";
                long number = _sourceNumber;
                int remainder;
                int power = 0;

                if ((number >= (long)Math.Pow(10, 15)) || number < 0)
                {
                    return "";
                }

                while (number > 0)
                {
                    remainder = (int)(number % 1000);
                    number = number / 1000;

                    switch (power)
                    {
                        case 12:
                            s = MakeText(remainder, hundreds, tens, from3till19, secondMale, firstMale, trillions) + s;
                            break;
                        case 9:
                            s = MakeText(remainder, hundreds, tens, from3till19, secondMale, firstMale, billions) + s;
                            break;
                        case 6:
                            s = MakeText(remainder, hundreds, tens, from3till19, secondMale, firstMale, millions) + s;
                            break;
                        case 3:
                            switch (_case)
                            {
                                case TextCase.Accusative:
                                    s = MakeText(remainder, hundreds, tens, from3till19, secondFemale, firstFemaleAccusative, thousandsAccusative) + s;
                                    break;
                                default:
                                    s = MakeText(remainder, hundreds, tens, from3till19, secondFemale, firstFemale, thousands) + s;
                                    break;
                            }
                            break;
                        default:
                            string[] powerArray = { };
                            switch (_case)
                            {
                                case TextCase.Genitive:
                                    s = MakeText(remainder, hundredsGenetive, tensGenetive, from3till19Genetive, _isMale ? secondMaleGenetive : secondFemaleGenetive, _isMale ? firstMaleGenetive : firstFemale, powerArray) + s;
                                    break;
                                case TextCase.Accusative:
                                    s = MakeText(remainder, hundreds, tens, from3till19, _isMale ? secondMale : secondFemale, _isMale ? firstMale : firstFemaleAccusative, powerArray) + s;
                                    break;
                                default:
                                    s = MakeText(remainder, hundreds, tens, from3till19, _isMale ? secondMale : secondFemale, _isMale ? firstMale : firstFemale, powerArray) + s;
                                    break;
                            }
                            break;
                    }

                    power += 3;
                }

                if (_sourceNumber == 0)
                {
                    s = zero + " ";
                }

                if (s != "" && _firstCapital)
                    s = s.Substring(0, 1).ToUpper() + s.Substring(1);

                return s.Trim();
            }
            public static string NumeralsDoubleToTxt(double _sourceNumber, int _decimal, TextCase _case, bool _firstCapital)
            {
                long decNum = (long)Math.Round(_sourceNumber * Math.Pow(10, _decimal)) % (long)(Math.Pow(10, _decimal));

                string s = String.Format(" {0} целых {1} сотых", NumeralsToTxt((long)_sourceNumber, _case, true, _firstCapital),
                                                      NumeralsToTxt((long)decNum, _case, true, false));
                return s.Trim();
            }
            /// <summary>
            /// название м-ца
            /// </summary>
            /// <param name="_month">с единицы</param>
            /// <param name="_case"></param>
            /// <returns></returns>
            public static string MonthName(int _month, TextCase _case)
            {
                string s = "";

                if (_month > 0 && _month <= 12 && monthNames.ContainsKey(_case))
                {
                    return monthNames[_case][_month];
                }

                return s;
            }
            public static string NumeralsRoman(int _number)
            {
                string s = "";

                switch (_number)
                {
                    case 1: s = "I"; break;
                    case 2: s = "II"; break;
                    case 3: s = "III"; break;
                    case 4: s = "IV"; break;
                }

                return s;
            }
        }
    }
}
