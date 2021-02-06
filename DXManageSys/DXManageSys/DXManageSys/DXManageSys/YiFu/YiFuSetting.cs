using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXManageSys.YiFu
{
    public  class YiFuSetting
    {
        public static string PL_SM_Path = ConfigSettings.ReadSetting("PL_SM_Path");

        public static string PL_QY_Path = ConfigSettings.ReadSetting("PL_QY_Path");

        public static string PL_MB_Path = ConfigSettings.ReadSetting("PL_MB_Path");


    }
}
