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
using DbProxy;

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
                case WindowsStruct.MalfAdd:
                    AddMalfOrSparesInDB();
                    Form1.AddListMalfunctionsInGrid(formForSelect.dataGridView);
                    break;
                case WindowsStruct.MalfView:
                    AddMalfOrSparesInDB();
                    Form1.AddListMalfunctionsInGrid(formForSelect.dataGridView, formAddRepair.id_repair.ToString());
                    break;
                case WindowsStruct.SpareAdd:
                    AddMalfOrSparesInDB();
                    Form1.AddListMalfunctionsInGrid(formForSelect.dataGridView);
                    break;
                case WindowsStruct.SpareView:
                    AddMalfOrSparesInDB();
                    Form1.AddListMalfunctionsInGrid(formForSelect.dataGridView, formAddRepair.id_repair.ToString());
                    break;
            }
            this.Close();
        }

        private void AddMalfOrSparesInDB()
        {
            Malfunctions malfAdd = new Malfunctions(formForSelect.dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString(),
                    int.Parse(numberUpDown.Value.ToString()));
            InvokeProcedure.AddMalfInRep(formAddRepair.id_repair, malfAdd.DescriptionOfMalf, malfAdd.Number);
        }
    }
}
