using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;
using DataMapper;
using DbProxy;
using AutoServiceLibrary;

namespace AutoService
{
    public partial class FormAddWayBill : Form
    {
        public WayBill wayBill = null;
        public WayBill insWayBill = null;
        Form1 mainForm;
        public FormAddWayBill()
        {
            InitializeComponent();
            DbProxy.DataSets.CreateDsForComboBox(comboBoxCar, DbProxy.Queries.CarForCBox, "car", "state_number");
            DbProxy.DataSets.CreateDsForComboBox(comboBoxDriver, DbProxy.Queries.DriverForCBox, "name", "tub_numb");
            txtBoxCost.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressFloat);
            textBoxFuel.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressFloat);
            textBoxKm.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressOnlyNumb);
        }
        public FormAddWayBill(Form1 mainForm) : this()
        {
            this.mainForm = mainForm;
        }
        private void FormAddWayBill_Load(object sender, EventArgs e)
        {
        }

        private void btnCreateWayBill_Click(object sender, EventArgs e)
        {
            if (textBoxClient.Text == "" || textBoxTrip.Text == ""
                || comboBoxCar.Text == "" || comboBoxDriver.Text == ""
                || !pickerStart.Checked)
            {
                MessageBox.Show("Вы ввели не все данные!");
                return;
            }
            if (pickerEnd.Checked && pickerEnd.Value < pickerStart.Value)
            {
                MessageBox.Show("Дата выгрузки не может быть меньше даты закрузки!");
            }
            WayBillMapper wm = new WayBillMapper();
            if (wayBill == null)
            {
                insWayBill.Car = new CarMapper().Get(comboBoxCar.SelectedValue.ToString());
                insWayBill.Driver = new EmployeeMapper().Get(comboBoxDriver.SelectedValue.ToString());
                insWayBill.LoadDate = pickerStart.Value;
                if (!pickerEnd.Checked)
                    insWayBill.UnloadDate = null;
                else
                    insWayBill.UnloadDate = pickerEnd.Value;
                insWayBill.BaseDocument = textBoxBaseDoc.Text;
                insWayBill.Kilometers = int.Parse(textBoxKm.Text);
                insWayBill.Cost = double.Parse(txtBoxCost.Text);
                insWayBill.Fuel = float.Parse(textBoxFuel.Text);
                insWayBill.Notes = textBoxNotes.Text;
                try
                {
                    wm.Insert(insWayBill);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            else
            {
                wayBill.Car = new CarMapper().Get(comboBoxCar.SelectedValue.ToString());
                wayBill.Driver = new EmployeeMapper().Get(comboBoxDriver.SelectedValue.ToString());
                wayBill.LoadDate = pickerStart.Value;
                if (!pickerEnd.Checked)
                    wayBill.UnloadDate = null;
                else
                    wayBill.UnloadDate = pickerEnd.Value;
                wayBill.BaseDocument = textBoxBaseDoc.Text;
                wayBill.Kilometers = int.Parse(textBoxKm.Text);
                wayBill.Cost = double.Parse(txtBoxCost.Text);
                wayBill.Fuel = float.Parse(textBoxFuel.Text);
                wayBill.Notes = textBoxNotes.Text;
                try
                {
                    wm.Update(wayBill);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            Form1.WindowIndex = WindowsStruct.ActiveWayBills;
            DataSets.CreateDSForDataGrid(Form1.WindowIndex, mainForm.dataGridView);
            this.Close();
        }
        private void btnAddRoute_Click(object sender, EventArgs e)
        {
            FormAddTrip formAddTrip = new FormAddTrip(this);
            formAddTrip.ShowDialog();
        }
        private void btnAddClient_Click(object sender, EventArgs e)
        {
            FormAddClient formAddClient = new FormAddClient(this, mainForm);
            formAddClient.ShowDialog();
        }

        private void btnAddCar_Click(object sender, EventArgs e)
        {
            FormAddAuto formAddAuto = new FormAddAuto(this);
            formAddAuto.ShowDialog();
        }

        private void btnAddDriver_Click(object sender, EventArgs e)
        {
            FormAddPersonal formAddPersonal = new FormAddPersonal(this);
            formAddPersonal.ShowDialog();
        }
        private void comboBoxCar_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void comboBoxCar_TextChanged(object sender, EventArgs e)
        {
        }

        private void AddClient_Click(object sender, EventArgs e)
        {
            FormAddClient formAddClient = new FormAddClient(this, mainForm);
            formAddClient.ShowDialog();
        }

        private void SelectClient_Click(object sender, EventArgs e)
        {
            FormForSelect formForSelect = new FormForSelect(this, mainForm);
            Form1.WindowIndex = WindowsStruct.AddClientInWay;
            formForSelect.ShowDialog();
        }

        private void AddTrip_Click(object sender, EventArgs e)
        {
            FormAddTrip formAddTrip = new FormAddTrip(this);
            formAddTrip.ShowDialog();
        }

        private void SelectTrip_Click(object sender, EventArgs e)
        {
            FormForSelect formForSelect = new FormForSelect(this, mainForm);
            Form1.WindowIndex = WindowsStruct.AddTripInWay;
            formForSelect.ShowDialog();
        }

        private void FormAddWayBill_FormClosing(object sender, FormClosingEventArgs e)
        {
            wayBill = null;
            insWayBill = null;
    }
    }
}
