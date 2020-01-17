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
