using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading ;
using System.Windows.Forms;
using System.Timers;

namespace CallJS
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }
        private delegate void SetTBMethodInvoke(object state);

        public StockTemple m_pStockTemplate = new StockTemple();
        public List<StockInfo> m_pStockInfo = new List<StockInfo>();

        private void Form1_Load(object sender, EventArgs e)
        {
            m_pStockTemplate.XMLDeserialize();
//             Thread t1 = new Thread(new ParameterizedThreadStart(TestMethod));
//             t1.Start("hello");
            System.Threading.Timer timerThr;
            timerThr = new System.Threading.Timer(new TimerCallback(SetTB), null, Timeout.Infinite, 5000);
            //listView1
        }

        public void SetTB(object value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetTBMethodInvoke(SetTB), value);
            }
            else
            {
                if (m_pStockInfo != null)
                {
                    m_pStockInfo.Clear();
                }
                getStockAllData(m_pStockTemplate);
            }
        }

//         public static void TestMethod(object data)
//         {
//             string datastr = data as str ing;
//             string display = @"带参数的线程函数，参数为："+datastr;
//             MessageBox.Show(display);
//         }

        private void getStockAllData(StockTemple pStockData)
        {
            foreach (DATAINFO datainfo in pStockData.mStockData)
            {
                getStockDatabyId(datainfo.StockID);
            }
        }

        private void getStockDatabyId(string strStockId)
        {
            string posturl = "";//pos地址
            string postdate = "";//post内容
            string strResult = "";
            string gpdm = strStockId.Trim();
            posturl = "http://hq.sinajs.cn/list=" + gpdm;
            postdate = "";
            strResult = NetSocket.PostUrl(posturl, postdate);

            strResult = strResult.Replace("\"", "");
            strResult = strResult.Replace(";", "");
            int equPosition = strResult.IndexOf('=');
            strResult = strResult.Substring(equPosition + 1);
            // MessageBox.Show(strResult);
            try
            {

                string[] strlist = strResult.Split(',');
                StockInfo pTempInfo = new StockInfo();
                pTempInfo.Name = strlist[0];
                pTempInfo.StockID = gpdm;
                pTempInfo.Today_Opening = strlist[1];
                pTempInfo.YesToday_Closing = strlist[2];
                pTempInfo.Current_price = strlist[3];
                pTempInfo.Stock_number = strlist[8];
                pTempInfo.Stock_Amount = strlist[9];
                pTempInfo.Stock_Date = strlist[30];
                pTempInfo.Stock_Time = strlist[31];
                m_pStockInfo.Add(pTempInfo);
//                 textBox2.Text = "股票名称：" + strlist[0] + "\r\n";
//                 textBox2.Text += "今日开盘价：" + strlist[1] + "\r\n";
//                 textBox2.Text += "昨日收盘价：" + strlist[2] + "\r\n";
//                 textBox2.Text += "当前价格：" + strlist[3] + "\r\n";
//                 textBox2.Text += "今日最高价：" + strlist[4] + "\r\n";
//                 textBox2.Text += "今日最低价：" + strlist[5] + "\r\n";
//                 textBox2.Text += "竞买价：" + strlist[6] + "\r\n";
//                 textBox2.Text += "竞卖价：" + strlist[7] + "\r\n";
//                 textBox2.Text += "成交股票数：" + strlist[8] + "\r\n";
//                 textBox2.Text += "成交金额：" + strlist[9] + "\r\n";
//                 textBox2.Text += "买一：" + strlist[10] + "\r\n";
//                 textBox2.Text += "买一报价：" + strlist[11] + "\r\n";
//                 textBox2.Text += "买二：" + strlist[12] + "\r\n";
//                 textBox2.Text += "买二报价：" + strlist[13] + "\r\n";
//                 textBox2.Text += "买三：" + strlist[14] + "\r\n";
//                 textBox2.Text += "买三报价：" + strlist[15] + "\r\n";
//                 textBox2.Text += "买四：" + strlist[16] + "\r\n";
//                 textBox2.Text += "买四报价：" + strlist[17] + "\r\n";
//                 textBox2.Text += "买五：" + strlist[18] + "\r\n";
//                 textBox2.Text += "买五报价：" + strlist[19] + "\r\n";
//                 textBox2.Text += "卖一：" + strlist[20] + "\r\n";
//                 textBox2.Text += "卖一报价：" + strlist[21] + "\r\n";
//                 textBox2.Text += "卖二：" + strlist[22] + "\r\n";
//                 textBox2.Text += "卖二报价：" + strlist[23] + "\r\n";
//                 textBox2.Text += "卖三：" + strlist[24] + "\r\n";
//                 textBox2.Text += "卖三报价：" + strlist[25] + "\r\n";
//                 textBox2.Text += "卖四：" + strlist[26] + "\r\n";
//                 textBox2.Text += "卖四报价：" + strlist[27] + "\r\n";
//                 textBox2.Text += "卖五：" + strlist[28] + "\r\n";
//                 textBox2.Text += "卖五报价：" + strlist[29] + "\r\n";
//                 textBox2.Text += "日期：" + strlist[30] + "\r\n";
//                 textBox2.Text += "时间：" + strlist[31] + "\r\n";
            }
            catch (Exception)
            {

            }
        }
        private void main_SizeChanged(object sender, EventArgs e)
        {
            //判断是否选择的是最小化按钮
            if (WindowState == FormWindowState.Minimized)
            {
                //隐藏任务栏区图标
                this.ShowInTaskbar = false;
                //图标显示在托盘区
                StockAnalyze.Visible = true;
            }
        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // 关闭所有的线程
                this.Dispose();
                this.Close();
            }
            else
            {
                e.Cancel = true;
            } 
        }

        private void StockAnalyze_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                //还原窗体显示    
                WindowState = FormWindowState.Normal;
                //激活窗体并给予它焦点
                this.Activate();
                //任务栏区显示图标
                this.ShowInTaskbar = true;
                //托盘区图标隐藏
                StockAnalyze.Visible = false;
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
        /// 托盘右键显示主界面

        private void ShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }
        private void CloseWindows()
        {
            if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // 关闭所有的线程
                this.Dispose();
                this.Close();
            }
        }
        /// 托盘右键退出程序
        private void ExistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseWindows();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            CloseWindows();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
    }
}
