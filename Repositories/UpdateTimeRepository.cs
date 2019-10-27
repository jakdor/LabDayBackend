using System;
using System.Linq;
using System.Threading.Tasks;
using LabDayBackend.Models;
using LabDayBackend.Models.Db;

namespace LabDayBackend.Repositories
{
    public class UpdateTimeRepository : IUpdateTimeRepository
    {
        private const string LastUpdateParamKey = "LastUpdate";
        
        private readonly LabDayContext _context;

        public UpdateTimeRepository(LabDayContext context)
        {
            _context = context;
        }

        public string GetLastUpdateTimestamp()
        {
            var lastUpdate = _context.Parameters.Where(obj => obj.Key == LastUpdateParamKey).FirstOrDefault();
            return lastUpdate?.Value ?? string.Empty;
        }

        public Task UpdateTimestamp()
        {
            var lastUpdate = _context.Parameters.Where(obj => obj.Key == LastUpdateParamKey).FirstOrDefault();

            if(lastUpdate != null)
            {
                lastUpdate.Value = DateTime.UtcNow.ToString("o");
                _context.Parameters.Update(lastUpdate);
            }
            else 
            {
                lastUpdate = new Parameter {
                    Key = LastUpdateParamKey,
                    Value = DateTime.UtcNow.ToString("o")
                };

                _context.Parameters.Add(lastUpdate);
            }

            return _context.SaveChangesAsync();
        }
    }
}