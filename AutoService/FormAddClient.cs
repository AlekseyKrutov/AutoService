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
using DbProxy;
using AutoServiceLibrary;
using System.Net.Mail;

namespace AutoService
{
    public partial class FormAddClient : Form
    {
        Form1 mainForm;
        FormAddWayBill formAddWayBill = new FormAddWayBill();
        string oldNameOrg;

        public FormAddClient()
        {
            InitializeComponent();
            textBoxINN.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressOnlyNumb);
            textBoxBill.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressOnlyNumb);
            textBoxKPP.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressOnlyNumb);
            textBoxOKTMO.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressOnlyNumb);
            textBoxOKATO.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressOnlyNumb);
            textBoxOGRN.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressOnlyNumb);
        }
        public FormAddClient(Form1 mainForm) : this ()
        {
            this.mainForm = mainForm;
        }
        public FormAddClient(FormAddWayBill formAddWayBill) : this ()
        {
            this.formAddWayBill = formAddWayBill;
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
            if (!MailIsValid(textBoxEmail.Text) && textBoxEmail.Text.Length != 0)
            {
                MessageBox.Show("Электронная почта введена некорректно!");
                return;
            }
            if (Form1.AddOrEdit == AddEditOrDelete.Add)
            {
                InvokeProcedure.ExecuteClientProcedure(InvokeProcedure.AddClient, textBoxINN.Text, textBoxName.Text, oldNameOrg, textBoxDirector.Text,
                    comboBoxBank.SelectedValue, textBoxNumbOfTel.Text, textBoxBill.Text, textBoxKPP.Text, textBoxOKTMO.Text, textBoxOKATO.Text, textBoxEmail.Text,
                    textBoxOGRN.Text, textBoxAddress.Text, textBoxFactAddress.Text);
                if (formAddWayBill != null && formAddWayBill.Visible)
                {
                    formAddWayBill.FillComboBox(formAddWayBill.comboBoxClient, Form1.db,
                        formAddWayBill.client_query, formAddWayBill.displayMembers[FormAddWayBill.DisplayMembers.Client]);
                    formAddWayBill.comboBoxClient.SelectedIndex = -1;
                    formAddWayBill.comboBoxClient.SelectedValue = textBoxName.Text;
                }
                mainForm.dataGridView.ClearSelection();
            }
            else if (Form1.AddOrEdit == AddEditOrDelete.Edit)
            {
                InvokeProcedure.ExecuteClientProcedure(InvokeProcedure.UpdateClient, textBoxINN.Text, textBoxName.Text, oldNameOrg, textBoxDirector.Text,
                    comboBoxBank.SelectedValue, textBoxNumbOfTel.Text, textBoxBill.Text, textBoxKPP.Text, textBoxOKTMO.Text, textBoxOKATO.Text, textBoxEmail.Text,
                    textBoxOGRN.Text, textBoxAddress.Text, textBoxFactAddress.Text);
            }
            Form1.AddListClientInGrid(mainForm.dataGridView);
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
        public bool MailIsValid(string emailaddress)
        {
            if (emailaddress.Length == 0)
                return false;
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
