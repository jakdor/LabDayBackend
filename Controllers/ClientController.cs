using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<ActionResult<LastUpdateResponse>> GetLastUpdate(){
            return new LastUpdateResponse {
                UpdatedAt = "test"
            };
        }

        [HttpGet("paths")]
        public ActionResult<IList<Path>> GetPaths()
        {
            return _clientRepository.GetAllPaths();
        }
    }
}
