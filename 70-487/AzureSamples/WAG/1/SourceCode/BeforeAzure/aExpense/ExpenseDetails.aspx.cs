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
    using System.Globalization;
    using System.Web.UI.WebControls;
    using AExpense.Model;
    using DataAccessLayer;
    using Microsoft.Security.Application;

    public partial class ExpenseDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { 
            if (!IsPostBack)
            {
                Guid expenseId;
                try
                {
                    expenseId = Guid.Parse(this.Request.QueryString["id"]);
                }
                catch (ArgumentNullException exception)
                {
                    throw exception;
                }

                var expenseRepository = new ExpenseRepository();
                var expense = expenseRepository.GetExpenseById(expenseId);

                if (expense == null)
                {
                    string errorMessage = string.Format(CultureInfo.CurrentCulture, "There is no expense with the id {0}.", expenseId);
                    throw new ArgumentException(errorMessage);
                }

                if (expense.User.UserName != this.User.Identity.Name)
                {
                    string errorMessage = string.Format("{0} cannot access the expense with id {1}.", this.User.Identity.Name, expense.Id);
                    throw new UnauthorizedAccessException(errorMessage);
                }

                this.ExpenseDate.Text = expense.Date.ToString("yyyy-MM-dd");
                this.ExpenseTitle.Text = Encoder.HtmlEncode(expense.Title);
                this.ExpenseItemsGridView.DataSource = expense.Details;
                this.ExpenseItemsGridView.DataBind();
                this.ExpenseReimbursementMethod.Text = Encoder.HtmlEncode(Enum.GetName(typeof(ReimbursementMethod), expense.ReimbursementMethod));
                this.ExpenseCostCenter.Text = Encoder.HtmlEncode(expense.CostCenter);
                this.Approver.Text = Encoder.HtmlEncode(expense.ApproverName);
            }
        }

        protected void ExpenseItemsGridViewOnRowDataBound(object sender, GridViewRowEventArgs eventArgs)
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
