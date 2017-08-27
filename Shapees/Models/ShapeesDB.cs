using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;  
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using MySql.Data.Entity;


namespace Shapees.Models
{
    public class ShapeesDB : DbContext
    {
        public ShapeesDB(DbContextOptions<ShapeesDB> dbcontextoption)  
            :base(dbcontextoption)  
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Users>()
                .HasKey(t => new { t.UserID });

        }

        public DbSet<Users> Users { get; set; }
    } 

    
}