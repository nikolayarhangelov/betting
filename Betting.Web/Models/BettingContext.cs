﻿using System.Data.Entity;

namespace Betting.Web.Models
{
    public class BettingContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public BettingContext() : base("name=BettingContext")
        {
#if DEBUG
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BettingContext>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<BettingContext>());
#endif
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<RaceList> RaceLists { get; set; }
    }
}