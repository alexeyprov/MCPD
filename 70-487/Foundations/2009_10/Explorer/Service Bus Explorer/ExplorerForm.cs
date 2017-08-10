//2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Collections.Generic;
using System.Windows.Forms;
using ServiceModelEx.Properties;
using System.ServiceModel.Description;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Threading;
using WinFormsEx;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.ServiceModel.Channels;
using Microsoft.ServiceBus;
using System.Drawing;

namespace ServiceModelEx
{
   partial class ExplorerForm : Form
   {
      public const int UnspecifiedSolutionIndex = 0;
      public const int SolutionIndex            = 1;
      public const int RouterIndex              = 2;
      public const int QueueIndex               = 3;
      public const int EventEndpointIndex       = 4;
      public const int EndpointIndex            = 5;
      public const int ServiceError             = 6;
      
      NodeViewControl m_CurrentViewControl;
      public Dictionary<string,ServiceBusGraph> Graphs = new Dictionary<string,ServiceBusGraph>();

      ServiceBusTreeNode m_DraggedNode;

      public string Solution
      {
         get
         {
            return m_SolutionTextBox.Text;
         }
      }

      public ExplorerForm()
      {
         InitializeComponent();
         m_ServiceBusTree.ImageList = new ImageList();
         m_ServiceBusTree.ImageList.Images.Add(Resources.UnspecifiedSolution);
         m_ServiceBusTree.ImageList.Images.Add(Resources.Solution);
         m_ServiceBusTree.ImageList.Images.Add(Resources.Router);
         m_ServiceBusTree.ImageList.Images.Add(Resources.Queue);
         m_ServiceBusTree.ImageList.Images.Add(Resources.EventEndpoint);
         m_ServiceBusTree.ImageList.Images.Add(Resources.Endpoint);
         m_ServiceBusTree.ImageList.Images.Add(Resources.ServiceError);

         m_CurrentViewControl = m_BlankViewControl;
         DisplayBlankControl();

         TreeNode blank = new SolutionTreeNode(this);
         m_ServiceBusTree.Nodes.Add(blank);

         SelectSolutionTextBox();
      }


      void AddNodesToTree(TreeView tree,ServiceBusNode[] nodes)
      {
         string solution = m_SolutionTextBox.Text;

         if(tree.Nodes[0].Text == "Unspecified Solution")
         {
            tree.Nodes.Clear();
         }
         else
         {
            foreach(TreeNode solutionNode in tree.Nodes)
            {
               if(solutionNode.Text == solution)
               {
                  tree.Nodes.Remove(solutionNode);
                  break;
               }
            }
         }
         TreeNode newSolutionNode = new SolutionTreeNode(this,solution);
         tree.Nodes.Add(newSolutionNode);

         tree.SelectedNode = newSolutionNode;
         tree.Focus();

         foreach(ServiceBusNode node in nodes)
         {
            AddNode(newSolutionNode,node);
         }
      }
      ServiceBusTreeNode MatchTreeNode(ServiceBusNode node)
      {
         if(node == null)
         {
            return new SolutionTreeNode(this,m_SolutionTextBox.Text);
         }
         if(node.Policy != null)
         {
            if(node.Policy is RouterPolicy)
            {
               return new RouterTreeNode(this,node,node.Name);
            }
            else
            {
               return new QueueTreeNode(this,node,node.Name);
            }
         }
         else
         {
            if(node.SubscribedTo != null)
            {
               return new RouterSubscriberTreeNode(this,node,node.Name);
            }
            else
            {
               return new EndpointTreeNode(this,node,node.Name);
            }
         }
      }      
                       
