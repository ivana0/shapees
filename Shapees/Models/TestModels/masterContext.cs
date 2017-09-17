using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Shapees.Models.TestModels
{
    public partial class masterContext : DbContext
    {
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Userprofile> Userprofile { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        public masterContext(DbContextOptions<masterContext> dbcontextoption)  
            :base(dbcontextoption)  
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("address");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasColumnType("nchar(20)");

                entity.Property(e => e.Postcode).HasColumnName("postcode");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state")
                    .HasColumnType("nchar(20)");

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasColumnName("street")
                    .HasColumnType("nchar(50)");
            });

            modelBuilder.Entity<Userprofile>(entity =>
            {
                entity.HasKey(e => e.ProfileId)
                    .HasName("PK__userprof__AEBB701F04F0FC90");

                entity.ToTable("userprofile");

                entity.Property(e => e.ProfileId).HasColumnName("profile_id");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.Dob)
                    .HasColumnName("dob")
                    .HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasColumnType("nchar(50)");

                entity.Property(e => e.HomePhone)
                    .HasColumnName("home_phone")
                    .HasColumnType("nchar(20)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasColumnType("nchar(50)");

                entity.Property(e => e.MobilePhone)
                    .HasColumnName("mobile_phone")
                    .HasColumnType("nchar(20)");

                entity.Property(e => e.Profileimage).HasColumnName("profileimage");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Userprofile)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Userprofile)
                    .HasForeignKey(d => d.Userid);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("PK__users__CBA1B257443D2283");

                entity.ToTable("users");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("nchar(20)");

                entity.Property(e => e.IsLoggedIn).HasColumnName("is_logged_in");

                entity.Property(e => e.LastLogin)
                    .HasColumnName("last_login")
                    .HasColumnType("datetime");

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasColumnName("pass")
                    .HasColumnType("nchar(20)");

                entity.Property(e => e.ProfileId).HasColumnName("profile_id");

                entity.Property(e => e.UserType).HasColumnName("user_type");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasColumnType("nchar(20)");
            });
        }
    }
}