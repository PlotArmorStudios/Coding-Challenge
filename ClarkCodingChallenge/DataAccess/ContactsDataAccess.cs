using ClarkCodingChallenge.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClarkCodingChallenge.DataAccess
{
    /// <summary>
    /// Set up of all CRUD operations applicable to this database.
    /// Prioritized set up of C and R operations to match the requirements of this project
    /// For a full application, U and D operations would also be implemented
    /// This applies to the IRepository implementation as well
    /// </summary>
    public class ContactsDataAccess : IDatabase<Contact>
    {
        public IConfiguration Configuration { get; }
        public string ConnectionString { get; }

        public ContactsDataAccess(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("ContactContext");
        }

        public async Task AddAsync(Contact contact)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "INSERT INTO Contact (FirstName, LastName, Email) " +
                       "VALUES (@FirstName, @LastName, @Email)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                    command.Parameters.AddWithValue("@LastName", contact.LastName);
                    command.Parameters.AddWithValue("@Email", contact.Email);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }

                await connection.CloseAsync();
            }
        }

        public async Task<IEnumerable<Contact>> GetSelectedAsync(string lastName, string sortOrder)
        {
            IEnumerable<Contact> contacts = await RunQuery(
                "SELECT Contact_ID, FirstName, LastName, Email " +
            "FROM Contact");

            if (!string.IsNullOrEmpty(lastName))
            {
                contacts = contacts.Where(m => m.LastName == lastName);
            }

            if (sortOrder.ToLower() == "asc")
                contacts = contacts.OrderBy(m => m.LastName).ThenBy(m => m.FirstName);
            if (sortOrder.ToLower() == "desc")
                contacts = contacts.OrderBy(m => m.LastName).ThenByDescending(m => m.FirstName);

            return contacts;
        }

        private async Task<IEnumerable<Contact>> RunQuery(string query)
        {
            List<Contact> contacts = new List<Contact>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    await command.Connection.OpenAsync();

                    using (SqlDataReader sqlDataReader = await command.ExecuteReaderAsync())
                    {
                        while (await sqlDataReader.ReadAsync())
                        {
                            contacts.Add(new Contact
                            {
                                Id = Convert.ToInt32(sqlDataReader["Contact_ID"]),
                                FirstName = sqlDataReader["FirstName"].ToString(),
                                LastName = sqlDataReader["LastName"].ToString(),
                                Email = sqlDataReader["Email"].ToString()
                            });
                        }
                    }
                    await command.Connection.CloseAsync();
                }
            }

            return contacts;
        }
    }
}
