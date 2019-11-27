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
using AutoServiceLibrary;

namespace AutoService
{
    public partial class FormAddWayBill : Form
    {
        public string client_query = $"select name from client_view";
        public string trip_query = $"select id_trip, trip_name from trip";
        public string car_query = $"select (car_model || state_number) as car, state_number from cars_view";
        public string driver_query = $"select (name || ' ' || 'таб. ' || tub_numb) name, tub_numb from staff_view" +
            $" where prof = 'Водитель'";

        public enum DisplayMembers { Client = 1, Driver, Trip, Car };
        public enum ValueMembers { Client = 1, Driver, Trip, Car };

        public Dictionary<DisplayMembers, string> displayMembers =
            new Dictionary<DisplayMembers, string>() {
                { DisplayMembers.Client, "name" },
                { DisplayMembers.Driver, "name" },
                { DisplayMembers.Car, "car" },
                { DisplayMembers.Trip, "trip_name"}
            };
        public Dictionary<ValueMembers, string> valueMembers =
            new Dictionary<ValueMembers, string>() {
                { ValueMembers.Driver, "tub_numb" },
                { ValueMembers.Car, "state_number" },
                { ValueMembers.Trip, "trip_name"}
            };

        public FormAddWayBill()
        {
            InitializeComponent();
            labelCarOwnerTxt.Text = "";
            txtBoxCost.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressFloat);
            comboBoxCar.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBoxCar.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
        private void FormAddWayBill_Load(object sender, EventArgs e)
        {
            FillComboBox(comboBoxCar, Form1.db, car_query, 
                displayMembers[DisplayMembers.Car], valueMembers[ValueMembers.Car]);
            FillComboBox(comboBoxClient, Form1.db, client_query, displayMembers[DisplayMembers.Client]);
            FillComboBox(comboBoxDriver, Form1.db, driver_query, 
                displayMembers[DisplayMembers.Driver], valueMembers[ValueMembers.Driver]);
            FillComboBox(comboBoxRoute, Form1.db, trip_query,
                displayMembers[DisplayMembers.Trip], valueMembers[ValueMembers.Trip]);
            comboBoxCar.SelectedIndex = -1;
            comboBoxClient.SelectedIndex = -1;
            comboBoxDriver.SelectedIndex = -1;
            comboBoxRoute.SelectedIndex = -1;
        }

        private void btnCreateWayBill_Click(object sender, EventArgs e)
        {
            if (comboBoxRoute.Text == string.Empty ||
                comboBoxClient.Text == string.Empty ||
                comboBoxCar.Text == string.Empty ||
                comboBoxDriver.Text == string.Empty ||
                labelCarOwnerTxt.Text == string.Empty ||
                txtBoxLoadDate.Text == string.Empty ||
                txtBoxUnloadDate.Text == string.Empty ||
                txtBoxCost.Text == string.Empty)
            {
                MessageBox.Show("Введены не все данные!");
                return;
            }
            DateTime loadDate = new DateTime();
            DateTime unloadDate = new DateTime();
            if (!DateTime.TryParse(txtBoxLoadDate.Text, out loadDate) ||
                !DateTime.TryParse(txtBoxUnloadDate.Text, out unloadDate))
            {
                MessageBox.Show("Введена некорректная дата!");
                return;
            }
            DateTime dt = DateTime.Parse(txtBoxLoadDate.Text);
            this.Close();
        }
        private void btnAddRoute_Click(object sender, EventArgs e)
        {
            FormAddTrip formAddTrip = new FormAddTrip(this);
            formAddTrip.ShowDialog();
        }
        private void btnAddClient_Click(object sender, EventArgs e)
        {
            FormAddClient formAddClient = new FormAddClient(this);
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

        // заполнение comboBox данными из БД
        public void FillComboBox(ComboBox comboBox, FbConnection db, string query, 
            string display_member, string value_member = null)
        {
            using (FbCommand command = new FbCommand(query, db))
            {
                FbDataAdapter dataAdapter = new FbDataAdapter(command);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                Form1.db.Open();
                comboBox.DataSource = dt;
                comboBox.DisplayMember = display_member;
                comboBox.ValueMember = value_member ?? display_member;
                comboBox.SelectedItem = -1;
                Form1.db.Close();
            }
        }
        private void SearchForComboBox()
        {

        }
        private void comboBoxCar_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (comboBoxCar.Text.Length == 0 || Form1.db.State == ConnectionState.Open)
                return;
            if (cb.Text.Length == 0)
                return;
            Car car = new Car(Form1.db, cb.Text.Split(' ').Last());
            labelCarOwnerTxt.Text = car.Owner.Name;
        }
        private void comboBoxCar_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxCar.Text.Length == 0)
                labelCarOwnerTxt.Text = "";
        }
    }
}
