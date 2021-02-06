using System;
using System.IO;
using System.Text.RegularExpressions;

namespace MH.Util.Ueditor
{
    /// <summary>
    /// 版 本 MH-ADMS V7.0.3 aosom platform api开发框架
    /// Copyright (c) 2013-2018 Aosom
    /// 创建人：Aosom-框架开发组
    /// 日 期：2017.03.07
    /// 描 述：百度编辑器UE上传文件路径格式化
    /// </summary>
    public static class UeditorPathFormatter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="originFileName"></param>
        /// <param name="pathFormat"></param>
        /// <returns></returns>
        public static string Format(string originFileName, string pathFormat)
        {
            if (String.IsNullOrWhiteSpace(pathFormat))
            {
                pathFormat = "{filename}{rand:6}";
            }

            var invalidPattern = new Regex(@"[\\\/\:\*\?\042\<\>\|]");
            originFileName = invalidPattern.Replace(originFileName, "");

            string extension = Path.GetExtension(originFileName);
            string filename = Path.GetFileNameWithoutExtension(originFileName);

            pathFormat = pathFormat.Replace("{filename}", filename);
            pathFormat = new Regex(@"\{rand(\:?)(\d+)\}", RegexOptions.Compiled).Replace(pathFormat, new MatchEvaluator(delegate (Match match)
            {
                var digit = 6;
                if (match.Groups.Count > 2)
                {
                    digit = Convert.ToInt32(match.Groups[2].Value);
                }
                var rand = new Random();
                return rand.Next((int)Math.Pow(10, digit), (int)Math.Pow(10, digit + 1)).ToString();
            }));

            pathFormat = pathFormat.Replace("{time}", Time.Now.Ticks.ToString());
            pathFormat = pathFormat.Replace("{yyyy}", Time.Now.Year.ToString());
            pathFormat = pathFormat.Replace("{yy}", (Time.Now.Year % 100).ToString("D2"));
            pathFormat = pathFormat.Replace("{mm}", Time.Now.Month.ToString("D2"));
            pathFormat = pathFormat.Replace("{dd}", Time.Now.Day.ToString("D2"));
            pathFormat = pathFormat.Replace("{hh}", Time.Now.Hour.ToString("D2"));
            pathFormat = pathFormat.Replace("{ii}", Time.Now.Minute.ToString("D2"));
            pathFormat = pathFormat.Replace("{ss}", Time.Now.Second.ToString("D2"));

            return pathFormat + extension;
        }
    }
}
