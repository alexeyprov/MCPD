//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace AExpense
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Microsoft.Security.Application;
    using Model;
    
    public partial class Approve : Page
    {
        protected void OnExpensesSelecting(object sender, ObjectDataSourceMethodEventArgs eventArgs)
        {
            eventArgs.InputParameters["approverName"] = User.Identity.Name;
        }

        protected void OnExpenseRowDataBound(object sender, GridViewRowEventArgs eventArgs)
        {
            var row = eventArgs.Row.DataItem as Expense;
            if (row != null)
            {
                if (row.Approved)
                {
                    eventArgs.Row.Enabled = false;
                }
            }

            foreach (TableCell cell in eventArgs.Row.Cells)
            {
                if (!string.IsNullOrEmpty(cell.Text) && !cell.Text.Equals("&nbsp;"))
                {
                    cell.Text = Encoder.HtmlEncode(cell.Text);
                }
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            this.ViewStateUserKey = this.User.Identity.Name;
        }
    }
}
