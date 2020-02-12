using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using FirebirdSql.Data.FirebirdClient;
using AutoServiceLibrary;
using DataMapper;
using DbProxy;

namespace AutoService
{
    public partial class FormForSelect : Form
    {
        public FormAddRepair formAddRepair;
        FormAddAuto formAddAuto;
        FormAddPrice formAddPrice;
        FormAddWayBill formAddWayBill;
        Form1 mainForm;
        string[] columnNames = { "ID", "Описание", "Количество", "Ед. измерения", "Стоимость", "Итого" };
        public FormForSelect()
        {
            InitializeComponent();
        }
        public FormForSelect(FormAddRepair FormAddRepair, Form1 mainForm) : this ()
        {
            this.mainForm = mainForm;
            this.formAddRepair = FormAddRepair;
        }
        public FormForSelect(FormAddAuto FormAddAuto, Form1 mainForm) : this ()
        {
            this.mainForm = mainForm;
            this.formAddAuto = FormAddAuto;
        }
        public FormForSelect(FormAddWayBill formAddWayBill, Form1 mainForm) : this()
        {
            this.formAddWayBill = formAddWayBill;
            this.mainForm = mainForm;
        }
        public FormForSelect(FormAddPrice FormAddPrice) : this()
        {
            this.formAddPrice = FormAddPrice;
        }
        private void FormForSelect_Load(object sender, EventArgs e)
        {
            dataGridView.RowHeadersVisible = false;
            dataGridView.RowTemplate.Height = 30;
            HideAddAndEditBtns();
            HideDeleteButton();
            //оператор для определения в какой части приложения вызывается окно
            switch (Form1.WindowIndex)
            {
                case WindowsStruct.Repairs:
                    Form1.AddListAutoInGrid(dataGridView);
                    break;
                case WindowsStruct.AddClientInAuto:
                    Form1.AddListClientInGrid(dataGridView);
                    break;
                case WindowsStruct.ViewAutoInRep:
                    ShowSearch();
                    Form1.AddListAutoInGrid(dataGridView);
                    break;
                case WindowsStruct.Worker:
                    Form1.AddListPersonalInGrid(dataGridView);
                    break;
                case WindowsStruct.MalfAdd:
                    ShowSearch();
                    ShowAddAndEditBtns();
                    Form1.AddListMalfunctionsInGrid(dataGridView);
                    break;
                case WindowsStruct.MalfView:
                    HideSearch();
                    ShowDeleteButton();
                    AddListMalfInGrid();
                    break;
                case WindowsStruct.SpareAdd:
                    ShowSearch();
                    ShowAddAndEditBtns();
                    Form1.AddListMalfunctionsInGrid(dataGridView);
                    break;
                case WindowsStruct.SpareView:
                    HideSearch();
                    ShowDeleteButton();
                    AddListSpareInGrid();
                    break;
                case WindowsStruct.WorkerAdd:
                    ShowSearch();
                    Form1.AddListPersonalInGrid(dataGridView);
                    break;
                case WindowsStruct.WorkerView:
                    HideSearch();
                    ShowDeleteButton();
                    AddListWorkersInGrid();
                    break;
                case WindowsStruct.AddClientInWay:
                    ShowSearch();
                    Form1.AddListClientInGrid(dataGridView);
                    break;
                case WindowsStruct.AddTripInWay:
                    btnEditPosition.Visible = true;
                    btnEditPosition.Location = btnAddNewPosition.Location;
                    ShowSearch();
                    Form1.AddListClientInGrid(dataGridView);
                    break;
            }
            Form1.SelectIndex = 0;
        }
        private void dataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                    return;
                Form1.SelectIndex = e.RowIndex;
                string selectedRowOneCellValue = dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString();
                FormAddNumber formAddNumber = new FormAddNumber(this, formAddRepair);
                //оператор для определения места вызова
                switch (Form1.WindowIndex)
                {
                    case WindowsStruct.ViewAutoInRep:
                        string state_number = dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString();
                        Form1.WindowIndex = WindowsStruct.Repairs;
                        Car car = new CarMapper().Get(state_number);
                        mainForm.FillCardWithCar(formAddRepair, car);
                        formAddRepair.repair.Car = car;
                        this.Close();
                        break;
                    case WindowsStruct.AddClientInAuto:
                        formAddAuto.client = new ClientMapper().Get(selectedRowOneCellValue);
                        formAddAuto.labelContentOwner.Text = formAddAuto.client.Name;
                        this.Close();
                        formAddAuto.OwnerSelected = true;
                        break;
                    case WindowsStruct.AddClientInWay:
                        if (formAddWayBill.wayBill != null)
                        {
                            formAddWayBill.wayBill.Client = new ClientMapper().Get(selectedRowOneCellValue);
                            formAddWayBill.textBoxClient.Text = formAddWayBill.wayBill.Client.Name;
                        }
                        else
                        {
                            formAddWayBill.insWayBill.Client = new ClientMapper().Get(selectedRowOneCellValue);
                            formAddWayBill.textBoxClient.Text = formAddWayBill.insWayBill.Client.Name;
                        }
                        this.Close();
                        break;
                    case WindowsStruct.AddTripInWay:
                        if (formAddWayBill.wayBill != null)
                        {
                            formAddWayBill.wayBill.Trip = new TripMapper().Get(selectedRowOneCellValue);
                            formAddWayBill.textBoxTrip.Text = formAddWayBill.wayBill.Trip.Name;
                        }
                        else
                        {
                            formAddWayBill.insWayBill.Trip = new TripMapper().Get(selectedRowOneCellValue);
                            formAddWayBill.textBoxTrip.Text = formAddWayBill.insWayBill.Trip.Name;
                        }
                        this.Close();
                        break;
                    case WindowsStruct.Worker:
                        //FormAddRepair.SelectedPersonLabel.Text = Employee.PersonalList[Form1.SelectIndex].Name;
                        break;
                    case WindowsStruct.MalfAdd:
                        formAddNumber.ShowDialog();
                        break;
                    case WindowsStruct.SpareAdd:
                        formAddNumber.ShowDialog();
                        break;
                    case WindowsStruct.WorkerAdd:
                        Employee emp = new EmployeeMapper().Get(selectedRowOneCellValue);
                        try
                        {
                            formAddRepair.repair.AddPersonalInList(emp);
                        }
                        catch (FormatException ex)
                        {
                            MessageBox.Show("Сотрудник уже добавлен!");
                            return;
                        }
                        this.Close();
                        break;
                    case WindowsStruct.MalfView:
                        formAddNumber.ShowDialog();
                        break;
                    case WindowsStruct.SpareView:
                        formAddNumber.ShowDialog();
                        break;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
        }
        private void FormForSelect_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (formAddAuto != null && formAddAuto.Visible && formAddRepair != null && formAddRepair.Visible)
                Form1.WindowIndex = WindowsStruct.Repairs;
            else if (formAddAuto != null && formAddAuto.Visible)
                Form1.WindowIndex = WindowsStruct.Auto;
            else if (formAddRepair != null && formAddRepair.Visible)
            {
                Form1.WindowIndex = WindowsStruct.Repairs;
                formAddRepair.textBoxInf.Text = formAddRepair.repair.ToString();
            }
            else if (formAddWayBill != null && formAddWayBill.Visible)
            {
                Form1.WindowIndex = WindowsStruct.ActiveWayBills;
            }

