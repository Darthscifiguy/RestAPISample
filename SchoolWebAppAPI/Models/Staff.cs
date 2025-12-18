namespace WebAppAPITest1.Models
{
    public class Staff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string? Notes { get; set; }

        public Staff() { }
        public Staff(int id, string name = "", string title = "", string status = "", string notes = "")
        {
            Id = id;
            Name = name;
            Title = title;
            Status = status;
            Notes = notes;
        }
    }
}