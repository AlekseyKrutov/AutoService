using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using AutoServiceLibrary;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;

namespace DataMapper
{
    public class EmployeeMapper : IDbMapperCommand<Employee>
    {
        public FbConnection db { get; } = new FbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        public void Delete(Employee employee)
        {
        }

        public Employee Get(string id)
        {
            Employee employee = new Employee();
            using (FbCommand command = new FbCommand(DbProxy.Queries.GetStaffById(id), db))
            {
                FbDataReader dr;
                if (db.State == ConnectionState.Closed)
                    db.Open();
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    employee.TubNumb = dr.GetInt32(dr.GetOrdinal("TUB_NUMB"));
                    employee.INN = dr.GetString(dr.GetOrdinal("INN"));
                    employee.FirstName = dr.GetString(dr.GetOrdinal("FIRST_NAME"));
                    employee.SecondName = dr.GetString(dr.GetOrdinal("SECOND_NAME"));
                    employee.LastName = dr.GetString(dr.GetOrdinal("LAST_NAME"));
                    employee.Passport = dr.GetString(dr.GetOrdinal("PASSPORT"));
                    employee.Address = dr.GetString(dr.GetOrdinal("ADDRESS"));
                    employee.BornDate = dr.GetDateTime(dr.GetOrdinal("DATE_BORN"));
                    employee.Gender = (Sex)dr.GetInt32(dr.GetOrdinal("GENDER"));
                    employee.Function = (Functions) dr.GetInt32(dr.GetOrdinal("PROFESSION"));
                    employee.PhoneNumb = dr.GetString(dr.GetOrdinal("PHONE_NUMB"));
                }
                db.Close();
            }
            return employee;
        }

        public Employee Insert(Employee employee)
        {
            string id = "";
            db.Open();
            using (FbTransaction trn = db.BeginTransaction())
            {
                FbCommand command = new FbCommand("NEW_STAFF_PROCEDURE", db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@INN", FbDbType.VarChar).Value = employee.INN;
                command.Parameters.Add("@FIRST_NAME", FbDbType.VarChar).Value = employee.FirstName;
                command.Parameters.Add("@SECOND_NAME", FbDbType.VarChar).Value = employee.SecondName;
                command.Parameters.Add("@LAST_NAME", FbDbType.VarChar).Value = employee.LastName;
                command.Parameters.Add("@PASSPORT", FbDbType.VarChar).Value = employee.Passport;
                command.Parameters.Add("@ADDRESS", FbDbType.VarChar).Value = employee.Address;
                command.Parameters.Add("@DATE_BORN", FbDbType.Date).Value = employee.BornDate;
                command.Parameters.Add("@GENDER", FbDbType.SmallInt).Value = employee.Gender;
                command.Parameters.Add("@PROFESSION", FbDbType.SmallInt).Value = employee.Function;
                if (employee.PhoneNumb == " (   )   -")
                    command.Parameters.Add("@PHONE_NUMB", FbDbType.VarChar).Value = "";
                else
                    command.Parameters.Add("@PHONE_NUMB", FbDbType.VarChar).Value = employee.PhoneNumb;
                try
                {
                    FbDataReader dr;
                    dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        id = dr.GetString(dr.GetOrdinal("NEW_TUB_NUMB"));
                    }
                }
                catch (FbException ex)
                {
                    MessageBox.Show(ex.Message);
                    db.Close();
                    return null;
                }
                trn.Commit();
            }
            db.Close();
            employee = Get(id);
            return employee;
        }

        public void Update(Employee employee)
        {
            db.Open();
            using (FbTransaction trn = db.BeginTransaction())
            {
                FbCommand command = new FbCommand("UPDATE_STAFF_PROCEDURE", db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@TUB_NUMB", FbDbType.SmallInt).Value = employee.TubNumb;
                command.Parameters.Add("@INN", FbDbType.VarChar).Value = employee.INN;
                command.Parameters.Add("@FIRST_NAME", FbDbType.VarChar).Value = employee.FirstName;
                command.Parameters.Add("@SECOND_NAME", FbDbType.VarChar).Value = employee.SecondName;
                command.Parameters.Add("@LAST_NAME", FbDbType.VarChar).Value = employee.LastName;
                command.Parameters.Add("@PASSPORT", FbDbType.VarChar).Value = employee.Passport;
                command.Parameters.Add("@ADDRESS", FbDbType.VarChar).Value = employee.Address;
                command.Parameters.Add("@DATE_BORN", FbDbType.Date).Value = employee.BornDate;
                command.Parameters.Add("@GENDER", FbDbType.SmallInt).Value = employee.Gender;
                command.Parameters.Add("@PROFESSION", FbDbType.SmallInt).Value = employee.Function;
                if (employee.PhoneNumb == " (   )   -")
                    command.Parameters.Add("@PHONE_NUMB", FbDbType.VarChar).Value = "";
                else
                    command.Parameters.Add("@PHONE_NUMB", FbDbType.VarChar).Value = employee.PhoneNumb;
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    db.Close();
                    return;
                }
                trn.Commit();
            }
            db.Close();
        }
    }
}
