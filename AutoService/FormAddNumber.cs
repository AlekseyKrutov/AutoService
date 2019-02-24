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
                    Malfunctions malfAdd = new Malfunctions(formForSelect.dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString(),
                    int.Parse(numberUpDown.Value.ToString()));
                    formAddRepair.ExecuteProcedureForAddMalf(formAddRepair.id_repair, malfAdd.DescriptionOfMalf, malfAdd.Number);
                    Form1.AddListMalfunctionsInGrid(formForSelect.dataGridView, Form1.queryForMalfunctions);
                    this.Close();
                    break;
                case (int)Form1.WindowsStruct.MalfView:
                    Malfunctions malfView = new Malfunctions(formForSelect.dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString(),
                    int.Parse(numberUpDown.Value.ToString()));
                    formAddRepair.ExecuteProcedureForAddMalf(formAddRepair.id_repair, malfView.DescriptionOfMalf, malfView.Number);
                    Form1.AddListMalfunctionsInGrid(formForSelect.dataGridView, formForSelect.queryForMalfView(formAddRepair.id_repair));
                    this.Close();
                    break;
                case (int)Form1.WindowsStruct.SpareAdd:
                    SparePart sparePartAdd = new SparePart(formForSelect.dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString(), int.Parse(numberUpDown.Value.ToString()));
                    this.Close();
                    formAddRepair.ExecuteProcedureForAddSparepart(formAddRepair.id_repair, int.Parse(sparePartAdd.Articul), sparePartAdd.Number);
                    Form1.AddSparePartInStock(formForSelect.dataGridView, Form1.queryForSparePart);
                    this.Close();
                    break;
                case (int)Form1.WindowsStruct.SpareView:
                    SparePart sparePartView = new SparePart(formForSelect.dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString(),
                    int.Parse(numberUpDown.Value.ToString()));
                    this.Close();
                    formAddRepair.ExecuteProcedureForAddSparepart(formAddRepair.id_repair, int.Parse(sparePartView.Articul), sparePartView.Number);
                    Form1.AddSparePartInStock(formForSelect.dataGridView, formForSelect.queryForSpareView(formAddRepair.id_repair));
                    this.Close();
                    break;

            }
        }
    }
}
