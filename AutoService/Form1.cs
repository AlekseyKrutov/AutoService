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
using System.Threading;

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
            PushInStock.Location = EndRepair.Location;
            PopInStock.Location = new Point(PushInStock.Location.X + PopInStock.Width + 5, PushInStock.Location.Y);
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
                formAddRepair = new FormAddRepair(formAddAuto, this);
                formAddRepair.id_repair = int.Parse(dataGridView.Rows[SelectIndex].Cells[0].Value.ToString());
                formAddRepair.repair = new CardMapper().Get(dataGridView.Rows[SelectIndex].Cells[0].Value.ToString());
                formAddRepair.textBoxInf.Text = formAddRepair.repair.ToString();
                formAddRepair.textBoxNotes.Text = formAddRepair.repair.Notes;
                formAddRepair.dateTimeStart.Value = formAddRepair.repair.TimeOfStart;
                //formAddRepair.dateTimeFinish.Value = (formAddRepair.repair.TimeOfFinish == null) ? formAddRepair.repair.TimeOfStart : formAddRepair.repair.TimeOfFinish;
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
            string model = (car.CarModel == null) ? "" : car.CarModel;
            formAddRepair.textBoxMark.Text = car.CarMark + " " + model;
            formAddRepair.textBoxGosNom.Text = car.NumberOfCar;
            formAddRepair.textBoxReg.Text = car.RegCertific;
            formAddRepair.textBoxOwner.Text = (car.Owner == null) ? "" : car.Owner.Name;
        }
        private void StartFinishedRepair_Click(object sender, EventArgs e)
        {
            if (GridRowsColumnIsNull())
                return;
            string rep_id = dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString();
            bool confirm = ((MessageBox.Show(string.Format("Вы действительно хотите восстановить ремонт №{0}?",
                dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString()), "Предупреждение",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK));
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
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
            {
                CardMapper cm = new CardMapper();
                CardOfRepair card = cm.Get(dataGridView.Rows[SelectIndex].Cells[0].Value.ToString());
                if (card.TimeOfFinish != null)
                    card.FinishRepair();
                else
                {
                    MessageBox.Show("В ремонте не указана дата завершения!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
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
            formAddAuto.textBoxGosNumb.Text = formAddAuto.car.NumberOfCar;
            formAddAuto.textBoxVIN.Text = formAddAuto.car.CarVIN;
            formAddAuto.textBoxReg.Text = formAddAuto.car.RegCertific;
            string model = (formAddAuto.car.CarModel == "") ? formAddAuto.car.CarMark :
                formAddAuto.car.CarMark + ' ' + formAddAuto.car.CarModel;
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
            fAddPrice.textBoxDescription.Text = fAddPrice.malf.DescriptionOfMalf;
            fAddPrice.textBoxPrice.Text = fAddPrice.malf.Price.ToString();
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
            formAddSparePart.textBoxCost.Text = formAddSparePart.part.Price.ToString();
            formAddSparePart.textBoxNumb.Text = formAddSparePart.part.Number.ToString();
            formAddSparePart.comboBoxUnit.Text = UnitsConvert.ConvertUnit(formAddSparePart.part.Unit);
            formAddSparePart.ShowDialog();
        }
        //положить на склад
        private void PushInStock_Click(object sender, EventArgs e)
        {
            FormAddNumbReason addNumb = new FormAddNumbReason();
            WindowIndex = WindowsStruct.PushInStock;
            SparePart sp = new SparePart();
            sp = new SpareMapper().Get(dataGridView.Rows[SelectIndex].Cells[0].Value.ToString());
            addNumb.textBoxDescrContent.Text = sp.Description;
            addNumb.ShowDialog();
        }
        //выдать со склада
        private void PopInStock_Click(object sender, EventArgs e)
        {
            FormAddNumbReason addNumb = new FormAddNumbReason();
            WindowIndex = WindowsStruct.PopFromStock;
            addNumb.ShowDialog();
        }
        private void AddWayBill_Click(object sender, EventArgs e)
        {
            FormAddWayBill formAddWayBill = new FormAddWayBill();
            formAddWayBill.ShowDialog();
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
