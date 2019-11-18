using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GeneralGlobal
{
    public class Supplier : MarshalByRefObject, IDisposable
    {
        public Supplier()
        {
        }

        public DataTable GetAllSuppliers()
        {
            using (DBConnect TU = new DBConnect())
            {
                object[] Params = { };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Supplier_GetSuppliers", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
