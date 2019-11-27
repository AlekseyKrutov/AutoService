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
using FirebirdSql.Data.FirebirdClient;
using AutoServiceLibrary;

namespace AutoService
{
    public partial class FormAddRepair : Form
    {
        public static bool logicForAddRepair;
        FormAddAuto formAddAutoInRepairs;
        FormForSelect formForSelect;
        Form1 mainForm;
        static public int addOrEditInRepair;
        public int id_repair; 
        public FormAddRepair()
        {
            InitializeComponent();
        }
        public FormAddRepair(FormAddAuto addAuto, Form1 mainForm) : this()
        {
            formAddAutoInRepairs = addAuto;
            this.mainForm = mainForm;
            formForSelect = new FormForSelect(this, mainForm);
        }
        private void FormAddRepair_Load(object sender, EventArgs e)
        {
            if (Form1.WindowIndex == Form1.WindowsStruct.ActOfEndsRepairs)
            {
                btnAddNewAutoRepair.Enabled = false;
                btnSelExistAutoRepair.Enabled = false;
                btnAddRepair.Visible = false;
                btnAddMalf.Visible = false;
                btnSelSparePart.Visible = false;
                btnSelectPersonal.Visible = false;
                btnShowMalf.Location = btnAddMalf.Location;
                btnShowSparePart.Location = btnSelSparePart.Location;
                btnShowWorker.Location = btnSelectPersonal.Location;
                checkBoxTurnTime.Visible = false;
                dateTimeStart.Enabled = false;
                dateTimeFinish.Enabled = false;
                textBoxNotes.Enabled = false;
            }
        }
        private void FormAddRepair_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!btnAddNewAutoRepair.Enabled)
            {
                Form1.WindowIndex = Form1.WindowsStruct.ActOfEndsRepairs;
                return;
            }
            if (e.CloseReason == CloseReason.UserClosing && addOrEditInRepair == (int) Form1.AddEditOrDelete.Add)
                DeleteSimpleRepair();
            Form1.WindowIndex = Form1.WindowsStruct.Repairs;
        }
        private void btnAddNewAutoRepair_Click(object sender, EventArgs e)
        {
            Form1.WindowIndex = Form1.WindowsStruct.AddAutoInRep;  
            logicForAddRepair = true;
            formAddAutoInRepairs = new FormAddAuto(this, mainForm);
            formAddAutoInRepairs.ShowDialog();
        }

        private void btnSelExistAutoRepair_Click(object sender, EventArgs e)
        {
            Form1.SelectIndex = 0;
            Form1.WindowIndex = Form1.WindowsStruct.ViewAutoInRep;
            formForSelect.ShowDialog();
        }

        private void btnSelectPersonal_Click(object sender, EventArgs e)
        {
            if (AutoNotSelected())
                return;
            Form1.SelectIndex = 0;
            Form1.WindowIndex = Form1.WindowsStruct.WorkerAdd;
            formForSelect.ShowDialog();
        }
        private void btnShowWorker_Click(object sender, EventArgs e)
        {
            if (AutoNotSelected())
                return;
            Form1.SelectIndex = 0;
            Form1.WindowIndex = Form1.WindowsStruct.WorkerView;
            formForSelect.ShowDialog();
        }
        private void btnAddMalf_Click(object sender, EventArgs e)
        {
            if (AutoNotSelected())
                return;
            Form1.SelectIndex = 0;
            Form1.WindowIndex = Form1.WindowsStruct.MalfAdd;
            formForSelect.ShowDialog();
        }

        private void btnShowMalf_Click(object sender, EventArgs e)
        {
            if (AutoNotSelected())
                return;
            Form1.WindowIndex = Form1.WindowsStruct.MalfView;
            formForSelect.ShowDialog();
        }
        private void btnSelSparePart_Click(object sender, EventArgs e)
        {
            if (AutoNotSelected())
                return;
            Form1.SelectIndex = 0;
            Form1.WindowIndex = Form1.WindowsStruct.SpareAdd;
            formForSelect.ShowDialog();
        }
        private void btnShowSparePart_Click(object sender, EventArgs e)
        {
            if (AutoNotSelected())
                return;
            Form1.SelectIndex = 0;
            Form1.WindowIndex = Form1.WindowsStruct.SpareView;
            formForSelect.ShowDialog();
        }
        private bool AutoNotSelected()
        {
            if (textBoxGosNom.Text.Length == 0)
            {
                MessageBox.Show("Сначала необходимо выбрать автомобиль");
                return true;
            }
            else
                return false;
        }
        //событие при нажатии на кнопку добавить ремонт
        private void btnAddRepair_Click(object sender, EventArgs e)
        {
            if (textBoxMark.Text.Length == 0)
            {
                MessageBox.Show("Пожалуйста выберете автомобиль!");
                return;
            }
            if (checkBoxTurnTime.Checked && (dateTimeStart.Value.Date > dateTimeFinish.Value.Date))
            {
                MessageBox.Show("Дата начала ремонта меньше даты завершения!");
                return;
            }
            AddRepairProcedure(id_repair, textBoxGosNom.Text, textBoxNotes.Text);
            Form1.AddListRepairsInGrid(mainForm.dataGridView);
            this.FormClosing -= FormAddRepair_FormClosing;
            this.Close();
        }
        public int GetIdRepair(string state_number)
        {
            Form1.db.Open();
            using (FbCommand command = new FbCommand("CREATE_SIMPLE_REPAIR_PROCEDURE", Form1.db))
            {
                command.CommandType = CommandType.StoredProcedure;
                FbTransaction trn = Form1.db.BeginTransaction();
                command.Transaction = trn;
                command.Parameters.Add("@STATE_NUMBER", FbDbType.VarChar).Value = state_number;
                FbDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                   id_repair  = int.Parse(dr.GetString(0));
                }
                dr.Close();
                trn.Commit();
            }
            Form1.db.Close();
            return id_repair;
        }
        public void DeleteSimpleRepair()
        {
            Form1.db.Open();
            using (FbCommand command = new FbCommand("DELETE_REPAIR_PROCEDURE", Form1.db))
            {
                FbTransaction trn = Form1.db.BeginTransaction();
                command.Transaction = trn;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_REPAIR", FbDbType.VarChar).Value = id_repair;
                command.ExecuteNonQuery();
                trn.Commit();
            }
            Form1.db.Close();
        }
        public void ExecuteProcedureForAddMalf(int id_repair, string description, int number)
        {
            Form1.db.Open();
            using (FbTransaction trn = Form1.db.BeginTransaction())
            {
                FbCommand command = new FbCommand("INS_OR_UP_WORKS_AND_REP", Form1.db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_CARD", FbDbType.SmallInt).Value = id_repair;
                command.Parameters.Add("@DESCRIPTION", FbDbType.VarChar).Value = description;
                command.Parameters.Add("@NUMBER", FbDbType.SmallInt).Value = number;
                command.ExecuteNonQuery();
                trn.Commit();
                Form1.db.Close();
            }
        }
        public void ExecuteProcedureForAddSparepart(int id_repair, int uniq_code, int number)
        {
            Form1.db.Open();
            using (FbTransaction trn = Form1.db.BeginTransaction())
            {
                FbCommand command = new FbCommand("INS_OR_UP_SPARE_REPAIR", Form1.db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_CARD", FbDbType.SmallInt).Value = id_repair;
                command.Parameters.Add("@UNIQ_CODE", FbDbType.Integer).Value = uniq_code;
                command.Parameters.Add("@NUMBER", FbDbType.SmallInt).Value = number;
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (FbException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                trn.Commit();
                Form1.db.Close();
            }
        }
        public void ExecuteProcedureForAddWorker(int id_repair, int tub_numb)
        {
            Form1.db.Open();
            using (FbTransaction trn = Form1.db.BeginTransaction())
            {
                FbCommand command = new FbCommand("INS_STAFF_REPAIR", Form1.db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_CARD", FbDbType.SmallInt).Value = id_repair;
                command.Parameters.Add("@TUB_NUMB", FbDbType.SmallInt).Value = tub_numb;
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (FbException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                trn.Commit();
                Form1.db.Close();
            }
        }
        public void ExecuteProcedureDelete(int id_repair, int uniqCodeOrTubNumb, string description, string nameProc)
        {
            Form1.db.Open();
            using (FbTransaction trn = Form1.db.BeginTransaction())
            {
                FbCommand command = new FbCommand(nameProc, Form1.db, trn);
                command.CommandType = CommandType.StoredProcedure;
                if (Form1.WindowIndex == Form1.WindowsStruct.MalfView)
                {
                    command.Parameters.Add("@ID_CARD", FbDbType.SmallInt).Value = id_repair;
                    command.Parameters.Add("@DESCRIPTION", FbDbType.Integer).Value = description;
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (FbException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    trn.Commit();
                    Form1.db.Close();
                    return;
                }
                else if (Form1.WindowIndex == Form1.WindowsStruct.SpareView)
                {
                    command.Parameters.Add("@ID_CARD", FbDbType.SmallInt).Value = id_repair;
                    command.Parameters.Add("@DESCRIPTION", FbDbType.Integer).Value = description;
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (FbException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    trn.Commit();
                    Form1.db.Close();
                    return;
                    /*
                    command.Parameters.Add("@ID_CARD", FbDbType.SmallInt).Value = id_repair;
                    command.Parameters.Add("@UNIQ_CODE", FbDbType.Integer).Value = uniqCodeOrTubNumb;
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (FbException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    trn.Commit();
                    Form1.db.Close();
                    return;
                    */
                }
                else if(Form1.WindowIndex == Form1.WindowsStruct.WorkerView)
                {
                    command.Parameters.Add("@ID_CARD", FbDbType.SmallInt).Value = id_repair;
                    command.Parameters.Add("@TUB_NUMB", FbDbType.Integer).Value = uniqCodeOrTubNumb;
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (FbException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    trn.Commit();
                    Form1.db.Close();
                    return;
                }
            } 
        }
        public void AddRepairProcedure(int id_card, string state_number, string notes)
        {
            Form1.db.Open();
            using (FbTransaction trn = Form1.db.BeginTransaction())
            {
                FbCommand command = new FbCommand("UPDATE_CARD_OF_REPAIR", Form1.db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_CARD", FbDbType.SmallInt).Value = id_repair;
                command.Parameters.Add("@STATE_NUMBER", FbDbType.VarChar).Value = state_number;
                command.Parameters.Add("@NOTES", FbDbType.VarChar).Value = notes;
                if (!checkBoxTurnTime.Checked)
                {
                    command.Parameters.Add("@START_DATE", FbDbType.TimeStamp).Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                    command.Parameters.Add("@FINISH_DATE", FbDbType.TimeStamp).Value = null;
                }
                else if (checkBoxTurnTime.Checked)
                {
                    command.Parameters.Add("@START_DATE", FbDbType.TimeStamp).Value = dateTimeStart.Value.ToString("dd/MM/yyyy HH:mm");
                    command.Parameters.Add("@FINISH_DATE", FbDbType.TimeStamp).Value = dateTimeFinish.Value.ToString("dd/MM/yyyy HH:mm");
                }
                else
                {
                    command.Parameters.Add("@START_DATE", FbDbType.TimeStamp).Value = null;
                    command.Parameters.Add("@FINISH_DATE", FbDbType.TimeStamp).Value = null;
                }
                
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (FbException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                trn.Commit();
                Form1.db.Close();
            }
        }

        private void checkBoxTurnTime_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTurnTime.Checked)
            {
                dateTimeStart.Enabled = true;
                dateTimeFinish.Enabled = true;
            }
            else
            {
                dateTimeStart.Enabled = false;
                dateTimeFinish.Enabled = false;
            }
        }
    }
}
