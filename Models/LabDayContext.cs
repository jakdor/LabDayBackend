using LabDayBackend.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace LabDayBackend.Models 
{
    public class LabDayContext : DbContext 
    {
        public LabDayContext(DbContextOptions<LabDayContext> options): base(options){
            Database.EnsureCreated();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Path>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Timetable>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Event>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Speaker>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Place>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Parameter>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Parameter>()
                .HasIndex(u => u.Key)
                .IsUnique();
        }

        public DbSet<Path> Paths { get; set; }
        public DbSet<Timetable> Timetables { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Place> Places { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
    }
}