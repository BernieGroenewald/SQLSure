﻿using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using SentryDataStuff;
using SentryGeneral;
using SentryCheckOut;

namespace SentryControls
{
    public partial class DBObjectMulti : UserControl
    {
        private string ConnectionString = string.Empty;
        private string DBOwner = string.Empty;

        public string ServerAlias;
        public int ControlNumber = 0;
        public string DatabaseName;
        public string ObjectName;
        public string ModifiedDate;
        public string CreateDate;
        public string FilterString = string.Empty;
        public string PoPFilter = string.Empty;
        public string ExtObjectsSelected = "10000";
        public bool AvailableForEdit = true;

        string UserName = string.Empty;
        int UserID = 0;
        int ServerID = 0;
        int ServerAliasID = 0;
        string ObjectsSelected = "10000";
        string WinMergePath = string.Empty;
        string CompareAlias1 = string.Empty;
        string CompareAlias2 = string.Empty;
        string CompareAlias3 = string.Empty;
        string CompareAlias4 = string.Empty;
        string CompareAlias5 = string.Empty;

        public event EventHandler DBChanged;
        public event EventHandler ObjectChanged;
        public event EventHandler ModifiedDateChanged;
        public event EventHandler CreateDateChanged;
        public event EventHandler ObjectTypesChanged;
        public event EventHandler AvailableForEditChange;

        Color AllOkay = Color.Lime;
        Color Danger = Color.Yellow;
        Color Warn = Color.DeepSkyBlue;

        public DBObjectMulti()
        {
            InitializeComponent();
        }

        private void DBObjectMulti_Load(object sender, EventArgs e)
        {
            using (DataStuff sn = new DataStuff())
            {
                DataTable dt = sn.SingleUser(Environment.UserName, "DBObject");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        UserName = row["UserName"].ToString();
                        UserID = Convert.ToInt32(row["UserID"].ToString());
                    }
                }
            }

