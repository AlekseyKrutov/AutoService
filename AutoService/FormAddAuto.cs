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
using DataMapper;
using FirebirdSql.Data.FirebirdClient;
using AutoServiceLibrary;
using DbProxy;

namespace AutoService
{
    public partial class FormAddAuto : Form
    {
        FormForSelect formSelectAuto;
        FormAddRepair formAddCarInRepair;
        FormAddWayBill formAddWayBill;

        public Car car = null;
        public Client client = null;

        int selectedIndex;
        public bool OwnerSelected = false;
        Form1 mainForm;
        public FormAddAuto()
        {
            InitializeComponent();
            DataSets.CreateDsForComboBox(comboBoxAuto, Queries.CarModelView, "MARK_MODEL", "", AddEditOrDelete.Add);
        }
        public FormAddAuto(Form1 mainForm) : this()
        {
            this.mainForm = mainForm;
        }
        public FormAddAuto(FormAddRepair addRepair, Form1 mainForm) : this ()
        {
            formAddCarInRepair = addRepair;
            this.mainForm = mainForm; 
        }
        public FormAddAuto(FormAddWayBill formAddWayBill) : this ()
        {
            this.formAddWayBill = formAddWayBill;
        }
        private void FormAddAuto_Load(object sender, EventArgs e)
        {
            selectedIndex = Form1.SelectIndex;
        }
        private void buttonAddAuto_Click(object sender, EventArgs e)
        {
            if (textBoxGosNumb.Text.Length == 0 || comboBoxAuto.Text.Length == 0 || 
               (textBoxReg.Text.Length > 0 && textBoxReg.Text.Length < 10))
            {
                MessageBox.Show("Вы ввели не все данные!");
                return;
            }
            string[] model = comboBoxAuto.Text.Split(' ');
            string carMark = model.First();
            string carModel = (model.Length > 1) ? model.Last() : "";
            if (car == null)
            {
                car = new Car(textBoxVIN.Text, textBoxReg.Text, 
                    carMark, carModel, textBoxGosNumb.Text, client);
                try
                {
                    car = new CarMapper().Insert(car);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("При добавлении данных произошла ошибка - " +
                        $"{ex.Message}");
                    car = null;
                    return;
                }
                if (formAddCarInRepair != null && formAddCarInRepair.Visible)
                {
                    Form1.WindowIndex = WindowsStruct.Repairs;
                    formAddCarInRepair.repair.Car = car;
                    mainForm.FillCardWithCar(formAddCarInRepair, formAddCarInRepair.repair.Car);
                    this.Close();
                    return;
                }
                if (formAddWayBill != null)
                {
                    formAddWayBill.FillComboBox(formAddWayBill.comboBoxCar, Form1.db,
                        formAddWayBill.car_query, formAddWayBill.displayMembers[FormAddWayBill.DisplayMembers.Car],
                        formAddWayBill.valueMembers[FormAddWayBill.ValueMembers.Car]);
                    formAddWayBill.comboBoxCar.SelectedIndex = -1;
                    formAddWayBill.comboBoxCar.SelectedValue = textBoxGosNumb.Text;
                    this.Close();
                    return;
                }
            }
            else
            {
                CarMapper cm = new CarMapper();
                car.CarVIN = textBoxVIN.Text;
                car.CarMark = carMark;
                car.CarModel = carModel;
                car.NumberOfCar = textBoxGosNumb.Text;
                car.RegCertific = textBoxReg.Text;
                car.Owner = client;
                try
                {
                    cm.Update(car);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("При добавлении данных произошла ошибка - " +
                        $"{ex.Message}");
                    return;
                }
            }
            Form1.AddListAutoInGrid(mainForm.dataGridView);
            mainForm.dataGridView.ClearSelection();
            if (Form1.SelectIndex != 0)
                mainForm.dataGridView.Rows[Form1.SelectIndex].Selected = true;
            this.Close();
        }
        
        private void buttonAddOwner_Click(object sender, EventArgs e)
        {
            Form1.SelectIndex = 0;
            Form1.WindowIndex = WindowsStruct.AddClientInAuto;
            formSelectAuto = new FormForSelect(this, mainForm);
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
        private void FormAddAuto_FormClosed(object sender, FormClosedEventArgs e)
        {
            car = null;
            client = null;
        }
    }
}
