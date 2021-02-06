namespace MH.Util
{
    /// <summary>
    /// 版 本 MH-ADMS V7.0.3 aosom platform api开发框架
    /// Copyright (c) 2013-2018 Aosom
    /// 创建人：Aosom-框架开发组
    /// 日 期：2017.03.08
    /// 描 述：接口响应数据
    /// </summary>
    public class ResParameter
    {
        /// <summary>
        /// 接口响应码
        /// </summary>
        public ResponseCode code { get; set; }
        /// <summary>
        /// 接口响应消息
        /// </summary>
        public string info { get; set; }
        /// <summary>
        /// 接口响应数据
        /// </summary>
        public object data { get; set; }
    }
}
