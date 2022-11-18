using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers;

public class MoviesController : Controller
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        // Go to MovieService => MovieRepository and get movie details from Movies Table
        // Go to database and get the movie information by
        // movie id and send the data(Model) to the view
        // ADO.NET
        // Dapper Stack Overflow -> Micro ORM
        // Entity Framework Core -> Full ORM
        var movieDetails = await _movieService.GetMovieDetails(id);
        return View(movieDetails);
    }
    
    public async Task<IActionResult> GenreMovies( int id, int pageSize = 30, int page = 1)
    {
        var pagedMovies = await _movieService.GetMoviesByPagination(id, pageSize, page);
        return View(pagedMovies);
    }
}