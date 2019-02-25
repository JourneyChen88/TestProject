using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Log4Net
{
    public interface ILogger
    {
        void Debug(string msg);

        void Info(string msg);

        void Error(string msg);

        void Fatal(string msg);

        void Warn(string msg);
    }
}
