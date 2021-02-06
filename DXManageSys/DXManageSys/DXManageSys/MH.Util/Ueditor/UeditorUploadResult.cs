namespace MH.Util.Ueditor
{
    /// <summary>
    /// 版 本 MH-ADMS V7.0.3 aosom platform api开发框架
    /// Copyright (c) 2013-2018 Aosom
    /// 创建人：Aosom-框架开发组
    /// 日 期：2017.03.07
    /// 描 述：百度编辑器UE上传返回结果
    /// </summary>
    public class UeditorUploadResult
    {
        /// <summary>
        /// 
        /// </summary>
        public UeditorUploadState State { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OriginFileName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public enum UeditorUploadState
    {
        /// <summary>
        /// 
        /// </summary>
        Success = 0,
        /// <summary>
        /// 
        /// </summary>
        SizeLimitExceed = -1,
        /// <summary>
        /// 
        /// </summary>
        TypeNotAllow = -2,
        /// <summary>
        /// 
        /// </summary>
        FileAccessError = -3,
        /// <summary>
        /// 
        /// </summary>
        NetworkError = -4,
        /// <summary>
        /// 
        /// </summary>
        Unknown = 1,
    }
}
