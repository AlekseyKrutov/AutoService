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
using DataMapper;
using DbProxy;

namespace AutoService
{
    public partial class FormAddRepair : Form
    {
        public CardOfRepair repair = null;
        public static bool logicForAddRepair;
        FormAddAuto formAddAutoInRepairs;
        FormForSelect formForSelect;
        Form1 mainForm;
        int infTxtWidth = 0;
        int fullWidth = 0;
        int cutedWidth = 0;
        static public AddEditOrDelete addOrEditInRepair;
        public int id_repair; 
        public FormAddRepair()
        {
            InitializeComponent();
            //расчет поля для изменения ширины формы, если информация по ремонту отсутствует
            if (Form1.WindowIndex == WindowsStruct.ActOfEndsRepairs)
            {
                btnAddNewAutoRepair.Visible = false;
                btnSelExistAutoRepair.Visible = false;
                btnAddRepair.Visible = false;
                btnAddMalf.Visible = false;
                btnSelSparePart.Visible = false;
                btnSelectPersonal.Visible = false;
                btnShowMalf.Visible = false;
                btnShowSparePart.Visible = false;
                btnShowWorker.Visible = false;
                dateTimeStart.Visible = false;
                dateTimeFinish.Visible = false;
                textBoxNotes.Visible = false;
                textBoxGosNom.Visible = false;
                textBoxMark.Visible = false;
                textBoxOwner.Visible = false;
                textBoxReg.Visible = false;
                textBoxVIN.Visible = false;
                labelFinishTime.Visible = false;
                labelNotes.Visible = false;
                labelStartTime.Visible = false;
                textBoxInf.Location = new Point(0, 0);
                this.Width = 0;
                this.Height = textBoxInf.Height + 30;
                return;
            }
            fullWidth = this.Width;
            infTxtWidth = textBoxInf.Location.X - (textBoxNotes.Location.X + textBoxNotes.Width);
            this.Width -= infTxtWidth + textBoxInf.Width;
            cutedWidth = this.Width;
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
        }
        private void FormAddRepair_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!btnAddNewAutoRepair.Visible)
            {
                Form1.WindowIndex = WindowsStruct.ActOfEndsRepairs;
                return;
            }
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
            if (textBoxMark.Text.Length == 0)
            {
                MessageBox.Show("Пожалуйста выберете автомобиль!");
                return;
            }
            if (dateTimeFinish.Checked && !dateTimeStart.Checked)
            {
                MessageBox.Show("Выберете дату начала ремонта!");
                return;
            }
            if (dateTimeStart.Checked && dateTimeFinish.Checked 
                && (dateTimeStart.Value.Date > dateTimeFinish.Value.Date))
            {
                MessageBox.Show("Дата начала ремонта меньше даты завершения!");
                return;
            }
            if (!dateTimeStart.Checked && !dateTimeFinish.Checked)
            {
                repair.TimeOfStart = DateTime.Now;
                repair.TimeOfFinish = null;
            }
            if (dateTimeStart.Checked && !dateTimeFinish.Checked)
            {
                repair.TimeOfStart = dateTimeStart.Value;
                repair.TimeOfFinish = null;
            }
            else if (dateTimeStart.Checked && dateTimeFinish.Checked)
            {
                repair.TimeOfStart = dateTimeStart.Value;
                repair.TimeOfFinish = dateTimeFinish.Value;
            }
            repair.Notes = textBoxNotes.Text;
            CardMapper cm = new CardMapper();
            repair.CalculateTotalPrice();
            if (Form1.AddOrEdit == AddEditOrDelete.Add)
                repair = cm.Insert(repair);
            else if (Form1.AddOrEdit == AddEditOrDelete.Edit)
                cm.Update(repair);
            Form1.AddListRepairsInGrid(mainForm.dataGridView);
            this.Close();
        }
        private void textBoxInf_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text != "" && this.Width != fullWidth)
            {
                this.Width += infTxtWidth + textBoxInf.Width;
                CenterToScreen();
            }
            else if (textBox.Text == "" && this.Width != cutedWidth)
            {
                this.Width -= infTxtWidth + textBoxInf.Width;
                CenterToScreen();
            }
        }
    }
}
