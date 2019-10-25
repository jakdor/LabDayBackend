using System.Threading.Tasks;
using LabDayBackend.Models.Db;
using LabDayBackend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LabDayBackend.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepository;

        public AdminController(IAdminRepository repository)
        {
            _adminRepository = repository;
        }

        [HttpPost("paths")]
        public async Task<ActionResult<Path>> PostPath(Path path){
            await _adminRepository.AddPath(path);
            return Created("paths", path);
        }
    }
}