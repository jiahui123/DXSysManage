using DataBase;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DXManageSys.Email
{
    public partial class EmailReceive : DevExpress.XtraEditors.XtraForm
    {
        DBModel db = new DBModel();

        private Thread Thread_MailDownload; // 邮件下载



        public EmailReceive()
        {
            InitializeComponent();
        }

        private void Load(object sender, EventArgs e)
        {
            timer1.Stop();
        }
        private void RefreshBtnClick(object sender, ItemClickEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            splashScreenManager1.SetWaitFormCaption("提示");
            splashScreenManager1.SetWaitFormDescription("数据加载中");
            gridControl1.DataSource = db.mail.Where(p => p.dataStaus == 1).OrderByDescending(p => p.creatDate).AsQueryable().ToList();
            gridControl1.RefreshDataSource();
            splashScreenManager1.CloseWaitForm();
        }

        private void Service_MailDownloadStart()
        {
            if (Thread_MailDownload == null || Thread_MailDownload.ThreadState == System.Threading.ThreadState.Aborted || !Thread_MailDownload.IsAlive)
            {
                Thread_MailDownload = new Thread(new ThreadStart(Service_MailDownloadFunc));
                Thread_MailDownload.Priority = ThreadPriority.AboveNormal;
                Thread_MailDownload.Start();
            }
        }
        private void Service_MailDownloadFunc()
        {

            try
            {
                UpdateProcess(0,"获取163邮箱配置");
                string Account = ConfigSettings.ReadSetting("163Account");
                string Password = ConfigSettings.ReadSetting("163Password");
                string Path = ConfigSettings.ReadSetting("163Path");

                if (string.IsNullOrEmpty(Account) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Path))
                {
                    UpdateProcess(5, "错误：请先维护邮箱资料！");
                    return;
                }

               
                UpdateProcess(10, "正在初始化邮箱");
                FactoryPop3 factory = new FactoryPop3();
                Pop3 pop = factory.CreatePop3();
                pop.Pop3Port = 110;
                pop.Pop3Address = "pop3.163.com";
                pop.EmailAddress = Account;//gudongxiao1@163.com
                pop.EmailPassword = Password;//FAURNNDDGCPZKCLO
                pop.Authenticate();
                UpdateProcess(20, "邮箱连接成功");

                int mailcount = pop.GetMailCount();

                var lastEmail = db.mail.Where(p => p.dataStaus == 1).OrderByDescending(p => p.creatDate).FirstOrDefault();
                bool startRead = false;
                if (lastEmail != null)
                {
                    if (lastEmail.creatDate < DateTime.UtcNow.AddHours(8).AddDays(-25))
                    {
                        startRead = true;
                    }
                }
                else
                {
                    startRead = true;
                }
                for (int i = 1; i <= mailcount; i++)
                {
                    int pro = int.Parse(Math.Round(decimal.Parse(i.ToString()) / mailcount * 1000, 0).ToString());
                    UpdateProcess(pro, "读取第" + i + "封邮件,总计" + mailcount + "封");
                    string MailUID = pop.GetMailUID(i);

                    if (!startRead)
                    {
                        if (MailUID == lastEmail.mailUID.Trim())//从上次读取的终点开始读取
                        {
                            startRead = true;
                            continue;
                        }
                        continue;
                    }

                    var oldmail = db.mail.FirstOrDefault(p => p.mailUID.Trim() == MailUID.Trim() && p.dataStaus == 1);
                    string mailSubject = pop.GetMailSubject(i);
                    if (oldmail == null)
                    {
                        string attachment = "";
                        if (mailSubject.Contains("议付资料"))
                        {
                            //string dic = mailSubject.Split('-')[1];
                            //if (Directory.Exists(Path + "/" + dic) == false)//如果不存在就创建file文件夹
                            //{
                            //    Directory.CreateDirectory(Path + "/" + dic);
                            //}
                            UpdateProcess(pro, "下载附件中");
                            attachment = pop.GetMailAttachment(i, Path + "/");
                        }

                        mail m = new mail();
                        m.id = Guid.NewGuid().ToString();
                        m.mailUID = MailUID;
                        m.sendAddress = pop.GetSendMialAddress(i);
                        m.subject = mailSubject;
                        m.MailBody = pop.GetMailBodyAsText(i);
                        m.Attachment = attachment;
                        m.creatDate = DateTime.UtcNow.AddHours(8);
                        m.creatUser = ConfigSettings.ReadSetting("UserId");
                        m.dataStaus = 1;
                        db.mail.Add(m);
                        db.SaveChanges();
                    }
                }
                UpdateProcess(1000, "邮箱同步成功");
            }
            catch (Exception ex)
            {
                UpdateProcess(20, "错误:线程终止");
                if (Thread_MailDownload != null)
                {
                    Thread_MailDownload.Abort();
                    Thread_MailDownload = null;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Service_MailDownloadStart();
        }

        private void CheckChanged(object sender, ItemClickEventArgs e)
        {
            if (this.barCheckItem1.Checked)
            {
                timer1.Interval = int.Parse(barEditItem2.EditValue.ToString()) * 60 * 1000;
                Service_MailDownloadStart();
                timer1.Start();
            }
            else
            {
                timer1.Stop();
                if (Thread_MailDownload != null)
                {
                    Thread_MailDownload.Abort();
                    Thread_MailDownload = null;
                }
            }
        }

        
        private void UpdateProcess(int process,string Message)
        {
            Action act = delegate ()
            {
                this.barEditItem1.EditValue = process;
                this.barStaticItem1.Caption = Message;
            };
            this.Invoke(act);
        }
    }
}