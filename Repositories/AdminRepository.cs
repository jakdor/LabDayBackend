using System.Threading.Tasks;
using LabDayBackend.Models;
using LabDayBackend.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace LabDayBackend.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly LabDayContext _context;

        public AdminRepository(LabDayContext context)
        {
            _context = context;
        }

        public Task AddEvent(Event eventModel)
        {
            _context.Events.Add(eventModel);
            return _context.SaveChangesAsync();
        }

        public Task AddPath(Path path)
        {
            _context.Paths.Add(path);
            return _context.SaveChangesAsync();
        }

        public Task AddPlace(Place place)
        {
            _context.Places.Add(place);
            return _context.SaveChangesAsync();
        }

        public Task AddSpeaker(Speaker speaker)
        {
            _context.Speakers.Add(speaker);
            return _context.SaveChangesAsync();
        }

        public Task AddTimetable(Timetable timetable)
        {
            _context.Timetables.Add(timetable);
            return _context.SaveChangesAsync();
        }

        public Task PutEvent(Event eventModel)
        {
            _context.Entry(eventModel).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public Task PutPath(Path path)
        {
            _context.Entry(path).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public Task PutPlace(Place place)
        {
            _context.Entry(place).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public Task PutSpeaker(Speaker speaker)
        {
            _context.Entry(speaker).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public Task PutTimetable(Timetable timetable)
        {
            _context.Entry(timetable).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public Event DeleteEvent(int id)
        {
            var obj = _context.Events.Find(id);
            obj.IsBlocked = true;
            _context.Events.Update(obj);
            _context.SaveChangesAsync();
            return obj;
        }

        public Path DeletePath(int id)
        {
            var obj = _context.Paths.Find(id);
            obj.IsBlocked = true;
            _context.Paths.Update(obj);
            _context.SaveChangesAsync();
            return obj;
        }

        public Place DeletePlace(int id)
        {
            var obj = _context.Places.Find(id);
            obj.IsBlocked = true;
            _context.Places.Update(obj);
            _context.SaveChangesAsync();
            return obj;
        }

        public Speaker DeleteSpeaker(int id)
        {
            var obj = _context.Speakers.Find(id);
            obj.IsBlocked = true;
            _context.Speakers.Update(obj);
            _context.SaveChangesAsync();
            return obj;
        }

        public Timetable DeleteTimetable(int id)
        {
            var obj = _context.Timetables.Find(id);
            obj.IsBlocked = true;
            _context.Timetables.Update(obj);
            _context.SaveChangesAsync();
            return obj;
        }
    }
}