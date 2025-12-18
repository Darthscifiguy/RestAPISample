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
    public class StaffController : ControllerBase
    {
        private readonly StaffTestDB db = new StaffTestDB();
        private readonly string connectionString = WebAppConst.CONSTRING;
        private readonly ILogger<StaffController> _logger;
        private List<Staff> listStaff = new List<Staff>();

        public StaffController(ILogger<StaffController> logger)
        {
            _logger = logger;

            /*if(Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING") != null)
            {
                connectionString = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
            }*/

        }

        [HttpGet(Name = "GetStaff")]
        public IEnumerable<Staff> Get()
        {

            using var conn = new SqlConnection(connectionString);
            conn.Open();

            var command = new SqlCommand("SELECT * FROM Staff", conn);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Staff staffer = new Staff(reader.GetInt32(0), (string)reader["NAME"], (string)reader["TITLE"], (string)reader["STATUS"], (string)reader["NOTES"]);

                //Console.WriteLine((string)reader["NAME"]);

                listStaff.Add(staffer);
            }

            //for local testing
            //return db.GetAllStaffs();

            return listStaff;
        }

        //[HttpGet("{id:int}")] another way to do it

        [HttpGet]
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
        }


        [HttpGet]
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
        }
    }
}