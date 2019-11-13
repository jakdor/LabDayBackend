using System.Collections.Generic;
using LabDayBackend.Models.Db;
using LabDayBackend.Models.Response;

namespace LabDayBackend.Repositories
{
    public interface IClientRepository
    {
        public AppDataResponse GetAppData(int pathId);

        public List<EventResponse> GetAllEvents();
        public List<Path> GetAllPaths();
        public List<Place> GetAllPlaces();
        public List<Speaker> GetAllSpeakers();
        public List<Timetable> GetAllTimetables();
    }
}