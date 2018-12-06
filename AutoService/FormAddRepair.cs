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
        public int id_repair; 
        public FormAddRepair()
        {
            InitializeComponent();
        }
        public FormAddRepair(FormAddAuto addAuto, Form1 mainForm)
        {
            formAddAutoInRepairs = addAuto;
            this.mainForm = mainForm;
            formForSelect = new FormForSelect(this, mainForm);
            InitializeComponent();
        }
        private void FormAddRepair_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && Form1.AddOrEdit == (int) Form1.AddEditOrDelete.Add)
                DeleteSimpleRepair();
            Form1.WindowIndex = (int)Form1.WindowsStruct.Repairs;
        }
        private void btnAddNewAutoRepair_Click(object sender, EventArgs e)
        {
            Form1.WindowIndex = (int)Form1.WindowsStruct.Repairs;  
            logicForAddRepair = true;
            formAddAutoInRepairs.ShowDialog();
        }

        private void btnSelExistAutoRepair_Click(object sender, EventArgs e)
        {
            Form1.SelectIndex = 0;
            Form1.WindowIndex = (int)Form1.WindowsStruct.Repairs;
            formForSelect.ShowDialog();
        }

        private void btnSelectPersonal_Click(object sender, EventArgs e)
        {
            if (AutoNotSelected())
                return;
            Form1.SelectIndex = 0;
            Form1.WindowIndex = (int) Form1.WindowsStruct.WorkerAdd;
            formForSelect.ShowDialog();
        }
        private void btnShowWorker_Click(object sender, EventArgs e)
        {
            if (AutoNotSelected())
                return;
            Form1.SelectIndex = 0;
            Form1.WindowIndex = (int)Form1.WindowsStruct.WorkerView;
            formForSelect.ShowDialog();
        }
        private void btnAddMalf_Click(object sender, EventArgs e)
        {
            if (AutoNotSelected())
                return;
            Form1.SelectIndex = 0;
            Form1.WindowIndex = (int)Form1.WindowsStruct.MalfAdd;
            formForSelect.ShowDialog();
        }

        private void btnShowMalf_Click(object sender, EventArgs e)
        {
            if (AutoNotSelected())
                return;
            Form1.WindowIndex = (int)Form1.WindowsStruct.MalfView;
            formForSelect.ShowDialog();
        }
        private void btnSelSparePart_Click(object sender, EventArgs e)
        {
            if (AutoNotSelected())
                return;
            Form1.SelectIndex = 0;
            Form1.WindowIndex = (int)Form1.WindowsStruct.SpareAdd;
            formForSelect.ShowDialog();
        }
        private void btnShowSparePart_Click(object sender, EventArgs e)
        {
            if (AutoNotSelected())
                return;
            Form1.SelectIndex = 0;
            Form1.WindowIndex = (int)Form1.WindowsStruct.SpareView;
            formForSelect.ShowDialog();
        }
        private bool AutoNotSelected()
        {
            if (textBoxVIN.Text.Length == 0)
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
            AddRepairProcedure(id_repair, textBoxVIN.Text, textBoxNotes.Text);
            Form1.AddListRepairsInGrid(mainForm.dataGridView);
            this.FormClosing -= FormAddRepair_FormClosing;
            this.Close();
        }
        public int GetIdRepair(string car_vin)
        {
            int id_repair = 0;
            Form1.db.Open();
            using (FbCommand command = new FbCommand("CREATE_SIMPLE_REPAIR_PROCEDURE", Form1.db))
            {
                command.CommandType = CommandType.StoredProcedure;
                FbTransaction trn = Form1.db.BeginTransaction();
                command.Transaction = trn;
                command.Parameters.Add("@CAR_VIN", FbDbType.VarChar).Value = car_vin;
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
                if (Form1.WindowIndex == (int)Form1.WindowsStruct.MalfView)
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
                else if (Form1.WindowIndex == (int)Form1.WindowsStruct.SpareView)
                {
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
                }
                else if(Form1.WindowIndex == (int)Form1.WindowsStruct.WorkerView)
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
        public void AddRepairProcedure(int id_card, string vin, string notes)
        {
            Form1.db.Open();
            using (FbTransaction trn = Form1.db.BeginTransaction())
            {
                FbCommand command = new FbCommand("UPDATE_CARD_OF_REPAIR", Form1.db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_CARD", FbDbType.SmallInt).Value = id_repair;
                command.Parameters.Add("@VIN", FbDbType.VarChar).Value = vin;
                command.Parameters.Add("@NOTES", FbDbType.VarChar).Value = notes;
                if (Form1.AddOrEdit == (int)Form1.AddEditOrDelete.Add)
                    command.Parameters.Add("@START_DATE", FbDbType.TimeStamp).Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                else
                    command.Parameters.Add("@START_DATE", FbDbType.TimeStamp).Value = null;
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
        
    }
}
