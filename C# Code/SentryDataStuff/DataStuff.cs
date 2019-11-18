using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;


namespace SentryDataStuff
{
    public class DataStuff : MarshalByRefObject, IDisposable
    {
        //public string ConnectionString = Properties.Settings.Default.connectionString;

        public string ConnectionString = string.Empty;

        private string _DatabaseSelection = string.Empty;
        private string _ObjectSelection = string.Empty;
        private string _ProjectScriptFilePath = string.Empty;
        private string _ServerAliasSelected = string.Empty;

        private string _DSServerName = string.Empty;
        private string _DSUserName = string.Empty;
        private string _DSPassword = string.Empty;
        private string _DSIntegratedSecurity = string.Empty;
        private string _DSDefaultDatabase = string.Empty;
        
        public DataStuff()
        {
            LoadDataStoreConnectionString();
        }

        private void LoadDataStoreConnectionString()
        {
            LoadDataStoreSettings();

            if (_DSServerName.Trim() != "")
            {
                if (_DSIntegratedSecurity.Trim() == "Y")
                {
                    ConnectionString = "Data Source=" + _DSServerName.Trim() + ";Initial Catalog=" + _DSDefaultDatabase.Trim()  + ";Integrated Security=True";
                }
                else
                {
                    ConnectionString = "Data Source=" + _DSServerName.Trim() + ";Initial Catalog=" + _DSDefaultDatabase.Trim() + ";User ID=" + _DSUserName.Trim() + ";Password=" + _DSPassword.Trim();
                }
            }
        }

        public string DataStoreConnectionString
        {
            get
            {
                LoadDataStoreSettings();

                if (_DSServerName.Trim() != "")
                {
                    if (_DSIntegratedSecurity.Trim() == "Y")
                    {
                        ConnectionString = "Data Source=" + _DSServerName.Trim() + ";Integrated Security=True";
                    }
                    else
                    {
                        ConnectionString = "Data Source=" + _DSServerName.Trim() + ";User ID=" + _DSUserName.Trim() + ";Password=" + _DSPassword.Trim();
                    }
                }

                return ConnectionString;
            }
        }

        public string DataStoreServerName
        {
            get
            {
                LoadDataStoreSettings();
                return _DSServerName;
            }
            set
            {
                _DSServerName = value;
                SaveDataStoreSettings();
            }
        }

        public string DataStoreUserName
        {
            get
            {
                LoadDataStoreSettings();
                return _DSUserName;
            }
            set
            {
                _DSUserName = value;
                SaveDataStoreSettings();
            }
        }

        public string DataStorePassword
        {
            get
            {
                LoadDataStoreSettings();
                return _DSPassword;
            }
            set
            {
                _DSPassword = value;
                SaveDataStoreSettings();
            }
        }

        public string DataStoreIntegratedSecurity
        {
            get
            {
                LoadDataStoreSettings();
                return _DSIntegratedSecurity;
            }
            set
            {
                _DSIntegratedSecurity = value;
                SaveDataStoreSettings();
            }
        }

        public string DataStoreDefaultDatabase
        {
            get
            {
                LoadDataStoreSettings();
                return _DSDefaultDatabase;
            }
            set
            {
                _DSDefaultDatabase = value;
                SaveDataStoreSettings();
            }
        }

        private void LoadDataStoreSettings()
        {
            DataStoreConfiguration DS;

            string path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)).FullName;

            XmlSerializer mySerializer = new XmlSerializer(typeof(DataStoreConfiguration));

            try
            {
                using (FileStream myFileStream = new FileStream(path + "\\SentryDataStoreSettings.xml", FileMode.Open))
                {
                    DS = (DataStoreConfiguration)mySerializer.Deserialize(myFileStream);

                    _DSServerName = DS.ServerName;
                    _DSUserName = DS.UserName;
                    _DSPassword = DS.Password;
                    _DSIntegratedSecurity = DS.IntegratedSecurity;
                    _DSDefaultDatabase = DS.DefaultDatabase;
                }
            }

