﻿//===============================================================================
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
    using System.Globalization;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using DataAccessLayer;
    using Microsoft.Security.Application;

    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            var repository = new ExpenseRepository();
            var expenses = repository.GetExpensesByUser(this.User.Identity.Name);
            this.MyExpensesGridView.DataSource = expenses;
            this.DataBind();
        }

        protected void MyExpensesGridViewOnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int selectedRow = Convert.ToInt32(e.CommandArgument);
                string expenseId = this.MyExpensesGridView.DataKeys[selectedRow].Value.ToString();
                string expenseDetailsUrl = string.Format(CultureInfo.InvariantCulture, "ExpenseDetails.aspx?id={0}", expenseId);
                this.Response.Redirect(expenseDetailsUrl);
            }
        }

        protected void MyExpensesGridViewOnRowDataBound(object sender, GridViewRowEventArgs eventArgs)
        {
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
