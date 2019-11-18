using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SentryUpdater
{
    public partial class UpdateStatus : Form
    {
        public UpdateStatus()
        {
            InitializeComponent();
        }

        public void DoUpdateStatus(string Status)
        {
            lStatus.Text = Status;
        }
    }
}
