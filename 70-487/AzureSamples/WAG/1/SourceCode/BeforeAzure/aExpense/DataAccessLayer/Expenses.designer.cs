﻿//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17626
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AExpense.DataAccessLayer
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="aExpense")]
	public partial class ExpensesDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertExpense(Expense instance);
    partial void UpdateExpense(Expense instance);
    partial void DeleteExpense(Expense instance);
    partial void InsertExpenseDetail(ExpenseDetail instance);
    partial void UpdateExpenseDetail(ExpenseDetail instance);
    partial void DeleteExpenseDetail(ExpenseDetail instance);
    #endregion
		
		public ExpensesDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["aExpenseConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public ExpensesDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ExpensesDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ExpensesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ExpensesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Expense> Expenses
		{
			get
			{
				return this.GetTable<Expense>();
			}
		}
		
		public System.Data.Linq.Table<ExpenseDetail> ExpenseDetails
		{
			get
			{
				return this.GetTable<ExpenseDetail>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Expense")]
	public partial class Expense : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _Id;
		
		private string _UserName;
		
		private string _Title;
		
		private string _Description;
		
		private decimal _Amount;
		
		private System.DateTime _Date;
		
		private bool _Approved;
		
		private string _CostCenter;
		
		private string _ReimbursementMethod;
		
		private string _Approver;
		
		private EntitySet<ExpenseDetail> _ExpenseDetails;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(System.Guid value);
    partial void OnIdChanged();
    partial void OnUserNameChanging(string value);
    partial void OnUserNameChanged();
    partial void OnTitleChanging(string value);
    partial void OnTitleChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    partial void OnAmountChanging(decimal value);
    partial void OnAmountChanged();
    partial void OnDateChanging(System.DateTime value);
    partial void OnDateChanged();
    partial void OnApprovedChanging(bool value);
    partial void OnApprovedChanged();
    partial void OnCostCenterChanging(string value);
    partial void OnCostCenterChanged();
    partial void OnReimbursementMethodChanging(string value);
    partial void OnReimbursementMethodChanged();
    partial void OnApproverChanging(string value);
    partial void OnApproverChanged();
    #endregion
		
		public Expense()
		{
			this._ExpenseDetails = new EntitySet<ExpenseDetail>(new Action<ExpenseDetail>(this.attach_ExpenseDetails), new Action<ExpenseDetail>(this.detach_ExpenseDetails));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		public System.Guid Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserName", DbType="NVarChar(1024) NOT NULL", CanBeNull=false)]
		public string UserName
		{
			get
			{
				return this._UserName;
			}
			set
			{
				if ((this._UserName != value))
				{
					this.OnUserNameChanging(value);
					this.SendPropertyChanging();
					this._UserName = value;
					this.SendPropertyChanged("UserName");
					this.OnUserNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Title", DbType="NVarChar(30) NOT NULL", CanBeNull=false)]
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				if ((this._Title != value))
				{
					this.OnTitleChanging(value);
					this.SendPropertyChanging();
					this._Title = value;
					this.SendPropertyChanged("Title");
					this.OnTitleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Description", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Amount", DbType="Money NOT NULL")]
		public decimal Amount
		{
			get
			{
				return this._Amount;
			}
			set
			{
				if ((this._Amount != value))
				{
					this.OnAmountChanging(value);
					this.SendPropertyChanging();
					this._Amount = value;
					this.SendPropertyChanged("Amount");
					this.OnAmountChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Date", DbType="DateTime NOT NULL")]
		public System.DateTime Date
		{
			get
			{
				return this._Date;
			}
			set
			{
				if ((this._Date != value))
				{
					this.OnDateChanging(value);
					this.SendPropertyChanging();
					this._Date = value;
					this.SendPropertyChanged("Date");
					this.OnDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Approved", DbType="Bit NOT NULL")]
		public bool Approved
		{
			get
			{
				return this._Approved;
			}
			set
			{
				if ((this._Approved != value))
				{
					this.OnApprovedChanging(value);
					this.SendPropertyChanging();
					this._Approved = value;
					this.SendPropertyChanged("Approved");
					this.OnApprovedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CostCenter", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string CostCenter
		{
			get
			{
				return this._CostCenter;
			}
			set
			{
				if ((this._CostCenter != value))
				{
					this.OnCostCenterChanging(value);
					this.SendPropertyChanging();
					this._CostCenter = value;
					this.SendPropertyChanged("CostCenter");
					this.OnCostCenterChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ReimbursementMethod", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string ReimbursementMethod
		{
			get
			{
				return this._ReimbursementMethod;
			}
			set
			{
				if ((this._ReimbursementMethod != value))
				{
					this.OnReimbursementMethodChanging(value);
					this.SendPropertyChanging();
					this._ReimbursementMethod = value;
					this.SendPropertyChanged("ReimbursementMethod");
					this.OnReimbursementMethodChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Approver", DbType="NVarChar(1024) NOT NULL", CanBeNull=false)]
		public string Approver
		{
			get
			{
				return this._Approver;
			}
			set
			{
				if ((this._Approver != value))
				{
					this.OnApproverChanging(value);
					this.SendPropertyChanging();
					this._Approver = value;
					this.SendPropertyChanged("Approver");
					this.OnApproverChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Expense_ExpenseDetail", Storage="_ExpenseDetails", ThisKey="Id", OtherKey="ExpenseId")]
		public EntitySet<ExpenseDetail> ExpenseDetails
		{
			get
			{
				return this._ExpenseDetails;
			}
			set
			{
				this._ExpenseDetails.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_ExpenseDetails(ExpenseDetail entity)
		{
			this.SendPropertyChanging();
			entity.Expense = this;
		}
		
		private void detach_ExpenseDetails(ExpenseDetail entity)
		{
			this.SendPropertyChanging();
			entity.Expense = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.ExpenseDetail")]
	public partial class ExpenseDetail : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _Id;
		
		private string _Description;
		
		private decimal _Amount;
		
		private System.Guid _ExpenseId;
		
		private string _ReceiptThumbnailUrl;
		
		private string _ReceiptUrl;
		
		private EntityRef<Expense> _Expense;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(System.Guid value);
    partial void OnIdChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    partial void OnAmountChanging(decimal value);
    partial void OnAmountChanged();
    partial void OnExpenseIdChanging(System.Guid value);
    partial void OnExpenseIdChanged();
    partial void OnReceiptThumbnailUrlChanging(string value);
    partial void OnReceiptThumbnailUrlChanged();
    partial void OnReceiptUrlChanging(string value);
    partial void OnReceiptUrlChanged();
    #endregion
		
		public ExpenseDetail()
		{
			this._Expense = default(EntityRef<Expense>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		public System.Guid Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Description", DbType="NVarChar(1024) NOT NULL", CanBeNull=false)]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Amount", DbType="Money NOT NULL")]
		public decimal Amount
		{
			get
			{
				return this._Amount;
			}
			set
			{
				if ((this._Amount != value))
				{
					this.OnAmountChanging(value);
					this.SendPropertyChanging();
					this._Amount = value;
					this.SendPropertyChanged("Amount");
					this.OnAmountChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ExpenseId", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid ExpenseId
		{
			get
			{
				return this._ExpenseId;
			}
			set
			{
				if ((this._ExpenseId != value))
				{
					if (this._Expense.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnExpenseIdChanging(value);
					this.SendPropertyChanging();
					this._ExpenseId = value;
					this.SendPropertyChanged("ExpenseId");
					this.OnExpenseIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ReceiptThumbnailUrl", DbType="NVarChar(MAX)")]
		public string ReceiptThumbnailUrl
		{
			get
			{
				return this._ReceiptThumbnailUrl;
			}
			set
			{
				if ((this._ReceiptThumbnailUrl != value))
				{
					this.OnReceiptThumbnailUrlChanging(value);
					this.SendPropertyChanging();
					this._ReceiptThumbnailUrl = value;
					this.SendPropertyChanged("ReceiptThumbnailUrl");
					this.OnReceiptThumbnailUrlChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ReceiptUrl", DbType="NVarChar(MAX)")]
		public string ReceiptUrl
		{
			get
			{
				return this._ReceiptUrl;
			}
			set
			{
				if ((this._ReceiptUrl != value))
				{
					this.OnReceiptUrlChanging(value);
					this.SendPropertyChanging();
					this._ReceiptUrl = value;
					this.SendPropertyChanged("ReceiptUrl");
					this.OnReceiptUrlChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Expense_ExpenseDetail", Storage="_Expense", ThisKey="ExpenseId", OtherKey="Id", IsForeignKey=true)]
		public Expense Expense
		{
			get
			{
				return this._Expense.Entity;
			}
			set
			{
				Expense previousValue = this._Expense.Entity;
				if (((previousValue != value) 
							|| (this._Expense.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Expense.Entity = null;
						previousValue.ExpenseDetails.Remove(this);
					}
					this._Expense.Entity = value;
					if ((value != null))
					{
						value.ExpenseDetails.Add(this);
						this._ExpenseId = value.Id;
					}
					else
					{
						this._ExpenseId = default(System.Guid);
					}
					this.SendPropertyChanged("Expense");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
