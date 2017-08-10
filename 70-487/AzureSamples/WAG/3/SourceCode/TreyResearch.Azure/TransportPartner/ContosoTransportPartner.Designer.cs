//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace TransportPartner
{
    partial class ContosoTransportPartner
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
            this.status = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ordersDataGrid = new System.Windows.Forms.DataGridView();
            this.ShipButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.status.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ordersDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // status
            // 
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.status.Location = new System.Drawing.Point(0, 351);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(551, 22);
            this.status.TabIndex = 0;
            this.status.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // ordersDataGrid
            // 
            this.ordersDataGrid.AllowUserToAddRows = false;
            this.ordersDataGrid.AllowUserToDeleteRows = false;
            this.ordersDataGrid.AllowUserToResizeRows = false;
            this.ordersDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ordersDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ShipButton});
            this.ordersDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ordersDataGrid.Location = new System.Drawing.Point(0, 0);
            this.ordersDataGrid.Name = "ordersDataGrid";
            this.ordersDataGrid.Size = new System.Drawing.Size(551, 351);
            this.ordersDataGrid.TabIndex = 2;
            this.ordersDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnOrdersDataGridCellClick);
            // 
            // ShipButton
            // 
            this.ShipButton.HeaderText = "";
            this.ShipButton.Name = "ShipButton";
            this.ShipButton.Text = "Shipped";
            this.ShipButton.UseColumnTextForButtonValue = true;
            // 
            // ContosoTransportPartner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 373);
            this.Controls.Add(this.ordersDataGrid);
            this.Controls.Add(this.status);
            this.Name = "ContosoTransportPartner";
            this.Text = "Active Orders";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnContosoPartnerFormClosed);
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ordersDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.DataGridView ordersDataGrid;
        private System.Windows.Forms.DataGridViewButtonColumn ShipButton;
    }
}

