using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Shapees.Models.DatabaseModel
{
    public partial class masterContext : DbContext
    {
        public virtual DbSet<Announcement> Announcement { get; set; }
        public virtual DbSet<Childinfo> Childinfo { get; set; }
        public virtual DbSet<Document> Document { get; set; }
        public virtual DbSet<Media> Media { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Report> Report { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<Task> Task { get; set; }
        public virtual DbSet<Userinfo> Userinfo { get; set; }

        public masterContext(DbContextOptions<masterContext> dbcontextoption)  
            :base(dbcontextoption)  
        { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Announcement>(entity =>
            {
                entity.ToTable("announcement");

                entity.Property(e => e.Announcementid).HasColumnName("announcementid");

                entity.Property(e => e.Announcementfor)
                    .IsRequired()
                    .HasColumnName("announcementfor")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Announcementtype)
                    .IsRequired()
                    .HasColumnName("announcementtype")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Datecreated)
                    .HasColumnName("datecreated")
                    .HasColumnType("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar(max)");

                entity.Property(e => e.Isdisplayed).HasColumnName("isdisplayed");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("nchar(255)");
            });

            modelBuilder.Entity<Childinfo>(entity =>
            {
                entity.HasKey(e => e.Childid)
                    .HasName("PK__childinf__223829E59681D5F3");

                entity.ToTable("childinfo");

                entity.Property(e => e.Childid).HasColumnName("childid");

                entity.Property(e => e.Childfirstname)
                    .IsRequired()
                    .HasColumnName("childfirstname")
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.Childlastname)
                    .IsRequired()
                    .HasColumnName("childlastname")
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasColumnType("nchar(20)");

                entity.Property(e => e.Contacnumber1)
                    .HasColumnName("contacnumber1")
                    .HasColumnType("nchar(20)");

                entity.Property(e => e.Contacnumber2)
                    .HasColumnName("contacnumber2")
                    .HasColumnType("nchar(20)");

                entity.Property(e => e.Currentage)
                    .HasColumnName("currentage")
                    .HasColumnType("nchar(30)");

                entity.Property(e => e.Dob)
                    .HasColumnName("dob")
                    .HasColumnType("date");

                entity.Property(e => e.Educatorfname)
                    .IsRequired()
                    .HasColumnName("educatorfname")
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.Educatorid).HasColumnName("educatorid");

                entity.Property(e => e.Educatorlname)
                    .IsRequired()
                    .HasColumnName("educatorlname")
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.Inroom)
                    .IsRequired()
                    .HasColumnName("inroom")
                    .HasColumnType("nchar(30)");

                entity.Property(e => e.Parent1).HasColumnName("parent1");

                entity.Property(e => e.Parent1fname)
                    .HasColumnName("parent1fname")
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.Parent1lname)
                    .HasColumnName("parent1lname")
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.Parent2).HasColumnName("parent2");

                entity.Property(e => e.Parent2fname)
                    .HasColumnName("parent2fname")
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.Parent2lname)
                    .HasColumnName("parent2lname")
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.Postcode).HasColumnName("postcode");

                entity.Property(e => e.Profileimage)
                    .HasColumnName("profileimage")
                    .HasColumnType("nchar(255)");

                entity.Property(e => e.Roomid).HasColumnName("roomid");

                entity.Property(e => e.Shortinfo)
                    .HasColumnName("shortinfo")
                    .HasColumnType("nchar(250)");

                entity.Property(e => e.Specialneeds)
                    .IsRequired()
                    .HasColumnName("specialneeds")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasColumnType("nchar(20)");

                entity.Property(e => e.Street)
                    .HasColumnName("street")
                    .HasColumnType("nchar(50)");

                entity.HasOne(d => d.Educator)
                    .WithMany(p => p.ChildinfoEducator)
                    .HasForeignKey(d => d.Educatorid)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Parent1Navigation)
                    .WithMany(p => p.ChildinfoParent1Navigation)
                    .HasForeignKey(d => d.Parent1);

                entity.HasOne(d => d.Parent2Navigation)
                    .WithMany(p => p.ChildinfoParent2Navigation)
                    .HasForeignKey(d => d.Parent2);

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Childinfo)
                    .HasForeignKey(d => d.Roomid);
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("document");

                entity.Property(e => e.Documentid).HasColumnName("documentid");

                entity.Property(e => e.Authorfirst)
                    .IsRequired()
                    .HasColumnName("authorfirst")
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.Authorid).HasColumnName("authorid");

                entity.Property(e => e.Authorlast)
                    .IsRequired()
                    .HasColumnName("authorlast")
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.Childfirst)
                    .IsRequired()
                    .HasColumnName("childfirst")
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.Childid).HasColumnName("childid");

                entity.Property(e => e.Childlast)
                    .IsRequired()
                    .HasColumnName("childlast")
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.Dateuploaded)
                    .HasColumnName("dateuploaded")
                    .HasColumnType("date");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("nchar(255)");

                entity.Property(e => e.Documenttype)
                    .IsRequired()
                    .HasColumnName("documenttype")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Filepath)
                    .HasColumnName("filepath")
                    .HasColumnType("nchar(255)");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("nchar(255)");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Document)
                    .HasForeignKey(d => d.Authorid)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_document_userinfo_userid");

                entity.HasOne(d => d.Child)
                    .WithMany(p => p.Document)
                    .HasForeignKey(d => d.Childid)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Media>(entity =>
            {
                entity.ToTable("media");

                entity.Property(e => e.Mediaid).HasColumnName("mediaid");

                entity.Property(e => e.Authorfirst)
                    .IsRequired()
                    .HasColumnName("authorfirst")
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.Authorid).HasColumnName("authorid");

                entity.Property(e => e.Authorlast)
                    .IsRequired()
                    .HasColumnName("authorlast")
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.Childfirst)
                    .IsRequired()
                    .HasColumnName("childfirst")
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.Childid).HasColumnName("childid");

                entity.Property(e => e.Childlast)
                    .IsRequired()
                    .HasColumnName("childlast")
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.Dateuploaded)
                    .HasColumnName("dateuploaded")
                    .HasColumnType("date");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("nchar(255)");

                entity.Property(e => e.Filepath)
                    .HasColumnName("filepath")
                    .HasColumnType("nchar(255)");

                entity.Property(e => e.Mediatype)
                    .IsRequired()
                    .HasColumnName("mediatype")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("nchar(255)");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.Authorid)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_media_userinfo_userid");

                entity.HasOne(d => d.Child)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.Childid)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("message");

                entity.Property(e => e.Messageid).HasColumnName("messageid");

                entity.Property(e => e.Authorfirst)
                    .IsRequired()
                    .HasColumnName("authorfirst")
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.Authorid).HasColumnName("authorid");

                entity.Property(e => e.Authorlast)
                    .IsRequired()
                    .HasColumnName("authorlast")
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.Dateopened)
                    .HasColumnName("dateopened")
                    .HasColumnType("date");

                entity.Property(e => e.Datereceived)
                    .HasColumnName("datereceived")
                    .HasColumnType("date");

                entity.Property(e => e.Datesent)
                    .HasColumnName("datesent")
                    .HasColumnType("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar(max)");

                entity.Property(e => e.Isopened).HasColumnName("isopened");

                entity.Property(e => e.Isrepliedto).HasColumnName("isrepliedto");

                entity.Property(e => e.Receiverfirst)
                    .IsRequired()
                    .HasColumnName("receiverfirst")
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.Receiverid).HasColumnName("receiverid");

                entity.Property(e => e.Receiverlast)
                    .IsRequired()
                    .HasColumnName("receiverlast")
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasColumnName("subject")
                    .HasColumnType("nchar(255)");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.MessageAuthor)
                    .HasForeignKey(d => d.Authorid)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.MessageReceiver)
                    .HasForeignKey(d => d.Receiverid)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("report");

                entity.Property(e => e.Reportid).HasColumnName("reportid");

                entity.Property(e => e.Attachmentcount).HasColumnName("attachmentcount");

                entity.Property(e => e.Attachmentpaths)
                    .HasColumnName("attachmentpaths")
                    .HasColumnType("varchar(max)");

                entity.Property(e => e.Authorfirst)
                    .IsRequired()
                    .HasColumnName("authorfirst")
                    .HasColumnType("nchar(50)");

                entity.Property(e => e.Authorid).HasColumnName("authorid");

                entity.Property(e => e.Authorlast)
                    .IsRequired()
                    .HasColumnName("authorlast")
                    .HasColumnType("nchar(50)");

                entity.Property(e => e.Bodytext)
                    .HasColumnName("bodytext")
                    .HasColumnType("varchar(max)");

                entity.Property(e => e.Childfirst)
                    .IsRequired()
                    .HasColumnName("childfirst")
                    .HasColumnType("nchar(50)");

                entity.Property(e => e.Childid).HasColumnName("childid");

                entity.Property(e => e.Childlast)
                    .IsRequired()
                    .HasColumnName("childlast")
                    .HasColumnType("nchar(50)");

                entity.Property(e => e.Datecompleted)
                    .HasColumnName("datecompleted")
                    .HasColumnType("date");

                entity.Property(e => e.Datecreated)
                    .HasColumnName("datecreated")
                    .HasColumnType("date");

                entity.Property(e => e.Datesubmitted)
                    .HasColumnName("datesubmitted")
                    .HasColumnType("date");

                entity.Property(e => e.Duedate)
                    .HasColumnName("duedate")
                    .HasColumnType("date");

                entity.Property(e => e.Filepath)
                    .HasColumnName("filepath")
                    .HasColumnType("nchar(255)");

                entity.Property(e => e.Iscompleted).HasColumnName("iscompleted");

                entity.Property(e => e.Issubmitted).HasColumnName("issubmitted");

                entity.Property(e => e.Lastmodified)
                    .HasColumnName("lastmodified")
                    .HasColumnType("date");

                entity.Property(e => e.Reporttype)
                    .IsRequired()
                    .HasColumnName("reporttype")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Subject)
                    .HasColumnName("subject")
                    .HasColumnType("nchar(255)");

                entity.Property(e => e.Taskid).HasColumnName("taskid");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("nchar(255)");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Report)
                    .HasForeignKey(d => d.Authorid)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_report_userinfo_userid");

                entity.HasOne(d => d.Child)
                    .WithMany(p => p.Report)
                    .HasForeignKey(d => d.Childid)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("room");

                entity.Property(e => e.Roomid).HasColumnName("roomid");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar(max)");

                entity.Property(e => e.Info)
                    .HasColumnName("info")
                    .HasColumnType("nchar(255)");

                entity.Property(e => e.Roomagegroup)
                    .HasColumnName("roomagegroup")
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.Roomname)
                    .IsRequired()
                    .HasColumnName("roomname")
                    .HasColumnType("nchar(100)");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("task");

                entity.Property(e => e.Taskid).HasColumnName("taskid");

                entity.Property(e => e.Assignedforid).HasColumnName("assignedforid");

                entity.Property(e => e.Authorfirst)
                    .IsRequired()
                    .HasColumnName("authorfirst")
                    .HasColumnType("nchar(50)");

                entity.Property(e => e.Authorid).HasColumnName("authorid");

                entity.Property(e => e.Authorlast)
                    .IsRequired()
                    .HasColumnName("authorlast")
                    .HasColumnType("nchar(50)");

                entity.Property(e => e.Childfirst)
                    .IsRequired()
                    .HasColumnName("childfirst")
                    .HasColumnType("nchar(50)");

                entity.Property(e => e.Childid).HasColumnName("childid");

                entity.Property(e => e.Childlast)
                    .IsRequired()
                    .HasColumnName("childlast")
                    .HasColumnType("nchar(50)");

                entity.Property(e => e.Dateassigned)
                    .HasColumnName("dateassigned")
                    .HasColumnType("date");

                entity.Property(e => e.Datecompleted)
                    .HasColumnName("datecompleted")
                    .HasColumnType("date");

                entity.Property(e => e.Duedate)
                    .HasColumnName("duedate")
                    .HasColumnType("date");

                entity.Property(e => e.Iscompleted).HasColumnName("iscompleted");

                entity.Property(e => e.Issubmitted).HasColumnName("issubmitted");

                entity.Property(e => e.Reportid).HasColumnName("reportid");

                entity.Property(e => e.Reportpath)
                    .HasColumnName("reportpath")
                    .HasColumnType("nchar(255)");

                entity.Property(e => e.Taskforchild)
                    .IsRequired()
                    .HasColumnName("taskforchild")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Taskforeducator)
                    .IsRequired()
                    .HasColumnName("taskforeducator")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Taskforfirst).HasColumnName("taskforfirst");

                entity.Property(e => e.Taskforlast).HasColumnName("taskforlast");

                entity.HasOne(d => d.Assignedfor)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.Assignedforid)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Child)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.Childid)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.Reportid)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Userinfo>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("PK__userinfo__CBA1B2573E0FC61B");

                entity.ToTable("userinfo");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.Dob)
                    .HasColumnName("dob")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.Employedon)
                    .HasColumnName("employedon")
                    .HasColumnType("date");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.Homephone)
                    .HasColumnName("homephone")
                    .HasColumnType("nchar(20)");

                entity.Property(e => e.Isloggedin).HasColumnName("isloggedin");

                entity.Property(e => e.Lastlogin)
                    .HasColumnName("lastlogin")
                    .HasColumnType("datetime");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasColumnName("lastname")
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.Mobilephone)
                    .HasColumnName("mobilephone")
                    .HasColumnType("nchar(20)");

                entity.Property(e => e.Othercontact)
                    .HasColumnName("othercontact")
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.Parentof)
                    .HasColumnName("parentof")
                    .HasColumnType("nchar(255)");

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasColumnName("pass")
                    .HasColumnType("nchar(20)");

                entity.Property(e => e.Postcode).HasColumnName("postcode");

                entity.Property(e => e.Profileimage)
                    .HasColumnName("profileimage")
                    .HasColumnType("nchar(255)");

                entity.Property(e => e.Roomassigned)
                    .HasColumnName("roomassigned")
                    .HasColumnType("nchar(250)");

                entity.Property(e => e.Roomid).HasColumnName("roomid");

                entity.Property(e => e.Shortbio)
                    .HasColumnName("shortbio")
                    .HasColumnType("nchar(250)");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasColumnType("nchar(20)");

                entity.Property(e => e.Street)
                    .HasColumnName("street")
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.Taskscompleted).HasColumnName("taskscompleted");

                entity.Property(e => e.Totaltasks).HasColumnName("totaltasks");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasColumnType("nchar(50)");

                entity.Property(e => e.Usertype).HasColumnName("usertype");

                entity.Property(e => e.Usertypename)
                    .HasColumnName("usertypename")
                    .HasColumnType("nchar(20)");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Userinfo)
                    .HasForeignKey(d => d.Roomid);
            });



        }
    }
}