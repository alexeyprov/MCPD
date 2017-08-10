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
    using System.Configuration;
    using System.Linq;
    using Model;

    public class ExpenseRepository
    {
        private readonly string expenseDatabaseConnectionString;

        public ExpenseRepository()
            : this(CloudConfiguration.GetConnectionString("aExpense"))
        {
        }

        public ExpenseRepository(string expenseDatabaseConnectionString)
        {
            this.expenseDatabaseConnectionString = expenseDatabaseConnectionString;
        }

        public void SaveExpense(Model.Expense expense)
        {
            using (var db = new ExpensesDataContext(this.expenseDatabaseConnectionString))
            {
                var entity = expense.ToEntity();
                db.Expenses.InsertOnSubmit(entity);

                foreach (var detail in expense.Details)
                {
                    var detailEntity = detail.ToEntity(expense);
                    db.ExpenseDetails.InsertOnSubmit(detailEntity);
                }

                db.SubmitChanges();
            }
        }

        public IEnumerable<Model.Expense> GetAllExpenses()
        {
            using (var db = new ExpensesDataContext(this.expenseDatabaseConnectionString))
            {
                var expenses = from e in db.Expenses
                               select e.ToModel();

                return expenses.ToList();                
            }
        }

        public IEnumerable<Model.Expense> GetExpensesForApproval(string approverName)
        {
            using (var db = new ExpensesDataContext(this.expenseDatabaseConnectionString))
            {
                var expenses = from e in db.Expenses
                               where e.Approver == approverName
                               select e.ToModel();

                return expenses.ToList();
            }
        }

        public IEnumerable<Model.Expense> GetExpensesByUser(string userName)
        {
            using (var db = new ExpensesDataContext(this.expenseDatabaseConnectionString))
            {
                var expenses = from e in db.Expenses
                               where e.UserName == userName
                               select e.ToModel();

                return expenses.ToList();
            }
        }

        public Model.Expense GetExpenseById(Guid expenseId)
        {
            using (var db = new ExpensesDataContext(this.expenseDatabaseConnectionString))
            {
                var entity = (from e in db.Expenses
                         where e.Id == expenseId
                         select e).SingleOrDefault();

                var expense = entity.ToModel();
                
                return expense;
            }
        }

        public void UpdateApproved(Model.Expense expense)
        {
            using (var db = new ExpensesDataContext(this.expenseDatabaseConnectionString))
            {
                var expenseToUpdate = db.Expenses.Single(e => e.Id == expense.Id);
                expenseToUpdate.Approved = expense.Approved;
                
                db.SubmitChanges();                
            }
        }        
    }
}