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
    public class CarMapper : IConnection, IDbMapperCommand<Car>
    {
        public FbConnection db { get; } = new FbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        public void Delete(Car id)
        {
            throw new NotImplementedException();
        }

        public Car Get(string id)
        {
            Car car = new Car();
            using (FbCommand command = new FbCommand(DbProxy.Queries.GetReadCarViaNumber(id), db))
            {
                FbDataReader dr;
                if (db.State != ConnectionState.Open)
                    db.Open();
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    string[] model = dr.GetString(dr.GetOrdinal("MODEL")).Split(' ');
                    car.Mark = dr.GetString(dr.GetOrdinal("MODEL")).Split(' ').First();
                    car.Model = (model.Length > 1) ? model.Last() : "";
                    car.Number = dr.GetString(dr.GetOrdinal("STATE_NUMBER"));
                    car.CarVIN = dr.GetString(dr.GetOrdinal("VIN"));
                    car.RegCertific = dr.GetString(dr.GetOrdinal("REG_CERT"));
                    car.Owner = new ClientMapper().Get(dr.GetString(dr.GetOrdinal("ID_CLIENT")));
                }
                db.Close();
            }
            return car;
        }

        public Car Insert(Car car)
        {
            if (db.State != ConnectionState.Open)
                db.Open();
            using (FbTransaction trn = db.BeginTransaction())
            {
                FbCommand command = new FbCommand("NEW_CAR_PROCEDURE", db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@VIN", FbDbType.VarChar).Value = car.CarVIN;
                command.Parameters.Add("@STATE_NUMBER", FbDbType.VarChar).Value = car.Number;
                command.Parameters.Add("@REG_CERTIFICATE", FbDbType.VarChar).Value = car.RegCertific;
                command.Parameters.Add("@CAR_MARK", FbDbType.VarChar).Value = car.Mark;
                command.Parameters.Add("@CAR_MODEL", FbDbType.VarChar).Value = car.Model;
                if (car.Owner != null)
                    command.Parameters.Add("@CLIENT_ID", FbDbType.SmallInt).Value = car.Owner.IdCompany;
                else
                    command.Parameters.Add("@CLIENT_ID", FbDbType.SmallInt).Value = DBNull.Value;
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
            }
            db.Close();
            return car;
        }

        public void Update(Car car)
        {
            if (db.State != ConnectionState.Open)
                db.Open();
            using (FbTransaction trn = db.BeginTransaction())
            {
                FbCommand command = new FbCommand("UPDATE_CAR_PROCEDURE", db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@VIN", FbDbType.VarChar).Value = car.CarVIN;
                command.Parameters.Add("@STATE_NUMBER", FbDbType.VarChar).Value = car.Number;
                command.Parameters.Add("@REG_CERTIFICATE", FbDbType.VarChar).Value = car.RegCertific;
                command.Parameters.Add("@CAR_MARK", FbDbType.VarChar).Value = car.Mark;
                command.Parameters.Add("@CAR_MODEL", FbDbType.VarChar).Value = car.Model;
                if (car.Owner != null)
                    command.Parameters.Add("@CLIENT_ID", FbDbType.SmallInt).Value = car.Owner.IdCompany;
                else
                    command.Parameters.Add("@CLIENT_ID", FbDbType.SmallInt).Value = DBNull.Value;
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
