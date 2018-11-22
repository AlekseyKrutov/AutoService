using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using FirebirdSql.Data.FirebirdClient;
using AutoServiceLibrary;

namespace AutoService
{
    public partial class FormAddClient : Form
    {
        Form1 mainForm;
        string oldNameOrg;
        public FormAddClient()
        {
            InitializeComponent();
            textBoxINN.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressOnlyNumb);
            textBoxBill.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressOnlyNumb);
            textBoxKPP.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressOnlyNumb);
            textBoxOKTMO.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressOnlyNumb);
            textBoxOKATO.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressOnlyNumb);
            textBoxBIK.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressOnlyNumb);
            textBoxOGRN.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressOnlyNumb);
        }
        public FormAddClient(Form1 mainForm) : this ()
        {
            this.mainForm = mainForm;
        }
        private void FormAddClient_Load(object sender, EventArgs e)
        {
            oldNameOrg = textBoxName.Text;
        }
        private void buttonAddClient_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text.Length == 0 || textBoxDirector.Text.Length == 0)
            {
                MessageBox.Show("Вы ввели не все данные!");
                return;
            }
            if (Form1.AddOrEdit == (int)Form1.AddEditOrDelete.Add)
            {
                ExecuteClientProcedure("NEW_CLIENT_PROCEDURE");
                mainForm.dataGridView.ClearSelection();
            }
            else if (Form1.AddOrEdit == (int)Form1.AddEditOrDelete.Edit)
            {
                ExecuteClientProcedure("UPDATE_CLIENT_PROCEDURE");
            }
            AddListClientInGrid();
            this.Close();
            mainForm.dataGridView.ClearSelection();
            mainForm.dataGridView.Rows[Form1.SelectIndex].Selected = true;
        }

        private void textBoxINN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void ExecuteClientProcedure(string nameProc)
        {
            Form1.db.Open();
            using (FbTransaction trn = Form1.db.BeginTransaction())
            {
                FbCommand command = new FbCommand(nameProc, Form1.db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@INN", FbDbType.VarChar).Value = textBoxINN.Text;
                command.Parameters.Add("@NAME_ORG", FbDbType.VarChar).Value = textBoxName.Text;
                command.Parameters.Add("@OLD_NAME_ORG", FbDbType.VarChar).Value = oldNameOrg;
                command.Parameters.Add("@DIRECTOR", FbDbType.VarChar).Value = textBoxDirector.Text;
                if (comboBoxBank.Text.Length == 0)
                    command.Parameters.Add("@BANK_BILL", FbDbType.VarChar).Value = null;
                else
                    command.Parameters.Add("@BANK_BILL", FbDbType.VarChar).Value = comboBoxBank.Text; ;
                if (textBoxNumbOfTel.Text == " (   )   -")
                    command.Parameters.Add("@PHONE_NUMB", FbDbType.VarChar).Value = "";
                else
                    command.Parameters.Add("@PHONE_NUMB", FbDbType.VarChar).Value = textBoxNumbOfTel.Text;
                command.Parameters.Add("@BILL", FbDbType.VarChar).Value = textBoxBill.Text;
                command.Parameters.Add("@KPP", FbDbType.VarChar).Value = textBoxKPP.Text;
                command.Parameters.Add("@OKTMO", FbDbType.VarChar).Value = textBoxOKTMO.Text;
                command.Parameters.Add("@OKATO", FbDbType.VarChar).Value = textBoxOKATO.Text;
                command.Parameters.Add("@BIK", FbDbType.VarChar).Value = textBoxBIK.Text;
                command.Parameters.Add("@OGRN", FbDbType.VarChar).Value = textBoxOGRN.Text;
                command.Parameters.Add("@ADDRESS", FbDbType.VarChar).Value = textBoxAddress.Text;
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
        private void AddListClientInGrid()
        {
            string query = @"select * from client_view";
            string[] columnNames = { "Наименование", "Директор", "ИНН", "Номер телефона" };
            Form1.CreateViewForDataGrid(query, columnNames, mainForm.dataGridView);
        }
    }
}
