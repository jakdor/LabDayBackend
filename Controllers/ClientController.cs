using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LabDayBackend.Models.Db;
using LabDayBackend.Models.Response;
using LabDayBackend.Repositories;
using LabDayBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LabDayBackend.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUpdateTimeRepository _updateTimeRepository;
        private readonly IAuthService _authService;

        public ClientController(IClientRepository clientRepository, IUpdateTimeRepository updateTimeRepository,
            IAuthService authService)
        {
            _clientRepository = clientRepository;
            _updateTimeRepository = updateTimeRepository;
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<LoginResponse> PostLogin(string username, string password)
        {
            var user = _authService.Authenticate(username, password);
            if (user == null) return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.IsAdmin ? "admin" : "user")
                }),
                Expires = DateTime.UtcNow.AddYears(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Startup.JWTSecret), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new LoginResponse {
                Token = tokenString
            };
        }
        
        [HttpGet("last_update")]
        public ActionResult<LastUpdateResponse> GetLastUpdate(){
            return new LastUpdateResponse {
                UpdatedAt = _updateTimeRepository.GetLastUpdateTimestamp()
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
