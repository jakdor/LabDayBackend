namespace LabDayBackend.Models.Db
{
    public class Path 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public bool Active { get; set; }
        public bool IsBlocked { get; set; }
    }
}