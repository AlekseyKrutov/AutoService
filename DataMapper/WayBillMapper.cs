using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using AutoServiceLibrary;
using FirebirdSql.Data.FirebirdClient;

namespace DataMapper
{
    public class WayBillMapper : IDbMapperCommand<WayBill>, IConnection
    {
        public FbConnection db { get; } = new FbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        public void Delete(WayBill wayBill)
        {
            wayBill.Delete();
            Update(wayBill);
        }

        public WayBill Get(string id)
        {
            WayBill wayBill = new WayBill();
            using (FbCommand command = new FbCommand(DbProxy.Queries.GetWayBillById(id), db))
            {
                FbDataReader dr;
                if (db.State != ConnectionState.Open)
                    db.Open();
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    wayBill.IdWayBill = int.Parse(id);
                    wayBill.LoadDate = dr.GetDateTime(dr.GetOrdinal("LOAD_DATE"));
                    try
                    {
                        wayBill.UnloadDate = dr.GetDateTime(dr.GetOrdinal("UNLOAD_DATE"));
                    }
                    catch (InvalidCastException)
                    {
                        wayBill.UnloadDate = null;
                    }
                    wayBill.Cost = dr.GetFloat(dr.GetOrdinal("COST"));
                    wayBill.Kilometers = dr.GetInt32(dr.GetOrdinal("KILOMETERS"));
                    wayBill.Fuel = dr.GetFloat(dr.GetOrdinal("FUEL"));
                    wayBill.PaidMoney = dr.GetFloat(dr.GetOrdinal("PAID_MONEY"));
                    wayBill.Notes = dr.GetString(dr.GetOrdinal("NOTES"));
                    wayBill.ActiveFlag = dr.GetBoolean(dr.GetOrdinal("ACTIVE_FLAG"));
                    wayBill.IsCurrent = dr.GetBoolean(dr.GetOrdinal("CURRENT_OR_NOT"));
                    wayBill.BaseDocument = dr.GetString(dr.GetOrdinal("BASE_DOCUMENT"));
                    wayBill.Trip = new TripMapper().Get(dr.GetString(dr.GetOrdinal("ID_TRIP")));
                    wayBill.Client = new ClientMapper().Get(dr.GetString(dr.GetOrdinal("CLIENT_ID")));
                    wayBill.Car = new CarMapper().Get(dr.GetString(dr.GetOrdinal("STATE_NUMBER")));
                    wayBill.Driver = new EmployeeMapper().Get(dr.GetString(dr.GetOrdinal("TUB_NUMBER")));
                }
                db.Close();
            }
            return wayBill;
        }

        public WayBill Insert(WayBill wayBill)
        {
            string id = "";
            if (db.State != ConnectionState.Open)
                db.Open();
            using (FbTransaction trn = db.BeginTransaction())
            {
                FbCommand command = new FbCommand("INSERT_WAYBILL", db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@LOAD_DATE", FbDbType.TimeStamp).Value = wayBill.LoadDate;
                command.Parameters.Add("@UNLOAD_DATE", FbDbType.TimeStamp).Value = wayBill.UnloadDate;
                command.Parameters.Add("@COST", FbDbType.Float).Value = wayBill.Cost;
                command.Parameters.Add("@KILOMETERS", FbDbType.SmallInt).Value = wayBill.Kilometers;
                command.Parameters.Add("@FUEL", FbDbType.Float).Value = wayBill.Fuel;
                command.Parameters.Add("@NOTES", FbDbType.VarChar).Value = wayBill.Notes;
                command.Parameters.Add("@BASE_DOCUMENT", FbDbType.VarChar).Value = wayBill.BaseDocument;
                command.Parameters.Add("@ID_TRIP", FbDbType.SmallInt).Value = wayBill.Trip.IdTrip;
                command.Parameters.Add("@CLIENT_ID", FbDbType.SmallInt).Value = wayBill.Client.IdCompany;
                command.Parameters.Add("@STATE_NUMBER", FbDbType.VarChar).Value = wayBill.Car.Number;
                command.Parameters.Add("@TUB_NUMBER", FbDbType.SmallInt).Value = wayBill.Driver.TubNumb;
                try
                {
                    FbDataReader dr;
                    dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        id = dr.GetString(dr.GetOrdinal("ID_WAYBILL"));
                    }
                    trn.Commit();
                }
                catch (Exception ex)
                {
                    db.Close();
                    throw ex;
                }
            }
            db.Close();
            wayBill.IdWayBill = Get(id).IdWayBill;
            return wayBill;
        }

        public void Update(WayBill wayBill)
        {
            if (db.State != ConnectionState.Open)
                db.Open();
            using (FbTransaction trn = db.BeginTransaction())
            {
                FbCommand command = new FbCommand("UPDATE_WAYBILL", db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_WAYBILL", FbDbType.TimeStamp).Value = wayBill.IdWayBill;
                command.Parameters.Add("@LOAD_DATE", FbDbType.TimeStamp).Value = wayBill.LoadDate;
                command.Parameters.Add("@UNLOAD_DATE", FbDbType.TimeStamp).Value = wayBill.UnloadDate;
                command.Parameters.Add("@COST", FbDbType.Float).Value = wayBill.Cost;
                command.Parameters.Add("@KILOMETERS", FbDbType.SmallInt).Value = wayBill.Kilometers;
                command.Parameters.Add("@FUEL", FbDbType.Float).Value = wayBill.Fuel;
                command.Parameters.Add("@NOTES", FbDbType.VarChar).Value = wayBill.Notes;
                command.Parameters.Add("@BASE_DOCUMENT", FbDbType.VarChar).Value = wayBill.BaseDocument;
                command.Parameters.Add("@CURRENT_OR_NOT", FbDbType.SmallInt).Value = wayBill.IsCurrent ? 1 : 0;
                command.Parameters.Add("@ACTIVE_FLAG", FbDbType.SmallInt).Value = wayBill.ActiveFlag ? 1 : 0;
                command.Parameters.Add("@ID_TRIP", FbDbType.SmallInt).Value = wayBill.Trip.IdTrip;
                command.Parameters.Add("@CLIENT_ID", FbDbType.SmallInt).Value = wayBill.Client.IdCompany;
                command.Parameters.Add("@STATE_NUMBER", FbDbType.VarChar).Value = wayBill.Car.Number;
                command.Parameters.Add("@TUB_NUMBER", FbDbType.SmallInt).Value = wayBill.Driver.TubNumb;
                command.Parameters.Add("@PAID_MONEY", FbDbType.Float).Value = wayBill.PaidMoney;
                try
                {
                    command.ExecuteNonQuery();
                    trn.Commit();
                }
                catch (Exception ex)
                {
                    db.Close();
                    throw ex;
                }
                db.Close();
                return;
            }
        }
    }
}
