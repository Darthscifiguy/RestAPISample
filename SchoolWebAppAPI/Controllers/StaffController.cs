using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using SchoolWebAppAPI.Models;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Net.NetworkInformation;
using WebAppAPITest1.Models;
using static System.Net.WebRequestMethods;

namespace WebAppAPITest1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly string connectionString = WebAppConst.CONSTRING;
        private readonly ILogger<StaffController> _logger;
        private List<Staff> listStaff = new List<Staff>();

        public StaffController(ILogger<StaffController> logger)
        {
            _logger = logger;

        }

        //Gets all staff
        [HttpGet(Name = "GetStaff")]
        public async Task<IActionResult> Get()
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();


                var command = new SqlCommand("Exec GetStaffFull ''", conn);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Staff staffer = new Staff(reader.GetInt32(0), (string)reader["NAME"], (string)reader["TITLE"], (string)reader["STATUS"], (string)reader["NOTES"], reader.GetDateTime(5), reader.GetBoolean(6));

                    //Console.WriteLine((string)reader["NAME"]);

                    listStaff.Add(staffer);
                }
                return Ok(listStaff);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } 
        }

        //Gets a staff by name with custom pathway
        [HttpGet]
        [Route("searcher/{name}")]
        public async Task<IActionResult> Get(string name)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();

                var command = new SqlCommand($"Exec GetStaffFull '{name}'", conn);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Staff staffer = new Staff(reader.GetInt32(0), (string)reader["NAME"], (string)reader["TITLE"], (string)reader["STATUS"], (string)reader["NOTES"], reader.GetDateTime(5), reader.GetBoolean(6));

                    //Console.WriteLine((string)reader["NAME"]);

                    listStaff.Add(staffer);
                }

                return Ok(listStaff);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }  
        }

        //Creates new record for staff table
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StaffInsert staffer)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();

                string insertQuery = "Exec InsertStaffTran @Name, @Title, @Status, @Notes";
                SqlCommand command = new SqlCommand(insertQuery, conn);
                string name = staffer.Name;
                string title = staffer.Title;
                string status = staffer.Status;
                string notes = staffer.Notes;


                if (name != null && title != null && status != null && notes != null)
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@Notes", notes);

                    try
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Record Inserted Successfully");
                        staffer.Result = "Staff successfully added.";
                        return Ok(staffer);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }

                }
                else if (name != null && title != null && status != null)
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@Notes", "");

                    try
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Record Inserted Successfully");
                        staffer.Result = "Staff successfully added.";
                        return Ok(staffer);
                    }
                    catch (Exception ex)
                    {

                        return BadRequest(ex.Message);
                    }
                }

                else
                {
                    return BadRequest("Conditions for Insert failed. Aborting procedure.");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Updates a record in staff table
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Staff staffer)
        {

            using var conn = new SqlConnection(connectionString);
            conn.Open();

            string updateQuery = "Exec UpdateStaffTran @Id, @Name, @Title, @Status, @Notes";
            SqlCommand command = new SqlCommand(updateQuery, conn);

            int id = staffer.Id;
            string name = staffer.Name;
            string title = staffer.Title;
            string status = staffer.Status;
            string notes = staffer.Notes;

            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Title", title);
            command.Parameters.AddWithValue("@Status", status);
            command.Parameters.AddWithValue("@Notes", notes);

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Record Inserted Successfully");
                staffer.Result = "Staff successfully modified.";
                return Ok(staffer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //Modifies deleted flag to hide record from user
        [HttpPut]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] Staff staffer)
        {

            using var conn = new SqlConnection(connectionString);
            conn.Open();

            string updateQuery = "Exec DeleteStaffTran @Id";
            SqlCommand command = new SqlCommand(updateQuery, conn);

            int id = staffer.Id;

            command.Parameters.AddWithValue("@Id", id);

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Record Deleted Successfully");
                staffer.Result = "Staff successfully deleted.";
                return Ok(staffer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}