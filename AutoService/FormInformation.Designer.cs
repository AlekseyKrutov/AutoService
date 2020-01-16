namespace AutoService
{
    partial class FormInformation
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInformation));
            this.textBoxInf = new System.Windows.Forms.TextBox();
            this.SendMail = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // textBoxInf
            // 
            this.textBoxInf.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxInf.Location = new System.Drawing.Point(74, 1);
            this.textBoxInf.Multiline = true;
            this.textBoxInf.Name = "textBoxInf";
            this.textBoxInf.ReadOnly = true;
            this.textBoxInf.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxInf.Size = new System.Drawing.Size(385, 663);
            this.textBoxInf.TabIndex = 32;
            this.textBoxInf.TabStop = false;
            // 
            // SendMail
            // 
            this.SendMail.BackColor = System.Drawing.Color.Transparent;
            this.SendMail.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.SendMail.FlatAppearance.BorderSize = 0;
            this.SendMail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SendMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SendMail.Image = ((System.Drawing.Image)(resources.GetObject("SendMail.Image")));
            this.SendMail.Location = new System.Drawing.Point(0, 1);
            this.SendMail.Name = "SendMail";
            this.SendMail.Size = new System.Drawing.Size(70, 70);
            this.SendMail.TabIndex = 34;
            this.toolTip1.SetToolTip(this.SendMail, "Отправить клиенту по почте");
            this.SendMail.UseVisualStyleBackColor = false;
            this.SendMail.Click += new System.EventHandler(this.SendMail_Click);
            // 
            // FormInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 664);
            this.Controls.Add(this.SendMail);
            this.Controls.Add(this.textBoxInf);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormInformation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Информация";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textBoxInf;
        private System.Windows.Forms.Button SendMail;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}