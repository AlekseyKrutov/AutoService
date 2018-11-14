using AutoService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Text.RegularExpressions;
using FirebirdSql.Data.FirebirdClient;
using AutoServiceLibrary;

namespace AutoService
{
    public partial class FormAddAuto : Form
    {
        FormForSelect formSelectAuto;
        FormAddRepair FormAddRepairInCar;
        int selectedIndex;
        public bool OwnerSelected = false;
        Form1 mainForm;
        public FormAddAuto()
        {
            InitializeComponent();
        }
        public FormAddAuto(Form1 mainForm) : this()
        {
            this.mainForm = mainForm;
        }
        public FormAddAuto(FormAddRepair addRepair, Form1 mainForm) : this ()
        {
            FormAddRepairInCar = addRepair;
            this.mainForm = mainForm; 
        }
        private void FormAddAuto_Load(object sender, EventArgs e)
        {
            selectedIndex = Form1.SelectIndex;
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
                comboBoxAuto.Text = mainForm.dataGridView.Rows[Form1.SelectIndex].Cells[1].Value.ToString();
            }
        }
        private void buttonAddAuto_Click(object sender, EventArgs e)
        {
            if (textBoxGosNumb.Text.Length == 0 || textBoxReg.Text.Length == 0
                || textBoxVIN.Text.Length == 0 || OwnerSelected == false)
            {
                MessageBox.Show("Вы ввели не все данные!");
                return;
            }
            if (Form1.AddOrEdit == (int)Form1.AddEditOrDelete.Add)
            {
                ExecuteAutoProcedure("NEW_CAR_PROCEDURE");
                mainForm.dataGridView.ClearSelection();
            }
            else if (Form1.AddOrEdit == (int)Form1.AddEditOrDelete.Edit)
            {
                ExecuteAutoProcedure("UPDATE_CAR_PROCEDURE");
                mainForm.dataGridView.Rows[selectedIndex].Selected = true;
            }
            AddListAutoInGrid();
            mainForm.dataGridView.ClearSelection();
            mainForm.dataGridView.Rows[Form1.SelectIndex].Selected = true;
            this.Close();
        }
        
        private void buttonAddOwner_Click(object sender, EventArgs e)
        {
            Form1.SelectIndex = 0;
            Form1.WindowIndex = (int)Form1.WindowsStruct.Auto;
            formSelectAuto = new FormForSelect(this);
            formSelectAuto.ShowDialog();
        }

        private void textBoxVIN_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationOnlyUpCharAndNumb(e);
        }

        private void textBoxReg_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationOnlyUpCharAndNumb(e);
        }

        private void textBoxGosNumb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsLetter(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
            if (Char.IsLetter(e.KeyChar))
            {
                if (!Regex.Match(e.KeyChar.ToString(), @"[а-яА-Я]").Success)
                {
                    e.Handled = true;
                }
            }
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void textBoxGosNumb_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Space)
            {
                e.Handled = true;
            }
            if (Char.IsLetter(e.KeyChar))
            {
                e.KeyChar = Char.ToUpper(e.KeyChar);
            }
        }
        
        private void ValidationOnlyUpCharAndNumb(KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
            if (!Char.IsNumber(e.KeyChar) && !Char.IsLetter(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        public void AddListAutoInGrid()
        {
            string query = @"select * from cars_view";
            using (FbCommand command = new FbCommand(query, Form1.db))
            {
                FbDataAdapter dataAdapter = new FbDataAdapter(command);
                DataSet ds = new DataSet();
                Form1.db.Open();
                dataAdapter.Fill(ds);
                mainForm.dataGridView.DataSource = ds.Tables[0];
                ds.Tables[0].Columns[0].ColumnName = "VIN";
                ds.Tables[0].Columns[1].ColumnName = "Марка";
                ds.Tables[0].Columns[2].ColumnName = "Гос.номер";
                ds.Tables[0].Columns[3].ColumnName = "Свидетельство о рег.";
                ds.Tables[0].Columns[4].ColumnName = "Владелец";
                Form1.db.Close();
            }
        }

        private void ExecuteAutoProcedure(string nameProc)
        {
            Form1.db.Open();
            using (FbTransaction trn = Form1.db.BeginTransaction())
            {
                FbCommand command = new FbCommand(nameProc, Form1.db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@VIN", FbDbType.VarChar).Value = textBoxVIN.Text;
                command.Parameters.Add("@STATE_NUMBER", FbDbType.VarChar).Value = textBoxGosNumb.Text;
                command.Parameters.Add("@REG_CERTIFICATE", FbDbType.VarChar).Value = textBoxReg.Text;
                command.Parameters.Add("@CAR_MARK", FbDbType.VarChar).Value = comboBoxAuto.Text.Split(' ').ToArray()[0];
                if (comboBoxAuto.Text.Split(' ').ToArray()[1].Length > 0)
                    command.Parameters.Add("@CAR_MODEL", FbDbType.VarChar).Value = comboBoxAuto.Text.Split(' ').ToArray()[1];
                else
                    command.Parameters.Add("@CAR_MODEL", FbDbType.VarChar).Value = null;
                command.Parameters.Add("@NAME_ORG", FbDbType.VarChar).Value = labelContentOwner.Text;
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (FbException e)
                {
                    MessageBox.Show(e.Message);
                    Form1.db.Close();
                    return;
                }
                trn.Commit();
                Form1.db.Close();
            }
        }
    }
}
