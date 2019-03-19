using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoServiceLibrary;

namespace AutoService
{
    public partial class FormAddNumber : Form
    {
        FormForSelect formForSelect;
        FormAddRepair formAddRepair;
        public FormAddNumber()
        {
            InitializeComponent();
        }
        public FormAddNumber(FormForSelect formForSelect, FormAddRepair formAddRepair) : this ()
        {
            this.formForSelect = formForSelect;
            this.formAddRepair = formAddRepair;
        }
        private void btnAddNumber_Click(object sender, EventArgs e)
        {
            switch (Form1.WindowIndex)
            {
                case (int) Form1.WindowsStruct.MalfAdd:
                    AddMalfOrSparesInDB(Form1.queryForMalfunctions);
                    Form1.AddListMalfunctionsInGrid(formForSelect.dataGridView, Form1.queryForMalfunctions);
                    break;
                case (int)Form1.WindowsStruct.MalfView:
                    AddMalfOrSparesInDB(formForSelect.queryForMalfView(formAddRepair.id_repair));
                    Form1.AddListMalfunctionsInGrid(formForSelect.dataGridView, 
                        formForSelect.queryForMalfView(formAddRepair.id_repair));
                    break;
                case (int)Form1.WindowsStruct.SpareAdd:
                    AddMalfOrSparesInDB(Form1.queryForSpares);
                    Form1.AddListMalfunctionsInGrid(formForSelect.dataGridView, Form1.queryForSpares);
                    break;
                case (int)Form1.WindowsStruct.SpareView:
                    AddMalfOrSparesInDB(formForSelect.queryForSpareView(formAddRepair.id_repair));
                    Form1.AddListMalfunctionsInGrid(formForSelect.dataGridView,
                        formForSelect.queryForSpareView(formAddRepair.id_repair));
                    break;
            }
            this.Close();
        }

        private void AddMalfOrSparesInDB(string query)
        {
            Malfunctions malfAdd = new Malfunctions(formForSelect.dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString(),
                    int.Parse(numberUpDown.Value.ToString()));
            formAddRepair.ExecuteProcedureForAddMalf(formAddRepair.id_repair, malfAdd.DescriptionOfMalf, malfAdd.Number);
        }
    }
}
