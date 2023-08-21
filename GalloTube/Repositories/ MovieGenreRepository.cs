using System.Data;
using GalloFlix.Interfaces;
using GalloFlix.Models;
using MySql.Data.MySqlClient;

namespace GalloFlix.Repositories;

public class MovieGenreRepository : IMovieGenreRepository
{
    readonly string connectionString = "server=localhost;port=3306;database=GalloFlixdb;uid=root;pwd=''";

    public void Create(int MovieId, byte GenreId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "insert into MovieGenre(MovieId, GenreId) values (@MovieId, @GenreId)";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@MovieId", MovieId);
        command.Parameters.AddWithValue("@GenreId", GenreId);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void Delete(int MovieId, byte GenreId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "delete from MovieGenre where MovieId = @MovieId and GenreId = @GenreId";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@MovieId", MovieId);
        command.Parameters.AddWithValue("@GenreId", GenreId);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void Delete(int MovieId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "delete from MovieGenre where MovieId = @MovieId";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@MovieId", MovieId);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public List<Genre> ReadGenresByMovie(int MovieId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "select * from genre where id in "
                   + "(select GenreId from MovieGenre where MovieId = @MovieId)";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@MovieId", MovieId);
        
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

    public List<MovieGenre> ReadMovieGenre()
    {
        MySqlConnection connection = new(connectionString);
        string sql = "select * from MovieGenre";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        
        List<MovieGenre> movieGenres = new();
        connection.Open();
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            MovieGenre movieGenre = new()
            {
                MovieId = reader.GetInt32("MovieId"),
                GenreId = reader.GetByte("GenreId")
            };
            movieGenres.Add(movieGenre);
        }
        connection.Close();
        return movieGenres;
    }

    public List<Movie> ReadMoviesByGenre(byte GenreId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "select * from movie where id in "
                   + "(select MovieId from moviegenre where GenreId = @GenreId)";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@GenreId", GenreId);
        
        List<Movie> movies = new();
        connection.Open();
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Movie movie = new()
            {
                Id = reader.GetInt32("id"),
                Title = reader.GetString("title"),
                OriginalTitle = reader.GetString("originalTitle"),
                Synopsis = reader.GetString("synopsis"),
                MovieYear = reader.GetInt16("movieYear"),
                Duration = reader.GetInt16("duration"),
                AgeRating = reader.GetByte("ageRating"),
                Image = reader.GetString("image")
            };
            movies.Add(movie);
        }
        connection.Close();
        return movies;
    }
}