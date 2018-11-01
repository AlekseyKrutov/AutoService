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

namespace AutoService
{
    public partial class FormAuthorization : Form
    {
        Form1 mainForm;
        int authVer;
        public FormAuthorization()
        {
            InitializeComponent();
            labelError.Visible = false;
        }
        public FormAuthorization(Form1 mainForm) : this ()
        {
            this.mainForm = mainForm;
        }
        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if (textBoxLogin.Text.Length == 0 || textBoxPassword.Text.Length == 0)
                return;
            Form1.db.Open();
            using (FbCommand command = new FbCommand("VERIFICATION_PROCEDURE", Form1.db))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@PASS", FbDbType.VarChar).Value = textBoxPassword.Text;
                command.Parameters.Add("@TUB_NUMB", FbDbType.SmallInt).Value = textBoxLogin.Text;
                FbDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    authVer = int.Parse(dr.GetString(0));
                }
                Form1.db.Close();
            }
            if (authVer == 1)
            {
                this.FormClosing -= FormAuthorization_FormClosing;
                this.Close();
                mainForm.Show();
            }
            else
            {
                labelError.Visible = true;
                return;
            }
        }

        private void textBoxLogin_MouseClick(object sender, MouseEventArgs e)
        {
            labelError.Visible = false;
        }
        private void textBoxPassword_MouseClick(object sender, MouseEventArgs e)
        {
            labelError.Visible = false;
        }
        private void FormAuthorization_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