            textBoxSearch.Clear();
        }
        public bool AnswerAboutDeleting => (MessageBox.Show($"Вы действительно хотите удалить\n" +
                    $" {dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString()}\n" +
                    $" {dataGridView.Rows[Form1.SelectIndex].Cells[1].Value.ToString()}", "Предупреждение",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == DialogResult.OK);
        private void btnAddNewPosition_Click(object sender, EventArgs e)
        {
            Form1.AddOrEdit = AddEditOrDelete.Add;
            switch (Form1.WindowIndex)
            {
                case WindowsStruct.MalfAdd:
                    formAddPrice = new FormAddPrice(mainForm, this);
                    formAddPrice.ShowDialog();
                    break;
                case WindowsStruct.SpareAdd:
                    formAddPrice = new FormAddPrice(mainForm, this);
                    formAddPrice.ShowDialog();
                    break;
            }
        }
        private void btnEditPosition_Click(object sender, EventArgs e)
        {
            Form1.AddOrEdit = AddEditOrDelete.Edit;
            switch (Form1.WindowIndex)
            {
                case WindowsStruct.MalfAdd:
                    formAddPrice = new FormAddPrice(mainForm, this);
                    mainForm.FillFormPrice(dataGridView, formAddPrice);
                    break;
                case WindowsStruct.SpareAdd:
                    formAddPrice = new FormAddPrice(mainForm, this);
                    mainForm.FillFormPrice(dataGridView, formAddPrice);
                    break;
                case WindowsStruct.AddTripInWay:
                    FormAddTrip formAddTrip = new FormAddTrip(this, mainForm);
                    formAddTrip.trip = new TripMapper().Get(dataGridView.SelectedRows[0].Cells[0].Value.ToString());
                    formAddTrip.textBoxTrip.Text = formAddTrip.trip.Name;
                    formAddTrip.ShowDialog();
                    break;
            }
        }
        private void btnDeletePosition_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
                return;
            string id = dataGridView.SelectedRows[0].Cells[0].Value.ToString();
            if (Form1.WindowIndex == WindowsStruct.MalfView|| Form1.WindowIndex == WindowsStruct.SpareView)
            {
                Malfunctions malf = new MalfMapper().Get(id);
                formAddRepair.repair.RemoveMalfFromList(malf);
            }
            else if (Form1.WindowIndex == WindowsStruct.WorkerView)
            {
                Employee emp = new EmployeeMapper().Get(id);
                formAddRepair.repair.RemovePersonalFromList(emp);
            }
            RefreshDataInGrid();
        }
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string content = textBoxSearch.Text.ToUpper();
            switch (Form1.WindowIndex)
            {
                case WindowsStruct.MalfAdd:
                    Form1.AddListMalfunctionsInGrid(dataGridView, content);
                    break;
                case WindowsStruct.SpareAdd:
                    Form1.AddListMalfunctionsInGrid(dataGridView, content);
                    break;
                case WindowsStruct.ViewAutoInRep:
                    Form1.AddListAutoInGrid(dataGridView, content);
                    break;
                case WindowsStruct.AddClientInAuto:
                    Form1.AddListClientInGrid(dataGridView, content);
                    break;
                case WindowsStruct.AddClientInWay:
                    Form1.AddListClientInGrid(dataGridView, content);
                    break;
                case WindowsStruct.AddTripInWay:
                    Form1.AddListTripInGrid(dataGridView, content);
                    break;
            }
        }
        private void ShowAddAndEditBtns()
        {
            btnAddNewPosition.Visible = true;
            btnEditPosition.Visible = true;
        }
        private void HideAddAndEditBtns()
        {
            btnAddNewPosition.Visible = false;
            btnEditPosition.Visible = false;
        }
        private void ShowDeleteButton()
        {
            btnDeletePosition.Visible = true;
        }
        private void HideDeleteButton()
        {
            btnDeletePosition.Visible = false;
        }
        private void dataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (Form1.SelectIndex < 0)
                return;
            Form1.SelectIndex = e.RowIndex;
        }
        private void HideSearch()
        {
            labelSearch.Visible = false;
            textBoxSearch.Visible = false;
        }
        private void HideHeader()
        {
            dataGridView.ColumnHeadersVisible = false;
        }
        private void ShowSearch()
        {
            labelSearch.Visible = true;
            textBoxSearch.Visible = true;
        }
        public void AddListMalfInGrid()
        {
            dataGridView.DataSource = formAddRepair.repair.ListOfMalf.Select(m => m)
            .Where(m => m.MalfOrSpare == 0).ToList();
            dataGridView.Columns[0].Visible = false;
            dataGridView.Columns[dataGridView.Columns.Count - 1].Visible = false;
            CallHeadersInDG(dataGridView, columnNames);
        }
        public void AddListSpareInGrid()
        {
            dataGridView.DataSource = formAddRepair.repair.ListOfMalf.Select(m => m)
            . Where(m => m.MalfOrSpare == 1).ToList();
            dataGridView.Columns[0].Visible = false;
            dataGridView.Columns[dataGridView.Columns.Count - 1].Visible = false;
            CallHeadersInDG(dataGridView, columnNames);
        }
        public void AddListWorkersInGrid()
        {
            dataGridView.DataSource = formAddRepair.repair.ListOfPersonal;
        }
        private void CallHeadersInDG(DataGridView dg, string[] colNames)
        {
            for (int i = 0; i < dg.Columns.Count && i < colNames.Length; i++)
            {
                dg.Columns[i].HeaderText = colNames[i];
            }
        }
        public void RefreshDataInGrid()
        {
            if (dataGridView.Rows.Count == 0)
                return;
            dataGridView.DataSource = null;
            if (Form1.WindowIndex == WindowsStruct.MalfView)
            {
                AddListMalfInGrid();
            }   
            else if (Form1.WindowIndex == WindowsStruct.SpareView)
            {
                AddListSpareInGrid();
            }
            else if (Form1.WindowIndex == WindowsStruct.WorkerView)
            {
                AddListWorkersInGrid();
            }
            dataGridView.ClearSelection();
            dataGridView.Refresh();
        }
        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            
        }
        
    }
}
