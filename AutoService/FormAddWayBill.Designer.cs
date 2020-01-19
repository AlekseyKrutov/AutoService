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
            this.textBoxBaseDoc = new System.Windows.Forms.TextBox();
            this.labelBaseDoc = new System.Windows.Forms.Label();
            this.labelFuel = new System.Windows.Forms.Label();
            this.textBoxFuel = new System.Windows.Forms.TextBox();
            this.labelKm = new System.Windows.Forms.Label();
            this.textBoxKm = new System.Windows.Forms.TextBox();
            this.textBoxNotes = new System.Windows.Forms.TextBox();
            this.labelNotes = new System.Windows.Forms.Label();
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
            this.labelDriver.Location = new System.Drawing.Point(12, 251);
            this.labelDriver.Name = "labelDriver";
            this.labelDriver.Size = new System.Drawing.Size(92, 20);
            this.labelDriver.TabIndex = 3;
            this.labelDriver.Text = "Водитель*";
            // 
            // labelTrip
            // 
            this.labelTrip.AutoSize = true;
            this.labelTrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTrip.Location = new System.Drawing.Point(12, 78);
            this.labelTrip.Name = "labelTrip";
            this.labelTrip.Size = new System.Drawing.Size(84, 20);
            this.labelTrip.TabIndex = 0;
            this.labelTrip.Text = "Маршрут*";
            // 
            // labelClient
            // 
            this.labelClient.AutoSize = true;
            this.labelClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelClient.Location = new System.Drawing.Point(12, 22);
            this.labelClient.Name = "labelClient";
            this.labelClient.Size = new System.Drawing.Size(71, 20);
            this.labelClient.TabIndex = 1;
            this.labelClient.Text = "Клиент*";
            // 
            // labelCar
            // 
            this.labelCar.AutoSize = true;
            this.labelCar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCar.Location = new System.Drawing.Point(12, 187);
            this.labelCar.Name = "labelCar";
            this.labelCar.Size = new System.Drawing.Size(110, 20);
            this.labelCar.TabIndex = 5;
            this.labelCar.Text = "Автомобиль*";
            // 
            // comboBoxDriver
            // 
            this.comboBoxDriver.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxDriver.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxDriver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDriver.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxDriver.FormattingEnabled = true;
            this.comboBoxDriver.ItemHeight = 20;
            this.comboBoxDriver.Location = new System.Drawing.Point(172, 251);
            this.comboBoxDriver.MaxDropDownItems = 10;
            this.comboBoxDriver.Name = "comboBoxDriver";
            this.comboBoxDriver.Size = new System.Drawing.Size(262, 28);
            this.comboBoxDriver.TabIndex = 2;
            // 
            // comboBoxCar
            // 
            this.comboBoxCar.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxCar.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxCar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxCar.FormattingEnabled = true;
            this.comboBoxCar.ItemHeight = 20;
            this.comboBoxCar.Location = new System.Drawing.Point(172, 187);
            this.comboBoxCar.MaxDropDownItems = 10;
            this.comboBoxCar.Name = "comboBoxCar";
            this.comboBoxCar.Size = new System.Drawing.Size(262, 28);
            this.comboBoxCar.TabIndex = 1;
            this.comboBoxCar.SelectedIndexChanged += new System.EventHandler(this.comboBoxCar_SelectedIndexChanged);
            this.comboBoxCar.TextChanged += new System.EventHandler(this.comboBoxCar_TextChanged);
            // 
            // labelCost
            // 
            this.labelCost.AutoSize = true;
            this.labelCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCost.Location = new System.Drawing.Point(3, 67);
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
            this.labelLdUnLd.Size = new System.Drawing.Size(127, 40);
            this.labelLdUnLd.TabIndex = 4;
            this.labelLdUnLd.Text = "Дата загрузки*/\r\nвыгрузки";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.09375F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.85417F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.55729F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.36458F));
            this.tableLayoutPanel2.Controls.Add(this.labelLdUnLd, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.pickerStart, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.pickerEnd, 2, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(9, 302);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(768, 67);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // pickerStart
            // 
            this.pickerStart.CustomFormat = "dd.MM.yyyy HH:mm";
            this.pickerStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.pickerStart.Location = new System.Drawing.Point(165, 3);
            this.pickerStart.Name = "pickerStart";
            this.pickerStart.ShowCheckBox = true;
            this.pickerStart.Size = new System.Drawing.Size(219, 26);
            this.pickerStart.TabIndex = 3;
            // 
            // pickerEnd
            // 
            this.pickerEnd.Checked = false;
            this.pickerEnd.CustomFormat = "dd.MM.yyyy HH:mm";
            this.pickerEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.pickerEnd.Location = new System.Drawing.Point(425, 3);
            this.pickerEnd.Name = "pickerEnd";
            this.pickerEnd.ShowCheckBox = true;
            this.pickerEnd.Size = new System.Drawing.Size(214, 26);
            this.pickerEnd.TabIndex = 4;
            // 
            // txtBoxCost
            // 
            this.txtBoxCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtBoxCost.Location = new System.Drawing.Point(164, 70);
            this.txtBoxCost.Name = "txtBoxCost";
            this.txtBoxCost.Size = new System.Drawing.Size(138, 26);
            this.txtBoxCost.TabIndex = 7;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.74004F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.35104F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.90892F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 281F));
            this.tableLayoutPanel3.Controls.Add(this.textBoxBaseDoc, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelBaseDoc, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelCost, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtBoxCost, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.labelFuel, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.textBoxFuel, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.labelKm, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.textBoxKm, 3, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(9, 370);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(807, 134);
            this.tableLayoutPanel3.TabIndex = 9;
            // 
            // textBoxBaseDoc
            // 
            this.textBoxBaseDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxBaseDoc.Location = new System.Drawing.Point(164, 3);
            this.textBoxBaseDoc.Name = "textBoxBaseDoc";
            this.textBoxBaseDoc.Size = new System.Drawing.Size(232, 26);
            this.textBoxBaseDoc.TabIndex = 5;
            // 
            // labelBaseDoc
            // 
            this.labelBaseDoc.AutoSize = true;
            this.labelBaseDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelBaseDoc.Location = new System.Drawing.Point(3, 0);
            this.labelBaseDoc.Name = "labelBaseDoc";
            this.labelBaseDoc.Size = new System.Drawing.Size(64, 20);
            this.labelBaseDoc.TabIndex = 24;
            this.labelBaseDoc.Text = "Заявка";
            // 
            // labelFuel
            // 
            this.labelFuel.AutoSize = true;
            this.labelFuel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelFuel.Location = new System.Drawing.Point(402, 67);
            this.labelFuel.Name = "labelFuel";
            this.labelFuel.Size = new System.Drawing.Size(97, 40);
            this.labelFuel.TabIndex = 11;
            this.labelFuel.Text = "Затрачено топлива(л):";
            // 
            // textBoxFuel
            // 
            this.textBoxFuel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxFuel.Location = new System.Drawing.Point(527, 70);
            this.textBoxFuel.Name = "textBoxFuel";
            this.textBoxFuel.Size = new System.Drawing.Size(109, 26);
            this.textBoxFuel.TabIndex = 8;
            // 
            // labelKm
            // 
            this.labelKm.AutoSize = true;
            this.labelKm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelKm.Location = new System.Drawing.Point(402, 0);
            this.labelKm.Name = "labelKm";
            this.labelKm.Size = new System.Drawing.Size(119, 20);
            this.labelKm.TabIndex = 9;
            this.labelKm.Text = "Пройдено(км):";
            // 
            // textBoxKm
            // 
            this.textBoxKm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxKm.Location = new System.Drawing.Point(527, 3);
            this.textBoxKm.Name = "textBoxKm";
            this.textBoxKm.Size = new System.Drawing.Size(109, 26);
            this.textBoxKm.TabIndex = 6;
            // 
            // textBoxNotes
            // 
            this.textBoxNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxNotes.Location = new System.Drawing.Point(172, 504);
            this.textBoxNotes.Multiline = true;
            this.textBoxNotes.Name = "textBoxNotes";
            this.textBoxNotes.Size = new System.Drawing.Size(290, 88);
            this.textBoxNotes.TabIndex = 9;
            // 
            // labelNotes
            // 
            this.labelNotes.AutoSize = true;
            this.labelNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelNotes.Location = new System.Drawing.Point(12, 504);
            this.labelNotes.Name = "labelNotes";
            this.labelNotes.Size = new System.Drawing.Size(75, 20);
            this.labelNotes.TabIndex = 13;
            this.labelNotes.Text = "Заметки";
            // 
            // btnCreateWayBill
            // 
            this.btnCreateWayBill.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCreateWayBill.Location = new System.Drawing.Point(541, 555);
            this.btnCreateWayBill.Name = "btnCreateWayBill";
            this.btnCreateWayBill.Size = new System.Drawing.Size(124, 37);
            this.btnCreateWayBill.TabIndex = 10;
            this.btnCreateWayBill.Text = "Добавить";
            this.btnCreateWayBill.UseVisualStyleBackColor = true;
            this.btnCreateWayBill.Click += new System.EventHandler(this.btnCreateWayBill_Click);
            // 
            // textBoxTrip
            // 
            this.textBoxTrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxTrip.Location = new System.Drawing.Point(172, 78);
            this.textBoxTrip.Multiline = true;
            this.textBoxTrip.Name = "textBoxTrip";
            this.textBoxTrip.ReadOnly = true;
            this.textBoxTrip.Size = new System.Drawing.Size(362, 77);
            this.textBoxTrip.TabIndex = 17;
            this.textBoxTrip.TabStop = false;
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
            this.AddClient.Click += new System.EventHandler(this.AddClient_Click);
            // 
            // SelectTrip
            // 
            this.SelectTrip.BackColor = System.Drawing.Color.Transparent;
            this.SelectTrip.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.SelectTrip.FlatAppearance.BorderSize = 0;
            this.SelectTrip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectTrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SelectTrip.Image = ((System.Drawing.Image)(resources.GetObject("SelectTrip.Image")));
            this.SelectTrip.Location = new System.Drawing.Point(609, 78);
            this.SelectTrip.Name = "SelectTrip";
            this.SelectTrip.Size = new System.Drawing.Size(30, 30);
            this.SelectTrip.TabIndex = 21;
            this.SelectTrip.UseVisualStyleBackColor = false;
            this.SelectTrip.Click += new System.EventHandler(this.SelectTrip_Click);
            // 
            // textBoxClient
            // 
            this.textBoxClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxClient.Location = new System.Drawing.Point(172, 22);
            this.textBoxClient.Name = "textBoxClient";
            this.textBoxClient.ReadOnly = true;
            this.textBoxClient.Size = new System.Drawing.Size(362, 26);
            this.textBoxClient.TabIndex = 1;
            this.textBoxClient.TabStop = false;
            // 
            // AddTrip
            // 
            this.AddTrip.BackColor = System.Drawing.Color.Transparent;
            this.AddTrip.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AddTrip.FlatAppearance.BorderSize = 0;
            this.AddTrip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddTrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddTrip.Image = ((System.Drawing.Image)(resources.GetObject("AddTrip.Image")));
            this.AddTrip.Location = new System.Drawing.Point(573, 78);
            this.AddTrip.Name = "AddTrip";
            this.AddTrip.Size = new System.Drawing.Size(30, 30);
            this.AddTrip.TabIndex = 22;
            this.AddTrip.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AddTrip.UseVisualStyleBackColor = false;
            this.AddTrip.Click += new System.EventHandler(this.AddTrip_Click);
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
            this.SelectClient.Click += new System.EventHandler(this.SelectClient_Click);
            // 
            // FormAddWayBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 598);
            this.Controls.Add(this.textBoxNotes);
            this.Controls.Add(this.SelectClient);
            this.Controls.Add(this.AddTrip);
            this.Controls.Add(this.SelectTrip);
            this.Controls.Add(this.AddClient);
            this.Controls.Add(this.textBoxClient);
            this.Controls.Add(this.labelNotes);
            this.Controls.Add(this.comboBoxDriver);
            this.Controls.Add(this.btnCreateWayBill);
            this.Controls.Add(this.labelCar);
            this.Controls.Add(this.labelDriver);
            this.Controls.Add(this.labelClient);
            this.Controls.Add(this.comboBoxCar);
            this.Controls.Add(this.labelTrip);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.textBoxTrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAddWayBill";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление маршрута";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAddWayBill_FormClosing);
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnCreateWayBill;
        public System.Windows.Forms.ComboBox comboBoxCar;
        public System.Windows.Forms.ComboBox comboBoxDriver;
        private System.Windows.Forms.Label labelFuel;
        private System.Windows.Forms.Label labelKm;
        private System.Windows.Forms.Button AddClient;
        private System.Windows.Forms.Button SelectTrip;
        private System.Windows.Forms.Button AddTrip;
        private System.Windows.Forms.Button SelectClient;
        public System.Windows.Forms.TextBox textBoxTrip;
        public System.Windows.Forms.TextBox textBoxClient;
        private System.Windows.Forms.Label labelNotes;
        private System.Windows.Forms.Label labelBaseDoc;
        public System.Windows.Forms.TextBox txtBoxCost;
        public System.Windows.Forms.DateTimePicker pickerStart;
        public System.Windows.Forms.DateTimePicker pickerEnd;
        public System.Windows.Forms.TextBox textBoxFuel;
        public System.Windows.Forms.TextBox textBoxKm;
        public System.Windows.Forms.TextBox textBoxNotes;
        public System.Windows.Forms.TextBox textBoxBaseDoc;
    }
}