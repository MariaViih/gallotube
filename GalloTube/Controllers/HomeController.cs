using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GalloFlix.Models;
using GalloFlix.Interfaces;


namespace GalloFlix.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMovieRepository _movieRepository;

    public HomeController(ILogger<HomeController> logger, IMovieRepository movieRepository)
    {
        _logger = logger;
        _movieRepository = movieRepository;
    }

    public IActionResult Index()
    {
        var movies = _movieRepository.ReadAllDetailed();
        return View(movies);
    }

    public IActionResult Movie(int id)
    {
        var movie  = _movieRepository.ReadByIdDetailed(id);
        return View(movie);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        _logger.LogError("Ocorreu um erro");
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
