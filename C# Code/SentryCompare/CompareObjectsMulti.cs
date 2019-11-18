using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SentryDataStuff;
using SentryControls;

namespace SentryCompare
{
    public partial class CompareObjectsMulti : Form
    {
        public int ServerCount = 0;
        int ControlCount = 0;
        string DBName = string.Empty;
        string ObjectName = string.Empty;
        string UserName = string.Empty;
        string ObjectsSelected = string.Empty;
        int UserID = 0;
        string Alias1 = string.Empty;
        string Alias2 = string.Empty;
        string Alias3 = string.Empty;
        string Alias4 = string.Empty;
        string Alias5 = string.Empty;
        Color AllOkay = Color.Lime;
        Color Danger = Color.Yellow;
        Color Warn = Color.DeepSkyBlue;

        public CompareObjectsMulti()
        {
            InitializeComponent();
        }

        private void CompareObjectsMulti_Load(object sender, EventArgs e)
        {
            using (DataStuff sn = new DataStuff())
            {
                DataTable dt = sn.SingleUser(Environment.UserName, "CompareObject");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        UserName = row["UserName"].ToString();
                        UserID = Convert.ToInt32(row["UserID"].ToString());
                    }
                }
            }

            GetServers(UserID);
        }

        private void GetServers(int UserID)
        {
            int LastX = 20;
            int LastY = 20;
            int LastXSG = 20;
            int MaxY = 0;

            string ServerGroup = string.Empty;
            GroupBox CurGroupBox = null;

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetServers(UserID);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["ServerGroupDesc"].ToString() == ServerGroup)
                            {
                                CheckBox c = new CheckBox();
                                c.Parent = CurGroupBox;
                                c.Name = "c" + row["ServerAliasDesc"].ToString();
                                c.Text = row["ServerAliasDesc"].ToString();
                                c.Left = LastX;
                                c.Top = LastY;
                                LastY = c.Top + c.Height;
                                c.CheckedChanged += CheckBox_Clicked;

                                CurGroupBox.Height = LastY;

                                if (MaxY < CurGroupBox.Height)
                                {
                                    MaxY = CurGroupBox.Height + 10;
                                }

                                CurGroupBox.Height = MaxY;
                            }
                            else
                            {
                                GroupBox gbServerGroup = new GroupBox();

                                ServerGroup = row["ServerGroupDesc"].ToString();
                                gbServerGroup.Parent = gbEnvironments;
                                gbServerGroup.Left = LastXSG;
                                gbServerGroup.Top = 18;
                                LastY = 20;
                                gbServerGroup.Name = "gbServerGroup" + ServerGroup;
                                LastXSG = gbServerGroup.Left + gbServerGroup.Width + 20;
                                gbServerGroup.Text = ServerGroup;

                                CurGroupBox = gbServerGroup;

                                CheckBox c = new CheckBox();
                                c.Parent = CurGroupBox;
                                c.Name = "c" + row["ServerAliasDesc"].ToString();
                                c.Text = row["ServerAliasDesc"].ToString();
                                c.Left = LastX;
                                c.Top = LastY;
                                LastY = c.Top + c.Height;
                                c.CheckedChanged += CheckBox_Clicked;

                                gbServerGroup.Height = LastY + 10;
                            }
                        }
                    }

                    gbEnvironments.Height = CurGroupBox.Top + MaxY + 10;
                    LastY = gbEnvironments.Top + gbEnvironments.Height + 5;
                    //gbEnvironments.Height = LastY + 10;
                    gbServerControls.Top = LastY + 30;
                }
            }

            catch
            {
                throw;
            }
        }

        private void CheckBox_Clicked(object sender, EventArgs e)
        {
            string ServerAlias = string.Empty;
            int CheckCount = 0;

            if (((CheckBox)sender).Checked)
            {
                ServerAlias = ((CheckBox)sender).Text;

                LoadControl(ServerAlias);

                DisableEnvironments();
            }

            if (!((CheckBox)sender).Checked)
            {
                gbServerControls.Controls.Clear();
                ControlCount = 0;

                foreach (Control OuterCtl in gbEnvironments.Controls)
                {
                    foreach (Control InnerCtl in OuterCtl.Controls)
                    {
                        if (InnerCtl is CheckBox)
                        {
                            if (((CheckBox)InnerCtl).Checked)
                            {
                                ServerAlias = ((CheckBox)InnerCtl).Text;
                                LoadControl(ServerAlias);
                            }
                        }
                    }
                }
            }

            foreach (Control OuterCtl in gbEnvironments.Controls)
            {
                foreach (Control InnerCtl in OuterCtl.Controls)
                {
                    if (InnerCtl is CheckBox)
                    {
                        if (((CheckBox)(InnerCtl)).Checked)
                        {
                            CheckCount++;
                        }
                    }
                }
            }

            if (CheckCount == 0)
            {
                DBName = "";
                ObjectName = "";
                EnableAllEnvironments();
            }
        }

        private void EnableAllEnvironments()
        {
            foreach (Control ctl in gbEnvironments.Controls)
            {
                if (ctl is GroupBox)
                {
                    ((GroupBox)ctl).Enabled = true;
                }
            }
        }

        private void DisableEnvironments()
        {
            foreach (Control ctl in gbEnvironments.Controls)
            {
                if (ctl is GroupBox)
                {
                    ((GroupBox)ctl).Enabled = false;
                }
            }

            foreach (Control OuterCtl in gbEnvironments.Controls)
            {
                if (OuterCtl is GroupBox)
                {
                    foreach (Control InnerCtl in OuterCtl.Controls)
                    {
                        if (InnerCtl is CheckBox)
                        {
                            if (((CheckBox)InnerCtl).Checked)
                            {
                                ((CheckBox)InnerCtl).Parent.Enabled = true;
                            }
                        }
                    }
                }
            }
        }

        private void LoadControl(string ServerAlias)
        {
            int LastX = 20;
            int LastY = 20;
            int ObjectWidth = 0;

            ControlCount++;

            if (ControlCount == 1)
            {
                DBObjectMulti DBObjectMulti = new DBObjectMulti();
                DBObjectMulti.Parent = gbServerControls;
                DBObjectMulti.ServerAlias = ServerAlias;
                DBObjectMulti.ControlNumber = ControlCount;
                DBObjectMulti.DBChanged += UpdateDBChange;
                DBObjectMulti.ObjectChanged += UpdateObjectChange;
                DBObjectMulti.ModifiedDateChanged += UpdateDateModifiedChange;
                DBObjectMulti.CreateDateChanged += UpdateDateCreatedChange;
                DBObjectMulti.ObjectTypesChanged += UpdateObjectSelectionChange;
                DBObjectMulti.AvailableForEditChange += UpdateAvailableForEditChange;

                DBObjectMulti.Left = LastX;
                DBObjectMulti.Top = LastY;
                ObjectWidth = DBObjectMulti.Width;
                LastX = DBObjectMulti.Left;

                DBObjectMulti.GetObjectDetail();

                if (DBName.Trim() != "")
                {
                    DBObjectMulti.SetDatabaseName(DBName);
                }

                if (ObjectName.Trim() != "")
                {
                    DBObjectMulti.SetObjectName(ObjectName);
                }

                gbServerControls.Height = DBObjectMulti.Height + 30;
            }
            else
            {
                DBObjectMulti DBObjectMulti = new DBObjectMulti();
                DBObjectMulti.Parent = gbServerControls;
                DBObjectMulti.ServerAlias = ServerAlias;
                DBObjectMulti.ControlNumber = ControlCount;
                DBObjectMulti.DBChanged += UpdateDBChange;
                DBObjectMulti.ObjectChanged += UpdateObjectChange;
                DBObjectMulti.ModifiedDateChanged += UpdateDateModifiedChange;
                DBObjectMulti.CreateDateChanged += UpdateDateCreatedChange;
                DBObjectMulti.ObjectTypesChanged += UpdateObjectSelectionChange;
                DBObjectMulti.AvailableForEditChange += UpdateAvailableForEditChange;

                DBObjectMulti.Left = LastX + ((ControlCount - 1) * DBObjectMulti.Width) + 20;
                DBObjectMulti.Top = LastY;
                ObjectWidth = DBObjectMulti.Width;
                LastX = DBObjectMulti.Left;

                DBObjectMulti.GetObjectDetail();

                if (DBName.Trim() != "")
                {
                    DBObjectMulti.SetDatabaseName(DBName);
                }

                if (ObjectName.Trim() != "")
                {
                    DBObjectMulti.SetObjectName(ObjectName);
                }

                gbServerControls.Height = DBObjectMulti.Height + 30;
            }

            gbServerControls.Top = gbEnvironments.Top + gbEnvironments.Height;
            gbServerControls.Left = gbEnvironments.Left;
            gbServerControls.Width = LastX + ObjectWidth + 25;

            this.Width = gbServerControls.Width + 50;
            this.Height = gbServerControls.Top + gbServerControls.Height + 50;

            gbEnvironments.Width = this.Width - 50;

            switch (ControlCount)
            {
                case 1:
                    Alias1 = ServerAlias;
                    break;

                case 2:
                    Alias2 = ServerAlias;
                    break;

                case 3:
                    Alias3 = ServerAlias;
                    break;

                case 4:
                    Alias4 = ServerAlias;
                    break;

                case 5:
                    Alias5 = ServerAlias;
                    break;

                default:
                    break;
            }

            UpdateCompareServer(Alias1, Alias2, Alias3, Alias4, Alias5);
        }

        private void UpdateDBChange(object sender, EventArgs e)
        {
            DBName = ((DBObjectMulti)sender).DatabaseName;

            foreach (Control ctl in gbServerControls.Controls)
            {
                if (ctl is DBObjectMulti)
                {
                    ((DBObjectMulti)(ctl)).SetDatabaseName(DBName);
                }
            }
        }

        private void UpdateObjectChange(object sender, EventArgs e)
        {
            ObjectName = ((DBObjectMulti)sender).ObjectName;

            foreach (Control ctl in gbServerControls.Controls)
            {
                if (ctl is DBObjectMulti)
                {
                    ((DBObjectMulti)(ctl)).SetObjectName(ObjectName);
                }
            }
        }

        private void UpdateAvailableForEditChange(object sender, EventArgs e)
        {
            bool AllowEdit = true;

            AllowEdit = ((DBObjectMulti)sender).AvailableForEdit;

            foreach (Control ctl in gbServerControls.Controls)
            {
                if (ctl is DBObjectMulti)
                {
                    ((DBObjectMulti)(ctl)).SetAvailableForEdit(AllowEdit);
                }
            }
        }

        private void UpdateCompareServer(string Alias1, string Alias2, string Alias3, string Alias4, string Alias5)
        {
            foreach (Control ctl in gbServerControls.Controls)
            {
                if (ctl is DBObjectMulti)
                {
                    ((DBObjectMulti)(ctl)).SetCompareServer(Alias1, Alias2, Alias3, Alias4, Alias5);
                }
            }
        }

        private void UpdateObjectSelectionChange(object sender, EventArgs e)
        {
            ObjectsSelected = ((DBObjectMulti)sender).ExtObjectsSelected;

            foreach (Control ctl in gbServerControls.Controls)
            {
                if (ctl is DBObjectMulti)
                {
                    ((DBObjectMulti)(ctl)).SetObjectsSelected(ObjectsSelected);
                }
            }
        }

        private void UpdateDateModifiedChange(object sender, EventArgs e)
        {
            string ObjectName = ((DBObjectMulti)sender).ObjectName;
            string FirstDate = string.Empty;
            string TestDate = string.Empty;

            foreach (Control ctl in gbServerControls.Controls)
            {
                if (ctl is DBObjectMulti)
                {
                    if (FirstDate.Trim() == "")
                    {
                        FirstDate = ((DBObjectMulti)(ctl)).ModifiedDate;
                        ((DBObjectMulti)(ctl)).SetDateModifiedColour(AllOkay);
                    }
                    else
                    {
                        TestDate = ((DBObjectMulti)(ctl)).ModifiedDate;

                        if (TestDate != FirstDate)
                        {
                            ((DBObjectMulti)(ctl)).SetDateModifiedColour(Warn);
                        }
                        else
                        {
                            ((DBObjectMulti)(ctl)).SetDateModifiedColour(AllOkay);
                        }
                    }
                }
            }
        }

        private void UpdateDateCreatedChange(object sender, EventArgs e)
        {
            string ObjectName = ((DBObjectMulti)sender).ObjectName;
            string FirstDate = string.Empty;
            string TestDate = string.Empty;

            foreach (Control ctl in gbServerControls.Controls)
            {
                if (ctl is DBObjectMulti)
                {
                    if (FirstDate.Trim() == "")
                    {
                        FirstDate = ((DBObjectMulti)(ctl)).CreateDate;
                        ((DBObjectMulti)(ctl)).SetDateCreatedColour(AllOkay);
                    }
                    else
                    {
                        TestDate = ((DBObjectMulti)(ctl)).CreateDate;

                        if (TestDate != FirstDate)
                        {
                            ((DBObjectMulti)(ctl)).SetDateCreatedColour(Warn);
                        }
                        else
                        {
                            ((DBObjectMulti)(ctl)).SetDateCreatedColour(AllOkay);
                        }
                    }
                }
            }
        }
    }
}
