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
using FirebirdSql.Data.FirebirdClient;
using AutoServiceLibrary;
using DbProxy;

namespace AutoService
{
    public partial class FormAddRepair : Form
    {
        public static bool logicForAddRepair;
        FormAddAuto formAddAutoInRepairs;
        FormForSelect formForSelect;
        Form1 mainForm;
        int infTxtWidth = 0;
        static public AddEditOrDelete addOrEditInRepair;
        public int id_repair; 
        public FormAddRepair()
        {
            InitializeComponent();
            //расчет поля для изменения ширины формы, если информация по ремонту отсутствует
            //infTxtWidth = textBoxInf.Location.X - (textBoxNotes.Location.X + textBoxNotes.Width);
            //this.Width -= infTxtWidth + textBoxInf.Width;
        }
        public FormAddRepair(FormAddAuto addAuto, Form1 mainForm) : this()
        {
            formAddAutoInRepairs = addAuto;
            this.mainForm = mainForm;
            formForSelect = new FormForSelect(this, mainForm);
        }
        private void FormAddRepair_Load(object sender, EventArgs e)
        {
            if (Form1.AddOrEdit == AddEditOrDelete.Edit &&
                Form1.WindowIndex == WindowsStruct.ActOfEndsRepairs)
                this.Text = "Просмотр ремонта";
            else if (Form1.AddOrEdit == AddEditOrDelete.Edit)
                this.Text = "Редактирование ремонта";
            if (Form1.WindowIndex == WindowsStruct.ActOfEndsRepairs)
            {
                btnAddNewAutoRepair.Enabled = false;
                btnSelExistAutoRepair.Enabled = false;
                btnAddRepair.Visible = false;
                btnAddMalf.Visible = false;
                btnSelSparePart.Visible = false;
                btnSelectPersonal.Visible = false;
                btnShowMalf.Location = btnAddMalf.Location;
                btnShowSparePart.Location = btnSelSparePart.Location;
                btnShowWorker.Location = btnSelectPersonal.Location;
                checkBoxTurnTime.Visible = false;
                dateTimeStart.Enabled = false;
                dateTimeFinish.Enabled = false;
                textBoxNotes.Enabled = false;
            }
        }
        private void FormAddRepair_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!btnAddNewAutoRepair.Enabled)
            {
                Form1.WindowIndex = WindowsStruct.ActOfEndsRepairs;
                return;
            }
            if (e.CloseReason == CloseReason.UserClosing && addOrEditInRepair == AddEditOrDelete.Add)
                InvokeProcedure.DeleteSimpleRepair(id_repair);
            Form1.WindowIndex = WindowsStruct.Repairs;
        }
        private void btnAddNewAutoRepair_Click(object sender, EventArgs e)
        {
            Form1.WindowIndex = WindowsStruct.AddAutoInRep;  
            logicForAddRepair = true;
            formAddAutoInRepairs = new FormAddAuto(this, mainForm);
            formAddAutoInRepairs.ShowDialog();
        }

        private void btnSelExistAutoRepair_Click(object sender, EventArgs e)
        {
            Form1.SelectIndex = 0;
            Form1.WindowIndex = WindowsStruct.ViewAutoInRep;
            formForSelect.ShowDialog();
        }

        private void btnSelectPersonal_Click(object sender, EventArgs e)
        {
            if (AutoNotSelected())
                return;
            Form1.SelectIndex = 0;
            Form1.WindowIndex = WindowsStruct.WorkerAdd;
            formForSelect.ShowDialog();
        }
        private void btnShowWorker_Click(object sender, EventArgs e)
        {
            if (AutoNotSelected())
                return;
            Form1.SelectIndex = 0;
            Form1.WindowIndex = WindowsStruct.WorkerView;
            formForSelect.ShowDialog();
        }
        private void btnAddMalf_Click(object sender, EventArgs e)
        {
            if (AutoNotSelected())
                return;
            Form1.SelectIndex = 0;
            Form1.WindowIndex = WindowsStruct.MalfAdd;
            formForSelect.ShowDialog();
        }

        private void btnShowMalf_Click(object sender, EventArgs e)
        {
            if (AutoNotSelected())
                return;
            Form1.WindowIndex = WindowsStruct.MalfView;
            formForSelect.ShowDialog();
        }
        private void btnSelSparePart_Click(object sender, EventArgs e)
        {
            if (AutoNotSelected())
                return;
            Form1.SelectIndex = 0;
            Form1.WindowIndex = WindowsStruct.SpareAdd;
            formForSelect.ShowDialog();
        }
        private void btnShowSparePart_Click(object sender, EventArgs e)
        {
            if (AutoNotSelected())
                return;
            Form1.SelectIndex = 0;
            Form1.WindowIndex = WindowsStruct.SpareView;
            formForSelect.ShowDialog();
        }
        private bool AutoNotSelected()
        {
            if (textBoxGosNom.Text.Length == 0)
            {
                MessageBox.Show("Сначала необходимо выбрать автомобиль");
                return true;
            }
            else
                return false;
        }
        //событие при нажатии на кнопку добавить ремонт
        private void btnAddRepair_Click(object sender, EventArgs e)
        {
            DateTime? startDate;
            DateTime? finishDate;
            if (textBoxMark.Text.Length == 0)
            {
                MessageBox.Show("Пожалуйста выберете автомобиль!");
                return;
            }
            if (checkBoxTurnTime.Checked && (dateTimeStart.Value.Date > dateTimeFinish.Value.Date))
            {
                MessageBox.Show("Дата начала ремонта меньше даты завершения!");
                return;
            }
            if (!checkBoxTurnTime.Checked)
            {
                startDate = DateTime.Now;
                finishDate = null;
            }
            else if (checkBoxTurnTime.Checked)
            {
                startDate = dateTimeStart.Value;
                finishDate = dateTimeFinish.Value;
            }
            else
            {
                startDate = null;
                finishDate = null;
            }
            InvokeProcedure.AddRepair(id_repair, textBoxGosNom.Text, textBoxNotes.Text, startDate, finishDate);
            Form1.AddListRepairsInGrid(mainForm.dataGridView);
            this.FormClosing -= FormAddRepair_FormClosing;
            this.Close();
        }
        private void checkBoxTurnTime_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTurnTime.Checked)
            {
                dateTimeStart.Enabled = true;
                dateTimeFinish.Enabled = true;
            }
            else
            {
                dateTimeStart.Enabled = false;
                dateTimeFinish.Enabled = false;
            }
        }
    }
}
