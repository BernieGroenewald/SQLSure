﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SentryDataStuff;

namespace SentryObjectViewer
{
    public partial class ObjectViewer : Form
    {
        public string ObjectDescription = string.Empty;
        public string ObjectText = string.Empty;
        public string ConnectionString = string.Empty;
        public string DatabaseName = string.Empty;
        public string ObjectName = string.Empty;
        public string AliasName = string.Empty;

        public ObjectViewer()
        {
            InitializeComponent();
        }

        private void DBObjectViewer_Load(object sender, EventArgs e)
        {
            lObjectDescription.Text = AliasName + " - " + DatabaseName + " - " + ObjectDescription;

            if (ObjectText.Trim() == "")
            {
                LoadHelpText();
            }
            else
            {
                tObjectText.Text = ObjectText;
            }

            tObjectText.SelectionLength = 0;
        }

        private void LoadHelpText()
        {
            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetObjectHelpText(ConnectionString, DatabaseName, ObjectName);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            ObjectText = ObjectText + row[0].ToString();
                        }
                    }

                    tObjectText.Text = ObjectText;
                }
            }

            catch
            {

            }
        }

        private void DBObjectViewer_Resize(object sender, EventArgs e)
        {
            tObjectText.Width = this.Width - (2 * tObjectText.Left);
            tObjectText.Height = this.Height - (2 * tObjectText.Top);
            tObjectText.Refresh();
        }

        private void DBObjectViewer_ResizeEnd(object sender, EventArgs e)
        {
            tObjectText.Width = this.Width - (2 * tObjectText.Left);
            tObjectText.Height = this.Height - (2 * tObjectText.Top);
            tObjectText.Refresh();
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
