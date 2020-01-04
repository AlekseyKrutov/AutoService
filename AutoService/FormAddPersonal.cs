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
using DataMapper;
using DbProxy;

namespace AutoService
{
    public partial class FormAddPersonal : Form
    {
        Form1 mainForm;
        FormAddWayBill formAddWayBill = new FormAddWayBill();

        public Employee emp = null;

        bool dateSelected = false;

        public string date;
        public FormAddPersonal()
        {
            InitializeComponent();
            textBoxINN.KeyPress += new KeyPressEventHandler(EventsInForm.KeyPressOnlyNumb);
        }
        public FormAddPersonal(Form1 mainForm) : this()
        {
            this.mainForm = mainForm;
            DbProxy.DataSets.CreateDsForComboBox(comboBoxFunction, Queries.Profession, "prof", "id_prof");
        }
        public FormAddPersonal(FormAddWayBill formAddWayBill) : this()
        {
            this.formAddWayBill = formAddWayBill;
        }
        private void FormAddPersonal_Load(object sender, EventArgs e)
        {
        }
        private void buttonAddPersonal_Click(object sender, EventArgs e)
        {
            if (textBoxLastName.Text.Length == 0 || textBoxFirstName.Text.Length == 0
                || comboBoxGender.Text.Length == 0 || comboBoxFunction.Text.Length == 0 ||
                (dateSelected == false && Form1.AddOrEdit == AddEditOrDelete.Add))
            {
                MessageBox.Show("Вы ввели не все данные!");
                return;
            }
            Functions funcCmb = (Functions)int.Parse(comboBoxFunction.SelectedValue.ToString());
            if (emp == null)
            {
                emp = new Employee(textBoxINN.Text, textBoxFirstName.Text, textBoxSecondName.Text,
                    textBoxLastName.Text, textBoxPassport.Text, textBoxAddress.Text, Convert.ToDateTime(date), 
                    UnitsConvert.ConvertSex(comboBoxGender.Text), textBoxNumbOfTel.Text, funcCmb);
                EmployeeMapper em = new EmployeeMapper();
                try
                {
                    emp = em.Insert(emp);
                }
                catch (Exception ex)
                {
                    emp = null;
                    MessageBox.Show("При добавлении данных произошла ошибка - " +
                        $"{ex.Message}");
                    return;
                }
            }
            else
            {
                EmployeeMapper em = new EmployeeMapper();
                emp.INN = textBoxINN.Text;
                emp.FirstName = textBoxFirstName.Text;
                emp.SecondName = textBoxSecondName.Text;
                emp.LastName = textBoxLastName.Text;
                emp.Passport = textBoxPassport.Text;
                emp.Address = textBoxAddress.Text;
                emp.BornDate = Convert.ToDateTime(date);
                emp.Gender = UnitsConvert.ConvertSex(comboBoxGender.Text);
                emp.Function = funcCmb;
                emp.PhoneNumb = textBoxNumbOfTel.Text;
                em.Update(emp);
            }
            Form1.AddListPersonalInGrid(mainForm.dataGridView, Queries.StaffView);
            this.Close();
            mainForm.dataGridView.ClearSelection();
            mainForm.dataGridView.Rows[Form1.SelectIndex].Selected = true;
        }
        private void monthCalendarDayBirth_DateSelected(object sender, DateRangeEventArgs e)
        {
            dateSelected = true;
            date = e.End.ToString("dd/MM/yyyy");
        }

        private void FormAddPersonal_FormClosing(object sender, FormClosingEventArgs e)
        {
            emp = null;
        }
    }
}
