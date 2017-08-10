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
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Windows.Forms;
    using Orders.Shared.Communication;
    using TransportPartner.Connectivity;

    /// <summary>
    /// Shows simulated interactions between TreyResearch and transport partners that use ServiceBus to communicate.
    /// </summary>
    public partial class ContosoTransportPartner : Form
    {
        private readonly Connector connector;
        private readonly string transportPartnerName;
        private List<ActiveOrder> activeOrders = new List<ActiveOrder>();

        public ContosoTransportPartner(string displayName, string name, string key, string acsPassword)
        {
            this.InitializeComponent();
            this.transportPartnerName = name;

            var topicName = ConfigurationManager.AppSettings["topicName"];
            var subscriptionName = string.Format("{0}Subscription", this.transportPartnerName.Replace(" ", string.Empty));
            var issuer = this.transportPartnerName;

            this.connector = new Connector(topicName, subscriptionName, displayName, issuer, key, acsPassword);
            this.connector.OnOrderProcessed += this.OrderProcessed;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.UpdateStatus("Initializing...");
            this.ordersDataGrid.AutoGenerateColumns = true;

            this.connector.Run();

            this.UpdateStatus("Ready");
        }

        private void BindGridView()
        {
            if (this.activeOrders.Any())
            {
                this.ordersDataGrid.DataSource = null;
                this.ordersDataGrid.DataSource = this.activeOrders;
            }
            else
            {
                this.ordersDataGrid.DataSource = null;
            }
        }

        private void OrderProcessed(object sender, OrderProcessedEventArgs args)
        {
            this.activeOrders.Add(args.ActiveOrder);
            this.ShowData(this.activeOrders);
        }

        private void ShowData(IEnumerable<ActiveOrder> activeOrdersToShow)
        {
            this.activeOrders = activeOrdersToShow.ToList();

            if (this.ordersDataGrid.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(this.BindGridView));
            }
            else
            {
                this.BindGridView();
            }
        }

        private void UpdateStatus(string statusMessage)
        {
            this.statusLabel.Text = statusMessage;
        }

        private void OnOrdersDataGridCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 0) && (e.RowIndex >= 0))
            {
                var orderId = (Guid)this.ordersDataGrid.Rows[e.RowIndex].Cells[1].Value;
                var order = this.activeOrders.Single(o => o.OrderId == orderId);

                var queueDescrpition = new ServiceBusQueueDescription
                {
                    Namespace = order.ReplyToNamespace,
                    QueueName = order.ReplyTo,
                    SwtAcsNamespace = order.SwtAcsNamespace
                };

                this.connector.ShipOrder(orderId, queueDescrpition);
               
                this.UpdateStatus("Ready");
                
                this.activeOrders.Remove(order);
                this.ShowData(this.activeOrders);
            }
        }

        private void OnContosoPartnerFormClosed(object sender, FormClosedEventArgs e)
        {
            this.connector.Cancel();
        }
    }
}