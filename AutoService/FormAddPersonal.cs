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

namespace AutoService
{
    public partial class FormAddPersonal : Form
    {
        Form1 mainForm;
        public FormAddPersonal(Form1 mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }
        public FormAddPersonal()
        {
            InitializeComponent();
        }

        private void buttonAddPersonal_Click(object sender, EventArgs e)
        {
            if (textBoxAddress.Text.Length == 0 || textBoxINN.Text.Length == 0 || textBoxFirstName.Text.Length == 0
                || textBoxNumbOfTel.Text.Length == 0)
            {
                MessageBox.Show("Вы ввели не все данные!");
                return;
            }
            Personal.PersonalList.Add(new Personal
            {
                Name = textBoxFirstName.Text,
                INN = textBoxINN.Text,
                Address = textBoxAddress.Text,
                NumberOfTel = textBoxNumbOfTel.Text
            });
            AddListPersonalInGrid();
            this.Visible = false;
        }
        public void AddListPersonalInGrid()
        {
            mainForm.dataGridView.Rows.Clear();
            foreach (Personal person in Personal.PersonalList)
            {
                mainForm.dataGridView.Rows.Add(person.Name, person.INN, person.Address, person.Function, person.NumberOfTel);
            }
        }

        private void textBoxINN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void FormAddPersonal_Load(object sender, EventArgs e)
        {

        }
    }
}
