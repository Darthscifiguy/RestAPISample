using System;
using WebAppAPITest1.Models;

namespace WebAppAPITest1
{
    public class StaffTestDB
    {
        public List <Staff> staffs = new List<Staff>();

        public StaffTestDB()
        {
            staffs.Add(new Staff(2000, "Smith, John", "Professor", "Active", "Workshop 7 / 17"));
            staffs.Add(new Staff(2001, "Jones, Mark", "Asst Prof", "Active", "Workshop 3/18"));
        }


        public List<Staff> GetAllStaffs()
        {
            return staffs;
        }


    }
}
