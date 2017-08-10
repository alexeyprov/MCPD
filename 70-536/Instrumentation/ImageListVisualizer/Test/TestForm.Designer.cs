namespace Test
{
	partial class TestForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestForm));
			this.imlData = new System.Windows.Forms.ImageList(this.components);
			this.btnTest = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// imlData
			// 
			this.imlData.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlData.ImageStream")));
			this.imlData.TransparentColor = System.Drawing.Color.Transparent;
			this.imlData.Images.SetKeyName(0, "StatusCtl.bmp");
			this.imlData.Images.SetKeyName(1, "TransView.bmp");
			// 
			// btnTest
			// 
			this.btnTest.Location = new System.Drawing.Point(12, 12);
			this.btnTest.Name = "btnTest";
			this.btnTest.Size = new System.Drawing.Size(75, 23);
			this.btnTest.TabIndex = 0;
			this.btnTest.Text = "&Test";
			this.btnTest.UseVisualStyleBackColor = true;
			this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
			// 
			// TestForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.btnTest);
			this.Name = "TestForm";
			this.Text = "Visualizer Test Form";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ImageList imlData;
		private System.Windows.Forms.Button btnTest;
	}
}

