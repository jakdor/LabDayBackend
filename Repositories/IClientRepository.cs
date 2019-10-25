using System.Collections.Generic;
using LabDayBackend.Models.Db;

namespace LabDayBackend.Repositories
{
    public interface IClientRepository
    {
        public List<Path> GetAllPaths();
    }
}