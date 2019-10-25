namespace LabDayBackend.Models.Db
{
    public class Place
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string Img { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool IsBlocked { get; set; }
    }
}