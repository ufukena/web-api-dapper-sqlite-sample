using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace HospitalAPI.Database
{
    public class DbConfig : IDbConfig
    {
        private readonly string _connectionString;

        public DbConfig(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public void CheckDb()
        {

            using var connection = new SqliteConnection(_connectionString);

            //Check Table exists
            var tableName = connection.QueryFirstOrDefault<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'Doctors';");

            if (!string.IsNullOrEmpty(tableName))
                return;

            var sqlQuery = "CREATE TABLE Doctors (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name VARCHAR(50) NOT NULL, SurName VARCHAR(50) NOT NULL)";

            connection.ExecuteAsync(sqlQuery);


        }

    }


}
