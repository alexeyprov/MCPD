//2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System.Drawing;
namespace ServiceModelEx
{
   partial class ExplorerForm
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise,false.</param>
      protected override void Dispose(bool disposing)
      {
         if(disposing &&(components != null))
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
         System.Windows.Forms.Label solutionLabel;
         System.Windows.Forms.ToolStripMenuItem metadataMenu;
         System.Windows.Forms.ToolStripMenuItem exploreMenuItem;
         System.Windows.Forms.ToolStripMenuItem proxyMenuItem;
         System.Windows.Forms.ToolStripMenuItem helpMenu;
         System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExplorerForm));
         System.Windows.Forms.ToolStripMenuItem serviceBusToolStripMenuItem;
         System.Windows.Forms.ToolStripMenuItem junctionsMenuItem;
         System.Windows.Forms.ToolStripSeparator separator;
         System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
         this.m_LogonMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.m_ExploreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.m_NewRouterMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.m_NewQueueMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.m_DeleteAllRoutersMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.m_DeleteAllQueuesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.m_DeleteAllRutersAndQueuesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.m_MainMenu = new System.Windows.Forms.MenuStrip();
         this.m_ServiceBusTree = new System.Windows.Forms.TreeView();
         this.m_ExploreButton = new System.Windows.Forms.Button();
         this.m_SolutionTextBox = new System.Windows.Forms.TextBox();
         this.allQueuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.allQueuesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
         this.allPoliciesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.lononTimer = new System.Windows.Forms.Timer(this.components);
         this.m_BlankViewControl = new ServiceModelEx.NodeViewControl();
         this.m_SolutionViewControl = new ServiceModelEx.SolutionViewControl();
         this.m_RouterSubscriberViewControl = new ServiceModelEx.RouterSubscriberViewControl();
         this.m_EndpointViewControl = new ServiceModelEx.EndpointViewControl();
         this.m_QueueViewControl = new ServiceModelEx.QueueViewControl();
         this.m_RouterViewControl = new ServiceModelEx.RouterViewControl();
         solutionLabel = new System.Windows.Forms.Label();
         metadataMenu = new System.Windows.Forms.ToolStripMenuItem();
         exploreMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         proxyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         helpMenu = new System.Windows.Forms.ToolStripMenuItem();
         aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         serviceBusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         junctionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         separator = new System.Windows.Forms.ToolStripSeparator();
         helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.m_MainMenu.SuspendLayout();
         this.SuspendLayout();
         // 
         // solutionLabel
         // 
         solutionLabel.AutoSize = true;
         solutionLabel.Location = new System.Drawing.Point(12,35);
         solutionLabel.Name = "solutionLabel";
         solutionLabel.Size = new System.Drawing.Size(48,13);
         solutionLabel.TabIndex = 3;
         solutionLabel.Text = "Solution:";
         // 
         // metadataMenu
         // 
         metadataMenu.Name = "metadataMenu";
         metadataMenu.Size = new System.Drawing.Size(76,20);
         metadataMenu.Text = "Service Bus";
         // 
         // exploreMenuItem
         // 
         exploreMenuItem.Image = global::ServiceModelEx.Properties.Resources.searchweb;
         exploreMenuItem.Name = "exploreMenuItem";
         exploreMenuItem.Size = new System.Drawing.Size(152,22);
         exploreMenuItem.Text = "Explore";
         exploreMenuItem.Click += new System.EventHandler(this.OnExplore);
         // 
         // proxyMenuItem
         // 
         proxyMenuItem.Name = "proxyMenuItem";
         proxyMenuItem.Size = new System.Drawing.Size(152,22);
         // 
         // helpMenu
         // 
         helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            aboutMenuItem});
         helpMenu.Name = "helpMenu";
         helpMenu.Size = new System.Drawing.Size(41,20);
         helpMenu.Text = "Help";
         // 
         // aboutMenuItem
         // 
         aboutMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutMenuItem.Image")));
         aboutMenuItem.Name = "aboutMenuItem";
         aboutMenuItem.Size = new System.Drawing.Size(105,22);
         aboutMenuItem.Text = "About";
         aboutMenuItem.Click += new System.EventHandler(this.OnAbout);
         // 
         // serviceBusToolStripMenuItem
         // 
         serviceBusToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_LogonMenuItem,
            this.m_ExploreToolStripMenuItem});
         serviceBusToolStripMenuItem.Name = "serviceBusToolStripMenuItem";
         serviceBusToolStripMenuItem.Size = new System.Drawing.Size(76,20);
         serviceBusToolStripMenuItem.Text = "Service Bus";
         // 
         // m_LogonMenuItem
         // 
         this.m_LogonMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("m_LogonMenuItem.Image")));
         this.m_LogonMenuItem.Name = "m_LogonMenuItem";
         this.m_LogonMenuItem.Size = new System.Drawing.Size(116,22);
         this.m_LogonMenuItem.Text = "Logon...";
         this.m_LogonMenuItem.Click += new System.EventHandler(this.OnLogon);
         // 
         // m_ExploreToolStripMenuItem
         // 
         this.m_ExploreToolStripMenuItem.Image = global::ServiceModelEx.Properties.Resources.searchweb1;
         this.m_ExploreToolStripMenuItem.Name = "m_ExploreToolStripMenuItem";
         this.m_ExploreToolStripMenuItem.Size = new System.Drawing.Size(116,22);
         this.m_ExploreToolStripMenuItem.Text = "Explore";
         this.m_ExploreToolStripMenuItem.Click += new System.EventHandler(this.OnExplore);
         // 
         // junctionsMenuItem
         // 
         junctionsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_NewRouterMenuItem,
            this.m_NewQueueMenuItem,
            separator,
            this.m_DeleteAllRoutersMenuItem,
            this.m_DeleteAllQueuesMenuItem,
            this.m_DeleteAllRutersAndQueuesMenuItem});
         junctionsMenuItem.Name = "junctionsMenuItem";
         junctionsMenuItem.Size = new System.Drawing.Size(64,20);
         junctionsMenuItem.Text = "Junctions";
         // 
         // m_NewRouterMenuItem
         // 
         this.m_NewRouterMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("m_NewRouterMenuItem.Image")));
         this.m_NewRouterMenuItem.Name = "m_NewRouterMenuItem";
         this.m_NewRouterMenuItem.Size = new System.Drawing.Size(223,22);
         this.m_NewRouterMenuItem.Text = "New Router...";
         this.m_NewRouterMenuItem.Click += new System.EventHandler(this.OnNewRouter);
         // 
         // m_NewQueueMenuItem
         // 
         this.m_NewQueueMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("m_NewQueueMenuItem.Image")));
         this.m_NewQueueMenuItem.Name = "m_NewQueueMenuItem";
         this.m_NewQueueMenuItem.Size = new System.Drawing.Size(223,22);
         this.m_NewQueueMenuItem.Text = "New Queue...";
         this.m_NewQueueMenuItem.Click += new System.EventHandler(this.OnNewQueue);
         // 
         // separator
         // 
         separator.Name = "separator";
         separator.Size = new System.Drawing.Size(220,6);
         // 
         // m_DeleteAllRoutersMenuItem
         // 
         this.m_DeleteAllRoutersMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("m_DeleteAllRoutersMenuItem.Image")));
         this.m_DeleteAllRoutersMenuItem.Name = "m_DeleteAllRoutersMenuItem";
         this.m_DeleteAllRoutersMenuItem.Size = new System.Drawing.Size(223,22);
         this.m_DeleteAllRoutersMenuItem.Text = "Delete All Routers";
         this.m_DeleteAllRoutersMenuItem.Click += new System.EventHandler(this.OnDeleteAllRouters);
         // 
         // m_DeleteAllQueuesMenuItem
         // 
         this.m_DeleteAllQueuesMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("m_DeleteAllQueuesMenuItem.Image")));
         this.m_DeleteAllQueuesMenuItem.Name = "m_DeleteAllQueuesMenuItem";
         this.m_DeleteAllQueuesMenuItem.Size = new System.Drawing.Size(223,22);
         this.m_DeleteAllQueuesMenuItem.Text = "Delete All Queues";
         this.m_DeleteAllQueuesMenuItem.Click += new System.EventHandler(this.OnDeleteAllQueues);
         // 
         // m_DeleteAllRutersAndQueuesMenuItem
         // 
         this.m_DeleteAllRutersAndQueuesMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("m_DeleteAllRutersAndQueuesMenuItem.Image")));
         this.m_DeleteAllRutersAndQueuesMenuItem.Name = "m_DeleteAllRutersAndQueuesMenuItem";
         this.m_DeleteAllRutersAndQueuesMenuItem.Size = new System.Drawing.Size(223,22);
         this.m_DeleteAllRutersAndQueuesMenuItem.Text = "Delete All Routers and Queues";
         this.m_DeleteAllRutersAndQueuesMenuItem.Click += new System.EventHandler(this.OnDeleteAllRoutersQueues);
         // 
         // helpToolStripMenuItem
         // 
         helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
         helpToolStripMenuItem.Name = "helpToolStripMenuItem";
         helpToolStripMenuItem.Size = new System.Drawing.Size(41,20);
         helpToolStripMenuItem.Text = "Help";
         // 
         // aboutToolStripMenuItem
         // 
         this.aboutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutToolStripMenuItem.Image")));
         this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
         this.aboutToolStripMenuItem.Size = new System.Drawing.Size(114,22);
         this.aboutToolStripMenuItem.Text = "About...";
         this.aboutToolStripMenuItem.Click += new System.EventHandler(this.OnAbout);
         // 
         // m_MainMenu
         // 
         this.m_MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            serviceBusToolStripMenuItem,
            junctionsMenuItem,
            helpToolStripMenuItem});
         this.m_MainMenu.Location = new System.Drawing.Point(0,0);
         this.m_MainMenu.Name = "m_MainMenu";
         this.m_MainMenu.Size = new System.Drawing.Size(666,24);
         this.m_MainMenu.TabIndex = 17;
         this.m_MainMenu.Text = "menuStrip1";
         // 
         // m_ServiceBusTree
         // 
         this.m_ServiceBusTree.AllowDrop = true;
         this.m_ServiceBusTree.HideSelection = false;
         this.m_ServiceBusTree.ImeMode = System.Windows.Forms.ImeMode.NoControl;
         this.m_ServiceBusTree.Location = new System.Drawing.Point(12,82);
         this.m_ServiceBusTree.Name = "m_ServiceBusTree";
         this.m_ServiceBusTree.Size = new System.Drawing.Size(242,339);
         this.m_ServiceBusTree.TabIndex = 0;
         this.m_ServiceBusTree.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
         this.m_ServiceBusTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnItemSelected);
         this.m_ServiceBusTree.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnterOver);
         this.m_ServiceBusTree.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.OnItemDrag);
         this.m_ServiceBusTree.DragOver += new System.Windows.Forms.DragEventHandler(this.OnDragEnterOver);
         // 
         // m_ExploreButton
         // 
         this.m_ExploreButton.Location = new System.Drawing.Point(579,53);
         this.m_ExploreButton.Name = "m_ExploreButton";
         this.m_ExploreButton.Size = new System.Drawing.Size(75,23);
         this.m_ExploreButton.TabIndex = 2;
         this.m_ExploreButton.Text = "Explore";
         this.m_ExploreButton.UseVisualStyleBackColor = true;
         this.m_ExploreButton.Click += new System.EventHandler(this.OnExplore);
         // 
         // m_SolutionTextBox
         // 
         this.m_SolutionTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
         this.m_SolutionTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
         this.m_SolutionTextBox.Location = new System.Drawing.Point(12,53);
         this.m_SolutionTextBox.Name = "m_SolutionTextBox";
         this.m_SolutionTextBox.Size = new System.Drawing.Size(561,20);
         this.m_SolutionTextBox.TabIndex = 4;
         this.m_SolutionTextBox.Text = "MySolution";
         // 
         // allQueuesToolStripMenuItem
         // 
         this.allQueuesToolStripMenuItem.Name = "allQueuesToolStripMenuItem";
         this.allQueuesToolStripMenuItem.Size = new System.Drawing.Size(152,22);
         this.allQueuesToolStripMenuItem.Text = "All Routers";
         // 
         // allQueuesToolStripMenuItem1
         // 
         this.allQueuesToolStripMenuItem1.Name = "allQueuesToolStripMenuItem1";
         this.allQueuesToolStripMenuItem1.Size = new System.Drawing.Size(152,22);
         this.allQueuesToolStripMenuItem1.Text = "All Queues";
         // 
         // allPoliciesToolStripMenuItem
         // 
         this.allPoliciesToolStripMenuItem.Name = "allPoliciesToolStripMenuItem";
         this.allPoliciesToolStripMenuItem.Size = new System.Drawing.Size(152,22);
         this.allPoliciesToolStripMenuItem.Text = "All Policies";
         // 
         // lononTimer
         // 
         this.lononTimer.Enabled = true;
         this.lononTimer.Interval = 500;
         this.lononTimer.Tick += new System.EventHandler(this.OnTimer);
         // 
         // m_BlankViewControl
         // 
         this.m_BlankViewControl.BackColor = System.Drawing.SystemColors.ControlDark;
         this.m_BlankViewControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.m_BlankViewControl.Location = new System.Drawing.Point(270,82);
         this.m_BlankViewControl.Name = "m_BlankViewControl";
         this.m_BlankViewControl.Size = new System.Drawing.Size(385,337);
         this.m_BlankViewControl.TabIndex = 19;
         this.m_BlankViewControl.Visible = false;
         // 
         // m_SolutionViewControl
         // 
         this.m_SolutionViewControl.BackColor = System.Drawing.SystemColors.Control;
         this.m_SolutionViewControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.m_SolutionViewControl.Location = new System.Drawing.Point(270,82);
         this.m_SolutionViewControl.Name = "m_SolutionViewControl";
         this.m_SolutionViewControl.Size = new System.Drawing.Size(385,337);
         this.m_SolutionViewControl.TabIndex = 12;
         this.m_SolutionViewControl.Visible = false;
         // 
         // m_RouterSubscriberViewControl
         // 
         this.m_RouterSubscriberViewControl.BackColor = System.Drawing.SystemColors.Control;
         this.m_RouterSubscriberViewControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.m_RouterSubscriberViewControl.Location = new System.Drawing.Point(270,82);
         this.m_RouterSubscriberViewControl.Name = "m_RouterSubscriberViewControl";
         this.m_RouterSubscriberViewControl.Size = new System.Drawing.Size(385,337);
         this.m_RouterSubscriberViewControl.TabIndex = 22;
         this.m_RouterSubscriberViewControl.Visible = false;
         // 
         // m_EndpointViewControl
         // 
         this.m_EndpointViewControl.BackColor = System.Drawing.SystemColors.Control;
         this.m_EndpointViewControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.m_EndpointViewControl.Location = new System.Drawing.Point(270,82);
         this.m_EndpointViewControl.Name = "m_EndpointViewControl";
         this.m_EndpointViewControl.Size = new System.Drawing.Size(385,337);
         this.m_EndpointViewControl.TabIndex = 21;
         this.m_EndpointViewControl.Visible = false;
         // 
         // m_QueueViewControl
         // 
         this.m_QueueViewControl.BackColor = System.Drawing.SystemColors.Control;
         this.m_QueueViewControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.m_QueueViewControl.Location = new System.Drawing.Point(270,82);
         this.m_QueueViewControl.Name = "m_QueueViewControl";
         this.m_QueueViewControl.Size = new System.Drawing.Size(385,337);
         this.m_QueueViewControl.TabIndex = 20;
         this.m_QueueViewControl.Visible = false;
         // 
         // m_RouterViewControl
         // 
         this.m_RouterViewControl.BackColor = System.Drawing.SystemColors.Control;
         this.m_RouterViewControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.m_RouterViewControl.Location = new System.Drawing.Point(270,82);
         this.m_RouterViewControl.Name = "m_RouterViewControl";
         this.m_RouterViewControl.Size = new System.Drawing.Size(385,337);
         this.m_RouterViewControl.TabIndex = 18;
         this.m_RouterViewControl.Visible = false;
         // 
         // ExplorerForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(666,433);
         this.Controls.Add(this.m_BlankViewControl);
         this.Controls.Add(this.m_SolutionViewControl);
         this.Controls.Add(this.m_SolutionTextBox);
         this.Controls.Add(solutionLabel);
         this.Controls.Add(this.m_ExploreButton);
         this.Controls.Add(this.m_ServiceBusTree);
         this.Controls.Add(this.m_MainMenu);
         this.Controls.Add(this.m_RouterSubscriberViewControl);
         this.Controls.Add(this.m_EndpointViewControl);
         this.Controls.Add(this.m_QueueViewControl);
         this.Controls.Add(this.m_RouterViewControl);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MainMenuStrip = this.m_MainMenu;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "ExplorerForm";
         this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
         this.Text = " IDesign Service Bus Explorer";
         this.m_MainMenu.ResumeLayout(false);
         this.m_MainMenu.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TreeView m_ServiceBusTree;
      private System.Windows.Forms.Button m_ExploreButton;
      private System.Windows.Forms.TextBox m_SolutionTextBox;


      private System.Windows.Forms.ToolStripMenuItem allQueuesToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem allQueuesToolStripMenuItem1;
      private System.Windows.Forms.ToolStripMenuItem allPoliciesToolStripMenuItem;
      private NodeViewControl m_BlankViewControl;
      private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
      private System.Windows.Forms.Timer lononTimer;
      private System.Windows.Forms.MenuStrip m_MainMenu;
      private System.Windows.Forms.ToolStripMenuItem m_LogonMenuItem;

      private RouterViewControl m_RouterViewControl;
      private QueueViewControl m_QueueViewControl;
      private EndpointViewControl m_EndpointViewControl;
      private RouterSubscriberViewControl m_RouterSubscriberViewControl;
      private SolutionViewControl m_SolutionViewControl;
      private System.Windows.Forms.ToolStripMenuItem m_ExploreToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem m_NewRouterMenuItem;
      private System.Windows.Forms.ToolStripMenuItem m_NewQueueMenuItem;
      private System.Windows.Forms.ToolStripMenuItem m_DeleteAllRoutersMenuItem;
      private System.Windows.Forms.ToolStripMenuItem m_DeleteAllQueuesMenuItem;
      private System.Windows.Forms.ToolStripMenuItem m_DeleteAllRutersAndQueuesMenuItem;
   }
}

