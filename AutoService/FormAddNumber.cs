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
using DataMapper;
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
                    AddMalfOrSparesInRepair();
                    Form1.AddListMalfunctionsInGrid(formForSelect.dataGridView);
                    break;
                case WindowsStruct.MalfView:
                    AddMalfOrSparesInRepair();
                    formForSelect.AddListMalfInGrid();
                    break;
                case WindowsStruct.SpareAdd:
                    AddMalfOrSparesInRepair();
                    Form1.AddListMalfunctionsInGrid(formForSelect.dataGridView);
                    break;
                case WindowsStruct.SpareView:
                    AddMalfOrSparesInRepair();
                    formForSelect.AddListSpareInGrid();
                    break;
            }
            this.Close();
        }

        private void AddMalfOrSparesInRepair()
        {
            Malfunctions malfAdd = new MalfMapper().Get(formForSelect.dataGridView.SelectedRows[0].Cells[0].Value.ToString());
            malfAdd.Number = (int) numberUpDown.Value;
            Malfunctions malf = formAddRepair.repair.ListOfMalf.Find(m => m.IdMalf == malfAdd.IdMalf);
            if (malf == null)
                formAddRepair.repair.AddMalfInList(malfAdd);
            else
                malf.Number = (int) numberUpDown.Value;
            formAddRepair.repair.CalculateTotalPrice();
        }
    }
}
