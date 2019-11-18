using System;
using System.Data;
using System.Windows.Forms;
using SentryDataStuff;
using System.Net.Mail;
using System.Net.Mime;

namespace SentryProject
{
    public partial class ProjectApproval : Form
    {
        public string ConnectionString = string.Empty;
        public int UserID = 0;

        string ServerName = string.Empty;
        string ProjectName = string.Empty;
        string SMTPPath = string.Empty;
        bool RequireTesterApproval = false;
        bool RequireApproverApproval = false;
        bool UserIsTester = false;
        bool UserIsApprover = false;
        string RequesterName = string.Empty;
        string RequesterEmailAddress = string.Empty;
        string ApproverName = string.Empty;
        string ApproverEmailAddress = string.Empty;

        public ProjectApproval()
        {
            InitializeComponent();
        }

        private void GetSettings()
        {
            string SettingValue = string.Empty;

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    SettingValue = sn.GetSystemSetting("Key5");

                    SettingValue = sn.GetSystemSetting("Key8");

                    if (SettingValue.Trim() == "true")
                    {
                        RequireTesterApproval = true;
                    }
                    else
                    {
                        RequireTesterApproval = false;
                    }

                    SettingValue = sn.GetSystemSetting("Key9");

                    if (SettingValue.Trim() == "true")
                    {
                        RequireApproverApproval = true;
                    }
                    else
                    {
                        RequireApproverApproval = false;
                    }

                    SMTPPath = sn.GetSystemSetting("Key10");
                }
            }

