using DAL.Db;
using log4net;
using UTILS.Utils.Logging;

namespace WebApplication_Vy
{
    public class DataBaseConfig
    {
        public static readonly ILog Log = LogHelper.GetLogger();
        public static void InitializeDatabase()
        {
            Log.Info("Database startup, initializing if not present");
            var db = new VyDbContext();
            db.Database.Initialize(true);
        }
    }
}