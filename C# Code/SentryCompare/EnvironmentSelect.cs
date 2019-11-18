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

namespace SentryCompare
{
    public partial class EnvironmentSelect : Form
    {
        string UserName = string.Empty;
        int UserID = 0;

        public string Alias1 = string.Empty;
        string Alias2 = string.Empty;
        string Alias3 = string.Empty;
        string Alias4 = string.Empty;
        string Alias5 = string.Empty;

        public string ObjectName = string.Empty;
        public string DatabaseName = string.Empty;
        public string ObjectType = string.Empty;

        public EnvironmentSelect()
        {
            InitializeComponent();
        }

        private void EnvironmentSelect_Load(object sender, EventArgs e)
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

            SetServerIn();
        }

        private void SetServerIn()
        {
            foreach (Control OuterCtl in gbEnvironments.Controls)
            {
                foreach (Control InnerCtl in OuterCtl.Controls)
                {
                    if (InnerCtl is CheckBox)
                    {
                        if (((CheckBox)(InnerCtl)).Text == Alias1)
                        {
                            ((CheckBox)(InnerCtl)).Checked = true;
                            ((CheckBox)(InnerCtl)).Enabled = false;
                        }
                    }
                }
            }
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

                                gbServerGroup.Height = LastY + 10;

                                c.CheckedChanged += CheckBox_Clicked;
                            }
                        }
                    }

                    gbEnvironments.Height = CurGroupBox.Top + MaxY + 10;
                    gbEnvironments.Width = LastXSG;
                    this.Width = gbEnvironments.Width + gbEnvironments.Left + 30;
                    this.Height = gbEnvironments.Height + gbEnvironments.Top + 60 + cmdNext.Height;

                    cmdNext.Left = gbEnvironments.Width + gbEnvironments.Left - cmdNext.Width;
                    cmdNext.Top = gbEnvironments.Top + gbEnvironments.Height + 10;
                    cmdCancel.Top = cmdNext.Top;
                    cmdCancel.Left = cmdNext.Left - cmdCancel.Width - 10;

                    cmdNext.Enabled = false;
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

            Alias2 = "";
            Alias3 = "";
            Alias4 = "";
            Alias5 = "";

            if (((CheckBox)sender).Checked)
            {
                ServerAlias = ((CheckBox)sender).Text;

                DisableEnvironments();
            }

            foreach (Control OuterCtl in gbEnvironments.Controls)
            {
                foreach (Control InnerCtl in OuterCtl.Controls)
                {
                    if (InnerCtl is CheckBox)
                    {
                        if ((((CheckBox)(InnerCtl)).Checked) && (((CheckBox)InnerCtl).Enabled))
                        {
                            if (Alias2 == "")
                            {
                                Alias2 = ((CheckBox)InnerCtl).Text;
                            }
                            else
                            {
                                if (Alias3 == "")
                                {
                                    Alias3 = ((CheckBox)InnerCtl).Text;
                                }
                                else
                                {
                                    if (Alias4 == "")
                                    {
                                        Alias4 = ((CheckBox)InnerCtl).Text;
                                    }
                                    else
                                    {
                                        if (Alias5 == "")
                                        {
                                            Alias5 = ((CheckBox)InnerCtl).Text;
                                        }
                                    }
                                }
                            }

                            CheckCount++;
                        }
                    }
                }
            }

            if (CheckCount == 0)
            {
                EnableAllEnvironments();
            }

            if (CheckCount >= 1)
            {
                cmdNext.Enabled = true;
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

            cmdNext.Enabled = false;
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

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdNext_Click(object sender, EventArgs e)
        {
            CompareObjects oc = new CompareObjects();

            oc.ServerAlias = Alias1;
            oc.DatabaseName = DatabaseName;
            oc.ObjectName = ObjectName;

            oc.Alias1 = Alias1;
            oc.Alias2 = Alias2;
            oc.Alias3 = Alias3;
            oc.Alias4 = Alias4;
            oc.Alias5 = Alias5;

            oc.ShowDialog();

            this.Close();
        }
    }
}
