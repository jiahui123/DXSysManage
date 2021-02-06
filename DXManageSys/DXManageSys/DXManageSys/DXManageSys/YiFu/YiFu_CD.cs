using DataBase;
using DataBase.Modules;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using MH.Util;
using MH.Util.App;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.XtraExport.Helpers.TableRowControl;

namespace DXManageSys.YiFu
{
    public partial class YiFu_CD : DevExpress.XtraEditors.XtraForm
    {
        List<pl_cd> list = new List<pl_cd>();
        DBModel db = new DBModel();
        public YiFu_CD()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(YiFuSetting.PL_MB_Path))
                {
                    XtraMessageBox.Show("请先设置扫描路径", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                list.Clear();
                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormCaption("提示");
                splashScreenManager1.SetWaitFormDescription("扫描中");

                DataRow[] rows = DirFileHelper.GetFilesByTime(YiFuSetting.PL_MB_Path, ".xls");
                foreach (var row in rows)
                {
                    if (row["filename"].ToString().Contains("舱单"))
                    {
                        string path1 = row["filename"].ToString().Trim();

                        string filename = Path.GetFileNameWithoutExtension(path1).Replace("舱单","").Trim();

                        string bgNumbers = "";

                        string tip = filename.Substring(0, 5);

                        string[] strs = filename.Split(' ');

                        foreach(var str in strs)
                        {
                            if (str.Trim().StartsWith(tip))
                            {
                                bgNumbers += str.Trim() + ";";
                            }
                            else
                            {
                                bgNumbers += tip + str.Trim() + ";";
                            }
                        }


                        db.pl_cd.RemoveRange(db.pl_cd.Where(p => p.bgNumber.Trim() == bgNumbers.Trim()));


                        //创建Workbook对象并加载Excel文档
                        Workbook workbook = new Workbook();
                        workbook.LoadFromFile(path1);

                        //获取第一张sheet
                        Worksheet sheet = workbook.Worksheets[0];

                        //设置range范围
                        Spire.Xls.CellRange range = sheet.Range[sheet.FirstRow, sheet.FirstColumn, sheet.LastRow, sheet.LastColumn];

                        //输出数据, 同时输出列名以及公式值
                        DataTable dt = sheet.ExportDataTable(range, true, true);


                        if (dt != null && dt.Rows.Count > 0)
                        {
                           for(int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (dt.Rows[i][0].ToString().Trim() == "按箱统计数据")
                                {
                                    i++;
                                    i++;
                                    while (dt.Rows[i][0].ToString().Trim() != "总票统计数据")
                                    {
                                        if (!string.IsNullOrEmpty(dt.Rows[i][0].ToString()))
                                        {
                                          
                                                var entity = new pl_cd();
                                                entity.id = Guid.NewGuid().ToString();
                                                entity.bgNumber = bgNumbers;
                                                entity.fileName = filename;
                                                if (AppCommon.IsDecimal(dt.Rows[i][7].ToString()))
                                                {
                                                    entity.jianshu = decimal.Parse(dt.Rows[i][7].ToString());
                                                }
                                                if (AppCommon.IsDecimal(dt.Rows[i][8].ToString()))
                                                {
                                                    entity.maozhong = decimal.Parse(dt.Rows[i][8].ToString());
                                                }
                                                if (AppCommon.IsDecimal(dt.Rows[i][9].ToString()))
                                                {
                                                    entity.tiji = decimal.Parse(dt.Rows[i][9].ToString());
                                                }
                                                db.pl_cd.Add(entity);
                                           
                                            list.Add(entity);

                                        }
                                        i++;
                                    }
                                }
                            }
                        }

                    }
                }

                db.SaveChanges();
                gridControl1.DataSource = list;
                gridControl1.RefreshDataSource() ;
                splashScreenManager1.CloseWaitForm();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "严重错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                splashScreenManager1.CloseWaitForm();
            }
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormCaption("提示");
                splashScreenManager1.SetWaitFormDescription("查询中");

                gridControl1.DataSource = db.pl_cd.AsQueryable().ToList();
                gridControl1.RefreshDataSource();
                splashScreenManager1.CloseWaitForm();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "严重错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                splashScreenManager1.CloseWaitForm();
            }
        }
    }
}