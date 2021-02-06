﻿using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Web;

namespace MH.Util.Ueditor
{
    /// <summary>
    /// 版 本 MH-ADMS V7.0.3 aosom platform api开发框架
    /// Copyright (c) 2013-2018 Aosom
    /// 创建人：Aosom-框架开发组
    /// 日 期：2017.03.07
    /// 描 述：百度编辑器UE配置文件操作文件操作
    /// </summary>
    public static class UeditorConfig
    {
        private static bool noCache = true;
        private static JObject BuildItems()
        {
            var json = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Content/ueditor/config/config.json"));
            return JObject.Parse(json);
        }
        /// <summary>
        /// 
        /// </summary>
        public static JObject Items
        {
            get
            {
                if (noCache || _Items == null)
                {
                    _Items = BuildItems();
                }
                return _Items;
            }
        }
        private static JObject _Items;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetValue<T>(string key)
        {
            return Items[key].Value<T>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static String[] GetStringList(string key)
        {
            return Items[key].Select(x => x.Value<String>()).ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static String GetString(string key)
        {
            return GetValue<String>(key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetInt(string key)
        {
            return GetValue<int>(key);
        }
    }
}
