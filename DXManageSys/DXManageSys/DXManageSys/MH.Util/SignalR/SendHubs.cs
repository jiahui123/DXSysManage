using Microsoft.AspNet.SignalR.Client;
using System.Threading;

namespace MH.Util
{
    /// <summary>
    /// 版 本 MH-ADMS V7.0.3 aosom platform api开发框架
    /// Copyright (c) 2013-2018 Aosom
    /// 创建人：Aosom-框架开发组
    /// 日 期：2018.06.15
    /// 描 述：发送消息给SignalR集结器
    /// </summary>
    public static class SendHubs
    {
        /// <summary>
        /// 调用hub方法
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <param name="args">参数</param>
        public static void callMethod(string methodName, params object[] args)
        {
            if (Config.GetValue("IMOpen") == "true")
            {
                var hubConnection = new HubConnection(Config.GetValue("IMUrl"));
                IHubProxy ChatsHub = hubConnection.CreateHubProxy("ChatsHub");
                bool done = false;
                hubConnection.Start().ContinueWith(task =>
                {
                    //连接成功调用服务端方法
                    if (!task.IsFaulted)
                    {
                        ChatsHub.Invoke(methodName, args);
                        done = true;
                    }
                    else
                    {
                        done = true;
                    }
                });
                while (!done)
                {
                    Thread.Sleep(100);
                }
                //结束连接
                hubConnection.Stop();
            }
        }
    }
}
