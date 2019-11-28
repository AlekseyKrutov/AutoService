﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using System.Configuration;
using AutoService;
using System.Windows.Forms;

namespace DbProxy
{
    
    public static class InvokeProcedure
    {
        public static FbConnection db = Form1.db;

        //наименования процедур
        public static string AddClient = "NEW_CLIENT_PROCEDURE";
        public static string UpdateClient = "UPDATE_CLIENT_PROCEDURE";
        public static void FinishRepair(int id_card)
        {
            if (db.State != ConnectionState.Open)
                Form1.db.Open();
            using (FbTransaction trn = Form1.db.BeginTransaction())
            {
                FbCommand command = new FbCommand("FINISH_REPAIR", db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_CARD", FbDbType.SmallInt).Value = id_card;
                command.Parameters.Add("@FINISH_DATE", FbDbType.TimeStamp).Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
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
                Form1.db.Open();
            using (FbTransaction trn = Form1.db.BeginTransaction())
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
        public static void ExecuteClientProcedure(string nameProc, string INN, string nameCl, string oldNameOrg,
           string director,  string bankBill, string phoneNumb, string bill, string KPP, string OKTMO,
           string OKATO, string email, string OGRN, string address, string factAddress)
        {
            if (db.State != ConnectionState.Open)
                Form1.db.Open();
            using (FbTransaction trn = Form1.db.BeginTransaction())
            {
                FbCommand command = new FbCommand(nameProc, Form1.db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@INN", FbDbType.VarChar).Value = INN;
                command.Parameters.Add("@NAME_ORG", FbDbType.VarChar).Value = nameCl;
                if (Form1.AddOrEdit == Form1.AddEditOrDelete.Edit)
                    command.Parameters.Add("@OLD_NAME_ORG", FbDbType.VarChar).Value = oldNameOrg;
                command.Parameters.Add("@DIRECTOR", FbDbType.VarChar).Value = director;
                if (bankBill.Length == 0)
                    command.Parameters.Add("@BANK_BILL", FbDbType.VarChar).Value = null;
                else
                    command.Parameters.Add("@BANK_BILL", FbDbType.VarChar).Value = bankBill;
                if (phoneNumb == " (   )   -")
                    command.Parameters.Add("@PHONE_NUMB", FbDbType.VarChar).Value = "";
                else
                    command.Parameters.Add("@PHONE_NUMB", FbDbType.VarChar).Value = phoneNumb;
                command.Parameters.Add("@BILL", FbDbType.VarChar).Value = bill;
                command.Parameters.Add("@KPP", FbDbType.VarChar).Value = KPP;
                command.Parameters.Add("@OKTMO", FbDbType.VarChar).Value = OKTMO;
                command.Parameters.Add("@OKATO", FbDbType.VarChar).Value = OKATO;
                command.Parameters.Add("@EMAIL", FbDbType.VarChar).Value = email;
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

        public static string GetClientByClientName(string ClientName) =>
            "select inn, name_org, director, bank.name_bank as bank, " +
                    "phone_numb, email, bill, kpp, oktmo, okato, ogrn, address, fact_address " +
                    $"from client " +
                    "left join bank on client.bank_bill = bank.kor_bill" + 
                    $" where name_org = '{ClientName}'";
        public static string GetCarViaNumber(string stateNumber) => $"select* from cars_view where state_number like '{stateNumber}'";
        public static string GetMalfByIdRep(string id_repair) =>
                        $"select tw.description, case when tw.unit = 0 then 'шт'" +
                        $" when tw.unit = 1 then 'нч' end as unit, tw.cost, cr.number" +
                        $" from type_of_work as tw, card_of_rep_and_works as cr" +
                        $" where cr.id_card_of_repair = {id_repair} and tw.id_work = cr.id_work and tw.malf_or_spare = '0'";
        public static string GetSparesByIdRep(string id_repair) =>
                        "select tw.description, case when tw.unit = 0 then 'шт'" +
                        " when tw.unit = 1 then 'нч' end as unit, tw.cost, cr.number" +
                        " from type_of_work as tw, card_of_rep_and_works as cr" +
                        $" where cr.id_card_of_repair = {id_repair} and tw.id_work = cr.id_work and tw.malf_or_spare = '1';";
        public static string GetStaffByIdRepair(string id_repair) =>
                        "select tub_numb, name, address, prof, phone" +
                        " from staff_view as s, cards_and_staff as cs" +
                        $" where cs.cardofrepair_id_card = {0} and s.tub_numb = cs.staff_tub_numb";
        public static string SearchMalf(string content) =>
                        "select tub_numb, name, address, prof, phone" +
                        " from staff_view as s, cards_and_staff as cs" +
                        $" where cs.cardofrepair_id_card = {0} and s.tub_numb = cs.staff_tub_numb";
        public static string SearchSpares(string content) =>
                        "select * from simple_spares_view where upper(description) LIKE '%{0}%'";
        public static string SearchInActiveRepairs(string content) =>
                        ActiveRepairsView + $" where client like '%{content}%' or car like '%{content}%'";
        public static string SearchInFinishedRepairs(string content) =>
                        FinishedRepairsView + $" where client like '%{content}%' or car like '%{content}%'";
        public static string SerachInAuto(string content) =>
                        CarView + $" where org like '%{content}%' or state_number like '%{content}%'";
    }
    public static class DataSets
    {
        public static FbConnection db = Form1.db;
        public static void CreateDSForDataGrid(string[] columnNames, DataGridView dg, string content = "")
        {
            string query = "";
            switch (Form1.WindowIndex)
            {
                case Form1.WindowsStruct.ActOfEndsRepairs:
                    query = (content.Length == 0) ? Queries.FinishedRepairsView : Queries.SearchInFinishedRepairs(content);
                    break;
                case Form1.WindowsStruct.Repairs:
                    query = (content.Length == 0) ? Queries.ActiveRepairsView : Queries.SearchInActiveRepairs(content);
                    break;
                case Form1.WindowsStruct.Auto:
                    query = Queries.CarView;
                    break;
                case Form1.WindowsStruct.AddAutoInRep:
                    query = Queries.ClientView;
                    break;
                case Form1.WindowsStruct.ViewAutoInRep:
                    query = content.Length == 0 ? Queries.CarView : Queries.SerachInAuto(content);
                    break;
                case Form1.WindowsStruct.Client:
                    query = Queries.ClientView;
                    break;
                case Form1.WindowsStruct.Worker:
                    query = Queries.StaffView;
                    break;
                case Form1.WindowsStruct.Price:
                    query = Queries.MalfunctionsView;
                    break;
                case Form1.WindowsStruct.Stock:
                    query = Queries.StockView;
                    break;
                case Form1.WindowsStruct.WayBill:
                    query = Queries.SparesView;
                    break;
                case (Form1.WindowsStruct.MalfAdd):
                    query = Queries.MalfunctionsView;
                    break;
                case Form1.WindowsStruct.MalfView:
                    query = Queries.GetMalfByIdRep(content);
                    break;
                case Form1.WindowsStruct.SpareAdd:
                    query = Queries.SparesView;
                    break;
                case Form1.WindowsStruct.SpareView:
                    query = Queries.GetSparesByIdRep(content);
                    break;
                case Form1.WindowsStruct.WorkerAdd:
                    query = Queries.StaffView;
                    break;
                case Form1.WindowsStruct.WorkerView:
                    query = Queries.GetStaffByIdRepair(content);
                    break;

            }
            using (FbCommand command = new FbCommand(query, Form1.db))
            {
                FbDataAdapter dataAdapter = new FbDataAdapter(command);
                DataSet ds = new DataSet();
                if (db.State != ConnectionState.Open)
                    Form1.db.Open();
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
        public static void CreateDsForComboBox(ComboBox cb, string query, string displayMember, string valueMember)
        {
            using (FbCommand command = new FbCommand(query, Form1.db))
            {
                FbDataAdapter dataAdapter = new FbDataAdapter(command);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                
                if (db.State != ConnectionState.Open)
                    Form1.db.Open();
                dt.Rows.Add(DBNull.Value);
                cb.DataSource = dt;
                cb.DisplayMember = displayMember;
                cb.ValueMember = valueMember;
                Form1.db.Close();
            }
        }
    }
}