namespace LabDayBackend.Models.Db
{
    public class Speaker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string Img { get; set; }
        public bool IsBlocked { get; set; }
    }
}