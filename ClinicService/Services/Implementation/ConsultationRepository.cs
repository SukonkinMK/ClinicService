using ClinicService.Models;
using Microsoft.Data.Sqlite;

namespace ClinicService.Services.Implementation
{
    public class ConsultationRepository : IConsultationRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ConsultationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("sqlite");
        }

        public int Create(Consultation entity)
        {   
            using SqliteConnection connection = new SqliteConnection(_connectionString);
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Consultations(ClientId, PetId, ConsultationDate, Description) VALUES(@ClientId, @PetId, @ConsultationDate, @Description)";
            command.Parameters.AddWithValue("@ClientId", entity.ClientId);
            command.Parameters.AddWithValue("@PetId", entity.PetId);
            command.Parameters.AddWithValue("@ConsultationDate", entity.ConsultationDate.Ticks);
            command.Parameters.AddWithValue("@Description", entity.Description);
            command.Prepare();
            return command.ExecuteNonQuery();
        }

        public int Delete(int id)
        {
            using SqliteConnection connection = new SqliteConnection(_connectionString);
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Consultations WHERE ConsultationId = @ConsultationId";
            command.Parameters.AddWithValue("@ConsultationId", id);
            command.Prepare();
            return command.ExecuteNonQuery();
        }

        public List<Consultation> GetAll()
        {
            List<Consultation> list = new();

            using SqliteConnection connection = new SqliteConnection(_connectionString);
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Consultations";
            SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Consultation consultation = new Consultation
                {
                    ConsultationId = reader.GetInt32(0),                    
                    ClientId = reader.GetInt32(1),
                    PetId = reader.GetInt32(2),
                    ConsultationDate = new DateTime(reader.GetInt64(3)),
                    Description = reader.GetString(4)                    
                };
                list.Add(consultation);
            }
            return list;
        }

        public Consultation GetById(int id)
        {
            using SqliteConnection connection = new SqliteConnection(_connectionString);
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Consultations WHERE ConsultationId = @ConsultationId";
            command.Parameters.AddWithValue("@ConsultationId", id);
            command.Prepare();
            SqliteDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Consultation consultation = new Consultation
                {
                    ConsultationId = reader.GetInt32(0),
                    ClientId = reader.GetInt32(1),
                    PetId = reader.GetInt32(2),
                    ConsultationDate = new DateTime(reader.GetInt64(3)),
                    Description = reader.GetString(4)
                };
                return consultation;
            }
            return null;
        }

        public int Update(Consultation entity)
        {
            using SqliteConnection connection = new SqliteConnection(_connectionString);
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE Consultations SET ClientId = @ClientId, PetId = @PetId, ConsultationDate = @ConsultationDate, Description = @Description WHERE ConsultationId = @ConsultationId";
            command.Parameters.AddWithValue("@ConsultationId", entity.ConsultationId);
            command.Parameters.AddWithValue("@ClientId", entity.ClientId);
            command.Parameters.AddWithValue("@PetId", entity.PetId);
            command.Parameters.AddWithValue("@ConsultationDate", entity.ConsultationDate.Ticks);
            command.Parameters.AddWithValue("@Description", entity.Description);
            command.Prepare();
            return command.ExecuteNonQuery();
        }
    }
}
