namespace AutoService
{
    partial class FormAddWayBill
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddWayBill));
            this.labelDriver = new System.Windows.Forms.Label();
            this.labelTrip = new System.Windows.Forms.Label();
            this.labelClient = new System.Windows.Forms.Label();
            this.labelCar = new System.Windows.Forms.Label();
            this.comboBoxDriver = new System.Windows.Forms.ComboBox();
            this.comboBoxCar = new System.Windows.Forms.ComboBox();
            this.labelCost = new System.Windows.Forms.Label();
            this.labelLdUnLd = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pickerStart = new System.Windows.Forms.DateTimePicker();
            this.pickerEnd = new System.Windows.Forms.DateTimePicker();
            this.txtBoxCost = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxFuel = new System.Windows.Forms.TextBox();
            this.labelFuel = new System.Windows.Forms.Label();
            this.textBoxKm = new System.Windows.Forms.TextBox();
            this.labelKm = new System.Windows.Forms.Label();
            this.btnCreateWayBill = new System.Windows.Forms.Button();
            this.textBoxTrip = new System.Windows.Forms.TextBox();
            this.AddClient = new System.Windows.Forms.Button();
            this.SelectTrip = new System.Windows.Forms.Button();
            this.textBoxClient = new System.Windows.Forms.TextBox();
            this.AddTrip = new System.Windows.Forms.Button();
            this.SelectClient = new System.Windows.Forms.Button();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelDriver
            // 
            this.labelDriver.AutoSize = true;
            this.labelDriver.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDriver.Location = new System.Drawing.Point(12, 259);
            this.labelDriver.Name = "labelDriver";
            this.labelDriver.Size = new System.Drawing.Size(86, 20);
            this.labelDriver.TabIndex = 3;
            this.labelDriver.Text = "Водитель";
            // 
            // labelTrip
            // 
            this.labelTrip.AutoSize = true;
            this.labelTrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTrip.Location = new System.Drawing.Point(12, 82);
            this.labelTrip.Name = "labelTrip";
            this.labelTrip.Size = new System.Drawing.Size(78, 20);
            this.labelTrip.TabIndex = 0;
            this.labelTrip.Text = "Маршрут";
            // 
            // labelClient
            // 
            this.labelClient.AutoSize = true;
            this.labelClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelClient.Location = new System.Drawing.Point(12, 22);
            this.labelClient.Name = "labelClient";
            this.labelClient.Size = new System.Drawing.Size(65, 20);
            this.labelClient.TabIndex = 1;
            this.labelClient.Text = "Клиент";
            // 
            // labelCar
            // 
            this.labelCar.AutoSize = true;
            this.labelCar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCar.Location = new System.Drawing.Point(12, 191);
            this.labelCar.Name = "labelCar";
            this.labelCar.Size = new System.Drawing.Size(104, 20);
            this.labelCar.TabIndex = 5;
            this.labelCar.Text = "Автомобиль";
            // 
            // comboBoxDriver
            // 
            this.comboBoxDriver.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxDriver.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxDriver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDriver.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxDriver.FormattingEnabled = true;
            this.comboBoxDriver.ItemHeight = 20;
            this.comboBoxDriver.Location = new System.Drawing.Point(172, 259);
            this.comboBoxDriver.MaxDropDownItems = 10;
            this.comboBoxDriver.Name = "comboBoxDriver";
            this.comboBoxDriver.Size = new System.Drawing.Size(262, 28);
            this.comboBoxDriver.TabIndex = 14;
            // 
            // comboBoxCar
            // 
            this.comboBoxCar.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxCar.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxCar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxCar.FormattingEnabled = true;
            this.comboBoxCar.ItemHeight = 20;
            this.comboBoxCar.Location = new System.Drawing.Point(172, 191);
            this.comboBoxCar.MaxDropDownItems = 10;
            this.comboBoxCar.Name = "comboBoxCar";
            this.comboBoxCar.Size = new System.Drawing.Size(262, 28);
            this.comboBoxCar.TabIndex = 13;
            this.comboBoxCar.SelectedIndexChanged += new System.EventHandler(this.comboBoxCar_SelectedIndexChanged);
            this.comboBoxCar.TextChanged += new System.EventHandler(this.comboBoxCar_TextChanged);
            // 
            // labelCost
            // 
            this.labelCost.AutoSize = true;
            this.labelCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCost.Location = new System.Drawing.Point(3, 53);
            this.labelCost.Name = "labelCost";
            this.labelCost.Size = new System.Drawing.Size(93, 20);
            this.labelCost.TabIndex = 6;
            this.labelCost.Text = "Стоимость";
            // 
            // labelLdUnLd
            // 
            this.labelLdUnLd.AutoSize = true;
            this.labelLdUnLd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLdUnLd.Location = new System.Drawing.Point(3, 0);
            this.labelLdUnLd.Name = "labelLdUnLd";
            this.labelLdUnLd.Size = new System.Drawing.Size(121, 40);
            this.labelLdUnLd.TabIndex = 4;
            this.labelLdUnLd.Text = "Дата загрузки/\r\nвыгрузки";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.09375F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.42708F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.73958F));
            this.tableLayoutPanel2.Controls.Add(this.labelLdUnLd, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.pickerStart, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.pickerEnd, 2, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(9, 312);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(768, 67);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // pickerStart
            // 
            this.pickerStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pickerStart.Location = new System.Drawing.Point(164, 3);
            this.pickerStart.Name = "pickerStart";
            this.pickerStart.Size = new System.Drawing.Size(193, 26);
            this.pickerStart.TabIndex = 6;
            // 
            // pickerEnd
            // 
            this.pickerEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pickerEnd.Location = new System.Drawing.Point(389, 3);
            this.pickerEnd.Name = "pickerEnd";
            this.pickerEnd.Size = new System.Drawing.Size(185, 26);
            this.pickerEnd.TabIndex = 5;
            // 
            // txtBoxCost
            // 
            this.txtBoxCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtBoxCost.Location = new System.Drawing.Point(162, 56);
            this.txtBoxCost.Name = "txtBoxCost";
            this.txtBoxCost.Size = new System.Drawing.Size(138, 26);
            this.txtBoxCost.TabIndex = 8;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.44523F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.81272F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.76923F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 223F));
            this.tableLayoutPanel3.Controls.Add(this.textBoxFuel, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelFuel, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.textBoxKm, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelKm, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelCost, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtBoxCost, 1, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(9, 385);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(785, 106);
            this.tableLayoutPanel3.TabIndex = 9;
            // 
            // textBoxFuel
            // 
            this.textBoxFuel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxFuel.Location = new System.Drawing.Point(563, 3);
            this.textBoxFuel.Name = "textBoxFuel";
            this.textBoxFuel.Size = new System.Drawing.Size(138, 26);
            this.textBoxFuel.TabIndex = 12;
            // 
            // labelFuel
            // 
            this.labelFuel.AutoSize = true;
            this.labelFuel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelFuel.Location = new System.Drawing.Point(391, 0);
            this.labelFuel.Name = "labelFuel";
            this.labelFuel.Size = new System.Drawing.Size(97, 40);
            this.labelFuel.TabIndex = 11;
            this.labelFuel.Text = "Затрачено топлива(л):";
            // 
            // textBoxKm
            // 
            this.textBoxKm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxKm.Location = new System.Drawing.Point(162, 3);
            this.textBoxKm.Name = "textBoxKm";
            this.textBoxKm.Size = new System.Drawing.Size(138, 26);
            this.textBoxKm.TabIndex = 10;
            // 
            // labelKm
            // 
            this.labelKm.AutoSize = true;
            this.labelKm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelKm.Location = new System.Drawing.Point(3, 0);
            this.labelKm.Name = "labelKm";
            this.labelKm.Size = new System.Drawing.Size(119, 20);
            this.labelKm.TabIndex = 9;
            this.labelKm.Text = "Пройдено(км):";
            // 
            // btnCreateWayBill
            // 
            this.btnCreateWayBill.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCreateWayBill.Location = new System.Drawing.Point(653, 512);
            this.btnCreateWayBill.Name = "btnCreateWayBill";
            this.btnCreateWayBill.Size = new System.Drawing.Size(124, 37);
            this.btnCreateWayBill.TabIndex = 10;
            this.btnCreateWayBill.Text = "Создать";
            this.btnCreateWayBill.UseVisualStyleBackColor = true;
            this.btnCreateWayBill.Click += new System.EventHandler(this.btnCreateWayBill_Click);
            // 
            // textBoxTrip
            // 
            this.textBoxTrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxTrip.Location = new System.Drawing.Point(172, 82);
            this.textBoxTrip.Multiline = true;
            this.textBoxTrip.Name = "textBoxTrip";
            this.textBoxTrip.ReadOnly = true;
            this.textBoxTrip.Size = new System.Drawing.Size(431, 77);
            this.textBoxTrip.TabIndex = 17;
            // 
            // AddClient
            // 
            this.AddClient.BackColor = System.Drawing.Color.Transparent;
            this.AddClient.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AddClient.FlatAppearance.BorderSize = 0;
            this.AddClient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddClient.Image = ((System.Drawing.Image)(resources.GetObject("AddClient.Image")));
            this.AddClient.Location = new System.Drawing.Point(573, 18);
            this.AddClient.Name = "AddClient";
            this.AddClient.Size = new System.Drawing.Size(30, 30);
            this.AddClient.TabIndex = 20;
            this.AddClient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AddClient.UseVisualStyleBackColor = false;
            // 
            // SelectTrip
            // 
            this.SelectTrip.BackColor = System.Drawing.Color.Transparent;
            this.SelectTrip.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.SelectTrip.FlatAppearance.BorderSize = 0;
            this.SelectTrip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectTrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SelectTrip.Image = ((System.Drawing.Image)(resources.GetObject("SelectTrip.Image")));
            this.SelectTrip.Location = new System.Drawing.Point(609, 129);
            this.SelectTrip.Name = "SelectTrip";
            this.SelectTrip.Size = new System.Drawing.Size(30, 30);
            this.SelectTrip.TabIndex = 21;
            this.SelectTrip.UseVisualStyleBackColor = false;
            // 
            // textBoxClient
            // 
            this.textBoxClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxClient.Location = new System.Drawing.Point(172, 22);
            this.textBoxClient.Name = "textBoxClient";
            this.textBoxClient.ReadOnly = true;
            this.textBoxClient.Size = new System.Drawing.Size(362, 26);
            this.textBoxClient.TabIndex = 19;
            // 
            // AddTrip
            // 
            this.AddTrip.BackColor = System.Drawing.Color.Transparent;
            this.AddTrip.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AddTrip.FlatAppearance.BorderSize = 0;
            this.AddTrip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddTrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddTrip.Image = ((System.Drawing.Image)(resources.GetObject("AddTrip.Image")));
            this.AddTrip.Location = new System.Drawing.Point(609, 82);
            this.AddTrip.Name = "AddTrip";
            this.AddTrip.Size = new System.Drawing.Size(30, 30);
            this.AddTrip.TabIndex = 22;
            this.AddTrip.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AddTrip.UseVisualStyleBackColor = false;
            // 
            // SelectClient
            // 
            this.SelectClient.BackColor = System.Drawing.Color.Transparent;
            this.SelectClient.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.SelectClient.FlatAppearance.BorderSize = 0;
            this.SelectClient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SelectClient.Image = ((System.Drawing.Image)(resources.GetObject("SelectClient.Image")));
            this.SelectClient.Location = new System.Drawing.Point(609, 18);
            this.SelectClient.Name = "SelectClient";
            this.SelectClient.Size = new System.Drawing.Size(30, 30);
            this.SelectClient.TabIndex = 23;
            this.SelectClient.UseVisualStyleBackColor = false;
            // 
            // FormAddWayBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 564);
            this.Controls.Add(this.SelectClient);
            this.Controls.Add(this.AddTrip);
            this.Controls.Add(this.SelectTrip);
            this.Controls.Add(this.AddClient);
            this.Controls.Add(this.textBoxClient);
            this.Controls.Add(this.comboBoxDriver);
            this.Controls.Add(this.labelCar);
            this.Controls.Add(this.labelDriver);
            this.Controls.Add(this.labelClient);
            this.Controls.Add(this.comboBoxCar);
            this.Controls.Add(this.labelTrip);
            this.Controls.Add(this.btnCreateWayBill);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.textBoxTrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAddWayBill";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление маршрута";
            this.Load += new System.EventHandler(this.FormAddWayBill_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelTrip;
        private System.Windows.Forms.Label labelClient;
        private System.Windows.Forms.Label labelCost;
        private System.Windows.Forms.Label labelLdUnLd;
        private System.Windows.Forms.Label labelDriver;
        private System.Windows.Forms.Label labelCar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox txtBoxCost;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnCreateWayBill;
        public System.Windows.Forms.ComboBox comboBoxCar;
        public System.Windows.Forms.ComboBox comboBoxDriver;
        private System.Windows.Forms.DateTimePicker pickerStart;
        private System.Windows.Forms.DateTimePicker pickerEnd;
        private System.Windows.Forms.TextBox textBoxFuel;
        private System.Windows.Forms.Label labelFuel;
        private System.Windows.Forms.TextBox textBoxKm;
        private System.Windows.Forms.Label labelKm;
        private System.Windows.Forms.Button AddClient;
        private System.Windows.Forms.Button SelectTrip;
        private System.Windows.Forms.Button AddTrip;
        private System.Windows.Forms.Button SelectClient;
        public System.Windows.Forms.TextBox textBoxTrip;
        public System.Windows.Forms.TextBox textBoxClient;
    }
}