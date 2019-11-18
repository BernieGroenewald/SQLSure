using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SentryGeneral;
using SentryDataStuff;

namespace SentryControls
{
    public partial class DBCheckBox : UserControl
    {
        public string ConnectionString = string.Empty;
        public string DefaultConnectionString = string.Empty;

        public string DatabaseSelection = string.Empty;

        public event EventHandler SelectionChangedDatabase;
        public event EventHandler RefreshDatabase;

        string UserName = string.Empty;
        int UserID = 0;
        int CheckCount = 0;
        int AllCount = 0;
        bool ResetAll = true;
        string XMLServerAlias = string.Empty;
        int LastX = 0;
        int LastY = 0;

        CheckBox cAll = new CheckBox();
        CheckBox cAllUserDatabases = new CheckBox();
        CheckBox cAllSystemDatabases = new CheckBox();
        Label lUserDB = new Label();
        Label lSystemDB = new Label();

        General gs = new General();
        
        public DBCheckBox()
        {
            InitializeComponent();
        }

        private void CheckCombo_Load(object sender, EventArgs e)
        {
            using (DataStuff sn = new DataStuff())
            {
                //sn.ConnectionString = DefaultConnectionString;

                DataTable dt = sn.SingleUser(Environment.UserName, "CheckCombo");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        UserName = row["UserName"].ToString();
                        UserID = Convert.ToInt32(row["UserID"].ToString());
                    }
                }
            }

            XMLServerAlias = gs.ServerAliasSelected;

            if (XMLServerAlias.Trim() != "")
            {
                if (!GetServerDetail(XMLServerAlias))
                {
                    //Get the databases on the default connection

                    LoadDatabaseNames();
                    cAll.Checked = true;
                }

                LoadDatabaseNames();

                DatabaseSelection = gs.DatabaseSelection;

                if (DatabaseSelection.Trim() == "")
                {
                    cAll.Checked = true;
                }
                else
                {
                    LoadDatabaseSelection();
                    SetChecks();
                }
            }
            else
            {
                //Get the databases on the default connection

                LoadDatabaseNames();
                cAll.Checked = true;
            }

