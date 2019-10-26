using System.Collections.Generic;
using LabDayBackend.Models.Db;

namespace LabDayBackend.Models.Response 
{
    public class AppDataResponse
    {
        public List<Path> Paths { get; set; }
        public List<Timetable> Timetables { get; set; }
        public List<Event> Events { get; set; }
        public List<Speaker> Speakers { get; set; }
        public List<Place> Places { get; set; }
    }
}