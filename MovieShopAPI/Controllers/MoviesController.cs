using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        
        [HttpGet]
        //Traditional Routing
        //http://localhost/movies/GetTopGrossingMovies
        // Attribute routing
        // http://localhost/api/movies/top-grossing
        [Route("top-grossing")]
        public async Task<IActionResult> GetTopGrossingMovies()
        {
           //call my service 
           var movies = await _movieService.GetTop30GrossingMovies();
           // Return movie information in JSON format
           //ASP.NET Core automatically serialize C# object to JSON Objects
           //system.text.json .NEt 3
           //
           // Return data(Json format) along with it return Http status code
           if (movies == null || !movies.Any())
           {
               return NotFound(new { errorMessage = "No Movies Found" });
           }

           return Ok(movies );
           
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);
            if (movie == null)
            {
                return NotFound(new { errorMessage = $"No Movie Found for {id}" });
            }
            return Ok(movie);
        }
    }
}
