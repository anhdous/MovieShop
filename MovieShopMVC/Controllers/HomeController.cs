using System.Diagnostics;
using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;

namespace MovieShopMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMovieService _movieService;

    public HomeController(ILogger<HomeController> logger, IMovieService movieService)
    {
        _logger = logger;
        _movieService = movieService;
    }

    public async Task<IActionResult> Index()
    {
        //Go to database and get the data
        // tightly coupled code
        // loosely coupled code
        // 
        var movies = await _movieService.GetTop30GrossingMovies();

        // 3 ways we can send the data from controller/ action methods to View
        // ViewBag
        // ViewData
        // Strongly Typed Models (Recommended)
        return View(movies);
    }
    
    
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}