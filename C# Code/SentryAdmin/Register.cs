using System;
using System.Windows.Forms;
using SentryDataStuff;

namespace SentryAdmin
{
    public partial class Register : Form
    {
        bool IsValidKey = true;

        public Register()
        {
            InitializeComponent();
        }

       

        private void Register_Load(object sender, EventArgs e)
        {
            cmdRegister.Enabled = false;
            tReg1.Focus();
        }

        private void tReg1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(tReg1.Text, "[^0-9]"))
                {
                    MessageBox.Show("Please enter a numeric value.");
                    tReg1.Text = tReg1.Text.Remove(tReg1.Text.Length - 1);
                }

                if ((tReg1.Text.Length == 4) && (tReg2.Text.Length == 4) && (tReg3.Text.Length == 4) && (tCompanyName.Text.Trim() != ""))
                {
                    cmdRegister.Enabled = true;
                }
                else
                {
                    cmdRegister.Enabled = false;
                }

                if (tReg1.Text.Length == 4)
                {
                    tReg2.Focus();
                }
            }

            catch
            {

            }
        }

        private void tReg2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(tReg2.Text, "[^0-9]"))
                {
                    MessageBox.Show("Please enter a numeric value.");
                    tReg2.Text = tReg2.Text.Remove(tReg2.Text.Length - 1);
                }

                if ((tReg1.Text.Length == 4) && (tReg2.Text.Length == 4) && (tReg3.Text.Length == 4) && (tCompanyName.Text.Trim() != ""))
                {
                    cmdRegister.Enabled = true;
                }
                else
                {
                    cmdRegister.Enabled = false;
                }

                if (tReg2.Text.Length == 4)
                {
                    tReg3.Focus();
                }
            }

            catch
            {

            }
        }

        private void tReg3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(tReg3.Text, "[^0-9]"))
                {
                    MessageBox.Show("Please enter a numeric value.");
                    tReg3.Text = tReg3.Text.Remove(tReg3.Text.Length - 1);
                }

                if ((tReg1.Text.Length == 4) && (tReg2.Text.Length == 4) && (tReg3.Text.Length == 4) && (tCompanyName.Text.Trim() != ""))
                {
                    cmdRegister.Enabled = true;
                }
                else
                {
                    cmdRegister.Enabled = false;
                }
            }

            catch
            {

            }
        }

        private void cmdRegister_Click(object sender, EventArgs e)
        {
            string FirstReg = string.Empty;
            string Part1 = string.Empty;
            string Part2 = string.Empty;
            string Part3 = string.Empty;
            int KeyPart1 = 0;
            int KeyPart2 = 0;
            int KeyPart3 = 0;
            int DaysNow = 0;
            DateTime Date2000;
            int NumberOfUsers = 0;

            if ((tReg1.Text.Length != 4) || (tReg2.Text.Length != 4) || (tReg3.Text.Length != 4) || (tCompanyName.Text.Trim() == ""))
            {
                if (tCompanyName.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter the name of your organization.", "Register", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                MessageBox.Show("The registration code is invalid, please enter a valid code.", "Register", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            IsValidKey = true;

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    FirstReg = sn.GetSystemSetting("Key1");
                    
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
                Part1 = tReg1.Text;
                Part2 = tReg2.Text;
                Part3 = tReg3.Text;

                if (int.TryParse(Part1, out KeyPart1))
                {
                    Date2000 = Convert.ToDateTime("22 Aug 2016");
                    DaysNow = (DateTime.Now - Date2000).Days;

                    if (!((KeyPart1 - 1009) >= DaysNow))
                    {
                        IsValidKey = false;
                    }
                }
                else
                {
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

            catch (Exception ex)
            {
                IsValidKey = false;
            }

            if (IsValidKey)
            {
                try
                {
                    using (DataStuff sn = new DataStuff())
                    {
                        sn.SaveSystemSetting("Key2", tReg1.Text.Trim() + tReg2.Text.Trim() + tReg3.Text.Trim());
                        sn.SaveSystemSetting("Key3", tCompanyName.Text);
                        sn.SaveSystemSetting("Key4", tReg1.Text.Trim() + tReg2.Text.Trim() + tReg3.Text.Trim());
                    }

                    MessageBox.Show("Thank you for registering your version of SQLSure.", "Register", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }

                catch
                {

                }
            }
            else
            {
                MessageBox.Show("The registration code is invalid, please enter a valid code.", "Register", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void tCompanyName_TextChanged(object sender, EventArgs e)
        {
            if ((tReg1.Text.Length == 4) && (tReg2.Text.Length == 4) && (tReg3.Text.Length == 4) && (tCompanyName.Text.Trim() != ""))
            {
                cmdRegister.Enabled = true;
            }
            else
            {
                cmdRegister.Enabled = false;
            }
        }
    }
}
