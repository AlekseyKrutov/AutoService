using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using AutoServiceLibrary;
using FirebirdSql.Data.FirebirdClient;

namespace DataMapper
{
    public class ClientMapper : IConnection, IDbMapperCommand<Client>
    {
        public FbConnection db { get; } = new FbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        public void Delete(Client owner)
        {
            throw new NotImplementedException();
        }

        public Client Get(string id)
        {
            Client client = new Client();
            if (id == "")
                return null;
            using (FbCommand command = new FbCommand(DbProxy.Queries.GetClientById(id), db))
            {
                FbDataReader dr;
                if (db.State != ConnectionState.Open)
                    db.Open();
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    client.IdCompany = int.Parse(id);
                    client.INN = dr.GetString(dr.GetOrdinal("INN"));
                    client.Name = dr.GetString(dr.GetOrdinal("NAME"));
                    client.Director = dr.GetString(dr.GetOrdinal("DIRECTOR"));
                    //client.IdContract = dr.GetInt32(dr.GetOrdinal("CONTRACT"));
                    client.PhoneNumber = dr.GetString(dr.GetOrdinal("PHONE_NUMB"));
                    client.Email = dr.GetString(dr.GetOrdinal("EMAIL"));
                    client.Bill= dr.GetString(dr.GetOrdinal("BILL"));
                    client.KPP = dr.GetString(dr.GetOrdinal("KPP"));
                    client.OKTMO = dr.GetString(dr.GetOrdinal("OKTMO"));
                    client.OKATO = dr.GetString(dr.GetOrdinal("OKATO"));
                    client.OGRN = dr.GetString(dr.GetOrdinal("OGRN"));
                    client.Address = dr.GetString(dr.GetOrdinal("ADDRESS"));
                    client.FactAddress = dr.GetString(dr.GetOrdinal("FACT_ADDRESS"));
                    client.Discount = dr.GetDouble(dr.GetOrdinal("DISCOUNT"));
                    client.Bank = new BankMapper().Get(dr.GetString(dr.GetOrdinal("BANK_BILL")));
                }
                db.Close();
            }
            return client;
        }

        public Client Insert(Client owner)
        {
            string id = "";
            if (db.State != ConnectionState.Open)
                db.Open();
            using (FbTransaction trn = db.BeginTransaction())
            {
                FbCommand command = new FbCommand("NEW_CLIENT_PROCEDURE", db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@INN", FbDbType.VarChar).Value = owner.INN;
                command.Parameters.Add("@NAME_ORG", FbDbType.VarChar).Value = owner.Name;
                command.Parameters.Add("@DIRECTOR", FbDbType.VarChar).Value = owner.Director;
                command.Parameters.Add("@BANK_BILL", FbDbType.VarChar).Value = owner.Bank.KorBill;
                if (owner.PhoneNumber == " (   )   -")
                    command.Parameters.Add("@PHONE_NUMB", FbDbType.VarChar).Value = "";
                else
                    command.Parameters.Add("@PHONE_NUMB", FbDbType.VarChar).Value = owner.PhoneNumber;
                command.Parameters.Add("@EMAIL", FbDbType.VarChar).Value = owner.Email;
                command.Parameters.Add("@BILL", FbDbType.VarChar).Value = owner.Bill;
                command.Parameters.Add("@KPP", FbDbType.VarChar).Value = owner.KPP;
                command.Parameters.Add("@OKTMO", FbDbType.VarChar).Value = owner.OKTMO;
                command.Parameters.Add("@OKATO", FbDbType.VarChar).Value = owner.OKATO;
                command.Parameters.Add("@OGRN", FbDbType.VarChar).Value = owner.OGRN;
                command.Parameters.Add("@ADDRESS", FbDbType.VarChar).Value = owner.Address;
                command.Parameters.Add("@FACT_ADDRESS", FbDbType.VarChar).Value = owner.FactAddress;
                try
                {
                    FbDataReader dr;
                    dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        id = dr.GetString(dr.GetOrdinal("ID_CLIENT"));
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
            owner = Get(id);
            return owner;
        }

        public void Update(Client owner)
        {
            if (db.State != ConnectionState.Open)
                db.Open();
            using (FbTransaction trn = db.BeginTransaction())
            {
                FbCommand command = new FbCommand("UPDATE_CLIENT_PROCEDURE", db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_CLIENT", FbDbType.SmallInt).Value = owner.IdCompany;
                command.Parameters.Add("@INN", FbDbType.VarChar).Value = owner.INN;
                command.Parameters.Add("@NAME_ORG", FbDbType.VarChar).Value = owner.Name;
                command.Parameters.Add("@DIRECTOR", FbDbType.VarChar).Value = owner.Director;
                command.Parameters.Add("@BANK_BILL", FbDbType.VarChar).Value = owner.Bank.KorBill;
                if (owner.PhoneNumber == " (   )   -")
                    command.Parameters.Add("@PHONE_NUMB", FbDbType.VarChar).Value = "";
                else
                    command.Parameters.Add("@PHONE_NUMB", FbDbType.VarChar).Value = owner.PhoneNumber;
                command.Parameters.Add("@EMAIL", FbDbType.VarChar).Value = owner.Email;
                command.Parameters.Add("@BILL", FbDbType.VarChar).Value = owner.Bill;
                command.Parameters.Add("@KPP", FbDbType.VarChar).Value = owner.KPP;
                command.Parameters.Add("@OKTMO", FbDbType.VarChar).Value = owner.OKTMO;
                command.Parameters.Add("@OKATO", FbDbType.VarChar).Value = owner.OKATO;
                command.Parameters.Add("@OGRN", FbDbType.VarChar).Value = owner.OGRN;
                command.Parameters.Add("@ADDRESS", FbDbType.VarChar).Value = owner.Address;
                command.Parameters.Add("@FACT_ADDRESS", FbDbType.VarChar).Value = owner.FactAddress;
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
