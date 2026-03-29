using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebAppAPITest1.Models;


namespace WebAppAPITest1.Controllers
{
    //Intial tester with local DB. Not in use for Azure webapp.
    [ApiController]
    [Route("[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly StaffTestDB db = new StaffTestDB();
        private readonly ILogger<PeopleController> _logger;

        public PeopleController(ILogger<PeopleController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetPeople")]
        public IEnumerable<Staff> Get()
        {
            //for local testing
            return db.GetAllStaffs();
        }
    }
}