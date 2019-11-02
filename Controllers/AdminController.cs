using System.Threading.Tasks;
using LabDayBackend.Models.Db;
using LabDayBackend.Models.Request;
using LabDayBackend.Models.Response;
using LabDayBackend.Repositories;
using LabDayBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabDayBackend.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/admin")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IAuthService _authService;

        public AdminController(IAdminRepository repository, IAuthService authService)
        {
            _adminRepository = repository;
            _authService = authService;
        }

        [HttpPost("register")]
        public ActionResult PostRegister(RegisterRequest model)
        {
            if(!_authService.Register(model.Username, model.Password, model.PathId, model.IsAdmin)) return BadRequest();
            return Created("register", model);
        }

        [HttpPost("initdb")]
        public async Task<ActionResult<AppDataResponse>> PostInitDb(AppDataResponse bundle){
            await _adminRepository.InitDb(bundle);
            return Created("initdb", bundle);
        }

        [HttpPost("events")]
        public async Task<ActionResult<Event>> PostPath(Event eventModel){
            await _adminRepository.AddEvent(eventModel);
            return Created("events", eventModel);
        }

        [HttpPost("paths")]
        public async Task<ActionResult<Path>> PostPath(Path path){
            await _adminRepository.AddPath(path);
            return Created("paths", path);
        }

        [HttpPost("places")]
        public async Task<ActionResult<Path>> PostPlace(Place place){
            await _adminRepository.AddPlace(place);
            return Created("places", place);
        }

        [HttpPost("speakers")]
        public async Task<ActionResult<Path>> PostSpeaker(Speaker speaker){
            await _adminRepository.AddSpeaker(speaker);
            return Created("speakers", speaker);
        }

        [HttpPost("timetables")]
        public async Task<ActionResult<Timetable>> PostTimetables(Timetable timetable){
            await _adminRepository.AddTimetable(timetable);
            return Created("timetables", timetable);
        }

        [HttpPut("events/{id}")]
        public async Task<IActionResult> PutEvent(int id, Event eventModel)
        {
            if (id != eventModel.Id) return BadRequest();
            await _adminRepository.PutEvent(eventModel);
            return NoContent();
        }

        [HttpPut("paths/{id}")]
        public async Task<IActionResult> PutPath(int id, Path path)
        {
            if (id != path.Id) return BadRequest();
            await _adminRepository.PutPath(path);
            return NoContent();
        }

        [HttpPut("places/{id}")]
        public async Task<IActionResult> PutPlace(int id, Place place)
        {
            if (id != place.Id) return BadRequest();
            await _adminRepository.PutPlace(place);
            return NoContent();
        }

        [HttpPut("speakers/{id}")]
        public async Task<IActionResult> PutSpeaker(int id, Speaker speaker)
        {
            if (id != speaker.Id) return BadRequest();
            await _adminRepository.PutSpeaker(speaker);
            return NoContent();
        }

        [HttpPut("timetables/{id}")]
        public async Task<IActionResult> PutTimetable(int id, Timetable timetable)
        {
            if (id != timetable.Id) return BadRequest();
            await _adminRepository.PutTimetable(timetable);
            return NoContent();
        }

        [HttpDelete("events/{id}")]
        public ActionResult<Event> DeleteEvent(int id)
        {
            var deletedEvent = _adminRepository.DeleteEvent(id);
            if(deletedEvent != null) return deletedEvent;
            else return Conflict();
        }

        [HttpDelete("paths/{id}")]
        public ActionResult<Path> DeletePath(int id)
        {
            return _adminRepository.DeletePath(id);
        }

        [HttpDelete("places/{id}")]
        public ActionResult<Place> DeletePlace(int id)
        {
            return _adminRepository.DeletePlace(id);
        }

        [HttpDelete("speakers/{id}")]
        public ActionResult<Speaker> DeleteSpeaker(int id)
        {
            var deletedSpeaker = _adminRepository.DeleteSpeaker(id);
            if(deletedSpeaker != null) return deletedSpeaker;
            else return Conflict();
        }

        [HttpDelete("timetables/{id}")]
        public ActionResult<Timetable> DeleteTimetable(int id)
        {
            return _adminRepository.DeleteTimetable(id);
        }
    }
}