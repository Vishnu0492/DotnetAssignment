using System;
using System.Data.SqlClient;
namespace Milestone5
{
    internal class Program
    {
        public class User
        {
            public int Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
        }
        static void Main(string[] args)
        {
            // Connection string to the database
            string connectionString = "Data Source=.;Initial Catalog=SampleDB;Integrated Security=True";

            // SQL query to retrieve data
            string query = "SELECT Id, UserName, Email FROM Users";

            // List to store the retrieved users
            List<User> users = new List<User>();

            try
            {
                //  Establish a connection to the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Execute the command
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Read the data and bind it to the User list
                            while (reader.Read())
                            {
                                User user = new User
                                {
                                    Id = reader.GetInt32(0),
                                    UserName = reader.GetString(1),
                                    Email = reader.GetString(2)
                                };
                                users.Add(user);
                            }
                        }
                    }
                }

                //  Display the data in a formatted manner
                foreach (var user in users)
                {
                    Console.WriteLine($"ID: {user.Id}, Name: {user.UserName}, Email: {user.Email}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}