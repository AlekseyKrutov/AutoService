namespace AutoService
{
    partial class FormAddSparePart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddSparePart));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBoxAuto = new System.Windows.Forms.ComboBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelCost = new System.Windows.Forms.Label();
            this.labelCar = new System.Windows.Forms.Label();
            this.textBoxUniqNumb = new System.Windows.Forms.TextBox();
            this.textBoxDescr = new System.Windows.Forms.TextBox();
            this.textBoxCost = new System.Windows.Forms.TextBox();
            this.labelNumb = new System.Windows.Forms.Label();
            this.textBoxNumb = new System.Windows.Forms.TextBox();
            this.checkBoxShowAuto = new System.Windows.Forms.CheckBox();
            this.labelUniqCode = new System.Windows.Forms.Label();
            this.buttonAddSparePart = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.comboBoxAuto, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelDescription, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelCost, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelCar, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBoxUniqNumb, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxDescr, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxCost, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelNumb, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.textBoxNumb, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxShowAuto, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelUniqCode, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.8056F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.80559F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.80559F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.38881F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.38881F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.80559F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(337, 293);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // comboBoxAuto
            // 
            this.comboBoxAuto.FormattingEnabled = true;
            this.comboBoxAuto.Location = new System.Drawing.Point(171, 177);
            this.comboBoxAuto.Name = "comboBoxAuto";
            this.comboBoxAuto.Size = new System.Drawing.Size(163, 21);
            this.comboBoxAuto.TabIndex = 2;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(3, 58);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(83, 13);
            this.labelDescription.TabIndex = 1;
            this.labelDescription.Text = "Наименование";
            // 
            // labelCost
            // 
            this.labelCost.AutoSize = true;
            this.labelCost.Location = new System.Drawing.Point(3, 116);
            this.labelCost.Name = "labelCost";
            this.labelCost.Size = new System.Drawing.Size(62, 13);
            this.labelCost.TabIndex = 2;
            this.labelCost.Text = "Стоимость";
            // 
            // labelCar
            // 
            this.labelCar.AutoSize = true;
            this.labelCar.Location = new System.Drawing.Point(3, 174);
            this.labelCar.Name = "labelCar";
            this.labelCar.Size = new System.Drawing.Size(69, 13);
            this.labelCar.TabIndex = 3;
            this.labelCar.Text = "Автомобиль";
            // 
            // textBoxUniqNumb
            // 
            this.textBoxUniqNumb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxUniqNumb.Location = new System.Drawing.Point(171, 3);
            this.textBoxUniqNumb.Multiline = true;
            this.textBoxUniqNumb.Name = "textBoxUniqNumb";
            this.textBoxUniqNumb.Size = new System.Drawing.Size(163, 30);
            this.textBoxUniqNumb.TabIndex = 5;
            // 
            // textBoxDescr
            // 
            this.textBoxDescr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxDescr.Location = new System.Drawing.Point(171, 61);
            this.textBoxDescr.Multiline = true;
            this.textBoxDescr.Name = "textBoxDescr";
            this.textBoxDescr.Size = new System.Drawing.Size(163, 30);
            this.textBoxDescr.TabIndex = 6;
            this.textBoxDescr.Enter += new System.EventHandler(this.textBoxDescr_Enter);
            this.textBoxDescr.Leave += new System.EventHandler(this.textBoxDescr_Leave);
            // 
            // textBoxCost
            // 
            this.textBoxCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxCost.Location = new System.Drawing.Point(171, 119);
            this.textBoxCost.Multiline = true;
            this.textBoxCost.Name = "textBoxCost";
            this.textBoxCost.Size = new System.Drawing.Size(163, 30);
            this.textBoxCost.TabIndex = 7;
            // 
            // labelNumb
            // 
            this.labelNumb.AutoSize = true;
            this.labelNumb.Location = new System.Drawing.Point(3, 234);
            this.labelNumb.Name = "labelNumb";
            this.labelNumb.Size = new System.Drawing.Size(66, 13);
            this.labelNumb.TabIndex = 4;
            this.labelNumb.Text = "Количество";
            // 
            // textBoxNumb
            // 
            this.textBoxNumb.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxNumb.Location = new System.Drawing.Point(171, 237);
            this.textBoxNumb.Multiline = true;
            this.textBoxNumb.Name = "textBoxNumb";
            this.textBoxNumb.Size = new System.Drawing.Size(163, 29);
            this.textBoxNumb.TabIndex = 9;
            // 
            // checkBoxShowAuto
            // 
            this.checkBoxShowAuto.AutoSize = true;
            this.checkBoxShowAuto.Location = new System.Drawing.Point(171, 207);
            this.checkBoxShowAuto.Name = "checkBoxShowAuto";
            this.checkBoxShowAuto.Size = new System.Drawing.Size(71, 17);
            this.checkBoxShowAuto.TabIndex = 11;
            this.checkBoxShowAuto.Text = "Без авто";
            this.checkBoxShowAuto.UseVisualStyleBackColor = true;
            this.checkBoxShowAuto.CheckedChanged += new System.EventHandler(this.checkBoxShowAuto_CheckedChanged);
            // 
            // labelUniqCode
            // 
            this.labelUniqCode.AutoSize = true;
            this.labelUniqCode.Location = new System.Drawing.Point(3, 0);
            this.labelUniqCode.Name = "labelUniqCode";
            this.labelUniqCode.Size = new System.Drawing.Size(48, 13);
            this.labelUniqCode.TabIndex = 0;
            this.labelUniqCode.Text = "Артикул";
            // 
            // buttonAddSparePart
            // 
            this.buttonAddSparePart.Location = new System.Drawing.Point(176, 311);
            this.buttonAddSparePart.Name = "buttonAddSparePart";
            this.buttonAddSparePart.Size = new System.Drawing.Size(120, 32);
            this.buttonAddSparePart.TabIndex = 1;
            this.buttonAddSparePart.Text = "Добавить";
            this.buttonAddSparePart.UseVisualStyleBackColor = true;
            this.buttonAddSparePart.Click += new System.EventHandler(this.buttonAddSparePart_Click);
            // 
            // FormAddSparePart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 344);
            this.Controls.Add(this.buttonAddSparePart);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAddSparePart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление запчастей";
            this.Load += new System.EventHandler(this.FormAddSparePart_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelUniqCode;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label labelCost;
        private System.Windows.Forms.Label labelCar;
        private System.Windows.Forms.Label labelNumb;
        private System.Windows.Forms.TextBox textBoxUniqNumb;
        private System.Windows.Forms.TextBox textBoxCost;
        private System.Windows.Forms.TextBox textBoxNumb;
        private System.Windows.Forms.CheckBox checkBoxShowAuto;
        private System.Windows.Forms.Button buttonAddSparePart;
        private System.Windows.Forms.TextBox textBoxDescr;
        private System.Windows.Forms.ComboBox comboBoxAuto;
    }
}