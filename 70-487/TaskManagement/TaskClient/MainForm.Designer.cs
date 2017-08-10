namespace TaskClient
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbUserName = new System.Windows.Forms.ComboBox();
            this.gvTasks = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnAddTask = new System.Windows.Forms.Button();
            this.btnCompleteTask = new System.Windows.Forms.Button();
            this.btnReassignTask = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gvTasks)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "User name:";
            // 
            // cmbUserName
            // 
            this.cmbUserName.FormattingEnabled = true;
            this.cmbUserName.Location = new System.Drawing.Point(81, 13);
            this.cmbUserName.Name = "cmbUserName";
            this.cmbUserName.Size = new System.Drawing.Size(121, 21);
            this.cmbUserName.TabIndex = 1;
            // 
            // gvTasks
            // 
            this.gvTasks.AllowUserToAddRows = false;
            this.gvTasks.AllowUserToDeleteRows = false;
            this.gvTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvTasks.Location = new System.Drawing.Point(13, 43);
            this.gvTasks.MultiSelect = false;
            this.gvTasks.Name = "gvTasks";
            this.gvTasks.ReadOnly = true;
            this.gvTasks.Size = new System.Drawing.Size(304, 180);
            this.gvTasks.TabIndex = 2;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(242, 11);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnAddTask
            // 
            this.btnAddTask.Location = new System.Drawing.Point(12, 229);
            this.btnAddTask.Name = "btnAddTask";
            this.btnAddTask.Size = new System.Drawing.Size(75, 23);
            this.btnAddTask.TabIndex = 4;
            this.btnAddTask.Text = "Add Task";
            this.btnAddTask.UseVisualStyleBackColor = true;
            this.btnAddTask.Click += new System.EventHandler(this.btnAddTask_Click);
            // 
            // btnCompleteTask
            // 
            this.btnCompleteTask.Location = new System.Drawing.Point(215, 229);
            this.btnCompleteTask.Name = "btnCompleteTask";
            this.btnCompleteTask.Size = new System.Drawing.Size(102, 23);
            this.btnCompleteTask.TabIndex = 4;
            this.btnCompleteTask.Text = "Complete Task";
            this.btnCompleteTask.UseVisualStyleBackColor = true;
            this.btnCompleteTask.Click += new System.EventHandler(this.btnCompleteTask_Click);
            // 
            // btnReassignTask
            // 
            this.btnReassignTask.Location = new System.Drawing.Point(117, 229);
            this.btnReassignTask.Name = "btnReassignTask";
            this.btnReassignTask.Size = new System.Drawing.Size(75, 23);
            this.btnReassignTask.TabIndex = 5;
            this.btnReassignTask.Text = "Reassign...";
            this.btnReassignTask.UseVisualStyleBackColor = true;
            this.btnReassignTask.Click += new System.EventHandler(this.btnReassignTask_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 262);
            this.Controls.Add(this.btnReassignTask);
            this.Controls.Add(this.btnCompleteTask);
            this.Controls.Add(this.btnAddTask);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.gvTasks);
            this.Controls.Add(this.cmbUserName);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "Tasks";
            ((System.ComponentModel.ISupportInitialize)(this.gvTasks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cmbUserName;
		private System.Windows.Forms.DataGridView gvTasks;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Button btnAddTask;
		private System.Windows.Forms.Button btnCompleteTask;
        private System.Windows.Forms.Button btnReassignTask;
	}
}

