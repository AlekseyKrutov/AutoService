namespace AutoService
{
    partial class FormAddPersonal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddPersonal));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.textBoxINN = new System.Windows.Forms.TextBox();
            this.labelAddress = new System.Windows.Forms.Label();
            this.labelModel = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxFunctions = new System.Windows.Forms.TextBox();
            this.labelNumberOfTel = new System.Windows.Forms.Label();
            this.labelFunctions = new System.Windows.Forms.Label();
            this.textBoxNumbOfTel = new System.Windows.Forms.MaskedTextBox();
            this.buttonAddPersonal = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.00227F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.99773F));
            this.tableLayoutPanel1.Controls.Add(this.textBoxAddress, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxINN, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelAddress, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelModel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxFunctions, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelNumberOfTel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelFunctions, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBoxNumbOfTel, 1, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(-1, 9);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(371, 173);
            this.tableLayoutPanel1.TabIndex = 25;
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.Location = new System.Drawing.Point(147, 71);
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(221, 20);
            this.textBoxAddress.TabIndex = 18;
            // 
            // textBoxINN
            // 
            this.textBoxINN.Location = new System.Drawing.Point(147, 37);
            this.textBoxINN.Name = "textBoxINN";
            this.textBoxINN.Size = new System.Drawing.Size(221, 20);
            this.textBoxINN.TabIndex = 17;
            this.textBoxINN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxINN_KeyPress);
            // 
            // labelAddress
            // 
            this.labelAddress.AutoSize = true;
            this.labelAddress.Location = new System.Drawing.Point(3, 68);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(38, 13);
            this.labelAddress.TabIndex = 5;
            this.labelAddress.Text = "Адрес";
            // 
            // labelModel
            // 
            this.labelModel.AutoSize = true;
            this.labelModel.Location = new System.Drawing.Point(3, 34);
            this.labelModel.Name = "labelModel";
            this.labelModel.Size = new System.Drawing.Size(31, 13);
            this.labelModel.TabIndex = 2;
            this.labelModel.Text = "ИНН";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(147, 3);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(221, 20);
            this.textBoxName.TabIndex = 16;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(3, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(40, 13);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Ф.И.О";
            // 
            // textBoxFunctions
            // 
            this.textBoxFunctions.Location = new System.Drawing.Point(147, 105);
            this.textBoxFunctions.Name = "textBoxFunctions";
            this.textBoxFunctions.Size = new System.Drawing.Size(221, 20);
            this.textBoxFunctions.TabIndex = 22;
            // 
            // labelNumberOfTel
            // 
            this.labelNumberOfTel.AutoSize = true;
            this.labelNumberOfTel.Location = new System.Drawing.Point(3, 136);
            this.labelNumberOfTel.Name = "labelNumberOfTel";
            this.labelNumberOfTel.Size = new System.Drawing.Size(93, 13);
            this.labelNumberOfTel.TabIndex = 19;
            this.labelNumberOfTel.Text = "Номер телефона";
            // 
            // labelFunctions
            // 
            this.labelFunctions.AutoSize = true;
            this.labelFunctions.Location = new System.Drawing.Point(3, 102);
            this.labelFunctions.Name = "labelFunctions";
            this.labelFunctions.Size = new System.Drawing.Size(65, 13);
            this.labelFunctions.TabIndex = 21;
            this.labelFunctions.Text = "Должность";
            // 
            // textBoxNumbOfTel
            // 
            this.textBoxNumbOfTel.Location = new System.Drawing.Point(147, 139);
            this.textBoxNumbOfTel.Mask = "(000) 000-0000";
            this.textBoxNumbOfTel.Name = "textBoxNumbOfTel";
            this.textBoxNumbOfTel.ResetOnPrompt = false;
            this.textBoxNumbOfTel.ResetOnSpace = false;
            this.textBoxNumbOfTel.Size = new System.Drawing.Size(221, 20);
            this.textBoxNumbOfTel.TabIndex = 23;
            // 
            // buttonAddPersonal
            // 
            this.buttonAddPersonal.Location = new System.Drawing.Point(262, 193);
            this.buttonAddPersonal.Name = "buttonAddPersonal";
            this.buttonAddPersonal.Size = new System.Drawing.Size(108, 36);
            this.buttonAddPersonal.TabIndex = 24;
            this.buttonAddPersonal.Text = "Добавить";
            this.buttonAddPersonal.UseVisualStyleBackColor = true;
            this.buttonAddPersonal.Click += new System.EventHandler(this.buttonAddPersonal_Click);
            // 
            // FormAddPersonal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 228);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.buttonAddPersonal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormAddPersonal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление сотрудника";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.TextBox textBoxINN;
        private System.Windows.Forms.Label labelAddress;
        private System.Windows.Forms.Label labelModel;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxFunctions;
        private System.Windows.Forms.Label labelNumberOfTel;
        private System.Windows.Forms.Label labelFunctions;
        public System.Windows.Forms.Button buttonAddPersonal;
        private System.Windows.Forms.MaskedTextBox textBoxNumbOfTel;
    }
}