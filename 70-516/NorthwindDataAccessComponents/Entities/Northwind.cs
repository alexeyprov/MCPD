using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Northwind.Data.Entities
{
    public class NorthwindDataValidationException : ApplicationException
    {
    }

    partial class NorthwindDataContext
    {
    }

    partial class Customer
    {
    }

    partial class Contact
    {
        private static readonly Regex PHONE_FAX_REGEX = new Regex(@"^\(\d{3}\)\s+\d{3}[-\s]+\d{2}[-\s]?\d{2}$");

        partial void OnFaxChanging(string value)
        {
            //ValidatePhoneOrFax(value);
        }

        partial void OnPhoneChanging(string value)
        {
            //ValidatePhoneOrFax(value);
        }

        private void ValidatePhoneOrFax(string value)
        {
            if (!PHONE_FAX_REGEX.IsMatch(value))
            {
                throw new NorthwindDataValidationException();
            }
        }
    }

    [MetadataType(typeof(ProductMetaData))]
    partial class Product
    {
        private int _supplierID;
        private int _categoryID;

        public int SupplierID
        {
            get
            {
                if (0 == _supplierID && this.Supplier != null)
                {
                    _supplierID = this.Supplier.SupplierID;
                }

                return _supplierID;
            }
            set
            {
                _supplierID = value;
            }
        }

        public int CategoryID
        {
            get
            {
                if (0 == _categoryID && this.Category != null)
                {
                    _categoryID = this.Category.CategoryID;
                }

                return _categoryID;
            }
            set
            {
                _categoryID = value;
            }
        }

        public class ProductMetaData
        {
            [StringLength(40)]
            public object ProductName
            {
                get;
                set;
            }

            [StringLength(20)]
            public object QuantityPerUnit
            {
                get;
                set;
            }

            [UIHint("Reorder Level")]
            [Range(0, 20000)]
            public object ReorderLevel
            {
                get;
                set;
            }

            [Range(0.0, 1000000.0)]
            [DisplayFormat(DataFormatString = "{0:C}")]
            public object UnitPrice
            {
                get;
                set;
            }

            [Range(0, 20000)]
            [DisplayName("In Stock")]
            [UIHint("RedText")]
            public object UnitsInStock
            {
                get;
                set;
            }

            [Range(0, 20000)]
            public object UnitsOnOrder
            {
                get;
                set;
            }
        }
    }

    [MetadataType(typeof(OrderMetadata))]
    partial class Order
    {
        public class OrderMetadata
        {
            [DisplayFormat(DataFormatString = "{0:d}")]
            [DisplayName("Ordered on")]
            [Required]
            public object OrderDate
            {
                get;
                set;
            }

            [DisplayFormat(DataFormatString = "{0:d}")]
            [DisplayName("Required by")]
            public object RequiredDate
            {
                get;
                set;
            }

            [DisplayFormat(DataFormatString = "{0:d}")]
            [DisplayName("Shipped on")]
            public object ShippedDate
            {
                get;
                set;
            }

            [Range(typeof(decimal), "0", "10000")]
            [Required]
            public object Freight
            {
                get;
                set;
            }
        }
    }

    [DisplayName("Order Lines")]
    partial class OrderLine
    {
    }

    [ScaffoldTable(false)]
    partial class Supplier
    {
    }

    [MetadataType(typeof(ShipperMetadata))]
    partial class Shipper
    {

        private class ShipperMetadata
        {
            [Required]
            public string CompanyName
            {
                get;
                set;
            }

            [DataType(DataType.PhoneNumber)]
            [Required]
            public string Phone
            {
                get;
                set;
            }
        }
    }
}
