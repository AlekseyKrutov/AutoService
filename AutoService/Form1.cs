using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;
using AutoServiceLibrary;
using System.Configuration;
using WorkWithExcelLibrary;
using DbProxy;
using DataMapper;

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

        //конструктор формы
        public Form1()
        {
            InitializeComponent();
            topForGridIdeal = dataGridView.Top;
            topForLabelOfHead = labelHeaderText.Top;
            dataGridView.MultiSelect = false;
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
            HideFinishedWayBillBtns();
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
            MakeDataGridReadOnly();
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
            HideFinishedWayBillBtns();
            SeeFinishedRepair.Location = AddRepair.Location;
            StartFinishedRepair.Location = EditRepair.Location;
            DataGridViewForFinishedReps();
            MakeDataGridReadOnly();
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
            HideFinishedWayBillBtns();
            AddListRepairsInGrid(dataGridView);
            DataGridViewForRepairs();
            DeleteSelectFirstRow();
            MakeDataGridReadOnly();
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
            HideFinishedWayBillBtns();
            HideSearch();
            AddClient.Location = AddRepair.Location;
            EditClient.Location = EditRepair.Location;
            labelHeaderText.Text = "Клиенты";
            dataGridView.Top = topForGridIdeal;
            labelHeaderText.Top = topForLabelOfHead;
            DataGridViewForClient();
            DeleteSelectFirstRow();
            MakeDataGridReadOnly();
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
            HideFinishedWayBillBtns();
            HideSearch();
            ShowPersonalButtons();
            AddPersonal.Location = AddRepair.Location;
            EditPersonal.Location = EditRepair.Location;
            labelHeaderText.Text = "Сотрудники";
            dataGridView.Top = topForGridIdeal;
            labelHeaderText.Top = topForLabelOfHead;
            DataGridViewForPersonal();
            DeleteSelectFirstRow();
            MakeDataGridReadOnly();
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
            HideFinishedWayBillBtns();
            HideSearch();
            dataGridView.Top = topForGridIdeal;
            labelHeaderText.Text = "Автомобили";
            labelHeaderText.Top = topForLabelOfHead;
            DataGridViewForAuto();
            DeleteSelectFirstRow();
            MakeDataGridReadOnly();
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
            HideFinishedWayBillBtns();
            ShowSearch();
            ShowPriceButtons();
            AddPosition.Location = AddRepair.Location;
            EditPosition.Location = EditRepair.Location;
            labelHeaderText.Text = "Прайс";
            dataGridView.Top = topForGridIdeal;
            labelHeaderText.Top = topForLabelOfHead;
            DataGridViewForPrice();
            DeleteSelectFirstRow();
            MakeDataGridReadOnly();
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
            HideFinishedWayBillBtns();
            ShowSearch();
            AddInStock.Location = AddRepair.Location;
            EditStock.Location = EditRepair.Location;
            PushInStock.Location = EndRepair.Location;
            PopInStock.Location = new Point(PushInStock.Location.X + PopInStock.Width + 5, PushInStock.Location.Y);
            labelHeaderText.Text = "Склад";
            dataGridView.Top = topForGridIdeal;
            labelHeaderText.Top = topForLabelOfHead;
            DataGridViewForStock();
            DeleteSelectFirstRow();
            MakeDataGridReadOnly();
        }
        private void WayBillToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void CurrentWayBillToolStrip_Click(object sender, EventArgs e)
        {
            WindowIndex = WindowsStruct.ActiveWayBills;
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
            HideFinishedWayBillBtns();
            ShowWayBillButtons();
            ShowSearch();
            AddWayBill.Location = AddRepair.Location;
            EditWayBill.Location = EditRepair.Location;
            DeleteWayBill.Location = EndRepair.Location;
            labelHeaderText.Text = "Текущие перевозки";
            dataGridView.Top = topForGridIdeal;
            labelHeaderText.Top = topForLabelOfHead;
            DataSets.CreateDSForDataGrid(WindowIndex, dataGridView);
            DeleteSelectFirstRow();
            MakeDataGridReadOnly();
        }
        private void FinishedWayBillToolStrip_Click(object sender, EventArgs e)
        {
            WindowIndex = WindowsStruct.FinishedWayBills;
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
            HideWayBillButtons();
            ShowFinishedWayBillBtns();
            ShowSearch();
            SeeFinishedWayBill.Location = AddRepair.Location;
            StartFinishedWayBill.Location = EditRepair.Location;
            labelHeaderText.Text = "Завершенные перевозки";
            dataGridView.Top = topForGridIdeal;
            labelHeaderText.Top = topForLabelOfHead;
            DataSets.CreateDSForDataGrid(WindowIndex, dataGridView);
            DeleteSelectFirstRow();
            MakeDataGridReadOnly();
        }
        private void repairReportToolStrip_Click(object sender, EventArgs e)
        {
            WindowIndex = WindowsStruct.RepairsReport;
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
            HideWayBillButtons();
            HideWayBillButtons();
            ShowSearch();
            SeeFinishedWayBill.Hide();
            SeeFinishedRepair.Visible = true;
            SeeFinishedRepair.Location = AddRepair.Location;
            labelHeaderText.Text = "Отчет по ремонтам";
            dataGridView.Top = topForGridIdeal;
            labelHeaderText.Top = topForLabelOfHead;
            DataSets.CreateDSForDataGrid(WindowIndex, dataGridView);
            DeleteSelectFirstRow();
            MakeDataGridForReports();
            ColourDataGrid();
        }

        private void wayBillReportToolStrip_Click(object sender, EventArgs e)
        {
            WindowIndex = WindowsStruct.WayBillsReport;
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
            HideWayBillButtons();
            HideWayBillButtons();
            ShowSearch();
            SeeFinishedRepair.Hide();
            SeeFinishedWayBill.Visible = true;
            SeeFinishedWayBill.Location = AddRepair.Location;
            labelHeaderText.Text = "Отчет по грузоперевозкам";
            dataGridView.Top = topForGridIdeal;
            labelHeaderText.Top = topForLabelOfHead;
            DataSets.CreateDSForDataGrid(WindowIndex, dataGridView);
            DeleteSelectFirstRow();
            MakeDataGridForReports();
            ColourDataGrid();
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
            PopInStock.Hide();
            PushInStock.Hide();
        }
        private void ShowStockButtons()
        {
            AddInStock.Visible = true;
            EditStock.Visible = true;
            PopInStock.Visible = true;
            PushInStock.Visible = true;
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
        private void ShowFinishedWayBillBtns()
        {
            SeeFinishedWayBill.Visible = true;
            StartFinishedWayBill.Visible = true;
        }
        private void HideFinishedWayBillBtns()
        {
            SeeFinishedWayBill.Hide();
            StartFinishedWayBill.Hide();
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
            formAddRepair = new FormAddRepair(formAddAuto, this);
            formAddRepair.repair = new CardOfRepair();
            formAddRepair.ShowDialog();
        }
        private void EditRepair_Click(object sender, EventArgs e)
        {
            if (GridRowsColumnIsNull())
                return;
            FormAddRepair.addOrEditInRepair = AddEditOrDelete.Edit;
            AddOrEdit = AddEditOrDelete.Edit;
            if (WindowIndex != WindowsStruct.ActOfEndsRepairs)
                WindowIndex = WindowsStruct.Repairs;
            //проверка на наличие открытой формы
            if (!formAddRepair.Visible)
            {
                if (WindowIndex == WindowsStruct.ActOfEndsRepairs)
                {
                    formAddRepair = new FormAddRepair();
                    formAddRepair.repair = new CardMapper().Get(dataGridView.Rows[SelectIndex].Cells[0].Value.ToString());
                    formAddRepair.textBoxInf.Text = formAddRepair.repair.ToString();
                }
                formAddRepair = new FormAddRepair(formAddAuto, this);
                formAddRepair.id_repair = int.Parse(dataGridView.Rows[SelectIndex].Cells[0].Value.ToString());
                formAddRepair.repair = new CardMapper().Get(dataGridView.Rows[SelectIndex].Cells[0].Value.ToString());
                formAddRepair.textBoxInf.Text = formAddRepair.repair.ToString();
                formAddRepair.textBoxNotes.Text = formAddRepair.repair.Notes;
                formAddRepair.dateTimeStart.Value = formAddRepair.repair.TimeOfStart;
                if (formAddRepair.repair.TimeOfFinish != null)
                {
                    formAddRepair.dateTimeFinish.Value = (DateTime)formAddRepair.repair.TimeOfFinish;
                    formAddRepair.dateTimeFinish.Checked = true;
                }
                else
                    formAddRepair.dateTimeFinish.Checked = false;
                FillCardWithCar(formAddRepair, formAddRepair.repair.Car);
                formAddRepair.ShowDialog();
            }
            else
                //если окно добавления автомобиля будет открыто выведется окно с предупреждением
                MessageBox.Show("Вы уже создаете новый ремонт, для создания нового ремонта завершите старый.", "", MessageBoxButtons.OK);
            return;
        }
        public void FillCardWithCar(FormAddRepair formAddRepair, Car car)
        {
            formAddRepair.textBoxVIN.Text = car.CarVIN;
            string model = (car.Model == null) ? "" : car.Model;
            formAddRepair.textBoxMark.Text = car.Mark + " " + model;
            formAddRepair.textBoxGosNom.Text = car.Number;
            formAddRepair.textBoxReg.Text = car.RegCertific;
            formAddRepair.textBoxOwner.Text = (car.Owner == null) ? "" : car.Owner.Name;
        }
        private void StartFinishedRepair_Click(object sender, EventArgs e)
        {
            if (GridRowsColumnIsNull())
                return;
            bool confirm = ((MessageBox.Show(string.Format("Вы действительно хотите восстановить ремонт №{0}?",
                dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString()), "Предупреждение",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK));
            if (confirm)
            {
                CardMapper cm = new CardMapper();
                CardOfRepair card = cm.Get(dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString());
                card.ActivateRepair();
                cm.Update(card);
            }
            AddListFinishedRepsInGrid(dataGridView);
        }
        private void SeeFinishedRepair_Click(object sender, EventArgs e)
        {
            if (GridRowsColumnIsNull())
                return;
            CardMapper cm = new CardMapper();
            CardOfRepair cr = cm.Get(dataGridView.Rows[SelectIndex].Cells[0].Value.ToString());
            FormInformation formInformation = new FormInformation();
            formInformation.card = cr;
            formInformation.textBoxInf.Text = cr.ToString();
            formInformation.ShowDialog();
        }
        private void EndRepair_Click(object sender, EventArgs e)
        {
            if (GridRowsColumnIsNull())
                return;
            if ((MessageBox.Show(string.Format("Вы действительно завершить ремонт №{0}?",
                dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString()), "Предупреждение",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
            {
                CardMapper cm = new CardMapper();
                CardOfRepair card = cm.Get(dataGridView.Rows[SelectIndex].Cells[0].Value.ToString());
                if (card.TimeOfFinish == null)
                    card.TimeOfFinish = DateTime.Now;
                card.FinishRepair();
                cm.Update(card);
                AddListRepairsInGrid(dataGridView);
            }
        }
        //событие при клике по кнопке добавить автомобиль
        private void AddAuto_Click(object sender, EventArgs e)
        {
            AddOrEdit = AddEditOrDelete.Add;
            formAddAuto = new FormAddAuto(formAddRepair, this);
            formAddAuto.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            formAddAuto.MaximizeBox = false;
            formAddAuto.ShowDialog();
        }
        private void EditAuto_Click(object sender, EventArgs e)
        {
            if (GridRowsColumnIsNull())
                return;
            AddOrEdit = AddEditOrDelete.Edit;
            formAddAuto = new FormAddAuto(this);
            formAddAuto.car = new CarMapper().Get(dataGridView.Rows[SelectIndex].Cells[0].Value.ToString());
            formAddAuto.textBoxGosNumb.Text = formAddAuto.car.Number;
            formAddAuto.textBoxVIN.Text = formAddAuto.car.CarVIN;
            formAddAuto.textBoxReg.Text = formAddAuto.car.RegCertific;
            string model = (formAddAuto.car.Model == "") ? formAddAuto.car.Mark :
                formAddAuto.car.Mark + ' ' + formAddAuto.car.Model;
            formAddAuto.comboBoxAuto.Text = model;
            formAddAuto.labelContentOwner.Text = (formAddAuto.car.Owner != null ) ? formAddAuto.car.Owner.Name : "";
            formAddAuto.client = formAddAuto.car.Owner;
            formAddAuto.Show();
        }
        //событие при клике по кнопке добавить клиента
        private void AddClient_Click(object sender, EventArgs e)
        {
            AddOrEdit = (int)AddEditOrDelete.Add;
            if (!formAddClient.Visible)
            {
                formAddClient = new FormAddClient(this);
                DataSets.CreateDsForComboBox(formAddClient.comboBoxBank, Queries.BankView,
                                            "name_bank", "kor_bill", AddOrEdit);
                formAddClient.ShowDialog();
            }
        }
        
        //событие при клике по кнопке удалить клиента
        private void EditClient_Click(object sender, EventArgs e)
        {
            if (GridRowsColumnIsNull())
                return;
            AddOrEdit = AddEditOrDelete.Edit;
            formAddClient = new FormAddClient(this);
            formAddClient.client = new ClientMapper().Get(dataGridView.Rows[SelectIndex].Cells[0].Value.ToString());
            formAddClient.textBoxINN.Text = formAddClient.client.INN;
            formAddClient.textBoxName.Text = formAddClient.client.Name;
            formAddClient.textBoxDirector.Text = formAddClient.client.Director;
            formAddClient.comboBoxBank.SelectedValue = (formAddClient.client.Bank.KorBill != null) ? formAddClient.client.Bank.KorBill : "";
            formAddClient.textBoxNumbOfTel.Text = formAddClient.client.PhoneNumber;
            formAddClient.textBoxBill.Text = formAddClient.client.Bill;
            formAddClient.textBoxKPP.Text = formAddClient.client.KPP;
            formAddClient.textBoxOKTMO.Text = formAddClient.client.OKTMO;
            formAddClient.textBoxOKATO.Text = formAddClient.client.OKATO;
            formAddClient.textBoxEmail.Text = formAddClient.client.Email;
            formAddClient.textBoxOGRN.Text = formAddClient.client.OGRN;
            formAddClient.textBoxAddress.Text = formAddClient.client.Address;
            formAddClient.textBoxFactAddress.Text = formAddClient.client.FactAddress;
            formAddClient.ShowDialog();
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
            string id = dataGridView.Rows[SelectIndex].Cells[0].Value.ToString();
            formAddPersonal.emp = new EmployeeMapper().Get(id);
            formAddPersonal.textBoxFirstName.Text = formAddPersonal.emp.FirstName;
            formAddPersonal.textBoxSecondName.Text = formAddPersonal.emp.SecondName;
            formAddPersonal.textBoxLastName.Text = formAddPersonal.emp.LastName;
            formAddPersonal.textBoxINN.Text = formAddPersonal.emp.INN;
            formAddPersonal.textBoxPassport.Text = formAddPersonal.emp.Passport;
            formAddPersonal.textBoxNumbOfTel.Text = formAddPersonal.emp.PhoneNumb;
            formAddPersonal.textBoxAddress.Text = formAddPersonal.emp.Address;
            formAddPersonal.monthCalendarDayBirth.SelectionEnd = formAddPersonal.emp.BornDate;
            formAddPersonal.date = formAddPersonal.emp.BornDate.ToString("dd/MM/yyyy");
            formAddPersonal.comboBoxGender.Text = UnitsConvert.ConvertSex(formAddPersonal.emp.Gender);
            formAddPersonal.comboBoxFunction.SelectedValue = formAddPersonal.emp.Function;
            formAddPersonal.ShowDialog();
        }
        //событие при клике по кнопке добавить позицию
        private void AddPosition_Click(object sender, EventArgs e)
        {
            AddOrEdit = (int)AddEditOrDelete.Add;
            formAddPrice = new FormAddPrice(this);
            formAddPrice.ShowDialog();
        }
        private void EditPosition_Click(object sender, EventArgs e)
        {
            if (GridRowsColumnIsNull())
                return;
            AddOrEdit = AddEditOrDelete.Edit;
            formAddPrice = new FormAddPrice(this);
            FillFormPrice(dataGridView, formAddPrice);
        }
        //используется при добавлении в formForSelect
        public void FillFormPrice(DataGridView dg, FormAddPrice fAddPrice)
        {
            fAddPrice.malf = new MalfMapper().Get(dg.Rows[SelectIndex].Cells[0].Value.ToString());
            fAddPrice.textBoxDescription.Text = fAddPrice.malf.Description;
            fAddPrice.textBoxPrice.Text = fAddPrice.malf.Cost.ToString();
            fAddPrice.comboBoxUnit.Text = UnitsConvert.ConvertUnit(fAddPrice.malf.Unit);
            fAddPrice.Show();
        }
        private void AddInStock_Click(object sender, EventArgs e)
        {
            AddOrEdit = (int)AddEditOrDelete.Add;
            formAddSparePart = new FormAddSparePart(this);
            formAddSparePart.ShowDialog();
        }
        private void EditStock_Click(object sender, EventArgs e)
        {
            if (GridRowsColumnIsNull())
                return;
            AddOrEdit = AddEditOrDelete.Edit;
            formAddSparePart = new FormAddSparePart(this);
            formAddSparePart.part = new SpareMapper().Get(dataGridView.Rows[SelectIndex].Cells[0].Value.ToString());
            formAddSparePart.textBoxUniqNumb.Text = formAddSparePart.part.Articul;
            formAddSparePart.textBoxDescr.Text = formAddSparePart.part.Description;
            formAddSparePart.textBoxCost.Text = formAddSparePart.part.Cost.ToString();
            formAddSparePart.textBoxNumb.Text = formAddSparePart.part.Number.ToString();
            formAddSparePart.comboBoxUnit.Text = UnitsConvert.ConvertUnit(formAddSparePart.part.Unit);
            formAddSparePart.ShowDialog();
        }
        //положить на склад
        private void PushInStock_Click(object sender, EventArgs e)
        {
            FormAddNumbReason addNumb = new FormAddNumbReason(this);
            WindowIndex = WindowsStruct.PushInStock;
            addNumb.part = new SpareMapper().Get(dataGridView.Rows[SelectIndex].Cells[0].Value.ToString());
            addNumb.textBoxDescrContent.Text = addNumb.part.Description;
            addNumb.labelNumber.Text += $"({UnitsConvert.ConvertUnit(addNumb.part.Unit)}):";
            addNumb.ShowDialog();
        }
        //выдать со склада
        private void PopInStock_Click(object sender, EventArgs e)
        {
            FormAddNumbReason addNumb = new FormAddNumbReason(this);
            WindowIndex = WindowsStruct.PopFromStock;
            addNumb.part = new SpareMapper().Get(dataGridView.Rows[SelectIndex].Cells[0].Value.ToString());
            addNumb.textBoxDescrContent.Text = addNumb.part.Description;
            addNumb.labelNumber.Text += $"({UnitsConvert.ConvertUnit(addNumb.part.Unit)}):";
            addNumb.ShowDialog();
        }
        private void AddWayBill_Click(object sender, EventArgs e)
        {
            WindowIndex = WindowsStruct.ActiveWayBills;
            FormAddWayBill formAddWayBill = new FormAddWayBill(this);
            formAddWayBill.insWayBill = new WayBill();
            formAddWayBill.ShowDialog();
        }
        private void EditWayBill_Click(object sender, EventArgs e)
        {
            if (GridRowsColumnIsNull())
                return;
            FormAddWayBill formAddWayBill = new FormAddWayBill(this);
            formAddWayBill.wayBill = new WayBillMapper().Get(dataGridView.Rows[SelectIndex].Cells[0].Value.ToString());
            formAddWayBill.textBoxClient.Text = formAddWayBill.wayBill.Client.Name;
            formAddWayBill.textBoxTrip.Text = formAddWayBill.wayBill.Trip.Name;
            formAddWayBill.comboBoxCar.SelectedValue = formAddWayBill.wayBill.Car.Number;
            formAddWayBill.comboBoxDriver.SelectedValue = formAddWayBill.wayBill.Driver.TubNumb;
            formAddWayBill.pickerStart.Value = formAddWayBill.wayBill.LoadDate;
            if (formAddWayBill.wayBill.UnloadDate != null)
            {
                formAddWayBill.pickerEnd.Value = (DateTime)formAddWayBill.wayBill.UnloadDate;
                formAddWayBill.pickerEnd.Checked = true;
            }
            else
                formAddWayBill.pickerEnd.Checked = false;
            formAddWayBill.textBoxBaseDoc.Text = formAddWayBill.wayBill.BaseDocument;
            formAddWayBill.textBoxKm.Text = formAddWayBill.wayBill.Kilometers.ToString();
            formAddWayBill.txtBoxCost.Text = formAddWayBill.wayBill.Cost.ToString();
            formAddWayBill.textBoxFuel.Text = formAddWayBill.wayBill.Fuel.ToString();
            formAddWayBill.textBoxNotes.Text = formAddWayBill.wayBill.Notes;
            formAddWayBill.ShowDialog();
        }
        private void DeleteWayBill_Click(object sender, EventArgs e)
        {
            if (GridRowsColumnIsNull())
                return;
            WayBillMapper wm = new WayBillMapper();
            WayBill wayBill = wm.Get(dataGridView.Rows[SelectIndex].Cells[0].Value.ToString());
            if (wayBill.UnloadDate == null)
            {
                MessageBox.Show("Для завершения перевозки необходимо указать дату выгрузки!");
                return;
            }
            if ((MessageBox.Show(string.Format("Вы действительно завершить перевозку №{0}?",
                dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString()), "Предупреждение",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
            {
                wayBill.Finish();
                wm.Update(wayBill);
                DataSets.CreateDSForDataGrid(WindowIndex, dataGridView);
            }
        }
        private void SeeFinishedWayBill_Click(object sender, EventArgs e)
        {
            if (GridRowsColumnIsNull())
                return;
            WayBill wayBill = new WayBill();
            WayBillMapper wm = new WayBillMapper();
            wayBill = wm.Get(dataGridView.Rows[SelectIndex].Cells[0].Value.ToString());
            FormInformation formInformation = new FormInformation();
            formInformation.wayBill = wayBill;
            formInformation.textBoxInf.Text = wayBill.ToString();
            formInformation.ShowDialog();
        }
        private void StartFinishedWayBill_Click(object sender, EventArgs e)
        {
            if (GridRowsColumnIsNull())
                return;
            bool confirm = ((MessageBox.Show(string.Format("Вы действительно хотите восстановить перевозку №{0}?",
                dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString()), "Предупреждение",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK));
            if (confirm)
            {
                WayBillMapper wm = new WayBillMapper();
                WayBill wayBill = wm.Get(dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString());
                wayBill.Activate();
                wm.Update(wayBill);
            }
            DataSets.CreateDSForDataGrid(WindowIndex, dataGridView);
        }
        //функции для добавления списка объектов в сетку
        public static void AddListRepairsInGrid(DataGridView dataGridView, string content = "")
        {
            DbProxy.DataSets.CreateDSForDataGrid(WindowIndex, dataGridView, content);
        }
        public static void AddListFinishedRepsInGrid(DataGridView dataGridView, string content= "")
        {
            DbProxy.DataSets.CreateDSForDataGrid(WindowIndex, dataGridView, content);
        }
        public static void AddListAutoInGrid(DataGridView dataGridView, string content = "")
        {
            DbProxy.DataSets.CreateDSForDataGrid(WindowIndex, dataGridView, content);
        }
        public static void AddListClientInGrid(DataGridView dataGridView, string content = "")
        {
            DbProxy.DataSets.CreateDSForDataGrid(WindowIndex, dataGridView, content);
        }
        public static void AddListPersonalInGrid(DataGridView dataGridView, string content = "")
        {
            DbProxy.DataSets.CreateDSForDataGrid(WindowIndex, dataGridView, content);
        }
        public static void AddListMalfunctionsInGrid(DataGridView dataGridView, string content = "")
        {
            DbProxy.DataSets.CreateDSForDataGrid(WindowIndex, dataGridView, content);
        }
        public static void AddSparePartInStock(DataGridView dataGridView, string content = "")
        {
            DbProxy.DataSets.CreateDSForDataGrid(WindowIndex, dataGridView, content);
        }
        public static void AddListTripInGrid(DataGridView dataGridView, string content = "")
        {
            DbProxy.DataSets.CreateDSForDataGrid(WindowIndex, dataGridView, content);
        }
        //функции для редактирования сетки 
        private void DataGridViewForRepairs()
        {
            AddListRepairsInGrid(dataGridView);
        }
        private void DataGridViewForFinishedReps()
        {
            AddListFinishedRepsInGrid(dataGridView);
        }
        private void DataGridViewForClient()
        {
            AddListClientInGrid(dataGridView);
        }
        private void DataGridViewForPersonal()
        {
            AddListPersonalInGrid(dataGridView, Queries.StaffView);
        }
        private void DataGridViewForAuto()
        {
            AddListAutoInGrid(dataGridView);
        }
        private void DataGridViewForPrice()
        {
            AddListMalfunctionsInGrid(dataGridView, Queries.MalfunctionsView);
        }
        private void DataGridViewForStock()
        {
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
            if (dataGridView.SelectedRows.Count == 0)
                return;
            WorkWithExcel.MakeBillInExcelAsync(dataGridView.SelectedRows[0].Cells[0].Value.ToString());
        }

        private void FinishActToolStripMenu_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
                return;
            WorkWithExcel.MakeActOfWorkInExcelAsync(dataGridView.SelectedRows[0].Cells[0].Value.ToString());
        }
        private void OrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
                return;
            WorkWithExcel.MakeOrderInExcelAsync(dataGridView.SelectedRows[0].Cells[0].Value.ToString());
        }
        private void UploadAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
                return;
            WorkWithExcel.UploadAllDocsInExcAsync(dataGridView.SelectedRows[0].Cells[0].Value.ToString());
        }
        public bool GridRowsColumnIsNull()
        {
            if (dataGridView.Rows.Count == 0 || dataGridView.SelectedRows.Count == 0)
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
        private void MakeDataGridReadOnly()
        {
            dataGridView.ReadOnly = true;
        }
        private void MakeDataGridForReports()
        {
            dataGridView.ReadOnly = false;
            for (int i = 0; i < dataGridView.Columns.Count; i++)
            {
                if (i == dataGridView.Columns.Count - 1)
                    dataGridView.Columns[i].ReadOnly = false;
                else
                {
                    dataGridView.Columns[i].ReadOnly = true;
                }
            }
        }
        private void ColourDataGrid()
        {
            int rows = dataGridView.Rows.Count;
            int columns = dataGridView.Columns.Count;
            for (int i = 0; i < rows; i++)
            {
                double preLastColValue = double.Parse(dataGridView.Rows[i].Cells[columns - 2].Value.ToString());
                double lastColValue = double.Parse(dataGridView.Rows[i].Cells[columns - 1].Value.ToString());
                if (preLastColValue == lastColValue)
                    dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                else if (preLastColValue > lastColValue && lastColValue != 0)
                    dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.PaleVioletRed;
            }
        }
        private void dataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox textBox = (TextBox) e.Control;
            textBox.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressFloat);
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            double cellValue = 0;
            try
            {
                cellValue = double.Parse(dataGridView.SelectedRows[0].Cells[e.ColumnIndex].Value.ToString());
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
            catch (FormatException)
            {
                
            }
            if (WindowIndex == WindowsStruct.RepairsReport)
            {
                CardMapper cm = new CardMapper();
                CardOfRepair card = cm.Get(dataGridView.SelectedRows[0].Cells[0].Value.ToString());
                card.PaidMoney = cellValue;
                cm.Update(card);
            }
            else if (WindowIndex == WindowsStruct.WayBillsReport)
            {
                WayBillMapper wm = new WayBillMapper();
                WayBill wayBill = wm.Get(dataGridView.SelectedRows[0].Cells[0].Value.ToString());
                wayBill.PaidMoney = cellValue;
                wm.Update(wayBill);
            }
            BeginInvoke(new MethodInvoker(() =>
            {
                DataSets.CreateDSForDataGrid(WindowIndex, dataGridView);
            }));
            BeginInvoke(new MethodInvoker(() =>
            {
                MakeDataGridForReports();
            }));
            BeginInvoke(new MethodInvoker(() =>
            {
                ColourDataGrid();
            }));
        }

        private void dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

        }
    }
}
