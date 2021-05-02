namespace GraphicalOperator
{
    partial class Plotter
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnCalculate = new System.Windows.Forms.Button();
            this.LblTotalRevenue = new System.Windows.Forms.Label();
            this.LblTotalRevenueValue = new System.Windows.Forms.Label();
            this.LblTotalRevenueLocal = new System.Windows.Forms.Label();
            this.boxLocalRevenue = new System.Windows.Forms.TextBox();
            this.boxTotalRevenueLocal = new System.Windows.Forms.TextBox();
            this.lblTotalCharges = new System.Windows.Forms.Label();
            this.boxTotalCharges = new System.Windows.Forms.TextBox();
            this.GroupTotals = new System.Windows.Forms.GroupBox();
            this.gridTransactionsByDay = new System.Windows.Forms.DataGridView();
            this.Renta20Text = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Renta20GlobalText = new System.Windows.Forms.TextBox();
            this.GroupTotals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTransactionsByDay)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnCalculate
            // 
            this.BtnCalculate.Location = new System.Drawing.Point(165, 126);
            this.BtnCalculate.Name = "BtnCalculate";
            this.BtnCalculate.Size = new System.Drawing.Size(100, 23);
            this.BtnCalculate.TabIndex = 0;
            this.BtnCalculate.Text = "Calcular";
            this.BtnCalculate.UseVisualStyleBackColor = true;
            this.BtnCalculate.Click += new System.EventHandler(this.BtnCalculate_Click);
            // 
            // LblTotalRevenue
            // 
            this.LblTotalRevenue.AutoSize = true;
            this.LblTotalRevenue.Location = new System.Drawing.Point(23, 33);
            this.LblTotalRevenue.Name = "LblTotalRevenue";
            this.LblTotalRevenue.Size = new System.Drawing.Size(80, 15);
            this.LblTotalRevenue.TabIndex = 1;
            this.LblTotalRevenue.Text = "Total Revenue";
            // 
            // LblTotalRevenueValue
            // 
            this.LblTotalRevenueValue.AutoSize = true;
            this.LblTotalRevenueValue.Location = new System.Drawing.Point(165, 36);
            this.LblTotalRevenueValue.Name = "LblTotalRevenueValue";
            this.LblTotalRevenueValue.Size = new System.Drawing.Size(0, 15);
            this.LblTotalRevenueValue.TabIndex = 2;
            // 
            // LblTotalRevenueLocal
            // 
            this.LblTotalRevenueLocal.AutoSize = true;
            this.LblTotalRevenueLocal.Location = new System.Drawing.Point(23, 62);
            this.LblTotalRevenueLocal.Name = "LblTotalRevenueLocal";
            this.LblTotalRevenueLocal.Size = new System.Drawing.Size(119, 15);
            this.LblTotalRevenueLocal.TabIndex = 3;
            this.LblTotalRevenueLocal.Text = "Total Revenue (Local)";
            // 
            // boxLocalRevenue
            // 
            this.boxLocalRevenue.Enabled = false;
            this.boxLocalRevenue.Location = new System.Drawing.Point(165, 30);
            this.boxLocalRevenue.Name = "boxLocalRevenue";
            this.boxLocalRevenue.Size = new System.Drawing.Size(100, 23);
            this.boxLocalRevenue.TabIndex = 4;
            // 
            // boxTotalRevenueLocal
            // 
            this.boxTotalRevenueLocal.Enabled = false;
            this.boxTotalRevenueLocal.Location = new System.Drawing.Point(165, 59);
            this.boxTotalRevenueLocal.Name = "boxTotalRevenueLocal";
            this.boxTotalRevenueLocal.Size = new System.Drawing.Size(100, 23);
            this.boxTotalRevenueLocal.TabIndex = 5;
            // 
            // lblTotalCharges
            // 
            this.lblTotalCharges.AutoSize = true;
            this.lblTotalCharges.Location = new System.Drawing.Point(23, 91);
            this.lblTotalCharges.Name = "lblTotalCharges";
            this.lblTotalCharges.Size = new System.Drawing.Size(78, 15);
            this.lblTotalCharges.TabIndex = 6;
            this.lblTotalCharges.Text = "Total Charges";
            // 
            // boxTotalCharges
            // 
            this.boxTotalCharges.Enabled = false;
            this.boxTotalCharges.Location = new System.Drawing.Point(165, 88);
            this.boxTotalCharges.Name = "boxTotalCharges";
            this.boxTotalCharges.Size = new System.Drawing.Size(100, 23);
            this.boxTotalCharges.TabIndex = 7;
            // 
            // GroupTotals
            // 
            this.GroupTotals.Controls.Add(this.LblTotalRevenue);
            this.GroupTotals.Controls.Add(this.BtnCalculate);
            this.GroupTotals.Controls.Add(this.boxTotalCharges);
            this.GroupTotals.Controls.Add(this.LblTotalRevenueValue);
            this.GroupTotals.Controls.Add(this.lblTotalCharges);
            this.GroupTotals.Controls.Add(this.LblTotalRevenueLocal);
            this.GroupTotals.Controls.Add(this.boxTotalRevenueLocal);
            this.GroupTotals.Controls.Add(this.boxLocalRevenue);
            this.GroupTotals.Location = new System.Drawing.Point(12, 12);
            this.GroupTotals.Name = "GroupTotals";
            this.GroupTotals.Size = new System.Drawing.Size(294, 167);
            this.GroupTotals.TabIndex = 8;
            this.GroupTotals.TabStop = false;
            this.GroupTotals.Text = "Totals";
            // 
            // gridTransactionsByDay
            // 
            this.gridTransactionsByDay.AllowUserToAddRows = false;
            this.gridTransactionsByDay.AllowUserToDeleteRows = false;
            this.gridTransactionsByDay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTransactionsByDay.Location = new System.Drawing.Point(1077, 12);
            this.gridTransactionsByDay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridTransactionsByDay.Name = "gridTransactionsByDay";
            this.gridTransactionsByDay.ReadOnly = true;
            this.gridTransactionsByDay.RowHeadersWidth = 47;
            this.gridTransactionsByDay.RowTemplate.Height = 28;
            this.gridTransactionsByDay.Size = new System.Drawing.Size(723, 815);
            this.gridTransactionsByDay.TabIndex = 9;
            this.gridTransactionsByDay.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Renta20Text
            // 
            this.Renta20Text.Location = new System.Drawing.Point(312, 12);
            this.Renta20Text.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Renta20Text.Multiline = true;
            this.Renta20Text.Name = "Renta20Text";
            this.Renta20Text.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Renta20Text.Size = new System.Drawing.Size(759, 815);
            this.Renta20Text.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Renta20GlobalText);
            this.groupBox1.Location = new System.Drawing.Point(12, 185);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(294, 167);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Renta 20 Global";
            // 
            // Renta20GlobalText
            // 
            this.Renta20GlobalText.Location = new System.Drawing.Point(7, 23);
            this.Renta20GlobalText.Multiline = true;
            this.Renta20GlobalText.Name = "Renta20GlobalText";
            this.Renta20GlobalText.Size = new System.Drawing.Size(281, 138);
            this.Renta20GlobalText.TabIndex = 0;
            // 
            // Plotter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1812, 838);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Renta20Text);
            this.Controls.Add(this.gridTransactionsByDay);
            this.Controls.Add(this.GroupTotals);
            this.Name = "Plotter";
            this.Text = "DeGiro Plotter";
            this.GroupTotals.ResumeLayout(false);
            this.GroupTotals.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTransactionsByDay)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnCalculate;
        private System.Windows.Forms.Label LblTotalRevenue;
        private System.Windows.Forms.Label LblTotalRevenueValue;
        private System.Windows.Forms.Label LblTotalRevenueLocal;
        private System.Windows.Forms.TextBox boxLocalRevenue;
        private System.Windows.Forms.TextBox boxTotalRevenueLocal;
        private System.Windows.Forms.Label lblTotalCharges;
        private System.Windows.Forms.TextBox boxTotalCharges;
        private System.Windows.Forms.GroupBox GroupTotals;
        private System.Windows.Forms.DataGridView gridTransactionsByDay;
        private System.Windows.Forms.TextBox Renta20Text;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox Renta20GlobalText;
    }
}

