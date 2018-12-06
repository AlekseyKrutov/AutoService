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
        //логическая переменная
        public static bool logicParamForRepair = true;
        //перменна для определения добавления или редактирования
        public static int AddOrEdit;
        //структура с названиями окон
        public enum WindowsStruct { Repairs = 1, Auto, ActOfEndsRepairs, Worker, MalfAdd, MalfView,
                                    SpareAdd, SpareView, Stock , Client, WorkerAdd, WorkerView }
        public enum AddEditOrDelete { Add, Edit, Delete };

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
            PaymenInvoiceToolStripMenuItem.Visible = false;
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
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
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
            formAuthorization.ShowDialog();
            DataGridViewForRepairs();
        }
        //событие при клике на законченные ремонты
        private void EndRepairsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectIndex = 0;
            WindowIndex = 0;
            PaymenInvoiceToolStripMenuItem.Visible = true;
            labelHeaderText.Text = "Завершенные ремонты";
            HideRepairButtons();
            HideAutoButtons();
            HideClientButtons();
            HidePersonalButtons();
            HidePriceButtons();
            HidePersonalButtons();
            HideStockButtons();
            HideSearch();
            if (logicParamForRepair == true)
            {
                dataGridView.Top -= resizeCount;
                dataGridView.Height += resizeCount;
            }
            logicParamForRepair = false;
            DataGridViewForRepairs();
        }
        //событие при клике на текущие ремонты
        private void CurrentRepairsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SelectIndex = 0;
            WindowIndex = 0;
            PaymenInvoiceToolStripMenuItem.Visible = false;
            labelHeaderText.Text = "Текущие ремонты";
            ShowRepairButtons();
            HideAutoButtons();
            HideClientButtons();
            HidePersonalButtons();
            HidePriceButtons();
            HidePersonalButtons();
            HideStockButtons();
            HideSearch();
            AddListRepairsInGrid(dataGridView);
            if (!logicParamForRepair)
            {
                labelHeaderText.Top = topForLabelOfHead;
                dataGridView.Top = topForGridIdeal;
            }
            logicParamForRepair = true;
            DataGridViewForRepairs();
            DeleteSelectFirstRow();
        }
        //событие при клике на клиенты
        private void ClientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logicParamForRepair = true;
            SelectIndex = 0;
            PaymenInvoiceToolStripMenuItem.Visible = false;
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
            PaymenInvoiceToolStripMenuItem.Visible = false;
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
            PaymenInvoiceToolStripMenuItem.Visible = false;
            WindowIndex = (int)WindowsStruct.Auto;
            logicParamForRepair = true;
            ShowAutoButtons();
            AddAuto.Location = AddRepair.Location;
            EditAuto.Location = EditRepair.Location;
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
            PaymenInvoiceToolStripMenuItem.Visible = false;
            HideAutoButtons();
            HideRepairButtons();
            HideClientButtons();
            HidePersonalButtons();
            HideStockButtons();
            HideSearch();
            ShowPriceButtons();
            AddPosition.Location = AddRepair.Location;
            EditPosition.Location = EditRepair.Location;
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
            PaymenInvoiceToolStripMenuItem.Visible = false;
            HideAutoButtons();
            HideRepairButtons();
            HideClientButtons();
            HidePersonalButtons();
            HidePriceButtons();
            ShowStockButtons();
            ShowSearch();
            AddInStock.Location = AddRepair.Location;
            EditStock.Location = EditRepair.Location;
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
        }
        private void ShowAutoButtons()
        {
            AddAuto.Visible = true;
            EditAuto.Visible = true;
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
        }
        private void ShowClientButtons()
        {
            AddClient.Visible = true;
            EditClient.Visible = true;
        }
        private void HidePersonalButtons()
        {
            AddPersonal.Hide();
            EditPersonal.Hide();
        }
        private void ShowPersonalButtons()
        {
            AddPersonal.Visible = true;
            EditPersonal.Visible = true;
        }
        private void HidePriceButtons()
        {
            AddPosition.Hide();
            EditPosition.Hide();
        }
        private void ShowPriceButtons()
        {
            AddPosition.Visible = true;
            EditPosition.Visible = true;
        }
        private void HideStockButtons()
        {
            AddInStock.Hide();
            EditStock.Hide();
        }
        private void ShowStockButtons()
        {
            AddInStock.Visible = true;
            EditStock.Visible = true;
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
            AddOrEdit = (int)AddEditOrDelete.Add;
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
        private void EditRepair_Click(object sender, EventArgs e)
        {
            AddOrEdit = (int)AddEditOrDelete.Edit;
            WindowIndex = (int)WindowsStruct.Repairs;
            //проверка на наличие открытой формы
            if (!formAddRepair.Visible)
            {
                string vin = "";
                formAddRepair = new FormAddRepair(formAddAuto, this);
                formAddRepair.id_repair = int.Parse(dataGridView.Rows[SelectIndex].Cells[0].Value.ToString());
                Form1.db.Open();
                using (FbCommand command = new FbCommand("TAKE_ID_CAR_FROM_REPAIR", Form1.db))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@ID_CARD", FbDbType.SmallInt).Value = formAddRepair.id_repair;
                    FbDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        vin = dr.GetString(0);
                        formAddRepair.textBoxNotes.Text = dr.GetString(1);
                    }
                    Form1.db.Close();
                }
                FormAddAuto.ReadAutoFromViewForRepair(vin, formAddRepair);
                formAddRepair.ShowDialog();
            }
            else
                //если окно добавления автомобиля будет открыто выведется окно с предупреждением
                MessageBox.Show("Вы уже создаете новый ремонт, для создания нового ремонта завершите старый.", "", MessageBoxButtons.OK);
            return;
        }
        private void EndRepair_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show(string.Format("Вы действительно завершить ремонт №{0}?",
                dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString()), "Предупреждение",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == DialogResult.OK))
            {
                FinishRepair(int.Parse(dataGridView.Rows[SelectIndex].Cells[0].Value.ToString()));
                AddListRepairsInGrid(dataGridView);
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
                    formAddPersonal.textBoxFirstName.Text = dataReader.GetString(0);
                    formAddPersonal.textBoxSecondName.Text = dataReader.GetString(1);
                    formAddPersonal.textBoxLastName.Text = dataReader.GetString(2);
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
                    AddListMalfunctionsInGrid(dataGridView, Form1.queryForMalfunctions);
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
        public static void AddListRepairsInGrid(DataGridView dataGridView)
        {
            if (logicParamForRepair == true)
            {
                string query = @"select * from active_repairs";
                string[] columnNames = { "№", "Дата начала", "Итоговая стоимость", "Автомобиль", "Заметки" };
                CreateViewForDataGrid(query, columnNames, dataGridView);
            }
            else
            {
                string query = @"select * from finished_repairs";
                string[] columnNames = { "№", "Дата начала", "Дата окончания", "Итоговая стоимость", "Автомобиль", "Заметки" };
                CreateViewForDataGrid(query, columnNames, dataGridView);
            }
        }
        public static void AddListAutoInGrid(DataGridView dataGridView)
        {
            string query = @"select * from cars_view";
            string[] columnNames = { "VIN", "Марка", "Гос.номер", "Свидетельство о рег.", "Владелец" };
            CreateViewForDataGrid(query, columnNames,dataGridView);
        }
        public static void AddListClientInGrid(DataGridView dataGridView)
        {
            string query = @"select * from client_view";
            string[] columnNames = { "Наименование", "Директор", "ИНН", "Номер телефона" };
            CreateViewForDataGrid(query, columnNames, dataGridView);
        }
        public static string queryForStaff = @"select * from staff_view";
        public static void AddListPersonalInGrid(DataGridView dataGridView, string query)
        {
            string[] columnNames = { "Табельный номер", "ФИО", "Адрес", "Должность", "Номер телефона" };
            CreateViewForDataGrid(query, columnNames, dataGridView);
        }
        public static string queryForMalfunctions = @"select * from type_of_work_view";
        public static void AddListMalfunctionsInGrid(DataGridView dataGridView, string query)
        {
            string[] columnNames = { "Наименование", "Единица измерения", "Стоимость(руб.)", "Количество" };
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
        public static string queryForSparePart = @"select * from stock_view";
        public static void AddSparePartInStock(DataGridView dataGridView, string query)
        {
            string[] columnNames = { "Артикул", "Наименование", "Количество", "Cтоимость(руб.)", "Автомобиль" };
            CreateViewForDataGrid(query, columnNames, dataGridView);
        }
        //функции для редактирования сетки 
        private void DataGridViewForRepairs()
        {
            dataGridView.Columns.Clear();
            AddListRepairsInGrid(dataGridView);

        }
        private void DataGridViewForClient()
        {
            dataGridView.Columns.Clear();
            AddListClientInGrid(dataGridView);
        }
        private void DataGridViewForPersonal()
        {
            dataGridView.Columns.Clear();
            AddListPersonalInGrid(dataGridView, Form1.queryForStaff);
        }
        private void DataGridViewForAuto()
        {
            dataGridView.Columns.Clear();
            AddListAutoInGrid(dataGridView);
        }
        private void DataGridViewForPrice()
        {
            dataGridView.Columns.Clear();
            AddListMalfunctionsInGrid(dataGridView, queryForMalfunctions);
        }
        private void DataGridViewForStock()
        {
            dataGridView.Columns.Clear();
            AddSparePartInStock(dataGridView, queryForSparePart);
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
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    ds.Tables[0].Columns[i].ColumnName = columnNames[i];
                }
                db.Close();
            }
            dg.ClearSelection();
        }
        public void FinishRepair(int id_card)
        {
            Form1.db.Open();
            using (FbTransaction trn = Form1.db.BeginTransaction())
            {
                FbCommand command = new FbCommand("FINISH_REPAIR", Form1.db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_CARD", FbDbType.SmallInt).Value = id_card;
                command.Parameters.Add("@FINISH_DATE", FbDbType.TimeStamp).Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (FbException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                trn.Commit();
                Form1.db.Close();
            }
        }
    }
}
