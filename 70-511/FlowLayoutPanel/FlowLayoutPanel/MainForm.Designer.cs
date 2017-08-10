namespace FlowLayoutPanel
{
	partial class MainForm
	{

#region " Windows Form Designer generated code "

    //Form overrides dispose to clean up the component list.
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (components != null)
            {
                components.Dispose();
            }
        }
        base.Dispose(disposing);
    }

	private System.Windows.Forms.FlowLayoutPanel FlowLayoutPanel1;
	private System.Windows.Forms.CheckBox wrapContentsCheckBox;
	private System.Windows.Forms.RadioButton flowTopDownBtn;
	private System.Windows.Forms.RadioButton flowBottomUpBtn;
	private System.Windows.Forms.RadioButton flowLeftToRight;
	private System.Windows.Forms.RadioButton flowRightToLeftBtn;
	private System.Windows.Forms.Button Button1;
	private System.Windows.Forms.Button Button2;
	private System.Windows.Forms.Button Button3;
	private System.Windows.Forms.Button Button4;

	//Required by the Windows Form Designer
    private System.ComponentModel.IContainer components;

    //NOTE: The following procedure is required by the Windows Form Designer
    //It can be modified using the Windows Form Designer.  
    //Do not modify it using the code editor.
    [System.Diagnostics.DebuggerNonUserCode]
    private void InitializeComponent()
    {
		this.FlowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
		this.Button1 = new System.Windows.Forms.Button();
		this.Button2 = new System.Windows.Forms.Button();
		this.Button3 = new System.Windows.Forms.Button();
		this.Button4 = new System.Windows.Forms.Button();
		this.wrapContentsCheckBox = new System.Windows.Forms.CheckBox();
		this.flowTopDownBtn = new System.Windows.Forms.RadioButton();
		this.flowBottomUpBtn = new System.Windows.Forms.RadioButton();
		this.flowLeftToRight = new System.Windows.Forms.RadioButton();
		this.flowRightToLeftBtn = new System.Windows.Forms.RadioButton();
		this.cbButtonVisible = new System.Windows.Forms.CheckBox();
		this.FlowLayoutPanel1.SuspendLayout();
		this.SuspendLayout();
		// 
		// FlowLayoutPanel1
		// 
		this.FlowLayoutPanel1.Controls.Add(this.Button1);
		this.FlowLayoutPanel1.Controls.Add(this.Button2);
		this.FlowLayoutPanel1.Controls.Add(this.Button3);
		this.FlowLayoutPanel1.Controls.Add(this.Button4);
		this.FlowLayoutPanel1.Location = new System.Drawing.Point(47, 55);
		this.FlowLayoutPanel1.Name = "FlowLayoutPanel1";
		this.FlowLayoutPanel1.Size = new System.Drawing.Size(200, 100);
		this.FlowLayoutPanel1.TabIndex = 0;
		// 
		// Button1
		// 
		this.Button1.Location = new System.Drawing.Point(3, 3);
		this.Button1.Name = "Button1";
		this.Button1.Size = new System.Drawing.Size(75, 23);
		this.Button1.TabIndex = 0;
		this.Button1.Text = "Button1";
		// 
		// Button2
		// 
		this.Button2.Location = new System.Drawing.Point(84, 3);
		this.Button2.Name = "Button2";
		this.Button2.Size = new System.Drawing.Size(75, 23);
		this.Button2.TabIndex = 1;
		this.Button2.Text = "Button2";
		// 
		// Button3
		// 
		this.Button3.Location = new System.Drawing.Point(3, 32);
		this.Button3.Name = "Button3";
		this.Button3.Size = new System.Drawing.Size(75, 23);
		this.Button3.TabIndex = 2;
		this.Button3.Text = "Button3";
		// 
		// Button4
		// 
		this.Button4.Location = new System.Drawing.Point(84, 32);
		this.Button4.Name = "Button4";
		this.Button4.Size = new System.Drawing.Size(75, 23);
		this.Button4.TabIndex = 3;
		this.Button4.Text = "Button4";
		// 
		// wrapContentsCheckBox
		// 
		this.wrapContentsCheckBox.Location = new System.Drawing.Point(46, 162);
		this.wrapContentsCheckBox.Name = "wrapContentsCheckBox";
		this.wrapContentsCheckBox.Size = new System.Drawing.Size(104, 24);
		this.wrapContentsCheckBox.TabIndex = 1;
		this.wrapContentsCheckBox.Text = "Wrap Contents";
		this.wrapContentsCheckBox.CheckedChanged += new System.EventHandler(this.wrapContentsCheckBox_CheckedChanged);
		// 
		// flowTopDownBtn
		// 
		this.flowTopDownBtn.Location = new System.Drawing.Point(45, 193);
		this.flowTopDownBtn.Name = "flowTopDownBtn";
		this.flowTopDownBtn.Size = new System.Drawing.Size(104, 24);
		this.flowTopDownBtn.TabIndex = 2;
		this.flowTopDownBtn.Text = "Flow TopDown";
		this.flowTopDownBtn.CheckedChanged += new System.EventHandler(this.flowTopDownBtn_CheckedChanged);
		// 
		// flowBottomUpBtn
		// 
		this.flowBottomUpBtn.Location = new System.Drawing.Point(44, 224);
		this.flowBottomUpBtn.Name = "flowBottomUpBtn";
		this.flowBottomUpBtn.Size = new System.Drawing.Size(104, 24);
		this.flowBottomUpBtn.TabIndex = 3;
		this.flowBottomUpBtn.Text = "Flow BottomUp";
		this.flowBottomUpBtn.CheckedChanged += new System.EventHandler(this.flowBottomUpBtn_CheckedChanged);
		// 
		// flowLeftToRight
		// 
		this.flowLeftToRight.Location = new System.Drawing.Point(156, 193);
		this.flowLeftToRight.Name = "flowLeftToRight";
		this.flowLeftToRight.Size = new System.Drawing.Size(132, 24);
		this.flowLeftToRight.TabIndex = 4;
		this.flowLeftToRight.Text = "Flow LeftToRight";
		this.flowLeftToRight.CheckedChanged += new System.EventHandler(this.flowLeftToRight_CheckedChanged);
		// 
		// flowRightToLeftBtn
		// 
		this.flowRightToLeftBtn.Location = new System.Drawing.Point(155, 224);
		this.flowRightToLeftBtn.Name = "flowRightToLeftBtn";
		this.flowRightToLeftBtn.Size = new System.Drawing.Size(133, 24);
		this.flowRightToLeftBtn.TabIndex = 5;
		this.flowRightToLeftBtn.Text = "Flow RightToLeft";
		this.flowRightToLeftBtn.CheckedChanged += new System.EventHandler(this.flowRightToLeftBtn_CheckedChanged);
		// 
		// cbButtonVisible
		// 
		this.cbButtonVisible.Checked = true;
		this.cbButtonVisible.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbButtonVisible.Location = new System.Drawing.Point(156, 162);
		this.cbButtonVisible.Name = "cbButtonVisible";
		this.cbButtonVisible.Size = new System.Drawing.Size(104, 24);
		this.cbButtonVisible.TabIndex = 1;
		this.cbButtonVisible.Text = "Button 2 Visible";
		this.cbButtonVisible.CheckedChanged += new System.EventHandler(this.cbButtonVisible_CheckedChanged);
		// 
		// MainForm
		// 
		this.ClientSize = new System.Drawing.Size(341, 266);
		this.Controls.Add(this.flowRightToLeftBtn);
		this.Controls.Add(this.flowLeftToRight);
		this.Controls.Add(this.flowBottomUpBtn);
		this.Controls.Add(this.flowTopDownBtn);
		this.Controls.Add(this.cbButtonVisible);
		this.Controls.Add(this.wrapContentsCheckBox);
		this.Controls.Add(this.FlowLayoutPanel1);
		this.Name = "MainForm";
		this.Text = "Main Form";
		this.FlowLayoutPanel1.ResumeLayout(false);
		this.ResumeLayout(false);

	}

#endregion

	private System.Windows.Forms.CheckBox cbButtonVisible;

	}
}

