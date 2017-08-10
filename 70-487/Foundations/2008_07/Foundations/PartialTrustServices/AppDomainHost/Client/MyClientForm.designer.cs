//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

partial class MyClientForm
{

   #region Windows Form Designer generated code

   /// <summary>
   /// Required method for Designer support - do not modify
   /// the contents of this method with the code editor.
   /// </summary>
   private void InitializeComponent()
   {
      System.Windows.Forms.Button callButton;
      System.Windows.Forms.GroupBox endpointsGroup;
      this.m_PartialTrustRadioButton = new System.Windows.Forms.RadioButton();
      this.m_InsufficientTrustRadioButton = new System.Windows.Forms.RadioButton();
      this.m_PermissionSetFileRadioButton = new System.Windows.Forms.RadioButton();
      this.m_NamedPermissionSetRadioButton = new System.Windows.Forms.RadioButton();
      this.m_FullTrustRadioButton = new System.Windows.Forms.RadioButton();
      this.m_DefaultRadioButton = new System.Windows.Forms.RadioButton();
      this.m_RadioButton1 = new System.Windows.Forms.RadioButton();
      callButton = new System.Windows.Forms.Button();
      endpointsGroup = new System.Windows.Forms.GroupBox();
      endpointsGroup.SuspendLayout();
      this.SuspendLayout();
      // 
      // callButton
      // 
      callButton.Location = new System.Drawing.Point(182,18);
      callButton.Name = "callButton";
      callButton.Size = new System.Drawing.Size(75,23);
      callButton.TabIndex = 0;
      callButton.Text = "Call Service";
      callButton.Click += new System.EventHandler(this.OnCall);
      // 
      // endpointsGroup
      // 
      endpointsGroup.Controls.Add(this.m_PartialTrustRadioButton);
      endpointsGroup.Controls.Add(this.m_InsufficientTrustRadioButton);
      endpointsGroup.Controls.Add(this.m_PermissionSetFileRadioButton);
      endpointsGroup.Controls.Add(this.m_NamedPermissionSetRadioButton);
      endpointsGroup.Controls.Add(this.m_FullTrustRadioButton);
      endpointsGroup.Controls.Add(this.m_DefaultRadioButton);
      endpointsGroup.Controls.Add(this.m_RadioButton1);
      endpointsGroup.Location = new System.Drawing.Point(12,12);
      endpointsGroup.Name = "endpointsGroup";
      endpointsGroup.Size = new System.Drawing.Size(148,166);
      endpointsGroup.TabIndex = 1;
      endpointsGroup.TabStop = false;
      endpointsGroup.Text = "Target Endpoint";
      // 
      // m_PartialTrustRadioButton
      // 
      this.m_PartialTrustRadioButton.AutoSize = true;
      this.m_PartialTrustRadioButton.Location = new System.Drawing.Point(6,65);
      this.m_PartialTrustRadioButton.Name = "m_PartialTrustRadioButton";
      this.m_PartialTrustRadioButton.Size = new System.Drawing.Size(81,17);
      this.m_PartialTrustRadioButton.TabIndex = 5;
      this.m_PartialTrustRadioButton.TabStop = true;
      this.m_PartialTrustRadioButton.Text = "Partial Trust";
      this.m_PartialTrustRadioButton.UseVisualStyleBackColor = true;
      this.m_PartialTrustRadioButton.CheckedChanged += new System.EventHandler(this.OnCheckedChanged);
      // 
      // m_InsufficientTrustRadioButton
      // 
      this.m_InsufficientTrustRadioButton.AutoSize = true;
      this.m_InsufficientTrustRadioButton.Location = new System.Drawing.Point(6,88);
      this.m_InsufficientTrustRadioButton.Name = "m_InsufficientTrustRadioButton";
      this.m_InsufficientTrustRadioButton.Size = new System.Drawing.Size(103,17);
      this.m_InsufficientTrustRadioButton.TabIndex = 4;
      this.m_InsufficientTrustRadioButton.TabStop = true;
      this.m_InsufficientTrustRadioButton.Text = "Insufficient Trust";
      this.m_InsufficientTrustRadioButton.UseVisualStyleBackColor = true;
      this.m_InsufficientTrustRadioButton.CheckedChanged += new System.EventHandler(this.OnCheckedChanged);
      // 
      // m_PermissionSetFileRadioButton
      // 
      this.m_PermissionSetFileRadioButton.AutoSize = true;
      this.m_PermissionSetFileRadioButton.Location = new System.Drawing.Point(6,111);
      this.m_PermissionSetFileRadioButton.Name = "m_PermissionSetFileRadioButton";
      this.m_PermissionSetFileRadioButton.Size = new System.Drawing.Size(113,17);
      this.m_PermissionSetFileRadioButton.TabIndex = 3;
      this.m_PermissionSetFileRadioButton.TabStop = true;
      this.m_PermissionSetFileRadioButton.Text = "Permission Set File";
      this.m_PermissionSetFileRadioButton.UseVisualStyleBackColor = true;
      this.m_PermissionSetFileRadioButton.CheckedChanged += new System.EventHandler(this.OnCheckedChanged);
      // 
      // m_NamedPermissionSetRadioButton
      // 
      this.m_NamedPermissionSetRadioButton.AutoSize = true;
      this.m_NamedPermissionSetRadioButton.Location = new System.Drawing.Point(6,134);
      this.m_NamedPermissionSetRadioButton.Name = "m_NamedPermissionSetRadioButton";
      this.m_NamedPermissionSetRadioButton.Size = new System.Drawing.Size(131,17);
      this.m_NamedPermissionSetRadioButton.TabIndex = 2;
      this.m_NamedPermissionSetRadioButton.Text = "Named Permission Set";
      this.m_NamedPermissionSetRadioButton.UseVisualStyleBackColor = true;
      this.m_NamedPermissionSetRadioButton.CheckedChanged += new System.EventHandler(this.OnCheckedChanged);
      // 
      // m_FullTrustRadioButton
      // 
      this.m_FullTrustRadioButton.AutoSize = true;
      this.m_FullTrustRadioButton.Location = new System.Drawing.Point(6,42);
      this.m_FullTrustRadioButton.Name = "m_FullTrustRadioButton";
      this.m_FullTrustRadioButton.Size = new System.Drawing.Size(68,17);
      this.m_FullTrustRadioButton.TabIndex = 1;
      this.m_FullTrustRadioButton.TabStop = true;
      this.m_FullTrustRadioButton.Text = "Full Trust";
      this.m_FullTrustRadioButton.UseVisualStyleBackColor = true;
      this.m_FullTrustRadioButton.CheckedChanged += new System.EventHandler(this.OnCheckedChanged);
      // 
      // m_DefaultRadioButton
      // 
      this.m_DefaultRadioButton.AutoSize = true;
      this.m_DefaultRadioButton.Checked = true;
      this.m_DefaultRadioButton.Location = new System.Drawing.Point(6,19);
      this.m_DefaultRadioButton.Name = "m_DefaultRadioButton";
      this.m_DefaultRadioButton.Size = new System.Drawing.Size(105,17);
      this.m_DefaultRadioButton.TabIndex = 0;
      this.m_DefaultRadioButton.TabStop = true;
      this.m_DefaultRadioButton.Text = "Default Full Trust";
      this.m_DefaultRadioButton.UseVisualStyleBackColor = true;
      this.m_DefaultRadioButton.CheckedChanged += new System.EventHandler(this.OnCheckedChanged);
      // 
      // m_RadioButton1
      // 
      this.m_RadioButton1.AutoSize = true;
      this.m_RadioButton1.Location = new System.Drawing.Point(6,19);
      this.m_RadioButton1.Name = "m_RadioButton1";
      this.m_RadioButton1.Size = new System.Drawing.Size(105,17);
      this.m_RadioButton1.TabIndex = 0;
      this.m_RadioButton1.TabStop = true;
      this.m_RadioButton1.Text = "Default Full Trust";
      this.m_RadioButton1.UseVisualStyleBackColor = true;
      // 
      // MyClientForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(269,196);
      this.Controls.Add(endpointsGroup);
      this.Controls.Add(callButton);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MyClientForm";
      this.Text = "Partial Trust Service - AppDomainHost";
      endpointsGroup.ResumeLayout(false);
      endpointsGroup.PerformLayout();
      this.ResumeLayout(false);

   }

   #endregion

   private System.Windows.Forms.RadioButton m_RadioButton1;
   private System.Windows.Forms.RadioButton m_PartialTrustRadioButton;
   private System.Windows.Forms.RadioButton m_InsufficientTrustRadioButton;
   private System.Windows.Forms.RadioButton m_PermissionSetFileRadioButton;
   private System.Windows.Forms.RadioButton m_NamedPermissionSetRadioButton;
   private System.Windows.Forms.RadioButton m_FullTrustRadioButton;
   private System.Windows.Forms.RadioButton m_DefaultRadioButton;
}

