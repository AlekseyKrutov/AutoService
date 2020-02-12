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
    public class SpareMapper : IDbMapperCommand<SparePart>
    {
        public FbConnection db { get; } = new FbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        public void Delete(SparePart id)
        {
            throw new NotImplementedException();
        }

        public SparePart Get(string id)
        {
            SparePart sparePart = new SparePart();
            using (FbCommand command = new FbCommand(DbProxy.Queries.GetSparesById(id.ToString()), db))
            {
                FbDataReader dr;
                db.Open();
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    sparePart = new SparePart(
                        dr.GetInt32(dr.GetOrdinal("ID_SPARE")),
                        dr.GetString(dr.GetOrdinal("VENDOR_NUMBER")),
                        dr.GetFloat(dr.GetOrdinal("NUMBER")),
                        dr.GetDouble(dr.GetOrdinal("COST")),
                        dr.GetString(dr.GetOrdinal("DESCRIPTION")),
                        (Units)dr.GetInt32(dr.GetOrdinal("UNIT")));
                }
                db.Close();
            }
            return sparePart;
        }

        public SparePart Insert(SparePart part)
        {
            string id = "";
            db.Open();
            using (FbTransaction trn = db.BeginTransaction())
            {
                FbCommand command = new FbCommand("NEW_SPAREPART_PROCEDURE", db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@VENDOR_NUMBER", FbDbType.VarChar).Value = part.Articul;
                command.Parameters.Add("@DESCRIPTION", FbDbType.VarChar).Value = part.Description;
                command.Parameters.Add("@COST", FbDbType.Float).Value = part.Cost;
                command.Parameters.Add("@UNIT", FbDbType.SmallInt).Value = part.Unit;
                command.Parameters.Add("@NUMBER", FbDbType.Float).Value = part.Number;

                try
                {
                    FbDataReader dr;
                    dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        id = dr.GetString(dr.GetOrdinal("ID_SPARE"));
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
            part = Get(id);
            return part;
        }

        public void Update(SparePart part)
        {
            db.Open();
            using (FbTransaction trn = db.BeginTransaction())
            {
                FbCommand command = new FbCommand("UPDATE_STOCK_PROCEDURE", db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_SPARE", FbDbType.SmallInt).Value = part.IdSpare;
                command.Parameters.Add("@VENDOR_NUMBER", FbDbType.VarChar).Value = part.Articul;
                command.Parameters.Add("@DESCRIPTION", FbDbType.VarChar).Value = part.Description;
                command.Parameters.Add("@UNIT", FbDbType.SmallInt).Value = part.Unit;
                command.Parameters.Add("@COST", FbDbType.Float).Value = part.Cost;
                command.Parameters.Add("@NUMBER", FbDbType.Float).Value = part.Number;
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
            return;
        }
    }
}
