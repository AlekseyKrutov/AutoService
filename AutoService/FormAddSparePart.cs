using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;

namespace AutoService
{
    public partial class FormAddSparePart : Form
    {
        Form1 mainForm;
        public FormAddSparePart()
        {
            InitializeComponent();
        }
        public FormAddSparePart(Form1 mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void FormAddSparePart_Load(object sender, EventArgs e)
        {
            string query = @"select CAR_ID, MARK || ' ' ||MODEL AS MARK_MODEL from CAR_MODEL";
            using (FbCommand command = new FbCommand(query, Form1.db))
            {
                FbDataAdapter dataAdapter = new FbDataAdapter(command);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                Form1.db.Open();
                comboBoxAuto.DataSource = dt;
                comboBoxAuto.DisplayMember = "MARK_MODEL";
                Form1.db.Close();
            }
        }

        private void checkBoxShowAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBoxAuto.Enabled)
            {
                comboBoxAuto.Enabled = false;
            }
            else
                comboBoxAuto.Enabled = true;
        }

        private void textBoxDescr_Enter(object sender, EventArgs e)
        {
            textBoxDescr.Height += 22;
        }

        private void textBoxDescr_Leave(object sender, EventArgs e)
        {
            textBoxDescr.Height -= 22;
        }

        private void buttonAddSparePart_Click(object sender, EventArgs e)
        {
            Form1.db.Open();
            using (FbTransaction trn = Form1.db.BeginTransaction())
            {
                FbCommand command = new FbCommand("NEW_SPAREPART_PROCEDURE", Form1.db, trn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@UNIQ_CODE", FbDbType.Integer).Value = textBoxUniqNumb.Text;
                command.Parameters.Add("@DESCRIPTION", FbDbType.VarChar).Value = textBoxDescr.Text;
                command.Parameters.Add("@COST", FbDbType.Float).Value = textBoxCost.Text;
                command.Parameters.Add("@CAR_ID", FbDbType.SmallInt).Value = "2";
                command.Parameters.Add("@NUMBER", FbDbType.SmallInt).Value = textBoxNumb.Text;
                command.ExecuteNonQuery();
                trn.Commit();
                Form1.db.Close();
            }
            AddSparePartInStock();
            this.Close();
        }
        private void AddSparePartInStock()
        {
            string query = @"select * from stock_view";
            FbCommand command = new FbCommand(query, Form1.db);
            FbDataAdapter dataAdapter = new FbDataAdapter(command);
            DataSet ds = new DataSet();
            Form1.db.Open();
            dataAdapter.Fill(ds);
            mainForm.dataGridView.DataSource = ds.Tables[0];
            ds.Tables[0].Columns[0].ColumnName = "Артикул";
            ds.Tables[0].Columns[1].ColumnName = "Наименование";
            ds.Tables[0].Columns[2].ColumnName = "Количество";
            ds.Tables[0].Columns[3].ColumnName = "Cтоимость(руб.)";
            ds.Tables[0].Columns[4].ColumnName = "Автомобиль";
            Form1.db.Close();
        }
    }
}
