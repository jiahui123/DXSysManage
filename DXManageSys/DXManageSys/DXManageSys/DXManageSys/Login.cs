using DataBase;
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

namespace DXManageSys
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {

        DBModel db = new DBModel();
        public Login()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if(CheckAuth(textEdit1.Text.Trim())){
                    XtraMessageBox.Show("保存成功,重启后生效！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }else
                {
                    XtraMessageBox.Show("无效密钥！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "严重错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckAuth(string auth)
        {
            //user u = new user();
            //u.id = Guid.NewGuid().ToString();
            //u.userId = "022005";
            //u.userPassword = "Aa123456";
            //u.auth = "MDIyMDA1OkFhMTIzNDU2";
            //u.creatDate = DateTime.UtcNow.AddHours(8);
            //u.creatUser = "system";
            //u.dataStaus = 1;
            //db.user.Add(u);
            //db.SaveChangesAsync();
            var user = db.user.FirstOrDefault(p => p.auth.Trim() == auth&&p.dataStaus==1);
            if (user == null)
            {
                return false;
            }
            else
            {
                ConfigSettings.WriteSetting("UserId", user.userId.Trim());
                ConfigSettings.WriteSetting("Auth", user.auth.Trim());
                return true;
            }
        }
    }
}