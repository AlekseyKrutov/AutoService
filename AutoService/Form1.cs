using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;
using AutoServiceLibrary;
using System.Configuration;
using WorkWithExcelLibrary;
using DbProxy;

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
        //переменная в которую записывается номер ремонта в зав. ремонтах
        public int SelectedIndEndRep;
        //переменная для определения окна с которым мы работаем
        static public WindowsStruct WindowIndex = 0;
        //логическая переменная
        public static bool logicParamForRepair = true;
        //перменна для определения добавления или редактирования
        public static AddEditOrDelete AddOrEdit;
        //структура с названиями окон
        public enum WindowsStruct { Nothing, Repairs, Auto, AddAutoInRep, ViewAutoInRep, ActOfEndsRepairs, Worker, MalfAdd, MalfView,
                                    SpareAdd, SpareView, Stock , Client, WorkerAdd, WorkerView, Price, WayBill }
        public enum AddEditOrDelete { Add, Edit, Delete };

        //конструктор формы
        public Form1()
        {
            InitializeComponent();
            topForGridIdeal = dataGridView.Top;
            topForLabelOfHead = labelHeaderText.Top;
            dataGridView.MultiSelect = false;
            SeeFinishedRepair.Click += new EventHandler(EditRepair_Click);
            UnloadAllToolStripMenuItem.Click += new EventHandler(UploadAllToolStripMenuItem_Click);
            UnldBillToolStripMenuItem.Click += new EventHandler(PaymenInvoiceToolStripMenuItem_Click);
            UnldActToolStripMenuItem.Click += new EventHandler(FinishActToolStripMenu_Click);
            UnldOrderToolStripMenuItem.Click += new EventHandler(OrderToolStripMenuItem_Click);
        }
        //события загрузки формы
        private void Form1_Load(object sender, EventArgs e)
        {
            Padding padding = new Padding(0, 8, 0, 8);
            WindowIndex = WindowsStruct.Repairs;
            HideToolStripForRepair();
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView.RowTemplate.DefaultCellStyle.Padding = padding;
            HideFinishedRepBtns();
            HideAutoButtons();
            HideClientButtons();
            HidePersonalButtons();
            HidePriceButtons();
            HideStockButtons();
            HideWayBillButtons();
            dataGridView.AllowUserToAddRows = false;
            dataGridView.ClearSelection();
            dataGridView.RowHeadersVisible = false;
            labelHeaderText.Text = "Текущие ремонты";
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            db = new FbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            //formAuthorization = new FormAuthorization(this);
            //formAuthorization.ShowDialog();
            DataGridViewForRepairs();
        }
        //событие при клике на законченные ремонты
        private void EndRepairsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WindowIndex == WindowsStruct.ActOfEndsRepairs)
                return;
            SelectIndex = 0;
            WindowIndex = WindowsStruct.ActOfEndsRepairs;
            ShowToolStripForRepair();
            labelHeaderText.Text = "Завершенные ремонты";
            ShowFinishedRepBtns();
            ShowSearch();
            HideRepairButtons();
            HideAutoButtons();
            HideClientButtons();
            HidePriceButtons();
            HidePersonalButtons();
            HideStockButtons();
            HideWayBillButtons();
            SeeFinishedRepair.Location = AddRepair.Location;
            StartFinishedRepair.Location = EditRepair.Location;
            DataGridViewForFinishedReps();
        }
        //событие при клике на текущие ремонты
        private void CurrentRepairsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            WindowIndex = WindowsStruct.Repairs;
            if (WindowIndex != WindowsStruct.ActOfEndsRepairs)
                SelectIndex = 0;
            HideToolStripForRepair();
            labelHeaderText.Text = "Текущие ремонты";
            ShowRepairButtons();
            ShowSearch();
            HideFinishedRepBtns();
            HideAutoButtons();
            HideClientButtons();
            HidePersonalButtons();
            HidePriceButtons();
            HidePersonalButtons();
            HideStockButtons();
            HideWayBillButtons();
            AddListRepairsInGrid(dataGridView);
            DataGridViewForRepairs();
            DeleteSelectFirstRow();
        }
        //событие при клике на клиенты
        private void ClientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logicParamForRepair = true;
            SelectIndex = 0;
            HideToolStripForRepair();
            WindowIndex = WindowsStruct.Client;
            HideFinishedRepBtns();
            HideAutoButtons();
            HideRepairButtons();
            HidePersonalButtons();
            HidePriceButtons();
            ShowClientButtons();
            HideStockButtons();
            HideWayBillButtons();
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
            HideToolStripForRepair();
            WindowIndex = WindowsStruct.Worker;
            HideFinishedRepBtns();
            HideAutoButtons();
            HideRepairButtons();
            HideClientButtons();
            HidePriceButtons();
            HideStockButtons();
            HideWayBillButtons();
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
            HideToolStripForRepair();
            WindowIndex = WindowsStruct.Auto;
            logicParamForRepair = true;
            ShowAutoButtons();
            AddAuto.Location = AddRepair.Location;
            EditAuto.Location = EditRepair.Location;
            HideFinishedRepBtns();
            HideRepairButtons();
            HideClientButtons();
            HidePersonalButtons();
            HidePriceButtons();
            HideStockButtons();
            HideWayBillButtons();
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
            WindowIndex = WindowsStruct.Price;
            HideToolStripForRepair();
            HideFinishedRepBtns();
            HideAutoButtons();
            HideRepairButtons();
            HideClientButtons();
            HidePersonalButtons();
            HideStockButtons();
            HideWayBillButtons();
            ShowSearch();
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
            WindowIndex = WindowsStruct.Stock;
            logicParamForRepair = true;
            SelectIndex = 0;
            HideToolStripForRepair();
            HideFinishedRepBtns();
            HideAutoButtons();
            HideRepairButtons();
            HideClientButtons();
            HidePersonalButtons();
            HidePriceButtons();
            ShowStockButtons();
            HideWayBillButtons();
            ShowSearch();
            AddInStock.Location = AddRepair.Location;
            EditStock.Location = EditRepair.Location;
            labelHeaderText.Text = "Склад";
            dataGridView.Top = topForGridIdeal;
            labelHeaderText.Top = topForLabelOfHead;
            DataGridViewForStock();
            DeleteSelectFirstRow();
        }
        private void WayBillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowIndex = WindowsStruct.WayBill;
            logicParamForRepair = true;
            SelectIndex = 0;
            HideToolStripForRepair();
            HideFinishedRepBtns();
            HideAutoButtons();
            HideRepairButtons();
            HideClientButtons();
            HidePersonalButtons();
            HidePriceButtons();
            HideStockButtons();
            ShowWayBillButtons();
            ShowSearch();
            AddWayBill.Location = AddRepair.Location;
            EditWayBill.Location = EditRepair.Location;
            DeleteWayBill.Location = EndRepair.Location;
            labelHeaderText.Text = "Перевозки";
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
                if (WindowIndex == WindowsStruct.ActOfEndsRepairs)
                {
                    SelectedIndEndRep = int.Parse(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
                }
                if (e.RowIndex == -1)
                    SelectIndex = 0;
                else
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
            if (e.Button == MouseButtons.Right)
                return;
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
        public void ShowFinishedRepBtns()
        {
            SeeFinishedRepair.Visible = true;
            StartFinishedRepair.Visible = true;
        }
        public void HideFinishedRepBtns()
        {
            SeeFinishedRepair.Hide();
            StartFinishedRepair.Hide();
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
        private void ShowWayBillButtons()
        {
            AddWayBill.Visible = true;
            EditWayBill.Visible = true;
            DeleteWayBill.Visible = true;
        }
        private void HideWayBillButtons()
        {
            AddWayBill.Hide();
            EditWayBill.Hide();
            DeleteWayBill.Hide();
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
        private void ShowToolStripForRepair()
        {
            PaymenInvoiceToolStripMenuItem.Visible = true;
            FinishActToolStripMenu.Visible = true;
            OrderToolStripMenuItem.Visible = true;
            UploadAllToolStripMenuItem.Visible = true;
        }
        private void HideToolStripForRepair()
        {
            PaymenInvoiceToolStripMenuItem.Visible = false;
            FinishActToolStripMenu.Visible = false;
            OrderToolStripMenuItem.Visible = false;
            UploadAllToolStripMenuItem.Visible = false;
        }
        //событие при клике по кнопке добавить ремонт
        private void AddRepair_Click(object sender, EventArgs e)
        {
            FormAddRepair.addOrEditInRepair = (int)AddEditOrDelete.Add;
            AddOrEdit = AddEditOrDelete.Add;
            WindowIndex = WindowsStruct.Repairs;
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
            if (GridRowsColumnIsNull())
                return;
            FormAddRepair.addOrEditInRepair = (int)AddEditOrDelete.Edit;
            AddOrEdit = AddEditOrDelete.Edit;
            if (WindowIndex != WindowsStruct.ActOfEndsRepairs)
                WindowIndex = WindowsStruct.Repairs;
            //проверка на наличие открытой формы
            if (!formAddRepair.Visible)
            {
                string state_numb = "";
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
                        state_numb = dr.GetString(dr.GetOrdinal("STATE_NUMBER"));
                        formAddRepair.textBoxNotes.Text = dr.GetString(dr.GetOrdinal("NOTES"));
                        formAddRepair.dateTimeStart.Value = dr.GetDateTime(dr.GetOrdinal("START_DATE"));
                        string str = dr.GetString(dr.GetOrdinal("FINISH_DATE"));
                        DateTime finishDate;
                        if (DateTime.TryParse(str, out finishDate))
                        {
                            formAddRepair.dateTimeFinish.Value = finishDate;
                            formAddRepair.checkBoxTurnTime.Checked = true;
                        }

                    }
                    Form1.db.Close();
                }
                FormAddAuto.ReadAutoFromViewForRepair(state_numb, formAddRepair);
                formAddRepair.ShowDialog();
            }
            else
                //если окно добавления автомобиля будет открыто выведется окно с предупреждением
                MessageBox.Show("Вы уже создаете новый ремонт, для создания нового ремонта завершите старый.", "", MessageBoxButtons.OK);
            return;
        }
        private void StartFinishedRepair_Click(object sender, EventArgs e)
        {
            if (GridRowsColumnIsNull())
                return;
            string rep_id = dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString();
            bool confirm = ((MessageBox.Show(string.Format("Вы действительно хотите восстановить ремонт №{0}?",
                dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString()), "Предупреждение",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == DialogResult.OK));
            if (confirm)
                DbProxy.InvokeProcedure.StartRepair(int.Parse(dataGridView.Rows[SelectIndex].Cells[0].Value.ToString()));
            AddListFinishedRepsInGrid(dataGridView);
        }
        private void EndRepair_Click(object sender, EventArgs e)
        {
            if (GridRowsColumnIsNull())
                return;
            if ((MessageBox.Show(string.Format("Вы действительно завершить ремонт №{0}?",
                dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString()), "Предупреждение",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == DialogResult.OK))
            {
                DbProxy.InvokeProcedure.FinishRepair(int.Parse(dataGridView.Rows[SelectIndex].Cells[0].Value.ToString()));
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
            if (GridRowsColumnIsNull())
                return;
            AddOrEdit = AddEditOrDelete.Edit;
            if (!formAddAuto.Visible)
            {
                formAddAuto = new FormAddAuto(this);
                formAddAuto.OwnerSelected = true;
                string query = string.Format("select * from cars_view where state_number like '{0}'", 
                    dataGridView.Rows[SelectIndex].Cells[2].Value.ToString());
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
            if (GridRowsColumnIsNull())
                return;
            AddOrEdit = AddEditOrDelete.Edit;
            if (!formAddClient.Visible)
            {
                formAddClient = new FormAddClient(this);
                AddBankInComBoxClient();
                string query = Queries.GetClientByClientName(dataGridView.Rows[SelectIndex].Cells[0].Value.ToString());
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
            if (GridRowsColumnIsNull())
                return;
            AddOrEdit = AddEditOrDelete.Edit;
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
        private void EditPosition_Click(object sender, EventArgs e)
        {
            if (GridRowsColumnIsNull())
                return;
            AddOrEdit = AddEditOrDelete.Edit;
            formAddPrice = new FormAddPrice(this);
            FillFormPrice(dataGridView, db, formAddPrice);
        }
        public void FillFormPrice(DataGridView dataGridView, FbConnection db, FormAddPrice formAddPrice)
        {
            string query = $" select description, unit, cost from type_of_work where description LIKE " +
                $"'{dataGridView.Rows[SelectIndex].Cells[0].Value.ToString()}'";
            using (FbCommand command = new FbCommand(query, db))
            {
                FbDataReader dr;
                db.Open();
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    formAddPrice.textBoxDescription.Text = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                    formAddPrice.comboBoxUnit.SelectedItem = dr.GetString(dr.GetOrdinal("UNIT"));
                    formAddPrice.textBoxPrice.Text = dr.GetDouble(dr.GetOrdinal("COST")).ToString();
                }
                db.Close();
            }
            formAddPrice.ShowDialog();
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
                    AddListMalfunctionsInGrid(dataGridView, Queries.MalfunctionsView);
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
            if (GridRowsColumnIsNull())
                return;
            AddOrEdit = AddEditOrDelete.Edit;
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
        private void AddWayBill_Click(object sender, EventArgs e)
        {
            FormAddWayBill formAddWayBill = new FormAddWayBill();
            formAddWayBill.ShowDialog();
        }
        //функции для добавления списка объектов в сетку
        public static void AddListRepairsInGrid(DataGridView dataGridView, string content = "")
        {
            string[] columnNames = { "№", "Дата начала", "Итоговая стоимость", "Заказчик", "Автомобиль", "Заметки" };
            DbProxy.DataSets.CreateDSForDataGrid(columnNames, dataGridView, content);
        }
        public static void AddListFinishedRepsInGrid(DataGridView dataGridView, string content= "")
        {
            string[] columnNames = { "№", "Дата начала", "Дата окончания", "Итоговая стоимость", "Заказчик", "Автомобиль", "Заметки" };
            DbProxy.DataSets.CreateDSForDataGrid(columnNames, dataGridView, content);
        }
        public static void AddListAutoInGrid(DataGridView dataGridView)
        {
            string[] columnNames = { "VIN", "Марка", "Гос.номер", "Свидетельство о рег.", "Владелец" };
            DbProxy.DataSets.CreateDSForDataGrid(columnNames,dataGridView);
        }
        public static void AddListClientInGrid(DataGridView dataGridView)
        {
            string[] columnNames = { "Наименование", "Директор", "ИНН", "Номер телефона" };
            DbProxy.DataSets.CreateDSForDataGrid(columnNames, dataGridView);
        }
        public static void AddListPersonalInGrid(DataGridView dataGridView, string content = "")
        {
            string[] columnNames = { "Табельный номер", "ФИО", "Адрес", "Должность", "Номер телефона" };
            DbProxy.DataSets.CreateDSForDataGrid(columnNames, dataGridView, content);
        }
        public static void AddListMalfunctionsInGrid(DataGridView dataGridView, string content = "")
        {
            string[] columnNames = { "Наименование", "Единица измерения", "Стоимость(руб.)", "Количество" };
            DbProxy.DataSets.CreateDSForDataGrid(columnNames, dataGridView, content);
        }
        public static void AddSparePartInStock(DataGridView dataGridView, string content = "")
        {
            string[] columnNames = { "Артикул", "Наименование", "Количество", "Cтоимость(руб.)", "Автомобиль" };
            DbProxy.DataSets.CreateDSForDataGrid(columnNames, dataGridView, content);
        }
        //функции для редактирования сетки 
        private void DataGridViewForRepairs()
        {
            dataGridView.Columns.Clear();
            AddListRepairsInGrid(dataGridView);
        }
        private void DataGridViewForFinishedReps()
        {
            dataGridView.Columns.Clear();
            AddListFinishedRepsInGrid(dataGridView);
        }
        private void DataGridViewForClient()
        {
            dataGridView.Columns.Clear();
            AddListClientInGrid(dataGridView);
        }
        private void DataGridViewForPersonal()
        {
            dataGridView.Columns.Clear();
            AddListPersonalInGrid(dataGridView, Queries.StaffView);
        }
        private void DataGridViewForAuto()
        {
            dataGridView.Columns.Clear();
            AddListAutoInGrid(dataGridView);
        }
        private void DataGridViewForPrice()
        {
            dataGridView.Columns.Clear();
            AddListMalfunctionsInGrid(dataGridView, Queries.MalfunctionsView);
        }
        private void DataGridViewForStock()
        {
            dataGridView.Columns.Clear();
            AddSparePartInStock(dataGridView, Queries.SparesView);
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
            string query;
            switch (WindowIndex)
            {
                case (WindowsStruct.ActOfEndsRepairs):
                    AddListFinishedRepsInGrid(dataGridView, textBoxSearch.Text);
                    break;
                case (WindowsStruct.Repairs):
                    AddListRepairsInGrid(dataGridView, textBoxSearch.Text);
                    break;
                case (WindowsStruct.Stock):
                    query = $"select * from stock_view where uniq_code LIKE " +
                        $"'%{((int.TryParse(textBoxSearch.Text, out int j)) ? int.Parse(textBoxSearch.Text) : 0)}%'";
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
                case (WindowsStruct.Price):
                    query = $"select * from type_of_work_view where upper(description) " +
                        $"LIKE '%{textBoxSearch.Text.ToUpper()}%'";
                    AddListMalfunctionsInGrid(dataGridView, query);
                    break;
            }
        }
        private void AddRepair_MouseEnter(object sender, EventArgs e)
        {
            SetToolTip(AddRepair, "Добавить ремонт");
        }

        private void AddRepair_MouseLeave(object sender, EventArgs e)
        {
            HideToolTip();
        }

        private void EditRepair_MouseEnter(object sender, EventArgs e)
        {
            SetToolTip(AddRepair, "Изменить данные по ремонту");
        }

        private void EditRepair_MouseLeave(object sender, EventArgs e)
        {
            HideToolTip();
        }

        private void AddAuto_MouseEnter(object sender, EventArgs e)
        {
            SetToolTip(AddAuto, "Добавить автомобиль");
        }

        private void AddAuto_MouseLeave(object sender, EventArgs e)
        {
            HideToolTip();
        }

        private void AddClient_MouseEnter(object sender, EventArgs e)
        {
            SetToolTip(AddClient, "Добавить клиента");
        }

        private void EditClient_MouseLeave(object sender, EventArgs e)
        {
            HideToolTip();
        }

        private void AddPosition_MouseEnter(object sender, EventArgs e)
        {
            SetToolTip(AddPosition, "Добавить позицию");
        }

        private void AddPosition_MouseLeave(object sender, EventArgs e)
        {
            HideToolTip();
        }
        private void EditPosition_MouseEnter(object sender, EventArgs e)
        {
            SetToolTip(AddPosition, "Изменить данные по позиции");
        }

        private void EditPosition_MouseLeave(object sender, EventArgs e)
        {
            HideToolTip();
        }

        private void EditAuto_MouseEnter(object sender, EventArgs e)
        {
            SetToolTip(EditAuto, "Изменить данные по автомобилю");
        }

        private void EditAuto_MouseLeave(object sender, EventArgs e)
        {
            HideToolTip();
        }

        private void AddClient_MouseLeave(object sender, EventArgs e)
        {
            SetToolTip(AddClient, "Добавить клиента");
        }

        private void EditClient_MouseEnter(object sender, EventArgs e)
        {
            SetToolTip(EditClient, "Изменить данные по клиенту");
        }

        private void AddPersonal_MouseEnter(object sender, EventArgs e)
        {
            SetToolTip(AddPersonal, "Добавить персонал");
        }

        private void AddPersonal_MouseLeave(object sender, EventArgs e)
        {
            HideToolTip();
        }

        private void EditPersonal_MouseEnter(object sender, EventArgs e)
        {
            SetToolTip(EditPersonal, "Изменить данные по работнику");
        }

        private void EditPersonal_MouseLeave(object sender, EventArgs e)
        {
            HideToolTip();
        }

        private void EndRepair_MouseEnter(object sender, EventArgs e)
        {
            SetToolTip(EndRepair, "Завершить ремонт");
        }

        private void EndRepair_MouseLeave(object sender, EventArgs e)
        {
            HideToolTip();
        }
        

        private void PaymenInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WorkWithExcel.MakeBillInExcelAsync(SelectedIndEndRep, db);
        }

        private void FinishActToolStripMenu_Click(object sender, EventArgs e)
        {
            WorkWithExcel.MakeActOfWorkInExcelAsync(SelectedIndEndRep, db);
        }
        private void OrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WorkWithExcel.MakeOrderInExcelAsync(SelectedIndEndRep, db);
        }
        private void UploadAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WorkWithExcel.UploadAllDocsInExcAsync(SelectedIndEndRep, db);
        }
        public bool GridRowsColumnIsNull()
        {
            if (dataGridView.Rows.Count == 0)
                return true;
            else
                return false;
        }

        private void dataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && WindowIndex == WindowsStruct.ActOfEndsRepairs
                && dataGridView.SelectedRows.Count > 0)
            {
                contMenuStripDataGrid.Show(Cursor.Position);
            }
        }
    }
}
