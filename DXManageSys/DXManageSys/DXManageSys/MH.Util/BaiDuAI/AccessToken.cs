using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MH.Util.BaiDuAI
{
	public static class AccessToken

	{
		// 调用getAccessToken()获取的 access_token建议根据expires_in 时间 设置缓存
		// 返回token示例
		public static string TOKEN = "24.adda70c11b9786206253ddb70affdc46.2592000.1493524354.282335-1234567";

		// 百度云中开通对应服务应用的 API Key 建议开通应用的时候多选服务
		private static string clientId = "jDeLF117iteI7xz1RiZCSXMQ";
		// 百度云中开通对应服务应用的 Secret Key
		private static string clientSecret = "764ATOgV8FIMEETPUfhyK73rTB2VmhA8";

		public static string getAccessToken()
		{
			string authHost = "https://aip.baidubce.com/oauth/2.0/token";
			HttpClient client = new HttpClient();
			List<KeyValuePair<String, String>> paraList = new List<KeyValuePair<string, string>>();
			paraList.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
			paraList.Add(new KeyValuePair<string, string>("client_id", clientId));
			paraList.Add(new KeyValuePair<string, string>("client_secret", clientSecret));

			HttpResponseMessage response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;
			string result = response.Content.ReadAsStringAsync().Result;
			Console.WriteLine(result);

            Token t =JsonConvert.DeserializeObject<Token>(result);
			return t.access_token;
		}


	}

    public class Token
    {
        /// <summary>
        /// 
        /// </summary>
        public string refresh_token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int expires_in { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string session_key { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string scope { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string session_secret { get; set; }
    }
}
