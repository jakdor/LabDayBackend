namespace LabDayBackend.Models.Db
{
    public class Event 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public string Info { get; set; }
        public string Topic { get; set; }
        public bool IsBlocked { get; set; }

        public int PlaceId { get; set; }
        public int SpeakerId { get; set; }
    }
}