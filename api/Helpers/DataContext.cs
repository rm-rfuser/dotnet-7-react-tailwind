namespace WebApi.Helpers;

using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;

public class DataContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IDbConnection CreateConnection()
    {
        return new SqliteConnection(Configuration.GetConnectionString("WebApiDatabase"));
    }

    public async Task Init()
    {
        // create database tables if they don't exist
        using var connection = CreateConnection();
        await _initArticles();

        async Task _initArticles()
        {
            var sql = """
                CREATE TABLE IF NOT EXISTS 
                Article (
                    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    Title TEXT,
                    Description TEXT,
                    ImageUrl TEXT,
                    [Type] INTEGER
                );
            """;
            await connection.ExecuteAsync(sql);
        }
    }
}