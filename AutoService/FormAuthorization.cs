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
using DataMapper;
using AutoServiceLibrary;

namespace AutoService
{
    public partial class FormAuthorization : Form
    {
        Form1 mainForm;
        int authVer;
        Account account = null;

        public FormAuthorization()
        {
            InitializeComponent();
            textBoxLogin.KeyDown += new KeyEventHandler(KeyDownEnter);
            textBoxPassword.KeyDown += new KeyEventHandler(KeyDownEnter);
            labelError.Visible = false;
        }
        public FormAuthorization(Form1 mainForm) : this ()
        {
            this.mainForm = mainForm;
        }
        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if (textBoxLogin.Text.Length == 0 || textBoxPassword.Text.Length == 0)
            {
                labelError.Text = "Пожалуйста введите все данные.";
                labelError.Visible = true;
                return;
            }
            account = new AccountMapper().Get(textBoxLogin.Text);
            if (account == null)
            {
                labelError.Text = "Пользователь не найден в системе.";
                labelError.Visible = true;
                return;
            }
            else if (account.VerifyUser(textBoxPassword.Text))
            {
                mainForm.labelAccount.Text = account.Login;
                mainForm.account = account;
                this.FormClosing -= FormAuthorization_FormClosing;
                this.Close();
                mainForm.Show();
            }
            else
            {
                labelError.Text = "Введены неверные учетные данные";
                labelError.Visible = true;
                return;
            }
            account = null;
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
        private void KeyDownEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonEnter_Click(sender, e);
            return;
        }
    }
}
