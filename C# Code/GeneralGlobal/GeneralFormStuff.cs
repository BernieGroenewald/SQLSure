using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Collections;
using GeneralGlobal;
using System.Xml.Serialization;
using System.IO;
using System.Drawing;

namespace GeneralGlobal
{
    public class GeneralFormStuff
    {
        public string SQLUpdate = "";
        public string SQLInsertColumns = "";
        public string SQLInsertValues = "";
        public string WhereClause = "";
        public Control CtlToValidate = null;
        public string ConnectionString;

        GeneralGlobal.GeneralStuff gs = new GeneralStuff();
        
        public GeneralFormStuff()
        {
        }

        public string SetComboInitValue(ComboBox cb)
        {
            if (cb.SelectedItem != null)
            {
                return ((LookupValues)cb.SelectedItem).LongName;
            }
            else
            {
                return "";
            }
        }

        public void MoveTo(ComboBox cb, int ListValue)
        {
            try
            {
                cb.SelectedIndex = -1;
                cb.SelectedValue = ListValue;
            }

            catch
            {
            }
        }

        public void MoveTo(ComboBox cb, string ListValue)
        {
            try
            {
                cb.SelectedIndex = -1;
                cb.SelectedValue = ListValue;
            }

            catch
            {
            }
        }

        public void MoveToDescription(ComboBox cb, string ListValue)
        {
            cb.SelectedIndex = -1;
            cb.Text = ListValue;
        }

        public ComboBox LoadLookup(ComboBox cb, string LookupType, string OrderBy)
        {
            using (GeneralGlobal.Lookup LU = new Lookup())
            {
                LU.ConnectionString = ConnectionString;

                ArrayList TmpData = new ArrayList();

                DataTable lu = LU.GetLookup(LookupType, OrderBy);

                foreach (DataRow row in lu.Rows)
                {
                    TmpData.Add(new LookupValues(row["ChildDesc"].ToString(), row["ChildId"].ToString()));
                    Application.DoEvents();
                }

                if (lu.Rows.Count > 0)
                {
                    cb.DisplayMember = "LongName";
                    cb.ValueMember = "ShortName";
                    cb.DataSource = TmpData;
                }

                return cb;
            }
        }

        public Label SetWarningLabel(Label l)
        {
            l.ForeColor = Color.Red;

            return l;
        }

        public CheckBox SetWarningLabel(CheckBox c)
        {
            c.ForeColor = Color.Red;

            return c;
        }

        public RadioButton SetWarningLabel(RadioButton r)
        {
            r.ForeColor = Color.Red;

            return r;
        }

        public void SetWarningLabel(Control.ControlCollection ctls, string LabelName)
        {
            foreach (Control ctl in ctls)
            {
                if (ctl.HasChildren)
                {
                    SetWarningLabel(ctl.Controls, LabelName);
                }
                else
                {
                    if (ctl is Label)
                    {
                        if (ctl.Name.ToString() == LabelName)
                        {
                            ctl.ForeColor = Color.Red;
                            return;
                        }
                    }
                }
            }
        }

        public void ResetWarningLabels(Control.ControlCollection ctls)
        {
            foreach (Control ctl in ctls)
            {
                if (ctl.HasChildren)
                {
                    ResetWarningLabels(ctl.Controls);
                }
                else
                {
                    if ((ctl is Label) || (ctl is RadioButton) || (ctl is CheckBox))
                    {
                        ctl.ForeColor = Color.Black;
                    }
                }
            }
        }

        public void ClearAll(Control.ControlCollection ctls)
        {
            try
            {
                foreach (Control ctl in ctls)
                {
                    //if (ctl.Name == "tvStock")
                    //{
                    //    MessageBox.Show("");
                    //}

                    if (ctl != null)
                    {
                        ctl.Tag = null;

                        if (ctl.HasChildren)
                        {
                            ClearAll(ctl.Controls);
                        }
                        else
                        {
                            if (ctl is TextBox)
                            {
                                ctl.Text = "";
                            }

                            if (ctl is ComboBox)
                            {
                                ctl.Text = "";
                            }
                        }
                    }
                }
            }

            catch
            {
            }
        }

        public void DisableAll(Control.ControlCollection ctls)
        {
            foreach (Control ctl in ctls)
            {
                if (ctl.HasChildren)
                {
                    DisableAll(ctl.Controls);
                }
                else
                {
                    ctl.Enabled = false;
                }
            }
        }

