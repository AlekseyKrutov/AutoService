﻿namespace AutoService
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.RepairsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CurrentRepairsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.EndRepairsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PaymenInvoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClientsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PersonalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PriceStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.StockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddRepair = new System.Windows.Forms.Button();
            this.labelHeaderText = new System.Windows.Forms.Label();
            this.EndRepair = new System.Windows.Forms.Button();
            this.AddAuto = new System.Windows.Forms.Button();
            this.AddClient = new System.Windows.Forms.Button();
            this.AddPersonal = new System.Windows.Forms.Button();
            this.AddPosition = new System.Windows.Forms.Button();
            this.EditRepair = new System.Windows.Forms.Button();
            this.EditPersonal = new System.Windows.Forms.Button();
            this.EditClient = new System.Windows.Forms.Button();
            this.EditAuto = new System.Windows.Forms.Button();
            this.EditPosition = new System.Windows.Forms.Button();
            this.AddInStock = new System.Windows.Forms.Button();
            this.EditStock = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.labelSearch = new System.Windows.Forms.Label();
            this.labelLogin = new System.Windows.Forms.Label();
            this.FinishActToolStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RepairsToolStripMenuItem,
            this.ClientsToolStripMenuItem,
            this.PersonalToolStripMenuItem,
            this.autoToolStripMenuItem,
            this.PriceStripMenuItem1,
            this.StockToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1003, 40);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // RepairsToolStripMenuItem
            // 
            this.RepairsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CurrentRepairsToolStripMenuItem1,
            this.EndRepairsToolStripMenuItem});
            this.RepairsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RepairsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("RepairsToolStripMenuItem.Image")));
            this.RepairsToolStripMenuItem.Name = "RepairsToolStripMenuItem";
            this.RepairsToolStripMenuItem.Size = new System.Drawing.Size(127, 36);
            this.RepairsToolStripMenuItem.Text = "Ремонты";
            // 
            // CurrentRepairsToolStripMenuItem1
            // 
            this.CurrentRepairsToolStripMenuItem1.Name = "CurrentRepairsToolStripMenuItem1";
            this.CurrentRepairsToolStripMenuItem1.Size = new System.Drawing.Size(312, 34);
            this.CurrentRepairsToolStripMenuItem1.Text = "Текущие ремонты";
            this.CurrentRepairsToolStripMenuItem1.Click += new System.EventHandler(this.CurrentRepairsToolStripMenuItem1_Click);
            // 
            // EndRepairsToolStripMenuItem
            // 
            this.EndRepairsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PaymenInvoiceToolStripMenuItem,
            this.FinishActToolStripMenu});
            this.EndRepairsToolStripMenuItem.Name = "EndRepairsToolStripMenuItem";
            this.EndRepairsToolStripMenuItem.Size = new System.Drawing.Size(312, 34);
            this.EndRepairsToolStripMenuItem.Text = "Завершенные ремонты";
            this.EndRepairsToolStripMenuItem.Click += new System.EventHandler(this.EndRepairsToolStripMenuItem_Click);
            // 
            // PaymenInvoiceToolStripMenuItem
            // 
            this.PaymenInvoiceToolStripMenuItem.Name = "PaymenInvoiceToolStripMenuItem";
            this.PaymenInvoiceToolStripMenuItem.Size = new System.Drawing.Size(320, 34);
            this.PaymenInvoiceToolStripMenuItem.Text = "Счет на оплату";
            this.PaymenInvoiceToolStripMenuItem.Click += new System.EventHandler(this.PaymenInvoiceToolStripMenuItem_Click);
            // 
            // ClientsToolStripMenuItem
            // 
            this.ClientsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ClientsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ClientsToolStripMenuItem.Image")));
            this.ClientsToolStripMenuItem.Name = "ClientsToolStripMenuItem";
            this.ClientsToolStripMenuItem.Size = new System.Drawing.Size(123, 36);
            this.ClientsToolStripMenuItem.Text = "Клиенты";
            this.ClientsToolStripMenuItem.Click += new System.EventHandler(this.ClientsToolStripMenuItem_Click);
            // 
            // PersonalToolStripMenuItem
            // 
            this.PersonalToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PersonalToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("PersonalToolStripMenuItem.Image")));
            this.PersonalToolStripMenuItem.Name = "PersonalToolStripMenuItem";
            this.PersonalToolStripMenuItem.Size = new System.Drawing.Size(154, 36);
            this.PersonalToolStripMenuItem.Text = "Сотрудники";
            this.PersonalToolStripMenuItem.Click += new System.EventHandler(this.PersonalToolStripMenuItem_Click);
            // 
            // autoToolStripMenuItem
            // 
            this.autoToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.autoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("autoToolStripMenuItem.Image")));
            this.autoToolStripMenuItem.Name = "autoToolStripMenuItem";
            this.autoToolStripMenuItem.Size = new System.Drawing.Size(161, 36);
            this.autoToolStripMenuItem.Text = "Автомобили";
            this.autoToolStripMenuItem.Click += new System.EventHandler(this.AutoToolStripMenuItem_Click);
            // 
            // PriceStripMenuItem1
            // 
            this.PriceStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PriceStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("PriceStripMenuItem1.Image")));
            this.PriceStripMenuItem1.Name = "PriceStripMenuItem1";
            this.PriceStripMenuItem1.Size = new System.Drawing.Size(101, 36);
            this.PriceStripMenuItem1.Text = "Прайс";
            this.PriceStripMenuItem1.Click += new System.EventHandler(this.PriceStripMenuItem1_Click);
            // 
            // StockToolStripMenuItem
            // 
            this.StockToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StockToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("StockToolStripMenuItem.Image")));
            this.StockToolStripMenuItem.Name = "StockToolStripMenuItem";
            this.StockToolStripMenuItem.Size = new System.Drawing.Size(97, 36);
            this.StockToolStripMenuItem.Text = "Склад";
            this.StockToolStripMenuItem.Click += new System.EventHandler(this.StockToolStripMenuItem_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeight = 50;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Aqua;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.EnableHeadersVisualStyles = false;
            this.dataGridView.Location = new System.Drawing.Point(0, 158);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView.ShowCellToolTips = false;
            this.dataGridView.Size = new System.Drawing.Size(1003, 539);
            this.dataGridView.TabIndex = 1;
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "№";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Время начала работы";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Неисправность";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Марка";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Модель";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Гос. номер";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // AddRepair
            // 
            this.AddRepair.BackColor = System.Drawing.Color.Transparent;
            this.AddRepair.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AddRepair.FlatAppearance.BorderSize = 0;
            this.AddRepair.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddRepair.Image = ((System.Drawing.Image)(resources.GetObject("AddRepair.Image")));
            this.AddRepair.Location = new System.Drawing.Point(6, 79);
            this.AddRepair.Name = "AddRepair";
            this.AddRepair.Size = new System.Drawing.Size(70, 70);
            this.AddRepair.TabIndex = 2;
            this.AddRepair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AddRepair.UseVisualStyleBackColor = false;
            this.AddRepair.Click += new System.EventHandler(this.AddRepair_Click);
            this.AddRepair.MouseEnter += new System.EventHandler(this.AddRepair_MouseEnter);
            this.AddRepair.MouseLeave += new System.EventHandler(this.AddRepair_MouseLeave);
            // 
            // labelHeaderText
            // 
            this.labelHeaderText.AutoSize = true;
            this.labelHeaderText.Font = new System.Drawing.Font("Segoe UI", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelHeaderText.Location = new System.Drawing.Point(0, 40);
            this.labelHeaderText.Name = "labelHeaderText";
            this.labelHeaderText.Size = new System.Drawing.Size(131, 31);
            this.labelHeaderText.TabIndex = 3;
            this.labelHeaderText.Text = "HeaderText";
            // 
            // EndRepair
            // 
            this.EndRepair.BackColor = System.Drawing.Color.Transparent;
            this.EndRepair.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.EndRepair.FlatAppearance.BorderSize = 0;
            this.EndRepair.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EndRepair.Image = ((System.Drawing.Image)(resources.GetObject("EndRepair.Image")));
            this.EndRepair.Location = new System.Drawing.Point(158, 79);
            this.EndRepair.Name = "EndRepair";
            this.EndRepair.Size = new System.Drawing.Size(70, 70);
            this.EndRepair.TabIndex = 4;
            this.EndRepair.UseVisualStyleBackColor = false;
            this.EndRepair.Click += new System.EventHandler(this.EndRepair_Click);
            this.EndRepair.MouseEnter += new System.EventHandler(this.EndRepair_MouseEnter);
            this.EndRepair.MouseLeave += new System.EventHandler(this.EndRepair_MouseLeave);
            // 
            // AddAuto
            // 
            this.AddAuto.BackColor = System.Drawing.Color.Transparent;
            this.AddAuto.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AddAuto.FlatAppearance.BorderSize = 0;
            this.AddAuto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddAuto.Image = ((System.Drawing.Image)(resources.GetObject("AddAuto.Image")));
            this.AddAuto.Location = new System.Drawing.Point(206, 193);
            this.AddAuto.Name = "AddAuto";
            this.AddAuto.Size = new System.Drawing.Size(70, 70);
            this.AddAuto.TabIndex = 5;
            this.AddAuto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AddAuto.UseVisualStyleBackColor = false;
            this.AddAuto.Click += new System.EventHandler(this.AddAuto_Click);
            this.AddAuto.MouseEnter += new System.EventHandler(this.AddAuto_MouseEnter);
            this.AddAuto.MouseLeave += new System.EventHandler(this.AddAuto_MouseLeave);
            // 
            // AddClient
            // 
            this.AddClient.BackColor = System.Drawing.Color.Transparent;
            this.AddClient.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AddClient.FlatAppearance.BorderSize = 0;
            this.AddClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddClient.Image = ((System.Drawing.Image)(resources.GetObject("AddClient.Image")));
            this.AddClient.Location = new System.Drawing.Point(206, 269);
            this.AddClient.Name = "AddClient";
            this.AddClient.Size = new System.Drawing.Size(70, 70);
            this.AddClient.TabIndex = 7;
            this.AddClient.UseVisualStyleBackColor = false;
            this.AddClient.Click += new System.EventHandler(this.AddClient_Click);
            this.AddClient.MouseEnter += new System.EventHandler(this.AddClient_MouseEnter);
            this.AddClient.MouseLeave += new System.EventHandler(this.AddClient_MouseLeave);
            // 
            // AddPersonal
            // 
            this.AddPersonal.BackColor = System.Drawing.Color.Transparent;
            this.AddPersonal.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AddPersonal.FlatAppearance.BorderSize = 0;
            this.AddPersonal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddPersonal.Image = ((System.Drawing.Image)(resources.GetObject("AddPersonal.Image")));
            this.AddPersonal.Location = new System.Drawing.Point(206, 431);
            this.AddPersonal.Name = "AddPersonal";
            this.AddPersonal.Size = new System.Drawing.Size(70, 70);
            this.AddPersonal.TabIndex = 9;
            this.AddPersonal.UseVisualStyleBackColor = false;
            this.AddPersonal.Visible = false;
            this.AddPersonal.Click += new System.EventHandler(this.AddPersonal_Click);
            this.AddPersonal.MouseEnter += new System.EventHandler(this.AddPersonal_MouseEnter);
            this.AddPersonal.MouseLeave += new System.EventHandler(this.AddPersonal_MouseLeave);
            // 
            // AddPosition
            // 
            this.AddPosition.BackColor = System.Drawing.Color.Transparent;
            this.AddPosition.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AddPosition.FlatAppearance.BorderSize = 0;
            this.AddPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddPosition.Image = ((System.Drawing.Image)(resources.GetObject("AddPosition.Image")));
            this.AddPosition.Location = new System.Drawing.Point(206, 355);
            this.AddPosition.Name = "AddPosition";
            this.AddPosition.Size = new System.Drawing.Size(70, 70);
            this.AddPosition.TabIndex = 11;
            this.AddPosition.UseVisualStyleBackColor = false;
            this.AddPosition.Visible = false;
            this.AddPosition.Click += new System.EventHandler(this.AddPosition_Click);
            this.AddPosition.MouseEnter += new System.EventHandler(this.AddPosition_MouseEnter);
            this.AddPosition.MouseLeave += new System.EventHandler(this.AddPosition_MouseLeave);
            // 
            // EditRepair
            // 
            this.EditRepair.BackColor = System.Drawing.Color.Transparent;
            this.EditRepair.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.EditRepair.FlatAppearance.BorderSize = 0;
            this.EditRepair.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EditRepair.Image = ((System.Drawing.Image)(resources.GetObject("EditRepair.Image")));
            this.EditRepair.Location = new System.Drawing.Point(82, 79);
            this.EditRepair.Name = "EditRepair";
            this.EditRepair.Size = new System.Drawing.Size(70, 70);
            this.EditRepair.TabIndex = 13;
            this.EditRepair.UseVisualStyleBackColor = false;
            this.EditRepair.Click += new System.EventHandler(this.EditRepair_Click);
            this.EditRepair.MouseEnter += new System.EventHandler(this.EditRepair_MouseEnter);
            this.EditRepair.MouseLeave += new System.EventHandler(this.EditRepair_MouseLeave);
            // 
            // EditPersonal
            // 
            this.EditPersonal.BackColor = System.Drawing.Color.Transparent;
            this.EditPersonal.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.EditPersonal.FlatAppearance.BorderSize = 0;
            this.EditPersonal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EditPersonal.Image = ((System.Drawing.Image)(resources.GetObject("EditPersonal.Image")));
            this.EditPersonal.Location = new System.Drawing.Point(282, 431);
            this.EditPersonal.Name = "EditPersonal";
            this.EditPersonal.Size = new System.Drawing.Size(70, 70);
            this.EditPersonal.TabIndex = 14;
            this.EditPersonal.UseVisualStyleBackColor = false;
            this.EditPersonal.Click += new System.EventHandler(this.EditPersonal_Click);
            this.EditPersonal.MouseEnter += new System.EventHandler(this.EditPersonal_MouseEnter);
            this.EditPersonal.MouseLeave += new System.EventHandler(this.EditPersonal_MouseLeave);
            // 
            // EditClient
            // 
            this.EditClient.BackColor = System.Drawing.Color.Transparent;
            this.EditClient.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.EditClient.FlatAppearance.BorderSize = 0;
            this.EditClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EditClient.Image = ((System.Drawing.Image)(resources.GetObject("EditClient.Image")));
            this.EditClient.Location = new System.Drawing.Point(282, 269);
            this.EditClient.Name = "EditClient";
            this.EditClient.Size = new System.Drawing.Size(70, 70);
            this.EditClient.TabIndex = 15;
            this.EditClient.UseVisualStyleBackColor = false;
            this.EditClient.Click += new System.EventHandler(this.EditClient_Click);
            this.EditClient.MouseEnter += new System.EventHandler(this.EditClient_MouseEnter);
            this.EditClient.MouseLeave += new System.EventHandler(this.EditClient_MouseLeave);
            // 
            // EditAuto
            // 
            this.EditAuto.BackColor = System.Drawing.Color.Transparent;
            this.EditAuto.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.EditAuto.FlatAppearance.BorderSize = 0;
            this.EditAuto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EditAuto.Image = ((System.Drawing.Image)(resources.GetObject("EditAuto.Image")));
            this.EditAuto.Location = new System.Drawing.Point(282, 193);
            this.EditAuto.Name = "EditAuto";
            this.EditAuto.Size = new System.Drawing.Size(70, 70);
            this.EditAuto.TabIndex = 16;
            this.EditAuto.UseVisualStyleBackColor = false;
            this.EditAuto.Click += new System.EventHandler(this.EditAuto_Click);
            this.EditAuto.MouseEnter += new System.EventHandler(this.EditAuto_MouseEnter);
            this.EditAuto.MouseLeave += new System.EventHandler(this.EditAuto_MouseLeave);
            // 
            // EditPosition
            // 
            this.EditPosition.BackColor = System.Drawing.Color.Transparent;
            this.EditPosition.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.EditPosition.FlatAppearance.BorderSize = 0;
            this.EditPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EditPosition.Image = ((System.Drawing.Image)(resources.GetObject("EditPosition.Image")));
            this.EditPosition.Location = new System.Drawing.Point(282, 355);
            this.EditPosition.Name = "EditPosition";
            this.EditPosition.Size = new System.Drawing.Size(70, 70);
            this.EditPosition.TabIndex = 17;
            this.EditPosition.UseVisualStyleBackColor = false;
            this.EditPosition.Visible = false;
            this.EditPosition.MouseEnter += new System.EventHandler(this.EditPosition_MouseEnter);
            this.EditPosition.MouseLeave += new System.EventHandler(this.EditPosition_MouseLeave);
            // 
            // AddInStock
            // 
            this.AddInStock.BackColor = System.Drawing.Color.Transparent;
            this.AddInStock.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AddInStock.FlatAppearance.BorderSize = 0;
            this.AddInStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddInStock.Image = ((System.Drawing.Image)(resources.GetObject("AddInStock.Image")));
            this.AddInStock.Location = new System.Drawing.Point(206, 507);
            this.AddInStock.Name = "AddInStock";
            this.AddInStock.Size = new System.Drawing.Size(70, 70);
            this.AddInStock.TabIndex = 18;
            this.AddInStock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AddInStock.UseVisualStyleBackColor = false;
            this.AddInStock.Click += new System.EventHandler(this.AddInStock_Click);
            this.AddInStock.MouseEnter += new System.EventHandler(this.AddInStock_MouseEnter);
            this.AddInStock.MouseLeave += new System.EventHandler(this.AddInStock_MouseLeave);
            // 
            // EditStock
            // 
            this.EditStock.BackColor = System.Drawing.Color.Transparent;
            this.EditStock.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.EditStock.FlatAppearance.BorderSize = 0;
            this.EditStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EditStock.Image = ((System.Drawing.Image)(resources.GetObject("EditStock.Image")));
            this.EditStock.Location = new System.Drawing.Point(282, 507);
            this.EditStock.Name = "EditStock";
            this.EditStock.Size = new System.Drawing.Size(70, 70);
            this.EditStock.TabIndex = 19;
            this.EditStock.UseVisualStyleBackColor = false;
            this.EditStock.Click += new System.EventHandler(this.EditStock_Click);
            this.EditStock.MouseEnter += new System.EventHandler(this.EditStock_MouseEnter);
            this.EditStock.MouseLeave += new System.EventHandler(this.EditStock_MouseLeave);
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxSearch.Location = new System.Drawing.Point(790, 63);
            this.textBoxSearch.Multiline = true;
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(201, 30);
            this.textBoxSearch.TabIndex = 21;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // labelSearch
            // 
            this.labelSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSearch.AutoSize = true;
            this.labelSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSearch.Location = new System.Drawing.Point(725, 67);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(59, 20);
            this.labelSearch.TabIndex = 22;
            this.labelSearch.Text = "Поиск:";
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLogin.Location = new System.Drawing.Point(860, 9);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(0, 16);
            this.labelLogin.TabIndex = 23;
            // 
            // FinishActToolStripMenu
            // 
            this.FinishActToolStripMenu.Name = "FinishActToolStripMenu";
            this.FinishActToolStripMenu.Size = new System.Drawing.Size(320, 34);
            this.FinishActToolStripMenu.Text = "Акт выполненных работ";
            this.FinishActToolStripMenu.Click += new System.EventHandler(this.FinishActToolStripMenu_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 700);
            this.Controls.Add(this.labelLogin);
            this.Controls.Add(this.labelSearch);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.EditStock);
            this.Controls.Add(this.AddInStock);
            this.Controls.Add(this.EditPosition);
            this.Controls.Add(this.EditAuto);
            this.Controls.Add(this.EditClient);
            this.Controls.Add(this.EditPersonal);
            this.Controls.Add(this.EditRepair);
            this.Controls.Add(this.AddPosition);
            this.Controls.Add(this.AddPersonal);
            this.Controls.Add(this.AddClient);
            this.Controls.Add(this.AddAuto);
            this.Controls.Add(this.EndRepair);
            this.Controls.Add(this.labelHeaderText);
            this.Controls.Add(this.AddRepair);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Автосервис";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem RepairsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClientsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PersonalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CurrentRepairsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem EndRepairsToolStripMenuItem;
        private System.Windows.Forms.Button AddRepair;
        private System.Windows.Forms.ToolStripMenuItem autoToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.Label labelHeaderText;
        private System.Windows.Forms.Button EndRepair;
        private System.Windows.Forms.Button AddAuto;
        public System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ToolStripMenuItem PriceStripMenuItem1;
        private System.Windows.Forms.Button AddClient;
        private System.Windows.Forms.Button AddPersonal;
        private System.Windows.Forms.Button AddPosition;
        private System.Windows.Forms.ToolStripMenuItem StockToolStripMenuItem;
        private System.Windows.Forms.Button EditRepair;
        private System.Windows.Forms.Button EditPersonal;
        private System.Windows.Forms.Button EditClient;
        private System.Windows.Forms.Button EditAuto;
        private System.Windows.Forms.Button EditPosition;
        private System.Windows.Forms.Button AddInStock;
        private System.Windows.Forms.Button EditStock;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label labelSearch;
        public System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.ToolStripMenuItem PaymenInvoiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FinishActToolStripMenu;
    }
}

