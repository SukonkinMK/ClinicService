using ClinicService.Models;
using Microsoft.Data.Sqlite;

namespace ClinicService.Services.Implementation
{
    public class PetRepository : IPetRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public PetRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("sqlite");
        }

        public int Create(Pet entity)
        {
            using SqliteConnection connection = new SqliteConnection(_connectionString);
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Pets(ClientId, Name, Birthday) VALUES(@ClientId, @Name, @Birthday)";
            command.Parameters.AddWithValue("@ClientId", entity.ClientId);
            command.Parameters.AddWithValue("@Name", entity.Name);
            command.Parameters.AddWithValue("@Birthday", entity.Birthday.Ticks);
            command.Prepare();
            return command.ExecuteNonQuery();
        }

        public int Delete(int id)
        {
            using SqliteConnection connection = new SqliteConnection(_connectionString);
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Pets WHERE PetId = @PetId";
            command.Parameters.AddWithValue("@PetId", id);
            command.Prepare();
            return command.ExecuteNonQuery();
        }

        public List<Pet> GetAll()
        {
            List<Pet> list = new();

            using SqliteConnection connection = new SqliteConnection(_connectionString);
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Pets";
            SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Pet pet = new Pet
                {
                    PetId = reader.GetInt32(0),
                    ClientId = reader.GetInt32(1),
                    Name = reader.GetString(2),                    
                    Birthday = new DateTime(reader.GetInt64(3))
                };
                list.Add(pet);
            }
            return list;
        }

        public Pet GetById(int id)
        {
            using SqliteConnection connection = new SqliteConnection(_connectionString);
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Pets WHERE PetId = @PetId";
            command.Parameters.AddWithValue("@PetId", id);
            command.Prepare();
            SqliteDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Pet pet = new Pet
                {
                    PetId = reader.GetInt32(0),
                    ClientId = reader.GetInt32(1),
                    Name = reader.GetString(2),
                    Birthday = new DateTime(reader.GetInt64(3))
                };
                return pet;
            }
            return null;
        }

        public int Update(Pet entity)
        {
            using SqliteConnection connection = new SqliteConnection(_connectionString);
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE Pets SET ClientId = @ClientId, Name = @Name, Birthday = @Birthday WHERE PetId = @PetId";
            command.Parameters.AddWithValue("@PetId", entity.PetId);
            command.Parameters.AddWithValue("@ClientId", entity.ClientId);
            command.Parameters.AddWithValue("@Name", entity.Name);
            command.Parameters.AddWithValue("@Birthday", entity.Birthday.Ticks);
            command.Prepare();
            return command.ExecuteNonQuery();
        }
    }
}
