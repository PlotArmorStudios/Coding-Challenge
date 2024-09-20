using ClarkCodingChallenge.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ClarkCodingChallenge.DataAccess
{
    public class ContactsDataAccess : IDatabase<Contact>
    {
        public string ConnectionString { get; }

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

        public Task<IEnumerable<Contact>> GetSelectedAsync(string lastName, string sortOrder)
        {
            throw new System.NotImplementedException();
        }
    }
}
