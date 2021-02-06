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

namespace DXManageSys.YiFu
{
    public partial class YiFuSettings : DevExpress.XtraEditors.XtraForm
    {
        public YiFuSettings()
        {
            InitializeComponent();
            DataInit();
        }

        private void DataInit()
        {
            //textEdit1.Text = ConfigSettings.ReadSetting("PL_SM_Path");
            //textEdit2.Text = ConfigSettings.ReadSetting("PL_QY_Path");
            //textEdit3.Text = ConfigSettings.ReadSetting("PL_MB_Path");



            textEdit1.Text = YiFuSetting.PL_SM_Path;
            textEdit2.Text = YiFuSetting.PL_QY_Path;
            textEdit3.Text = YiFuSetting.PL_MB_Path; ;
        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                ConfigSettings.WriteSetting("PL_SM_Path", textEdit1.Text.Trim());
                YiFuSetting.PL_SM_Path = textEdit1.Text.Trim();
                ConfigSettings.WriteSetting("PL_QY_Path", textEdit2.Text.Trim());
                YiFuSetting.PL_QY_Path = textEdit2.Text.Trim();
                ConfigSettings.WriteSetting("PL_MB_Path", textEdit3.Text.Trim());
                YiFuSetting.PL_MB_Path = textEdit3.Text.Trim();

                XtraMessageBox.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "严重错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 扫描路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plclick1(object sender, EventArgs e)
        {
            XtraFolderBrowserDialog xtraFolder = new XtraFolderBrowserDialog();
            if (xtraFolder.ShowDialog() == DialogResult.OK)
            {
                textEdit1.Text = xtraFolder.SelectedPath;
            }
        }
        /// <summary>
        /// 迁移路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plclick2(object sender, EventArgs e)
        {
            XtraFolderBrowserDialog xtraFolder = new XtraFolderBrowserDialog();
            if (xtraFolder.ShowDialog() == DialogResult.OK)
            {
                textEdit2.Text = xtraFolder.SelectedPath;
            }
        }
        /// <summary>
        /// PL PDF导出模板路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plclick3(object sender, EventArgs e)
        {
            XtraFolderBrowserDialog xtraFolder = new XtraFolderBrowserDialog();
            if (xtraFolder.ShowDialog() == DialogResult.OK)
            {
                textEdit3.Text = xtraFolder.SelectedPath;
            }
        }
    }
}