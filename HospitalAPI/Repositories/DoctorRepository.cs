using Dapper;
using HospitalAPI.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalAPI.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {

        private readonly string _connectionString;

        public DoctorRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<Doctor> Create(Doctor doctor)
        {
            var sqlQuery = "INSERT INTO Doctors (Name, Surname) VALUES (@Name, @Surname)";


            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new
                {
                    doctor.Name,
                    doctor.Surname                    
                });

                return doctor;
            }
        }

        public async Task Delete(int id)
        {
            var sqlQuery = "DELETE FROM Doctors WHERE Id = @Id";

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new { Id = id });               
            }
        }

        public async Task<IEnumerable<Doctor>> Get()
        {
            var sqlQuery = "SELECT * FROM Doctors";
            

            using (var connection = new SqliteConnection(_connectionString))
            {
                return await connection.QueryAsync<Doctor>(sqlQuery);
            }
            
        }

        public async Task<Doctor> Get(int id)
        {
            var sqlQuery = "SELECT * FROM Doctors WHERE Id = @DoctorId";


            using (var connection = new SqliteConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Doctor>(sqlQuery, new { DoctorId = id});
            }
        }

        public async Task Update(Doctor doctor)
        {
            var sqlQuery = "UPDATE Doctors SET Name = @Name, Surname = @Surname WHERE Id = @Id";


            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new
                {
                    doctor.Id,
                    doctor.Name,
                    doctor.Surname                    
                });

               
            }
        }

    }
}
