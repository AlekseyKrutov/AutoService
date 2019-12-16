using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoServiceLibrary;
using System.Configuration;
using FirebirdSql.Data.FirebirdClient;

namespace DataMapper
{
    public class CardOfRepairMapper : IDbMapperCommand<CardOfRepair>, IConnection
    {
        public FbConnection db { get => new FbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString); }

        public void Insert()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
        public void Delete()
        {
            throw new NotImplementedException();
        }
        public CardOfRepair Get(string id)
        {
            CardOfRepair card = new CardOfRepair(); 
            using (FbCommand command = new FbCommand(DbProxy.Queries.GetRepairById(id), db))
            {
                FbDataReader dr;
                db.Open();
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    card.id_repair = int.Parse(id);
                    card.TimeOfStart = dr.GetDateTime(dr.GetOrdinal("START_DATE"));
                    card.TimeOfFinish = dr.GetDateTime(dr.GetOrdinal("FINISH_DATE"));
                    card.TotalPrice = dr.GetDouble(dr.GetOrdinal("TOTAL_COST"));
                    card.RepairIsCurrent = dr.GetBoolean(dr.GetOrdinal("CURRENT_OR_NOT"));
                    card.Notes = @dr.GetString(dr.GetOrdinal("NOTES"));
                    card.CarVIN = dr.GetString(dr.GetOrdinal("VIN"));
                    card.NumberOfCar = dr.GetString(dr.GetOrdinal("STATE_NUMBER"));
                    card.CarMark = dr.GetString(dr.GetOrdinal("CAR_MODEL"));
                    card.RegCertific = dr.GetString(dr.GetOrdinal("REG_CERT"));
                    card.Owner = new Client
                    {
                        @Address = dr.GetString(dr.GetOrdinal("ADDRESS")),
                        INN = dr.GetString(dr.GetOrdinal("INN")),
                        @Name = dr.GetString(dr.GetOrdinal("NAME_ORG"))
                    };
                }
                db.Close();
            }
            return card;
        }
    }
}
