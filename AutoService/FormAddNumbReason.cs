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
        public SparePart sameSpare = null;
        public CardOfRepair card = null;
        SpareMapper sm = new SpareMapper();
        CardMapper cm = new CardMapper();
        public Form1 mainForm;
        public FormAddNumbReason()
        {
            InitializeComponent();
            textBoxNumber.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressFloat);
            DataSets.CreateDsForComboBox(comboBoxRepair, Queries.RepairsForCBox, "info", "id");
        }
        public FormAddNumbReason(Form1 mainForm) : this()
        {
            this.mainForm = mainForm;
        }
        private void FormAddNumbReason_Load(object sender, EventArgs e)
        {
            if (Form1.WindowIndex == WindowsStruct.PushInStock)
            {
                this.Text = "Добавление на склад";
            }
            else if (Form1.WindowIndex == WindowsStruct.PopFromStock)
                this.Text = "Выдача со склада";
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxNumber.Text == "")
            {
                MessageBox.Show("Вы не указали количество запчастей!");
                return;
            }
            if (Form1.WindowIndex == WindowsStruct.PushInStock)
            {
                if (card == null)
                {
                    part.Number += float.Parse(textBoxNumber.Text);
                    try
                    {
                        sm.Update(part);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        this.Close();
                        return;
                    }
                }
                else
                {
                    if (sameSpare == null)
                    {
                        MessageBox.Show("В данном ремонте отсутствует заданная запчасть!");
                        return;
                    }
                    else
                    {
                        SparePart sparePart = card.ListOfSpareParts.Find(s => s.IdSpare == sameSpare.IdSpare);
                        sparePart.Number -= float.Parse(textBoxNumber.Text);
                        if (sparePart.Number == 0)
                        {
                            card.RemoveSpareFromList(part);
                        }
                        part.Number += float.Parse(textBoxNumber.Text);
                        cm.Update(card);
                        sm.Update(part);
                    }
                }
            }
            else if (Form1.WindowIndex == WindowsStruct.PopFromStock)
            {
                SparePart addPart = new SparePart(part.IdSpare, part.Articul, 0, part.Price, part.Description, part.Unit);
                addPart.Number = float.Parse(textBoxNumber.Text);
                if (comboBoxRepair.SelectedValue != null)
                {
                    SparePart sparePart = card.ListOfSpareParts.Find(s => s.IdSpare == part.IdSpare);
                    if (sparePart == null)
                        card.AddSpareInList(addPart);
                    else
                    {
                        sparePart.Number += addPart.Number;
                    }
                    part.Number -= addPart.Number;
                    cm.Update(card);
                    sm.Update(part);
                }
                else
                {
                    part.Number -= addPart.Number;
                    sm.Update(part);
                }
            }
            Form1.WindowIndex = WindowsStruct.Stock;
            Form1.AddSparePartInStock(mainForm.dataGridView);
            this.Close();
        }

        private void textBoxNumber_TextChanged(object sender, EventArgs e)
        {
            if (textBoxNumber.Text == "")
                return;
            if (Form1.WindowIndex == WindowsStruct.PushInStock && comboBoxRepair.SelectedValue != null)
            {
                if (sameSpare != null && sameSpare.Number < float.Parse(textBoxNumber.Text))
                    textBoxNumber.Text = sameSpare.Number.ToString();

            }
            else if (Form1.WindowIndex == WindowsStruct.PopFromStock && part.Number < float.Parse(textBoxNumber.Text))
                textBoxNumber.Text = part.Number.ToString();
        }

        private void FormAddNumbReason_FormClosing(object sender, FormClosingEventArgs e)
        {
            card = null;
            part = null;
            Form1.WindowIndex = WindowsStruct.Stock;
        }
        private void comboBoxRepair_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBoxRepair.Text != "" && part != null)
            {
                card = cm.Get(comboBoxRepair.SelectedValue.ToString());
                if (Form1.WindowIndex == WindowsStruct.PushInStock)
                {
                    textBoxNumber.Text = "";
                    sameSpare = card.ListOfSpareParts.Find(s => s.IdSpare == part.IdSpare);
                    if (sameSpare == null)
                    {
                        comboBoxRepair.Text = "";
                    }
                }
            }
            else
            {
                card = null;
                sameSpare = null;
            }
        }
    }
}
