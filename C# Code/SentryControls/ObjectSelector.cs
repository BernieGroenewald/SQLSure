using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SentryGeneral;

namespace SentryControls
{
    public partial class ObjectSelector : UserControl
    {
        int CheckCount = 0;
        public string ObjectsSelected = string.Empty;
        bool ResetAll = true;

        string XMLDB = string.Empty;
        string XMLPath = string.Empty;
        string XMLServerAlias = string.Empty;

        General gs = new General();

        public event EventHandler SelectionChangedObjectSelector;

        public ObjectSelector()
        {
            InitializeComponent();
        }

        private void FindSelectedObjects()
        {
            string CurBox = string.Empty;

            CheckCount = 0;
            ObjectsSelected = "";

            foreach (Control Ctl in this.Controls)
            {
                if (Ctl is CheckBox)
                {
                    if (((CheckBox)Ctl).Checked)
                    {
                        //Exclude All

                        CurBox = ((CheckBox)Ctl).Text;

                        if (CurBox != "All Objects")
                        {
                            if (ObjectsSelected.Trim() == "")
                            {
                                ObjectsSelected = CurBox;
                            }
                            else
                            {
                                ObjectsSelected = ObjectsSelected + "," + CurBox;
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
                    tSelected.Text = ObjectsSelected + " selected...";
                }
                else
                {
                    if (CheckCount == 6)
                    {
                        tSelected.Text = "All object types selected...";
                    }
                    else
                    {
                        tSelected.Text = CheckCount.ToString() + " object types selected...";
                    }
                }
            }
            else
            {
                tSelected.Text = "No object types selected...";
            }
        }

        private void SetChecks()
        {
            bool FoundUnchecked = false;
            
            foreach (Control Ctl in this.Controls)
            {
                if (Ctl is CheckBox)
                {
                    if (((CheckBox)Ctl).Checked == false)
                    {
                        if (((CheckBox)Ctl).Text != "All Objects")
                        {
                            FoundUnchecked = true;
                        }
                    }
                }
            }

            if (!FoundUnchecked)
            {
                cAllObjects.Checked = true;
            }
        }

        private void cObjectTables_CheckedChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Checked)
            {
                ResetAll = false;
                cAllObjects.Checked = false;
            }

            FindSelectedObjects();

            ResetAll = true;
        }

        private void cObjectViews_CheckedChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Checked)
            {
                ResetAll = false;
                cAllObjects.Checked = false;
            }

            FindSelectedObjects();

            ResetAll = true;
        }

        private void cObjectStoredProcedures_CheckedChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Checked)
            {
                ResetAll = false;
                cAllObjects.Checked = false;
            }

            FindSelectedObjects();

            ResetAll = true;
        }

        private void cObjectFunctions_CheckedChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Checked)
            {
                ResetAll = false;
                cAllObjects.Checked = false;
            }

            FindSelectedObjects();

            ResetAll = true;
        }

        private void cObjectConstraints_CheckedChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Checked)
            {
                ResetAll = false;
                cAllObjects.Checked = false;
            }

            FindSelectedObjects();

            ResetAll = true;
        }

        private void cObjectTriggers_CheckedChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Checked)
            {
                ResetAll = false;
                cAllObjects.Checked = false;
            }

            FindSelectedObjects();

            ResetAll = true;
        }

        private void cAllObjects_CheckedChanged(object sender, EventArgs e)
        {
            if (!ResetAll)
            {
                return;
            }

            if (((CheckBox)sender).Checked)
            {
                foreach (Control Ctl in this.Controls)
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
                foreach (Control Ctl in this.Controls)
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

            FindSelectedObjects();
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            gs.ObjectSelection = ObjectsSelected;

            this.Height = 30;

            if (SelectionChangedObjectSelector != null)
            {
                SelectionChangedObjectSelector(this, e);
            }
        }

        private void pObjects_Leave(object sender, EventArgs e)
        {
            gs.ObjectSelection = ObjectsSelected;

            this.Height = 30;
        }

        private void ObjectSelector_Leave(object sender, EventArgs e)
        {
            this.Height = 30;
        }

        private void ObjectSelector_Load(object sender, EventArgs e)
        {
            this.Height = 30;
            cAllObjects.Checked = true;

            ObjectsSelected = gs.ObjectSelection;

            if (ObjectsSelected.Trim() == "")
            {
                cAllObjects.Checked = true;
            }
            else
            {
                LoadObjectSelection();
                SetChecks();
            }
        }

        private void cmdDropDownObjects_Click(object sender, EventArgs e)
        {
            if (this.Height >= 100)
            {
                this.Height = 30;
            }
            else
            {
                this.Height = 262;
            }
        }

        private void LoadObjectSelection()
        {
            List<String> ObjectElements;
            string CurBox = string.Empty;

            ObjectElements = ObjectsSelected.Split(',').ToList();
            ObjectsSelected = "";
            CheckCount = 0;

            foreach (Control Ctl in this.Controls)
            {
                if (Ctl is CheckBox)
                {
                    ((CheckBox)Ctl).Checked = false;
                }
            }

            foreach (string obj in ObjectElements)
            {
                foreach (Control Ctl in this.Controls)
                {
                    if (Ctl is CheckBox)
                    {
                        CurBox = ((CheckBox)Ctl).Text;

                        if (CurBox != "All Objects")
                        {
                            if (CurBox == obj)
                            {
                                ((CheckBox)Ctl).Checked = true;
                                CheckCount++;
                            }
                        }
                    }
                }
            }
        }
    }
}
