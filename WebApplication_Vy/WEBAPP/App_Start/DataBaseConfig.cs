using DAL.Db;

namespace WebApplication_Vy
{
    public class DataBaseConfig
    {
        public static void InitializeDatabase()
        {
            var db = new VyDbContext();
            db.Database.Initialize(true);
        }
    }
}