            tsbFilter.Enabled = false;
            tsbRemoveFilter.Enabled = false;
            tsbViewObject.Enabled = false;
            //tsbCheckInOut.Enabled = false;
            tsbHistory.Enabled = false;
            tsbCompare.Enabled = false;
        }

        public void GetObjectDetail()
        {
            if (GetServerDetail())
            {
                LoadDatabaseNames();

                lServer.Text = ServerAlias;
            }
        }

        private void LoadDatabaseNames()
        {
            try
            {
                cbDatabaseName.Items.Clear();

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetSystemDatabases(ConnectionString);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            cbDatabaseName.Items.Add(row["Name"].ToString());
                        }
                    }
                }
            }

            catch
            {
                throw;
            }
        }

        private void LoadDatabaseObjects()
        {
            try
            {
                tError.Text = "";

                cbObjects.Items.Clear();

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetDatabaseObjects(ConnectionString, cbDatabaseName.Text, ObjectsSelected);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            cbObjects.Items.Add(row["Name"].ToString());
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                tError.Text = ex.Message;
            }
        }

        private void LoadDatabaseFilteredObjects()
        {
            try
            {
                tError.Text = "";

                cbObjects.Items.Clear();

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetDatabaseObjectsFiltered(ConnectionString, cbDatabaseName.Text, FilterString, ObjectsSelected, false);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            cbObjects.Items.Add(row["Name"].ToString());
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                tError.Text = ex.Message;
            }
        }

        private void LoadDatabaseFilteredObjectsPOP()
        {
            string ObjectList = string.Empty;

            try
            {
                ObjectList = GetProjectFilter();

                if (ObjectList.Trim() == "")
                {
                    MessageBox.Show("No objects were found in the current database selected for this project.", "Filter Objects", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                tError.Text = "";

                cbObjects.Items.Clear();

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetDatabaseObjectsFiltered(ConnectionString, cbDatabaseName.Text, ObjectList, ObjectsSelected, true);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            cbObjects.Items.Add(row["Name"].ToString());
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                tError.Text = ex.Message;
            }
        }

        private void LoadObjectDetail()
        {
            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetObjectDetail(ConnectionString, DatabaseName, DBOwner, ObjectName);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            tDateCreated.Text = row["create_date"].ToString();
                            tDateModified.Text = row["modify_date"].ToString();
                        }

                        if (ModifiedDateChanged != null)
                        {
                            ModifiedDate = tDateModified.Text;
                            ModifiedDateChanged(this, null);
                        }

                        if (CreateDateChanged != null)
                        {
                            CreateDate = tDateCreated.Text;
                            CreateDateChanged(this, null);
                        }
                    }
                    else
                    {
                        tDateCreated.Text = "Not Found";
                        tDateModified.Text = "";
                    }
                }
            }

            catch
            {

            }
        }

        public void SetDatabaseName(string NewName)
        {
            cbDatabaseName.Text = NewName;
        }

        public void SetObjectName(string NewName)
        {
            cbObjects.Text = NewName;
        }

        public void SetAvailableForEdit(bool AllowEdit)
        {
            //AvailableForEdit = AllowEdit;

            //if (!AvailableForEdit)
            //{
            //    tsbCheckInOut.Enabled = false;
            //}
            //else
            //{
            //    tsbCheckInOut.Enabled = true;
            //}
        }

        public void SetObjectsSelected(string NewSelection)
        {
            if (NewSelection.Substring(0, 1) == "1") //Stored procedures
            {
                cObjStoredProcedures.Checked = true;
            }
            else
            {
                cObjStoredProcedures.Checked = false;
            }

            if (NewSelection.Substring(1, 1) == "1") //Functions
            {
                cObjFunctions.Checked = true;
            }
            else
            {
                cObjFunctions.Checked = false;
            }

            if (NewSelection.Substring(2, 1) == "1") //Tables
            {
                cObjTables.Checked = true;
            }
            else
            {
                cObjTables.Checked = false;
            }

            if (NewSelection.Substring(3, 1) == "1") //Triggers
            {
                cObjTriggers.Checked = true;
            }
            else
            {
                cObjTriggers.Checked = false;
            }

            if (NewSelection.Substring(4, 1) == "1") //Views
            {
                cObjViews.Checked = true;
            }
            else
            {
                cObjViews.Checked = false;
            }

            ExtObjectsSelected = NewSelection;
        }

        public void SetCompareServer(string Alias1, string Alias2, string Alias3, string Alias4, string Alias5)
        {
            CompareAlias1 = Alias1;
            CompareAlias2 = Alias2;
            CompareAlias3 = Alias3;
            CompareAlias4 = Alias4;
            CompareAlias5 = Alias5;

            if (CompareAlias2.Trim() != "")
            {
                if (cbDatabaseName.Text.Trim() != "")
                {
                    if (cbObjects.Text.Trim() != "")
                    {
                        tsbCompare.Enabled = true;
                    }
                }
            }
        }

        public void SetDateModifiedColour(Color NewColour)
        {
            tDateModified.BackColor = NewColour;
        }

        public void SetDateCreatedColour(Color NewColour)
        {
            tDateCreated.BackColor = NewColour;
        }

        private bool GetServerDetail()
        {
            try
            {
                string ServerName = string.Empty;
                string UserName = string.Empty;
                byte[] Password = null;
                string IntegratedSecurity = string.Empty;

                ConnectionString = "";

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetServerDetail(ServerAlias, UserID);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            ServerName = row["ServerName"].ToString();
                            UserName = row["UserName"].ToString();
                            Password = (byte[])row["Password"];
                            DBOwner = row["DBOwner"].ToString();
                            IntegratedSecurity = row["IntegratedSecurity"].ToString();
                            ServerID = Convert.ToInt32(row["ServerID"].ToString());
                            ServerAliasID = Convert.ToInt32(row["ServerAliasID"].ToString());
                        }
                    }
                }

                if (IntegratedSecurity == "Y")
                {
                    ConnectionString = "Data Source=" + ServerName + ";Integrated Security=True";
                }
                else
                {
                    string DecryptedPassword = string.Empty;

                    using (DataStuff ds = new DataStuff())
                    {
                        DecryptedPassword = ds.Ontsyfer(Password);
                    }

                    ConnectionString = "Data Source=" + ServerName + ";User ID=" + UserName + ";Password=" + DecryptedPassword;
                }

                if (ConnectionString.Trim() != "")
                {
                    return true;
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

        private void cbDatabaseName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatabaseName = cbDatabaseName.Text;
            LoadDatabaseObjects();
            tsbFilter.Enabled = true;

            if (DBChanged != null)
            {
                DBChanged(this, e);
            }
        }

        private void NotifyMainForm()
        {
        }

        private void cbObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObjectName = cbObjects.Text;
            LoadObjectDetail();
            tsbViewObject.Enabled = true;
            tsbHistory.Enabled = true;

            if (ObjectChanged != null)
            {
                ObjectChanged(this, e);
            }

            GetObjectStatus();

            DisableStatus();

            if (CompareAlias2.Trim() != "")
            {
                if (cbDatabaseName.Text.Trim() != "")
                {
                    if (cbObjects.Text.Trim() != "")
                    {
                        tsbCompare.Enabled = true;
                    }
                }
            }

            //if (!AvailableForEdit)
            //{
            //    tsbCheckInOut.Enabled = false;
            //}
            //else
            //{
            //    tsbCheckInOut.Enabled = true;
            //}

            if (AvailableForEditChange != null)
            {
                AvailableForEditChange(this, null);
            }
        }

        private void GetObjectStatus()
        {
            tStatus.Text = "";
            tUser.Text = "";
            tStatusDate.Text = "";
            tComment.Text = "";
            AvailableForEdit = true;

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetObjectHistoryLast(ObjectName, ServerAliasID);

                    if (dt.Rows.Count > 0)
                    {
                        tStatus.Text = dt.Rows[0]["Status"].ToString();
                        tUser.Text = dt.Rows[0]["User"].ToString();
                        tStatusDate.Text = dt.Rows[0]["Date"].ToString();
                        tComment.Text = dt.Rows[0]["Comment"].ToString();

                        if (dt.Rows[0]["AvailableForEdit"].ToString() == "N")
                        {
                            AvailableForEdit = false;
                        }
                        else
                        {
                            AvailableForEdit = true;
                        }
                    }
                }

                if (AvailableForEditChange != null)
                {
                    AvailableForEditChange(this, null);
                }
            }

            catch
            {
                throw;
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tsbFilter_Click(object sender, EventArgs e)
        {
            FilterObject fo = new FilterObject();
            fo.FilterString = FilterString;

            fo.ShowDialog();
            FilterString = fo.FilterString;
            PoPFilter = fo.PoPFilter;

            if (FilterString.Trim() != "")
            {
                LoadDatabaseFilteredObjects();
                tsbRemoveFilter.Enabled = true;
                return;
            }

            if (PoPFilter.Trim() != "")
            {
                LoadDatabaseFilteredObjectsPOP();
                tsbRemoveFilter.Enabled = true;
                return;
            }
        }

        private string GetProjectFilter()
        {
            string ObjName = string.Empty;
            string DBName = string.Empty;
            string ObjectList = string.Empty;

            using (DataStuff sn = new DataStuff())
            {
                DataTable dt = sn.GetProjectObjects(PoPFilter);

                if (dt.Rows.Count > 0)
                {
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            DBName = row["DBName"].ToString();

                            if (DBName.Trim() == cbDatabaseName.Text.Trim())
                            {
                                ObjName = row["DBObjectName"].ToString();

                                if (ObjectList.Trim() == "")
                                {
                                    ObjectList = "'" + ObjName + "'";
                                }
                                else
                                {
                                    ObjectList = ObjectList + ", '" + ObjName + "'";
                                }
                            }
                        }
                    }
                }
            }

            return ObjectList;
        }

        private void tsbRemoveFilter_Click(object sender, EventArgs e)
        {
            FilterString = "";
            tsbRemoveFilter.Enabled = false;
            LoadDatabaseObjects();
        }

        private void tsbViewObject_Click(object sender, EventArgs e)
        {
            ObjectViewer ov = new ObjectViewer();
            string ObjectText = string.Empty;
            string ObjectDescription = string.Empty;

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetDatabaseObjectText(ConnectionString, DatabaseName, ObjectName);

                    if (dt.Rows.Count > 1)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            ObjectText = row["FullDefinition"].ToString();
                            ObjectDescription = row["ROUTINE_NAME"].ToString() + ": " + row["ROUTINE_TYPE"].ToString();
                        }
                    }
                    else
                    {
                        ObjectText = LoadHelpText(ConnectionString, DatabaseName, ObjectName);
                        ObjectDescription = ObjectName;

                        if (ObjectText.Trim() == "")
                        {
                            dt = sn.GetTableHelpText(ConnectionString, DatabaseName, ObjectName);

                            foreach (DataRow row in dt.Rows)
                            {
                                ObjectText = row[0].ToString();
                            }

                            ObjectText = ObjectText.Replace("~", Environment.NewLine);
                        }
                    }
                }

                ov.ObjectDescription = ObjectDescription;
                ov.ObjectText = ObjectText;
                ov.ConnectionString = ConnectionString;
                ov.DatabaseName = DatabaseName;
                ov.ObjectName = ObjectName;
                ov.AliasName = ServerAlias;

                ov.Show();
            }

            catch
            {

            }
        }

        private string LoadHelpText(string CnnString, string DBName, string ObjName)
        {
            string ObjectText = string.Empty;

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetObjectHelpText(CnnString, DBName, ObjName);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            ObjectText = ObjectText + row[0].ToString();
                        }
                    }
                }

                return ObjectText;
            }

            catch
            {
                return "";
            }
        }

        private void DisableStatus()
        {
            string ObjectTypeReturned = string.Empty;

            //tsbCheckInOut.Enabled = false;
            tsbHistory.Enabled = false;

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetObjectType(ConnectionString, DatabaseName, ObjectName);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            ObjectTypeReturned = row[0].ToString();
                        }
                    }

                    if ((ObjectTypeReturned.Trim() == "P") || (ObjectTypeReturned.Trim() == "FN") || (ObjectTypeReturned.Trim() == "TF") || (ObjectTypeReturned.Trim() == "IF"))
                    {
                        //if (!AvailableForEdit)
                        //{
                        //    tsbCheckInOut.Enabled = false;
                        //}
                        //else
                        //{
                        //    tsbCheckInOut.Enabled = true;
                        //}

                        tsbHistory.Enabled = true;
                    }

                    /*
                    FN = Scalar function
                    P = Stored procedure
                    TF = Table function
                    TR = SQL DML Trigger
                    V = View
                    IF = In-lined table-function
                    D = Default or DEFAULT constraint
                    */

                    switch (ObjectTypeReturned.Trim())
                    {
                        case "P":
                            tObjectType.Text = "Stored Procedure";
                            break;

                        case "FN":
                            tObjectType.Text = "Scalar Function";
                            break;

                        case "TF":
                            tObjectType.Text = "Table Function";
                            break;

                        case "IF":
                            tObjectType.Text = "In-Line Table Function";
                            break;

                        case "U":
                            tObjectType.Text = "User Table";
                            break;

                        case "TR":
                            tObjectType.Text = "SQL DML Trigger";
                            break;

                        case "V":
                            tObjectType.Text = "View";
                            break;

                        default:
                            break;
                    }
                }
            }

            catch
            {
                return;
            }
        }

        //private void tsbCheckInOut_Click(object sender, EventArgs e)
        //{
        //    ObjectCheckOut cio = new ObjectCheckOut();

        //    string ObjectText = string.Empty;
        //    string ObjectDescription = string.Empty;

        //    try
        //    {
        //        using (DataStuff sn = new DataStuff())
        //        {
        //            DataTable dt = sn.GetDatabaseObjectText(ConnectionString, DatabaseName, ObjectName);

        //            if (dt.Rows.Count > 1)
        //            {
        //                foreach (DataRow row in dt.Rows)
        //                {
        //                    ObjectText = row["FullDefinition"].ToString();
        //                    ObjectDescription = row["ROUTINE_NAME"].ToString() + ": " + row["ROUTINE_TYPE"].ToString();
        //                }
        //            }
        //            else
        //            {
        //                ObjectText = LoadHelpText(ConnectionString, DatabaseName, ObjectName);

        //                if (ObjectText.Trim() == "")
        //                {
        //                    dt = sn.GetTableHelpText(ConnectionString, DatabaseName, ObjectName);

        //                    foreach (DataRow row in dt.Rows)
        //                    {
        //                        ObjectText = row[0].ToString();
        //                    }

        //                    ObjectText = ObjectText.Replace("~", Environment.NewLine);
        //                }
        //            }

        //            cio.ObjectDescription = ObjectDescription;
        //            cio.ObjectText = ObjectText;
        //            cio.ConnectionString = ConnectionString;
        //            cio.DatabaseName = DatabaseName;
        //            cio.ObjectName = ObjectName;
        //            cio.AliasName = ServerAlias;

        //            cio.ShowDialog();

        //            LoadObjectDetail();
        //            GetObjectStatus();
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void tsbHistory_Click(object sender, EventArgs e)
        {
            ViewObjectHistory oh = new ViewObjectHistory();

            oh.ConnectionString = ConnectionString;
            oh.DatabaseName = DatabaseName;
            oh.ObjectName = ObjectName;

            oh.ShowDialog();
        }

        private void cObjStoredProcedures_CheckedChanged(object sender, EventArgs e)
        {
            if (cObjStoredProcedures.Checked)
            {
                ObjectsSelected = "1" + ObjectsSelected.Substring(1, 4);
            }
            else
            {
                ObjectsSelected = "0" + ObjectsSelected.Substring(1, 4);
            }

            ExtObjectsSelected = ObjectsSelected;

            if (ObjectTypesChanged != null)
            {
                ObjectTypesChanged(this, e);
            }

            LoadDatabaseObjects();
        }

        private void cObjFunctions_CheckedChanged(object sender, EventArgs e)
        {
            if (cObjFunctions.Checked)
            {
                ObjectsSelected = ObjectsSelected.Substring(0, 1) + "1" + ObjectsSelected.Substring(2, 3);
            }
            else
            {
                ObjectsSelected = ObjectsSelected.Substring(0, 1) + "0" + ObjectsSelected.Substring(2, 3);
            }

            ExtObjectsSelected = ObjectsSelected;

            if (ObjectTypesChanged != null)
            {
                ObjectTypesChanged(this, e);
            }

            LoadDatabaseObjects();
        }

        private void cObjTables_CheckedChanged(object sender, EventArgs e)
        {
            if (cObjTables.Checked)
            {
                ObjectsSelected = ObjectsSelected.Substring(0, 2) + "1" + ObjectsSelected.Substring(3, 2);
            }
            else
            {
                ObjectsSelected = ObjectsSelected.Substring(0, 2) + "0" + ObjectsSelected.Substring(3, 2);
            }

            ExtObjectsSelected = ObjectsSelected;

            if (ObjectTypesChanged != null)
            {
                ObjectTypesChanged(this, e);
            }

            LoadDatabaseObjects();
        }

        private void cObjTriggers_CheckedChanged(object sender, EventArgs e)
        {
            if (cObjTriggers.Checked)
            {
                ObjectsSelected = ObjectsSelected.Substring(0, 3) + "1" + ObjectsSelected.Substring(4, 1);
            }
            else
            {
                ObjectsSelected = ObjectsSelected.Substring(0, 3) + "0" + ObjectsSelected.Substring(4, 1);
            }

            ExtObjectsSelected = ObjectsSelected;

            if (ObjectTypesChanged != null)
            {
                ObjectTypesChanged(this, e);
            }

            LoadDatabaseObjects();
        }

        private void cObjViews_CheckedChanged(object sender, EventArgs e)
        {
            if (cObjViews.Checked)
            {
                ObjectsSelected = ObjectsSelected.Substring(0, 4) + "1";
            }
            else
            {
                ObjectsSelected = ObjectsSelected.Substring(0, 4) + "0";
            }

            ExtObjectsSelected = ObjectsSelected;

            if (ObjectTypesChanged != null)
            {
                ObjectTypesChanged(this, e);
            }

            LoadDatabaseObjects();
        }

        private void tsbCompare_Click(object sender, EventArgs e)
        {
            string File1 = string.Empty;
            string File2 = string.Empty;
            string File1Content = string.Empty;
            string File2Content = string.Empty;
            bool FoundContent = false;
            string CompareWith = string.Empty;

            string Server2ConnString = string.Empty;

            string path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)).FullName;


            File1 = path.Trim() + @"\" + "Compare1" + DateTime.Now.ToLongTimeString().Replace(":", "").Replace(" ", "") + ".txt";
            File2 = path.Trim() + @"\" + "Compare2" + DateTime.Now.ToLongTimeString().Replace(":", "").Replace(" ", "") + ".txt";

            switch (ControlNumber)
            {
                case 1:
                    File1Content = LoadObject(ConnectionString, DatabaseName, ObjectName);
                    Server2ConnString = GetServerConnectionString(CompareAlias2);
                    CompareWith = CompareAlias2;

                    break;

                case 2:
                    File1Content = LoadObject(ConnectionString, DatabaseName, ObjectName);

                    if (CompareAlias3.Trim() != "")
                    {
                        Server2ConnString = GetServerConnectionString(CompareAlias3);
                        CompareWith = CompareAlias3;
                    }
                    else
                    {
                        Server2ConnString = GetServerConnectionString(CompareAlias1);
                        CompareWith = CompareAlias1;
                    }

                    break;

                case 3:
                    File1Content = LoadObject(ConnectionString, DatabaseName, ObjectName);

                    if (CompareAlias4.Trim() != "")
                    {
                        Server2ConnString = GetServerConnectionString(CompareAlias4);
                        CompareWith = CompareAlias4;
                    }
                    else
                    {
                        Server2ConnString = GetServerConnectionString(CompareAlias1);
                        CompareWith = CompareAlias1;
                    }

                    break;

                case 4:
                    File1Content = LoadObject(ConnectionString, DatabaseName, ObjectName);
                    Server2ConnString = GetServerConnectionString(CompareAlias1);
                    CompareWith = CompareAlias1;

                    break;

                case 5:
                    File1Content = LoadObject(ConnectionString, DatabaseName, ObjectName);
                    Server2ConnString = GetServerConnectionString(CompareAlias4);
                    CompareWith = CompareAlias4;

                    break;

                default:
                    break;
            }

            if (Server2ConnString.Trim() != "")
            {
                File2Content = LoadObject(Server2ConnString, DatabaseName, ObjectName);
            }

            if ((File1Content.Trim() != "") && (File2Content.Trim() != ""))
            {
                try
                {
                    System.IO.File.WriteAllText(File1, File1Content);
                    System.IO.File.WriteAllText(File2, File2Content);
                    FoundContent = true;
                }

                catch
                {
                    FoundContent = false;
                }
            }

            if (FoundContent)
            {
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();

                    WinMergePath = @"c:\Program Files (x86)\WinMerge\WinMergeU.exe";

                    if (!File.Exists(WinMergePath))
                    {
                        WinMergePath = @"c:\Program Files\WinMerge\WinMergeU.exe";
                    }

                    startInfo.FileName = WinMergePath;

                    startInfo.Arguments = "/e /dl " + "\"" + ServerAlias + "\" /dr " + "\"" + CompareWith + "\" " + @File1 + " " + @File2;
                    Process.Start(startInfo);
                }

                catch
                {
                    MessageBox.Show("Oops, something went wrong. Please check your WinMerge installation.", "WinMerge", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string LoadObject(string CnnString, string DBName, string ObjName)
        {
            string ObjectText = string.Empty;

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetDatabaseObjectText(CnnString, DBName, ObjName);

                    if (dt.Rows.Count > 1)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            ObjectText = row["FullDefinition"].ToString();
                        }
                    }
                    else
                    {
                        ObjectText = LoadHelpText(CnnString, DBName, ObjName);

                        if (ObjectText.Trim() == "")
                        {
                            dt = sn.GetTableHelpText(CnnString, DBName, ObjName);

                            foreach (DataRow row in dt.Rows)
                            {
                                ObjectText = row[0].ToString();
                            }

                            ObjectText = ObjectText.Replace("~", Environment.NewLine);
                        }
                    }
                }

                return ObjectText;

            }

            catch
            {
                return "";
            }
        }

        private string GetServerConnectionString(string ServerAlias)
        {
            try
            {
                string ServerName = string.Empty;
                string UserName = string.Empty;
                byte[] Password = null;
                string IntegratedSecurity = string.Empty;
                string ConnectionString = string.Empty;

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetServerDetail(ServerAlias, UserID);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            ServerName = row["ServerName"].ToString();
                            UserName = row["UserName"].ToString();
                            Password = (byte[])row["Password"];
                            IntegratedSecurity = row["IntegratedSecurity"].ToString();
                        }
                    }
                }

                if (IntegratedSecurity == "Y")
                {
                    ConnectionString = "Data Source=" + ServerName + ";Integrated Security=True";
                }
                else
                {
                    string DecryptedPassword = string.Empty;

                    using (DataStuff ds = new DataStuff())
                    {
                        DecryptedPassword = ds.Ontsyfer(Password);
                    }

                    ConnectionString = "Data Source=" + ServerName + ";User ID=" + UserName + ";Password=" + DecryptedPassword;
                }

                return ConnectionString;
            }

            catch
            {
                return "";
            }
        }
    }
}
