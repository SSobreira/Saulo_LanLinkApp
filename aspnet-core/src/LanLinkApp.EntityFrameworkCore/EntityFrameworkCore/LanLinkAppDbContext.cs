using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using LanLinkApp.Authorization.Roles;
using LanLinkApp.Authorization.Users;
using LanLinkApp.MultiTenancy;
using LanLinkApp.Classes;

namespace LanLinkApp.EntityFrameworkCore
{
    public class LanLinkAppDbContext : AbpZeroDbContext<Tenant, Role, User, LanLinkAppDbContext>
    {
        /* Define an IDbSet for each entity of the application */
        public DbSet<Department> Departments { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<UserDepartment> UserDepartments { get; set; } 


        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>()
                        .HasOne(a => a.DepartmentCreator)
                        .WithMany(b => b.Transactions)
                        .HasForeignKey(c => c.DepartmentCreatorID)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transaction>()
                        .HasOne(a => a.UserCreator)
                        .WithMany(b => b.Transactions)
                        .HasForeignKey(c => c.UserCreatorID)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserDepartment>()
                        .HasOne(a => a.RefDepartment)
                        .WithMany(b => b.UsersDepartment)
                        .HasForeignKey(c => c.RefDepartmentID)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserDepartment>()
                        .HasOne(a => a.RefUser)
                        .WithMany(b => b.UserDepartments)
                        .HasForeignKey(c => c.RefUserID)
                        .OnDelete(DeleteBehavior.Cascade);
        }


        public LanLinkAppDbContext(DbContextOptions<LanLinkAppDbContext> options)
            : base(options)
        {
               
        }
    }
}
