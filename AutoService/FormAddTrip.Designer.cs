namespace AutoService
{
    partial class FormAddTrip
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddTrip));
            this.btnAddTrip = new System.Windows.Forms.Button();
            this.textBoxTrip = new System.Windows.Forms.TextBox();
            this.labelTrip = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnAddTrip
            // 
            this.btnAddTrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddTrip.Location = new System.Drawing.Point(402, 200);
            this.btnAddTrip.Name = "btnAddTrip";
            this.btnAddTrip.Size = new System.Drawing.Size(106, 36);
            this.btnAddTrip.TabIndex = 0;
            this.btnAddTrip.Text = "Добавить";
            this.btnAddTrip.UseVisualStyleBackColor = true;
            this.btnAddTrip.Click += new System.EventHandler(this.btnAddTrip_Click);
            // 
            // textBoxTrip
            // 
            this.textBoxTrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxTrip.Location = new System.Drawing.Point(118, 27);
            this.textBoxTrip.Multiline = true;
            this.textBoxTrip.Name = "textBoxTrip";
            this.textBoxTrip.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxTrip.Size = new System.Drawing.Size(390, 148);
            this.textBoxTrip.TabIndex = 1;
            // 
            // labelTrip
            // 
            this.labelTrip.AutoSize = true;
            this.labelTrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTrip.Location = new System.Drawing.Point(12, 27);
            this.labelTrip.Name = "labelTrip";
            this.labelTrip.Size = new System.Drawing.Size(82, 20);
            this.labelTrip.TabIndex = 2;
            this.labelTrip.Text = "Маршрут:";
            // 
            // FormAddTrip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 255);
            this.Controls.Add(this.labelTrip);
            this.Controls.Add(this.textBoxTrip);
            this.Controls.Add(this.btnAddTrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormAddTrip";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление маршрута";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAddTrip_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddTrip;
        private System.Windows.Forms.Label labelTrip;
        public System.Windows.Forms.TextBox textBoxTrip;
    }
}