using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;

using Northwind.Data.Entities;

namespace Northwind.UI.Entities
{
    public partial class CustomersPage : Northwind.UI.NorthwindBasePage
    {
        #region Private Constants

        private const string UNKNOWN_COUNTRY = "(NULL)";
        private const string ALL_COUNTRIES = "(ALL)";

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsCallback)
            {
                Debug.WriteLine("Callback in Customers.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    string connectionString = WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_ENTITIES_CONNECTION_STRING].ConnectionString;

                    using (NorthwindObjectContext context = new NorthwindObjectContext(connectionString))
                    {
                        //Do manual binding for regions combo
                        cmbCountries.DataSource = (from c in context.Customers
                                                   where !String.IsNullOrEmpty(c.Address.Country)
                                                   select c.Address.Country).Distinct();
                        cmbCountries.DataBind();
                    }

                    cmbCountries.Items.Insert(0, new ListItem("(All countries)", ALL_COUNTRIES));
                    cmbCountries.Items.Insert(1, new ListItem("(Unknown country)", UNKNOWN_COUNTRY));
                }

                pnlData.Visible = true;
                lblError.Visible = false;
            }
        }

        protected void grdCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("CustomerOrders.aspx?{0}={1}",
                ConstantsHelper.CUSTOMER_ID_PARAMETER,
                Server.UrlEncode(grdCustomers.SelectedDataKey.Value.ToString())));
        }

        protected void srcCustomers_ReadCommandDone(object sender, EntityDataSourceSelectedEventArgs e)
        {
            e.ExceptionHandled = ProcessException(e.Exception);
        }

        protected void srcCustomers_WriteCommandDone(object sender, EntityDataSourceChangedEventArgs e)
        {
            e.ExceptionHandled = ProcessException(e.Exception);
        }

        protected void grdCustomers_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            e.ExceptionHandled = ProcessException(e.Exception);
        }

        protected void dvwNewCustomer_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            e.ExceptionHandled = ProcessException(e.Exception);
        }

        protected void qexCustomers_OnQuerying(object sender, CustomExpressionEventArgs e)
        {
            switch (cmbCountries.SelectedValue)
            {
                case UNKNOWN_COUNTRY:
                    e.Query = from c in e.Query.Cast<Northwind.Data.Entities.Customer>()
                              where String.IsNullOrEmpty(c.Address.Country)
                              select c;
                    break;
                case ALL_COUNTRIES:
                    break;
                default:
                    e.Query = from c in e.Query.Cast<Northwind.Data.Entities.Customer>()
                              where c.Address.Country == cmbCountries.SelectedValue
                              select c;
                    break;
            }
        }

        protected void cmbCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdCustomers.DataBind();
        }

        #endregion

        #region Implementation

        private bool ProcessException(Exception exception)
        {
            if (exception != null)
            {
                Debug.WriteLine(exception);
                pnlData.Visible = false;
                lblError.Visible = true;
                return true;
            }

            return false;
        }

        #endregion


    }
}
