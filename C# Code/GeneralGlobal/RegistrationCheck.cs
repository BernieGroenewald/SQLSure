using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
//using MySql.Data;
//using MySql.Data.MySqlClient;
using GeneralGlobal;
using System.Data.OleDb;

namespace GeneralGlobal
{
    class RegistrationCheck
    {
        public RegistrationCheck()
        {
        }
  

         public string GetLastKnownDate()
         {
             // Try to get the last known date from the system.
             // Return the current system date for confirmation if no known date was set previously.
             // If we find a known date on the system and it is greater than the current system date,
             // the current system date might have been set to a day in the past.
             // We need to get the correct date and time for transactions.

              string CurKey;
              int TmpYear;
              int TmpMonth;
              int TmpDay;
              //int CurYear;
              //int CurMonth;
              //int CurDay;
              //int KnownDay;
              //int ThisDay;
              string DtStr;
              //int v1;
              //int v3;
              //int v4;
              //int v5;
              //string vx;
              //int TmpYr;
              //int TmpMth;
              //string TmpKey;
  
              using (GeneralGlobal.GeneralStuff GS = new GeneralGlobal.GeneralStuff())
              {
                  CurKey = GS.GetKey("Public Key 4");
              }

              if (CurKey.Trim() != "")
              {
                  TmpDay = Convert.ToInt16(CurKey.Substring(3, 2)) - 51;
                  TmpMonth = (Convert.ToInt16(CurKey.Substring(11, 2)) - 10) / 2;
                  TmpYear = Convert.ToInt16(CurKey.Substring(7, 4)) - 33;

                  switch (TmpMonth)
                  {
                      case 1:
                          DtStr = TmpDay.ToString() + " Jan " + TmpYear.ToString();
                          break;

                      case 2:
                          DtStr = TmpDay.ToString() + " Feb " + TmpYear.ToString();
                          break;

                      case 3:
                          DtStr = TmpDay.ToString() + " Mar " + TmpYear.ToString();
                          break;

                      case 4:
                          DtStr = TmpDay.ToString() + " Apr " + TmpYear.ToString();
                          break;

                      case 5:
                          DtStr = TmpDay.ToString() + " May " + TmpYear.ToString();
                          break;

                      case 6:
                          DtStr = TmpDay.ToString() + " Jun " + TmpYear.ToString();
                          break;

                      case 7:
                          DtStr = TmpDay.ToString() + " Jul " + TmpYear.ToString();
                          break;

                      case 8:
                          DtStr = TmpDay.ToString() + " Aug " + TmpYear.ToString();
                          break;

                      case 9:
                          DtStr = TmpDay.ToString() + " Sep " + TmpYear.ToString();
                          break;

                      case 10:
                          DtStr = TmpDay.ToString() + " Oct " + TmpYear.ToString();
                          break;

                      case 11:
                          DtStr = TmpDay.ToString() + " Nov " + TmpYear.ToString();
                          break;

                      case 12:
                          DtStr = TmpDay.ToString() + " Dec " + TmpYear.ToString();
                          break;

                      default:
                          DtStr = DateTime.Now.ToString ();
                          break;
                  }

                  return DtStr;
              }
              else //Last known date not found
              {
                  return DateTime.Now.ToString ();
              }
         }

        public void SetLastKnownDate(string DateIn)
        {
            int TmpYear;
            int TmpMonth;
            int TmpDay;
            int v1;
            int v3;
            int v4;
            int v5;
            int vx;
            string TmpKey;
            //DateTime KnownDate;

            TmpYear = Convert.ToInt32( Convert.ToDateTime(DateIn).ToString("y"));
            TmpMonth = Convert.ToInt32( Convert.ToDateTime(DateIn).ToString("m"));
            TmpDay = Convert.ToInt32( Convert.ToDateTime(DateIn).ToString("d"));

            Random random = new Random();

            v1 = random.Next(10, 99);
            v3 = random.Next(10,99);
            vx = TmpDay + 51;
            v4 = TmpYear + 33;
            v5 = (TmpMonth * 2) + 10;
            TmpKey = Convert.ToString(v1) + Convert.ToString(vx) + Convert.ToString(v3) + Convert.ToString(v4) + Convert.ToString(v5) ;
        }
    }
}

