using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabDayBackend.Models;
using LabDayBackend.Models.Db;
using LabDayBackend.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace LabDayBackend.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly LabDayContext _context;
        private readonly IUpdateTimeRepository _updateTimeRepository;

        public AdminRepository(LabDayContext context, IUpdateTimeRepository updateTimeRepository)
        {
            _context = context;
            _updateTimeRepository = updateTimeRepository;
        }

        public Task InitDb(AppDataResponse appData)
        {
            appData.Events.Sort((i1, i2) => i1.Id.CompareTo(i2.Id));
            appData.Paths.Sort((i1, i2) => i1.Id.CompareTo(i2.Id));
            appData.Places.Sort((i1, i2) => i1.Id.CompareTo(i2.Id));
            appData.Speakers.Sort((i1, i2) => i1.Id.CompareTo(i2.Id));
            appData.Timetables.Sort((i1, i2) => i1.Id.CompareTo(i2.Id));

            var newEvents = new List<Event>(appData.Events.Last().Id);
            var newPaths = new List<Path>(appData.Paths.Last().Id);
            var newPlaces = new List<Place>(appData.Places.Last().Id);
            var newSpeakers = new List<Speaker>(appData.Speakers.Last().Id);
            var newTimetables = new List<Timetable>(appData.Timetables.Last().Id);

            for(var i = 1; i <= appData.Places.Last().Id; ++i){
                var obj = appData.Places.FirstOrDefault(o => o.Id == i);
                if(obj != null) newPlaces.Add(obj);
                else newPlaces.Add(new Place { Id = i, IsBlocked = true });
            }

            for(var i = 1; i <= appData.Events.Last().Id; ++i){
                var obj = appData.Events.FirstOrDefault(o => o.Id == i);
                if(obj != null)
                {
                    newPlaces.Add(new Place {
                        Id = (newPlaces.LastOrDefault()?.Id ?? 1) + 1,
                        Type = 0,
                        Name = obj.Room,
                        Address = obj.Address,
                        Dor1Img = obj.Dor1Img,
                        Dor2Img = obj.Dor2Img,
                        Latitude = obj.Latitude,
                        Longitude = obj.Longitude
                    });
                    
                    newEvents.Add(new Event {
                        Id = obj.Id,
                        Name = obj.Name,
                        Img = obj.Img,
                        Info = obj.Info,
                        Topic = obj.Topic,
                        SpeakerId = obj.SpeakerId,
                        PlaceId = newPlaces.LastOrDefault()?.Id ?? 1
                    });
                }
                else
                {
                    newEvents.Add(new Event { Id = i, IsBlocked = true });
                } 
            }

            for(var i = 1; i <= appData.Paths.Last().Id; ++i){
                var obj = appData.Paths.FirstOrDefault(o => o.Id == i);
                if(obj != null) newPaths.Add(obj);
                else newPaths.Add(new Path { Id = i, IsBlocked = true });
            }

            for(var i = 1; i <= appData.Speakers.Last().Id; ++i){
                var obj = appData.Speakers.FirstOrDefault(o => o.Id == i);
                if(obj != null) newSpeakers.Add(obj);
                else newSpeakers.Add(new Speaker { Id = i, IsBlocked = true });
            }

            for(var i = 1; i <= appData.Timetables.Last().Id; ++i){
                var obj = appData.Timetables.FirstOrDefault(o => o.Id == i);
                if(obj != null) newTimetables.Add(obj);
                else newTimetables.Add(new Timetable { Id = i, IsBlocked = true });
            }

            _context.Paths.AddRange(newPaths);
            _context.Places.AddRange(newPlaces);
            _context.Speakers.AddRange(newSpeakers);
            _context.Events.AddRange(newEvents);
            _context.Timetables.AddRange(newTimetables);

            return _updateTimeRepository.UpdateTimestamp();
        }

        public Task AddEvent(Event eventModel)
        {
            _context.Events.Add(eventModel);
            return _updateTimeRepository.UpdateTimestamp();
        }

        public Task AddPath(Path path)
        {
            _context.Paths.Add(path);
            return _updateTimeRepository.UpdateTimestamp();
        }

        public Task AddPlace(Place place)
        {
            _context.Places.Add(place);
            return _updateTimeRepository.UpdateTimestamp();
        }

        public Task AddSpeaker(Speaker speaker)
        {
            _context.Speakers.Add(speaker);
            return _updateTimeRepository.UpdateTimestamp();
        }

        public Task AddTimetable(Timetable timetable)
        {
            _context.Timetables.Add(timetable);
            return _updateTimeRepository.UpdateTimestamp();
        }

        public Task PutEvent(Event eventModel)
        {
            _context.Entry(eventModel).State = EntityState.Modified;
            return _updateTimeRepository.UpdateTimestamp();
        }

        public Task PutPath(Path path)
        {
            _context.Entry(path).State = EntityState.Modified;
            return _updateTimeRepository.UpdateTimestamp();
        }

        public Task PutPlace(Place place)
        {
            _context.Entry(place).State = EntityState.Modified;
            return _updateTimeRepository.UpdateTimestamp();
        }

        public Task PutSpeaker(Speaker speaker)
        {
            _context.Entry(speaker).State = EntityState.Modified;
            return _updateTimeRepository.UpdateTimestamp();
        }

        public Task PutTimetable(Timetable timetable)
        {
            _context.Entry(timetable).State = EntityState.Modified;
            return _updateTimeRepository.UpdateTimestamp();
        }

        public Event DeleteEvent(int id)
        {
            var constraintCheck = _context.Timetables.Where(obj => obj.EventId == id);
            if(constraintCheck.Any()) return null;
            
            var obj = _context.Events.Find(id);
            obj.IsBlocked = true;
            _context.Events.Update(obj);
            _updateTimeRepository.UpdateTimestamp();
            return obj;
        }

        public Path DeletePath(int id)
        {
            var obj = _context.Paths.Find(id);
            obj.IsBlocked = true;
            _context.Paths.Update(obj);
            _updateTimeRepository.UpdateTimestamp();
            return obj;
        }

        public Place DeletePlace(int id)
        {
            var obj = _context.Places.Find(id);
            obj.IsBlocked = true;
            _context.Places.Update(obj);
            _updateTimeRepository.UpdateTimestamp();
            return obj;
        }

        public Speaker DeleteSpeaker(int id)
        {
            var constraintCheck = _context.Events.Where(obj => obj.SpeakerId == id);
            if(constraintCheck.Any()) return null;
            
            var obj = _context.Speakers.Find(id);
            obj.IsBlocked = true;
            _context.Speakers.Update(obj);
            _updateTimeRepository.UpdateTimestamp();
            return obj;
        }

        public Timetable DeleteTimetable(int id)
        {
            var obj = _context.Timetables.Find(id);
            obj.IsBlocked = true;
            _context.Timetables.Update(obj);
            _updateTimeRepository.UpdateTimestamp();
            return obj;
        }
    }
}