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
    public class BankMapper : IDbMapperCommand<Bank>
    {
        public FbConnection db { get; } = new FbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        public void Delete(Bank id)
        {
            throw new NotImplementedException();
        }

        public Bank Get(string id)
        {
            Bank bank = new Bank();
            using (FbCommand command = new FbCommand(DbProxy.Queries.GetBankViaBill(id), db))
            {
                FbDataReader dr;
                if (db.State != ConnectionState.Open)
                    db.Open();
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    bank.KorBill = dr.GetString(dr.GetOrdinal("KOR_BILL"));
                    bank.Name = dr.GetString(dr.GetOrdinal("NAME_BANK"));
                    bank.BIK = dr.GetString(dr.GetOrdinal("BIK"));
                }
                db.Close();
            }
            return bank;
        }

        public Bank Insert(Bank id)
        {
            return new Bank();
        }

        public void Update(Bank id)
        {
            throw new NotImplementedException();
        }
    }
}
