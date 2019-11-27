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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddCar = new System.Windows.Forms.Button();
            this.comboBoxCar = new System.Windows.Forms.ComboBox();
            this.comboBoxClient = new System.Windows.Forms.ComboBox();
            this.labelCarOwnerTxt = new System.Windows.Forms.Label();
            this.labelDriver = new System.Windows.Forms.Label();
            this.labelRoute = new System.Windows.Forms.Label();
            this.labelClient = new System.Windows.Forms.Label();
            this.comboBoxRoute = new System.Windows.Forms.ComboBox();
            this.labelCar = new System.Windows.Forms.Label();
            this.btnAddDriver = new System.Windows.Forms.Button();
            this.labelCarOwner = new System.Windows.Forms.Label();
            this.btnAddClient = new System.Windows.Forms.Button();
            this.btnAddRoute = new System.Windows.Forms.Button();
            this.comboBoxDriver = new System.Windows.Forms.ComboBox();
            this.labelCost = new System.Windows.Forms.Label();
            this.labelLdUnLd = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtBoxUnloadDate = new System.Windows.Forms.MaskedTextBox();
            this.txtBoxLoadDate = new System.Windows.Forms.MaskedTextBox();
            this.txtBoxCost = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCreateWayBill = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.88235F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tableLayoutPanel1.Controls.Add(this.btnAddCar, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelCarOwnerTxt, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelDriver, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelRoute, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelClient, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelCar, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnAddDriver, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelCarOwner, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnAddClient, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnAddRoute, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxDriver, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxCar, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxClient, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxRoute, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(850, 380);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnAddCar
            // 
            this.btnAddCar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddCar.Location = new System.Drawing.Point(716, 155);
            this.btnAddCar.Name = "btnAddCar";
            this.btnAddCar.Size = new System.Drawing.Size(124, 37);
            this.btnAddCar.TabIndex = 15;
            this.btnAddCar.Text = "Добавить";
            this.btnAddCar.UseVisualStyleBackColor = true;
            this.btnAddCar.Click += new System.EventHandler(this.btnAddCar_Click);
            // 
            // comboBoxCar
            // 
            this.comboBoxCar.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxCar.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxCar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxCar.FormattingEnabled = true;
            this.comboBoxCar.ItemHeight = 20;
            this.comboBoxCar.Location = new System.Drawing.Point(224, 155);
            this.comboBoxCar.MaxDropDownItems = 10;
            this.comboBoxCar.Name = "comboBoxCar";
            this.comboBoxCar.Size = new System.Drawing.Size(441, 28);
            this.comboBoxCar.TabIndex = 13;
            this.comboBoxCar.SelectedIndexChanged += new System.EventHandler(this.comboBoxCar_SelectedIndexChanged);
            this.comboBoxCar.TextChanged += new System.EventHandler(this.comboBoxCar_TextChanged);
            // 
            // comboBoxClient
            // 
            this.comboBoxClient.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxClient.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxClient.FormattingEnabled = true;
            this.comboBoxClient.ItemHeight = 20;
            this.comboBoxClient.Location = new System.Drawing.Point(224, 79);
            this.comboBoxClient.MaxDropDownItems = 10;
            this.comboBoxClient.Name = "comboBoxClient";
            this.comboBoxClient.Size = new System.Drawing.Size(441, 28);
            this.comboBoxClient.TabIndex = 12;
            // 
            // labelCarOwnerTxt
            // 
            this.labelCarOwnerTxt.AutoSize = true;
            this.labelCarOwnerTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCarOwnerTxt.Location = new System.Drawing.Point(224, 228);
            this.labelCarOwnerTxt.Name = "labelCarOwnerTxt";
            this.labelCarOwnerTxt.Size = new System.Drawing.Size(14, 20);
            this.labelCarOwnerTxt.TabIndex = 11;
            this.labelCarOwnerTxt.Text = "-";
            // 
            // labelDriver
            // 
            this.labelDriver.AutoSize = true;
            this.labelDriver.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDriver.Location = new System.Drawing.Point(3, 304);
            this.labelDriver.Name = "labelDriver";
            this.labelDriver.Size = new System.Drawing.Size(86, 20);
            this.labelDriver.TabIndex = 3;
            this.labelDriver.Text = "Водитель";
            // 
            // labelRoute
            // 
            this.labelRoute.AutoSize = true;
            this.labelRoute.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRoute.Location = new System.Drawing.Point(3, 0);
            this.labelRoute.Name = "labelRoute";
            this.labelRoute.Size = new System.Drawing.Size(78, 20);
            this.labelRoute.TabIndex = 0;
            this.labelRoute.Text = "Маршрут";
            // 
            // labelClient
            // 
            this.labelClient.AutoSize = true;
            this.labelClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelClient.Location = new System.Drawing.Point(3, 76);
            this.labelClient.Name = "labelClient";
            this.labelClient.Size = new System.Drawing.Size(65, 20);
            this.labelClient.TabIndex = 1;
            this.labelClient.Text = "Клиент";
            // 
            // comboBoxRoute
            // 
            this.comboBoxRoute.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxRoute.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxRoute.DropDownHeight = 100;
            this.comboBoxRoute.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxRoute.FormattingEnabled = true;
            this.comboBoxRoute.IntegralHeight = false;
            this.comboBoxRoute.ItemHeight = 20;
            this.comboBoxRoute.Location = new System.Drawing.Point(224, 3);
            this.comboBoxRoute.MaxDropDownItems = 10;
            this.comboBoxRoute.Name = "comboBoxRoute";
            this.comboBoxRoute.Size = new System.Drawing.Size(441, 28);
            this.comboBoxRoute.TabIndex = 9;
            // 
            // labelCar
            // 
            this.labelCar.AutoSize = true;
            this.labelCar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCar.Location = new System.Drawing.Point(3, 152);
            this.labelCar.Name = "labelCar";
            this.labelCar.Size = new System.Drawing.Size(104, 20);
            this.labelCar.TabIndex = 5;
            this.labelCar.Text = "Автомобиль";
            // 
            // btnAddDriver
            // 
            this.btnAddDriver.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddDriver.Location = new System.Drawing.Point(716, 307);
            this.btnAddDriver.Name = "btnAddDriver";
            this.btnAddDriver.Size = new System.Drawing.Size(124, 37);
            this.btnAddDriver.TabIndex = 16;
            this.btnAddDriver.Text = "Добавить";
            this.btnAddDriver.UseVisualStyleBackColor = true;
            this.btnAddDriver.Click += new System.EventHandler(this.btnAddDriver_Click);
            // 
            // labelCarOwner
            // 
            this.labelCarOwner.AutoSize = true;
            this.labelCarOwner.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCarOwner.Location = new System.Drawing.Point(3, 228);
            this.labelCarOwner.Name = "labelCarOwner";
            this.labelCarOwner.Size = new System.Drawing.Size(184, 20);
            this.labelCarOwner.TabIndex = 2;
            this.labelCarOwner.Text = "Владелец автомобиля";
            // 
            // btnAddClient
            // 
            this.btnAddClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddClient.Location = new System.Drawing.Point(716, 79);
            this.btnAddClient.Name = "btnAddClient";
            this.btnAddClient.Size = new System.Drawing.Size(124, 37);
            this.btnAddClient.TabIndex = 8;
            this.btnAddClient.Text = "Добавить";
            this.btnAddClient.UseVisualStyleBackColor = true;
            this.btnAddClient.Click += new System.EventHandler(this.btnAddClient_Click);
            // 
            // btnAddRoute
            // 
            this.btnAddRoute.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddRoute.Location = new System.Drawing.Point(716, 3);
            this.btnAddRoute.Name = "btnAddRoute";
            this.btnAddRoute.Size = new System.Drawing.Size(124, 37);
            this.btnAddRoute.TabIndex = 7;
            this.btnAddRoute.Text = "Добавить";
            this.btnAddRoute.UseVisualStyleBackColor = true;
            this.btnAddRoute.Click += new System.EventHandler(this.btnAddRoute_Click);
            // 
            // comboBoxDriver
            // 
            this.comboBoxDriver.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxDriver.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxDriver.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxDriver.FormattingEnabled = true;
            this.comboBoxDriver.ItemHeight = 20;
            this.comboBoxDriver.Location = new System.Drawing.Point(224, 307);
            this.comboBoxDriver.MaxDropDownItems = 10;
            this.comboBoxDriver.Name = "comboBoxDriver";
            this.comboBoxDriver.Size = new System.Drawing.Size(441, 28);
            this.comboBoxDriver.TabIndex = 14;
            // 
            // labelCost
            // 
            this.labelCost.AutoSize = true;
            this.labelCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCost.Location = new System.Drawing.Point(3, 0);
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
            this.labelLdUnLd.Size = new System.Drawing.Size(189, 20);
            this.labelLdUnLd.TabIndex = 4;
            this.labelLdUnLd.Text = "Дата загрузки/выгрузки";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.88235F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.11765F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.88235F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.88235F));
            this.tableLayoutPanel2.Controls.Add(this.labelLdUnLd, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtBoxUnloadDate, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtBoxLoadDate, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 395);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(850, 67);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // txtBoxUnloadDate
            // 
            this.txtBoxUnloadDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtBoxUnloadDate.Location = new System.Drawing.Point(394, 3);
            this.txtBoxUnloadDate.Mask = "00/00/0000 90:00";
            this.txtBoxUnloadDate.Name = "txtBoxUnloadDate";
            this.txtBoxUnloadDate.Size = new System.Drawing.Size(138, 26);
            this.txtBoxUnloadDate.TabIndex = 11;
            this.txtBoxUnloadDate.ValidatingType = typeof(System.DateTime);
            // 
            // txtBoxLoadDate
            // 
            this.txtBoxLoadDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtBoxLoadDate.Location = new System.Drawing.Point(223, 3);
            this.txtBoxLoadDate.Mask = "00/00/0000 90:00";
            this.txtBoxLoadDate.Name = "txtBoxLoadDate";
            this.txtBoxLoadDate.Size = new System.Drawing.Size(138, 26);
            this.txtBoxLoadDate.TabIndex = 9;
            this.txtBoxLoadDate.ValidatingType = typeof(System.DateTime);
            // 
            // txtBoxCost
            // 
            this.txtBoxCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtBoxCost.Location = new System.Drawing.Point(223, 3);
            this.txtBoxCost.Name = "txtBoxCost";
            this.txtBoxCost.Size = new System.Drawing.Size(183, 26);
            this.txtBoxCost.TabIndex = 8;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.88235F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.23529F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.76471F));
            this.tableLayoutPanel3.Controls.Add(this.labelCost, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtBoxCost, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(12, 465);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(850, 67);
            this.tableLayoutPanel3.TabIndex = 9;
            // 
            // btnCreateWayBill
            // 
            this.btnCreateWayBill.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCreateWayBill.Location = new System.Drawing.Point(728, 538);
            this.btnCreateWayBill.Name = "btnCreateWayBill";
            this.btnCreateWayBill.Size = new System.Drawing.Size(124, 37);
            this.btnCreateWayBill.TabIndex = 10;
            this.btnCreateWayBill.Text = "Создать";
            this.btnCreateWayBill.UseVisualStyleBackColor = true;
            this.btnCreateWayBill.Click += new System.EventHandler(this.btnCreateWayBill_Click);
            // 
            // FormAddWayBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 587);
            this.Controls.Add(this.btnCreateWayBill);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAddWayBill";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление маршрута";
            this.Load += new System.EventHandler(this.FormAddWayBill_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelRoute;
        private System.Windows.Forms.Label labelCarOwnerTxt;
        private System.Windows.Forms.Label labelClient;
        private System.Windows.Forms.Label labelCarOwner;
        private System.Windows.Forms.Button btnAddRoute;
        private System.Windows.Forms.Button btnAddClient;
        private System.Windows.Forms.Label labelCost;
        private System.Windows.Forms.Label labelLdUnLd;
        private System.Windows.Forms.Label labelDriver;
        private System.Windows.Forms.Label labelCar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox txtBoxCost;
        private System.Windows.Forms.MaskedTextBox txtBoxLoadDate;
        private System.Windows.Forms.MaskedTextBox txtBoxUnloadDate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnCreateWayBill;
        private System.Windows.Forms.Button btnAddCar;
        private System.Windows.Forms.Button btnAddDriver;
        public System.Windows.Forms.ComboBox comboBoxRoute;
        public System.Windows.Forms.ComboBox comboBoxCar;
        public System.Windows.Forms.ComboBox comboBoxClient;
        public System.Windows.Forms.ComboBox comboBoxDriver;
    }
}