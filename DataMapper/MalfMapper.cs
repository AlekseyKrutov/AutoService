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
    public class MalfMapper : IDbMapperCommand<Malfunctions>
    {
        public FbConnection db { get; } = new FbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        public void Delete(Malfunctions id)
        {
            throw new NotImplementedException();
        }

        public Malfunctions Get(string id)
        {
            Malfunctions malf = new Malfunctions();
            using (FbCommand command = new FbCommand(DbProxy.Queries.GetMalfById(id), db))
            {
                FbDataReader dr;
                if (db.State == ConnectionState.Closed)
                    db.Open();
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    malf.IdMalf = dr.GetInt32(dr.GetOrdinal("ID"));
                    malf.Description = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                    malf.Unit = (Units) dr.GetInt32(dr.GetOrdinal("UNIT"));
                    malf.Cost = dr.GetDouble(dr.GetOrdinal("COST"));
                    malf.MalfOrSpare = dr.GetInt32(dr.GetOrdinal("MALF_OR_SPARE"));
                }
                db.Close();
            }
            return malf;
        }

        public Malfunctions Insert(Malfunctions malf)
        {
            string id = "";
            db.Open();
            using (FbTransaction trn = db.BeginTransaction())
            {
                FbCommand command = new FbCommand("NEW_TYPE_OF_W_PROCEDURE", db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@DESCRIPTION", FbDbType.VarChar).Value = malf.Description;
                command.Parameters.Add("@UNIT", FbDbType.SmallInt).Value = malf.Unit;
                command.Parameters.Add("@COST", FbDbType.Float).Value = malf.Cost;
                command.Parameters.Add("@MALF_OR_SPARE", FbDbType.SmallInt).Value = malf.MalfOrSpare;
                try
                {
                    FbDataReader dr;
                    dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        id = dr.GetString(dr.GetOrdinal("MALF_ID"));
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
            malf = Get(id);
            return malf;
        }

        public void Update(Malfunctions malf)
        {
            db.Open();
            using (FbTransaction trn = db.BeginTransaction())
            {
                FbCommand command = new FbCommand("UPDATE_TYPE_WORK", db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_WORK", FbDbType.SmallInt).Value = malf.IdMalf;
                command.Parameters.Add("@DESCRIPTION", FbDbType.VarChar).Value = malf.Description;
                command.Parameters.Add("@UNIT", FbDbType.SmallInt).Value = malf.Unit;
                command.Parameters.Add("@COST", FbDbType.Float).Value = malf.Cost;
                command.Parameters.Add("@MALF_OR_SPARE", FbDbType.SmallInt).Value = malf.MalfOrSpare;
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
