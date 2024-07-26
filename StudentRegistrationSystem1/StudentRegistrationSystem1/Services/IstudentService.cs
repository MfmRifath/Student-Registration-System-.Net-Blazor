using StudentRegistrationSystem1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationSystem1.Services
{
    public interface IstudentService
    {
        Task<List<StudentModel>> GetAllStudent();
        Task<StudentModel> GetStudentByID(int StudentID);
        Task<int> AddStudent(StudentModel studentModel);
        Task<int> UpdateStudent(StudentModel studentModel);
        Task<int> DeleteStudent(StudentModel studentModel); 
    }
}
