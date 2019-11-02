namespace LabDayBackend.Models.Db
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBlocked { get; set; }

        public int PathId { get; set; }
        
    }
}