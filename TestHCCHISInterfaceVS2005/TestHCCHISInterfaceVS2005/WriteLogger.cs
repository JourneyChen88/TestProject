using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Log4Net
{
    public class WriteLogger
    {
        private static WriteLogger _writeLogger = null;
        private string fileDict;
        private string filePath;

        private WriteLogger()
        {
            this.fileDict = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["LoggerWritePath"];
            CreateDir();
        }

        public static WriteLogger GetWriteLoggerInstance()
        {
            if (_writeLogger == null)
                _writeLogger = new WriteLogger();

            return _writeLogger;
        }

        private void CreateDir()
        {
            if (!Directory.Exists(fileDict))
            {
                Directory.CreateDirectory(fileDict);
            }
        }

        public void WriteLog(string level, string log)
        {
            if (string.IsNullOrEmpty(level))
            {
                throw new ArgumentNullException("level");
            }
            if (string.IsNullOrEmpty(log))
            {
                throw new ArgumentNullException("log");
            }
            WriteTXT(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "==>" + level + "：" + log);
        }

        private void WriteTXT(string msg)
        {
            if (string.IsNullOrEmpty(msg))
            {
                throw new ArgumentNullException("msg");
            }

            filePath = this.fileDict + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            FileMode mode = File.Exists(filePath) ? FileMode.Append : FileMode.OpenOrCreate;
            FileStream fs = new FileStream(filePath, mode);
            StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("GB2312"));
            sw.WriteLine(msg);
            sw.Close();
            fs.Close();
        }
    }
}
