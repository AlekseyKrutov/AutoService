using AutoService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Xml;
using System.Xml.Linq;
using FirebirdSql.Data.FirebirdClient;
using AutoServiceLibrary;

namespace AutoService
{
    public partial class Form1 : Form
    {
        FbConnectionStringBuilder csb = new FbConnectionStringBuilder();
        static public FbConnection db;
        //формы окон
        FormAddRepair formAddRepair = new FormAddRepair();
        FormAddAuto formAddAuto = new FormAddAuto();
        FormAddClient formAddClient = new FormAddClient();
        FormAddPersonal formAddPersonal = new FormAddPersonal();
        FormAddPrice formAddPrice = new FormAddPrice();
        FormAddSparePart formAddSparePart = new FormAddSparePart();

        //переменная изменения размера
        int resizeCount = 80;
        //переменная для верхнего положения сетки
        int topForGridIdeal = 0;  
        //переменная для верхнего положения заголовка
        int topForLabelOfHead = 0;
        //переменная выбранного индекса в сетке
        static public int SelectIndex;
        //переменная для определения окна с которым мы работаем
        static public int WindowIndex = 0;
        //структура с названиями окон
        public enum WindowsStruct { Repairs = 1, Auto, ActOfEndsRepairs, Worker, MalfAdd, MalfView }
        public enum AddEditOrDelete { Add, Edit, Delete };
        //список добавленных неисправностей
        public static List<Malfunctions> malfListForRepairAll = new List<Malfunctions>();
        public static List<Malfunctions> malfListForRepairAdded = new List<Malfunctions>();

