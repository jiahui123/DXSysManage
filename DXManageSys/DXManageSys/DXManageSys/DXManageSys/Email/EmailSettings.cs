using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DXManageSys.Email
{
    public partial class EmailSettings : DevExpress.XtraEditors.XtraForm
    {
        public EmailSettings()
        {
            InitializeComponent();
            DataInit();
        }

        private void DataInit()
        {
            Account163.Text = ConfigSettings.ReadSetting("163Account");
            password163.Text= ConfigSettings.ReadSetting("163Password");
            path163.Text = ConfigSettings.ReadSetting("163Path");
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                ConfigSettings.WriteSetting("163Account", Account163.Text.Trim());
                ConfigSettings.WriteSetting("163Password", password163.Text.Trim());
                ConfigSettings.WriteSetting("163Path", path163.Text.Trim());
                XtraMessageBox.Show("保存成功,重启后生效！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "严重错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 163文件夹路径选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Path163Click(object sender, EventArgs e)
        {
            XtraFolderBrowserDialog xtraFolder = new XtraFolderBrowserDialog();
            if (xtraFolder.ShowDialog() == DialogResult.OK)
            {
                path163.Text = xtraFolder.SelectedPath;
            }
        }
    }
}