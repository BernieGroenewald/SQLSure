using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GeneralGlobal
{
    public class Stock : MarshalByRefObject, IDisposable
    {
        public Stock()
        {
        }

        public DataTable AllStock(string OrderBy)
        {
            using (DBConnect TU = new DBConnect())
            {
                object[] Params = { OrderBy };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Stock_GetAllStock", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable SingleStock(string StockId)
        {
            using (DBConnect TU = new DBConnect())
            {
                object[] Params = { StockId };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Stock_SingleStock", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetStockSupplier(string StockId)
        {
            using (DBConnect TU = new DBConnect())
            {
                object[] Params = { StockId };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Stock_GetSupplier", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public int SaveStock(object[] Params)
        {
            using (DBConnect TU = new DBConnect())
            {
                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Stock_InsertUpdateStock", Params);

                    return Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0].ToString());
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public int SaveStockSupplier(object[] Params)
        {
            using (DBConnect TU = new DBConnect())
            {
                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Stock_InsertUpdateSupplier", Params);

                    return Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0].ToString());
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
