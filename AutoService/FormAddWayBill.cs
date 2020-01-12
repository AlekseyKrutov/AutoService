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
        public FormAddWayBill()
        {
            InitializeComponent();
            DbProxy.DataSets.CreateDsForComboBox(comboBoxCar, DbProxy.Queries.CarForCBox, "car", "state_number");
            DbProxy.DataSets.CreateDsForComboBox(comboBoxDriver, DbProxy.Queries.DriverForCBox, "name", "tub_numb");
            txtBoxCost.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressFloat);
        }
        private void FormAddWayBill_Load(object sender, EventArgs e)
        {
        }

        private void btnCreateWayBill_Click(object sender, EventArgs e)
        {
            
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
        private void comboBoxCar_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void comboBoxCar_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
