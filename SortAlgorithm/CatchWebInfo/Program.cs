using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net;

namespace CatchWebInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            SendRequest();
        }

        //方法一

        public static void webClientMethod1()
        {
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            //以字符串的形式返回数据
            string html = wc.DownloadString("https://www.baidu.com/");

            //以正则表达式的形式匹配到字符串网页中想要的数据
            MatchCollection matches = Regex.Matches(html, "<a.*>(.*)</a>");
            //依次取得匹配到的数据
            foreach (Match item in matches)
            {
              
                foreach (Group info in item.Groups)
                {
                    Console.WriteLine(info.Value);
                }
            }
            Console.ReadKey();
        }


        //方法二
        public static string SendRequest()
        {
            string url = "https://www.baidu.com/";
            Uri httpURL = new Uri(url);

            ///HttpWebRequest类继承于WebRequest，并没有自己的构造函数，需通过WebRequest的Creat方法 建立，并进行强制的类型转换   
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(httpURL);
            //httpReq.Headers.Add("cityen", "tj");

            ///通过HttpWebRequest的GetResponse()方法建立HttpWebResponse,强制类型转换   
            HttpWebResponse httpResp = (HttpWebResponse)httpReq.GetResponse();

            ///GetResponseStream()方法获取HTTP响应的数据流,并尝试取得URL中所指定的网页内容   
            ///若成功取得网页的内容，则以System.IO.Stream形式返回，若失败则产生ProtoclViolationException错 误。
            System.IO.Stream respStream = httpResp.GetResponseStream();

            ///返回的内容是Stream形式的，所以可以利用StreamReader类获取GetResponseStream的内容
            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(respStream, Encoding.UTF8);
            //从流的当前位置读取到结尾
            string strBuff = respStreamReader.ReadToEnd();

         
            respStreamReader.Close();
            respStream.Close();
            return strBuff;
        }

    }
}
