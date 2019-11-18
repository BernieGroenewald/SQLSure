using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SentryControls
{
    public partial class DBObjectContainer : Form
    {
        public string ServerAlias = string.Empty;
        public string Alias1 = string.Empty;
        public string Alias2 = string.Empty;
        public string Alias3 = string.Empty;
        public string Alias4 = string.Empty;
        public string Alias5 = string.Empty;
        public string DatabaseName = string.Empty;
        public string ObjectName = string.Empty;

        Color AllOkay = Color.Lime;
        Color Danger = Color.Yellow;
        Color Warn = Color.DeepSkyBlue;

        public DBObjectContainer()
        {
            InitializeComponent();
        }

        private void DBObjectContainer_Load(object sender, EventArgs e)
        {
            int LastX = 10;
            int SpaceX = 10;

            if (Alias1.Trim() != "")
            {
                DBObject dbo1 = new DBObject();

                dbo1.DatabaseName = DatabaseName;
                dbo1.ObjectName = ObjectName;
                dbo1.ServerAlias = ServerAlias;
                dbo1.ControlNumber = 1;

                dbo1.ModifiedDateChanged += UpdateDateModifiedChange;
                dbo1.CreateDateChanged += UpdateDateCreatedChange;
                dbo1.AvailableForEditChange += UpdateAvailableForEditChange;

                dbo1.Parent = this;
                dbo1.Left = LastX;
                dbo1.Top = 10;
                LastX = dbo1.Left + dbo1.Width;

                if (Alias2.Trim() != "")
                {
                    DBObject dbo2 = new DBObject();

                    dbo2.DatabaseName = DatabaseName;
                    dbo2.ObjectName = ObjectName;
                    dbo2.ServerAlias = Alias2;
                    dbo2.ControlNumber = 2;

                    dbo2.ModifiedDateChanged += UpdateDateModifiedChange;
                    dbo2.CreateDateChanged += UpdateDateCreatedChange;
                    dbo2.AvailableForEditChange += UpdateAvailableForEditChange;

                    dbo2.Parent = this;
                    dbo2.Left = LastX + SpaceX;
                    dbo2.Top = 10;
                    LastX = dbo2.Left + dbo2.Width;

                    if (Alias3.Trim() != "")
                    {
                        DBObject dbo3 = new DBObject();

                        dbo3.DatabaseName = DatabaseName;
                        dbo3.ObjectName = ObjectName;
                        dbo3.ServerAlias = Alias3;
                        dbo3.ControlNumber = 3;

                        dbo3.ModifiedDateChanged += UpdateDateModifiedChange;
                        dbo3.CreateDateChanged += UpdateDateCreatedChange;
                        dbo3.AvailableForEditChange += UpdateAvailableForEditChange;

                        dbo3.Parent = this;
                        dbo3.Left = LastX + SpaceX;
                        dbo3.Top = 10;
                        LastX = dbo3.Left + dbo3.Width;

                        if (Alias4.Trim() != "")
                        {
                            DBObject dbo4 = new DBObject();

                            dbo4.DatabaseName = DatabaseName;
                            dbo4.ObjectName = ObjectName;
                            dbo4.ServerAlias = Alias4;
                            dbo4.ControlNumber = 4;

                            dbo4.ModifiedDateChanged += UpdateDateModifiedChange;
                            dbo4.CreateDateChanged += UpdateDateCreatedChange;
                            dbo4.AvailableForEditChange += UpdateAvailableForEditChange;

                            dbo4.Parent = this;
                            dbo4.Left = LastX + SpaceX;
                            dbo4.Top = 10;
                            LastX = dbo4.Left + dbo4.Width;

                            if (Alias5.Trim() != "")
                            {
                                DBObject dbo5 = new DBObject();

                                dbo5.DatabaseName = DatabaseName;
                                dbo5.ObjectName = ObjectName;
                                dbo5.ServerAlias = Alias5;
                                dbo5.ControlNumber = 5;

                                dbo5.ModifiedDateChanged += UpdateDateModifiedChange;
                                dbo5.CreateDateChanged += UpdateDateCreatedChange;
                                dbo5.AvailableForEditChange += UpdateAvailableForEditChange;

                                dbo5.Parent = this;
                                dbo5.Left = LastX + SpaceX;
                                dbo5.Top = 10;
                                LastX = dbo5.Left + dbo5.Width;
                            }
                        }
                    }
                }
            }

            this.Width = LastX + 20;

            UpdateCompareServer(Alias1, Alias2, Alias3, Alias4, Alias5);

            this.CenterToScreen();
        }

        private void UpdateDateModifiedChange(object sender, EventArgs e)
        {
            string ObjectName = ((DBObject)sender).ObjectName;
            string FirstDate = string.Empty;
            string TestDate = string.Empty;

            foreach (Control ctl in this.Controls)
            {
                if (ctl is DBObject)
                {
                    if (FirstDate.Trim() == "")
                    {
                        FirstDate = ((DBObject)(ctl)).ModifiedDate;
                        ((DBObject)(ctl)).SetDateModifiedColour(AllOkay);
                    }
                    else
                    {
                        TestDate = ((DBObject)(ctl)).ModifiedDate;

                        if (TestDate != FirstDate)
                        {
                            ((DBObject)(ctl)).SetDateModifiedColour(Warn);
                        }
                        else
                        {
                            ((DBObject)(ctl)).SetDateModifiedColour(AllOkay);
                        }
                    }
                }
            }
        }

        private void UpdateCompareServer(string Alias1, string Alias2, string Alias3, string Alias4, string Alias5)
        {
            foreach (Control ctl in this.Controls)
            {
                if (ctl is DBObject)
                {
                    ((DBObject)(ctl)).SetCompareServer(Alias1, Alias2, Alias3, Alias4, Alias5);
                }
            }
        }

        private void UpdateAvailableForEditChange(object sender, EventArgs e)
        {
            bool AllowEdit = true;

            AllowEdit = ((DBObject)sender).AvailableForEdit;

            foreach (Control ctl in this.Controls)
            {
                if (ctl is DBObject)
                {
                    ((DBObject)(ctl)).SetAvailableForEdit(AllowEdit);
                }
            }
        }

        private void UpdateDateCreatedChange(object sender, EventArgs e)
        {
            string ObjectName = ((DBObject)sender).ObjectName;
            string FirstDate = string.Empty;
            string TestDate = string.Empty;

            foreach (Control ctl in this.Controls)
            {
                if (ctl is DBObject)
                {
                    if (FirstDate.Trim() == "")
                    {
                        FirstDate = ((DBObject)(ctl)).CreateDate;
                        ((DBObject)(ctl)).SetDateCreatedColour(AllOkay);
                    }
                    else
                    {
                        TestDate = ((DBObject)(ctl)).CreateDate;

                        if (TestDate != FirstDate)
                        {
                            ((DBObject)(ctl)).SetDateCreatedColour(Warn);
                        }
                        else
                        {
                            ((DBObject)(ctl)).SetDateCreatedColour(AllOkay);
                        }
                    }
                }
            }
        }
    }
}
