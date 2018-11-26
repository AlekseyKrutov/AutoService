namespace AutoService
{
    partial class FormAddRepair
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddRepair));
            this.CreateRepair = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxGosNom = new System.Windows.Forms.TextBox();
            this.textBoxReg = new System.Windows.Forms.TextBox();
            this.textBoxVIN = new System.Windows.Forms.TextBox();
            this.OwnerLabel = new System.Windows.Forms.Label();
            this.GosNomLabel = new System.Windows.Forms.Label();
            this.RegLabel = new System.Windows.Forms.Label();
            this.VINLabel = new System.Windows.Forms.Label();
            this.textBoxMark = new System.Windows.Forms.TextBox();
            this.MarkLabel = new System.Windows.Forms.Label();
            this.textBoxOwner = new System.Windows.Forms.TextBox();
            this.btnAddNewAutoRepair = new System.Windows.Forms.Button();
            this.btnSelExistAutoRepair = new System.Windows.Forms.Button();
            this.MalfLabel = new System.Windows.Forms.Label();
            this.PersonalLabel = new System.Windows.Forms.Label();
            this.btnAddMalf = new System.Windows.Forms.Button();
            this.btnShowMalf = new System.Windows.Forms.Button();
            this.btnSelectPersonal = new System.Windows.Forms.Button();
            this.SelectedPersonLabel = new System.Windows.Forms.Label();
            this.labelNotes = new System.Windows.Forms.Label();
            this.textBoxNotes = new System.Windows.Forms.TextBox();
            this.btnShowWorker = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CreateRepair
            // 
            this.CreateRepair.Location = new System.Drawing.Point(423, 786);
            this.CreateRepair.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CreateRepair.Name = "CreateRepair";
            this.CreateRepair.Size = new System.Drawing.Size(190, 55);
            this.CreateRepair.TabIndex = 5;
            this.CreateRepair.Text = "Добавить";
            this.CreateRepair.UseVisualStyleBackColor = true;
            this.CreateRepair.Click += new System.EventHandler(this.CreateRepair_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.00227F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.99773F));
            this.tableLayoutPanel1.Controls.Add(this.textBoxMark, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.MarkLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.VINLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxVIN, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.RegLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxReg, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.GosNomLabel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBoxGosNom, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.OwnerLabel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.textBoxOwner, 1, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 82);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(515, 280);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // textBoxGosNom
            // 
            this.textBoxGosNom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxGosNom.Location = new System.Drawing.Point(204, 173);
            this.textBoxGosNom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxGosNom.Name = "textBoxGosNom";
            this.textBoxGosNom.ReadOnly = true;
            this.textBoxGosNom.Size = new System.Drawing.Size(301, 26);
            this.textBoxGosNom.TabIndex = 20;
            // 
            // textBoxReg
            // 
            this.textBoxReg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxReg.Location = new System.Drawing.Point(204, 117);
            this.textBoxReg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxReg.Name = "textBoxReg";
            this.textBoxReg.ReadOnly = true;
            this.textBoxReg.Size = new System.Drawing.Size(301, 26);
            this.textBoxReg.TabIndex = 19;
            // 
            // textBoxVIN
            // 
            this.textBoxVIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxVIN.Location = new System.Drawing.Point(204, 61);
            this.textBoxVIN.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxVIN.Name = "textBoxVIN";
            this.textBoxVIN.ReadOnly = true;
            this.textBoxVIN.Size = new System.Drawing.Size(301, 26);
            this.textBoxVIN.TabIndex = 18;
            // 
            // OwnerLabel
            // 
            this.OwnerLabel.AutoSize = true;
            this.OwnerLabel.Location = new System.Drawing.Point(4, 224);
            this.OwnerLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.OwnerLabel.Name = "OwnerLabel";
            this.OwnerLabel.Size = new System.Drawing.Size(87, 20);
            this.OwnerLabel.TabIndex = 10;
            this.OwnerLabel.Text = "Владелец";
            // 
            // GosNomLabel
            // 
            this.GosNomLabel.AutoSize = true;
            this.GosNomLabel.Location = new System.Drawing.Point(4, 168);
            this.GosNomLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.GosNomLabel.Name = "GosNomLabel";
            this.GosNomLabel.Size = new System.Drawing.Size(90, 20);
            this.GosNomLabel.TabIndex = 9;
            this.GosNomLabel.Text = "Гос. номер";
            // 
            // RegLabel
            // 
            this.RegLabel.AutoSize = true;
            this.RegLabel.Location = new System.Drawing.Point(4, 112);
            this.RegLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.RegLabel.Name = "RegLabel";
            this.RegLabel.Size = new System.Drawing.Size(147, 40);
            this.RegLabel.TabIndex = 7;
            this.RegLabel.Text = "Свидетельство о регистрации";
            // 
            // VINLabel
            // 
            this.VINLabel.AutoSize = true;
            this.VINLabel.Location = new System.Drawing.Point(4, 56);
            this.VINLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.VINLabel.Name = "VINLabel";
            this.VINLabel.Size = new System.Drawing.Size(184, 20);
            this.VINLabel.TabIndex = 5;
            this.VINLabel.Text = "VIN номер автомобиля";
            // 
            // textBoxMark
            // 
            this.textBoxMark.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxMark.Location = new System.Drawing.Point(204, 5);
            this.textBoxMark.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxMark.Name = "textBoxMark";
            this.textBoxMark.ReadOnly = true;
            this.textBoxMark.Size = new System.Drawing.Size(301, 26);
            this.textBoxMark.TabIndex = 16;
            // 
            // MarkLabel
            // 
            this.MarkLabel.AutoSize = true;
            this.MarkLabel.Location = new System.Drawing.Point(4, 0);
            this.MarkLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.MarkLabel.Name = "MarkLabel";
            this.MarkLabel.Size = new System.Drawing.Size(57, 20);
            this.MarkLabel.TabIndex = 0;
            this.MarkLabel.Text = "Марка";
            // 
            // textBoxOwner
            // 
            this.textBoxOwner.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxOwner.Location = new System.Drawing.Point(204, 229);
            this.textBoxOwner.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxOwner.Name = "textBoxOwner";
            this.textBoxOwner.ReadOnly = true;
            this.textBoxOwner.Size = new System.Drawing.Size(301, 26);
            this.textBoxOwner.TabIndex = 21;
            // 
            // btnAddNewAutoRepair
            // 
            this.btnAddNewAutoRepair.Location = new System.Drawing.Point(8, 20);
            this.btnAddNewAutoRepair.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddNewAutoRepair.Name = "btnAddNewAutoRepair";
            this.btnAddNewAutoRepair.Size = new System.Drawing.Size(236, 52);
            this.btnAddNewAutoRepair.TabIndex = 10;
            this.btnAddNewAutoRepair.Text = "Добавить новый автомобиль";
            this.btnAddNewAutoRepair.UseVisualStyleBackColor = true;
            this.btnAddNewAutoRepair.Click += new System.EventHandler(this.btnAddNewAutoRepair_Click);
            // 
            // btnSelExistAutoRepair
            // 
            this.btnSelExistAutoRepair.Location = new System.Drawing.Point(260, 20);
            this.btnSelExistAutoRepair.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSelExistAutoRepair.Name = "btnSelExistAutoRepair";
            this.btnSelExistAutoRepair.Size = new System.Drawing.Size(219, 52);
            this.btnSelExistAutoRepair.TabIndex = 11;
            this.btnSelExistAutoRepair.Text = "Выбрать существующий автомобиль";
            this.btnSelExistAutoRepair.UseVisualStyleBackColor = true;
            this.btnSelExistAutoRepair.Click += new System.EventHandler(this.btnSelExistAutoRepair_Click);
            // 
            // MalfLabel
            // 
            this.MalfLabel.AutoSize = true;
            this.MalfLabel.Location = new System.Drawing.Point(12, 379);
            this.MalfLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.MalfLabel.Name = "MalfLabel";
            this.MalfLabel.Size = new System.Drawing.Size(127, 20);
            this.MalfLabel.TabIndex = 12;
            this.MalfLabel.Text = "Неисправности";
            // 
            // PersonalLabel
            // 
            this.PersonalLabel.AutoSize = true;
            this.PersonalLabel.Location = new System.Drawing.Point(12, 417);
            this.PersonalLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PersonalLabel.Name = "PersonalLabel";
            this.PersonalLabel.Size = new System.Drawing.Size(81, 20);
            this.PersonalLabel.TabIndex = 13;
            this.PersonalLabel.Text = "Работник";
            // 
            // btnAddMalf
            // 
            this.btnAddMalf.Location = new System.Drawing.Point(212, 372);
            this.btnAddMalf.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddMalf.Name = "btnAddMalf";
            this.btnAddMalf.Size = new System.Drawing.Size(112, 35);
            this.btnAddMalf.TabIndex = 14;
            this.btnAddMalf.Text = "Добавить";
            this.btnAddMalf.UseVisualStyleBackColor = true;
            this.btnAddMalf.Click += new System.EventHandler(this.btnAddMalf_Click);
            // 
            // btnShowMalf
            // 
            this.btnShowMalf.Location = new System.Drawing.Point(332, 372);
            this.btnShowMalf.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnShowMalf.Name = "btnShowMalf";
            this.btnShowMalf.Size = new System.Drawing.Size(112, 35);
            this.btnShowMalf.TabIndex = 15;
            this.btnShowMalf.Text = "Просмотр...";
            this.btnShowMalf.UseVisualStyleBackColor = true;
            this.btnShowMalf.Click += new System.EventHandler(this.btnShowMalf_Click);
            // 
            // btnSelectPersonal
            // 
            this.btnSelectPersonal.Location = new System.Drawing.Point(212, 417);
            this.btnSelectPersonal.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSelectPersonal.Name = "btnSelectPersonal";
            this.btnSelectPersonal.Size = new System.Drawing.Size(112, 35);
            this.btnSelectPersonal.TabIndex = 16;
            this.btnSelectPersonal.Text = "Выбрать";
            this.btnSelectPersonal.UseVisualStyleBackColor = true;
            this.btnSelectPersonal.Click += new System.EventHandler(this.btnSelectPersonal_Click);
            // 
            // SelectedPersonLabel
            // 
            this.SelectedPersonLabel.AutoSize = true;
            this.SelectedPersonLabel.Location = new System.Drawing.Point(237, 652);
            this.SelectedPersonLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SelectedPersonLabel.Name = "SelectedPersonLabel";
            this.SelectedPersonLabel.Size = new System.Drawing.Size(0, 20);
            this.SelectedPersonLabel.TabIndex = 17;
            // 
            // labelNotes
            // 
            this.labelNotes.AutoSize = true;
            this.labelNotes.Location = new System.Drawing.Point(13, 455);
            this.labelNotes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNotes.Name = "labelNotes";
            this.labelNotes.Size = new System.Drawing.Size(75, 20);
            this.labelNotes.TabIndex = 18;
            this.labelNotes.Text = "Заметки";
            // 
            // textBoxNotes
            // 
            this.textBoxNotes.Location = new System.Drawing.Point(212, 462);
            this.textBoxNotes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxNotes.Multiline = true;
            this.textBoxNotes.Name = "textBoxNotes";
            this.textBoxNotes.Size = new System.Drawing.Size(311, 101);
            this.textBoxNotes.TabIndex = 19;
            // 
            // btnShowWorker
            // 
            this.btnShowWorker.Location = new System.Drawing.Point(332, 417);
            this.btnShowWorker.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnShowWorker.Name = "btnShowWorker";
            this.btnShowWorker.Size = new System.Drawing.Size(112, 35);
            this.btnShowWorker.TabIndex = 20;
            this.btnShowWorker.Text = "Просмотр...";
            this.btnShowWorker.UseVisualStyleBackColor = true;
            // 
            // FormAddRepair
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 574);
            this.Controls.Add(this.btnShowWorker);
            this.Controls.Add(this.textBoxNotes);
            this.Controls.Add(this.labelNotes);
            this.Controls.Add(this.SelectedPersonLabel);
            this.Controls.Add(this.btnSelectPersonal);
            this.Controls.Add(this.btnShowMalf);
            this.Controls.Add(this.btnAddMalf);
            this.Controls.Add(this.PersonalLabel);
            this.Controls.Add(this.MalfLabel);
            this.Controls.Add(this.btnSelExistAutoRepair);
            this.Controls.Add(this.btnAddNewAutoRepair);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.CreateRepair);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "FormAddRepair";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление ремонта";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAddRepair_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button CreateRepair;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.Label OwnerLabel;
        public System.Windows.Forms.Label GosNomLabel;
        public System.Windows.Forms.Label RegLabel;
        public System.Windows.Forms.Label VINLabel;
        public System.Windows.Forms.TextBox textBoxGosNom;
        public System.Windows.Forms.TextBox textBoxReg;
        public System.Windows.Forms.TextBox textBoxVIN;
        public System.Windows.Forms.TextBox textBoxMark;
        public System.Windows.Forms.Label MarkLabel;
        public System.Windows.Forms.Button btnAddNewAutoRepair;
        public System.Windows.Forms.Button btnSelExistAutoRepair;
        public System.Windows.Forms.TextBox textBoxOwner;
        private System.Windows.Forms.Label MalfLabel;
        private System.Windows.Forms.Label PersonalLabel;
        public System.Windows.Forms.Button btnAddMalf;
        public System.Windows.Forms.Button btnShowMalf;
        public System.Windows.Forms.Button btnSelectPersonal;
        public System.Windows.Forms.Label SelectedPersonLabel;
        private System.Windows.Forms.Label labelNotes;
        private System.Windows.Forms.TextBox textBoxNotes;
        public System.Windows.Forms.Button btnShowWorker;
    }
}