            catch
            {
                return;
            }
        }

        public void SaveDataStoreSettings()
        {
            string path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)).FullName;

            DataStoreConfiguration DS = new DataStoreConfiguration();

            DS.ServerName = _DSServerName;
            DS.UserName = _DSUserName;
            DS.Password = _DSPassword;
            DS.IntegratedSecurity = _DSIntegratedSecurity;
            DS.DefaultDatabase = _DSDefaultDatabase;

            XmlSerializer mySerializer = new XmlSerializer(typeof(DataStoreConfiguration));

            try
            {
                using (StreamWriter myWriter = new StreamWriter(path + "\\SentryDataStoreSettings.xml"))
                {
                    mySerializer.Serialize(myWriter, DS);
                    myWriter.Close();
                }
            }
            catch
            {
                return;
            }
        }

        public DataTable GetServers(int UserID)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { UserID };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ServerGetUserServers", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetObjectHistorySSMS(string ServerName, int UserID)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ServerName, UserID };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ServerGetObjectHistorySSMS", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetServersInServerGroup(string ServerAlias, int UserID)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ServerAlias, UserID };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ServerGetServersInServerGroupEncrypted", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool IsServerGroupDefined()
        {
            DataTable dt = new DataTable();
            string Sql = string.Empty;

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                Sql = "select count(*) as GroupCount from SVC_LU_ServerGroup";

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    dt = dbs.DataTable(Sql);

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

                catch
                {
                    return false;
                }
            }
        }
        public DataTable GetServersInGroupDetail(string ServerGroup)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ServerGroup };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ServerGetServersInGroupDetail", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public void SaveServerGroup(string ServerGroup, string ServerAlias, string ServerName, string ServerRole, string ReleaseOrder )
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ServerGroup, ServerAlias, ServerName, ServerRole, ReleaseOrder };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ServerSaveGroup", Params);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetServerRole(string ServerName, int UserID)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ServerName, UserID };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_GetServerRole", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetServerRoles()
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ServerGetServerRoles", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetServerAliases()
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ServerGetServerAliases", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetCheckOutFromServers(string DevServerAlias)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { DevServerAlias };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ServerGetCheckoutFromServer", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetCheckOutFromServersProduction(string DevServerAlias)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { DevServerAlias };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ServerGetCheckoutFromServerProduction", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetObjectStatus(string QTNumber)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { QTNumber };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ServerGetObjectStatus", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetProjects()
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ServerGetProjects", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetBackupSets()
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ServerGetBackupSets", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetBackupSetDetail(string SetName)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { SetName };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ServerGetBackupSetDetail", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public DataTable GetServerGroups()
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ServerGetServerGroups", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetServerGroupID(string GroupName)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { GroupName };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ServerGetServerGroupID", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetServerGroupFromAlias(string AliasName)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { AliasName };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ServerGetServerGroupFromAlias", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public string IsProjectActive(string ProjectName)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ProjectName };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ServerGetProjectActive", Params);

                    return ds.Tables[0].Rows[0][0].ToString();
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        
        public DataTable GetProjectsByServerGroup(string GroupName, string IncludeInactive)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { GroupName, IncludeInactive };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ServerGetProjectsByServerGroup", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetPersons()
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_Security_GetSystemUsers", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetServerDetail(string ServerAlias, int UserID)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ServerAlias, UserID };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ServerGetServerDetailEncrypted", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetServerAliasDetail(string ServerAlias)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ServerAlias };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ServerGetServerAliasDetail", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetServerDetailUAT(string ServerAlias, int UserID)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ServerAlias, UserID };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ServerGetUserUATServer", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetServerDetailPreProd(string ServerAlias, int UserID)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ServerAlias, UserID };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ServerGetUserPreProdServer", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetServerDetailProd(string ServerAlias, int UserID)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ServerAlias, UserID };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ServerGetUserProdServer", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable GetObjectStatusDetail(string ServerAlias, string ObjectName)
        {
            DataTable dt = new DataTable();
            string Sql = string.Empty;

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    Sql = "declare @ServerAliasID int " +
                           "select @ServerAliasID = ServerAliasID from SVC_LU_ServerAlias where ServerAliasDesc = '" + ServerAlias + "' " +
                           "select top 1 OS.ObjectStatusDesc, " +
                           "OH.HistoryID, " +
                           "OH.ServerID, " +
                           "OH.HistoryDate, " +
                           "OH.ObjectStatusID, " +
                           "OH.UserName, " +
                           "OH.ObjectName, " +
                           "OH.DatabaseName, " +
                           "OH.ObjectText, " +
                           "OH.Comment, " +
                           "'" + ServerAlias + "' as ServerAlias, " +
                           "SA.ServerName, " +
                           "P.ProjectName, " +
                           "OH.ObjectVersion " +
                           "from SVC_LU_ObjectStatus AS OS INNER JOIN " +
                           "SVC_ObjectHistory AS OH ON OS.ObjectStatusID = OH.ObjectStatusID INNER JOIN " +
                           "SVC_Server AS S ON OH.ServerID = S.ServerID INNER JOIN " +
                           "SVC_LU_ServerAlias as SA ON S.ServerAliasID = SA.ServerAliasID LEFT OUTER JOIN " +
                           "SVC_Project AS P ON OH.ProjectID = P.ProjectID " +
                           "where OH.ObjectName = '" + ObjectName + "' " +
                           "order by OH.HistoryID desc";

                    dt = dbs.DataTable(Sql);

                    return dt;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //public DataTable GetObjectHistory(string ObjectName)
        //{
        //    DataSet ds = new DataSet();

        //    using (DBStuff dbs = new DBStuff())
        //    {
        //        dbs.ConnectionString = ConnectionString;

        //        object[] Params = { ObjectName };

        //        try
        //        {
        //            dbs.ConnectionTimeOut = 0;
        //            dbs.CommandTimeOut = 0;

        //            ds = dbs.ExecuteDataset("SVC_ServerGetObjectHistory", Params);

        //            return ds.Tables[0];
        //        }

        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}

        public DataTable GetObjectHistory(string DatabaseName, string ObjectName)
        {
            DataTable dt = new DataTable();
            string Sql = string.Empty;

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    Sql = "select OH.ObjectVersion as [ObjectVersion], " + 
                           "OS.ObjectStatusDesc as [Status], " +
	                       "OH.HistoryDate as [Date], " +
		                   "OH.UserName as [User], " +
		                   "OH.ObjectName as [Name], " +
		                   "OH.DatabaseName as [Database], " +
		                   "OH.Comment as [Comment], " +
		                   "LSA.ServerAliasDesc as [Alias], " +
		                   "LSA.ServerName as [Server], " +
		                   "OH.ObjectText as [Text] " +
		                   "from SVC_LU_ObjectStatus as OS inner join " +
                           "SVC_ObjectHistory as OH on OS.ObjectStatusID = OH.ObjectStatusID inner join " +
                           "SVC_Server as S on OH.ServerID = S.ServerID inner join " +
                           "SVC_LU_ServerAlias as LSA on S.ServerAliasID = LSA.ServerAliasID " +
                           "where OH.ObjectName = '" + ObjectName + "' " +
                           "and OH.DatabaseName = '" + DatabaseName + "' " +
                           "order by OH.HistoryID desc";

                    dt = dbs.DataTable(Sql);

                    return dt;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetObjectHistoryLast(string ObjectName, int ServerAliasID)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ObjectName, ServerAliasID };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ServerGetObjectHistoryLast", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public int GetServerID(string ServerAlias, int UserID)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ServerAlias, UserID };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ServerGetServerID", Params);

                    return Convert.ToInt32(ds.Tables[0].Rows[0]["ServerID"].ToString());
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool IsDevServer(string ServerAlias, int UserID)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ServerAlias, UserID };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ServerIsDevServer", Params);

                    if (ds.Tables[0].Rows[0]["IsDevServer"].ToString() == "Y")
                    {
                        return true;
                    }

                    return false;
                }

                catch
                {
                    return false;
                }
            }
        }

        public byte[] Verbloem(string ValueIn)
        {
            DataTable dt = new DataTable();

            string Sql = string.Empty;
            string Verbloemer = "Anelda_Chanelle_Christopher";

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                Sql = "select EncryptByPassPhrase('"+ Verbloemer +  "', '" + ValueIn + "')";

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    dt = dbs.DataTable(Sql);

                    if (dt.Rows.Count > 0)
                    {
                        return (byte[])dt.Rows[0][0];
                    }
                }

                catch (Exception ex)
                {
                    
                }
            }

            return null;
        }

        public string Ontsyfer(byte[] ValueIn)
        {
            DataTable dt = new DataTable();

            string Sql = string.Empty;
            string Verbloemer = "Anelda_Chanelle_Christopher";

            try {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    Sql = "select convert(varchar, DecryptByPassPhrase(@Verbloemer, @ValueIn))";

                    using (SqlCommand cmd = new SqlCommand(Sql, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("Verbloemer", SqlDbType.VarChar).Value = Verbloemer;
                        cmd.Parameters.Add("ValueIn", SqlDbType.VarBinary, -1).Value = ValueIn;

                        conn.Open();
                        return (string)cmd.ExecuteScalar();
                    }
                }
            }
            
            catch (Exception ex)
            {
                return "";
            }
        }

        public int GetUserCount()
        {
            DataTable dt = new DataTable();
            string Sql = string.Empty;

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    Sql = "select count(*) from SVC_SystemUser where [Active] = 'Y'";

                    dt = dbs.DataTable(Sql);

                    return Convert.ToInt32(dt.Rows[0][0]);
                }

                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public string GetSystemSetting(string KeyName)
        {
            DataSet ds = new DataSet();
            string RetVal = string.Empty;

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { KeyName };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_SystemGetSettings", Params);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        RetVal = Ontsyfer((byte[])ds.Tables[0].Rows[0]["KeyValue"]);
                        return RetVal;
                    }
                    else
                    {
                        return "";
                    }
                    
                }

                catch (Exception ex)
                {
                    return "";
                }
            }
        }

        public void SaveSystemSetting(string KeyName, string KeyValue)
        {
            DataSet ds = new DataSet();
            byte[] EncryptedValue;

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;
                EncryptedValue = Verbloem(KeyValue);


                object[] Params = { KeyName, EncryptedValue };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_SystemSaveSettings", Params);
                }

                catch (Exception ex)
                {
                }
            }
        }

        public void SaveServer(string ServerAlias, string UserName, string Password, string IntegratedSecurity, int UserID)
        {
            DataSet ds = new DataSet();
            byte[] EncryptedPassword;

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;
                EncryptedPassword = Verbloem(Password);

                object[] Params = { ServerAlias, UserName, EncryptedPassword, IntegratedSecurity, UserID };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ServerSaveServerSettings", Params);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void SaveProject(string ProjectName, string ProjectDesc, int UserID, string ServerGroup, string ProjectActive)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ProjectName, ProjectDesc, UserID, ServerGroup, ProjectActive };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_SaveProject", Params);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void SaveBackupSet(string SetName, string SetDesc, int UserID, string ServerAlias, string SetActive)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { SetName, SetDesc, UserID, ServerAlias, SetActive };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_SaveBackupSet", Params);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void SaveProjectStatus(string ProjectName, string ProjectStatus, string Comment, int UserID)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ProjectName, ProjectStatus, Comment, UserID };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_SaveProjectStatus", Params);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void SaveProjectObject(string ProjectName, string FileObject, string FileObjectDesc, string DBName, string DBObjectName, int ObjectSequence, string ObjectSelected, int UserID, string DoDelete, string ProjectActive)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ProjectName, FileObject, FileObjectDesc, DBName, DBObjectName, ObjectSequence, ObjectSelected, UserID, DoDelete, ProjectActive };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_SaveProjectDetail", Params);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void SaveBackupSetDatabaseSelected(string BackupSetName, string DatabaseName, string DoDelete)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { BackupSetName, DatabaseName, DoDelete };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_SaveBackupSetDatabase", Params);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void SaveBackupSetObjectTypeSelected(string BackupSetName, string ObjectType, string DoDelete)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { BackupSetName, ObjectType, DoDelete };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_SaveBackupSetObjectType", Params);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void UpdateChangeLog(string ServerName, string ProjectName, int UserID)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ServerName, ProjectName, UserID };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ProjectUpdateChangeLog", Params);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetBackupSetDatabaseSelected(string BackupSetName)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { BackupSetName };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_GetBackupSetDatabase", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetRestoreSetObject(string BackupSetName)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { BackupSetName };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_GetRestoreSetObject", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetBackupSetObjectTypeSelected(string BackupSetName)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { BackupSetName };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_GetBackupSetObjectType", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //public bool BackupObject(string ServerName, string ObjectStatus, string UserName, string ObjectName, string DatabaseName, string ObjectText, string Comment, string ProjectName, string BackupSetName, string ObjectType, string DoDelete)
        //{
        //    DataTable dt = new DataTable();
        //    string Sql = string.Empty;

        //    using (DBStuff dbs = new DBStuff())
        //    {
        //        dbs.ConnectionString = ConnectionString;

        //        try
        //        {
        //            dbs.ConnectionTimeOut = 0;
        //            dbs.CommandTimeOut = 0;

        //            Sql =   "declare @ServerID int " +
        //                    "declare @StatusID int " +
        //                    "declare @ProjectID int " +
        //                    "declare @ServerAliasID int " +
        //                    "declare @UserID int " +
        //                    "declare @BackupSetID int " +
        //                    "declare @HistoryID int " +
        //                    "declare @ObjectVersion int " +
        //                    "declare @ObjectText varchar(max) " +
        //                    "set @ObjectText = '" + ObjectText + "' " +
        //                    "select @UserID = UserID " +
        //                    "from SVC_SystemUser " +
        //                    "where UserName = '" + UserName + "' " +
        //                    "select @ServerAliasID = ServerAliasID " +
        //                    "from SVC_LU_ServerAlias " +
        //                    "where ServerAliasDesc = '" + ServerName + "' " +
        //                    "select @ServerID = ServerID " +
        //                    "from [SVC_Server] " +
        //                    "where ServerAliasID = @ServerAliasID " +
        //                    "and UserID = @UserID " +
        //                    "select @ObjectVersion = isnull(ObjectVersion, 0) " +
        //                    "from SVC_ObjectHistory " +
        //                    "where ObjectName = '" + ObjectName + "' " +
        //                    "and DatabaseName = '" + DatabaseName + "' " +
        //                    "and HistoryDate = (select max(HistoryDate) from SVC_ObjectHistory where ObjectName = '" + ObjectName + "' and DatabaseName = '" + DatabaseName + "') " +
        //                    "if @ObjectVersion is null " +
        //                    "begin " +
        //                    "set @ObjectVersion = 0 " +
        //                    "end " +
        //                    "if '" + ObjectStatus + "' = 'Released to Production' " +
        //                    "begin " +
        //                    "set @ObjectVersion = @ObjectVersion + 1 " +
        //                    "end " +
        //                    "if '" + ObjectStatus + "' = 'Backup' " +
        //                    "begin " +
        //                    "select @StatusID = ObjectStatusID " +
        //                    "from SVC_ObjectHistory " +
        //                    "where ObjectName = '" + ObjectName + "' " +
        //                    "and DatabaseName = '" + DatabaseName + "' " +
        //                    "and HistoryDate = (select max(HistoryDate) from SVC_ObjectHistory where ObjectName = '" + ObjectName + "'  and DatabaseName = '" + DatabaseName + "' ) " +
        //                    "if @StatusID is null " +
        //                    "begin " +
        //                    "select @StatusID = ObjectStatusID  " +
        //                    "from SVC_LU_ObjectStatus  " +
        //                    "where ObjectStatusDesc = '" + ObjectStatus + "' " +
        //                    "end " +
        //                    "end " +
        //                    "else " +
        //                    "begin " +
        //                    "select @StatusID = ObjectStatusID  " +
        //                    "from SVC_LU_ObjectStatus  " +
        //                    "where ObjectStatusDesc = '" + ObjectStatus + "' " +
        //                    "end " +
        //                    "set @ProjectID = 0 " +
        //                    "select @ProjectID = isnull(ProjectID, 0) " +
        //                    "from SVC_Project " +
        //                     "where ProjectName = '" + ProjectName + "' " +
        //                    "set @BackupSetID = 0 " +
        //                    "if " + BackupSetName + " <> '' " +
        //                    "begin " +
        //                    "select @BackupSetID = BackupSetID " +
        //                    "from SVC_BackupSet " +
        //                    "where BackupSetName = '" + BackupSetName + "' " +
        //                    "end " +
        //                    "insert into SVC_ObjectHistory (ServerID, HistoryDate, ObjectStatusID, UserName, ObjectName, DatabaseName, ObjectText, Comment, ProjectID, ObjectVersion) " +
        //                    "select @ServerID, getdate(), @StatusID, '" + UserName + "', '" + ObjectName + "', '" + DatabaseName + "', @ObjectText, '" + Comment + "', @ProjectID, @ObjectVersion" +
        //                    "select @HistoryID =  ident_current('SVC_ObjectHistory') " +
        //                    "if @ProjectID > 0 " +
        //                    "begin " +
        //                    "if not exists(select UserID from SVC_ProjectObject where ProjectID = @ProjectID and DBObjectName = '" + ObjectName + "' and DBName = '" + DatabaseName + "' ) " +
        //                    "begin " +
        //                    "insert into SVC_ProjectObject (ProjectID, FileObjectID, DBName, DBObjectName, ObjectSequence, ObjectSelected, DateModified, UserID) " +
        //                    "select @ProjectID, " +
        //                    "0, " +
        //                    "'" + DatabaseName + "', " +
        //                    "'" + ObjectName + "', " +
        //                    "0, " +
        //                    "'N', " +
        //                    "getdate(), " +
        //                    "@UserID " + " " +
        //                    "end " +
        //                    "end " +
        //                    "if @BackupSetID > 0 " +
        //                    "begin " +
        //                    "if '" + DoDelete + "' = 'Y' " +
        //                    "begin " +
        //                    "delete from SVC_BackupSetObject " +
        //                    "where BackupSetID = @BackupSetID " +
        //                    "end " +
        //                    "insert into SVC_BackupSetObject (BackupSetID, HistoryID, CreateDate, ObjectType) " +
        //                    "select @BackupSetID, @HistoryID, getdate(), '" + ObjectType + "' " +
        //                    "end ";

        //            dt = dbs.DataTable(Sql);

        //            return true;
        //        }

        //        catch (Exception ex)
        //        {
        //            return false;
        //        }
        //    }
        //}

        public bool BackupObject(string ServerName, string ObjectStatus, string UserName, string ObjectName, string DatabaseName, string ObjectText, string Comment, string ProjectName, string BackupSetName, string ObjectType, string DoDelete, string VersionAtCheckOut)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ServerName, ObjectStatus, UserName, ObjectName, DatabaseName, ObjectText, Comment, ProjectName, BackupSetName, ObjectType, DoDelete, VersionAtCheckOut };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ObjectSaveHistory", Params);

                    return true;
                }

                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public bool BackupObjectBackupSet(string ServerName, string ObjectStatus, string UserName, string ObjectName, string DatabaseName, string ObjectText, string Comment, string ProjectName, string BackupSetName, string ObjectType, string DoDelete, string VersionAtCheckOut)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ServerName, ObjectStatus, UserName, ObjectName, DatabaseName, ObjectText, Comment, ProjectName, BackupSetName, ObjectType, DoDelete, VersionAtCheckOut };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ObjectSaveObjectBackup", Params);

                    return true;
                }

                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public string GetVersionAtCheckOut(string ServerName, string ObjectName, string DatabaseName)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ServerName, ObjectName, DatabaseName };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ObjectGetVersionAtCheckOut", Params);

                    if (ds.Tables.Count > 0)
                    {
                        return ds.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        return "";
                    }
                }

                catch (Exception ex)
                {
                    return "";
                }
            }
        }

        public DataTable GetSystemDatabases(string ConnString)
        {
            DataTable dt = new DataTable();

            string Sql = string.Empty;

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnString;

                Sql = "select Name from Sys.Databases order by Name";

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    dt = dbs.DataTable(Sql);

                    return dt;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetNetworkSystemUsers()
        {
            DataTable dt = new DataTable();

            string Sql = string.Empty;

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                Sql = "select UserID, UserName, QTNumber, UserPassword, Active, EmailAddress from SVC_SystemUser order by QTNumber";

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    dt = dbs.DataTable(Sql);

                    return dt;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetNetworkSystemUserByID(string UserNetworkName)
        {
            DataTable dt = new DataTable();

            string Sql = string.Empty;

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                Sql = "select SU.UserID, SU.UserName, SU.QTNumber, SU.UserPassword, SU.Active, SU.EmailAddress, R.RoleName " +
                      "from SVC_SystemRole as R inner join SVC_SystemUserRole as UR on R.RoleId = UR.RoleId right outer join SVC_SystemUser as SU on UR.UserID = SU.UserID " +
                      "where SU.QTNumber = '" + UserNetworkName + "'";

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    dt = dbs.DataTable(Sql);

                    return dt;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool SaveNetworkSystemUser(int UserID, string UserNetworkName, string UserName, string Email, string IsActive)
        {
            string Sql = string.Empty;

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                if (UserID == 0)
                {
                    Sql = "insert into SVC_SystemUser (QTNumber, UserName, UserPassword, Active, EmailAddress, CreateDate) select '" + UserNetworkName + "', '" 
                                                                                                                                     + UserName + "', '', '" 
                                                                                                                                     + IsActive + "', '" 
                                                                                                                                     + Email + "', getdate()";
                }
                else
                {
                    Sql = "update SVC_SystemUser set QTNumber = '" + UserNetworkName + "', " +
                                                     "UserName = '" + UserName + "', " +
                                                     "Active = '" + IsActive + "', " +
                                                     "EmailAddress = '" + Email + "' " +
                                                     "where UserID = " + UserID;
                }

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    dbs.SQLExecute(Sql);

                    return true;
                }

                catch 
                {
                    return false;
                }
            }
        }

        public bool DeleteNetworkSystemUser(int UserID)
        {
            string Sql = string.Empty;

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                if (UserID != 0)
                {
                    Sql = "delete from SVC_SystemUser where UserID = " + UserID;
                }
                else
                {
                    return false;
                }

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    dbs.SQLExecute(Sql);

                    return true;
                }

                catch
                {
                    return false;
                }
            }
        }

        public bool SaveRoleSetting(string Role, string RoleFunction, string AddRemove)
        {
            string Sql = string.Empty;

            try
            {
                using (DBStuff dbs = new DBStuff())
                {
                    dbs.ConnectionString = ConnectionString;

                    if (AddRemove == "Add")
                    {
                        Sql = "declare @RoleID int " + 
                              "declare @SystemFunctionID int " +
                              "select @RoleID = RoleID from SVC_SystemRole where RoleName = '" + Role + "' " +
                              "select @SystemFunctionID = SystemFunctionID from SVC_SystemFunction where FunctionName = '" + RoleFunction + "' " +
                              "insert into SVC_SystemRoleFuntion (RoleID, SystemFunctionId) select @RoleID, @SystemFunctionID";
                    }
                    else
                    {
                        Sql = "declare @RoleID int " +
                              "declare @SystemFunctionID int " +
                              "select @RoleID = RoleID from SVC_SystemRole where RoleName = '" + Role + "' " +
                              "select @SystemFunctionID = SystemFunctionID from SVC_SystemFunction where FunctionName = '" + RoleFunction + "' " +
                              "delete from SVC_SystemRoleFuntion where RoleID = @RoleID and SystemFunctionId = @SystemFunctionID";
                    }

                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    dbs.SQLExecute(Sql);
                }
            }

            catch
            {
                return false;
            }

            return true;
        }

        public bool SaveUserRole(string RoleName, string UserNetworkName)
        {
            string Sql = string.Empty;

            try
            {
                using (DBStuff dbs = new DBStuff())
                {
                    dbs.ConnectionString = ConnectionString;

                    Sql = "declare @RoleID int " +
                          "declare @UserID int " +
                          "select @RoleID = RoleID from SVC_SystemRole where RoleName = '" + RoleName + "' " +
                          "select @UserID = UserID from SVC_SystemUser where QTNumber = '" + UserNetworkName + "' " +
                          "delete from SVC_SystemUserRole where UserID = @UserID " +
                          "insert into SVC_SystemUserRole (RoleID, UserId) select @RoleID, @UserID";
                    
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    dbs.SQLExecute(Sql);
                }
            }

            catch
            {
                return false;
            }

            return true;
        }

        public DataTable GetRoleValues()
        {
            DataTable dt = new DataTable();

            string Sql = string.Empty;

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                Sql = "select R.RoleName, F.FunctionName from SVC_SystemRoleFuntion as RF inner join " +
                      "SVC_SystemRole as R ON RF.RoleID = R.RoleId inner join " +
                      "SVC_SystemFunction as F ON RF.SystemFunctionId = F.SystemFunctionId " +
                      "order by R.RoleName, F.FunctionName ";

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    dt = dbs.DataTable(Sql);

                    return dt;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        
        public DataTable ProjectGetRequestStatus(string ServerName, string ProjectName)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ServerName, ProjectName };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ProjectGetRequestStatus", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        

        public DataTable ProjectGetApproverEmails()
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ProjectGetApproverEmails", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable ProjectGetRequesterEmail(string ServerName, string ProjectName)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ServerName, ProjectName };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ProjectGetRequesterEmail", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable ProjectGetApprovalStatus(string ServerName, string ProjectName)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ServerName, ProjectName };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ProjectGetApprovalStatus", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable ProjectGetApprovalStatusRelease(string ServerName, string ProjectName)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ServerName, ProjectName };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ProjectGetApprovalStatusRelease", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void ProjectSaveApprovalStatus(string ServerGroup, string ProjectName, int UserID, string ApprovalRole)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ServerGroup, ProjectName, UserID, ApprovalRole };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ProjectSaveApprovalStatus", Params);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void ProjectSaveApprovalRequest(string ServerGroup, string ProjectName, int UserID)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ServerGroup, ProjectName, UserID };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ProjectSaveApprovalRequest", Params);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void ProjectUpdateChangeLog(string ServerName, string ProjectName, int UserID)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ServerName, ProjectName, UserID };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ProjectUpdateChangeLog", Params);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetRoles()
        {
            DataTable dt = new DataTable();

            string Sql = string.Empty;

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                Sql = "select RoleName from SVC_SystemRole order by RoleName";

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    dt = dbs.DataTable(Sql);

                    return dt;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public int NetworkSystemUserReferenced(int UserID)
        {
            string Sql = string.Empty;
            DataTable dt = new DataTable();
            int RecCount = 0;

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                if (UserID != 0)
                {
                    Sql = "select count(*) from SVC_Project where UserID = " + UserID;
                }
                else
                {
                    return 0;
                }

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    dt = dbs.DataTable(Sql);

                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            RecCount = Convert.ToInt32(dt.Rows[0][0].ToString());

                            if (RecCount > 0)
                            {
                                return 1;
                            }
                        }
                    }

                    Sql = "select count(*) from SVC_Server where UserID = " + UserID;

                    dt = dbs.DataTable(Sql);

                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            RecCount = Convert.ToInt32(dt.Rows[0][0].ToString());

                            if (RecCount > 0)
                            {
                                return 2;
                            }
                        }
                    }

                    Sql = "select count(*) from SVC_BackupSet where UserID = " + UserID;

                    dt = dbs.DataTable(Sql);

                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            RecCount = Convert.ToInt32(dt.Rows[0][0].ToString());

                            if (RecCount > 0)
                            {
                                return 3;
                            }
                        }
                    }

                    Sql = "select count(*) from SVC_ProjectObject where UserID = " + UserID;

                    dt = dbs.DataTable(Sql);

                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            RecCount = Convert.ToInt32(dt.Rows[0][0].ToString());

                            if (RecCount > 0)
                            {
                                return 4;
                            }
                        }
                    }

                    Sql = "select count(*) from SVC_ProjectObjectHistory where UserID = " + UserID;

                    dt = dbs.DataTable(Sql);

                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            RecCount = Convert.ToInt32(dt.Rows[0][0].ToString());

                            if (RecCount > 0)
                            {
                                return 5;
                            }
                        }
                    }
                    return RecCount;
                }

                catch
                {
                    return 1;
                }
            }
        }

        public DataTable GetSystemUserDatabases(string ConnString)
        {
            DataTable dt = new DataTable();

            string Sql = string.Empty;

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnString;

                Sql = "select Name, database_id from Sys.Databases order by Name";

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    dt = dbs.DataTable(Sql);

                    return dt;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        
        /*
         *  FN = Scalar function
            P = Stored procedure
            TF = Table function
            TR = SQL DML Trigger
            V = View
            IF = In-lined table-function
            D = Default or DEFAULT constraint
         
         SELECT     
                DB_NAME() AS DataBaseName,                  
                dbo.SysObjects.Name AS TriggerName,
                dbo.sysComments.Text AS SqlContent,
	            dbo.SysObjects.xType
            FROM 
                dbo.SysObjects INNER JOIN 
                    dbo.sysComments ON 
                    dbo.SysObjects.ID = dbo.sysComments.ID
         */

        public DataTable GetDatabaseObjects(string ConnString, string DatabaseName, string ObjectTypes)
        {
            DataTable dt = new DataTable();
            string IncludeObjects = string.Empty;

            if (ObjectTypes.Trim() == "00000") //None selected
            {
                return null;
            }

            if (ObjectTypes.Substring(0, 1) == "1") //Stored procedures
            {
                IncludeObjects = "'P'";
            }

            if (ObjectTypes.Substring(1, 1) == "1") //Functions
            {
                if (IncludeObjects.Trim() == "")
                {
                    IncludeObjects = "'FN', 'TF'";
                }
                else
                {
                    IncludeObjects = IncludeObjects + ", 'FN', 'TF'";
                }
            }

            if (ObjectTypes.Substring(2, 1) == "1") //Tables
            {
                if (IncludeObjects.Trim() == "")
                {
                    IncludeObjects = "'U'";
                }
                else
                {
                    IncludeObjects = IncludeObjects + ", 'U'";
                }
            }

            if (ObjectTypes.Substring(3, 1) == "1") //Triggers
            {
                if (IncludeObjects.Trim() == "")
                {
                    IncludeObjects = "'TR'";
                }
                else
                {
                    IncludeObjects = IncludeObjects + ", 'TR'";
                }
            }

            if (ObjectTypes.Substring(4, 1) == "1") //Views
            {
                if (IncludeObjects.Trim() == "")
                {
                    IncludeObjects = "'V'";
                }
                else
                {
                    IncludeObjects = IncludeObjects + ", 'V'";
                }
            }

            string Sql = string.Empty;

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnString;



                //Sql = "select Name from " + DatabaseName + ".sys.procedures order by Name";

                Sql = "select Name from " + DatabaseName + ".dbo.SysObjects where xType in (" + IncludeObjects + ") order by Name";

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    dt = dbs.DataTable(Sql);

                    return dt;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetDatabaseObjectsForBackup(string ConnString, string DatabaseName, string ObjectTypes, string DateFrom)
        {
            DataTable dt = new DataTable();
            string IncludeObjects = string.Empty;

            if (ObjectTypes.Trim() == "00000") //None selected
            {
                return null;
            }

            if (ObjectTypes.Substring(0, 1) == "1") //Stored procedures
            {
                IncludeObjects = "'P'";
            }

            if (ObjectTypes.Substring(1, 1) == "1") //Functions
            {
                if (IncludeObjects.Trim() == "")
                {
                    IncludeObjects = "'FN', 'TF'";
                }
                else
                {
                    IncludeObjects = IncludeObjects + ", 'FN', 'TF'";
                }
            }

            if (ObjectTypes.Substring(2, 1) == "1") //Tables
            {
                if (IncludeObjects.Trim() == "")
                {
                    IncludeObjects = "'U'";
                }
                else
                {
                    IncludeObjects = IncludeObjects + ", 'U'";
                }
            }

            if (ObjectTypes.Substring(3, 1) == "1") //Triggers
            {
                if (IncludeObjects.Trim() == "")
                {
                    IncludeObjects = "'TR'";
                }
                else
                {
                    IncludeObjects = IncludeObjects + ", 'TR'";
                }
            }

            if (ObjectTypes.Substring(4, 1) == "1") //Views
            {
                if (IncludeObjects.Trim() == "")
                {
                    IncludeObjects = "'V'";
                }
                else
                {
                    IncludeObjects = IncludeObjects + ", 'V'";
                }
            }

            string Sql = string.Empty;

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnString;

                Sql = "select Name, modify_date, type_desc from " + DatabaseName + ".sys.objects where name in (select Name from " + DatabaseName + ".dbo.SysObjects where xType in (" + IncludeObjects + ")) "
                    + " and modify_date >= '" + DateFrom + "'"
                    + " order by Name";

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    dt = dbs.DataTable(Sql);

                    return dt;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetDatabaseObjectsFiltered(string ConnString, string DatabaseName, string Filter, string ObjectTypes, bool POPFilter)
        {
            DataTable dt = new DataTable();

            string Sql = string.Empty;

            string IncludeObjects = string.Empty;

            if (ObjectTypes.Trim() == "00000") //None selected
            {
                return null;
            }

            if (ObjectTypes.Substring(0, 1) == "1") //Stored procedures
            {
                IncludeObjects = "'P'";
            }

            if (ObjectTypes.Substring(1, 1) == "1") //Functions
            {
                if (IncludeObjects.Trim() == "")
                {
                    IncludeObjects = "'FN', 'TF'";
                }
                else
                {
                    IncludeObjects = IncludeObjects + ", 'FN', 'TF'";
                }
            }

            if (ObjectTypes.Substring(2, 1) == "1") //Tables
            {
                if (IncludeObjects.Trim() == "")
                {
                    IncludeObjects = "'U'";
                }
                else
                {
                    IncludeObjects = IncludeObjects + ", 'U'";
                }
            }

            if (ObjectTypes.Substring(3, 1) == "1") //Triggers
            {
                if (IncludeObjects.Trim() == "")
                {
                    IncludeObjects = "'TR'";
                }
                else
                {
                    IncludeObjects = IncludeObjects + ", 'TR'";
                }
            }

            if (ObjectTypes.Substring(4, 1) == "1") //Views
            {
                if (IncludeObjects.Trim() == "")
                {
                    IncludeObjects = "'V'";
                }
                else
                {
                    IncludeObjects = IncludeObjects + ", 'V'";
                }
            }

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnString;

                if (!POPFilter)
                {
                    Sql = "select Name from " + DatabaseName + ".dbo.SysObjects where Name like '%" + Filter.Trim() + "%' and xType in (" + IncludeObjects + ") order by Name";
                }
                else
                {
                    Sql = "select Name from " + DatabaseName + ".dbo.SysObjects where Name in (" + Filter.Trim() + ") order by Name";
                }

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    dt = dbs.DataTable(Sql);

                    return dt;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataSet Search(string ConnString, string SearchText, string ObjectTypesSelected, string ExactMatch)
        {
            string Sql = string.Empty;
            DataSet ds = new DataSet();
            List<String> ObjectElements;

            string Procedures = string.Empty;
            string Functions = string.Empty;
            string Tables = string.Empty;
            string Views = string.Empty;
            string Constraints = string.Empty;
            string Triggers = string.Empty;
            string IncludeFilter = string.Empty;
            bool IncludeProgrammability = false;
            bool IncludeTables = false;

            Procedures = "'CLR_STORED_PROCEDURE', 'EXTENDED_STORED_PROCEDURE', 'SQL_STORED_PROCEDURE'";
            Functions = "'SQL_TABLE_VALUED_FUNCTION', 'AGGREGATE_FUNCTION', 'SQL_INLINE_TABLE_VALUED_FUNCTION', 'CLR_SCALAR_FUNCTION', 'SQL_SCALAR_FUNCTION'";
            Tables = "'USER_TABLE', 'SYSTEM_TABLE', 'INTERNAL_TABLE', 'TYPE_TABLE'";
            Views = "'VIEW'";
            Constraints = "'DEFAULT_CONSTRAINT', 'FOREIGN_KEY_CONSTRAINT', 'PRIMARY_KEY_CONSTRAINT', 'UNIQUE_CONSTRAINT'";
            Triggers = "'SQL_TRIGGER'";

            //Triggers,Constraints,Functions,Views,Tables,Stored Procedures

            ObjectElements = ObjectTypesSelected.Split(',').ToList();

            for (int i = 0; i < ObjectElements.Count; i++)
            {
                switch (ObjectElements[i])
                {
                    case "Stored Procedures":
                        IncludeProgrammability = true;

                        if (IncludeFilter.Trim() == "")
                        {
                            IncludeFilter = "(" + Procedures;
                        }
                        else
                        {
                            IncludeFilter = IncludeFilter + ", " + Procedures;
                        }

                        break;

                    case "Tables":
                        IncludeTables = true;

                        if (IncludeFilter.Trim() == "")
                        {
                            IncludeFilter = "(" + Tables;
                        }
                        else
                        {
                            IncludeFilter = IncludeFilter + ", " + Tables;
                        }

                        break;

                    case "Functions":
                        IncludeProgrammability = true;

                        if (IncludeFilter.Trim() == "")
                        {
                            IncludeFilter = "(" + Functions;
                        }
                        else
                        {
                            IncludeFilter = IncludeFilter + ", " + Functions;
                        }

                        break;

                    case "Views":
                        IncludeTables = true;

                        if (IncludeFilter.Trim() == "")
                        {
                            IncludeFilter = "(" + Views;
                        }
                        else
                        {
                            IncludeFilter = IncludeFilter + ", " + Views;
                        }

                        break;

                    case "Triggers":
                        IncludeProgrammability = true;

                        if (IncludeFilter.Trim() == "")
                        {
                            IncludeFilter = "(" + Triggers;
                        }
                        else
                        {
                            IncludeFilter = IncludeFilter + ", " + Triggers;
                        }

                        break;

                    case "Constraints":
                        if (IncludeFilter.Trim() == "")
                        {
                            IncludeFilter = "(" + Constraints;
                        }
                        else
                        {
                            IncludeFilter = IncludeFilter + ", " + Constraints;
                        }

                        break;

                    default:
                        break;
                }
            }

            if (IncludeFilter.Trim() != "")
            {
                IncludeFilter = " and AO.type_desc in " + IncludeFilter;
                IncludeFilter = IncludeFilter + ")";
            }

            if (ExactMatch == "Y")
            {
                SearchText = " = '" + SearchText + "' ";
            }
            else
            {
                SearchText = " like '%" + SearchText + "%' ";
            }

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnString;

                Sql = "create table #AllObjects (DatabaseName nvarchar(128), ObjectName nvarchar(128), ObjectType nvarchar(60), ObjectID int, SchemaName nvarchar(128)) " +
                      "create table #AllColumns (DatabaseName nvarchar(128), TableName nvarchar(128),  ColumnName nvarchar(128), DataType nvarchar(128), ColumnLength int, ColumnNo int, TableObjectId int) " +
                      "create table #CommentText(DatabaseName nvarchar(128), ObjectName nvarchar(128), ObjectId int, LineId int, ObjectText nvarchar(4000)) " +
                      "create table #TmpObjects( ObjectId int) " +
                      "" +
                      "declare @ObjectName nvarchar(776) = NULL " +
                      "declare @TmpObjectName nvarchar(128) " +
                      "declare @dbname sysname " +
                      "declare @objid int " +
                      "declare @BlankSpaceAdded int " +
                      "declare @BasePos int " +
                      "declare @CurrentPos int " +
                      "declare @TextLength int " +
                      "declare @LineId int " +
                      "declare @AddOnLen int " +
                      "declare @LFCR int " +
                      "declare @DefinedLength int " +
                      "declare @SyscomText nvarchar(4000) " +
                      "declare @Line nvarchar(255) " +
                      "declare @DetailId int " +
                      "declare @Text nvarchar(255) " +
                      "select @DefinedLength = 255 " +
                      "select @BlankSpaceAdded = 0 " +
                      "select @LFCR = 2 " +
                      "select @LineId = 1 " +
                      "" +
                      "insert into #AllObjects (DatabaseName, ObjectName, ObjectType, ObjectID, SchemaName) " +
                      "select db_name(),  AO.name, AO.type_desc, AO.object_id, S.name " +
                      "from sys.all_objects AO inner join sys.schemas S on AO.schema_id = S.schema_id " +
                      "where AO.name " + SearchText + " " + IncludeFilter;

                if (IncludeTables)
                {
                    Sql = Sql + " insert into #AllColumns (DatabaseName, TableName, ColumnName, DataType, ColumnLength, ColumnNo, TableObjectId) " +
                                  "select distinct db_name(), AO.name, AC.name, type_name(AC.user_type_id), convert(int, AC.max_length), AC.column_id, AO.object_id " +
                                  "from sys.all_columns AC inner join sys.all_objects AO on AC.object_id = AO.object_id " +
                                  "where AC.name " + SearchText + " ";

                    Sql = Sql + " insert into #AllObjects (DatabaseName, ObjectName, ObjectType, ObjectID, SchemaName) " +
                                  "select db_name(), AO.name, AO.type_desc, AO.object_id, S.name " +
                                  "from sys.all_objects AO inner join sys.schemas S on AO.schema_id = S.schema_id " +
                                  "where AO.object_id in (select distinct TableObjectId from #AllColumns where TableObjectId not in (select ObjectID from #AllObjects)) ";

                    Sql = Sql + " insert into #AllColumns (DatabaseName, TableName, ColumnName, DataType, ColumnLength, ColumnNo, TableObjectId) " +
                                  "select distinct db_name(), AO.name, AC.name, type_name(AC.user_type_id), convert(int, AC.max_length), AC.column_id, AO.object_id " +
                                  "from sys.all_columns AC inner join sys.all_objects AO on AC.object_id = AO.object_id " +
                                  "where AO.name in (select distinct ObjectName from #AllObjects)";
                }

                if (IncludeProgrammability)
                {
                    Sql = Sql + " insert into #TmpObjects(ObjectId) select id from syscomments where text " + SearchText + " ";

                    Sql = Sql + "insert into #AllObjects (DatabaseName, ObjectName, ObjectType, ObjectID, SchemaName) " +
                                "select db_name(), AO.name, AO.type_desc, AO.object_id, S.name " + 
                                "from sys.all_objects AO inner join sys.schemas S on AO.schema_id = S.schema_id " +
                                "where AO.object_id in (select ObjectId from #TmpObjects) ";

                    Sql = Sql + " declare ms_crs_syscom  cursor local for select id, text from syscomments where id in (select distinct ObjectId from #TmpObjects) and encrypted = 0 " +
                                  "order by id, number, colid for read only " +
                                  "open ms_crs_syscom fetch next from ms_crs_syscom into @DetailId, @SyscomText " +
                                  "while @@fetch_status >= 0 begin set  @BasePos = 1 set  @CurrentPos = 1 set  @TextLength = len(@SyscomText) " +
                                  "select @TmpObjectName = name from sys.all_objects where object_id = @DetailId " +
                                  "while @CurrentPos  != 0 begin select @CurrentPos = charindex(char(13) + char(10), @SyscomText, @BasePos) " +
                                  "if @CurrentPos != 0 begin while (isnull(LEN(@Line),0) + @BlankSpaceAdded + @CurrentPos-@BasePos + @LFCR) > @DefinedLength " +
                                  "begin select @AddOnLen = @DefinedLength-(isnull(LEN(@Line),0) + @BlankSpaceAdded) " +
                                  "set @Text = isnull(@Line, N'') + isnull(SUBSTRING(@SyscomText, @BasePos, @AddOnLen), N'') " +
                                  "insert into #CommentText (DatabaseName, ObjectName, ObjectId, LineId, ObjectText) " +
                                  "select db_name(), @TmpObjectName, @DetailId, @LineId, @Text " +
                                  "select @Line = null, @LineId = @LineId + 1, @BasePos = @BasePos + @AddOnLen, @BlankSpaceAdded = 0 end " +
                                  "set @Line = isnull(@Line, N'') + isnull(substring(@SyscomText, @BasePos, @CurrentPos-@BasePos + @LFCR), N'') " +
                                  "select @BasePos = @CurrentPos + 2 " +
                                  "insert into #CommentText (DatabaseName, ObjectName, ObjectId, LineId, ObjectText) " +
                                  "select db_name(), @TmpObjectName, @DetailId, @LineId, @Line " +
                                  "set @LineId = @LineId + 1 set @Line = NULL end else begin if @BasePos <= @TextLength begin " +
                                  "while (isnull(len(@Line),0) + @BlankSpaceAdded + @TextLength - @BasePos + 1 ) > @DefinedLength " +
                                  "begin set @AddOnLen = @DefinedLength - (isnull(len(@Line),0) + @BlankSpaceAdded) " +
                                  "set @Text = isnull(@Line, N'') + isnull(substring(@SyscomText, @BasePos, @AddOnLen), N'') " +
                                  "insert into #CommentText (DatabaseName, ObjectName, ObjectId, LineId, ObjectText) " +
                                  "select db_name(), @TmpObjectName, @DetailId, @LineId, @Text select @Line = null, @LineId = @LineId + 1, @BasePos = @BasePos + @AddOnLen, @BlankSpaceAdded = 0 end " +
                                  "select @Line = isnull(@Line, N'') + isnull(substring(@SyscomText, @BasePos, @TextLength - @BasePos + 1 ), N'') " +
                                  "if len(@Line) < @DefinedLength and charindex(' ', @SyscomText, @TextLength+1 ) > 0 " +
                                  "begin select @Line = @Line + ' ', @BlankSpaceAdded = 1 end if @Line is not null begin " +
                                  "insert into #CommentText (DatabaseName, ObjectName, ObjectId, LineId, ObjectText) " +
                                  "select db_name(), @TmpObjectName,  @DetailId, @LineId, @Line end set @Line = null end end end " +
                                  "fetch next from ms_crs_syscom into @DetailId, @SyscomText end close  ms_crs_syscom deallocate ms_crs_syscom ";
                }

                Sql = Sql + " select * from #AllObjects order by ObjectName select distinct * from #AllColumns order by TableName, ColumnNo " +
                            "select DatabaseName, ObjectName, ObjectId, ObjectText from #CommentText order by ObjectName, LineId " +
                            "drop table #CommentText drop table #TmpObjects drop table #AllObjects drop table #AllColumns";

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.DataSet(Sql);

                    return ds;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetDatabaseObjectText(string ConnString, string DatabaseName, string ObjectName)
        {
            DataTable dt = new DataTable();

            string Sql = string.Empty;

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnString;

                Sql = "select ROUTINE_NAME, ROUTINE_TYPE, OBJECT_DEFINITION(object_id(ROUTINE_NAME)) as FullDefinition " +
                      "from " + DatabaseName.Trim() + ".INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = '" + ObjectName.Trim() + "'";


                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    dt = dbs.DataTable(Sql);

                    return dt;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetObjectHelpText(string ConnString, string DatabaseName, string ObjectName)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnString + ";Initial Catalog=" + DatabaseName;

                object[] Params = { ObjectName, null };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("sp_helptext", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable SaveProjectBackupSet(string ProjectName)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { ProjectName };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_SaveProjectBackupSet", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DoProjectBackup(int BackupSetID, int ProjectID, string ObjectName, string DatabaseName, string ObjectText)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { BackupSetID, ProjectID, ObjectName, DatabaseName, ObjectText };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_ObjectDoProjectBackup", Params);

                    return true;
                }

                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public DataTable GetProjectBackupSets(string Active)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { Active };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_GetProjectBackupSets", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetProjectBackupSetDetail(int SetID)
        {
            DataSet ds = new DataSet();

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                object[] Params = { SetID };

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    ds = dbs.ExecuteDataset("SVC_GetProjectBackupSetDetail", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetObjectDetail(string ConnString, string DatabaseName, string DBOwner, string ObjectName)
        {
            DataTable dt = new DataTable();
            string Sql = string.Empty;

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnString;

                Sql = "select Name, create_date, modify_date from " + DatabaseName + ".sys.objects where name = " + "'" + ObjectName + "'";

                //SELECT name, [modify_date], * FROM sys.tables

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    dt = dbs.DataTable(Sql);

                    return dt;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetObjectType(string ConnString, string DatabaseName, string ObjectName)
        {
            DataTable dt = new DataTable();
            string Sql = string.Empty;

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnString;

                Sql = "select Type from " + DatabaseName + ".sys.objects where name = " + "'" + ObjectName + "'";

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    dt = dbs.DataTable(Sql);

                    return dt;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetTableHelpText(string ConnString, string DatabaseName, string ObjectName)
        {
            DataTable dt = new DataTable();
            string Sql = string.Empty;

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnString + ";Initial Catalog=" + DatabaseName;

                Sql = TableDef(ObjectName);

                //SELECT name, [modify_date], * FROM sys.tables

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    //string retval = dbs.SqlCommandS("SET ARITHABORT ON");

                    dt = dbs.DataTable(Sql);

                    return dt;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private string TableDef(string TabName)
        {
            return "SET ARITHABORT ON " +
            "DECLARE @table_name SYSNAME " +
            "SELECT @table_name = 'dbo." + TabName + "' " +
            " " +
            "DECLARE  " +
                  "@object_name SYSNAME " +
                ", @object_id INT " +
            " " +
            "SELECT  " +
                  "@object_name = '[' + s.name + '].[' + o.name + ']' " +
                ", @object_id = o.[object_id] " +
            "FROM sys.objects o WITH (NOWAIT) " +
            "JOIN sys.schemas s WITH (NOWAIT) ON o.[schema_id] = s.[schema_id] " +
            "WHERE s.name + '.' + o.name = @table_name " +
                "AND o.[type] = 'U' " +
            "    AND o.is_ms_shipped = 0 " +
            " " +
            "DECLARE @SQL NVARCHAR(MAX) = '' " +
            " " +
            ";WITH index_column AS  " +
            "( " +
                "SELECT  " +
                      "ic.[object_id] " +
                    ", ic.index_id " +
            "        , ic.is_descending_key " +
                    ", ic.is_included_column " +
            "        , c.name " +
                "FROM sys.index_columns ic WITH (NOWAIT) " +
            "    JOIN sys.columns c WITH (NOWAIT) ON ic.[object_id] = c.[object_id] AND ic.column_id = c.column_id " +
                "WHERE ic.[object_id] = @object_id " +
            "), " +
            "fk_columns AS  " +
            "( " +
                 "SELECT  " +
                      "k.constraint_object_id " +
                    ", cname = c.name " +
            "        , rcname = rc.name " +
                "FROM sys.foreign_key_columns k WITH (NOWAIT) " +
            "    JOIN sys.columns rc WITH (NOWAIT) ON rc.[object_id] = k.referenced_object_id AND rc.column_id = k.referenced_column_id  " +
                "JOIN sys.columns c WITH (NOWAIT) ON c.[object_id] = k.parent_object_id AND c.column_id = k.parent_column_id " +
            "    WHERE k.parent_object_id = @object_id " +
            ") " +
            "SELECT @SQL = 'CREATE TABLE ' + @object_name + '~' + '(' + '~' + STUFF(( " +
                "SELECT CHAR(9) + ', [' + c.name + '] ' +  " +
                    "CASE WHEN c.is_computed = 1 " +
                        "THEN 'AS ' + cc.[definition]  " +
            "            ELSE UPPER(tp.name) +  " +
                            "CASE WHEN tp.name IN ('varchar', 'char', 'varbinary', 'binary', 'text') " +
                                   "THEN '(' + CASE WHEN c.max_length = -1 THEN 'MAX' ELSE CAST(c.max_length AS VARCHAR(5)) END + ')' " +
                                 "WHEN tp.name IN ('nvarchar', 'nchar', 'ntext') " +
                                   "THEN '(' + CASE WHEN c.max_length = -1 THEN 'MAX' ELSE CAST(c.max_length / 2 AS VARCHAR(5)) END + ')' " +
                                 "WHEN tp.name IN ('datetime2', 'time2', 'datetimeoffset')  " +
                                   "THEN '(' + CAST(c.scale AS VARCHAR(5)) + ')' " +
                                 "WHEN tp.name = 'decimal'  " +
                                   "THEN '(' + CAST(c.[precision] AS VARCHAR(5)) + ',' + CAST(c.scale AS VARCHAR(5)) + ')' " +
                                "ELSE '' " +
                            "END + " +
            "                CASE WHEN c.collation_name IS NOT NULL THEN ' COLLATE ' + c.collation_name ELSE '' END + " +
                            "CASE WHEN c.is_nullable = 1 THEN ' NULL' ELSE ' NOT NULL' END + " +
            "                CASE WHEN dc.[definition] IS NOT NULL THEN ' DEFAULT' + dc.[definition] ELSE '' END +  " +
                            "CASE WHEN ic.is_identity = 1 THEN ' IDENTITY(' + CAST(ISNULL(ic.seed_value, '0') AS CHAR(1)) + ',' + CAST(ISNULL(ic.increment_value, '1') AS CHAR(1)) + ')' ELSE '' END  " +
                    "END + '~' " +
                "FROM sys.columns c WITH (NOWAIT) " +
            "    JOIN sys.types tp WITH (NOWAIT) ON c.user_type_id = tp.user_type_id " +
                "LEFT JOIN sys.computed_columns cc WITH (NOWAIT) ON c.[object_id] = cc.[object_id] AND c.column_id = cc.column_id " +
            "    LEFT JOIN sys.default_constraints dc WITH (NOWAIT) ON c.default_object_id != 0 AND c.[object_id] = dc.parent_object_id AND c.column_id = dc.parent_column_id " +
                "LEFT JOIN sys.identity_columns ic WITH (NOWAIT) ON c.is_identity = 1 AND c.[object_id] = ic.[object_id] AND c.column_id = ic.column_id " +
            "    WHERE c.[object_id] = @object_id " +
                "ORDER BY c.column_id " +
            "    FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, CHAR(9) + ' ') " +
                "+ ISNULL((SELECT CHAR(9) + ', CONSTRAINT [' + k.name + '] PRIMARY KEY (' +  " +
                                "(SELECT STUFF(( " +
                                     "SELECT ', [' + c.name + '] ' + CASE WHEN ic.is_descending_key = 1 THEN 'DESC' ELSE 'ASC' END " +
            "                         FROM sys.index_columns ic WITH (NOWAIT) " +
                                     "JOIN sys.columns c WITH (NOWAIT) ON c.[object_id] = ic.[object_id] AND c.column_id = ic.column_id " +
            "                         WHERE ic.is_included_column = 0 " +
                                         "AND ic.[object_id] = k.parent_object_id  " +
            "                             AND ic.index_id = k.unique_index_id      " +
                                     "FOR XML PATH(N''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '')) " +
                        "+ ')' + '~' " +
            "            FROM sys.key_constraints k WITH (NOWAIT) " +
                        "WHERE k.parent_object_id = @object_id  " +
                            "AND k.[type] = 'PK'), '') + ')'  + '~' " +
                "+ ISNULL((SELECT ( " +
                    "SELECT '~' + " +
                         "'ALTER TABLE ' + @object_name + ' WITH'  " +
                        "+ CASE WHEN fk.is_not_trusted = 1  " +
                            "THEN ' NOCHECK'  " +
            "                ELSE ' CHECK'  " +
                          "END +  " +
            "              ' ADD CONSTRAINT [' + fk.name  + '] FOREIGN KEY('  " +
                          "+ STUFF(( " +
                            "SELECT ', [' + k.cname + ']' " +
            "                FROM fk_columns k " +
                            "WHERE k.constraint_object_id = fk.[object_id] " +
            "                FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') " +
                           "+ ')' + " +
                          "' REFERENCES [' + SCHEMA_NAME(ro.[schema_id]) + '].[' + ro.name + '] (' " +
            "              + STUFF(( " +
                            "SELECT ', [' + k.rcname + ']' " +
            "                FROM fk_columns k " +
                            "WHERE k.constraint_object_id = fk.[object_id] " +
            "                FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') " +
                           "+ ')' " +
                        "+ CASE  " +
                            "WHEN fk.delete_referential_action = 1 THEN ' ON DELETE CASCADE'  " +
            "                WHEN fk.delete_referential_action = 2 THEN ' ON DELETE SET NULL' " +
                            "WHEN fk.delete_referential_action = 3 THEN ' ON DELETE SET DEFAULT'  " +
            "                ELSE ''  " +
                          "END " +
                        "+ CASE  " +
                            "WHEN fk.update_referential_action = 1 THEN ' ON UPDATE CASCADE' " +
            "                WHEN fk.update_referential_action = 2 THEN ' ON UPDATE SET NULL' " +
                            "WHEN fk.update_referential_action = 3 THEN ' ON UPDATE SET DEFAULT'   " +
            "                ELSE ''  " +
                          "END  " +
                        "+ '~' + 'ALTER TABLE ' + @object_name + ' CHECK CONSTRAINT [' + fk.name  + ']' + '~' " +
                    "FROM sys.foreign_keys fk WITH (NOWAIT) " +
            "        JOIN sys.objects ro WITH (NOWAIT) ON ro.[object_id] = fk.referenced_object_id " +
                    "WHERE fk.parent_object_id = @object_id " +
            "        FOR XML PATH(N''), TYPE).value('.', 'NVARCHAR(MAX)')), '') " +
                "+ ISNULL(((SELECT " +
                     "'~' + 'CREATE' + CASE WHEN i.is_unique = 1 THEN ' UNIQUE' ELSE '' END  " +
                            "+ ' NONCLUSTERED INDEX [' + i.name + '] ON ' + @object_name + ' (' + " +
            "                STUFF(( " +
                            "SELECT ', [' + c.name + ']' + CASE WHEN c.is_descending_key = 1 THEN ' DESC' ELSE ' ASC' END " +
            "                FROM index_column c " +
                            "WHERE c.is_included_column = 0 " +
                                "AND c.index_id = i.index_id " +
                            "FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') + ')'   " +
            "                + ISNULL('~' + 'INCLUDE (' +  " +
                                "STUFF(( " +
            "                    SELECT ', [' + c.name + ']' " +
                                "FROM index_column c " +
            "                    WHERE c.is_included_column = 1 " +
                                    "AND c.index_id = i.index_id " +
                                "FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') + ')', '')  + '~' " +
                    "FROM sys.indexes i WITH (NOWAIT) " +
            "        WHERE i.[object_id] = @object_id " +
                        "AND i.is_primary_key = 0 " +
            "            AND i.[type] = 2 " +
                    "FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') " +
                "), '') " +
            " " +
            "select @SQL ";
        }

        public Boolean CreateObject(string ConnString, string ObjectText)
        {
            DataTable dt = new DataTable();

            string Sql = string.Empty;

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnString;

                Sql = ObjectText;

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    return dbs.SqlCommand(ObjectText);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public string CreateObjectS(string ConnString, string ObjectText)
        {
            DataTable dt = new DataTable();

            string Sql = string.Empty;

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnString;

                Sql = ObjectText;

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    return dbs.SqlCommandS(ObjectText);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public string CreateObjectSMO(string ConnString, string ObjectText)
        {
            try
            {
                SqlConnection connection = new SqlConnection(ConnString);

                Server server = new Server(new ServerConnection(connection));

                server.ConnectionContext.ExecuteNonQuery(ObjectText);


                return "";
            }

            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return ex.InnerException.ToString();
                }
                else
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        return ex.InnerException.InnerException.ToString();
                    }
                    else
                    {
                        return "There was an error executing the script, please verify that the script runs in SSMS.";
                    }
                }
            }
        }

        #region Users

        public DataTable AllUsers()
        {
            using (DBStuff TU = new DBStuff())
            {
                TU.ConnectionString = ConnectionString;

                object[] Params = { "All" };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("SVC_Security_GetSystemUser", Params);

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
            using (DBStuff TU = new DBStuff())
            {
                TU.ConnectionString = ConnectionString;

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("SVC_Security_GetSystemRoles");

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

            using (DBStuff TU = new DBStuff())
            {
                TU.ConnectionString = ConnectionString;

                try
                {
                    DataSet ds = null;

                    object[] Params = { RoleName, FunctionName };

                    ds = TU.ExecuteDataset("SVC_Security_SystemRoleHasFunction", Params);

                    Found = false;

                    foreach (DataRow row in ds.Tables[0].Rows)
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
            using (DBStuff TU = new DBStuff())
            {
                TU.ConnectionString = ConnectionString;

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("SVC_Security_GetSystemFunctions");

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
            using (DBStuff TU = new DBStuff())
            {
                TU.ConnectionString = ConnectionString;

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("SVC_Security_AllSystemRolesByUsers");

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
            using (DBStuff TU = new DBStuff())
            {
                TU.ConnectionString = ConnectionString;

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("SVC_Security_RolesByRole");

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public string SaveRoleFunction(string RoleName, string FunctionName, string UserName)
        {
            using (DBStuff TU = new DBStuff())
            {
                TU.ConnectionString = ConnectionString;

                object[] Params = { RoleName, FunctionName, UserName };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("SVC_Security_InsertUpdateRoleFunction", Params);

                    return ds.Tables[0].Rows[0].ItemArray[0].ToString();
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //public string SaveUserRole(string RoleName, string UserName)
        //{
        //    using (DBStuff TU = new DBStuff())
        //    {
        //        TU.ConnectionString = ConnectionString;

        //        object[] Params = { UserName, RoleName, UserName };

        //        try
        //        {
        //            DataSet ds = null;

        //            ds = TU.ExecuteDataset("SVC_Security_InsertUpdateUserRole", Params);

        //            return ds.Tables[0].Rows[0].ItemArray[0].ToString();
        //        }

        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}

        public void GrantAllRoleFunction(string RoleName)
        {
            bool retval;

            using (DBStuff DC = new DBStuff())
            {
                DC.ConnectionString = ConnectionString;

                try
                {
                    if (!ExistingRole(RoleName))
                    {
                        //Create the role

                        if (DC.SqlCommand("insert into SVC_Role (Role) values('" + RoleName + "')"))
                        {
                            // Add rights to the role

                            retval = DC.SqlCommand("delete from SVC_SystemRole where Role = '" + RoleName + "'");
                            retval = DC.SqlCommand("insert into SVC_SystemRole (Role, ProgramName) select distinct '" + RoleName + "', FunctionName from SVC_SystemFunctions");
                        }
                    }
                    else
                    {
                        //Add rights to the role

                        retval = DC.SqlCommand("delete from SVC_SystemRole where Role = '" + RoleName + "'");
                        retval = DC.SqlCommand("insert into SVC_SystemRole (Role, ProgramName) select distinct '" + RoleName + "', FunctionName from SVC_SystemFunctions");
                    }
                }

                catch
                {
                }
            }
        }

        public int NewRole(string RoleName, string UserName)
        {
            using (DBStuff TU = new DBStuff())
            {
                TU.ConnectionString = ConnectionString;

                object[] Params = { RoleName, UserName };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("SVC_Security_InsertRole", Params);

                    return 0;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable SingleUser(string QTNumber, string CallingForm)
        {
            //using (DBStuff TU = new DBStuff())
            //{
            //    TU.ConnectionString = ConnectionString;

            //    object[] Params = { QTNumber, CallingForm };

            //    try
            //    {
            //        DataSet ds = null;

            //        ds = TU.ExecuteDataset("SVC_Security_GetSystemUser", Params);

            //        return ds.Tables[0];
            //    }

            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //}

            DataTable dt = new DataTable();

            string Sql = string.Empty;

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                Sql = "declare @QTNumber varchar(10) " +
                      "declare @CallingForm varchar(50) " +
                      "insert into SVC_UsageLog(QTNumber, CallingForm, Logdate) " +
                      "select '" + QTNumber + "', '" + CallingForm + "', getdate() " +
                      "select SU.UserID, SU.UserName, SU.QTNumber, SU.UserPassword, SU.Active, SU.EmailAddress, R.RoleName " +
                      "from SVC_SystemRole as R inner join SVC_SystemUserRole as UR on R.RoleId = UR.RoleId right outer join SVC_SystemUser as SU on UR.UserID = SU.UserID " +
                      "where SU.QTNumber = '" + QTNumber + "'";

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    dt = dbs.DataTable(Sql);

                    return dt;
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    throw ex;
                }
            }
        }

        public DataTable GetUserFunctions(int UserID)
        {
            DataTable dt = new DataTable();

            string Sql = string.Empty;

            using (DBStuff dbs = new DBStuff())
            {
                dbs.ConnectionString = ConnectionString;

                Sql = "select UR.UserID, R.RoleName, F.FunctionName " +
                      "from SVC_SystemRole as R inner join SVC_SystemRoleFuntion as RF on R.RoleId = RF.RoleID inner join " +
                      "SVC_SystemUserRole as UR on R.RoleId = UR.RoleId inner join SVC_SystemFunction as F on RF.SystemFunctionId = F.SystemFunctionId " +
                      "where UR.UserID = " + UserID + " order by F.FunctionName";

                try
                {
                    dbs.ConnectionTimeOut = 0;
                    dbs.CommandTimeOut = 0;

                    dt = dbs.DataTable(Sql);

                    return dt;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetProjectObjects(string ProjectName)
        {
            using (DBStuff TU = new DBStuff())
            {
                TU.ConnectionString = ConnectionString;

                object[] Params = { ProjectName };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("SVC_GetProjectObjects", Params);

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
            using (DBStuff TU = new DBStuff())
            {
                TU.ConnectionString = ConnectionString;

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("SVC_Security_InsertUpdateSystemUser", Params);

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
            using (DBStuff TU = new DBStuff())
            {
                TU.ConnectionString = ConnectionString;

                object[] Params = { UserName, UserName };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("SVC_Security_DeleteUser", Params);

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

            using (DBStuff TU = new DBStuff())
            {
                TU.ConnectionString = ConnectionString;

                DataTable dr;

                Found = false;

                try
                {
                    dr = TU.DataTable("SELECT * from SVC_SystemRole where Role = '" + RoleName + "' and ProgramName = '" + FunctionName + "'");

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

            using (DBStuff TU = new DBStuff())
            {
                TU.ConnectionString = ConnectionString;

                DataTable dr;

                Found = false;

                try
                {
                    dr = TU.DataTable("SELECT * from SVC_UserRole where Role = '" + RoleName + "' and UserName = '" + UserName + "'");

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

            using (DBStuff TU = new DBStuff())
            {
                TU.ConnectionString = ConnectionString;

                DataTable dr;

                Found = false;

                try
                {
                    dr = TU.DataTable("SELECT * from SVC_Role where Role = '" + RoleName + "'");

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

            using (DBStuff TU = new DBStuff())
            {
                TU.ConnectionString = ConnectionString;

                object[] Params = { Username };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("SVC_Security_GetSystemUser", Params);


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
            using (DBStuff TU = new DBStuff())
            {
                TU.ConnectionString = ConnectionString;

                object[] Params = { Username, SystemArea };

                try
                {
                    DataSet ds = null;

                    ds = TU.ExecuteDataset("SVC_Security_GetUserAccess", Params);

                    return ds.Tables[0];
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #endregion

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
