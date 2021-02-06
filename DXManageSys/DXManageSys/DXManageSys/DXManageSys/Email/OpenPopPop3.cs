using OpenPop.Mime;
using OpenPop.Mime.Header;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXManageSys.Email
{
    public class OpenPopPop3 : Pop3
    {

        #region 窗体变量

        /// <summary>
        /// 是否存在错误
        /// </summary>
        public override Boolean ExitsError { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public override String ErrorMessage { get; set; }
        /// <summary>
        /// POP3端口号
        /// </summary>
        public override Int32 Pop3Port { set; get; }
        /// <summary>
        /// POP3地址
        /// </summary>
        public override String Pop3Address { set; get; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public override String EmailAddress { set; get; }
        /// <summary>
        /// 邮箱密码
        /// </summary>
        public override String EmailPassword { set; get; }

        #endregion

        #region 私有变量
        private Pop3Client pop3Client;

        // private List<POP3_ClientMessage> pop3MessageList = new List<POP3_ClientMessage>();

        private Int32 mailTotalCount;
        #endregion

        #region 构造函数
        public OpenPopPop3() { }
        #endregion

        #region 链接至服务器并读取邮件集合
        /// <summary>
        /// 链接至服务器并读取邮件集合
        /// </summary>
        public override Boolean Authenticate()
        {
            try
            {
                pop3Client = new Pop3Client();
                if (pop3Client.Connected)
                    pop3Client.Disconnect();
                pop3Client.Connect(Pop3Address, Pop3Port, false);
                pop3Client.Authenticate(EmailAddress, EmailPassword, AuthenticationMethod.UsernameAndPassword);
                mailTotalCount = pop3Client.GetMessageCount();

                return ExitsError = true;
            }
            catch (Exception ex) { ErrorMessage = ex.Message; return ExitsError = false; }
        }
        #endregion

        #region 获取邮件数量
        /// <summary>
        /// 获取邮件数量
        /// </summary>
        /// <returns></returns>
        public override Int32 GetMailCount()
        {
            return mailTotalCount;
        }
        #endregion

        #region 获取发件人
        /// <summary>
        /// 获取发件人
        /// </summary>
        /// <param name="mailIndex"></param>
        /// <returns></returns>
        public override String GetSendMialAddress(Int32 mailIndex)
        {
            RfcMailAddress address = pop3Client.GetMessageHeaders(mailIndex).From;
            return address.Address;
        }
        #endregion

        #region 获取邮件的主题
        /// <summary>
        /// 获取邮件的主题
        /// </summary>
        /// <param name="mailIndex"></param>
        /// <returns></returns>
        public override String GetMailUID(Int32 mailIndex)
        {
            return pop3Client.GetMessageUid(mailIndex);

        }
        #endregion

        #region 获取邮件的UID
        /// <summary>
        /// 获取邮件的UID
        /// </summary>
        /// <param name="mailIndex"></param>
        /// <returns></returns>
        public override String GetMailSubject(Int32 mailIndex)
        {
            return pop3Client.GetMessageHeaders(mailIndex).Subject;
        }
        #endregion

        #region 获取邮件正文
        /// <summary>
        /// 获取邮件正文
        /// </summary>
        /// <param name="mailIndex">邮件顺序</param>
        /// <returns></returns>
        public override String GetMailBodyAsText(Int32 mailIndex)
        {
            string body = "";
            Message message = pop3Client.GetMessage(mailIndex);
            MessagePart selectedMessagePart = message.MessagePart;
            if (selectedMessagePart.IsText)
            {
                body = selectedMessagePart.GetBodyAsText();
            }
            else if (selectedMessagePart.IsMultiPart)
            {
                MessagePart plainTextPart = message.FindFirstPlainTextVersion();
                MessagePart htmlTextPart = message.FindFirstHtmlVersion();
                if (htmlTextPart != null)
                {

                    body = htmlTextPart.GetBodyAsText();
                }

                if (plainTextPart != null)
                {

                    body = plainTextPart.GetBodyAsText();
                }
                else
                {
                    List<MessagePart> textVersions = message.FindAllTextVersions();
                    if (textVersions.Count >= 1)
                        body = textVersions[0].GetBodyAsText();
                    else
                        body = "<<OpenPop>> Cannot find a text version body in this message.";
                }
            }
            return body;
        }
        #endregion

        #region 获取邮件的附件
        public override string GetMailAttachment(Int32 mailIndex, String receiveBackpath)
        {
            string attachmentsName = "";
            if (mailIndex == 0)
                return "";
            else if (mailIndex > mailTotalCount)
                return "";
            else
            {
                try
                {
                    Message message = pop3Client.GetMessage(mailIndex);
                    //邮件的全部附件.
                    List<MessagePart> attachments = message.FindAllAttachments();
                    foreach (MessagePart attachment in attachments)
                    {
                        string fileName = attachment.FileName;
                        attachmentsName += fileName + ";";
                        string fileFullName = receiveBackpath + "\\" + fileName;
                        FileInfo fileInfo = new FileInfo(fileFullName);
                        if (fileInfo.Exists) fileInfo.Delete();
                        attachment.Save(fileInfo);
                    }
                    pop3Client.DeleteMessage(mailIndex);
                    return attachmentsName;
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    return "";
                }
            }
            return attachmentsName;
        }
        #endregion

        #region 删除邮件
        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="mailIndex"></param>
        public override void DeleteMail(Int32 mailIndex)
        {
            pop3Client.DeleteMessage(mailIndex);
        }
        #endregion

        #region 关闭邮件服务器
        public override void Pop3Close()
        {
            pop3Client.Disconnect();
            pop3Client.Dispose();
        }
        #endregion





    }
}
