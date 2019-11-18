using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GeneralGlobal
{
    public class DBSchema : MarshalByRefObject, IDisposable
    {
        public event ProgressHandler Tick;
        public delegate void ProgressHandler(DBSchema dbs, ProgressEventArgs e);
        public int count;
        
        public DBSchema()
        {
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public class ProgressEventArgs : EventArgs
        {
            public int count;

            public ProgressEventArgs(int count)
            {
                this.count = count;
            }
        } 

        public Boolean DBUpToDate(int CurrentVersion)
        {
            using (DBConnect GK = new DBConnect())
            {
                DataTable dr;
                try
                {
                    dr = GK.DataTable("select * from DatabaseVersion where DBVersion = '" + CurrentVersion + "'");

                    if (dr.Rows.Count > 0)
                    {
                        return true;
                    }
                }

                catch
                {
                    return false;
                }

                return false;
            }
        }

        public void UpdateVersion(int NewVersion)
        {
            string sql;

            using (DBConnect UV = new DBConnect())
            {
                try
                {
                    sql = "update DatabaseVersion set DBVersion = '" + NewVersion + "'";

                    UV.SqlCommand(sql);
                }

                catch
                {
                    sql = "insert into DatabaseVersion (DBVersion) values(' = '" + NewVersion + "')";

                    UV.SqlCommand(sql);
                }
            }
        }

        private void DoUpdate(string ChangeType, string TableIn, string ColumnIn, string DataTypeIn)
        {
            string TmpTable;
            string DataString;

            //if (_DBType.Trim() != "Access")
            //{
            //    DataTypeIn = DataTypeIn.Replace("TEXT", "VARCHAR");
            //    DataTypeIn = DataTypeIn.Replace("NUMBER", "NUMERIC");
            //    DataTypeIn = DataTypeIn.Replace("CURRENCY", "MONEY");

            //    if (DataTypeIn.Contains("COUNTER"))
            //    {
            //        DataTypeIn = "[numeric](18, 0) IDENTITY(1,1) NOT NULL";
            //    }

            //    if (ChangeType == "Create Multiple")
            //    {
            //        if (TableIn.Contains("ALTER COLUMN"))
            //        {
            //            TableIn.Replace("ALTER COLUMN", "CHANGE");
            //        }

            //        if (TableIn.ToUpper().Contains("COUNTER"))
            //        {
            //            TableIn.Replace("CURRENCY", "FLOAT");

            //            int Pos = TableIn.IndexOf("(");
            //            int Pos2 = TableIn.Substring(Pos).IndexOf(" ") + Pos;

            //            if (Pos2 > 0)
            //            {
            //                TmpTable = TableIn.Substring(Pos + 1, Pos2 - Pos - 1);

            //                if (Pos > 0)
            //                {
            //                    TableIn = TableIn.Substring(0, Pos - 1);
            //                    TableIn = TableIn + " (" + TmpTable + " BIGINT NOT NULL AUTO_INCREMENT KEY)";
            //                }
            //            }
            //        }
            //    }
            //}

            //switch (ChangeType)
            //{
            //    case "Alter Table":
            //        if (DBType.Trim() == "Access")
            //        {
            //            DataString = "ALTER TABLE " + TableIn.Trim() + " " + "ADD COLUMN " + ColumnIn.Trim() + " " + DataTypeIn.Trim();
            //        }
            //        else
            //        {
            //            DataTypeIn = DataTypeIn.Replace("MONEY", "FLOAT");
            //            DataTypeIn = DataTypeIn.Replace("[numeric](18, 0) IDENTITY(1,1) NOT NULL", "BIGINT NOT NULL AUTO_INCREMENT KEY");

            //            DataString = "ALTER TABLE " + TableIn.Trim () + " " + "ADD COLUMN " + ColumnIn.Trim() + " " + DataTypeIn.Trim();
            //        }

            //        break;

            //    case "Create Table":
            //        DataString = "CREATE TABLE " + TableIn.Trim() + " (" + ColumnIn.Trim() + " " + DataTypeIn.Trim() + ")";
            //        break;

            //    case "Create Multiple":
            //        DataString = TableIn.Trim ();
            //        break;

            //    default:
            //        DataString = "";
            //        break;
            //}

            //using (DBConnect SC = new DBConnect())
            //{
            //    try
            //    {
            //        SC.ConnectionString = _ConnectionString;
                    
            //        SC.SqlCommand(DataString);
            //    }

            //    catch
            //    {
            //    }
            //}
        }

        private Boolean CheckTable(string TableIn)
        {
            if (Tick != null)
            {
                count++;

                //if (count == 3)
                //{
                //    count++;
                //}

                ProgressEventArgs pea = new ProgressEventArgs(count);
                Tick(this, pea);
            }

            using (DBConnect SC = new DBConnect())
            {
                DataTable dt;
                
                try
                {
                    dt = SC.DataTable("select * from " + TableIn.Trim () + " where 0 = 1");

                    if (dt.Columns.Count > 0)
                    {
                        return true;
                    }
                }

                catch
                {
                    return false;
                }

                return false;
            }
        }

        private Boolean CheckColumn(string TableIn, string ColumnIn)
        {
            using (DBConnect SC = new DBConnect())
            {
                DataTable dt;
                
                try
                {
                    dt = SC.DataTable("select " + ColumnIn.Trim() + " from " + TableIn.Trim () + " where 0 = 1");

                    if (dt.Columns.Count > 0)
                    {
                        return true;
                    }
                }

                catch
                {
                    return false;
                }

                return false;
            }
        }

        public void CompareAll()

        {
            int CurVersion = 4;

            if (DBUpToDate(CurVersion))
            {
                return;
            }

            //DatabaseVersion
  
            if (!CheckTable("DatabaseVersion"))
            {
                DoUpdate("Create Table", "DatabaseVersion", "DBVersion", "TEXT(50) PRIMARY KEY");
            }

            //Agent
  
            if (!CheckTable("Agent"))
            {
                DoUpdate("Create Multiple", "CREATE TABLE Agent (AgentId COUNTER PRIMARY KEY)", "", "");
            }
             
            if (!CheckColumn("Agent", "AgentName"))
            {
                DoUpdate("Alter Table", "Agent", "AgentName", "TEXT(100)");
            }

            //AgentStock

            if (!CheckTable("AgentStock")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE AgentStock (AgentStockId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("AgentStock", "AccountNumber"))
            {
                DoUpdate("Alter Table", "AgentStock", "AccountNumber", "TEXT(50)");
            }
  
            if (!CheckColumn("AgentStock", "AccountName"))
            {
                DoUpdate("Alter Table", "AgentStock", "AccountName", "TEXT(100)");
            }
  
            if (!CheckColumn("AgentStock", "AgentStockDate"))
            {
                DoUpdate("Alter Table", "AgentStock", "AgentStockDate", "DATETIME");
            }
  
            if (!CheckColumn("AgentStock", "UserName"))
            {
                DoUpdate("Alter Table", "AgentStock", "UserName", "TEXT(50)");
            }
  
            if (!CheckColumn("AgentStock", "Status"))
            {
                DoUpdate("Alter Table", "AgentStock", "Status", "TEXT(20)");
            }
  
            if (!CheckColumn("AgentStock", "Notes"))
            {
                DoUpdate("Alter Table", "AgentStock", "Notes", "TEXT(255)");
            }
  
            if (!CheckColumn("AgentStock", "JobReference"))
            {
                DoUpdate("Alter Table", "AgentStock", "JobReference", "TEXT(100) NULL");
            }
  
            if (!CheckColumn("AgentStock", "Installer"))
            {
                DoUpdate("Alter Table", "AgentStock", "Installer", "TEXT(100) NULL");
            }
  
            //AgentStockItems
  
            if (!CheckTable("AgentStockItems"))
            {
                DoUpdate("Create Multiple", "CREATE TABLE AgentStockItems (AgentStockItemsId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("AgentStockItems", "AgentStockId"))
            {
                DoUpdate("Alter Table", "AgentStockItems", "AgentStockId", "NUMBER");
            }

            if (!CheckColumn("AgentStockItems", "StockId"))
            {
                DoUpdate("Alter Table", "AgentStockItems", "StockId", "TEXT(50)");
            }
  
            if (!CheckColumn("AgentStockItems", "Description"))
            {
                DoUpdate("Alter Table", "AgentStockItems", "Description", "TEXT(100)");
            }
  
            if (!CheckColumn("AgentStockItems", "Quantity"))
            {
                DoUpdate("Alter Table", "AgentStockItems", "Quantity", "NUMBER");
            }
  
            if (!CheckColumn("AgentStockItems", "Price"))
            {
                DoUpdate("Alter Table", "AgentStockItems", "Price", "CURRENCY");
            }
  
            if (!CheckColumn("AgentStockItems", "StockItemStatus"))
            {
                DoUpdate("Alter Table", "AgentStockItems", "StockItemStatus", "TEXT(20)");
            }
  
            //AgentStockItemsReturned
  
            if (!CheckTable("AgentStockItemsReturned"))
            {
                DoUpdate("Create Multiple", "CREATE TABLE AgentStockItemsReturned (AgentStockItemsId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("AgentStockItemsReturned", "AgentStockId"))
            {
                DoUpdate("Alter Table", "AgentStockItemsReturned", "AgentStockId", "NUMBER");
            }

            if (!CheckColumn("AgentStockItemsReturned", "StockId"))
            {
                DoUpdate("Alter Table", "AgentStockItemsReturned", "StockId", "TEXT(50)");
            }
  
            if (!CheckColumn("AgentStockItemsReturned", "Description"))
            {
                DoUpdate("Alter Table", "AgentStockItemsReturned", "Description", "TEXT(100)");
            }
  
            if (!CheckColumn("AgentStockItemsReturned", "Quantity"))
            {
                DoUpdate("Alter Table", "AgentStockItemsReturned", "Quantity", "NUMBER");
            }
  
            if (!CheckColumn("AgentStockItemsReturned", "Price"))
            {
                DoUpdate("Alter Table", "AgentStockItemsReturned", "Price", "CURRENCY");
            }
  
            if (!CheckColumn("AgentStockItemsReturned", "StockItemStatus"))
            {
                DoUpdate("Alter Table", "AgentStockItemsReturned", "StockItemStatus", "TEXT(20)");
            }
  
  
  
  
            //Appliance
  
            if (!CheckTable("Appliance"))
            {
                DoUpdate("Create Multiple", "CREATE TABLE Appliance (AccountNumber TEXT(50), " + 
                "ReceivalNumber TEXT(50), " + 
                "DateReceived DateTime, " + 
                "StockNumber TEXT(50), CONSTRAINT PkAppliance PRIMARY KEY (AccountNumber, ReceivalNumber, DateReceived, StockNumber))", "", "");
            }
  
            if (!CheckColumn("Appliance", "ApplianceType"))
            {
                DoUpdate("Alter Table", "Appliance", "ApplianceType", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("Appliance", "ApplianceMake"))
            {
                DoUpdate("Alter Table", "Appliance", "ApplianceMake", "TEXT(50)");
            }
  
            if (!CheckColumn("Appliance", "ApplianceModel"))
            {
                DoUpdate("Alter Table", "Appliance", "ApplianceModel", "TEXT(50)");
            }
  
            if (!CheckColumn("Appliance", "SerialNumber"))
            {
                DoUpdate("Alter Table", "Appliance", "SerialNumber", "TEXT(50)");
            }
  
            if (!CheckColumn("Appliance", "Fault"))
            {
                DoUpdate("Alter Table", "Appliance", "Fault", "TEXT(50)");
            }
  
            if (!CheckColumn("Appliance", "PromiseDate"))
            {
                DoUpdate("Alter Table", "Appliance", "PromiseDate", "DATETIME");
            }
  
            if (!CheckColumn("Appliance", "Notes"))
            {
                DoUpdate("Alter Table", "Appliance", "Notes", "TEXT(255)");
            }
  
            if (!CheckColumn("Appliance", "ApplianceStatus"))
            {
                DoUpdate("Alter Table", "Appliance", "ApplianceStatus", "TEXT(50)");
            }
  
            if (!CheckColumn("Appliance", "Technician"))
            {
                DoUpdate("Alter Table", "Appliance", "Technician", "TEXT(50)");
            }
  
            if (!CheckColumn("Appliance", "InvoiceNumber"))
            {
                DoUpdate("Alter Table", "Appliance", "InvoiceNumber", "TEXT(50)");
            }
  
  
  
            //ApplianceInvoiceNumber
  
            if (!CheckTable("ApplianceInvoiceNumber"))
            {
                DoUpdate("Create Multiple", "CREATE TABLE ApplianceInvoiceNumber (DebtorName TEXT(50), " + 
                "ApplianceNumberDate DateTime, CONSTRAINT PkApplianceInvNumber PRIMARY KEY (DebtorName, ApplianceNumberDate))", "", "");
            }
  
            if (!CheckColumn("ApplianceInvoiceNumber", "CurrentId"))
            {
                DoUpdate("Alter Table", "ApplianceInvoiceNumber", "CurrentId", "NUMBER");
            }
  
  
  
            //ApplianceItem
  
            if (!CheckTable("ApplianceItem"))
            {
                DoUpdate("Create Multiple", "CREATE TABLE ApplianceItem (AccountNumber TEXT(50), " + 
                "ReceivalNumber TEXT(50), StockId TEXT(50), CONSTRAINT PkApplianceItem PRIMARY KEY (AccountNumber, ReceivalNumber, StockId))", "", "");
            }
  
            if (!CheckColumn("ApplianceItem", "Description"))
            {
                DoUpdate("Alter Table", "ApplianceItem", "Description", "TEXT(50)");
            }
  
            if (!CheckColumn("ApplianceItem", "Quantity"))
            {
                DoUpdate("Alter Table", "ApplianceItem", "Quantity", "CURRENCY");
            }
  
            if (!CheckColumn("ApplianceItem", "UnitSalePrice"))
            {
                DoUpdate("Alter Table", "ApplianceItem", "UnitSalePrice", "CURRENCY");
            }
  
            if (!CheckColumn("ApplianceItem", "VATAmount"))
            {
                DoUpdate("Alter Table", "ApplianceItem", "VATAmount", "CURRENCY");
            }
  
            if (!CheckColumn("ApplianceItem", "TotalAmount"))
            {
                DoUpdate("Alter Table", "ApplianceItem", "TotalAmount", "CURRENCY");
            }
  
            if (!CheckColumn("ApplianceItem", "AddVAT"))
            {
                DoUpdate("Alter Table", "ApplianceItem", "AddVAT", "TEXT(1)");
            }
  
  
  
  
            //ApplianceNumber
  
            if (!CheckTable("ApplianceNumber"))
            {
                DoUpdate("Create Multiple", "CREATE TABLE ApplianceNumber (DebtorName TEXT(50), " + 
                "ApplianceNumberDate DateTime, CONSTRAINT PkApplianceNumber PRIMARY KEY (DebtorName, ApplianceNumberDate))", "", "");
            }
  
            if (!CheckColumn("ApplianceNumber", "CurrentId"))
            {
                DoUpdate("Alter Table", "ApplianceNumber", "CurrentId", "NUMBER");
            }
  
  
  
  
            //BarcodeLabel
  
            if (!CheckTable("BarcodeLabel"))
            {
                DoUpdate("Create Multiple", "CREATE TABLE BarcodeLabel (SetName TEXT(50))", "", "");
            }
  
            if (!CheckColumn("BarcodeLabel", "Label1X"))
            {
                DoUpdate("Alter Table", "BarcodeLabel", "Label1X", "TEXT(5)");
            }
  
            if (!CheckColumn("BarcodeLabel", "Label2X"))
            {
                DoUpdate("Alter Table", "BarcodeLabel", "Label2X", "TEXT(5)");
            }
  
            if (!CheckColumn("BarcodeLabel", "Label3X"))
            {
                DoUpdate("Alter Table", "BarcodeLabel", "Label3X", "TEXT(5)");
            }
  
            if (!CheckColumn("BarcodeLabel", "CompanyY"))
            {
                DoUpdate("Alter Table", "BarcodeLabel", "CompanyY", "TEXT(5)");
            }
  
            if (!CheckColumn("BarcodeLabel", "CompanyX")) 
            {
                DoUpdate("Alter Table", "BarcodeLabel", "CompanyX", "TEXT(5)");
            }
  
            if (!CheckColumn("BarcodeLabel", "CompanyFont")) 
            {
                DoUpdate("Alter Table", "BarcodeLabel", "CompanyFont", "TEXT(30)");
            }
  
            if (!CheckColumn("BarcodeLabel", "DescriptionY")) 
            {
                DoUpdate("Alter Table", "BarcodeLabel", "DescriptionY", "TEXT(5)");
            }
  
            if (!CheckColumn("BarcodeLabel", "DescriptionX")) 
            {
                DoUpdate("Alter Table", "BarcodeLabel", "DescriptionX", "TEXT(5)");
            }
  
            if (!CheckColumn("BarcodeLabel", "DescriptionFont")) 
            {
                DoUpdate("Alter Table", "BarcodeLabel", "DescriptionFont", "TEXT(30)");
            }
  
            if (!CheckColumn("BarcodeLabel", "PricingY")) 
            {
                DoUpdate("Alter Table", "BarcodeLabel", "PricingY", "TEXT(5)");
            }
  
            if (!CheckColumn("BarcodeLabel", "PricingX")) 
            {
                DoUpdate("Alter Table", "BarcodeLabel", "PricingX", "TEXT(5)");
            }
  
            if (!CheckColumn("BarcodeLabel", "PricingFont")) 
            {
                DoUpdate("Alter Table", "BarcodeLabel", "PricingFont", "TEXT(30)");
            }
  
            if (!CheckColumn("BarcodeLabel", "BarcodeY")) 
            {
                DoUpdate("Alter Table", "BarcodeLabel", "BarcodeY", "TEXT(5)");
            }
  
            if (!CheckColumn("BarcodeLabel", "BarcodeX")) 
            {
                DoUpdate("Alter Table", "BarcodeLabel", "BarcodeX", "TEXT(5)");
            }
  
            if (!CheckColumn("BarcodeLabel", "BarcodeHeight")) 
            {
                DoUpdate("Alter Table", "BarcodeLabel", "BarcodeHeight", "TEXT(5)");
            }
  
            if (!CheckColumn("BarcodeLabel", "DescriptorY")) 
            {
                DoUpdate("Alter Table", "BarcodeLabel", "DescriptorY", "TEXT(5)");
            }
  
            if (!CheckColumn("BarcodeLabel", "DescriptorX")) 
            {
                DoUpdate("Alter Table", "BarcodeLabel", "DescriptorX", "TEXT(5)");
            }
  
            if (!CheckColumn("BarcodeLabel", "DescriptorFont")) 
            {
                DoUpdate("Alter Table", "BarcodeLabel", "DescriptorFont", "TEXT(30)");
            }
  
            if (!CheckColumn("BarcodeLabel", "PrintDescriptor")) 
            {
                DoUpdate("Alter Table", "BarcodeLabel", "PrintDescriptor", "TEXT(1)");
            }
  
            if (!CheckColumn("BarcodeLabel", "StyleY")) 
            {
                DoUpdate("Alter Table", "BarcodeLabel", "StyleY", "TEXT(5) NULL");
            }
  
            if (!CheckColumn("BarcodeLabel", "StyleX")) 
            {
                DoUpdate("Alter Table", "BarcodeLabel", "StyleX", "TEXT(5) NULL");
            }
  
            if (!CheckColumn("BarcodeLabel", "StyleFont")) 
            {
                DoUpdate("Alter Table", "BarcodeLabel", "StyleFont", "TEXT(30) NULL");
            }
  
            if (!CheckColumn("BarcodeLabel", "SizeY")) 
            {
                DoUpdate("Alter Table", "BarcodeLabel", "SizeY", "TEXT(5) NULL");
            }
  
            if (!CheckColumn("BarcodeLabel", "SizeX")) 
            {
                DoUpdate("Alter Table", "BarcodeLabel", "SizeX", "TEXT(5) NULL");
            }
  
            if (!CheckColumn("BarcodeLabel", "SizeFont")) 
            {
                DoUpdate("Alter Table", "BarcodeLabel", "SizeFont", "TEXT(30) NULL");
            }
  
  
  
  
            //BarcodeLabelLog
  
            if (!CheckTable("BarcodeLabelLog")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE BarcodeLabelLog (LogId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("BarcodeLabelLog", "AccountNumber")) 
            {
                DoUpdate("Alter Table", "BarcodeLabelLog", "AccountNumber", "TEXT(50)");
            }
  
            if (!CheckColumn("BarcodeLabelLog", "CreditorName")) 
            {
                DoUpdate("Alter Table", "BarcodeLabelLog", "CreditorName", "TEXT(200)");
            }
  
            if (!CheckColumn("BarcodeLabelLog", "LabelCount")) 
            {
                DoUpdate("Alter Table", "BarcodeLabelLog", "LabelCount", "NUMBER");
            }
  
            if (!CheckColumn("BarcodeLabelLog", "UserName")) 
            {
                DoUpdate("Alter Table", "BarcodeLabelLog", "UserName", "TEXT(50)");
            }
  
            if (!CheckColumn("BarcodeLabelLog", "LogDate")) 
            {
                DoUpdate("Alter Table", "BarcodeLabelLog", "LogDate", "DATETIME");
            }
  
  
  
  
  
            //BarcodeLabelPrint
  
            if (!CheckTable("BarcodeLabelPrint")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE BarcodeLabelPrint (PrintId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("BarcodeLabelPrint", "StockId")) 
            {
                DoUpdate("Alter Table", "BarcodeLabelPrint", "StockId", "TEXT(50)");
            }
  
            if (!CheckColumn("BarcodeLabelPrint", "LabelCount")) 
            {
                DoUpdate("Alter Table", "BarcodeLabelPrint", "LabelCount", "NUMBER");
            }
  
  
  
  
  
            //BarcodeLabelPriceChange
  
            if (!CheckTable("BarcodeLabelPriceChange")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE BarcodeLabelPriceChange (PrintId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("BarcodeLabelPriceChange", "StockId")) 
            {
                DoUpdate("Alter Table", "BarcodeLabelPriceChange", "StockId", "TEXT(50)");
            }
  
            if (!CheckColumn("BarcodeLabelPriceChange", "LabelCount")) 
            {
                DoUpdate("Alter Table", "BarcodeLabelPriceChange", "LabelCount", "NUMBER");
            }
  
  
  
  
            //BookingItems
  
            if (!CheckTable("BookingItems")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE BookingItems (BookingItemId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("BookingItems", "BookingId")) 
            {
                DoUpdate("Alter Table", "BookingItems", "BookingId", "NUMBER");
            }
  
            if (!CheckColumn("BookingItems", "StockId")) 
            {
                DoUpdate("Alter Table", "BookingItems", "StockId", "TEXT(50)");
            }
  
  
  
  
            //Bookings
  
            if (!CheckTable("Bookings")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE Bookings (BookingId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("Bookings", "BookingDate")) 
            {
                DoUpdate("Alter Table", "Bookings", "BookingDate", "DATETIME");
            }
  
            if (!CheckColumn("Bookings", "BookingTime")) 
            {
                DoUpdate("Alter Table", "Bookings", "BookingTime", "TEXT(10)");
            }
  
            if (!CheckColumn("Bookings", "StaffName")) 
            {
                DoUpdate("Alter Table", "Bookings", "StaffName", "TEXT(50)");
            }
  
            if (!CheckColumn("Bookings", "UserName")) 
            {
                DoUpdate("Alter Table", "Bookings", "UserName", "TEXT(50)");
            }
  
            if (!CheckColumn("Bookings", "BookingDescription")) 
            {
                DoUpdate("Alter Table", "Bookings", "BookingDescription", "TEXT(255)");
            }
  
            if (!CheckColumn("Bookings", "CustomerName")) 
            {
                DoUpdate("Alter Table", "Bookings", "CustomerName", "TEXT(50)");
            }
  
            if (!CheckColumn("Bookings", "CustomerSurname")) 
            {
                DoUpdate("Alter Table", "Bookings", "CustomerSurname", "TEXT(50)");
            }
  
            if (!CheckColumn("Bookings", "CustomerContact")) 
            {
                DoUpdate("Alter Table", "Bookings", "CustomerContact", "TEXT(50)");
            }
  
            if (!CheckColumn("Bookings", "CustomerId")) 
            {
                DoUpdate("Alter Table", "Bookings", "CustomerId", "TEXT(50)");
            }
  
            if (!CheckColumn("Bookings", "BookingEnd")) 
            {
                DoUpdate("Alter Table", "Bookings", "BookingEnd", "TEXT(10)");
            }
  
            if (!CheckColumn("Bookings", "Status")) 
            {
                DoUpdate("Alter Table", "Bookings", "Status", "TEXT(50)");
            }
  
  
  
  
  
  
            //Branch
  
            if (!CheckTable("Branch")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE Branch (BranchName TEXT(50))", "", "");
            }
  
            if (!CheckColumn("Branch", "DatabasePath")) 
            {
                DoUpdate("Alter Table", "Branch", "DatabasePath", "TEXT(200)");
            }
  
            if (!CheckColumn("Branch", "Email")) 
            {
                DoUpdate("Alter Table", "Branch", "Email", "TEXT(200)");
            }
  
            if (!CheckColumn("Branch", "TelephoneNumber")) 
            {
                DoUpdate("Alter Table", "Branch", "TelephoneNumber", "TEXT(20)");
            }
  
            if (!CheckColumn("Branch", "FaxNumber")) 
            {
                DoUpdate("Alter Table", "Branch", "FaxNumber", "TEXT(20)");
            }
  
  
  
  
            //CafeSession
  
            if (!CheckTable("CafeSession")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE CafeSession (CafeSessionID NUMBER)", "", "");
            }
  
            if (!CheckColumn("CafeSession", "CreateDate")) 
            {
                DoUpdate("Alter Table", "CafeSession", "CreateDate", "DATETIME NULL");
            }
  
            if (!CheckColumn("CafeSession", "StockID")) 
            {
                DoUpdate("Alter Table", "CafeSession", "StockID", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("CafeSession", "StartDate")) 
            {
                DoUpdate("Alter Table", "CafeSession", "StartDate", "DATETIME NULL");
            }
  
            if (!CheckColumn("CafeSession", "EndDate")) 
            {
                DoUpdate("Alter Table", "CafeSession", "EndDate", "DATETIME NULL");
            }
  
            if (!CheckColumn("CafeSession", "InitialDuration")) 
            {
                DoUpdate("Alter Table", "CafeSession", "InitialDuration", "NUMBER NULL");
            }
  
            if (!CheckColumn("CafeSession", "RemainingTime")) 
            {
                DoUpdate("Alter Table", "CafeSession", "RemainingTime", "NUMBER NULL");
            }
  
            if (!CheckColumn("CafeSession", "AssignedMachineName")) 
            {
                DoUpdate("Alter Table", "CafeSession", "AssignedMachineName", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("CafeSession", "ActualMachineName")) 
            {
                DoUpdate("Alter Table", "CafeSession", "ActualMachineName", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("CafeSession", "Status")) 
            {
                DoUpdate("Alter Table", "CafeSession", "Status", "TEXT(10) NULL");
            }
  
  
  
  
            //CafeInstructions
  
            if (!CheckTable("CafeInstructions")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE CafeInstructions (MachineName TEXT(50))", "", "");
            }
  
            if (!CheckColumn("CafeInstructions", "Instruction")) 
            {
                DoUpdate("Alter Table", "CafeInstructions", "Instruction", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("CafeInstructions", "InstructionDate")) 
            {
                DoUpdate("Alter Table", "CafeInstructions", "InstructionDate", "DATETIME NULL");
            }
  
  
  
            //CafeWorkstation
  
            if (!CheckTable("CafeWorkstation")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE CafeWorkstation (MachineName TEXT(50))", "", "");
            }
  
            if (!CheckColumn("CafeWorkstation", "Status")) 
            {
                DoUpdate("Alter Table", "CafeWorkstation", "Status", "TEXT(5) NULL");
            }
  
  
  
  
  
            //CaseLots
  
            if (!CheckTable("CaseLots")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE CaseLots (CaseLotId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("CaseLots", "StockId1"))
            {
                DoUpdate("Alter Table", "CaseLots", "StockId1", "TEXT(50)");
            }
  
            if (!CheckColumn("CaseLots", "StockId2")) 
            {
                DoUpdate("Alter Table", "CaseLots", "StockId2", "TEXT(50)");
            }
  
            if (!CheckColumn("CaseLots", "Quantity1")) 
            {
                DoUpdate("Alter Table", "CaseLots", "Quantity1", "NUMBER");
            }
  
            if (!CheckColumn("CaseLots", "Quantity2")) 
            {
                DoUpdate("Alter Table", "CaseLots", "Quantity2", "NUMBER");
            }
  
  
            //Customer
  
            if (!CheckTable("Customer")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE Customer (CustomerId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("Customer", "CustomerName")) 
            {
                DoUpdate("Alter Table", "Customer", "CustomerName", "TEXT(50)");
            }
  
            if (!CheckColumn("Customer", "CustomerSurname")) 
            {
                DoUpdate("Alter Table", "Customer", "CustomerSurname", "TEXT(50)");
            }
  
            if (!CheckColumn("Customer", "AddressLine1")) 
            {
                DoUpdate("Alter Table", "Customer", "AddressLine1", "TEXT(100)");
            }
  
            if (!CheckColumn("Customer", "AddressLine2")) 
            {
                DoUpdate("Alter Table", "Customer", "AddressLine2", "TEXT(100)");
            }
  
            if (!CheckColumn("Customer", "AddressLine3")) 
            {
                DoUpdate("Alter Table", "Customer", "AddressLine3", "TEXT(100)");
            }
  
            if (!CheckColumn("Customer", "PostalCode")) 
            {
                DoUpdate("Alter Table", "Customer", "PostalCode", "TEXT(5)");
            }
  
            if (!CheckColumn("Customer", "PhysicalAddressLine1")) 
            {
                DoUpdate("Alter Table", "Customer", "PhysicalAddressLine1", "TEXT(100) NULL");
            }
  
            if (!CheckColumn("Customer", "PhysicalAddressLine2")) 
            {
                DoUpdate("Alter Table", "Customer", "PhysicalAddressLine2", "TEXT(100) NULL");
            }
  
            if (!CheckColumn("Customer", "PhysicalAddressLine3")) 
            {
                DoUpdate("Alter Table", "Customer", "PhysicalAddressLine3", "TEXT(100) NULL");
            }
  
            if (!CheckColumn("Customer", "PhysicalPostalCode")) 
            {
                DoUpdate("Alter Table", "Customer", "PhysicalPostalCode", "TEXT(5) NULL");
            }
  
            if (!CheckColumn("Customer", "ContactNumber1")) 
            {
                DoUpdate("Alter Table", "Customer", "ContactNumber1", "TEXT(30)");
            }
  
            if (!CheckColumn("Customer", "ContactNumber2")) 
            {
                DoUpdate("Alter Table", "Customer", "ContactNumber2", "TEXT(30)");
            }
  
            if (!CheckColumn("Customer", "FaxNumber")) 
            {
                DoUpdate("Alter Table", "Customer", "FaxNumber", "TEXT(30)");
            }
  
            if (!CheckColumn("Customer", "Title")) 
            {
                DoUpdate("Alter Table", "Customer", "Title", "TEXT(20)");
            }
  
            if (!CheckColumn("Customer", "DateRegistered")) 
            {
                DoUpdate("Alter Table", "Customer", "DateRegistered", "TEXT(20)");
            }
  
            if (!CheckColumn("Customer", "CustomerContact1")) 
            {
                DoUpdate("Alter Table", "Customer", "CustomerContact1", "TEXT(50)");
            }
  
            if (!CheckColumn("Customer", "CustomerContact2")) 
            {
                DoUpdate("Alter Table", "Customer", "CustomerContact2", "TEXT(50)");
            }
  
            if (!CheckColumn("Customer", "StandardDiscount")) 
            {
                DoUpdate("Alter Table", "Customer", "StandardDiscount", "CURRENCY");
            }
  
            if (!CheckColumn("Customer", "VATNumber")) 
            {
                DoUpdate("Alter Table", "Customer", "VATNumber", "TEXT(50)");
            }
  
            if (!CheckColumn("Customer", "IDNumber")) 
            {
                DoUpdate("Alter Table", "Customer", "IDNumber", "TEXT(20)");
            }
  
            if (!CheckColumn("Customer", "CreditLimit")) 
            {
                DoUpdate("Alter Table", "Customer", "CreditLimit", "CURRENCY NULL");
            }
  
            if (!CheckColumn("Customer", "DiscountMatrixName")) 
            {
                DoUpdate("Alter Table", "Customer", "DiscountMatrixName", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("Customer", "AccountNumber")) 
            {
                DoUpdate("Alter Table", "Customer", "AccountNumber", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("Customer", "EmailAddress")) 
            {
                DoUpdate("Alter Table", "Customer", "EmailAddress", "TEXT(100) NULL");
            }
  
            if (!CheckColumn("Customer", "SalesPerson")) 
            {
                DoUpdate("Alter Table", "Customer", "SalesPerson", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("Customer", "AccountContactPerson")) 
            {
                DoUpdate("Alter Table", "Customer", "AccountContactPerson", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("Customer", "AccountContactNumber1")) 
            {
                DoUpdate("Alter Table", "Customer", "AccountContactNumber1", "TEXT(30) NULL");
            }
  
            if (!CheckColumn("Customer", "AccountContactNumber2")) 
            {
                DoUpdate("Alter Table", "Customer", "AccountContactNumber2", "TEXT(30) NULL");
            }
  
            if (!CheckColumn("Customer", "AccountFaxNumber")) 
            {
                DoUpdate("Alter Table", "Customer", "AccountFaxNumber", "TEXT(30) NULL");
            }
  
            if (!CheckColumn("Customer", "GeographicalArea")) 
            {
                DoUpdate("Alter Table", "Customer", "GeographicalArea", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("Customer", "PriceLevel")) 
            {
                DoUpdate("Alter Table", "Customer", "PriceLevel", "TEXT(1) NULL");
            }
  
            if (!CheckColumn("Customer", "ImagePath")) 
            {
                DoUpdate("Alter Table", "Customer", "ImagePath", "TEXT(255) NULL");
            }
  
            if (!CheckColumn("Customer", "PicSize")) 
            {
                DoUpdate("Alter Table", "Customer", "PicSize", "TEXT(50) NULL");
            }
  
  
  
            //CustomerAccount
  
            if (!CheckTable("CustomerAccount")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE CustomerAccount (CustomerAccountId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("CustomerAccount", "TransactionType")) 
            {
                DoUpdate("Alter Table", "CustomerAccount", "TransactionType", "TEXT(50)");
            }

            if (!CheckColumn("CustomerAccount", "RegisterTransactionId")) 
            {
                DoUpdate("Alter Table", "CustomerAccount", "RegisterTransactionId", "NUMBER");
            }
  
            if (!CheckColumn("CustomerAccount", "UserName")) 
            {
                DoUpdate("Alter Table", "CustomerAccount", "UserName", "TEXT(50)");
            }
  
            if (!CheckColumn("CustomerAccount", "AccountNumber")) 
            {
                DoUpdate("Alter Table", "CustomerAccount", "AccountNumber", "TEXT(50)");
            }
  
            if (!CheckColumn("CustomerAccount", "AccountName")) 
            {
                DoUpdate("Alter Table", "CustomerAccount", "AccountName", "TEXT(100)");
            }
  
            if (!CheckColumn("CustomerAccount", "TransactionDate")) 
            {
                DoUpdate("Alter Table", "CustomerAccount", "TransactionDate", "DATETIME");
            }
  
            if (!CheckColumn("CustomerAccount", "TransactionAmount")) 
            {
                DoUpdate("Alter Table", "CustomerAccount", "TransactionAmount", "TEXT(50)");
            }
  
            if (!CheckColumn("CustomerAccount", "Description")) 
            {
                DoUpdate("Alter Table", "CustomerAccount", "Description", "TEXT(200)");
            }
  
            if (!CheckColumn("CustomerAccount", "StatementCategory")) 
            {
                DoUpdate("Alter Table", "CustomerAccount", "StatementCategory", "TEXT(50) NULL");
            }
  
  
  
  
            //CustomerLayBuy
  
            if (!CheckTable("CustomerLayBuy")) 
            {
            DoUpdate("Create Multiple", "CREATE TABLE CustomerLayBuy (CustomerLayBuyId NUMBER, " + 
                "TransactionType TEXT(50), RegisterTransactionId NUMBER, StockId TEXT(50), CONSTRAINT PkCustomerLayBuy PRIMARY KEY (CustomerLayBuyId, TransactionType, RegisterTransactionId, StockId))", "", "");
            }
  
            if (!CheckColumn("CustomerLayBuy", "UserName")) 
            {
                DoUpdate("Alter Table", "CustomerLayBuy", "UserName", "TEXT(50)");
            }
  
            if (!CheckColumn("CustomerLayBuy", "AccountNumber")) 
            {
                DoUpdate("Alter Table", "CustomerLayBuy", "AccountNumber", "TEXT(50)");
            }
  
            if (!CheckColumn("CustomerLayBuy", "AccountName")) 
            {
                DoUpdate("Alter Table", "CustomerLayBuy", "AccountName", "TEXT(100)");
            }
  
            if (!CheckColumn("CustomerLayBuy", "TransactionDate")) 
            {
                DoUpdate("Alter Table", "CustomerLayBuy", "TransactionDate", "DATETIME");
            }
  
            if (!CheckColumn("CustomerLayBuy", "TransactionAmount")) 
            {
                DoUpdate("Alter Table", "CustomerLayBuy", "TransactionAmount", "TEXT(50)");
            }
  
  
  
  
            //CustomerLayBuyItems
  
            if (!CheckTable("CustomerLayBuyItems")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE CustomerLayBuyItems (CustomerLayBuyItemsId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("CustomerLayBuyItems", "CustomerLayBuyId")) 
            {
                DoUpdate("Alter Table", "CustomerLayBuyItems", "CustomerLayBuyId", "NUMBER");
            }

            if (!CheckColumn("CustomerLayBuyItems", "StockId")) 
            {
                DoUpdate("Alter Table", "CustomerLayBuyItems", "StockId", "TEXT(50)");
            }
  
            if (!CheckColumn("CustomerLayBuyItems", "Status")) 
            {
                DoUpdate("Alter Table", "CustomerLayBuyItems", "Status", "TEXT(50)");
            }
  
            if (!CheckColumn("CustomerLayBuyItems", "Description")) 
            {
                DoUpdate("Alter Table", "CustomerLayBuyItems", "Description", "TEXT(100)");
            }
  
            if (!CheckColumn("CustomerLayBuyItems", "Price")) 
            {
                DoUpdate("Alter Table", "CustomerLayBuyItems", "Price", "TEXT(20)");
            }
  
  
  
  
            //CustomerHistory
  
            if (!CheckTable("CustomerHistory")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE CustomerHistory (CustomerHistoryId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("CustomerHistory", "CustomerId")) 
            {
                DoUpdate("Alter Table", "CustomerHistory", "CustomerId", "NUMBER");
            }
  
            if (!CheckColumn("CustomerHistory", "TransactionDate")) {
            DoUpdate("Alter Table", "CustomerHistory", "TransactionDate", "DATETIME");
            }
  
            if (!CheckColumn("CustomerHistory", "TransactionDescription")) {
            DoUpdate("Alter Table", "CustomerHistory", "TransactionDescription", "TEXT(255)");
            }
  
            if (!CheckColumn("CustomerHistory", "TransactionAmount")) {
            DoUpdate("Alter Table", "CustomerHistory", "TransactionAmount", "CURRENCY");
            }
  
  
  
            //CustomerSerialTracking
  
            if (!CheckTable("CustomerSerialTracking")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE CustomerSerialTracking (CustomerSerialTrackingId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("CustomerSerialTracking", "CustomerId")) 
            {
                DoUpdate("Alter Table", "CustomerSerialTracking", "CustomerId", "NUMBER");
            }
  
            if (!CheckColumn("CustomerSerialTracking", "StockId")) 
            {
                DoUpdate("Alter Table", "CustomerSerialTracking", "StockId", "TEXT(50)");
            }
  
            if (!CheckColumn("CustomerSerialTracking", "Description")) 
            {
                DoUpdate("Alter Table", "CustomerSerialTracking", "Description", "TEXT(100)");
            }
  
            if (!CheckColumn("CustomerSerialTracking", "RegisterTransactionId")) 
            {
                DoUpdate("Alter Table", "CustomerSerialTracking", "RegisterTransactionId", "NUMBER");
            }
  
            if (!CheckColumn("CustomerSerialTracking", "SerialNumber")) 
            {
                DoUpdate("Alter Table", "CustomerSerialTracking", "SerialNumber", "TEXT(50)");
            }
  
            if (!CheckColumn("CustomerSerialTracking", "TransactionNotes")) 
            {
                DoUpdate("Alter Table", "CustomerSerialTracking", "TransactionNotes", "TEXT(255)");
            }
  
            if (!CheckColumn("CustomerSerialTracking", "Guarantee")) 
            {
                DoUpdate("Alter Table", "CustomerSerialTracking", "Guarantee", "TEXT(50)");
            }
  
  
  
            //CustomerSerialTrackingHistory
  
            if (!CheckTable("CustomerSerialTrackingHistory")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE CustomerSerialTrackingHistory (CustomerSerialTrackingHistoryId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("CustomerSerialTrackingHistory", "CustomerSerialTrackingId")) 
            {
                DoUpdate("Alter Table", "CustomerSerialTrackingHistory", "CustomerSerialTrackingId", "NUMBER");
            }
  
            if (!CheckColumn("CustomerSerialTrackingHistory", "TrackingDate")) 
            {
                DoUpdate("Alter Table", "CustomerSerialTrackingHistory", "TrackingDate", "DATETIME");
            }
  
            if (!CheckColumn("CustomerSerialTrackingHistory", "TrackingNotes")) 
            {
                DoUpdate("Alter Table", "CustomerSerialTrackingHistory", "TrackingNotes", "TEXT(255)");
            }
  
  
  
  
            //CustomerStock
  
            if (!CheckTable("CustomerStock")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE CustomerStock (CustomerStockId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("CustomerStock", "StockId")) 
            {
                DoUpdate("Alter Table", "CustomerStock", "StockId", "TEXT(20)");
            }

            if (!CheckColumn("CustomerStock", "Description")) 
            {
                DoUpdate("Alter Table", "CustomerStock", "Description", "TEXT(20)");
            }
  
            if (!CheckColumn("CustomerStock", "StockCategory")) 
            {
                DoUpdate("Alter Table", "CustomerStock", "StockCategory", "TEXT(50)");
            }
  
            if (!CheckColumn("CustomerStock", "ItemType")) 
            {
                DoUpdate("Alter Table", "CustomerStock", "ItemType", "TEXT(50)");
            }
  
            if (!CheckColumn("CustomerStock", "ItemMake")) 
            {
                DoUpdate("Alter Table", "CustomerStock", "ItemMake", "TEXT(50)");
            }
  
            if (!CheckColumn("CustomerStock", "ItemModel")) 
            {
                DoUpdate("Alter Table", "CustomerStock", "ItemModel", "TEXT(50)");
            }
  
            if (!CheckColumn("CustomerStock", "SerialNumber")) 
            {
                DoUpdate("Alter Table", "CustomerStock", "SerialNumber", "TEXT(50)");
            }
  
            if (!CheckColumn("CustomerStock", "UserName")) 
            {
                DoUpdate("Alter Table", "CustomerStock", "UserName", "TEXT(50)");
            }
  
            if (!CheckColumn("CustomerStock", "UnitPrice")) 
            {
                DoUpdate("Alter Table", "CustomerStock", "UnitPrice", "CURRENCY");
            }
  
            if (!CheckColumn("CustomerStock", "SellingPrice")) 
            {
                DoUpdate("Alter Table", "CustomerStock", "SellingPrice", "CURRENCY");
            }
  
            if (!CheckColumn("CustomerStock", "TransactionDate")) 
            {
                DoUpdate("Alter Table", "CustomerStock", "TransactionDate", "DATETIME");
            }
  
            if (!CheckColumn("CustomerStock", "Notes")) 
            {
                DoUpdate("Alter Table", "CustomerStock", "Notes", "TEXT(255)");
            }
  
            if (!CheckColumn("CustomerStock", "Status")) 
            {
                DoUpdate("Alter Table", "CustomerStock", "Status", "TEXT(20) NULL");
            }
  
  
  
  
            //CustomerStockRelease
  
            if (!CheckTable("CustomerStockRelease")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE CustomerStockRelease (CustomerStockReleaseId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("CustomerStockRelease", "CustomerStockId")) 
            {
                DoUpdate("Alter Table", "CustomerStockRelease", "CustomerStockId", "NUMBER");
            }
  
            if (!CheckColumn("CustomerStockRelease", "SAPSMemberId")) 
            {
                DoUpdate("Alter Table", "CustomerStockRelease", "SAPSMemberId", "NUMBER");
            }
  
            if (!CheckColumn("CustomerStockRelease", "TransactionDate")) 
            {
                DoUpdate("Alter Table", "CustomerStockRelease", "ReleaseDate", "DATETIME");
            }
  
            if (!CheckColumn("CustomerStockRelease", "Reason")) 
            {
                DoUpdate("Alter Table", "CustomerStockRelease", "Reason", "TEXT(255)");
            }
  
  
  
  
            //CustomerVehicle
  
            if (!CheckTable("CustomerVehicle")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE CustomerVehicle (CustomerVehicleId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("CustomerVehicle", "CustomerId")) 
            {
                DoUpdate("Alter Table", "CustomerVehicle", "CustomerId", "NUMBER");
            }
  
            if (!CheckColumn("CustomerVehicle", "VehicleRegistration")) 
            {
                DoUpdate("Alter Table", "CustomerVehicle", "VehicleRegistration", "TEXT(12)");
            }
  
            if (!CheckColumn("CustomerVehicle", "CardNumber")) 
            {
                DoUpdate("Alter Table", "CustomerVehicle", "CardNumber", "TEXT(30)");
            }
  
  
  
            //Debtor
  
            if (!CheckTable("Debtor")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE Debtor (AccountNumber TEXT(50))", "", "");
            }
  
            if (!CheckColumn("Debtor", "DebtorName")) 
            {
                DoUpdate("Alter Table", "Debtor", "DebtorName", "TEXT(200)");
            }
  
            if (!CheckColumn("Debtor", "RegistrationNumber")) 
            {
                DoUpdate("Alter Table", "Debtor", "RegistrationNumber", "TEXT(30)");
            }
  
            if (!CheckColumn("Debtor", "AddressLine1")) 
            {
                DoUpdate("Alter Table", "Debtor", "AddressLine1", "TEXT(100)");
            }
  
            if (!CheckColumn("Debtor", "AddressLine2")) 
            {
                DoUpdate("Alter Table", "Debtor", "AddressLine2", "TEXT(100)");
            }
  
            if (!CheckColumn("Debtor", "AddressLine3")) 
            {
                DoUpdate("Alter Table", "Debtor", "AddressLine3", "TEXT(100)");
            }
  
            if (!CheckColumn("Debtor", "PostalCode")) 
            {
                DoUpdate("Alter Table", "Debtor", "PostalCode", "TEXT(5)");
            }
  
            if (!CheckColumn("Debtor", "ContactNumber1")) 
            {
                DoUpdate("Alter Table", "Debtor", "ContactNumber1", "TEXT(30)");
            }
  
            if (!CheckColumn("Debtor", "ContactNumber2")) 
            {
                DoUpdate("Alter Table", "Debtor", "ContactNumber2", "TEXT(30)");
            }
  
            if (!CheckColumn("Debtor", "FaxNumber")) 
            {
                DoUpdate("Alter Table", "Debtor", "FaxNumber", "TEXT(30)");
            }
  
            if (!CheckColumn("Debtor", "ContactPerson")) 
            {
                DoUpdate("Alter Table", "Debtor", "ContactPerson", "TEXT(100)");
            }
  
            if (!CheckColumn("Debtor", "PaymentTerms")) 
            {
                DoUpdate("Alter Table", "Debtor", "PaymentTerms", "TEXT(50)");
            }
  
            if (!CheckColumn("Debtor", "CreditLimit")) 
            {
                DoUpdate("Alter Table", "Debtor", "CreditLimit", "TEXT(20)");
            }
  
            if (!CheckColumn("Debtor", "AllowTransaction")) 
            {
                DoUpdate("Alter Table", "Debtor", "AllowTransaction", "TEXT(1)");
            }
  
            if (!CheckColumn("Debtor", "RegistrationDate")) 
            {
                DoUpdate("Alter Table", "Debtor", "RegistrationDate", "DATETIME");
            }
  
            if (!CheckColumn("Debtor", "DiscountPercentage")) 
            {
                DoUpdate("Alter Table", "Debtor", "DiscountPercentage", "TEXT(50)");
            }
  
            if (!CheckColumn("Debtor", "VATNumber")) 
            {
                DoUpdate("Alter Table", "Debtor", "VATNumber", "TEXT(50)");
            }
  
  
            //DebtorId
  
            if (!CheckTable("DebtorId")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE DebtorId (DebtorId NUMBER PRIMARY KEY)", "", "");
            }
  
  
  
            //DeliveryAddress
  
            if (!CheckTable("DeliveryAddress")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE DeliveryAddress (AccountNumber TEXT(50), " + 
                "SiteName TEXT(100), " + 
                "CONSTRAINT PkDA PRIMARY KEY (AccountNumber, SiteName))", "", "");
            }
  
            if (!CheckColumn("DeliveryAddress", "AddressLine1")) 
            {
                DoUpdate("Alter Table", "DeliveryAddress", "AddressLine1", "TEXT(100)");
            }
  
            if (!CheckColumn("DeliveryAddress", "AddressLine2")) 
            {
                DoUpdate("Alter Table", "DeliveryAddress", "AddressLine2", "TEXT(100)");
            }
  
            if (!CheckColumn("DeliveryAddress", "AddressLine3")) 
            {
                DoUpdate("Alter Table", "DeliveryAddress", "AddressLine3", "TEXT(100)");
            }
  
            if (!CheckColumn("DeliveryAddress", "ContactNumber1")) 
            {
                DoUpdate("Alter Table", "DeliveryAddress", "ContactNumber1", "TEXT(30)");
            }
  
            if (!CheckColumn("DeliveryAddress", "ContactNumber2")) 
            {
                DoUpdate("Alter Table", "DeliveryAddress", "DeliveryAddress", "TEXT(30)");
            }
  
            if (!CheckColumn("DeliveryAddress", "ContactPerson")) 
            {
                DoUpdate("Alter Table", "DeliveryAddress", "ContactPerson", "TEXT(100)");
            }
  
            if (!CheckColumn("DeliveryAddress", "SecurityComplex")) 
            {
                DoUpdate("Alter Table", "DeliveryAddress", "SecurityComplex", "TEXT(1)");
            }
  
            if (!CheckColumn("DeliveryAddress", "DeliveryTime")) 
            {
                DoUpdate("Alter Table", "DeliveryAddress", "DeliveryTime", "TEXT(50)");
            }
  
            if (!CheckColumn("DeliveryAddress", "InterlinkAllowed")) 
            {
                DoUpdate("Alter Table", "DeliveryAddress", "InterlinkAllowed", "TEXT(1)");
            }
  
  
  
  
            //DiscountMatrix
  
            if (!CheckTable("DiscountMatrix")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE DiscountMatrix (MatrixName TEXT(50), " + 
                "StockId TEXT(50), " + 
                "CONSTRAINT PkDM PRIMARY KEY (MatrixName, StockId))", "", "");
            }
  
            if (!CheckColumn("DiscountMatrix", "DiscountedSalePrice")) 
            {
                DoUpdate("Alter Table", "DiscountMatrix", "DiscountedSalePrice", "CURRENCY");
            }
  
            if (!CheckColumn("DiscountMatrix", "DiscountType")) 
            {
                DoUpdate("Alter Table", "DiscountMatrix", "DiscountType", "TEXT(30)");
            }
  
            if (!CheckColumn("DiscountMatrix", "DiscountValue")) 
            {
                DoUpdate("Alter Table", "DiscountMatrix", "DiscountValue", "CURRENCY");
            }
  
            if (!CheckColumn("DiscountMatrix", "ValidFrom")) 
            {
                DoUpdate("Alter Table", "DiscountMatrix", "ValidFrom", "DATETIME");
            }
  
            if (!CheckColumn("DiscountMatrix", "ValidTo")) 
            {
                DoUpdate("Alter Table", "DiscountMatrix", "ValidTo", "DATETIME");
            }
  
  
  
            //DispatchId
  
            if (!CheckTable("DispatchId")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE DispatchId (DispatchId NUMBER PRIMARY KEY)", "", "");
            }
  
            
            //GoodsReturn
  
            if (!CheckTable("GoodsReturn")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE GoodsReturn (StockId TEXT(50), " + 
                "ReturnDate DATETIME, " + 
                "CONSTRAINT PkGR PRIMARY KEY (StockId, ReturnDate))", "", "");
            }
  
            if (!CheckColumn("GoodsReturn", "Description")) 
            {
                DoUpdate("Alter Table", "GoodsReturn", "Description", "TEXT(50)");
            }
  
            if (!CheckColumn("GoodsReturn", "ReturnReason")) 
            {
                DoUpdate("Alter Table", "GoodsReturn", "ReturnReason", "TEXT(50)");
            }
  
            if (!CheckColumn("GoodsReturn", "TransactionId")) 
            {
                DoUpdate("Alter Table", "GoodsReturn", "TransactionId", "TEXT(50)");
            }
  
            if (!CheckColumn("GoodsReturn", "QuantityReturned")) 
            {
                DoUpdate("Alter Table", "GoodsReturn", "QuantityReturned", "CURRENCY");
            }
  
            if (!CheckColumn("GoodsReturn", "OriginalPaymentMethod")) 
            {
                DoUpdate("Alter Table", "GoodsReturn", "OriginalPaymentMethod", "TEXT(50)");
            }
  
            if (!CheckColumn("GoodsReturn", "OriginalPurchaseDate")) 
            {
                DoUpdate("Alter Table", "GoodsReturn", "OriginalPurchaseDate", "DATETIME");
            }
  
            if (!CheckColumn("GoodsReturn", "ReturnNotes")) 
            {
                DoUpdate("Alter Table", "GoodsReturn", "ReturnNotes", "TEXT(255)");
            }
  
            if (!CheckColumn("GoodsReturn", "CreditorName")) 
            {
                DoUpdate("Alter Table", "GoodsReturn", "CreditorName", "TEXT(50)");
            }
  
            if (!CheckColumn("GoodsReturn", "UserName")) 
            {
                DoUpdate("Alter Table", "GoodsReturn", "UserName", "TEXT(50)");
            }
  
            if (!CheckColumn("GoodsReturn", "Destination")) 
            {
                DoUpdate("Alter Table", "GoodsReturn", "Destination", "TEXT(50)");
            }
  
            if (!CheckColumn("GoodsReturn", "ReturnId")) 
            {
                DoUpdate("Alter Table", "GoodsReturn", "ReturnId", "TEXT(50)");
            }
  
            if (!CheckColumn("GoodsReturn", "RedeemStatus")) 
            {
                DoUpdate("Alter Table", "GoodsReturn", "RedeemStatus", "TEXT(50)");
            }
  
            if (!CheckColumn("GoodsReturn", "VoucherValue")) 
            {
                DoUpdate("Alter Table", "GoodsReturn", "VoucherValue", "CURRENCY");
            }
  
  
  
  
  
            //HappyHourSpecials
  
            if (!CheckTable("HappyHourSpecials")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE HappyHourSpecials (StockId TEXT(50) PRIMARY KEY)", "", "");
            }
    
            if (!CheckColumn("HappyHourSpecials", "DiscountedSalePrice")) 
            {
                DoUpdate("Alter Table", "HappyHourSpecials", "DiscountedSalePrice", "CURRENCY");
            }
  
            if (!CheckColumn("HappyHourSpecials", "DiscountType")) 
            {
                DoUpdate("Alter Table", "HappyHourSpecials", "DiscountType", "TEXT(30)");
            }
  
            if (!CheckColumn("HappyHourSpecials", "DiscountValue")) 
            {
                DoUpdate("Alter Table", "HappyHourSpecials", "DiscountValue", "CURRENCY");
            }
  
            if (!CheckColumn("HappyHourSpecials", "ValidFrom")) 
            {
                DoUpdate("Alter Table", "HappyHourSpecials", "ValidFrom", "DATETIME");
            }
  
            if (!CheckColumn("HappyHourSpecials", "ValidTo")) 
            {
                DoUpdate("Alter Table", "HappyHourSpecials", "ValidTo", "DATETIME");
            }
  
            if (!CheckColumn("HappyHourSpecials", "ValidMon")) 
            {
                DoUpdate("Alter Table", "HappyHourSpecials", "ValidMon", "TEXT(1)");
            }
  
            if (!CheckColumn("HappyHourSpecials", "ValidTue")) 
            {
                DoUpdate("Alter Table", "HappyHourSpecials", "ValidTue", "TEXT(1)");
            }
  
            if (!CheckColumn("HappyHourSpecials", "ValidWed")) 
            {
                DoUpdate("Alter Table", "HappyHourSpecials", "ValidWed", "TEXT(1)");
            }
  
            if (!CheckColumn("HappyHourSpecials", "ValidThu")) 
            {
                DoUpdate("Alter Table", "HappyHourSpecials", "ValidThu", "TEXT(1)");
            }
  
            if (!CheckColumn("HappyHourSpecials", "ValidFri")) 
            {
                DoUpdate("Alter Table", "HappyHourSpecials", "ValidFri", "TEXT(1)");
            }
  
            if (!CheckColumn("HappyHourSpecials", "ValidSat")) 
            {
                DoUpdate("Alter Table", "HappyHourSpecials", "ValidSat", "TEXT(1)");
            }
  
            if (!CheckColumn("HappyHourSpecials", "ValidSon")) 
            {
                DoUpdate("Alter Table", "HappyHourSpecials", "ValidSon", "TEXT(1)");
            }
  
  
            //Location
  
            if (!CheckTable("Location")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE Location (ParentId NUMBER, " + 
                "ChildId NUMBER, " + 
                "CONSTRAINT PkLoc PRIMARY KEY (ParentId, ChildId))", "", "");
            }
  
            if (!CheckColumn("Location", "ParentDesc")) 
            {
                DoUpdate("Alter Table", "Location", "ParentDesc", "TEXT(100)");
            }
  
            if (!CheckColumn("Location", "ChildDesc")) 
            {
                DoUpdate("Alter Table", "Location", "ChildDesc", "TEXT(100)");
            }
  
            if (!CheckColumn("Location", "Rank")) 
            {
                DoUpdate("Alter Table", "Location", "Rank", "NUMBER");
            }


            //Macro
  
            if (!CheckTable("Macro")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE Macro (MacroId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("Macro", "MacroKey")) 
            {
                DoUpdate("Alter Table", "Macro", "MacroKey", "TEXT(1)");
            }
  
            if (!CheckColumn("Macro", "MacroStrokes")) 
            {
                DoUpdate("Alter Table", "Macro", "MacroStrokes", "TEXT(250)");
            }
  
  
            //Lookup
  
            if (!CheckTable("Lookup")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE Lookup (ParentId NUMBER)", "", "");
            }
  
            if (!CheckColumn("Lookup", "ParentDesc")) 
            {
                DoUpdate("Alter Table", "Lookup", "ParentDesc", "TEXT(100)");
            }
  
            if (!CheckColumn("Lookup", "ChildId")) 
            {
                DoUpdate("Alter Table", "Lookup", "ChildId", "NUMBER");
            }
  
            if (!CheckColumn("Lookup", "ChildDesc")) 
            {
                DoUpdate("Alter Table", "Lookup", "ChildDesc", "TEXT(100)");
            }
  
            if (!CheckColumn("Lookup", "Rank")) 
            {
                DoUpdate("Alter Table", "Lookup", "Rank", "NUMBER");
            }
  
  
  
            //MenuItem
  
            if (!CheckTable("MenuItem")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE MenuItem (MenuItemId NUMBER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("MenuItem", "ParentId")) 
            {
                DoUpdate("Alter Table", "MenuItem", "ParentId", "TEXT(50)");
            }
  
            if (!CheckColumn("MenuItem", "ParentDesc")) 
            {
                DoUpdate("Alter Table", "MenuItem", "ParentDesc", "TEXT(50)");
            }
  
            if (!CheckColumn("MenuItem", "ParentRank")) 
            {
                DoUpdate("Alter Table", "MenuItem", "ParentRank", "NUMBER");
            }
  
            if (!CheckColumn("MenuItem", "ItemType")) 
            {
                DoUpdate("Alter Table", "MenuItem", "ItemType", "TEXT(50)");
            }
  
            if (!CheckColumn("MenuItem", "Child1Id")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child1Id", "TEXT(50)");
            }
  
            if (!CheckColumn("MenuItem", "Child1Desc")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child1Desc", "TEXT(50)");
            }
  
            if (!CheckColumn("MenuItem", "Child1Rank")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child1Rank", "NUMBER");
            }
  
            if (!CheckColumn("MenuItem", "Child2Id")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child2Id", "TEXT(50)");
            }
  
            if (!CheckColumn("MenuItem", "Child2Desc")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child2Desc", "TEXT(50)");
            }
  
            if (!CheckColumn("MenuItem", "Child2Rank")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child2Rank", "NUMBER");
            }
  
            if (!CheckColumn("MenuItem", "Child3Id")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child3Id", "TEXT(50)");
            }
  
            if (!CheckColumn("MenuItem", "Child3Desc")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child3Desc", "TEXT(50)");
            }
  
            if (!CheckColumn("MenuItem", "Child3Rank")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child3Rank", "NUMBER");
            }
  
            if (!CheckColumn("MenuItem", "Child4Id")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child4Id", "TEXT(50)");
            }
  
            if (!CheckColumn("MenuItem", "Child4Desc")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child4Desc", "TEXT(50)");
            }
  
            if (!CheckColumn("MenuItem", "Child4Rank")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child4Rank", "NUMBER");
            }
  
            if (!CheckColumn("MenuItem", "Child5Id")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child5Id", "TEXT(50)");
            }
  
            if (!CheckColumn("MenuItem", "Child5Desc")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child5Desc", "TEXT(50)");
            }
  
            if (!CheckColumn("MenuItem", "Child5Rank")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child5Rank", "NUMBER");
            }
  
            if (!CheckColumn("MenuItem", "Child6Id")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child6Id", "TEXT(50)");
            }
  
            if (!CheckColumn("MenuItem", "Child6Desc")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child6Desc", "TEXT(50)");
            }
  
            if (!CheckColumn("MenuItem", "Child6Rank")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child6Rank", "NUMBER");
            }
  
            if (!CheckColumn("MenuItem", "Child7Id")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child7Id", "TEXT(50)");
            }
  
            if (!CheckColumn("MenuItem", "Child7Desc")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child7Desc", "TEXT(50)");
            }
  
            if (!CheckColumn("MenuItem", "Child7Rank")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child7Rank", "NUMBER");
            }
  
            if (!CheckColumn("MenuItem", "Child8Id")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child8Id", "TEXT(50)");
            }
  
            if (!CheckColumn("MenuItem", "Child8Desc")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child8Desc", "TEXT(50)");
            }
  
            if (!CheckColumn("MenuItem", "Child8Rank")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child8Rank", "NUMBER");
            }
  
            if (!CheckColumn("MenuItem", "Child9Id")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child9Id", "TEXT(50)");
            }
  
            if (!CheckColumn("MenuItem", "Child9Desc")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child9Desc", "TEXT(50)");
            }
  
            if (!CheckColumn("MenuItem", "Child9Rank")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child9Rank", "NUMBER");
            }
  
            if (!CheckColumn("MenuItem", "Child10Id")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child10Id", "TEXT(50)");
            }
  
            if (!CheckColumn("MenuItem", "Child10Desc")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child10Desc", "TEXT(50)");
            }
  
            if (!CheckColumn("MenuItem", "Child10Rank")) 
            {
                DoUpdate("Alter Table", "MenuItem", "Child10Rank", "NUMBER");
            }
  
            if (!CheckColumn("MenuItem", "ButtonColour")) 
            {
                DoUpdate("Alter Table", "MenuItem", "ButtonColour", "NUMBER NULL");
            }
  
            //MenuOptions
  
            if (!CheckTable("MenuOptions"))
            {
                DoUpdate("Create Multiple", "CREATE TABLE MenuOptions (MenuItemId NUMBER, " + 
                "OptionDesc TEXT(50), " + 
                "CONSTRAINT PkMO PRIMARY KEY (MenuItemId, OptionDesc))", "", "");
            }
  
  
  
            //MenuStock
  
            if (!CheckTable("MenuStock")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE MenuStock (MenuItemId NUMBER, " + 
                "StockId TEXT(50), " + 
                "CONSTRAINT PkMS PRIMARY KEY (MenuItemId, StockId))", "", "");
            }
  
  
            //ModuleAccess
  
            if (!CheckTable("ModuleAccess")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE ModuleAccess (ModuleAccessId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("ModuleAccess", "ModuleName")) 
            {
                DoUpdate("Alter Table", "ModuleAccess", "ModuleName", "TEXT(50)");
            }
  
            if (!CheckColumn("ModuleAccess", "ModuleType")) 
            {
                DoUpdate("Alter Table", "ModuleAccess", "ModuleType", "TEXT(1)");
            }
  
  
  
  
            //MovementLogPerson
  
            if (!CheckTable("MovementLogPerson")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE MovementLogPerson (MovementId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("MovementLogPerson", "ActionDesc")) 
            {
                DoUpdate("Alter Table", "MovementLogPerson", "ActionDesc", "TEXT(50)");
            }
  
            if (!CheckColumn("MovementLogPerson", "CardNo")) 
            {
                DoUpdate("Alter Table", "MovementLogPerson", "CardNo", "TEXT(50)");
            }
  
            if (!CheckColumn("MovementLogPerson", "ScannedAt")) 
            {
                DoUpdate("Alter Table", "MovementLogPerson", "ScannedAt", "TEXT(50)");
            }
  
            if (!CheckColumn("MovementLogPerson", "ActionDate")) 
            {
                DoUpdate("Alter Table", "MovementLogPerson", "ActionDate", "DATETIME");
            }
  
            if (!CheckColumn("MovementLogPerson", "LogDate")) 
            {
                DoUpdate("Alter Table", "MovementLogPerson", "LogDate", "DATETIME");
            }
  
            if (!CheckColumn("MovementLogPerson", "UserName")) 
            {
                DoUpdate("Alter Table", "MovementLogPerson", "UserName", "TEXT(50)");
            }
  
            if (!CheckColumn("MovementLogPerson", "Processed")) 
            {
                DoUpdate("Alter Table", "MovementLogPerson", "Processed", "DATETIME NULL");
            }

  
  
            //OpenTransactions
  
  
            //if (DBType.Trim() == "Access")
            //{
            //    if (!CheckTable("OpenTransactions")) 
            //    {
            //        DoUpdate("Create Multiple", "CREATE TABLE OpenTransactions (OpenTranId COUNTER, " + 
            //            "TransactionId NUMBER, " + 
            //            "SeatingId NUMBER, " + 
            //            "RegisterTransactionId NUMBER, " + 
            //            "CONSTRAINT PkOT PRIMARY KEY (RegisterTransactionId, TransactionId, SeatingId, OpenTranId))", "", "");
            //    }
            //}
            //else
            //    {
            //        if (!CheckTable("OpenTransactions")) 
            //        {
            //            DoUpdate("Create Multiple", "CREATE TABLE OpenTransactions (OpenTranId COUNTER, " + 
            //            "TransactionId NUMBER, " + 
            //            "SeatingId NUMBER, " + 
            //            "RegisterTransactionId NUMBER, " + 
            //            "CONSTRAINT PkOT PRIMARY KEY (RegisterTransactionId, TransactionId, SeatingId, OpenTranId))", "", "");
            //        }
            //}
  
            if (!CheckColumn("OpenTransactions", "TransactionId")) 
            {
                DoUpdate("Alter Table", "OpenTransactions", "TransactionId", "NUMBER");
            }
  
            if (!CheckColumn("OpenTransactions", "SeatingId")) 
            {
                DoUpdate("Alter Table", "OpenTransactions", "SeatingId", "NUMBER");
            }
  
            if (!CheckColumn("OpenTransactions", "RegisterTransactionId")) 
            {
                DoUpdate("Alter Table", "OpenTransactions", "RegisterTransactionId", "NUMBER");
            }
  
            //if (DBType.Trim() == "MySQL")
            //{
            //    DoUpdate("Create Multiple", "ALTER TABLE `quicksale`.`opentransactions` DROP PRIMARY KEY, ADD PRIMARY KEY  USING BTREE(`OpenTranId`, `TransactionId`, `SeatingId`, `RegisterTransactionId`);", "", "");
            //}
  
            if (!CheckColumn("OpenTransactions", "TransactionType"))
            {
                DoUpdate("Alter Table", "OpenTransactions", "TransactionType", "TEXT(50)");
            }
  
            if (!CheckColumn("OpenTransactions", "UserName")) 
            {
                DoUpdate("Alter Table", "OpenTransactions", "UserName", "TEXT(50)");
            }
  
            if (!CheckColumn("OpenTransactions", "StockId")) 
            {
                DoUpdate("Alter Table", "OpenTransactions", "StockId", "TEXT(50)");
            }
  
            if (!CheckColumn("OpenTransactions", "Description")) 
            {
                DoUpdate("Alter Table", "OpenTransactions", "Description", "TEXT(50)");
            }
  
            if (!CheckColumn("OpenTransactions", "Quantity")) 
            {
                DoUpdate("Alter Table", "OpenTransactions", "Quantity", "CURRENCY");
            }
  
            if (!CheckColumn("OpenTransactions", "UnitPrice")) 
            {
                DoUpdate("Alter Table", "OpenTransactions", "UnitPrice", "TEXT(50)");
            }
  
            if (!CheckColumn("OpenTransactions", "VATAmount")) 
            {
                DoUpdate("Alter Table", "OpenTransactions", "VATAmount", "TEXT(50)");
            }
  
            if (!CheckColumn("OpenTransactions", "VATPercentage")) 
            {
                DoUpdate("Alter Table", "OpenTransactions", "VATPercentage", "TEXT(50)");
            }
  
            if (!CheckColumn("OpenTransactions", "TotalAmount")) 
            {
                DoUpdate("Alter Table", "OpenTransactions", "TotalAmount", "TEXT(50)");
            }
  
            if (!CheckColumn("OpenTransactions", "DiscountAmount")) 
            {
                DoUpdate("Alter Table", "OpenTransactions", "DiscountAmount", "TEXT(50)");
            }
  
            if (!CheckColumn("OpenTransactions", "RegisterTransactionStart")) 
            {
                DoUpdate("Alter Table", "OpenTransactions", "RegisterTransactionStart", "DATETIME");
            }
  
            if (!CheckColumn("OpenTransactions", "TransactionStart")) 
            {
                DoUpdate("Alter Table", "OpenTransactions", "TransactionStart", "DATETIME");
            }
  
            if (!CheckColumn("OpenTransactions", "TransactionEnd")) 
            {
                DoUpdate("Alter Table", "OpenTransactions", "TransactionEnd", "DATETIME");
            }
  
            if (!CheckColumn("OpenTransactions", "OpenTranId")) 
            {
                DoUpdate("Alter Table", "OpenTransactions", "OpenTranId", "NUMBER");
            }
  
            if (!CheckColumn("OpenTransactions", "MealOptions")) 
            {
                DoUpdate("Alter Table", "OpenTransactions", "MealOptions", "TEXT(255)");
            }
  
            if (!CheckColumn("OpenTransactions", "SlipRank")) 
            {
                DoUpdate("Alter Table", "OpenTransactions", "SlipRank", "NUMBER NULL");
            }
  
            if (!CheckColumn("OpenTransactions", "CustomerId")) 
            {
                DoUpdate("Alter Table", "OpenTransactions", "CustomerId", "TEXT(50) NULL");
            }
  
  
            //OrderId
  
            if (!CheckTable("OrderId")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE OrderId (OrderId NUMBER PRIMARY KEY)", "", "");
            }
  
  
  
  
            //OrderItem
  
            if (!CheckTable("OrderItem")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE OrderItem (OrderNumber TEXT(50), " + 
                "StockId TEXT(50), " + 
                "CONSTRAINT PkOI PRIMARY KEY (OrderNumber, StockId))", "", "");
            }
  
            if (!CheckColumn("OrderItem", "SupplierStockCode")) 
            {
                DoUpdate("Alter Table", "OrderItem", "SupplierStockCode", "TEXT(50)");
            }
  
            if (!CheckColumn("OrderItem", "Description")) 
            {
                DoUpdate("Alter Table", "OrderItem", "Description", "TEXT(50)");
            }
  
            if (!CheckColumn("OrderItem", "Quantity")) 
            {
                DoUpdate("Alter Table", "OrderItem", "Quantity", "CURRENCY");
            }
  
            if (!CheckColumn("OrderItem", "PreviousStockUnitCost")) 
            {
                DoUpdate("Alter Table", "OrderItem", "PreviousStockUnitCost", "CURRENCY");
            }
  
            if (!CheckColumn("OrderItem", "VATAmount")) 
            {
                DoUpdate("Alter Table", "OrderItem", "VATAmount", "CURRENCY");
            }
  
            if (!CheckColumn("OrderItem", "ItemStatus")) 
            {
                DoUpdate("Alter Table", "OrderItem", "ItemStatus", "TEXT(50)");
            }
  
            if (!CheckColumn("OrderItem", "InvoiceNumber")) 
            {
                DoUpdate("Alter Table", "OrderItem", "InvoiceNumber", "TEXT(50)");
            }
  
            if (!CheckColumn("OrderItem", "QuantityReceived")) 
            {
                DoUpdate("Alter Table", "OrderItem", "QuantityReceived", "CURRENCY");
            }
  
            if (!CheckColumn("OrderItem", "DateReceived")) 
            {
                DoUpdate("Alter Table", "OrderItem", "DateReceived", "DATETIME");
            }
  
            if (!CheckColumn("OrderItem", "OrderUnit")) 
            {
                DoUpdate("Alter Table", "OrderItem", "OrderUnit", "CURRENCY");
            }
  
            if (!CheckColumn("OrderItem", "DiscountReceived")) 
            {
                DoUpdate("Alter Table", "OrderItem", "DiscountReceived", "CURRENCY NULL");
            }
  
            if (!CheckColumn("OrderItem", "UpdatePrice")) 
            {
                DoUpdate("Alter Table", "OrderItem", "UpdatePrice", "TEXT(1) NULL");
            }
  
            if (!CheckColumn("OrderItem", "UpdatedPrice")) 
            {
                DoUpdate("Alter Table", "OrderItem", "UpdatedPrice", "CURRENCY NULL");
            }
  
            if (!CheckColumn("OrderItem", "StockUpdated")) 
            {
                DoUpdate("Alter Table", "OrderItem", "StockUpdated", "TEXT(1) NULL");
            }
  
            if (!CheckColumn("OrderItem", "InternalReference")) 
            {
                DoUpdate("Alter Table", "OrderItem", "InternalReference", "TEXT(50) NULL");
            }
  
  
  
            //Orders
  
            if (!CheckTable("Orders")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE Orders (CreditorName TEXT(50), " + 
                "OrderNumber TEXT(20), " + 
                "CONSTRAINT PkO PRIMARY KEY (CreditorName, OrderNumber))", "", "");
            }
  
            if (!CheckColumn("Orders", "OrderDate")) 
            {
                DoUpdate("Alter Table", "Orders", "OrderDate", "DATETIME");
            }
  
            if (!CheckColumn("Orders", "OrderStatus")) 
            {
                DoUpdate("Alter Table", "Orders", "OrderStatus", "TEXT(50)");
            }
  
            if (!CheckColumn("Orders", "OrderNotes")) 
            {
                DoUpdate("Alter Table", "Orders", "OrderNotes", "TEXT(255)");
            }
  
  
  
  
  
  
            //Performance
  
            if (!CheckTable("Performance")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE Performance (PerformanceId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("Performance", "PerformanceName")) 
            {
                DoUpdate("Alter Table", "Performance", "PerformanceName", "TEXT(50)");
            }
  
            if (!CheckColumn("Performance", "PerformanceStartDate")) 
            {
                DoUpdate("Alter Table", "Performance", "PerformanceStartDate", "DATETIME");
            }
  
            if (!CheckColumn("Performance", "PerformanceEndDate")) 
            {
                DoUpdate("Alter Table", "Performance", "PerformanceEndDate", "DATETIME");
            }
  
            if (!CheckColumn("Performance", "StockId")) 
            {
                DoUpdate("Alter Table", "Performance", "StockId", "TEXT(50)");
            }
  
            if (!CheckColumn("Performance", "TheaterId")) 
            {
                DoUpdate("Alter Table", "Performance", "TheaterId", "TEXT(50)");
            }
  
  
  
  
  
  
  
            //Person
  
            if (!CheckTable("Person")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE Person (PersonId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("Person", "CardNumber")) 
            {
                DoUpdate("Alter Table", "Person", "CardNumber", "TEXT(50)");
            }
  
            if (!CheckColumn("Person", "PersonType")) 
            {
                DoUpdate("Alter Table", "Person", "PersonType", "TEXT(50)");
            }
  
            if (!CheckColumn("Person", "PersonName")) 
            {
                DoUpdate("Alter Table", "Person", "PersonName", "TEXT(50)");
            }
  
            if (!CheckColumn("Person", "PersonSurname")) 
            {
                DoUpdate("Alter Table", "Person", "PersonSurname", "TEXT(50)");
            }
  
            if (!CheckColumn("Person", "AddressLine1")) 
            {
                DoUpdate("Alter Table", "Person", "AddressLine1", "TEXT(100)");
            }
  
            if (!CheckColumn("Person", "AddressLine2")) 
            {
                DoUpdate("Alter Table", "Person", "AddressLine2", "TEXT(100)");
            }
  
            if (!CheckColumn("Person", "AddressLine3")) 
            {
                DoUpdate("Alter Table", "Person", "AddressLine3", "TEXT(100)");
            }
  
            if (!CheckColumn("Person", "PostalCode")) 
            {
                DoUpdate("Alter Table", "Person", "PostalCode", "TEXT(5)");
            }
  
            if (!CheckColumn("Person", "ContactNumber1")) 
            {
                DoUpdate("Alter Table", "Person", "ContactNumber1", "TEXT(30)");
            }
  
            if (!CheckColumn("Person", "ContactNumber2")) 
            {
                DoUpdate("Alter Table", "Person", "ContactNumber2", "TEXT(30)");
            }
  
            if (!CheckColumn("Person", "FaxNumber")) 
            {
                DoUpdate("Alter Table", "Person", "FaxNumber", "TEXT(30)");
            }
  
            if (!CheckColumn("Person", "Title")) 
            {
                DoUpdate("Alter Table", "Person", "Title", "TEXT(20)");
            }
  
            if (!CheckColumn("Person", "DateRegistered")) 
            {
                DoUpdate("Alter Table", "Person", "DateRegistered", "TEXT(20)");
            }
  
            if (!CheckColumn("Person", "PersonContact1")) 
            {
                DoUpdate("Alter Table", "Person", "PersonContact1", "TEXT(50)");
            }
  
            if (!CheckColumn("Person", "PersonContact2")) 
            {
                DoUpdate("Alter Table", "Person", "PersonContact2", "TEXT(50)");
            }
  
            if (!CheckColumn("Person", "IDNumber")) 
            {
                DoUpdate("Alter Table", "Person", "IDNumber", "TEXT(20)");
            }
  
            if (!CheckColumn("Person", "ImagePath")) 
            {
                DoUpdate("Alter Table", "Person", "ImagePath", "TEXT(255)");
            }
  
            if (!CheckColumn("Person", "Notes")) 
            {
                DoUpdate("Alter Table", "Person", "Notes", "TEXT(255)");
            }
  
            if (!CheckColumn("Person", "Status")) 
            {
                DoUpdate("Alter Table", "Person", "Status", "TEXT(10)");
            }
  
            if (!CheckColumn("Person", "StayInHostel")) 
            {
                DoUpdate("Alter Table", "Person", "StayInHostel", "TEXT(1) NULL");
            }
  
            if (!CheckColumn("Person", "PicSize")) 
            {
                DoUpdate("Alter Table", "Person", "PicSize", "TEXT(50) NULL");
            }
  
  
  
            //PersonCard
  
            if (!CheckTable("PersonCard")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE PersonCard (PersonCardId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("PersonCard", "CardNumber")) 
            {
                DoUpdate("Alter Table", "PersonCard", "CardNumber", "TEXT(50)");
            }
  
            if (!CheckColumn("PersonCard", "PersonId")) 
            {
                DoUpdate("Alter Table", "PersonCard", "PersonId", "NUMBER");
            }
  
            if (!CheckColumn("PersonCard", "ActiveCard")) 
            {
                DoUpdate("Alter Table", "PersonCard", "ActiveCard", "TEXT(1)");
            }
  
  
  
  
            //PersonHistory
  
            if (!CheckTable("PersonHistory")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE PersonHistory (PersonHistoryId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("PersonHistory", "PersonId")) 
            {
                DoUpdate("Alter Table", "PersonHistory", "PersonId", "NUMBER");
            }
  
            if (!CheckColumn("PersonHistory", "TransactionDate")) 
            {
                DoUpdate("Alter Table", "PersonHistory", "TransactionDate", "DATETIME");
            }
  
            if (!CheckColumn("PersonHistory", "TransactionDescription")) 
            {
                DoUpdate("Alter Table", "PersonHistory", "TransactionDescription", "TEXT(255)");
            }
  
  
  
  
            //ProductSpecials
  
            if (!CheckTable("ProductSpecials")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE ProductSpecials (StockId TEXT(50) PRIMARY KEY)", "", "");
            }
    
            if (!CheckColumn("ProductSpecials", "DiscountedSalePrice")) 
            {
                DoUpdate("Alter Table", "ProductSpecials", "DiscountedSalePrice", "CURRENCY");
            }
  
            if (!CheckColumn("ProductSpecials", "DiscountType")) 
            {
                DoUpdate("Alter Table", "ProductSpecials", "DiscountType", "TEXT(30)");
            }
  
            if (!CheckColumn("ProductSpecials", "DiscountValue")) 
            {
                DoUpdate("Alter Table", "ProductSpecials", "DiscountValue", "CURRENCY");
            }
  
            if (!CheckColumn("ProductSpecials", "ValidFrom")) 
            {
                DoUpdate("Alter Table", "ProductSpecials", "ValidFrom", "DATETIME");
            }
  
            if (!CheckColumn("ProductSpecials", "ValidTo")) 
            {
                DoUpdate("Alter Table", "ProductSpecials", "ValidTo", "DATETIME");
            }
  
  
            //QueCard
  
  
            if (!CheckTable("QueCard")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE QueCard (QueCardId NUMBER, " + 
                "LineNumber NUMBER, " + 
                "CONSTRAINT PkQC PRIMARY KEY (QueCardId, LineNumber))", "", "");
            }
  
            if (!CheckColumn("QueCard", "LineDesc")) 
            {
                DoUpdate("Alter Table", "QueCard", "LineDesc", "TEXT(255)");
            }
  
  
            if (!CheckColumn("QueCard", "LineType")) 
            {
                DoUpdate("Alter Table", "QueCard", "LineType", "TEXT(1)");
            }
  
  
            if (!CheckColumn("QueCard", "LineLinkItem")) 
            {
                DoUpdate("Alter Table", "QueCard", "LineLinkItem", "NUMBER");
            }
  
  
  
  
            //Quote
  
            if (!CheckTable("Quote"))
            {
                DoUpdate("Create Multiple", "CREATE TABLE Quote (DebtorName TEXT(50), " + 
                "QuoteNumber TEXT(20), " + 
                "CONSTRAINT PkQuote PRIMARY KEY (DebtorName, QuoteNumber))", "", "");
            }
  
            if (!CheckColumn("Quote", "QuoteDate")) 
            {
                DoUpdate("Alter Table", "Quote", "QuoteDate", "DATETIME");
            }
  
            if (!CheckColumn("Quote", "QuoteNotes")) 
            {
                DoUpdate("Alter Table", "Quote", "QuoteNotes", "TEXT(255)");
            }
  
            if (!CheckColumn("Quote", "Terms")) 
            {
                DoUpdate("Alter Table", "Quote", "Terms", "TEXT(255)");
            }
  
            if (!CheckColumn("Quote", "DeliveryAddress")) 
            {
                DoUpdate("Alter Table", "Quote", "DeliveryAddress", "TEXT(1)");
            }
  
            if (!CheckColumn("Quote", "DiscountPercentage"))
            {
                DoUpdate("Alter Table", "Quote", "DiscountPercentage", "TEXT(50)");
            }
  
            if (!CheckColumn("Quote", "SiteName")) 
            {
                DoUpdate("Alter Table", "Quote", "SiteName", "TEXT(50)");
            }
  
            if (!CheckColumn("Quote", "DeliveryDate")) 
            {
                DoUpdate("Alter Table", "Quote", "DeliveryDate", "DATETIME");
            }
  
            if (!CheckColumn("Quote", "Status")) 
            {
                DoUpdate("Alter Table", "Quote", "Status", "TEXT(20) NULL");
            }
  
  
  


            //QuoteItem
  
            if (!CheckTable("QuoteItem")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE QuoteItem (QuoteNumber TEXT(50), " + 
                "StockId TEXT(50), " + 
                "Colour TEXT(50), " + 
                "CONSTRAINT PkQuoteItem PRIMARY KEY (QuoteNumber, StockId, Colour))", "", "");
            }
  
            if (!CheckColumn("QuoteItem", "Description")) 
            {
                DoUpdate("Alter Table", "QuoteItem", "Description", "TEXT(50)");
            }
  
            if (!CheckColumn("QuoteItem", "Quantity")) 
            {
                DoUpdate("Alter Table", "QuoteItem", "Quantity", "CURRENCY");
            }
  
            if (!CheckColumn("QuoteItem", "M2")) 
            {
                DoUpdate("Alter Table", "QuoteItem", "M2", "CURRENCY");
            }
  
            if (!CheckColumn("QuoteItem", "Price")) 
            {
                DoUpdate("Alter Table", "QuoteItem", "Price", "CURRENCY");
            }
  
            if (!CheckColumn("QuoteItem", "Weight")) 
            {
                DoUpdate("Alter Table", "QuoteItem", "Weight", "CURRENCY");
            }
  
            if (!CheckColumn("QuoteItem", "QuantityPerM2")) 
            {
                DoUpdate("Alter Table", "QuoteItem", "QuantityPerM2", "CURRENCY");
            }
  
            if (!CheckColumn("QuoteItem", "WeightPerM2")) 
            {
                DoUpdate("Alter Table", "QuoteItem", "WeightPerM2", "CURRENCY");
            }
  
            if (!CheckColumn("QuoteItem", "DeliveryCharge")) 
            {
                DoUpdate("Alter Table", "QuoteItem", "DeliveryCharge", "CURRENCY");
            }




            //QuoteItemScanner
  
            if (!CheckTable("QuoteItemScanner"))
            {
                DoUpdate("Create Multiple", "CREATE TABLE QuoteItemScanner (QuoteNumber TEXT(50), " + 
                "StockId TEXT(50), " + 
                "CONSTRAINT PkQuoteItemSc PRIMARY KEY (QuoteNumber, StockId))", "", "");
            }
  
            if (!CheckColumn("QuoteItemScanner", "Description")) 
            {
                DoUpdate("Alter Table", "QuoteItemScanner", "Description", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("QuoteItemScanner", "Quantity")) 
            {
                DoUpdate("Alter Table", "QuoteItemScanner", "Quantity", "CURRENCY");
            }
  
            if (!CheckColumn("QuoteItemScanner", "Price")) 
            {
                DoUpdate("Alter Table", "QuoteItemScanner", "Price", "CURRENCY NULL");
            }
  
            if (!CheckColumn("QuoteItemScanner", "Discount")) 
            {
                DoUpdate("Alter Table", "QuoteItemScanner", "Discount", "CURRENCY NULL");
            }
  
  
  
            //QuoteItemStandard
  
            if (!CheckTable("QuoteItemStandard")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE QuoteItemStandard (QuoteNumber TEXT(50), " + 
                "StockId TEXT(50), " + 
                "CONSTRAINT PkQuoteItemS PRIMARY KEY (QuoteNumber, StockId))", "", "");
            }
  
            if (!CheckColumn("QuoteItemStandard", "Description")) 
            {
                DoUpdate("Alter Table", "QuoteItemStandard", "Description", "TEXT(50)");
            }
  
            if (!CheckColumn("QuoteItemStandard", "Quantity")) 
            {
                DoUpdate("Alter Table", "QuoteItemStandard", "Quantity", "CURRENCY");
            }
  
            if (!CheckColumn("QuoteItemStandard", "Price")) 
            {
                DoUpdate("Alter Table", "QuoteItemStandard", "Price", "CURRENCY");
            }
  
            if (!CheckColumn("QuoteItemStandard", "Discount")) 
            {
                DoUpdate("Alter Table", "QuoteItemStandard", "Discount", "CURRENCY NULL");
            }
  
            if (!CheckColumn("QuoteItemStandard", "OrderDate")) 
            {
                DoUpdate("Alter Table", "QuoteItemStandard", "OrderDate", "DATETIME NULL");
            }
  
            if (!CheckColumn("QuoteItemStandard", "InternalReference")) 
            {
                DoUpdate("Alter Table", "QuoteItemStandard", "InternalReference", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("QuoteItemStandard", "ExpectedDeliveryDate")) 
            {
                DoUpdate("Alter Table", "QuoteItemStandard", "ExpectedDeliveryDate", "DATETIME NULL");
            }
  
            if (!CheckColumn("QuoteItemStandard", "OrderNumber")) 
            {
                DoUpdate("Alter Table", "QuoteItemStandard", "OrderNumber", "TEXT(20) NULL");
            }
  
            if (!CheckColumn("QuoteItemStandard", "ItemStatus")) 
            {
                DoUpdate("Alter Table", "QuoteItemStandard", "ItemStatus", "TEXT(20) NULL");
            }
  
  
  
  
  
            DoUpdate("Create Multiple", "ALTER TABLE QuoteItemStandard DROP CONSTRAINT PkQuoteItemS", "", "");
  
            //Recipy
  
            if (!CheckTable("Recipy")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE Recipy (RecipyId TEXT(50), " + 
                "StockId TEXT(50), " + 
                "CONSTRAINT PkRecipy PRIMARY KEY (RecipyId, StockId))", "", "");
            }
  
            if (!CheckColumn("Recipy", "RecipyQuantity")) 
            {
                DoUpdate("Alter Table", "Recipy", "RecipyQuantity", "CURRENCY");
            }
  
            if (!CheckColumn("Recipy", "PrintDetail")) 
            {
                DoUpdate("Alter Table", "Recipy", "PrintDetail", "TEXT(1) NULL");
            }
  
  
  
  
            //RecordId
  
            if (!CheckTable("RecordId")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE RecordId (RecId NUMBER PRIMARY KEY)", "", "");
            }
  
  
  
  
  
            //RestaurantGratuity
  
            if (!CheckTable("RestaurantGratuity")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE RestaurantGratuity (GratuityId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("RestaurantGratuity", "ShiftId")) 
            {
                DoUpdate("Alter Table", "RestaurantGratuity", "ShiftId", "NUMBER");
            }
  
            if (!CheckColumn("RestaurantGratuity", "GratuityAmount")) 
            {
                DoUpdate("Alter Table", "RestaurantGratuity", "GratuityAmount", "CURRENCY");
            }
  
            if (!CheckColumn("RestaurantGratuity", "GratuityDate")) 
            {
                DoUpdate("Alter Table", "RestaurantGratuity", "GratuityDate", "DATETIME");
            }
  
            if (!CheckColumn("RestaurantGratuity", "UserName")) 
            {
                DoUpdate("Alter Table", "RestaurantGratuity", "UserName", "TEXT(50)");
            }
  
  
  
  
            //RestaurantSplitBill
  
  
            if (!CheckTable("RestaurantSplitBill")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE RestaurantSplitBill (SplitId COUNTER, " + 
                "SeatingId NUMBER, " + 
                "RegisterTransactionId NUMBER, " + 
                "CONSTRAINT PkRSB PRIMARY KEY (RegisterTransactionId, SeatingId, SplitId))", "", "");
            }
  
            if (!CheckColumn("RestaurantSplitBill", "SeatingId")) 
            {
                DoUpdate("Alter Table", "RestaurantSplitBill", "SeatingId", "NUMBER");
            }
  
            if (!CheckColumn("RestaurantSplitBill", "RegisterTransactionId")) 
            {
                DoUpdate("Alter Table", "RestaurantSplitBill", "RegisterTransactionId", "NUMBER");
            }
  
            //if (DBType.Trim () == "MySQL")
            //{
            //    DoUpdate("Create Multiple", "ALTER TABLE `quicksale`.`restaurantsplitbill` DROP PRIMARY KEY, ADD PRIMARY KEY  USING BTREE(`SplitId`, `SeatingId`, `RegisterTransactionId`);", "", "");
            //}
  
            if (!CheckColumn("RestaurantSplitBill", "UserName")) 
            {
                DoUpdate("Alter Table", "RestaurantSplitBill", "UserName", "TEXT(50)");
            }
  
            if (!CheckColumn("RestaurantSplitBill", "StockId")) 
            {
                DoUpdate("Alter Table", "RestaurantSplitBill", "StockId", "TEXT(50)");
            }
  
            if (!CheckColumn("RestaurantSplitBill", "Description")) 
            {
                DoUpdate("Alter Table", "RestaurantSplitBill", "Description", "TEXT(50)");
            }
  
            if (!CheckColumn("RestaurantSplitBill", "Quantity"))
            {
                DoUpdate("Alter Table", "RestaurantSplitBill", "Quantity", "CURRENCY");
            }
  
            if (!CheckColumn("RestaurantSplitBill", "UnitPrice")) 
            {
                DoUpdate("Alter Table", "RestaurantSplitBill", "UnitPrice", "TEXT(50)");
            }
  
            if (!CheckColumn("RestaurantSplitBill", "VATAmount")) 
            {
                DoUpdate("Alter Table", "RestaurantSplitBill", "VATAmount", "TEXT(50)");
            }
  
            if (!CheckColumn("RestaurantSplitBill", "VATPercentage")) 
            {
                DoUpdate("Alter Table", "RestaurantSplitBill", "VATPercentage", "TEXT(50)");
            }
  
            if (!CheckColumn("RestaurantSplitBill", "TotalAmount")) 
            {
                DoUpdate("Alter Table", "RestaurantSplitBill", "TotalAmount", "TEXT(50)");
            }
  
            if (!CheckColumn("RestaurantSplitBill", "DiscountAmount")) 
            {
                DoUpdate("Alter Table", "RestaurantSplitBill", "DiscountAmount", "TEXT(50)");
            }
  
            if (!CheckColumn("RestaurantSplitBill", "Customer")) 
            {
                DoUpdate("Alter Table", "RestaurantSplitBill", "Customer", "NUMBER");
            }
  
            if (!CheckColumn("RestaurantSplitBill", "OriginalQty")) 
            {
                DoUpdate("Alter Table", "RestaurantSplitBill", "OriginalQty", "CURRENCY");
            }
  
  
  
            //Role
  
            if (!CheckTable("Role")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE Role (Role TEXT(50) PRIMARY KEY)", "", "");
            }
  
  
  
  
  
            //SaleItemScanner
  
            if (!CheckTable("SaleItemScanner")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE SaleItemScanner (SaleNumber TEXT(50), " + 
                "StockId TEXT(50), " + 
                "CONSTRAINT PkSaleItemSc PRIMARY KEY (SaleNumber, StockId))", "", "");
            }
  
            if (!CheckColumn("SaleItemScanner", "Description")) 
            {
                DoUpdate("Alter Table", "SaleItemScanner", "Description", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("SaleItemScanner", "Quantity")) 
            {
                DoUpdate("Alter Table", "SaleItemScanner", "Quantity", "CURRENCY");
            }
  
            if (!CheckColumn("SaleItemScanner", "Price")) 
            {
                DoUpdate("Alter Table", "SaleItemScanner", "Price", "CURRENCY NULL");
            }
  
  
  
  
  
            //SalesCode
  
            if (!CheckTable("SalesCode")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE SalesCode (SalesCode TEXT(10), " + 
                "CONSTRAINT PkSalesCode PRIMARY KEY (SalesCode))", "", "");
            }
  
            if (!CheckColumn("SalesCode", "Description")) 
            {
                DoUpdate("Alter Table", "SalesCode", "Description", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("SalesCode", "UserName")) 
            {
                DoUpdate("Alter Table", "SalesCode", "UserName", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("SalesCode", "Commission")) 
            {
                DoUpdate("Alter Table", "SalesCode", "Commission", "CURRENCY");
            }
  
  
  
  
  
  
            //SAPSMember
  
            if (!CheckTable("SAPSMember")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE SAPSMember (SAPSMemberId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("SAPSMember", "MemberName")) 
            {
                DoUpdate("Alter Table", "SAPSMember", "MemberName", "TEXT(50)");
            }
  
            if (!CheckColumn("SAPSMember", "Rank")) 
            {
                DoUpdate("Alter Table", "SAPSMember", "Rank", "TEXT(50)");
            }
  
            if (!CheckColumn("SAPSMember", "ContactNumber1")) 
            {
                DoUpdate("Alter Table", "SAPSMember", "ContactNumber1", "TEXT(30)");
            }
  
            if (!CheckColumn("SAPSMember", "ContactNumber2")) 
            {
                DoUpdate("Alter Table", "SAPSMember", "ContactNumber2", "TEXT(30)");
            }
  
            if (!CheckColumn("SAPSMember", "FaxNumber")) 
            {
                DoUpdate("Alter Table", "SAPSMember", "FaxNumber", "TEXT(30)");
            }
  
            if (!CheckColumn("SAPSMember", "Station")) 
            {
                DoUpdate("Alter Table", "SAPSMember", "Station", "TEXT(50)");
            }
  
            if (!CheckColumn("SAPSMember", "DateRegistered")) 
            {
                DoUpdate("Alter Table", "SAPSMember", "DateRegistered", "TEXT(20)");
            }
  
            if (!CheckColumn("SAPSMember", "ForceNumber")) 
            {
                DoUpdate("Alter Table", "SAPSMember", "ForceNumber", "TEXT(20)");
            }
  
            //Shift
  
            if (!CheckTable("Shift")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE Shift (ShiftId NUMBER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("Shift", "UserName")) 
            {
                DoUpdate("Alter Table", "Shift", "UserName", "TEXT(50)");
            }
  
            if (!CheckColumn("Shift", "ShiftStart")) 
            {
                DoUpdate("Alter Table", "Shift", "ShiftStart", "DATETIME");
            }
  
            if (!CheckColumn("Shift", "ShiftEnd")) 
            {
                DoUpdate("Alter Table", "Shift", "ShiftEnd", "DATETIME");
            }
  
            if (!CheckColumn("Shift", "CashAmountStart")) 
            {
                DoUpdate("Alter Table", "Shift", "CashAmountStart", "TEXT(50)");
            }
  
            if (!CheckColumn("Shift", "CreditCardAmountStart")) 
            {
                DoUpdate("Alter Table", "Shift", "CreditCardAmountStart", "TEXT(50)");
            }
  
            if (!CheckColumn("Shift", "ChequeAmountStart")) 
            {
                DoUpdate("Alter Table", "Shift", "ChequeAmountStart", "TEXT(50)");
            }
  
            if (!CheckColumn("Shift", "DebitCardAmountStart")) 
            {
                DoUpdate("Alter Table", "Shift", "DebitCardAmountStart", "TEXT(50)");
            }
  
            if (!CheckColumn("Shift", "AccountAmountStart")) 
            {
                DoUpdate("Alter Table", "Shift", "AccountAmountStart", "TEXT(50)");
            }
  
            if (!CheckColumn("Shift", "OtherAmountStart")) 
            {
                DoUpdate("Alter Table", "Shift", "OtherAmountStart", "TEXT(50)");
            }
  
            if (!CheckColumn("Shift", "CashAmountEnd")) 
            {
                DoUpdate("Alter Table", "Shift", "CashAmountEnd", "TEXT(50)");
            }
  
            if (!CheckColumn("Shift", "CreditCardAmountEnd")) 
            {
                DoUpdate("Alter Table", "Shift", "CreditCardAmountEnd", "TEXT(50)");
            }
  
            if (!CheckColumn("Shift", "ChequeAmountEnd")) 
            {
                DoUpdate("Alter Table", "Shift", "ChequeAmountEnd", "TEXT(50)");
            }
  
            if (!CheckColumn("Shift", "DebitCardAmountEnd")) 
            {
                DoUpdate("Alter Table", "Shift", "DebitCardAmountEnd", "TEXT(50)");
            }
  
            if (!CheckColumn("Shift", "AccountAmountEnd")) 
            {
                DoUpdate("Alter Table", "Shift", "AccountAmountEnd", "TEXT(50)");
            }
  
            if (!CheckColumn("Shift", "OtherAmountEnd")) 
            {
                DoUpdate("Alter Table", "Shift", "OtherAmountEnd", "TEXT(50)");
            }
  
  
  
            //Stock
  
            if (!CheckTable("Stock")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE Stock (StockId TEXT(50) PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("Stock", "Description")) 
            {
                DoUpdate("Alter Table", "Stock", "Description", "TEXT(100)");
            }
  
            if (!CheckColumn("Stock", "SupplierStockCode")) 
            {
                DoUpdate("Alter Table", "Stock", "SupplierStockCode", "TEXT(100)");
            }
  
            if (!CheckColumn("Stock", "StockCategory")) 
            {
                DoUpdate("Alter Table", "Stock", "StockCategory", "TEXT(100)");
            }

            if (!CheckColumn("Stock", "StockCategoryID"))
            {
                DoUpdate("Alter Table", "Stock", "StockCategoryID", "NUMBER");
            }
  
            if (!CheckColumn("Stock", "StockSubCategory")) 
            {
                DoUpdate("Alter Table", "Stock", "StockSubCategory", "TEXT(100) NULL");
            }

            if (!CheckColumn("Stock", "StockSubCategoryID"))
            {
                DoUpdate("Alter Table", "Stock", "StockSubCategoryID", "NUMBER NULL");
            }

            if (!CheckColumn("Stock", "StockLocation")) 
            {
                DoUpdate("Alter Table", "Stock", "StockLocation", "TEXT(100) NULL");
            }

            if (!CheckColumn("Stock", "StockLocationID"))
            {
                DoUpdate("Alter Table", "Stock", "StockLocationID", "NUMBER NULL");
            }

            if (!CheckColumn("Stock", "BinNumber")) 
            {
                DoUpdate("Alter Table", "Stock", "BinNumber", "TEXT(100) NULL");
            }

            if (!CheckColumn("Stock", "BinNumberID"))
            {
                DoUpdate("Alter Table", "Stock", "BinNumberID", "NUMBER NULL");
            }

            if (!CheckColumn("Stock", "StockLevel")) 
            {
                DoUpdate("Alter Table", "Stock", "StockLevel", "CURRENCY");
            }
  
            if (!CheckColumn("Stock", "MinimumLevel")) 
            {
                DoUpdate("Alter Table", "Stock", "MinimumLevel", "CURRENCY NULL");
            }
  
            if (!CheckColumn("Stock", "MaximumLevel")) 
            {
                DoUpdate("Alter Table", "Stock", "MaximumLevel", "CURRENCY NULL");
            }
  
            if (!CheckColumn("Stock", "StockOrderUnit")) 
            {
                DoUpdate("Alter Table", "Stock", "StockOrderUnit", "CURRENCY");
            }

            if (!CheckColumn("Stock", "StockOrderUnitID"))
            {
                DoUpdate("Alter Table", "Stock", "StockOrderUnitID", "NUMBER NULL");
            }

            if (!CheckColumn("Stock", "StockUnitCost")) 
            {
                DoUpdate("Alter Table", "Stock", "StockUnitCost", "CURRENCY");
            }
  
            if (!CheckColumn("Stock", "StockSaleUnit")) 
            {
                DoUpdate("Alter Table", "Stock", "StockSaleUnit", "CURRENCY");
            }

            if (!CheckColumn("Stock", "StockSaleUnitID"))
            {
                DoUpdate("Alter Table", "Stock", "StockSaleUnitID", "NUMBER NULL");
            }

            if (!CheckColumn("Stock", "UnitCost")) 
            {
                DoUpdate("Alter Table", "Stock", "UnitCost", "CURRENCY");
            }
  
            if (!CheckColumn("Stock", "UnitMarkup")) 
            {
                DoUpdate("Alter Table", "Stock", "UnitMarkup", "CURRENCY");
            }
  
            if (!CheckColumn("Stock", "UnitSalePrice")) 
            {
                DoUpdate("Alter Table", "Stock", "UnitSalePrice", "CURRENCY");
            }
  
            if (!CheckColumn("Stock", "AddVAT")) 
            {
                DoUpdate("Alter Table", "Stock", "AddVAT", "TEXT(1)");
            }
  
            if (!CheckColumn("Stock", "PreviousStockUnitCost")) 
            {
                DoUpdate("Alter Table", "Stock", "PreviousStockUnitCost", "CURRENCY");
            }
  
            if (!CheckColumn("Stock", "Supplier1")) 
            {
                DoUpdate("Alter Table", "Stock", "Supplier1", "TEXT(50)");
            }

            if (!CheckColumn("Stock", "Supplier1ID"))
            {
                DoUpdate("Alter Table", "Stock", "Supplier1ID", "NUMBER NULL");
            }

            if (!CheckColumn("Stock", "Supplier2")) 
            {
                DoUpdate("Alter Table", "Stock", "Supplier2", "TEXT(50)");
            }

            if (!CheckColumn("Stock", "Supplier2ID"))
            {
                DoUpdate("Alter Table", "Stock", "Supplier2ID", "NUMBER NULL");
            }

            if (!CheckColumn("Stock", "AllowDiscount")) 
            {
                DoUpdate("Alter Table", "Stock", "AllowDiscount", "CURRENCY");
            }
  
            if (!CheckColumn("Stock", "Guarantee")) 
            {
                DoUpdate("Alter Table", "Stock", "Guarantee", "TEXT(50)");
            }

            if (!CheckColumn("Stock", "GuaranteeID"))
            {
                DoUpdate("Alter Table", "Stock", "GuaranteeID", "NUMBER NULL");
            }

            if (!CheckColumn("Stock", "LastSold")) 
            {
                DoUpdate("Alter Table", "Stock", "LastSold", "DATETIME");
            }
  
            if (!CheckColumn("Stock", "DiscountType")) 
            {
                DoUpdate("Alter Table", "Stock", "DiscountType", "TEXT(1)");
            }
  
            if (!CheckColumn("Stock", "ManualPrice")) 
            {
                DoUpdate("Alter Table", "Stock", "ManualPrice", "TEXT(1)");
            }
  
            if (!CheckColumn("Stock", "Scale")) 
            {
                DoUpdate("Alter Table", "Stock", "Scale", "TEXT(1)");
            }
  
            if (!CheckColumn("Stock", "ScaleFunction")) 
            {
                DoUpdate("Alter Table", "Stock", "ScaleFunction", "TEXT(1)");
            }
  
            if (!CheckColumn("Stock", "ScaleDigits")) 
            {
                DoUpdate("Alter Table", "Stock", "ScaleDigits", "TEXT(20)");
            }
  
            if (!CheckColumn("Stock", "ScaleVAT")) 
            {
                DoUpdate("Alter Table", "Stock", "ScaleVAT", "TEXT(1)");
            }
  
            if (!CheckColumn("Stock", "PhysicalStock")) 
            {
                DoUpdate("Alter Table", "Stock", "PhysicalStock", "TEXT(1)");
            }
    
            if (!CheckColumn("Stock", "SerialNumberTracking")) 
            {
                DoUpdate("Alter Table", "Stock", "SerialNumberTracking", "TEXT(1)");
            }
  
            if (!CheckColumn("Stock", "UnitWholeSalePrice")) 
            {
                DoUpdate("Alter Table", "Stock", "UnitWholeSalePrice", "CURRENCY");
            }
  
            if (!CheckColumn("Stock", "Released")) 
            {
                DoUpdate("Alter Table", "Stock", "Released", "TEXT(1) NULL");
            }
  
            if (!CheckColumn("Stock", "LabelPricing")) 
            {
                DoUpdate("Alter Table", "Stock", "LabelPricing", "TEXT(100) NULL");
            }
  
            if (!CheckColumn("Stock", "ImagePath")) 
            {
                DoUpdate("Alter Table", "Stock", "ImagePath", "TEXT(100) NULL");
            }
  
            if (!CheckColumn("Stock", "ItemPrinter")) 
            {
                DoUpdate("Alter Table", "Stock", "ItemPrinter", "TEXT(1) NULL");
            }
  
            if (!CheckColumn("Stock", "Notes")) 
            {
                DoUpdate("Alter Table", "Stock", "Notes", "TEXT(200) NULL");
            }
  
            if (!CheckColumn("Stock", "MultipleRegisterEntries")) 
            {
                DoUpdate("Alter Table", "Stock", "MultipleRegisterEntries", "TEXT(1) NULL");
            }
  
            if (!CheckColumn("Stock", "SlipRank")) 
            {
                DoUpdate("Alter Table", "Stock", "SlipRank", "NUMBER NULL");
            }
  
            if (!CheckColumn("Stock", "CustomerStatementCategory")) 
            {
                DoUpdate("Alter Table", "Stock", "CustomerStatementCategory", "TEXT(100) NULL");
            }

            if (!CheckColumn("Stock", "CustomerStatementCategoryID"))
            {
                DoUpdate("Alter Table", "Stock", "CustomerStatementCategoryID", "NUMBER NULL");
            }

            if (!CheckColumn("Stock", "ShelfLive")) 
            {
                DoUpdate("Alter Table", "Stock", "ShelfLive", "TEXT(3) NULL");
            }
  
            if (!CheckColumn("Stock", "ScalePLUStart")) 
            {
                DoUpdate("Alter Table", "Stock", "ScalePLUStart", "NUMBER NULL");
            }
  
            if (!CheckColumn("Stock", "ScalePLULength")) 
            {
                DoUpdate("Alter Table", "Stock", "ScalePLULength", "NUMBER NULL");
            }
  
            if (!CheckColumn("Stock", "SMItem")) 
            {
                DoUpdate("Alter Table", "Stock", "SMItem", "TEXT(1) NULL");
            }
  
            if (!CheckColumn("Stock", "SecondryStockId")) 
            {
                DoUpdate("Alter Table", "Stock", "SecondryStockId", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("Stock", "ScalePriceDecimals")) 
            {
                DoUpdate("Alter Table", "Stock", "ScalePriceDecimals", "NUMBER NULL");
            }
  
            if (!CheckColumn("Stock", "ParentCostStockId")) 
            {
                DoUpdate("Alter Table", "Stock", "ParentCostStockId", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("Stock", "MinimumGrossProfit")) 
            {
                DoUpdate("Alter Table", "Stock", "MinimumGrossProfit", "CURRENCY NULL");
            }
  
            if (!CheckColumn("Stock", "NegativeItem")) 
            {
                DoUpdate("Alter Table", "Stock", "NegativeItem", "TEXT(1) NULL");
            }
  
            if (!CheckColumn("Stock", "ConsolidateRecipe")) 
            {
                DoUpdate("Alter Table", "Stock", "ConsolidateRecipe", "TEXT(1) NULL");
            }
  
            if (!CheckColumn("Stock", "MaxCommissionPerc")) 
            {
                DoUpdate("Alter Table", "Stock", "MaxCommissionPerc", "NUMBER NULL");
            }
  
            if (!CheckColumn("Stock", "InternetCafeItem")) 
            {
                DoUpdate("Alter Table", "Stock", "InternetCafeItem", "TEXT(1) NULL");
            }
  
            if (!CheckColumn("Stock", "DefaultQuantity")) 
            {
                DoUpdate("Alter Table", "Stock", "DefaultQuantity", "NUMBER NULL");
            }

            if (!CheckColumn("Stock", "Deleted"))
            {
                DoUpdate("Alter Table", "Stock", "Deleted", "TEXT(1) NULL");
            }
  
            DoUpdate("Create Multiple", "alter table stock alter column UnitSalePrice double", "", "");

  
            //StockAttributes
  
            if (!CheckTable("StockAttributes")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE StockAttributes (StockId TEXT(50) PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("StockAttributes", "Quantity")) 
            {
                DoUpdate("Alter Table", "StockAttributes", "Quantity", "NUMBER");
            }
  
            if (!CheckColumn("StockAttributes", "Weight")) 
            {
                DoUpdate("Alter Table", "StockAttributes", "Weight", "CURRENCY");
            }
  
            if (!CheckColumn("StockAttributes", "Colour")) 
            {
                DoUpdate("Alter Table", "StockAttributes", "Colour", "TEXT(50)");
            }
  
  
  
            //StockChange
  
            if (!CheckTable("StockChange")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE StockChange (StockId TEXT(50) NULL)", "", "");
            }
  
  
  
            //StockHistory
  
            if (!CheckTable("StockHistory")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE StockHistory (StockHistoryId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("StockHistory", "StockId")) 
            {
                DoUpdate("Alter Table", "StockHistory", "StockId", "TEXT(50)");
            }
  
            if (!CheckColumn("StockHistory", "Description")) 
            {
                DoUpdate("Alter Table", "StockHistory", "Description", "TEXT(100)");
            }
  
            if (!CheckColumn("StockHistory", "StockLevel")) 
            {
                DoUpdate("Alter Table", "StockHistory", "StockLevel", "CURRENCY");
            }
  
            if (!CheckColumn("StockHistory", "UnitCost")) 
            {
                DoUpdate("Alter Table", "StockHistory", "UnitCost", "CURRENCY");
            }
  
            if (!CheckColumn("StockHistory", "UnitMarkup")) 
            {
                DoUpdate("Alter Table", "StockHistory", "UnitMarkup", "CURRENCY");
            }
  
            if (!CheckColumn("StockHistory", "UnitSalePrice")) 
            {
                DoUpdate("Alter Table", "StockHistory", "UnitSalePrice", "CURRENCY");
            }
  
            if (!CheckColumn("StockHistory", "AddVAT")) 
            {
                DoUpdate("Alter Table", "StockHistory", "AddVAT", "TEXT(1)");
            }
  
            if (!CheckColumn("StockHistory", "StockAction")) 
            {
                DoUpdate("Alter Table", "StockHistory", "StockAction", "TEXT(100)");
            }
  
            if (!CheckColumn("StockHistory", "ActionDate")) 
            {
                DoUpdate("Alter Table", "StockHistory", "ActionDate", "DATETIME");
            }
  
  
  
  
  
  
  
            //StockPriceLevels
  
            if (!CheckTable("StockPriceLevels")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE StockPriceLevels (StockPriceLevelId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("StockPriceLevels", "StockId")) 
            {
                DoUpdate("Alter Table", "StockPriceLevels", "StockId", "TEXT(50)");
            }
  
            if (!CheckColumn("StockPriceLevels", "Price1")) 
            {
                DoUpdate("Alter Table", "StockPriceLevels", "Price1", "CURRENCY");
            }
  
            if (!CheckColumn("StockPriceLevels", "Price2")) 
            {
                DoUpdate("Alter Table", "StockPriceLevels", "Price2", "CURRENCY");
            }
  
            if (!CheckColumn("StockPriceLevels", "Price3")) 
            {
                DoUpdate("Alter Table", "StockPriceLevels", "Price3", "CURRENCY");
            }
  
            if (!CheckColumn("StockPriceLevels", "Price4")) 
            {
                DoUpdate("Alter Table", "StockPriceLevels", "Price4", "CURRENCY");
            }
  
            if (!CheckColumn("StockPriceLevels", "Price5")) 
            {
                DoUpdate("Alter Table", "StockPriceLevels", "Price5", "CURRENCY");
            }
  
            if (!CheckColumn("StockPriceLevels", "PriceLevelType")) 
            {
                DoUpdate("Alter Table", "StockPriceLevels", "PriceLevelType", "TEXT(1) NULL");
            }
  
  
  
  
            //StockSerialNumbers
  
            if (!CheckTable("StockSerialNumbers")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE StockSerialNumbers (StockSerialNumberId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("StockSerialNumbers", "StockId")) 
            {
                DoUpdate("Alter Table", "StockSerialNumbers", "StockId", "TEXT(50)");
            }
  
            if (!CheckColumn("StockSerialNumbers", "SerialNumber")) 
            {
                DoUpdate("Alter Table", "StockSerialNumbers", "SerialNumber", "TEXT(100)");
            }
  
            if (!CheckColumn("StockSerialNumbers", "DateLoaded")) 
            {
                DoUpdate("Alter Table", "StockSerialNumbers", "DateLoaded", "DATETIME");
            }
  
            if (!CheckColumn("StockSerialNumbers", "SoldToCustomerId")) 
            {
                DoUpdate("Alter Table", "StockSerialNumbers", "SoldToCustomerId", "NUMBER NULL");
            }
  
            if (!CheckColumn("StockSerialNumbers", "DateSold")) 
            {
                DoUpdate("Alter Table", "StockSerialNumbers", "DateSold", "DATETIME NULL");
            }
  
  
  
  
  
            //StockStoreMovement
  
            if (!CheckTable("StockStoreMovement")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE StockStoreMovement (StockStoreMovementId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("StockStoreMovement", "DispatchId")) 
            {
                DoUpdate("Alter Table", "StockStoreMovement", "DispatchId", "NUMBER");
            }
  
            if (!CheckColumn("StockStoreMovement", "StockId")) 
            {
                DoUpdate("Alter Table", "StockStoreMovement", "StockId", "TEXT(50)");
            }
  
            if (!CheckColumn("StockStoreMovement", "Description")) 
            {
                DoUpdate("Alter Table", "StockStoreMovement", "Description", "TEXT(100)");
            }
  
            if (!CheckColumn("StockStoreMovement", "SupplierStockCode")) 
            {
                DoUpdate("Alter Table", "StockStoreMovement", "SupplierStockCode", "TEXT(100)");
            }
  
            if (!CheckColumn("StockStoreMovement", "StockCategory")) 
            {
                DoUpdate("Alter Table", "StockStoreMovement", "StockCategory", "TEXT(100)");
            }
  
            if (!CheckColumn("StockStoreMovement", "StockLevel")) 
            {
                DoUpdate("Alter Table", "StockStoreMovement", "StockLevel", "CURRENCY");
            }
  
            if (!CheckColumn("StockStoreMovement", "StockOrderUnit")) 
            {
                DoUpdate("Alter Table", "StockStoreMovement", "StockOrderUnit", "CURRENCY");
            }
  
            if (!CheckColumn("StockStoreMovement", "StockUnitCost")) 
            {
                DoUpdate("Alter Table", "StockStoreMovement", "StockUnitCost", "CURRENCY");
            }
  
            if (!CheckColumn("StockStoreMovement", "StockSaleUnit")) 
            {
                DoUpdate("Alter Table", "StockStoreMovement", "StockSaleUnit", "CURRENCY");
            }
  
            if (!CheckColumn("StockStoreMovement", "UnitCost")) 
            {
                DoUpdate("Alter Table", "StockStoreMovement", "UnitCost", "CURRENCY");
            }
  
            if (!CheckColumn("StockStoreMovement", "UnitMarkup")) 
            {
                DoUpdate("Alter Table", "StockStoreMovement", "UnitMarkup", "CURRENCY");
            }
  
            if (!CheckColumn("StockStoreMovement", "UnitSalePrice")) 
            {
                DoUpdate("Alter Table", "StockStoreMovement", "UnitSalePrice", "CURRENCY");
            }
  
            if (!CheckColumn("StockStoreMovement", "AddVAT")) 
            {
                DoUpdate("Alter Table", "StockStoreMovement", "AddVAT", "TEXT(1)");
            }
  
            if (!CheckColumn("StockStoreMovement", "ManualPrice")) 
            {
                DoUpdate("Alter Table", "StockStoreMovement", "ManualPrice", "TEXT(1)");
            }
  
            if (!CheckColumn("StockStoreMovement", "Scale")) 
            {
                DoUpdate("Alter Table", "StockStoreMovement", "Scale", "TEXT(1)");
            }
  
            if (!CheckColumn("StockStoreMovement", "ScaleFunction")) 
            {
                DoUpdate("Alter Table", "StockStoreMovement", "ScaleFunction", "TEXT(1)");
            }
  
            if (!CheckColumn("StockStoreMovement", "ScaleDigits")) 
            {
                DoUpdate("Alter Table", "StockStoreMovement", "ScaleDigits", "TEXT(20)");
            }
  
            if (!CheckColumn("StockStoreMovement", "ScaleVAT")) 
            {
                DoUpdate("Alter Table", "StockStoreMovement", "ScaleVAT", "TEXT(1)");
            }
  
            if (!CheckColumn("StockStoreMovement", "PhysicalStock")) 
            {
                DoUpdate("Alter Table", "StockStoreMovement", "PhysicalStock", "TEXT(1)");
            }
  
            if (!CheckColumn("StockStoreMovement", "SerialNumberTracking")) 
            {
                DoUpdate("Alter Table", "StockStoreMovement", "SerialNumberTracking", "TEXT(1)");
            }
  
            if (!CheckColumn("StockStoreMovement", "SerialNumberTracking")) 
            {
                DoUpdate("Alter Table", "StockStoreMovement", "SerialNumberTracking", "TEXT(1)");
            }
  
            if (!CheckColumn("StockStoreMovement", "StoreName")) 
            {
                DoUpdate("Alter Table", "StockStoreMovement", "StoreName", "TEXT(50)");
            }
  
            if (!CheckColumn("StockStoreMovement", "MovementDate")) 
            {
                DoUpdate("Alter Table", "StockStoreMovement", "MovementDate", "DATETIME");
            }
  
            if (!CheckColumn("StockStoreMovement", "Status")) 
            {
                DoUpdate("Alter Table", "StockStoreMovement", "Status", "TEXT(20)");
            }
  
            if (!CheckColumn("StockStoreMovement", "LabelPricing")) 
            {
                DoUpdate("Alter Table", "StockStoreMovement", "LabelPricing", "TEXT(100) NULL");
            }
  
  
  
            //StockId
  
            if (!CheckTable("StockId")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE StockId (StartCode TEXT(50) PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("StockId", "StockCode")) 
            {
                DoUpdate("Alter Table", "StockId", "StockCode", "NUMBER");
            }
  
  
  
  
  
            //StockSubCategory
  
            if (!CheckTable("StockSubCategory")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE StockSubCategory (SubCategoryId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("StockSubCategory", "StockCategory")) 
            {
                DoUpdate("Alter Table", "StockSubCategory", "StockCategory", "TEXT(100) NULL");
            }
  
            if (!CheckColumn("StockSubCategory", "StockSubCategory")) 
            {
                DoUpdate("Alter Table", "StockSubCategory", "StockSubCategory", "TEXT(100) NULL");
            }
  
  
  
  
  
            //StockTake
  
            if (!CheckTable("StockTake")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE StockTake (StockTakeId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("StockTake", "Status")) 
            {
                DoUpdate("Alter Table", "StockTake", "Status", "TEXT(20)");
            }
  
            if (!CheckColumn("StockTake", "DateStarted")) 
            {
                DoUpdate("Alter Table", "StockTake", "DateStarted", "DATETIME");
            }
  
            if (!CheckColumn("StockTake", "DateCompleted")) 
            {
                DoUpdate("Alter Table", "StockTake", "DateCompleted", "DATETIME");
            }
  
            if (!CheckColumn("StockTake", "UserName")) 
            {
                DoUpdate("Alter Table", "StockTake", "UserName", "TEXT(20)");
            }
  
  
  
  
  
  
            //StockTakeItem
  
            if (!CheckTable("StockTakeItem"))
            {
                DoUpdate("Create Multiple", "CREATE TABLE StockTakeItem (StockTakeItemId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("StockTakeItem", "StockTakeId")) 
            {
                DoUpdate("Alter Table", "StockTakeItem", "StockTakeId", "NUMBER");
            }
  
            if (!CheckColumn("StockTakeItem", "StockId")) 
            {
                DoUpdate("Alter Table", "StockTakeItem", "StockId", "TEXT(50)");
            }
  
            if (!CheckColumn("StockTakeItem", "Description")) 
            {
                DoUpdate("Alter Table", "StockTakeItem", "Description", "TEXT(100)");
            }
  
            if (!CheckColumn("StockTakeItem", "QuantityRecorded")) 
            {
                DoUpdate("Alter Table", "StockTakeItem", "QuantityRecorded", "CURRENCY");
            }
  
            if (!CheckColumn("StockTakeItem", "SystemQuantity")) 
            {
                DoUpdate("Alter Table", "StockTakeItem", "SystemQuantity", "CURRENCY");
            }
  
            if (!CheckColumn("StockTakeItem", "StockDate")) 
            {
                DoUpdate("Alter Table", "StockTakeItem", "StockDate", "DATETIME");
            }
  
            if (!CheckColumn("StockTakeItem", "UpdateQuantity")) 
            {
                DoUpdate("Alter Table", "StockTakeItem", "UpdateQuantity", "CURRENCY");
            }
  
            if (!CheckColumn("StockTakeItem", "DiscrepancyAction")) 
            {
                DoUpdate("Alter Table", "StockTakeItem", "DiscrepancyAction", "TEXT(50)");
            }
  
            if (!CheckColumn("StockTakeItem", "ActionStatus")) 
            {
                DoUpdate("Alter Table", "StockTakeItem", "ActionStatus", "TEXT(50)");
            }
  
  
  
  
  
            //StockTakeVariance
  
            if (!CheckTable("StockTakeVariance")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE StockTakeVariance (StockTakeItemId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("StockTakeVariance", "StockTakeId")) 
            {
                DoUpdate("Alter Table", "StockTakeVariance", "StockTakeId", "NUMBER");
            }
  
            if (!CheckColumn("StockTakeVariance", "StockId")) 
            {
                DoUpdate("Alter Table", "StockTakeVariance", "StockId", "TEXT(50)");
            }
  
            if (!CheckColumn("StockTakeVariance", "Description")) 
            {
                DoUpdate("Alter Table", "StockTakeVariance", "Description", "TEXT(100)");
            }
  
            if (!CheckColumn("StockTakeVariance", "Variance")) 
            {
                DoUpdate("Alter Table", "StockTakeVariance", "Variance", "CURRENCY");
            }
  
            if (!CheckColumn("StockTakeVariance", "UnitPriceExcl")) 
            {
                DoUpdate("Alter Table", "StockTakeVariance", "UnitPriceExcl", "CURRENCY");
            }
  
            if (!CheckColumn("StockTakeVariance", "SalePriceExcl")) 
            {
                DoUpdate("Alter Table", "StockTakeVariance", "SalePriceExcl", "CURRENCY");
            }
  
            if (!CheckColumn("StockTakeVariance", "StockDate")) 
            {
                DoUpdate("Alter Table", "StockTakeVariance", "StockDate", "DATETIME");
            }
  
  
  
  
  
            //StockWholeSale
  
            if (!CheckTable("StockWholeSale")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE StockWholeSale (StockWholeSaleId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("StockWholeSale", "StockId")) 
            {
                DoUpdate("Alter Table", "StockWholeSale", "StockId", "TEXT(50)");
            }
  
            if (!CheckColumn("StockWholeSale", "LevelNo")) 
            {
                DoUpdate("Alter Table", "StockWholeSale", "LevelNo", "NUMBER");
            }
  
            if (!CheckColumn("StockWholeSale", "MarkupValue")) 
            {
                DoUpdate("Alter Table", "StockWholeSale", "MarkupValue", "CURRENCY");
            }
  
            if (!CheckColumn("StockWholeSale", "QuantityFrom")) 
            {
                DoUpdate("Alter Table", "StockWholeSale", "QuantityFrom", "CURRENCY");
            }
  
            if (!CheckColumn("StockWholeSale", "QuantityTo")) 
            {
                DoUpdate("Alter Table", "StockWholeSale", "QuantityTo", "CURRENCY");
            }
  
            if (!CheckColumn("StockWholeSale", "WholeSaleLevelType")) 
            {
                DoUpdate("Alter Table", "StockWholeSale", "WholeSaleLevelType", "TEXT(1)");
            }
  
  

            //Store
  
            if (!CheckTable("Store")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE Store (StoreId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("Store", "StoreName")) 
            {
                DoUpdate("Alter Table", "Store", "StoreName", "TEXT(50)");
            }
  
            if (!CheckColumn("Store", "EmailAddress")) 
            {
                DoUpdate("Alter Table", "Store", "EmailAddress", "TEXT(100)");
            }



            //Supplier

            if (!CheckTable("Supplier"))
            {
                DoUpdate("Create Multiple", "CREATE TABLE Supplier (SupplierId COUNTER PRIMARY KEY)", "", "");
            }

            if (!CheckColumn("Supplier", "AccountNumber"))
            {
                DoUpdate("Alter Table", "Supplier", "AccountNumber", "TEXT(50)");
            }

            if (!CheckColumn("Supplier", "SupplierName"))
            {
                DoUpdate("Alter Table", "Supplier", "SupplierName", "TEXT(200)");
            }

            if (!CheckColumn("Supplier", "RegistrationNumber"))
            {
                DoUpdate("Alter Table", "Supplier", "RegistrationNumber", "TEXT(30)");
            }

            if (!CheckColumn("Supplier", "AddressLine1"))
            {
                DoUpdate("Alter Table", "Supplier", "AddressLine1", "TEXT(100)");
            }

            if (!CheckColumn("Supplier", "AddressLine2"))
            {
                DoUpdate("Alter Table", "Supplier", "AddressLine2", "TEXT(100)");
            }

            if (!CheckColumn("Supplier", "AddressLine3"))
            {
                DoUpdate("Alter Table", "Supplier", "AddressLine3", "TEXT(100)");
            }

            if (!CheckColumn("Supplier", "PostalCode"))
            {
                DoUpdate("Alter Table", "Supplier", "PostalCode", "TEXT(5)");
            }

            if (!CheckColumn("Supplier", "ContactNumber1"))
            {
                DoUpdate("Alter Table", "Supplier", "ContactNumber1", "TEXT(30)");
            }

            if (!CheckColumn("Supplier", "ContactNumber2"))
            {
                DoUpdate("Alter Table", "Supplier", "ContactNumber2", "TEXT(30)");
            }

            if (!CheckColumn("Supplier", "FaxNumber"))
            {
                DoUpdate("Alter Table", "Supplier", "FaxNumber", "TEXT(30)");
            }

            if (!CheckColumn("Supplier", "ContactPerson"))
            {
                DoUpdate("Alter Table", "Supplier", "ContactPerson", "TEXT(100)");
            }

            if (!CheckColumn("Supplier", "EmailAddress"))
            {
                DoUpdate("Alter Table", "Supplier", "EmailAddress", "TEXT(100) NULL");
            }

            if (!CheckColumn("Supplier", "PaymentTerms"))
            {
                DoUpdate("Alter Table", "Supplier", "PaymentTerms", "TEXT(50)");
            }

            if (!CheckColumn("Supplier", "CreditLimit"))
            {
                DoUpdate("Alter Table", "Supplier", "CreditLimit", "TEXT(20)");
            }

            if (!CheckColumn("Supplier", "RegistrationDate"))
            {
                DoUpdate("Alter Table", "Supplier", "RegistrationDate", "DATETIME");
            }

            if (!CheckColumn("Supplier", "OrderAddVAT"))
            {
                DoUpdate("Alter Table", "Supplier", "OrderAddVAT", "TEXT(1)");
            }

            if (!CheckColumn("Supplier", "BankName"))
            {
                DoUpdate("Alter Table", "Supplier", "BankName", "TEXT(50) NULL");
            }

            if (!CheckColumn("Supplier", "BankAccountNumber"))
            {
                DoUpdate("Alter Table", "Supplier", "BankAccountNumber", "TEXT(20) NULL");
            }

            if (!CheckColumn("Supplier", "BankAccountType"))
            {
                DoUpdate("Alter Table", "Supplier", "BankAccountType", "TEXT(30) NULL");
            }

            if (!CheckColumn("Supplier", "BankAccountBranchCode"))
            {
                DoUpdate("Alter Table", "Supplier", "BankAccountBranchCode", "TEXT(15) NULL");
            }

            if (!CheckColumn("Supplier", "AccountHolderName"))
            {
                DoUpdate("Alter Table", "Supplier", "AccountHolderName", "TEXT(50) NULL");
            }

            if (!CheckColumn("Supplier", "StandardMarkup"))
            {
                DoUpdate("Alter Table", "Supplier", "StandardMarkup", "TEXT(10) NULL");
            }

            if (!CheckColumn("Supplier", "NumberOfTables"))
            {
                DoUpdate("Alter Table", "Supplier", "NumberOfTables", "TEXT(2) NULL");
            }

            if (!CheckColumn("Supplier", "PostalAddressLine1"))
            {
                DoUpdate("Alter Table", "Supplier", "PostalAddressLine1", "TEXT(100) NULL");
            }

            if (!CheckColumn("Supplier", "PostalAddressLine2"))
            {
                DoUpdate("Alter Table", "Supplier", "PostalAddressLine2", "TEXT(100) NULL");
            }

            if (!CheckColumn("Supplier", "PostalAddressLine3"))
            {
                DoUpdate("Alter Table", "Supplier", "PostalAddressLine3", "TEXT(100) NULL");
            }

            if (!CheckColumn("Supplier", "PostalCode"))
            {
                DoUpdate("Alter Table", "Supplier", "PostalCode", "TEXT(100) NULL");
            }




            //SupplierAccount

            if (!CheckTable("SupplierAccount"))
            {
                DoUpdate("Create Multiple", "CREATE TABLE SupplierAccount (SupplierAccountId COUNTER PRIMARY KEY)", "", "");
            }

            if (!CheckColumn("SupplierAccount", "SupplierId"))
            {
                DoUpdate("Alter Table", "SupplierAccount", "SupplierId", "NUMBER");
            }

            if (!CheckColumn("SupplierAccount", "AccountNumber"))
            {
                DoUpdate("Alter Table", "SupplierAccount", "AccountNumber", "TEXT(50)");
            }

            if (!CheckColumn("SupplierAccount", "TransactionType"))
            {
                DoUpdate("Alter Table", "SupplierAccount", "TransactionType", "TEXT(50)");
            }

            if (!CheckColumn("SupplierAccount", "UserName"))
            {
                DoUpdate("Alter Table", "SupplierAccount", "UserName", "TEXT(50)");
            }

            if (!CheckColumn("SupplierAccount", "AccountName"))
            {
                DoUpdate("Alter Table", "SupplierAccount", "AccountName", "TEXT(100)");
            }

            if (!CheckColumn("SupplierAccount", "TransactionDate"))
            {
                DoUpdate("Alter Table", "SupplierAccount", "TransactionDate", "DATETIME");
            }

            if (!CheckColumn("SupplierAccount", "TransactionAmount"))
            {
                DoUpdate("Alter Table", "SupplierAccount", "TransactionAmount", "TEXT(50)");
            }

            if (!CheckColumn("SupplierAccount", "TransactionDescription"))
            {
                DoUpdate("Alter Table", "SupplierAccount", "TransactionDescription", "TEXT(50) NULL");
            }

            if (!CheckColumn("SupplierAccount", "TransactionNotes"))
            {
                DoUpdate("Alter Table", "SupplierAccount", "TransactionNotes", "TEXT(255) NULL");
            }




            //SupplierExpenses

            if (!CheckTable("SupplierExpenses"))
            {
                DoUpdate("Create Multiple", "CREATE TABLE SupplierExpenses (SupplierExpenseId COUNTER PRIMARY KEY)", "", "");
            }

            if (!CheckColumn("SupplierExpenses", "SupplierId"))
            {
                DoUpdate("Alter Table", "SupplierExpenses", "SupplierId", "NUMBER");
            }

            if (!CheckColumn("SupplierExpenses", "ExpenseType"))
            {
                DoUpdate("Alter Table", "SupplierExpenses", "ExpenseType", "TEXT(50)");
            }

            if (!CheckColumn("SupplierExpenses", "ExpenseAmount"))
            {
                DoUpdate("Alter Table", "SupplierExpenses", "ExpenseAmount", "CURRENCY");
            }




            //SupplierHistory

            if (!CheckTable("SupplierHistory"))
            {
                DoUpdate("Create Multiple", "CREATE TABLE SupplierHistory (SupplierHistoryId COUNTER PRIMARY KEY)", "", "");
            }

            if (!CheckColumn("SupplierHistory", "SupplierId"))
            {
                DoUpdate("Alter Table", "SupplierHistory", "SupplierId", "NUMBER");
            }

            if (!CheckColumn("SupplierHistory", "AccountNumber"))
            {
                DoUpdate("Alter Table", "SupplierHistory", "AccountNumber", "TEXT(50)");
            }

            if (!CheckColumn("SupplierHistory", "TransactionDate"))
            {
                DoUpdate("Alter Table", "SupplierHistory", "TransactionDate", "DATETIME");
            }

            if (!CheckColumn("SupplierHistory", "TransactionDescription"))
            {
                DoUpdate("Alter Table", "SupplierHistory", "TransactionDescription", "TEXT(255)");
            }

            if (!CheckColumn("SupplierHistory", "TransactionAmount"))
            {
                DoUpdate("Alter Table", "SupplierHistory", "TransactionAmount", "CURRENCY");
            }



  
            //SuspendedOrderItem
  
            if (!CheckTable("SuspendedOrderItem")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE SuspendedOrderItem (SuspendedOrderNumber TEXT(50), " + 
                "StockId TEXT(50), " + 
                "CONSTRAINT PkSOI PRIMARY KEY (SuspendedOrderNumber, StockId))", "", "");
            }
  
            if (!CheckColumn("SuspendedOrderItem", "Description")) 
            {
                DoUpdate("Alter Table", "SuspendedOrderItem", "Description", "TEXT(50)");
            }
  
            if (!CheckColumn("SuspendedOrderItem", "PreviousStockUnitCost")) 
            {
                DoUpdate("Alter Table", "SuspendedOrderItem", "PreviousStockUnitCost", "TEXT(20)");
            }
  
            if (!CheckColumn("SuspendedOrderItem", "QuantityReceived")) 
            {
                DoUpdate("Alter Table", "SuspendedOrderItem", "QuantityReceived", "TEXT(20)");
            }
  
            if (!CheckColumn("SuspendedOrderItem", "StockUnitCost")) 
            {
                DoUpdate("Alter Table", "SuspendedOrderItem", "StockUnitCost", "TEXT(20)");
            }
  
            if (!CheckColumn("SuspendedOrderItem", "VATAmount")) 
            {
                DoUpdate("Alter Table", "SuspendedOrderItem", "VATAmount", "TEXT(20)");
            }
  
            if (!CheckColumn("SuspendedOrderItem", "TotalIncl")) 
            {
                DoUpdate("Alter Table", "SuspendedOrderItem", "TotalIncl", "TEXT(20)");
            }
  
            if (!CheckColumn("SuspendedOrderItem", "UpdatePrice")) 
            {
                DoUpdate("Alter Table", "SuspendedOrderItem", "UpdatePrice", "TEXT(2) NULL");
            }
  
            if (!CheckColumn("SuspendedOrderItem", "StockUpdated")) 
            {
                DoUpdate("Alter Table", "SuspendedOrderItem", "StockUpdated", "TEXT(2) NULL");
            }
  
            if (!CheckColumn("SuspendedOrderItem", "AddVAT")) 
            {
                DoUpdate("Alter Table", "SuspendedOrderItem", "AddVAT", "TEXT(1) NULL");
            }
  
            if (!CheckColumn("SuspendedOrderItem", "StockReturn")) 
            {
                DoUpdate("Alter Table", "SuspendedOrderItem", "StockReturn", "TEXT(2) NULL");
            }
  
            if (!CheckColumn("SuspendedOrderItem", "MarkupPercentage")) 
            {
                DoUpdate("Alter Table", "SuspendedOrderItem", "MarkupPercentage", "TEXT(20)");
            }
  
            if (!CheckColumn("SuspendedOrderItem", "StockOrderUnit")) 
            {
                DoUpdate("Alter Table", "SuspendedOrderItem", "StockOrderUnit", "TEXT(20)");
            }
  
  
  
            //SuspendedOrders
  
            if (!CheckTable("SuspendedOrders")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE SuspendedOrders (CreditorName TEXT(50), " + 
                "InvoiceNumber TEXT(20), " + 
                "CONSTRAINT PkO PRIMARY KEY (CreditorName, InvoiceNumber))", "", "");
            }
  
            if (!CheckColumn("SuspendedOrders", "OrderDate")) 
            {
                DoUpdate("Alter Table", "SuspendedOrders", "OrderDate", "DATETIME");
            }
  
            if (!CheckColumn("SuspendedOrders", "SuspendedOrderNumber")) 
            {
                DoUpdate("Alter Table", "SuspendedOrders", "SuspendedOrderNumber", "TEXT(50)");
            }
  
  
  
  
  
  
            //SuspendedTransactions
  
            if (!CheckTable("SuspendedTransactions")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE SuspendedTransactions (RegisterTransactionId NUMBER, " + 
                "TransactionId NUMBER, " + 
                "TransactionDate DATETIME, " + 
                "StockId TEXT(50), " + 
                "CONSTRAINT PkTR PRIMARY KEY (RegisterTransactionId, TransactionId, TransactionDate, StockId))", "", "");
            }
  
            if (!CheckColumn("SuspendedTransactions", "TransactionType")) 
            {
                DoUpdate("Alter Table", "SuspendedTransactions", "TransactionType", "TEXT(50)");
            }
  
            if (!CheckColumn("SuspendedTransactions", "UserName")) 
            {
                DoUpdate("Alter Table", "SuspendedTransactions", "UserName", "TEXT(50)");
            }
  
            if (!CheckColumn("SuspendedTransactions", "Description")) 
            {
                DoUpdate("Alter Table", "SuspendedTransactions", "Description", "TEXT(50)");
            }
  
            if (!CheckColumn("SuspendedTransactions", "Quantity")) 
            {
                DoUpdate("Alter Table", "SuspendedTransactions", "Quantity", "CURRENCY");
            }
  
            if (!CheckColumn("SuspendedTransactions", "UnitPrice")) 
            {
                DoUpdate("Alter Table", "SuspendedTransactions", "UnitPrice", "TEXT(50)");
            }
  
            if (!CheckColumn("SuspendedTransactions", "VATAmount")) 
            {
                DoUpdate("Alter Table", "SuspendedTransactions", "VATAmount", "TEXT(50)");
            }
  
            if (!CheckColumn("SuspendedTransactions", "VATPercentage")) 
            {
                DoUpdate("Alter Table", "SuspendedTransactions", "VATPercentage", "TEXT(50)");
            }
  
            if (!CheckColumn("SuspendedTransactions", "TotalAmount")) 
            {
                DoUpdate("Alter Table", "SuspendedTransactions", "TotalAmount", "TEXT(50)");
            }
  
            if (!CheckColumn("SuspendedTransactions", "DiscountAmount")) 
            {
                DoUpdate("Alter Table", "SuspendedTransactions", "DiscountAmount", "TEXT(50)");
            }
  
            if (!CheckColumn("SuspendedTransactions", "DiscountAllowed")) 
            {
                DoUpdate("Alter Table", "SuspendedTransactions", "DiscountAllowed", "TEXT(50)");
            }
  
            if (!CheckColumn("SuspendedTransactions", "SerialNumberTracking")) 
            {
                DoUpdate("Alter Table", "SuspendedTransactions", "SerialNumberTracking", "TEXT(1)");
            }
  
            if (!CheckColumn("SuspendedTransactions", "PaymentComplete")) 
            {
                DoUpdate("Alter Table", "SuspendedTransactions", "PaymentComplete", "CURRENCY");
            }
  
            if (!CheckColumn("SuspendedTransactions", "CustomerId")) 
            {
                DoUpdate("Alter Table", "SuspendedTransactions", "CustomerId", "TEXT(10) NULL");
            }
  
            if (!CheckColumn("SuspendedTransactions", "SlipRank")) 
            {
                DoUpdate("Alter Table", "SuspendedTransactions", "SlipRank", "NUMBER NULL");
            }
  
            if (!CheckColumn("SuspendedTransactions", "Notes")) 
            {
                DoUpdate("Alter Table", "SuspendedTransactions", "Notes", "TEXT(255) NULL");
            }
  
  
  
            //SuspendedTransactionsLog
  
            if (!CheckTable("SuspendedTransactionsLog")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE SuspendedTransactionsLog (SuspendTransactionId NUMBER, " + 
                "SuspendTransactionDate DATETIME, " + 
                "StockId TEXT(50), " + 
                "CONSTRAINT PkSTL PRIMARY KEY (SuspendTransactionId, SuspendTransactionDate, StockId))", "", "");
            }
  
            if (!CheckColumn("SuspendedTransactionsLog", "SuspendUserName")) 
            {
                DoUpdate("Alter Table", "SuspendedTransactionsLog", "SuspendUserName", "TEXT(50)");
            }
  
            if (!CheckColumn("SuspendedTransactionsLog", "Description")) 
            {
                DoUpdate("Alter Table", "SuspendedTransactionsLog", "Description", "TEXT(50)");
            }
  
            if (!CheckColumn("SuspendedTransactionsLog", "Quantity")) 
            {
                DoUpdate("Alter Table", "SuspendedTransactionsLog", "Quantity", "CURRENCY");
            }
  
            if (!CheckColumn("SuspendedTransactionsLog", "UnitPrice")) 
            {
                DoUpdate("Alter Table", "SuspendedTransactionsLog", "UnitPrice", "TEXT(50)");
            }
  
            if (!CheckColumn("SuspendedTransactionsLog", "VATAmount")) 
            {
                DoUpdate("Alter Table", "SuspendedTransactionsLog", "VATAmount", "TEXT(50)");
            }
  
            if (!CheckColumn("SuspendedTransactionsLog", "TotalAmount")) 
            {
                DoUpdate("Alter Table", "SuspendedTransactionsLog", "TotalAmount", "TEXT(50)");
            }
  
            if (!CheckColumn("SuspendedTransactionsLog", "TranTrandID")) 
            {
                DoUpdate("Alter Table", "SuspendedTransactionsLog", "TranTrandID", "NUMBER NULL");
            }
  
            if (!CheckColumn("SuspendedTransactionsLog", "TranUserName")) 
            {
                DoUpdate("Alter Table", "SuspendedTransactionsLog", "TranUserName", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("SuspendedTransactionsLog", "TranTransactionDate")) 
            {
                DoUpdate("Alter Table", "SuspendedTransactionsLog", "TranTransactionDate", "DATETIME NULL");
            }
  
  
            //SystemData
  
            if (!CheckTable("SystemData")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE SystemData (SystemValue TEXT(50))", "", "");
            }
  
            if (!CheckColumn("SystemData", "DataValue")) 
            {
                DoUpdate("Alter Table", "SystemData", "DataValue", "TEXT(50)");
            }
  
  
  
  
  
            //SystemFunctions
  
            if (!CheckTable("SystemFunctions")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE SystemFunctions (SystemFunctionId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("SystemFunctions", "Level1")) 
            {
                DoUpdate("Alter Table", "SystemFunctions", "Level1", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("SystemFunctions", "Level2")) 
            {
                DoUpdate("Alter Table", "SystemFunctions", "Level2", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("SystemFunctions", "Level3")) 
            {
                DoUpdate("Alter Table", "SystemFunctions", "Level3", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("SystemFunctions", "FunctionName")) 
            {
                DoUpdate("Alter Table", "SystemFunctions", "FunctionName", "TEXT(100) NULL");
            }
  
            if (!CheckColumn("SystemFunctions", "Rank")) 
            {
                DoUpdate("Alter Table", "SystemFunctions", "Rank", "NUMBER");
            }

            if (!CheckColumn("SystemFunctions", "RoleAccess"))
            {
                DoUpdate("Alter Table", "SystemFunctions", "RoleAccess", "TEXT(20) NULL");
            }


            if (!CheckTable("SystemImages")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE SystemImages (ImageId COUNTER PRIMARY KEY)", "", "");
            }

            if (!CheckColumn("SystemImages", "ImageDesc"))
            {
                DoUpdate("Alter Table", "SystemImages", "ImageDesc", "TEXT(255)");
            }

            if (!CheckColumn("SystemImages", "ImageCategory"))
            {
                DoUpdate("Alter Table", "SystemImages", "ImageCategory", "TEXT(50)");
            }

            if (!CheckColumn("SystemImages", "FileName")) 
            {
                DoUpdate("Alter Table", "SystemImages", "FileName", "TEXT(255)");
            }

            if (!CheckColumn("SystemImages", "FileSize")) 
            {
                DoUpdate("Alter Table", "SystemImages", "FileSize", "NUMBER");
            }

            if (!CheckColumn("SystemImages", "DateUploaded")) 
            {
                DoUpdate("Alter Table", "SystemImages", "DateUploaded", "DateTime");
            }

            //if (_DBType == "Access")
            //{
            //    if (!CheckColumn("SystemImages", "FileData"))
            //    {
            //        DoUpdate("Alter Table", "SystemImages", "FileData", "IMAGE");
            //    }
            //}
            //else
            //{
            //    if (!CheckColumn("SystemImages", "FileData"))
            //    {
            //        DoUpdate("Alter Table", "SystemImages", "FileData", "LONGBLOB");
            //    }
            //}


  
  
            //SystemLog
  
            if (!CheckTable("SystemLog")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE SystemLog (LogId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("SystemLog", "UserName")) 
            {
                DoUpdate("Alter Table", "SystemLog", "UserName", "TEXT(50)");
            }
  
            if (!CheckColumn("SystemLog", "LogType")) 
            {
                DoUpdate("Alter Table", "SystemLog", "LogType", "TEXT(50)");
            }
  
            if (!CheckColumn("SystemLog", "UserAction")) 
            {
                DoUpdate("Alter Table", "SystemLog", "UserAction", "TEXT(100)");
            }
  
            if (!CheckColumn("SystemLog", "ActionDate")) 
            {
                DoUpdate("Alter Table", "SystemLog", "ActionDate", "DATETIME");
            }
  
  
  
  
            //SystemTerms
  
            if (!CheckTable("SystemTerms")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE SystemTerms (CategoryId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("SystemTerms", "StockCategory")) 
            {
                DoUpdate("Alter Table", "SystemTerms", "StockCategory", "TEXT(50)");
            }
  
  
  
  
            //SystemRole
  
            if (!CheckTable("SystemRole")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE SystemRole (Role TEXT(50), " + 
                "ProgramName TEXT(50), " + 
                "CONSTRAINT PkSRole PRIMARY KEY (Role, ProgramName))", "", "");
            }
  
  
  
  
            //SystemUser
  
            if (!CheckTable("SystemUser")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE SystemUser (UserName TEXT(50) PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("SystemUser", "UserPassword")) 
            {
                DoUpdate("Alter Table", "SystemUser", "UserPassword", "TEXT(50)");
            }
  
            if (!CheckColumn("SystemUser", "Active")) 
            {
                DoUpdate("Alter Table", "SystemUser", "Active", "TEXT(1)");
            }
  
            if (!CheckColumn("SystemUser", "ValidFrom")) 
            {
                DoUpdate("Alter Table", "SystemUser", "ValidFrom", "DATETIME");
            }
  
            if (!CheckColumn("SystemUser", "ValidTo")) 
            {
                DoUpdate("Alter Table", "SystemUser", "ValidTo", "DATETIME");
            }
  
            if (!CheckColumn("SystemUser", "NextPasswordExpiryDate")) 
            {
                DoUpdate("Alter Table", "SystemUser", "NextPasswordExpiryDate", "DATETIME");
            }
  
            if (!CheckColumn("SystemUser", "DevicePassword")) 
            {
                DoUpdate("Alter Table", "SystemUser", "DevicePassword", "TEXT(50)");
            }
  
            if (!CheckColumn("SystemUser", "PasswordNeverExpires")) 
            {
                DoUpdate("Alter Table", "SystemUser", "PasswordNeverExpires", "TEXT(1) NULL");
            }
  
            if (!CheckColumn("SystemUser", "ImagePath")) 
            {
                DoUpdate("Alter Table", "SystemUser", "ImagePath", "TEXT(255) NULL");
            }
  
            if (!CheckColumn("SystemUser", "PicSize")) 
            {
                DoUpdate("Alter Table", "SystemUser", "PicSize", "TEXT(50) NULL");
            }

            if (!CheckColumn("SystemUser", "IDNumber"))
            {
                DoUpdate("Alter Table", "SystemUser", "IDNumber", "TEXT(20) NULL");
            }

            if (!CheckColumn("SystemUser", "TitleId"))
            {
                DoUpdate("Alter Table", "SystemUser", "TitleId", "NUMBER NULL");
            }

            if (!CheckColumn("SystemUser", "Sex"))
            {
                DoUpdate("Alter Table", "SystemUser", "Sex", "TEXT(1) NULL");
            }

            if (!CheckColumn("SystemUser", "FullNames"))
            {
                DoUpdate("Alter Table", "SystemUser", "FullNames", "TEXT(100) NULL");
            }

            if (!CheckColumn("SystemUser", "Surname"))
            {
                DoUpdate("Alter Table", "SystemUser", "Surname", "TEXT(100) NULL");
            }

            if (!CheckColumn("SystemUser", "NickName"))
            {
                DoUpdate("Alter Table", "SystemUser", "NickName", "TEXT(50) NULL");
            }

            if (!CheckColumn("SystemUser", "ResidentialStreet"))
            {
                DoUpdate("Alter Table", "SystemUser", "ResidentialStreet", "TEXT(100) NULL");
            }

            if (!CheckColumn("SystemUser", "ResidentialSuburb"))
            {
                DoUpdate("Alter Table", "SystemUser", "ResidentialSuburb", "TEXT(100) NULL");
            }

            if (!CheckColumn("SystemUser", "ResidentialCity"))
            {
                DoUpdate("Alter Table", "SystemUser", "ResidentialCity", "TEXT(100) NULL");
            }

            if (!CheckColumn("SystemUser", "ResidentialOther"))
            {
                DoUpdate("Alter Table", "SystemUser", "ResidentialOther", "TEXT(10) NULL");
            }

            if (!CheckColumn("SystemUser", "ResidentialPostalCode"))
            {
                DoUpdate("Alter Table", "SystemUser", "ResidentialPostalCode", "TEXT(10) NULL");
            }

            if (!CheckColumn("SystemUser", "PostalStreet"))
            {
                DoUpdate("Alter Table", "SystemUser", "PostalStreet", "TEXT(100) NULL");
            }

            if (!CheckColumn("SystemUser", "PostalSuburb"))
            {
                DoUpdate("Alter Table", "SystemUser", "PostalSuburb", "TEXT(100) NULL");
            }

            if (!CheckColumn("SystemUser", "PostalCity"))
            {
                DoUpdate("Alter Table", "SystemUser", "PostalCity", "TEXT(100) NULL");
            }

            if (!CheckColumn("SystemUser", "PostalOther"))
            {
                DoUpdate("Alter Table", "SystemUser", "PostalOther", "TEXT(10) NULL");
            }

            if (!CheckColumn("SystemUser", "PostalPostalCode"))
            {
                DoUpdate("Alter Table", "SystemUser", "PostalPostalCode", "TEXT(10) NULL");
            }

            if (!CheckColumn("SystemUser", "ContactNumber1"))
            {
                DoUpdate("Alter Table", "SystemUser", "ContactNumber1", "TEXT(20) NULL");
            }

            if (!CheckColumn("SystemUser", "ContactNumber2"))
            {
                DoUpdate("Alter Table", "SystemUser", "ContactNumber2", "TEXT(20) NULL");
            }




            //TaxInvoice
  
            if (!CheckTable("TaxInvoice")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE TaxInvoice (DebtorName TEXT(50), " + 
                "InvoiceNumber TEXT(20), " + 
                "CONSTRAINT PkQuote PRIMARY KEY (DebtorName, InvoiceNumber))", "", "");
            }
  
            if (!CheckColumn("TaxInvoice", "InvoiceDate")) 
            {
                DoUpdate("Alter Table", "TaxInvoice", "InvoiceDate", "DATETIME");
            }
  
            if (!CheckColumn("TaxInvoice", "InvoiceNotes")) 
            {
                DoUpdate("Alter Table", "TaxInvoice", "InvoiceNotes", "TEXT(255)");
            }
  
            if (!CheckColumn("TaxInvoice", "Terms")) 
            {
                DoUpdate("Alter Table", "TaxInvoice", "Terms", "TEXT(255)");
            }
  
            if (!CheckColumn("TaxInvoice", "DeliveryAddress")) 
            {
                DoUpdate("Alter Table", "TaxInvoice", "DeliveryAddress", "TEXT(1)");
            }
  
            if (!CheckColumn("TaxInvoice", "QuoteNumber")) 
            {
                DoUpdate("Alter Table", "TaxInvoice", "QuoteNumber", "TEXT(50)");
            }
  
            if (!CheckColumn("TaxInvoice", "SettlementDiscount")) 
            {
                DoUpdate("Alter Table", "TaxInvoice", "SettlementDiscount", "CURRENCY");
            }
  
            if (!CheckColumn("TaxInvoice", "ReceiptNumber")) 
            {
                DoUpdate("Alter Table", "TaxInvoice", "ReceiptNumber", "TEXT(50)");
            }
  
            if (!CheckColumn("TaxInvoice", "TransactionId")) 
            {
                DoUpdate("Alter Table", "TaxInvoice", "TransactionId", "NUMBER");
            }
  
            if (!CheckColumn("TaxInvoice", "DeliveryDate")) 
            {
                DoUpdate("Alter Table", "TaxInvoice", "DeliveryDate", "DATETIME");
            }
  
            if (!CheckColumn("TaxInvoice", "SiteName")) 
            {
                DoUpdate("Alter Table", "TaxInvoice", "SiteName", "NUMBER");
            }
  
            if (!CheckColumn("TaxInvoice", "DiscountPercentage")) 
            {
                DoUpdate("Alter Table", "TaxInvoice", "DiscountPercentage", "TEXT(50)");
            }
  
  
  
            //TaxInvoiceItem
  
            if (!CheckTable("TaxInvoiceItem")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE TaxInvoiceItem (InvoiceNumber TEXT(50), " + 
                "StockId TEXT(50), " + 
                "Colour TEXT(50), " + 
                "CONSTRAINT PkInvItem PRIMARY KEY (InvoiceNumber, StockId, Colour))", "", "");
            }
  
            if (!CheckColumn("TaxInvoiceItem", "Description")) 
            {
                DoUpdate("Alter Table", "TaxInvoiceItem", "Description", "TEXT(50)");
            }
  
            if (!CheckColumn("TaxInvoiceItem", "Quantity")) 
            {
                DoUpdate("Alter Table", "TaxInvoiceItem", "Quantity", "CURRENCY");
            }
  
            if (!CheckColumn("TaxInvoiceItem", "M2")) 
            {
                DoUpdate("Alter Table", "TaxInvoiceItem", "M2", "CURRENCY");
            }
  
            if (!CheckColumn("TaxInvoiceItem", "Price")) 
            {
                DoUpdate("Alter Table", "TaxInvoiceItem", "Price", "CURRENCY");
            }
  
            if (!CheckColumn("TaxInvoiceItem", "Weight")) 
            {
                DoUpdate("Alter Table", "TaxInvoiceItem", "Weight", "CURRENCY");
            }
  
            if (!CheckColumn("TaxInvoiceItem", "QuantityPerM2")) 
            {
                DoUpdate("Alter Table", "TaxInvoiceItem", "QuantityPerM2", "CURRENCY");
            }
  
            if (!CheckColumn("TaxInvoiceItem", "WeightPerM2")) 
            {
                DoUpdate("Alter Table", "TaxInvoiceItem", "WeightPerM2", "CURRENCY");
            }
  
            if (!CheckColumn("TaxInvoiceItem", "ConfirmedPrice")) 
            {
                DoUpdate("Alter Table", "TaxInvoiceItem", "ConfirmedPrice", "CURRENCY");
            }
  
            if (!CheckColumn("TaxInvoiceItem", "SupplierReference")) 
            {
                DoUpdate("Alter Table", "TaxInvoiceItem", "SupplierReference", "TEXT(50)");
            }
  
            if (!CheckColumn("TaxInvoiceItem", "Reference")) 
            {
                DoUpdate("Alter Table", "TaxInvoiceItem", "Reference", "TEXT(50)");
            }
  
            if (!CheckColumn("TaxInvoiceItem", "DeliveryCharge")) 
            {
                DoUpdate("Alter Table", "TaxInvoiceItem", "DeliveryCharge", "CURRENCY");
            }
  



            //Terms
  
            if (!CheckTable("Terms")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE Terms (TermType TEXT(50) PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("Terms", "Terms")) 
            {
                DoUpdate("Alter Table", "Terms", "Terms", "TEXT(255)");
            }
  
  
  
  
            //TransactionSeatStats
  
            if (!CheckTable("TransactionSeatStats")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE TransactionSeatStats (RegisterTransactionId NUMBER, " + 
                "SeatingId NUMBER, " + 
                "CONSTRAINT PkTSS PRIMARY KEY (RegisterTransactionId, SeatingId))", "", "");
            }
  
            if (!CheckColumn("TransactionSeatStats", "Adults")) 
            {
                DoUpdate("Alter Table", "TransactionSeatStats", "Adults", "NUMBER NULL");
            }
  
            if (!CheckColumn("TransactionSeatStats", "Kids")) 
            {
                DoUpdate("Alter Table", "TransactionSeatStats", "Kids", "NUMBER NULL");
            }
  
            if (!CheckColumn("TransactionSeatStats", "EatInTakeOut")) 
            {
                DoUpdate("Alter Table", "TransactionSeatStats", "EatInTakeOut", "TEXT(1) NULL");
            }
  
  
  
  
            //TransactionOptions
  
            if (!CheckTable("TransactionOptions")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE TransactionOptions (RegisterTransactionId NUMBER, " + 
                "TransactionId NUMBER, " + 
                "OptionId NUMBER, " + 
                "CONSTRAINT PkTO PRIMARY KEY (RegisterTransactionId, TransactionId, OptionId))", "", "");
            }
  
  
  

            //TransactionPayment
  
            if (!CheckTable("TransactionPayment")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE TransactionPayment (RegisterTransactionId NUMBER NOT NULL)", "", "");
            }
  
            if (!CheckColumn("TransactionPayment", "TransactionPaymentType")) 
            {
                DoUpdate("Alter Table", "TransactionPayment", "TransactionPaymentType", "TEXT(50)");
            }

            if (!CheckColumn("TransactionPayment", "UserName")) 
            {
                DoUpdate("Alter Table", "TransactionPayment", "UserName", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPayment", "TransactionDate")) 
            {
                DoUpdate("Alter Table", "TransactionPayment", "TransactionDate", "DATETIME");
            }

            if (!CheckColumn("TransactionPayment", "CashAmount")) 
            {
                DoUpdate("Alter Table", "TransactionPayment", "CashAmount", "CURRENCY");
            }
  
            if (!CheckColumn("TransactionPayment", "CreditCardHolder")) 
            {
                DoUpdate("Alter Table", "TransactionPayment", "CreditCardHolder", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPayment", "CreditCardNumber")) 
            {
                DoUpdate("Alter Table", "TransactionPayment", "CreditCardNumber", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPayment", "CreditCardExpiry")) 
            {
                DoUpdate("Alter Table", "TransactionPayment", "CreditCardExpiry", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPayment", "CreditCardType")) 
            {
                DoUpdate("Alter Table", "TransactionPayment", "CreditCardType", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPayment", "ChequeNumber")) 
            {
                DoUpdate("Alter Table", "TransactionPayment", "ChequeNumber", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPayment", "ChequeBank")) 
            {
                DoUpdate("Alter Table", "TransactionPayment", "ChequeBank", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPayment", "ChequeBranch")) 
            {
                DoUpdate("Alter Table", "TransactionPayment", "ChequeBranch", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPayment", "DebitCardNumber")) 
            {
                DoUpdate("Alter Table", "TransactionPayment", "DebitCardNumber", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPayment", "DebitCardBank")) 
            {
                DoUpdate("Alter Table", "TransactionPayment", "DebitCardBank", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPayment", "AccountNumber")) 
            {
                DoUpdate("Alter Table", "TransactionPayment", "AccountNumber", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPayment", "AccountName")) 
            {
                DoUpdate("Alter Table", "TransactionPayment", "AccountName", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPayment", "OtherDetails1")) 
            {
                DoUpdate("Alter Table", "TransactionPayment", "OtherDetails1", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPayment", "OtherDetails2")) 
            {
                DoUpdate("Alter Table", "TransactionPayment", "OtherDetails2", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPayment", "AmountTendered")) 
            {
                DoUpdate("Alter Table", "TransactionPayment", "AmountTendered", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPayment", "AccountPaymentType")) 
            {
                DoUpdate("Alter Table", "TransactionPayment", "AccountPaymentType", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("TransactionPayment", "ShiftId")) 
            {
                DoUpdate("Alter Table", "TransactionPayment", "ShiftId", "NUMBER NULL");
            }
  
            if (!CheckColumn("TransactionPayment", "SalesCode")) 
            {
                DoUpdate("Alter Table", "TransactionPayment", "SalesCode", "TEXT(10)");
            }
  
            if (!CheckColumn("TransactionPayment", "TransactionPaymentId")) 
            {
                DoUpdate("Alter Table", "TransactionPayment", "TransactionPaymentId", "COUNTER");
    
                DoUpdate("Create Multiple", "ALTER TABLE TransactionPayment DROP CONSTRAINT PRIMARYKEY", "", "");
    
                DoUpdate("Create Multiple", "ALTER TABLE TransactionPayment ADD CONSTRAINT IDTP PRIMARY KEY (TransactionPaymentId)", "", "");
            }
  
            //TransactionPaymentArchive
  
            if (!CheckTable("TransactionPaymentArchive")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE TransactionPaymentArchive (RegisterTransactionId NUMBER, " + 
                "TransactionPaymentId NUMBER, " + 
                "CONSTRAINT PkTPA PRIMARY KEY (RegisterTransactionId, TransactionPaymentId))", "", "");
            }
  
            if (!CheckColumn("TransactionPaymentArchive", "TransactionPaymentType")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentArchive", "TransactionPaymentType", "TEXT(50) NULL");
            }

            if (!CheckColumn("TransactionPaymentArchive", "UserName")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentArchive", "UserName", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("TransactionPaymentArchive", "TransactionDate")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentArchive", "TransactionDate", "DATETIME NULL");
            }

            if (!CheckColumn("TransactionPaymentArchive", "CashAmount")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentArchive", "CashAmount", "CURRENCY NULL");
            }
  
            if (!CheckColumn("TransactionPaymentArchive", "CreditCardHolder")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentArchive", "CreditCardHolder", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("TransactionPaymentArchive", "CreditCardNumber")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentArchive", "CreditCardNumber", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("TransactionPaymentArchive", "CreditCardExpiry")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentArchive", "CreditCardExpiry", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("TransactionPaymentArchive", "CreditCardType")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentArchive", "CreditCardType", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("TransactionPaymentArchive", "ChequeNumber")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentArchive", "ChequeNumber", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("TransactionPaymentArchive", "ChequeBank")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentArchive", "ChequeBank", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("TransactionPaymentArchive", "ChequeBranch")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentArchive", "ChequeBranch", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("TransactionPaymentArchive", "DebitCardNumber")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentArchive", "DebitCardNumber", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("TransactionPaymentArchive", "DebitCardBank")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentArchive", "DebitCardBank", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("TransactionPaymentArchive", "AccountNumber")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentArchive", "AccountNumber", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("TransactionPaymentArchive", "AccountName")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentArchive", "AccountName", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("TransactionPaymentArchive", "OtherDetails1")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentArchive", "OtherDetails1", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("TransactionPaymentArchive", "OtherDetails2")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentArchive", "OtherDetails2", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("TransactionPaymentArchive", "AmountTendered")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentArchive", "AmountTendered", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("TransactionPaymentArchive", "AccountPaymentType")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentArchive", "AccountPaymentType", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("TransactionPaymentArchive", "ShiftId")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentArchive", "ShiftId", "NUMBER NULL");
            }
  
            if (!CheckColumn("TransactionPaymentArchive", "SalesCode")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentArchive", "SalesCode", "TEXT(10)");
            }
    
            //TransactionPaymentOverring
  
            if (!CheckColumn("TransactionPaymentOverring", "TransactionPaymentId")) 
            {
                DoUpdate("Create Multiple", "DROP TABLE TransactionPaymentOverring", "", "");
            }
  
            if (!CheckTable("TransactionPaymentOverring"))
            {
                DoUpdate("Create Multiple", "CREATE TABLE TransactionPaymentOverring (TransactionPaymentId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckTable("TransactionPaymentOverring")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE TransactionPaymentOverring (RegisterTransactionId NUMBER PRIMARY KEY)", "", "");
                DoUpdate("Alter Table", "TransactionPaymentOverring", "RegisterTransactionId", "NUMBER");
            }
  
            if (!CheckColumn("TransactionPaymentOverring", "TransactionPaymentType")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentOverring", "TransactionPaymentType", "TEXT(50)");
            }

            if (!CheckColumn("TransactionPaymentOverring", "UserName")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentOverring", "UserName", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPaymentOverring", "TransactionDate")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentOverring", "TransactionDate", "DATETIME");
            }

            if (!CheckColumn("TransactionPaymentOverring", "CashAmount")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentOverring", "CashAmount", "CURRENCY");
            }
  
            if (!CheckColumn("TransactionPaymentOverring", "CreditCardHolder")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentOverring", "CreditCardHolder", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPaymentOverring", "CreditCardNumber")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentOverring", "CreditCardNumber", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPaymentOverring", "CreditCardExpiry")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentOverring", "CreditCardExpiry", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPaymentOverring", "CreditCardType")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentOverring", "CreditCardType", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPaymentOverring", "ChequeNumber")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentOverring", "ChequeNumber", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPaymentOverring", "ChequeBank")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentOverring", "ChequeBank", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPaymentOverring", "ChequeBranch")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentOverring", "ChequeBranch", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPaymentOverring", "DebitCardNumber")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentOverring", "DebitCardNumber", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPaymentOverring", "DebitCardBank")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentOverring", "DebitCardBank", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPaymentOverring", "AccountNumber")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentOverring", "AccountNumber", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPaymentOverring", "AccountName")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentOverring", "AccountName", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPaymentOverring", "OtherDetails1")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentOverring", "OtherDetails1", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPaymentOverring", "OtherDetails2")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentOverring", "OtherDetails2", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPaymentOverring", "AmountTendered")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentOverring", "AmountTendered", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPaymentOverring", "AccountPaymentType")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentOverring", "AccountPaymentType", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionPaymentOverring", "ShiftId")) 
            {
                DoUpdate("Alter Table", "TransactionPaymentOverring", "ShiftId", "NUMBER NULL");
            }
  
  
  
  
            //Transactions
  
            if (!CheckTable("Transactions")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE Transactions (RegisterTransactionId NUMBER, " + 
                "TransactionId NUMBER, " + 
                "TransactionDate DATETIME, " + 
                "StockId TEXT(50), " + 
                "CONSTRAINT PkTRAN PRIMARY KEY (RegisterTransactionId, TransactionId, TransactionDate, StockId))", "", "");
            }
  
            if (!CheckColumn("Transactions", "TransactionType")) 
            {
                DoUpdate("Alter Table", "Transactions", "TransactionType", "TEXT(50)");
            }
  
            if (!CheckColumn("Transactions", "UserName")) 
            {
                DoUpdate("Alter Table", "Transactions", "UserName", "TEXT(50)");
            }
  
            if (!CheckColumn("Transactions", "Description")) 
            {
                DoUpdate("Alter Table", "Transactions", "Description", "TEXT(50)");
            }
  
            if (!CheckColumn("Transactions", "Quantity")) 
            {
                DoUpdate("Alter Table", "Transactions", "Quantity", "CURRENCY");
            }
  
            if (!CheckColumn("Transactions", "UnitPrice")) 
            {
                DoUpdate("Alter Table", "Transactions", "UnitPrice", "TEXT(50)");
            }
  
            if (!CheckColumn("Transactions", "VATAmount")) 
            {
                DoUpdate("Alter Table", "Transactions", "VATAmount", "TEXT(50)");
            }
  
            if (!CheckColumn("Transactions", "VATPercentage")) 
            {
                DoUpdate("Alter Table", "Transactions", "VATPercentage", "TEXT(50)");
            }
  
            if (!CheckColumn("Transactions", "TotalAmount")) 
            {
                DoUpdate("Alter Table", "Transactions", "TotalAmount", "TEXT(50)");
            }
  
            if (!CheckColumn("Transactions", "DiscountAmount")) 
            {
                DoUpdate("Alter Table", "Transactions", "DiscountAmount", "TEXT(50)");
            }
  
            if (!CheckColumn("Transactions", "StockUnitCost")) 
            {
                DoUpdate("Alter Table", "Transactions", "StockUnitCost", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("Transactions", "RegisterName")) 
            {
                DoUpdate("Alter Table", "Transactions", "RegisterName", "TEXT(10) NULL");
            }
  
  
  
  
            //TransactionsArchive
  
            if (!CheckTable("TransactionsArchive")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE TransactionsArchive (RegisterTransactionId NUMBER, " + 
                "TransactionId NUMBER, " + 
                "TransactionDate DATETIME, " + 
                "StockId TEXT(50), " + 
                "CONSTRAINT PkTRA PRIMARY KEY (RegisterTransactionId, TransactionId, TransactionDate, StockId))", "", "");
            }
  
            if (!CheckColumn("TransactionsArchive", "TransactionType")) 
            {
                DoUpdate("Alter Table", "TransactionsArchive", "TransactionType", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("TransactionsArchive", "UserName")) 
            {
                DoUpdate("Alter Table", "TransactionsArchive", "UserName", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("TransactionsArchive", "Description")) 
            {
                DoUpdate("Alter Table", "TransactionsArchive", "Description", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("TransactionsArchive", "Quantity")) 
            {
                DoUpdate("Alter Table", "TransactionsArchive", "Quantity", "CURRENCY NULL");
            }
  
            if (!CheckColumn("TransactionsArchive", "UnitPrice")) 
            {
                DoUpdate("Alter Table", "TransactionsArchive", "UnitPrice", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("TransactionsArchive", "VATAmount")) 
            {
                DoUpdate("Alter Table", "TransactionsArchive", "VATAmount", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("TransactionsArchive", "VATPercentage")) 
            {
                DoUpdate("Alter Table", "TransactionsArchive", "VATPercentage", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("TransactionsArchive", "TotalAmount")) 
            {
                DoUpdate("Alter Table", "TransactionsArchive", "TotalAmount", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("TransactionsArchive", "DiscountAmount")) 
            {
                DoUpdate("Alter Table", "TransactionsArchive", "DiscountAmount", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("TransactionsArchive", "StockUnitCost")) 
            {
                DoUpdate("Alter Table", "TransactionsArchive", "StockUnitCost", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("TransactionsArchive", "RegisterName")) 
            {
                DoUpdate("Alter Table", "TransactionsArchive", "RegisterName", "TEXT(10) NULL");
            }
  
  
  
  
            //TransactionsDispatch
  
            if (!CheckTable("TransactionsDispatch")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE TransactionsDispatch (RegisterTransactionId NUMBER, " + 
                "TransactionId NUMBER, " + 
                "StockId TEXT(50), " + 
                "CONSTRAINT PkTRD PRIMARY KEY (RegisterTransactionId, TransactionId, StockId))", "", "");
            }
  
            if (!CheckColumn("TransactionsDispatch", "UserName")) 
            {
                DoUpdate("Alter Table", "TransactionsDispatch", "UserName", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionsDispatch", "Description")) 
            {
                DoUpdate("Alter Table", "TransactionsDispatch", "Description", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionsDispatch", "Quantity")) 
            {
                DoUpdate("Alter Table", "TransactionsDispatch", "Quantity", "CURRENCY");
            }
  
            if (!CheckColumn("TransactionsDispatch", "VerifiedQuantity")) 
            {
                DoUpdate("Alter Table", "TransactionsDispatch", "VerifiedQuantity", "CURRENCY");
            }
  
  
            //TransactionsOverring
  
            if (!CheckTable("TransactionsOverring")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE TransactionsOverring (RegisterTransactionId NUMBER, " + 
                "TransactionId NUMBER, " + 
                "TransactionDate DATETIME, " + 
                "StockId TEXT(50), " + 
                "CONSTRAINT PkTRO PRIMARY KEY (RegisterTransactionId, TransactionId, TransactionDate, StockId))", "", "");
            }
  
            if (!CheckColumn("TransactionsOverring", "TransactionType")) 
            {
                DoUpdate("Alter Table", "TransactionsOverring", "TransactionType", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionsOverring", "UserName")) 
            {
                DoUpdate("Alter Table", "TransactionsOverring", "UserName", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionsOverring", "Description")) 
            {
                DoUpdate("Alter Table", "TransactionsOverring", "Description", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionsOverring", "Quantity")) 
            {
                DoUpdate("Alter Table", "TransactionsOverring", "Quantity", "CURRENCY");
            }
  
            if (!CheckColumn("TransactionsOverring", "UnitPrice")) 
            {
                DoUpdate("Alter Table", "TransactionsOverring", "UnitPrice", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionsOverring", "VATAmount")) 
            {
                DoUpdate("Alter Table", "TransactionsOverring", "VATAmount", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionsOverring", "VATPercentage")) 
            {
                DoUpdate("Alter Table", "TransactionsOverring", "VATPercentage", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionsOverring", "TotalAmount")) 
            {
                DoUpdate("Alter Table", "TransactionsOverring", "TotalAmount", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionsOverring", "DiscountAmount")) 
            {
                DoUpdate("Alter Table", "TransactionsOverring", "DiscountAmount", "TEXT(50)");
            }
  
            if (!CheckColumn("TransactionsOverring", "StockUnitCost")) 
            {
                DoUpdate("Alter Table", "TransactionsOverring", "StockUnitCost", "TEXT(50) NULL");
            }
  
            if (!CheckColumn("TransactionsOverring", "RegisterName")) 
            {
                DoUpdate("Alter Table", "TransactionsOverring", "RegisterName", "TEXT(10) NULL");
            }
  
  
            //TranId
  
            if (!CheckTable("TranId")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE TranId (TranId NUMBER PRIMARY KEY)", "", "");
            }
  
  
  
  
            //UserRole
  
            if (!CheckTable("UserRole")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE UserRole (Role TEXT(50), " + 
                "UserName TEXT(50), " + 
                "CONSTRAINT PkT PRIMARY KEY (Role, UserName))", "", "");
            }



            //UserSeating
  
            if (!CheckTable("UserSeating")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE UserSeating (UserName TEXT(50), " + 
                "SeatingId NUMBER, " + 
                "CONSTRAINT PkUS PRIMARY KEY (UserName, SeatingId))", "", "");
            }
  
  
  
  
            //WholesaleBuy
  
            if (!CheckTable("WholesaleBuy")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE WholesaleBuy (WholesaleBuyId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("WholesaleBuy", "Description")) 
            {
                DoUpdate("Alter Table", "WholesaleBuy", "Description", "TEXT(20)");
            }
  
            if (!CheckColumn("WholesaleBuy", "UserName")) 
            {
                DoUpdate("Alter Table", "WholesaleBuy", "UserName", "TEXT(50)");
            }
  
            if (!CheckColumn("WholesaleBuy", "UnitPrice")) 
            {
                DoUpdate("Alter Table", "WholesaleBuy", "UnitPrice", "CURRENCY");
            }
  
            if (!CheckColumn("WholesaleBuy", "TransactionDate")) 
            {
                DoUpdate("Alter Table", "WholesaleBuy", "TransactionDate", "DATETIME");
            }
  
            if (!CheckColumn("WholesaleBuy", "Notes")) 
            {
                DoUpdate("Alter Table", "WholesaleBuy", "Notes", "TEXT(255)");
            }
  
            if (!CheckColumn("WholesaleBuy", "AccountNumber")) 
            {
                DoUpdate("Alter Table", "WholesaleBuy", "AccountNumber", "TEXT(20) NULL");
            }
  
            if (!CheckColumn("WholesaleBuy", "IDNumber")) 
            {
                DoUpdate("Alter Table", "WholesaleBuy", "IDNumber", "TEXT(20) NULL");
            }
  
            if (!CheckColumn("WholesaleBuy", "Status")) 
            {
                DoUpdate("Alter Table", "WholesaleBuy", "Status", "TEXT(20) NULL");
            }
  
  
            //WorkOrder
  
            if (!CheckTable("WorkOrder")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE WorkOrder (WorkOrderId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("WorkOrder", "AccountNumber")) 
            {
                DoUpdate("Alter Table", "WorkOrder", "AccountNumber", "TEXT(50)");
            }
  
            if (!CheckColumn("WorkOrder", "AccountName")) 
            {
                DoUpdate("Alter Table", "WorkOrder", "AccountName", "TEXT(100)");
            }
  
            if (!CheckColumn("WorkOrder", "WorkOrderDate")) 
            {
                DoUpdate("Alter Table", "WorkOrder", "WorkOrderDate", "DATETIME");
            }
  
            if (!CheckColumn("WorkOrder", "UserName")) 
            {
                DoUpdate("Alter Table", "WorkOrder", "UserName", "TEXT(50)");
            }
  
            if (!CheckColumn("WorkOrder", "Status")) 
            {
                DoUpdate("Alter Table", "WorkOrder", "Status", "TEXT(20)");
            }
  
            if (!CheckColumn("WorkOrder", "Notes")) 
            {
                DoUpdate("Alter Table", "WorkOrder", "Notes", "TEXT(255)");
            }
  
  
            //WorkOrderItems
  
            if (!CheckTable("WorkOrderItems")) 
            {
                DoUpdate("Create Multiple", "CREATE TABLE WorkOrderItems (WorkOrderItemId COUNTER PRIMARY KEY)", "", "");
            }
  
            if (!CheckColumn("WorkOrderItems", "WorkOrderId")) 
            {
                DoUpdate("Alter Table", "WorkOrderItems", "WorkOrderId", "NUMBER");
            }

            if (!CheckColumn("WorkOrderItems", "StockId")) 
            {
                DoUpdate("Alter Table", "WorkOrderItems", "StockId", "TEXT(50)");
            }
  
            if (!CheckColumn("WorkOrderItems", "Description")) 
            {
                DoUpdate("Alter Table", "WorkOrderItems", "Description", "TEXT(100)");
            }
  
            if (!CheckColumn("WorkOrderItems", "Quantity")) 
            {
                DoUpdate("Alter Table", "WorkOrderItems", "Quantity", "NUMBER");
            }
  
            if (!CheckColumn("WorkOrderItems", "Price")) 
            {
                DoUpdate("Alter Table", "WorkOrderItems", "Price", "CURRENCY");
            }
  
  
            //BookingItemsView
  
            if (!CheckTable("BookingItemsView")) 
            {
                DoUpdate("Create Multiple", "CREATE VIEW BookingItemsView " + 
                "(BookingId, BookingDate, BookingTime, StaffName, UserName, " + 
                "BookingDescription, CustomerName, CustomerSurname, CustomerContact, " + 
                "CustomerId , BookingEnd, Status, Description) AS " + 
                "SELECT Bookings.BookingId, Bookings.BookingDate, Bookings.BookingTime, Bookings.StaffName, " + 
                "Bookings.UserName, Bookings.BookingDescription, Bookings.CustomerName, Bookings.CustomerSurname, " + 
                "Bookings.CustomerContact, Bookings.CustomerId, Bookings.BookingEnd, Bookings.Status, Stock.Description " + 
                "FROM (Bookings INNER JOIN BookingItems ON Bookings.BookingId = BookingItems.BookingId) INNER JOIN " + 
                "Stock ON BookingItems.StockId = Stock.StockId", "", "");
            }
  
  
  
  
            //SAPSStockReleaseDetail
  
            if (!CheckTable("SAPSStockReleaseDetail")) 
            {
                DoUpdate("Create Multiple", "CREATE VIEW SAPSStockReleaseDetail " + 
                "(StockId, Description, SellingPrice, TransactionDate, Status, " + 
                "ReleaseDate, Reason, MemberName, Station, " + 
                "ContactNumber1) AS " + 
                "SELECT CustomerStock.StockId, CustomerStock.Description, CustomerStock.SellingPrice, " + 
                "CustomerStock.TransactionDate , CustomerStock.Status, CustomerStockRelease.ReleaseDate, " + 
                "CustomerStockRelease.Reason , SAPSMember.MemberName, SAPSMember.Station, SAPSMember.ContactNumber1 " + 
                "FROM (CustomerStock INNER JOIN CustomerStockRelease ON CustomerStock.CustomerStockId = CustomerStockRelease.CustomerStockId) " + 
                "INNER JOIN SAPSMember ON CustomerStockRelease.SAPSMemberId = SAPSMember.SAPSMemberId", "", "");
            }

            UpdateVersion(CurVersion);
        }
    }
}
