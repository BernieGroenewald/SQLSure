using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace GeneralGlobal
{
    public class Logon : MarshalByRefObject, IDisposable
    {
        public Logon()
        {

        }

        public int UpdatePassword(string Username, string Password)
        {
            using (DBConnect TU = new DBConnect())
            {
                object[] Params = { Username, Password, Username };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Security_ChangePassword", Params);

                    return 0;
                }

                catch
                {
                    return 1;
                }
            }
        }

        public string DoLogon(string Username, string Password)
        {
            using (DBConnect TU = new DBConnect())
            {
                object[] Params = { Username, Password };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Security_CheckLogin", Params);

                    return ds.Tables[0].Rows[0].ItemArray[0].ToString();
                    
                }

                catch
                {
                    return "Error...";
                }
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
