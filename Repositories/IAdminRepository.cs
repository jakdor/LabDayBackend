using System.Threading.Tasks;
using LabDayBackend.Models.Db;
using LabDayBackend.Models.Response;

namespace LabDayBackend.Repositories
{
    public interface IAdminRepository
    {
        public Task InitDb(AppDataResponse appData);
        
        public Task AddEvent(Event eventModel);
        public Task AddPath(Path path);
        public Task AddPlace(Place place);
        public Task AddSpeaker(Speaker speaker);
        public Task AddTimetable(Timetable timetable);

        public Task PutEvent(Event eventModel);
        public Task PutPath(Path path);
        public Task PutPlace(Place place);
        public Task PutSpeaker(Speaker speaker);
        public Task PutTimetable(Timetable timetable);

        public Event DeleteEvent(int id);
        public Path DeletePath(int id);
        public Place DeletePlace(int id);
        public Speaker DeleteSpeaker(int id);
        public Timetable DeleteTimetable(int id);
    }
}