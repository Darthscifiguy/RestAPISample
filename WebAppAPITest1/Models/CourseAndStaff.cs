namespace WebAppAPITest1.Models
{
    public class CourseAndStaff
    {
        public CourseAndStaff() { }
        public CourseAndStaff(int courseId, string courseName, int teacherCourseID, int studentCount, DateTime courseStartDate, DateTime courseEndDate, string notes, string staffName, string positionName, string currentStatus)
        {
            Id = courseId;
            Name = courseName;
            TeacherID = teacherCourseID;
            StudentCount = studentCount;
            StartDate = courseStartDate;
            EndDate = courseEndDate;
            Notes = notes;
            Staff = staffName;
            Position = positionName;
            Status = currentStatus;

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int TeacherID { get; set; }
        public int StudentCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Notes { get; set; }
        public string? Staff { get; set; }
        public string? Position { get; set; }
        public string? Status { get; set; }
    }
}