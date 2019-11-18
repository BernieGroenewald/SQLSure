using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GeneralGlobal
{
    public class PersonEntity : MarshalByRefObject, IDisposable
    {
        public DataTable AllEntities(string FilterStr1, string FilterStr2, string FilterStr3)
        {
            using (DBConnect TU = new DBConnect())
            {
                object[] Params = { FilterStr1, FilterStr2, FilterStr3 };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Entity_GetAllEntities", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetPersonEntityDetail(string EntityType, int PersonEntityId)
        {
            using (DBConnect TU = new DBConnect())
            {
                object[] Params = { PersonEntityId };

                try
                {
                    DataSet ds = null;

                    if (EntityType == "Person")
                    {
                        ds = TU.ExecuteDataset("Entity_GetPerson", Params);
                    }
                    else
                    {
                        ds = TU.ExecuteDataset("Entity_GetEntity", Params);
                    }

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetPersonEntitySystemType(string EntityType, int PersonEntityId)
        {
            using (DBConnect TU = new DBConnect())
            {
                object[] Params = { PersonEntityId };

                try
                {
                    DataSet ds = null;

                    if (EntityType == "Person")
                    {
                        ds = TU.ExecuteDataset("Entity_GetSystemPersonType", Params);
                    }
                    else
                    {
                        ds = TU.ExecuteDataset("Entity_GetSystemEntityType", Params);
                    }

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public int SavePersonEntity(object[] Params)
        {
            using (DBConnect TU = new DBConnect())
            {
                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Entity_InsertUpdatePersonEntity", Params);

                    return Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0].ToString());
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public int SavePersonEntityAddress(object[] Params)
        {
            using (DBConnect TU = new DBConnect())
            {
                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Entity_InsertUpdatePersonEntityAddress", Params);

                    return Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0].ToString());
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetPersonEntityAddress(string EntityType, int PersonEntityId)
        {
            using (DBConnect TU = new DBConnect())
            {
                object[] Params = { PersonEntityId, EntityType };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Entity_GetPersonEntityAddress", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public int SavePersonEntityContactNumber(object[] Params)
        {
            using (DBConnect TU = new DBConnect())
            {
                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Entity_InsertUpdatePersonEntityContactNumber", Params);

                    return Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0].ToString());
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetPersonEntityContactNumbers(string EntityType, int PersonEntityId)
        {
            using (DBConnect TU = new DBConnect())
            {
                object[] Params = { PersonEntityId, EntityType };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Entity_GetPersonEntityContactNumbers", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public int DeletePersonEntity(string EntityType, int PersonEntityId)
        {
            using (DBConnect TU = new DBConnect())
            {
                object[] Params = { PersonEntityId, EntityType, Globals.UserName };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Entity_DeletePersonEntity", Params);

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
