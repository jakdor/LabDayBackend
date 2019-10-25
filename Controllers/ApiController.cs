
using System.Threading.Tasks;
using LabDayBackend.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace LabDayBackend.Controllers
{
    [Route("api")]
    public class ApiController : Controller
    {
        [Route("last_update")]
        [HttpGet]
        public async Task<ActionResult<LastUpdateResponse>> GetLastUpdate(){
            return new LastUpdateResponse {
                UpdatedAt = "test"
            };
        }
    }
}

