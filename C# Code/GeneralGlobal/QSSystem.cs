using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GeneralGlobal
{
    class QSSystem : MarshalByRefObject, IDisposable
    {
        public string GetSystemData(string KeyName)
        {
            using (DBConnect TU = new DBConnect())
            {
                object[] Params = { KeyName };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("System_GetSystemData", Params);

                    return ds.Tables[0].Rows[0].ItemArray[0].ToString();
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public int SaveSystemData(string KeyName, string KeyValue)
        {
            using (DBConnect TU = new DBConnect())
            {
                object[] Params = { KeyName, KeyValue };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("System_InsertUpdateSystemData", Params);

                    return Convert.ToInt16(ds.Tables[0].Rows[0].ItemArray[0].ToString());
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
