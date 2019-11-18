using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;

namespace GeneralGlobal
{
    public class GeneralStuff : MarshalByRefObject, IDisposable
    {
        private float VATRate = 14;

        public string ConnectionString;

        public GeneralStuff()
        {
        }

        public int CInt(string IntString)
        {
            try
            {
                return Int16.Parse(IntString);
            }

            catch
            {
                return 0;
            }
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

        public string ChangeCreateToAlter(string TextIn)
        {
            for (int i = 1; i < 20; i++)
            {
                TextIn = ReplaceString(TextIn, "CREATE  ", "CREATE ", StringComparison.CurrentCultureIgnoreCase);
            }

            TextIn = ReplaceString(TextIn, "CREATE PROCEDURE ", "ALTER PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "CREATE  PROCEDURE ", "ALTER PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "CREATE   PROCEDURE ", "ALTER PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "CREATE    PROCEDURE ", "ALTER PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);

            TextIn = ReplaceString(TextIn, "CREATE PROC ", "ALTER PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "CREATE  PROC ", "ALTER PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "CREATE   PROC ", "ALTER PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "CREATE    PROC ", "ALTER PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);

            TextIn = ReplaceString(TextIn, "CREATE FUNCTION ", "ALTER FUNCTION ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "CREATE  FUNCTION ", "ALTER FUNCTION ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "CREATE   FUNCTION ", "ALTER FUNCTION ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "CREATE    FUNCTION ", "ALTER FUNCTION ", StringComparison.CurrentCultureIgnoreCase);

            return TextIn;
        }

        public string ChangeAlterToCreate(string TextIn)
        {
            for (int i = 1; i < 20; i++)
            {
                TextIn = ReplaceString(TextIn, "ALTER  ", "ALTER ", StringComparison.CurrentCultureIgnoreCase);
            }

            TextIn = ReplaceString(TextIn, "ALTER PROCEDURE ", "CREATE PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "ALTER  PROCEDURE ", "CREATE PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "ALTER   PROCEDURE ", "CREATE PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "ALTER    PROCEDURE ", "CREATE PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);

            TextIn = ReplaceString(TextIn, "ALTER PROC ", "CREATE PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "ALTER  PROC ", "CREATE PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "ALTER   PROC ", "CREATE PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "ALTER    PROC ", "CREATE PROCEDURE ", StringComparison.CurrentCultureIgnoreCase);

            TextIn = ReplaceString(TextIn, "ALTER FUNCTION ", "CREATE FUNCTION ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "ALTER  FUNCTION ", "CREATE FUNCTION ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "ALTER   FUNCTION ", "CREATE FUNCTION ", StringComparison.CurrentCultureIgnoreCase);
            TextIn = ReplaceString(TextIn, "ALTER    FUNCTION ", "CREATE FUNCTION ", StringComparison.CurrentCultureIgnoreCase);

            return TextIn;
        }

        public string  GetKey(string KeyName)
        {
            try
            {
                using (QSSystem qss = new QSSystem())
                {
                    return qss.GetSystemData(KeyName);
                }
            }

            catch
            {
                //Do nothing
            }

            return "";
        }

        public void SaveKey(string KeyName, string KeyValue)
        {
            try
            {
                using (QSSystem qss = new QSSystem())
                {
                    int i = qss.SaveSystemData(KeyName, KeyValue);
                }
            }

            catch
            {
                //Do nothing
            }
        }

        public string CheckNull(string StringToCheck)
        {
            string TmpStr = "";

            TmpStr = StringToCheck != null ? StringToCheck : "";
            return TmpStr;
        }

        public string SafeString(string StringToCheck)
        {
            string TmpStr = "";

            TmpStr = StringToCheck.Replace("'", "`");
            return TmpStr;
        }

        public string FormatMoney(string StringToFormat)
        {
            CultureInfo provider;
            NumberStyles styles;

            provider = new CultureInfo("en-US");
            styles = NumberStyles.Currency;

            if (IsNumeric(StringToFormat))
            {
                float i;

                bool result = float.TryParse(StringToFormat, styles, provider, out i);
                return i.ToString("F");
            }
            else
            {
                return "0.00";
            }
        }

        public String FormatDigits(String StringToFormat, int Digits)
        {
            if (IsNumeric(StringToFormat))
            {
                float i;
                int x;
                string TmpFormat = "#0.";

                i = float.Parse(StringToFormat);

                for (x = 0; x < Digits; x++)
                {
                    TmpFormat = TmpFormat.Trim() + "0";
                }

                return i.ToString(TmpFormat);
            }
            else
            {
                return "0.00";
            }
        }

        public bool IsNumeric(string NumericString)
        {
            try
            {
                float.Parse(NumericString);
                return true;
            }

            catch
            {
                return false;
            }
        }

        public bool IsNotZero(string NumericString)
        {
            try
            {
                if (float.Parse(NumericString) != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            catch
            {
                return false;
            }
        }

        public float AddVAT(float AmountIn)
        {
            float TmpAmount = 0;

            TmpAmount = AmountIn * (VATRate / 100);
            return AmountIn + TmpAmount;
        }

        public string AddVAT(string AmountIn)
        {
            float TmpAmount = 0;
            float TmpVAT = 0;

            if (IsNumeric(AmountIn))
            {
                TmpAmount = float.Parse(AmountIn);
                TmpVAT = TmpAmount * (VATRate / 100);
                TmpAmount = TmpAmount + TmpVAT;
                return TmpAmount.ToString("F");
            }
            else
            {
                return "";
            }
        }

        public string SubtractVAT(string AmountIn)
        {
            float TmpAmount = 0;
            
            if (IsNumeric(AmountIn))
            {
                TmpAmount = float.Parse(AmountIn);
                TmpAmount = TmpAmount / (1 + (VATRate / 100));
                return FormatDigits(TmpAmount.ToString(), 6);
            }
            else
            {
                return "";
            }
        }

        public DataTable GetComboData(string SQLString)
        {
            using (DBConnect DC = new DBConnect())
            {
                return DC.DataTable(SQLString);
            }
        }

        public string SupplierName(int SupplierID)
        {
            using (DBConnect GK = new DBConnect())
            {
                IDataReader dr;

                try
                {
                    dr = GK.SqlDataReader("select SupplierName from Supplier where SupplierId = " + SupplierID);

                    while (dr.Read())
                    {
                        return dr["SupplierName"].ToString();
                    }
                }

                catch
                {
                    //Do nothing
                }

                return "";
            }
        }

        //public string DBDate(string SQLStringIn)
        //{
        //    int Pos1;
        //    int Pos2;
        //    string tDate;
        //    string Str1;
        //    string Str2;
        //    string TmpStr;

        //}

//        Public Function DBDate(SQLStringIn As String) As String
//  On Error GoTo ErrTrap
  
//  Dim Pos1   As Long
//  Dim Pos2   As Long
//  Dim tDate  As String
//  Dim Str1   As String
//  Dim Str2   As String
//  Dim TmpStr As String
  
//  If Not AccessDB Then
//    If MySQLDB Then
//      'Date 1
      
//      Pos1 = InStr(1, SQLStringIn, "#")
      
//      If Pos1 > 0 Then
//        Pos2 = InStr(Pos1 + 1, SQLStringIn, "#")
        
//        If Pos2 > 0 Then
//          tDate = Mid(SQLStringIn, Pos1 + 1, Pos2 - Pos1 - 1)
          
//          If IsDate(tDate) Then
//            Str1 = Mid(SQLStringIn, 1, Pos1 - 1) & "'"
//            Str2 = "'" & Mid(SQLStringIn, Pos2 + 1)
//            TmpStr = Str1 & Format(tDate, "yyyy/mm/dd hh:mm:ss") & Str2
//          End If
//        End If
//      Else
//        TmpStr = Replace(SQLStringIn, "#", "'")
//      End If
      
//      'Date 2
      
//      Pos1 = InStr(1, TmpStr, "#")
      
//      If Pos1 > 0 Then
//        Pos2 = InStr(Pos1 + 1, TmpStr, "#")
        
//        If Pos2 > 0 Then
//          tDate = Mid(TmpStr, Pos1 + 1, Pos2 - Pos1 - 1)
          
//          If IsDate(tDate) Then
//            Str1 = Mid(TmpStr, 1, Pos1 - 1) & "'"
//            Str2 = "'" & Mid(TmpStr, Pos2 + 1)
//            TmpStr = Str1 & Format(tDate, "yyyy/mm/dd hh:mm:ss") & Str2
//          End If
//        End If
//      Else
//        TmpStr = Replace(TmpStr, "#", "'")
//      End If
      
//      'Date 3
      
//      Pos1 = InStr(1, TmpStr, "#")
      
//      If Pos1 > 0 Then
//        Pos2 = InStr(Pos1 + 1, TmpStr, "#")
        
//        If Pos2 > 0 Then
//          tDate = Mid(TmpStr, Pos1 + 1, Pos2 - Pos1 - 1)
          
//          If IsDate(tDate) Then
//            Str1 = Mid(TmpStr, 1, Pos1 - 1) & "'"
//            Str2 = "'" & Mid(TmpStr, Pos2 + 1)
//            TmpStr = Str1 & Format(tDate, "yyyy/mm/dd hh:mm:ss") & Str2
//          End If
//        End If
//      Else
//        TmpStr = Replace(TmpStr, "#", "'")
//      End If
      
//      'Date 4
      
//      Pos1 = InStr(1, TmpStr, "#")
      
//      If Pos1 > 0 Then
//        Pos2 = InStr(Pos1 + 1, TmpStr, "#")
        
//        If Pos2 > 0 Then
//          tDate = Mid(TmpStr, Pos1 + 1, Pos2 - Pos1 - 1)
          
//          If IsDate(tDate) Then
//            Str1 = Mid(TmpStr, 1, Pos1 - 1) & "'"
//            Str2 = "'" & Mid(TmpStr, Pos2 + 1)
//            DBDate = Str1 & Format(tDate, "yyyy/mm/dd hh:mm:ss") & Str2
//          End If
//        End If
//      Else
//        DBDate = Replace(TmpStr, "#", "'")
//      End If
//    Else
//      DBDate = Replace(SQLStringIn, "#", "'")
//    End If
//  Else
//    'Date 1
      
//    Pos1 = InStr(1, SQLStringIn, "#")
    
//    If Pos1 > 0 Then
//      Pos2 = InStr(Pos1 + 1, SQLStringIn, "#")
      
//      If Pos2 > 0 Then
//        tDate = Mid(SQLStringIn, Pos1 + 1, Pos2 - Pos1 - 1)
        
//        If IsDate(tDate) Then
//          Str1 = Mid(SQLStringIn, 1, Pos1 - 1) & "#"
//          Str2 = "#" & Mid(SQLStringIn, Pos2 + 1)
//          TmpStr = Str1 & Format(tDate, "dd mmm yyyy hh:mm:ss") & Str2
//        End If
//      End If
//    End If
    
//    'Date 2
    
//    Pos1 = InStr(Pos2 + 2, TmpStr, "#")
    
//    If Pos1 > 0 Then
//      Pos2 = InStr(Pos1 + 1, TmpStr, "#")
      
//      If Pos2 > 0 Then
//        tDate = Mid(TmpStr, Pos1 + 1, Pos2 - Pos1 - 1)
        
//        If IsDate(tDate) Then
//          Str1 = Mid(TmpStr, 1, Pos1 - 1) & "#"
//          Str2 = "#" & Mid(TmpStr, Pos2 + 1)
//          TmpStr = Str1 & Format(tDate, "dd mmm yyyy hh:mm:ss") & Str2
//        End If
//      End If
//    End If
    
//    'Date 3
    
//    Pos1 = InStr(Pos2 + 2, TmpStr, "#")
    
//    If Pos1 > 0 Then
//      Pos2 = InStr(Pos1 + 1, TmpStr, "#")
      
//      If Pos2 > 0 Then
//        tDate = Mid(TmpStr, Pos1 + 1, Pos2 - Pos1 - 1)
        
//        If IsDate(tDate) Then
//          Str1 = Mid(TmpStr, 1, Pos1 - 1) & "#"
//          Str2 = "#" & Mid(TmpStr, Pos2 + 1)
//          TmpStr = Str1 & Format(tDate, "dd mmm yyyy hh:mm:ss") & Str2
//        End If
//      End If
//    End If
    
//    'Date 4
    
//    Pos1 = InStr(Pos2 + 2, TmpStr, "#")
    
//    If Pos1 > 0 Then
//      Pos2 = InStr(Pos1 + 1, TmpStr, "#")
      
//      If Pos2 > 0 Then
//        tDate = Mid(TmpStr, Pos1 + 1, Pos2 - Pos1 - 1)
        
//        If IsDate(tDate) Then
//          Str1 = Mid(TmpStr, 1, Pos1 - 1) & "#"
//          Str2 = "#" & Mid(TmpStr, Pos2 + 1)
//          TmpStr = Str1 & Format(tDate, "dd mmm yyyy hh:mm:ss") & Str2
//        End If
//      End If
//    End If
    
//    If Trim(TmpStr) = "" Then
//      TmpStr = SQLStringIn
//    End If
    
//    DBDate = TmpStr
//  End If
  
//  Exit Function
  
//ErrTrap:
//  If ErrorHandler(Err.Number, Err.Description) = 0 Then
//    Err.Clear
//    Resume Next
//  Else
//    Err.Clear
//    Exit Function
//  End If
//End Function

        private void AddSystemRole(string DataStringIn, string AccessType, string ModName)
        {
            string sql;

            using (DBConnect SK = new DBConnect())
            {
                try
                {
                    sql = "insert into SystemFunctions (Level1, Level2, Level3, FunctionName, Rank, RoleAccess) values " + DataStringIn;
                    
                    SK.SqlCommand(sql);
                }

                catch
                {
                }
            }
        }

        private void RemoveSystemFunctions()
        {
            string sql;

            using (DBConnect SK = new DBConnect())
            {
                try
                {
                    sql = "delete from SystemFunctions";

                    SK.SqlCommand(sql);
                }

                catch
                {
                }
            }
        }

        private void RemoveSystemRoleFunctions()
        {
            string sql;

            using (DBConnect SK = new DBConnect())
            {
                try
                {
                    sql = "delete from SystemRole where ProgramName not in (select FunctionName from SystemFunctions)";

                    SK.SqlCommand(sql);
                }

                catch
                {
                }
            }
        }

        public void CheckSystemRoles()
        {
            string LatestVersion = "2";
            string CurVersion = GetKey("RoleVersion");
            string CurAccessType = "E";

            if (CurVersion.Trim() == "")
            {
                CurVersion = "0";
            }

            if (LatestVersion != CurVersion)
            {
                RemoveSystemFunctions();

                AddSystemRole("('Codes','','','mnuCodes',1000, 'AM')", CurAccessType, "mnuCodes");
                AddSystemRole("('Codes','Code Maintenance','','mnuCodeMaintenance',1005, 'AM')", CurAccessType, "mnuCodeMaintenance");
                AddSystemRole("('Codes','Code Maintenance','New','mnuNewCode',1010, 'AM')", CurAccessType, "mnuNewCode");
                AddSystemRole("('Codes','Code Maintenance','Modify','mnuSaveCode',1020, 'AM')", CurAccessType, "mnuSaveCode");
                AddSystemRole("('Codes','Code Maintenance','Delete','mnuDeleteCode',1030, 'AM')", CurAccessType, "mnuDeleteCode");
                AddSystemRole("('System','','','mnuSystem',2000, 'AM')", CurAccessType, "mnuSystem");
                AddSystemRole("('System','Settings','','mnuSettings',2010, 'AM')", CurAccessType, "mnuSettings");
                AddSystemRole("('System','Sale Conditions','','mnuSaleConditions',2015, 'AM')", CurAccessType, "mnuSaleConditions");
                AddSystemRole("('Stock','','','mnuStock',3000, 'AMCW')", CurAccessType, "mnuStock");
                AddSystemRole("('Stock','Location Maintenance','','mnuLocationMaintenance',3010, 'AM')", CurAccessType, "mnuLocationMaintenance");
                AddSystemRole("('Stock','Location Maintenance','New','mnuNewLocation',3015, 'AM')", CurAccessType, "mnuNewLocation");
                AddSystemRole("('Stock','Location Maintenance','Modify','mnuSaveLocation',3020, 'AM')", CurAccessType, "mnuSaveLocation");
                AddSystemRole("('Stock','Location Maintenance','Delete','mnuDeleteLocation',3030, 'AM')", CurAccessType, "mnuDeleteLocation");
                AddSystemRole("('Stock','Bin Maintenance','','mnuBinMaintenance',3040, 'AM')", CurAccessType, "mnuBinMaintenance");
                AddSystemRole("('Stock','Bin Maintenance','New','mnuNewBin',3050, 'AM')", CurAccessType, "mnuNewBin");
                AddSystemRole("('Stock','Bin Maintenance','Modify','mnuSaveBin',3060, 'AM')", CurAccessType, "mnuSaveBin");
                AddSystemRole("('Stock','Bin Maintenance','Delete','mnuDeleteBin',3070, 'AM')", CurAccessType, "mnuDeleteBin");
                AddSystemRole("('Stock','Stock Maintenance','','mnuStockMaintenance',3080, 'AM')", CurAccessType, "mnuStockMaintenance");
                AddSystemRole("('Stock','Stock Maintenance','New','mnuNewStock',3090, 'AM')", CurAccessType, "mnuNewStock");
                AddSystemRole("('Stock','Stock Maintenance','Modify','mnuSaveStock',3100, 'AM')", CurAccessType, "mnuSaveStock");
                AddSystemRole("('Stock','Stock Maintenance','Delete','mnuDeleteStock',3110, 'AM')", CurAccessType, "mnuDeleteStock");
                AddSystemRole("('Stock','Stock Adjustments','','mnuStockAdjustment',3111, 'AM')", CurAccessType, "mnuStockAdjustment");
                AddSystemRole("('Stock','Case Lots Definition','','mnuCaseLots',3113, 'AM')", CurAccessType, "mnuCaseLots");
                AddSystemRole("('Stock','Stock History','','mnuStockHistory',3115, 'AM')", CurAccessType, "mnuStockHistory");
                AddSystemRole("('Stock','Dispatch Stock From PDC','','mnuDispatchStockPDC',3120, 'AM')", CurAccessType, "mnuDispatchStockPDC");
                AddSystemRole("('Stock','Receive Dispatched Stock','','mnuReceiveDispatchedStock',3130, 'AM')", CurAccessType, "mnuReceiveDispatchedStock");
                AddSystemRole("('Stock','Goods Returned','','mnuGoodsReturned',3135, 'AMCW')", CurAccessType, "mnuGoodsReturned");
                AddSystemRole("('Stock','Product Specials','','mnuProductSpecials',3140, 'AM')", CurAccessType, "mnuProductSpecials");
                AddSystemRole("('Stock','Customer Discount Matrix','','mnuDiscountMatrix',3145, 'AM')", CurAccessType, "mnuDiscountMatrix");
                AddSystemRole("('Stock','Happy Hour Specials','','mnuHappyHour',3146, 'AM')", CurAccessType, "mnuHappyHour");
                AddSystemRole("('Stock','Gift Vouchers','','mnuGiftVouchers',3148, 'AMCW')", CurAccessType, "mnuGiftVouchers");
                AddSystemRole("('Stock','Quick Add Stock','','mnuQuickStock',3150, 'AM')", CurAccessType, "mnuQuickStock");
                AddSystemRole("('Stock','Stock Take','','mnuStockTake',3160, 'AMCW')", CurAccessType, "mnuStockTake");
                AddSystemRole("('Stock','Stock Take Form','','mnuStockTakeForm',3170, 'AM')", CurAccessType, "mnuStockTakeForm");
                AddSystemRole("('Stock','Stock Detail List','','mnuStockDetailList',3175, 'AMCW')", CurAccessType, "mnuStockDetailList");
                AddSystemRole("('Stock','Stock Catalog','','mnuStockCatalog',3180, 'AM')", CurAccessType, "mnuStockCatalog");
                AddSystemRole("('Stock','Work Orders','','mnuWorkOrders',3190, 'AM')", CurAccessType, "mnuWorkOrders");
                AddSystemRole("('Stock','Buy Customer Goods','','mnuBuyCustomerGoods',3200, 'AM')", CurAccessType, "mnuBuyCustomerGoods");
                AddSystemRole("('Stock','Release Stock to Sales','','mnuReleaseStockToSales',3210, 'AM')", CurAccessType, "mnuReleaseStockToSales");
                AddSystemRole("('Stock','SAPS Stock Release','','mnuSAPSStockRelease',3220, 'AM')", CurAccessType, "mnuSAPSStockRelease");
                AddSystemRole("('Stock','Wholesale Buy','','mnuWholesaleBuy',3222, 'AM')", CurAccessType, "mnuWholesaleBuy");
                AddSystemRole("('Stock','Split Wholesale Buy','','mnuSplitWholesaleBuy',3224, 'AM')", CurAccessType, "mnuSplitWholesaleBuy");
                AddSystemRole("('Stock','Garments','','mnuGarments',3230, 'AM')", CurAccessType, "mnuGarments");
                AddSystemRole("('Stock','Garments','Fabric Usage','mnuFabricUsage',3235, 'AM')", CurAccessType, "mnuFabricUsage");
                AddSystemRole("('Stock','Garments','Stock at Branch','mnuStockAtBranch',3240, 'AM')", CurAccessType, "mnuStockAtBranch");
                AddSystemRole("('Stock','Agent Stock Out','','mnuAgentStock',3264, 'AM')", CurAccessType, "mnuAgentStock");
                AddSystemRole("('Stock','Agent Stock Out','Book Out','mnuAgentStockOut',3265, 'AM')", CurAccessType, "mnuAgentStockOut");
                AddSystemRole("('Stock','Agent Stock Out','New','mnuNewAgentStock',3266, 'AM')", CurAccessType, "mnuNewAgentStock");
                AddSystemRole("('Stock','Agent Stock Out','Modify','mnuSaveAgentStock',3267, 'AM')", CurAccessType, "mnuSaveAgentStock");
                AddSystemRole("('Stock','Agent Stock Out','Delete','mnuDeleteAgentStock',3268, 'AM')", CurAccessType, "mnuDeleteAgentStock");
                AddSystemRole("('Stock','Agent Stock In','','mnuAgentStockIn',3269, 'AM')", CurAccessType, "mnuAgentStockIn");
                AddSystemRole("('Stock','Import','','mnuImport',3270, 'AM')", CurAccessType, "mnuImport");
                AddSystemRole("('Stock','Scale Functions','','mnuScaleFunctions',3275, 'AM')", CurAccessType, "mnuScaleFunctions");
                AddSystemRole("('Stock','Scale Functions','SM-25 Download','mnuSM25Download',3276, 'AM')", CurAccessType, "mnuSM25Download");
                AddSystemRole("('Stock','Scale Functions','Mettler Toledo Download','mnuMettlerToledoDownload',3277, 'AM')", CurAccessType, "mnuMettlerToledoDownload");
                AddSystemRole("('Stock','Import','','mnuImport',3279, 'AM')", CurAccessType, "mnuImport");
                AddSystemRole("('Stock','Import','Import External Stock','mnuImportExternalStock',3280, 'AM')", CurAccessType, "mnuImportExternalStock");
                AddSystemRole("('Stock','Bulk Barcode Printing','','mnuBulkBarcodePrinting',3290, 'AMC')", CurAccessType, "mnuBulkBarcodePrinting");
                AddSystemRole("('Stock','TOTalizer Download','','mnuTotalizerDownload',3300, 'AM')", CurAccessType, "mnuTotalizerDownload");
                AddSystemRole("('Stock','Serial Number History','','mnuSerialNumberHistory',3310, 'AM')", CurAccessType, "mnuSerialNumberHistory");
                AddSystemRole("('Stock','Register Macro','','mnuRegisterMacro',3320, 'AM')", CurAccessType, "mnuRegisterMacro");
                AddSystemRole("('Stock','Performance Maintenance','','mnuPerformanceMaintenance',3330, 'AM')", CurAccessType, "mnuPerformanceMaintenance");
                AddSystemRole("('Register','','','mnuRegister',4000, 'AMCW')", CurAccessType, "mnuRegister");
                AddSystemRole("('Register','Allow Discount','','mnuAllowDiscount',4010, 'AMCW')", CurAccessType, "mnuAllowDiscount");
                AddSystemRole("('Register','Overring','','mnuOverring',4020, 'AM')", CurAccessType, "mnuOverring");
                AddSystemRole("('Register','Pro Forma Slip','','mnuProformaSlip',4030, 'AMCW')", CurAccessType, "mnuProformaSlip");
                AddSystemRole("('Register','Discount Override','','mnuDiscountOverride',4040, 'AM')", CurAccessType, "mnuDiscountOverride");
                AddSystemRole("('Register','Allow Register Goods Return','','mnuAllowRegisterGoodsReturn',4045, 'AM')", CurAccessType, "mnuAllowRegisterGoodsReturn");
                AddSystemRole("('Register','Allow Register Increment Quantity','','mnuAllowRegisterInc',4041, 'AMCW')", CurAccessType, "mnuAllowRegisterInc");
                AddSystemRole("('Register','Allow Register Decrement Quantity','','mnuAllowRegisterDec',4042, 'AMCW')", CurAccessType, "mnuAllowRegisterDec");
                AddSystemRole("('Register','Allow Register Line Up','','mnuAllowRegisterLineUp',4043, 'AMCW')", CurAccessType, "mnuAllowRegisterLineUp");
                AddSystemRole("('Register','Allow Register Line Down','','mnuAllowRegisterLineDown',4044, 'AMCW')", CurAccessType, "mnuAllowRegisterLineDown");
                AddSystemRole("('Register','Allow Register Cancel','','mnuAllowRegisterCancel',4046, 'AMCW')", CurAccessType, "mnuAllowRegisterCancel");
                AddSystemRole("('Register','Allow Register Suspend','','mnuAllowRegisterSuspend',4047, 'AMCW')", CurAccessType, "mnuAllowRegisterSuspend");
                AddSystemRole("('Register','Allow Register Unsuspend','','mnuAllowRegisterUnsuspend',4048, 'AMCW')", CurAccessType, "mnuAllowRegisterUnsuspend");
                AddSystemRole("('Register','Store Expenses','','mnuStoreExpense',4050, 'AMCW')", CurAccessType, "mnuStoreExpense");
                AddSystemRole("('Register','Allow Register Cash','','mnuAllowRegisterCash',4051, 'AMCW')", CurAccessType, "mnuAllowRegisterCash");
                AddSystemRole("('Register','Allow Register Credit Card','','mnuAllowRegisterCreditCard',4052, 'AMCW')", CurAccessType, "mnuAllowRegisterCreditCard");
                AddSystemRole("('Register','Allow Register Cheque','','mnuAllowRegisterCheque',4053, 'AMCW')", CurAccessType, "mnuAllowRegisterCheque");
                AddSystemRole("('Register','Allow Register Debit Card','','mnuAllowRegisterDebitCard',4054, 'AMCW')", CurAccessType, "mnuAllowRegisterDebitCard");
                AddSystemRole("('Register','Allow Register Account','','mnuAllowRegisterAccount',4055, 'AMCW')", CurAccessType, "mnuAllowRegisterAccount");
                AddSystemRole("('Register','Allow Register Other','','mnuAllowRegisterOther',4056, 'AMCW')", CurAccessType, "mnuAllowRegisterOther");
                AddSystemRole("('Register','Allow Close Shift','','mnuAllowCloseShift',4057, 'AMCW')", CurAccessType, "mnuAllowCloseShift");
                AddSystemRole("('Register','Allow Negative Payout','','mnuAllowNegativePayout',4058, 'AM')", CurAccessType, "mnuAllowNegativePayout");
                AddSystemRole("('Register','Allow Re-print Slip','','mnuAllowPrintSlip',4059, 'AMCW')", CurAccessType, "mnuAllowPrintSlip");
                AddSystemRole("('Register','Allow Register Exit','','mnuRegisterExit',4060, 'AMCW')", CurAccessType, "mnuRegisterExit");
                AddSystemRole("('Register','Allow No Sale','','mnuAllowNoSale',4061, 'AM')", CurAccessType, "mnuAllowNoSale");
                AddSystemRole("('Register','Allow Register Delete','','mnuRegisterDelete',4070, 'AMCW')", CurAccessType, "mnuRegisterDelete");
                AddSystemRole("('Menu','','','mnuMenu',5000, 'AM')", CurAccessType, "mnuMenu");
                AddSystemRole("('Menu','New','','mnuNewMenu',5010, 'AM')", CurAccessType, "mnuNewMenu");
                AddSystemRole("('Menu','Modify','','mnuSaveMenu',5020, 'AM')", CurAccessType, "mnuSaveMenu");
                AddSystemRole("('Menu','Delete','','mnuDeleteMenu',5030, 'AM')", CurAccessType, "mnuDeleteMenu");
                AddSystemRole("('Customers','','','mnuCustomer',6000, 'AMCW')", CurAccessType, "mnuCustomer");
                AddSystemRole("('Customers','Customer Maintenance','','mnuCustomerMaintenance',6010, 'AMCW')", CurAccessType, "mnuCustomerMaintenance");
                AddSystemRole("('Customers','Customer Maintenance','New','mnuNewCustomer',6020, 'AMCW')", CurAccessType, "mnuNewCustomer");
                AddSystemRole("('Customers','Customer Maintenance','Modify','mnuSaveCustomer',6030, 'AMCW')", CurAccessType, "mnuSaveCustomer");
                AddSystemRole("('Customers','Customer Maintenance','Delete','mnuDeleteCustomer',6040, 'AM')", CurAccessType, "mnuDeleteCustomer");
                AddSystemRole("('Customers','Quotes','','mnuQuote',6050, 'AMCW')", CurAccessType, "mnuQuote");
                AddSystemRole("('Customers','Quotes','New','mnuNewQuote',6060, 'AMCW')", CurAccessType, "mnuNewQuote");
                AddSystemRole("('Customers','Quotes','Modify','mnuSaveQuote',6070, 'AMCW')", CurAccessType, "mnuSaveQuote");
                AddSystemRole("('Customers','Quotes','Delete','mnuDeleteQuote',6080, 'AMCW')", CurAccessType, "mnuDeleteQuote");
                AddSystemRole("('Customers','Quotation List','','mnuQuotationList',6082, 'AMCW')", CurAccessType, "mnuQuotationList");
                AddSystemRole("('Customers','Create Order From Quote','','mnuCreateOrderFromQuote',6084, 'AMCW')", CurAccessType, "mnuCreateOrderFromQuote");
                AddSystemRole("('Customers','Create Sale From Quote','','mnuCreateSaleFromQuote',6086, 'AMCW')", CurAccessType, "mnuCreateSaleFromQuote");
                AddSystemRole("('Customers','Customer Invoice','','mnuInvoice',6090, 'AMCW')", CurAccessType, "mnuInvoice");
                AddSystemRole("('Customers','Customer Invoice','New','mnuNewInvoice',6100, 'AMCW')", CurAccessType, "mnuNewInvoice");
                AddSystemRole("('Customers','Customer Invoice','Modify','mnuSaveInvoice',6110, 'AMCW')", CurAccessType, "mnuSaveInvoice");
                AddSystemRole("('Customers','Customer Invoice','Delete','mnuDeleteInvoice',6120, 'AMCW')", CurAccessType, "mnuDeleteInvoice");
                AddSystemRole("('Customers','Payment','','mnuPayment',6130, 'AMCW')", CurAccessType, "mnuPayment");
                AddSystemRole("('Customers','Vehicles','','mnuCustomerVehicles',6135, 'AM')", CurAccessType, "mnuCustomerVehicles");
                AddSystemRole("('Customers','Statement','','mnuCustomerStatement',6138, 'AMCW')", CurAccessType, "mnuCustomerStatement");
                AddSystemRole("('Customers','Customer Age Analysis','','mnuCustomerAgeAnalysis',6140, 'AMCW')", CurAccessType, "mnuCustomerAgeAnalysis");
                AddSystemRole("('Customers','Customer Age Analysis (All)','','mnuCustomerAgeAnalysisAll',6150, 'AM')", CurAccessType, "mnuCustomerAgeAnalysisAll");
                AddSystemRole("('Customers','Sales Code Maintenance','','mnuSalesCodeMaintenance',6160, 'AM')", CurAccessType, "mnuSalesCodeMaintenance");
                AddSystemRole("('Customers','Customer List Report','','mnuCustomerListReport',6170, 'AM')", CurAccessType, "mnuCustomerListReport");
                AddSystemRole("('Customers','Account Journals','','mnuAccountJournals',6180, 'AM')", CurAccessType, "mnuAccountJournals");
                AddSystemRole("('Suppliers','','','mnuDebtors',7000, 'AM')", CurAccessType, "mnuDebtors");
                AddSystemRole("('Suppliers','Supplier Maintenance','','mnuCreditorMaintenance',7010, 'AM')", CurAccessType, "mnuCreditorMaintenance");
                AddSystemRole("('Suppliers','Supplier Maintenance','New','mnuNewCreditor',7020, 'AM')", CurAccessType, "mnuNewCreditor");
                AddSystemRole("('Suppliers','Supplier Maintenance','Modify','mnuSaveCreditor',7030, 'AM')", CurAccessType, "mnuSaveCreditor");
                AddSystemRole("('Suppliers','Supplier Maintenance','Delete','mnuDeleteCreditor',7040, 'AM')", CurAccessType, "mnuDeleteCreditor");
                AddSystemRole("('Suppliers','Receive Stock','','mnuReceiveStock',7050, 'AMCW')", CurAccessType, "mnuReceiveStock");
                AddSystemRole("('Suppliers','Buy Supplier Stock','','mnuBuySupplierStock',7053, 'AM')", CurAccessType, "mnuBuySupplierStock");
                AddSystemRole("('Suppliers','Buy Supplier Stock','Suspend Buy Supplier Stock','mnuSuspendBuySupplierStock',7054, 'AMCW')", CurAccessType, "mnuSuspendBuySupplierStock");
                AddSystemRole("('Suppliers','Buy Supplier Stock','Finalize Buy Supplier Stock','mnuFinalizeBuySupplierStock',7055, 'AM')", CurAccessType, "mnuFinalizeBuySupplierStock");
                AddSystemRole("('Suppliers','Create Order From Sale','','mnuCreateOrderFromSale',7056, 'AMCW')", CurAccessType, "mnuCreateOrderFromSale");
                AddSystemRole("('Suppliers','Supplier Payment','','mnuSupplierPayment',7060, 'AM')", CurAccessType, "mnuSupplierPayment");
                AddSystemRole("('Suppliers','Supplier Age Analysis','','mnuSupplierAgeAnalysis',7070, 'AM')", CurAccessType, "mnuSupplierAgeAnalysis");
                AddSystemRole("('Suppliers','Supplier Age Analysis (All)','','mnuSupplierAgeAnalysisAll',7080, 'AM')", CurAccessType, "mnuSupplierAgeAnalysisAll");
                AddSystemRole("('Suppliers','Supplier Details Report','','mnuSupplierDetailsReport',7100, 'AM')", CurAccessType, "mnuSupplierDetailsReport");
                AddSystemRole("('Suppliers','Marketers','','mnuMarketer',7200, 'AM')", CurAccessType, "mnuMarketer");
                AddSystemRole("('Suppliers','Marketers','Marketer Expenses','mnuMarketerExpenses',7210, 'AM')", CurAccessType, "mnuMarketerExpenses");
                AddSystemRole("('Suppliers','Marketers','Payment to Marketer','mnuMarketerPayment',7220, 'AM')", CurAccessType, "mnuMarketerPayment");
                AddSystemRole("('Suppliers','Marketers','Receive Payment from Marketer','mnuMarketerReceivePayment',7225, 'AM')", CurAccessType, "mnuMarketerReceivePayment");
                AddSystemRole("('Suppliers','Marketers','Marketer Reconciliation','mnuMarketerReconciliation',7230, 'AM')", CurAccessType, "mnuMarketerReconciliation");
                AddSystemRole("('Suppliers','Marketers','Marketer Detail Recon','mnuMarketerDetailRecon',7235, 'AM')", CurAccessType, "mnuMarketerDetailRecon");
                AddSystemRole("('Suppliers','Marketers','Standard Markup','mnuMarketerStandardMarkup',7240, 'AM')", CurAccessType, "mnuMarketerStandardMarkup");
                AddSystemRole("('Suppliers','Marketers','Expense Setup','mnuExpenseSetup',7250, 'AM')", CurAccessType, "mnuExpenseSetup");
                AddSystemRole("('Orders','','','mnuOrders',8000, 'AM')", CurAccessType, "mnuOrders");
                AddSystemRole("('Orders','Order Maintenance','','mnuOrderMaintenance',8010, 'AM')", CurAccessType, "mnuOrderMaintenance");
                AddSystemRole("('Orders','Order Maintenance','New','mnuNewOrder',8020, 'AM')", CurAccessType, "mnuNewOrder");
                AddSystemRole("('Orders','Order Maintenance','Modify','mnuSaveOrder',8030, 'AM')", CurAccessType, "mnuSaveOrder");
                AddSystemRole("('Orders','Order Maintenance','Delete','mnuDeleteOrder',8040, 'AM')", CurAccessType, "mnuDeleteOrder");
                AddSystemRole("('Appointments','','','mnuAppointments',9000, 'AM')", CurAccessType, "mnuAppointments");
                AddSystemRole("('Repairs','','','mnuRepairs',10000, 'AM')", CurAccessType, "mnuRepairs");
                AddSystemRole("('Repairs','Receive Appliance','','mnuReceiveAppliance',10010, 'AM')", CurAccessType, "mnuReceiveAppliance");
                AddSystemRole("('Repairs','Repair Appliance','','mnuRepairAppliance',10020, 'AM')", CurAccessType, "mnuRepairAppliance");
                AddSystemRole("('Repairs','Invoice','','mnuRepairInvoice',10030, 'AM')", CurAccessType, "mnuRepairInvoice");
                AddSystemRole("('Repairs','Delivery Note','','mnuRepairDeliveryNote',10040, 'AM')", CurAccessType, "mnuRepairDeliveryNote");
                AddSystemRole("('Repairs','Receive Appliance from Repairs','','mnuReceiveApplianceFromRepairs',10050, 'AM')", CurAccessType, "mnuReceiveApplianceFromRepairs");
                AddSystemRole("('Repairs','Send Appliance for Repairs','','mnuSendApplianceForRepairs',10060, 'AM')", CurAccessType, "mnuSendApplianceForRepairs");
                AddSystemRole("('Security','','','mnuSecurity',12000, 'AM')", CurAccessType, "mnuSecurity");
                AddSystemRole("('Security','Users','','mnuSystemUsers',12010, 'A')", CurAccessType, "mnuSystemUsers");
                AddSystemRole("('Security','Users','New','mnuNewUser',12020, 'A')", CurAccessType, "mnuNewUser");
                AddSystemRole("('Security','Users','Modify','mnuSaveUser',12030, 'A')", CurAccessType, "mnuSaveUser");
                AddSystemRole("('Security','Users','Delete','mnuDeleteUser',12040, 'A')", CurAccessType, "mnuDeleteUser");
                AddSystemRole("('Security','Roles','','mnuSystemRoles',12050, 'A')", CurAccessType, "mnuSystemRoles");
                AddSystemRole("('Security','Roles','Delete','mnuDeleteRole',12080, 'A')", CurAccessType, "mnuDeleteRole");
                AddSystemRole("('Security','Profiles','','mnuUserProfiles',12090, 'A')", CurAccessType, "mnuUserProfiles");
                AddSystemRole("('Security','Profiles','Link User to Role','mnuUserToRole',12100, 'A')", CurAccessType, "mnuUserToRole");
                AddSystemRole("('Security','Audit Trail','','mnuViewAuditTrail',12110, 'AM')", CurAccessType, "mnuViewAuditTrail");
                AddSystemRole("('Export','','','mnuExport',13000, 'A')", CurAccessType, "mnuExport");
                AddSystemRole("('Export','Customer Detail','','mnuExportCustomerDetail',13010, 'AM')", CurAccessType, "mnuExportCustomerDetail");
                AddSystemRole("('Export','Sales Detail','','mnuExportSalesDetail',13020, 'AM')", CurAccessType, "mnuExportSalesDetail");
                AddSystemRole("('Export','Stock Detail','','mnuExportStockDetail',13030, 'AM')", CurAccessType, "mnuExportStockDetail");
                AddSystemRole("('Pastel Export','','','mnuPastelExport',13100, 'A')", CurAccessType, "mnuPastelExport");
                AddSystemRole("('Pastel Export','Daily Sales','','mnuPastelExportDailySales',13110, 'A')", CurAccessType, "mnuPastelExportDailySales");
                AddSystemRole("('Backup','','','mnuBackup',13500, 'AMCW')", CurAccessType, "mnuBackup");
                AddSystemRole("('Backup','Do Daily Backup','','mnuDoDailyBackup',13510, 'AMCW')", CurAccessType, "mnuDoDailyBackup");
                AddSystemRole("('Backup','Set Backup Path','','mnuSetBackupPath',13520, 'AMCW')", CurAccessType, "mnuSetBackupPath");
                AddSystemRole("('Branches','','','mnuBranches',13600, 'AM')", CurAccessType, "mnuBranches");
                AddSystemRole("('Branches','Branch Detail','','mnuBranchDetail',13610, 'AM')", CurAccessType, "mnuBranchDetail");
                AddSystemRole("('Branches','Connect to Branch','','mnuConnectToBranch',13620, 'AM')", CurAccessType, "mnuConnectToBranch");
                AddSystemRole("('Branches','Connect Local','','mnuConnectLocal',13630, 'AM')", CurAccessType, "mnuConnectLocal");
                AddSystemRole("('Reports','','','mnuReports',14000, 'AM')", CurAccessType, "mnuReports");
                AddSystemRole("('Reports','Daily Sales Detail','','mnuDailySalesDetail',14010, 'AM')", CurAccessType, "mnuDailySalesDetail");
                AddSystemRole("('Reports','Daily Sales Summary','','mnuDailySalesSummary',14020, 'AM')", CurAccessType, "mnuDailySalesSummary");
                AddSystemRole("('Reports','Daily Sales Analysis','','mnuDailySalesAnalysis',14030, 'AM')", CurAccessType, "mnuDailySalesAnalysis");
                AddSystemRole("('Reports','Sales Summary By Category','','mnuSalesSummaryByCategory',14035, 'AM')", CurAccessType, "mnuSalesSummaryByCategory");
                AddSystemRole("('Reports','Sales Summary By Customer','','mnuSalesSummaryByCustomer',14036, 'AM')", CurAccessType, "mnuSalesSummaryByCustomer");
                AddSystemRole("('Reports','Sales by Transaction','','mnuSalesByTransaction',14040, 'AM')", CurAccessType, "mnuSalesByTransaction");
                AddSystemRole("('Reports','Transaction Detail','','mnuRepTransactionDetail',14050, 'AM')", CurAccessType, "mnuRepTransactionDetail");
                AddSystemRole("('Reports','VAT Summary','','mnuRepVATSummary',14060, 'AM')", CurAccessType, "mnuRepVATSummary");
                AddSystemRole("('Reports','Register Balance Sheet','','mnuRepRegisterBalance',14070, 'AM')", CurAccessType, "mnuRepRegisterBalance");
                AddSystemRole("('Reports','Sales by Cashier','','mnuSalesByCashier',14080, 'AM')", CurAccessType, "mnuSalesByCashier");
                AddSystemRole("('Reports','Sales by Cashier By Shift','','mnuSalesByCashierByShift',14085, 'AM')", CurAccessType, "mnuSalesByCashierByShift");
                AddSystemRole("('Reports','Sales by Shift','','mnuSalesByShift',14090, 'AM')", CurAccessType, "mnuSalesByShift");
                AddSystemRole("('Reports','Sales by Register','','mnuRepSalesByRegister',14093, 'AM')", CurAccessType, "mnuRepSalesByRegister");
                AddSystemRole("('Reports','Sales Detail by Shift','','mnuRepSalesDetailByShift',14095, 'AM')", CurAccessType, "mnuRepSalesDetailByShift");
                AddSystemRole("('Reports','Sales Detail by Shift By Category','','mnuRepSalesDetailByShiftByCat',14096, 'AM')", CurAccessType, "mnuRepSalesDetailByShiftByCat");
                AddSystemRole("('Reports','Sales by Stock','','mnuSalesByStock',14100, 'AM')", CurAccessType, "mnuSalesByStock");
                AddSystemRole("('Reports','Sales by Supplier','','mnuSalesBySupplier',14101, 'AM')", CurAccessType, "mnuSalesBySupplier");
                AddSystemRole("('Reports','Sales by Category','','mnuSalesByCategory',14102, 'AM')", CurAccessType, "mnuSalesByCategory");
                AddSystemRole("('Reports','Sales Commission','','mnuRepSalesCommission',14103, 'AM')", CurAccessType, "mnuRepSalesCommission");
                AddSystemRole("('Reports','Sales Detail by Category By Cashier','','mnuRepSalesDCatCash',14104, 'AM')", CurAccessType, "mnuRepSalesDCatCash");
                AddSystemRole("('Reports','Sales Summary by Transaction','','mnuSalesSummaryByTransaction',14110, 'AM')", CurAccessType, "mnuSalesSummaryByTransaction");
                AddSystemRole("('Reports','Stock Variance','','mnuStockVariance',14115, 'AM')", CurAccessType, "mnuStockVariance");
                AddSystemRole("('Reports','Stock Take Variance','','mnuStockTakeVariance',14117, 'AM')", CurAccessType, "mnuStockTakeVariance");
                AddSystemRole("('Reports','Stock Value','','mnuStockValue',14120, 'AM')", CurAccessType, "mnuStockValue");
                AddSystemRole("('Reports','Cost vs Sales','','mnuCostVsSale',14130, 'AM')", CurAccessType, "mnuCostVsSale");
                AddSystemRole("('Reports','High Stock Movement','','mnuHighStockMovement',14140, 'AM')", CurAccessType, "mnuHighStockMovement");
                AddSystemRole("('Reports','Low Stock Level By Supplier','','mnuLowStockBySupplier',14145, 'AM')", CurAccessType, "mnuLowStockBySupplier");
                AddSystemRole("('Reports','Low Stock Level','','mnuLowStockLevel',14150, 'AM')", CurAccessType, "mnuLowStockLevel");
                AddSystemRole("('Reports','Low Stock Movement','','mnuLowStockMovement',14160, 'AM')", CurAccessType, "mnuLowStockMovement");
                AddSystemRole("('Reports','Stock Location','','mnuStockLocation',14170, 'AM')", CurAccessType, "mnuStockLocation");
                AddSystemRole("('Reports','Goods Returned','','mnuViewGoodsReturned',14180, 'AM')", CurAccessType, "mnuViewGoodsReturned");
                AddSystemRole("('Reports','Transaction Overring','','mnuViewOverring',14190, 'AM')", CurAccessType, "mnuViewOverring");
                AddSystemRole("('Reports','View Negative Suspended Trans','','mnuViewSuspendedTran',14195, 'AM')", CurAccessType, "mnuViewSuspendedTran");
                AddSystemRole("('Reports','Supplier Purchases','','mnuSupplierPurchases',14200, 'AM')", CurAccessType, "mnuSupplierPurchases");
                AddSystemRole("('Reports','View Transaction Numbers','','mnuViewTransactionNumbers',14210, 'AM')", CurAccessType, "mnuViewTransactionNumbers");
                AddSystemRole("('Charts','','','mnuCharts',15000, 'AM')", CurAccessType, "mnuCharts");
                AddSystemRole("('Charts','Daily Sales Summary','','mnuChartDailySalesSummary',15010, 'AM')", CurAccessType, "mnuChartDailySalesSummary");
                AddSystemRole("('Charts','Daily Sales Summary by Payment Type','','mnuChartDSSPaymentType',15020, 'AM')", CurAccessType, "mnuChartDSSPaymentType");
                AddSystemRole("('Charts','Sales Summary by Category (All)','','mnuChartSSCategoryAll',15030, 'AM')", CurAccessType, "mnuChartSSCategoryAll");
                AddSystemRole("('Charts','Sales Summary by Category (Period)','','mnuChartSSCategoryP',15040, 'AM')", CurAccessType, "mnuChartSSCategoryP");
                AddSystemRole("('Charts','Sales Summary by Month','','mnuChartSSMonth',15050, 'AM')", CurAccessType, "mnuChartSSMonth");
                AddSystemRole("('Time and Attendance','','','mnuTimeAndAttendance',16000, 'AMCW')", CurAccessType, "mnuTimeAndAttendance");
                AddSystemRole("('Time and Attendance','Personnel Maintenance','','mnuPersonnelMaintenance',16100, 'AM')", CurAccessType, "mnuPersonnelMaintenance");
                AddSystemRole("('Time and Attendance','Personnel Maintenance','New','mnuNewPerson',16110, 'AM')", CurAccessType, "mnuNewPerson");
                AddSystemRole("('Time and Attendance','Personnel Maintenance','Modify','mnuSavePerson',16120, 'AM')", CurAccessType, "mnuSavePerson");
                AddSystemRole("('Time and Attendance','Personnel Maintenance','Delete','mnuDeletePerson',16130, 'AM')", CurAccessType, "mnuDeletePerson");
                AddSystemRole("('Time and Attendance','Clock In and Out','','mnuClockInOut',16200, 'AMCW')", CurAccessType, "mnuClockInOut");
                AddSystemRole("('Time and Attendance','Movement Detail','','mnuPersonMovementDetail',16300, 'AM')", CurAccessType, "mnuPersonMovementDetail");
                AddSystemRole("('Time and Attendance','Personnel Attendance','','mnuPersonnelAttendance',16400, 'AM')", CurAccessType, "mnuPersonnelAttendance");
                AddSystemRole("('Internet Cafe','','','mnuInternetCafe',17000, 'AM')", CurAccessType, "mnuInternetCafe");

                SaveKey("RoleVersion", LatestVersion);

                RemoveSystemRoleFunctions();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
