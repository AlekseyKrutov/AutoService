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
        FormForSelect formSelectAuto;
        FormForSelect formSelectWorker;
        FormForSelect formSelectMalf;
        FormForSelect formViewMalf;
        FormForSelect formSelectSparePart;
        Form1 mainForm;
        public List<Malfunctions> malfList = new List<Malfunctions>();
        public List<SparePart> spareList = new List<SparePart>();
        public int id_repair; 
        public FormAddRepair()
        {
            InitializeComponent();
        }
        public FormAddRepair(FormAddAuto addAuto, Form1 mainForm)
        {
            foreach (Malfunctions malf in Malfunctions.MalfList) 
            {
                Form1.malfListForRepairAll.Add(malf);
            }
            formAddAutoInRepairs = addAuto;
            this.mainForm = mainForm;
            InitializeComponent();
        }
        private void FormAddRepair_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (id_repair != 0)
            {
                DeleteSimpleRepair();
            }
            Form1.WindowIndex = (int)Form1.WindowsStruct.Repairs;
            Form1.malfListForRepairAll.Clear();
            Form1.malfListForRepairAdded.Clear();
        }
        private void btnAddNewAutoRepair_Click(object sender, EventArgs e)
        {
            Form1.WindowIndex = (int)Form1.WindowsStruct.Repairs;
            if (!formAddAutoInRepairs.Visible)
            {   
                logicForAddRepair = true;
                formAddAutoInRepairs = new FormAddAuto(this, mainForm);
                formAddAutoInRepairs.StartPosition = FormStartPosition.CenterScreen;
                formAddAutoInRepairs.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                formAddAutoInRepairs.MaximizeBox = false;
                formAddAutoInRepairs.ShowDialog();
            }
            else
                return;
        }

        private void btnSelExistAutoRepair_Click(object sender, EventArgs e)
        {
            if (id_repair != 0)
                DeleteSimpleRepair();
            Form1.SelectIndex = 0;
            Form1.WindowIndex = (int)Form1.WindowsStruct.Repairs;
            formSelectAuto = new FormForSelect(this, mainForm);
            formSelectAuto.ShowDialog();
        }

        private void btnSelectPersonal_Click(object sender, EventArgs e)
        {
            if (AutoNotSelected())
                return;
            Form1.SelectIndex = 0;
            Form1.WindowIndex = (int) Form1.WindowsStruct.Worker;
            formSelectWorker = new FormForSelect(this, mainForm);
            formSelectWorker.ShowDialog();
        }

        private void btnAddMalf_Click(object sender, EventArgs e)
        {
            if (AutoNotSelected())
                return;
            Form1.SelectIndex = 0;
            Form1.WindowIndex = (int)Form1.WindowsStruct.MalfAdd;
            formSelectMalf = new FormForSelect(this, mainForm);
            formSelectMalf.ShowDialog();
        }

        private void btnShowMalf_Click(object sender, EventArgs e)
        {
            if (AutoNotSelected())
                return;
            Form1.WindowIndex = (int)Form1.WindowsStruct.MalfView;
            formViewMalf = new FormForSelect(this, mainForm);
            formViewMalf.ShowDialog();
        }
        private void btnSelSparePart_Click(object sender, EventArgs e)
        {
            if (AutoNotSelected())
                return;
            Form1.SelectIndex = 0;
            Form1.WindowIndex = (int)Form1.WindowsStruct.SpareAdd;
            formSelectSparePart = new FormForSelect(this, mainForm);
            formSelectSparePart.ShowDialog();
        }
        private void btnShowSparePart_Click(object sender, EventArgs e)
        {
            if (AutoNotSelected())
                return;
            Form1.SelectIndex = 0;
            Form1.WindowIndex = (int)Form1.WindowsStruct.SpareView;
            formSelectSparePart = new FormForSelect(this, mainForm);
            formSelectSparePart.ShowDialog();
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
            else if (Form1.malfListForRepairAdded.Count == 0)
            {
                MessageBox.Show("Пожалуйста выберете неисправности!");
                return;
            }
            else if (SelectedPersonLabel.Text.Length == 0)
            {
                MessageBox.Show("Пожалуйста выберете работника!");
                return;
            }
            Form1.malfListForRepairAll.Clear();
            Form1.malfListForRepairAdded.Clear();
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
                command.Parameters.Add("@DUNIQ_CODE", FbDbType.Integer).Value = uniq_code;
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

        
    }
}
