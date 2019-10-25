namespace LabDayBackend.Models.Db
{
    public class Timetable 
    {
        public int Id { get; set; }
        public long TimeStart { get; set; }
        public bool IsBlocked { get; set; }

        public int PathId { get; set; }
        public int EventId { get; set; }
    }
}