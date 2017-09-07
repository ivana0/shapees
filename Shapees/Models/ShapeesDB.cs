/* ---------------------------------- SHAPEES 2017 ---------------------------------- */
/*                                                                                    */
/* Created and Edited by Four Corner Solutions team:                                  */
/* Ivana Ozakovic, Nicole Lardner, Damon Walker,                                      */
/* Cassandra Kalabric, Matthew Mauri, Sima Narain                                     */
/*                                                                                    */
/* Created for Kid's Uni, University of Wollongong, Wollongong                        */
/*                                                                                    */
/* Filename: Models/ShapeesDB.cs                                                      */
/*                                                                                    */
/* File description: DB Context - ShapeesDB for creating other models.                */
/* ---------------------------------------------------------------------------------- */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;  
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Shapees.Models
{
    public class ShapeesDB : DbContext
    {
        //DB context - ShapeesDB
        public ShapeesDB(DbContextOptions<ShapeesDB> dbcontextoption)  
            :base(dbcontextoption)  
        { }


        //Model builder
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //User model building for User entity
            modelBuilder.Entity<Users>()
                .HasKey(t => new { t.UserID });

        }

        //Variables for sets of entities
        public DbSet<Users> Users { get; set; }             //Users
    } 

    
}