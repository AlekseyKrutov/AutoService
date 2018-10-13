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
using System.Text.RegularExpressions;

namespace AutoService
{
    public partial class FormAddAuto : Form
    {
        FormForSelect formSelectAuto;
        FormAddRepair FormAddRepairInCar;
        Form1 mainForm;
        public FormAddAuto(FormAddRepair addRepair, Form1 mainForm)
        {
            FormAddRepairInCar = addRepair;
            this.mainForm = mainForm; 
            InitializeComponent();
        }
        public FormAddAuto()
        {
            
            InitializeComponent();
        }
        private void buttonAddAuto_Click(object sender, EventArgs e)
        {
            if (textBoxGosNumb.Text.Length == 0 || textBoxMark.Text.Length == 0 || textBoxModel.Text.Length == 0 ||
                textBoxReg.Text.Length == 0 || textBoxVIN.Text.Length == 0 || labelContentOwner.Text == "Выберите владельца")
            {
                MessageBox.Show("Вы ввели не все данные!");
                return;
            }
            if (!FormAddRepairInCar.Visible)
            {
                Car.CarList.Add(new Car(textBoxVIN.Text, textBoxReg.Text, textBoxMark.Text, textBoxModel.Text, textBoxGosNumb.Text, Client.ClientList[Form1.SelectIndex]));
                AddListAutoInGrid();
                this.Visible = false;
                Form1.SelectIndex = 0;
            }
            else
            {
                //запись данных в текстовые поля добавления ремонтов
                Car.CarList.Add(new Car(textBoxVIN.Text, textBoxReg.Text, textBoxMark.Text, textBoxModel.Text, textBoxGosNumb.Text, Client.ClientList[Form1.SelectIndex]));
                FormAddRepairInCar.textBoxMark.Text = Car.CarList.FindLast(delegate { return true; }).CarMark;
                FormAddRepairInCar.textBoxModel.Text = Car.CarList.FindLast(delegate { return true; }).CarModel;
                FormAddRepairInCar.textBoxVIN.Text = Car.CarList.FindLast(delegate { return true; }).CarVIN;
                FormAddRepairInCar.textBoxReg.Text = Car.CarList.FindLast(delegate { return true; }).RegCertific;
                FormAddRepairInCar.textBoxGosNom.Text = Car.CarList.FindLast(delegate { return true; }).NumberOfCar;
                FormAddRepairInCar.textBoxOwner.Text = Car.CarList.FindLast(delegate { return true; }).Owner.Name;
                this.Visible = false;
            }
            
        }
        public void AddListAutoInGrid()
        {
            mainForm.dataGridView.Rows.Clear();
            foreach (Car car in Car.CarList)
            {
                mainForm.dataGridView.Rows.Add(car.CarMark, car.CarModel, car.CarVIN, car.RegCertific, car.NumberOfCar, car.Owner.Name);
            }
        }

        private void buttonAddOwner_Click(object sender, EventArgs e)
        {
            if (Client.ClientList.Count == 0)
            {
                return;
            }
            Form1.SelectIndex = 0;
            Form1.WindowIndex = (int)Form1.WindowsStruct.Auto;
            formSelectAuto = new FormForSelect(this);
            formSelectAuto.ShowDialog();
        }

        private void textBoxVIN_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationOnlyUpCharAndNumb(e);
        }

        private void textBoxReg_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationOnlyUpCharAndNumb(e);
        }

        private void textBoxGosNumb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsLetter(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
            if (Char.IsLetter(e.KeyChar))
            {
                if (!Regex.Match(e.KeyChar.ToString(), @"[а-яА-Я]").Success)
                {
                    e.Handled = true;
                }
            }
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void textBoxGosNumb_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Space)
            {
                e.Handled = true;
            }
            if (Char.IsLetter(e.KeyChar))
            {
                e.KeyChar = Char.ToUpper(e.KeyChar);
            }
        }
        
        private void ValidationOnlyUpCharAndNumb(KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
            if (!Char.IsNumber(e.KeyChar) && !Char.IsLetter(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }      
    }
}
