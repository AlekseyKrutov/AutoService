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
        private void FormForSelect_Load(object sender, EventArgs e)
        {
            dataGridView.RowHeadersVisible = false;
            dataGridView.RowTemplate.Height = 30;
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
                    Form1.AddListMalfunctionsInGrid(dataGridView, Form1.queryForMalfunctions);
                    break;
                case (int)Form1.WindowsStruct.MalfView:
                    Form1.AddListMalfunctionsInGrid(dataGridView, queryForMalfView(FormAddRepair.id_repair));
                    break;
                case (int)Form1.WindowsStruct.SpareAdd:
                    Form1.AddSparePartInStock(dataGridView, Form1.queryForSparePart);
                    break;
                case (int)Form1.WindowsStruct.SpareView:
                    Form1.AddSparePartInStock(dataGridView, queryForSpareView(FormAddRepair.id_repair));
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
        private void dataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
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
                        if (FormAddRepair.id_repair == 0 && Form1.AddOrEdit == (int) Form1.AddEditOrDelete.Add)
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
                                FormAddRepair.ExecuteProcedureDelete(FormAddRepair.id_repair, int.Parse(selectedRowOneCellValue), "", "DELETE_SPARE_WORKS");
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
        }
        public bool AnswerAboutDeleting => (MessageBox.Show($"Вы действительно хотите удалить\n" +
                    $" {dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString()}\n" +
                    $" {dataGridView.Rows[Form1.SelectIndex].Cells[1].Value.ToString()}", "Предупреждение",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == DialogResult.OK);
        public string queryForMalfView(int id_repair) => string.Format("select tw.description, tw.unit, tw.cost, cr.number" +
                        " from type_of_work as tw, card_of_rep_and_works as cr" +
                        " where cr.id_card_of_repair = {0} and tw.id_work = cr.id_work;", id_repair);
        public string queryForSpareView(int id_repair) => string.Format("select sr.uniq_code, s.description, sr.number, s.cost" +
                        " from stock_view as s, sparepart_and_card_of_rep as sr" +
                        " where sr.id_card_of_repair = {0} and s.uniq_code = sr.uniq_code;", id_repair);
        public string queryForStaffView(int id_repair) => string.Format("select tub_numb, name, address, prof, phone" +
                         " from staff_view as s, cards_and_staff as cs" +
                         " where cs.cardofrepair_id_card = {0} and s.tub_numb = cs.staff_tub_numb", id_repair);
    }
}
