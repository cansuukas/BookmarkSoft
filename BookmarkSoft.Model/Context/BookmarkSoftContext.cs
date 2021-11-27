using BookmarkSoft.Core.Entity;
using BookmarkSoft.Model.Entities;
using BookmarkSoft.Model.Maps;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookmarkSoft.Model.Context
{
    public class BookmarkSoftContext : DbContext
    {
        public BookmarkSoftContext(DbContextOptions<BookmarkSoftContext> options) : base(options)
        {

        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new PostMap());
            modelBuilder.ApplyConfiguration(new UserMap());

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }


        public override int SaveChanges()
        {
         

            var modifiedEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified || x.State == EntityState.Added).ToList();

            string computerName = Environment.MachineName; 
            string ipAddress = "127.0.0.1"; 
            DateTime date = DateTime.Now; 

            foreach (var item in modifiedEntities)
            {
                CoreEntity entity = item.Entity as CoreEntity; 

                if (item != null)
                {
                    switch (item.State)
                    {
                        case EntityState.Modified:
                            entity.ModifiedComputerName = computerName;
                            entity.ModifiedIP = ipAddress;
                            entity.ModifiedDate = date;
                            break;
                        case EntityState.Added:
                            entity.CreatedComputerName = computerName;
                            entity.CreatedIP = ipAddress;
                            entity.CreatedDate = date;
                            break;
                    }
                }
            }

            return base.SaveChanges();
        }

    }
}