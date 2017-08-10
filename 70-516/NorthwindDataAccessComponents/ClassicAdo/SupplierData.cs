using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;

namespace Northwind.Data.ClassicAdo
{
    [DataObject]
    public class SupplierData : BaseDataAccessComponent
    {
        #region Private Constants

        private const string SP_SUPPLIER_BY_ID_GET = "SP_SUPPLIER_BY_ID_GET";
        private const string SP_SUPPLIERS_GET = "SP_SUPPLIERS_GET";
        private const string SP_SUPPLIERS_ALL_GET = "SP_SUPPLIERS_ALL_GET";
        private const string SP_SUPPLIER_COUNT_GET = "SP_SUPPLIER_COUNT_GET";

        #endregion

        #region Constructor

        public SupplierData()
        {
        }

        public SupplierData(string connectionString)
            : base(connectionString)
        {
        }

        #endregion

        #region Public Methods

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<Supplier> GetAllSuppliers(int startRow, int maxRows)
        {
            List<Supplier> retval = new List<Supplier>();

            using (DbCommand cmd = GetStoredProcCommand(SP_SUPPLIERS_GET, startRow, maxRows))
            {
                using (DbDataReader reader = ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        retval.Add(CreateSupplier(reader));
                    }
                }
            }

            return retval;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public IList<Supplier> GetAllSuppliers()
        {
            IList<Supplier> retval = new List<Supplier>();

            using (DbCommand cmd = GetStoredProcCommand(SP_SUPPLIERS_ALL_GET))
            {
                using (DbDataReader reader = ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        retval.Add(CreateSupplier(reader));
                    }
                }
            }

            return retval;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public int GetSupplierCount()
        {
            using (DbCommand cmd = GetStoredProcCommand(SP_SUPPLIER_COUNT_GET))
            {
                using (DbDataReader reader = ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        return (int)reader[0];
                    }
                }
            }

            return 0;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public Supplier GetSupplier(string id)
        {
            using (DbCommand cmd = GetStoredProcCommand(SP_SUPPLIER_BY_ID_GET, id))
            {
                using (DbDataReader reader = ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        return CreateSupplier(reader);
                    }
                }
            }

            return null;
        }

        #endregion

        #region Implementation

        private static Supplier CreateSupplier(DbDataReader reader)
        {
            Supplier s = new Supplier();

            s.ID = reader.GetInt32(0);
            s.CompanyName = reader[1].ToString();

            s.Contact.Name = reader[2].ToString();
            s.Contact.Title = reader[3].ToString();
            s.Contact.Phone = reader[8].ToString();

            s.Address.StreetAddress = reader[4].ToString();
            s.Address.City = reader[5].ToString();
            s.Address.Region = reader[9].ToString();
            s.Address.PostalCode = reader[7].ToString();
            s.Address.Country = reader[6].ToString();

            return s;
        }

        #endregion
    }
}