            this.Height = 30;
        }

        public void SetDatabaseName(string NewName)
        {
            string CurrentAlias = string.Empty;
            //bool IsInGroup = false;

            //See if the server alias is in the same server group (Not relevant, get specific server's databases 24 Jan 2017)

            CurrentAlias = gs.ServerAliasSelected;

            //using (DataStuff ds = new DataStuff())
            //{
            //    ds.ConnectionString = DefaultConnectionString;

            //    DataTable dt = ds.GetServersInServerGroup(CurrentAlias, UserID);

            //    if (dt.Rows.Count > 0)
            //    {
            //        foreach (DataRow row in dt.Rows)
            //        {
            //           if (row["ServerAliasDesc"].ToString() == NewName)
            //           {
            //                IsInGroup = true;
            //           }
            //        }
            //    }
            //}

            //if (!IsInGroup)
            //{
                //Clear the selection

            DatabaseSelection = "";
            GetServerDetail(NewName);
            LoadDatabaseNames();
            cAll.Checked = false;
            gs.ServerAliasSelected = NewName;
            cmdDropDownDatabases.Enabled = true;
            ResetAll = true;
            SetChecks();
            FindSelected();

            //}
            //else
            //{
            //    SetChecks();
            //}
        }

        private void LoadDatabaseSelection()
        {
            List<String> DatabaseElements;
            string CurBox = string.Empty;

            DatabaseElements = DatabaseSelection.Split(',').ToList();
            DatabaseSelection = "";
            CheckCount = 0;

            foreach (Control Ctl in pMain.Controls)
            {
                if (Ctl is CheckBox)
                {
                    ((CheckBox)Ctl).Checked = false;
                }
            }

            foreach (string db in DatabaseElements)
            {
                foreach (Control Ctl in pMain.Controls)
                {
                    if (Ctl is CheckBox)
                    {
                        CurBox = ((CheckBox)Ctl).Tag.ToString();

                        if (CurBox != "None")
                        {
                            if (CurBox.Substring(0, 4) == "User")
                            {
                                CurBox = CurBox.Substring(5);
                            }
                            else
                            {
                                if (CurBox.Substring(0, 6) == "System")
                                {
                                    CurBox = CurBox.Substring(7);
                                }
                            }

                            if (CurBox == db)
                            {
                                ((CheckBox)Ctl).Checked = true;
                                CheckCount++;
                            }
                        }
                    }
                }
            }

            //if (CheckCount == 0)
            //{
            //    cAll.Checked = true;
            //}
        }

        private void SetChecks()
        {
            string CurBox = string.Empty;
            bool FoundUserDBUnchecked = false;
            bool FoundSystemDBUnchecked = false;

            foreach (Control Ctl in pMain.Controls)
            {
                if (Ctl is CheckBox)
                {
                    if (((CheckBox)Ctl).Checked == false)
                    {
                        CurBox = ((CheckBox)Ctl).Tag.ToString();

                        if (CurBox != "None")
                        {
                            if (CurBox.Substring(0, 4) == "User")
                            {
                                FoundUserDBUnchecked = true;
                            }
                            else
                            {
                                if (CurBox.Substring(0, 6) == "System")
                                {
                                    FoundSystemDBUnchecked = true;
                                }
                            }
                        }
                    }
                }
            }

            if (!FoundUserDBUnchecked)
            {
                cAllUserDatabases.Checked = true;
            }

            if (!FoundSystemDBUnchecked)
            {
                cAllSystemDatabases.Checked = true;
            }

            if ((!FoundUserDBUnchecked) && (!FoundSystemDBUnchecked))
            {
                cAll.Checked = true;
            }
        }

        private void AddStaticControls()
        {
            cAll.Parent = pMain;
            cAll.Name = "cAll";
            cAll.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
            cAll.Text = "All Databases";
            cAll.Tag = "None";
            cAll.Left = 12;
            cAll.Top = 12;
            cAll.Width = 200;
            cAll.CheckedChanged += cAll_CheckedChanged;

            lUserDB.Parent = pMain;
            lUserDB.Text = "User Databases";
            lUserDB.BackColor = Color.White;
            lUserDB.ForeColor = lTemplate.ForeColor;
            lUserDB.Height = lTemplate.Height;
            lUserDB.Left = 24;
            lUserDB.Top = 42;

            cAllUserDatabases.Parent = pMain;
            cAllUserDatabases.Name = "cAllUserDatabases";
            cAllUserDatabases.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
            cAllUserDatabases.Text = "All User Databases";
            cAllUserDatabases.Tag = "None";
            cAllUserDatabases.Left = 27;
            cAllUserDatabases.Top = 59;
            cAllUserDatabases.Width = 200;
            cAllUserDatabases.CheckedChanged += cAllUserDatabases_CheckedChanged;

            lSystemDB.Parent = pMain;
            lSystemDB.Text = "System Databases";
            lSystemDB.BackColor = Color.White;
            lSystemDB.ForeColor = lTemplate.ForeColor;
            lSystemDB.Height = lTemplate.Height;
            lSystemDB.Left = 24;

            cAllSystemDatabases.Parent = pMain;
            cAllSystemDatabases.Name = "cAllSystemDatabases";
            cAllSystemDatabases.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
            cAllSystemDatabases.Text = "All System Databases";
            cAllSystemDatabases.Tag = "None";
            cAllSystemDatabases.Left = 27;
            cAllSystemDatabases.Width = 200;
            cAllSystemDatabases.CheckedChanged += cAllSystemDatabases_CheckedChanged;

            LastY = cAllUserDatabases.Top + cAllUserDatabases.Height + 5;
            LastX = cAllUserDatabases.Left;
        }

        private void LoadDatabaseNames()
        {
            pMain.Controls.Clear();

            AddStaticControls();

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    sn.ConnectionString = ConnectionString;
                    DataTable dt = sn.GetSystemUserDatabases(ConnectionString);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (Convert.ToInt32(row["database_id"].ToString()) > 4)
                            {
                                //User database

                                CheckBox c = new CheckBox();
                                c.Parent = pMain;
                                c.Name = "c" + row["Name"].ToString();
                                c.Text = row["Name"].ToString();
                                c.Tag = "User " + row["Name"].ToString();
                                c.Left = LastX;
                                c.Top = LastY;
                                c.Width = 200;
                                LastY = c.Top + c.Height;
                                c.CheckedChanged += CheckBox_Clicked;
                                AllCount++;
                            }
                        }

                        LastY = LastY + 10;
                        lSystemDB.Top = LastY;
                        cAllSystemDatabases.Top = lSystemDB.Top + lSystemDB.Height + 5;
                        LastY = cAllSystemDatabases.Top + cAllSystemDatabases.Height + 5;

                        foreach (DataRow row in dt.Rows)
                        {
                            if (Convert.ToInt32(row["database_id"].ToString()) <= 4)
                            {
                                //System database

                                CheckBox c = new CheckBox();
                                c.Parent = pMain;
                                c.Name = "c" + row["Name"].ToString();
                                c.Text = row["Name"].ToString();
                                c.Tag = "System " + row["Name"].ToString();
                                c.Left = LastX;
                                c.Top = LastY;
                                c.Width = 200;
                                LastY = c.Top + c.Height;
                                c.CheckedChanged += CheckBox_Clicked;
                                AllCount++;
                            }
                        }
                    }
                }
            }

            catch
            {
                throw;
            }
        }

        private void CheckBox_Clicked(object sender, EventArgs e)
        {
            string CurBox = string.Empty;

            if (!((CheckBox)sender).Checked)
            {
                CurBox = ((CheckBox)sender).Tag.ToString();

                ResetAll = false;

                if (CurBox.Contains("User"))
                {
                    cAll.Checked = false;
                    cAllUserDatabases.Checked = false;
                }
                else
                {
                    cAll.Checked = false;
                    cAllSystemDatabases.Checked = false;
                }

                ResetAll = true;
            }

            FindSelected();
        }

        private void FindSelected()
        {
            string CurBox = string.Empty;

            CheckCount = 0;
            DatabaseSelection = "";

            foreach (Control Ctl in pMain.Controls)
            {
                if (Ctl is CheckBox)
                {
                    if (((CheckBox)Ctl).Checked)
                    {
                        //Exclude All, All User Databases and All System Databases

                        CurBox = ((CheckBox)Ctl).Tag.ToString();

                        if (CurBox != "None")
                        {
                            if (CurBox.Substring(0, 4) == "User")
                            {
                                CurBox = CurBox.Substring(5);
                            }
                            else
                            {
                                if (CurBox.Substring(0, 6) == "System")
                                {
                                    CurBox = CurBox.Substring(7);
                                }
                            }

                            if (DatabaseSelection.Trim() == "")
                            {
                                DatabaseSelection = CurBox;
                            }
                            else
                            {
                                DatabaseSelection = DatabaseSelection + "," + CurBox;
                            }

                            CheckCount++;
                        }
                    }
                }
            }

            if (CheckCount > 0)
            {
                if (CheckCount == 1)
                {
                    tSelected.Text = DatabaseSelection + " selected...";
                }
                else
                {
                    if (AllCount == CheckCount)
                    {
                        tSelected.Text = "All databases selected...";
                    }
                    else
                    {
                        tSelected.Text = CheckCount.ToString() + " databases selected...";
                    }
                }
            }
            else
            {
                tSelected.Text = "No databases selected...";
            }
        }

        private bool GetServerDetail(string ServerAlias)
        {
            try
            {
                string ServerName = string.Empty;
                string UserName = string.Empty;
                byte[] Password = null;
                string IntegratedSecurity = string.Empty;

                using (DataStuff sn = new DataStuff())
                {
                    sn.ConnectionString = DefaultConnectionString;
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

                if (ServerName.Trim() == "")
                {
                    return false;
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

        private void cmdDropDown_Click(object sender, EventArgs e)
        {
            if (this.Height >= 355)
            {
                this.Height = 30;
            }
            else
            {
                this.Height = 355;
            }
        }

        private void CheckCombo_Leave(object sender, EventArgs e)
        {
            //Save the settings

            gs.DatabaseSelection = DatabaseSelection;

            this.Height = 30;

            if (RefreshDatabase != null)
            {
                RefreshDatabase(this, e);
            }
        }

        private void cAll_CheckedChanged(object sender, EventArgs e)
        {
            if (!ResetAll)
            {
                return;
            }

            if (((CheckBox)sender).Checked)
            {
                foreach (Control Ctl in pMain.Controls)
                {
                    if (Ctl is CheckBox)
                    {
                        if (!((CheckBox)Ctl).Checked)
                        {
                            ((CheckBox)Ctl).Checked = true;
                        }
                    }
                }
            }
            else
            {
                foreach (Control Ctl in pMain.Controls)
                {
                    if (Ctl is CheckBox)
                    {
                        if (((CheckBox)Ctl).Checked)
                        {
                            ((CheckBox)Ctl).Checked = false;
                        }
                    }
                }
            }

            FindSelected();
        }

        private void cAllUserDatabases_CheckedChanged(object sender, EventArgs e)
        {
            if (!ResetAll)
            {
                return;
            }

            if (((CheckBox)sender).Checked)
            {
                foreach (Control Ctl in pMain.Controls)
                {
                    if (Ctl is CheckBox)
                    {
                        if (!((CheckBox)Ctl).Checked)
                        {
                            if (((CheckBox)Ctl).Tag.ToString() != "None")
                            {
                                if (((CheckBox)Ctl).Tag.ToString().Substring(0, 4) == "User")
                                {
                                    ((CheckBox)Ctl).Checked = true;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (Control Ctl in pMain.Controls)
                {
                    if (Ctl is CheckBox)
                    {
                        if (((CheckBox)Ctl).Checked)
                        {
                            if (((CheckBox)Ctl).Tag.ToString() != "None")
                            {
                                if (((CheckBox)Ctl).Tag.ToString().Substring(0, 4) == "User")
                                {
                                    ((CheckBox)Ctl).Checked = false;
                                }
                            }
                        }
                    }
                }
            }

            FindSelected();
        }

        private void cAllSystemDatabases_CheckedChanged(object sender, EventArgs e)
        {
            if (!ResetAll)
            {
                return;
            }

            if (((CheckBox)sender).Checked)
            {
                foreach (Control Ctl in pMain.Controls)
                {
                    if (Ctl is CheckBox)
                    {
                        if (!((CheckBox)Ctl).Checked)
                        {
                            if (((CheckBox)Ctl).Tag.ToString() != "None")
                            {
                                if (((CheckBox)Ctl).Tag.ToString().Substring(0, 6) == "System")
                                {
                                    ((CheckBox)Ctl).Checked = true;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (Control Ctl in pMain.Controls)
                {
                    if (Ctl is CheckBox)
                    {
                        if (((CheckBox)Ctl).Checked)
                        {
                            if (((CheckBox)Ctl).Tag.ToString() != "None")
                            {
                                if (((CheckBox)Ctl).Tag.ToString().Substring(0, 6) == "System")
                                {
                                    ((CheckBox)Ctl).Checked = false;
                                }
                            }
                        }
                    }
                }
            }

            FindSelected();
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            //Save settings

            gs.DatabaseSelection = DatabaseSelection;

            this.Height = 30;

            if (RefreshDatabase != null)
            {
                RefreshDatabase(this, e);
            }
        }

        private void tSelected_TextChanged(object sender, EventArgs e)
        {
            if (SelectionChangedDatabase != null)
            {
                SelectionChangedDatabase(this, e);
            }
        }

       
    }
}
