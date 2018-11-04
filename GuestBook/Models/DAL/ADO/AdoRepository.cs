using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;

namespace GuestBook.Models
{
    public class AdoRepository : IRepository
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Default"]
                                .ConnectionString;
        private static IEnumerable<Record> _records;

        public IEnumerable<Record> GetRecords()
        {
            if (_records == null)
                _records = ReadAll();
            return _records;
        }

        private IEnumerable<Record> ReadAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT Id, Text, Author, CreationDate, UpdationDate FROM Records";
                List<Record> records = new List<Record>();
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        records.Add(new Record
                        {
                            Id = reader.GetInt32(0),
                            Text = reader.GetString(1),
                            Author = reader.GetString(2),
                            CreationDate = reader.GetDateTime(3),
                            UpdationDate = reader.GetDateTime(4)
                        });                        
                    }
                }
                return records;
            }
        }

        public Record Find(int id)
        {
            return GetRecords().FirstOrDefault(r => r.Id == id);            
        }

        public void Create(Record record)
        {
            string commandText = "INSERT INTO Records (Text, Author, CreationDate, UpdationDate)" +
                " VALUES (@text, @author, @creationDate, @updDate) ";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@text", record.Text),
                new SqlParameter("@author", record.Author),
                new SqlParameter("@creationDate", record.CreationDate),
                new SqlParameter("@updDate", record.CreationDate)
            };
            NonQueryOperations(commandText, parameters);
        }

        public void Update(Record record)
        {
            string commandText = "UPDATE Records SET Text = @text, " +
                "UpdationDate = @updDate WHERE Id = @id";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@text", record.Text),
                new SqlParameter("@id", record.Id),
                new SqlParameter("@updDate", record.UpdationDate)
            };
            NonQueryOperations(commandText, parameters);
        }

        public void Delete(int id)
        {
            string commandText = "DELETE FROM Records WHERE Id = @id";
            var parameters = new SqlParameter[] { new SqlParameter("@id", id) };
            NonQueryOperations(commandText, parameters);
        }

        private void NonQueryOperations(string commandText, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.AddRange(parameters);
                connection.Open();
                object locker = new object();
                lock (locker)
                {
                    if (command.ExecuteNonQuery() > 0)
                    {
                        _records = ReadAll();
                    }
                }
            }
        }
    }
}