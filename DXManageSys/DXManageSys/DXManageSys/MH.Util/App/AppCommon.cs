using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MH.Util.App
{
    public class AppCommon
    {
        public static AppCommon _instance;
        public static AppCommon GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AppCommon();
            }
            return _instance;
        }
        #region 数据格式的判断

        public static bool IsDecimal(string str)
        {
            if (str == String.Empty)
                return false;
            try
            {
                decimal tmp = decimal.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsInt(string str)
        {
            if (str == String.Empty)
                return false;
            try
            {
                int tmp = int.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }




        public static bool IsDateTime(string str)
        {
            if (str == String.Empty)
                return false;
            try
            {
                DateTime tmp = DateTime.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public static bool IsDateTimeExact(string str)
        {
            if (str == String.Empty)
                return false;
            try
            {
                DateTime tmp = DateTime.ParseExact(str, @"yyyyMMdd", null, DateTimeStyles.None);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 地址信息 解析
        /// <summary>
        ///从地址中解析省简称,城市，街道
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public bool AnalyseSOAddress(string Address, out string Street, out string City, out string Province, out string Country)
        {
            Street = "";
            City = "";
            Province = "";
            Country = "";
            string _address = Address;

            string[] strs = _address.Split(',');
            if (strs.Length > 0 && strs.Length < 4)
            {
                return false;
            }

            Street = strs[0].Trim();
            City = strs[1].Trim();
            Province = strs[2].Trim();
            Country = strs[3].Trim();

            return true;
        }
        #endregion
    }
}