        //конструктор формы
        public Form1()
        {
            InitializeComponent();
            topForGridIdeal = dataGridView.Top;
            topForLabelOfHead = labelHeaderText.Top;
        }
        //события загрузки формы
        private void Form1_Load(object sender, EventArgs e)
        {
            Padding padding = new Padding(0, 8, 0, 8);
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView.RowTemplate.DefaultCellStyle.Padding = padding;
            HideAutoButtons();
            HideClientButtons();
            HidePersonalButtons();
            HidePriceButtons();
            HideStockButtons();
            dataGridView.AllowUserToAddRows = false;
            dataGridView.ClearSelection();
            dataGridView.RowHeadersVisible = false;
            labelHeaderText.Text = "Текущие ремонты";
            DataGridViewForRepairs();
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            //вывод данных в XML
            if (GetXmlInventory() != null)
            {
                XDocument xdoc = GetXmlInventory();
                //добавление ремонтов в список
                var itemsRepair = from xe in xdoc.Element("elements").Element("repairs").Elements("repair")
                                  select new CardOfRepair
                                  {
                                      CarMark = xe.Element("CarMark").Value,
                                      CarModel = xe.Element("CarModel").Value,
                                      CarVIN = xe.Element("VIN").Value,
                                      NumberOfCar = xe.Element("NumberOfCar").Value,
                                      RegCertific = xe.Element("RegCertific").Value,
                                      Owner = new Client
                                      {
                                          Address = xe.Element("Owner").Element("Address").Value,
                                          INN = xe.Element("Owner").Element("INN").Value,
                                          Name = xe.Element("Owner").Element("OwnerName").Value,
                                          NumberOfTel = xe.Element("Owner").Element("NumberOfTel").Value
                                      },
                                      Mechanic = new Personal(xe.Element("Mechanic").Element("Name").Value),
                                      Notes = xe.Element("Notes").Value,
                                      TotalPrice = double.Parse(xe.Element("TotalPrice").Value),
                                      RepairIsCurrent = bool.Parse(xe.Element("RepairIsCurrent").Value),
                                      TimeOfStart = xe.Element("TimeOfStart").Value,
                                      TimeOfEnd = xe.Element("TimeOfEnd").Value,
                                      NumberOfAct = int.Parse(xe.Element("NumberOfAct").Value),
                                      ListOfMalf = (from xx in xe.Elements("malfunction")
                                                   select new Malfunctions { Price = (double.Parse(xx.Attribute("Price").Value)), 
                                                          DescriptionOfMalf = xx.Attribute("DescriptionOfMulf").Value, Currancy = "Рублей" }).ToList()
                                  };
                foreach (var item in itemsRepair)
                {
                    CardOfRepair.repairsList.Add((CardOfRepair)item);
                }
                int i = 0;
                int biggestNumb = 0;
                if (CardOfRepair.repairsList.Count == 0)
                    biggestNumb = 0;
                else
                {
                    biggestNumb = CardOfRepair.repairsList[0].NumberOfAct;
                    while (i < CardOfRepair.repairsList.Count - 1)
                    {
                        if (biggestNumb < CardOfRepair.repairsList[i + 1].NumberOfAct)
                            biggestNumb = CardOfRepair.repairsList[i + 1].NumberOfAct;
                        i++;
                    }
                }
                CardOfRepair.Number = biggestNumb;
                //добавление автомобилей в список
                var itemsCar = from xe in xdoc.Element("elements").Element("cars").Elements("car")
                            select new Car
                            {
                                CarMark = xe.Element("CarMark").Value,
                                CarModel = xe.Element("CarModel").Value,
                                CarVIN = xe.Element("VIN").Value,
                                NumberOfCar = xe.Element("NumberOfCar").Value,
                                RegCertific = xe.Element("RegCertific").Value,
                                Owner = new Client
                                {
                                    Address = xe.Element("Owner").Element("Address").Value,
                                    INN = xe.Element("Owner").Element("INN").Value,
                                    Name = xe.Element("Owner").Element("OwnerName").Value,
                                    NumberOfTel = xe.Element("Owner").Element("NumberOfTel").Value
                                }
                            };
                foreach (var item in itemsCar)
                {
                    Car.CarList.Add((Car)item);
                }
                //добавление клиентов в список
                var itemsClients = from xe in xdoc.Element("elements").Element("clients").Elements("client")
                            select new Client
                            {
                                Address = xe.Element("Address").Value,
                                INN = xe.Element("INN").Value,
                                Name = xe.Element("Name").Value,
                                NumberOfTel = xe.Element("NumberOfTel").Value
                            };
                foreach (var item in itemsClients)
                {
                    Client.ClientList.Add((Client)item);
                }
                //добавление персонала в список
                var itemsPersonal = from xe in xdoc.Element("elements").Element("persons").Elements("personal")
                                   select new Personal
                                   {
                                       Address = xe.Element("Address").Value,
                                       INN = xe.Element("INN").Value,
                                       Name = xe.Element("Name").Value,
                                       Function = xe.Element("Function").Value,
                                       NumberOfTel = xe.Element("NumberOfTel").Value
                                   };
                foreach (var item in itemsPersonal)
                {
                    Personal.PersonalList.Add((Personal) item);
                }
                //
                var itemsMalfunctions = from xe in xdoc.Element("elements").Element("malfunctions").Elements("malfunction")
                                    select new Malfunctions
                                    {
                                        Currancy = xe.Element("Currancy").Value,
                                        DescriptionOfMalf = xe.Element("DescriptionOfMulf").Value,
                                        Price = double.Parse(xe.Element("Price").Value)
                                    };
                foreach (var item in itemsMalfunctions)
                {
                    Malfunctions.MalfList.Add((Malfunctions) item);
                }
                if (CardOfRepair.repairsList.Count > 0)
                {
                    AddListRepairsInGrid();
                    DeleteSelectFirstRow();
                }

            }
            csb.DataSource = "localhost";
            csb.Port = 3050;
            csb.Database = @"C:\Users\admin\Desktop\проекты\AUTOSERVICE_DB\AUTOSERVICE_DB.FDB";
            csb.UserID = "SYSDBA";
            csb.Password = "masterkey";
            csb.ServerType = FbServerType.Default;
            db = new FbConnection(csb.ToString());
        }
        //логическая переменная
        bool logicParamForRepair = true;
        //событие при клике на законченные ремонты
        private void EndRepairsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectIndex = 0;
            WindowIndex = 0;
            DataGridViewForRepairs();
            dataGridView.Rows.Clear();
            labelHeaderText.Text = "Завершенные ремонты";
            HideAutoButtons();
            HideRepairButtons();
            HideClientButtons();
            HidePersonalButtons();
            HidePriceButtons();
            if (logicParamForRepair)
            {
                labelHeaderText.Top -= resizeCount;
                dataGridView.Top -= resizeCount;
                dataGridView.Height += resizeCount;
            }
            logicParamForRepair = false;
            foreach (CardOfRepair card in CardOfRepair.repairsList)
            {
                if (!card.RepairIsCurrent)
                {
                    dataGridView.Rows.Add(card.NumberOfAct, card.TimeOfStart, card.MalfunctionsToString(), card.CarInRepairToString(), card.Mechanic.Name, card.Notes);
                }
            }
            DeleteSelectFirstRow();
        }
        //событие при клике на автомобили
        private void AutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            SelectIndex = 0;
            WindowIndex = 0;
            DataGridViewForAuto();
            dataGridView.Visible = true;
            logicParamForRepair = true;
            ShowAutoButtons();
            AddAuto.Location = AddRepair.Location;
            EditAuto.Location = EditRepair.Location;
            DeleteAuto.Location = EndRepair.Location;
            HideRepairButtons();
            HideClientButtons();
            HidePersonalButtons();
            HidePriceButtons();
            HideStockButtons();
            dataGridView.Top = topForGridIdeal;
            labelHeaderText.Text = "Автомобили";
            labelHeaderText.Top = topForLabelOfHead;
            dataGridView.Rows.Clear();
            AddListAutoInGrid();
            DeleteSelectFirstRow();
        }
        //событие при клике на текущие ремонты
        private void CurrentRepairsToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            SelectIndex = 0;
            WindowIndex = 0;
            DataGridViewForRepairs();
            labelHeaderText.Text = "Текущие ремонты";
            ShowRepairButtons();
            HideAutoButtons();
            HideClientButtons();
            HidePersonalButtons();
            HidePriceButtons();
            HidePersonalButtons();
            HideStockButtons();
            AddListRepairsInGrid();
            if (!logicParamForRepair)
            {
                labelHeaderText.Top = topForLabelOfHead;
                dataGridView.Top = topForGridIdeal;
            }
            logicParamForRepair = true;
            DeleteSelectFirstRow();
        }
        //событие при клике на клиенты
        private void ClientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logicParamForRepair = true;
            SelectIndex = 0;
            WindowIndex = 0;  
            HideAutoButtons();
            HideRepairButtons();
            HidePersonalButtons();
            HidePriceButtons();
            ShowClientButtons();
            HideStockButtons();
            AddClient.Location = AddRepair.Location;
            EditClient.Location = EditRepair.Location;
            DeleteClient.Location = EndRepair.Location;
            dataGridView.Rows.Clear();
            labelHeaderText.Text = "Клиенты";
            dataGridView.Top = topForGridIdeal;
            labelHeaderText.Top = topForLabelOfHead;
            DataGridViewForClient();
            DeleteSelectFirstRow();
        }
        //событие при клике на персонал
        private void PersonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logicParamForRepair = true;
            SelectIndex = 0;
            WindowIndex = 0;
            dataGridView.Rows.Clear();
            HideAutoButtons();
            HideRepairButtons();
            HideClientButtons();
            HidePriceButtons();
            HideStockButtons();
            ShowPersonalButtons();
            AddPersonal.Location = AddRepair.Location;
            EditPersonal.Location = EditRepair.Location;
            DeletePersonal.Location = EndRepair.Location;
            labelHeaderText.Text = "Сотрудники";
            dataGridView.Top = topForGridIdeal;
            labelHeaderText.Top = topForLabelOfHead;
            DataGridViewForPersonal();
            DeleteSelectFirstRow();
        }
        //событие при клике на прайс
        private void PriceStripMenuItem1_Click(object sender, EventArgs e)
        {
            logicParamForRepair = true;
            SelectIndex = 0;
            WindowIndex = 0;
            dataGridView.Rows.Clear();
            HideAutoButtons();
            HideRepairButtons();
            HideClientButtons();
            HidePersonalButtons();
            HideStockButtons();
            ShowPriceButtons();
            AddPosition.Location = AddRepair.Location;
            EditPosition.Location = EditRepair.Location;
            DeletePosition.Location = EndRepair.Location;
            labelHeaderText.Text = "Прайс";
            dataGridView.Top = topForGridIdeal;
            labelHeaderText.Top = topForLabelOfHead;
            DataGridViewForPrice();
            DeleteSelectFirstRow();
        }

        private void StockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logicParamForRepair = true;
            SelectIndex = 0;
            WindowIndex = 0;
            HideAutoButtons();
            HideRepairButtons();
            HideClientButtons();
            HidePersonalButtons();
            HidePriceButtons();
            ShowStockButtons();
            AddInStock.Location = AddRepair.Location;
            EditStock.Location = EditRepair.Location;
            DeleteFromStock.Location = EndRepair.Location;
            labelHeaderText.Text = "Склад";
            dataGridView.Top = topForGridIdeal;
            labelHeaderText.Top = topForLabelOfHead;
            DataGridViewForStock();
            DeleteSelectFirstRow();
        }
        //событие при клике по сетке
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SelectIndex = e.RowIndex;
            }
            catch (ArgumentOutOfRangeException)
            {
                SelectIndex = 0;
                return;
            }
        }
        //событие при двойном клике по сетке
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridView.Rows[e.RowIndex].Selected = false;
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                dataGridView.Rows[e.RowIndex].Selected = true;
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
        }

        //функции для сокрытия или показа кнопок
        private void HideAutoButtons()
        {
            AddAuto.Hide();
            EditAuto.Hide();
            DeleteAuto.Hide();
        }
        private void ShowAutoButtons()
        {
            AddAuto.Visible = true;
            EditAuto.Visible = true;
            DeleteAuto.Visible = true;
        }
        private void HideRepairButtons()
        {
            AddRepair.Hide();
            EditRepair.Hide();
            EndRepair.Hide();
        }
        private void ShowRepairButtons()
        {
            AddRepair.Visible = true;
            EditRepair.Visible = true;
            EndRepair.Visible = true;
        }
        private void HideClientButtons()
        {
            AddClient.Hide();
            EditClient.Hide();
            DeleteClient.Hide();
        }
        private void ShowClientButtons()
        {
            AddClient.Visible = true;
            EditClient.Visible = true;
            DeleteClient.Visible = true;
        }
        private void HidePersonalButtons()
        {
            AddPersonal.Hide();
            EditPersonal.Hide();
            DeletePersonal.Hide();
        }
        private void ShowPersonalButtons()
        {
            AddPersonal.Visible = true;
            EditPersonal.Visible = true;
            DeletePersonal.Visible = true;
        }
        private void HidePriceButtons()
        {
            AddPosition.Hide();
            EditPosition.Hide();
            DeletePosition.Hide();
        }
        private void ShowPriceButtons()
        {
            AddPosition.Visible = true;
            EditPosition.Visible = true;
            DeletePosition.Visible = true;
        }
        private void HideStockButtons()
        {
            AddInStock.Hide();
            EditStock.Hide();
            DeleteFromStock.Hide();
        }
        private void ShowStockButtons()
        {
            AddInStock.Visible = true;
            EditStock.Visible = true;
            DeleteFromStock.Visible = true;
        }
        //событие при клике по кнопке добавить ремонт
        private void AddRepair_Click(object sender, EventArgs e)
        {
            WindowIndex = (int) WindowsStruct.Repairs;
            //проверка на наличие открытой формы
            if (!formAddRepair.Visible)
            {
                formAddRepair = new FormAddRepair(formAddAuto, this);
                formAddRepair.ShowDialog();
            }
            else
                //если окно добавления автомобиля будет открыто выведется окно с предупреждением
                MessageBox.Show("Вы уже создаете новый ремонт, для создания нового ремонта завершите старый.", "", MessageBoxButtons.OK);
            return;

        }
        private void EndRepair_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show(string.Format("Вы действительно завершить ремонт №{0}?", CardOfRepair.repairsList[SelectIndex].NumberOfAct), "Предупреждение",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == DialogResult.OK) && CardOfRepair.repairsList.Count > 0)
            {
                CardOfRepair.repairsList[SelectIndex].RepairIsCurrent = false;
                CardOfRepair.repairsList[SelectIndex].TimeOfEnd = DateTime.Now.ToString();
                CardOfRepair.repairsList.Sort(delegate(CardOfRepair card1, CardOfRepair card2)
                {
                    if (!card1.RepairIsCurrent)
                        return 1;
                    if (card1.RepairIsCurrent)
                        return -1;
                    else
                        return 0;
                });
                AddListRepairsInGrid();
            }
        }
        //событие при клике по кнопке добавить автомобиль
        private void AddAuto_Click(object sender, EventArgs e)
        {
            //проверка на наличие открытой формы
            if (!formAddAuto.Visible)
            {
                formAddAuto = new FormAddAuto(formAddRepair, this);
                formAddAuto.StartPosition = FormStartPosition.CenterScreen;
                formAddAuto.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                formAddAuto.MaximizeBox = false;
                formAddAuto.ShowDialog();
            }
            // если окно добавления уже открыто событие игнорируется
            else
                return;
        }
        //событие при клике по кнопке удалить автомобиль
        private void DeleteAuto_Click(object sender, EventArgs e)
        {
            //при клике по заголовку сетки ловится исключение и событие игнорируется
            try
            {
                //при попытке удалить объект появляется окно с подтверждением удаления
                if ((MessageBox.Show(string.Format("Вы действительно хотите удалить эту машину?{0}", Car.CarList[SelectIndex].ToString()), "Предупреждение",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == DialogResult.OK) && Car.CarList.Count > 0)
                {
                    Car.CarList.RemoveAt(SelectIndex);
                    AddListAutoInGrid();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
        }
        
        //событие при клике по кнопке добавить клиента
        private void AddClient_Click(object sender, EventArgs e)
        {
            if (!formAddClient.Visible)
            {
                formAddClient = new FormAddClient(this);
                formAddClient.ShowDialog();
            }
        }
        //событие при клике по кнопке удалить клиента
        private void DeleteClient_Click(object sender, EventArgs e)
        {
            //при клике по заголовку сетки ловится исключение и событие игнорируется
            try
            {
                //при попытке удалить объект появляется окно с подтверждением удаления
                if ((MessageBox.Show(string.Format("Вы действительно хотите удалить эту этого клиента?{0}", Client.ClientList[SelectIndex].ToString()), "Предупреждение",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == DialogResult.OK) && Client.ClientList.Count > 0)
                {
                    Client.ClientList.RemoveAt(SelectIndex);
                    AddListClientInGrid();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
        }
        //событие при клике по кнопке добавить персонал
        private void AddPersonal_Click(object sender, EventArgs e)
        {
            if (!formAddPersonal.Visible)
            {
                formAddPersonal = new FormAddPersonal(this);
                formAddPersonal.ShowDialog();
            }
            else
                return;
        }
        //событие при клике по кнопке удалить клиента
        private void DeletePersonal_Click(object sender, EventArgs e)
        {
            //при клике по заголовку сетки ловится исключение и событие игнорируется
            try
            {
                //при попытке удалить объект появляется окно с подтверждением удаления
                if ((MessageBox.Show(string.Format("Вы действительно хотите удалить эту этого клиента?{0}", Personal.PersonalList[SelectIndex].ToString()), "Предупреждение",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == DialogResult.OK) && Personal.PersonalList.Count > 0)
                {
                    Personal.PersonalList.RemoveAt(SelectIndex);
                    AddListPersonalInGrid();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
        }
        //событие при клике по кнопке добавить позицию
        private void AddPosition_Click(object sender, EventArgs e)
        {
            if (!formAddPrice.Visible)
            {
                formAddPrice = new FormAddPrice(this);
                formAddPrice.ShowDialog();
            }
            else
                return;
        }
        //событие при клике по кнопке удалить позицию
        private void DeletePosition_Click(object sender, EventArgs e)
        {
            //при клике по заголовку сетки ловится исключение и событие игнорируется
            try
            {
                //при попытке удалить объект появляется окно с подтверждением удаления
                if ((MessageBox.Show(string.Format("Вы действительно хотите удалить эту позицию?{0}", Malfunctions.MalfList[SelectIndex].ToString()), "Предупреждение",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == DialogResult.OK) && Malfunctions.MalfList.Count > 0)
                {
                    Malfunctions.MalfList.RemoveAt(SelectIndex);
                    AddListMalfunctionsInGrid();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
        }

        private void AddInStock_Click(object sender, EventArgs e)
        {
            FormAddSparePart.addOrEdit = (int)AddEditOrDelete.Add;
            if (!formAddSparePart.Visible)
            {
                formAddSparePart = new FormAddSparePart(this);
                formAddSparePart.ShowDialog();
            }
            else
                return;
        }
        private void EditStock_Click(object sender, EventArgs e)
        {
            FormAddSparePart.addOrEdit = (int)AddEditOrDelete.Edit;
            if (!formAddSparePart.Visible)
            {
                formAddSparePart = new FormAddSparePart(this);
                string query = string.Format("select sp.uniq_code, sp.description, sp.cost, st.number " +
                    "from sparepart as sp, stock as st where sp.uniq_code = '{0}'  and st.uniq_code = '{0}'",
                dataGridView.Rows[SelectIndex].Cells[0].Value.ToString());
                using (FbCommand command = new FbCommand(query, db))
                {
                    FbDataReader dataReader;
                    db.Open();
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        formAddSparePart.textBoxUniqNumb.Text = dataReader.GetString(0);
                        formAddSparePart.textBoxDescr.Text = dataReader.GetString(1);
                        formAddSparePart.textBoxCost.Text = dataReader.GetString(2);
                        formAddSparePart.textBoxNumb.Text = dataReader.GetString(3);
                    }
                    db.Close();
                }
                formAddSparePart.ShowDialog();
            }
            else
                return;
        }
        //функции для добавления списка объектов в сетку
        public void AddListRepairsInGrid()
        {
            dataGridView.Rows.Clear();
            foreach (CardOfRepair repair in CardOfRepair.repairsList)
            {
                if (repair.RepairIsCurrent)
                {
                    dataGridView.Rows.Add(repair.NumberOfAct, repair.TimeOfStart, repair.MalfunctionsToString(), repair.CarInRepairToString(), repair.Mechanic.Name, repair.Notes);
                }
            }
        }
        public void AddListAutoInGrid()
        {
            dataGridView.Rows.Clear();
            foreach (Car car in Car.CarList)
            {
                dataGridView.Rows.Add(car.CarMark, car.CarModel, car.CarVIN, car.RegCertific, car.NumberOfCar, car.Owner.Name);
            }
        }
        public void AddListClientInGrid()
        {
            dataGridView.Rows.Clear();
            foreach (Client client in Client.ClientList)
            {
                dataGridView.Rows.Add(client.Name, client.INN, client.Address, client.NumberOfTel);
            }
        }
        public void AddListPersonalInGrid()
        {
            dataGridView.Rows.Clear();
            foreach (Personal person in Personal.PersonalList)
            {
                dataGridView.Rows.Add(person.Name, person.INN, person.Address, person.Function, person.NumberOfTel);
            }
        }
        public void AddListMalfunctionsInGrid()
        {
            dataGridView.Rows.Clear();
            foreach (Malfunctions malf in Malfunctions.MalfList)
            {
                dataGridView.Rows.Add(malf.DescriptionOfMalf, malf.Price);
            }
        }
        private void AddSparePartInStock()
        {
            string query = @"select * from stock_view";
            using (FbCommand command = new FbCommand(query, Form1.db))
            {
                FbDataAdapter dataAdapter = new FbDataAdapter(command);
                DataSet ds = new DataSet();
                db.Open();
                dataAdapter.Fill(ds);
                dataGridView.DataSource = ds.Tables[0];
                ds.Tables[0].Columns[0].ColumnName = "Артикул";
                ds.Tables[0].Columns[1].ColumnName = "Наименование";
                ds.Tables[0].Columns[2].ColumnName = "Количество";
                ds.Tables[0].Columns[3].ColumnName = "Cтоимость(руб.)";
                ds.Tables[0].Columns[4].ColumnName = "Автомобиль";
                db.Close();
            }
        }
        //функции для редактирования сетки 
        private void DataGridViewForAuto()
        {
            dataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[0].HeaderText = "Марка";
            dataGridView.Columns[1].HeaderText = "Модель";
            dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[2].HeaderText = "VIN";
            dataGridView.Columns[3].HeaderText = "Свиделельство о регистрации";
            dataGridView.Columns[4].HeaderText = "Гос. номер";
            dataGridView.Columns[5].HeaderText = "Владелец";
            VisibleColumns();
        }
        private void DataGridViewForRepairs()
        {
            dataGridView.Columns[0].HeaderText = "№";
            dataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns[1].HeaderText = "Время начала ремонта";
            dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[2].HeaderText = "Неисправности";
            dataGridView.Columns[3].HeaderText = "Автомобиль";
            dataGridView.Columns[4].HeaderText = "Слесарь";
            dataGridView.Columns[5].HeaderText = "Заметки";
            VisibleColumns();
            /*
            dataGridView.Columns[6].HeaderText = "Гос. номер";
            dataGridView.Columns[7].HeaderText = "Неисправности";
            */
        }
        private void DataGridViewForClient()
        {
            dataGridView.Rows.Clear();
            AddListClientInGrid();
            dataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[0].HeaderText = "Наименование";
            dataGridView.Columns[1].HeaderText = "ИНН";
            dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[2].HeaderText = "Адрес";
            dataGridView.Columns[3].HeaderText = "Номер телефона";
            dataGridView.Columns[2].Visible = true;
            dataGridView.Columns[3].Visible = true;
            dataGridView.Columns[4].Visible = false;
            dataGridView.Columns[5].Visible = false;
        }
        private void DataGridViewForPersonal()
        {
            dataGridView.Rows.Clear();
            AddListPersonalInGrid();
            dataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[0].HeaderText = "ФИО";
            dataGridView.Columns[1].HeaderText = "ИНН";
            dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[2].HeaderText = "Адрес";
            dataGridView.Columns[3].HeaderText = "Должность";
            dataGridView.Columns[4].Visible = true;
            dataGridView.Columns[4].HeaderText = "Номер телефона";
            dataGridView.Columns[5].Visible = false;
        }
        private void DataGridViewForPrice()
        {
            dataGridView.Rows.Clear();
            AddListMalfunctionsInGrid();
            dataGridView.Columns[0].HeaderText = "Описание                                                                                                                                ";
            dataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns[1].HeaderText = "Стоимость";
            dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns[2].Visible = false;
            dataGridView.Columns[3].Visible = false;
            dataGridView.Columns[4].Visible = false;
            dataGridView.Columns[5].Visible = false;
        }
        private void DataGridViewForStock()
        {
            dataGridView.Columns.Clear();
            AddSparePartInStock();
        }

        void VisibleColumns()
        {
            dataGridView.Columns[2].Visible = true;
            dataGridView.Columns[3].Visible = true;
            dataGridView.Columns[4].Visible = true;
            dataGridView.Columns[5].Visible = true;
        }

        //событие при закрытии формы
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            XDocument xdoc = new XDocument();
            XElement repairs = new XElement("repairs");
            if (CardOfRepair.repairsList.Count != 0)
            {
                foreach (CardOfRepair repair in CardOfRepair.repairsList)
                {
                    XElement repairToRepairs = new XElement("repair");
                    repairToRepairs.Add(new XElement("VIN", repair.CarVIN), new XElement("RegCertific", repair.RegCertific),
                        new XElement("CarMark", repair.CarMark), new XElement("CarModel", repair.CarModel), new XElement("NumberOfCar", repair.NumberOfCar),
                        new XElement("Owner", new XElement("OwnerName", repair.Owner.Name), new XElement("INN", repair.Owner.INN), new XElement("Address", repair.Owner.Address),
                        new XElement("NumberOfTel", repair.Owner.NumberOfTel)), new XElement("TimeOfStart", repair.TimeOfStart), new XElement("TimeOfEnd", repair.TimeOfEnd),
                        new XElement("Notes", repair.Notes), new XElement("Mechanic", new XElement("Name", repair.Mechanic.Name)), new XElement("RepairIsCurrent", repair.RepairIsCurrent),
                        new XElement("TotalPrice", repair.TotalPrice), new XElement("NumberOfAct", repair.NumberOfAct));
                    foreach (Malfunctions malf in repair.ListOfMalf)
                    {
                        repairToRepairs.Add(new XElement("malfunction", new XAttribute("Price", malf.Price), new XAttribute("DescriptionOfMulf", malf.DescriptionOfMalf),
                        new XAttribute("Currancy", malf.Currancy)));
                    }
                    repairs.Add(repairToRepairs);
                }
            }
           
            //добавление автомобилей в xml
            XElement cars = new XElement("cars");
            if (Car.CarList.Count != 0)
            {
                foreach (Car car in Car.CarList)
                {
                    cars.Add(new XElement("car", new XElement("VIN", car.CarVIN), new XElement("RegCertific", car.RegCertific),
                        new XElement("CarMark", car.CarMark), new XElement("CarModel", car.CarModel), new XElement("NumberOfCar", car.NumberOfCar),
                        new XElement("Owner", new XElement("OwnerName", car.Owner.Name), new XElement("INN", car.Owner.INN), new XElement("Address", car.Owner.Address),
                        new XElement("NumberOfTel", car.Owner.NumberOfTel))));
                }
            }
            //добавление клиентов в xml
            XElement clients = new XElement("clients");
            if (Client.ClientList.Count != 0)
            {
                foreach (Client client in Client.ClientList)
                {
                    clients.Add(new XElement("client", new XElement("Name", client.Name), new XElement("INN", client.INN),
                    new XElement("Address", client.Address), new XElement("NumberOfTel", client.NumberOfTel)));
                }
            }
            //добавление сотрудников в xml
            XElement persons = new XElement("persons");
            if (Personal.PersonalList.Count != 0)
            {
                foreach (Personal person in Personal.PersonalList)
                {
                    persons.Add(new XElement("personal", new XElement("Name", person.Name), new XElement("INN", person.INN),
                    new XElement("Address", person.Address), new XElement("Function", person.Function), new XElement("NumberOfTel", person.NumberOfTel)));
                }
            }
            //добавление позиций в xml
            XElement malfunctions = new XElement("malfunctions");
            if (Malfunctions.MalfList.Count != 0)
            {
                foreach (Malfunctions malf in Malfunctions.MalfList) 
                {
                    malfunctions.Add(new XElement("malfunction", new XElement("Price", malf.Price), new XElement("DescriptionOfMulf", malf.DescriptionOfMalf),
                    new XElement("Currancy", malf.Currancy)));
                }
            }
            XElement elements = new XElement("elements");
            elements.Add(repairs, cars, clients, persons, malfunctions);
            xdoc.Add(elements);
            xdoc.Save("elements.xml");
        }
        //функция для загрузки xml документа
        public XDocument GetXmlInventory()
        {
            try
            {
                XDocument xDox = XDocument.Load("elements.xml");
                return xDox;
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        //убирает выделение первой строки
        public void DeleteSelectFirstRow()
        {
            if (dataGridView.Rows.Count > 0)
            {
                dataGridView.Rows[0].Cells[0].Selected = false;
            }
        }
        private void SetToolTip(Button buttonType, String text)
        {
            toolTip1.SetToolTip(buttonType, text);
            Point clientCoor = this.PointToClient(new Point(Cursor.Position.X + 20, Cursor.Position.Y + 40));
            toolTip1.Show(text, this, clientCoor);
        }
        private void HideToolTip()
        {
            toolTip1.Hide(this);
        }
        //события при наведении на кнопку
        private void AddInStock_MouseEnter(object sender, EventArgs e)
        {
            SetToolTip(AddInStock, "Добавить новый вид запчастей на склад");
        }
        private void EditStock_MouseEnter(object sender, EventArgs e)
        {
            SetToolTip(AddInStock, "Изменить данные по запчастям");
        }

        private void DeleteFromStock_MouseEnter(object sender, EventArgs e)
        {
            SetToolTip(AddInStock, "Удалить запчасть");
        }

        private void AddInStock_MouseLeave(object sender, EventArgs e)
        {
            HideToolTip();
        }

        private void EditStock_MouseLeave(object sender, EventArgs e)
        {
            HideToolTip();
        }

        private void DeleteFromStock_MouseLeave(object sender, EventArgs e)
        {
            HideToolTip();
        }
    }
}
