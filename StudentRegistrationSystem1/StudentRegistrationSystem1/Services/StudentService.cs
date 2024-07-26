using SQLite;
using StudentRegistrationSystem1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationSystem1.Services
{
    public class StudentService : IstudentService
    {
        private SQLiteAsyncConnection _dbConnection;
        public StudentService()
        {
            setUpDb();
        }
        private async void setUpDb()
        {
            if (_dbConnection == null) 
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Student.db3");
                _dbConnection = new SQLiteAsyncConnection(dbPath);
                await _dbConnection.CreateTableAsync<StudentModel>();
            }
        }
        public async Task<int> AddStudent(StudentModel studentModel)
        {
           return await _dbConnection.InsertAsync(studentModel);
        }

        public Task<int> DeleteStudent(StudentModel studentModel)
        {
           return _dbConnection.DeleteAsync(studentModel);
        }

        public async Task<List<StudentModel>> GetAllStudent()
        {
          return await _dbConnection.Table<StudentModel>().ToListAsync();
        }

        public async Task<int> UpdateStudent(StudentModel studentModel)
        {
            return await _dbConnection.UpdateAsync(studentModel);
        }
        public async Task<StudentModel>GetStudentByID(int StudentID)
        {
            var student = await _dbConnection.QueryAsync<StudentModel>($"Select * From {nameof(StudentModel)} where StudentID={StudentID}");
           return student.FirstOrDefault();
        }
    }
}