      void AddNode(TreeNode root,ServiceBusNode nodeToAdd)
      {
         TreeNode newTreeNode = MatchTreeNode(nodeToAdd);

         root.Nodes.Add(newTreeNode);
         if(nodeToAdd.Subscribers == null)
         {
            return;
         }
         foreach(ServiceBusNode subscriber in nodeToAdd.Subscribers)
         {
            AddNode(newTreeNode,subscriber);
         }
      }
      public ServiceBusTreeNode SelectedTreeNode
      {
         get
         {
            return m_ServiceBusTree.SelectedNode as ServiceBusTreeNode;
         }
         set
         {
            m_ServiceBusTree.SelectedNode = value;
            m_ServiceBusTree.Select();

            value.DisplayControl();
         }
      }
      public void OnExplore(object sender,EventArgs e)
      {
         Cursor currentCursor = Cursor.Current;
         Cursor.Current = Cursors.WaitCursor;

         m_ExploreToolStripMenuItem.Enabled = m_ExploreButton.Enabled = false;
         
         string solution = m_SolutionTextBox.Text;

         if(String.IsNullOrEmpty(solution))
         {
            MessageBox.Show("You need to provide a solution name","Service Bus Explorer",MessageBoxButtons.OK,MessageBoxIcon.Error);
            m_ExploreToolStripMenuItem.Enabled = m_ExploreButton.Enabled = true;

            Cursor.Current = currentCursor;

            return;
         }

         if(Graphs.ContainsKey(solution.ToLower()) == false)
         {
            LogonDialog dialog = new LogonDialog(m_SolutionTextBox.Text);
            dialog.ShowDialog();

            if(dialog.Password == null)
            {
               m_ExploreToolStripMenuItem.Enabled = m_ExploreButton.Enabled = true;
               Cursor.Current = currentCursor;

               return;
            }
            try
            {
               Graphs[solution.ToLower()] = new ServiceBusGraph(solution,dialog.Password);
            }
            catch(Exception exception)
            {
               MessageBox.Show("Invalid solution name: " + exception.Message,"Service Bus Explorer",MessageBoxButtons.OK,MessageBoxIcon.Error);
               m_ExploreToolStripMenuItem.Enabled = m_ExploreButton.Enabled = true;
               Cursor.Current = currentCursor;
               return;
            }
         }
          
         SplashScreen splash = new SplashScreen(Resources.Progress);         

         try
         {
            Application.DoEvents();

            ServiceBusNode[] nodes = Graphs[solution.ToLower()].Discover();

            AddNodesToTree(m_ServiceBusTree,nodes);

            DisplaySolutionControl(solution);
         }
         catch(Exception exception)
         {
            MessageBox.Show("Some error occurred discovering the solution: " + exception.Message,"Service Bus Explorer",MessageBoxButtons.OK,MessageBoxIcon.Error);

            for(int index = 0;index < m_ServiceBusTree.Nodes.Count;index++)
            {
               if(m_ServiceBusTree.Nodes[index].Text == solution)
               {
                  m_ServiceBusTree.Nodes.Add(new SolutionTreeNode(this,solution,ServiceError));
                  m_ServiceBusTree.Nodes.RemoveAt(index);
                  break;
               }
            }
         }
         finally
         {
            splash.Close();
            m_ExploreToolStripMenuItem.Enabled = m_ExploreButton.Enabled = true;
            Cursor.Current = currentCursor;
         }
      } 
      void OnItemSelected(object sender,TreeViewEventArgs treeEventArgs)
      {
         ServiceBusTreeNode node = treeEventArgs.Node as ServiceBusTreeNode;

         if(node != null)
         {
            TreeNode solutionNode = node;
            if(solutionNode.Parent != null)
            {
               while(solutionNode.Parent is SolutionTreeNode == false)
               {
                  solutionNode = solutionNode.Parent;
               }

               m_SolutionTextBox.Text = solutionNode.Parent.Text;
            }
         }
         node.DisplayControl();
      }
      void DisplayControl(NodeViewControl control)
      {
         m_CurrentViewControl.Visible = false;
         control.Visible = true;
         m_CurrentViewControl = control;
      }
      internal void DisplayBlankControl()
      {
         DisplayControl(m_BlankViewControl);
      }

      internal void DisplaySolutionControl(string solution)
      {
         if(solution == "Unspecified Solution")
         {
            DisplayBlankControl();
            return;
         }
         m_SolutionTextBox.Text = solution;
         m_SolutionViewControl.Refresh(solution);
         DisplayControl(m_SolutionViewControl);
      }
      internal void DisplayQueueControl(ServiceBusNode node,TransportClientEndpointBehavior credential)
      {
         m_QueueViewControl.Refresh(node,credential);
         DisplayControl(m_QueueViewControl);
      }
      internal void DisplayRouterSubscriberControl(ServiceBusNode node,TransportClientEndpointBehavior credential)
      {
         m_RouterSubscriberViewControl.Refresh(node,credential);
         DisplayControl(m_RouterSubscriberViewControl);
      }
      internal void DisplayEndpointControl(ServiceBusNode node,TransportClientEndpointBehavior credential)
      {
         m_EndpointViewControl.Refresh(node,credential);
         DisplayControl(m_EndpointViewControl);
      }
      internal void DisplayRouterControl(ServiceBusNode node,TransportClientEndpointBehavior credential)
      {
         m_RouterViewControl.Refresh(node,credential);
         DisplayControl(m_RouterViewControl);
      }
      void OnAbout(object sender,EventArgs e)
      {
         AboutBox about = new AboutBox();
         about.ShowDialog();
      }

