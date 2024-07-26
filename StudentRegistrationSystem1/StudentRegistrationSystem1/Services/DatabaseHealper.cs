using SQLite;
using BCrypt.Net;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentRegistrationSystem1.Models;

namespace StudentRegistrationSystem1.Services
{



    public class DatabaseHelper : IDisposable
    {
        private SQLiteConnection _database;

        public DatabaseHelper()
        {
            try
            {
                // Get the local application data folder
                var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                // Combine the folder path with the database file name
                var dbPath = Path.Combine(folderPath, "AppDatabase.db3");

                Console.WriteLine($"Attempting to create or open the database at path: {dbPath}");

                if (!File.Exists(dbPath))
                {
                    Console.WriteLine("Database file does not exist. Creating a new one.");
                }

                _database = new SQLiteConnection(dbPath);
                _database.CreateTable<User>();

                Console.WriteLine("Database connection established and table created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing database: {ex.Message}");
                throw; // Re-throw the exception to indicate a failure during initialization
            }
        }

        public bool ValidateUser(string username, string password)
        {
            try
            {
                if (_database == null || !_database.TableMappings.Any(m => m.MappedType == typeof(User)))
                {
                    Console.WriteLine("Database connection or table mapping is invalid.");
                    return false;
                }

                var user = _database.Table<User>().FirstOrDefault(u => u.Username == username);

                if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validating user: {ex.Message}");
                return false;
            }
        }

        public void Insert(User user)
        {
            try
            {
                if (_database == null || !_database.TableMappings.Any(m => m.MappedType == typeof(User)))
                {
                    Console.WriteLine("Database connection or table mapping is invalid.");
                    return;
                }

                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
                _database.Insert(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting user: {ex.Message}");
            }
        }

        public void Dispose()
        {
            try
            {
                _database?.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error disposing database: {ex.Message}");
            }
        }
    }
}
