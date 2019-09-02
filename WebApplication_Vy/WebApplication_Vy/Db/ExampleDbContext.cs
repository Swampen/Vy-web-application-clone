using System.Data.Entity;
using WebApplication_Vy.Models.Entities;

namespace WebApplication_Vy.Db
{
    public class ExampleDbContext : DbContext
    {
        public ExampleDbContext() 
            : base("name=ExampleDB")
        {
            Database.CreateIfNotExists();
        }
        
        public DbSet<ExampleEntity> ExampleEntities { get; set; }
    }
}