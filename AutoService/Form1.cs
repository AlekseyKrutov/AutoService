﻿using AutoService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Xml;
using System.Xml.Linq;
using FirebirdSql.Data.FirebirdClient;
using AutoServiceLibrary;

namespace AutoService
{
    public partial class Form1 : Form
    {
        FbConnectionStringBuilder csb = new FbConnectionStringBuilder();
        static public FbConnection db;
        //формы окон
        FormAddRepair formAddRepair = new FormAddRepair();
        FormAddAuto formAddAuto = new FormAddAuto();
        FormAddClient formAddClient = new FormAddClient();
        FormAddPersonal formAddPersonal = new FormAddPersonal();
        FormAddPrice formAddPrice = new FormAddPrice();
        FormAddSparePart formAddSparePart = new FormAddSparePart();
        FormAuthorization formAuthorization = new FormAuthorization();

        //переменная изменения размера
        int resizeCount = 80;
        //переменная для верхнего положения сетки
        int topForGridIdeal = 0;  
        //переменная для верхнего положения заголовка
        int topForLabelOfHead = 0;
        //переменная выбранного индекса в сетке
        static public int SelectIndex;
        //переменная для определения окна с которым мы работаем
        static public int WindowIndex = 0;

        public static int AddOrEdit;
        //структура с названиями окон
        public enum WindowsStruct { Repairs = 1, Auto, ActOfEndsRepairs, Worker, MalfAdd, MalfView, Stock , Client }
        public enum AddEditOrDelete { Add, Edit, Delete };
        //список добавленных неисправностей
        public static List<Malfunctions> malfListForRepairAll = new List<Malfunctions>();
        public static List<Malfunctions> malfListForRepairAdded = new List<Malfunctions>();

