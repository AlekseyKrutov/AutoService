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
using DbProxy;

namespace AutoService
{
    public partial class FormAddNumbReason : Form
    {
        public SparePart part = null;
        public FormAddNumbReason()
        {
            InitializeComponent();
            textBoxNumber.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressFloat);
            DataSets.CreateDsForComboBox(comboBoxRepair, Queries.RepairsForCBox, "info", "id");
        }

        private void FormAddNumbReason_Load(object sender, EventArgs e)
        {
            if (Form1.WindowIndex == WindowsStruct.PushInStock)
            {
                this.Text = "Добавление на склад";
                this.comboBoxRepair.Visible = false;
            }
            else if (Form1.WindowIndex == WindowsStruct.PopFromStock)
                this.Text = "Выдача со склада";
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            SpareMapper sm = new SpareMapper();
            if (Form1.WindowIndex == WindowsStruct.PushInStock)
            {
                part.Number += float.Parse(textBoxNumber.Text);
                try
                {
                    sm.Insert(part);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    this.Close();
                    return;
                }
            }
            this.Close();
        }
    }
}