            catch
            {

            }
        }

        private void ProjectApproval_Load(object sender, EventArgs e)
        {
            project2.ConnReleaseFromServer = ConnectionString;
            project2.ProjectLoaded += Project2_ProjectLoaded;

            gbApproval.Enabled = false;

            GetSettings();

            SetUserFunction();
        }

        private void Project2_ProjectLoaded(ProjectEventArgs e)
        {
            ServerName = e.ServerName;
            ProjectName = e.ProjectName;

            gbApproval.Enabled = true;

            GetRequestStatus();

            GetApprovalStatus();
        }

        private void GetApprovalStatus()
        {
            cmdTestingApprove.Enabled = false;
            cmdFinalApprove.Enabled = false;

            tTestingApprovedBy.Text = "Not Approved";
            tTestingApproveDate.Text = "";
            tFinalApprovedBy.Text = "Not Approved";
            tFinalApproveDate.Text = "";
            //tRequestBy.Text = "Not Requested";
            //tRequestDate.Text = "";

            if (tRequestBy.Text == "Not Requested")
            {
                cmdFinalApprove.Enabled = false;
                cmdTestingApprove.Enabled = false;
                return;
            }

            try
            {
                cmdTestingApprove.Enabled = false;
                cmdFinalApprove.Enabled = false;

                if (UserIsApprover)
                {
                    cmdFinalApprove.Enabled = true;
                }

                if (UserIsTester)
                {
                    cmdTestingApprove.Enabled = true;
                }

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.ProjectGetApprovalStatusRelease(ServerName, ProjectName);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["FunctionName"].ToString() == "Approve Testing")
                            {
                                tTestingApprovedBy.Text = row["UserName"].ToString();
                                tTestingApproveDate.Text = row["ApproveDate"].ToString();
                                cmdTestingApprove.Enabled = false;
                            }

                            if (row["FunctionName"].ToString() == "Approve Final")
                            {
                                tFinalApprovedBy.Text = row["UserName"].ToString();
                                tFinalApproveDate.Text = row["ApproveDate"].ToString();
                                cmdFinalApprove.Enabled = false;
                            }
                        }
                    }
                }
            }

            catch
            {

            }
        }

        private void GetRequestStatus()
        {
            cmdRequestApproval.Enabled = false;

            tRequestBy.Text = "Not Requested";
            tRequestDate.Text = "";

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.ProjectGetRequestStatus(ServerName, ProjectName);

                    if (dt.Rows.Count > 0)
                    {
                        tRequestBy.Text = dt.Rows[0]["UserName"].ToString();
                        tRequestDate.Text = dt.Rows[0]["RequestDate"].ToString();
                    }
                    else
                    {
                        cmdRequestApproval.Enabled = true;
                    }
                }
            }
            catch
            {

            }
        }

        private void cmdRequestApproval_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("This will send an email to all approvers to approve the project. Continue?", "Approval Request", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (SendEmail())
                {
                    //Insert request record

                    using (DataStuff sn = new DataStuff())
                    {
                        sn.ProjectSaveApprovalRequest(ServerName, ProjectName, UserID);
                    }

                    GetRequestStatus();
                }
            }
        }

        private void SetUserFunction()
        {
            try
            {
                DataTable dtEmail;

                using (DataStuff sn = new DataStuff())
                {
                    dtEmail = sn.ProjectGetApproverEmails();
                    
                    if (dtEmail != null)
                    {
                        if (dtEmail.Rows.Count > 0)
                        {
                            foreach (DataRow row in dtEmail.Rows)
                            {
                                if (row["QTNumber"].ToString().ToUpper() == Environment.UserName.ToUpper())
                                {
                                    if (RequireTesterApproval)
                                    {
                                        if (row["FunctionName"].ToString() == "Approve Testing")
                                        {
                                            UserIsTester = true;
                                        }
                                    }

                                    if (RequireApproverApproval)
                                    {
                                        if (row["FunctionName"].ToString() == "Approve Final")
                                        {
                                            UserIsApprover = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            catch
            {

            }

        }
        private bool SendEmail()
        {
            try
            {
                string HTMLBody = string.Empty;
                string Subject = string.Empty;
                string TextBody = string.Empty;
                string IsHTML = string.Empty;
                string RecipientAddress = string.Empty;
                string RemoteFilePath = string.Empty;
                string PickupDirectoryLocation = string.Empty;
                string CurrentUser = string.Empty;
                
                string ToEmailAddress = string.Empty;
                string ToName = string.Empty;

                string ApproverEmailAddress = string.Empty;
                string CurrentUserEmailAddress = string.Empty;
                string CurrentUserName = string.Empty;

                MailMessage mail = new MailMessage();

                Subject = "SQL Project Approval Request";
                HTMLBody = "";
                TextBody = "";
                int EmailCount = 0;

                try
                {
                    DataTable dtEmail;

                    using (DataStuff sn = new DataStuff())
                    {
                        dtEmail = sn.ProjectGetApproverEmails();
                        
                        if (dtEmail != null)
                        {
                            if (dtEmail.Rows.Count > 0)
                            {
                                foreach (DataRow row in dtEmail.Rows)
                                {
                                    if (row["QTNumber"].ToString().ToUpper() == Environment.UserName.ToUpper())
                                    {
                                        CurrentUserName = row["UserName"].ToString();
                                        CurrentUserEmailAddress = row["EmailAddress"].ToString();
                                        break;
                                    }
                                }
                            }
                        }

                        if (dtEmail != null)
                        {
                            if (dtEmail.Rows.Count > 0)
                            {
                                foreach (DataRow row in dtEmail.Rows)
                                {
                                    if (RequireTesterApproval)
                                    {
                                        if (row["FunctionName"].ToString() == "Approve Testing")
                                        {
                                            ToEmailAddress = row["EmailAddress"].ToString();
                                            ToName = row["UserName"].ToString();

                                            if ((ToEmailAddress.Trim() != "") && (CurrentUserEmailAddress.Trim() != ""))
                                            {
                                                if (SendSingleEmail(CurrentUserName, ToName, ToEmailAddress, CurrentUserEmailAddress))
                                                {
                                                    EmailCount++;
                                                }
                                            }
                                        }
                                    }

                                    if (RequireApproverApproval)
                                    {
                                        if (row["FunctionName"].ToString() == "Approve Final")
                                        {
                                            ToEmailAddress = row["EmailAddress"].ToString();
                                            ToName = row["UserName"].ToString();

                                            if ((ToEmailAddress.Trim() != "") && (CurrentUserEmailAddress.Trim() != ""))
                                            {
                                                if (SendSingleEmail(CurrentUserName, ToName, ToEmailAddress, CurrentUserEmailAddress))
                                                {
                                                    EmailCount++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                catch
                {

                }

                if (EmailCount > 0)
                {
                    MessageBox.Show(EmailCount.ToString() + " people successfully requested to approve the project.", "Project Approval", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show("No people were requested to approve the project.", "Project Approval", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void GetRequesterEmail()
        {
            try
            {
                DataTable dtEmail;

                using (DataStuff sn = new DataStuff())
                {
                    dtEmail = sn.ProjectGetRequesterEmail(ServerName, ProjectName);

                    if (dtEmail != null)
                    {
                        if (dtEmail.Rows.Count > 0)
                        {
                            RequesterEmailAddress = dtEmail.Rows[0]["EmailAddress"].ToString();
                            RequesterName = dtEmail.Rows[0]["UserName"].ToString();
                        }
                    }
                }
            }

            catch
            {
            }
        }

        private void GetApproverEmail()
        {
            try
            {
                DataTable dtEmail;

                using (DataStuff sn = new DataStuff())
                {
                    dtEmail = sn.ProjectGetApproverEmails();

                    if (dtEmail != null)
                    {
                        if (dtEmail.Rows.Count > 0)
                        {
                            foreach (DataRow row in dtEmail.Rows)
                            {
                                if (row["QTNumber"].ToString().ToUpper() == Environment.UserName.ToUpper())
                                {
                                    ApproverName = row["UserName"].ToString();
                                    ApproverEmailAddress = row["EmailAddress"].ToString();
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            catch
            {

            }
        }

        private bool SendRequesterEmail()
        {
            try
            {
                string HTMLBody = string.Empty;
                string Subject = string.Empty;
                string TextBody = string.Empty;
                string IsHTML = string.Empty;

                GetRequesterEmail();

                GetApproverEmail();

                if ((RequesterEmailAddress.Trim() == "") || (ApproverEmailAddress.Trim() == ""))
                {
                    return false;
                }

                MailMessage mail = new MailMessage();

                Subject = "SQL Project Approval";
                HTMLBody = "";
                TextBody = "";

                HTMLBody = string.Format(@"<p>Dear {0}</p>" +
                                         @"<p> </p>" +
                                         @"<p>{1} has approved your project {2} on server {3} for a production release.</p>" +
                                         @"<p> </p>" +
                                         @"<p> </p>" +
                                         @"<p>Many thanks</p>" +
                                         @"<p></p>" +
                                         @"<p>BMW Financial Services Release Approver</p>",
                                         RequesterName,
                                         ApproverName,
                                         ProjectName,
                                         ServerName);

                mail.To.Add(RequesterEmailAddress);

                mail.From = new MailAddress(ApproverEmailAddress);
                mail.Subject = Subject;
                mail.Body = TextBody;

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(HTMLBody, null, MediaTypeNames.Text.Html);

                mail.AlternateViews.Add(htmlView);

                SmtpClient client = new SmtpClient();

                client.PickupDirectoryLocation = SMTPPath;

                client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                client.Send(mail);

                return true;
            }

            catch (System.Exception ex)
            {
                return false;
            }
        }

        private bool SendSingleEmail(string FromUser, string ToUser, string ToEmailAddress, string FromEmailAddress)
        {
            try
            {
                string HTMLBody = string.Empty;
                string Subject = string.Empty;
                string TextBody = string.Empty;
                string IsHTML = string.Empty;
                
                MailMessage mail = new MailMessage();

                Subject = "SQL Project Approval Request";
                HTMLBody = "";
                TextBody = "";
                
                HTMLBody = string.Format(@"<p>Dear {0}</p>" +
                                         @"<p> </p>" +
                                         @"<p>{1} is requesting you to approve project {2} on server {3} for a production release.</p>" +
                                         @"<p> </p>" +
                                         @"<p> </p>" +
                                         @"<p>Many thanks</p>" +
                                         @"<p></p>" +
                                         @"<p>BMW Financial Services Release Team</p>",
                                         ToUser,
                                         FromUser,
                                         ProjectName,
                                         ServerName);


                mail.To.Add(ToEmailAddress);

                mail.From = new MailAddress(FromEmailAddress);
                mail.Subject = Subject;
                mail.Body = TextBody;

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(HTMLBody, null, MediaTypeNames.Text.Html);

                mail.AlternateViews.Add(htmlView);

                SmtpClient client = new SmtpClient();

                client.PickupDirectoryLocation = SMTPPath;

                client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                client.Send(mail);

                return true;
            }

            catch (System.Exception ex)
            {
                return false;
            }
        }

        private void cmdTestingApprove_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to approve this project?", "Approve Project", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (DataStuff sn = new DataStuff())
                {
                    sn.ProjectSaveApprovalStatus(ServerName, ProjectName, UserID, "Approve Testing");
                }

                GetApprovalStatus();

                SendRequesterEmail();
            }
        }

        private void cmdFinalApprove_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to approve this project?", "Approve Project", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (DataStuff sn = new DataStuff())
                {
                    sn.ProjectSaveApprovalStatus(ServerName, ProjectName, UserID, "Approve Final");
                }

                GetApprovalStatus();

                SendRequesterEmail();
            }
        }
    }
}