namespace WebAppAPITest1.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int staffID { get; set; }
        public int StudentCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Notes { get; set; }
        public DateTime DateChanged { get; set; }
        public Boolean IsDeleted { get; set; }
        public Course() { }
        public Course(int courseId, string courseName, int teacherCourseID, int studentCount, DateTime courseStartDate, DateTime courseEndDate, string notes, DateTime dateChanged = default, Boolean isDeleted = false)
        {
            Id = courseId;
            Name = courseName;
            staffID = teacherCourseID;
            StudentCount = studentCount;
            StartDate = courseStartDate;
            EndDate = courseEndDate;
            Notes = notes;
            DateChanged = dateChanged;
            IsDeleted = isDeleted;
        }

       
    }
}