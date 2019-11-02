using LabDayBackend.Models.Db;

namespace LabDayBackend.Services
{
    public interface IAuthService
    {
        public User Authenticate(string username, string password);
        public User GetById(int userId);
        public bool Register(string username, string password, int pathId, bool isAdmin);
        public bool Delete(int userId);
    }
}