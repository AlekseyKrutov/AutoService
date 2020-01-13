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
using DataMapper;
using DbProxy;

namespace AutoService
{
    public partial class FormAddPrice : Form
    {
        Form1 mainForm;
        FormForSelect formForSelect;

        public Malfunctions malf = null;

        public FormAddPrice()
        {
            InitializeComponent();
            textBoxPrice.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressFloat);
            comboBoxUnit.SelectedIndex = 0;
        }
        public FormAddPrice(Form1 mainForm) : this()
        {
            this.mainForm = mainForm;
        }
        public FormAddPrice(FormForSelect formForSelect) : this()
        {
            this.formForSelect = formForSelect;
        }
        public FormAddPrice(Form1 mainForm, FormForSelect formForSelect) : this()
        {
            this.mainForm = mainForm;
            this.formForSelect = formForSelect;
        }
        private void buttonAddPosition_Click(object sender, EventArgs e)
        {
            MalfMapper mm = new MalfMapper();
            if (malf == null)
            {
                malf = new Malfunctions(textBoxDescription.Text, Convert.ToDouble(textBoxPrice.Text),
                    UnitsConvert.ConvertUnit(comboBoxUnit.Text));
                if (formForSelect != null && Form1.WindowIndex == WindowsStruct.MalfAdd)
                {
                    malf = mm.Insert(malf);
                }
                else if (formForSelect != null && Form1.WindowIndex == WindowsStruct.SpareAdd)
                {
                    malf.MalfOrSpare = 1;
                    malf = mm.Insert(malf);
                }
                else
                    malf = mm.Insert(malf);
            }
            else
            {
                malf.DescriptionOfMalf = textBoxDescription.Text;
                malf.Unit = UnitsConvert.ConvertUnit(comboBoxUnit.Text);
                malf.Price = Convert.ToDouble(textBoxPrice.Text);
                if (formForSelect != null && Form1.WindowIndex == WindowsStruct.MalfAdd)
                {
                    mm.Update(malf);
                }
                else if (formForSelect != null && Form1.WindowIndex == WindowsStruct.SpareAdd)
                {
                    malf.MalfOrSpare = 1;
                    mm.Update(malf);
                }
            }   
            if (formForSelect != null && Form1.WindowIndex == WindowsStruct.MalfAdd)
            {
                Form1.AddListMalfunctionsInGrid(formForSelect.dataGridView, Queries.MalfunctionsView);
                Form1.SelectIndex = 0;
                formForSelect.textBoxSearch.Clear();
                formForSelect.textBoxSearch.Text = textBoxDescription.Text;
                this.Close();
                return;
            }
            else if (formForSelect != null && Form1.WindowIndex == WindowsStruct.SpareAdd)
            {
                Form1.AddListMalfunctionsInGrid(formForSelect.dataGridView, Queries.MalfunctionsView);
                Form1.SelectIndex = 0;
                formForSelect.textBoxSearch.Clear();
                formForSelect.textBoxSearch.Text = textBoxDescription.Text;
                this.Close();
                return;
            }
            this.Close();
            Form1.AddListMalfunctionsInGrid(mainForm.dataGridView, Queries.MalfunctionsView);
            mainForm.dataGridView.ClearSelection();
        }

        private void FormAddPrice_Shown(object sender, EventArgs e)
        {
            if (Form1.WindowIndex == WindowsStruct.MalfAdd)
                comboBoxUnit.SelectedIndex = 1;
        }

        private void FormAddPrice_Load(object sender, EventArgs e)
        {
            malf = null;
        }
    }
}
