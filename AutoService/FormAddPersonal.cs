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
using FirebirdSql.Data.FirebirdClient;

namespace AutoService
{
    public partial class FormAddPersonal : Form
    {
        Form1 mainForm;

        public string date;
        public FormAddPersonal()
        {
            InitializeComponent();
            textBoxINN.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressOnlyNumb);
        }
        public FormAddPersonal(Form1 mainForm) : this()
        {
            this.mainForm = mainForm;
        }
        private void FormAddPersonal_Load(object sender, EventArgs e)
        {
            string query = @"select p.profession from profession as p";
            using (FbCommand command = new FbCommand(query, Form1.db))
            {
                FbDataAdapter dataAdapter = new FbDataAdapter(command);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                Form1.db.Open();
                comboBoxFunction.DataSource = dt;
                comboBoxFunction.DisplayMember = "PROFESSION";
                Form1.db.Close();
            }
            if (Form1.AddOrEdit == (int)Form1.AddEditOrDelete.Edit)
            {
                Form1.db.Open();
                using (FbCommand command = new FbCommand("RETURN_GENDER_FUNC_PROCEDURE", Form1.db))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@TUB_NUMB", FbDbType.VarChar).Value =
                        mainForm.dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString();
                    FbDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        comboBoxFunction.Text = dr.GetString(0);
                        if (int.Parse(dr.GetString(1)) == 0)
                            comboBoxGender.Text = "М";
                        else
                            comboBoxGender.Text = "Ж";
                    }
                    Form1.db.Close();
                }
            }
        }
        private void buttonAddPersonal_Click(object sender, EventArgs e)
        {
            if (textBoxLastName.Text.Length == 0 || textBoxFirstName.Text.Length == 0
                || comboBoxGender.Text.Length == 0)
            {
                MessageBox.Show("Вы ввели не все данные!");
                return;
            }
            if (Form1.AddOrEdit == (int)Form1.AddEditOrDelete.Add)
            {
                ExecutePersonalProcedure("NEW_STAFF_PROCEDURE");
                mainForm.dataGridView.ClearSelection();
            }
            else if (Form1.AddOrEdit == (int)Form1.AddEditOrDelete.Edit)
            {
                ExecutePersonalProcedure("UPDATE_STAFF_PROCEDURE");
                mainForm.dataGridView.ClearSelection();
                mainForm.dataGridView.Rows[Form1.SelectIndex].Selected = true;
            }
            AddListPersonalInGrid();
            this.Close();
        }
        public void AddListPersonalInGrid()
        {
            string query = @"select * from staff_view";
            using (FbCommand command = new FbCommand(query, Form1.db))
            {
                FbDataAdapter dataAdapter = new FbDataAdapter(command);
                DataSet ds = new DataSet();
                Form1.db.Open();
                dataAdapter.Fill(ds);
                mainForm.dataGridView.DataSource = ds.Tables[0];
                ds.Tables[0].Columns[0].ColumnName = "Табельный номер";
                ds.Tables[0].Columns[1].ColumnName = "ФИО";
                ds.Tables[0].Columns[2].ColumnName = "Адрес";
                ds.Tables[0].Columns[3].ColumnName = "Должность";
                ds.Tables[0].Columns[4].ColumnName = "Номер телефона";
                Form1.db.Close();
            }
        }
        private void ExecutePersonalProcedure(string nameProc)
        {
            Form1.db.Open();
            using (FbTransaction trn = Form1.db.BeginTransaction())
            {
                FbCommand command = new FbCommand(nameProc, Form1.db, trn);
                command.CommandType = CommandType.StoredProcedure;
                if (Form1.AddOrEdit == (int)Form1.AddEditOrDelete.Edit)
                {
                    command.Parameters.Add("@TUB_NUMB", FbDbType.SmallInt).Value =
                        mainForm.dataGridView.Rows[Form1.SelectIndex].Cells[0].Value.ToString();
                }
                if (textBoxINN.Text.Length == 0)
                    command.Parameters.Add("@INN", FbDbType.BigInt).Value = null;
                else
                    command.Parameters.Add("@INN", FbDbType.BigInt).Value = textBoxINN.Text;
                command.Parameters.Add("@FIRST_NAME", FbDbType.VarChar).Value = textBoxFirstName.Text;
                command.Parameters.Add("@SECOND_NAME", FbDbType.VarChar).Value = textBoxSecondName.Text;
                command.Parameters.Add("@LAST_NAME", FbDbType.VarChar).Value = textBoxLastName.Text;
                command.Parameters.Add("@PASSPORT", FbDbType.VarChar).Value = textBoxPassport.Text;
                command.Parameters.Add("@ADDRESS", FbDbType.VarChar).Value = textBoxAddress.Text;
                command.Parameters.Add("@DATE_BORN", FbDbType.Date).Value = date;
                if (comboBoxGender.Text == "М")
                    command.Parameters.Add("@GENDER", FbDbType.SmallInt).Value = 0;
                else if (comboBoxGender.Text == "Ж")
                    command.Parameters.Add("@GENDER", FbDbType.SmallInt).Value = 1;
                command.Parameters.Add("@PROFESSION", FbDbType.VarChar).Value = comboBoxFunction.Text;
                if (textBoxNumbOfTel.Text == " (   )   -")
                    command.Parameters.Add("@PHONE_NUMB", FbDbType.VarChar).Value = "";
                else
                    command.Parameters.Add("@PHONE_NUMB", FbDbType.VarChar).Value = textBoxNumbOfTel.Text;
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (FbException ex)
                {
                    MessageBox.Show(ex.Message);
                    Form1.db.Close();
                    return;
                }
                trn.Commit();
                Form1.db.Close();
            }
        }

        private void monthCalendarDayBirth_DateSelected(object sender, DateRangeEventArgs e)
        {
            date = e.End.ToString("dd/MM/yyyy");
        }
    }
}
