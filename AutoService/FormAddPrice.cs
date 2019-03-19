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

namespace AutoService
{
    public partial class FormAddPrice : Form
    {
        Form1 mainForm;
        FormForSelect formForSelect;
        public string oldDescription;

        public FormAddPrice()
        {
            InitializeComponent();
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
            if (Form1.AddOrEdit == (int)Form1.AddEditOrDelete.Add)
            {
                ExecuteTypeWorkProc("NEW_TYPE_OF_W_PROCEDURE");
            }
            else
                ExecuteTypeWorkProc("UPDATE_TYPE_WORK");
        }
        private void textBoxPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ',')
                e.KeyChar = '.';
            if (!(Char.IsDigit(e.KeyChar)) && !((e.KeyChar == '.' || e.KeyChar == ',')
                && (textBoxPrice.Text.IndexOf(".") == -1) && (textBoxPrice.Text.Length != 0)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
            if (textBoxPrice.Text.Split('.').ToArray().Length > 1
                && textBoxPrice.Text.Split('.').ToArray().Last().Length > 1 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
        public void ExecuteTypeWorkProc(string nameProc)
        {
            Form1.db.Open();
            using (FbTransaction trn = Form1.db.BeginTransaction())
            {
                FbCommand command = new FbCommand(nameProc, Form1.db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@DESCRIPTION", FbDbType.VarChar).Value = textBoxDescription.Text;
                if (Form1.AddOrEdit != (int) Form1.AddEditOrDelete.Add)
                    command.Parameters.Add("@OLDDESCRIPTION", FbDbType.VarChar).Value = oldDescription;
                if (comboBoxUnit.Text == "шт")
                {
                    command.Parameters.Add("@UNIT", FbDbType.VarChar).Value = 0;
                }
                else
                    command.Parameters.Add("@UNIT", FbDbType.VarChar).Value = 1;
                command.Parameters.Add("@COST", FbDbType.Float).Value = textBoxPrice.Text;
                if (Form1.WindowIndex == (int)Form1.WindowsStruct.SpareAdd)
                    command.Parameters.Add("@MALF_OR_SPARE", FbDbType.VarChar).Value = 1;
                else
                    command.Parameters.Add("@MALF_OR_SPARE", FbDbType.VarChar).Value = null;
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (FbException ex)
                {
                    MessageBox.Show($"Данные не сохранены! \n{ex.Message}");
                    Form1.db.Close();
                    return;
                }
                catch (FormatException)
                {
                    MessageBox.Show("Возможно вы ввели не все данные!");
                    Form1.db.Close();
                    return;
                }
                trn.Commit();
                Form1.db.Close();
                if (formForSelect != null && Form1.WindowIndex == (int)Form1.WindowsStruct.MalfAdd)
                {
                    Form1.AddListMalfunctionsInGrid(formForSelect.dataGridView, Form1.queryForMalfunctions);
                    Form1.SelectIndex = 0;
                    formForSelect.textBoxSearch.Clear();
                    formForSelect.textBoxSearch.Text = textBoxDescription.Text;
                    this.Close();
                    return;
                }
                else if (formForSelect != null && Form1.WindowIndex == (int)Form1.WindowsStruct.SpareAdd)
                {
                    Form1.AddListMalfunctionsInGrid(formForSelect.dataGridView, Form1.queryForSpares);
                    Form1.SelectIndex = 0;
                    formForSelect.textBoxSearch.Clear();
                    formForSelect.textBoxSearch.Text = textBoxDescription.Text;
                    this.Close();
                    return;
                }
            }
            this.Close();
            Form1.AddListMalfunctionsInGrid(mainForm.dataGridView, Form1.queryForMalfunctions);
            mainForm.dataGridView.ClearSelection();
        }

        private void FormAddPrice_Shown(object sender, EventArgs e)
        {
            oldDescription = textBoxDescription.Text;
            if (Form1.WindowIndex == (int)Form1.WindowsStruct.MalfAdd)
                comboBoxUnit.SelectedIndex = 1;
        }
    }
}
