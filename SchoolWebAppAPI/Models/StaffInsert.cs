namespace SchoolWebAppAPI.Models
{
    public class StaffInsert
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string? Notes { get; set; }
        //Special part for JSON payload to give a message for its completed state
        public string? Result {  get; set; }

        public StaffInsert() { }
        public StaffInsert(string name, string title, string status, string notes = "")
        {
            Name = name;
            Title = title;
            Status = status;
            Notes = notes;
        }
    }
}
