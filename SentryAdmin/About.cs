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

namespace SentryAdmin
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void cmdRegister_Click(object sender, EventArgs e)
        {
            Register r = new Register();
            r.ShowDialog();
            this.Close();
        }

        private void GetRegistrationDetail()
        {
            string FirstReg = string.Empty;
            string RegistrationKey = string.Empty;
            string TamperKey = string.Empty;
            string Part1 = string.Empty;
            string Part2 = string.Empty;
            string Part3 = string.Empty;
            int KeyPart1 = 0;
            int KeyPart2 = 0;
            int KeyPart3 = 0;
            int DaysNow = 0;
            DateTime dt1;
            DateTime dt2;
            DateTime Date2000;
            string CompanyName = string.Empty;
            bool IsTrialVersion = false;
            string UpdatesValidTo = string.Empty;
            int TrialDayNo = 0;
            bool TrialHasExpired = false;
            bool IsValidKey = true;
            int NumberOfUsers = 0;

            lUsers.Visible = false;

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    FirstReg = sn.GetSystemSetting("Key1");
                    RegistrationKey = sn.GetSystemSetting("Key2");
                    CompanyName = sn.GetSystemSetting("Key3");

                    if (CompanyName.Trim() == "")
                    {
                        lCompanyName.Text = "Trial Version";
                    }
                    else
                    {
                        lCompanyName.Text = "Registered to " + CompanyName.Trim();
                    }

                    TamperKey = sn.GetSystemSetting("Key4");

                    if (FirstReg.Trim() == "")
                    {
                        FirstReg = DateTime.Now.ToString("dd MMM yyyy");

                        sn.SaveSystemSetting("Key1", FirstReg);
                    }
                }
            }

            catch
            {

            }

            try
            {
                if (RegistrationKey.Trim() == "")
                {
                    //No registration yet, trial version

                    IsTrialVersion = true;
                    UpdatesValidTo = Convert.ToDateTime(FirstReg).AddDays(14).ToString("dd MMM yyyy");

                    dt1 = Convert.ToDateTime(FirstReg);
                    dt2 = Convert.ToDateTime(DateTime.Now.ToString("dd MMM yyyy"));

                    TrialDayNo = dt2.Subtract(dt1).Days;

                    if (TrialDayNo > 14)
                    {
                        TrialHasExpired = true;
                        lSupportDate.Text = "Your trial has expired.";
                    }
                    else
                    {
                        lSupportDate.Text = "Your trial is valid to " + UpdatesValidTo;
                    }

                    return;
                }
            }

            catch
            {
                TrialDayNo = 99;
                IsTrialVersion = true;
                TrialHasExpired = true;
            }

            try
            {
                {
                    if (RegistrationKey.Length == 12)
                    {
                        if (RegistrationKey != TamperKey)
                        {
                            //Invalid key

                            IsValidKey = false;
                        }
                        else
                        {
                            Part1 = RegistrationKey.Substring(0, 4);
                            Part2 = RegistrationKey.Substring(4, 4);
                            Part3 = RegistrationKey.Substring(8, 4);

                            if (int.TryParse(Part1, out KeyPart1))
                            {
                                Date2000 = Convert.ToDateTime("22 Aug 2016");
                                DaysNow = (DateTime.Now - Date2000).Days;

                                if ((KeyPart1 - 1009) >= DaysNow)
                                {
                                    UpdatesValidTo = Convert.ToDateTime(Date2000).AddDays(KeyPart1 - 1009).ToString("dd MMM yyyy");
                                }
                            }
                            else
                            {
                                UpdatesValidTo = "No information found";
                                IsValidKey = false;
                            }

                            if (int.TryParse(Part2, out KeyPart2))
                            {
                                if (KeyPart2 > 4831)
                                {
                                    NumberOfUsers = 0;
                                    IsValidKey = false;
                                }
                                else
                                {
                                    NumberOfUsers = 4831 - KeyPart2;
                                }
                            }
                            else
                            {
                                IsValidKey = false;
                            }

                            if (int.TryParse(Part3, out KeyPart3))
                            {
                                if (KeyPart3 % 22 != 0)
                                {
                                    IsValidKey = false;
                                }
                            }
                            else
                            {
                                IsValidKey = false;
                            }
                        }
                    }
                    else
                    {
                        //Invalid key

                        IsValidKey = false;
                    }
                }
            }

            catch (Exception ex)
            {
                IsValidKey = false;
            }

            if (IsValidKey)
            {
                lSupportDate.Text = "Your support is valid to " + UpdatesValidTo;
                lUsers.Visible = true;
                lUsers.Text = "Your software is registered for " + NumberOfUsers.ToString() + " users";
            }
        }

        private void About_Load(object sender, EventArgs e)
        {
            GetRegistrationDetail();
        }
    }
}
