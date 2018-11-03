using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GuestBook.Models
{
    public class AdoRepository : IRepository
    {       
        string connectionString = System.Configuration.ConfigurationManager.
            ConnectionStrings["Default"].ConnectionString;

        public void Create(Record record)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var command = connection.CreateCommand();

                command.CommandText = "INSERT INTO Records (Text, Author, RecordDate)" +
                    @" VALUES (@Text, @Author, @RecordDate) ";

                command.Parameters.AddRange(new SqlParameter[] 
                {
                    new SqlParameter("@Text", record.Text),
                    new SqlParameter("@Author", record.Author),
                    new SqlParameter("@RecordDate", record.RecordDate)
                });

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var command = connection.CreateCommand();

                command.CommandText = "DELETE FROM Records WHERE Id = @id";

                command.Parameters.Add(new SqlParameter("@id", id));

                connection.Open();

                int res = command.ExecuteNonQuery();
            }
        }

        public Record Find(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var command = connection.CreateCommand();

                command.CommandText = "SELECT Id, Text, Author, RecordDate " +
                    "FROM Records WHERE Id = @id";

                command.Parameters.Add(new SqlParameter("@id", id));

                connection.Open();

                Record record = null;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        record = new Record
                        {
                            Id = reader.GetInt32(0),
                            Text = reader.GetString(1),
                            Author = reader.GetString(2),
                            RecordDate = reader.GetDateTime(3)
                        };
                    }
                }
                return record;
            }
        }

        public IEnumerable<Record> GetRecords()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var command = connection.CreateCommand();

                command.CommandText = "SELECT Id, Text, Author, RecordDate FROM Records";

                List<Record> records = new List<Record>();

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        records.Add(new Record()
                        {
                            Id = reader.GetInt32(0),
                            Text = reader.GetString(1),
                            Author = reader.GetString(2),
                            RecordDate = reader.GetDateTime(3)
                        });
                    }
                }
                return records;
            }
        }

        public void Update(Record record)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var command = connection.CreateCommand();

                command.CommandText = "UPDATE Records SET Text = @Text WHERE Id = @id";

                command.Parameters.Add(new SqlParameter("@Text", record.Text));
                command.Parameters.Add(new SqlParameter("@id", record.Id));

                connection.Open();

                command.ExecuteNonQuery();
            }
        }
    }
}