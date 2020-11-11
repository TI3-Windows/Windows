using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travelore.Model;
using Task = Travelore.Model.Task;

namespace Travelore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

        public DbSet<TravelList> TravelLists { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionstring = @"Server=.;Database=TraveloreDbAPI;Trusted_Connection=True";
            optionsBuilder.UseSqlServer(connectionstring);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TravelList>()
                .HasMany(tl => tl.Categories)
                .WithOne()
                .HasForeignKey("TravelListId");
            builder.Entity<TravelList>()
                .HasMany(tl => tl.Itinerary)
                .WithOne()
                .HasForeignKey("TravelListId");
            builder.Entity<TravelList>()
                .HasMany(tl => tl.Tasks)
                .WithOne()
                .HasForeignKey("TravelListId");
            builder.Entity<TravelList>()
                .Property(tl => tl.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Entity<TravelList>()
                .Property(tl => tl.Country)
                .IsRequired()
                .HasMaxLength(50);
            builder.Entity<TravelList>()
                .Property(tl => tl.Street)
                .IsRequired()
                .HasMaxLength(50);
            builder.Entity<TravelList>()
                .Property(tl => tl.HouseNr)
                .IsRequired()
                .HasMaxLength(50);
            builder.Entity<TravelList>()
                .Property(tl => tl.DateLeave)
                .IsRequired()
                .HasMaxLength(50);
            builder.Entity<TravelList>()
                .Property(tl => tl.DateBack)
                .IsRequired()
                .HasMaxLength(50);

            builder.Entity<Category>()
                .HasMany(c => c.Items)
                .WithOne()
                .HasForeignKey("CategoryId");
            builder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Entity<Category>()
                .Property(c => c.DoneCat)
                .IsRequired()
                .HasMaxLength(50);

            builder.Entity<Item>()
                .Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Entity<Item>()
                .Property(i => i.Amount)
                .IsRequired()
                .HasMaxLength(50);
            builder.Entity<Item>()
                .Property(i => i.DoneItem)
                .IsRequired()
                .HasMaxLength(50);

            builder.Entity<Task>()
                .Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Entity<Task>()
                .Property(i => i.Description)
                .IsRequired()
                .HasMaxLength(200);
            builder.Entity<Task>()
                .Property(i => i.EndDate)
                .IsRequired();
            builder.Entity<Task>()
                .Property(i => i.DoneTask)
                .IsRequired()
                .HasMaxLength(50);

            builder.Entity<Destination>()
                .Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Entity<Destination>()
                .Property(d => d.Street)
                .IsRequired()
                .HasMaxLength(50);
            builder.Entity<Destination>()
                .Property(d => d.Nr)
                .IsRequired()
                .HasMaxLength(50);
            builder.Entity<Destination>()
                .Property(d => d.Description)
                .IsRequired()
                .HasMaxLength(50);
            builder.Entity<Destination>()
                .Property(d => d.City)
                .IsRequired()
                .HasMaxLength(50);
            builder.Entity<Destination>()
                .Property(d => d.VisitTime)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
