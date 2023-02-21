using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Shift>().HasData(
                new Shift
                {
                    Id = 1,
                    Name = "Morning"
                },
                new Shift
                {
                    Id = 2,
                    Name = "Evening"
                });

            modelBuilder.Entity<MonthList>().HasData(
                new MonthList { Id = 1, Name = "January" },
                new MonthList { Id = 2, Name = "February" },
                new MonthList { Id = 3, Name = "March" },
                new MonthList { Id = 4, Name = "April" },
                new MonthList { Id = 5, Name = "May" },
                new MonthList { Id = 6, Name = "June" },
                new MonthList { Id = 7, Name = "July" },
                new MonthList { Id = 8, Name = "August" },
                new MonthList { Id = 9, Name = "September" },
                new MonthList { Id = 10, Name = "October" },
                new MonthList { Id = 11, Name = "November" },
                new MonthList { Id = 12, Name = "December" }
                );

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=ALAMIN-PC;database=CRMDB;User Id=sa;Password = Admin@123!@#;TrustServerCertificate=True;");
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("server=ALAMIN-PC;database=CRMDB;User Id=sa;Password = Admin@123!@#;TrustServerCertificate=True;");
        //    }
        //}
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<SubArea> SubAreas { get; set; }
        public DbSet<ContactBy> Contacts { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ContractDetails> ContractDetails { get; set; }
        public DbSet<BuildingDetails> BuildingDetails { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Followup> Followups { get; set; }
        public DbSet<Shift> ShiftInfos { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<ServiceFeedback> ServiceFeedbacks { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<MonthList> MonthList { get; set; }
        public DbSet<ComplainRegister> Complains { get; set; }
        public DbSet<ComplainFeedback> ComplainFeedback { get; set; }
    }
}
