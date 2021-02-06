using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using DXManageSys.Email;
using DXManageSys.YiFu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DXManageSys
{
    public partial class Main : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        Dictionary<string, XtraTabPage> dictXtraTabPage = new Dictionary<string, XtraTabPage>();
        Dictionary<string, Form> dictXtraForm = new Dictionary<string, Form>();
        public Main()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 打开Tab分页
        /// </summary>
        /// <param name="cText"></param>
        /// <param name="frm"></param>
        private void ShowMDIForm(string cText, Form frm)
        {
            if (string.IsNullOrEmpty(ConfigSettings.ReadSetting("Auth")))
            {
                XtraMessageBox.Show("程序未授权", "严重错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //判断是否已创建过
            if (dictXtraTabPage.ContainsKey(cText))
            {
                xtraTabControl1.SelectedTabPage = dictXtraTabPage[cText];
                return;
            }

            frm.Visible = false;
            frm.Dock = DockStyle.Fill;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.WindowState = FormWindowState.Maximized;
            frm.TopLevel = false;//注意这里，否则加载不出来


            XtraTabPage xpage = new XtraTabPage();
            xpage.Controls.Add(frm);//添加要增加的控件
            xpage.Text = cText;//添加名称
            xpage.ShowCloseButton = DevExpress.Utils.DefaultBoolean.True;
            xtraTabControl1.TabPages.Add(xpage);
            
            xtraTabControl1.SelectedTabPage = xpage;//显示该页

            dictXtraTabPage.Add(cText, xpage);//加入XtraTabPage字典
            dictXtraForm.Add(cText, frm);//加入XtraForm字典

            frm.Visible = true;
        }
        /// <summary>
        /// 关闭Tab分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            ClosePageButtonEventArgs a = (ClosePageButtonEventArgs)e;
            string cText = a.Page.Text;

            if (dictXtraForm.ContainsKey(cText))
            {
                Form form = dictXtraForm[cText] as Form;
                form.Close();
                form.Dispose();
                dictXtraForm.Remove(cText);
            }

            if (dictXtraTabPage.ContainsKey(cText))
            {
                xtraTabControl1.TabPages.Remove((XtraTabPage)a.Page);
                dictXtraTabPage.Remove(cText);
            }
        }

        #region 菜单
        /// <summary>
        /// 收件箱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmailReceiveMenuClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form er = new EmailReceive();

            ShowMDIForm("收件箱", er);

        }
        /// <summary>
        /// 邮件基础信息配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmailSettingClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form es = new EmailSettings();
            ShowMDIForm("邮箱基础设置", es);
        }
        /// <summary>
        /// PL资料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            YiFu_PL es = new YiFu_PL();
            ShowMDIForm("PL资料", es);
        }

        /// <summary>
        /// 议付资料基础设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            YiFuSettings es = new YiFuSettings();
            ShowMDIForm("议付资料基础设置", es);
        }

        /// <summary>
        /// 程序激活
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AuthClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form login = new Login();
            if (login.ShowDialog() == DialogResult.OK)
            {
                this.Close();
            }
        }


        #endregion
        /// <summary>
        /// 舱单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form er = new YiFu_CD();

            ShowMDIForm("舱单", er);
        }
    }
}
