using ClinicService.Models;
using Microsoft.Data.Sqlite;

namespace ClinicService.Services
{
    public class ClientRepository : IClientRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ClientRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("sqlite");
        }
         
        public int Create(Client entity)
        {
            using SqliteConnection connection = new SqliteConnection(_connectionString);
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Clients(Document, SurName, FirstName, Patronymic, Birthday) VALUES(@Document, @SurName, @FirstName, @Patronymic, @Birthday)";
            command.Parameters.AddWithValue("@Document", entity.Document);
            command.Parameters.AddWithValue("@SurName", entity.SurName);
            command.Parameters.AddWithValue("@FirstName", entity.FirstName);
            command.Parameters.AddWithValue("@Patronymic", entity.Patronymic);
            command.Parameters.AddWithValue("@Birthday", entity.Birthday.Ticks);
            command.Prepare();
            return command.ExecuteNonQuery();
        }

        public int Delete(int id)
        {
            using SqliteConnection connection = new SqliteConnection(_connectionString);
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Clients WHERE ClientId = @ClientId";
            command.Parameters.AddWithValue("@ClientId", id);
            command.Prepare();
            return command.ExecuteNonQuery();
        }

        public List<Client> GetAll()
        {
            List<Client> list = new();

            using SqliteConnection connection = new SqliteConnection(_connectionString);
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Clients";
            SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Client client = new Client
                {
                    ClientId = reader.GetInt32(0),
                    Document = reader.GetString(1),
                    SurName = reader.GetString(2),
                    FirstName = reader.GetString(3),
                    Patronymic = reader.GetString(4),
                    Birthday = new DateTime(reader.GetInt64(5))
                };
                list.Add(client);
            }
            return list;
        }

        public Client GetById(int id)
        {
            using SqliteConnection connection = new SqliteConnection(_connectionString);
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Clients WHERE ClientId = @ClientId";
            command.Parameters.AddWithValue("@ClientId", id);
            command.Prepare();
            SqliteDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Client client = new Client
                {
                    ClientId = reader.GetInt32(0),
                    Document = reader.GetString(1),
                    SurName = reader.GetString(2),
                    FirstName = reader.GetString(3),
                    Patronymic = reader.GetString(4),
                    Birthday = new DateTime(reader.GetInt64(5))
                };
                return client;
            }
            return null;
        }

        public int Update(Client entity)
        {
            using SqliteConnection connection = new SqliteConnection(_connectionString);
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE Clients SET Document = @Document, SurName = @SurName, FirstName = @FirstName, Patronymic = @Patronymic, Birthday = @Birthday WHERE ClientId = @ClientId";
            command.Parameters.AddWithValue("@ClientId", entity.ClientId);
            command.Parameters.AddWithValue("@Document", entity.Document);
            command.Parameters.AddWithValue("@SurName", entity.SurName);
            command.Parameters.AddWithValue("@FirstName", entity.FirstName);
            command.Parameters.AddWithValue("@Patronymic", entity.Patronymic);
            command.Parameters.AddWithValue("@Birthday", entity.Birthday.Ticks);
            command.Prepare();
            return command.ExecuteNonQuery();
        }
    }
}
