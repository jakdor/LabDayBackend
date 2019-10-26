using System.Collections.Generic;
using LabDayBackend.Models.Db;

namespace LabDayBackend.Repositories
{
    public interface IClientRepository
    {
        public List<Event> GetAllEvents();
        public List<Path> GetAllPaths();
        public List<Place> GetAllPlaces();
        public List<Speaker> GetAllSpeakers();
        public List<Timetable> GetAllTimetables();
    }
}