        //конструктор формы
        public Form1()
        {
            InitializeComponent();
            topForGridIdeal = dataGridView.Top;
            topForLabelOfHead = labelHeaderText.Top;
        }
        //события загрузки формы
        private void Form1_Load(object sender, EventArgs e)
        {
            Padding padding = new Padding(0, 8, 0, 8);
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView.RowTemplate.DefaultCellStyle.Padding = padding;
            HideAutoButtons();
            HideClientButtons();
            HidePersonalButtons();
            HidePriceButtons();
            HideStockButtons();
            HideSearch();
            dataGridView.AllowUserToAddRows = false;
            dataGridView.ClearSelection();
            dataGridView.RowHeadersVisible = false;
            labelHeaderText.Text = "Текущие ремонты";
            DataGridViewForRepairs();
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            //вывод данных в XML
            if (GetXmlInventory() != null)
            {
                XDocument xdoc = GetXmlInventory();
                //добавление ремонтов в список
                var itemsRepair = from xe in xdoc.Element("elements").Element("repairs").Elements("repair")
                                  select new CardOfRepair
                                  {
                                      CarMark = xe.Element("CarMark").Value,
                                      CarModel = xe.Element("CarModel").Value,
                                      CarVIN = xe.Element("VIN").Value,
                                      NumberOfCar = xe.Element("NumberOfCar").Value,
                                      RegCertific = xe.Element("RegCertific").Value,
                                      Owner = new Client
                                      {
                                          Address = xe.Element("Owner").Element("Address").Value,
                                          INN = xe.Element("Owner").Element("INN").Value,
                                          Name = xe.Element("Owner").Element("OwnerName").Value,
                                          NumberOfTel = xe.Element("Owner").Element("NumberOfTel").Value
                                      },
                                      Mechanic = new Personal(xe.Element("Mechanic").Element("Name").Value),
                                      Notes = xe.Element("Notes").Value,
                                      TotalPrice = double.Parse(xe.Element("TotalPrice").Value),
                                      RepairIsCurrent = bool.Parse(xe.Element("RepairIsCurrent").Value),
                                      TimeOfStart = xe.Element("TimeOfStart").Value,
                                      TimeOfEnd = xe.Element("TimeOfEnd").Value,
                                      NumberOfAct = int.Parse(xe.Element("NumberOfAct").Value),
                                      ListOfMalf = (from xx in xe.Elements("malfunction")
                                                   select new Malfunctions { Price = (double.Parse(xx.Attribute("Price").Value)), 
                                                          DescriptionOfMalf = xx.Attribute("DescriptionOfMulf").Value, Currancy = "Рублей" }).ToList()
                                  };
                foreach (var item in itemsRepair)
                {
                    CardOfRepair.repairsList.Add((CardOfRepair)item);
                }
                int i = 0;
                int biggestNumb = 0;
                if (CardOfRepair.repairsList.Count == 0)
                    biggestNumb = 0;
                else
                {
                    biggestNumb = CardOfRepair.repairsList[0].NumberOfAct;
                    while (i < CardOfRepair.repairsList.Count - 1)
                    {
                        if (biggestNumb < CardOfRepair.repairsList[i + 1].NumberOfAct)
                            biggestNumb = CardOfRepair.repairsList[i + 1].NumberOfAct;
                        i++;
                    }
                }
                CardOfRepair.Number = biggestNumb;
                //добавление автомобилей в список
                var itemsCar = from xe in xdoc.Element("elements").Element("cars").Elements("car")
                            select new Car
                            {
                                CarMark = xe.Element("CarMark").Value,
                                CarModel = xe.Element("CarModel").Value,
                                CarVIN = xe.Element("VIN").Value,
                                NumberOfCar = xe.Element("NumberOfCar").Value,
                                RegCertific = xe.Element("RegCertific").Value,
                                Owner = new Client
                                {
                                    Address = xe.Element("Owner").Element("Address").Value,
                                    INN = xe.Element("Owner").Element("INN").Value,
                                    Name = xe.Element("Owner").Element("OwnerName").Value,
                                    NumberOfTel = xe.Element("Owner").Element("NumberOfTel").Value
                                }
                            };
                foreach (var item in itemsCar)
                {
                    Car.CarList.Add((Car)item);
                }
                //добавление клиентов в список
                var itemsClients = from xe in xdoc.Element("elements").Element("clients").Elements("client")
                            select new Client
                            {
                                Address = xe.Element("Address").Value,
                                INN = xe.Element("INN").Value,
                                Name = xe.Element("Name").Value,
                                NumberOfTel = xe.Element("NumberOfTel").Value
                            };
                foreach (var item in itemsClients)
                {
                    Client.ClientList.Add((Client)item);
                }
                //
                if (CardOfRepair.repairsList.Count > 0)
                {
                    AddListRepairsInGrid();
                    DeleteSelectFirstRow();
                }

            }
            csb.DataSource = "localhost";
            csb.Port = 3050;
            csb.Charset = "UTF8";
            csb.Database = @"C:\Users\admin\Desktop\проекты\AUTOSERVICE_DB\AUTOSERVICE_DB.FDB";
            csb.UserID = "SYSDBA";
            csb.Password = "masterkey";
            csb.ServerType = FbServerType.Default;
            db = new FbConnection(csb.ToString());
            formAuthorization = new FormAuthorization(this);
        }
        //логическая переменная
        bool logicParamForRepair = true;
        //событие при клике на законченные ремонты
        private void EndRepairsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectIndex = 0;
            WindowIndex = 0;
            DataGridViewForRepairs();
            dataGridView.Rows.Clear();
            labelHeaderText.Text = "Завершенные ремонты";
            HideAutoButtons();
            HideRepairButtons();
            HideClientButtons();
            HidePersonalButtons();
            HidePriceButtons();
            HideSearch();
            if (logicParamForRepair)
            {
                labelHeaderText.Top -= resizeCount;
                dataGridView.Top -= resizeCount;
                dataGridView.Height += resizeCount;
            }
            logicParamForRepair = false;
            foreach (CardOfRepair card in CardOfRepair.repairsList)
            {
                if (!card.RepairIsCurrent)
                {
                    dataGridView.Rows.Add(card.NumberOfAct, card.TimeOfStart, card.MalfunctionsToString(), card.CarInRepairToString(), card.Mechanic.Name, card.Notes);
                }
            }
            DeleteSelectFirstRow();
        }
        //событие при клике на текущие ремонты
        private void CurrentRepairsToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            SelectIndex = 0;
            WindowIndex = 0;
            DataGridViewForRepairs();
            labelHeaderText.Text = "Текущие ремонты";
            ShowRepairButtons();
            HideAutoButtons();
            HideClientButtons();
            HidePersonalButtons();
            HidePriceButtons();
            HidePersonalButtons();
            HideStockButtons();
            HideSearch();
            AddListRepairsInGrid();
            if (!logicParamForRepair)
            {
                labelHeaderText.Top = topForLabelOfHead;
                dataGridView.Top = topForGridIdeal;
            }
            logicParamForRepair = true;
            DeleteSelectFirstRow();
        }
        //событие при клике на клиенты
        private void ClientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logicParamForRepair = true;
            SelectIndex = 0;
            WindowIndex = (int)WindowsStruct.Client;
            HideAutoButtons();
            HideRepairButtons();
            HidePersonalButtons();
            HidePriceButtons();
            ShowClientButtons();
            HideStockButtons();
            HideSearch();
            AddClient.Location = AddRepair.Location;
            EditClient.Location = EditRepair.Location;
            DeleteClient.Location = EndRepair.Location;
            labelHeaderText.Text = "Клиенты";
            dataGridView.Top = topForGridIdeal;
            labelHeaderText.Top = topForLabelOfHead;
            DataGridViewForClient();
            DeleteSelectFirstRow();
        }
        //событие при клике на персонал
        private void PersonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logicParamForRepair = true;
            SelectIndex = 0;
            WindowIndex = (int)WindowsStruct.Worker;
            HideAutoButtons();
            HideRepairButtons();
            HideClientButtons();
            HidePriceButtons();
            HideStockButtons();
            HideSearch();
            ShowPersonalButtons();
            AddPersonal.Location = AddRepair.Location;
            EditPersonal.Location = EditRepair.Location;
            DeletePersonal.Location = EndRepair.Location;
            labelHeaderText.Text = "Сотрудники";
            dataGridView.Top = topForGridIdeal;
            labelHeaderText.Top = topForLabelOfHead;
            DataGridViewForPersonal();
            DeleteSelectFirstRow();
        }
        //событие при клике на автомобили
        private void AutoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            SelectIndex = 0;
            WindowIndex = (int)WindowsStruct.Auto;
            logicParamForRepair = true;
            ShowAutoButtons();
            AddAuto.Location = AddRepair.Location;
            EditAuto.Location = EditRepair.Location;
            DeleteAuto.Location = EndRepair.Location;
            HideRepairButtons();
            HideClientButtons();
            HidePersonalButtons();
            HidePriceButtons();
            HideStockButtons();
            HideSearch();
            dataGridView.Top = topForGridIdeal;
            labelHeaderText.Text = "Автомобили";
            labelHeaderText.Top = topForLabelOfHead;
            DataGridViewForAuto();
            DeleteSelectFirstRow();
        }
        //событие при клике на прайс
        private void PriceStripMenuItem1_Click(object sender, EventArgs e)
        {
            logicParamForRepair = true;
            SelectIndex = 0;
            WindowIndex = 0;
            HideAutoButtons();
            HideRepairButtons();
            HideClientButtons();
            HidePersonalButtons();
            HideStockButtons();
            HideSearch();
            ShowPriceButtons();
            AddPosition.Location = AddRepair.Location;
            EditPosition.Location = EditRepair.Location;
            DeletePosition.Location = EndRepair.Location;
            labelHeaderText.Text = "Прайс";
            dataGridView.Top = topForGridIdeal;
            labelHeaderText.Top = topForLabelOfHead;
            DataGridViewForPrice();
            DeleteSelectFirstRow();
        }

        private void StockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowIndex = (int) WindowsStruct.Stock;
            logicParamForRepair = true;
            SelectIndex = 0;
            HideAutoButtons();
            HideRepairButtons();
            HideClientButtons();
            HidePersonalButtons();
            HidePriceButtons();
            ShowStockButtons();
            ShowSearch();
            AddInStock.Location = AddRepair.Location;
            EditStock.Location = EditRepair.Location;
            DeleteFromStock.Location = EndRepair.Location;
            labelHeaderText.Text = "Склад";
            dataGridView.Top = topForGridIdeal;
            labelHeaderText.Top = topForLabelOfHead;
            DataGridViewForStock();
            DeleteSelectFirstRow();
        }
        //событие при клике по сетке
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SelectIndex = e.RowIndex;
            }
            catch (ArgumentOutOfRangeException)
            {
                SelectIndex = 0;
                return;
            }
        }
        //событие при двойном клике по сетке
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridView.Rows[e.RowIndex].Selected = false;
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                dataGridView.Rows[e.RowIndex].Selected = true;
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
        }

        //функции для сокрытия или показа кнопок
        private void HideAutoButtons()
        {
            AddAuto.Hide();
            EditAuto.Hide();
            DeleteAuto.Hide();
        }
        private void ShowAutoButtons()
        {
            AddAuto.Visible = true;
            EditAuto.Visible = true;
            DeleteAuto.Visible = true;
        }
        private void HideRepairButtons()
        {
            AddRepair.Hide();
            EditRepair.Hide();
            EndRepair.Hide();
        }
        private void ShowRepairButtons()
        {
            AddRepair.Visible = true;
            EditRepair.Visible = true;
            EndRepair.Visible = true;
        }
        private void HideClientButtons()
        {
            AddClient.Hide();
            EditClient.Hide();
            DeleteClient.Hide();
        }
        private void ShowClientButtons()
        {
            AddClient.Visible = true;
            EditClient.Visible = true;
            DeleteClient.Visible = true;
        }
        private void HidePersonalButtons()
        {
            AddPersonal.Hide();
            EditPersonal.Hide();
            DeletePersonal.Hide();
        }
        private void ShowPersonalButtons()
        {
            AddPersonal.Visible = true;
            EditPersonal.Visible = true;
            DeletePersonal.Visible = true;
        }
        private void HidePriceButtons()
        {
            AddPosition.Hide();
            EditPosition.Hide();
            DeletePosition.Hide();
        }
        private void ShowPriceButtons()
        {
            AddPosition.Visible = true;
            EditPosition.Visible = true;
            DeletePosition.Visible = true;
        }
        private void HideStockButtons()
        {
            AddInStock.Hide();
            EditStock.Hide();
            DeleteFromStock.Hide();
        }
        private void ShowStockButtons()
        {
            AddInStock.Visible = true;
            EditStock.Visible = true;
            DeleteFromStock.Visible = true;
        }
        private void ShowSearch()
        {
            labelSearch.Visible = true;
            textBoxSearch.Visible = true;
        }
        private void HideSearch()
        {
            labelSearch.Visible = false;
            textBoxSearch.Visible = false;
        }
        //событие при клике по кнопке добавить ремонт
        private void AddRepair_Click(object sender, EventArgs e)
        {
            WindowIndex = (int) WindowsStruct.Repairs;
            //проверка на наличие открытой формы
            if (!formAddRepair.Visible)
            {
                formAddRepair = new FormAddRepair(formAddAuto, this);
                formAddRepair.ShowDialog();
            }
            else
                //если окно добавления автомобиля будет открыто выведется окно с предупреждением
                MessageBox.Show("Вы уже создаете новый ремонт, для создания нового ремонта завершите старый.", "", MessageBoxButtons.OK);
            return;

        }
        private void EndRepair_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show(string.Format("Вы действительно завершить ремонт №{0}?", CardOfRepair.repairsList[SelectIndex].NumberOfAct), "Предупреждение",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == DialogResult.OK) && CardOfRepair.repairsList.Count > 0)
            {
                CardOfRepair.repairsList[SelectIndex].RepairIsCurrent = false;
                CardOfRepair.repairsList[SelectIndex].TimeOfEnd = DateTime.Now.ToString();
                CardOfRepair.repairsList.Sort(delegate(CardOfRepair card1, CardOfRepair card2)
                {
                    if (!card1.RepairIsCurrent)
                        return 1;
                    if (card1.RepairIsCurrent)
                        return -1;
                    else
                        return 0;
                });
                AddListRepairsInGrid();
            }
        }
        //событие при клике по кнопке добавить автомобиль
        private void AddAuto_Click(object sender, EventArgs e)
        {
            AddOrEdit = (int)AddEditOrDelete.Add;
            //проверка на наличие открытой формы
            if (!formAddAuto.Visible)
            {
                formAddAuto = new FormAddAuto(formAddRepair, this);
                formAddAuto.StartPosition = FormStartPosition.CenterScreen;
                formAddAuto.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                formAddAuto.MaximizeBox = false;
                formAddAuto.ShowDialog();
            }
            // если окно добавления уже открыто событие игнорируется
            else
                return;
        }
        private void EditAuto_Click(object sender, EventArgs e)
        {
            AddOrEdit = (int)AddEditOrDelete.Edit;
            if (!formAddAuto.Visible)
            {
                formAddAuto = new FormAddAuto(this);
                formAddAuto.OwnerSelected = true;
                string query = string.Format("select * from cars_view where VIN like '{0}'", 
                    dataGridView.Rows[SelectIndex].Cells[0].Value.ToString());
                using (FbCommand command = new FbCommand(query, db))
                {
                    FbDataReader dataReader;
                    db.Open();
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        formAddAuto.textBoxVIN.Text = dataReader.GetString(0);
                        formAddAuto.textBoxGosNumb.Text = dataReader.GetString(2);
                        formAddAuto.textBoxReg.Text = dataReader.GetString(3);
                        formAddAuto.labelContentOwner.Text = dataReader.GetString(4);
                    }
                    db.Close();
                }
                formAddAuto.ShowDialog();
            }
            else
                return;
        }
        private void AddBankInComBoxClient()
        {
            string query = @"select kor_bill 
                             from bank";
            using (FbCommand command = new FbCommand(query, Form1.db))
            {
                FbDataAdapter dataAdapter = new FbDataAdapter(command);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                Form1.db.Open();
                formAddClient.comboBoxBank.DataSource = dt;
                formAddClient.comboBoxBank.DisplayMember = "kor_bill";
                formAddClient.comboBoxBank.Text = "";
                if (Form1.AddOrEdit == (int)Form1.AddEditOrDelete.Add)
                    formAddClient.comboBoxBank.SelectedIndex = -1;
                Form1.db.Close();
            }
        }
        //событие при клике по кнопке добавить клиента
        private void AddClient_Click(object sender, EventArgs e)
        {
            AddOrEdit = (int)AddEditOrDelete.Add;
            if (!formAddClient.Visible)
            {
                formAddClient = new FormAddClient(this);
                AddBankInComBoxClient();
                formAddClient.ShowDialog();
            }
        }
        
        //событие при клике по кнопке удалить клиента
        private void EditClient_Click(object sender, EventArgs e)
        {
            AddOrEdit = (int)AddEditOrDelete.Edit;
            if (!formAddClient.Visible)
            {
                formAddClient = new FormAddClient(this);
                AddBankInComBoxClient();
                string query = string.Format("select inn, name_org, director, bank_bill," +
                    "phone_numb, bill, kpp, oktmo, okato, bik, ogrn, address " +
                    "from client where name_org = '{0}'",
                dataGridView.Rows[SelectIndex].Cells[0].Value.ToString());
                using (FbCommand command = new FbCommand(query, db))
                {
                    FbDataReader dataReader;
                    db.Open();
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        formAddClient.textBoxINN.Text = dataReader.GetString(0);
                        formAddClient.textBoxName.Text = dataReader.GetString(1);
                        formAddClient.textBoxDirector.Text = dataReader.GetString(2);
                        if (dataReader.GetString(3).Length != 0)
                            formAddClient.comboBoxBank.Text = dataReader.GetString(3);
                        else
                            formAddClient.comboBoxBank.SelectedIndex = -1;
                        formAddClient.textBoxNumbOfTel.Text = dataReader.GetString(4);
                        formAddClient.textBoxBill.Text = dataReader.GetString(5);
                        formAddClient.textBoxKPP.Text = dataReader.GetString(6);
                        formAddClient.textBoxOKTMO.Text = dataReader.GetString(7);
                        formAddClient.textBoxOKATO.Text = dataReader.GetString(8);
                        formAddClient.textBoxBIK.Text = dataReader.GetString(9);
                        formAddClient.textBoxOGRN.Text = dataReader.GetString(10);
                        formAddClient.textBoxAddress.Text = dataReader.GetString(11);
                    }
                    db.Close();
                }
                formAddClient.ShowDialog();
            }
            else
                return;
        }
        //событие при клике по кнопке добавить персонал
        private void AddPersonal_Click(object sender, EventArgs e)
        {
            AddOrEdit = (int)AddEditOrDelete.Add;
            if (!formAddPersonal.Visible)
            {
                formAddPersonal = new FormAddPersonal(this);
                formAddPersonal.ShowDialog();
            }
            else
                return;
        }
        private void EditPersonal_Click(object sender, EventArgs e)
        {
            AddOrEdit = (int)AddEditOrDelete.Edit;
            formAddPersonal = new FormAddPersonal(this);
            string query = string.Format("select first_name, second_name, last_name," +
                                        "inn, passport, phone_numb, profession, gender, address, date_born " +
                                        "from staff " +
                                        "where tub_numb = {0};",
                dataGridView.Rows[SelectIndex].Cells[0].Value.ToString());
            using (FbCommand command = new FbCommand(query, db))
            {
                FbDataReader dataReader;
                db.Open();
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    formAddPersonal.textBoxLastName.Text = dataReader.GetString(0);
                    formAddPersonal.textBoxFirstName.Text = dataReader.GetString(1);
                    formAddPersonal.textBoxSecondName.Text = dataReader.GetString(2);
                    formAddPersonal.textBoxINN.Text = dataReader.GetString(3);
                    formAddPersonal.textBoxPassport.Text = dataReader.GetString(4);
                    formAddPersonal.textBoxNumbOfTel.Text = dataReader.GetString(5);
                    formAddPersonal.textBoxAddress.Text = dataReader.GetString(8);
                    string[] date = dataReader.GetString(9).Split(' ').ToArray()[0].Split('.').Reverse().ToArray();
                    DateTime dateTime = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));
                    formAddPersonal.monthCalendarDayBirth.SelectionEnd = dateTime;
                    formAddPersonal.date = dateTime.ToString("dd/MM/yyyy");
                }
                db.Close();
            }
            formAddPersonal.ShowDialog();
        }
        //событие при клике по кнопке добавить позицию
        private void AddPosition_Click(object sender, EventArgs e)
        {
            AddOrEdit = (int)AddEditOrDelete.Add;
            if (!formAddPrice.Visible)
            {
                formAddPrice = new FormAddPrice(this);
                formAddPrice.ShowDialog();
            }
            else
                return;
        }
        //событие при клике по кнопке удалить позицию
        private void DeletePosition_Click(object sender, EventArgs e)
        {
            //при клике по заголовку сетки ловится исключение и событие игнорируется
            try
            {
                //при попытке удалить объект появляется окно с подтверждением удаления
                if ((MessageBox.Show(string.Format("Вы действительно хотите удалить эту позицию?{0}", Malfunctions.MalfList[SelectIndex].ToString()), "Предупреждение",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == DialogResult.OK) && Malfunctions.MalfList.Count > 0)
                {
                    Malfunctions.MalfList.RemoveAt(SelectIndex);
                    AddListMalfunctionsInGrid();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
        }

        private void AddInStock_Click(object sender, EventArgs e)
        {
            AddOrEdit = (int)AddEditOrDelete.Add;
            if (!formAddSparePart.Visible)
            {
                formAddSparePart = new FormAddSparePart(this);
                formAddSparePart.ShowDialog();
            }
            else
                return;
        }
        private void EditStock_Click(object sender, EventArgs e)
        {
            AddOrEdit = (int)AddEditOrDelete.Edit;
            if (!formAddSparePart.Visible)
            {
                formAddSparePart = new FormAddSparePart(this);
                string query = string.Format("select sp.uniq_code, sp.description, sp.cost, st.number " +
                    "from sparepart as sp, stock as st where sp.uniq_code = '{0}'  and st.uniq_code = '{0}'",
                dataGridView.Rows[SelectIndex].Cells[0].Value.ToString());
                using (FbCommand command = new FbCommand(query, db))
                {
                    FbDataReader dataReader;
                    db.Open();
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        formAddSparePart.textBoxUniqNumb.Text = dataReader.GetString(0);
                        formAddSparePart.textBoxDescr.Text = dataReader.GetString(1);
                        formAddSparePart.textBoxCost.Text = dataReader.GetString(2);
                        formAddSparePart.textBoxNumb.Text = dataReader.GetString(3);
                    }
                    db.Close();
                }
                formAddSparePart.ShowDialog();
            }
            else
                return;
        }
        //функции для добавления списка объектов в сетку
        public void AddListRepairsInGrid()
        {
            dataGridView.Rows.Clear();
            foreach (CardOfRepair repair in CardOfRepair.repairsList)
            {
                if (repair.RepairIsCurrent)
                {
                    dataGridView.Rows.Add(repair.NumberOfAct, repair.TimeOfStart, repair.MalfunctionsToString(), repair.CarInRepairToString(), repair.Mechanic.Name, repair.Notes);
                }
            }
        }
        public void AddListAutoInGrid()
        {
            string query = @"select * from cars_view";
            string[] columnNames = { "VIN", "Марка", "Гос.номер", "Свидетельство о рег.", "Владелец" };
            CreateViewForDataGrid(query, columnNames,dataGridView);
        }
        public void AddListClientInGrid()
        {
            string query = @"select * from client_view";
            string[] columnNames = { "Наименование", "Директор", "ИНН", "Номер телефона" };
            CreateViewForDataGrid(query, columnNames, dataGridView);
        }
        public void AddListPersonalInGrid()
        {
            string query = @"select * from staff_view";
            string[] columnNames = { "Табельный номер", "ФИО", "Адрес", "Должность", "Номер телефона" };
            CreateViewForDataGrid(query, columnNames, dataGridView);
        }
        public void AddListMalfunctionsInGrid()
        {
            string query = @"select * from type_of_work_view";
            string[] columnNames = { "Наименование", "Единица измерения", "Стоимость(руб.)" };
            CreateViewForDataGrid(query, columnNames, dataGridView);
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                if (dataGridView.Rows[i].Cells[1].Value.ToString() == "0")
                {
                    dataGridView.Rows[i].Cells[1].Value = "шт";
                }
                else
                    dataGridView.Rows[i].Cells[1].Value = "нч";
            }

        }
        private void AddSparePartInStock()
        {
            string query = @"select * from stock_view";
            string[] columnNames = { "Артикул", "Наименование", "Количество", "Cтоимость(руб.)", "Автомобиль" };
            CreateViewForDataGrid(query, columnNames, dataGridView);
        }
        //функции для редактирования сетки 
        private void DataGridViewForRepairs()
        {
            dataGridView.Columns[0].HeaderText = "№";
            dataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns[1].HeaderText = "Время начала ремонта";
            dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[2].HeaderText = "Неисправности";
            dataGridView.Columns[3].HeaderText = "Автомобиль";
            dataGridView.Columns[4].HeaderText = "Слесарь";
            dataGridView.Columns[5].HeaderText = "Заметки";
            VisibleColumns();
            /*
            dataGridView.Columns[6].HeaderText = "Гос. номер";
            dataGridView.Columns[7].HeaderText = "Неисправности";
            */
        }
        private void DataGridViewForClient()
        {
            dataGridView.Columns.Clear();
            AddListClientInGrid();
        }
        private void DataGridViewForPersonal()
        {
            dataGridView.Columns.Clear();
            AddListPersonalInGrid();
        }
        private void DataGridViewForAuto()
        {
            dataGridView.Columns.Clear();
            AddListAutoInGrid();
        }
        private void DataGridViewForPrice()
        {
            dataGridView.Columns.Clear();
            AddListMalfunctionsInGrid();
        }
        private void DataGridViewForStock()
        {
            dataGridView.Columns.Clear();
            AddSparePartInStock();
        }

        void VisibleColumns()
        {
            dataGridView.Columns[2].Visible = true;
            dataGridView.Columns[3].Visible = true;
            dataGridView.Columns[4].Visible = true;
            dataGridView.Columns[5].Visible = true;
        }

        //событие при закрытии формы
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            XDocument xdoc = new XDocument();
            XElement repairs = new XElement("repairs");
            if (CardOfRepair.repairsList.Count != 0)
            {
                foreach (CardOfRepair repair in CardOfRepair.repairsList)
                {
                    XElement repairToRepairs = new XElement("repair");
                    repairToRepairs.Add(new XElement("VIN", repair.CarVIN), new XElement("RegCertific", repair.RegCertific),
                        new XElement("CarMark", repair.CarMark), new XElement("CarModel", repair.CarModel), new XElement("NumberOfCar", repair.NumberOfCar),
                        new XElement("Owner", new XElement("OwnerName", repair.Owner.Name), new XElement("INN", repair.Owner.INN), new XElement("Address", repair.Owner.Address),
                        new XElement("NumberOfTel", repair.Owner.NumberOfTel)), new XElement("TimeOfStart", repair.TimeOfStart), new XElement("TimeOfEnd", repair.TimeOfEnd),
                        new XElement("Notes", repair.Notes), new XElement("Mechanic", new XElement("Name", repair.Mechanic.Name)), new XElement("RepairIsCurrent", repair.RepairIsCurrent),
                        new XElement("TotalPrice", repair.TotalPrice), new XElement("NumberOfAct", repair.NumberOfAct));
                    foreach (Malfunctions malf in repair.ListOfMalf)
                    {
                        repairToRepairs.Add(new XElement("malfunction", new XAttribute("Price", malf.Price), new XAttribute("DescriptionOfMulf", malf.DescriptionOfMalf),
                        new XAttribute("Currancy", malf.Currancy)));
                    }
                    repairs.Add(repairToRepairs);
                }
            }
           
            //добавление автомобилей в xml
            XElement cars = new XElement("cars");
            if (Car.CarList.Count != 0)
            {
                foreach (Car car in Car.CarList)
                {
                    cars.Add(new XElement("car", new XElement("VIN", car.CarVIN), new XElement("RegCertific", car.RegCertific),
                        new XElement("CarMark", car.CarMark), new XElement("CarModel", car.CarModel), new XElement("NumberOfCar", car.NumberOfCar),
                        new XElement("Owner", new XElement("OwnerName", car.Owner.Name), new XElement("INN", car.Owner.INN), new XElement("Address", car.Owner.Address),
                        new XElement("NumberOfTel", car.Owner.NumberOfTel))));
                }
            }
            //добавление клиентов в xml
            XElement clients = new XElement("clients");
            if (Client.ClientList.Count != 0)
            {
                foreach (Client client in Client.ClientList)
                {
                    clients.Add(new XElement("client", new XElement("Name", client.Name), new XElement("INN", client.INN),
                    new XElement("Address", client.Address), new XElement("NumberOfTel", client.NumberOfTel)));
                }
            }
            //добавление сотрудников в xml
            XElement persons = new XElement("persons");
            if (Personal.PersonalList.Count != 0)
            {
                foreach (Personal person in Personal.PersonalList)
                {
                    persons.Add(new XElement("personal", new XElement("Name", person.Name), new XElement("INN", person.INN),
                    new XElement("Address", person.Address), new XElement("Function", person.Function), new XElement("NumberOfTel", person.NumberOfTel)));
                }
            }
            XElement elements = new XElement("elements");
            elements.Add(repairs, cars, clients, persons);
            xdoc.Add(elements);
            xdoc.Save("elements.xml");
        }
        //функция для загрузки xml документа
        public XDocument GetXmlInventory()
        {
            try
            {
                XDocument xDox = XDocument.Load("elements.xml");
                return xDox;
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        //убирает выделение первой строки
        public void DeleteSelectFirstRow()
        {
            if (dataGridView.Rows.Count > 0)
            {
                dataGridView.Rows[0].Cells[0].Selected = false;
            }
        }
        private void SetToolTip(Button buttonType, String text)
        {
            toolTip1.SetToolTip(buttonType, text);
            Point clientCoor = this.PointToClient(new Point(Cursor.Position.X + 20, Cursor.Position.Y + 40));
            toolTip1.Show(text, this, clientCoor);
        }
        private void HideToolTip()
        {
            toolTip1.Hide(this);
        }
        //события при наведении на кнопку
        private void AddInStock_MouseEnter(object sender, EventArgs e)
        {
            SetToolTip(AddInStock, "Добавить новый вид запчастей на склад");
        }
        private void EditStock_MouseEnter(object sender, EventArgs e)
        {
            SetToolTip(AddInStock, "Изменить данные по запчастям");
        }

        private void DeleteFromStock_MouseEnter(object sender, EventArgs e)
        {
            SetToolTip(AddInStock, "Удалить запчасть");
        }

        private void AddInStock_MouseLeave(object sender, EventArgs e)
        {
            HideToolTip();
        }

        private void EditStock_MouseLeave(object sender, EventArgs e)
        {
            HideToolTip();
        }

        private void DeleteFromStock_MouseLeave(object sender, EventArgs e)
        {
            HideToolTip();
        }
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            switch (WindowIndex)
            {
                case ((int)WindowsStruct.Stock):
                    string query;
                    query = $"select * from stock_view where uniq_code LIKE '%{textBoxSearch.Text}%'";
                    using (FbCommand command = new FbCommand(query, Form1.db))
                    {
                        FbDataAdapter dataAdapter = new FbDataAdapter(command);
                        DataSet ds = new DataSet();
                        db.Open();
                        dataAdapter.Fill(ds);
                        dataGridView.DataSource = ds.Tables[0];
                        ds.Tables[0].Columns[0].ColumnName = "Артикул";
                        ds.Tables[0].Columns[1].ColumnName = "Наименование";
                        ds.Tables[0].Columns[2].ColumnName = "Количество";
                        ds.Tables[0].Columns[3].ColumnName = "Cтоимость(руб.)";
                        ds.Tables[0].Columns[4].ColumnName = "Автомобиль";
                        db.Close();
                    }
                    break;
            }
        }
        public static void CreateViewForDataGrid(string query, string[] columnNames, DataGridView dg)
        {
            using (FbCommand command = new FbCommand(query, Form1.db))
            {
                FbDataAdapter dataAdapter = new FbDataAdapter(command);
                DataSet ds = new DataSet();
                db.Open();
                dataAdapter.Fill(ds);
                dg.DataSource = ds.Tables[0];
                for (int i = 0; i < columnNames.Length; i++)
                {
                    ds.Tables[0].Columns[i].ColumnName = columnNames[i];
                }
                db.Close();
            }
        }
    }
}
