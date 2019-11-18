using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SentryDataStuff;
using System.Data;

namespace SentryGeneral
{
    public class AccessRights
    {
        public int UserID = 0;

        bool _Administration = false;
        bool _Backup = false;
        bool _CheckObjectOutDevelopment = false;
        bool _CheckObjectOutUAT = false;
        bool _CheckObjectOutPreProduction = false;
        bool _CheckObjectOutProduction = false;
        bool _ProjectMaintenance = false;
        bool _ReleasePreProduction = false;
        bool _ReleaseProduction = false;
        bool _ReleaseUAT = false;
        bool _RestoreDevelopment = false;
        bool _RestorePreProduction = false;
        bool _RestoreProduction = false;
        bool _RestoreUAT = false;
        bool _CheckOutOverride = false;
        bool _QuickRelease = false;
        bool _ApproverTesting = false;
        bool _ApproverFinal = false;

        public bool Administration
        {
            get
            {
                LoadUserFunctions();
                return _Administration;
            }
        }

        public bool CheckOutOverride
        {
            get
            {
                LoadUserFunctions();
                return _CheckOutOverride;
            }
        }

        public bool QuickRelease
        {
            get
            {
                LoadUserFunctions();
                return _QuickRelease;
            }
        }

        public bool Backup
        {
            get
            {
                LoadUserFunctions();
                return _Backup;
            }
        }

        public bool CheckObjectOutDevelopment
        {
            get
            {
                LoadUserFunctions();
                return _CheckObjectOutDevelopment;
            }
        }

        public bool CheckObjectOutUAT
        {
            get
            {
                LoadUserFunctions();
                return _CheckObjectOutUAT;
            }
        }

        public bool CheckObjectOutPreProduction
        {
            get
            {
                LoadUserFunctions();
                return _CheckObjectOutPreProduction;
            }
        }

        public bool CheckObjectOutProduction
        {
            get
            {
                LoadUserFunctions();
                return _CheckObjectOutProduction;
            }
        }

        public bool ProjectMaintenance
        {
            get
            {
                LoadUserFunctions();
                return _ProjectMaintenance;
            }
        }

        public bool ReleasePreProduction
        {
            get
            {
                LoadUserFunctions();
                return _ReleasePreProduction;
            }
        }

        public bool ReleaseProduction
        {
            get
            {
                LoadUserFunctions();
                return _ReleaseProduction;
            }
        }

        public bool ReleaseUAT
        {
            get
            {
                LoadUserFunctions();
                return _ReleaseUAT;
            }
        }

        public bool RestoreDevelopment
        {
            get
            {
                LoadUserFunctions();
                return _RestoreDevelopment;
            }
        }

        public bool RestorePreProduction
        {
            get
            {
                LoadUserFunctions();
                return _RestorePreProduction;
            }
        }

        public bool RestoreProduction
        {
            get
            {
                LoadUserFunctions();
                return _RestoreProduction;
            }
        }

        public bool RestoreUAT
        {
            get
            {
                LoadUserFunctions();
                return _RestoreUAT;
            }
        }

        public bool ApproverTesting
        {
            get
            {
                LoadUserFunctions();
                return _ApproverTesting;
            }
        }

        public bool ApproverFinal
        {
            get
            {
                LoadUserFunctions();
                return _ApproverFinal;
            }
        }

        private void LoadUserFunctions()
        {
            try
            {
                _Administration = false;
                _Backup = false;
                _CheckObjectOutDevelopment = false;
                _CheckObjectOutPreProduction = false;
                _CheckObjectOutProduction = false;
                _CheckObjectOutUAT = false;
                _ProjectMaintenance = false;
                _ReleasePreProduction = false;
                _ReleaseProduction = false;
                _ReleaseUAT = false;
                _RestoreDevelopment = false;
                _RestorePreProduction = false;
                _RestoreProduction = false;
                _RestoreUAT = false;
                _CheckOutOverride = false;
                _QuickRelease = false;
                _ApproverFinal = false;
                _ApproverTesting = false;

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetUserFunctions(UserID);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            switch (row["FunctionName"].ToString())
                            {
                                case "Administration":
                                    _Administration = true;
                                    break;

                                case "Backup":
                                    _Backup = true;
                                    break;

                                case "Check Object Out Development":
                                    _CheckObjectOutDevelopment = true;
                                    break;

                                case "Check Object Out UAT":
                                    _CheckObjectOutUAT = true;
                                    break;

                                case "Check Object Out Pre-Production":
                                    _CheckObjectOutPreProduction = true;
                                    break;

                                case "Check Object Out Production":
                                    _CheckObjectOutProduction = true;
                                    break;

                                case "Check Out Override":
                                    _CheckOutOverride = true;
                                    break;

                                case "Project Maintenance":
                                    _ProjectMaintenance = true;
                                    break;

                                case "Release to Pre-Production":
                                    _ReleasePreProduction = true;
                                    break;

                                case "Release to Production":
                                    _ReleaseProduction = true;
                                    break;

                                case "Quick Release":
                                    _QuickRelease = true;
                                    break;

                                case "Release to UAT":
                                    _ReleaseUAT = true;
                                    break;

                                case "Restore Development":
                                    _RestoreDevelopment = true;
                                    break;

                                case "Restore Pre-Production":
                                    _RestorePreProduction = true;
                                    break;

                                case "Restore Production":
                                    _RestoreProduction = true;
                                    break;

                                case "Restore UAT":
                                    _RestoreUAT = true;
                                    break;

                                case "Approve Testing":
                                    _ApproverTesting = true;
                                    break;

                                case "Approve Final":
                                    _ApproverFinal = true;
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                }
            }

            catch
            {
            }
        }
    }
}
