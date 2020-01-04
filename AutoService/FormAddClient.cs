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
using DataMapper;
using DbProxy;
using AutoServiceLibrary;
using System.Net.Mail;

namespace AutoService
{
    public partial class FormAddClient : Form
    {
        Form1 mainForm;
        FormAddWayBill formAddWayBill = new FormAddWayBill();
        public Client client = null;

        public FormAddClient()
        {
            InitializeComponent();
            DataSets.CreateDsForComboBox(comboBoxBank, Queries.BankView,
                                            "name_bank", "kor_bill");
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
            if (client == null)
            {
                Client client = new Client(textBoxDirector.Text, textBoxINN.Text, textBoxName.Text, new BankMapper().Get(comboBoxBank.Text),
                    textBoxNumbOfTel.Text, textBoxEmail.Text, textBoxBill.Text, textBoxKPP.Text, textBoxOKTMO.Text, textBoxOKATO.Text,
                    textBoxOGRN.Text, textBoxAddress.Text, textBoxFactAddress.Text);
                try
                {
                    client = new ClientMapper().Insert(client);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("При добавлении данных произошла ошибка - " +
                        $"{ex.Message}");
                    client = null;
                    return;
                }
                if (formAddWayBill != null && formAddWayBill.Visible)
                {
                    formAddWayBill.FillComboBox(formAddWayBill.comboBoxClient, Form1.db,
                        formAddWayBill.client_query, formAddWayBill.displayMembers[FormAddWayBill.DisplayMembers.Client]);
                    formAddWayBill.comboBoxClient.SelectedIndex = -1;
                    formAddWayBill.comboBoxClient.SelectedValue = textBoxName.Text;
                }
                mainForm.dataGridView.ClearSelection();
            }
            else
            {
                ClientMapper cm = new ClientMapper();
                client.Director = textBoxDirector.Text;
                client.INN = textBoxINN.Text;
                client.Name = textBoxName.Text;
                try
                {
                    client.Bank = new BankMapper().Get(comboBoxBank.SelectedValue.ToString());
                }
                catch (NullReferenceException ex) { }
                client.PhoneNumber = textBoxNumbOfTel.Text;
                client.Email = textBoxEmail.Text;
                client.Bill = textBoxBill.Text;
                client.KPP = textBoxKPP.Text;
                client.OKTMO = textBoxOKTMO.Text;
                client.OKATO = textBoxOKATO.Text;
                client.OGRN = textBoxOGRN.Text;
                client.Address = textBoxAddress.Text;
                client.FactAddress = textBoxFactAddress.Text;
                try
                {
                    cm.Update(client);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("При добавлении данных произошла ошибка - " +
                        $"{ex.Message}");
                    return;
                }
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

        private void FormAddClient_FormClosed(object sender, FormClosedEventArgs e)
        {
            client = null;
        }
    }
}
