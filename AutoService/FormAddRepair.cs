using AutoService;
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
using AutoServiceLibrary;

namespace AutoService
{
    public partial class FormAddRepair : Form
    {
        public static bool logicForAddRepair;

        FormAddAuto formAddAutoInRepairs;
        FormForSelect formSelectAuto;
        FormForSelect formSelectWorker;
        FormForSelect formSelectMalf;
        FormForSelect formViewMalf;
        Form1 mainForm;

        public FormAddRepair()
        {
            InitializeComponent();
        }
        public FormAddRepair(FormAddAuto addAuto, Form1 mainForm)
        {
            foreach (Malfunctions malf in Malfunctions.MalfList) 
            {
                Form1.malfListForRepairAll.Add(malf);
            }
            formAddAutoInRepairs = addAuto;
            this.mainForm = mainForm;
            InitializeComponent();
        }
        private void FormAddRepair_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1.malfListForRepairAll.Clear();
            Form1.malfListForRepairAdded.Clear();
        }
        private void btnAddNewAutoRepair_Click(object sender, EventArgs e)
        {
            Form1.WindowIndex = (int)Form1.WindowsStruct.Repairs;
            if (!formAddAutoInRepairs.Visible)
            {   
                logicForAddRepair = true;
                formAddAutoInRepairs = new FormAddAuto(this, mainForm);
                formAddAutoInRepairs.StartPosition = FormStartPosition.CenterScreen;
                formAddAutoInRepairs.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                formAddAutoInRepairs.MaximizeBox = false;
                formAddAutoInRepairs.ShowDialog();
            }
            else
                return;
        }

        private void btnSelExistAutoRepair_Click(object sender, EventArgs e)
        {
            Form1.SelectIndex = 0;
            Form1.WindowIndex = (int)Form1.WindowsStruct.Repairs;
            formSelectAuto = new FormForSelect(this, mainForm);
            formSelectAuto.ShowDialog();
        }

        private void btnSelectPersonal_Click(object sender, EventArgs e)
        {
            Form1.WindowIndex = (int) Form1.WindowsStruct.Worker;
            formSelectWorker = new FormForSelect(this, mainForm);
            formSelectWorker.ShowDialog();
        }

        private void btnAddMalf_Click(object sender, EventArgs e)
        {
            Form1.SelectIndex = 0;
            Form1.WindowIndex = (int)Form1.WindowsStruct.MalfAdd;
            formSelectMalf = new FormForSelect(this, mainForm);
            formSelectMalf.ShowDialog();
        }

        private void btnShowMalf_Click(object sender, EventArgs e)
        {
            Form1.WindowIndex = (int)Form1.WindowsStruct.MalfView;
            formViewMalf = new FormForSelect(this, mainForm);
            formViewMalf.ShowDialog();
        }
        //событие при нажатии на кнопку добавить ремонт
        private void CreateRepair_Click(object sender, EventArgs e)
        {
            if (textBoxMark.Text.Length == 0)
            {
                MessageBox.Show("Пожалуйста выберете автомобиль!");
                return;
            }
            else if (Form1.malfListForRepairAdded.Count == 0)
            {
                MessageBox.Show("Пожалуйста выберете неисправности!");
                return;
            }
            else if (SelectedPersonLabel.Text.Length == 0)
            {
                MessageBox.Show("Пожалуйста выберете работника!");
                return;
            }
            CardOfRepair.Number++;
            CardOfRepair.repairsList.Sort(
                delegate(CardOfRepair card1, CardOfRepair card2)
                {
                    if (!card1.RepairIsCurrent)
                        return 1;
                    if (card1.RepairIsCurrent)
                        return -1;
                    else
                        return 0;
                });
            AddListRepairsInGrid();
            Form1.malfListForRepairAll.Clear();
            Form1.malfListForRepairAdded.Clear();
            this.Close();
        }
        public void AddListRepairsInGrid()
        {
            mainForm.dataGridView.Rows.Clear();
            foreach (CardOfRepair repair in CardOfRepair.repairsList)
            {
                if (repair.RepairIsCurrent)
                {
                    mainForm.dataGridView.Rows.Add(repair.NumberOfAct, repair.TimeOfStart, repair.MalfunctionsToString(), repair.CarInRepairToString(), repair.Mechanic.Name, repair.Notes);
                }
            }
        }

    }
}
