using System.Runtime.CompilerServices;
using log4net;

namespace WebApplication_Vy
{
    public class LogHelper
    {
        public static ILog GetLogger([CallerFilePathAttribute] string filename = "")
        {
            return LogManager.GetLogger(filename);
        }
    }
}