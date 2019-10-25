using System.Threading.Tasks;
using LabDayBackend.Models.Db;

namespace LabDayBackend.Repositories
{
    public interface IAdminRepository
    {
        public Task AddPath(Path path);
    }
}