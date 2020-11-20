using GroupClass.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Infrastructure
{
    public class AppDbContext:DbContext
    {
        public AppDbContext( DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Class> Classes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.ActivationCode)
                .IsRequired()
                .HasColumnName("activationCode")
                .HasColumnType("varchar(10)");

                entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnName("createdAt")
                .HasColumnType("datetime");

                entity.Property(e => e.ModifiedAt)
                .IsRequired()
                .HasColumnName("modifiedAt")
                .HasColumnType("datetime");

                entity.Property(e => e.Email)
                .IsRequired()
                .HasColumnName("email")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.Firstname)
                .IsRequired()
                .HasColumnName("firstname")
                .HasColumnType("varchar(20)");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int(11)");

                entity.Property(e => e.Lastname)
                .IsRequired()
                .HasColumnName("lastname")
                .HasColumnType("varchar(20)");

                entity.Property(e => e.Password)
                .IsRequired()
                .HasColumnName("password")
                .HasColumnType("varchar(250)");

                entity.Property(e => e.ProfileImageUrl)
                .IsRequired()
                .HasColumnName("profileImageUrl")
                .HasColumnType("varchar(100)");

                entity.Property(e => e.SchoolNumber)
                .IsRequired()
                .HasColumnName("schoolNumber")
                .HasColumnType("varchar(9)");

                entity.Property(e => e.UserRole)
                .IsRequired()
                .HasColumnName("userRole")
                .HasColumnType("varchar(50)");
            });
            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("member");

                entity.Property(e => e.Userid)
                .IsRequired()
                .HasColumnName("userId")
                .HasColumnType("int(11)");

                entity.Property(e => e.Classid)
                .IsRequired()
                .HasColumnName("classId")
                .HasColumnType("int(11)");

                entity.Property(e => e.Id)
                .IsRequired()
                .HasColumnName("id")
                .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnName("createdAt")
                .HasColumnType("datetime");

                entity.Property(e => e.ModifiedAt)
                .IsRequired()
                .HasColumnName("modifiedAt")
                .HasColumnType("datetime");
            });
            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("class");

                entity.Property(e => e.Id)
                .IsRequired()
                .HasColumnName("id")
                .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnName("createdAt")
                .HasColumnType("datetime");

                entity.Property(e => e.ModifiedAt)
                .IsRequired()
                .HasColumnName("modifiedAt")
                .HasColumnType("datetime");

                entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.AlphaNumericCode)
                .IsRequired()
                .HasColumnName("alphaNumericCode")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.EducationYear)
                .IsRequired()
                .HasColumnName("educationYear")
                .HasColumnType("varchar(11)");
            });
            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("post");

                entity.Property(e => e.Userid)
                .IsRequired()
                .HasColumnName("userId")
                .HasColumnType("int(11)");

                entity.Property(e => e.Classid)
                .IsRequired()
                .HasColumnName("classId")
                .HasColumnType("int(11)");

                entity.Property(e => e.Id)
                .IsRequired()
                .HasColumnName("id")
                .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnName("createdAt")
                .HasColumnType("datetime");

                entity.Property(e => e.ModifiedAt)
                .IsRequired()
                .HasColumnName("modifiedAt")
                .HasColumnType("datetime");

                entity.Property(e => e.Title)
                .IsRequired()
                .HasColumnName("title")
                .HasColumnType("varchar(100)");

                entity.Property(e => e.Body)
                .IsRequired()
                .HasColumnName("body")
                .HasColumnType("varchar(500)");
            });

            modelBuilder.Entity<Class>().HasMany(c => c.Members).WithOne(c=>c.Class);

            modelBuilder.Entity<Member>().HasOne(c => c.Class);

            modelBuilder.Entity<Member>().HasIndex(m => m.Classid);

            //modelBuilder.Entity<Member>().HasOne(c => c.User);

            //modelBuilder.Entity<Member>().HasIndex(m => m.Userid);
        }
                
                

            
        
       
    }
}
