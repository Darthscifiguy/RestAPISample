using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebAppAPITest1.Models;
using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Hosting.Server;
using System.Data;

namespace WebAppAPITest1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly StaffTestDB db = new StaffTestDB();
        private readonly string connectionString = "server = rdabramsschool.database.windows.net; user=CloudSAbeb2522a;password=Master_yoda6!;database=schooldb;Connection Timeout=30";
        private readonly ILogger<CoursesController> _logger;
        private List<Course> listCourses = new List<Course>();
        private List<CourseAndStaff> listCoursesAndStaff= new List<CourseAndStaff>();

        public CoursesController(ILogger<CoursesController> logger)
        {
            _logger = logger;

            /*if(Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING") != null)
            {
                connectionString = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
            }*/

        }

        [HttpGet(Name = "GetCourses")]
        public IEnumerable<Course> Get()
        {

            using var conn = new SqlConnection(connectionString);
            conn.Open();

            var command = new SqlCommand("SELECT * FROM Courses", conn);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Course course = new Course(reader.GetInt32(0), (string)reader["NAME"], reader.GetInt32(2), reader.GetInt32(3), reader.GetDateTime(4), reader.GetDateTime(5), (string)reader["NOTES"]);

                //Console.WriteLine((string)reader["NAME"]);

                listCourses.Add(course);
            }

            //for local testing
            //return db.GetAllStaffs();

            return listCourses;
        }

        [HttpGet]
        [Route("Courses_And_Staff")]
        public IEnumerable<CourseAndStaff> GetCourseAndStaff()
        {

            using var conn = new SqlConnection(connectionString);
            conn.Open();

            var command = new SqlCommand("SELECT courses.*, staff.Name as Staffer, staff.Title as Position, staff.Status from courses LEFT JOIN staff ON courses.teacher_id = staff.staff_id", conn);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CourseAndStaff courseAndStaff = new CourseAndStaff(reader.GetInt32(0), (string)reader["NAME"], reader.GetInt32(2), reader.GetInt32(3), reader.GetDateTime(4), reader.GetDateTime(5), (string)reader["NOTES"], (string)reader["Staffer"], (string)reader["Position"], (string)reader["Status"]);

                //Console.WriteLine((string)reader["NAME"]);

                listCoursesAndStaff.Add(courseAndStaff);
            }

            //for local testing
            //return db.GetAllStaffs();

            return listCoursesAndStaff;
        }

        [HttpGet]
        [Route("{id}/{name}/{stuCount}")]
        public IEnumerable<CourseAndStaff> GetCourseAndStaff(int id = 0000, string name = "a", int stuCount = 0000)
        {

            using var conn = new SqlConnection(connectionString);
            conn.Open();

            if (id == 0000 && name.Length > 1 && stuCount != 0000)
            {
                var command = new SqlCommand($"SELECT courses.*, staff.Name as Staffer, staff.Title as Position, staff.Status from courses LEFT JOIN staff ON courses.teacher_id = staff.staff_id where staff.name LIKE '%{name}%' AND courses.STU_Count >= {stuCount}", conn);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CourseAndStaff courseAndStaff = new CourseAndStaff(reader.GetInt32(0), (string)reader["NAME"], reader.GetInt32(2), reader.GetInt32(3), reader.GetDateTime(4), reader.GetDateTime(5), (string)reader["NOTES"], (string)reader["Staffer"], (string)reader["Position"], (string)reader["Status"]);

                    //Console.WriteLine((string)reader["NAME"]);

                    listCoursesAndStaff.Add(courseAndStaff);
                }
            }
            else if (name.Length <= 1 && id != 0000 && stuCount != 0000)
            {
                var command = new SqlCommand($"SELECT courses.*, staff.Name as Staffer, staff.Title as Position, staff.Status from courses LEFT JOIN staff ON courses.teacher_id = staff.staff_id where courses.course_id = {id} AND courses.STU_Count >= {stuCount}", conn);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CourseAndStaff courseAndStaff = new CourseAndStaff(reader.GetInt32(0), (string)reader["NAME"], reader.GetInt32(2), reader.GetInt32(3), reader.GetDateTime(4), reader.GetDateTime(5), (string)reader["NOTES"], (string)reader["Staffer"], (string)reader["Position"], (string)reader["Status"]);

                    //Console.WriteLine((string)reader["NAME"]);

                    listCoursesAndStaff.Add(courseAndStaff);
                }
            }
            else if (stuCount == 0000 && name.Length > 1 && id != 0000)
            {
                var command = new SqlCommand($"SELECT courses.*, staff.Name as Staffer, staff.Title as Position, staff.Status from courses LEFT JOIN staff ON courses.teacher_id = staff.staff_id where courses.course_id = {id} AND staff.name LIKE '%{name}%'", conn);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CourseAndStaff courseAndStaff = new CourseAndStaff(reader.GetInt32(0), (string)reader["NAME"], reader.GetInt32(2), reader.GetInt32(3), reader.GetDateTime(4), reader.GetDateTime(5), (string)reader["NOTES"], (string)reader["Staffer"], (string)reader["Position"], (string)reader["Status"]);

                    //Console.WriteLine((string)reader["NAME"]);

                    listCoursesAndStaff.Add(courseAndStaff);
                }
            }
            else if (id == 0000 && stuCount == 0000 && name.Length > 1)
            {
                var command = new SqlCommand($"SELECT courses.*, staff.Name as Staffer, staff.Title as Position, staff.Status from courses LEFT JOIN staff ON courses.teacher_id = staff.staff_id where staff.name LIKE '%{name}%'", conn);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CourseAndStaff courseAndStaff = new CourseAndStaff(reader.GetInt32(0), (string)reader["NAME"], reader.GetInt32(2), reader.GetInt32(3), reader.GetDateTime(4), reader.GetDateTime(5), (string)reader["NOTES"], (string)reader["Staffer"], (string)reader["Position"], (string)reader["Status"]);

                    //Console.WriteLine((string)reader["NAME"]);

                    listCoursesAndStaff.Add(courseAndStaff);
                }
            }
            else if (id == 0000 && name.Length <= 1 && stuCount != 0000)
            {
                var command = new SqlCommand($"SELECT courses.*, staff.Name as Staffer, staff.Title as Position, staff.Status from courses LEFT JOIN staff ON courses.teacher_id = staff.staff_id where courses.STU_Count >= {stuCount}", conn);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CourseAndStaff courseAndStaff = new CourseAndStaff(reader.GetInt32(0), (string)reader["NAME"], reader.GetInt32(2), reader.GetInt32(3), reader.GetDateTime(4), reader.GetDateTime(5), (string)reader["NOTES"], (string)reader["Staffer"], (string)reader["Position"], (string)reader["Status"]);

                    //Console.WriteLine((string)reader["NAME"]);

                    listCoursesAndStaff.Add(courseAndStaff);
                }
            }
            else if (name.Length <= 1 && stuCount == 0000 && id != 0000)
            {
                var command = new SqlCommand($"SELECT courses.*, staff.Name as Staffer, staff.Title as Position, staff.Status from courses LEFT JOIN staff ON courses.teacher_id = staff.staff_id where courses.course_id = {id}", conn);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CourseAndStaff courseAndStaff = new CourseAndStaff(reader.GetInt32(0), (string)reader["NAME"], reader.GetInt32(2), reader.GetInt32(3), reader.GetDateTime(4), reader.GetDateTime(5), (string)reader["NOTES"], (string)reader["Staffer"], (string)reader["Position"], (string)reader["Status"]);

                    //Console.WriteLine((string)reader["NAME"]);

                    listCoursesAndStaff.Add(courseAndStaff);
                }
            }
            else
            {
                var command = new SqlCommand($"SELECT courses.*, staff.Name as Staffer, staff.Title as Position, staff.Status from courses LEFT JOIN staff ON courses.teacher_id = staff.staff_id where courses.course_id = {id} AND staff.name LIKE '%{name}%' AND courses.STU_Count >= {stuCount}", conn);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CourseAndStaff courseAndStaff = new CourseAndStaff(reader.GetInt32(0), (string)reader["NAME"], reader.GetInt32(2), reader.GetInt32(3), reader.GetDateTime(4), reader.GetDateTime(5), (string)reader["NOTES"], (string)reader["Staffer"], (string)reader["Position"], (string)reader["Status"]);

                    //Console.WriteLine((string)reader["NAME"]);

                    listCoursesAndStaff.Add(courseAndStaff);
                }
            }

            return listCoursesAndStaff;
        }


        //[HttpGet("{id:int}")] another way to do it

        /*[HttpGet]
        [Route("{id}")]
        public IEnumerable<Staff> Get(int id)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            var command = new SqlCommand($"SELECT * FROM Staff WHERE STAFF_ID={id}", conn);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Staff staffer = new Staff(reader.GetInt32(0), (string)reader["NAME"], (string)reader["TITLE"], (string)reader["STATUS"], (string)reader["NOTES"]);

                //Console.WriteLine((string)reader["NAME"]);

                listStaff.Add(staffer);
            }

            return listStaff;
        }*/


        /*[HttpGet]
        [Route("searcher/{name}")]
        public IEnumerable<Staff> Get(string name)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            var command = new SqlCommand($"SELECT * FROM Staff Where Name LIKE '%{name}%'", conn);
            using SqlDataReader reader = command.ExecuteReader(); 
            while (reader.Read())
            {
                Staff staffer = new Staff(reader.GetInt32(0), (string)reader["NAME"], (string)reader["TITLE"], (string)reader["STATUS"], (string)reader["NOTES"]);

                //Console.WriteLine((string)reader["NAME"]);

                listStaff.Add(staffer);
            }

            return listStaff;
        }*/
    }
}