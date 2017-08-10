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
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using DataAccessLayer;
    using Microsoft.Security.Application;
    using Model;

    public partial class AddExpense : Page
    {
        private List<ExpenseItem> ExpenseItems
        {
            get
            {
                if (this.Session["ExpenseItems"] == null)
                {
                   this.Session["ExpenseItems"] = new List<ExpenseItem>();
                }

                return (List<ExpenseItem>)this.Session["ExpenseItems"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Session["ExpenseItems"] = null;
                this.InitializeControls();
            }
        }

        protected void AddExpenseButtonOnClick(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                this.SaveExpense();
                Response.Redirect("~/Default.aspx", true);
            }
        }

        protected void OnAddNewExpenseItemClick(object sender, EventArgs e)
        {
            this.Validate("AddNewExpenseItem");
            /// Here must be placed all the extra validations for the inputs 
            /// (like length, potentially dangerous characters, etc.)
            if (this.IsValid)
            {
                this.ExpenseItems.Add(
                    new ExpenseItem
                        {
                            Id = Guid.NewGuid(),
                            Description = this.ExpenseItemDescription.Text,
                            Amount = double.Parse(this.ExpenseItemAmount.Text, CultureInfo.CurrentUICulture),
                        });

                this.ExpenseItemDescription.Text = string.Empty;
                this.ExpenseItemAmount.Text = string.Empty;
            }

            this.ExpenseItemsGridView.DataSource = this.ExpenseItems;
            this.ExpenseItemsGridView.DataBind();
        }

        protected void ExpenseItemsGridViewOnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.ExpenseItems.RemoveAt(e.RowIndex);
            this.ExpenseItemsGridView.DataSource = this.ExpenseItems;
            this.ExpenseItemsGridView.DataBind();
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

        private void SaveExpense()
        {
            var userRepository = new UserRepository();
            var user = userRepository.GetUser(this.User.Identity.Name);
            
            var approverName = this.Approver.Text;

            if (string.IsNullOrEmpty(approverName))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The approver {0} does not exists", this.Approver.Text));
            }

            var newExpense = new Model.Expense
            {
                Id = Guid.NewGuid(),
                Title = this.ExpenseTitle.Text,
                CostCenter = user.CostCenter,
                Approved = false,
                ReimbursementMethod = (ReimbursementMethod)Enum.Parse(typeof(ReimbursementMethod), this.ExpenseReimbursementMethod.SelectedItem.Value),
                User = user,
                Date = DateTime.Parse(this.ExpenseDate.Text, CultureInfo.CurrentUICulture),
                ApproverName = approverName,
                Description = string.Empty,
                Total = this.ExpenseItems.Sum(i => i.Amount)
            };
            this.ExpenseItems.ForEach(ei => newExpense.Details.Add(ei));

            var expenseRepository = new ExpenseRepository();
            expenseRepository.SaveExpense(newExpense);

            user.PreferredReimbursementMethod = (ReimbursementMethod)Enum.Parse(typeof(ReimbursementMethod), this.ExpenseReimbursementMethod.SelectedValue);
            userRepository.UpdateUserPreferredReimbursementMethod(user);
        }

        private void InitializeControls()
        {
            var userRepository = new UserRepository();
            var user = userRepository.GetUser(this.User.Identity.Name);
            if (user == null)
            {
                throw new InvalidOperationException("User does not exist");
            }

            this.ExpenseReimbursementMethod.Items.Add(new ListItem("Check", ReimbursementMethod.Check.ToString()));
            this.ExpenseReimbursementMethod.Items.Add(new ListItem("Cash", ReimbursementMethod.Cash.ToString()));
            this.ExpenseReimbursementMethod.Items.Add(new ListItem("Direct Deposit", ReimbursementMethod.DirectDeposit.ToString()));
            if (user.PreferredReimbursementMethod != ReimbursementMethod.NotSet)
            {
                this.ExpenseReimbursementMethod.SelectedValue = user.PreferredReimbursementMethod.ToString();
            }

            this.ExpenseCostCenter.Text = Encoder.HtmlEncode(user.CostCenter);
            this.Approver.Text = user.Manager;

            this.ExpenseItemsGridView.DataSource = this.ExpenseItems;
            this.ExpenseItemsGridView.DataBind();
        }
    }
}
