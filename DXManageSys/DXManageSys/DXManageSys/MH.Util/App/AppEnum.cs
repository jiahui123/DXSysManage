using System.ComponentModel;

namespace MH.Util
{
    /// <summary>
    /// 枚举
    /// </summary>
    public class AppEnum
    {
        #region ORG_ID
        //===========================================
        /// <summary>
        /// 国别
        /// </summary>
        public enum ORG_ID : int
        {
            /// <summary>
            /// 中国
            /// </summary>
            [Description("CN")]
            CN = 81,
            /// <summary>
            /// 美国
            /// </summary>
            [Description("US")]
            US = 83,
            /// <summary>
            /// 加拿大
            /// </summary>
            [Description("CA")]
            CA = 84,
            /// <summary>
            /// 英国
            /// </summary>
            [Description("UK")]
            UK = 86,
            /// <summary>
            /// 德国
            /// </summary>
            [Description("DE")]
            DE = 85,
            /// <summary>
            /// 法国
            /// </summary>
            [Description("FR")]
            FR = 87,
            /// <summary>
            /// 意大利
            /// </summary>
            [Description("IT")]
            IT = 88,
            /// <summary>
            /// 西班牙
            /// </summary>
            [Description("SP")]
            SP = 89,
            /// <summary>
            /// 
            /// </summary>
            [Description("HKI")]
            HKI = 221,
            /// <summary>
            /// 
            /// </summary>
            [Description("NWK")]
            NWK = 261,
            /// <summary>
            /// 
            /// </summary>
            [Description("PL")]
            PL = 1000,
            /// <summary>
            /// 
            /// </summary>
            [Description("IE")]
            IE = 1001,
            /// <summary>
            /// 葡萄牙
            /// </summary>
            [Description("PT")]
            PT = 1002,

        }
        #endregion

        #region Yes/No 0, 1
        //===========================================
        /// <summary>
        /// Yes/No 0, 1
        /// </summary>
        public enum YNStatus : int
        {
            /// <summary>
            /// 
            /// </summary>
            [Description("Yes")]
            Y = 1,
            /// <summary>
            /// 
            /// </summary>
            [Description("No")]
            N = 0

        }
        #endregion

    }
}
