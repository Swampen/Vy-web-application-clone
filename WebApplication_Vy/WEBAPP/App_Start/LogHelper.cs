using System.Runtime.CompilerServices;

namespace WebApplication_Vy
{
    public class LogHelper
    {
        public static log4net.ILog GetLogger([CallerFilePathAttribute] string filename = "")
        {
            return log4net.LogManager.GetLogger(filename);
        }
    }
}