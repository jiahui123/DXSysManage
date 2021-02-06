using DataBase;
using DataBase.Modules;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using HtmlAgilityPack;
using MH.Util;
using MH.Util.App;
using MH.Util.BaiDuAI;
using Spire.Pdf;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace DXManageSys.YiFu
{
    public partial class YiFu_PL : DevExpress.XtraEditors.XtraForm
    {
        public List<pl_head> Pl_Heads = new List<pl_head>();
        DBModel db = new DBModel();
        public YiFu_PL()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 扫描
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(YiFuSetting.PL_SM_Path))
                {
                    XtraMessageBox.Show("请先设置扫描路径", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Pl_Heads.Clear();
                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormCaption("提示");
                splashScreenManager1.SetWaitFormDescription("扫描中");

                DataRow[] rows = DirFileHelper.GetFilesByTime(YiFuSetting.PL_SM_Path, ".xls");
                foreach (var row in rows)
                {
                    if (row["filename"].ToString().Contains("PL"))
                    {
                        string path1 = row["filename"].ToString().Trim();
                        string path2 = Path.ChangeExtension(path1, "txt");
                        if (File.Exists(path2))
                        {
                            File.Delete(path2);
                        }

                        System.IO.File.Copy(path1, path2);

                        string [] strs =File.ReadAllLines(path2);
                        pl_head head = new pl_head();
                        head.matched = "";

                        string html = "";
                        for (int i=0;i<strs.Length;i++)
                        {
                            if (strs[i].Contains("INVOICE NO"))
                            {
                                string r = RemoveHTMLTags(strs[i + 2]);
                                if (!string.IsNullOrEmpty(r))
                                {
                                    head.invoice = r;
                                }
                            }
                            if (strs[i].Contains("TOTAL:"))
                            {

                                while (!strs[i].Contains("PACKED"))
                                {
                                    html += strs[i];
                                    i++;
                                }
                                string[] rss = RemoveHTMLTags(html).Split(' ');

                                if (rss.Length == 7)
                                {
                                    if (AppCommon.IsDecimal(rss[1].Trim()))
                                    {
                                        head.total_qty = decimal.Parse(rss[1].Trim());
                                    }
                                    if (AppCommon.IsDecimal(rss[3].Trim()))
                                    {
                                        head.total_cin = decimal.Parse(rss[3].Trim());
                                    }
                                    if (AppCommon.IsDecimal(rss[4].Trim()))
                                    {
                                        head.total_nkgs = decimal.Parse(rss[4].Trim());
                                    }
                                    if (AppCommon.IsDecimal(rss[5].Trim()))
                                    {
                                        head.total_gkgs = decimal.Parse(rss[5].Trim());
                                    }
                                    if (AppCommon.IsDecimal(rss[6].Trim()))
                                    {
                                        head.total_cbm = decimal.Parse(rss[6].Trim());
                                    }
                                 
                                    head.path = path1;
                                }
                                else//测试使用
                                {

                                }
                            }
                        }
                        if (string.IsNullOrEmpty(head.invoice))
                        {

                        }
                        Pl_Heads.Add(head);
                        File.Delete(path2);
                    }
                }

                rows = DirFileHelper.GetFilesByTime(YiFuSetting.PL_SM_Path, ".xls");
                foreach (var row in rows)
                {
                    if (row["filename"].ToString().Contains("INV"))
                    {
                        string path1 = row["filename"].ToString().Trim();
                        string path2 = Path.ChangeExtension(path1, "txt");
                        if (File.Exists(path2))
                        {
                            File.Delete(path2);
                        }

                        System.IO.File.Copy(path1, path2);

                        string[] strs = File.ReadAllLines(path2);

                        pl_head head=null;

                        for (int i = 0; i < strs.Length; i++)
                        {
                            if (strs[i].Contains("INVOICE NO"))
                            {
                                string r = RemoveHTMLTags(strs[i + 2]);
                                if (!string.IsNullOrEmpty(r))
                                {
                                    head = Pl_Heads.FirstOrDefault(p => p.invoice.Trim() == r.Trim());
                                }
                            }
                            if(strs[i].Contains("FROM")&& strs[i].Contains("TO") && strs[i].Contains("BY"))
                            {
                                string r = RemoveHTMLTags(strs[i]);
                                if (head != null)
                                {
                                    string strstart = "TO";
                                    int strlength = strstart.Length;
                                    string str = r.Substring(r.IndexOf("TO") + strlength, r.IndexOf("BY") - r.IndexOf("TO") - strlength).Replace("United","").Replace("States", "").Trim();
                                    head.port = str;
                                }
                            }
                        }

                        File.Delete(path2);
                    }
                }


                gridControl1.DataSource = Pl_Heads;
                gridControl1.RefreshDataSource();
                splashScreenManager1.CloseWaitForm();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "严重错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                splashScreenManager1.CloseWaitForm();
            }
        }
        public string RemoveHTMLTags(string htmlStream)
        {
            if (htmlStream == null)
            {
                throw new Exception("Your input html stream is null!");
                return null;
            }
            /*
             * 最好把所有的特殊HTML标记都找出来，然后把与其相对应的Unicode字符一起影射到Hash表内，最后一起都替换掉
             */
            //先单独测试,成功后,再把所有模式合并
            //注:这两个必须单独处理
            //去掉嵌套了HTML标记的JavaScript:(<script)[\\s\\S]*(</script>)
            //去掉css标记:(<style)[\\s\\S]*(</style>)
            //去掉css标记:\\..*\\{[\\s\\S]*\\}
            htmlStream = Regex.Replace(htmlStream, "(<script)[\\s\\S]*?(</script>)|(<style)[\\s\\S]*?(</style>)", " ", RegexOptions.IgnoreCase);
            //htmlStream = RemoveTag(htmlStream, "script");
            //htmlStream = RemoveTag(htmlStream, "style");
            //去掉普通HTML标记:<[^>]+>
            //替换空格: |&|­| |­
            htmlStream = Regex.Replace(htmlStream, "<[^>]+>| |&|­| |­|•|<|>", " ", RegexOptions.IgnoreCase);
            //htmlStream = RemoveTag(htmlStream);
            //替换左尖括号
            //htmlStream = Regex.Replace(htmlStream, "<", "<");
            //替换右尖括号
            //htmlStream = Regex.Replace(htmlStream, ">", ">");
            //替换空行
            //htmlStream = Regex.Replace(htmlStream, "[\n|\r|\t]", " ");//[\n|\r][\t*| *]*[\n|\r]
            htmlStream = Regex.Replace(htmlStream, "(\r\n[\r|\n|\t| ]*\r\n)|(\n[\r|\n|\t| ]*\n)", "\r\n");
            htmlStream = Regex.Replace(htmlStream, "[\t| ]{1,}", " ");
            return htmlStream.Trim();
        }
        /// <summary>
        /// 截取字符串中开始和结束字符串中间的字符串
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="startStr">开始字符串</param>
        /// <param name="endStr">结束字符串</param>
        /// <returns>中间字符串</returns>
        public string SubstringSingle(string source, string startStr, string endStr)
        {
            Regex rg = new Regex("(?<=(" + startStr + "))[.\\s\\S]*?(?=(" + endStr + "))", RegexOptions.Multiline | RegexOptions.Singleline);
            return rg.Match(source).Value;
        }

        /// <summary>
        /// PDF转换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(YiFuSetting.PL_SM_Path))
                {
                    XtraMessageBox.Show("请先设置扫描路径", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormCaption("提示");
                splashScreenManager1.SetWaitFormDescription("扫描中");
                DataRow[] rows = DirFileHelper.GetFilesByTime(YiFuSetting.PL_SM_Path, ".xls");
                foreach (var row in rows)
                {
                    if (row["filename"].ToString().Contains("PL")|| row["filename"].ToString().Contains("INV"))
                    {
                        try
                        {
                            string path1 = row["filename"].ToString().Trim();
                            string path3 = Path.ChangeExtension(path1, "pdf");

                            // load excel file
                            Workbook wb = new Workbook();
                            wb.LoadFromFile(path1);

                            // wb.SaveToFile("Output.xlsx", ExcelVersion.Version2013);
                            //wb.ConverterSetting.SheetFitToPage = true;
                            // convert excel file to pdf file
                            //获取第一个工作表
                            Worksheet sheet = wb.Worksheets[0];
                            sheet.PageSetup.PaperSize = PaperSizeType.PaperA4;
                            //sheet.PageSetup.Zoom = 0;
                            sheet.AllocatedRange.AutoFitColumns();
                            sheet.AllocatedRange.AutoFitRows();
                            sheet.PageSetup.FitToPagesTall = 1;
                            sheet.PageSetup.FitToPagesWide = 1;


                            //设置range范围
                            Spire.Xls.CellRange range = sheet.Range[sheet.FirstRow, sheet.FirstColumn, sheet.LastRow, sheet.LastColumn];

                            //输出数据, 同时输出列名以及公式值
                            DataTable dt = sheet.ExportDataTable(range, true, true);//Aosom E-Commerce Inc，

                            if(dt.Rows[0][0].ToString().Contains("Aosom E-Commerce Inc"))
                            {
                                string imgsrc = System.Windows.Forms.Application.StartupPath + "//Image//E-Commerce.png";

                                //加载图片，添加到指定单元格

                                ExcelPicture picture = sheet.Pictures.Add(sheet.LastRow+1, 1, imgsrc);

                                //指定图片宽度和高度

                                picture.Width = 300;

                                picture.Height = 180;
                            }
                            else if (dt.Rows[0][0].ToString().Contains("AOSOM INTERNATIONAL"))
                            {
                                string imgsrc = System.Windows.Forms.Application.StartupPath + "//Image//AOSOM INTERNATIONAL.png";

                                //加载图片，添加到指定单元格

                                ExcelPicture picture = sheet.Pictures.Add(sheet.LastRow + 1, 1, imgsrc);

                                //指定图片宽度和高度

                                picture.Width = 300;

                                picture.Height = 180;
                            }
                            else if (dt.Rows[0][0].ToString().Contains("Ningbo Aosom Internet"))
                            {
                                string imgsrc = System.Windows.Forms.Application.StartupPath + "//Image//Ningbo Aosom Internet.png";

                                //加载图片，添加到指定单元格

                                ExcelPicture picture = sheet.Pictures.Add(sheet.LastRow + 1, 1, imgsrc);

                                //指定图片宽度和高度

                                picture.Width = 300;

                                picture.Height = 180;
                            }
                            //sheet.Range["A1:D50"].Style.WrapText = true;
                            //sheet.SetColumnWidth(1, 20);
                            //sheet.SetColumnWidth(2, 20);
                            //sheet.SetColumnWidth(3, 10);
                            //sheet.SetColumnWidth(4, 10);
                            sheet.SaveToPdf(path3, Spire.Xls.FileFormat.PDF);
                        }catch(Exception exx)
                        {
                            continue;
                        }

                    }
                }
                splashScreenManager1.CloseWaitForm();
                XtraMessageBox.Show("转换成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "严重错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                splashScreenManager1.CloseWaitForm();
            }
        }

        /// <summary>
        /// 比对
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormCaption("提示");
                splashScreenManager1.SetWaitFormDescription("比对中");

                foreach(var head in Pl_Heads)
                {
                    var r = db.pl_cd.Where(p => p.bgNumber.Contains(head.invoice) && p.jianshu == head.total_cin && p.maozhong == head.total_gkgs && p.tiji == head.total_cbm);
                    if (r != null && r.Count() == 1)
                    {
                        head.matched = "Y";
                    }
                }

                gridControl1.DataSource = Pl_Heads;
                gridControl1.RefreshDataSource();
                splashScreenManager1.CloseWaitForm();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "严重错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                splashScreenManager1.CloseWaitForm();
            }
        }

        /// <summary>
        /// 迁移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(YiFuSetting.PL_QY_Path))
                {
                    XtraMessageBox.Show("请先设置迁移路径", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormCaption("提示");
                splashScreenManager1.SetWaitFormDescription("迁移中");

                var successList = Pl_Heads.Where(p => p.matched == "Y").ToList();
                foreach (var head in successList)
                {
                    string DirectoryName = Path.GetDirectoryName(head.path);
                    string FileName = Path.GetFileName(head.path).Split('-')[0].Trim();

                    var cd = db.pl_cd.FirstOrDefault(p => p.bgNumber.Contains(FileName));
                    if (cd != null)
                    {
                        string[] strs = DirFileHelper.GetFileNames(DirectoryName);
                        foreach (var str in strs)
                        {
                            if (str.Contains(FileName))
                            {
                                if (string.IsNullOrEmpty(head.port))
                                {
                                    continue;
                                }
                                string dic = YiFuSetting.PL_QY_Path + "//" + head.port+"//"+cd.fileName.Trim();
                                if (!System.IO.Directory.Exists(dic))
                                {
                                    System.IO.Directory.CreateDirectory(dic);
                                }
                                File.Move(str, Path.Combine(dic, Path.GetFileName(str)));
                            }
                        }
                        Pl_Heads.Remove(head);
                    }
                }
                gridControl1.DataSource = Pl_Heads;
                gridControl1.RefreshDataSource();
                splashScreenManager1.CloseWaitForm();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "严重错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                splashScreenManager1.CloseWaitForm();
            }
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            string matched = gridView1.GetRowCellValue(e.RowHandle, "matched").ToString();
            if (matched=="Y")
            {
                 e.Appearance.BackColor = Color.ForestGreen;//改变背景色
                //e.Appearance.ForeColor = Color.Red;//改变字体颜色
            }
        }
    }
}