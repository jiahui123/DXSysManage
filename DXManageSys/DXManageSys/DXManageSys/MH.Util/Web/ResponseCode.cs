namespace MH.Util
{
    /// <summary>
    /// 版 本 MH-ADMS V7.0.3 aosom platform api开发框架
    /// Copyright (c) 2013-2018 Aosom
    /// 创建人：Aosom-框架开发组
    /// 日 期：2017.03.08
    /// 描 述：接口响应码
    /// </summary>
    public enum ResponseCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        success = 200,

        /// <summary>
        /// 失败
        /// </summary>
        fail = 400,

        /// <summary>
        /// 异常
        /// </summary>
        exception = 500,

        /// <summary>
        /// 没有登录信息
        /// </summary>
        nologin = 410
    }
}
