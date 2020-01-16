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
using DataMapper;
using FirebirdSql.Data.FirebirdClient;

namespace AutoService
{
    public partial class FormAddTrip : Form
    {
        FormAddWayBill formAddWayBill = new FormAddWayBill();
        FormForSelect formForSelect;
        Form1 mainForm;
        public Trip trip = null;
        public FormAddTrip()
        {
            InitializeComponent();
        }
        public FormAddTrip(FormAddWayBill formAddWayBill) : this ()
        {
            this.formAddWayBill = formAddWayBill;
        }
        public FormAddTrip(FormForSelect formForSelect, Form1 mainForm) : this ()
        {
            this.formForSelect = formForSelect;
            this.mainForm = mainForm;
        }
        private void btnAddTrip_Click(object sender, EventArgs e)
        {
            if (textBoxTrip.Text == string.Empty)
            {
                MessageBox.Show("Введены не все данные!");
                return;
            }
            TripMapper tm = new TripMapper();
            if (trip == null)
            {
                trip = new Trip(textBoxTrip.Text);
                try
                {
                    tm.Insert(trip);
                }
                catch (Exception ex)
                {
                    trip = null;
                    MessageBox.Show(ex.Message);
                    return;
                }
                if (formAddWayBill != null && formAddWayBill.Visible)
                {
                    if (formAddWayBill.insWayBill != null)
                    {
                        formAddWayBill.insWayBill.Trip = trip;
                    }
                    else
                    {
                        formAddWayBill.wayBill.Trip = trip;
                    }
                    formAddWayBill.textBoxTrip.Text = trip.Name;
                    this.Close();
                    return;
                }
            }
            else if (trip != null)
            {
                trip.Name = textBoxTrip.Text;
                try
                {
                    tm.Update(trip);
                }
                catch (Exception ex)
                {
                    trip = null;
                    MessageBox.Show(ex.Message);
                    return;
                }
                if (formForSelect != null && formForSelect.Visible)
                {
                    Form1.AddListTripInGrid(formForSelect.dataGridView);
                    this.Close();
                }
            }
            this.Close();
        }

        private void FormAddTrip_FormClosing(object sender, FormClosingEventArgs e)
        {
            trip = null;
        }
    }
}
