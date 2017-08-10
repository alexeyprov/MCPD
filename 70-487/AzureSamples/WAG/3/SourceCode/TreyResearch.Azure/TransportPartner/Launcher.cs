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
    using System.Configuration;
    using System.Windows.Forms;

    public partial class Launcher : Form
    {
        public Launcher()
        {
            this.InitializeComponent();
        }

        private void OnPartnerButtonOneClick(object sender, EventArgs e)
        {
            // Launch Contoso Transport Partner
            var name = ConfigurationManager.AppSettings["transportpartner1.name"];
            var key = ConfigurationManager.AppSettings["transportpartner1.key"];

            var acsPassword = ConfigurationManager.AppSettings["transportpartner1.acspassword"];

            var form = new ContosoTransportPartner("Contoso", name, key, acsPassword)
                           {
                               Text = "Contoso (Illinois and neighbour states)"
                           };
            form.Show();
        }
        
        private void OnPartnerTwoButtonTwoClick(object sender, EventArgs e)
        {
            // Fabrikam Transport Partner
            var name = ConfigurationManager.AppSettings["transportpartner2.name"];
            var key = ConfigurationManager.AppSettings["transportpartner2.key"];

            var acsPassword = ConfigurationManager.AppSettings["transportpartner2.acspassword"];

            var form = new FabrikamTransportPartner("Fabrikam", name, key, acsPassword)
                           {
                               Text = "Fabrikam"
                           };
            form.Show();
        }
    }
}
