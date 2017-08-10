namespace TestApp
{
	partial class MainForm
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
			this.calcComponent = new CalcComponent.PrimeNumberCalculator(this.components);
			this.lstTasks = new System.Windows.Forms.ListView();
			this.colNumber = new System.Windows.Forms.ColumnHeader();
			this.colProgress = new System.Windows.Forms.ColumnHeader();
			this.colCurrent = new System.Windows.Forms.ColumnHeader();
			this.colTaskId = new System.Windows.Forms.ColumnHeader();
			this.colResult = new System.Windows.Forms.ColumnHeader();
			this.colFirstDivisor = new System.Windows.Forms.ColumnHeader();
			this.pnlButtons = new System.Windows.Forms.Panel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnStart = new System.Windows.Forms.Button();
			this.pnlButtons.SuspendLayout();
			this.SuspendLayout();
			// 
			// calcComponent
			// 
			this.calcComponent.ProgressChanged += new System.EventHandler<CalcComponent.CalcPrimeProgressChangedEventArgs>(this.calcComponent_ProgressChanged);
			this.calcComponent.CalcPrimeCompleted += new System.EventHandler<CalcComponent.CalcPrimeCompletedEventArgs>(this.calcComponent_CalcPrimeCompleted);
			// 
			// lstTasks
			// 
			this.lstTasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colNumber,
            this.colProgress,
            this.colCurrent,
            this.colTaskId,
            this.colResult,
            this.colFirstDivisor});
			this.lstTasks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstTasks.FullRowSelect = true;
			this.lstTasks.GridLines = true;
			this.lstTasks.Location = new System.Drawing.Point(0, 0);
			this.lstTasks.Name = "lstTasks";
			this.lstTasks.Size = new System.Drawing.Size(546, 266);
			this.lstTasks.TabIndex = 0;
			this.lstTasks.UseCompatibleStateImageBehavior = false;
			this.lstTasks.View = System.Windows.Forms.View.Details;
			// 
			// colNumber
			// 
			this.colNumber.Text = "Test Number";
			this.colNumber.Width = 80;
			// 
			// colProgress
			// 
			this.colProgress.Text = "Progress";
			// 
			// colCurrent
			// 
			this.colCurrent.Text = "Current";
			// 
			// colTaskId
			// 
			this.colTaskId.Text = "Task ID";
			this.colTaskId.Width = 200;
			// 
			// colResult
			// 
			this.colResult.Text = "Result";
			// 
			// colFirstDivisor
			// 
			this.colFirstDivisor.Text = "First Divisor";
			this.colFirstDivisor.Width = 80;
			// 
			// pnlButtons
			// 
			this.pnlButtons.Controls.Add(this.btnCancel);
			this.pnlButtons.Controls.Add(this.btnStart);
			this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlButtons.Location = new System.Drawing.Point(0, 234);
			this.pnlButtons.Name = "pnlButtons";
			this.pnlButtons.Size = new System.Drawing.Size(546, 32);
			this.pnlButtons.TabIndex = 1;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(442, 3);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(101, 23);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnStart
			// 
			this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnStart.Location = new System.Drawing.Point(3, 3);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(101, 23);
			this.btnStart.TabIndex = 0;
			this.btnStart.Text = "&Start New Task";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(546, 266);
			this.Controls.Add(this.pnlButtons);
			this.Controls.Add(this.lstTasks);
			this.MinimumSize = new System.Drawing.Size(554, 300);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Prime Number Calculator";
			this.pnlButtons.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private CalcComponent.PrimeNumberCalculator calcComponent;
		private System.Windows.Forms.ListView lstTasks;
		private System.Windows.Forms.Panel pnlButtons;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.ColumnHeader colNumber;
		private System.Windows.Forms.ColumnHeader colProgress;
		private System.Windows.Forms.ColumnHeader colCurrent;
		private System.Windows.Forms.ColumnHeader colTaskId;
		private System.Windows.Forms.ColumnHeader colResult;
		private System.Windows.Forms.ColumnHeader colFirstDivisor;
	}
}

