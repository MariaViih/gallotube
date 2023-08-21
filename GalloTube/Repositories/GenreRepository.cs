using System.Data;
using GalloFlix.Interfaces;
using GalloFlix.Models;
using MySql.Data.MySqlClient;

namespace GalloFlix.Repositories;

public class GenreRepository : IGenreRepository
{
    readonly string connectionString = "server=localhost;port=3306;database=GalloFlixdb;uid=root;pwd=''";

    public void Create(Genre model)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "insert into Genre(Name) values (@Name)";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@Name", model.Name);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void Delete(int? id)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "delete from Genre where Id = @Id";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@Id", id);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public List<Genre> ReadAll()
    {
        MySqlConnection connection = new(connectionString);
        string sql = "select * from Genre";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        
        List<Genre> genres = new();
        connection.Open();
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Genre genre = new()
            {
                Id = reader.GetByte("id"),
                Name = reader.GetString("name")
            };
            genres.Add(genre);
        }
        connection.Close();
        return genres;
    }

    public Genre ReadById(int? id)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "select * from Genre where Id = @Id";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@Id", id);
        
        connection.Open();
        MySqlDataReader reader = command.ExecuteReader();
        reader.Read();
        if (reader.HasRows)
        {
            Genre genre = new()
            {
                Id = reader.GetByte("id"),
                Name = reader.GetString("name")
            };
            connection.Close();
            return genre;
        }
        connection.Close();
        return null;
    }

    public void Update(Genre model)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "update Genre set Name = @Name where Id = @Id";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@Id", model.Id);
        command.Parameters.AddWithValue("@Name", model.Name);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }
}