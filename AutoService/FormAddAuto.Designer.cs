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
            this.textBoxMark = new System.Windows.Forms.TextBox();
            this.labelModel = new System.Windows.Forms.Label();
            this.labelVIN = new System.Windows.Forms.Label();
            this.labelReg = new System.Windows.Forms.Label();
            this.labelGosNumb = new System.Windows.Forms.Label();
            this.label1Owner = new System.Windows.Forms.Label();
            this.textBoxModel = new System.Windows.Forms.TextBox();
            this.textBoxVIN = new System.Windows.Forms.TextBox();
            this.textBoxReg = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxGosNumb = new System.Windows.Forms.MaskedTextBox();
            this.labelContentOwner = new System.Windows.Forms.Label();
            this.buttonAddOwner = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonAddAuto
            // 
            this.buttonAddAuto.Location = new System.Drawing.Point(267, 340);
            this.buttonAddAuto.Name = "buttonAddAuto";
            this.buttonAddAuto.Size = new System.Drawing.Size(108, 36);
            this.buttonAddAuto.TabIndex = 12;
            this.buttonAddAuto.Text = "Добавить";
            this.buttonAddAuto.UseVisualStyleBackColor = true;
            this.buttonAddAuto.Click += new System.EventHandler(this.buttonAddAuto_Click);
            // 
            // labelMark
            // 
            this.labelMark.AutoSize = true;
            this.labelMark.Location = new System.Drawing.Point(3, 0);
            this.labelMark.Name = "labelMark";
            this.labelMark.Size = new System.Drawing.Size(40, 13);
            this.labelMark.TabIndex = 0;
            this.labelMark.Text = "Марка";
            // 
            // textBoxMark
            // 
            this.textBoxMark.Location = new System.Drawing.Point(147, 3);
            this.textBoxMark.Name = "textBoxMark";
            this.textBoxMark.Size = new System.Drawing.Size(221, 20);
            this.textBoxMark.TabIndex = 16;
            // 
            // labelModel
            // 
            this.labelModel.AutoSize = true;
            this.labelModel.Location = new System.Drawing.Point(3, 53);
            this.labelModel.Name = "labelModel";
            this.labelModel.Size = new System.Drawing.Size(46, 13);
            this.labelModel.TabIndex = 2;
            this.labelModel.Text = "Модель";
            // 
            // labelVIN
            // 
            this.labelVIN.AutoSize = true;
            this.labelVIN.Location = new System.Drawing.Point(3, 106);
            this.labelVIN.Name = "labelVIN";
            this.labelVIN.Size = new System.Drawing.Size(124, 13);
            this.labelVIN.TabIndex = 5;
            this.labelVIN.Text = "VIN номер автомобиля";
            // 
            // labelReg
            // 
            this.labelReg.AutoSize = true;
            this.labelReg.Location = new System.Drawing.Point(3, 159);
            this.labelReg.Name = "labelReg";
            this.labelReg.Size = new System.Drawing.Size(96, 26);
            this.labelReg.TabIndex = 7;
            this.labelReg.Text = "Свидетельство о регистрации";
            // 
            // labelGosNumb
            // 
            this.labelGosNumb.AutoSize = true;
            this.labelGosNumb.Location = new System.Drawing.Point(3, 212);
            this.labelGosNumb.Name = "labelGosNumb";
            this.labelGosNumb.Size = new System.Drawing.Size(63, 13);
            this.labelGosNumb.TabIndex = 9;
            this.labelGosNumb.Text = "Гос. номер";
            // 
            // label1Owner
            // 
            this.label1Owner.AutoSize = true;
            this.label1Owner.Location = new System.Drawing.Point(7, 283);
            this.label1Owner.Name = "label1Owner";
            this.label1Owner.Size = new System.Drawing.Size(56, 13);
            this.label1Owner.TabIndex = 10;
            this.label1Owner.Text = "Владелец";
            // 
            // textBoxModel
            // 
            this.textBoxModel.Location = new System.Drawing.Point(147, 56);
            this.textBoxModel.Name = "textBoxModel";
            this.textBoxModel.Size = new System.Drawing.Size(221, 20);
            this.textBoxModel.TabIndex = 17;
            // 
            // textBoxVIN
            // 
            this.textBoxVIN.Location = new System.Drawing.Point(147, 109);
            this.textBoxVIN.Name = "textBoxVIN";
            this.textBoxVIN.Size = new System.Drawing.Size(221, 20);
            this.textBoxVIN.TabIndex = 18;
            this.textBoxVIN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxVIN_KeyPress);
            // 
            // textBoxReg
            // 
            this.textBoxReg.Location = new System.Drawing.Point(147, 162);
            this.textBoxReg.Name = "textBoxReg";
            this.textBoxReg.Size = new System.Drawing.Size(221, 20);
            this.textBoxReg.TabIndex = 19;
            this.textBoxReg.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxReg_KeyPress);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.00227F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.99773F));
            this.tableLayoutPanel1.Controls.Add(this.textBoxReg, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBoxVIN, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxModel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelGosNumb, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelReg, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelVIN, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelModel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxMark, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelMark, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxGosNumb, 1, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(371, 268);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // textBoxGosNumb
            // 
            this.textBoxGosNumb.Location = new System.Drawing.Point(147, 215);
            this.textBoxGosNumb.Mask = "L000LL000";
            this.textBoxGosNumb.Name = "textBoxGosNumb";
            this.textBoxGosNumb.ResetOnSpace = false;
            this.textBoxGosNumb.Size = new System.Drawing.Size(221, 20);
            this.textBoxGosNumb.TabIndex = 20;
            this.textBoxGosNumb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxGosNumb_KeyPress_1);
            // 
            // labelContentOwner
            // 
            this.labelContentOwner.AutoSize = true;
            this.labelContentOwner.Location = new System.Drawing.Point(151, 287);
            this.labelContentOwner.Name = "labelContentOwner";
            this.labelContentOwner.Size = new System.Drawing.Size(114, 13);
            this.labelContentOwner.TabIndex = 14;
            this.labelContentOwner.Text = "Выберите владельца";
            // 
            // buttonAddOwner
            // 
            this.buttonAddOwner.Location = new System.Drawing.Point(151, 304);
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
            this.ClientSize = new System.Drawing.Size(379, 380);
            this.Controls.Add(this.buttonAddOwner);
            this.Controls.Add(this.labelContentOwner);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.buttonAddAuto);
            this.Controls.Add(this.label1Owner);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAddAuto";
            this.Text = "Новый автомобиль";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAddAuto;
        private System.Windows.Forms.Label labelMark;
        private System.Windows.Forms.TextBox textBoxMark;
        private System.Windows.Forms.Label labelModel;
        private System.Windows.Forms.Label labelVIN;
        private System.Windows.Forms.Label labelReg;
        private System.Windows.Forms.Label labelGosNumb;
        private System.Windows.Forms.Label label1Owner;
        private System.Windows.Forms.TextBox textBoxModel;
        private System.Windows.Forms.TextBox textBoxVIN;
        private System.Windows.Forms.TextBox textBoxReg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonAddOwner;
        public System.Windows.Forms.Label labelContentOwner;
        private System.Windows.Forms.MaskedTextBox textBoxGosNumb;

    }
}