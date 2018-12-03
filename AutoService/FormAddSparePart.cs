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
    public partial class FormAddSparePart : Form
    {
        Form1 mainForm;

        public FormAddSparePart()
        {
            InitializeComponent();
            textBoxUniqNumb.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressOnlyNumb);
            textBoxNumb.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressOnlyNumb);
            textBoxNumb.MaxLength = 5;
        }
        public FormAddSparePart(Form1 mainForm) : this()
        {
            this.mainForm = mainForm;
        }

        private void FormAddSparePart_Load(object sender, EventArgs e)
        {
            string query = @"select CAR_ID, MARK || ' ' || coalesce(model, '') AS MARK_MODEL 
                             from CAR_MODEL
                             order by MARK_MODEL";
            using (FbCommand command = new FbCommand(query, Form1.db))
            {
                FbDataAdapter dataAdapter = new FbDataAdapter(command);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                Form1.db.Open();
                comboBoxAuto.DataSource = dt;
                comboBoxAuto.DisplayMember = "MARK_MODEL";
                Form1.db.Close();
            }
            if (Form1.AddOrEdit == (int)Form1.AddEditOrDelete.Edit)
            {
                checkBoxOnlyNumb.Checked = true;
                if (mainForm.dataGridView.Rows[Form1.SelectIndex].Cells[4].Value.ToString().Length != 0)
                {
                    comboBoxAuto.Text = mainForm.dataGridView.Rows[Form1.SelectIndex].Cells[4].Value.ToString();
                }
                else if (mainForm.dataGridView.Rows[Form1.SelectIndex].Cells[4].Value.ToString().Length == 0)
                {
                    checkBoxShowAuto.Checked = true;
                    comboBoxAuto.Enabled = false;
                }
            }
            if (Form1.AddOrEdit == (int)Form1.AddEditOrDelete.Add)
            {
                checkBoxOnlyNumb.Visible = false;
            }
        }

        private void checkBoxShowAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBoxAuto.Enabled)
            {
                comboBoxAuto.Enabled = false;
            }
            else
                comboBoxAuto.Enabled = true;
        }

        private void textBoxDescr_Enter(object sender, EventArgs e)
        {
            textBoxDescr.Height += 22;
        }

        private void textBoxDescr_Leave(object sender, EventArgs e)
        {
            textBoxDescr.Height -= 22;
        }

        private void buttonAddSparePart_Click(object sender, EventArgs e)
        {
            if (Form1.AddOrEdit == (int) Form1.AddEditOrDelete.Add)
            {
                Form1.db.Open();
                using (FbTransaction trn = Form1.db.BeginTransaction())
                {
                    FbCommand command = new FbCommand("NEW_SPAREPART_PROCEDURE", Form1.db, trn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@UNIQ_CODE", FbDbType.Integer).Value = textBoxUniqNumb.Text;
                    command.Parameters.Add("@DESCRIPTION", FbDbType.VarChar).Value = textBoxDescr.Text;
                    command.Parameters.Add("@COST", FbDbType.Float).Value = textBoxCost.Text;
                    command.Parameters.Add("@NUMBER", FbDbType.SmallInt).Value = textBoxNumb.Text;
                    if (comboBoxAuto.Enabled)
                    {
                        command.Parameters.Add("@CAR_MARK", FbDbType.VarChar).Value = comboBoxAuto.Text.Split(' ').ToArray()[0];
                        if (comboBoxAuto.Text.Split(' ').ToArray()[1].Length > 0)
                            command.Parameters.Add("@CAR_MOD", FbDbType.VarChar).Value = comboBoxAuto.Text.Split(' ').ToArray()[1];
                        else
                            command.Parameters.Add("@CAR_MOD", FbDbType.VarChar).Value = null;
                    }
                    else
                    {
                        command.Parameters.Add("@CAR_MARK", FbDbType.VarChar).Value = null;
                        command.Parameters.Add("@CAR_MOD", FbDbType.VarChar).Value = null;
                    }
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (FbException)
                    {
                        MessageBox.Show("Запчасть с таким артикулом уже есть на складе.");
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
                }
                Form1.AddSparePartInStock(mainForm.dataGridView, Form1.queryForSparePart);
                this.Close();
            }
            else if(Form1.AddOrEdit == (int) Form1.AddEditOrDelete.Edit)
            {
                Form1.db.Open();
                using (FbTransaction trn = Form1.db.BeginTransaction())
                {
                    FbCommand command = new FbCommand("UPDATE_STOCK_PROCEDURE", Form1.db, trn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@OLD_UNIQ_CODE", FbDbType.Integer).Value = mainForm.dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString();
                    command.Parameters.Add("@NEW_UNIQ_CODE", FbDbType.Integer).Value = textBoxUniqNumb.Text;
                    command.Parameters.Add("@DESCRIPTION", FbDbType.VarChar).Value = textBoxDescr.Text;
                    command.Parameters.Add("@COST", FbDbType.Float).Value = textBoxCost.Text;
                    command.Parameters.Add("@NUMBER", FbDbType.SmallInt).Value = textBoxNumb.Text;
                    if (comboBoxAuto.Enabled || (mainForm.dataGridView.Rows[Form1.SelectIndex].Cells[4].Value.ToString().Length != 0 && !checkBoxShowAuto.Checked))
                    {
                        command.Parameters.Add("@CAR_MARK", FbDbType.VarChar).Value = comboBoxAuto.Text.Split(' ').ToArray()[0];
                        if (comboBoxAuto.Text.Split(' ').ToArray()[1].Length > 0)
                            command.Parameters.Add("@CAR_MOD", FbDbType.VarChar).Value = comboBoxAuto.Text.Split(' ').ToArray()[1];
                        else
                            command.Parameters.Add("@CAR_MOD", FbDbType.VarChar).Value = null;
                    }
                    else if(!comboBoxAuto.Enabled || (mainForm.dataGridView.Rows[Form1.SelectIndex].Cells[4].Value.ToString().Length == 0))
                    {
                        command.Parameters.Add("@CAR_MARK", FbDbType.VarChar).Value = null;
                        command.Parameters.Add("@CAR_MOD", FbDbType.VarChar).Value = null;
                    }
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (FbException)
                    {
                        MessageBox.Show("Запчасть с таким артикулом уже есть на складе.");
                        Form1.db.Close();
                        return;
                    }
                    
                    trn.Commit();
                    Form1.db.Close();
                }
                Form1.AddSparePartInStock(mainForm.dataGridView, Form1.queryForSparePart);
                this.Close();
                mainForm.dataGridView.ClearSelection();
                mainForm.dataGridView.Rows[Form1.SelectIndex].Selected = true;
            }
        }
        private void checkBoxOnlyNumb_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOnlyNumb.Checked)
            {
                textBoxUniqNumb.Enabled = false;
                textBoxDescr.Enabled = false;
                textBoxCost.Enabled = false;
                comboBoxAuto.Enabled = false;
                checkBoxShowAuto.Visible = false;
            }
            else
            {
                textBoxUniqNumb.Enabled = true;
                textBoxDescr.Enabled = true;
                textBoxCost.Enabled = true;
                checkBoxShowAuto.Visible = true;
                if (checkBoxShowAuto.Checked)
                {
                    comboBoxAuto.Enabled = false;
                }
                else
                    comboBoxAuto.Enabled = true;
            }
        }

        private void textBoxCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ',')
                e.KeyChar = '.';
            if (!(Char.IsDigit(e.KeyChar)) && !((e.KeyChar == '.' || e.KeyChar == ',')
                && (textBoxCost.Text.IndexOf(".") == -1) && (textBoxCost.Text.Length != 0)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
            if (textBoxCost.Text.Split('.').ToArray().Length > 1 
                && textBoxCost.Text.Split('.').ToArray().Last().Length > 1 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
    }
}
