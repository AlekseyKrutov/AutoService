namespace AutoService
{
    partial class FormAddClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddClient));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelNumberOfTel = new System.Windows.Forms.Label();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.textBoxINN = new System.Windows.Forms.TextBox();
            this.labelAddress = new System.Windows.Forms.Label();
            this.labelModel = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxNumbOfTel = new System.Windows.Forms.MaskedTextBox();
            this.buttonAddClient = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.00227F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.99773F));
            this.tableLayoutPanel1.Controls.Add(this.labelNumberOfTel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBoxAddress, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxINN, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelAddress, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelModel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxNumbOfTel, 1, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(371, 173);
            this.tableLayoutPanel1.TabIndex = 23;
            // 
            // labelNumberOfTel
            // 
            this.labelNumberOfTel.AutoSize = true;
            this.labelNumberOfTel.Location = new System.Drawing.Point(3, 129);
            this.labelNumberOfTel.Name = "labelNumberOfTel";
            this.labelNumberOfTel.Size = new System.Drawing.Size(93, 13);
            this.labelNumberOfTel.TabIndex = 19;
            this.labelNumberOfTel.Text = "Номер телефона";
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.Location = new System.Drawing.Point(147, 89);
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(221, 20);
            this.textBoxAddress.TabIndex = 18;
            // 
            // textBoxINN
            // 
            this.textBoxINN.Location = new System.Drawing.Point(147, 46);
            this.textBoxINN.Name = "textBoxINN";
            this.textBoxINN.Size = new System.Drawing.Size(221, 20);
            this.textBoxINN.TabIndex = 17;
            this.textBoxINN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxINN_KeyPress);
            // 
            // labelAddress
            // 
            this.labelAddress.AutoSize = true;
            this.labelAddress.Location = new System.Drawing.Point(3, 86);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(38, 13);
            this.labelAddress.TabIndex = 5;
            this.labelAddress.Text = "Адрес";
            // 
            // labelModel
            // 
            this.labelModel.AutoSize = true;
            this.labelModel.Location = new System.Drawing.Point(3, 43);
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
            this.labelName.Size = new System.Drawing.Size(129, 26);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Наименовние организации или Ф.И.О";
            // 
            // textBoxNumbOfTel
            // 
            this.textBoxNumbOfTel.Location = new System.Drawing.Point(147, 132);
            this.textBoxNumbOfTel.Mask = "(000) 000-0000";
            this.textBoxNumbOfTel.Name = "textBoxNumbOfTel";
            this.textBoxNumbOfTel.ResetOnSpace = false;
            this.textBoxNumbOfTel.Size = new System.Drawing.Size(221, 20);
            this.textBoxNumbOfTel.SkipLiterals = false;
            this.textBoxNumbOfTel.TabIndex = 20;
            // 
            // buttonAddClient
            // 
            this.buttonAddClient.Location = new System.Drawing.Point(261, 191);
            this.buttonAddClient.Name = "buttonAddClient";
            this.buttonAddClient.Size = new System.Drawing.Size(108, 36);
            this.buttonAddClient.TabIndex = 22;
            this.buttonAddClient.Text = "Добавить";
            this.buttonAddClient.UseVisualStyleBackColor = true;
            this.buttonAddClient.Click += new System.EventHandler(this.buttonAddClient_Click);
            // 
            // FormAddClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 227);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.buttonAddClient);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormAddClient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление клиента";
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
        private System.Windows.Forms.Button buttonAddClient;
        private System.Windows.Forms.Label labelNumberOfTel;
        private System.Windows.Forms.MaskedTextBox textBoxNumbOfTel;

    }
}