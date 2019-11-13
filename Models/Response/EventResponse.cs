namespace LabDayBackend.Models.Response
{
    public class EventResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public string Address { get; set; }
        public string Room { get; set; }
        public string Info { get; set; }
        public string Topic { get; set; }
        public string Dor1Img { get; set; }
        public string Dor2Img { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool IsBlocked { get; set; }

        public int SpeakerId { get; set; }
    }
}