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
    public class CardMapper : IDbMapperCommand<CardOfRepair>, IConnection
    {
        public FbConnection db { get; } = new FbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        public CardOfRepair Insert(CardOfRepair card)
        {
            string id = "";
            if (db.State != ConnectionState.Open)
                db.Open();
            using (FbTransaction trn = db.BeginTransaction())
            {
                FbCommand command = new FbCommand("INSERT_CARD_OF_REPAIR", db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@STATE_NUMBER", FbDbType.VarChar).Value = card.Car.NumberOfCar;
                command.Parameters.Add("@NOTES", FbDbType.VarChar).Value = card.Notes;
                command.Parameters.Add("@START_DATE", FbDbType.TimeStamp).Value = card.TimeOfStart;
                command.Parameters.Add("@FINISH_DATE", FbDbType.TimeStamp).Value = card.TimeOfFinish;
                command.Parameters.Add("@TOTAL_COST", FbDbType.TimeStamp).Value = card.TotalPrice;
                try
                {
                    FbDataReader dr;
                    dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        id = dr.GetString(dr.GetOrdinal("ID_CARD"));
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
            card.id_repair = Get(id).id_repair;
            InsertWorks(card);
            InsertSpares(card);
            InsertWorkers(card);
            return card;
        }
        public void InsertWorks(CardOfRepair card)
        {
            if (db.State != ConnectionState.Open)
                db.Open();
            using (FbTransaction trn = db.BeginTransaction())
            {
                foreach (Malfunctions malf in card.ListOfMalf)
                {
                    FbCommand command = new FbCommand("INSERT_WORK_REPAIR", db, trn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@ID_CARD", FbDbType.SmallInt).Value = card.id_repair;
                    command.Parameters.Add("@ID_WORK", FbDbType.SmallInt).Value = malf.IdMalf;
                    command.Parameters.Add("@NUMBER", FbDbType.SmallInt).Value = malf.Number;
                    command.Parameters.Add("@COST", FbDbType.Float).Value = malf.Price;
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        db.Close();
                        throw ex;
                    }
                }
                trn.Commit();
            }
            db.Close();
            return;
        }
        public void InsertSpares(CardOfRepair card)
        {
            if (db.State != ConnectionState.Open)
                db.Open();
            using (FbTransaction trn = db.BeginTransaction())
            {
                foreach (SparePart part in card.ListOfSpareParts)
                {
                    FbCommand command = new FbCommand("INSERT_SPARE_REPAIR", db, trn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@ID_CARD", FbDbType.SmallInt).Value = card.id_repair;
                    command.Parameters.Add("@ID_SPARE", FbDbType.SmallInt).Value = part.IdSpare;
                    command.Parameters.Add("@NUMBER", FbDbType.Float).Value = part.Number;
                    command.Parameters.Add("@COST", FbDbType.Float).Value = part.Price;
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        db.Close();
                        throw ex;
                    }
                }
                trn.Commit();
            }
            db.Close();
            return;
        }
        public void InsertWorkers(CardOfRepair card)
        {
            if (db.State != ConnectionState.Open)
                db.Open();
            using (FbTransaction trn = db.BeginTransaction())
            {
                foreach (Employee emp in card.ListOfPersonal)
                {
                    FbCommand command = new FbCommand("INSERT_WORKER_REPAIR", db, trn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@ID_CARD", FbDbType.SmallInt).Value = card.id_repair;
                    command.Parameters.Add("@TUB_NUMB", FbDbType.SmallInt).Value = emp.TubNumb;
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        db.Close();
                        throw ex;
                    }
                }
                trn.Commit();
            }
            db.Close();
            return;
        }
        public void Update(CardOfRepair card)
        {
            if (db.State != ConnectionState.Open)
                db.Open();
            using (FbTransaction trn = db.BeginTransaction())
            {
                FbCommand command = new FbCommand("UPDATE_CARD_OF_REPAIR", db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_CARD", FbDbType.SmallInt).Value = card.id_repair;
                command.Parameters.Add("@STATE_NUMBER", FbDbType.VarChar).Value = card.Car.NumberOfCar;
                command.Parameters.Add("@NOTES", FbDbType.VarChar).Value = card.Notes;
                command.Parameters.Add("@START_DATE", FbDbType.TimeStamp).Value = card.TimeOfStart;
                command.Parameters.Add("@FINISH_DATE", FbDbType.TimeStamp).Value = card.TimeOfFinish;
                command.Parameters.Add("@TOTAL_COST", FbDbType.TimeStamp).Value = card.TotalPrice;
                command.Parameters.Add("@CURRENT_OR_NOT", FbDbType.SmallInt).Value = card.RepairIsCurrent ? 1 : 0;
                try
                {
                    command.ExecuteNonQuery();
                    trn.Commit();
                    UpdateWorks(card);
                    UpdateSpares(card);
                    UpdateWorks(card);
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
        public void UpdateWorks(CardOfRepair card)
        {
            DeleteWorks(card);
            InsertWorks(card);
        }
        public void UpdateSpares(CardOfRepair card)
        {
            DeleteSpares(card);
            InsertSpares(card);
        }
        public void UpdateWorkers(CardOfRepair card)
        {
            DeleteWorkers(card);
            InsertWorkers(card);
        }
        public void Delete(CardOfRepair card)
        {
            DbProxy.InvokeProcedure.DeleteRepair(card.id_repair);
        }
        public void DeleteWorks(CardOfRepair card)
        {
            if (db.State != ConnectionState.Open)
                db.Open();
            using (FbTransaction trn = db.BeginTransaction())
            {
                FbCommand command = new FbCommand("DELETE_WORK_REPAIR", db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_CARD", FbDbType.SmallInt).Value = card.id_repair;
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
        public void DeleteSpares(CardOfRepair card)
        {
            if (db.State != ConnectionState.Open)
                db.Open();
            using (FbTransaction trn = db.BeginTransaction())
            {
                FbCommand command = new FbCommand("DELETE_SPARE_REPAIR", db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_CARD", FbDbType.SmallInt).Value = card.id_repair;
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
        public void DeleteWorkers(CardOfRepair card)
        {
            if (db.State != ConnectionState.Open)
                db.Open();
            using (FbTransaction trn = db.BeginTransaction())
            {
                FbCommand command = new FbCommand("DELETE_WORKER_REPAIR", db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_CARD", FbDbType.SmallInt).Value = card.id_repair;
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
        public CardOfRepair Get(string id)
        {
            CardOfRepair card = new CardOfRepair(); 
            using (FbCommand command = new FbCommand(DbProxy.Queries.GetRepairById(id), db))
            {
                FbDataReader dr;
                if (db.State != ConnectionState.Open)
                    db.Open();
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    card.id_repair = int.Parse(id);
                    card.TimeOfStart = dr.GetDateTime(dr.GetOrdinal("START_DATE"));
                    //card.TimeOfFinish = dr.GetDateTime(dr.GetOrdinal("FINISH_DATE"));
                    card.TotalPrice = dr.GetDouble(dr.GetOrdinal("TOTAL_COST"));
                    card.RepairIsCurrent = dr.GetBoolean(dr.GetOrdinal("CURRENT_OR_NOT"));
                    card.Notes = dr.GetString(dr.GetOrdinal("NOTES"));
                    card.Car = new CarMapper().Get(dr.GetString(dr.GetOrdinal("STATE_NUMBER")));
                }
                db.Close();
            }
            card.ListOfMalf = GetListMalf(card.id_repair);
            card.ListOfSpareParts = GetListSpare(card.id_repair);
            card.ListOfPersonal = GetListPersonal(card.id_repair);
            return card;
        }
        public List<Malfunctions> GetListMalf(int id_repair)
        {
            List<Malfunctions> listMalf = new List<Malfunctions>();
            using (FbCommand command = new FbCommand(DbProxy.Queries.GetMalfSpares(id_repair.ToString()), db))
            {
                FbDataReader dr;
                if (db.State != ConnectionState.Open)
                    db.Open();
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    listMalf.Add(new Malfunctions(
                        dr.GetInt32(dr.GetOrdinal("ID_WORK")),
                        dr.GetDouble(dr.GetOrdinal("COST")),
                        dr.GetString(dr.GetOrdinal("DESCRIPTION")),
                        (Units) dr.GetInt32(dr.GetOrdinal("UNIT")),
                        dr.GetInt32(dr.GetOrdinal("NUMBER")),
                        dr.GetInt32(dr.GetOrdinal("MALF_OR_SPARE"))
                        ));
                }
                db.Close();
            }
            return listMalf;
        }
        public List<SparePart> GetListSpare(int id_repair)
        {
            List<SparePart> listSpare = new List<SparePart>();
            using (FbCommand command = new FbCommand(DbProxy.Queries.GetTrueSparesByIdRep(id_repair.ToString()), db))
            {
                FbDataReader dr;
                if (db.State != ConnectionState.Open)
                    db.Open();
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    listSpare.Add(new SparePart(
                        dr.GetString(dr.GetOrdinal("ARTICUL")),
                        dr.GetInt32(dr.GetOrdinal("NUMBER")),
                        dr.GetDouble(dr.GetOrdinal("COST")),
                        dr.GetString(dr.GetOrdinal("DESCRIPTION")),
                        (Units)dr.GetInt32(dr.GetOrdinal("UNIT"))
                        ));
                }
                db.Close();
            }
            return listSpare;
        }
        public List<Employee> GetListPersonal(int id_repair)
        {
            List<Employee> listPersonal = new List<Employee>();
            using (FbCommand command = new FbCommand(DbProxy.Queries.GetStaffByIdRepair(id_repair.ToString()), db))
            {
                FbDataReader dr;
                if (db.State != ConnectionState.Open)
                    db.Open();
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    listPersonal.Add(new EmployeeMapper().Get(
                        dr.GetString(dr.GetOrdinal("TUB_NUMB"))
                        ));
                }
                db.Close();
            }
            return listPersonal;
        }
    }
}
