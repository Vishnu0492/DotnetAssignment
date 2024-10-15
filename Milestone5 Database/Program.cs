using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace Milestone5_Database
{
    internal class Program
    {
        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }
        static void Main(string[] args)
        {
            

        // Connection string to the database
        string connectionString = "Data Source=.;Initial Catalog=SampleDB;Integrated Security=True";
            // List to store the retrieved users
            List<User> users = new List<User>();
            //Establish a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // SQL query to retrieve data
                string query = "SELECT * FROM Users";
                // Execute the command
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    //Read the data and bind it to the User list
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Email = reader.GetString(2)
                            };
                            users.Add(user);
                        }
                    }
                }
            }
            //Display the data in a formatted manner
            foreach (var user in users)
            {
                Console.WriteLine($"ID:{user.Id},Name:{user.Name},Email:{user.Email}");

            }
        }
    }
}
