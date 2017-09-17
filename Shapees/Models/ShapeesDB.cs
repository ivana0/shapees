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
using Shapees.Models;


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

            modelBuilder.Entity<UserProfile>()
                .HasKey(t => new { t.profile_id });

            modelBuilder.Entity<Address>()
                .HasKey(t => new { t.AddressID });


        }

        //Variables for sets of entities

        public DbSet<Users> Users { get; set; }                         //Users


        public DbSet<UserProfile> UserProfiles { get; set; }            //User Profiles
        public DbSet<Address> UsersAddress { get; set; }                //User Address
        public DbSet<Director> Director { get; set; }                   //Director
        public DbSet<Educator> Educator { get; set; }                   //Educator
        public DbSet<Parent> Parent { get; set; }                       //Educator

    } 

    
}