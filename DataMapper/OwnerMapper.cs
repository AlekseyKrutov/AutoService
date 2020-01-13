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
    public class OwnerMapper : IConnection, IDbMapperCommand<SystemOwner>
    {
        public FbConnection db { get; } = new FbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        public void Delete(SystemOwner owner)
        {
            throw new NotImplementedException();
        }

        public SystemOwner Get(string id = "")
        {
            SystemOwner owner = new SystemOwner();
            using (FbCommand command = new FbCommand(DbProxy.Queries.GetSystemOwner(), db))
            {
                FbDataReader dr;
                db.Open();
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    owner.INN = dr.GetString(dr.GetOrdinal("INN"));
                    owner.Name = dr.GetString(dr.GetOrdinal("NAME"));
                    owner.Director = new Employee
                    {
                        TubNumb = dr.GetInt32(dr.GetOrdinal("DIRECTOR"))
                    };
                    owner.PhoneNumber = dr.GetString(dr.GetOrdinal("PHONE_NUMB"));
                    owner.Email = dr.GetString(dr.GetOrdinal("EMAIL"));
                    owner.Bill = dr.GetString(dr.GetOrdinal("BILL"));
                    owner.KPP = dr.GetString(dr.GetOrdinal("KPP"));
                    owner.OKTMO = dr.GetString(dr.GetOrdinal("OKTMO"));
                    owner.OKATO = dr.GetString(dr.GetOrdinal("OKATO"));
                    owner.OGRN = dr.GetString(dr.GetOrdinal("OGRN"));
                    owner.Address = dr.GetString(dr.GetOrdinal("ADDRESS"));
                    owner.FactAddress = dr.GetString(dr.GetOrdinal("FACT_ADDRESS"));
                    owner.Bank = new BankMapper().Get(dr.GetString(dr.GetOrdinal("BANK_BILL")));
                }
                db.Close();
            }
            EmployeeMapper em = new EmployeeMapper();
            owner.Director = em.Get(owner.Director.TubNumb.ToString());
            return owner;
        }

        public SystemOwner Insert(SystemOwner owner)
        {
            return new SystemOwner();
        }

        public void Update(SystemOwner owner)
        {
            throw new NotImplementedException();
        }
    }
}
