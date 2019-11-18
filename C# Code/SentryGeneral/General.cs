using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace SentryGeneral
{
    public class General
    {
        private string _DatabaseSelection = string.Empty;
        private string _ObjectSelection = string.Empty;
        private string _ProjectScriptFilePath = string.Empty;
        private string _ServerAliasSelected = string.Empty;

        private string _DSServerName = string.Empty;
        private string _DSUserName = string.Empty;
        private string _DSPassword = string.Empty;
        private string _DSIntegratedSecurity = string.Empty;
        private string _DSDefaultDatabase = string.Empty;
        
        private string _ConnectionString = string.Empty;

        
        public string DataStoreConnectionString
        {
            get
            {
                LoadDataStoreSettings();

                if (_DSServerName.Trim() != "")
                {
                    if (_DSIntegratedSecurity.Trim() == "Y")
                    {
                        _ConnectionString = "Data Source=" + _DSServerName.Trim() + ";Integrated Security=True";
                    }
                    else
                    {
                        _ConnectionString = "Data Source=" + _DSServerName.Trim() + ";User ID=" + _DSUserName.Trim() + ";Password=" + _DSPassword.Trim();
                    }
                }

                return _ConnectionString;
            }
        }

        public string DataStoreServerName
        {
            get
            {
                LoadDataStoreSettings();
                return _DSServerName;
            }
            set
            {
                _DSServerName = value;
                SaveDataStoreSettings();
            }
        }

        public string DataStoreUserName
        {
            get
            {
                LoadDataStoreSettings();
                return _DSUserName;
            }
            set
            {
                _DSUserName = value;
                SaveDataStoreSettings();
            }
        }

        public string DataStorePassword
        {
            get
            {
                LoadDataStoreSettings();
                return _DSPassword;
            }
            set
            {
                _DSPassword = value;
                SaveDataStoreSettings();
            }
        }

        public string DataStoreIntegratedSecurity
        {
            get
            {
                LoadDataStoreSettings();
                return _DSIntegratedSecurity;
            }
            set
            {
                _DSIntegratedSecurity = value;
                SaveDataStoreSettings();
            }
        }

        public string DataStoreDefaultDatabase
        {
            get
            {
                LoadDataStoreSettings();
                return _DSDefaultDatabase;
            }
            set
            {
                _DSDefaultDatabase = value;
                SaveDataStoreSettings();
            }
        }
        public string DatabaseSelection
        {
            get
            {
                LoadSettings();
                return _DatabaseSelection;
            }
            set
            {
                _DatabaseSelection = value;
                SaveSettings();
            }
        }
        public string ObjectSelection
        {
            get
            {
                LoadSettings();
                return _ObjectSelection;
            }
            set
            {
                _ObjectSelection = value;
                SaveSettings();
            }
        }

        public string ProjectScriptFilePath
        {
            get
            {
                LoadSettings();
                return _ProjectScriptFilePath;
            }
            set
            {
                _ProjectScriptFilePath = value;
                SaveSettings();
            }
        }

        public string ServerAliasSelected
        {
            get
            {
                LoadSettings();
                return _ServerAliasSelected;
            }
            set
            {
                _ServerAliasSelected = value;
                SaveSettings();
            }
        }

        private void LoadSettings()
        {
            Settings AS;

            string path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)).FullName;

            XmlSerializer mySerializer = new XmlSerializer(typeof(Settings));

            try
            {
                using (FileStream myFileStream = new FileStream(path + "\\SentrySettings.xml", FileMode.Open))
                {
                    AS = (Settings)mySerializer.Deserialize(myFileStream);

                    _DatabaseSelection = AS.SelectedDatabases;
                    _ObjectSelection = AS.SelectedObjects;
                    _ProjectScriptFilePath = AS.SQLFilePath;
                    _ServerAliasSelected = AS.ServerAliasSelected;
                }
            }
            catch
            {
                return;
            }
        }

        private void LoadDataStoreSettings()
        {
            DataStoreConfiguration DS;

            string path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)).FullName;

            XmlSerializer mySerializer = new XmlSerializer(typeof(DataStoreConfiguration));

            try
            {
                using (FileStream myFileStream = new FileStream(path + "\\SentryDataStoreSettings.xml", FileMode.Open))
                {
                    DS = (DataStoreConfiguration)mySerializer.Deserialize(myFileStream);

                    _DSServerName = DS.ServerName;
                    _DSUserName = DS.UserName;
                    _DSPassword = DS.Password;
                    _DSIntegratedSecurity = DS.IntegratedSecurity;
                    _DSDefaultDatabase = DS.DefaultDatabase;
                }
            }

            catch
            {
                return;
            }
        }

        public void SaveSettings()
        {
            string path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)).FullName;

            Settings AS = new Settings();

            AS.SelectedDatabases = _DatabaseSelection;
            AS.SelectedObjects = _ObjectSelection;
            AS.SQLFilePath = _ProjectScriptFilePath;
            AS.ServerAliasSelected = _ServerAliasSelected;

            XmlSerializer mySerializer = new XmlSerializer(typeof(Settings));

            try
            {
                using (StreamWriter myWriter = new StreamWriter(path + "\\SentrySettings.xml"))
                {
                    mySerializer.Serialize(myWriter, AS);
                    myWriter.Close();
                }
            }
            catch
            {
                return;
            }
        }

        public void SaveDataStoreSettings()
        {
            string path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)).FullName;

            DataStoreConfiguration DS = new DataStoreConfiguration();

            DS.ServerName = _DSServerName;
            DS.UserName = _DSUserName;
            DS.Password = _DSPassword;
            DS.IntegratedSecurity = _DSIntegratedSecurity;
            DS.DefaultDatabase = _DSDefaultDatabase;

            XmlSerializer mySerializer = new XmlSerializer(typeof(DataStoreConfiguration));

            try
            {
                using (StreamWriter myWriter = new StreamWriter(path + "\\SentryDataStoreSettings.xml"))
                {
                    mySerializer.Serialize(myWriter, DS);
                    myWriter.Close();
                }
            }
            catch
            {
                return;
            }
        }

        public string ChangeCreateToAlter(string TextIn)
        {
            for (int i = 1; i < 20; i++)
            {
                TextIn = ReplaceString(TextIn, "CREATE  ", "CREATE ", StringComparison.CurrentCultureIgnoreCase);
            }

            TextIn = ReplaceString(TextIn, "CREATE PROCEDURE", "ALTER PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "CREATE  PROCEDURE", "ALTER PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "CREATE   PROCEDURE", "ALTER PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "CREATE    PROCEDURE", "ALTER PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);

            TextIn = ReplaceString(TextIn, "CREATE PROC", "ALTER PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "CREATE  PROC", "ALTER PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "CREATE   PROC", "ALTER PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "CREATE    PROC", "ALTER PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);

            TextIn = ReplaceString(TextIn, "CREATE FUNCTION", "ALTER FUNCTION ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "CREATE  FUNCTION", "ALTER FUNCTION ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "CREATE   FUNCTION", "ALTER FUNCTION ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "CREATE    FUNCTION", "ALTER FUNCTION ", StringComparison.CurrentCultureIgnoreCase);

            return TextIn;
        }

        public string ChangeAlterToCreate(string TextIn)
        {
            for (int i = 1; i < 20; i++)
            {
                TextIn = ReplaceString(TextIn, "ALTER  ", "ALTER ", StringComparison.CurrentCultureIgnoreCase);
            }

            TextIn = ReplaceString(TextIn, "ALTER PROCEDURE", "CREATE PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "ALTER  PROCEDURE", "CREATE PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "ALTER   PROCEDURE", "CREATE PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "ALTER    PROCEDURE", "CREATE PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);

            TextIn = ReplaceString(TextIn, "ALTER PROC", "CREATE PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "ALTER  PROC", "CREATE PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "ALTER   PROC", "CREATE PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "ALTER    PROC", "CREATE PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);

            TextIn = ReplaceString(TextIn, "ALTER FUNCTION", "CREATE FUNCTION ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "ALTER  FUNCTION", "CREATE FUNCTION ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "ALTER   FUNCTION", "CREATE FUNCTION ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "ALTER    FUNCTION", "CREATE FUNCTION ", StringComparison.CurrentCultureIgnoreCase);

            return TextIn;
        }

        public static string ReplaceString(string str, string oldValue, string newValue, StringComparison comparison)
        {
            StringBuilder sb = new StringBuilder();

            int previousIndex = 0;
            int index = str.IndexOf(oldValue, comparison);

            while (index != -1)
            {
                sb.Append(str.Substring(previousIndex, index - previousIndex));
                sb.Append(newValue);
                index += oldValue.Length;

                previousIndex = index;
                index = str.IndexOf(oldValue, index, comparison);
            }

            sb.Append(str.Substring(previousIndex));

            return sb.ToString();
        }

        public string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if ((str[i] >= '0' && str[i] <= '9') || (str[i] >= 'A' && str[i] <= 'z' || (str[i] == '.' || str[i] == '_' || str[i] == ' ' || str[i] == '-')))
                {
                    sb.Append(str[i]);
                }
            }

            return sb.ToString();
        }
    }
}
