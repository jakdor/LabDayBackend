namespace LabDayBackend.Models.Request
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int PathId { get; set; }
        public bool IsAdmin { get; set; }
    }
}