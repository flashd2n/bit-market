﻿using Flash.BitMarket.Interfaces.Data.Models;
using Flash.BitMarket.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Flash.BitMarket.Data
{
    public class MainDbContext : IdentityDbContext<User>
    {
        public MainDbContext()
            : base("MainDb", throwIfV1Schema: false)
        {
        }

        public static MainDbContext Create()
        {
            return new MainDbContext();
        }

        public IDbSet<QuoteTest> Quotes { get; set; }

        public override int SaveChanges()
        {
            //this.ApplyAuditInfo();
            return base.SaveChanges();
        }

        private void ApplyAuditInfo()
        {
            Func<DbEntityEntry, bool> filterEntityRule = x => x.Entity is IAuditable && (x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach (var entry in this.ChangeTracker.Entries().Where(filterEntityRule))
            {
                var entity = (IAuditable)entry.Entity;

                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
