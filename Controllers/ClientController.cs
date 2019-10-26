using System.Collections.Generic;
using LabDayBackend.Models.Db;
using LabDayBackend.Models.Response;
using LabDayBackend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LabDayBackend.Controllers
{
    [Route("api")]
    [ApiController]
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository repository)
        {
            _clientRepository = repository;
        }
        
        [HttpGet("last_update")]
        public ActionResult<LastUpdateResponse> GetLastUpdate(){
            return new LastUpdateResponse {
                UpdatedAt = "test" //todo
            };
        }

        [HttpGet("app_data")]
        public ActionResult<AppDataResponse> GetAppData()
        {
            return _clientRepository.GetAppData(2); //todo pathId based on user
        }

        [HttpGet("events")]
        public ActionResult<IList<Event>> GetEvents()
        {
            return _clientRepository.GetAllEvents();
        }

        [HttpGet("paths")]
        public ActionResult<IList<Path>> GetPaths()
        {
            return _clientRepository.GetAllPaths();
        }

        [HttpGet("places")]
        public ActionResult<IList<Place>> GetPlaces()
        {
            return _clientRepository.GetAllPlaces();
        }

        [HttpGet("speakers")]
        public ActionResult<IList<Speaker>> GetSpeakers()
        {
            return _clientRepository.GetAllSpeakers();
        }

        [HttpGet("timetables")]
        public ActionResult<IList<Timetable>> GetTimetables()
        {
            return _clientRepository.GetAllTimetables();
        }
    }
}
