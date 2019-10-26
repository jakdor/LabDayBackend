using System.Collections.Generic;
using System.Linq;
using LabDayBackend.Models;
using LabDayBackend.Models.Db;

namespace LabDayBackend.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly LabDayContext _context;

        public ClientRepository(LabDayContext context)
        {
            _context = context;
        }

        public List<Event> GetAllEvents()
        {
            return _context.Events.Where(obj => !obj.IsBlocked).ToList();
        }

        public List<Path> GetAllPaths()
        {
            return _context.Paths.Where(obj => !obj.IsBlocked).ToList();
        }

        public List<Place> GetAllPlaces()
        {
            return _context.Places.Where(obj => !obj.IsBlocked).ToList();
        }

        public List<Speaker> GetAllSpeakers()
        {
            return _context.Speakers.Where(obj => !obj.IsBlocked).ToList();
        }

        public List<Timetable> GetAllTimetables()
        {
            return _context.Timetables.Where(obj => !obj.IsBlocked).ToList();
        }
    }
}