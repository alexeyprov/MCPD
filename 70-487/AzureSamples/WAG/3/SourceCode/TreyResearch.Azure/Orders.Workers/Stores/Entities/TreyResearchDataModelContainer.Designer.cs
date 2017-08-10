﻿//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

[assembly: EdmRelationshipAttribute("Orders.Website.Stores.Entities", "FK_OrderStatus_Order", "Order", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(Orders.Workers.Stores.Entities.Order), "OrderStatus", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Orders.Workers.Stores.Entities.OrderStatus), true)]
[assembly: EdmRelationshipAttribute("Orders.Website.Stores.Entities", "FK_OrderProcessStatus_Order", "Order", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(Orders.Workers.Stores.Entities.Order), "OrderProcessStatus", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Orders.Workers.Stores.Entities.OrderProcessStatus), true)]

#endregion

namespace Orders.Workers.Stores.Entities
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class TreyResearchModel : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new TreyResearchModel object using the connection string found in the 'TreyResearchModel' section of the application configuration file.
        /// </summary>
        public TreyResearchModel() : base("name=TreyResearchModel", "TreyResearchModel")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new TreyResearchModel object.
        /// </summary>
        public TreyResearchModel(string connectionString) : base(connectionString, "TreyResearchModel")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new TreyResearchModel object.
        /// </summary>
        public TreyResearchModel(EntityConnection connection) : base(connection, "TreyResearchModel")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Order> Order
        {
            get
            {
                if ((_Order == null))
                {
                    _Order = base.CreateObjectSet<Order>("Order");
                }
                return _Order;
            }
        }
        private ObjectSet<Order> _Order;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<OrderStatus> OrderStatus
        {
            get
            {
                if ((_OrderStatus == null))
                {
                    _OrderStatus = base.CreateObjectSet<OrderStatus>("OrderStatus");
                }
                return _OrderStatus;
            }
        }
        private ObjectSet<OrderStatus> _OrderStatus;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<OrderProcessStatus> OrderProcessStatus
        {
            get
            {
                if ((_OrderProcessStatus == null))
                {
                    _OrderProcessStatus = base.CreateObjectSet<OrderProcessStatus>("OrderProcessStatus");
                }
                return _OrderProcessStatus;
            }
        }
        private ObjectSet<OrderProcessStatus> _OrderProcessStatus;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Order EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToOrder(Order order)
        {
            base.AddObject("Order", order);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the OrderStatus EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToOrderStatus(OrderStatus orderStatus)
        {
            base.AddObject("OrderStatus", orderStatus);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the OrderProcessStatus EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToOrderProcessStatus(OrderProcessStatus orderProcessStatus)
        {
            base.AddObject("OrderProcessStatus", orderProcessStatus);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Orders.Website.Stores.Entities", Name="Order")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Order : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Order object.
        /// </summary>
        /// <param name="orderId">Initial value of the OrderId property.</param>
        /// <param name="orderDate">Initial value of the OrderDate property.</param>
        /// <param name="userName">Initial value of the UserName property.</param>
        /// <param name="total">Initial value of the Total property.</param>
        /// <param name="address">Initial value of the Address property.</param>
        /// <param name="city">Initial value of the City property.</param>
        /// <param name="state">Initial value of the State property.</param>
        /// <param name="postalCode">Initial value of the PostalCode property.</param>
        /// <param name="country">Initial value of the Country property.</param>
        /// <param name="phone">Initial value of the Phone property.</param>
        /// <param name="email">Initial value of the Email property.</param>
        public static Order CreateOrder(global::System.Guid orderId, global::System.DateTime orderDate, global::System.String userName, global::System.Decimal total, global::System.String address, global::System.String city, global::System.String state, global::System.String postalCode, global::System.String country, global::System.String phone, global::System.String email)
        {
            Order order = new Order();
            order.OrderId = orderId;
            order.OrderDate = orderDate;
            order.UserName = userName;
            order.Total = total;
            order.Address = address;
            order.City = city;
            order.State = state;
            order.PostalCode = postalCode;
            order.Country = country;
            order.Phone = phone;
            order.Email = email;
            return order;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid OrderId
        {
            get
            {
                return _OrderId;
            }
            set
            {
                if (_OrderId != value)
                {
                    OnOrderIdChanging(value);
                    ReportPropertyChanging("OrderId");
                    _OrderId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("OrderId");
                    OnOrderIdChanged();
                }
            }
        }
        private global::System.Guid _OrderId;
        partial void OnOrderIdChanging(global::System.Guid value);
        partial void OnOrderIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime OrderDate
        {
            get
            {
                return _OrderDate;
            }
            set
            {
                OnOrderDateChanging(value);
                ReportPropertyChanging("OrderDate");
                _OrderDate = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("OrderDate");
                OnOrderDateChanged();
            }
        }
        private global::System.DateTime _OrderDate;
        partial void OnOrderDateChanging(global::System.DateTime value);
        partial void OnOrderDateChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                OnUserNameChanging(value);
                ReportPropertyChanging("UserName");
                _UserName = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("UserName");
                OnUserNameChanged();
            }
        }
        private global::System.String _UserName;
        partial void OnUserNameChanging(global::System.String value);
        partial void OnUserNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal Total
        {
            get
            {
                return _Total;
            }
            set
            {
                OnTotalChanging(value);
                ReportPropertyChanging("Total");
                _Total = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Total");
                OnTotalChanged();
            }
        }
        private global::System.Decimal _Total;
        partial void OnTotalChanging(global::System.Decimal value);
        partial void OnTotalChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Address
        {
            get
            {
                return _Address;
            }
            set
            {
                OnAddressChanging(value);
                ReportPropertyChanging("Address");
                _Address = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Address");
                OnAddressChanged();
            }
        }
        private global::System.String _Address;
        partial void OnAddressChanging(global::System.String value);
        partial void OnAddressChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String City
        {
            get
            {
                return _City;
            }
            set
            {
                OnCityChanging(value);
                ReportPropertyChanging("City");
                _City = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("City");
                OnCityChanged();
            }
        }
        private global::System.String _City;
        partial void OnCityChanging(global::System.String value);
        partial void OnCityChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String State
        {
            get
            {
                return _State;
            }
            set
            {
                OnStateChanging(value);
                ReportPropertyChanging("State");
                _State = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("State");
                OnStateChanged();
            }
        }
        private global::System.String _State;
        partial void OnStateChanging(global::System.String value);
        partial void OnStateChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String PostalCode
        {
            get
            {
                return _PostalCode;
            }
            set
            {
                OnPostalCodeChanging(value);
                ReportPropertyChanging("PostalCode");
                _PostalCode = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("PostalCode");
                OnPostalCodeChanged();
            }
        }
        private global::System.String _PostalCode;
        partial void OnPostalCodeChanging(global::System.String value);
        partial void OnPostalCodeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Country
        {
            get
            {
                return _Country;
            }
            set
            {
                OnCountryChanging(value);
                ReportPropertyChanging("Country");
                _Country = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Country");
                OnCountryChanged();
            }
        }
        private global::System.String _Country;
        partial void OnCountryChanging(global::System.String value);
        partial void OnCountryChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Phone
        {
            get
            {
                return _Phone;
            }
            set
            {
                OnPhoneChanging(value);
                ReportPropertyChanging("Phone");
                _Phone = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Phone");
                OnPhoneChanged();
            }
        }
        private global::System.String _Phone;
        partial void OnPhoneChanging(global::System.String value);
        partial void OnPhoneChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Email
        {
            get
            {
                return _Email;
            }
            set
            {
                OnEmailChanging(value);
                ReportPropertyChanging("Email");
                _Email = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Email");
                OnEmailChanged();
            }
        }
        private global::System.String _Email;
        partial void OnEmailChanging(global::System.String value);
        partial void OnEmailChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String TransportPartner
        {
            get
            {
                return _TransportPartner;
            }
            set
            {
                OnTransportPartnerChanging(value);
                ReportPropertyChanging("TransportPartner");
                _TransportPartner = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("TransportPartner");
                OnTransportPartnerChanged();
            }
        }
        private global::System.String _TransportPartner;
        partial void OnTransportPartnerChanging(global::System.String value);
        partial void OnTransportPartnerChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Guid> TrackingId
        {
            get
            {
                return _TrackingId;
            }
            set
            {
                OnTrackingIdChanging(value);
                ReportPropertyChanging("TrackingId");
                _TrackingId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("TrackingId");
                OnTrackingIdChanged();
            }
        }
        private Nullable<global::System.Guid> _TrackingId;
        partial void OnTrackingIdChanging(Nullable<global::System.Guid> value);
        partial void OnTrackingIdChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Orders.Website.Stores.Entities", "FK_OrderStatus_Order", "OrderStatus")]
        public EntityCollection<OrderStatus> OrderStatus
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<OrderStatus>("Orders.Website.Stores.Entities.FK_OrderStatus_Order", "OrderStatus");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<OrderStatus>("Orders.Website.Stores.Entities.FK_OrderStatus_Order", "OrderStatus", value);
                }
            }
        }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Orders.Website.Stores.Entities", "FK_OrderProcessStatus_Order", "OrderProcessStatus")]
        public OrderProcessStatus OrderProcessStatus
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<OrderProcessStatus>("Orders.Website.Stores.Entities.FK_OrderProcessStatus_Order", "OrderProcessStatus").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<OrderProcessStatus>("Orders.Website.Stores.Entities.FK_OrderProcessStatus_Order", "OrderProcessStatus").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<OrderProcessStatus> OrderProcessStatusReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<OrderProcessStatus>("Orders.Website.Stores.Entities.FK_OrderProcessStatus_Order", "OrderProcessStatus");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<OrderProcessStatus>("Orders.Website.Stores.Entities.FK_OrderProcessStatus_Order", "OrderProcessStatus", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Orders.Website.Stores.Entities", Name="OrderProcessStatus")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class OrderProcessStatus : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new OrderProcessStatus object.
        /// </summary>
        /// <param name="orderId">Initial value of the OrderId property.</param>
        /// <param name="processStatus">Initial value of the ProcessStatus property.</param>
        /// <param name="retryCount">Initial value of the RetryCount property.</param>
        public static OrderProcessStatus CreateOrderProcessStatus(global::System.Guid orderId, global::System.String processStatus, global::System.Int32 retryCount)
        {
            OrderProcessStatus orderProcessStatus = new OrderProcessStatus();
            orderProcessStatus.OrderId = orderId;
            orderProcessStatus.ProcessStatus = processStatus;
            orderProcessStatus.RetryCount = retryCount;
            return orderProcessStatus;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid OrderId
        {
            get
            {
                return _OrderId;
            }
            set
            {
                if (_OrderId != value)
                {
                    OnOrderIdChanging(value);
                    ReportPropertyChanging("OrderId");
                    _OrderId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("OrderId");
                    OnOrderIdChanged();
                }
            }
        }
        private global::System.Guid _OrderId;
        partial void OnOrderIdChanging(global::System.Guid value);
        partial void OnOrderIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String ProcessStatus
        {
            get
            {
                return _ProcessStatus;
            }
            set
            {
                OnProcessStatusChanging(value);
                ReportPropertyChanging("ProcessStatus");
                _ProcessStatus = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("ProcessStatus");
                OnProcessStatusChanged();
            }
        }
        private global::System.String _ProcessStatus;
        partial void OnProcessStatusChanging(global::System.String value);
        partial void OnProcessStatusChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String LockedBy
        {
            get
            {
                return _LockedBy;
            }
            set
            {
                OnLockedByChanging(value);
                ReportPropertyChanging("LockedBy");
                _LockedBy = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("LockedBy");
                OnLockedByChanged();
            }
        }
        private global::System.String _LockedBy;
        partial void OnLockedByChanging(global::System.String value);
        partial void OnLockedByChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> LockedUntil
        {
            get
            {
                return _LockedUntil;
            }
            set
            {
                OnLockedUntilChanging(value);
                ReportPropertyChanging("LockedUntil");
                _LockedUntil = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("LockedUntil");
                OnLockedUntilChanged();
            }
        }
        private Nullable<global::System.DateTime> _LockedUntil;
        partial void OnLockedUntilChanging(Nullable<global::System.DateTime> value);
        partial void OnLockedUntilChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.Byte[] Version
        {
            get
            {
                return StructuralObject.GetValidValue(_Version);
            }
            set
            {
                OnVersionChanging(value);
                ReportPropertyChanging("Version");
                _Version = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Version");
                OnVersionChanged();
            }
        }
        private global::System.Byte[] _Version;
        partial void OnVersionChanging(global::System.Byte[] value);
        partial void OnVersionChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Guid> BatchId
        {
            get
            {
                return _BatchId;
            }
            set
            {
                OnBatchIdChanging(value);
                ReportPropertyChanging("BatchId");
                _BatchId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("BatchId");
                OnBatchIdChanged();
            }
        }
        private Nullable<global::System.Guid> _BatchId;
        partial void OnBatchIdChanging(Nullable<global::System.Guid> value);
        partial void OnBatchIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 RetryCount
        {
            get
            {
                return _RetryCount;
            }
            set
            {
                OnRetryCountChanging(value);
                ReportPropertyChanging("RetryCount");
                _RetryCount = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("RetryCount");
                OnRetryCountChanged();
            }
        }
        private global::System.Int32 _RetryCount;
        partial void OnRetryCountChanging(global::System.Int32 value);
        partial void OnRetryCountChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Orders.Website.Stores.Entities", "FK_OrderProcessStatus_Order", "Order")]
        public Order Order
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Order>("Orders.Website.Stores.Entities.FK_OrderProcessStatus_Order", "Order").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Order>("Orders.Website.Stores.Entities.FK_OrderProcessStatus_Order", "Order").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Order> OrderReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Order>("Orders.Website.Stores.Entities.FK_OrderProcessStatus_Order", "Order");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Order>("Orders.Website.Stores.Entities.FK_OrderProcessStatus_Order", "Order", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Orders.Website.Stores.Entities", Name="OrderStatus")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class OrderStatus : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new OrderStatus object.
        /// </summary>
        /// <param name="orderId">Initial value of the OrderId property.</param>
        /// <param name="status">Initial value of the Status property.</param>
        /// <param name="timestamp">Initial value of the Timestamp property.</param>
        public static OrderStatus CreateOrderStatus(global::System.Guid orderId, global::System.String status, global::System.DateTime timestamp)
        {
            OrderStatus orderStatus = new OrderStatus();
            orderStatus.OrderId = orderId;
            orderStatus.Status = status;
            orderStatus.Timestamp = timestamp;
            return orderStatus;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid OrderId
        {
            get
            {
                return _OrderId;
            }
            set
            {
                if (_OrderId != value)
                {
                    OnOrderIdChanging(value);
                    ReportPropertyChanging("OrderId");
                    _OrderId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("OrderId");
                    OnOrderIdChanged();
                }
            }
        }
        private global::System.Guid _OrderId;
        partial void OnOrderIdChanging(global::System.Guid value);
        partial void OnOrderIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Status
        {
            get
            {
                return _Status;
            }
            set
            {
                if (_Status != value)
                {
                    OnStatusChanging(value);
                    ReportPropertyChanging("Status");
                    _Status = StructuralObject.SetValidValue(value, false);
                    ReportPropertyChanged("Status");
                    OnStatusChanged();
                }
            }
        }
        private global::System.String _Status;
        partial void OnStatusChanging(global::System.String value);
        partial void OnStatusChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime Timestamp
        {
            get
            {
                return _Timestamp;
            }
            set
            {
                OnTimestampChanging(value);
                ReportPropertyChanging("Timestamp");
                _Timestamp = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Timestamp");
                OnTimestampChanged();
            }
        }
        private global::System.DateTime _Timestamp;
        partial void OnTimestampChanging(global::System.DateTime value);
        partial void OnTimestampChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Orders.Website.Stores.Entities", "FK_OrderStatus_Order", "Order")]
        public Order Order
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Order>("Orders.Website.Stores.Entities.FK_OrderStatus_Order", "Order").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Order>("Orders.Website.Stores.Entities.FK_OrderStatus_Order", "Order").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Order> OrderReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Order>("Orders.Website.Stores.Entities.FK_OrderStatus_Order", "Order");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Order>("Orders.Website.Stores.Entities.FK_OrderStatus_Order", "Order", value);
                }
            }
        }

        #endregion
    }

    #endregion
    
}
