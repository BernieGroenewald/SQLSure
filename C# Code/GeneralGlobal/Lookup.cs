using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GeneralGlobal
{
    public class Lookup : MarshalByRefObject, IDisposable
    {
        public string ConnectionString;

        public Lookup()
        {
        }

        public DataTable GetLookup(string LookupType, string OrderBy)
        {
            using (DBConnect TU = new DBConnect())
            {
                TU.ConnectionString = ConnectionString;

                object[] Params = { LookupType };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("System_GetLookupFromParent", Params);

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
