using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using System.Configuration;
using AutoService;
using System.Windows.Forms;
using AutoServiceLibrary;

namespace DbProxy
{
    
    public static class InvokeProcedure
    {
        public static FbConnection db = new FbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        //наименования процедур
        public static string AddClient = "NEW_CLIENT_PROCEDURE";
        public static string UpdateClient = "UPDATE_CLIENT_PROCEDURE";
        public static int GetIdRepairViaCarNumber(string state_number)
        {
            int id_repair = 0;
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
                    id_repair = int.Parse(dr.GetString(0));
                }
                dr.Close();
                trn.Commit();
            }
            Form1.db.Close();
            return id_repair;
        }
        public static void DeleteSimpleRepair(int id_repair)
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
        public static void AddMalfInRep(int id_repair, string description, int number)
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
        public static void AddSpareInRep(int id_repair, int uniq_code, int number)
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
        public static void AddWorkerInRep(int id_repair, int tub_numb)
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
        public static void AddRepair(int id_repair, string state_number, string notes,
            DateTime? startDate, DateTime? finishDate)
        {
            Form1.db.Open();
            using (FbTransaction trn = Form1.db.BeginTransaction())
            {
                FbCommand command = new FbCommand("UPDATE_CARD_OF_REPAIR", Form1.db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_CARD", FbDbType.SmallInt).Value = id_repair;
                command.Parameters.Add("@STATE_NUMBER", FbDbType.VarChar).Value = state_number;
                command.Parameters.Add("@NOTES", FbDbType.VarChar).Value = notes;
                command.Parameters.Add("@START_DATE", FbDbType.TimeStamp).Value = startDate;
                command.Parameters.Add("@FINISH_DATE", FbDbType.TimeStamp).Value = finishDate;
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
        public static void FinishRepair(int id_card)
        {
            if (db.State != ConnectionState.Open)
                Form1.db.Open();
            using (FbTransaction trn = Form1.db.BeginTransaction())
            {
                FbCommand command = new FbCommand("FINISH_REPAIR", db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_CARD", FbDbType.SmallInt).Value = id_card;
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (FbException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                trn.Commit();
            }
            db.Close();
        }
        public static void StartRepair(int id_card)
        {
            if (db.State != ConnectionState.Open)
                db.Open();
            using (FbTransaction trn = db.BeginTransaction())
            {
                FbCommand command = new FbCommand("START_REPAIR", db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_CARD", FbDbType.SmallInt).Value = id_card;
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (FbException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                trn.Commit();
            }
            db.Close();
        }
        public static void DeleteWorksInRep(int id_repair, string description)
        {
            Form1.db.Open();
            using (FbTransaction trn = Form1.db.BeginTransaction())
            {
                FbCommand command = new FbCommand("DELETE_REPAIRS_WORKS", Form1.db, trn);
                command.CommandType = CommandType.StoredProcedure;
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
            }
        }
        public static void ExecuteClientProcedure(string nameProc, string INN, string nameCl, string oldNameOrg,
           string director,  object bankBill, string phoneNumb, string bill, string KPP, string OKTMO,
           string OKATO, string email, string OGRN, string address, string factAddress)
        {
            if (db.State != ConnectionState.Open)
                db.Open();
            using (FbTransaction trn = db.BeginTransaction())
            {
                FbCommand command = new FbCommand(nameProc, db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@INN", FbDbType.VarChar).Value = INN;
                command.Parameters.Add("@NAME_ORG", FbDbType.VarChar).Value = nameCl;
                if (Form1.AddOrEdit == Form1.AddEditOrDelete.Edit)
                    command.Parameters.Add("@OLD_NAME_ORG", FbDbType.VarChar).Value = oldNameOrg;
                command.Parameters.Add("@DIRECTOR", FbDbType.VarChar).Value = director;
                bankBill = bankBill ?? "";
                if (bankBill.ToString().Length == 0)
                    command.Parameters.Add("@BANK_BILL", FbDbType.VarChar).Value = null;
                else
                    command.Parameters.Add("@BANK_BILL", FbDbType.VarChar).Value = bankBill.ToString();
                if (phoneNumb == " (   )   -")
                    command.Parameters.Add("@PHONE_NUMB", FbDbType.VarChar).Value = "";
                else
                    command.Parameters.Add("@PHONE_NUMB", FbDbType.VarChar).Value = phoneNumb;
                command.Parameters.Add("@EMAIL", FbDbType.VarChar).Value = email;
                command.Parameters.Add("@BILL", FbDbType.VarChar).Value = bill;
                command.Parameters.Add("@KPP", FbDbType.VarChar).Value = KPP;
                command.Parameters.Add("@OKTMO", FbDbType.VarChar).Value = OKTMO;
                command.Parameters.Add("@OKATO", FbDbType.VarChar).Value = OKATO;
                command.Parameters.Add("@OGRN", FbDbType.VarChar).Value = OGRN;
                command.Parameters.Add("@ADDRESS", FbDbType.VarChar).Value = address;
                command.Parameters.Add("@FACT_ADDRESS", FbDbType.VarChar).Value = factAddress;
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (FbException e)
                {
                    MessageBox.Show(e.Message);
                    db.Close();
                    return;
                }
                trn.Commit();
                db.Close();
            }
        }
        public static void ExecuteAutoProcedure(string nameProc, string VIN, string stateNumb, string regCert, string model, string owner)
        {
            Form1.db.Open();
            using (FbTransaction trn = Form1.db.BeginTransaction())
            {
                FbCommand command = new FbCommand(nameProc, Form1.db, trn);
                command.CommandType = CommandType.StoredProcedure;
                if (VIN.Length != 0)
                    command.Parameters.Add("@VIN", FbDbType.VarChar).Value = VIN;
                else
                    command.Parameters.Add("@VIN", FbDbType.VarChar).Value = null;
                command.Parameters.Add("@STATE_NUMBER", FbDbType.VarChar).Value = stateNumb;
                command.Parameters.Add("@REG_CERTIFICATE", FbDbType.VarChar).Value = (regCert.Length != 0) ? regCert : null;
                command.Parameters.Add("@CAR_MARK", FbDbType.VarChar).Value =model.Split(' ').ToArray().First();
                command.Parameters.Add("@CAR_MODEL", FbDbType.VarChar).Value = 
                    (model.Split(' ').ToArray()[1].Length > 0) ? model.Split(' ').ToArray().Last() : null;
                command.Parameters.Add("@NAME_ORG", FbDbType.VarChar).Value = owner;
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (FbException e)
                {
                    MessageBox.Show(e.Message);
                    Form1.db.Close();
                    return;
                }
                trn.Commit();
                Form1.db.Close();
            }
        }
    }
    public static class Queries
    {
        //views
        public static string ActiveRepairsView = "select* from active_repairs";
        public static string FinishedRepairsView = "select * from finished_repairs";
        public static string CarView = "select* from cars_view";
        public static string ClientView = "select * from client_view";
        public static string StaffView = "select * from staff_view";
        public static string MalfunctionsView = "select * from type_of_work_view";
        public static string SparesView = "select * from simple_spares_view";
        public static string StockView = "select * from stock_view";
        public static string BankView = "select kor_bill, name_bank from bank";
        public static string CarModelView = "select mark || ' ' || coalesce(model, '') as mark_model from car_model";

        public static string GetRepairById(string id_repair) =>
            $"select * from repair_cars_owner where id_card_of_repair = {id_repair}";
        public static string GetClientByClientName(string ClientName) =>
                        "select inn, name_org, director, bank.name_bank as bank, " +
                        "phone_numb, email, bill, kpp, oktmo, okato, ogrn, address, fact_address " +
                        $"from client " +
                        "left join bank on client.bank_bill = bank.kor_bill" + 
                        $" where name_org = '{ClientName}'";
        public static string GetCarViaNumber(string stateNumber) => $"select* from cars_view where state_number = '{stateNumber}'";
        public static string GetMalfByIdRep(string id_repair) =>
                        $"select tw.description, case when tw.unit = 0 then 'шт'" +
                        $" when tw.unit = 1 then 'нч' end as unit, cr.cost, cr.number, (cr.cost * cr.number) as totalcost" +
                        $" from type_of_work as tw, card_of_rep_and_works as cr" +
                        $" where cr.id_card_of_repair = {id_repair} and tw.id_work = cr.id_work and tw.malf_or_spare = '0'";
        public static string GetSparesByIdRep(string id_repair) =>
                        "select tw.description, case when tw.unit = 0 then 'шт'" +
                        " when tw.unit = 1 then 'нч' end as unit, cr.cost, cr.number, (cr.cost * cr.number) as totalcost" +
                        " from type_of_work as tw, card_of_rep_and_works as cr" +
                        $" where cr.id_card_of_repair = {id_repair} and tw.id_work = cr.id_work and tw.malf_or_spare = '1';";
        public static string GetStaffByIdRepair(string id_repair) =>
                        "select tub_numb, name, address, prof, phone" +
                        " from staff_view as s, cards_and_staff as cs" +
                        $" where cs.cardofrepair_id_card = {0} and s.tub_numb = cs.staff_tub_numb";
        public static string SearchMalf(string content) =>
                        Queries.MalfunctionsView + $" where upper(description) LIKE '%{content}%'";
        public static string SearchSpares(string content) =>
                        Queries.SparesView + $" where upper(description) LIKE '%{content}%'";
        public static string SearchInActiveRepairs(string content) =>
                        ActiveRepairsView + $" where client like '%{content}%' or car like '%{content}%'";
        public static string SearchInFinishedRepairs(string content) =>
                        FinishedRepairsView + $" where client like '%{content}%' or car like '%{content}%'";
        public static string SearchInAuto(string content) =>
                        CarView + $" where state_number like '%{content}%' or org like '%{content}%'";
        public static string SearchInClient(string content) => ClientView +
                        $" where name like '%{content}%'";

    }
    public static class DataSets
    {
        public static FbConnection db = Form1.db;
        public static void CreateDSForDataGrid(WindowsStruct windowIndex, string[] columnNames, DataGridView dg, string content = "")
        {
            dg.Columns.Clear();
            string query = "";
            switch (windowIndex)
            {
                case WindowsStruct.ActOfEndsRepairs:
                    query = (content.Length == 0) ? Queries.FinishedRepairsView : Queries.SearchInFinishedRepairs(content);
                    break;
                case WindowsStruct.Repairs:
                    query = (content.Length == 0) ? Queries.ActiveRepairsView : Queries.SearchInActiveRepairs(content);
                    break;
                case WindowsStruct.Auto:
                    query = Queries.CarView;
                    break;
                case WindowsStruct.AddClientInAuto:
                    query = content.Length == 0 ? Queries.ClientView : Queries.SearchInClient(content);
                    break;
                case WindowsStruct.ViewAutoInRep:
                    query = content.Length == 0 ? Queries.CarView : Queries.SearchInAuto(content);
                    break;
                case WindowsStruct.Client:
                    query = content.Length == 0 ? Queries.ClientView : Queries.SearchInClient(content);
                    break;
                case WindowsStruct.Worker:
                    query = Queries.StaffView;
                    break;
                case WindowsStruct.Price:
                    query = Queries.MalfunctionsView;
                    break;
                case WindowsStruct.Stock:
                    query = Queries.StockView;
                    break;
                case WindowsStruct.WayBill:
                    query = Queries.SparesView;
                    break;
                case (WindowsStruct.MalfAdd):
                    query = content.Length == 0 ? Queries.MalfunctionsView : Queries.SearchMalf(content);
                    break;
                case WindowsStruct.MalfView:
                    query = Queries.GetMalfByIdRep(content);
                    break;
                case WindowsStruct.SpareAdd:
                    query = content.Length == 0 ? Queries.SparesView : Queries.SearchSpares(content);
                    break;
                case WindowsStruct.SpareView:
                    query = Queries.GetSparesByIdRep(content);
                    break;
                case WindowsStruct.WorkerAdd:
                    query = Queries.StaffView;
                    break;
                case WindowsStruct.WorkerView:
                    query = Queries.GetStaffByIdRepair(content);
                    break;

            }
            using (FbCommand command = new FbCommand(query, db))
            {
                FbDataAdapter dataAdapter = new FbDataAdapter(command);
                DataSet ds = new DataSet();
                if (db.State != ConnectionState.Open)
                    db.Open();
                dataAdapter.Fill(ds);
                dg.DataSource = ds.Tables[0];
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    ds.Tables[0].Columns[i].ColumnName = columnNames[i];
                }
                db.Close();
            }
            dg.ClearSelection();
        }
        public static void CreateDsForComboBox(ComboBox cb, string query, string displayMember, 
            string valueMember = "", AddEditOrDelete? addOrEdit = null)
        {
            using (FbCommand command = new FbCommand(query, db))
            {
                FbDataAdapter dataAdapter = new FbDataAdapter(command);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                dt.Rows.Add(DBNull.Value);
                if (db.State != ConnectionState.Open)
                    db.Open();
                cb.DataSource = dt;
                cb.DisplayMember = displayMember;
                cb.ValueMember = (valueMember.Length != 0) ? valueMember : displayMember;
                if (addOrEdit == null || addOrEdit == AddEditOrDelete.Add)
                    cb.SelectedIndex = -1;
                db.Close();
            }
        }
    }
}
