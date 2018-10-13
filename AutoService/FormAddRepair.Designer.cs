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
            this.textBoxModel = new System.Windows.Forms.TextBox();
            this.OwnerLabel = new System.Windows.Forms.Label();
            this.GosNomLabel = new System.Windows.Forms.Label();
            this.RegLabel = new System.Windows.Forms.Label();
            this.VINLabel = new System.Windows.Forms.Label();
            this.ModelLabel = new System.Windows.Forms.Label();
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
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CreateRepair
            // 
            this.CreateRepair.Location = new System.Drawing.Point(282, 511);
            this.CreateRepair.Name = "CreateRepair";
            this.CreateRepair.Size = new System.Drawing.Size(127, 36);
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
            this.tableLayoutPanel1.Controls.Add(this.textBoxGosNom, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.textBoxReg, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBoxVIN, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxModel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.OwnerLabel, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.GosNomLabel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.RegLabel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.VINLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.ModelLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxMark, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.MarkLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxOwner, 1, 5);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(-1, 66);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(410, 300);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // textBoxGosNom
            // 
            this.textBoxGosNom.Location = new System.Drawing.Point(162, 203);
            this.textBoxGosNom.Name = "textBoxGosNom";
            this.textBoxGosNom.ReadOnly = true;
            this.textBoxGosNom.Size = new System.Drawing.Size(245, 20);
            this.textBoxGosNom.TabIndex = 20;
            // 
            // textBoxReg
            // 
            this.textBoxReg.Location = new System.Drawing.Point(162, 153);
            this.textBoxReg.Name = "textBoxReg";
            this.textBoxReg.ReadOnly = true;
            this.textBoxReg.Size = new System.Drawing.Size(245, 20);
            this.textBoxReg.TabIndex = 19;
            // 
            // textBoxVIN
            // 
            this.textBoxVIN.Location = new System.Drawing.Point(162, 103);
            this.textBoxVIN.Name = "textBoxVIN";
            this.textBoxVIN.ReadOnly = true;
            this.textBoxVIN.Size = new System.Drawing.Size(245, 20);
            this.textBoxVIN.TabIndex = 18;
            // 
            // textBoxModel
            // 
            this.textBoxModel.Location = new System.Drawing.Point(162, 53);
            this.textBoxModel.Name = "textBoxModel";
            this.textBoxModel.ReadOnly = true;
            this.textBoxModel.Size = new System.Drawing.Size(245, 20);
            this.textBoxModel.TabIndex = 17;
            // 
            // OwnerLabel
            // 
            this.OwnerLabel.AutoSize = true;
            this.OwnerLabel.Location = new System.Drawing.Point(3, 250);
            this.OwnerLabel.Name = "OwnerLabel";
            this.OwnerLabel.Size = new System.Drawing.Size(56, 13);
            this.OwnerLabel.TabIndex = 10;
            this.OwnerLabel.Text = "Владелец";
            // 
            // GosNomLabel
            // 
            this.GosNomLabel.AutoSize = true;
            this.GosNomLabel.Location = new System.Drawing.Point(3, 200);
            this.GosNomLabel.Name = "GosNomLabel";
            this.GosNomLabel.Size = new System.Drawing.Size(63, 13);
            this.GosNomLabel.TabIndex = 9;
            this.GosNomLabel.Text = "Гос. номер";
            // 
            // RegLabel
            // 
            this.RegLabel.AutoSize = true;
            this.RegLabel.Location = new System.Drawing.Point(3, 150);
            this.RegLabel.Name = "RegLabel";
            this.RegLabel.Size = new System.Drawing.Size(96, 26);
            this.RegLabel.TabIndex = 7;
            this.RegLabel.Text = "Свидетельство о регистрации";
            // 
            // VINLabel
            // 
            this.VINLabel.AutoSize = true;
            this.VINLabel.Location = new System.Drawing.Point(3, 100);
            this.VINLabel.Name = "VINLabel";
            this.VINLabel.Size = new System.Drawing.Size(124, 13);
            this.VINLabel.TabIndex = 5;
            this.VINLabel.Text = "VIN номер автомобиля";
            // 
            // ModelLabel
            // 
            this.ModelLabel.AutoSize = true;
            this.ModelLabel.Location = new System.Drawing.Point(3, 50);
            this.ModelLabel.Name = "ModelLabel";
            this.ModelLabel.Size = new System.Drawing.Size(46, 13);
            this.ModelLabel.TabIndex = 2;
            this.ModelLabel.Text = "Модель";
            // 
            // textBoxMark
            // 
            this.textBoxMark.Location = new System.Drawing.Point(162, 3);
            this.textBoxMark.Name = "textBoxMark";
            this.textBoxMark.ReadOnly = true;
            this.textBoxMark.Size = new System.Drawing.Size(245, 20);
            this.textBoxMark.TabIndex = 16;
            // 
            // MarkLabel
            // 
            this.MarkLabel.AutoSize = true;
            this.MarkLabel.Location = new System.Drawing.Point(3, 0);
            this.MarkLabel.Name = "MarkLabel";
            this.MarkLabel.Size = new System.Drawing.Size(40, 13);
            this.MarkLabel.TabIndex = 0;
            this.MarkLabel.Text = "Марка";
            // 
            // textBoxOwner
            // 
            this.textBoxOwner.Location = new System.Drawing.Point(162, 253);
            this.textBoxOwner.Name = "textBoxOwner";
            this.textBoxOwner.ReadOnly = true;
            this.textBoxOwner.Size = new System.Drawing.Size(245, 20);
            this.textBoxOwner.TabIndex = 21;
            // 
            // btnAddNewAutoRepair
            // 
            this.btnAddNewAutoRepair.Location = new System.Drawing.Point(5, 13);
            this.btnAddNewAutoRepair.Name = "btnAddNewAutoRepair";
            this.btnAddNewAutoRepair.Size = new System.Drawing.Size(157, 34);
            this.btnAddNewAutoRepair.TabIndex = 10;
            this.btnAddNewAutoRepair.Text = "Добавить новый автомобиль";
            this.btnAddNewAutoRepair.UseVisualStyleBackColor = true;
            this.btnAddNewAutoRepair.Click += new System.EventHandler(this.btnAddNewAutoRepair_Click);
            // 
            // btnSelExistAutoRepair
            // 
            this.btnSelExistAutoRepair.Location = new System.Drawing.Point(173, 13);
            this.btnSelExistAutoRepair.Name = "btnSelExistAutoRepair";
            this.btnSelExistAutoRepair.Size = new System.Drawing.Size(146, 34);
            this.btnSelExistAutoRepair.TabIndex = 11;
            this.btnSelExistAutoRepair.Text = "Выбрать существующий автомобиль";
            this.btnSelExistAutoRepair.UseVisualStyleBackColor = true;
            this.btnSelExistAutoRepair.Click += new System.EventHandler(this.btnSelExistAutoRepair_Click);
            // 
            // MalfLabel
            // 
            this.MalfLabel.AutoSize = true;
            this.MalfLabel.Location = new System.Drawing.Point(5, 364);
            this.MalfLabel.Name = "MalfLabel";
            this.MalfLabel.Size = new System.Drawing.Size(86, 13);
            this.MalfLabel.TabIndex = 12;
            this.MalfLabel.Text = "Неисправности";
            // 
            // PersonalLabel
            // 
            this.PersonalLabel.AutoSize = true;
            this.PersonalLabel.Location = new System.Drawing.Point(5, 403);
            this.PersonalLabel.Name = "PersonalLabel";
            this.PersonalLabel.Size = new System.Drawing.Size(55, 13);
            this.PersonalLabel.TabIndex = 13;
            this.PersonalLabel.Text = "Работник";
            // 
            // btnAddMalf
            // 
            this.btnAddMalf.Location = new System.Drawing.Point(161, 364);
            this.btnAddMalf.Name = "btnAddMalf";
            this.btnAddMalf.Size = new System.Drawing.Size(75, 23);
            this.btnAddMalf.TabIndex = 14;
            this.btnAddMalf.Text = "Добавить";
            this.btnAddMalf.UseVisualStyleBackColor = true;
            this.btnAddMalf.Click += new System.EventHandler(this.btnAddMalf_Click);
            // 
            // btnShowMalf
            // 
            this.btnShowMalf.Location = new System.Drawing.Point(242, 364);
            this.btnShowMalf.Name = "btnShowMalf";
            this.btnShowMalf.Size = new System.Drawing.Size(75, 23);
            this.btnShowMalf.TabIndex = 15;
            this.btnShowMalf.Text = "Просмотр...";
            this.btnShowMalf.UseVisualStyleBackColor = true;
            this.btnShowMalf.Click += new System.EventHandler(this.btnShowMalf_Click);
            // 
            // btnSelectPersonal
            // 
            this.btnSelectPersonal.Location = new System.Drawing.Point(161, 398);
            this.btnSelectPersonal.Name = "btnSelectPersonal";
            this.btnSelectPersonal.Size = new System.Drawing.Size(75, 23);
            this.btnSelectPersonal.TabIndex = 16;
            this.btnSelectPersonal.Text = "Выбрать";
            this.btnSelectPersonal.UseVisualStyleBackColor = true;
            this.btnSelectPersonal.Click += new System.EventHandler(this.btnSelectPersonal_Click);
            // 
            // SelectedPersonLabel
            // 
            this.SelectedPersonLabel.AutoSize = true;
            this.SelectedPersonLabel.Location = new System.Drawing.Point(158, 424);
            this.SelectedPersonLabel.Name = "SelectedPersonLabel";
            this.SelectedPersonLabel.Size = new System.Drawing.Size(0, 13);
            this.SelectedPersonLabel.TabIndex = 17;
            // 
            // labelNotes
            // 
            this.labelNotes.AutoSize = true;
            this.labelNotes.Location = new System.Drawing.Point(5, 440);
            this.labelNotes.Name = "labelNotes";
            this.labelNotes.Size = new System.Drawing.Size(51, 13);
            this.labelNotes.TabIndex = 18;
            this.labelNotes.Text = "Заметки";
            // 
            // textBoxNotes
            // 
            this.textBoxNotes.Location = new System.Drawing.Point(161, 440);
            this.textBoxNotes.Multiline = true;
            this.textBoxNotes.Name = "textBoxNotes";
            this.textBoxNotes.Size = new System.Drawing.Size(245, 67);
            this.textBoxNotes.TabIndex = 19;
            // 
            // FormAddRepair
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 555);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
        public System.Windows.Forms.Label ModelLabel;
        public System.Windows.Forms.TextBox textBoxGosNom;
        public System.Windows.Forms.TextBox textBoxReg;
        public System.Windows.Forms.TextBox textBoxVIN;
        public System.Windows.Forms.TextBox textBoxModel;
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
    }
}