      void OnLogon(object sender,EventArgs e)
      {
         LogonDialog dialog = new LogonDialog(m_SolutionTextBox.Text);
         dialog.ShowDialog();

         string solution = m_SolutionTextBox.Text;

         Graphs[solution.ToLower()] = new ServiceBusGraph(solution,dialog.Password);
      }

      void OnTimer(object sender,EventArgs e)
      {
         string solution = m_SolutionTextBox.Text;
         m_LogonMenuItem.Enabled = Graphs.ContainsKey(solution.ToLower()) == false;

         m_NewRouterMenuItem.Enabled = !m_LogonMenuItem.Enabled;
         m_NewQueueMenuItem.Enabled = !m_LogonMenuItem.Enabled;
         m_DeleteAllRoutersMenuItem.Enabled = !m_LogonMenuItem.Enabled;
         m_DeleteAllQueuesMenuItem.Enabled = !m_LogonMenuItem.Enabled;
         m_DeleteAllRutersAndQueuesMenuItem.Enabled = !m_LogonMenuItem.Enabled;
      }
      internal void SelectSolutionTextBox()
      {
         m_SolutionTextBox.Focus();
      }

      void OnNewRouter(object sender,EventArgs e)
      {
         string solution = m_SolutionTextBox.Text;
         NewRouterDialog dialog = new NewRouterDialog(solution);
         dialog.ShowDialog();

         if(dialog.Client != null)
         {
            OnExplore(this,EventArgs.Empty);
         }
      }

      void OnNewQueue(object sender,EventArgs e)
      {
         string solution = m_SolutionTextBox.Text;
         NewQueueDialog dialog = new NewQueueDialog(solution);
         dialog.ShowDialog();

         if(dialog.Client != null)
         {
            OnExplore(this,EventArgs.Empty);
         }
      }

