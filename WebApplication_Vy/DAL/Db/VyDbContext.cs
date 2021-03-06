﻿using MODEL.Models.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using UTILS.Utils.Auth;

namespace DAL.Db
{
    public class VyDbContext : DbContext, IDisposable
    {
        private static HashingAndSaltingService Hasher = new HashingAndSaltingService();
        public VyDbContext() : base("name=VyDb")
        {
            Database.SetInitializer(new VyDbInitializer<VyDbContext>());
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Zipcode> Zipcodes { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<ChangeLog> ChangeLogs { get; set; }

        public class VyDbInitializer<T> : CreateDatabaseIfNotExists<VyDbContext>
        {
            protected override void Seed(VyDbContext context)
            {
                var zipxml = XElement.Load(HttpContext.Current.Server.MapPath("~/Content/data/") + "zipcodes.xml");
                var zipz = zipxml.Descendants("Zipcode");

                foreach (var zipcode in zipz)
                    try
                    {
                        context.Zipcodes.Add(new Zipcode
                        {
                            Postalcode = (string)zipcode.Element("Postalcode"),
                            Postaltown = (string)zipcode.Element("Postaltown")
                        });
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("XML config is wrong");
                        Console.WriteLine(e.StackTrace);
                        throw;
                    }

                try
                {
                    var data = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                        @"Content\data\stops.csv"));
                    var reader = new StringReader(data);
                    reader.ReadLine();
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                        var strings = line.Split(',');
                        context.Stations.Add(new Station
                        {
                            Name = strings[1],
                            StopId = strings[0]
                        });
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                try
                {
                    string salt = Hasher.MakeSalt();
                    var hashedPword = Hasher.GenerateSaltedHash(Encoding.UTF8.GetBytes("admin"), Encoding.UTF8.GetBytes(salt));
                    AdminUser superAdmin = new AdminUser
                    {
                        UserName = "admin",
                        Password = hashedPword,
                        SuperAdmin = true,
                        salt = salt
                    };
                    context.AdminUsers.Add(superAdmin);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                base.Seed(context);
            }
        }

        public override int SaveChanges()
        {
            var modifiedEntities = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified).ToList();
            var now = DateTime.UtcNow;

            var addedEntities = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Added).ToList();

            foreach (var change in modifiedEntities)
            {
                var entityName = change.Entity.GetType().Name;
                var primaryKey = GetPrimaryKeyValue(change);

                foreach (var prop in change.OriginalValues.PropertyNames)
                {
                    var originalValue = change.OriginalValues[prop].ToString();
                    var currentValue = change.CurrentValues[prop].ToString();
                    if (originalValue != currentValue)
                    {
                        ChangeLog log = new ChangeLog()
                        {
                            EntityName = entityName,
                            PrimaryKeyValue = primaryKey.ToString(),
                            PropertyName = prop,
                            OldValue = originalValue,
                            NewValue = currentValue,
                            DateChanged = now
                        };
                        ChangeLogs.Add(log);
                    }
                }
            }

            foreach (var added in addedEntities)
            {
                var entityName = added.Entity.GetType().Name;

                //foreach (var prop in added.OriginalValues.PropertyNames)
                //{
                //    var value = added.OriginalValues[prop].ToString();
                //    ChangeLog log = new ChangeLog()
                //    {
                //        EntityName = entityName,
                //        PrimaryKeyValue = "1",
                //        PropertyName = prop,
                //        OldValue = null,
                //        NewValue = value,
                //        DateChanged = now
                //    };
                //    ChangeLogs.Add(log);
                //}
            }
            return base.SaveChanges();


        }

        object GetPrimaryKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            return objectStateEntry.EntityKey.EntityKeyValues[0].Value;
        }

    }
}