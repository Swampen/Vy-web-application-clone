using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebApplication_Vy.Db.Repositories.Contracts;
using WebApplication_Vy.Models.Entities;

namespace WebApplication_Vy.Db.Repositories.Implementation
{
    public class ExampleRepositoryImpl : ExampleRepository
    {
        public List<ExampleEntity> findAll()
        {
            var db = new ExampleDbContext();
            return db.ExampleEntities.ToList();
        }
    }
}