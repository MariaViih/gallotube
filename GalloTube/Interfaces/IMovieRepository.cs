using GalloFlix.Models;

namespace GalloFlix.Interfaces;

public interface IMovieRepository : IRepository<Movie>
{
    List<Movie> ReadAllDetailed();

    Movie ReadByIdDetailed(int id);
}
