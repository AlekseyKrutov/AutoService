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
                    Malfunctions malf = new Malfunctions(formForSelect.dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString(),
                    int.Parse(numberUpDown.Value.ToString()));
                    formAddRepair.ExecuteProcedureForAddMalf(formAddRepair.id_repair, malf.DescriptionOfMalf, malf.Number);
                    this.Close();
                    break;
                case (int)Form1.WindowsStruct.SpareAdd:
                    formAddRepair.spareList.Add(
                    new SparePart(int.Parse(formForSelect.dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString()),
                    int.Parse(numberUpDown.Value.ToString())));
                    this.Close();
                    formForSelect.Close();
                    break;
            }
        }
    }
}
