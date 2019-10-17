using System.Runtime.CompilerServices;
using log4net;

namespace UTILS.Utils.Logging
{
    public class LogHelper
    {
        public static ILog GetLogger([CallerFilePath] string filename = "")
        {
            return LogManager.GetLogger(filename);
        }
    }
}