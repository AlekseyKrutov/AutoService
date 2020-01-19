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
    public class TripMapper : IDbMapperCommand<Trip>, IConnection
    {
        public FbConnection db { get; } = new FbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        public void Delete(Trip trip)
        {
            trip.Delete();
            Update(trip);
        }

        public Trip Get(string id)
        {
            Trip trip = new Trip();
            using (FbCommand command = new FbCommand(DbProxy.Queries.GetTripById(id), db))
            {
                FbDataReader dr;
                if (db.State != ConnectionState.Open)
                    db.Open();
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    trip.IdTrip = dr.GetInt32(dr.GetOrdinal("ID_TRIP"));
                    trip.Name = dr.GetString(dr.GetOrdinal("TRIP_NAME"));
                    trip.ActiveFlag = dr.GetBoolean(dr.GetOrdinal("ACTIVE_FLAG"));
                }
                db.Close();
            }
            return trip;
        }

        public Trip Insert(Trip trip)
        {
            string id = "";
            if (db.State != ConnectionState.Open)
                db.Open();
            using (FbTransaction trn = db.BeginTransaction())
            {
                FbCommand command = new FbCommand("INSERT_TRIP", db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@TRIP_NAME", FbDbType.SmallInt).Value = trip.Name;
                try
                {
                    FbDataReader dr;
                    dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        id = dr.GetString(dr.GetOrdinal("ID_TRIP"));
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
            trip.IdTrip = Get(id).IdTrip;
            return trip;
        }

        public void Update(Trip trip)
        {
            if (db.State != ConnectionState.Open)
                db.Open();
            using (FbTransaction trn = db.BeginTransaction())
            {
                FbCommand command = new FbCommand("UPDATE_TRIP", db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_TRIP", FbDbType.SmallInt).Value = trip.IdTrip;
                command.Parameters.Add("@TRIP_NAME", FbDbType.VarChar).Value = trip.Name;
                command.Parameters.Add("@ACTIVE_FLAG", FbDbType.SmallInt).Value = trip.ActiveFlag ? 1 : 0;
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
