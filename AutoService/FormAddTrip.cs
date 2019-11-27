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

namespace AutoService
{
    public partial class FormAddTrip : Form
    {
        FormAddWayBill formAddWayBill = new FormAddWayBill();
        public FormAddTrip()
        {
            InitializeComponent();
        }
        public FormAddTrip(FormAddWayBill formAddWayBill) : this ()
        {
            this.formAddWayBill = formAddWayBill;
        }
        private void btnAddTrip_Click(object sender, EventArgs e)
        {
            if (textBoxTrip.Text == string.Empty)
            {
                MessageBox.Show("Введены не все данные!");
                return;
            }
            try
            {
                string sqlCommand = $"insert into trip (trip_name) values('{textBoxTrip.Text}')";
                Form1.db.Open();
                using (FbTransaction trn = Form1.db.BeginTransaction())
                {
                    FbCommand command = new FbCommand(sqlCommand, Form1.db, trn);
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                    trn.Commit();
                    Form1.db.Close();
                    formAddWayBill.FillComboBox(formAddWayBill.comboBoxRoute, Form1.db,
                        formAddWayBill.trip_query, formAddWayBill.displayMembers[FormAddWayBill.DisplayMembers.Trip],
                        formAddWayBill.valueMembers[FormAddWayBill.ValueMembers.Trip]);
                    formAddWayBill.comboBoxRoute.SelectedIndex = -1;
                    formAddWayBill.comboBoxRoute.SelectedValue = textBoxTrip.Text;
                }
            }
            catch (Exception ex)
            {
                Form1.db.Close();
                MessageBox.Show(ex.Message);
                return;
            }
            this.Close();
        }
    }
}
