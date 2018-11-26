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
                    Form1.AddListMalfunctionsInGrid(dataGridView);
                    break;
                case (int)Form1.WindowsStruct.MalfView:
                    dataGridView.Rows.Clear();
                    foreach (Malfunctions malf in Form1.malfListForRepairAdded)
                    {
                        dataGridView.Rows.Add(malf.DescriptionOfMalf, malf.Price);
                    }
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
                        FormAddRepair.textBoxMark.Text = Car.CarList[Form1.SelectIndex].CarMark;
                        FormAddRepair.textBoxVIN.Text = Car.CarList[Form1.SelectIndex].CarVIN;
                        FormAddRepair.textBoxReg.Text = Car.CarList[Form1.SelectIndex].RegCertific;
                        FormAddRepair.textBoxGosNom.Text = Car.CarList[Form1.SelectIndex].NumberOfCar;
                        FormAddRepair.textBoxOwner.Text = Car.CarList[Form1.SelectIndex].Owner.Name;
                        break;
                    case (int)Form1.WindowsStruct.Auto:
                        FormAddAuto.labelContentOwner.Text = dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString();
                        FormAddAuto.OwnerSelected = true;
                        break;
                    case (int)Form1.WindowsStruct.Worker:
                        FormAddRepair.SelectedPersonLabel.Text = Personal.PersonalList[Form1.SelectIndex].Name;
                        break;
                    case (int)Form1.WindowsStruct.MalfAdd:
                        Form1.malfListForRepairAdded.Add(Form1.malfListForRepairAll[Form1.SelectIndex]);
                        Form1.malfListForRepairAll.RemoveAt(Form1.SelectIndex);
                        break;
                    case (int)Form1.WindowsStruct.MalfView:
                        if ((MessageBox.Show(string.Format("Вы действительно хотите удалить этот ремонт из списка?{0}", Form1.malfListForRepairAdded[Form1.SelectIndex].ToString()), "Предупреждение",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == DialogResult.OK) && Form1.malfListForRepairAdded.Count > 0)
                        {
                            Form1.malfListForRepairAll.Add(Form1.malfListForRepairAdded[Form1.SelectIndex]);
                            Form1.malfListForRepairAdded.RemoveAt(Form1.SelectIndex);
                        }
                        break;
                }
                Form1.WindowIndex = 0;
                this.Visible = false;
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
        }
    }
}
