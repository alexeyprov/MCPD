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
    using TransportPartner.Connectivity;
    using TransportPartner.TransportServices;

    /// <summary>
    /// Shows simulated interactions between TreyResearch and TransportPartners that do not use ServiceBus to communicate.
    /// </summary>
    public partial class FabrikamTransportPartner : Form
    {
        private readonly string transportPartnerName;
        private readonly string displayName;
        private readonly string key;
        private readonly string acsPassword;
        private readonly ITransportServiceWrapper transportService;
        private IList<ActiveOrder> activeOrders;
        private Adapter adapter;

        public FabrikamTransportPartner(string displayName, string name, string key, string acsPassword)
        {
            this.InitializeComponent();
            this.activeOrders = new List<ActiveOrder>();
            this.displayName = displayName;
            this.transportPartnerName = name;
            this.key = key;
            this.acsPassword = acsPassword;
            this.transportService = new TransportServiceWrapper();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.UpdateStatus("Initializing...");
            this.ordersDataGrid.AutoGenerateColumns = true;

            var topicName = ConfigurationManager.AppSettings["topicName"];
            var subscriptionName = string.Format("{0}Subscription", this.transportPartnerName.Replace(" ", string.Empty));
            var issuer = this.transportPartnerName;

            this.adapter = new Adapter(topicName, subscriptionName, this.displayName, issuer, this.key, this.transportService, this.acsPassword);
            this.adapter.OnOrderProcessed += this.OrderProcessed;
            this.adapter.Run();
            this.UpdateStatus("Ready");
        }

        private void OrderProcessed(object sender, OrderProcessedEventArgs args)
        {
            this.activeOrders.Add(args.ActiveOrder);
            this.ShowData(this.activeOrders);
        }

        private void ShowData(IEnumerable<ActiveOrder> activeOrdersToUse)
        {
            this.activeOrders = activeOrdersToUse.ToList();

            if (this.ordersDataGrid.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(this.BindGridView));
            }
            else
            {
                this.BindGridView();
            }
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

        private void OnFabrikamTransportPartnerFormClosed(object sender, FormClosedEventArgs e)
        {
            this.adapter.Cancel();
        }

        private void UpdateStatus(string statusMessage)
        {
            this.statusLabel.Text = statusMessage;
        }
    }
}
