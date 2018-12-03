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
        string query;
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
            dataGridView.ClearSelection();
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
                    Form1.AddListPersonalInGrid(dataGridView);
                    break;
                case (int)Form1.WindowsStruct.MalfAdd:
                    Form1.AddListMalfunctionsInGrid(dataGridView, Form1.queryForMalfunctions);
                    break;
                case (int)Form1.WindowsStruct.MalfView:
                    query = string.Format("select tw.description, tw.unit, tw.cost, cr.number" +
                        " from type_of_work as tw, card_of_rep_and_works as cr" +
                        " where cr.id_card_of_repair = {0} and tw.id_work = cr.id_work;", FormAddRepair.id_repair);
                    Form1.AddListMalfunctionsInGrid(dataGridView, query);
                    break;
                case (int)Form1.WindowsStruct.SpareAdd:
                    Form1.AddSparePartInStock(dataGridView, Form1.queryForSparePart);
                    break;
                case (int)Form1.WindowsStruct.SpareView:
                    query = string.Format("select sr.uniq_code, s.description, sr.number, s.cost" +
                        " from stock_view as s, sparepart_and_card_of_rep as sr" +
                        " where sr.id_card_of_repair = {0} and s.uniq_code = sr.uniq_code;", FormAddRepair.id_repair);
                    Form1.AddSparePartInStock(dataGridView, query);
                    break;
            }
            Form1.SelectIndex = 0;
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Form1.SelectIndex = e.RowIndex;
                //оператор для определения места вызова
                switch (Form1.WindowIndex)
                {
                    case (int)Form1.WindowsStruct.Repairs:
                        string car_vin = dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString();
                        FormAddAuto.ReadAutoFromViewForRepair(car_vin, FormAddRepair);
                        FormAddRepair.id_repair = FormAddRepair.GetIdRepair(car_vin);
                        this.Close();
                        break;
                    case (int)Form1.WindowsStruct.Auto:
                        FormAddAuto.labelContentOwner.Text = dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString();
                        FormAddAuto.OwnerSelected = true;
                        break;
                    case (int)Form1.WindowsStruct.Worker:
                        FormAddRepair.SelectedPersonLabel.Text = Personal.PersonalList[Form1.SelectIndex].Name;
                        break;
                    case (int)Form1.WindowsStruct.MalfAdd:
                        FormAddNumber formAddNumber = new FormAddNumber(this, FormAddRepair);
                        formAddNumber.ShowDialog();
                        break;
                    case (int)Form1.WindowsStruct.MalfView:
                        formAddNumber = new FormAddNumber(this, FormAddRepair);
                        formAddNumber.ShowDialog();
                        break;
                    case (int)Form1.WindowsStruct.SpareAdd:
                        formAddNumber = new FormAddNumber(this, FormAddRepair);
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
            Form1.WindowIndex = (int)Form1.WindowsStruct.Repairs;
        }
    }
}
