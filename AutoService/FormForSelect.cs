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
                case (int)Form1.WindowsStruct.Repairs:
                    Form1.AddListAutoInGrid(dataGridView);
                    break;
                case (int)Form1.WindowsStruct.Auto:
                    Form1.AddListClientInGrid(dataGridView);
                    break;
                case (int)Form1.WindowsStruct.Worker:
                    Form1.AddListPersonalInGrid(dataGridView, Form1.queryForStaff);
                    break;
                case (int)Form1.WindowsStruct.MalfAdd:
                    ShowAddAndEditBtns();
                    Form1.AddListMalfunctionsInGrid(dataGridView, Form1.queryForMalfunctions);
                    break;
                case (int)Form1.WindowsStruct.MalfView:
                    Form1.AddListMalfunctionsInGrid(dataGridView, queryForMalfView(FormAddRepair.id_repair));
                    break;
                case (int)Form1.WindowsStruct.SpareAdd:
                    ShowAddAndEditBtns();
                    Form1.AddListMalfunctionsInGrid(dataGridView, Form1.queryForSpares);
                    break;
                case (int)Form1.WindowsStruct.SpareView:
                    Form1.AddListMalfunctionsInGrid(dataGridView, queryForSpareView(FormAddRepair.id_repair));
                    break;
                case (int)Form1.WindowsStruct.WorkerAdd:
                    Form1.AddListPersonalInGrid(dataGridView, Form1.queryForStaff);
                    break;
                case (int)Form1.WindowsStruct.WorkerView:
                    Form1.AddListPersonalInGrid(dataGridView, queryForStaffView(FormAddRepair.id_repair));
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
                    case (int)Form1.WindowsStruct.Repairs:
                        string state_number = dataGridView.Rows[Form1.SelectIndex].Cells[2].Value.ToString();
                        FormAddAuto.ReadAutoFromViewForRepair(state_number, FormAddRepair);
                        if (FormAddRepair.id_repair == 0 && Form1.AddOrEdit == (int)Form1.AddEditOrDelete.Add)
                            FormAddRepair.id_repair = FormAddRepair.GetIdRepair(state_number);
                        this.Close();
                        break;
                    case (int)Form1.WindowsStruct.Auto:
                        FormAddAuto.labelContentOwner.Text = selectedRowOneCellValue;
                        this.Close();
                        FormAddAuto.OwnerSelected = true;
                        break;
                    case (int)Form1.WindowsStruct.Worker:
                        FormAddRepair.SelectedPersonLabel.Text = Personal.PersonalList[Form1.SelectIndex].Name;
                        break;
                    case (int)Form1.WindowsStruct.MalfAdd:
                        formAddNumber.ShowDialog();
                        break;
                    case (int)Form1.WindowsStruct.MalfView:
                        if (e.Button == MouseButtons.Left)
                        {
                            formAddNumber.ShowDialog();
                        }
                        else
                        {
                            if (AnswerAboutDeleting)
                            {
                                FormAddRepair.ExecuteProcedureDelete(FormAddRepair.id_repair, 0, selectedRowOneCellValue, "DELETE_REPAIRS_WORKS");
                                Form1.AddListMalfunctionsInGrid(dataGridView, queryForMalfView(FormAddRepair.id_repair));
                            }
                        }
                        break;
                    case (int)Form1.WindowsStruct.SpareAdd:
                        formAddNumber.ShowDialog();
                        break;
                    case (int)Form1.WindowsStruct.SpareView:
                        if (e.Button == MouseButtons.Left)
                            formAddNumber.ShowDialog();
                        else
                        {
                            if (AnswerAboutDeleting)
                            {
                                FormAddRepair.ExecuteProcedureDelete(FormAddRepair.id_repair, 0, selectedRowOneCellValue, "DELETE_REPAIRS_WORKS");
                                Form1.AddSparePartInStock(dataGridView, queryForSpareView(FormAddRepair.id_repair));
                            }
                        }
                        break;
                    case (int)Form1.WindowsStruct.WorkerAdd:
                        FormAddRepair.ExecuteProcedureForAddWorker(FormAddRepair.id_repair, int.Parse(selectedRowOneCellValue));
                        this.Close();
                        break;
                    case (int)Form1.WindowsStruct.WorkerView:
                        Form1.AddListPersonalInGrid(dataGridView, queryForStaffView(FormAddRepair.id_repair));
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
            Form1.WindowIndex = (int)Form1.WindowsStruct.Repairs;
            Form1.AddOrEdit = (int) Form1.AddEditOrDelete.Add;
            textBoxSearch.Clear();
        }
        public bool AnswerAboutDeleting => (MessageBox.Show($"Вы действительно хотите удалить\n" +
                    $" {dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString()}\n" +
                    $" {dataGridView.Rows[Form1.SelectIndex].Cells[1].Value.ToString()}", "Предупреждение",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == DialogResult.OK);
        public string queryForMalfView(int id_repair) => string.Format(
                        "select tw.description, case when tw.unit = 0 then 'шт'" +
                        " when tw.unit = 1 then 'нч' end as unit, tw.cost, cr.number" +
                        " from type_of_work as tw, card_of_rep_and_works as cr" +
                        " where cr.id_card_of_repair = {0} and tw.id_work = cr.id_work and tw.malf_or_spare = '0';", id_repair);
        public string queryForSpareView(int id_repair) => string.Format(
                        "select tw.description, case when tw.unit = 0 then 'шт'" +
                        " when tw.unit = 1 then 'нч' end as unit, tw.cost, cr.number" +
                        " from type_of_work as tw, card_of_rep_and_works as cr" +
                        " where cr.id_card_of_repair = {0} and tw.id_work = cr.id_work and tw.malf_or_spare = '1';", id_repair);
        public string queryForStaffView(int id_repair) => string.Format("select tub_numb, name, address, prof, phone" +
                         " from staff_view as s, cards_and_staff as cs" +
                         " where cs.cardofrepair_id_card = {0} and s.tub_numb = cs.staff_tub_numb", id_repair);

        private void btnAddNewPosition_Click(object sender, EventArgs e)
        {
            Form1.AddOrEdit = (int) Form1.AddEditOrDelete.Add;
            switch (Form1.WindowIndex)
            {
                case (int)Form1.WindowsStruct.MalfAdd:
                    FormAddPrice = new FormAddPrice(mainForm, this);
                    FormAddPrice.ShowDialog();
                    break;
                case (int)Form1.WindowsStruct.SpareAdd:
                    FormAddPrice = new FormAddPrice(mainForm, this);
                    FormAddPrice.ShowDialog();
                    break;
            }
        }
        private void btnEditPosition_Click(object sender, EventArgs e)
        {
            Form1.AddOrEdit = (int)Form1.AddEditOrDelete.Edit;
            switch (Form1.WindowIndex)
            {
                case (int)Form1.WindowsStruct.MalfAdd:
                    FormAddPrice = new FormAddPrice(mainForm, this);
                    mainForm.FillFormPrice(dataGridView, Form1.db, FormAddPrice);
                break;
                case (int)Form1.WindowsStruct.SpareAdd:
                    FormAddPrice = new FormAddPrice(mainForm, this);
                    mainForm.FillFormPrice(dataGridView, Form1.db, FormAddPrice);
                break;
            }
        }
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string query;
            switch (Form1.WindowIndex)
            {
                case ((int) Form1. WindowsStruct.MalfAdd):
                    query = $"select * from type_of_work_view where upper(description) " +
                        $"LIKE '%{textBoxSearch.Text.ToUpper()}%'";
                    Form1.AddListMalfunctionsInGrid(dataGridView, query);
                    break;
                case ((int)Form1.WindowsStruct.SpareAdd):
                    query = $"select * from simple_spares_view where upper(description) " +
                        $"LIKE '%{textBoxSearch.Text.ToUpper()}%'";
                    Form1.AddListMalfunctionsInGrid(dataGridView, query);
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
    }
}
