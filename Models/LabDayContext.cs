using LabDayBackend.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace LabDayBackend.Models 
{
    public class LabDayContext : DbContext 
    {
        public LabDayContext(DbContextOptions<LabDayContext> options): base(options){
        }

        public DbSet<Path> Paths { get; set; }
        public DbSet<Timetable> Timetables { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Place> Places { get; set; }
    }
}