      void OnDeleteAllRouters(object sender,EventArgs e)
      {       
         DialogResult result = MessageBox.Show("Are you sure you want to delete all routers? You will also lose all subscribers","Service Bus Explorer",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
         if(result == DialogResult.No)
         {
            return;
         }
         DeleteAllRouters();
         OnExplore(this,EventArgs.Empty);
      }
      void DeleteAllRouters()
      {
         foreach(TreeNode node in m_ServiceBusTree.Nodes)
         {
            DeleteRouters(node as ServiceBusTreeNode);
         }
      }
      void DeleteRouters(ServiceBusTreeNode treeNode)
      {
         if(treeNode.ServiceBusNode != null)
         {
            if(treeNode.ServiceBusNode.Policy != null)
            {
               if(treeNode.ServiceBusNode.Policy is RouterPolicy)
               {
                  string nodeAddress = treeNode.ServiceBusNode.Address;
                  nodeAddress = nodeAddress.Replace(@"https://",@"sb://");
                  nodeAddress = nodeAddress.Replace(@"http://",@"sb://");

                  TransportClientEndpointBehavior credential = Graphs[Solution.ToLower()].Credential;
                  Uri address = new Uri(nodeAddress);
                  try
                  {
                     RouterManagementClient.DeleteRouter(credential,address);
                  }
                  catch
                  {}
               }
            }
         }
         foreach(TreeNode node in treeNode.Nodes)
         {
            DeleteRouters(node as ServiceBusTreeNode);
         }
      }

      void OnDeleteAllQueues(object sender,EventArgs e)
      {       
         DialogResult result = MessageBox.Show("Are you sure you want to delete all queues?","Service Bus Explorer",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
         if(result == DialogResult.No)
         {
            return;
         }
         DeleteAllQueues();
         OnExplore(this,EventArgs.Empty);
      }
      void DeleteAllQueues()
      {
         foreach(TreeNode node in m_ServiceBusTree.Nodes)
         {
            DeleteQueues(node as ServiceBusTreeNode);
         }
      }
      void DeleteQueues(ServiceBusTreeNode treeNode)
      {
         if(treeNode.ServiceBusNode != null)
         {
            if(treeNode.ServiceBusNode.Policy != null)
            {
               if(treeNode.ServiceBusNode.Policy is QueuePolicy)
               {
                  string nodeAddress = treeNode.ServiceBusNode.Address;
                  nodeAddress = nodeAddress.Replace(@"https://",@"sb://");
                  nodeAddress = nodeAddress.Replace(@"http://",@"sb://");

                  TransportClientEndpointBehavior credential = Graphs[Solution.ToLower()].Credential;
                  Uri address = new Uri(nodeAddress);
                  try
                  {
                     QueueManagementClient.DeleteQueue(credential,address);
                  }
                  catch
                  {}
               }
            }
         }
         foreach(TreeNode node in treeNode.Nodes)
         {
            DeleteQueues(node as ServiceBusTreeNode);
         }
      }

      void OnDeleteAllRoutersQueues(object sender,EventArgs e)
      {
         DialogResult result = MessageBox.Show("Are you sure you want to delete all routers and queues?","Service Bus Explorer",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
         if(result == DialogResult.No)
         {
            return;
         }
         DeleteAllRouters();
         DeleteAllQueues();
         OnExplore(this,EventArgs.Empty);
      }
      void OnItemDrag(object sender,ItemDragEventArgs args)
      {
         m_DraggedNode = args.Item as ServiceBusTreeNode;

         if(m_DraggedNode != null)
         {
            if(m_DraggedNode.ServiceBusNode == null)
            {
               return;
            }
            if(m_DraggedNode.ServiceBusNode.Policy != null)//A router or a queue
            {
               Cursor.Current = Cursors.Hand;
               DoDragDrop(m_DraggedNode,DragDropEffects.Link);
            }
         }
      }

      void OnDragDrop(object sender,DragEventArgs args)
      {           
         Cursor.Current = Cursors.Default;
         Debug.Assert(m_DraggedNode != null);

         ServiceBusTreeNode targetNode = GetTargetNode(args);
                                    
         if(targetNode.ServiceBusNode == null)
         {
            return;
         }
         if(targetNode.ServiceBusNode.Policy != null)//A router or a queue
         {
            if(targetNode.ServiceBusNode.Policy is RouterPolicy)
            {
               Trace.WriteLine("Droped at: " + targetNode.Text);
               string draggedAddress = m_DraggedNode.ServiceBusNode.Address;
               draggedAddress = draggedAddress.Replace(@"https://",@"sb://");
               draggedAddress = draggedAddress.Replace(@"http://",@"sb://");

               string targetAddress = targetNode.ServiceBusNode.Address;
               targetAddress = targetAddress.Replace(@"https://",@"sb://");
               targetAddress = targetAddress.Replace(@"http://",@"sb://");
               
               TransportClientEndpointBehavior credential = Graphs[Solution.ToLower()].Credential;

               Uri draggedUri = new Uri(draggedAddress);
               Uri targetUri = new Uri(targetAddress);

               try
               {
                  RouterClient targetClient  = RouterManagementClient.GetRouter(credential,targetUri);
                  if(m_DraggedNode.ServiceBusNode.Policy is RouterPolicy)
                  {
                     RouterClient draggedClient = RouterManagementClient.GetRouter(credential,draggedUri);

                     draggedClient.SubscribeToRouter(targetClient,TimeSpan.MaxValue);
                  }
                  else
                  {
                     QueueClient draggedClient = QueueManagementClient.GetQueue(credential,draggedUri);

                     draggedClient.SubscribeToRouter(targetClient,TimeSpan.MaxValue);
                  }
                  OnExplore(this,EventArgs.Empty);

                  m_ServiceBusTree.SelectedNode = targetNode;
                  m_ServiceBusTree.Select();  
               }
               catch(Exception exception)
               {
                  MessageBox.Show("Unable to subscribe: " + exception.Message,"Service Bus Explorer",MessageBoxButtons.OK,MessageBoxIcon.Error);
               }
            }
         }
         m_DraggedNode = null;
		}

      void OnDragEnterOver(object sender,DragEventArgs args)
      {
         Debug.Assert(m_DraggedNode != null);
         
         ServiceBusTreeNode targetNode = GetTargetNode(args);

         if(targetNode == m_DraggedNode)
         {
            return;
         }
         if(targetNode.ServiceBusNode != null)
         {
            if(targetNode.ServiceBusNode.Policy != null)
            {
               if(targetNode.ServiceBusNode.Policy is RouterPolicy)
               {
                  foreach(ServiceBusNode node in targetNode.ServiceBusNode.Subscribers)
                  {
                     if(node.Address == m_DraggedNode.ServiceBusNode.Address)
                     {
                        Cursor.Current = Cursors.No;

                        return;
                     }
                  }
                  args.Effect = DragDropEffects.Link;
                  m_ServiceBusTree.SelectedNode = targetNode;
                  m_ServiceBusTree.Select();      
               }
            }
         }
      }
      ServiceBusTreeNode GetTargetNode(DragEventArgs args)
      {
         Point point = m_ServiceBusTree.PointToClient(new Point(args.X,args.Y));
			return m_ServiceBusTree.GetNodeAt(point) as ServiceBusTreeNode;
      }
   }
}