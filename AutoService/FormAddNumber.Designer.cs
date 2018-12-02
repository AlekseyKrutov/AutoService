namespace AutoService
{
    partial class FormAddNumber
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddNumber));
            this.btnAddNumber = new System.Windows.Forms.Button();
            this.numberUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numberUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddNumber
            // 
            this.btnAddNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddNumber.Location = new System.Drawing.Point(120, 41);
            this.btnAddNumber.Name = "btnAddNumber";
            this.btnAddNumber.Size = new System.Drawing.Size(102, 34);
            this.btnAddNumber.TabIndex = 0;
            this.btnAddNumber.Text = "Добавить";
            this.btnAddNumber.UseVisualStyleBackColor = true;
            this.btnAddNumber.Click += new System.EventHandler(this.btnAddNumber_Click);
            // 
            // numberUpDown
            // 
            this.numberUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numberUpDown.Location = new System.Drawing.Point(12, 42);
            this.numberUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numberUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numberUpDown.Name = "numberUpDown";
            this.numberUpDown.Size = new System.Drawing.Size(102, 31);
            this.numberUpDown.TabIndex = 1;
            this.numberUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // FormAddNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(231, 125);
            this.Controls.Add(this.numberUpDown);
            this.Controls.Add(this.btnAddNumber);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormAddNumber";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавить количество";
            ((System.ComponentModel.ISupportInitialize)(this.numberUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddNumber;
        private System.Windows.Forms.NumericUpDown numberUpDown;
    }
}