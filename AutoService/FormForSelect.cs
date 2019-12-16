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
using DbProxy;

namespace AutoService
{
    public partial class FormForSelect : Form
    {
        FormAddRepair FormAddRepair;
        FormAddAuto FormAddAuto;
        FormAddPrice FormAddPrice;
        Form1 mainForm;

        public FormForSelect()
        {
            InitializeComponent();
        }
        public FormForSelect(FormAddRepair FormAddRepair, Form1 mainForm) : this ()
        {
            this.mainForm = mainForm;
            this.FormAddRepair = FormAddRepair;
        }
        public FormForSelect(FormAddAuto FormAddAuto, Form1 mainForm) : this ()
        {
            this.mainForm = mainForm;
            this.FormAddAuto = FormAddAuto;
        }
        public FormForSelect(FormAddPrice FormAddPrice) : this()
        {
            this.FormAddPrice = FormAddPrice;
        }
        private void FormForSelect_Load(object sender, EventArgs e)
        {
            dataGridView.RowHeadersVisible = false;
            dataGridView.RowTemplate.Height = 30;
            HideAddAndEditBtns();
            //оператор для определения в какой части приложения вызывается окно
            switch (Form1.WindowIndex)
            {
                case WindowsStruct.Repairs:
                    Form1.AddListAutoInGrid(dataGridView);
                    break;
                //case WindowsStruct.Auto:
                //    Form1.AddListClientInGrid(dataGridView);
                //    break;
                //case WindowsStruct.AddAutoInRep:
                //    Form1.AddListClientInGrid(dataGridView);
                //    break;
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
                    Form1.AddListMalfunctionsInGrid(dataGridView, FormAddRepair.id_repair.ToString());
                    break;
                case WindowsStruct.SpareAdd:
                    ShowSearch();
                    ShowAddAndEditBtns();
                    Form1.AddListMalfunctionsInGrid(dataGridView);
                    break;
                case WindowsStruct.SpareView:
                    HideSearch();
                    Form1.AddListMalfunctionsInGrid(dataGridView, FormAddRepair.id_repair.ToString());
                    break;
                case WindowsStruct.WorkerAdd:
                    ShowSearch();
                    Form1.AddListPersonalInGrid(dataGridView);
                    break;
                case WindowsStruct.WorkerView:
                    HideSearch();
                    Form1.AddListPersonalInGrid(dataGridView, FormAddRepair.id_repair.ToString());
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
                FormAddNumber formAddNumber = new FormAddNumber(this, FormAddRepair);
                //оператор для определения места вызова
                switch (Form1.WindowIndex)
                {
                    case WindowsStruct.ViewAutoInRep:
                        string state_number = dataGridView.Rows[Form1.SelectIndex].Cells[1].Value.ToString();
                        Form1.WindowIndex = WindowsStruct.Repairs;
                        FormAddAuto.ReadAutoFromViewForRepair(state_number, FormAddRepair);
                        if (FormAddRepair.id_repair == 0 && Form1.AddOrEdit == AddEditOrDelete.Add)
                            FormAddRepair.id_repair = InvokeProcedure.GetIdRepairViaCarNumber(state_number);
                        this.Close();
                        break;
                    //case WindowsStruct.Auto:
                    //    FormAddAuto.labelContentOwner.Text = selectedRowOneCellValue;
                    //    this.Close();
                    //    FormAddAuto.OwnerSelected = true;
                    //    break;
                    //case WindowsStruct.AddAutoInRep:
                    //    FormAddAuto.labelContentOwner.Text = selectedRowOneCellValue;
                    //    this.Close();
                    //    FormAddAuto.OwnerSelected = true;
                    //    break;
                    case WindowsStruct.AddClientInAuto:
                        FormAddAuto.labelContentOwner.Text = selectedRowOneCellValue;
                        this.Close();
                        FormAddAuto.OwnerSelected = true;
                        break;
                    case WindowsStruct.Worker:
                        FormAddRepair.SelectedPersonLabel.Text = Personal.PersonalList[Form1.SelectIndex].Name;
                        break;
                    case WindowsStruct.MalfAdd:
                        formAddNumber.ShowDialog();
                        break;
                    case WindowsStruct.MalfView:
                        if (e.Button == MouseButtons.Left)
                        {
                            formAddNumber.ShowDialog();
                        }
                        else
                        {
                            if (AnswerAboutDeleting)
                            {
                                InvokeProcedure.DeleteWorksInRep(FormAddRepair.id_repair, selectedRowOneCellValue);
                                Form1.AddListMalfunctionsInGrid(dataGridView, FormAddRepair.id_repair.ToString());
                            }
                        }
                        break;
                    case WindowsStruct.SpareAdd:
                        formAddNumber.ShowDialog();
                        break;
                    case WindowsStruct.SpareView:
                        if (e.Button == MouseButtons.Left)
                            formAddNumber.ShowDialog();
                        else
                        {
                            if (AnswerAboutDeleting)
                            {
                                InvokeProcedure.DeleteWorksInRep(FormAddRepair.id_repair, selectedRowOneCellValue);
                                Form1.AddSparePartInStock(dataGridView, FormAddRepair.id_repair.ToString());
                            }
                        }
                        break;
                    case WindowsStruct.WorkerAdd:
                        InvokeProcedure.AddWorkerInRep(FormAddRepair.id_repair, int.Parse(selectedRowOneCellValue));
                        this.Close();
                        break;
                    case WindowsStruct.WorkerView:
                        Form1.AddListPersonalInGrid(dataGridView, Queries.GetStaffByIdRepair(FormAddRepair.id_repair.ToString()));
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
            if (FormAddAuto != null && FormAddAuto.Visible && FormAddRepair != null && FormAddRepair.Visible)
                Form1.WindowIndex = WindowsStruct.Repairs;
            else if (FormAddAuto != null && FormAddAuto.Visible)
                Form1.WindowIndex = WindowsStruct.Auto;
            else if (FormAddRepair != null && FormAddRepair.Visible)
                Form1.WindowIndex = WindowsStruct.Repairs;

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
                    FormAddPrice = new FormAddPrice(mainForm, this);
                    FormAddPrice.ShowDialog();
                    break;
                case WindowsStruct.SpareAdd:
                    FormAddPrice = new FormAddPrice(mainForm, this);
                    FormAddPrice.ShowDialog();
                    break;
            }
        }
        private void btnEditPosition_Click(object sender, EventArgs e)
        {
            Form1.AddOrEdit = AddEditOrDelete.Edit;
            switch (Form1.WindowIndex)
            {
                case WindowsStruct.MalfAdd:
                    FormAddPrice = new FormAddPrice(mainForm, this);
                    mainForm.FillFormPrice(dataGridView, Form1.db, FormAddPrice);
                break;
                case WindowsStruct.SpareAdd:
                    FormAddPrice = new FormAddPrice(mainForm, this);
                    mainForm.FillFormPrice(dataGridView, Form1.db, FormAddPrice);
                break;
            }
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
        private void ShowSearch()
        {
            labelSearch.Visible = true;
            textBoxSearch.Visible = true;
        }
    }
}
