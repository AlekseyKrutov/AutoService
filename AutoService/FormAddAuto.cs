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
using DbProxy;

namespace AutoService
{
    public partial class FormAddAuto : Form
    {
        FormForSelect formSelectAuto;
        FormAddRepair formAddCarInRepair;
        FormAddWayBill formAddWayBill;

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
            formAddCarInRepair = addRepair;
            this.mainForm = mainForm; 
        }
        public FormAddAuto(FormAddWayBill formAddWayBill) : this ()
        {
            this.formAddWayBill = formAddWayBill;
        }
        private void FormAddAuto_Load(object sender, EventArgs e)
        {
            if (formAddCarInRepair != null && formAddCarInRepair.Visible)
            {
                DataSets.CreateDsForComboBox(comboBoxAuto, Queries.CarModelView, "MARK_MODEL", "", AddEditOrDelete.Add);
                return;
            }
            if (Form1.AddOrEdit == AddEditOrDelete.Edit)
            {
                comboBoxAuto.Text = mainForm.dataGridView.Rows[Form1.SelectIndex].Cells[1].Value.ToString();
            }
            selectedIndex = Form1.SelectIndex;
        }
        private void buttonAddAuto_Click(object sender, EventArgs e)
        {
            if (textBoxGosNumb.Text.Length == 0 || 
                comboBoxAuto.Text.Length == 0 || (textBoxReg.Text.Length > 0 && textBoxReg.Text.Length < 10))
            {
                MessageBox.Show("Вы ввели не все данные!");
                return;
            }
            if (formAddCarInRepair != null && formAddCarInRepair.Visible)
            {
                InvokeProcedure.ExecuteAutoProcedure("NEW_CAR_PROCEDURE",
                    textBoxVIN.Text, textBoxGosNumb.Text, textBoxReg.Text, comboBoxAuto.Text, labelContentOwner.Text);
                ReadAutoFromViewForRepair(textBoxGosNumb.Text, formAddCarInRepair);
                formAddCarInRepair.GetIdRepair(textBoxGosNumb.Text);
                Form1.WindowIndex = WindowsStruct.Repairs;
                this.Close();
                return;
            }
            if (Form1.AddOrEdit == AddEditOrDelete.Add)
            {
                InvokeProcedure.ExecuteAutoProcedure("NEW_CAR_PROCEDURE", textBoxVIN.Text, textBoxGosNumb.Text, textBoxReg.Text,
                    comboBoxAuto.Text, labelContentOwner.Text);
                ReadAutoFromViewForRepair(textBoxGosNumb.Text, formAddCarInRepair);
                if (formAddWayBill != null)
                {
                    formAddWayBill.FillComboBox(formAddWayBill.comboBoxCar, Form1.db,
                        formAddWayBill.car_query, formAddWayBill.displayMembers[FormAddWayBill.DisplayMembers.Car],
                        formAddWayBill.valueMembers[FormAddWayBill.ValueMembers.Car]);
                    formAddWayBill.comboBoxCar.SelectedIndex = -1;
                    formAddWayBill.comboBoxCar.SelectedValue = textBoxGosNumb.Text;
                    this.Close();
                    return;
                }
            }

            else if (Form1.AddOrEdit == AddEditOrDelete.Edit)
            {
                InvokeProcedure.ExecuteAutoProcedure("UPDATE_CAR_PROCEDURE", textBoxVIN.Text, textBoxGosNumb.Text, textBoxReg.Text,
                    comboBoxAuto.Text, labelContentOwner.Text);
                ReadAutoFromViewForRepair(textBoxGosNumb.Text, formAddCarInRepair);
                mainForm.dataGridView.Rows[selectedIndex].Selected = true;
            }
            Form1.AddListAutoInGrid(mainForm.dataGridView);
            mainForm.dataGridView.ClearSelection();
            if (Form1.SelectIndex != 0)
                mainForm.dataGridView.Rows[Form1.SelectIndex].Selected = true;
            this.Close();
        }
        
        private void buttonAddOwner_Click(object sender, EventArgs e)
        {
            Form1.SelectIndex = 0;
            Form1.WindowIndex = WindowsStruct.AddClientInAuto;
            formSelectAuto = new FormForSelect(this, mainForm);
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
        public static void ReadAutoFromViewForRepair(string state_number, FormAddRepair addRepair)
        {
            using (FbCommand command = new FbCommand(Queries.GetCarViaNumber(state_number), Form1.db))
            {
                FbDataReader dr;
                Form1.db.Open();
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    addRepair.textBoxVIN.Text = dr.GetString(dr.GetOrdinal("VIN"));
                    addRepair.textBoxMark.Text = dr.GetString(dr.GetOrdinal("CAR_MODEL"));
                    addRepair.textBoxGosNom.Text = dr.GetString(dr.GetOrdinal("STATE_NUMBER"));
                    addRepair.textBoxReg.Text = dr.GetString(dr.GetOrdinal("CERTIFICATE"));
                    addRepair.textBoxOwner.Text = dr.GetString(dr.GetOrdinal("ORG"));
                }
                Form1.db.Close();
            }
        }
    }
}
