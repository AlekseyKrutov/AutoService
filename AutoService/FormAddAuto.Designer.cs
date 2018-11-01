namespace AutoService
{
    partial class FormAddAuto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddAuto));
            this.buttonAddAuto = new System.Windows.Forms.Button();
            this.labelMark = new System.Windows.Forms.Label();
            this.labelVIN = new System.Windows.Forms.Label();
            this.labelReg = new System.Windows.Forms.Label();
            this.labelGosNumb = new System.Windows.Forms.Label();
            this.label1Owner = new System.Windows.Forms.Label();
            this.textBoxVIN = new System.Windows.Forms.TextBox();
            this.textBoxReg = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBoxAuto = new System.Windows.Forms.ComboBox();
            this.textBoxGosNumb = new System.Windows.Forms.MaskedTextBox();
            this.labelContentOwner = new System.Windows.Forms.Label();
            this.buttonAddOwner = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonAddAuto
            // 
            this.buttonAddAuto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAddAuto.Location = new System.Drawing.Point(275, 309);
            this.buttonAddAuto.Name = "buttonAddAuto";
            this.buttonAddAuto.Size = new System.Drawing.Size(109, 36);
            this.buttonAddAuto.TabIndex = 12;
            this.buttonAddAuto.Text = "Добавить";
            this.buttonAddAuto.UseVisualStyleBackColor = true;
            this.buttonAddAuto.Click += new System.EventHandler(this.buttonAddAuto_Click);
            // 
            // labelMark
            // 
            this.labelMark.AutoSize = true;
            this.labelMark.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelMark.Location = new System.Drawing.Point(3, 0);
            this.labelMark.Name = "labelMark";
            this.labelMark.Size = new System.Drawing.Size(112, 16);
            this.labelMark.TabIndex = 0;
            this.labelMark.Text = "Марка и модель";
            // 
            // labelVIN
            // 
            this.labelVIN.AutoSize = true;
            this.labelVIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelVIN.Location = new System.Drawing.Point(3, 60);
            this.labelVIN.Name = "labelVIN";
            this.labelVIN.Size = new System.Drawing.Size(87, 32);
            this.labelVIN.TabIndex = 5;
            this.labelVIN.Text = "VIN номер автомобиля";
            // 
            // labelReg
            // 
            this.labelReg.AutoSize = true;
            this.labelReg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelReg.Location = new System.Drawing.Point(3, 120);
            this.labelReg.Name = "labelReg";
            this.labelReg.Size = new System.Drawing.Size(123, 32);
            this.labelReg.TabIndex = 7;
            this.labelReg.Text = "Свидетельство о регистрации";
            // 
            // labelGosNumb
            // 
            this.labelGosNumb.AutoSize = true;
            this.labelGosNumb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelGosNumb.Location = new System.Drawing.Point(3, 180);
            this.labelGosNumb.Name = "labelGosNumb";
            this.labelGosNumb.Size = new System.Drawing.Size(77, 16);
            this.labelGosNumb.TabIndex = 9;
            this.labelGosNumb.Text = "Гос. номер";
            // 
            // label1Owner
            // 
            this.label1Owner.AutoSize = true;
            this.label1Owner.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1Owner.Location = new System.Drawing.Point(7, 255);
            this.label1Owner.Name = "label1Owner";
            this.label1Owner.Size = new System.Drawing.Size(73, 16);
            this.label1Owner.TabIndex = 10;
            this.label1Owner.Text = "Владелец";
            // 
            // textBoxVIN
            // 
            this.textBoxVIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxVIN.Location = new System.Drawing.Point(163, 63);
            this.textBoxVIN.MaxLength = 17;
            this.textBoxVIN.Multiline = true;
            this.textBoxVIN.Name = "textBoxVIN";
            this.textBoxVIN.Size = new System.Drawing.Size(214, 30);
            this.textBoxVIN.TabIndex = 18;
            this.textBoxVIN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxVIN_KeyPress);
            // 
            // textBoxReg
            // 
            this.textBoxReg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxReg.Location = new System.Drawing.Point(163, 123);
            this.textBoxReg.MaxLength = 10;
            this.textBoxReg.Multiline = true;
            this.textBoxReg.Name = "textBoxReg";
            this.textBoxReg.Size = new System.Drawing.Size(163, 30);
            this.textBoxReg.TabIndex = 19;
            this.textBoxReg.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxReg_KeyPress);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.labelMark, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxAuto, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelVIN, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxVIN, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelReg, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxReg, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelGosNumb, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBoxGosNumb, 1, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(380, 240);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // comboBoxAuto
            // 
            this.comboBoxAuto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAuto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxAuto.FormattingEnabled = true;
            this.comboBoxAuto.Location = new System.Drawing.Point(163, 3);
            this.comboBoxAuto.Name = "comboBoxAuto";
            this.comboBoxAuto.Size = new System.Drawing.Size(214, 28);
            this.comboBoxAuto.TabIndex = 21;
            // 
            // textBoxGosNumb
            // 
            this.textBoxGosNumb.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxGosNumb.Location = new System.Drawing.Point(163, 183);
            this.textBoxGosNumb.Mask = "L000LL000";
            this.textBoxGosNumb.Name = "textBoxGosNumb";
            this.textBoxGosNumb.ResetOnSpace = false;
            this.textBoxGosNumb.Size = new System.Drawing.Size(163, 29);
            this.textBoxGosNumb.TabIndex = 20;
            this.textBoxGosNumb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxGosNumb_KeyPress_1);
            // 
            // labelContentOwner
            // 
            this.labelContentOwner.AutoSize = true;
            this.labelContentOwner.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelContentOwner.Location = new System.Drawing.Point(164, 257);
            this.labelContentOwner.Name = "labelContentOwner";
            this.labelContentOwner.Size = new System.Drawing.Size(114, 13);
            this.labelContentOwner.TabIndex = 14;
            this.labelContentOwner.Text = "Выберите владельца";
            // 
            // buttonAddOwner
            // 
            this.buttonAddOwner.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAddOwner.Location = new System.Drawing.Point(167, 276);
            this.buttonAddOwner.Name = "buttonAddOwner";
            this.buttonAddOwner.Size = new System.Drawing.Size(75, 23);
            this.buttonAddOwner.TabIndex = 15;
            this.buttonAddOwner.Text = "Выбрать...";
            this.buttonAddOwner.UseVisualStyleBackColor = true;
            this.buttonAddOwner.Click += new System.EventHandler(this.buttonAddOwner_Click);
            // 
            // FormAddAuto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 357);
            this.Controls.Add(this.buttonAddOwner);
            this.Controls.Add(this.labelContentOwner);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.buttonAddAuto);
            this.Controls.Add(this.label1Owner);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormAddAuto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Новый автомобиль";
            this.Load += new System.EventHandler(this.FormAddAuto_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAddAuto;
        private System.Windows.Forms.Label labelMark;
        private System.Windows.Forms.Label labelVIN;
        private System.Windows.Forms.Label labelReg;
        private System.Windows.Forms.Label labelGosNumb;
        private System.Windows.Forms.Label label1Owner;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.Label labelContentOwner;
        public System.Windows.Forms.TextBox textBoxVIN;
        public System.Windows.Forms.TextBox textBoxReg;
        public System.Windows.Forms.Button buttonAddOwner;
        public System.Windows.Forms.MaskedTextBox textBoxGosNumb;
        public System.Windows.Forms.ComboBox comboBoxAuto;
    }
}