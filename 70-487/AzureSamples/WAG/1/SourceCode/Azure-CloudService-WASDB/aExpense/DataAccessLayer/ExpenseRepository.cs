//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace AExpense.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AExpense.Helpers;
    using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling;
    using Microsoft.Practices.TransientFaultHandling;
    using Model;

    public class ExpenseRepository
    {
        private readonly string expenseDatabaseConnectionString;
        private readonly RetryPolicy sqlCommandRetryPolicy;

        public ExpenseRepository()
            : this(CloudConfiguration.GetConnectionString("aExpense"))
        {
        }

        public ExpenseRepository(string expenseDatabaseConnectionString)
        {
            this.expenseDatabaseConnectionString = expenseDatabaseConnectionString;
            this.sqlCommandRetryPolicy = RetryPolicyFactory.GetDefaultSqlCommandRetryPolicy();
            this.sqlCommandRetryPolicy.Retrying += (sender, args) => TraceHelper.TraceInformation("Retry in OrderStore - Count:{0}, Delay:{1}, Exception:{2}", args.CurrentRetryCount, args.Delay, args.LastException);
        }

        public void SaveExpense(Model.Expense expense)
        {
            using (var db = new DataAccessLayer.ExpensesDataContext(this.expenseDatabaseConnectionString))
            {
                var entity = expense.ToEntity();
                db.Expenses.InsertOnSubmit(entity);

                foreach (var detail in expense.Details)
                {
                    var detailEntity = detail.ToEntity(expense);
                    db.ExpenseDetails.InsertOnSubmit(detailEntity);
                }
                
                this.sqlCommandRetryPolicy.ExecuteAction(() => db.SubmitChanges());
            }
        }

        public IEnumerable<Model.Expense> GetAllExpenses()
        {
            using (var db = new DataAccessLayer.ExpensesDataContext(this.expenseDatabaseConnectionString))
            {
                return this.sqlCommandRetryPolicy.ExecuteAction(() =>
                (from e in db.Expenses
                 select ModelExtensions.ToModel(e)).ToList());
            }
        }

        public IEnumerable<Model.Expense> GetExpensesForApproval(string approverName)
        {
            using (var db = new DataAccessLayer.ExpensesDataContext(this.expenseDatabaseConnectionString))
            {
                return this.sqlCommandRetryPolicy.ExecuteAction(() => 
                        (from e in db.Expenses
                         where e.Approver == approverName
                         select e.ToModel()).ToList());
            }
        }

        public IEnumerable<Model.Expense> GetExpensesByUser(string userName)
        {
            using (var db = new DataAccessLayer.ExpensesDataContext(this.expenseDatabaseConnectionString))
            {
                return this.sqlCommandRetryPolicy.ExecuteAction(() =>
                       (from e in db.Expenses
                       where e.UserName == userName
                       select e.ToModel()).ToList());
            }
        }

        public Model.Expense GetExpenseById(Guid expenseId)
        {
            using (var db = new ExpensesDataContext(this.expenseDatabaseConnectionString))
            {
                var entity = this.sqlCommandRetryPolicy.ExecuteAction(() => 
                        (from e in db.Expenses
                        where e.Id == expenseId
                        select e).SingleOrDefault());

                var expense = entity.ToModel();

                return expense;
            }
        }

        public void UpdateApproved(Model.Expense expense)
        {
            using (var db = new DataAccessLayer.ExpensesDataContext(this.expenseDatabaseConnectionString))
            {
                var expenseToUpdate = this.sqlCommandRetryPolicy.ExecuteAction(() => db.Expenses.Single(e => e.Id == expense.Id));
                expenseToUpdate.Approved = expense.Approved;
                this.sqlCommandRetryPolicy.ExecuteAction(() => db.SubmitChanges());                
            }
        }        
    }
}