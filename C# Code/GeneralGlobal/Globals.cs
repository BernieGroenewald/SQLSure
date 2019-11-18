using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneralGlobal
{
    public static class Globals
    {
        static string _UserName = "";
        public static string _ConnectionString = "";
        static string _ServerPath = "";
        static string _FocusColour = "";
        static string _ToolTipTextStockMaintenance = "";

        public static string UserName 
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
            }
        }

        public static string ConnectionString
        {
            get
            {
                return _ConnectionString;
            }
            set
            {
                _ConnectionString = value;
            }
        }

        public static string ServerPath
        {
            get
            {
                return _ServerPath;
            }
            set
            {
                _ServerPath = value;
            }
        }

        public static string FocusColour
        {
            get
            {
                return _FocusColour;
            }
            set
            {
                _FocusColour = value;
            }
        }

        public static string ToolTipTextStockMaintenance
        {
            get
            {
                return _ToolTipTextStockMaintenance;
            }
            set
            {
                _ToolTipTextStockMaintenance = value;
            }
        }
    }
}
