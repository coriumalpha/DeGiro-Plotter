﻿namespace GraphicalOperator
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
            this.GroupTotals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTransactionsByDay)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnCalculate
            // 
            this.BtnCalculate.Location = new System.Drawing.Point(189, 160);
            this.BtnCalculate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnCalculate.Name = "BtnCalculate";
            this.BtnCalculate.Size = new System.Drawing.Size(114, 29);
            this.BtnCalculate.TabIndex = 0;
            this.BtnCalculate.Text = "Calcular";
            this.BtnCalculate.UseVisualStyleBackColor = true;
            this.BtnCalculate.Click += new System.EventHandler(this.BtnCalculate_Click);
            // 
            // LblTotalRevenue
            // 
            this.LblTotalRevenue.AutoSize = true;
            this.LblTotalRevenue.Location = new System.Drawing.Point(26, 42);
            this.LblTotalRevenue.Name = "LblTotalRevenue";
            this.LblTotalRevenue.Size = new System.Drawing.Size(94, 19);
            this.LblTotalRevenue.TabIndex = 1;
            this.LblTotalRevenue.Text = "Total Revenue";
            // 
            // LblTotalRevenueValue
            // 
            this.LblTotalRevenueValue.AutoSize = true;
            this.LblTotalRevenueValue.Location = new System.Drawing.Point(189, 46);
            this.LblTotalRevenueValue.Name = "LblTotalRevenueValue";
            this.LblTotalRevenueValue.Size = new System.Drawing.Size(0, 19);
            this.LblTotalRevenueValue.TabIndex = 2;
            // 
            // LblTotalRevenueLocal
            // 
            this.LblTotalRevenueLocal.AutoSize = true;
            this.LblTotalRevenueLocal.Location = new System.Drawing.Point(26, 79);
            this.LblTotalRevenueLocal.Name = "LblTotalRevenueLocal";
            this.LblTotalRevenueLocal.Size = new System.Drawing.Size(137, 19);
            this.LblTotalRevenueLocal.TabIndex = 3;
            this.LblTotalRevenueLocal.Text = "Total Revenue (Local)";
            // 
            // boxLocalRevenue
            // 
            this.boxLocalRevenue.Enabled = false;
            this.boxLocalRevenue.Location = new System.Drawing.Point(189, 38);
            this.boxLocalRevenue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.boxLocalRevenue.Name = "boxLocalRevenue";
            this.boxLocalRevenue.Size = new System.Drawing.Size(114, 26);
            this.boxLocalRevenue.TabIndex = 4;
            // 
            // boxTotalRevenueLocal
            // 
            this.boxTotalRevenueLocal.Enabled = false;
            this.boxTotalRevenueLocal.Location = new System.Drawing.Point(189, 75);
            this.boxTotalRevenueLocal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.boxTotalRevenueLocal.Name = "boxTotalRevenueLocal";
            this.boxTotalRevenueLocal.Size = new System.Drawing.Size(114, 26);
            this.boxTotalRevenueLocal.TabIndex = 5;
            // 
            // lblTotalCharges
            // 
            this.lblTotalCharges.AutoSize = true;
            this.lblTotalCharges.Location = new System.Drawing.Point(26, 115);
            this.lblTotalCharges.Name = "lblTotalCharges";
            this.lblTotalCharges.Size = new System.Drawing.Size(92, 19);
            this.lblTotalCharges.TabIndex = 6;
            this.lblTotalCharges.Text = "Total Charges";
            // 
            // boxTotalCharges
            // 
            this.boxTotalCharges.Enabled = false;
            this.boxTotalCharges.Location = new System.Drawing.Point(189, 111);
            this.boxTotalCharges.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.boxTotalCharges.Name = "boxTotalCharges";
            this.boxTotalCharges.Size = new System.Drawing.Size(114, 26);
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
            this.GroupTotals.Location = new System.Drawing.Point(14, 15);
            this.GroupTotals.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GroupTotals.Name = "GroupTotals";
            this.GroupTotals.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GroupTotals.Size = new System.Drawing.Size(336, 212);
            this.GroupTotals.TabIndex = 8;
            this.GroupTotals.TabStop = false;
            this.GroupTotals.Text = "Totals";
            // 
            // gridTransactionsByDay
            // 
            this.gridTransactionsByDay.AllowUserToAddRows = false;
            this.gridTransactionsByDay.AllowUserToDeleteRows = false;
            this.gridTransactionsByDay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTransactionsByDay.Location = new System.Drawing.Point(356, 15);
            this.gridTransactionsByDay.Name = "gridTransactionsByDay";
            this.gridTransactionsByDay.ReadOnly = true;
            this.gridTransactionsByDay.RowHeadersWidth = 47;
            this.gridTransactionsByDay.RowTemplate.Height = 28;
            this.gridTransactionsByDay.Size = new System.Drawing.Size(1059, 1250);
            this.gridTransactionsByDay.TabIndex = 9;
            this.gridTransactionsByDay.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Plotter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1427, 1277);
            this.Controls.Add(this.gridTransactionsByDay);
            this.Controls.Add(this.GroupTotals);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Plotter";
            this.Text = "DeGiro Plotter";
            this.GroupTotals.ResumeLayout(false);
            this.GroupTotals.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTransactionsByDay)).EndInit();
            this.ResumeLayout(false);

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
    }
}

