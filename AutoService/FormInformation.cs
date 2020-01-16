using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataMapper;
using AutoServiceLibrary;

namespace AutoService
{
    public partial class FormInformation : Form
    {
        public CardOfRepair card = null;
        public WayBill wayBill = null;

        Form1 mainForm;

        public FormInformation()
        {
            InitializeComponent();
        }

        private void SendMail_Click(object sender, EventArgs e)
        {
            MailSender mailSender = new MailSender();
            SystemOwner systemOwner = new OwnerMapper().Get();
            string subject = "";
            try
            {
                if (card != null)
                {
                    subject = $"Информация по ремонту №{card.IdRepair}";
                    mailSender = new MailSender(card.Car.Owner, systemOwner,
                        subject, card.ToString());
                }
                else if (wayBill != null)
                {
                    subject = $"Информация по грузоперевозке №{wayBill.IdWayBill}";
                    mailSender = new MailSender(wayBill.Client, systemOwner,
                        subject, wayBill.ToString());
                }
                mailSender.SendMessage();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Сообщение не отправлено ошибка:\n {ex.Message}");
            }
        }
    }
}