        public void EnableAll(Control.ControlCollection ctls)
        {
            foreach (Control ctl in ctls)
            {
                if (ctl.HasChildren)
                {
                    EnableAll(ctl.Controls);
                }
                else
                {
                    ctl.Enabled = true;
                }
            }
        }

        public void ctl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift || e.Alt || e.Control)
            {
                // Swallow this invalid key
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
            {
                // Digits are OK
            }
            else if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
            {
                // Digits are OK
            }
            else if (e.KeyCode == Keys.OemPeriod)
            {
                // Period key is OK
            }
            else if (e.KeyCode == Keys.Back)
            {
                // Backspace key is OK
            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                // Arrow keys 
            }
            else
            {
                // Swallow this invalid key
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        public bool CheckBoxChanged(CheckBox cb, string InitVal)
        {
            try
            {
                if (cb.Checked == true)
                {
                    if (InitVal == "N")
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
                    if (InitVal == "Y")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RadioButtonChanged(RadioButton cb, string InitVal)
        {
            if (cb.Checked == true)
            {
                if (InitVal == "N")
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
                if (InitVal == "Y")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public ArrayList LoadLookup(string LookupType, string OrderBy)
        {
            using (GeneralGlobal.Lookup LU = new Lookup())
            {
                ArrayList TmpData = new ArrayList();

                DataTable lu = LU.GetLookup(LookupType, OrderBy);

                foreach (DataRow row in lu.Rows)
                {
                    TmpData.Add(row["ChildDesc"].ToString());
                    Application.DoEvents();
                }

                return TmpData;
            }
        }

        public ComboBox LoadCombo(ComboBox cb, string SQLString)
        {
            using (GeneralGlobal.GeneralStuff LU = new GeneralStuff())
            {
                ArrayList TmpData = new ArrayList();

                DataTable lu = LU.GetComboData(SQLString);

                foreach (DataRow row in lu.Rows)
                {
                    TmpData.Add(new LookupValues(row[0].ToString(), row[1].ToString()));
                    Application.DoEvents();
                }

                if (lu.Rows.Count > 0)
                {
                    cb.DataSource = TmpData;
                    cb.DisplayMember = "LongName";
                    cb.ValueMember = "ShortName";
                }

                return cb;
            }
        }

        public ComboBox LoadSuppliers(ComboBox cb)
        {
            using (GeneralGlobal.Supplier s = new Supplier())
            {
                ArrayList TmpData = new ArrayList();

                DataTable sl = s.GetAllSuppliers();

                foreach (DataRow row in sl.Rows)
                {
                    TmpData.Add(new LookupValues(row[0].ToString(), row[1].ToString()));
                    Application.DoEvents();
                }

                if (sl.Rows.Count > 0)
                {
                    cb.DataSource = TmpData;
                    cb.DisplayMember = "LongName";
                    cb.ValueMember = "ShortName";
                }

                return cb;
            }
        }

        public ComboBox LoadComboFixedValue(ComboBox cb)
        {
            using (GeneralGlobal.GeneralStuff LU = new GeneralStuff())
            {
                LU.ConnectionString = ConnectionString;

                ArrayList TmpData = new ArrayList();

                if (cb.Name == "cbSendItemToPrinter")
                {
                    TmpData.Add(new LookupValues("Bar", "B"));
                    TmpData.Add(new LookupValues("Display", "D"));
                    TmpData.Add(new LookupValues("Kitchen", "K"));
                    TmpData.Add(new LookupValues("None", "N"));

                    cb.DataSource = TmpData;
                    cb.DisplayMember = "LongName";
                    cb.ValueMember = "ShortName";
                }

                if (cb.Name == "cbPersonSex")
                {
                    TmpData.Add(new LookupValues("Female", "F"));
                    TmpData.Add(new LookupValues("Male", "M"));
                    
                    cb.DataSource = TmpData;
                    cb.DisplayMember = "LongName";
                    cb.ValueMember = "ShortName";
                }

                return cb;
            }
        }

        private string CheckboxValue(CheckBox cb)
        {
            return cb.Checked ? "Y" : "N";
        }

        private string RadioButtonValue(RadioButton cb)
        {
            return ((SQLTag)cb.Tag).CtlValue;
        }

        public string ComboValueMember(ComboBox cb)
        {
            string TmpId = "";

            if (cb.Tag == null)
            {
                return "";
            }

            try
            {
                if (((SQLTag)cb.Tag).UseValueDisplayMember == "Y")
                {
                    if (cb.SelectedValue != null)
                    {
                        TmpId = cb.SelectedValue.ToString();
                    }
                    else
                    {
                        TmpId = "0";
                    }
                }
                else
                {
                    TmpId = cb.Text;
                }

                if (TmpId == "")
                {
                    TmpId = "0";
                }

                return TmpId;
            }

            catch
            {
                return "";
            }
        }

        public void SetToolTips(string SubSystem, Control.ControlCollection ctls, ToolTip tt1)
        {
            string TempToolTip = "";

            foreach (Control ctl in ctls)
            {
                if (ctl.HasChildren)
                {
                    SetToolTips(SubSystem, ctl.Controls, tt1);
                }

                using (GeneralGlobal.SystemField sf = new SystemField())
                {
                    DataTable tt = sf.GetToolTips(SubSystem, ctl.Name.ToString());

                    foreach (DataRow row in tt.Rows)
                    {
                        TempToolTip = gs.CheckNull(row["ToolTipText"].ToString());

                        if (TempToolTip.Trim() != "")
                        {
                            tt1.SetToolTip(ctl, TempToolTip);
                        }
                    }
                }
            }
        }

        //public void UnSetToolTips(Control.ControlCollection ctls, ToolTip tt1)
        //{
        //    string TempToolTip = "";

        //    foreach (Control ctl in ctls)
        //    {
        //        if (ctl.HasChildren)
        //        {
        //            SetToolTips(ctl.Controls, tt1);
        //        }

        //        using (GeneralGlobal.SystemField sf = new SystemField())
        //        {
        //            DataTable tt = sf.GetToolTips(SubSystem, ctl.Name.ToString());

        //            foreach (DataRow row in tt.Rows)
        //            {
        //                TempToolTip = gs.CheckNull(row["ToolTipText"].ToString());

        //                if (TempToolTip.Trim() != "")
        //                {
        //                    tt1.SetToolTip(ctl, TempToolTip);
        //                }
        //            }
        //        }
        //    }
        //}

        public void SetTagTextBox(TextBox ControlName)
        {
            using (GeneralGlobal.SystemField sf = new SystemField())
            {
                DataTable lu = sf.GetSystemField(ControlName.Name.ToString());

                foreach (DataRow row in lu.Rows)
                {
                    ControlName.Tag = new SQLTag(row["FieldType"].ToString(), 
                                                 row["Mandatory"].ToString(),
                                                 row["HumanName"].ToString(),
                                                 row["StartValue"].ToString(),
                                                 row["TabName"].ToString(),
                                                 row["LabelName"].ToString(), 
                                                 ControlName.Text,
                                                 "");
                }
            }
        }

        public void SetTagCombobox(ComboBox ControlName)
        {
            using (GeneralGlobal.SystemField sf = new SystemField())
            {
                DataTable lu = sf.GetSystemField(ControlName.Name.ToString());

                foreach (DataRow row in lu.Rows)
                {
                    ControlName.Tag = new SQLTag(row["FieldType"].ToString(),
                                                 row["Mandatory"].ToString(),
                                                 row["HumanName"].ToString(),
                                                 row["StartValue"].ToString(),
                                                 row["TabName"].ToString(),
                                                 row["LabelName"].ToString(),
                                                 SetComboInitValue(ControlName),
                                                 row["UseValueDisplayMember"].ToString());
                }
            }
        }

        public void SetTagCheckbox(CheckBox ControlName)
        {
            using (GeneralGlobal.SystemField sf = new SystemField())
            {
                DataTable lu = sf.GetSystemField(ControlName.Name.ToString());

                foreach (DataRow row in lu.Rows)
                {
                    ControlName.Tag = new SQLTag(row["FieldType"].ToString(),
                                                 row["Mandatory"].ToString(),
                                                 row["HumanName"].ToString(),
                                                 row["StartValue"].ToString(),
                                                 row["TabName"].ToString(),
                                                 row["LabelName"].ToString(),
                                                 ControlName.Checked ? "Y" : "N",
                                                 row["UseValueDisplayMember"].ToString());
                }
            }
        }

        public void SetTagRadiobutton(RadioButton ControlName)
        {
            using (GeneralGlobal.SystemField sf = new SystemField())
            {
                DataTable lu = sf.GetSystemField(ControlName.Name.ToString());

                foreach (DataRow row in lu.Rows)
                {
                    ControlName.Tag = new SQLTag(row["FieldType"].ToString(),
                                                 row["Mandatory"].ToString(),
                                                 row["HumanName"].ToString(),
                                                 row["StartValue"].ToString(),
                                                 row["TabName"].ToString(),
                                                 row["LabelName"].ToString(),
                                                 ControlName.Checked ? "Y" : "N",
                                                 row["UseValueDisplayMember"].ToString());
                }
            }
        }

        public void SetTag(TextBox t, string DBName, string Mandatory, string HumanName, string StartValue, string TabName, string LabelName, string CtlValue, string UseValueDisplayMember)
        {
            t.Tag = new SQLTag(DBName, Mandatory, HumanName, StartValue, TabName, LabelName, CtlValue, UseValueDisplayMember);
        }

        public Control Validate(Control.ControlCollection ctls)
        {
            if (CtlToValidate != null)
            {
                return CtlToValidate;
            }

            try
            {
                foreach (Control ctl in ctls.Cast<Control>().OrderBy(c => c.TabIndex))
                {
                    //if (ctl.Name == "cbStockCategory")
                    //{
                    //    MessageBox.Show(ctl.Name);
                    //}

                    if (ctl.HasChildren)
                    {
                        Validate(ctl.Controls);
                    }
                     
                    if (ctl.Tag != null)
                    {    
                        //Datatype validation

                        if (((SQLTag)ctl.Tag).DBType.ToString() == "Number")
                        {
                            // MessageBox.Show("Number: " + ctl.Name);

                            if (ctl.Text != "")
                            {
                                if (!ctl.GetType().ToString().Contains("ComboBox"))
                                {
                                    if (!gs.IsNumeric(ctl.Text))
                                    {
                                        return CtlToValidate = ctl;
                                    }
                                }
                            }
                            else
                            {
                                ctl.Text = "0";
                            }
                        }

                        if (((SQLTag)ctl.Tag).DBType.ToString() == "NumberNotZero")
                        {
                            // MessageBox.Show("Number: " + ctl.Name);

                            if (ctl.Text != "")
                            {
                                if (!ctl.GetType().ToString().Contains("ComboBox"))
                                {
                                    if (!gs.IsNotZero(ctl.Text))
                                    {
                                        return CtlToValidate = ctl;
                                    }
                                }
                            }
                            else
                            {
                                return CtlToValidate = ctl;
                            }
                        }

                        if (((SQLTag)ctl.Tag).DBType.ToString() == "Text")
                        {
                            //MessageBox.Show("Text: " + ctl.Name);

                            if (ctl is TextBox)
                            {
                                if (ctl.Text != "")
                                {
                                    ctl.Text = gs.SafeString(ctl.Text);
                                }
                            }
                        }

                        //Mandatory field validation

                        if (((SQLTag)ctl.Tag).Mandatory == "Y")
                        {
                            //MessageBox.Show("Mandatory: " + ctl.Name);

                            if (ctl.Text == "")
                            {
                                return CtlToValidate = ctl;
                            }
                        }
                    }
                }
            }

            catch
            {
            }

            return CtlToValidate;
        }

        public void ValidateAll(Control.ControlCollection ctls)
        {
            try
            {
                foreach (Control ctl in ctls.Cast<Control>().OrderBy(c => c.TabIndex))
                {
                    if (ctl.HasChildren)
                    {
                        ValidateAll(ctl.Controls);
                    }

                    if (ctl.Tag != null)
                    {
                        //Datatype validation

                        if (((SQLTag)ctl.Tag).DBType.ToString() == "Number")
                        {
                            if (ctl.Text != "")
                            {
                                if (!ctl.GetType().ToString().Contains("ComboBox"))
                                {
                                    if (!gs.IsNumeric(ctl.Text))
                                    {
                                        SetWarningLabel(ctls, ((SQLTag)ctl.Tag).LabelName);
                                    }
                                }
                            }
                            else
                            {
                                ctl.Text = "0";
                            }
                        }

                        if (((SQLTag)ctl.Tag).DBType.ToString() == "NumberNotZero")
                        {
                            if (ctl.Text != "")
                            {
                                if (!ctl.GetType().ToString().Contains("ComboBox"))
                                {
                                    if (!gs.IsNotZero(ctl.Text))
                                    {
                                        SetWarningLabel(ctls, ((SQLTag)ctl.Tag).LabelName);
                                    }
                                }
                            }
                            else
                            {
                                SetWarningLabel(ctls, ((SQLTag)ctl.Tag).LabelName);
                            }
                        }

                        //Mandatory field validation

                        if (((SQLTag)ctl.Tag).Mandatory == "Y")
                        {
                            //MessageBox.Show("Mandatory: " + ctl.Name);

                            if (ctl.Text == "")
                            {
                                SetWarningLabel(ctls, ((SQLTag)ctl.Tag).LabelName);
                            }
                        }
                    }
                }
            }

            catch
            {
            }
        }

        public void SetSystemFields(Control.ControlCollection ctls)
        {
            try
            {
                foreach (Control ctl in ctls)
                {
                    if (ctl.HasChildren)
                    {
                        SetSystemFields(ctl.Controls);
                    }

                    if (ctl is TextBox)
                    {
                        SetTagTextBox((TextBox)ctl);
                    }

                    if (ctl is ComboBox)
                    {
                        SetTagCombobox((ComboBox)ctl);
                    }

                    if (ctl is CheckBox)
                    {
                        SetTagCheckbox((CheckBox)ctl);
                    }

                    if (ctl is RadioButton)
                    {
                        SetTagRadiobutton((RadioButton)ctl);
                    }
                }
            }

            catch
            {
            }
        }

        public string CheckChange(Control.ControlCollection ctls)
        {
            try
            {
                foreach (Control ctl in ctls)
                {
                    if (ctl.HasChildren)
                    {
                        CheckChange(ctl.Controls);
                    }
                    else
                    {
                        if (ctl.Tag != null)
                        {
                            if (ctl is TextBox)
                            {
                                if (((SQLTag)ctl.Tag).StartValue != ctl.Text)
                                {
                                    return ctl.Name.ToString() + " : " + ((SQLTag)ctl.Tag).StartValue + " : " + ctl.Text;
                                }
                            }

                            if (ctl is ComboBox)
                            {
                                if (((SQLTag)ctl.Tag).StartValue != ctl.Text)
                                {
                                    return ctl.Name.ToString() + " : " + ((SQLTag)ctl.Tag).StartValue + " : " + ctl.Text;
                                }
                            }

                            if (ctl is RadioButton)
                            {
                                if (RadioButtonChanged((RadioButton)ctl, ((SQLTag)ctl.Tag).StartValue))
                                {
                                    return ctl.Name.ToString() + " : " + ((SQLTag)ctl.Tag).StartValue;
                                }
                            }

                            if (ctl is CheckBox)
                            {
                                if (CheckBoxChanged((CheckBox)ctl, ((SQLTag)ctl.Tag).StartValue))
                                {
                                    return ctl.Name.ToString() + " : " + ((SQLTag)ctl.Tag).StartValue;
                                }
                            }

                            if (ctl is DateTimePicker)
                            {
                                if (((SQLTag)ctl.Tag).StartValue != ctl.Text)
                                {
                                    return ctl.Name.ToString() + " : " + ((SQLTag)ctl.Tag).StartValue + " : " + ctl.Text;
                                }
                            }
                        }
                    }
                }
            }

            catch
            {
            }

            return ""; 
        }

        public AppSettings GetXMLSettings()
        {
            AppSettings AS;

            string path = Path.GetDirectoryName(Application.ExecutablePath);

            XmlSerializer mySerializer = new XmlSerializer(typeof(AppSettings));

            using (FileStream myFileStream = new FileStream(path + "\\QSSettings.xml", FileMode.Open))
            {
                AS = (AppSettings)mySerializer.Deserialize(myFileStream);
                return AS;
            }
        }

        public void LoadSettings()
        {
            AppSettings AS;

            string path = Path.GetDirectoryName(Application.ExecutablePath);

            XmlSerializer mySerializer = new XmlSerializer(typeof(AppSettings));

            try
            {
                using (FileStream myFileStream = new FileStream(path + "\\QSSettings.xml", FileMode.Open))
                {
                    AS = (AppSettings)mySerializer.Deserialize(myFileStream);

                    Globals.ServerPath = AS.AccessPath;
                    Globals.UserName = AS.UserName;
                    Globals.FocusColour = AS.FocusColour;
                    Globals.ToolTipTextStockMaintenance = AS.ToolTipTextStockMaintenance;
                }
            }

            catch
            {
                MessageBox.Show("Error reading the system's initial settings.", "System Settings", MessageBoxButtons.OK);
                return;
            }
        }

        public void SaveSettings()
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath);

            AppSettings AS = new AppSettings();

            AS.UserName = Globals.UserName;
            AS.FocusColour = "LightBlue"; //Globals.FocusColour
            AS.AccessPath = Globals.ServerPath;
            AS.ToolTipTextStockMaintenance = Globals.ToolTipTextStockMaintenance;
            
            XmlSerializer mySerializer = new XmlSerializer(typeof(AppSettings));

            try
            {
                using (StreamWriter myWriter = new StreamWriter(path + "\\QSSettings.xml"))
                {
                    mySerializer.Serialize(myWriter, AS);
                    myWriter.Close();
                }
            }
            catch
            {
                MessageBox.Show("Error writing the system's initial settings.", "System Settings", MessageBoxButtons.OK);
                return;
            }
        }
    }

    public class SQLTag
    {
        private string _DBType;
        private string _Mandatory;
        private string _HumanName;
        private string _StartValue;
        private string _TabName;
        private string _CtlValue;
        private string _UseValueDisplayMember;
        private string _LabelName;

        public SQLTag(string DBType, string Mandatory, string HumanName, string StartValue, string TabName, string LabelName, string CtlValue, string UseValueDisplayMember)
        {

            this._DBType = DBType;
            this._Mandatory = Mandatory;
            this._HumanName = HumanName;
            this._StartValue = StartValue;
            this._TabName = TabName;
            this._LabelName = LabelName;
            this._CtlValue = CtlValue;
            this._UseValueDisplayMember = UseValueDisplayMember;
        }

        //public SQLTag(string DBType, string Mandatory, string HumanName, string StartValue, string TabName, string CtlValue)
        //{

        //    this._DBType = DBType;
        //    this._Mandatory = Mandatory;
        //    this._HumanName = HumanName;
        //    this._StartValue = StartValue;
        //    this._TabName = TabName;
        //    this._CtlValue = CtlValue;
        //}

        //public SQLTag(string DBType, string Mandatory, string HumanName, string StartValue, string TabName, bool UseValueDisplayMember)
        //{

        //    this._DBType = DBType;
        //    this._Mandatory = Mandatory;
        //    this._HumanName = HumanName;
        //    this._StartValue = StartValue;
        //    this._TabName = TabName;
        //    this._CtlValue = CtlValue;
        //    this._UseValueDisplayMember = UseValueDisplayMember;
        //}

        public string DBType
        {
            get
            {
                return _DBType;
            }
        }

        public string Mandatory
        {
            get
            {
                return _Mandatory;
            }
        }

        public string HumanName
        {
            get
            {
                return _HumanName;
            }
        }

        public string StartValue
        {
            get
            {
                return _StartValue;
            }
        }

        public string TabName
        {
            get
            {
                return _TabName;
            }
        }

        public string CtlValue
        {
            get
            {
                return _CtlValue;
            }

            set
            {
                _CtlValue = value;
            }
        }

        public string UseValueDisplayMember
        {
            get
            {
                return _UseValueDisplayMember;
            }

            set
            {
                _UseValueDisplayMember = value;
            }
        }

        public string LabelName
        {
            get
            {
                return _LabelName;
            }

            set
            {
                _LabelName = value;
            }
        }
    }

    public class LookupValues
    {
        private string myShortName;
        private string myLongName;

        public LookupValues(string strLongName, string strShortName)
        {

            this.myShortName = strShortName;
            this.myLongName = strLongName;
        }

        public string ShortName
        {
            get
            {
                return myShortName;
            }
        }

        public string LongName
        {

            get
            {
                return myLongName;
            }
        }

    }
}

