using System.Collections.Generic;
using System.Linq;
using LabDayBackend.Models;
using LabDayBackend.Models.Db;

namespace LabDayBackend.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly LabDayContext _context;

        public ClientRepository(LabDayContext context)
        {
            _context = context;
        }

        public List<Path> GetAllPaths()
        {
            return _context.Paths.ToList();
        }
    }
}