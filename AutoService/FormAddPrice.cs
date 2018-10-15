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
    public partial class FormAddPrice : Form
    {
        Form1 mainForm;

        public FormAddPrice(Form1 mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }
        public FormAddPrice()
        {
            InitializeComponent();
        }

        private void buttonAddPosition_Click(object sender, EventArgs e)
        {
            if (textBoxDescription.Text.Length == 0 || textBoxPrice.Text.Length == 0)
            {
                MessageBox.Show("Вы ввели не все данные!");
                return;
            }
            Malfunctions.MalfList.Add(new Malfunctions 
            {
                DescriptionOfMalf = textBoxDescription.Text,
                Price = double.Parse(textBoxPrice.Text)
            });
            AddListPriceInGrid();
            this.Visible = false;
        }

        public void AddListPriceInGrid()
        {
            mainForm.dataGridView.Rows.Clear();
            foreach (Malfunctions malf in Malfunctions.MalfList)
            {
                mainForm.dataGridView.Rows.Add(malf.DescriptionOfMalf, malf.Price);
            }
        }

        private void textBoxPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
    }
}
