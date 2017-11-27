using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Xml.Serialization;

namespace CallJS
{

    [Serializable]
    public class StockList
    {
        public StockProperty[] StockPropertys;
        public StockList()
        {
        }
    }

    [Serializable]
    public class StockProperty
    {
        public string StockID;
        public string Name;
        public StockProperty()
        {
        }
        public StockProperty(string sStockID, string sName)
        {
            Name = sName;
            StockID = sStockID;
        }
    }

    [Serializable]
    public class DATAINFO
    {
        public string StockID { get; set; }
        public string Name { get; set; }
    }

    public class StockTemple
    {
        public List<DATAINFO> mStockData = new List<DATAINFO>();

        public void XMLDeserialize()
        {
            try
            {
                if (mStockData != null)
                {
                    mStockData.Clear();
                }
                //从XML用反序列化的形式读出数据
                XmlSerializer xs = new XmlSerializer(typeof(StockList));
                Stream stream = new FileStream(@"G:\Project\My_Source\C_SHARP\CallJS\CallJS\StockList.xml",
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.Read);
                StockList p = xs.Deserialize(stream) as StockList;
                //将读出的数据放入到需要显示的LISTBOX数据中
                for (int i = 0; p.StockPropertys[i] != null; i++)
                {
                    DATAINFO pTemp = new DATAINFO();
                    pTemp.StockID = p.StockPropertys[i].StockID;
                    pTemp.Name = p.StockPropertys[i].Name;
                    mStockData.Add(pTemp);
                }

                stream.Close();
            }
            catch (System.Exception ex)
            {
                
            }

        }

        public void XMLSerialize()
        {
            try
            {
                //从XML用序列化的形式写入数据
                XmlSerializer xs = new XmlSerializer(typeof(StockList));
                //Stream stream = new FileStream(@"StockList.xml",
                Stream stream = new FileStream(@"G:\Project\My_Source\C_SHARP\CallJS\CallJS\StockList.xml",
                    FileMode.Open,
                    FileAccess.Write,
                    FileShare.Read);
                xs.Serialize(stream, mStockData);
                stream.Close();
            }
            catch (System.Exception ex)
            {

            }

        }
    }

}
