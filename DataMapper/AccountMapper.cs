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
    public class AccountMapper : IDbMapperCommand<Account>
    {
        public FbConnection db { get; } = new FbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        public void Delete(Account account)
        {
            account.DeleteAccount();
            Update(account);
        }

        public Account Get(string login)
        {
            Account account = new Account();
            using (FbCommand command = new FbCommand(DbProxy.Queries.GetAccountByLogin(login.ToUpper()), db))
            {
                FbDataReader dr;
                if (db.State != ConnectionState.Open)
                    db.Open();
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    account.Employee = new EmployeeMapper().Get(dr.GetString(dr.GetOrdinal("TUB_NUMB")));
                    account.Login = dr.GetString(dr.GetOrdinal("LOGIN"));
                    account.Password = dr.GetString(dr.GetOrdinal("PASS_WORD"));
                    account.AccessRole = (AccessRoles)dr.GetInt32(dr.GetOrdinal("ACCESS_ROLE"));
                    account.ActiveFlag = dr.GetBoolean(dr.GetOrdinal("ACTIVE_FLAG"));
                }
                db.Close();
            }
            return account;
        }
        public List<Account> GetAccounts()
        {
            List<Account> accounts = new List<Account>();
            using (FbCommand command = new FbCommand(DbProxy.Queries.AccountReadView, db))
            {
                FbDataReader dr;
                if (db.State != ConnectionState.Open)
                    db.Open();
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    Account account = new Account();
                    account.Employee = new EmployeeMapper().Get(dr.GetString(dr.GetOrdinal("TUB_NUMB")));
                    account.Login = dr.GetString(dr.GetOrdinal("LOGIN"));
                    account.Password = dr.GetString(dr.GetOrdinal("PASS_WORD"));
                    account.AccessRole = (AccessRoles)dr.GetInt32(dr.GetOrdinal("ACCESS_ROLE"));
                    account.ActiveFlag = dr.GetBoolean(dr.GetOrdinal("ACTIVE_FLAG"));
                    accounts.Add(account);
                }
                db.Close();
            }
            return accounts;
        }
        public Account Insert(Account account)
        {
            if (db.State != ConnectionState.Open)
                db.Open();
            using (FbTransaction trn = db.BeginTransaction())
            {
                FbCommand command = new FbCommand("INSERT_ACCOUNT", db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@TUB_NUMB", FbDbType.SmallInt).Value = account.Employee.TubNumb;
                command.Parameters.Add("@LOGIN", FbDbType.VarChar).Value = account.Login;
                command.Parameters.Add("@PASSWORD", FbDbType.VarChar).Value = account.Password;
                command.Parameters.Add("@ACCESS_ROLE", FbDbType.SmallInt).Value = account.AccessRole;
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
            return account;
        }

        public void Update(Account account)
        {
            if (db.State != ConnectionState.Open)
                db.Open();
            using (FbTransaction trn = db.BeginTransaction())
            {
                FbCommand command = new FbCommand("UPDATE_ACCOUNT", db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@TUB_NUMB", FbDbType.SmallInt).Value = account.Employee.TubNumb;
                command.Parameters.Add("@LOGIN", FbDbType.VarChar).Value = account.Login;
                command.Parameters.Add("@PASSWORD", FbDbType.VarChar).Value = account.Password;
                command.Parameters.Add("@ACCESS_ROLE", FbDbType.SmallInt).Value = account.AccessRole;
                command.Parameters.Add("@ACTIVE_FLAG", FbDbType.SmallInt).Value = account.ActiveFlag;
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
