using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using AutoServiceLibrary;

namespace AutoService
{
    public partial class FormAddClient : Form
    {
        Form1 mainForm;
        public FormAddClient(Form1 mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }
        public FormAddClient()
        {
            InitializeComponent();
        }

        private void buttonAddClient_Click(object sender, EventArgs e)
        {
            if (textBoxAddress.Text.Length == 0 || textBoxINN.Text.Length == 0 || textBoxName.Text.Length == 0 || textBoxNumbOfTel.Text.Length == 0)
            {
                MessageBox.Show("Вы ввели не все данные!");
                return;
            }
            Client.ClientList.Add(new Client
            {
                Address = textBoxAddress.Text,
                INN = textBoxINN.Text,
                Name = textBoxName.Text,
                NumberOfTel = textBoxNumbOfTel.Text
            });
            AddListClientInGrid();
            this.Visible = false;
        }
        public void AddListClientInGrid()
        {
            mainForm.dataGridView.Rows.Clear();
            foreach (Client client in Client.ClientList)
            {
                mainForm.dataGridView.Rows.Add(client.Name, client.INN, client.Address, client.NumberOfTel);
            }
        }

        private void textBoxINN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
    }
}
