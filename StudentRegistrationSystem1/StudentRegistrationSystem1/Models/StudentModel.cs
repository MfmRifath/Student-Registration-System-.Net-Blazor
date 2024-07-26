using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationSystem1.Models
{
    public class StudentModel
    {
        [PrimaryKey,AutoIncrement]
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        

        public string LastName { get; set; }
        public string Email { get; set; }   
        public string Gender { get; set; }
        public string Course { get; set; }

    }
}
