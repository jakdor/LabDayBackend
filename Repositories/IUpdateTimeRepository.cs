using System.Threading.Tasks;

namespace LabDayBackend.Repositories
{
    public interface IUpdateTimeRepository
    {
        public Task UpdateTimestamp();
        public string GetLastUpdateTimestamp();
    }
}