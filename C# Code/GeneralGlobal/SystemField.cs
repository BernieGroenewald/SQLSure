using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GeneralGlobal
{
    public class SystemField : MarshalByRefObject, IDisposable
    {
        public SystemField()
        {
        }

        public DataTable GetSystemField(string FieldName)
        {
            using (DBConnect TU = new DBConnect())
            {
                object[] Params = { FieldName };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("System_GetField", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetToolTips(string SubSystem, string ControlName)
        {
            using (DBConnect TU = new DBConnect())
            {
                object[] Params = { SubSystem, ControlName };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("System_GetToolTips", Params);

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
