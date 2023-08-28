using System.Data;
using GalloTube.Interfaces;
using GalloTube.Models;
using MySql.Data.MySqlClient;

namespace GalloTube.Repositories;

public class VideoTagRepository : IVideoTagRepository
{
    readonly string connectionString = "server=localhost;port=3306;database=GalloFlixdb;uid=root;pwd=''";

    public void Create(int VideoId, byte TagId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "insert into VideoTag(MovieId, TagId) values (@VideoId, @TagId)";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@VideoId", MovieId);
        command.Parameters.AddWithValue("@TagId", GenreId);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void Delete(int VideoId, byte TagId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "delete from VideoTag where VideoId = @VideoId and TagId = @TagId";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@VideoId", VideoId);
        command.Parameters.AddWithValue("@TagId", TagId);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void Delete(int VideoId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "delete from MovieGenre where VideoId = @VideoId";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@VideoId", VideoId);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public List<Genre> ReadGenresByMovie(int VideoId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "select * from tag where id in "
                   + "(select TagId from VideoTag where VideoId = @VideoId)";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@VideoId", MovieId);
        
        List<Tag> tags = new();
        connection.Open();
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Tag tag= new()
            {
                Id = reader.GetByte("id"),
                Name = reader.GetString("name")
            };
            tags.Add(genre);
        }
        connection.Close();
        return tags;
    }

    public List<VideoTag> ReadVideoTag()
    {
        MySqlConnection connection = new(connectionString);
        string sql = "select * from VideoTag";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        
        List<VideoTag> videoTags = new();
        connection.Open();
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            VideoTag videoTag = new()
            {
                VideoId = reader.GetInt32("MovieId"),
                TagId = reader.GetByte("GenreId")
            };
            videoTags.Add(videoTag);
        }
        connection.Close();
        return videoTags;
    }

    public List<Video> ReadVideosByTag(byte TagId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "select * from movie where id in "
                   + "(select MovieId from moviegenre where GenreId = @GenreId)";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@GenreId", GenreId);
        
        List<Video> videos = new();
        connection.Open();
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Video video = new()
            {
                Id = reader.GetInt32("id"),
                Title = reader.GetString("title"),
                OriginalTitle = reader.GetString("originalTitle"),
                Synopsis = reader.GetString("synopsis"),
                VideoYear = reader.GetInt16("videoYear"),
                Duration = reader.GetInt16("duration"),
                AgeRating = reader.GetByte("ageRating"),
                Image = reader.GetString("image")
            };
            videos.Add(video);
        }
        connection.Close();
        return videos;
    }
}