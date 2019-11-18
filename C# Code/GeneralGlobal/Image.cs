using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GeneralGlobal
{
    public class Image : MarshalByRefObject, IDisposable
    {
        public int SaveImage(object[] Params)
        {
            using (DBConnect TU = new DBConnect())
            {
                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Image_InsertImage", Params);

                    return Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0].ToString());
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetAllImages()
        {
            using (DBConnect TU = new DBConnect())
            {
                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Image_GetImages", 0);
                    
                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetImagesByCategory(int ImageCategoryId)
        {
            using (DBConnect TU = new DBConnect())
            {
                object[] Params = { ImageCategoryId };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Image_GetImages", Params);
                    
                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetImageById(int ImageId)
        {
            using (DBConnect TU = new DBConnect())
            {
                object[] Params = { ImageId };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Image_GetImageById", Params);

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
