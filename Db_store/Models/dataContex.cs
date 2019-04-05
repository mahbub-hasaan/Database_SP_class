using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Db_store.Models;

namespace Db_store.Models
{
    public class dataContex : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .MapToStoredProcedures();
            modelBuilder.Entity<Account>()
                .MapToStoredProcedures();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}