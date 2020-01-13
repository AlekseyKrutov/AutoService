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
    public partial class FormAddSparePart : Form
    {
        Form1 mainForm;
        int resizeValue = 40;

        public SparePart part = null;

        public FormAddSparePart()
        {
            InitializeComponent();
            textBoxUniqNumb.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressUpper);
            textBoxNumb.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressFloat);
            textBoxCost.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressFloat);
            textBoxNumb.MaxLength = 5;
        }
        public FormAddSparePart(Form1 mainForm) : this()
        {
            this.mainForm = mainForm;
        }

        private void FormAddSparePart_Load(object sender, EventArgs e)
        {
            
        }

        private void checkBoxShowAuto_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void textBoxDescr_Enter(object sender, EventArgs e)
        {
            textBoxDescr.Height += resizeValue;
        }

        private void textBoxDescr_Leave(object sender, EventArgs e)
        {
            textBoxDescr.Height -= resizeValue;
        }

        private void buttonAddSparePart_Click(object sender, EventArgs e)
        {
            SpareMapper sm = new SpareMapper();
            if (part == null)
            {
                part = new SparePart(textBoxUniqNumb.Text, float.Parse(textBoxNumb.Text),
                    double.Parse(textBoxCost.Text), textBoxDescr.Text, UnitsConvert.ConvertUnit(comboBoxUnit.Text));
                try
                {
                    sm.Insert(part);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            else
            {
                part.Articul = textBoxUniqNumb.Text;
                part.Description = textBoxDescr.Text;
                part.Number = float.Parse(textBoxNumb.Text);
                part.Price = double.Parse(textBoxCost.Text);
                part.Unit = UnitsConvert.ConvertUnit(comboBoxUnit.Text);
                try
                {
                    sm.Update(part);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            Form1.AddSparePartInStock(mainForm.dataGridView);
            this.Close();
        }

        private void FormAddSparePart_FormClosed(object sender, FormClosedEventArgs e)
        {
            part = null;
        }
    }
}
