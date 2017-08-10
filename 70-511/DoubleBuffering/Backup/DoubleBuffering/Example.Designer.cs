namespace DoubleBufferExample
{
	partial class Example
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
            this.RefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.StartBtn = new System.Windows.Forms.Button();
            this.StopBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.TestMethodSelector = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.RefreshInterval = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.PaintMethodSelector = new System.Windows.Forms.ComboBox();
            this.DoubleBufferControl1 = new DoubleBufferExample.DoubleBufferControl();
            ((System.ComponentModel.ISupportInitialize)(this.RefreshInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // RefreshTimer
            // 
            this.RefreshTimer.Enabled = true;
            this.RefreshTimer.Interval = 800;
            this.RefreshTimer.Tick += new System.EventHandler(this.RefreshTimer_Tick);
            // 
            // StartBtn
            // 
            this.StartBtn.Location = new System.Drawing.Point(5, 170);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(75, 23);
            this.StartBtn.TabIndex = 16;
            this.StartBtn.Text = "Start";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // StopBtn
            // 
            this.StopBtn.Location = new System.Drawing.Point(95, 170);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(75, 23);
            this.StopBtn.TabIndex = 15;
            this.StopBtn.Text = "Stop";
            this.StopBtn.UseVisualStyleBackColor = true;
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Test method";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TestMethodSelector
            // 
            this.TestMethodSelector.BackColor = System.Drawing.Color.Snow;
            this.TestMethodSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TestMethodSelector.FormattingEnabled = true;
            this.TestMethodSelector.Items.AddRange(new object[] {
            "Draw test",
            "Fill test"});
            this.TestMethodSelector.Location = new System.Drawing.Point(5, 71);
            this.TestMethodSelector.Name = "TestMethodSelector";
            this.TestMethodSelector.Size = new System.Drawing.Size(165, 21);
            this.TestMethodSelector.TabIndex = 13;
            this.TestMethodSelector.SelectedIndexChanged += new System.EventHandler(this.TestMethodSelector_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Refresh interval";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RefreshInterval
            // 
            this.RefreshInterval.BackColor = System.Drawing.Color.Snow;
            this.RefreshInterval.Location = new System.Drawing.Point(5, 115);
            this.RefreshInterval.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.RefreshInterval.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.RefreshInterval.Name = "RefreshInterval";
            this.RefreshInterval.Size = new System.Drawing.Size(72, 20);
            this.RefreshInterval.TabIndex = 11;
            this.RefreshInterval.Value = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.RefreshInterval.ValueChanged += new System.EventHandler(this.RefreshInterval_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Paint method";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PaintMethodSelector
            // 
            this.PaintMethodSelector.BackColor = System.Drawing.Color.Snow;
            this.PaintMethodSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PaintMethodSelector.FormattingEnabled = true;
            this.PaintMethodSelector.Items.AddRange(new object[] {
            "Without double buffer",
            "Built-in double buffer ",
            "Built-in optimized double buffer",
            "Manual double buffer 1.1",
            "Manual double buffer 2.0"});
            this.PaintMethodSelector.Location = new System.Drawing.Point(5, 26);
            this.PaintMethodSelector.Name = "PaintMethodSelector";
            this.PaintMethodSelector.Size = new System.Drawing.Size(165, 21);
            this.PaintMethodSelector.TabIndex = 9;
            this.PaintMethodSelector.SelectedIndexChanged += new System.EventHandler(this.PaintMethodSelector_SelectedIndexChanged);
            // 
            // DoubleBufferControl1
            // 
            this.DoubleBufferControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DoubleBufferControl1.BackColor = System.Drawing.Color.Black;
            this.DoubleBufferControl1.GraphicTest = DoubleBufferExample.DoubleBufferControl.GraphicTestMethods.DrawTest;
            this.DoubleBufferControl1.Location = new System.Drawing.Point(176, 0);
            this.DoubleBufferControl1.Name = "DoubleBufferControl1";
            this.DoubleBufferControl1.PaintMethod = DoubleBufferExample.DoubleBufferControl.DoubleBufferMethod.NoDoubleBuffer;
            this.DoubleBufferControl1.Size = new System.Drawing.Size(372, 270);
            this.DoubleBufferControl1.TabIndex = 0;
            this.DoubleBufferControl1.Text = "doubleBufferControl1";
            // 
            // Example
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OldLace;
            this.ClientSize = new System.Drawing.Size(546, 268);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.StopBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TestMethodSelector);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RefreshInterval);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PaintMethodSelector);
            this.Controls.Add(this.DoubleBufferControl1);
            this.Name = "Example";
            this.Text = "Double Buffer Example";
            ((System.ComponentModel.ISupportInitialize)(this.RefreshInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private DoubleBufferControl DoubleBufferControl1;
        private System.Windows.Forms.Timer RefreshTimer;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.Button StopBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox TestMethodSelector;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown RefreshInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox PaintMethodSelector;
	}
}