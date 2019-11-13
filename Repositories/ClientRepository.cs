using System.Collections.Generic;
using System.Linq;
using LabDayBackend.Models;
using LabDayBackend.Models.Db;
using LabDayBackend.Models.Response;

namespace LabDayBackend.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly LabDayContext _context;

        public ClientRepository(LabDayContext context)
        {
            _context = context;
        }

        public AppDataResponse GetAppData(int pathId)
        {
            var pathQuery = _context.Paths.Where(obj => obj.Id == pathId && !obj.IsBlocked).ToList();
            var pathObj = pathQuery.FirstOrDefault();

            if(pathObj == null) return null;

            pathObj.Active = true;

            var timetablesQuery = _context.Timetables.Where(obj => obj.PathId == pathObj.Id && !obj.IsBlocked).ToList();
            var eventsIds = timetablesQuery.Select(obj => obj.EventId);

            var eventsQuery = _context.Events.Where(obj => eventsIds.Contains(obj.Id))
                .Join(_context.Places, 
                e => e.PlaceId, 
                p => p.Id,
                (e, p) => new EventResponse{
                    Id = e.Id,
                    Name = e.Name,
                    Img = e.Img,
                    Address = p.Info,
                    Room = p.Name,
                    Info = e.Info,
                    Topic = e.Topic,
                    Dor1Img = p.Dor1Img,
                    Dor2Img = p.Dor2Img,
                    Latitude = p.Latitude,
                    Longitude = p.Longitude,
                    SpeakerId = e.SpeakerId,
                    IsBlocked = e.IsBlocked || p.IsBlocked
                }).Where(obj => !obj.IsBlocked).ToList();
                
            var speakersIds = eventsQuery.Select(obj => obj.SpeakerId);

            var speakersQuery = _context.Speakers.Where(obj => speakersIds.Contains(obj.Id)).ToList();

            var placesQuery = _context.Places.Where(obj => !obj.IsBlocked && obj.Type != 0).ToList();

            return new AppDataResponse {
                Paths = pathQuery,
                Timetables = timetablesQuery,
                Events = eventsQuery,
                Speakers = speakersQuery,
                Places = placesQuery
            };
        }

        public List<EventResponse> GetAllEvents()
        {
            return _context.Events.Join(_context.Places, 
                e => e.PlaceId, 
                p => p.Id,
                (e, p) => new EventResponse{
                    Id = e.Id,
                    Name = e.Name,
                    Img = e.Img,
                    Address = p.Info,
                    Room = p.Name,
                    Info = e.Info,
                    Topic = e.Topic,
                    Dor1Img = p.Dor1Img,
                    Dor2Img = p.Dor2Img,
                    Latitude = p.Latitude,
                    Longitude = p.Longitude,
                    SpeakerId = e.SpeakerId,
                    IsBlocked = e.IsBlocked || p.IsBlocked
                }).Where(obj => !obj.IsBlocked).ToList();
        }

        public List<Path> GetAllPaths()
        {
            return _context.Paths.Where(obj => !obj.IsBlocked).ToList();
        }

        public List<Place> GetAllPlaces()
        {
            return _context.Places.Where(obj => !obj.IsBlocked && obj.Type != 0).ToList();
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