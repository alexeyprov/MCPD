namespace ImageListVisualizer
{
	partial class ImageListForm
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
			this.lvwData = new System.Windows.Forms.ListView();
			this.imlPictures = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// lvwData
			// 
			this.lvwData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvwData.LargeImageList = this.imlPictures;
			this.lvwData.Location = new System.Drawing.Point(0, 0);
			this.lvwData.Name = "lvwData";
			this.lvwData.Size = new System.Drawing.Size(292, 273);
			this.lvwData.SmallImageList = this.imlPictures;
			this.lvwData.TabIndex = 0;
			this.lvwData.UseCompatibleStateImageBehavior = false;
			// 
			// imlPictures
			// 
			this.imlPictures.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imlPictures.ImageSize = new System.Drawing.Size(16, 16);
			this.imlPictures.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// ImageListForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.lvwData);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(300, 300);
			this.Name = "ImageListForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Image List";
			this.Load += new System.EventHandler(this.ImageListForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lvwData;
		private System.Windows.Forms.ImageList imlPictures;
	}
}