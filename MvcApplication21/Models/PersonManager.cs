using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MvcApplication21.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    public class PersonManager
    {
        private string _connectionString;

        public PersonManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Person> GetPeople()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM People";
                connection.Open();
                List<Person> people = new List<Person>();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Person p = new Person();
                    p.Id = (int)reader["Id"];
                    p.FirstName = (string)reader["FirstName"];
                    p.LastName = (string)reader["LastName"];
                    p.Age = (int)reader["Age"];
                    people.Add(p);
                }

                return people;
            }
        }

        public Person GetPerson(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM People WHERE Id = @id";
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                var reader = command.ExecuteReader();
                reader.Read();
                return new Person
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]
                };
            }
        }

        public void Add(Person person)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO People (FirstName, LastName, Age) " +
                                      "Values (@firstName, @lastName, @age)";
                command.Parameters.AddWithValue("@firstName", person.FirstName);
                command.Parameters.AddWithValue("@lastName", person.LastName);
                command.Parameters.AddWithValue("@age", person.Age);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int personId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM People WHERE Id = @personId";
                command.Parameters.AddWithValue("@personId", personId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Edit(Person person)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE People SET FirstName = @firstName, LastName = @lastName, Age = @age " +
                                      "WHERE Id = @id";
                command.Parameters.AddWithValue("@firstName", person.FirstName);
                command.Parameters.AddWithValue("@lastName", person.LastName);
                command.Parameters.AddWithValue("@age", person.Age);
                command.Parameters.AddWithValue("@id", person.Id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}