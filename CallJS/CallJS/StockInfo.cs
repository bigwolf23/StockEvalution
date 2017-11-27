using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallJS
{
    public class StockInfo
    {
        public string StockID { get; set; }             //股票ID
        public string Name { get; set; }                //股票名称
        public string Today_Opening { get; set; }       //今日开盘价
        public string YesToday_Closing { get; set; }    //昨日收盘价
        public string Current_price { get; set; }       //当前价格
        public string Stock_number { get; set; }        //成交股票数
        public string Stock_Amount { get; set; }        //成交金额
        public string Stock_Date { get; set; }          //日期
        public string Stock_Time { get; set; }          //时间
    }
}
