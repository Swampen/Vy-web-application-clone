using System.Runtime.CompilerServices;
using log4net;

namespace WebApplication_Vy.Utils.Logging
{
    public class LogHelper
    {
        public static ILog GetLogger([CallerFilePath] string filename = "")
        {
            return LogManager.GetLogger(filename);
        }
    }
}