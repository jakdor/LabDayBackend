using System.Threading.Tasks;
using LabDayBackend.Models;
using LabDayBackend.Models.Db;

namespace LabDayBackend.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly LabDayContext _context;

        public AdminRepository(LabDayContext context)
        {
            _context = context;
        }

        public Task AddPath(Path path)
        {
            _context.Paths.Add(path);
            return _context.SaveChangesAsync();
        }
    }
}