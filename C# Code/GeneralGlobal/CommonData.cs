using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneralGlobal
{
    public class CommonData
    {
        private static CommonData instance;

        public CommonData()
        {
        }

        public static CommonData Instance
        {
           get 
           {
              if (instance == null)
              {
                  instance = new CommonData();
              }
              return instance;
           }
        }
    }
}
