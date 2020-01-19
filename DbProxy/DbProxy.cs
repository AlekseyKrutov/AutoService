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
        public static void DeleteRepair(int id_repair)
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
    }
    public static class Queries
    {
        public static string ActiveRepairsView = "select* from active_repairs";
        public static string FinishedRepairsView = "select * from finished_repairs";
        public static string ActiveWayBills = "select * from active_waybills";
        public static string FinishedWayBills = "select * from finished_waybills";
        public static string CarView = "select* from cars_view";
        public static string ClientView = "select * from client_view";
        public static string StaffView = "select * from staff_view";
        public static string MalfunctionsView = "select * from type_of_work_view";
        public static string SparesView = "select * from simple_spares_view";
        public static string StockView = "select * from stock_view";
        public static string BankView = "select kor_bill, name_bank from bank";
        public static string CarModelView = "select mark || coalesce(' ' || model, '') as mark_model from car_model";
        public static string CompanyView = "select * from company_view";
        public static string WorksReadView = "select * from works_read_view";
        public static string SparesReadView = "select * from stock_read_view";
        public static string StaffReadView = "select * from staff";
        public static string WayBillReadView = "select * from waybill";
        public static string TripReadView = "select * from trip";
        public static string TripView = "select id_trip, trip_name as \"Маршрут\" from trip";
        public static string RepairsReportView = "select * from repairs_report";
        public static string WayBillsReportView = "select * from waybills_report";
        public static string Profession = "select id_prof, profession as prof from profession";
        public static string RepairsForCBox = "select id, id || '  ' || \"Машина\" as info from active_repairs";
        public static string CarForCBox = "select state_number, state_number || ' ' ||cm.mark || ' ' || coalesce(cm.model, '') as car" +
                                          " from cars as c inner join client as cl" +
                                          " on c.owner = cl.id_client and cl.system_owner = 1" +
                                          " left join car_model as cm" +
                                          " on c.car_model_id = cm.car_id";
        public static string DriverForCBox = "select tub_numb, (last_name || ' ' || first_name || coalesce((' ' || second_name), '')) as name" +
                                             " from staff where profession = 7";
        public static string GetRepairById(string id_repair) =>
            $"select * from repair_cars_owner where id_card_of_repair = {id_repair}";
        public static string GetBankViaBill(string bill) =>
            $"select * from bank where kor_bill = '{bill}'";
        public static string GetClientByClientName(string ClientName) =>
                        "select inn, name_org, director, bank.name_bank as bank, " +
                        "phone_numb, email, bill, kpp, oktmo, okato, ogrn, address, fact_address " +
                        $"from client " +
                        "left join bank on client.bank_bill = bank.kor_bill" + 
                        $" where name_org = '{ClientName}'";
        public static string GetCarViaNumber(string stateNumber) => $"select* from cars_view where state_number = '{stateNumber}'";
        public static string GetReadCarViaNumber(string stateNumber) => $"select* from cars_read_view where state_number = '{stateNumber}'";
        public static string GetMalfByIdRep(string id_repair) =>
                        $"select tw.description, case when tw.unit = 0 then 'шт'" +
                        $" when tw.unit = 1 then 'нч' end as unit, cr.cost, cr.number, (cr.cost * cr.number) as totalcost" +
                        $" from type_of_work as tw, card_of_rep_and_works as cr" +
                        $" where cr.id_card_of_repair = {id_repair} and tw.id_work = cr.id_work and tw.malf_or_spare = '0'";
        public static string GetMalfSpares(string id_repair) => 
            $"select * from malf_and_repair where id_repair = {id_repair}";
        public static string GetSparesByIdRep(string id_repair) =>
                        "select tw.description, case when tw.unit = 0 then 'шт'" +
                        " when tw.unit = 1 then 'нч' end as unit, cr.cost, cr.number, (cr.cost * cr.number) as totalcost" +
                        " from type_of_work as tw, card_of_rep_and_works as cr" +
                        $" where cr.id_card_of_repair = {id_repair} and tw.id_work = cr.id_work and tw.malf_or_spare = '1';";
        public static string GetTrueSparesByIdRep(string id_repair) => $"select * from spare_and_repair where id_repair = {id_repair}";
        public static string GetStaffByIdRepair(string id_repair) =>
                        $"select * from workers_and_repair where id_card = {id_repair}";
        public static string GetClientById(string id) => CompanyView + $" where id = {id} and system_owner = 0";
        public static string GetMalfById(string id) => WorksReadView + $" where id = {id}";
        public static string GetSparesById(string id) => SparesReadView + $" where id_spare = '{id}'";
        public static string GetSystemOwner() => CompanyView + $" where system_owner = 1";
        public static string GetStaffById(string tubNumb) => StaffReadView + $" where tub_numb = {tubNumb}";
        public static string GetWayBillById(string id) => WayBillReadView + $" where id_waybill = {id}";
        public static string GetTripById(string id) => TripReadView + $" where id_trip = {id}";
        public static string SearchMalf(string content) =>
                        Queries.MalfunctionsView + $" where upper(\"Наименование\") LIKE '%{content.ToUpper()}%'";
        public static string SearchSpares(string content) =>
                        Queries.SparesView + $" where upper(\"Наименование\") LIKE '%{content.ToUpper()}%'";
        public static string SearchInActiveRepairs(string content) =>
                        ActiveRepairsView + $" where \"Клиент\" like '%{content}%' or \"Машина\" like '%{content}%'";
        public static string SearchInFinishedRepairs(string content) =>
                        FinishedRepairsView + $" where \"Клиент\" like '%{content}%' or \"Машина\" like '%{content}%'";
        public static string SearchInAuto(string content) =>
                        CarView + $" where \"Гос.номер\" like '%{content}%' or \"Владелец\" like '%{content}%'";
        public static string SearchInClient(string content) => ClientView +
                        $" where upper(\"Наименование\") like '%{content.ToUpper()}%'";
        public static string SearchInTrip(string content) => TripView +
                        $" where upper(trip_name) like '%{content.ToUpper()}%'";
    }
    public static class DataSets
    {
        public static FbConnection db = new FbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        public static void CreateDSForDataGrid(WindowsStruct windowIndex, DataGridView dg, string content = "")
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
                case WindowsStruct.ActiveWayBills:
                    query = Queries.ActiveWayBills;
                    break;
                case WindowsStruct.FinishedWayBills:
                    query = Queries.FinishedWayBills;
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
                case WindowsStruct.AddClientInWay:
                    query = content.Length == 0 ? Queries.ClientView : Queries.SearchInClient(content);
                    break;
                case WindowsStruct.AddTripInWay:
                    query = content.Length == 0 ? Queries.TripView : Queries.SearchInTrip(content);
                    break;
                case WindowsStruct.RepairsReport:
                    query = Queries.RepairsReportView;
                    break;
                case WindowsStruct.WayBillsReport:
                    query = Queries.WayBillsReportView;
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
                dg.Columns[0].Visible = false;
                db.Close();
            }
            dg.ClearSelection();
        }
        public static void CreateDsForComboBox(ComboBox cb, string query, string displayMember, 
            string valueMember = "", AddEditOrDelete? addOrEdit = null)
        {
            if (db.State != ConnectionState.Open)
                db.Open();
            using (FbCommand command = new FbCommand(query, db))
            {
                FbDataAdapter dataAdapter = new FbDataAdapter(command);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                dt.Rows.Add(DBNull.Value);
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
