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

        public FormAddPrice()
        {
            InitializeComponent();
            comboBoxUnit.SelectedIndex = 0;
        }
        public FormAddPrice(Form1 mainForm) : this()
        {
            this.mainForm = mainForm;
        }

        private void buttonAddPosition_Click(object sender, EventArgs e)
        {
           if (Form1.AddOrEdit == (int) Form1.AddEditOrDelete.Add)
           {
                Form1.db.Open();
                using (FbTransaction trn = Form1.db.BeginTransaction())
                {
                    FbCommand command = new FbCommand("NEW_TYPE_OF_W_PROCEDURE", Form1.db, trn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@DESCRIPTION", FbDbType.VarChar).Value = textBoxDescription.Text;
                    if (comboBoxUnit.Text == "шт")
                    {
                        command.Parameters.Add("@UNIT", FbDbType.VarChar).Value = 0;
                    }
                    else
                        command.Parameters.Add("@UNIT", FbDbType.VarChar).Value = 1;
                    command.Parameters.Add("@COST", FbDbType.Float).Value = textBoxPrice.Text;
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Возможно вы ввели не все данные!");
                        Form1.db.Close();
                        return;
                    }
                    trn.Commit();
                    Form1.db.Close();
                }
                this.Close();
                AddListMalfunctionsInGrid();
                mainForm.dataGridView.ClearSelection();
            }
        }
        public void AddListMalfunctionsInGrid()
        {
            string query = @"select * from type_of_work_view";
            using (FbCommand command = new FbCommand(query, Form1.db))
            {
                FbDataAdapter dataAdapter = new FbDataAdapter(command);
                DataSet ds = new DataSet();
                Form1.db.Open();
                dataAdapter.Fill(ds);
                mainForm.dataGridView.DataSource = ds.Tables[0];
                ds.Tables[0].Columns[0].ColumnName = "Наименование";
                ds.Tables[0].Columns[1].ColumnName = "Единица измерения";
                ds.Tables[0].Columns[2].ColumnName = "Стоимость(руб.)";
                for (int i = 0; i < mainForm.dataGridView.RowCount; i++)
                {
                    if (mainForm.dataGridView.Rows[i].Cells[1].Value.ToString() == "0")
                    {
                        mainForm.dataGridView.Rows[i].Cells[1].Value = "шт";
                    }
                    else
                        mainForm.dataGridView.Rows[i].Cells[1].Value = "нч";
                }
                Form1.db.Close();
            }
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
    }
}
