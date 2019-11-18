using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;


namespace GeneralGlobal
{
    public class Users : MarshalByRefObject, IDisposable
    {
        public string ConnectionString;

        public Users()
        {
        }

        public DataTable AllUsers()
        {
            using (DBConnect TU = new DBConnect())
            {
                TU.ConnectionString = ConnectionString;

                object[] Params = { "All" };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Security_GetSystemUser", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable AllRoles()
        {
            using (DBConnect TU = new DBConnect())
            {
                TU.ConnectionString = ConnectionString;

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Security_GetSystemRoles");

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool RoleHasFunction(string RoleName, string FunctionName)
        {
            Boolean Found;

            using (DBConnect TU = new DBConnect())
            {
                TU.ConnectionString = ConnectionString;

                try
                {
                    DataSet ds = null;

                    object[] Params = { RoleName, FunctionName };

                    ds = TU.ExecuteDataset("Security_SystemRoleHasFunction", Params);

                    Found = false;

                    foreach (DataRow row in ds.Tables [0].Rows)
                    {
                        Found = true;
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

            
            if (Found)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable AllFunctions()
        {
            using (DBConnect TU = new DBConnect())
            {
                TU.ConnectionString = ConnectionString;

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Security_GetSystemFunctions");

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable RolesByUser()
        {
            using (DBConnect TU = new DBConnect())
            {
                TU.ConnectionString = ConnectionString;

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Security_AllSystemRolesByUsers");

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable RolesByRole()
        {
            using (DBConnect TU = new DBConnect())
            {
                TU.ConnectionString = ConnectionString;

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Security_RolesByRole");

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public string SaveRoleFunction(string RoleName, string FunctionName)
        {
            using (DBConnect TU = new DBConnect())
            {
                TU.ConnectionString = ConnectionString;

                object[] Params = { RoleName, FunctionName, Globals.UserName };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Security_InsertUpdateRoleFunction", Params);

                    return ds.Tables[0].Rows[0].ItemArray[0].ToString();
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public string SaveUserRole(string RoleName, string UserName)
        {
            using (DBConnect TU = new DBConnect())
            {
                TU.ConnectionString = ConnectionString;

                object[] Params = { UserName, RoleName, Globals.UserName };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Security_InsertUpdateUserRole", Params);

                    return ds.Tables[0].Rows[0].ItemArray[0].ToString();
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void GrantAllRoleFunction(string RoleName)
        {
            bool retval;

            using (DBConnect DC = new DBConnect())
            {
                DC.ConnectionString = ConnectionString;

                try
                {
                    if (!ExistingRole(RoleName))
                    {
                        //Create the role

                        if (DC.SqlCommand("insert into Role (Role) values('" + RoleName + "')"))
                        {
                            // Add rights to the role

                            retval = DC.SqlCommand("delete from SystemRole where Role = '" + RoleName + "'");
                            retval = DC.SqlCommand("insert into SystemRole (Role, ProgramName) select distinct '" + RoleName + "', FunctionName from SystemFunctions");
                        }
                    }
                    else
                    {
                        //Add rights to the role

                        retval = DC.SqlCommand("delete from SystemRole where Role = '" + RoleName + "'");
                        retval = DC.SqlCommand("insert into SystemRole (Role, ProgramName) select distinct '" + RoleName + "', FunctionName from SystemFunctions");
                    }
                }

                catch
                {
                }
            }
        }

        public bool DeleteRoleFunction(string RoleName)
        {
            using (DBConnect TU = new DBConnect())
            {
                TU.ConnectionString = ConnectionString;

                object[] Params = { RoleName, Globals.UserName };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Security_DeleteRole", Params);

                    return true;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public int NewRole(string RoleName)
        {
            using (DBConnect TU = new DBConnect())
            {
                TU.ConnectionString = ConnectionString;

                object[] Params = { RoleName, Globals.UserName };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Security_InsertRole", Params);

                    return 0;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void GrantManagerRoleFunction(string RoleName)
        {
            bool retval;

            using (DBConnect DC = new DBConnect())
            {
                try
                {
                    DC.ConnectionString = ConnectionString;

                    if (!ExistingRole(RoleName))
                    {
                        //Create the role

                        if (DC.SqlCommand("insert into Role (Role) values('" + RoleName + "')"))
                        {
                            // Add rights to the role

                            retval = DC.SqlCommand("delete from SystemRole where Role = '" + RoleName + "'");
                            retval = DC.SqlCommand("insert into SystemRole (Role, ProgramName) select distinct '" + RoleName + "', FunctionName from SystemFunctions where RoleAccess like '%M%'");
                        }
                    }
                    else
                    {
                        //Add rights to the role

                        retval = DC.SqlCommand("delete from SystemRole where Role = '" + RoleName + "'");
                        retval = DC.SqlCommand("insert into SystemRole (Role, ProgramName) select distinct '" + RoleName + "', FunctionName from SystemFunctions where RoleAccess like '%M%'");
                    }
                }

                catch
                {
                }
            }
        }

        public void GrantCashierRoleFunction(string RoleName)
        {
            bool retval;

            using (DBConnect DC = new DBConnect())
            {
                DC.ConnectionString = ConnectionString;

                try
                {
                    if (!ExistingRole(RoleName))
                    {
                        //Create the role

                        if (DC.SqlCommand("insert into Role (Role) values('" + RoleName + "')"))
                        {
                            // Add rights to the role

                            retval = DC.SqlCommand("delete from SystemRole where Role = '" + RoleName + "'");
                            retval = DC.SqlCommand("insert into SystemRole (Role, ProgramName) select distinct '" + RoleName + "', FunctionName from SystemFunctions where RoleAccess like '%C%'");
                        }
                    }
                    else
                    {
                        //Add rights to the role

                        retval = DC.SqlCommand("delete from SystemRole where Role = '" + RoleName + "'");
                        retval = DC.SqlCommand("insert into SystemRole (Role, ProgramName) select distinct '" + RoleName + "', FunctionName from SystemFunctions where RoleAccess like '%C%'");
                    }
                }

                catch
                {
                }
            }
        }

        public void GrantWaiterRoleFunction(string RoleName)
        {
            bool retval;

            using (DBConnect DC = new DBConnect())
            {
                DC.ConnectionString = ConnectionString;

                try
                {
                    if (!ExistingRole(RoleName))
                    {
                        //Create the role

                        if (DC.SqlCommand("insert into Role (Role) values('" + RoleName + "')"))
                        {
                            // Add rights to the role

                            retval = DC.SqlCommand("delete from SystemRole where Role = '" + RoleName + "'");
                            retval = DC.SqlCommand("insert into SystemRole (Role, ProgramName) select distinct '" + RoleName + "', FunctionName from SystemFunctions where RoleAccess like '%W%'");
                        }
                    }
                    else
                    {
                        //Add rights to the role

                        retval = DC.SqlCommand("delete from SystemRole where Role = '" + RoleName + "'");
                        retval = DC.SqlCommand("insert into SystemRole (Role, ProgramName) select distinct '" + RoleName + "', FunctionName from SystemFunctions where RoleAccess like '%W%'");
                    }
                }

                catch
                {
                }
            }
        }

        public DataTable SingleUser(string UserName)
        {
            using (DBConnect TU = new DBConnect())
            {
                TU.ConnectionString = ConnectionString;

                object[] Params = { UserName };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Security_GetSystemUser", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public string SaveUser(object[] Params)
        {
            using (DBConnect TU = new DBConnect())
            {
                TU.ConnectionString = ConnectionString;

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Security_InsertUpdateSystemUser", Params);

                    return ds.Tables[0].Rows[0].ItemArray[0].ToString();
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteUser(string UserName)
        {
            using (DBConnect TU = new DBConnect())
            {
                TU.ConnectionString = ConnectionString;

                object[] Params = { UserName, Globals.UserName };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Security_DeleteUser", Params);

                    return true;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ExistingRoleFunction(string RoleName, string FunctionName)
        {
            Boolean Found;

            using (DBConnect TU = new DBConnect())
            {
                TU.ConnectionString = ConnectionString;

                DataTable dr;

                Found = false;

                try
                {
                    dr = TU.DataTable("SELECT * from SystemRole where Role = '" + RoleName + "' and ProgramName = '" + FunctionName + "'");

                    foreach (DataRow row in dr.Rows)
                    {
                        Found = true;
                    }
                }

                catch
                {
                }
            }

            if (Found)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ExistingUserRole(string RoleName, string UserName)
        {
            Boolean Found;

            using (DBConnect TU = new DBConnect())
            {
                TU.ConnectionString = ConnectionString;

                DataTable dr;

                Found = false;

                try
                {
                    dr = TU.DataTable("SELECT * from UserRole where Role = '" + RoleName + "' and UserName = '" + UserName + "'");

                    foreach (DataRow row in dr.Rows)
                    {
                        Found = true;
                    }
                }

                catch
                {
                }
            }

            if (Found)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ExistingRole(string RoleName)
        {
            Boolean Found;

            using (DBConnect TU = new DBConnect())
            {
                TU.ConnectionString = ConnectionString;

                DataTable dr;

                Found = false;

                try
                {
                    dr = TU.DataTable("SELECT * from Role where Role = '" + RoleName + "'");

                    foreach (DataRow row in dr.Rows)
                    {
                        Found = true;
                    }
                }

                catch
                {
                }
            }

            if (Found)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ExistingUser(string Username)
        {
            Boolean Found = false;

            using (DBConnect TU = new DBConnect())
            {
                TU.ConnectionString = ConnectionString;

                object[] Params = { Username };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Security_GetSystemUser", Params);

                    
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

            if (Found)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable UserHasAccess(string Username, string SystemArea)
        {
            using (DBConnect TU = new DBConnect())
            {
                TU.ConnectionString = ConnectionString;

                object[] Params = { Username, SystemArea };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("Security_GetUserAccess", Params);

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
